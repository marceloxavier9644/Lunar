using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace Lunar.Telas.Estoques
{
    public partial class FrmBalancoEstoque : Form
    {
        BalancoEstoqueController balancoEstoqueController = new BalancoEstoqueController();
        EstoqueDAO estoqueDAO = new EstoqueDAO();
        ProdutoController produtoController = new ProdutoController();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmBalancoEstoque()
        {
            InitializeComponent();
            txtDataAjuste.Value = DateTime.Now;
            this.grid.DataSource = dsBalanco.Tables["Balanco"];
        }

        public FrmBalancoEstoque(BalancoEstoque balancoEstoque)
        {
            InitializeComponent();
            txtDataAjuste.Value = DateTime.Now;
            this.grid.DataSource = dsBalanco.Tables["Balanco"];
            get_BalancoEstoque(balancoEstoque);
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string eanDigitado = "";
                if (!String.IsNullOrEmpty(txtProduto.Texts))
                {
                    IList<Produto> listaProdutos = new List<Produto>();
                    Produto produto = new Produto();
                    if (generica.eNumero(txtProduto.Texts))
                    {
                        if (txtProduto.Texts.Length > 5)
                        {
                            eanDigitado = txtProduto.Texts.Trim();
                            listaProdutos = produtoController.selecionarProdutoPorCodigoBarras(txtProduto.Texts.Trim());
                        }
                        else if (txtProduto.Texts.Length <= 5)
                        {
                            produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(txtProduto.Texts.Trim()), Sessao.empresaFilialLogada);
                        }
                    }
                    else
                    {
                        listaProdutos = produtoController.selecionarProdutosComVariosFiltros(txtProduto.Texts.Trim(), Sessao.empresaFilialLogada);
                    }

                    //Acima fiz as pesquisas, agora tratamos o resultado
                    if (listaProdutos.Count == 1)
                    {
                        foreach (Produto prod in listaProdutos)
                        {
                            txtProduto.Texts = prod.Descricao;
                            txtCodProduto.Texts = prod.Id.ToString();
                            txtEstoqueAuxiliar.Texts = prod.EstoqueAuxiliar.ToString();
                            txtEstoqueAtual.Texts = prod.Estoque.ToString();
                            calcularEstoqueData();
                            txtQuantidadeEfetiva.Focus();
                            if (eanDigitado.Equals(prod.Ean))
                            {
                                txtQuantidadeEfetiva.Texts = "1";
                                if (chkParaCampoQuantidade.Checked == false)
                                    btnConfirmar.PerformClick();
                            }
                        }
                    }
                    else if (listaProdutos.Count > 1)
                    {
                        Object produtoOjeto = new Produto();
                        Form formBackground = new Form();
                        try
                        {
                            using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + txtProduto.Texts + "%'"))
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
                                        formBackground.Dispose();
                                        uu.Dispose();
                                        break;
                                    case DialogResult.OK:
                                        txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                        txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                        txtEstoqueAuxiliar.Texts = ((Produto)produtoOjeto).EstoqueAuxiliar.ToString();
                                        txtEstoqueAtual.Texts = ((Produto)produtoOjeto).Estoque.ToString();
                                        calcularEstoqueData();
                                        txtQuantidadeEfetiva.Focus();
                                        break;
                                }
                                formBackground.Dispose();
                            }
                        }
                        catch
                        {

                        }
                    }
                    else if (produto.Id > 0)
                    {
                        txtProduto.Texts = produto.Descricao;
                        txtCodProduto.Texts = produto.Id.ToString();
                        txtEstoqueAuxiliar.Texts = produto.EstoqueAuxiliar.ToString();
                        txtEstoqueAtual.Texts = produto.Estoque.ToString();
                        calcularEstoqueData();
                        txtQuantidadeEfetiva.Focus();
                    }
                }
                else
                {
                    btnPesquisaProduto.PerformClick();
                }
            }
        }

        private void txtVencimentoInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime dataAjust = new DateTime();
                dataAjust = DateTime.Parse(txtDataAjuste.Value.ToString());
                lblEstoqueData.Text = "Estoque em " + dataAjust.ToShortDateString();
                lblEstoqueAuxiliarData.Text = "Est. Aux em " + dataAjust.ToShortDateString();
            }
            catch
            {

            }
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            Object produtoOjeto = new Produto();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", ""))
                {
                    txtProduto.Texts = "";
                    txtCodProduto.Texts = "";
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
                            formBackground.Dispose();
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                            txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                            txtEstoqueAuxiliar.Texts = ((Produto)produtoOjeto).EstoqueAuxiliar.ToString();
                            txtEstoqueAtual.Texts = ((Produto)produtoOjeto).Estoque.ToString();
                            calcularEstoqueData();
                            txtQuantidadeEfetiva.Focus();
                            break;
                    }
                    formBackground.Dispose();
                }
            }
            catch
            {

            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calcularEstoqueData()
        {
            try
            {
                DateTime dataAjust = new DateTime();
                dataAjust = DateTime.Parse(txtDataAjuste.Value.ToString());
                lblEstoqueData.Text = "Estoque em " + dataAjust.ToShortDateString();
                lblEstoqueAuxiliarData.Text = "Est. Aux em " + dataAjust.ToShortDateString();
                DateTime dataIni = DateTime.Parse(txtDataAjuste.Value.ToString());
                String dat = dataIni.ToString("yyyy-MM-dd");
                double estoqueConciliado = estoqueDAO.calcularEstoqueConciliadoPorProdutoeData(int.Parse(txtCodProduto.Texts),Sessao.empresaFilialLogada, dat);
                txtEstoqueData.Texts = estoqueConciliado.ToString();
                double estoqueNaoConciliado = estoqueDAO.calcularEstoqueNaoConciliadoPorProdutoeData(int.Parse(txtCodProduto.Texts), Sessao.empresaFilialLogada, dat);
                txtEstoqueAuxiliarData.Texts = estoqueNaoConciliado.ToString();
            }
            catch
            {

            }
        }

        private void btnAtualizarEstoque_Click(object sender, EventArgs e)
        {
            calcularEstoqueData();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts) && !String.IsNullOrEmpty(txtQuantidadeEfetiva.Texts))
            {
                calcularEstoqueData();
                Produto produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)Controller.getInstance().selecionar(produto);

                inserirItemContagem(produto);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Você deve selecionar um produto corretamente e inserir a quantidade!");
            }
        }

        private void inserirItemContagem(Produto produto)
        {
            try
            {
                bool conciliado = false;
                double qtdData = 0;
                if (radioConciliado.Checked == true)
                {
                    conciliado = true;
                    qtdData = double.Parse(txtEstoqueData.Texts);
                }
                else
                    qtdData = double.Parse(txtEstoqueAuxiliarData.Texts);

                DateTime dataAjust = new DateTime();
                dataAjust = DateTime.Parse(txtDataAjuste.Value.ToString());

                //var records = grid.View.Records;
                //foreach (var record in records)
                //{
                //    var dataRowView = record.Data as DataRowView;

                //    if(produto.Id == int.Parse(dataRowView.Row["IdProduto"].ToString()))
                //    {
                //        int l = int.Parse(dataRowView.Row["LinhaDataSet"].ToString());
                //        dsBalanco.Tables[0].Rows[l][5] = txtQuantidadeEfetiva.Text;
                //    }
                //}

                string linha = dsBalanco.Tables[0].Rows.Count.ToString();
                System.Data.DataRow row = dsBalanco.Tables[0].NewRow();
                row.SetField("IdProduto", produto.Id.ToString());
                row.SetField("DescricaoProduto", produto.Descricao);
                row.SetField("Conciliado", conciliado);
                row.SetField("DataAjuste", dataAjust.ToShortDateString());
                row.SetField("QuantidadeData", qtdData);
                row.SetField("NovaQuantidade", double.Parse(txtQuantidadeEfetiva.Texts));
                row.SetField("LinhaDataSet", linha);
                row.SetField("Id", 0);
                dsBalanco.Tables[0].Rows.Add(row);
                limpar();
                txtProduto.Focus();
            }
            catch
            {
                GenericaDesktop.ShowErro("Confira os campos data, produto e quantidade!");
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void limpar()
        {
            txtCodProduto.Texts = "";
            txtEstoqueAtual.Texts = "";
            txtEstoqueAuxiliar.Texts = "";
            txtEstoqueAuxiliarData.Texts = "";
            txtEstoqueData.Texts = "";
            txtProduto.Texts = "";
            txtQuantidadeEfetiva.Texts = "1";
        }

        private void txtQuantidadeEfetiva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnConfirmar.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (grid.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir a linha selecionada?"))
                {
                    var selectedItem = this.grid.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        BalancoEstoqueProduto balancoEstoqueProduto = new BalancoEstoqueProduto();
                        balancoEstoqueProduto.Id = int.Parse(id);
                        balancoEstoqueProduto = (BalancoEstoqueProduto)Controller.getInstance().selecionar(balancoEstoqueProduto);
                        if (balancoEstoqueProduto != null)
                            Controller.getInstance().excluir(balancoEstoqueProduto);
                    }
                    dsBalanco.Tables[0].Rows[grid.SelectedIndex].Delete();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione a linha que deseja excluir para depois clicar no botão de excluir");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            set_Balanco();
        }

        private void set_Balanco()
        {
            BalancoEstoque balancoEstoque = new BalancoEstoque();
            if (!String.IsNullOrEmpty(txtId.Texts))
                balancoEstoque.Id = int.Parse(txtId.Texts);
            else
                balancoEstoque.Id = 0;
            balancoEstoque.Filial = Sessao.empresaFilialLogada;
            balancoEstoque.Efetivado = false;
            balancoEstoque.Conciliado = false;
            if (radioConciliado.Checked == true)
                balancoEstoque.Conciliado = true;
            balancoEstoque.DataAjuste = DateTime.Parse(txtDataAjuste.Value.ToString());
            balancoEstoque.Descricao = txtDescricao.Texts;
            if (String.IsNullOrEmpty(balancoEstoque.Descricao))
                balancoEstoque.Descricao = "BALANÇO " + txtDataAjuste.Value.ToString();
            balancoEstoque.DataCadastro = DateTime.Now;
            balancoEstoque.OperadorCadastro = Sessao.usuarioLogado.Id.ToString();
            balancoEstoque.OperadorEfetivacao = null;
            if (chkZerarEstoque.Checked == true)
                balancoEstoque.ZerarEstoqueNaoContado = true;
            else
                balancoEstoque.ZerarEstoqueNaoContado = false;

            if (balancoEstoque.Id > 0)
            {
                balancoEstoque.DataAlteracao = DateTime.Now;
                balancoEstoque.OperadorAlteracao = Sessao.usuarioLogado.Id.ToString();
            }
            IList<BalancoEstoqueProduto> listaProdutos = new List<BalancoEstoqueProduto>();
            var records = grid.View.Records;
            int i = 0;
            foreach (var record in records)
            {
                i++;
                var dataRowViewXXX = record.Data as DataRowView;
                BalancoEstoqueProduto balancoEstoqueProduto = new BalancoEstoqueProduto();
                Produto produto = new Produto();

                if (int.Parse(dataRowViewXXX.Row["Id"].ToString()) > 0)
                    balancoEstoqueProduto.Id = int.Parse(dataRowViewXXX.Row["Id"].ToString());
                else
                    balancoEstoqueProduto.Id = 0;

                produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["IdProduto"].ToString()), Sessao.empresaFilialLogada);
                balancoEstoqueProduto.DescricaoProduto = produto.Descricao;
                balancoEstoqueProduto.Produto = produto;
                balancoEstoqueProduto.Quantidade = double.Parse(dataRowViewXXX.Row["NovaQuantidade"].ToString());
                balancoEstoqueProduto.Tipo = "E";
                bool Conc = Boolean.Parse(dataRowViewXXX.Row["Conciliado"].ToString());
                if (Conc == false)
                    balancoEstoqueProduto.Conciliado = false;
                else
                    balancoEstoqueProduto.Conciliado = true;
                listaProdutos.Add(balancoEstoqueProduto);
            }
            balancoEstoqueController.salvarBalancoEstoqueComItens(balancoEstoque, listaProdutos);
            GenericaDesktop.ShowInfo("Salvo com Sucesso");
            this.Close();
        }

        private void get_BalancoEstoque(BalancoEstoque balancoEstoque)
        {
            txtDescricao.Texts = balancoEstoque.Descricao;
            txtId.Texts = balancoEstoque.Id.ToString();
            txtDataAjuste.Text = balancoEstoque.DataAjuste.ToString();
            DateTime dataAjust = new DateTime();
            dataAjust = DateTime.Parse(txtDataAjuste.Value.ToString());
            if (balancoEstoque.Conciliado == true)
                radioConciliado.Checked = true;
            else
                radioNaoConciliado.Checked = true;

            if (balancoEstoque.ZerarEstoqueNaoContado == true)
                chkZerarEstoque.Checked = true;
            else
                chkZerarEstoque.Checked = false;
            BalancoEstoqueProdutoController balancoEstoqueProdutoController = new BalancoEstoqueProdutoController();
            IList<BalancoEstoqueProduto> listProdutos = balancoEstoqueProdutoController.selecionarProdutosPorBalanco(balancoEstoque.Id);
            if(listProdutos.Count > 0)
            {
                foreach(BalancoEstoqueProduto balancoEstoqueProduto in listProdutos)
                {
                    string linha = dsBalanco.Tables[0].Rows.Count.ToString();
                    System.Data.DataRow row = dsBalanco.Tables[0].NewRow();
                    row.SetField("IdProduto", balancoEstoqueProduto.Produto.Id.ToString());
                    row.SetField("DescricaoProduto", balancoEstoqueProduto.DescricaoProduto);
                    row.SetField("Conciliado", balancoEstoqueProduto.Conciliado);
                    row.SetField("DataAjuste", dataAjust.ToShortDateString());

                    txtCodProduto.Texts = balancoEstoqueProduto.Produto.Id.ToString();
                    calcularEstoqueData();
                    double qtdData = 0;
                    if (balancoEstoqueProduto.Conciliado == true)
                    {
                        qtdData = double.Parse(txtEstoqueData.Texts);
                    }
                    else 
                    {
                        qtdData = double.Parse(txtEstoqueAuxiliarData.Texts); 
                    }

                    row.SetField("QuantidadeData", qtdData);
                    row.SetField("NovaQuantidade", balancoEstoqueProduto.Quantidade);
                    row.SetField("LinhaDataSet", linha);
                    row.SetField("Id", balancoEstoqueProduto.Id);
                    dsBalanco.Tables[0].Rows.Add(row);
                    limpar();
                    txtProduto.Focus();
                }
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
