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
    public partial class FrmImprimirDuplicata : Form
    {
        Pessoa cliente = new Pessoa();
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        public FrmImprimirDuplicata(Pessoa cliente, IList<ContaReceber> listaContaReceber)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.listaContaReceber = listaContaReceber;
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

                    ReportParameter[] p = new ReportParameter[19];
                    p[0] = (new ReportParameter("EMPRESA", receber.EmpresaFilial.NomeFantasia));
                    p[1] = (new ReportParameter("RAZAOSOCIAL", receber.EmpresaFilial.RazaoSocial));
                    p[2] = (new ReportParameter("ENDERECOEMPRESA", receber.EmpresaFilial.Endereco.Logradouro +", "+ receber.EmpresaFilial.Endereco.Numero));
                    p[3] = (new ReportParameter("CEPEMPRESA", receber.EmpresaFilial.Endereco.Cep));
                    p[4] = (new ReportParameter("INSCRICAOEMPRESA", receber.EmpresaFilial.InscricaoEstadual));
                    p[5] = (new ReportParameter("CNPJEMPRESA", cnpjFormatado));
                    p[6] = (new ReportParameter("DATAEMISSAO", receber.Data.ToShortDateString()));
                    //p[7] = (new ReportParameter("VALORTOTAL", Sessao.empresaFilialLogada.Endereco.Cep));
                    //p[8] = (new ReportParameter("NUMERO", Sessao.empresaFilialLogada.Endereco.Cep));
                    //p[9] = (new ReportParameter("VALORPRESTACAO", Sessao.empresaFilialLogada.Endereco.Cep));
                    //p[10] = (new ReportParameter("NUMEROORDEM", Sessao.empresaFilialLogada.Endereco.Cep));
                    //p[11] = (new ReportParameter("VENCIMENTO", "16/04/2023"));
                    p[7] = (new ReportParameter("CLIENTE", cliente.RazaoSocial));
                    string enderecoClienteFormatado = "";
                    string cidadeCliente = "";
                    if (cliente.EnderecoPrincipal != null)
                    {
                        enderecoClienteFormatado = cliente.EnderecoPrincipal.Logradouro + ", " + cliente.EnderecoPrincipal.Numero + " - " + cliente.EnderecoPrincipal.Bairro;
                        if (cliente.EnderecoPrincipal.Cidade != null)
                            cidadeCliente = cliente.EnderecoPrincipal.Cidade.Descricao;
                        cepCliente = cliente.EnderecoPrincipal.Cep;
                    }
                    p[8] = (new ReportParameter("ENDERECOCLIENTE", enderecoClienteFormatado));
                    p[9] = (new ReportParameter("CIDADECLIENTE", cidadeCliente));
                    p[10] = (new ReportParameter("PRACAPAGAMENTO", receber.EmpresaFilial.Endereco.Cidade.Descricao + "-" + receber.EmpresaFilial.Endereco.Cidade.Estado.Uf));
                    p[11] = (new ReportParameter("CPFCLIENTE", cpfCliente));
                    p[12] = (new ReportParameter("CEPCLIENTE", cepCliente));
                    p[13] = (new ReportParameter("INSCRICAOCLIENTE", receber.Cliente.InscricaoEstadual));
                    p[14] = (new ReportParameter("VALOREXTENSO", "NAO UTILIZADO"));
                    p[15] = (new ReportParameter("JUROCONFIGURADO", Sessao.parametroSistema.Juro));
                    p[16] = (new ReportParameter("MULTACONFIGURADO", Sessao.parametroSistema.Multa));
                    p[17] = (new ReportParameter("LOGO", Sessao.parametroSistema.Logo));
                    p[18] = (new ReportParameter("FONEEMPRESA", GenericaDesktop.formatarFone(receber.EmpresaFilial.DddPrincipal + receber.EmpresaFilial.TelefonePrincipal)));

                    reportViewer1.LocalReport.SetParameters(p);
                }

                dsParcelaDuplicata.Duplicata.AddDuplicataRow(int.Parse(receber.Parcela), 
                    receber.ValorParcela, receber.ValorTotalOrigem + valorSinalEntrada, receber.Vencimento.ToShortDateString(), 
                    receber.Id.ToString(), receber.Documento, ConverterMoedaPorExtenso.toExtenso(receber.ValorParcela));
            }
            this.reportViewer1.RefreshReport();
        }
        private void FrmImprimirDuplicata_Load(object sender, EventArgs e)
        {
            gerar();
            //this.reportViewer1.RefreshReport();
        }
    }
}
