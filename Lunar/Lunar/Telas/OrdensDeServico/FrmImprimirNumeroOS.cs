using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico
{

    public partial class FrmImprimirNumeroOS : Form
    {
        string numeroOS = "";
        string nomeCliente = "";
        public FrmImprimirNumeroOS(String numeroOS, String nomeCliente)
        {
            InitializeComponent();
            this.numeroOS = numeroOS;
            this.nomeCliente = nomeCliente;
        }

        private void FrmImprimirNumeroOS_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico." + Sessao.parametroSistema.ModeloEtiquetaNumeroOs;
            ReportParameter[] p = new ReportParameter[2];
            p[0] = (new ReportParameter("NumeroOS", this.numeroOS));
            p[1] = (new ReportParameter("Nome", this.nomeCliente));
            reportViewer1.LocalReport.SetParameters(p);
            this.reportViewer1.RefreshReport();
        }
    }
}
