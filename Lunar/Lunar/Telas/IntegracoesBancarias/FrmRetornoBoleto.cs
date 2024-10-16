using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf;
using Syncfusion.WinForms.DataGridConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.IntegracoesBancarias
{
    public partial class FrmRetornoBoleto : Form
    {
        IList<RetornoBanco> listaRetorno = new List<RetornoBanco>();
        RetornoBancoController retornoBancoController = new RetornoBancoController();
        public FrmRetornoBoleto()
        {
            InitializeComponent();
            txtDataAberturaInicial.Value = DateTime.Now.AddDays(-1);
            txtDataAberturaFinal.Value = DateTime.Now;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            grid.DataSource = null;
            grid.Refresh();
            listaRetorno = new List<RetornoBanco>();
            DateTime dataIni = DateTime.Parse(txtDataAberturaInicial.Value.ToString());
            DateTime dataFin = DateTime.Parse(txtDataAberturaFinal.Value.ToString());
            String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
            String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

            listaRetorno = retornoBancoController.selecionarRetornoPorPeriodo(dataInicial, dataFinal);
            grid.DataSource = listaRetorno;
            GridSummary.PreencherSumario(grid, "ValorLiquidado");
            grid.Refresh();
            if (listaRetorno.Count > 0)
                btnImprimir.Visible = true;
            else
                btnImprimir.Visible = false;
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void imprimir()
        {
            try
            {
                // Criando opções de exportação para o PDF
                PdfExportingOptions pdfExportingOptions = new PdfExportingOptions
                {
                    AutoColumnWidth = true
                };
                pdfExportingOptions.FitAllColumnsInOnePage = true;
                int maxDescricaoLength = 45;

                foreach (var retorno in listaRetorno)
                {
                    if (retorno.Descricao.Length > maxDescricaoLength)
                    {
                        retorno.Descricao = retorno.Descricao.Substring(0, maxDescricaoLength) + "...";
                    }
                }

                pdfExportingOptions.ExcludeColumns.Add("Descricao");
                pdfExportingOptions.ExcludeColumns.Add("CodigoBeneficiario");
                pdfExportingOptions.ExcludeColumns.Add("NossoNumero");
                pdfExportingOptions.ExcludeColumns.Add("DescontoLiquido");
                pdfExportingOptions.ExcludeColumns.Add("AbatimentoLiquido");
                pdfExportingOptions.ExcludeColumns.Add("ContaReceber.Cliente.Id");
                // Exportando o grid para um documento PDF
                PdfDocument pdfDocument = grid.ExportToPdf(pdfExportingOptions);


                string tempDirectory = Path.GetTempPath();
                string tempFilePath = Path.Combine(tempDirectory, "RetornoBanco.pdf");

                // Salvando o arquivo PDF no sistema
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfDocument.Save(fileStream);
                }

                // Abrindo o PDF gerado
                System.Diagnostics.Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar o PDF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }
    }
}
