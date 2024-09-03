using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.VendaItensDAO;


namespace Lunar.Telas.Vendas.Relatorios
{
    public partial class FrmVendaProdutos : Form
    {
        public FrmVendaProdutos()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
            pesquisarProdutosVendidos();
        }

        private void pesquisarProdutosVendidos()
        {
            VendaItensDAO vendaItensDAO = new VendaItensDAO();
            string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
            string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");


            string sql = "SELECT p.ID, p.Descricao, SUM(vi.Quantidade) AS Quantidade, SUM(vi.ValorFinal) AS ValorTotal FROM vendaitens vi INNER JOIN venda v ON vi.Venda = v.ID INNER JOIN produto p ON vi.Produto = p.ID WHERE v.DataVenda BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' and v.Concluida = true and v.Cancelado = false";
            if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
            {
                sql += " AND v.VendedorID = " + txtCodVendedor.Texts;
            }
            sql += " GROUP BY p.ID, p.Descricao ORDER BY Quantidade DESC;";

            IList<ProdutoVendido> lista = vendaItensDAO.SelecionarProdutosVendidosPorSqlNativo(sql);
            grid.DataSource = lista;

            GridSummary.PreencherSumario(grid, "ValorTotal");
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidos();
        }

        private void btnPesquisaVender_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true"))
                {
                    txtCodVendedor.Texts = "";
                    txtVendedor.Texts = "";
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
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial.ToString();
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            btnPesquisar.PerformClick();
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

        private void txtVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisaVender.PerformClick();
        }

        private void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisaVender.PerformClick();
        }

        private void txtDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisar.PerformClick();
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
