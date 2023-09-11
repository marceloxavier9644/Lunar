using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.ContasPagar.Relatorios
{
    public partial class FrmRelatorioContaPagar : Form
    {
        IList<ContaPagar> listaPagar = new List<ContaPagar>();
        public FrmRelatorioContaPagar(IList<ContaPagar> listaPagar)
        {
            InitializeComponent();
            this.listaPagar = listaPagar;
        }

        private void FrmRelatorioContaPagar_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Lista Conta a Pagar";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource();
            ds.Name = "dsContaPagar";
            ds.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(ds);

            ReportParameter[] p = new ReportParameter[2];
            p[0] = (new ReportParameter("TituloRelatorio", "Contas a Pagar"));
            p[1] = (new ReportParameter("Filtro", "Contas selecionadas pelo Usuário: " + Sessao.usuarioLogado.Login));
            reportViewer1.LocalReport.SetParameters(p);


            foreach (ContaPagar contaPagar in listaPagar)
            {
                String planoConta = "";
                if (contaPagar.PlanoConta != null)
                    planoConta = contaPagar.PlanoConta.Descricao;

            dsContaPagar.ContaPagar.AddContaPagarRow(contaPagar.Id.ToString(), contaPagar.Pessoa.RazaoSocial, contaPagar.EmpresaFilial.NomeFantasia, 
                contaPagar.ValorTotal, 0, 0, contaPagar.ValorTotal, contaPagar.Descricao, contaPagar.Descricao + " - " + contaPagar.Historico,
                planoConta, contaPagar.DataOrigem.ToShortDateString(), contaPagar.DVenc.ToShortDateString(), contaPagar.Pago);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
