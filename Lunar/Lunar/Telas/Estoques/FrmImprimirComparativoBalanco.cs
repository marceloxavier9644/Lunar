using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.Estoques
{
    public partial class FrmImprimirComparativoBalanco : Form
    {
        BalancoEstoqueProdutoDAO balancoEstoqueProdutoDAO = new BalancoEstoqueProdutoDAO();
        BalancoEstoque balancoEstoque = new BalancoEstoque();
        public FrmImprimirComparativoBalanco(BalancoEstoque balancoEstoque)
        {
            InitializeComponent();
            this.balancoEstoque = balancoEstoque;
        }

        private void FrmImprimirComparativoBalanco_Load(object sender, EventArgs e)
        {
            gerarImpressao();
        }

        private void gerarImpressao()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsBalanco = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsBalanco.Name = "dsBalancoEstoque";
            dsBalanco.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsBalanco);

            this.reportViewer1.LocalReport.DisplayName = "Balanço de Estoque " + balancoEstoque.Descricao;

            String cnpjFormatado = "";

            //CNPJ DA EMPRESA
            if (balancoEstoque.Filial.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(balancoEstoque.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (balancoEstoque.Filial.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(balancoEstoque.Filial.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cnpjFormatado = "";
            }
           
            ReportParameter[] p = new ReportParameter[2];
            p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia + " " + cnpjFormatado));
            p[1] = (new ReportParameter("DataAjuste", balancoEstoque.DataAjuste.ToShortDateString()));
            reportViewer1.LocalReport.SetParameters(p);

            BalancoEstoqueProdutoController balancoEstoqueProdutoController = new BalancoEstoqueProdutoController();
            IList<BalancoEstoqueProduto> listaProduto = balancoEstoqueProdutoController.selecionarProdutosPorBalancoSemRepetirItem(balancoEstoque.Id);

            if (listaProduto.Count > 0) 
            {
                foreach (BalancoEstoqueProduto balancoEstoqueProduto in listaProduto) 
                {
                   double estoqData = calcularEstoqueData(balancoEstoqueProduto);
                   //double quantidadeItem = balancoEstoqueProdutoDAO.calcularQuantidadeDoMesmoProduto(balancoEstoqueProduto);
                    
                   dsBalancoEstoque.BalancoEstoque.AddBalancoEstoqueRow(balancoEstoque.Id.ToString(), balancoEstoqueProduto.Produto.Id.ToString() + 
                        balancoEstoqueProduto.Produto.IdComplementar, balancoEstoqueProduto.DescricaoProduto, estoqData,
                        balancoEstoqueProduto.Quantidade, balancoEstoqueProduto.Produto.ValorCusto);
                }
            }
            this.reportViewer1.RefreshReport();
        }

        private double calcularEstoqueData(BalancoEstoqueProduto balancoEstoqueProduto)
        {
            try
            {
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                DateTime dataAjust = new DateTime();
                dataAjust = balancoEstoque.DataAjuste;
                DateTime dataIni = balancoEstoque.DataAjuste;
                String dat = dataIni.ToString("yyyy-MM-dd");
                double estoqueConciliado = estoqueDAO.calcularEstoqueConciliadoPorProdutoeData(balancoEstoqueProduto.Produto.Id, Sessao.empresaFilialLogada, dat);
                if (balancoEstoqueProduto.Conciliado == true)
                    return estoqueConciliado;
                else
                {
                    double estoqueNaoConciliado = estoqueDAO.calcularEstoqueNaoConciliadoPorProdutoeData(balancoEstoqueProduto.Produto.Id, Sessao.empresaFilialLogada, dat);
                    return estoqueNaoConciliado;
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Ocorreu um erro no calculo do estoque anterior do produto: (" + balancoEstoqueProduto.Produto.Id.ToString() + ") " + erro.Message);
                return 0;
            }
        }
    }
}
