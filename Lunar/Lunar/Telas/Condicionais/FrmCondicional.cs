using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Condicionais
{
    public partial class FrmCondicional : Form
    {
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
                PesquisarProduto(txtPesquisaProduto.Texts);
            }
        }

        private void PesquisarProduto(string valor)
        {
            IList<Produto> listaProdutos = new List<Produto>();
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

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
                    else
                        return false;
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
                pesquisaCliente();
            }
        }

        private void pesquisaCliente()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%' and Tabela.Cliente = true"))
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
                                txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            break;
                    }
                    txtVendedor.Focus();
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

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            txtCodCliente.Texts = "";
            txtCliente.Texts = "";
            pesquisaCliente();
        }
        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%' and Tabela.Vendedor = true"))
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
                        condicional.Vendedor = vendedor;
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
    }
}
