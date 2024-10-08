using Ghostscript.NET.Rasterizer;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmImprimirTicketVenda : Form
    {
        Venda venda = new Venda();
        bool imprimirDireto = false;
        public FrmImprimirTicketVenda(Venda venda, bool imprimirDireto)
        {
            InitializeComponent();
            this.venda = venda;
            this.imprimirDireto = imprimirDireto;
        }

        private void FrmImprimirTicketVenda_Load(object sender, EventArgs e)
        {
            gerarRelatorio();
        }

        private void gerarRelatorio()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsVendaX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaX.Name = "dsVenda";
            dsVendaX.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaX);

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

            string nomeCliente = "";
            if (venda.Cliente != null)
            {
                nomeCliente = venda.Cliente.RazaoSocial;
                //CNPJ DO CLIENTE
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

                string foneCliente = "";
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
            else
            {
                nomeCliente = "CONSUMIDOR NÃO IDENTIFICADO";
                cpfFormatado = "000.000.000-00";
            }

            
            IList<VendaFormaPagamento> listaPagamento = new List<VendaFormaPagamento>();
            VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
            listaPagamento = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);

            string descricaoPagamento = "";
            if(listaPagamento.Count > 0)
            {
                foreach(VendaFormaPagamento vendaFormaPagamento in listaPagamento)
                {
                    if(String.IsNullOrEmpty(descricaoPagamento))
                        descricaoPagamento = descricaoPagamento + vendaFormaPagamento.FormaPagamento.Descricao + " " + vendaFormaPagamento.ValorRecebido.ToString("C");
                    else
                        descricaoPagamento = descricaoPagamento + " " + vendaFormaPagamento.FormaPagamento.Descricao + " " + vendaFormaPagamento.ValorRecebido.ToString("C");
                }
            }

            string vendedor = "";
            if (venda.Vendedor != null)
                vendedor = "Vendedor(a): " + venda.Vendedor.RazaoSocial.Substring(0, venda.Vendedor.RazaoSocial.IndexOf(" "));
            ReportParameter[] p = new ReportParameter[11];
            p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia));
            p[1] = (new ReportParameter("CnpjEmpresa", cnpjFormatado));
            p[2] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[3] = (new ReportParameter("DadosCliente", nomeCliente));
            p[4] = (new ReportParameter("NumeroPedido", venda.Id.ToString()));
            p[5] = (new ReportParameter("DescontoRecebido", string.Format("{0:0.00}", venda.ValorDesconto)));
            p[6] = (new ReportParameter("Acrescimo", string.Format("{0:0.00}", venda.ValorAcrescimo)));
            p[7] = (new ReportParameter("TotalGeral", string.Format("{0:0.00}", venda.ValorFinal)));
            p[8] = (new ReportParameter("Observacoes", "Pedido Realizado em " + venda.DataVenda.ToShortDateString() + " " + venda.DataVenda.ToLongTimeString() + "\n" + descricaoPagamento + "\n" + vendedor));
            p[9] = (new ReportParameter("CpfCliente", cpfFormatado));
            p[10] = (new ReportParameter("FoneEmpresa", foneEmp));
            reportViewer1.LocalReport.SetParameters(p);

            IList<VendaItens> listaProdutos = new List<VendaItens>();
            VendaItensController vendaItensController = new VendaItensController();
            listaProdutos = vendaItensController.selecionarProdutosPorVenda(venda.Id);
            foreach(VendaItens vendaItem in listaProdutos)
            {
                string descricaoAbreviada = vendaItem.Produto.Descricao;
                if (descricaoAbreviada.Length > 33)
                    descricaoAbreviada = descricaoAbreviada.Substring(0, 33);
                dsVenda.Venda.AddVendaRow(venda.Id, nomeCliente, venda.EmpresaFilial.RazaoSocial, vendaItem.Produto.Id.ToString(), descricaoAbreviada,
                    vendaItem.ValorProduto, vendaItem.ValorDesconto, vendaItem.Quantidade, vendaItem.ValorFinal);
            }
            this.reportViewer1.RefreshReport();

            //if(imprimirDireto == true)
            //    ImprimirRelatorio();
        }

        


    }
}
