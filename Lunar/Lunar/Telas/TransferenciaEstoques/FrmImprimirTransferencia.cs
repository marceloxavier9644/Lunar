using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.TransferenciaEstoques
{
    public partial class FrmImprimirTransferencia : Form
    {
        TransferenciaEstoque transferenciaEstoque = new TransferenciaEstoque();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        TransferenciaEstoqueProdutoController transferenciaEstoqueProdutoController = new TransferenciaEstoqueProdutoController();
        public FrmImprimirTransferencia(TransferenciaEstoque transferenciaEstoque)
        {
            InitializeComponent();
            this.transferenciaEstoque = transferenciaEstoque;
        }

        private void FrmImprimirTransferencia_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsOrdem = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdem.Name = "dsTransferenciaEstoque";         
            dsOrdem.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdem);
            this.reportViewer1.LocalReport.DisplayName = "Transferência " + transferenciaEstoque.Id;

            ReportParameter[] p = new ReportParameter[4];
            p[0] = (new ReportParameter("FilialOrigem", transferenciaEstoque.EmpresaOrigem.RazaoSocial + " CNPJ: " + genericaDesktop.FormatarCNPJ(transferenciaEstoque.EmpresaOrigem.Cnpj)));
            p[1] = (new ReportParameter("FilialDestino", transferenciaEstoque.EmpresaDestino.RazaoSocial + " CNPJ: " + genericaDesktop.FormatarCNPJ(transferenciaEstoque.EmpresaDestino.Cnpj)));
            p[2] = (new ReportParameter("DataTransferencia", transferenciaEstoque.Data.ToShortDateString()));
            p[3] = (new ReportParameter("Id", transferenciaEstoque.Id.ToString()));
            reportViewer1.LocalReport.SetParameters(p);

            IList<TransferenciaEstoqueProduto> listaProduto = transferenciaEstoqueProdutoController.selecionarProdutosPorTransferencia(transferenciaEstoque.Id);
            if(listaProduto.Count > 0)
            {
                foreach(TransferenciaEstoqueProduto transferenciaEstoqueProduto in listaProduto)
                {
                    dsTransferenciaEstoque.Transferencia.AddTransferenciaRow(transferenciaEstoqueProduto.Produto.Id.ToString(), transferenciaEstoqueProduto.Produto.Descricao, transferenciaEstoqueProduto.Quantidade, transferenciaEstoqueProduto.ValorUnitario, transferenciaEstoqueProduto.Desconto, transferenciaEstoqueProduto.ValorFinal);
                }
                this.reportViewer1.RefreshReport();
            }    
        }
    }
}
