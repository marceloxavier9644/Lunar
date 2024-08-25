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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmSetorLista : Form
    {
        bool passou = false;
        private IList<ObjetoPadrao> listaSetor;
        ProdutoSetorController produtoSetorController = new ProdutoSetorController();
        ProdutoSetor produtoSetor = new ProdutoSetor();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmSetorLista()
        {
            InitializeComponent();
        }
        private void carregarLista()
        {
            listaSetor = produtoSetorController.selecionarTodos(produtoSetor);

            if (listaSetor.Count > 0)
            {
                sfDataPager1.DataSource = listaSetor;
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
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaSetor.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmSetorLista_Load(object sender, EventArgs e)
        {
            if (passou == false)
            {
                carregarLista();
                passou = true;
                //Ajustar Permissoes de usuario
                //if (Sessao.permissoes.Count > 0)
                //{
                //    // Habilitar ou desabilitar os controles com base nas permissões
                //    btnNovo.Enabled = Sessao.permissoes.Contains("9");
                //    btnEditar.Enabled = Sessao.permissoes.Contains("10");
                //    btnExcluirProduto.Enabled = Sessao.permissoes.Contains("11");
                //    btnExtratoProduto.Enabled = Sessao.permissoes.Contains("12");
                //    btnExportarPDF.Enabled = Sessao.permissoes.Contains("13");
                //    btnExportarExcel.Enabled = Sessao.permissoes.Contains("13");
                //}
            }
        }
        private void PesquisarSetorPorDescricao(string valor)
        {
            IList<ProdutoSetor> listaSetor = produtoSetorController.selecionarProdutoSetorComVariosFiltros(valor, Sessao.empresaFilialLogada);
            sfDataPager1.DataSource = listaSetor;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (listaSetor.Count == 0)
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

        private void txtPesquisaProdutoPorDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtPesquisaProdutoPorDescricao.Texts))
                    PesquisarSetorPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarParaEditar();
        }

        private void selecionarParaEditar()
        {
            if (grid.SelectedIndex >= 0)
            {
                produtoSetor = new ProdutoSetor();
                produtoSetor = (ProdutoSetor)grid.SelectedItem;
                editarCadastro(produtoSetor);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do setor que deseja editar!");
        }

        private void editarCadastro(ProdutoSetor produtoSetor)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmSetorCadastro uu = new FrmSetorCadastro(produtoSetor))
                {
                    formBackground.StartPosition = FormStartPosition.CenterParent;
                    // formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.MinimizeBox = false;
                    formBackground.MaximizeBox = false;
                    formBackground.Left = 0;
                    formBackground.Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    uu.Owner = formBackground;
                    uu.ShowDialog();

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
                using (FrmSetorCadastro uu = new FrmSetorCadastro())
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
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            //if (!Sessao.permissoes.Contains("10"))
            //{
            //e.Cancel = true; 
            //GenericaDesktop.ShowAlerta("Usuário sem Permissão para essa operação!");
            //}
            //else
            selecionarParaEditar();
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
                FileName = "ListaSetores",
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
                FileName = "ListaSetores",
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                produtoSetor = new ProdutoSetor();
                produtoSetor = (ProdutoSetor)grid.SelectedItem;
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o grupo " + produtoSetor.Descricao + "? Todos os produtos desse setor vão ficar sem o grupo!"))
                {
                    ProdutoController produtoController = new ProdutoController();
                    IList<Produto> listaProdutoComGrupo = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.ProdutoSetor = " + produtoSetor.Id + " and Tabela.FlagExcluido <> true");
                    if (listaProdutoComGrupo.Count > 0)
                    {
                        if (GenericaDesktop.ShowConfirmacao("Você possui " + listaProdutoComGrupo.Count + " Produtos Utilizando este grupo! Deseja continuar?"))
                        {
                            Controller.getInstance().excluir(produtoSetor);
                            foreach (Produto produto in listaProdutoComGrupo)
                            {
                                produto.ProdutoSetor = null;
                                Controller.getInstance().salvar(produto);
                            }
                            GenericaDesktop.ShowInfo("Excluído com Sucesso");
                        }
                    }
                    else
                    {
                        Controller.getInstance().excluir(produtoSetor);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso");
                    }
                    PesquisarSetorPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do grupo que deseja excluir!");
        }
    }
}
