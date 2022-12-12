using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Condicionais
{
    public partial class FrmImprimirCondicional : Form
    {
        Condicional condicional = new Condicional();
        public FrmImprimirCondicional(Condicional condicional)
        {
            InitializeComponent();
            this.condicional = condicional;
        }

        private void FrmImprimirCondicional_Load(object sender, EventArgs e)
        {
            gerarRelatorio();
        }

        private void gerarRelatorio()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsVendaX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaX.Name = "dsCondicional";
            dsVendaX.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaX);

            this.reportViewer1.LocalReport.DisplayName = "Condicional " + condicional.Id;

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
            if (condicional.Filial.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(condicional.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (condicional.Filial.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(condicional.Filial.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cnpjFormatado = "";
            }
            //Cidade e Bairro da empresa
            if (condicional.Filial.Endereco != null)
            {
                cidadeEmpresa = condicional.Filial.Endereco.Cidade.Descricao + "-" + condicional.Filial.Endereco.Cidade.Estado.Uf;
                logradouroEmpresa = condicional.Filial.Endereco.Logradouro + ", " + condicional.Filial.Endereco.Numero + " " + condicional.Filial.Endereco.Complemento;
                bairroEmpresa = condicional.Filial.Endereco.Bairro;
            }
            string foneEmp = GenericaDesktop.RemoveCaracteres(condicional.Filial.DddPrincipal + condicional.Filial.TelefonePrincipal);
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
            if (condicional.Cliente != null)
            {
                nomeCliente = condicional.Cliente.RazaoSocial;
                //CNPJ DO CLIENTE
                if (condicional.Cliente.Cnpj.Length == 14)
                {
                    cpfFormatado = Convert.ToUInt64(condicional.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00");
                }
                else if (condicional.Cliente.Cnpj.Length == 11)
                {
                    cpfFormatado = Convert.ToUInt64(condicional.Cliente.Cnpj).ToString(@"000\.000\.000\-00");
                }
                else
                {
                    cpfFormatado = "";
                }
                //Cidade e Bairro do cliente
                String cidadeCliente = "";
                if (condicional.Cliente.EnderecoPrincipal != null)
                {
                    numeroEndereco = condicional.Cliente.EnderecoPrincipal.Numero;
                    bairroCliente = condicional.Cliente.EnderecoPrincipal.Bairro;
                    cidadeCliente = condicional.Cliente.EnderecoPrincipal.Cidade.Descricao + " - " + condicional.Cliente.EnderecoPrincipal.Cidade.Estado.Uf;

                    enderecoCliente = condicional.Cliente.EnderecoPrincipal.Logradouro + ", " +
                    numeroEndereco + " " + condicional.Cliente.EnderecoPrincipal.Complemento + " " + bairroCliente;
                }

                string foneCliente = "";
                if (condicional.Cliente.PessoaTelefone != null)
                {
                    foneCliente = GenericaDesktop.RemoveCaracteres(condicional.Cliente.PessoaTelefone.Ddd + condicional.Cliente.PessoaTelefone.Telefone);
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

            string vendedor = "";
            if (condicional.Vendedor != null)
                vendedor = "Vendedor(a): " + condicional.Vendedor.RazaoSocial.Substring(0, condicional.Vendedor.RazaoSocial.IndexOf(" "));
            ReportParameter[] p = new ReportParameter[9];
            p[0] = (new ReportParameter("Empresa", condicional.Filial.NomeFantasia));
            p[1] = (new ReportParameter("CNPJ", cnpjFormatado));
            p[2] = (new ReportParameter("FoneEmpresa", foneEmp));
            p[3] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[4] = (new ReportParameter("Cliente", nomeCliente + " - " + cpfFormatado));
            p[5] = (new ReportParameter("EnderecoCliente", enderecoCliente));
            p[6] = (new ReportParameter("Vendedor", vendedor));
            p[7] = (new ReportParameter("Id", condicional.Id.ToString()));
            p[8] = (new ReportParameter("Data", condicional.Data.ToShortDateString() + " " + condicional.Data.ToShortTimeString()));
            reportViewer1.LocalReport.SetParameters(p);

            IList<CondicionalProduto> listaProdutos = new List<CondicionalProduto>();
            CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
            listaProdutos = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);
            foreach (CondicionalProduto condicionalProduto in listaProdutos)
            {
                string descricaoAbreviada = condicionalProduto.Produto.Descricao;
                if (descricaoAbreviada.Length > 33)
                    descricaoAbreviada = descricaoAbreviada.Substring(0, 33);
                dsCondicional.Condicional.AddCondicionalRow(condicional.Id, condicional.Cliente.RazaoSocial, 
                    condicional.Filial.RazaoSocial, condicionalProduto.Id.ToString() + condicionalProduto.Produto.IdComplementar, 
                    condicionalProduto.ValorUnitario, condicionalProduto.ValorTotal, descricaoAbreviada, 
                    condicionalProduto.Quantidade);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
