using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Financeiro.Cartoes
{
    public partial class FrmAdquirenteCartaoLista : Form
    {
        AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
        IList<AdquirenteCartao> lista;
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        public FrmAdquirenteCartaoLista()
        {
            InitializeComponent();
        }

        private void carregarLista()
        {
            lista = adquirenteCartaoController.selecionarTodasAdquirentes();

            sfDataPager1.DataSource = lista;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;
            sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, lista.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Pesquisar(txtPesquisa.Texts.Trim());
            }
        }
        private void Pesquisar(string valor)
        {
            lista = adquirenteCartaoController.selecionarAdquirenteComVariosFiltros(valor);

            sfDataPager1.DataSource = lista;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (lista.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisa.Texts = "";
                txtPesquisa.PlaceholderText = "";
                txtPesquisa.Select();
            }
        }

        private void FrmAdquirenteCartaoLista_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            timer1.Start();
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
                FileName = "ListaAdquirentes",
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
                FileName = "ListaAdquirentes",
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            try
            {
                using (FrmAdquirenteCartao uu = new FrmAdquirenteCartao())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
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

        private void FrmAdquirenteCartaoLista_Paint(object sender, PaintEventArgs e)
        {
            carregarLista();
            txtPesquisa.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarParaEditar();
        }
        private void selecionarParaEditar()
        {
            if (grid.SelectedIndex >= 0)
            {
                adquirenteCartao = new AdquirenteCartao();
                adquirenteCartao = (AdquirenteCartao)grid.SelectedItem;
                editarCadastro(adquirenteCartao);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha da empresa que deseja editar!");
        }

        private void editarCadastro(AdquirenteCartao adquirenteCartao)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmAdquirenteCartao uu = new FrmAdquirenteCartao(adquirenteCartao))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarParaEditar();
        }
    }
}
