using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Dashboards
{
    public partial class FrmDashboard1 : Form
    {
        VendaItensController vendaItensController = new VendaItensController();
        OrdemServicoProdutoController OrdemServicoProdutoController = new OrdemServicoProdutoController();
        VendaController vendaController = new VendaController();
        public FrmDashboard1()
        {
            InitializeComponent();
            produtosVendidos();
            getVendedoresDia();
            getVendasPorData();
        }

        private void produtosVendidos()
        {
            IList<VendaItens> listaItensVendidos = vendaItensController.selecionarProdutosVendidosPorPeriodo(Sessao.empresaFilialLogada, "", "");
            if(listaItensVendidos.Count > 0)
            {
                foreach (VendaItens vendaItem in listaItensVendidos)
                {
                    System.Data.DataRow row = dsProduto.Tables[0].NewRow();
                    row.SetField("Descricao", vendaItem.Produto.Id + " - " + vendaItem.DescricaoProduto);
                    row.SetField("Quantidade", vendaItem.Quantidade);
                    dsProduto.Tables[0].Rows.Add(row);
                }
            }

            //Ordem de Serviço
            IList<OrdemServicoProduto> listaItensOS = OrdemServicoProdutoController.selecionarProdutosVendidosPorPeriodo(Sessao.empresaFilialLogada, "", "");
            if (listaItensOS.Count > 0)
            {
                foreach (OrdemServicoProduto ordemServicoProduto in listaItensOS)
                {
                    System.Data.DataRow row = dsProduto.Tables[0].NewRow();
                    row.SetField("Descricao", ordemServicoProduto.Produto.Id + " - " + ordemServicoProduto.DescricaoProduto);
                    row.SetField("Quantidade", ordemServicoProduto.Quantidade);
                    dsProduto.Tables[0].Rows.Add(row);
                }
            }
        }

        private void getVendedoresDia()
        {
            String dataInicial = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            String dataFinal = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";

            IList<Venda> listaVendasPorVendedor = vendaController.selecionarTop5VendaPorVendedores(Sessao.empresaFilialLogada, dataInicial, dataFinal);
            if (listaVendasPorVendedor.Count > 0)
            {
                foreach (Venda venda in listaVendasPorVendedor)
                {
                    System.Data.DataRow row = dsVendaDia.Tables[0].NewRow();
                    row.SetField("Nome", venda.Vendedor.Id + " - " + venda.Vendedor.RazaoSocial);
                    row.SetField("Quantidade", venda.Quantidade);
                    row.SetField("Valor", venda.ValorFinal);
                    dsVendaDia.Tables[0].Rows.Add(row);
                }
            }
        }
        private void getVendasPorData()
        {
            String dataInicial = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            String dataFinal = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";

            IList<Venda> listaVendas = vendaController.selecionarVendaPorPeriodo(Sessao.empresaFilialLogada, dataInicial, dataFinal);
            if (listaVendas.Count > 0)
            {
                decimal totalVendas = 0;
                int totalizador = 0;
                foreach (Venda venda in listaVendas)
                {
                    totalVendas = totalVendas + venda.ValorFinal;
                    totalizador++;
                }
                lblClientesAtendidos.Text = totalizador.ToString();
                lblVendas.Text = totalVendas.ToString("C2", CultureInfo.CurrentCulture);
            }
        }
    }
}
