using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.Compras;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Seagull.BarTender.Print;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using NSSuite_CSharp.src.JSON.NFe;
using NSSuite_CSharp.src.JSON.MDFe;

namespace Lunar.Telas.Estoques
{
    public partial class FrmEtiquetas : Form
    {
        Ean13 ean13 = new Ean13();
        string caminhoPastaEtiquetas = "";
        GenericaDesktop generica = new GenericaDesktop();
        public FrmEtiquetas()
        {
            InitializeComponent();
            this.grid.DataSource = dsProdutos;
            txtProduto.Focus();
            VerificarECriarPastaEtiquetas();
            CarregarModelosEtiquetas();
            CarregarImpressoras();
            txtParcelas.TextAlign = HorizontalAlignment.Center;
        }

        private void CarregarImpressoras()
        {
            // Limpa o ComboBox para evitar duplicatas
            comboImpressoras.Items.Clear();

            // Obtém uma coleção de todas as impressoras instaladas no sistema
            PrinterSettings.StringCollection impressoras = PrinterSettings.InstalledPrinters;

            // Adiciona cada impressora ao ComboBox
            foreach (string impressora in impressoras)
            {
                comboImpressoras.Items.Add(impressora);
            }

            // Seleciona a primeira impressora por padrão, se houver alguma
            if (comboImpressoras.Items.Count > 0)
            {
                comboImpressoras.SelectedIndex = 0;
            }
        }
        private void VerificarECriarPastaEtiquetas()
        {
            string caminhoExecutavel = Application.StartupPath;
            caminhoPastaEtiquetas = Path.Combine(caminhoExecutavel, "Etiquetas");
            if (!Directory.Exists(caminhoPastaEtiquetas))
            {
                Directory.CreateDirectory(caminhoPastaEtiquetas);
            }
            if (!Directory.Exists(caminhoPastaEtiquetas + "\\TempFile"))
                Directory.CreateDirectory(caminhoPastaEtiquetas + "\\TempFile");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            txtProduto.Texts = "";
            txtCodProduto.Texts = "";
            PesquisarProduto("");
        }

        private void PesquisarProduto(string valor)
        {
            IList<Produto> listaProdutos = new List<Produto>();
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            ProdutoController produtoController = new ProdutoController();
            String valorAux = "";
            valorAux = valor;

            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {
                foreach (Produto prod in listaProdutos)
                {
                    txtProduto.Texts = prod.Descricao;
                    txtCodProduto.Texts = prod.Id.ToString();
                    txtQuantidade.Texts = "1";
                    txtQuantidade.Focus();
                    if (prod.Ean.Equals(valor.Trim()))
                        inserirItem(prod);
                    else
                    {
                        txtQuantidade.Focus();
                        txtProduto.SelectAll();
                    }
                }
            }
            else if (listaProdutos.Count > 1)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%'"))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //formBackground.Left = Top = 0;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        switch (uu.showModal("Produto", "", ref produtoOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmProdutoCadastro form = new FrmProdutoCadastro();
                                if (form.showModalNovo(ref produtoOjeto, false) == DialogResult.OK)
                                {
                                    txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtQuantidade.Texts = "1";
                                    txtQuantidade.Focus();
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                txtQuantidade.Texts = "1";
                                txtQuantidade.Focus();
                                txtProduto.SelectAll();
                                break;
                        }

                        formBackground.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    formBackground.Dispose();
                }
                // GenericaDesktop.ShowInfo("Função de pesquisa extra em desenvolvimento...");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtProduto.SelectAll();
            }
        }

        private void inserirItem(Produto produto)
        {
            txtQuantidade.TextAlign = HorizontalAlignment.Center;          
            try
            {
                System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                row.SetField("Id", produto.Id.ToString());
                row.SetField("Descricao", produto.Descricao);
                row.SetField("Valor", string.Format("{0:0.00}", produto.ValorVenda));
                if(String.IsNullOrEmpty(txtNotaCompra.Texts))
                    row.SetField("Quantidade", txtQuantidade.Texts);
                else
                    row.SetField("Quantidade", produto.Estoque);
                dsProdutos.Tables[0].Rows.Add(row);

                txtQuantidade.Texts = "1";
                txtCodProduto.Texts = "";
                txtProduto.Texts = "";
                txtProduto.Focus();

                if (this.grid.View != null)
                {
                    if (this.grid.View.Records.Count > 0)
                    {
                        grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                        this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        grid.AutoSizeController.Refresh();
                    }
                }
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto e quantidade");
            }
        }

        private void btnConfirmaItem_Click(object sender, EventArgs e)
        {
            confirmarItem();
        }

        private void confirmarItem()
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts))
            {
                ProdutoController produtoController = new ProdutoController();
                Produto produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)produtoController.selecionar(produto);
                if(produto != null)
                {
                    if (produto.Id > 0)
                    {
                        inserirItem(produto);
                    }
                }
            }
            else if (!String.IsNullOrEmpty(txtNotaCompra.Texts))
            {
                NfeProdutoController nfeProdutoController = new NfeProdutoController();

                IList<NfeProduto> listaProdutosNota = nfeProdutoController.selecionarProdutosPorNumeroNfe(int.Parse(txtNotaCompra.Texts));
                if(listaProdutosNota.Count > 0)
                {
                    //grid.DataSource = null;
                    dsProdutos.Tables[0].Clear();
                    foreach(NfeProduto nfeProduto in listaProdutosNota)
                    {
                        Produto prodsel = new Produto();
                        prodsel.Id = nfeProduto.Produto.Id;
                        prodsel = (Produto)ProdutoController.getInstance().selecionar(prodsel);
                        prodsel.Estoque = nfeProduto.QuantidadeEntrada;
                        if (prodsel.Estoque == 0)
                            prodsel.Estoque = 1;
                        inserirItem(prodsel);
                    }
                }
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtQuantidade.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmaItem.PerformClick();
            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProduto(txtProduto.Texts);
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCodProduto.Texts, e);
            if (e.KeyChar == 13)
            {
                try
                {
                    ProdutoController produtoController = new ProdutoController();
                    if (!String.IsNullOrEmpty(txtCodProduto.Texts))
                    {
                        Produto produto = new Produto();
                        txtProduto.Texts = "";
                        produto.Id = int.Parse(txtCodProduto.Texts);
                        produto = (Produto)produtoController.selecionar(produto);
                        if (produto != null)
                        {
                            txtProduto.Texts = produto.Descricao;
                            txtCodProduto.Texts = produto.Id.ToString();
                            txtQuantidade.Focus();
                        }
                        else
                        {

                            txtCodProduto.Texts = "";
                            txtProduto.Texts = "";
                            GenericaDesktop.ShowAlerta("Produto não encontrado");
                        }
                    }
                }
                catch (System.Exception erro)
                {
                    txtProduto.Texts = "";
                    txtCodProduto.Texts = "";
                    GenericaDesktop.ShowAlerta("Produto não encontrado");
                }
            }
        }

        private void imprimir()
        {
            if(grid.RowCount > 0)
            {
                var records = grid.View.Records;
                IList<Produto> listaProdutosEtiquetas = new List<Produto>();
                foreach (var record in records)
                {
                    Produto produto = new Produto();
                    var dataRowView = record.Data as DataRowView;
                    produto.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    produto = (Produto)ProdutoController.getInstance().selecionar(produto);
                    produto.Descricao = dataRowView.Row["Descricao"].ToString();
                    produto.ValorVenda = decimal.Parse(dataRowView.Row["Valor"].ToString());
                    produto.Estoque = double.Parse(dataRowView.Row["Quantidade"].ToString());
                    listaProdutosEtiquetas.Add(produto);
                }
                if (Sessao.empresaFilialLogada.Otica == true)
                {
                    FrmImprimirEtiquetasOtica frmImprimirEtiquetasOtica = new FrmImprimirEtiquetasOtica(listaProdutosEtiquetas);
                    frmImprimirEtiquetasOtica.ShowDialog();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Etiqueta configurada apenas para óticas!");
                }
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtParcelas.Texts))
                txtParcelas.Texts = "1";

            if (grid.View.Records.Count > 0)
            {
                var records = grid.View.Records;
                IList<Produto> listaProdutosEtiquetas = new List<Produto>();
                foreach (var record in records)
                {
                    Produto produto = new Produto();
                    var dataRowView = record.Data as DataRowView;
                    produto.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    produto = (Produto)ProdutoController.getInstance().selecionar(produto);
                    produto.Descricao = dataRowView.Row["Descricao"].ToString();
                    produto.ValorVenda = decimal.Parse(dataRowView.Row["Valor"].ToString());
                    produto.Estoque = double.Parse(dataRowView.Row["Quantidade"].ToString());
                    produto.Observacoes = txtObservacoes.Texts;
                    listaProdutosEtiquetas.Add(produto);
                }
                string nomeArquivoEtiqueta = comboModeloEtiqueta.SelectedItem.ToString();
                string nomeImpressoraSelecionada = comboImpressoras.SelectedItem.ToString();
                PrintLabel3(nomeArquivoEtiqueta, listaProdutosEtiquetas, nomeImpressoraSelecionada);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Insira os produtos que deseja imprimir!");
            }
        }

        private void PrintLabel2(string nomeArquivoEtiqueta, IList<Produto> listaProdutos, string nomeImpressora)
        {
            string labelFile = Path.Combine(caminhoPastaEtiquetas, nomeArquivoEtiqueta);
            int numArquivo = 0;

            string combinedPdfPath = Path.Combine(caminhoPastaEtiquetas+ "\\TempFile", "PreviewEtiquetas.pdf");
            DirectoryInfo di = new DirectoryInfo(caminhoPastaEtiquetas + "\\TempFile");
            foreach (FileInfo file in di.GetFiles("*.pdf"))
            {
                //Deleta os pdfs da pasta, para caso o usuario queira gerar um pdf visualizador, pois vou ler todos pdf da pasta
                file.Delete();
            }
            try
            {
                using (Engine btEngine = new Engine(true))
                {
                    foreach (var produto in listaProdutos)
                    {
                        LabelFormatDocument labelFormat = btEngine.Documents.Open(labelFile);
                        numArquivo++;
                        try { labelFormat.SubStrings["nomeEmpresa"].Value = Sessao.empresaFilialLogada.NomeFantasia; } catch (KeyNotFoundException) { }
                        try { labelFormat.SubStrings["descricaoProduto"].Value = produto.Descricao; } catch (KeyNotFoundException) { }
                        try { labelFormat.SubStrings["codigoBarras"].Value = produto.Ean; } catch (KeyNotFoundException) { }
                        try { labelFormat.SubStrings["observacoes"].Value = produto.Observacoes; } catch (KeyNotFoundException) { }
                        try { labelFormat.SubStrings["preco"].Value = (produto.ValorVenda / decimal.Parse(txtParcelas.Texts)).ToString("C2"); } catch (KeyNotFoundException) { }
                        try { labelFormat.SubStrings["quantidade"].Value = (produto.Estoque.ToString()); } catch (KeyNotFoundException) { }
                        labelFormat.PrintSetup.PrinterName = nomeImpressora;
                        if (nomeImpressora.Contains("PDF"))
                        {
                            labelFormat.PrintSetup.PrinterName = "PDF";
                            labelFormat.PrintSetup.PrintToFile = true;
                            labelFormat.PrintSetup.PrintToFileName = caminhoPastaEtiquetas + $@"\TempFile\Preview_" + numArquivo + ".pdf";
                        }
                        labelFormat.PrintSetup.IdenticalCopiesOfLabel = int.Parse(produto.Estoque.ToString());
                        labelFormat.Print("Etiqueta");
                        labelFormat.Close(SaveOptions.DoNotSaveChanges);
                    }
                    if (nomeImpressora.Contains("PDF"))
                    {
                        if (!Directory.Exists(caminhoPastaEtiquetas + "\\TempFile"))
                            Directory.CreateDirectory(caminhoPastaEtiquetas + "\\TempFile");
                        CombinePDFs(caminhoPastaEtiquetas+"\\TempFile", combinedPdfPath);
                        Process.Start(combinedPdfPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao imprimir: " + ex.Message);
            }
        }

        private void PrintLabel3(string nomeArquivoEtiqueta, IList<Produto> listaProdutos, string nomeImpressora)
        {
            string labelFile = Path.Combine(caminhoPastaEtiquetas, nomeArquivoEtiqueta);
            string combinedPdfPath = Path.Combine(caminhoPastaEtiquetas + "\\TempFile", "PreviewEtiquetas.pdf");
            DirectoryInfo di = new DirectoryInfo(caminhoPastaEtiquetas + "\\TempFile");

            // Deleta os PDFs da pasta
            foreach (FileInfo file in di.GetFiles("*.json"))
            {
                file.Delete();
            }

            try
            {
                // Lista para armazenar as etiquetas
                List<LabelData> listaEtiquetas = new List<LabelData>();

                // Converter lista de produtos em lista de etiquetas
                foreach (var produto in listaProdutos)
                {
                    //Se o produto nao tem codigo de barras, vamos gerar um
                    if (String.IsNullOrEmpty(produto.Ean))
                    {
                        while (String.IsNullOrEmpty(produto.Ean))
                        {
                            produto.Ean = gerarCodigoBarras(produto);
                        }
                    }
                    for (int i = 0; i < produto.Estoque; i++)
                    {
                        LabelData etiqueta = new LabelData
                        {
                            NomeEmpresa = Sessao.empresaFilialLogada.NomeFantasia,
                            CodigoProduto = produto.Id.ToString(),
                            DescricaoProduto = produto.Descricao,
                            CodigoBarras = produto.Ean,
                            Observacoes = produto.Observacoes,
                            Preco = (produto.ValorVenda / decimal.Parse(txtParcelas.Texts)).ToString("C2"),
                            Quantidade = produto.Estoque,
                            Parcelas = double.Parse(txtParcelas.Texts)
                        };

                        listaEtiquetas.Add(etiqueta);
                    }
                }

                // Encapsular as etiquetas dentro de um objeto LabelDataContainer
                LabelDataContainer container = new LabelDataContainer
                {
                    LabelDataList = listaEtiquetas
                };

                // Serializar o objeto LabelDataContainer em JSON
                string json = JsonConvert.SerializeObject(container, Formatting.Indented);
                string caminhoArquivoJson = caminhoPastaEtiquetas + "\\TempFile\\Etiquetas.json";
                File.WriteAllText(caminhoArquivoJson, json);

                string appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string tempFileDirectory = Path.Combine(appBaseDirectory, "Etiquetas", "TempFile");

                string caminhoBartender = @"C:\Program Files\Seagull\BarTender 2022\bartend.exe";
                string caminhoArquivoJSON = @tempFileDirectory+@"\Etiquetas.json";

                if (chkPreview.Checked == true)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = caminhoBartender;
                    startInfo.Arguments = $"/F=\"{labelFile}\" /D=\"{caminhoArquivoJSON}\" /PD";
                    Process.Start(startInfo);
                }
                else
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = caminhoBartender,
                        Arguments = $"/AF=\"{labelFile}\" /D=\"{caminhoArquivoJSON}\" /P /X /PRN=\"{nomeImpressora}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    // Inicie o processo
                    Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta("Erro ao imprimir: " + ex.Message);
            }
        }
  
        public class LabelData
        {
            public string NomeEmpresa { get; set; }
            public string CodigoProduto { get; set; }
            public string DescricaoProduto { get; set; }
            public string CodigoBarras { get; set; }
            public string Observacoes { get; set; }
            public string Preco { get; set; }
            public double Quantidade { get; set; }
            public double Parcelas { get; set; }
        }


        public class LabelDataContainer
        {
            public List<LabelData> LabelDataList { get; set; }
        }


        private void CombinePDFs(string folderPath, string combinedPdfPath)
        {
            // Obtém todos os arquivos PDF na pasta especificada
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            // Cria um novo documento PDF para combinar os arquivos
            using (FileStream combinedStream = new FileStream(combinedPdfPath, FileMode.Create))
            {
                using (Document document = new Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, combinedStream))
                    {
                        document.Open();

                        // Adiciona cada arquivo PDF ao documento combinado
                        foreach (string pdfFile in pdfFiles)
                        {
                            using (PdfReader reader = new PdfReader(pdfFile))
                            {
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    PdfImportedPage page = copy.GetImportedPage(reader, i);
                                    copy.AddPage(page);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                var selectedItem = this.grid.CurrentItem as DataRowView;
                var dataRow = (selectedItem as DataRowView).Row;
                dsProdutos.Tables[0].Rows[grid.SelectedIndex].Delete();
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodProduto.Texts = "";
            txtProduto.Texts = "";
            txtNotaCompra.Texts = "";
            txtQuantidade.Texts = "1";
            dsProdutos.Tables[0].Clear();
            txtProduto.Focus();
        }

        private void CarregarModelosEtiquetas()
        {
            if (Directory.Exists(caminhoPastaEtiquetas))
            {
                string[] arquivosEtiquetas = Directory.GetFiles(caminhoPastaEtiquetas, "*.btw");
                comboModeloEtiqueta.Items.Clear();
                if (arquivosEtiquetas.Length > 0)
                {
                    foreach (string arquivo in arquivosEtiquetas)
                    {
                        comboModeloEtiqueta.Items.Add(Path.GetFileName(arquivo));
                    }
                    comboModeloEtiqueta.SelectedIndex = 0;
                }
                else
                {
                    comboModeloEtiqueta.Items.Add("Nenhum modelo disponível");
                }
            }
        }

        private void txtParcelas__TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtParcelas.Texts))
                {
                    if(int.Parse(txtParcelas.Texts) > 1)
                        txtObservacoes.Texts = "Por Apenas " + txtParcelas.Texts + "x de ";
                }
            }
            catch { }
        }

        private void txtParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Se não for um número, marca o evento como manipulado para evitar a entrada
                e.Handled = true;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPreview.Checked == true)
                    comboImpressoras.Enabled = false;
                if (chkPreview.Checked == false)
                    comboImpressoras.Enabled = true;
            }
            catch
            {

            }
        }

        private string gerarCodigoBarras(Produto product)
        {
            Produto prod = new Produto();
            prod = product;
            ProdutoController produtoController = new ProdutoController();
            CreateEan13();
            IList<Produto> listaProd = produtoController.selecionarProdutosComVariosFiltros(ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit, Sessao.empresaFilialLogada);
            if (listaProd.Count == 0)
            {
                prod.Ean = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                Controller.getInstance().salvar(prod);
                return prod.Ean;
            }
            else
                return "";
        }
        private void CreateEan13()
        {
            ean13 = new Ean13();
            ean13.CountryCode = RandomNumber(10, 78).ToString();
            ean13.ManufacturerCode = RandomNumber(79000, 99000).ToString();
            ean13.ProductCode = RandomNumber(10000, 99000).ToString();
            ean13.ChecksumDigit = RandomNumber(1, 9).ToString();
            ean13.Scale = 1;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
