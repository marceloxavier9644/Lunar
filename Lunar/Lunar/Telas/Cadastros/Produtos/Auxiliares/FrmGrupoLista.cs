using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
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

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmGrupoLista : Form
    {
        bool passou = false;
        private IList<ObjetoPadrao> listaGrupo;
        ProdutoGrupoController produtoGrupoController = new ProdutoGrupoController();
        ProdutoGrupo produtoGrupo = new ProdutoGrupo();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmGrupoLista()
        {
            InitializeComponent();
        }

        private void carregarLista()
        {
           listaGrupo = produtoGrupoController.selecionarTodos(produtoGrupo);

            if (listaGrupo.Count > 0)
            {
                sfDataPager1.DataSource = listaGrupo;
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
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaGrupo.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void FrmGrupoLista_Load(object sender, EventArgs e)
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



        private void PesquisarGrupoPorDescricao(string valor)
        {
            IList<ProdutoGrupo> listaGrupo = produtoGrupoController.selecionarGrupoComVariosFiltros(valor);
            sfDataPager1.DataSource = listaGrupo;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (listaGrupo.Count == 0)
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
                    PesquisarGrupoPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
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
                produtoGrupo = new ProdutoGrupo();
                produtoGrupo = (ProdutoGrupo)grid.SelectedItem;
                editarCadastro(produtoGrupo);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do grupo que deseja editar!");
        }

        private void editarCadastro(ProdutoGrupo produtoGrupo)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmGrupoProdutoCadastro uu = new FrmGrupoProdutoCadastro(produtoGrupo))
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
                using (FrmGrupoProdutoCadastro uu = new FrmGrupoProdutoCadastro())
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
                FileName = "ListaGrupos",
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
                FileName = "ListaGrupos",
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

        private void FrmGrupoLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    break;
            }
        }

        private void btnExcluirGrupo_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                produtoGrupo = new ProdutoGrupo();
                produtoGrupo = (ProdutoGrupo)grid.SelectedItem;
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o grupo " + produtoGrupo.Descricao + "? Todos os produtos desse grupo vão ficar sem o grupo!"))
                {
                    ProdutoController produtoController = new ProdutoController();
                    IList<Produto> listaProdutoComGrupo = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.ProdutoGrupo = " + produtoGrupo.Id + " and Tabela.FlagExcluido <> true");
                    if (listaProdutoComGrupo.Count > 0)
                    {
                        if (GenericaDesktop.ShowConfirmacao("Você possui " + listaProdutoComGrupo.Count + " Produtos Utilizando este grupo! Deseja continuar?"))
                        {
                            Controller.getInstance().excluir(produtoGrupo);
                            foreach(Produto produto in listaProdutoComGrupo)
                            {
                                produto.ProdutoGrupo = null;
                                Controller.getInstance().salvar(produto);
                            }
                            GenericaDesktop.ShowInfo("Excluído com Sucesso");
                        }
                    }
                    else
                    {
                        Controller.getInstance().excluir(produtoGrupo);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso");
                    }
                    PesquisarGrupoPorDescricao(txtPesquisaProdutoPorDescricao.Texts.Trim());
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do grupo que deseja excluir!");
        }
    }
}
