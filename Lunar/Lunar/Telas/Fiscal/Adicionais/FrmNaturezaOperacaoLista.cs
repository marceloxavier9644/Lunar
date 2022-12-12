using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
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

namespace Lunar.Telas.Fiscal.Adicionais
{
    public partial class FrmNaturezaOperacaoLista : Form
    {
        private IList<NaturezaOperacao> listaNaturezaOperacao;
        NaturezaOperacaoController naturezaOperacaoController = new NaturezaOperacaoController();
        NaturezaOperacao naturezaOperacao = new NaturezaOperacao();
        bool passou = false;
        public FrmNaturezaOperacaoLista()
        {
            InitializeComponent();
            this.Opacity = 0.0;
        }
        private void carregarLista()
        {
            txtNaturezaOperacao.Texts = "";
            NaturezaOperacao naturezaOperacao = new NaturezaOperacao();
            listaNaturezaOperacao = naturezaOperacaoController.selecionarTodasNaturezaOperacao();
            sfDataPager1.DataSource = listaNaturezaOperacao;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;
            sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
            txtNaturezaOperacao.Focus();
        }

        private void FrmNaturezaOperacaoLista_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void PesquisarNaturezaOperacao(string valor)
        {
            listaNaturezaOperacao = naturezaOperacaoController.selecionarNaturezaOperacaoVariosFiltros(valor);

            sfDataPager1.DataSource = listaNaturezaOperacao;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (listaNaturezaOperacao.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtNaturezaOperacao.Texts = "";
                txtNaturezaOperacao.Focus();
            }
        }

        private void txtNaturezaOperacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarNaturezaOperacao(txtNaturezaOperacao.Texts.Trim());
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
            carregarLista();
        }

        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            try
            {
                using (FrmNaturezaOperacao uu = new FrmNaturezaOperacao())
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

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaNaturezaOperacao.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarNaturezaParaEditar();
        }

        private void editarCadastro(NaturezaOperacao naturezaOperacao)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmNaturezaOperacao uu = new FrmNaturezaOperacao(naturezaOperacao))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                   // formBackground.Left = Top = 0;
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

        private void selecionarNaturezaParaEditar()
        {
            if (grid.SelectedIndex >= 0)
            {
                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao = (NaturezaOperacao)grid.SelectedItem;
                editarCadastro(naturezaOperacao);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha que deseja editar!");
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarNaturezaParaEditar();
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
                FileName = "ListaNaturezaOperacao",
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
                FileName = "ListaNaturezaOperacao",
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
        }

        private void FrmNaturezaOperacaoLista_Paint(object sender, PaintEventArgs e)
        {
            if (passou == false)
            {
                passou = true;
                e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
                carregarLista();
            }
        }
    }
}
