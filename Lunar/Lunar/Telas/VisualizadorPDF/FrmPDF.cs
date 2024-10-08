using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lunar.Telas.VisualizadorPDF
{
    public partial class FrmPDF : Form
    {
        //PdfiumViewer.PdfViewer pdf;
        String caminhoPDF = "";
        Timer closeTimer;
        public FrmPDF(String caminhoPDF)
        {
            InitializeComponent();
            this.caminhoPDF = caminhoPDF;
            closeTimer = new Timer();
            closeTimer.Interval = 1000; // Dura 1 segundo (1000 ms)
            closeTimer.Tick += CloseTimer_Tick; // Evento quando o timer expira
            abrirArquivo();
        }

        private void abrirArquivo()
        {
            try
            {
                // Syncfusion
                if (caminhoPDF.Contains("NFC"))
                {
                    Process.Start(caminhoPDF);
                    closeTimer.Start(); // Inicia o timer para fechar o formulário
                }
                else
                {
                    pdfViewerControl1.Load(caminhoPDF);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o arquivo: " + ex.Message);
            }
        }
        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            closeTimer.Stop(); // Para o timer
            this.Close(); // Fecha o formulário
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPDF_Load(object sender, EventArgs e)
        {
           

        }

        private void FrmPDF_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        private void FrmPDF_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    break;
            }
        }
    }
}
