using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Lunar.Telas.Condicionais
{
    public partial class FrmDevolucaoCondicional : Form
    {
        double quantidadeProdutosCondicional = 0;
        CondicionalDevolucaoController condicionalDevolucaoController = new CondicionalDevolucaoController();
        CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
        Condicional condicional = new Condicional();
        CondicionalProduto condicionalproduto = new CondicionalProduto();
        public FrmDevolucaoCondicional(Condicional condicional)
        {
            InitializeComponent();
            this.condicional = condicional;
            txtNumeroCondicional.Texts = condicional.Id.ToString();
            txtCliente.Texts = condicional.Cliente.RazaoSocial;
            txtCodCliente.Texts = condicional.Cliente.Id.ToString();
            txtCodCliente.TextAlign = HorizontalAlignment.Center;
            txtNumeroCondicional.TextAlign = HorizontalAlignment.Center;
            gridDevolucao.DataSource = dsProdutoDevolucao;
            gridProdutos.DataSource = dsProdutos;
            retornaProdutosCondicional();
            retornaProdutosDevolvidos();
        }

        private void retornaProdutosCondicional()
        {
            quantidadeProdutosCondicional = 0;
            IList<CondicionalProduto> listaProdutosCondicional = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);
            if (listaProdutosCondicional.Count > 0)
            {
                dsProdutos.Tables[0].Clear();
                IList<SobraProduto> listaSobra = new List<SobraProduto>();
                foreach (CondicionalProduto condProduto in listaProdutosCondicional)
                {
                    quantidadeProdutosCondicional = quantidadeProdutosCondicional + condProduto.Quantidade;
                    CondicionalDevolucaoDAO condDAO = new CondicionalDevolucaoDAO();
                    double quantidadeDevolvida = condDAO.calcularQuantidadeProdutoDevolvido(condProduto.Condicional.Id, condProduto.Produto.Id);
                    double saldo = 0;
                    System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                    row.SetField("Id", condProduto.Id.ToString());
                    row.SetField("Codigo", condProduto.Produto.Id.ToString());
                    row.SetField("Descricao", condProduto.Produto.Descricao);
                    row.SetField("ValorUnitario", string.Format("{0:0.00}", condProduto.ValorUnitario));
                    row.SetField("Quantidade", condProduto.Quantidade);
                    if (condProduto.Quantidade >= quantidadeDevolvida) 
                    {
                        row.SetField("QuantidadeDevolvida", quantidadeDevolvida);
                        saldo = condProduto.Quantidade - quantidadeDevolvida;
                    }
                    else
                    {
                        row.SetField("QuantidadeDevolvida", condProduto.Quantidade);
                        saldo = 0;
                        SobraProduto sobraProduto = new SobraProduto();
                        sobraProduto.Produto = condProduto.Produto;
                        sobraProduto.QuantidadeSobra = quantidadeDevolvida - condProduto.Quantidade;
                        listaSobra.Add(sobraProduto);
                    }
                    row.SetField("Saldo", saldo);
                    row.SetField("ValorTotal", string.Format("{0:0.00}", condProduto.ValorTotal));
                    row.SetField("Desconto", string.Format("{0:0.00}", condProduto.Desconto));
                    row.SetField("Acrescimo", string.Format("{0:0.00}", condProduto.Acrescimo));
                    dsProdutos.Tables[0].Rows.Add(row);
                }
                if (listaSobra.Count > 0)
                {
                    foreach (SobraProduto sobraProduto in listaSobra)
                    {
                        var records = gridProdutos.View.Records;
                        if (records.Count > 0)
                        {
                            foreach (var record in records)
                            {
                                condicionalproduto = new CondicionalProduto();
                                var dataRowView = record.Data as DataRowView;
                                condicionalproduto.Id = int.Parse(dataRowView.Row["Id"].ToString());
                                double sald = double.Parse(dataRowView.Row["Saldo"].ToString());
                                condicionalproduto = (CondicionalProduto)Controller.getInstance().selecionar(condicionalproduto);

                                //if (condicionalproduto.Produto.Id == sobraProduto.Produto.Id && (sald > 0))
                                //{

                                //}
                            }
                        }
                    }
                }
                gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                gridProdutos.AutoSizeController.Refresh();
            }
        }

        private class SobraProduto
        {
            private Produto produto;
            private double quantidadeSobra;
            public Produto Produto { get => produto; set => produto = value; }
            public double QuantidadeSobra { get => quantidadeSobra; set => quantidadeSobra = value; }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PesquisarProduto(string valor)
        {
            IList<CondicionalProduto> listaProdutos = new List<CondicionalProduto>();
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtCodProduto.TextAlign = HorizontalAlignment.Center;
            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            listaProdutos = condicionalProdutoController.selecionarProdutosCondicionalComVariosFiltros(valor);
            if (listaProdutos.Count == 1)
            {
                foreach (CondicionalProduto prod in listaProdutos)
                {
                    txtPesquisaProduto.Texts = prod.DescricaoProduto;
                    txtQuantidadeItem.Texts = "1";
                    txtCodProduto.Texts = prod.Id.ToString();
                    this.condicionalproduto = prod;
                    if (valorAux.Contains("*"))
                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    if (condicionalproduto.Produto.Ean.Equals(valor.Trim()))
                        inserirItem(this.condicionalproduto);
                    else
                    {
                        txtQuantidadeItem.Focus();
                        txtQuantidadeItem.Select();
                    }
                }
            }
            else if (listaProdutos.Count > 1)
            {
                Object produtoOjeto = new CondicionalProduto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("CondicionalProduto", "and CONCAT(Tabela.DescricaoProduto, ' ', Tabela.Produto.Descricao, ' ', Tabela.Produto.Ean, ' ', Tabela.Produto.Referencia, ' ', Tabela.Produto.Ncm) like '%" + valor + "%' and Tabela.Condicional = " + txtNumeroCondicional.Texts.Trim()))
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
                        switch (uu.showModal("CondicionalProduto", "", ref produtoOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                break;
                            case DialogResult.OK:
                                txtPesquisaProduto.Texts = ((CondicionalProduto)produtoOjeto).DescricaoProduto;
                                txtQuantidadeItem.Texts = "1";
                                txtCodProduto.Texts = ((CondicionalProduto)produtoOjeto).Produto.Id.ToString() + ((CondicionalProduto)produtoOjeto).Produto.IdComplementar;
                                this.condicionalproduto = ((CondicionalProduto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                if (((CondicionalProduto)produtoOjeto).Produto.Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                    inserirItem(this.condicionalproduto);
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

        private void inserirItem(CondicionalProduto condicionalproduto)
        {
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtCodProduto.TextAlign = HorizontalAlignment.Center;
            try
            {
                if (validaDevolucao(condicionalproduto.Produto, double.Parse(txtQuantidadeItem.Texts)))
                {
                    CondicionalDevolucao condicionalDevolucao = new CondicionalDevolucao();
                    condicionalDevolucao.Id = 0;
                    condicionalDevolucao.Condicional = condicional;
                    condicionalDevolucao.Observacoes = "DEVOLUÇÃO EM " + DateTime.Now + " USUÁRIO: " + Sessao.usuarioLogado;
                    condicionalDevolucao.DataDevolucao = DateTime.Now;
                    condicionalDevolucao.Quantidade = double.Parse(txtQuantidadeItem.Texts);
                    condicionalDevolucao.Produto = condicionalproduto.Produto;
                    Controller.getInstance().salvar(condicionalDevolucao);

                    retornaProdutosDevolvidos();

                    txtPesquisaProduto.Texts = "";
                    txtQuantidadeItem.Texts = "1";
                    txtCodProduto.Texts = "";
                    txtPesquisaProduto.Focus();

                    this.condicionalproduto = new CondicionalProduto();

                    if (this.gridDevolucao.View != null)
                    {
                        if (this.gridDevolucao.View.Records.Count > 0)
                        {
                            gridDevolucao.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                            //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                            this.gridDevolucao.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                            gridDevolucao.AutoSizeController.Refresh();
                        }
                    }
                }
                txtPesquisaProduto.Focus();
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto e quantidade");
            }
        }

        private void retornaProdutosDevolvidos()
        {
            double quantidadeDevolvido = 0;
            dsProdutoDevolucao.Tables[0].Clear();
            IList<CondicionalDevolucao> listaDevolvidos = condicionalDevolucaoController.selecionarProdutosDevolvidosPorCondicional(condicional.Id);
            foreach (CondicionalDevolucao condicionalDevolucao1 in listaDevolvidos)
            {
                System.Data.DataRow row = dsProdutoDevolucao.Tables[0].NewRow();
                row.SetField("Id", condicionalDevolucao1.Id.ToString());
                row.SetField("Codigo", condicionalDevolucao1.Produto.Id.ToString());
                row.SetField("Descricao", condicionalDevolucao1.Produto.Descricao);
                row.SetField("ItemExcluido", "");
                row.SetField("QuantidadeDevolvida", condicionalDevolucao1.Quantidade);
                row.SetField("DataDevolucao", condicionalDevolucao1.DataDevolucao.ToShortDateString());
                quantidadeDevolvido = quantidadeDevolvido + condicionalDevolucao1.Quantidade;
                dsProdutoDevolucao.Tables[0].Rows.Add(row);
            }
            gridDevolucao.AutoSizeController.ResetAutoSizeWidthForAllColumns();
            this.gridDevolucao.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
            gridDevolucao.AutoSizeController.Refresh();
            if (quantidadeDevolvido == quantidadeProdutosCondicional)
            {
                txtPesquisaProduto.Enabled = false;
                txtQuantidadeItem.Enabled = false;
                btnConfirmaItem.Enabled = false;
                lblInformativo.Text = "Todos produtos da condicional foram devolvidos!";
                lblInformativo.Visible = true;
            }
        }

        //private void avaliarItemSeJaFoiDevolvidoTotalmente()
        //{
        //    CondicionalDevolucao condicionalDevolucao = new CondicionalDevolucao();
        //    var records = gridDevolucao.View.Records;
        //    if (records.Count > 0)
        //    {
        //        foreach (var record in records)
        //        {
        //            var dataRowView = record.Data as DataRowView;
        //            condicionalDevolucao.Id = int.Parse(dataRowView.Row["Id"].ToString());
        //            condicionalDevolucao = (CondicionalDevolucao)condicionalDevolucaoController.selecionar(condicionalDevolucao);
        //            Produto produto = new Produto();
        //            produto = condicionalDevolucao.Produto;

        //            IList<Produto> listaProdutoDevolvido = new List<Produto>();
        //            listaProdutoDevolvido.
        //        }
        //        //Se esse produto ja foi devolvido anteriormente, bloqueia a devolucao
        //        if (quantidadeProduto == quantidadeItemLevado)
        //        {
        //            GenericaDesktop.ShowErro("Este Produto já foi Totalmente Devolvido!");
        //            txtCodProduto.Texts = "";
        //            txtPesquisaProduto.Texts = "";
        //            txtQuantidadeItem.Texts = "1";
        //            return false;
        //        }
        //        //se a devolucao atual + a soma do q ja foi devolvido desse item ultrapassa a qtd levada, da erro tb.
        //        if (quantidadeProduto + quantidadeDevolvendo > quantidadeItemLevado)
        //        {
        //            GenericaDesktop.ShowErro("Quantidade informada de " + quantidadeDevolvendo + " ultrapassa a quantidade restante desse produto!");
        //            txtCodProduto.Texts = "";
        //            txtPesquisaProduto.Texts = "";
        //            txtQuantidadeItem.Texts = "1";
        //            return false;
        //        }
        //        else
        //            return true;
        //    }
        //}
        private bool validaDevolucao(Produto produto, double quantidadeDevolvendo)
        {
            CondicionalDevolucao condicionalDevolucao = new CondicionalDevolucao();
            double quantidadeProduto = 0;
            double quantidadeItemLevado = 0;
            IList<CondicionalProduto> listaLevados = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);
            foreach (CondicionalProduto condProd in listaLevados)
            {
                if (condProd.Produto.Id == produto.Id)
                {
                    quantidadeItemLevado = quantidadeItemLevado + condProd.Quantidade;
                }
            }
            var records = gridDevolucao.View.Records;
            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    condicionalDevolucao.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    condicionalDevolucao = (CondicionalDevolucao)condicionalDevolucaoController.selecionar(condicionalDevolucao); 
                    CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
                    if (condicionalDevolucao.Produto.Id == produto.Id)
                    {
                        quantidadeProduto = quantidadeProduto + condicionalDevolucao.Quantidade;
                    }
                }
                //Se esse produto ja foi devolvido anteriormente, bloqueia a devolucao
                if (quantidadeProduto == quantidadeItemLevado)
                {
                    GenericaDesktop.ShowErro("Este Produto já foi Totalmente Devolvido!");
                    txtCodProduto.Texts = "";
                    txtPesquisaProduto.Texts = "";
                    txtQuantidadeItem.Texts = "1";
                    return false;
                }
                //se a devolucao atual + a soma do q ja foi devolvido desse item ultrapassa a qtd levada, da erro tb.
                if(quantidadeProduto + quantidadeDevolvendo > quantidadeItemLevado)
                {
                    GenericaDesktop.ShowErro("Quantidade informada de " + quantidadeDevolvendo + " ultrapassa a quantidade restante desse produto!");
                    txtCodProduto.Texts = "";
                    txtPesquisaProduto.Texts = "";
                    txtQuantidadeItem.Texts = "1";
                    return false;
                }
                else
                    return true;
            }
            return true;
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProduto(txtPesquisaProduto.Texts);
            }
        }

        private void btnConfirmaItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts))
            {
                inserirItem(condicionalproduto);
            }
        }

        private void txtQuantidadeItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                inserirItem(condicionalproduto);
            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (gridDevolucao.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o produto selecionado?"))
                {
                    var selectedItem = this.gridDevolucao.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        CondicionalDevolucao condicionalDevolucao = new CondicionalDevolucao();
                        condicionalDevolucao.Id = int.Parse(id);
                        condicionalDevolucao = (CondicionalDevolucao)Controller.getInstance().selecionar(condicionalDevolucao);
                        if (condicionalDevolucao != null)
                            Controller.getInstance().excluir(condicionalDevolucao);
                        txtPesquisaProduto.Enabled = true;
                        txtQuantidadeItem.Enabled = true;
                        btnConfirmaItem.Enabled = true;
                        lblInformativo.Visible = false;
                    }
                    dsProdutoDevolucao.Tables[0].Rows[gridDevolucao.SelectedIndex].Delete();
              
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o produto que deseja excluir!");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (lblInformativo.Visible == true)
            {
                condicional.Encerrado = true;
                condicional.DataEncerramento = DateTime.Now;
                Controller.getInstance().salvar(condicional);
                GenericaDesktop.ShowAlerta("Condicional totalmente devolvida e encerrada!");
            }

        }
    }
}
