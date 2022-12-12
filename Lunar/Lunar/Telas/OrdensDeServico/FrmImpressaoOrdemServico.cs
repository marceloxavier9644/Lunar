using Lunar.Telas.OrdensDeServico.DataSetOrdemServico;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmImpressaoOrdemServico : Form
    {
        OrdemServico ordemServico = new OrdemServico();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmImpressaoOrdemServico(OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
            gerarImpressao();
        }

        private void gerarImpressao()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsOrdem = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdem.Name = "dsOrdemServico";
            dsOrdem.Value = this.bindingSourceOrdem;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdem);

            Microsoft.Reporting.WinForms.ReportDataSource dsOrdemProd = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdemProd.Name = "dsOrdemServicoProduto";
            dsOrdemProd.Value = this.bindingSourceProd;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdemProd);

            Microsoft.Reporting.WinForms.ReportDataSource dsOrdemServico = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdemServico.Name = "dsOrdemServicoServico";
            dsOrdemServico.Value = this.bindingSourceServico;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdemServico);

            this.reportViewer1.LocalReport.DisplayName = "Ordem de Serviço " + ordemServico.Id;
            //Microsoft.Reporting.WinForms.ReportDataSource dsOrdemExame = new Microsoft.Reporting.WinForms.ReportDataSource();
            //dsOrdemExame.Name = "dsOrdemServicoExame";
            //dsOrdemExame.Value = this.bindingSourceExame;
            //this.reportViewer1.LocalReport.DataSources.Add(dsOrdemExame);

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
            if (ordemServico.Filial.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (ordemServico.Filial.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cnpjFormatado = "";
            }
            //Cidade e Bairro da empresa
            if (ordemServico.Filial.Endereco != null)
            {
                cidadeEmpresa = ordemServico.Filial.Endereco.Cidade.Descricao + "-" + ordemServico.Filial.Endereco.Cidade.Estado.Uf;
                logradouroEmpresa = ordemServico.Filial.Endereco.Logradouro + ", " + ordemServico.Filial.Endereco.Numero + " " + ordemServico.Filial.Endereco.Complemento;
                bairroEmpresa = ordemServico.Filial.Endereco.Bairro;
            }

            //CNPJ DO CLIENTE
            if (ordemServico.Cliente.Cnpj.Length == 14)
            {
                cpfFormatado = Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (ordemServico.Cliente.Cnpj.Length == 11)
            {
                cpfFormatado = Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cpfFormatado = "";
            }
            //Cidade e Bairro do cliente
            String cidadeCliente = "";
            if (ordemServico.Cliente.EnderecoPrincipal != null)
            {
                numeroEndereco = ordemServico.Cliente.EnderecoPrincipal.Numero;
                bairroCliente = ordemServico.Cliente.EnderecoPrincipal.Bairro;
                cidadeCliente = ordemServico.Cliente.EnderecoPrincipal.Cidade.Descricao + " - " + ordemServico.Cliente.EnderecoPrincipal.Cidade.Estado.Uf;

                enderecoCliente = ordemServico.Cliente.EnderecoPrincipal.Logradouro + ", " +
                numeroEndereco + " " + ordemServico.Cliente.EnderecoPrincipal.Complemento + " " + bairroCliente;  
            }
            string foneEmp = GenericaDesktop.RemoveCaracteres(ordemServico.Filial.DddPrincipal + ordemServico.Filial.TelefonePrincipal);
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
            string foneCliente = "";
            if (ordemServico.Cliente.PessoaTelefone != null)
            {
                foneCliente = GenericaDesktop.RemoveCaracteres(ordemServico.Cliente.PessoaTelefone.Ddd + ordemServico.Cliente.PessoaTelefone.Telefone);
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

            ReportParameter[] p = new ReportParameter[14];
            p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia));
            p[1] = (new ReportParameter("OrdemServicoID", ordemServico.Id.ToString()));
            p[2] = (new ReportParameter("CNPJ", cnpjFormatado));
            p[3] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[4] = (new ReportParameter("Telefone", foneEmp));
            p[5] = (new ReportParameter("logo", Sessao.parametroSistema.Logo));
            p[6] = (new ReportParameter("Cliente", ordemServico.Cliente.RazaoSocial));
            p[7] = (new ReportParameter("CpfCliente", cpfFormatado));
            p[8] = (new ReportParameter("EnderecoCliente", enderecoCliente));
            p[9] = (new ReportParameter("TelefoneCliente", foneCliente));
            p[10] = (new ReportParameter("Observacoes", ordemServico.Observacoes));
            p[11] = (new ReportParameter("CidadeEmpresa", cidadeEmpresa));
            p[12] = (new ReportParameter("InscricaoEstadual", ordemServico.Filial.InscricaoEstadual));
            p[13] = (new ReportParameter("CidadeCliente", cidadeCliente));
            reportViewer1.LocalReport.SetParameters(p);

            //dsOrdemServico ds = new dsOrdemServico();

            //var rowOS = ds.OrdemServico.NewOrdemServicoRow();
            //rowOS = ds.OrdemServico.AddOrdemServicoRow(ordemServico.Id, ordemServico.Cliente.RazaoSocial, ordemServico.Cliente.Id.ToString(),
            //    ordemServico.DataAbertura.ToShortDateString(), ordemServico.DataEncerramento.ToShortDateString(),
            //    ordemServico.ValorProduto, ordemServico.ValorServico, ordemServico.ValorTotal);

            dsOrdemServico1.OrdemServico.AddOrdemServicoRow(ordemServico.Id, ordemServico.Cliente.RazaoSocial, ordemServico.Cliente.Id.ToString(),
                ordemServico.DataAbertura.ToShortDateString(), ordemServico.DataEncerramento.ToShortDateString(),
                ordemServico.ValorProduto, ordemServico.ValorServico, ordemServico.ValorTotal);

            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            IList<OrdemServicoProduto> listaProdutos = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            if (listaProdutos.Count > 0)
            {

                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
                {
                    dsOrdemServicoProduto.OrdemServicoProduto.AddOrdemServicoProdutoRow(ordemServicoProduto.Id, ordemServicoProduto.Produto.Id.ToString(),
                        ordemServicoProduto.DescricaoProduto, ordemServicoProduto.ValorUnitario, ordemServicoProduto.Desconto,
                        ordemServicoProduto.Acrescimo, ordemServicoProduto.Quantidade, ordemServicoProduto.ValorTotal, ordemServico.Id);
                }
            }
            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServicos = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            if (listaServicos.Count > 0)
            {
                foreach (OrdemServicoServico ordemServicoServico in listaServicos)
                {
                    dsOrdemServicoServico.OrdemServicoServico.AddOrdemServicoServicoRow(ordemServicoServico.Id, ordemServicoServico.Servico.Id.ToString(),
                        ordemServicoServico.DescricaoServico, ordemServicoServico.ValorUnitario, ordemServicoServico.Desconto,
                        ordemServicoServico.Acrescimo, ordemServicoServico.Quantidade, ordemServicoServico.ValorTotal, ordemServico.Id);
                }
            }
            this.reportViewer1.RefreshReport();
        }
        private void FrmImpressaoOrdemServico_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
