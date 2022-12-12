using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGridConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExcelExportingOptions = Syncfusion.WinForms.DataGridConverter.ExcelExportingOptions;

namespace Lunar.Telas.Cadastros.Cidades
{
    public partial class FrmPesquisaCidade : Form
    {

        private List<Cidade> listaCidades = new List<Cidade>();
        CidadeController cidadeController = new CidadeController();
        Cidade cidade = new Cidade();
        GenericaDesktop generica = new GenericaDesktop();
        string descricao = "";

        public DialogResult showModal(ref Cidade cidade)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                cidade = this.cidade;
            }
            return DialogResult;
        }

        public DialogResult showModalComDescricao(ref Cidade cidade, string descricao)
        {
            this.descricao = descricao;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                cidade = this.cidade;
            }
            return DialogResult;
        }

        public FrmPesquisaCidade()
        {
            InitializeComponent();       
        }

        //Definir campos grid para nao puxar lista toda

        //gridCidade.AutoGenerateColumns = false;
        //gridCidade.Columns.Add(new GridNumericColumn() { MappingName = "Id" });
        //gridCidade.Columns.Add(new GridTextColumn() { MappingName = "Descricao" });

        private void carregarListaSync()
        {
            listaCidades = cidadeController.selecionarTodasCidades().ToList();
            sfDataPager1.DataSource = listaCidades;
            if(!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            gridCidade.DataSource = sfDataPager1.PagedSource;
            sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
        }


        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                FiltrarCidade();
                if(gridCidade.RowCount > 0)
                {
                    gridCidade.SelectedIndex = 0;
                    gridCidade.Select();
                    gridCidade.Focus();
                }
            }
        }

        private void FrmPesquisaCidade_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.descricao))
            {
                txtPesquisa.Texts = this.descricao;
                FiltrarCidade();
            }
            else
            {
                carregarListaSync();
                txtPesquisa.Select();
            }
        }


        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaCidades.Skip(e.StartRowIndex).Take(e.PageSize));
        }


        private void FiltrarCidade()
        {
            listaCidades = new List<Cidade>();
            if (string.IsNullOrEmpty(descricao))
                listaCidades = cidadeController.selecionarListaCidadePorDescricao(txtPesquisa.Texts.Trim()).ToList();
            else
            {
                listaCidades = cidadeController.selecionarListaCidadePorDescricao(descricao).ToList();
                descricao = "";
            }
            sfDataPager1.DataSource = listaCidades;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            gridCidade.DataSource = sfDataPager1.PagedSource;
        }

        private void btnClose_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            selecionarCidade();
        }

        private void gridCidade_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarCidade();
        }

        private void selecionarCidade()
        {
            cidade = new Cidade();
            cidade = (Cidade)gridCidade.SelectedItem;
            //MessageBox.Show(gridCidade.SelectedIndex.ToString());
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void gridCidade_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    selecionarCidade();
                    break;
            }        
        }

        private void gridCidade_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selecionarCidade();
        }

        private void ExportarParaExcel()
        {
            try
            {
                String pastaSalvar = "";
                folderBrowser1.Description = "Cidades.xlsx";
                if (this.folderBrowser1.ShowDialog() == DialogResult.OK)
                {
                    pastaSalvar = this.folderBrowser1.DirectoryPath;
                    var options = new ExcelExportingOptions();
                    var excelEngine = gridCidade.ExportToExcel(gridCidade.View, options);
                    var workBook = excelEngine.Excel.Workbooks[0];
                    workBook.SaveAs(pastaSalvar + "\\Cidades.xlsx");
                    GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void ExportarParaPDF()
        {
            //try
            //{
            //    String pastaSalvar = "";
            //    folderBrowser1.Description = "Cidades.pdf";
            //    if (this.folderBrowser1.ShowDialog() == DialogResult.OK)
            //    {
            //        pastaSalvar = this.folderBrowser1.DirectoryPath;
            //        var document = this.gridCidade.ExportToPdf();
            //        document.Save(pastaSalvar + "\\Cidades.pdf");
            //        GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
            //    } 
            //}
            //catch (Exception ex)
            //{
            //    GenericaDesktop.ShowErro(ex.Message);
            //}
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Portrait;
            var page = document.Pages.Add();
            var PDFGrid = gridCidade.ExportToPdfGrid(gridCidade.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ListaCidades",
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarParaExcel();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportarParaPDF();
        }

        private void txtRegistroPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtRegistroPorPagina.Text, e);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
