using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber.Reports
{
    public partial class FrmExtratoCliente : Form
    {
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        public FrmExtratoCliente(IList<ContaReceber> listaContaReceber)
        {
            InitializeComponent();
            this.listaContaReceber = listaContaReceber;
        }

        private void FrmExtratoCliente_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "ExtratoCliente";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource();
            ds.Name = "dsContaReceber";
            ds.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(ds);

            String cnpjFormatado = "";
            String cidadeEmpresa = "";
            String cpfCliente = "";
            string cepCliente = "";
            int cont = 0;
            foreach (ContaReceber receber in listaContaReceber)
            {
                cont++;
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

                    ReportParameter[] p = new ReportParameter[12];
                    p[0] = (new ReportParameter("RAZAOSOCIAL", receber.EmpresaFilial.RazaoSocial));
                    p[1] = (new ReportParameter("ENDERECOEMPRESA", receber.EmpresaFilial.Endereco.Logradouro + ", " + receber.EmpresaFilial.Endereco.Numero));
                    p[2] = (new ReportParameter("CEPEMPRESA", receber.EmpresaFilial.Endereco.Cep));
                    p[3] = (new ReportParameter("INSCRICAOEMPRESA", receber.EmpresaFilial.InscricaoEstadual));
                    p[4] = (new ReportParameter("CNPJEMPRESA", cnpjFormatado));
                    p[5] = (new ReportParameter("DATAEMISSAO", DateTime.Now.ToShortDateString()));
                    p[6] = (new ReportParameter("FONEEMPRESA", GenericaDesktop.formatarFone(receber.EmpresaFilial.DddPrincipal + receber.EmpresaFilial.TelefonePrincipal)));
                    p[7] = (new ReportParameter("NOMECLIENTE", receber.Cliente.RazaoSocial));
                    string enderecoClienteFormatado = "";
                    string cidadeCliente = "";
                    string bairroCliente = "";
                    if (receber.Cliente.EnderecoPrincipal != null)
                    {
                        enderecoClienteFormatado = receber.Cliente.EnderecoPrincipal.Logradouro + ", " + receber.Cliente.EnderecoPrincipal.Numero + " - " + receber.Cliente.EnderecoPrincipal.Bairro;
                        if (receber.Cliente.EnderecoPrincipal.Cidade != null)
                            cidadeCliente = receber.Cliente.EnderecoPrincipal.Cidade.Descricao;
                        cepCliente = receber.Cliente.EnderecoPrincipal.Cep;
                        bairroCliente = receber.Cliente.EnderecoPrincipal.Bairro;
                    }
                    p[8] = (new ReportParameter("ENDERECOCLIENTE", enderecoClienteFormatado));
                    p[9] = (new ReportParameter("CPFCLIENTE", cpfCliente));
                    p[10] = (new ReportParameter("LOGO", Sessao.parametroSistema.Logo));
                    p[11] = (new ReportParameter("BAIRROCLIENTE", bairroCliente));

                    reportViewer1.LocalReport.SetParameters(p);
                }

                dsContaReceber.ContaReceber.AddContaReceberRow(receber.Cliente.Id, receber.Cliente.RazaoSocial, 
                    receber.Data.ToShortDateString(), receber.Vencimento.ToShortDateString(), receber.Documento, 
                    receber.Parcela, receber.ValorParcela, receber.Multa, receber.Juro, receber.ValorTotal,receber.Origem);
            }
            this.reportViewer1.RefreshReport();

        }
    }
}
