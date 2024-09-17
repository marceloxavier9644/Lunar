using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Utils;
using Lunar.Utils.Balancas;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.ProdutoDAO;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmCargaBalanca : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        BalancaService balancaService = new BalancaService();
        Produto produto = new Produto();
        ProdutoController produtoController = new ProdutoController();
        public FrmCargaBalanca()
        {
            InitializeComponent();
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            txtPortaBalanca.Text = "9000";
        }
        private bool ValidarIp(string ip)
        {
            string pattern = @"^(\d{1,3}\.){3}\d{1,3}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(ip))
            {
                return false;
            }

            string[] partes = ip.Split('.');
            foreach (string parte in partes)
            {
                if (int.TryParse(parte, out int valor))
                {
                    if (valor < 0 || valor > 255)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void txtIpBalanca_Leave(object sender, EventArgs e)
        {
            string ip = txtIpBalanca.Text;

            if (ValidarIp(ip))
            {

            }
            else
            {
                GenericaDesktop.ShowAlerta("IP inválido");
            }
        }
    
        private async void enviarCargaBalanca()
        {
            // Obter a lista de itens (simulado ou da base de dados)
            List<ItemBalanca> itens = balancaService.ObterItensDaBaseDeDados();

            // Gerar o arquivo de itens
            balancaService.GerarArquivoItens(itens);

            // Caminho do arquivo gerado
            string caminhoAreaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string caminhoArquivo = Path.Combine(caminhoAreaDeTrabalho, "Itensmgv.txt");

            // Enviar arquivo para a balança via TCP/IP
            string ipBalanca = txtIpBalanca.Text; // Coloque o IP correto da balança
            int portaBalanca = int.Parse(txtPortaBalanca.Text); // Porta padrão da balança (ajuste conforme necessário)
            balancaService.EnviarArquivoParaBalanca(caminhoArquivo, ipBalanca, portaBalanca);
        }

        private void txtPortaBalanca_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtPortaBalanca.Text, e);
        }

        private void btnTestarComunicacao_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPortaBalanca.Text) && !String.IsNullOrEmpty(txtIpBalanca.Text))
            {
                BalancaService balancaService = new BalancaService();
                bool retorno = balancaService.TestarComunicacao(txtIpBalanca.Text.Trim(), int.Parse(txtPortaBalanca.Text.Trim()));
                if(retorno == true)
                {
                    panel2.Visible = true;
                    panel3.Visible = true;
                    panel4.Visible = true;
                    btnEnviarCarga.Enabled = true;
                    gridProdutos.Visible = true;
                }
            }
            else
                GenericaDesktop.ShowAlerta("Informe um IP e porta da Balança!");
        }

        private CancellationTokenSource _cancellationTokenSource;
        private async void btnScan_Click(object sender, EventArgs e)
        {
            progressBarBalanca.Visible = true;
            btnParar.Visible = true;
            string ipLocal = BalancaService.ObterIpLocal();
            if (string.IsNullOrEmpty(ipLocal))
            {
                throw new Exception("Não foi possível obter o IP local.");
            }

            string faixaIp = ipLocal.Substring(0, ipLocal.LastIndexOf('.') + 1);
            int portaBalanca = int.Parse(txtPortaBalanca.Text);
            int inicioRange = 1;
            int fimRange = 254;

            // Cria um CancellationTokenSource
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                await balancaService.ScanBalancasNaRede(faixaIp, portaBalanca, inicioRange, fimRange, progressBarBalanca, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                GenericaDesktop.ShowAlerta("Scan cancelado.");
            }
            finally
            {
                progressBarBalanca.Invoke((MethodInvoker)(() =>
                {
                    progressBarBalanca.Visible = false;
                }));
                _cancellationTokenSource.Dispose();
                btnParar.Visible = false; // Esconde o botão de parar após o scan
            }
        }

        private void cancelarScan()
        {
            _cancellationTokenSource?.Cancel();
        }
        

        private void FrmCargaBalanca_Load(object sender, EventArgs e)
        {
            try
            {
                string ipLocal = BalancaService.ObterIpLocal();
                if (string.IsNullOrEmpty(ipLocal))
                {
                    throw new Exception("Não foi possível obter o IP local.");
                }

                txtIpBalanca.Text = ipLocal.Substring(0, ipLocal.LastIndexOf('.') + 1);
            }
            catch
            {

            }
        }

        private void btnGerarTxt_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            gridProdutos.Visible = true;
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            cancelarScan();
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodProduto.Texts))
                    {
                        produto = new Produto();
                        txtPesquisaProduto.Texts = "";
                        produto.Id = int.Parse(txtCodProduto.Texts);
                        produto = (Produto)produtoController.selecionar(produto);
                        if (produto != null)
                        {
                            if (produto.Grade == true)
                            {
                                ProdutoGrade produtoGrade = new ProdutoGrade();
                                produtoGrade = selecionarGrade(produto);

                                if (produtoGrade != null)
                                {
                                    txtPesquisaProduto.Texts = produto.Descricao;
                                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                    this.produto.GradePrincipal = produtoGrade;
                                    AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                                }
                            }
                            else
                            {
                                txtPesquisaProduto.Texts = "";
                                txtCodProduto.Texts = "";
                                AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                            }
                        }
                        else
                        {

                            txtCodProduto.Texts = "";
                            txtPesquisaProduto.Texts = "";
                            GenericaDesktop.ShowAlerta("Produto não encontrado");
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtPesquisaProduto.Texts = "";
                    txtCodProduto.Texts = "";
                    GenericaDesktop.ShowAlerta("Produto não encontrado");
                }
            }
        }

        private ProdutoGrade selecionarGrade(Produto produto)
        {
            using (var formBackground = new Form())
            {
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d; // Define a opacidade
                formBackground.BackColor = Color.Black; // Define a cor de fundo
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width; // Define a largura
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height; // Define a altura
                formBackground.WindowState = FormWindowState.Maximized; // Maximiza a janela
                formBackground.TopMost = true; // Garante que o formulário de fundo fique acima de outros
                formBackground.ShowInTaskbar = false; // Não mostra na barra de tarefas
                formBackground.Show(); // Exibe o formulário de fundo

                using (var frmSelecionarGrade = new FrmSelecionarGrade(produto))
                {
                    frmSelecionarGrade.StartPosition = FormStartPosition.CenterParent; // Centraliza em relação ao formulário de fundo
                    frmSelecionarGrade.FormBorderStyle = FormBorderStyle.FixedDialog; // Configura a borda do formulário
                    if (frmSelecionarGrade.ShowDialog(formBackground) == DialogResult.OK)
                    {
                        var gradeSelecionada = frmSelecionarGrade.GradeSelecionada;
                        if (gradeSelecionada != null)
                        {
                            return gradeSelecionada;
                        }
                    }
                }
                formBackground.Dispose();
                return null;
            }
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProduto(txtPesquisaProduto.Texts.Trim());
            }
        }

        private void PesquisarProduto(string valor)
        {
            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            //Verifica se é codigo de barras
            if (IsNumericAndHasMoreThan7Digits(valor))
            {
                ProdutoDAO produtoDAO = new ProdutoDAO();
                String sql = "SELECT p.Id AS ProdutoId, p.DESCRICAO AS ProdutoNome, pg.Id AS ProdutoGradeId, pg.Descricao AS DescricaoGrade, COALESCE(um.Id, p.UnidadeMedida) AS UnidadeMedida, COALESCE(pg.ValorVenda, p.ValorVenda) AS ValorVenda, COALESCE(pcb.CodigoBarras, (SELECT pcb2.CodigoBarras FROM ProdutoCodigoBarras pcb2 WHERE pcb2.Produto = p.Id LIMIT 1)) AS CodigoBarras FROM Produto p JOIN ProdutoGrade pg ON p.Id = pg.Produto LEFT JOIN ProdutoCodigoBarras pcb ON pg.Id = pcb.ProdutoGrade LEFT JOIN UnidadeMedida um ON pg.UnidadeMedida = um.Id WHERE pcb.CodigoBarras = '" + valor + "' AND p.FlagExcluido = 0 AND pg.FlagExcluido = 0 AND (pcb.FlagExcluido = 0 OR pcb.FlagExcluido IS NULL) AND (um.FlagExcluido = 0 OR um.FlagExcluido IS NULL)";

                ProdutoGradeController produtoGradeController = new ProdutoGradeController();
                IList<ProdutoResult> lista = produtoDAO.selecionarProdutosPorSqlResult(sql);
                if (lista.Count == 1)
                {
                    foreach (ProdutoResult prod in lista)
                    {
                        IList<ProdutoGrade> listaGrade = produtoGradeController.selecionarGradePorProduto(prod.ProdutoId);
                        if (Sessao.parametroSistema.SelecionarGradeEan == true && listaGrade.Count > 1)
                        {
                            Produto produtoSel = new Produto();
                            produtoSel.Id = prod.ProdutoId;
                            produtoSel = (Produto)Controller.getInstance().selecionar(produtoSel);
                            if (produtoSel.Grade == true)
                            {
                                ProdutoGrade produtoGrade = new ProdutoGrade();
                                produtoGrade = selecionarGrade(produtoSel);

                                if (produtoGrade != null)
                                {
                                    txtPesquisaProduto.Texts = produtoSel.Descricao;
                                    this.produto = produtoSel;
                                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                    txtCodProduto.Texts = produto.Id.ToString();
                                    produto.GradePrincipal = produtoGrade;
                                    txtPesquisaProduto.Texts = "";
                                    txtCodProduto.Texts = "";
                                    AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                                }
                            }
                        }
                        //1 Codigo de barras por grade, nao pergunta grade na tela de vendas se bipar o produto
                        else
                        {
                            txtPesquisaProduto.Texts = prod.DescricaoGrade;
                            produto.Id = prod.ProdutoId;
                            produto = (Produto)Controller.getInstance().selecionar(produto);
                            UnidadeMedida unidadeMedida = new UnidadeMedida();
                            unidadeMedida.Id = prod.UnidadeMedida;
                            unidadeMedida = (UnidadeMedida)Controller.getInstance().selecionar(unidadeMedida);
                            //lblUnidadeMedida.Text = unidadeMedida.Sigla;
                            produto.UnidadeMedida = unidadeMedida;

                            ProdutoGrade prdgrade = new ProdutoGrade();
                            prdgrade.Id = int.Parse(prod.ProdutoGradeId.ToString());
                            prdgrade = (ProdutoGrade)Controller.getInstance().selecionar(prdgrade);
                            produto.GradePrincipal = prdgrade;
                            txtCodProduto.Texts = produto.Id.ToString();
                            AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                        }
                    }
                }
            }
            //Verifica se é Id do produto
            else if (ENumeroMenorQue5Digitos(valor))
            {
                IList<Produto> listaProdutos = new List<Produto>();
                listaProdutos = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.FlagExcluido = false and Tabela.Id = " + valor);
                if (listaProdutos.Count > 0)
                {
                    foreach (Produto prod in listaProdutos)
                    {
                        if (prod.Veiculo == true)
                        {
                            FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(prod, false, true);
                            frmProdutoCadastro.ShowDialog();
                        }
                        if (prod.Grade == true)
                        {
                            ProdutoGrade produtoGrade = new ProdutoGrade();
                            produtoGrade = selecionarGrade(prod);

                            if (produtoGrade != null)
                            {
                                txtCodProduto.Texts = prod.Id.ToString();
                                txtPesquisaProduto.Texts = prod.Descricao;
                               
                                this.produto = prod;
                                this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                produto.GradePrincipal = produtoGrade;
                                AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                            }
                        }
                        else
                        {
                            txtPesquisaProduto.Texts = prod.Descricao;
                            txtCodProduto.Texts = prod.Id.ToString();
                            AdicionarProdutoAoGrid(prod.Id, prod.Descricao, prod.ValorVenda);
                        }
                    }
                }
                else
                {
                    novaPesquisaProdutos();
                }
            }
            //Pesquisa por descricao ou referencia
            else
            {
                novaPesquisaProdutos();
            }
        }
        public bool IsNumericAndHasMoreThan7Digits(string input)
        {
            // Verifica se a string não é nula ou vazia
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Verifica se a string é composta apenas por dígitos e tem mais de 7 caracteres
            return input.All(char.IsDigit) && input.Length > 7;
        }
        public bool ENumeroMenorQue5Digitos(string input)
        {
            // Verifica se a string não é nula ou vazia
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Verifica se a string é composta apenas por dígitos e tem mais de 7 caracteres
            return input.All(char.IsDigit) && input.Length <= 5;
        }

        private void novaPesquisaProdutos()
        {
            object produtoOjeto = new Produto();
            Produto product = new Produto();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaProduto uu = new FrmPesquisaProduto(txtPesquisaProduto.Texts))
                {
                    txtPesquisaProduto.Texts = "";
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
                    switch (uu.showModal(ref product))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmProdutoCadastro form = new FrmProdutoCadastro();
                            Object produtoObj = new Produto();
                            if (form.showModalNovo(ref produtoObj, false) == DialogResult.OK)
                            {
                                txtPesquisaProduto.Texts = ((Produto)produtoObj).Descricao;
                                txtCodProduto.Texts = ((Produto)produtoObj).Id.ToString();
                                produto = ((Produto)produtoObj);
                                puxarGradePorProduto(product);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPesquisaProduto.Texts = product.Descricao;
                            txtCodProduto.Texts = product.Id.ToString();
                            produto = product;
                            puxarGradePorProduto(product);
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
        }
        private void puxarGradePorProduto(Produto prod)
        {
            string valorAux = txtPesquisaProduto.Texts.Trim();
            if (prod.Grade == true)
            {
                ProdutoGrade produtoGrade = new ProdutoGrade();
                produtoGrade = selecionarGrade(prod);

                if (produtoGrade != null)
                {
                    txtPesquisaProduto.Texts = prod.Descricao;

                    this.produto = prod;
                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                    this.produto.GradePrincipal = produtoGrade;
                    AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                }
            }
            else
            {
                this.produto = prod;
                if (prod.Ean.Equals(txtPesquisaProduto.Texts.Trim()) && !String.IsNullOrEmpty(txtPesquisaProduto.Texts))
                    AdicionarProdutoAoGrid(produto.Id, produto.Descricao, produto.ValorVenda);
                if (prod.Veiculo == true)
                {
                    FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(prod, false, true);
                    frmProdutoCadastro.ShowDialog();
                }
            }
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProduto("");
        }

        private List<Produto> produtos = new List<Produto>();
        private void AdicionarProdutoAoGrid(int id, string descricao, decimal valorVenda)
        {
            // Adiciona um novo produto à lista
            produtos.Add(new Produto
            {
                Id = id,
                Descricao = descricao,
                ValorVenda = valorVenda
            });

            // Atualiza a fonte de dados do grid
            gridProdutos.DataSource = null; // Limpa a fonte de dados atual
            gridProdutos.DataSource = produtos;
        }

        private void RemoverProdutoDoGrid(int id)
        {
            // Encontre o produto na lista com o ID correspondente
            Produto produtoRemover = produtos.FirstOrDefault(p => p.Id == id);

            if (produtoRemover != null)
            {
                // Remove o produto da lista
                produtos.Remove(produtoRemover);

                // Atualiza a fonte de dados do grid
                gridProdutos.DataSource = null; // Limpa a fonte de dados atual
                gridProdutos.DataSource = produtos;
            }
        }

        private void gridProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Verifica se há uma linha selecionada
                if (gridProdutos.SelectedItem is Produto produtoSelecionado)
                {
                    // Remove o item do grid
                    RemoverProdutoDoGrid(produtoSelecionado.Id);
                }
            }
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnGerarArquivo_Click(object sender, EventArgs e)
        {
            balancaService.GerarArquivoItens(ObterItensDoGrid());
        }

        private List<ItemBalanca> ObterItensDoGrid()
        {
            var listaItens = new List<ItemBalanca>();

            foreach (var item in gridProdutos.View.Records)
            {
                var produto = item.Data as Produto; // Assumindo que o tipo de dados é Produto

                if (String.IsNullOrEmpty(produto.Ean))
                {
                    //Gerar codigo
                    produto.Ean = "1234567891232";
                }

                if (produto != null)
                {
                    var itemBalanca = new ItemBalanca();


                    itemBalanca.CodigoDepartamento = produto.ProdutoGrupo != null
                        ? produto.ProdutoGrupo.Id.ToString().PadLeft(2, '0')
                        : "01";

                    itemBalanca.TipoProduto = "0";

                    itemBalanca.CodigoItem = produto.Id.ToString("D6"); // Garante que CodigoItem tenha 6 dígitos

                    decimal valorVendaDecimal;
                    if (decimal.TryParse(produto.ValorVenda.ToString(), out valorVendaDecimal))
                    {
                        // Formata o preço para 2 casas decimais e garante que o total tenha exatamente 6 caracteres
                        itemBalanca.Preco = valorVendaDecimal.ToString("0.00").Replace(",", ".").PadLeft(6, '0').Substring(0, 6);
                    }
                    else
                    {
                        itemBalanca.Preco = "000000"; // Valor padrão se não conseguir converter
                    }

                    itemBalanca.DiasValidade = "000"; // Ajuste conforme necessário

                    itemBalanca.Descritivo1 = produto.Descricao?.PadRight(25) ?? "".PadRight(25);

                    itemBalanca.Descritivo2 = produto.Marca != null
                        ? produto.Marca.Descricao?.PadRight(25) ?? "".PadRight(25)
                        : "".PadRight(25);

                    itemBalanca.CodigoInfoExtra = "".PadLeft(6, '0'); // Preenche com zeros se necessário

                    itemBalanca.CodigoImagem = "".PadLeft(4, '0'); // Preenche com zeros

                    itemBalanca.CodigoInfoNutricional = "".PadLeft(6, '0'); // Preenche com zeros

                    itemBalanca.DataValidade = "0"; // Ajuste conforme necessário

                    itemBalanca.DataEmbalagem = "0"; // Ajuste conforme necessário

                    itemBalanca.CodigoFornecedor = "0000"; // Preenche com zeros

                    itemBalanca.Lote = "000000000001"; // Preenche com zeros

                    itemBalanca.CodigoEANEspecial = produto.Ean.PadLeft(13, '0'); // Ajuste para comprimento esperado

                    itemBalanca.VersaoPreco = "1"; // Ajuste conforme necessário

                    itemBalanca.CodigoSom = "0001"; // Preenche com zeros

                    itemBalanca.CodigoTara = "0000"; // Preenche com zeros

                    itemBalanca.CodigoFracionador = "0000"; // Preenche com zeros

                    itemBalanca.CodigoCampoExtra1 = "0000"; // Preenche com zeros

                    itemBalanca.CodigoCampoExtra2 = "0000"; // Preenche com zeros

                    itemBalanca.CodigoConservacao = "0001"; // Preenche com zeros

                    itemBalanca.EANFornecedor = produto.Ean.PadLeft(12, '0'); // Ajuste para comprimento esperado

                    itemBalanca.PercentualGlaciamento = "000000"; // Preenche com zeros

                    itemBalanca.Descritivo3 = "".PadRight(35); // Preenche com espaços

                    itemBalanca.Descritivo4 = "".PadRight(35); // Preenche com espaços

                    itemBalanca.CodigoCampoExtra3 = "".PadLeft(6, '0'); // Preenche com zeros

                    itemBalanca.CodigoCampoExtra4 = "".PadLeft(6, '0'); // Preenche com zeros

                    itemBalanca.CodigoMidia = "".PadLeft(6, '0'); // Preenche com zeros

                    itemBalanca.PrecoPromocional = "".PadLeft(6, '0'); // Preenche com zeros

                    itemBalanca.SolicitacaoFornecedor = "0"; // Ajuste conforme necessário

                    itemBalanca.CodigoFornecedorAssociado = "".PadLeft(4, '0'); // Preenche com zeros

                    itemBalanca.SolicitacaoTara = "0"; // Ajuste conforme necessário

                    itemBalanca.BalancasInativas = "0"; // Ajuste conforme necessário

                    itemBalanca.CodigoEANEspecialG1 = produto.Ean.PadLeft(12, '0'); // Ajuste para comprimento esperado

                    itemBalanca.PercentualGlaciamentoPG = "0000"; // Preenche com zeros

                    listaItens.Add(itemBalanca);
                }
            }

            return listaItens;
        }


    }
}
