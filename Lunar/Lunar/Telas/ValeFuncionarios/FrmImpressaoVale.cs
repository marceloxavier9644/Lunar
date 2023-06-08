using Lunar.Utils;
using LunarBase.Classes;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.ValeFuncionarios
{
    public partial class FrmImpressaoVale : Form
    {
        Caixa caixa = new Caixa();
        public FrmImpressaoVale(Caixa caixa)
        {
            InitializeComponent();
            this.caixa = caixa;
        }

        private void FrmImpressaoVale_Load(object sender, EventArgs e)
        {
            ReportParameter[] p = new ReportParameter[5];
            p[0] = (new ReportParameter("Funcionario", caixa.Pessoa.RazaoSocial));
            p[1] = (new ReportParameter("Data", caixa.DataLancamento.ToShortDateString()));
            p[2] = (new ReportParameter("Valor", string.Format("{0:0.00}", caixa.Valor)));
            p[3] = (new ReportParameter("ValorExtenso", ConverterMoedaPorExtenso.toExtenso(caixa.Valor)));
            p[4] = (new ReportParameter("Controle", caixa.IdOrigem));
            reportViewer1.LocalReport.SetParameters(p);
            this.reportViewer1.RefreshReport();
        }
    }
}
