using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas
{
    public partial class FrmRelatorioVendas : Form
    {
        String dataInicial = "";
        String dataFinal = "";
        IList<Venda> listaVendas = new List<Venda>();
        public FrmRelatorioVendas(String dataInicial, String dataFinal, IList<Venda> listaVendas)
        {
            InitializeComponent();
            this.dataInicial = dataInicial;
            this.dataFinal = dataFinal;
            this.listaVendas = listaVendas;
        }

        private void FrmRelatorioVendas_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource dsVendaX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaX.Name = "dsVendas";
            dsVendaX.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaX);

            this.reportViewer1.LocalReport.DisplayName = "Vendas";

            string variavel = Sessao.empresaFilialLogada.NomeFantasia;
            if (!String.IsNullOrEmpty(dataInicial))
                variavel = "  a";
            ReportParameter[] p = new ReportParameter[4];
            p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia));
            p[1] = (new ReportParameter("DataInicial", dataInicial + variavel));
            p[2] = (new ReportParameter("DataFinal", dataFinal));
            p[3] = (new ReportParameter("Logo", Sessao.parametroSistema.Logo));
            reportViewer1.LocalReport.SetParameters(p);

            foreach (Venda venda in listaVendas)
            {
                string cliente = "";
                if (venda.Cliente != null)
                    cliente = venda.Cliente.RazaoSocial;
                dsVendas.Venda.AddVendaRow(venda.DataVenda.ToShortDateString(), cliente, venda.ValorFinal + venda.ValorDesconto - venda.ValorAcrescimo, venda.ValorFinal, venda.Id.ToString(), 0);
            }
            this.reportViewer1.RefreshReport();
        }

    }
}
