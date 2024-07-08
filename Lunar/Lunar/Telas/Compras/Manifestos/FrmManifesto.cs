using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Syncfusion.Data;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.Compras.Manifestos
{
    public partial class FrmManifesto : Form
    {
        private IList<Nfe> lista;
        Nfe nfe = new Nfe();
        GenericaDesktop generica = new GenericaDesktop();
        ManifestoDownload.Manifesto manifesto = new ManifestoDownload.Manifesto();
        bool primeiraVerificacao = true;
        NfeController nfeController = new NfeController();
        Genericos genericos = new Genericos();
        public FrmManifesto()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inserirNotasNoGrid(ManifestoDownload.Manifesto manifesto)
        {
            bool JaExiste = false;
            String chaveVerificada = "";
            int ultimoNsuSalvo = 0;

            for (int i = 0; i < manifesto.xmls.Length; i++)
            {
                nfe = new Nfe();
                nfe = nfeController.selecionarNotaPorChave(manifesto.xmls[i].chave);
                if (nfe is null)
                {
                    var records = grid.View.Records;
                    if (records.Count > 0)
                    {
                        foreach (var record in records)
                        {
                            var dataRowView = record.Data as DataRowView;
                            chaveVerificada = dataRowView.Row["CHAVE"].ToString();
                            if (chaveVerificada.Equals(manifesto.xmls[i].chave))
                            {
                                JaExiste = true;
                            }
                        }
                    }
                    if (JaExiste == false && !String.IsNullOrEmpty(manifesto.xmls[i].xml))
                    {
                        if (!String.IsNullOrEmpty(manifesto.xmls[i].xml))
                            generica.gravarXMLNaPasta(manifesto.xmls[i].xml, manifesto.xmls[i].chave, @"XML\", manifesto.xmls[i].chave + ".xml");

                        if (!String.IsNullOrEmpty(manifesto.xmls[i].pdf))
                            gerarPDF2(manifesto.xmls[i].pdf, manifesto.xmls[i].chave, false);

                        var nfe = Genericos.LoadFromXMLString<TNfeProc>(manifesto.xmls[i].xml);
                        Genericos genericos = new Genericos();
                        genericos.gravarXMLNoBanco(nfe, manifesto.xmls[i].nsu, "E", 0, true);
                        Sessao.parametroSistema.UltNsu = manifesto.ultNSU.ToString();
                        Controller.getInstance().salvar(Sessao.parametroSistema);
                    }
                    JaExiste = false;
                }
            }
            ultimoNsuSalvo = nfeController.selecionarUltimoNsu();

            ParametroSistema param = new ParametroSistema();
            param.Id = 1;
            param = (ParametroSistema)Controller.getInstance().selecionar(param);
            param.UltNsu = ultimoNsuSalvo.ToString();
            Controller.getInstance().salvar(param);
            Sessao.parametroSistema = param;
            if (ultimoNsuSalvo != int.Parse(Sessao.parametroSistema.UltNsu))
                verificarNotasSefaz();
        }
    
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                {
                    xElement.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));
                }

                return xElement;
            }

            else
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));

                foreach (XAttribute attribute in xmlDocument.Attributes())
                {
                    xElement.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));
                }

                return xElement;
            }
        }

        private void gerarPDF2(String pdf, String chave, bool imprimir)
        {
            if (!File.Exists(@"XML\" + chave + ".pdf"))
            {
                byte[] bytes = Convert.FromBase64String(pdf);
                System.IO.FileStream stream = new FileStream(@"XML\" + chave + ".pdf", FileMode.CreateNew);
                System.IO.BinaryWriter writer =
                    new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
            if (imprimir == true)
                Process.Start(@"XML\" + chave + ".pdf");
        }

      
        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as Nfe) != null)
                {
                    if ((e.RowData as Nfe).Manifesto.Contains("210220"))
                        e.Style.TextColor = Color.Red;
                    if ((e.RowData as Nfe).Manifesto.Contains("210200"))
                        e.Style.TextColor = Color.Green;
                }
            }
            catch
            {

            }
            //e.Style.TextColor = Color.FromArgb(255, 99, 71);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                //var dataRowView = grid.SelectedItem as DataRowView;
                nfe = (Nfe)grid.SelectedItem;
                //MessageBox.Show(dataRowView.Row["CHAVE"].ToString());

                if (File.Exists(@"XML\" + nfe.Chave + ".pdf"))
                    Process.Start(@"XML\" + nfe.Chave + ".pdf");
                else
                {
                    NotaUnique notaUnique = new NotaUnique();
                    notaUnique = generica.ConsultaNotas_Manifesto_Unique(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                    if (notaUnique != null)
                    {
                        if(notaUnique.listaDocs == false)
                            gerarPDF2(notaUnique.pdf, notaUnique.chave, true);
                    }
                }

            }
        }

        private void verificarNotasSefaz()
        {
            try
            {
                string ultimaDataNotaBaixada = nfeController.selecionarUltimaDataNotaBaixada();
                if (!String.IsNullOrEmpty(ultimaDataNotaBaixada))
                    ultimaDataNotaBaixada = ultimaDataNotaBaixada.Replace("T", " ").Replace("-03:00", "");
                else
                    ultimaDataNotaBaixada = DateTime.Now.AddMonths(-2).ToShortDateString();

                DateTime ultDt = DateTime.Parse(ultimaDataNotaBaixada);
                if (ultDt < DateTime.Now.AddMonths(-2))
                    ultDt = DateTime.Now.AddMonths(-2);
                Task<ManifestoDownload.Manifesto> taskNota = Task.Run(() => generica.ConsultaNotas_Manifesto(Sessao.empresaFilialLogada.Cnpj, ultDt));
                taskNota.Wait();
                var result = taskNota.Result;
                if (result != null)
                    inserirNotasNoGrid(result);
                grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.AutoSizeController.Refresh();
            }
            catch
            {

            }
        }

        private void selecionarNotasBancoDados(string dataInicial, string dataFinal)
        {
            lista = nfeController.selecionarNotasEntradaPorPeriodo(dataInicial, dataFinal);
            if(lista.Count > 0)
            {
                sfDataPager1.DataSource = lista;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
                txtPesquisa.Focus();

                grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.AutoSizeController.Refresh();
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, lista.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void Pesquisar(string dataInicial, string dataFinal, string valorPesquisa)
        {
            lista = nfeController.selecionarNotaEntradaComVariosFiltros(dataInicial, dataFinal, valorPesquisa);

            sfDataPager1.DataSource = lista;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (lista.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisa.Texts = "";
                txtPesquisa.PlaceholderText = "";
                txtPesquisa.Select();
            }
            calculaTotalNotas();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisaNotas();
        }

        private void txtRegistroPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void pesquisaNotas()
        {
            try
            {
                DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
                DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");
                Pesquisar(dataInicial, dataFinal, txtPesquisa.Texts.Trim());
            }
            catch
            {
                GenericaDesktop.ShowErro("Informe as datas corretamente, caso esteja correto solicite suporte para seu representante!");
            }
        }

        private void btnAtualizarNotas_Click(object sender, EventArgs e)
        {
            verificarNotasSefaz();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
            var page = document.Pages.Add();
            var PDFGrid = grid.ExportToPdfGrid(grid.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == grid.Columns[0].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ListaNotasCompra",
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    document.Save(stream);
                }
                //Message box confirmation to view the created Pdf file.
                if (MessageBox.Show("Deseja abrir o arquivo Pdf?", "Pdf criado com sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the Pdf file using the default Application.
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaNotasCompra",
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.grid, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void calculaTotalNotas()
        {
            try
            {
                //sumario no grid
                GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
                tableSummaryRow1.Name = "TableSummary";
                tableSummaryRow1.ShowSummaryInRow = true;
                tableSummaryRow1.Title = " Notas: {TotalNotas}";
                tableSummaryRow1.Position = VerticalPosition.Bottom;

                GridTableSummaryRow tableSummaryRow2 = new GridTableSummaryRow();
                tableSummaryRow2.Name = "TableSummary2";
                tableSummaryRow2.ShowSummaryInRow = true;
                tableSummaryRow2.Title = " Valor Total: {ValorTotal}";
                tableSummaryRow2.Position = VerticalPosition.Bottom;

                GridSummaryColumn summaryColumn1 = new GridSummaryColumn();
                summaryColumn1.Name = "TotalNotas";
                summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
                summaryColumn1.Format = "{Count}";
                summaryColumn1.MappingName = "Chave";

                GridSummaryColumn summaryColumn2 = new GridSummaryColumn();
                summaryColumn2.Name = "ValorTotal";
                summaryColumn2.SummaryType = SummaryType.DoubleAggregate;
                summaryColumn2.Format = "{Sum:c}";
                summaryColumn2.MappingName = "VNf";

                //tableSummaryRow1.SummaryColumns.Clear();
                //tableSummaryRow2.SummaryColumns.Clear();
                this.grid.TableSummaryRows.Clear();

                tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
                tableSummaryRow2.SummaryColumns.Add(summaryColumn2);

                this.grid.TableSummaryRows.Add(tableSummaryRow1);
                this.grid.TableSummaryRows.Add(tableSummaryRow2);
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
            }
        }

        private void confirmaNota()
        {
            if (grid.RowCount > 0 && grid.SelectedIndex >= 0)
            {
                nfe = (Nfe)grid.SelectedItem;
                ManifestacaoNS_retorno retorno = generica.Ns_ConfirmaNotaManifesto(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                if (retorno != null)
                {
                    if (retorno.status == 200 && retorno.motivo.Contains("Consulta realizada com sucesso"))
                    {
                        nfe.Manifesto = "210200 - Confirmação da Operação - " + retorno.retEvento.xMotivo;
                        Controller.getInstance().salvar(nfe);
                        grid.Refresh();
                        GenericaDesktop.ShowInfo(retorno.retEvento.xMotivo);
                    }
                    else if (retorno.status == 200 && retorno.motivo.Contains("Documento já manifestado"))
                    {
                        if (String.IsNullOrEmpty(nfe.Manifesto))
                        {
                            nfe.Manifesto = "210200 - Confirmação da Operação - " + retorno.retEvento.xMotivo;
                            Controller.getInstance().salvar(nfe);
                            grid.Refresh();
                        }
                        GenericaDesktop.ShowInfo("Documento já manifestado e autorizado anteriormente");
                    }
                    else
                    {
                        if (retorno.erro != null)
                        {
                            GenericaDesktop.ShowAlerta("Nao foi possivel manifestar o documento: " + retorno.erro.xMotivo);
                            if (retorno.erro.xMotivo.Contains("Duplicidade"))
                            {
                                nfe.Manifesto = "210200 - Confirmação da Operação - " + retorno.erro.xMotivo;
                                Controller.getInstance().salvar(nfe);
                                grid.Refresh();
                            }
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nao foi possivel manifestar o documento");
                    }

                }
            }
        }

        private void btnConfirmarNota_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        
        private void btnLancarNota_Click(object sender, EventArgs e)
        {
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                Nfe nfeGrid = (Nfe)grid.SelectedItem;
                if (nfeGrid.Lancada == false)
                {
                    if (GenericaDesktop.possuiConexaoInternet())
                    {
                        NotaUnique notaUnique = new NotaUnique();
                        notaUnique = generica.ConsultaNotas_Manifesto_Unique(Sessao.empresaFilialLogada.Cnpj, nfeGrid.Chave);
                        if (notaUnique != null)
                        {
                            TNfeProc nfXML = Genericos.LoadFromXMLString<TNfeProc>(notaUnique.xml);
                            FrmLancarNotaFiscalCompra frmLancarNota = new FrmLancarNotaFiscalCompra(nfXML, nfeGrid);
                            frmLancarNota.ShowDialog();
                            grid.Refresh();
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("XML não disponível para lançamento automático, baixe o xml pelo site da SEFAZ!");
                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Instabilidade com sua internet, tente novamente ou verifique a conexão");
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Esta nota fiscal já foi lançada");

            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione uma Nota");
            }

        }

        private void txtDataInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void txtDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void btnRejeitarNota_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja rejeitar essa nota? Será enviado para Sefaz o manifesto de Desconhecimento da Operação!"))
            {
                if (grid.RowCount > 0 && grid.SelectedIndex >= 0)
                {
                    nfe = (Nfe)grid.SelectedItem;
                    ManifestacaoNS_retorno retorno = generica.Ns_RejeitarNotaManifesto(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                    if (retorno != null)
                    {
                        if (retorno.status == 200)
                        {
                            nfe.Manifesto = "210220 - Desconhecimento da Operação - " + retorno.retEvento.xMotivo;
                            Controller.getInstance().salvar(nfe);
                            grid.Refresh();
                            GenericaDesktop.ShowInfo(retorno.retEvento.xMotivo);
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nao foi possivel manifestar o documento: " + retorno.retEvento.xMotivo);
                    }
                }
            }
        }

        private void btnCancelarLancamento_Click(object sender, EventArgs e)
        {
            Nfe nfeGrid = (Nfe)grid.SelectedItem;
            if (nfeGrid.Lancada == true)
            { 
                if(GenericaDesktop.ShowConfirmacao("Tem certeza que deseja cancelar o lançamento desta nota?"))
                {
                    nfeGrid.Lancada = false;
                    Controller.getInstance().salvar(nfeGrid);
                    NfeProdutoController nfeProdutoController = new NfeProdutoController();
                    IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfeGrid.Id);
                    foreach(NfeProduto nfeProduto in listaProdutos)
                    {
                        Produto prod = new Produto();
                        prod = nfeProduto.Produto;  
                        prod.Estoque = prod.Estoque - nfeProduto.QuantidadeEntrada;
                        prod.EstoqueAuxiliar = prod.EstoqueAuxiliar - nfeProduto.QuantidadeEntrada;
                        Estoque estoque = new Estoque();
                        estoque.Conciliado = true;
                        estoque.DataEntradaSaida = nfeGrid.DataLancamento;
                        estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                        estoque.Entrada = false;
                        estoque.Origem = "CANCEL. ENTRADA DOC: " + nfeGrid.NNf;
                        estoque.Produto = prod;
                        estoque.Quantidade = nfeProduto.QuantidadeEntrada;
                        estoque.Saida = true;

                        Controller.getInstance().salvar(estoque);
                        Controller.getInstance().excluir(nfeProduto);
                        Controller.getInstance().salvar(prod);
                    }
                    NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                    ContaPagarDAO contaPagarDAO = new ContaPagarDAO();
                    //nfeProdutoDAO.excluirProdutosNfeParaAtualizar(nfeGrid.Id.ToString());
                    contaPagarDAO.excluirContaPagarNfe(nfeGrid.Id.ToString());

                    GenericaDesktop.gravarLinhaLog("[CANCELAMENTO_COMPRA_COM_CONTASPAGAR_NFE]: Usuário: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login +
                        " NFeID: " + nfeGrid.Id + " Chave: " + nfeGrid.Chave, "CANCELAMENTO COMPRA");

                    GenericaDesktop.ShowInfo("Lançamento cancelado com sucesso");
                    grid.Refresh();
                }
            }
        }

        private void FrmManifesto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnLancarNota.PerformClick();
                    break;
                case Keys.F6:
                    btnConfirmarNota.PerformClick();
                    break;
                case Keys.F7:
                    btnRejeitarNota.PerformClick();
                    break;
                case Keys.F8:
                    btnImprimir.PerformClick();
                    break;
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            confirmaNota();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                GenericaDesktop.ShowAlerta("Manifesto Cancelado");
            }
            else if (e.Error != null)
            {
                GenericaDesktop.ShowAlerta("Erro no manifesto");
            }
        }

        private void btnImportarXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Arquivo XML de NFe (*.XML;)|*.XML|" + "Todos Arquivos (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //XML TO STRING
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(openFileDialog1.FileName);
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    xmlDoc.WriteTo(xw);
                    String XmlString = sw.ToString();

                    TNfeProc nfXML = Genericos.LoadFromXMLString<TNfeProc>(XmlString);
                    Nfe nfe = new Nfe();
                    nfe = nfeController.selecionarNotaPorChave(GenericaDesktop.RemoveCaracteres(nfXML.protNFe.infProt.chNFe.Trim()));
                    if(nfe != null)
                    {
                        FrmLancarNotaFiscalCompra frmLancarNota = new FrmLancarNotaFiscalCompra(nfXML, nfe);
                        frmLancarNota.ShowDialog();
                        grid.Refresh();
                    }
                    else
                    {
                        if (nfXML.NFe.infNFe.dest != null)
                        {
                            if (nfXML.NFe.infNFe.dest.Item.Equals(Sessao.empresaFilialLogada.Cnpj))
                            {
                                //SALVA XML NO BANCO E INICIA LANÇAMENTO
                                genericos.gravarXMLNoBanco(nfXML, 0, "E", 0, true);
                                nfe = nfeController.selecionarNotaPorChave(GenericaDesktop.RemoveCaracteres(nfXML.protNFe.infProt.chNFe.Trim()));
                                FrmLancarNotaFiscalCompra frmLancarNota = new FrmLancarNotaFiscalCompra(nfXML, nfe);
                                frmLancarNota.ShowDialog();
                                grid.Refresh();
                            }
                            else
                            {
                                GenericaDesktop.ShowAlerta("Atenção, esse xml não tem a empresa " + Sessao.empresaFilialLogada.NomeFantasia + " como destinatário!");
                            }
                        }
                        else
                        {
                            //XML NAO POSSUI DESTINATÁRIO, PORTANDO É UM XML INVÁLIDO PARA IMPORTAÇÃO
                            GenericaDesktop.ShowAlerta("Arquivo XML inválido, verifique se selecionou o arquivo correto!");
                        }
                    }

                    //FrmLancarNotaFiscalCompra frmLancarNota = new FrmLancarNotaFiscalCompra(nfXML, nfeGrid);
                    //frmLancarNota.ShowDialog();
                    //grid.Refresh();
                }
                catch (Exception erro)
                {
                    Logger logger = new Logger();
                    logger.WriteLog("Erro ao importar XML: " + erro.Message, "Logs");
                    //GenericaDesktop.ShowAlerta("Arquivo XML inválido, verifique se selecionou o arquivo correto!");
                }
            }
        }

        private void FrmManifesto_Load(object sender, EventArgs e)
        {
            if (primeiraVerificacao == true)
            {
                dsManifesto.Tables[0].Clear();
                this.grid.DataSource = dsManifesto.Tables["Nota"];
                primeiraVerificacao = false;
                if (GenericaDesktop.possuiConexaoInternet())
                    verificarNotasSefaz();
                else
                    GenericaDesktop.ShowAlerta("Instabilidade com sua internet para atualização de novas notas, tente novamente ou verifique a conexão");

                //Insere Notas no Grid
                DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                DateTime ultimoDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                ultimoDiaDoMes = ultimoDiaDoMes.AddMonths(1).AddDays(-1);

                txtDataInicial.Text = primeiroDiaDoMes.ToShortDateString();
                txtDataFinal.Text = ultimoDiaDoMes.ToShortDateString();

                selecionarNotasBancoDados(primeiroDiaDoMes.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), ultimoDiaDoMes.ToString("yyyy'-'MM'-'dd' '00':'00':'00"));
                calculaTotalNotas();
            }
        }
    }
}
