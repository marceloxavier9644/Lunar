using System;
using System.Windows.Forms;


namespace Lunar.Telas.VisualizadorPDF
{
    public partial class FrmPDF : Form
    {
        PdfiumViewer.PdfViewer pdf;
        String caminhoPDF = "";

        public FrmPDF(String caminhoPDF)
        {
            InitializeComponent();
            //pdf = new PdfiumViewer.PdfViewer();
            //pdf.Width = this.Width - 20;
            //pdf.Height = this.Height - 40;
            //pdf.ZoomMode = PdfViewerZoomMode.FitBest;
            //this.Controls.Add(pdf);
            this.caminhoPDF = caminhoPDF;
            abrirArquivo();
        }

        private void abrirArquivo()
        {
            try
            {
                //Syncfusion
                pdfViewerControl1.Load(caminhoPDF);
            }
            catch
            {
 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPDF_Load(object sender, EventArgs e)
        {


        }
    }
}
