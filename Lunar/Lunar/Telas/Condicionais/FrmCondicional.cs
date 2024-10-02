using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Utils;
using Lunar.Utils.PesquisasClass;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using MySqlX.XDevAPI.Relational;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.ProdutoDAO;
using Exception = System.Exception;

namespace Lunar.Telas.Condicionais
{
    public partial class FrmCondicional : Form
    {
        IList<Produto> listaProdutoProd = new List<Produto>();
        int indexProduto = 0;
        double quantidadeAtual = 0;
        IList<CondicionalProduto> listaProdutos = new List<CondicionalProduto>();
        Condicional condicional = new Condicional();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        ProdutoController produtoController = new ProdutoController();
        Produto produto = new Produto();
        public FrmCondicional()
        {
            InitializeComponent();
            this.gridProdutos.DataSource = dsProdutos;
            txtDataAbertura.Value = DateTime.Now;
            txtDataPrevisao.Value = DateTime.Now.AddDays(1);
        }
        public FrmCondicional(Condicional condicional)
        {
            InitializeComponent();
            this.gridProdutos.DataSource = dsProdutos;
            get_Condicional(condicional);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //PesquisarProduto(txtPesquisaProduto.Texts);
                string valorPesquisa = txtPesquisaProduto.Texts.Trim(); // Captura o valor digitado no campo de texto

                ProdutoPesquisaService produtoService = new ProdutoPesquisaService();
                var resultado = produtoService.PesquisarProduto(valorPesquisa); // Chama o serviço de pesquisa

                // Aqui você vai verificar o retorno
                if (resultado.produto != null && resultado.grade != null)
                {
                    Produto produto = resultado.produto;
                    ProdutoGrade grade = resultado.grade;

                    // Preenche os campos da tela com os dados retornados
                    txtPesquisaProduto.Texts = produto.Descricao;
                    txtQuantidadeItem.Texts = "1";
                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", grade.ValorVenda);
                    txtValorTotalItem.Texts = string.Format("{0:0.00}", grade.ValorVenda);
                    txtCodProduto.Texts = produto.Id.ToString();
                    txtQuantidadeItem.Focus();
                    txtQuantidadeItem.SelectAll();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Produto não encontrados ou não selecionado!");
                    txtPesquisaProduto.Texts = "";
                }
            }
        }
        public static bool eNumero(string input)
        {
            if (input != "")
                return input.Length < 6 && input.All(char.IsDigit);
            else
                return false;
        }

        private void PesquisarProduto(string valor)
        {
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            //Verifica se é codigo de barras
            if (IsNumericAndHasMoreThan7Digits(valor))
            {
                ProdutoDAO produtoDAO = new ProdutoDAO();
                //Consulta para casos que produtograde podem ser null
                //String sql = "SELECT p.Id AS ProdutoId, p.DESCRICAO AS ProdutoNome, pg.Id AS ProdutoGradeId, pg.Descricao AS DescricaoGrade, COALESCE(um.Descricao, p.UnidadeMedida) AS UnidadeMedida, COALESCE(pg.ValorVenda, p.ValorVenda) AS ValorVenda, COALESCE(pcb.CodigoBarras, (SELECT pcb2.CodigoBarras FROM ProdutoCodigoBarras pcb2 WHERE pcb2.Produto = p.Id LIMIT 1)) AS CodigoBarras FROM Produto p LEFT JOIN ProdutoGrade pg ON p.Id = pg.Produto LEFT JOIN ProdutoCodigoBarras pcb ON pg.Id = pcb.PRODUTOGRADE LEFT JOIN UnidadeMedida um ON pg.UnidadeMedida = um.Id WHERE (pcb.CodigoBarras = '" + valor+"' OR (pg.Id IS NULL AND pcb.CodigoBarras IS NULL AND p.Id IN (SELECT p2.Id FROM Produto p2 LEFT JOIN ProdutoCodigoBarras pcb2 ON p2.Id = pcb2.Produto WHERE pcb2.CodigoBarras = '"+valor+"'))) AND p.FlagExcluido = 0 AND (pg.FlagExcluido = 0 OR pg.FlagExcluido IS NULL) AND (pcb.FlagExcluido = 0 OR pcb.FlagExcluido IS NULL) AND (um.FlagExcluido = 0 OR um.FlagExcluido IS NULL);";

                //Consulta para casos que todos itens tenha uma grade
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
                                    txtCodProduto.Texts = produtoSel.Id.ToString();
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                    this.produto = produtoSel;
                                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                     
                                    //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;

                                    produto.GradePrincipal = produtoGrade;

                                    //inserirItem(this.produto);
                                    txtQuantidadeItem.Focus();
                                    txtQuantidadeItem.Select();
                                }
                            }
                        }
                        //1 Codigo de barras por grade, nao pergunta grade na tela de vendas se bipar o produto
                        else
                        {
                            txtPesquisaProduto.Texts = prod.DescricaoGrade;
                            txtQuantidadeItem.Texts = "1";
                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                            txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
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


                            if (valorAux.Contains("*"))
                                txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                            if (prod.CodigoBarras.Equals(valor.Trim()))
                                inserirItem(this.produto);
                            else
                            {
                                txtQuantidadeItem.Focus();
                                txtQuantidadeItem.Select();
                            }
                        }
                    }
                }
                //Verificando se é um produto de balança
                else if (valor.StartsWith("2") && valor.Substring(1, 1).Equals("0"))
                {
                    ProcessarCodigoBarras(txtPesquisaProduto.Texts.Trim());
                    return;
                }
            }
            //Verifica se é Id do produto
            else if (ENumeroMenorQue5Digitos(valor))
            {
                listaProdutoProd = new List<Produto>();
                listaProdutoProd = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.FlagExcluido = false and Tabela.Id = " + valor);
                if (listaProdutoProd.Count > 0)
                {
                    foreach (Produto prod in listaProdutoProd)
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
                                txtQuantidadeItem.Texts = "1";
                                txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                this.produto = prod;
                                this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                produto.GradePrincipal = produtoGrade;

                                inserirItem(this.produto);
                                txtQuantidadeItem.Focus();
                                txtQuantidadeItem.Select();
                            }
                        }
                        else
                        {
                            txtCodProduto.Texts = prod.Id.ToString();
                            txtPesquisaProduto.Texts = prod.Descricao;
                            txtQuantidadeItem.Texts = "1";
                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                            txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                            this.produto = prod;
                            if (valorAux.Contains("*"))
                                txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                            if (prod.Ean.Equals(valor.Trim()) || prod.Pesavel == true)
                                inserirItem(this.produto);
                            else
                            {
                                txtQuantidadeItem.Focus();
                                txtQuantidadeItem.Select();
                            }
                        }
                    }
                }
                else
                {
                    //pode ser uma referencia, ai ele pesquisa por descricao e referencia
                    PesquisarProdutoPorDescricao(valor);
                }
            }
            //Pesquisa por descricao ou referencia
            else if (valor.Contains("/"))
            {
                listaProdutoProd = new List<Produto>();
                listaProdutoProd = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.FlagExcluido = false and Tabela.Referencia = '" + valor.Replace("/", "") + "'");
                if (listaProdutoProd.Count > 0)
                {
                    foreach (Produto prod in listaProdutoProd)
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
                                txtPesquisaProduto.Texts = prod.Descricao;
                                txtQuantidadeItem.Texts = "1";
                                txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                this.produto = prod;
                                this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;

                                produto.GradePrincipal = produtoGrade;

                                //inserirItem(this.produto);
                                txtQuantidadeItem.Focus();
                                txtQuantidadeItem.Select();
                            }
                        }
                        else
                        {
                            txtPesquisaProduto.Texts = prod.Descricao;
                            txtQuantidadeItem.Texts = "1";
                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                            txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                            this.produto = prod;
                            if (valorAux.Contains("*"))
                                txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                            if (prod.Ean.Equals(valor.Trim()))
                                inserirItem(this.produto);
                            else
                            {
                                txtQuantidadeItem.Focus();
                                txtQuantidadeItem.Select();
                            }
                        }
                    }
                }
                else
                {
                    //pode ser uma referencia, ai ele pesquisa por descricao e referencia
                    PesquisarProdutoPorDescricao(valor);
                }
            }
            else
            {
                novaPesquisaProdutos();
            }
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
                                txtCodProduto.Texts = ((Produto)produtoObj).Id.ToString();
                                txtPesquisaProduto.Texts = ((Produto)produtoObj).Descricao;
                                produto = ((Produto)produtoObj);
                                puxarGradePorProduto(product);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCodProduto.Texts = product.Id.ToString();
                            txtPesquisaProduto.Texts = product.Descricao;
                            produto = product;
                            puxarGradePorProduto(product);
                            //txtPesquisaProduto.Focus();
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
                    txtCodProduto.Texts = prod.Id.ToString();
                    txtPesquisaProduto.Texts = prod.Descricao;
                    txtQuantidadeItem.Texts = "1";
                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                    txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                    this.produto = prod;
                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                    //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                    this.produto.GradePrincipal = produtoGrade;
                    inserirItem(this.produto);
                    txtQuantidadeItem.Focus();
                    txtQuantidadeItem.Select();
                }
            }
            else
            {
                txtCodProduto.Texts = prod.Id.ToString();
                txtQuantidadeItem.Texts = "1";
                txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                this.produto = prod;
                if (txtPesquisaProduto.Texts.Contains("*"))
                    txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                if (prod.Ean.Equals(txtPesquisaProduto.Texts.Trim()) && !String.IsNullOrEmpty(txtPesquisaProduto.Texts))
                    inserirItem(this.produto);
                else
                {
                    txtQuantidadeItem.Focus();
                    txtQuantidadeItem.SelectAll();
                }
                if (prod.Veiculo == true)
                {
                    FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(prod, false, true);
                    frmProdutoCadastro.ShowDialog();
                }
            }
        }
        private void PesquisarProdutoAntigo(string valor)
        {
            IList<Produto> listaProdutos = new List<Produto>();
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            if (eNumero(valor) && valor.Length <=6)
            {
                Produto produto = new Produto();
                produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(valor), Sessao.empresaFilialLogada);
                if (produto != null)
                {
                    if(produto.Id > 0)
                    {
                        listaProdutos.Add(produto);
                    }
                }
            }
            else
                listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {
                foreach (Produto prod in listaProdutos)
                {
                    txtPesquisaProduto.Texts = prod.Descricao;
                    txtQuantidadeItem.Texts = "1";
                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtDescontoItem.Texts = string.Format("{0:0.00}", 0);
                    txtAcrescimoItem.Texts = string.Format("{0:0.00}", 0);
                    txtCodProduto.Texts = prod.Id.ToString();
                    this.produto = prod;
                    if (valorAux.Contains("*"))
                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    if (prod.Ean.Equals(valor.Trim()))
                        inserirItem(this.produto);
                    else if (prod.Id.ToString().Equals(valor.Trim()))
                        inserirItem(this.produto);
                    else
                    {
                        txtQuantidadeItem.Focus();
                        txtQuantidadeItem.Select();
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
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                        inserirItem(this.produto);
                                    else
                                    {
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.SelectAll();
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtQuantidadeItem.Texts = "1";
                                txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                this.produto = ((Produto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                    inserirItem(this.produto);
                                else
                                {
                                    txtQuantidadeItem.Focus();
                                    txtQuantidadeItem.SelectAll();
                                }
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
                txtPesquisaProduto.SelectAll();
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
        private bool verificaSeItemJaExiste(Produto produto)
        {
            if (gridProdutos.View != null)
            {
                var records = gridProdutos.View.Records;
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    if (produto.Id == int.Parse(dataRowView.Row["Codigo"].ToString()))
                    {
                        indexProduto = gridProdutos.TableControl.ResolveToRowIndex(record);
                        quantidadeAtual = double.Parse(dataRowView.Row["Quantidade"].ToString());
                        return true;
                    }
                }
                return false;
            }
            else
                return false;
        }
        private void inserirItem(Produto produto)
        {
            if (verificaSeItemJaExiste(produto) == false)
            {
                txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
                txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
                txtValorTotalItem.TextAlign = HorizontalAlignment.Center;
                try
                {
                    System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                    row.SetField("Id", 0);
                    row.SetField("Codigo", produto.Id.ToString());
                    row.SetField("Descricao", produto.Descricao);
                    decimal valorUnitForm = decimal.Parse(txtValorUnitarioItem.Texts);
                    row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                    row.SetField("Quantidade", txtQuantidadeItem.Texts);
                    decimal valorTotal = decimal.Parse(txtValorTotalItem.Texts);
                    row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                    row.SetField("Desconto", string.Format("{0:0.00}", decimal.Parse(txtDescontoItem.Texts)));
                    row.SetField("Acrescimo", string.Format("{0:0.00}", decimal.Parse(txtAcrescimoItem.Texts)));
                    dsProdutos.Tables[0].Rows.Add(row);

                    txtPesquisaProduto.Texts = "";
                    somaEnquantoDigitaItens();
                    txtQuantidadeItem.Texts = "1";
                    txtValorUnitarioItem.Texts = "0,00";
                    txtValorTotalItem.Texts = "0,00";
                    txtDescontoItem.Texts = "0,00";
                    txtAcrescimoItem.Texts = "0,00";
                    txtCodProduto.Texts = "";
                    txtPesquisaProduto.Focus();

                    this.produto = new Produto();

                    if (this.gridProdutos.View != null)
                    {
                        if (this.gridProdutos.View.Records.Count > 0)
                        {
                            gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                            //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                            this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                            gridProdutos.AutoSizeController.Refresh();
                        }
                    }

                    txtPesquisaProduto.Focus();
                }
                catch
                {
                    GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
                }
            }
            //Se o produto ja existe no grid vamos apenas alterar a quantidade
            else
            {
                decimal valorUnitForm = decimal.Parse(txtValorUnitarioItem.Texts);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(indexProduto), gridProdutos.Columns["ValorUnitario"].MappingName, string.Format("{0:0.00}", valorUnitForm));
                double qtd = quantidadeAtual + double.Parse(txtQuantidadeItem.Texts);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(indexProduto), gridProdutos.Columns["Quantidade"].MappingName, qtd.ToString());
                decimal valorTotal = valorUnitForm * decimal.Parse(qtd.ToString());
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(indexProduto), gridProdutos.Columns["ValorTotal"].MappingName, string.Format("{0:0.00}", valorTotal));

                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(indexProduto), gridProdutos.Columns["Desconto"].MappingName, string.Format("{0:0.00}", decimal.Parse(txtDescontoItem.Texts)));
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(indexProduto), gridProdutos.Columns["Acrescimo"].MappingName, decimal.Parse(txtAcrescimoItem.Texts));
                
                txtPesquisaProduto.Texts = "";
                somaEnquantoDigitaItens();
                txtQuantidadeItem.Texts = "1";
                txtValorUnitarioItem.Texts = "0,00";
                txtValorTotalItem.Texts = "0,00";
                txtDescontoItem.Texts = "0,00";
                txtAcrescimoItem.Texts = "0,00";
                txtCodProduto.Texts = "";
                txtPesquisaProduto.Focus();

                this.produto = new Produto();

                if (this.gridProdutos.View != null)
                {
                    if (this.gridProdutos.View.Records.Count > 0)
                    {
                        gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                        this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridProdutos.AutoSizeController.Refresh();
                    }
                }
            }
        }

        private void somaEnquantoDigitaItens()
        {
            try
            {
                decimal valorTotalProdutos = 0;
                double pecas = 0;
                if (gridProdutos.View != null)
                {
                    var records = gridProdutos.View.Records;
                    decimal descontoItem = 0;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;

                        if (!String.IsNullOrEmpty(dataRowView.Row[0].ToString()))
                            descontoItem = descontoItem + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        valorTotalProdutos = valorTotalProdutos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());

                    }
                    txtTotalPeças.Texts = pecas.ToString();
                    txtValorTotalTodosProdutos.Texts = string.Format("{0:0.00}", valorTotalProdutos);
                }
            }
            catch
            {

            }
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProduto("");
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        //private void pesquisaCliente()
        //{
        //    Object pessoaOjeto = new Pessoa();
        //    Form formBackground = new Form();
        //    try
        //    {
        //        using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%' and Tabela.Cliente = true"))
        //        {
        //            formBackground.StartPosition = FormStartPosition.Manual;
        //            //formBackground.FormBorderStyle = FormBorderStyle.None;
        //            formBackground.Opacity = .50d;
        //            formBackground.BackColor = Color.Black;
        //            //formBackground.Left = Top = 0;
        //            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
        //            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
        //            formBackground.WindowState = FormWindowState.Maximized;
        //            formBackground.TopMost = false;
        //            formBackground.Location = this.Location;
        //            formBackground.ShowInTaskbar = false;
        //            formBackground.Show();
        //            uu.Owner = formBackground;
        //            switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
        //            {
        //                case DialogResult.Ignore:
        //                    uu.Dispose();
        //                    FrmClienteCadastro form = new FrmClienteCadastro();
        //                    if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
        //                    {
        //                        txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                        txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                    }
        //                    form.Dispose();
        //                    break;
        //                case DialogResult.OK:
        //                    txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                    break;
        //            }
        //            txtVendedor.Focus();
        //            formBackground.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        formBackground.Dispose();
        //    }
        //}

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtCliente.Texts))
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
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
                    switch (uu.showModal(ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtCliente.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                txtVendedor.Focus();
                                genericaDesktop.buscarAlertaCadastrado((Pessoa)pessoaObj);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtVendedor.Focus();
                            genericaDesktop.buscarAlertaCadastrado((Pessoa)pessoaOjeto);
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

      


        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true"))
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            {
                                txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            txtCodVendedor.Texts = "";
            txtVendedor.Texts = "";
            pesquisaVendedor();
        }

        private void txtQuantidadeItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtQuantidadeItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtValorUnitarioItem.Focus();
            }
        }

        private void txtValorUnitarioItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValorUnitarioItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDescontoItem.Focus();
            }
        }

        private void txtDescontoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtAcrescimoItem.Focus();
            }
        }

        private void txtAcrescimoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtAcrescimoItem.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmaItem.Focus();
                btnConfirmaItem.PerformClick();
            }
        }

        private void txtQuantidadeItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void txtValorUnitarioItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void txtDescontoItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void txtAcrescimoItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void calculaTotalItem()
        {
            try
            {
                if (String.IsNullOrEmpty(txtQuantidadeItem.Texts))
                    txtQuantidadeItem.Texts = "1";
                if (String.IsNullOrEmpty(txtDescontoItem.Texts))
                    txtDescontoItem.Texts = "0,00";
                if (String.IsNullOrEmpty(txtAcrescimoItem.Texts))
                    txtAcrescimoItem.Texts = "0,00";

                txtValorTotalItem.Texts = string.Format("{0:0.00}", ((decimal.Parse(txtValorUnitarioItem.Texts) * decimal.Parse(txtQuantidadeItem.Texts)) - decimal.Parse(txtDescontoItem.Texts)) + decimal.Parse(txtAcrescimoItem.Texts));
            }
            catch
            {

            }
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnConfirmaItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts))
            {
                produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)Controller.getInstance().selecionar(produto);
                inserirItem(produto);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione um produto");
            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o produto selecionado?"))
                {
                    var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        CondicionalProduto condicionalProduto = new CondicionalProduto();
                        condicionalProduto.Id = int.Parse(id);
                        condicionalProduto = (CondicionalProduto)Controller.getInstance().selecionar(condicionalProduto);
                        if (condicionalProduto != null)
                            Controller.getInstance().excluir(condicionalProduto);
                        Condicional cond = condicionalProduto.Condicional;
                        cond.QtdPeca = cond.QtdPeca - condicionalProduto.Quantidade;
                        Controller.getInstance().salvar(cond);
                    }
                    dsProdutos.Tables[0].Rows[gridProdutos.SelectedIndex].Delete();
                    somaEnquantoDigitaItens();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o produto que deseja excluir!");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            set_Condicional();
            this.Close();
        }

        private void set_Condicional()
        {
            condicional = new Condicional();
            if (!String.IsNullOrEmpty(txtNumeroCondicional.Texts))
            {
                condicional.Id = int.Parse(txtNumeroCondicional.Texts);
                condicional = (Condicional)Controller.getInstance().selecionar(condicional);
            }
            else
            {
                condicional.Id = 0;
            }

            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                if (pessoa != null)
                {
                    Pessoa vendedor = new Pessoa();
                    if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
                    {
                        vendedor.Id = int.Parse(txtCodVendedor.Texts);
                        vendedor = (Pessoa)Controller.getInstance().selecionar(vendedor);
                    }
                    if (vendedor != null)
                    {
                        if (vendedor.Id > 0)
                            condicional.Vendedor = vendedor;
                        else 
                            condicional.Vendedor = null;
                    }
                    condicional.Cliente = pessoa;
                    condicional.Data = DateTime.Parse(txtDataAbertura.Value.ToString());
                    condicional.DataEncerramento = DateTime.Parse("1900-01-01 00:00:00");
                    condicional.DataPrevisao = DateTime.Parse(txtDataPrevisao.Value.ToString());
                    condicional.Encerrado = false;
                    condicional.Filial = Sessao.empresaFilialLogada;
                    condicional.Observacoes = "";
                    try { condicional.QtdPeca = double.Parse(txtTotalPeças.Texts); } catch { condicional.QtdPeca = 0; }
                    condicional.ValorTotal = decimal.Parse(txtValorTotalTodosProdutos.Texts.Replace("R$ ", ""));
                    capturarProdutos();
                    CondicionalController condicionalController = new CondicionalController();
                    condicionalController.salvarCondicionalComProdutos(condicional, listaProdutos);
                    lblAutomatico.Visible = false;
                    txtNumeroCondicional.Texts = condicional.Id.ToString();
                    if (GenericaDesktop.ShowConfirmacao("Condicional " + condicional.Id + " Registrada com Sucesso, deseja imprimir?"))
                    {
                        FrmImprimirCondicional frmImprimirOrdem = new FrmImprimirCondicional(condicional);
                        frmImprimirOrdem.ShowDialog();
                    }
                }
            }
            else
                GenericaDesktop.ShowErro("Você deve selecionar um cliente para realizar a Condicional!");
        }

        private void capturarProdutos()
        {
            listaProdutos = new List<CondicionalProduto>();

            decimal acrescimoProdutos = 0;
            decimal descontoProdutos = 0;
            decimal totalProdutos = 0;

            if (gridProdutos.View != null)
            {
                var records = gridProdutos.View.Records;
                if (records.Count > 0)
                {
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;
                        acrescimoProdutos = acrescimoProdutos + decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                        descontoProdutos = descontoProdutos + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        totalProdutos = totalProdutos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());

                        produto = new Produto();
                        CondicionalProduto condicionalProduto = new CondicionalProduto();
                        produto.Id = int.Parse(dataRowView.Row["Codigo"].ToString());
                        produto = (Produto)Controller.getInstance().selecionar(produto);
                        if (produto != null)
                        {
                            condicionalProduto = new CondicionalProduto();
                            if (int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                            {
                                condicionalProduto.Id = int.Parse(dataRowView.Row["Id"].ToString());
                            }
                            condicionalProduto.CodigoProduto = produto.Id + "" + produto.IdComplementar;
                            condicionalProduto.DescricaoProduto = dataRowView.Row["Descricao"].ToString();
                            condicionalProduto.Desconto = decimal.Parse(dataRowView.Row["Desconto"].ToString());
                            condicionalProduto.Acrescimo = decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                            condicionalProduto.Condicional = condicional;
                            condicionalProduto.Produto = produto;
                            condicionalProduto.Quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                            condicionalProduto.ValorUnitario = decimal.Parse(dataRowView.Row["ValorUnitario"].ToString());
                            condicionalProduto.ValorTotal = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                            listaProdutos.Add(condicionalProduto);
                        }
                    }
                }
            }
        }

        private void get_Condicional(Condicional condicional)
        {
            lblAutomatico.Visible = false;
            txtNumeroCondicional.TextAlign = HorizontalAlignment.Center;
            txtNumeroCondicional.Texts = condicional.Id.ToString();
            txtCliente.Texts = condicional.Cliente.RazaoSocial;
            txtCodCliente.Texts = condicional.Cliente.Id.ToString();
            txtDataAbertura.Value = condicional.Data;
            txtDataPrevisao.Value = condicional.DataPrevisao;
            if (condicional.Vendedor != null)
            {
                txtVendedor.Texts = condicional.Vendedor.RazaoSocial;
                txtCodVendedor.Texts = condicional.Vendedor.Id.ToString();
            }
            else
            {
                txtVendedor.Texts = "";
                txtCodVendedor.Texts = "";
            }
            CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
            IList<CondicionalProduto> listaProdutos = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);
            if(listaProdutos.Count > 0)
            {
                dsProdutos.Tables[0].Clear();
                foreach(CondicionalProduto condicionalProduto in listaProdutos)
                {
                    System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                    row.SetField("Id", condicionalProduto.Id.ToString());
                    row.SetField("Codigo", condicionalProduto.Produto.Id.ToString());
                    row.SetField("Descricao", condicionalProduto.Produto.Descricao);
                    row.SetField("ValorUnitario", string.Format("{0:0.00}", condicionalProduto.ValorUnitario));
                    row.SetField("Quantidade", condicionalProduto.Quantidade);
                    row.SetField("ValorTotal", string.Format("{0:0.00}", condicionalProduto.ValorTotal));
                    row.SetField("Desconto", string.Format("{0:0.00}", condicionalProduto.Desconto));
                    row.SetField("Acrescimo", string.Format("{0:0.00}", condicionalProduto.Acrescimo));
                    dsProdutos.Tables[0].Rows.Add(row);
                }
                gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                gridProdutos.AutoSizeController.Refresh();
                somaEnquantoDigitaItens();
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                pesquisaVendedor();
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

        private void ProcessarCodigoBarras(string codigoBarras)
        {
            // Ignora o primeiro dígito verificador
            string codigoProdutoComZeros = codigoBarras.Substring(1, 6); // Captura os 6 dígitos do código do produto
            string codigoProduto = codigoProdutoComZeros.TrimStart('0');
            string valorTotalString = codigoBarras.Substring(7, 5);
            decimal valorTotal = Convert.ToDecimal(valorTotalString) / 100;
            Produto produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(codigoProduto), Sessao.empresaFilialLogada); // Método para obter o produto pelo código

            if (produto != null)
            {
                decimal quantidade = valorTotal / produto.ValorVenda;
                if (quantidade.ToString().Length >= 6)
                    txtPesquisaProduto.Texts = quantidade.ToString().Substring(0, 6) + "*" + codigoProduto;
                else
                    txtPesquisaProduto.Texts = quantidade.ToString() + "*" + codigoProduto;
                PesquisarProduto(txtPesquisaProduto.Texts.Trim());
                //MessageBox.Show($"Código do Produto: {codigoProduto}\nValor Total: {valorTotal:C}\nQuantidade: {quantidade}");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Produto não encontrado");
            }
        }
        private async void PesquisarProdutoPorDescricao(string valor)
        {
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            //lblCarregando.Visible = true;
            listaProdutoProd = await Task.Run(() => produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada));
            //lblCarregando.Visible = false;

            //listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutoProd.Count == 1)
            {
                foreach (Produto prod in listaProdutoProd)
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
                            txtQuantidadeItem.Texts = "1";
                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                            txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                            this.produto = prod;
                            this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                            //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                            this.produto.GradePrincipal = produtoGrade;
                            //inserirItem(this.produto);
                            txtQuantidadeItem.Focus();
                            txtQuantidadeItem.Select();
                        }
                    }
                    else
                    {
                        txtCodProduto.Texts = prod.Id.ToString();
                        txtPesquisaProduto.Texts = prod.Descricao;
                        txtQuantidadeItem.Texts = "1";
                        txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                        txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                        this.produto = prod;
                        if (valorAux.Contains("*"))
                            txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                        if (prod.Ean.Equals(valor.Trim()))
                            inserirItem(this.produto);
                        else
                        {
                            txtQuantidadeItem.Focus();
                            txtQuantidadeItem.Select();
                        }
                    }
                }
            }
            else if (listaProdutos.Count > 1 && listaProdutos.Count < 50)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia) like '%" + valor + "%'"))
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
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (this.produto.Grade == true)
                                    {
                                        ProdutoGrade produtoGrade = new ProdutoGrade();
                                        produtoGrade = selecionarGrade(produto);

                                        if (produtoGrade != null)
                                        {
                                            txtPesquisaProduto.Texts = produto.Descricao;
                                            txtQuantidadeItem.Texts = "1";
                                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                            //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                            this.produto.GradePrincipal = produtoGrade;
                                            //inserirItem(this.produto);
                                            txtQuantidadeItem.Focus();
                                            txtQuantidadeItem.Select();
                                        }
                                    }
                                    else
                                    {
                                        if (valorAux.Contains("*"))
                                            txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                        if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                            inserirItem(this.produto);
                                        else
                                        {
                                            txtQuantidadeItem.Focus();
                                            txtQuantidadeItem.SelectAll();
                                        }
                                        if (((Produto)produtoOjeto).Veiculo == true)
                                        {
                                            FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                            frmProdutoCadastro.ShowDialog();
                                        }
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;

                                Produto prod = ((Produto)produtoOjeto);
                                if (prod.Grade == true)
                                {
                                    ProdutoGrade produtoGrade = new ProdutoGrade();
                                    produtoGrade = selecionarGrade(prod);

                                    if (produtoGrade != null)
                                    {
                                        txtCodProduto.Texts = prod.Id.ToString();
                                        txtPesquisaProduto.Texts = prod.Descricao;
                                        txtQuantidadeItem.Texts = "1";
                                        txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        this.produto = prod;
                                        this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                       // lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                        this.produto.GradePrincipal = produtoGrade;
                                        //inserirItem(this.produto);
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.Select();
                                    }
                                }
                                else
                                {
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                        inserirItem(this.produto);
                                    else
                                    {
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.SelectAll();
                                    }
                                    if (((Produto)produtoOjeto).Veiculo == true)
                                    {
                                        FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                        frmProdutoCadastro.ShowDialog();
                                    }
                                }
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
            else if (listaProdutoProd.Count >= 50)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao(listaProdutoProd))
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
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (this.produto.Grade == true)
                                    {
                                        ProdutoGrade produtoGrade = new ProdutoGrade();
                                        produtoGrade = selecionarGrade(produto);

                                        if (produtoGrade != null)
                                        {
                                            txtPesquisaProduto.Texts = produto.Descricao;
                                            txtQuantidadeItem.Texts = "1";
                                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                           // lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                            this.produto.GradePrincipal = produtoGrade;
                                            //inserirItem(this.produto);
                                            txtQuantidadeItem.Focus();
                                            txtQuantidadeItem.Select();
                                        }
                                    }
                                    else
                                    {
                                        if (valorAux.Contains("*"))
                                            txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                        if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                            inserirItem(this.produto);
                                        else
                                        {
                                            txtQuantidadeItem.Focus();
                                            txtQuantidadeItem.SelectAll();
                                        }
                                        if (((Produto)produtoOjeto).Veiculo == true)
                                        {
                                            FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                            frmProdutoCadastro.ShowDialog();
                                        }
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:

                                
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                Produto prod = ((Produto)produtoOjeto);
                                if (prod.Grade == true)
                                {
                                    ProdutoGrade produtoGrade = new ProdutoGrade();
                                    produtoGrade = selecionarGrade(prod);

                                    if (produtoGrade != null)
                                    {
                                        txtCodProduto.Texts = prod.Id.ToString();
                                        txtPesquisaProduto.Texts = prod.Descricao;
                                        txtQuantidadeItem.Texts = "1";
                                        txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        txtValorTotalItem.Texts = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        this.produto = prod;
                                        this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                        //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                        this.produto.GradePrincipal = produtoGrade;
                                        //inserirItem(this.produto);
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.Select();
                                    }
                                }
                                else
                                {
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                        inserirItem(this.produto);
                                    else
                                    {
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.SelectAll();
                                    }
                                    if (((Produto)produtoOjeto).Veiculo == true)
                                    {
                                        FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                        frmProdutoCadastro.ShowDialog();
                                    }
                                }
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
            else
            {
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtPesquisaProduto.SelectAll();
            }
        }
    }
}
