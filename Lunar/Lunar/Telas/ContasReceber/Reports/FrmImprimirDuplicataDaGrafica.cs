using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber.Reports
{
    public partial class FrmImprimirDuplicataDaGrafica : Form
    {
        Pessoa cliente = new Pessoa();
        GenericaDesktop generica = new GenericaDesktop();
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        public FrmImprimirDuplicataDaGrafica(Pessoa cliente, IList<ContaReceber> listaContaReceber)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.listaContaReceber = listaContaReceber;
        }

        private void FrmImprimirDuplicataDaGrafica_Load(object sender, EventArgs e)
        {
            gerar();  
        }

        private void gerar()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Duplicata";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource();
            ds.Name = "dsParcelaDuplicata";
            ds.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(ds);

            String cnpjFormatado = "";
            String cidadeEmpresa = "";
            String cpfCliente = "";
            string cepCliente = "";
            int cont = 0;
            decimal valorSinalEntrada = 0;
            foreach (ContaReceber receber in listaContaReceber)
            {
                cont++;
                //Verifica se tem entrada para sair no total da nota
                if (cont == 1)
                {
                    if (receber.OrdemServico != null)
                    {
                        if (receber.OrdemServico.Entrada == true)
                        {
                            CreditoClienteController creditoClienteController = new CreditoClienteController();
                            CreditoCliente creditoCliente = new CreditoCliente();
                            IList<CreditoCliente> listaCredito = creditoClienteController.selecionarCreditoPorClienteEOrigem(receber.OrdemServico.Cliente.Id, "ORDEMSERVICO", receber.OrdemServico.Id.ToString());
                            if (listaCredito.Count > 0)
                            {
                                foreach (CreditoCliente credit in listaCredito)
                                {
                                    valorSinalEntrada = valorSinalEntrada + credit.Valor;
                                }
                            }
                        }
                    }
                }
                if (cont == listaContaReceber.Count)
                {
                    //CNPJ DA EMPRESA
                    if (receber.EmpresaFilial.Cnpj.Length == 14)
                    {
                        cnpjFormatado = Convert.ToUInt64(receber.EmpresaFilial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
                    }
                    else if (receber.EmpresaFilial.Cnpj.Length == 11)
                    {
                        cnpjFormatado = Convert.ToUInt64(receber.EmpresaFilial.Cnpj).ToString(@"000\.000\.000\-00");
                    }
                    //CNPJ DA CLIENTE
                    if (receber.Cliente.Cnpj.Length == 14)
                    {
                        cpfCliente = Convert.ToUInt64(receber.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00");
                    }
                    else if (receber.Cliente.Cnpj.Length == 11)
                    {
                        cpfCliente = Convert.ToUInt64(receber.Cliente.Cnpj).ToString(@"000\.000\.000\-00");
                    }
                    //Cidade e Bairro da empresa
                    if (Sessao.empresaFilialLogada.Endereco != null)
                    {
                        cidadeEmpresa = receber.EmpresaFilial.Endereco.Cidade.Descricao + "-" + receber.EmpresaFilial.Endereco.Cidade.Estado.Uf;
                    }

                    ReportParameter[] p = new ReportParameter[10];
                    p[0] = (new ReportParameter("DATAEMISSAO", receber.Data.ToShortDateString()));
                    p[1] = (new ReportParameter("CLIENTE", cliente.RazaoSocial + " (" + cliente.Id.ToString()+")"));
                    string enderecoClienteFormatado = "";
                    string cidadeCliente = "";
                    string estadoCliente = "";
                    if (cliente.EnderecoPrincipal != null)
                    {
                        enderecoClienteFormatado = cliente.EnderecoPrincipal.Logradouro + ", " + cliente.EnderecoPrincipal.Numero + " - " + cliente.EnderecoPrincipal.Bairro;
                        if (cliente.EnderecoPrincipal.Cidade != null)
                        {
                            cidadeCliente = cliente.EnderecoPrincipal.Cidade.Descricao;
                            estadoCliente = cliente.EnderecoPrincipal.Cidade.Estado.Uf;
                        }
                        cepCliente = cliente.EnderecoPrincipal.Cep;
                        
                    }
                    p[2] = (new ReportParameter("ENDERECOCLIENTE", enderecoClienteFormatado.Replace("NAO INFORMADO", "")));
                    p[3] = (new ReportParameter("CIDADECLIENTE", cidadeCliente));
                    p[4] = (new ReportParameter("PRACAPAGAMENTO", receber.EmpresaFilial.Endereco.Cidade.Descricao + "-" + receber.EmpresaFilial.Endereco.Cidade.Estado.Uf));
                    p[5] = (new ReportParameter("CPFCLIENTE", cpfCliente));
                    p[6] = (new ReportParameter("CEPCLIENTE", cepCliente));
                    p[7] = (new ReportParameter("INSCRICAOCLIENTE", receber.Cliente.InscricaoEstadual));
                    p[8] = (new ReportParameter("VALOREXTENSO", "NAO"));
                    p[9] = (new ReportParameter("ESTADOCLIENTE", estadoCliente));
                    reportViewer1.LocalReport.SetParameters(p);
                }
                dsParcelaDuplicata.Duplicata.AddDuplicataRow(int.Parse(receber.Parcela),
 receber.ValorParcela, receber.ValorTotalOrigem + valorSinalEntrada, receber.Vencimento.ToShortDateString(),
 receber.Id.ToString(), receber.Documento, ConverterMoedaPorExtenso.toExtenso(receber.ValorParcela));

            }
            this.reportViewer1.RefreshReport();
        }
    }
}
