using System;
using System.Diagnostics;
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
            this.caminhoPDF = caminhoPDF;
            //abrirArquivo();
            Process.Start(caminhoPDF);
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
            this.Close();

        }

        private void FrmPDF_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
