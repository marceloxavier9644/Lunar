using Lunar.Telas.Vendas.Adicionais;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Relatorios
{
    public partial class FrmVendaEOrdemPorPeriodo : Form
    {
        VendaController vendaController = new VendaController();
        OrdemServicoController ordemServicoController = new OrdemServicoController();

        public FrmVendaEOrdemPorPeriodo()
        {
            InitializeComponent();
            DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtDataInicial.Value = primeiroDiaDoMes;
            txtDataFinal.Value = DateTime.Now;
            txtDataInicial.Focus();
        }

        private async void Pesquisar()
        {
            string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
            string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");

            IList<Venda> listaVendas = vendaController.selecionarVendaPorPeriodo(Sessao.empresaFilialLogada, dataInicial, dataFinal);
            IList<OrdemServico> listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL(
                "Select * FROM OrdemServico as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.DataAbertura between '" + dataInicial + "' and '" + dataFinal + "'");

            // Combina as listas e ordena por data
            var combinados = listaVendas.Cast<object>()
                .Concat(listaOrdemServico.Cast<object>())
                .OrderBy(item =>
                    item is Venda venda ? venda.DataVenda : (item as OrdemServico).DataAbertura)
                .ToList();

            // Limpa DataSources antigos
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Vendas.Vendas01.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            // Define os parâmetros do relatório
            ReportParameter[] parametros = new ReportParameter[4];
            parametros[0] = new ReportParameter("DataInicial", DateTime.Parse(txtDataInicial.Value.ToString()).ToShortDateString());
            parametros[1] = new ReportParameter("DataFinal", DateTime.Parse(txtDataFinal.Value.ToString()).ToShortDateString());
            parametros[2] = new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia);
            parametros[3] = new ReportParameter("Logo", Sessao.parametroSistema.Logo);

            // Passa os parâmetros para o ReportViewer
            reportViewer1.LocalReport.SetParameters(parametros);

            // Cria o DataTable do DataSet 'dsVendas'
            dsVendas.VendaDataTable tabelaVendas = new dsVendas.VendaDataTable();

            // Preenche o DataTable com dados combinados e ordenados
            foreach (var item in combinados)
            {
                if (item is Venda venda)
                {
                    string cliente = venda.Cliente?.RazaoSocial ?? "";
                    tabelaVendas.AddVendaRow(
                        venda.DataVenda.ToShortDateString(),
                        cliente,
                        venda.ValorFinal + venda.ValorDesconto,
                        venda.ValorFinal,
                        "VENDA: " + venda.Id.ToString(),
                        0
                    );
                }
                else if (item is OrdemServico ordem)
                {
                    string cliente = ordem.Cliente?.RazaoSocial ?? "";
                    tabelaVendas.AddVendaRow(
                        ordem.DataAbertura.ToShortDateString(),
                        cliente,
                        ordem.ValorTotal - ordem.ValorDesconto,
                        ordem.ValorTotal,
                        "O.S: " + ordem.Id.ToString(),
                        0
                    );
                }
            }

            // Define o DataSource para o ReportViewer
            ReportDataSource rds = new ReportDataSource("dsVendas", (DataTable)tabelaVendas);
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Atualiza o relatório
            reportViewer1.RefreshReport();
        }

        private async void PesquisarOld()
        {
            string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
            string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");

            IList<Venda> listaVendas = vendaController.selecionarVendaPorPeriodo(Sessao.empresaFilialLogada, dataInicial, dataFinal);
            IList<OrdemServico> listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL(
                "Select * FROM OrdemServico as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.DataAbertura between '" + dataInicial + "' and '" + dataFinal + "'");

            // Limpa DataSources antigos
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Vendas.Vendas01.rdlc";
            reportViewer1.LocalReport.EnableExternalImages = true;

            // Define os parâmetros do relatório
            ReportParameter[] parametros = new ReportParameter[4];
            parametros[0] = new ReportParameter("DataInicial", DateTime.Parse(txtDataInicial.Value.ToString()).ToShortDateString());
            parametros[1] = new ReportParameter("DataFinal", DateTime.Parse(txtDataFinal.Value.ToString()).ToShortDateString());
            parametros[2] = new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia);
            parametros[3] = new ReportParameter("Logo", Sessao.parametroSistema.Logo);

            // Passa os parâmetros para o ReportViewer
            reportViewer1.LocalReport.SetParameters(parametros);

            // Cria o DataTable do DataSet 'dsVendas'
            dsVendas.VendaDataTable tabelaVendas = new dsVendas.VendaDataTable();

            // Preenche o DataTable com dados das vendas
            foreach (var venda in listaVendas)
            {
                string cliente = venda.Cliente?.RazaoSocial ?? "";  // Usa operador condicional para evitar nulos
                tabelaVendas.AddVendaRow(
                    venda.DataVenda.ToShortDateString(),
                    cliente,
                    venda.ValorFinal + venda.ValorDesconto,
                    venda.ValorFinal,
                    "VENDA: " + venda.Id.ToString(),
                    0
                );
            }

            // Preenche o DataTable com dados das ordens de serviço
            foreach (var ordem in listaOrdemServico)
            {
                string cliente = ordem.Cliente?.RazaoSocial ?? "";
                tabelaVendas.AddVendaRow(
                    ordem.DataAbertura.ToShortDateString(),
                    cliente,
                    ordem.ValorTotal - ordem.ValorDesconto,
                    ordem.ValorTotal,
                    "O.S: " + ordem.Id.ToString(),
                    0
                );
            }

            // Define o DataSource para o ReportViewer (mantendo apenas um Clear)
            ReportDataSource rds = new ReportDataSource("dsVendas", (DataTable)tabelaVendas);
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Atualiza o relatório
            reportViewer1.RefreshReport();
        }

        private void FrmVendaEOrdemPorPeriodo_Load(object sender, EventArgs e)
        {

          
        }

        private void btnPesquisaVender_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }
    }
}
