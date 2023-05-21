using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.Compras;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Estoques
{
    public partial class FrmEtiquetas : Form
    {
        public FrmEtiquetas()
        {
            InitializeComponent();
            this.grid.DataSource = dsProdutos;
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
                decimal descontoItem = 0;
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
            imprimir();
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            var selectedItem = this.grid.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            dsProdutos.Tables[0].Rows[grid.SelectedIndex].Delete();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
