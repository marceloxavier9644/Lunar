using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            pesquisarProdutosVendidosGeral();
        }


        private void pesquisarProdutosVendidosGeral()
        {
            VendaItensDAO vendaItensDAO = new VendaItensDAO();
            string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
            string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");

            // Montando a query SQL
            string sql = @"
    SELECT ID, Descricao, SUM(Quantidade) AS Quantidade, SUM(ValorTotal) AS ValorTotal
    FROM (
        SELECT p.ID, p.Descricao, SUM(vi.Quantidade) AS Quantidade, SUM(vi.ValorFinal) AS ValorTotal 
        FROM vendaitens vi 
        INNER JOIN venda v ON vi.Venda = v.ID 
        INNER JOIN produto p ON vi.Produto = p.ID 
        WHERE v.DataVenda BETWEEN '" + dataInicial + "' AND '" + dataFinal + @"' 
        AND v.Concluida = true 
        AND v.Cancelado = false";

            if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
            {
                sql += " AND v.VendedorID = " + txtCodVendedor.Texts;
            }

            sql += @"
        GROUP BY p.ID, p.Descricao";

            // Se o checkbox chkOrdemServico estiver marcado, adicionar a consulta da ordem de serviço
            if (chkOrdemServico.Checked)
            {
                sql += @"

        UNION ALL

        SELECT p.ID, p.Descricao, SUM(osp.Quantidade) AS Quantidade, SUM(osp.VALORTOTAL) AS ValorTotal 
        FROM ordemservicoproduto osp 
        INNER JOIN ordemservico os ON osp.OrdemServico = os.ID 
        INNER JOIN produto p ON osp.Produto = p.ID 
        WHERE os.DATAENCERRAMENTO BETWEEN '" + dataInicial + "' AND '" + dataFinal + @"' 
        AND os.STATUS = 'ENCERRADA' 
        AND os.FLAGEXCLUIDO = false";

                if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
                {
                    sql += " AND os.VendedorID = " + txtCodVendedor.Texts;
                }

                sql += @"
        GROUP BY p.ID, p.Descricao";
            }

            // Fechando a subquery
            sql += @"
    ) AS ProdutosVendidos
    GROUP BY ID, Descricao
    ORDER BY Quantidade DESC;";

            // Executando a consulta e preenchendo a lista
            IList<ProdutoVendido> lista = vendaItensDAO.SelecionarProdutosVendidosPorSqlNativo(sql);
            grid.DataSource = lista;
            btnExportarExcel.Visible = true;
            // Preenchendo o sumário
            GridSummary.PreencherSumario(grid, "ValorTotal");
        }



        //private void pesquisarProdutosVendidos()
        //{
        //    VendaItensDAO vendaItensDAO = new VendaItensDAO();
        //    string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
        //    string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");


        //    string sql = "SELECT p.ID, p.Descricao, SUM(vi.Quantidade) AS Quantidade, SUM(vi.ValorFinal) AS ValorTotal FROM vendaitens vi INNER JOIN venda v ON vi.Venda = v.ID INNER JOIN produto p ON vi.Produto = p.ID WHERE v.DataVenda BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' and v.Concluida = true and v.Cancelado = false";
        //    if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
        //    {
        //        sql += " AND v.VendedorID = " + txtCodVendedor.Texts;
        //    }
        //    sql += " GROUP BY p.ID, p.Descricao ORDER BY Quantidade DESC;";

        //    IList<ProdutoVendido> lista = vendaItensDAO.SelecionarProdutosVendidosPorSqlNativo(sql);
        //    grid.DataSource = lista;

        //    GridSummary.PreencherSumario(grid, "ValorTotal");
        //}

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidosGeral();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidosGeral();
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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }

        private void exportarExcel()
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ProdutosVendidos",
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

    }
}
