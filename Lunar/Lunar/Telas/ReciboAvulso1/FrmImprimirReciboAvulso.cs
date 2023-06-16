using Lunar.Utils;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.ReciboAvulso1
{
    public partial class FrmImprimirReciboAvulso : Form
    {
        public FrmImprimirReciboAvulso(String valorNumerico, String valorExtenso, String emitente, String destinatario, String cpfEmitente, String dataEmissao, String referente)
        {
            InitializeComponent();

            String cidade = "";
            String cnpj = "";
            GenericaDesktop generica = new GenericaDesktop();
            if (cpfEmitente.Length == 11)
                cnpj = generica.FormatarCPF(cpfEmitente);
            else if (cpfEmitente.Length == 14)
                cnpj = generica.FormatarCNPJ(cpfEmitente);
            else
                cnpj = "";
            if (Sessao.empresaFilialLogada.Endereco != null)
                cidade = Sessao.empresaFilialLogada.Endereco.Cidade.Descricao;
            ReportParameter[] p = new ReportParameter[8];
            p[0] = (new ReportParameter("EMPRESAFANTASIA", emitente));
            p[1] = (new ReportParameter("EMPRESACNPJ", cnpj));
            p[2] = (new ReportParameter("CLIENTENOME", destinatario));
            p[3] = (new ReportParameter("DESCRICAO", referente));
            p[4] = (new ReportParameter("VALORNUMERICO", valorNumerico));
            p[5] = (new ReportParameter("VALOREXTENSO", valorExtenso));
            p[6] = (new ReportParameter("CIDADE", cidade));
            p[7] = (new ReportParameter("DATALONGA", DateTime.Parse(dataEmissao).ToLongDateString().ToUpper()));
            reportViewer1.LocalReport.SetParameters(p);
            this.reportViewer1.RefreshReport();
        }

        private void FrmImprimirReciboAvulso_Load(object sender, EventArgs e)
        {

        }
    }
}
