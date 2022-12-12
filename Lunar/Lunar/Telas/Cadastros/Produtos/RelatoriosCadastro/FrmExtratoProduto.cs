using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.RelatoriosCadastro
{
    public partial class FrmExtratoProduto : Form
    {
        EstoqueController estoqueController = new EstoqueController();
        IList<Estoque> lista = new List<Estoque>();
        GenericaDesktop generica = new GenericaDesktop();
        EstoqueDAO estoqueDAO = new EstoqueDAO();
        bool conciliado = false;
        public FrmExtratoProduto(Produto produto, bool conciliado)
        {
            InitializeComponent();
            this.conciliado = conciliado;
            gerar(produto);
        }

        private void FrmExtratoProduto_Load(object sender, EventArgs e)
        {

        }

        private void gerar(Produto produto)
        {
            lista = estoqueController.selecionarEstoqueMovimentoPorProduto(Sessao.empresaFilialLogada, produto.Id, conciliado);
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Extrato do Produto: " + produto.Id + produto.IdComplementar;
            Microsoft.Reporting.WinForms.ReportDataSource dsInvent = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsInvent.Name = "dsExtratoProduto";
            dsInvent.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsInvent);

            EmpresaFilial empresaSelecionada = Sessao.empresaFilialLogada;

            string comp = "";
            if (!String.IsNullOrEmpty(empresaSelecionada.Endereco.Complemento))
                comp = " - " + empresaSelecionada.Endereco.Complemento;
            if (!String.IsNullOrEmpty(empresaSelecionada.Endereco.Bairro))
                comp = comp + " - " + empresaSelecionada.Endereco.Bairro;

            string fone = GenericaDesktop.formatarFone(empresaSelecionada.DddPrincipal + empresaSelecionada.TelefonePrincipal.Trim());

            ReportParameter[] p = new ReportParameter[4];
            p[0] = (new ReportParameter("Data", "PERÍODO INTEGRAL"));
            p[1] = (new ReportParameter("Produto", produto.Id + produto.IdComplementar + " - " + produto.Descricao));
            p[2] = (new ReportParameter("Filial", empresaSelecionada.NomeFantasia + " (" + empresaSelecionada.Id.ToString() + ")"));
            double saldoAnterior = 0;
            if(conciliado == true)
                saldoAnterior = estoqueDAO.calcularEstoqueConciliadoPorProduto(produto.Id, empresaSelecionada);
            else
                saldoAnterior = estoqueDAO.calcularEstoqueNaoConciliadoPorProduto(produto.Id, empresaSelecionada);

            p[3] = (new ReportParameter("SaldoAnterior", "Saldo de estoque do produto em " + DateTime.Now.AddDays(-1).ToShortDateString() + ": " + saldoAnterior.ToString() + " " + produto.UnidadeMedida.Sigla));

            reportViewer1.LocalReport.SetParameters(p);

            //double saldo = saldoAnterior;
            double saldo = 0;
            foreach (Estoque estoque in lista)
            {
                double quantEntrada = 0;
                double quantSaida = 0;
                if (estoque.Entrada == true)
                    quantEntrada = estoque.Quantidade;
                else
                    quantSaida = estoque.Quantidade;
                String fornecedor = "";
                //esse vai ser usado qdo passar datas
                //saldo = saldo + (quantEntrada - quantSaida);

                //esse aqui qdo periodo for integral
                saldo = saldo + (quantEntrada - quantSaida);
                if (estoque.Pessoa != null)
                    fornecedor = estoque.Pessoa.NomeFantasia;
                dsExtratoProduto.ProdutoEstoque.AddProdutoEstoqueRow(estoque.Produto.Id + estoque.Produto.IdComplementar, estoque.Produto.Descricao, estoque.Descricao, estoque.DataEntradaSaida.ToShortDateString() + " " + estoque.DataEntradaSaida.ToShortTimeString(), quantEntrada, quantSaida, saldo, fornecedor);
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
