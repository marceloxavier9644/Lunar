using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmImprimirTicketVendaA4 : Form
    {
        Venda venda = new Venda();
        IList<VendaItens> listaVendas = new List<VendaItens>();
        public FrmImprimirTicketVendaA4(Venda venda, IList<VendaItens> listaVendas)
        {
            InitializeComponent();
            this.venda = venda;
            this.listaVendas = listaVendas;
        }

        private void FrmImprimirTicketVendaA4_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsVendaAc = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaAc.Name = "dsVenda";
            dsVendaAc.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaAc);

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

            // Definir o zoom para 100%
            reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            this.reportViewer1.LocalReport.DisplayName = "Venda " + venda.Id;
            String cnpjFormatado = "";
            String cpfFormatado = "";
            String enderecoCliente = "";
            String bairroCliente = "";
            String cidadeEmpresa = "";
            String bairroEmpresa = "";
            String numeroEndereco = "";
            String logradouroEmpresa = "";
            String foneEmpresa = "";
            String foneCliente = "";
            String nomeCliente = "";
            String nomeAssinatura = "";
            //CNPJ DA EMPRESA
            if (venda.EmpresaFilial.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(venda.EmpresaFilial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (venda.EmpresaFilial.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(venda.EmpresaFilial.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cnpjFormatado = "";
            }
            //Cidade e Bairro da empresa
            if (venda.EmpresaFilial.Endereco != null)
            {
                cidadeEmpresa = venda.EmpresaFilial.Endereco.Cidade.Descricao + "-" + venda.EmpresaFilial.Endereco.Cidade.Estado.Uf;
                logradouroEmpresa = venda.EmpresaFilial.Endereco.Logradouro + ", " + venda.EmpresaFilial.Endereco.Numero + " " + venda.EmpresaFilial.Endereco.Complemento;
                bairroEmpresa = venda.EmpresaFilial.Endereco.Bairro;
            }
            string foneEmp = GenericaDesktop.RemoveCaracteres(venda.EmpresaFilial.DddPrincipal + venda.EmpresaFilial.TelefonePrincipal);
            foneEmp = foneEmp.Trim();
            if (foneEmp.Length == 11)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"(00) 0 0000-0000");
            }
            else if (foneEmp.Length == 9)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"00000-0000");
            }
            else if (foneEmp.Length == 10)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"(00) 0000-0000");
            }

            //DADOS DO CLIENTE
            if (venda.Cliente != null)
            {
                nomeCliente = venda.Cliente.RazaoSocial;
                nomeAssinatura = venda.Cliente.RazaoSocial;
                if (venda.Cliente.Cnpj.Length == 14)
                {
                    cpfFormatado = Convert.ToUInt64(venda.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00");
                }
                else if (venda.Cliente.Cnpj.Length == 11)
                {
                    cpfFormatado = Convert.ToUInt64(venda.Cliente.Cnpj).ToString(@"000\.000\.000\-00");
                }
                else
                {
                    cpfFormatado = "";
                }
                //Cidade e Bairro do cliente
                String cidadeCliente = "";
                if (venda.Cliente.EnderecoPrincipal != null)
                {
                    numeroEndereco = venda.Cliente.EnderecoPrincipal.Numero;
                    bairroCliente = venda.Cliente.EnderecoPrincipal.Bairro;
                    cidadeCliente = venda.Cliente.EnderecoPrincipal.Cidade.Descricao + " - " + venda.Cliente.EnderecoPrincipal.Cidade.Estado.Uf;

                    enderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " +
                    numeroEndereco + " " + venda.Cliente.EnderecoPrincipal.Complemento + " " + bairroCliente;
                }
                if (venda.Cliente.PessoaTelefone != null)
                {
                    foneCliente = GenericaDesktop.RemoveCaracteres(venda.Cliente.PessoaTelefone.Ddd + venda.Cliente.PessoaTelefone.Telefone);
                    foneCliente = foneCliente.Trim();
                    if (foneCliente.Length == 11)
                    {
                        foneCliente = long.Parse(foneCliente).ToString(@"(00) 0 0000-0000");
                    }
                    else if (foneCliente.Length == 9)
                    {
                        foneCliente = long.Parse(foneCliente).ToString(@"00000-0000");
                    }
                    else if (foneCliente.Length == 10)
                    {
                        foneCliente = long.Parse(foneCliente).ToString(@"(00) 0000-0000");
                    }
                }
            }
            if (String.IsNullOrEmpty(nomeAssinatura))
                nomeAssinatura = "CLIENTE";
            ReportParameter[] p = new ReportParameter[17];
            p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia));
            p[1] = (new ReportParameter("VendaID", venda.Id.ToString()));
            p[2] = (new ReportParameter("CnpjEmpresa", cnpjFormatado));
            p[3] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[4] = (new ReportParameter("FoneEmpresa", foneEmp));
            p[5] = (new ReportParameter("Logo", Sessao.parametroSistema.Logo));
            p[6] = (new ReportParameter("NomeCliente", nomeCliente));
            p[7] = (new ReportParameter("CpfCliente", cpfFormatado));
            p[8] = (new ReportParameter("EnderecoCliente", enderecoCliente));
            p[9] = (new ReportParameter("FoneCliente", foneCliente));
            p[10] = (new ReportParameter("Observacoes", venda.Observacoes));
            p[11] = (new ReportParameter("DataVenda", venda.DataVenda.ToShortDateString()));
            p[12] = (new ReportParameter("ValorBruto", venda.ValorProdutos.ToString("C")));
            p[13] = (new ReportParameter("Desconto", venda.ValorDesconto.ToString("C")));
            p[14] = (new ReportParameter("Acrescimo", venda.ValorAcrescimo.ToString("C")));
            p[15] = (new ReportParameter("ValorLiquido", venda.ValorFinal.ToString("C")));
            p[16] = (new ReportParameter("Assinatura", nomeAssinatura));
            reportViewer1.LocalReport.SetParameters(p);

            foreach(VendaItens vendaItens in listaVendas)
            {
                dsVenda.Venda.AddVendaRow(venda.Id, nomeCliente, venda.EmpresaFilial.NomeFantasia, vendaItens.Produto.Id.ToString(),
                    vendaItens.Produto.Descricao, vendaItens.ValorProduto, vendaItens.ValorProduto - (vendaItens.ValorDesconto/decimal.Parse(vendaItens.Quantidade.ToString())), vendaItens.Quantidade, vendaItens.ValorFinal);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
