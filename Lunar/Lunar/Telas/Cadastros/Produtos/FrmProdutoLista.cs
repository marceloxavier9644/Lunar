using Lunar.Telas.Cadastros.Produtos.RelatoriosCadastro;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos
{
    public partial class FrmProdutoLista : Form
    {
        private IList<Produto> listaProdutos;
        ProdutoController produtoController = new ProdutoController();
        Produto produto = new Produto();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmProdutoLista()
        {
            InitializeComponent();
        }

        private void carregarLista()
        {
            listaProdutos = produtoController.selecionarTodosProdutorPorFilial(Sessao.empresaFilialLogada);

            if (listaProdutos.Count > 0)
            {
                sfDataPager1.DataSource = listaProdutos;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                grid.AutoSizeController.Refresh();
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaProdutos.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void FrmProdutoLista_Load(object sender, EventArgs e)
        {
            carregarLista();
        }

        private void txtPesquisaProdutoPorCodigoUnico_KeyPress(object sender, KeyPressEventArgs e)
        {    
            generica.SoNumero(txtPesquisaProdutoPorCodigoUnico.Texts, e);
            if (e.KeyChar == 13)
            {
                if(!String.IsNullOrEmpty(txtPesquisaProdutoPorCodigoUnico.Texts))
                    PesquisarProdutoPorCodigo();
            }
        }

        private void PesquisarProdutoPorDescricao(string valor)
        {
            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            sfDataPager1.DataSource = listaProdutos;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (listaProdutos.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisaProdutoPorDescricao.Texts = "";
                txtPesquisaProdutoPorDescricao.PlaceholderText = "";
                txtPesquisaProdutoPorDescricao.Select();
            }
            txtPesquisaProdutoPorDescricao.Texts = "";

            grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
            this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
            grid.AutoSizeController.Refresh();
        }

        private void PesquisarProdutoPorCodigo()
        {
            if (!String.IsNullOrEmpty(txtPesquisaProdutoPorCodigoUnico.Texts)) 
            { 
                produto = new Produto();
                produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(txtPesquisaProdutoPorCodigoUnico.Texts), Sessao.empresaFilialLogada);
                listaProdutos = new List<Produto>();
                listaProdutos.Add(produto);
                if (produto != null)
                {
                    sfDataPager1.DataSource = listaProdutos;
                    if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                        sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                    else
                        sfDataPager1.PageSize = 100;
                    grid.DataSource = sfDataPager1.PagedSource;
                    if (listaProdutos.Count == 0)
                    {
                        GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                        txtPesquisaProdutoPorCodigoUnico.Texts = "";
                        txtPesquisaProdutoPorCodigoUnico.PlaceholderText = "";
                        txtPesquisaProdutoPorCodigoUnico.Select();
                    }
                }
                txtPesquisaProdutoPorCodigoUnico.Texts = "";

                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                grid.AutoSizeController.Refresh();
            }
        }

        private void txtPesquisaProdutoPorDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtPesquisaProdutoPorDescricao.Texts))
                    PesquisarProdutoPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarProdutoParaEditar();
        }

        private void selecionarProdutoParaEditar()
        {
            if (grid.SelectedIndex >= 0)
            {
                produto = new Produto();
                produto = (Produto)grid.SelectedItem;
                editarCadastro(produto);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do produto que deseja editar!");
        }

        private void editarCadastro(Produto produto)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmProdutoCadastro uu = new FrmProdutoCadastro(produto, false))
                {
                    formBackground.StartPosition = FormStartPosition.CenterParent;
                    //  formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.MinimizeBox = false;
                    formBackground.MaximizeBox = false;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    formBackground.Dispose();
                    carregarLista();
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
        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            try
            {
                using (FrmProdutoCadastro uu = new FrmProdutoCadastro())
                {
                    formBackground.StartPosition = FormStartPosition.CenterParent;
                  //  formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.MinimizeBox = false;             
                    formBackground.MaximizeBox = false;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    formBackground.Dispose();
                    carregarLista();
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
            //FrmClienteCadastro frm = new FrmClienteCadastro();
            //frm.ShowDialog();
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarProdutoParaEditar();
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
                FileName = "ListaProdutos",
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
                FileName = "ListaProdutos",
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

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnExtratoProduto_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                produto = new Produto();
                produto = (Produto)grid.SelectedItem;
                bool conciliado = false;
                if (GenericaDesktop.ShowConfirmacao("Deseja visualizar apenas o estoque conciliado?"))
                    conciliado = true;
                FrmExtratoProduto frmExtratoProduto = new FrmExtratoProduto(produto, conciliado);
                frmExtratoProduto.ShowDialog();
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do produto que deseja visualizar!");
        }

        private void FrmProdutoLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    break;
            }
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.grid.SearchController.AllowFiltering = true;
                this.grid.SearchController.Search(rjTextBox1.Texts);
                rjTextBox1.Focus();
            }
            catch
            {

            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                produto = new Produto();
                produto = (Produto)grid.SelectedItem;
                if(produto.Estoque <= 0)
                {
                    if(GenericaDesktop.ShowConfirmacao("Deseja excluir o produto " + produto.Id.ToString() + "?"))
                    {
                        Controller.getInstance().excluir(produto);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso");
                        PesquisarProdutoPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Não é possível excluir um produto que tenha estoque contábil!");
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do produto que deseja excluir!");
        }
    }
}
