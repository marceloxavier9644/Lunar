using Lunar.Telas.OrdensDeServico.DataSetOrdemServico;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
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
            if (Sessao.empresaFilialLogada.Otica == false)
                gerarImpressao();
            else
            {
                gerarImpressaoOrdemServicoOtica();
            }
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

            ReportParameter[] p = new ReportParameter[17];
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
            p[14] = (new ReportParameter("DataOrdemServico", ordemServico.DataAbertura.ToShortDateString()));
            if(ordemServico.Status.Equals("ENCERRADA") && ordemServico.DataServico != DateTime.Parse("1900-01-01 00:00:00"))
                p[15] = (new ReportParameter("DataServico", "DATA SERVIÇO: " + ordemServico.DataServico.ToShortDateString()));
            else if(ordemServico.Status.Equals("ABERTA") && ordemServico.DataServico != DateTime.Parse("1900-01-01 00:00:00"))
                p[15] = (new ReportParameter("DataServico", "DATA PREVISTA: " + ordemServico.DataServico.ToShortDateString() + " " + ordemServico.DataServico.ToShortTimeString()));
            else
                p[15] = (new ReportParameter("DataServico", ""));
            p[16] = (new ReportParameter("Via", ""));
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


        public string GerarPDF(OrdemServico ordemServico)
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                AdicionarFontesDeDados();
                DefinirParametrosRelatorio();
                this.reportViewer1.RefreshReport();
                byte[] bytes = RenderizarRelatorioPDF();
                string caminhoArquivoTemporario = SalvarPDFEmTemp(bytes);

                return caminhoArquivoTemporario;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void AdicionarFontesDeDados()
        {
            // Adicionar fontes de dados ao relatório
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
        }

        private void DefinirParametrosRelatorio()
        {
            // Definir parâmetros do relatório
            string cnpjFormatado = "";
            string cpfFormatado = "";
            string enderecoCliente = "";
            string bairroCliente = "";
            string cidadeEmpresa = "";
            string bairroEmpresa = "";
            string numeroEndereco = "";
            string logradouroEmpresa = "";
            string foneEmpresa = "";

            // CNPJ DA EMPRESA
            if (ordemServico.Filial.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (ordemServico.Filial.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"000\.000\.000\-00");
            }

            // Cidade e Bairro da empresa
            if (ordemServico.Filial.Endereco != null)
            {
                cidadeEmpresa = ordemServico.Filial.Endereco.Cidade.Descricao + "-" + ordemServico.Filial.Endereco.Cidade.Estado.Uf;
                logradouroEmpresa = ordemServico.Filial.Endereco.Logradouro + ", " + ordemServico.Filial.Endereco.Numero + " " + ordemServico.Filial.Endereco.Complemento;
                bairroEmpresa = ordemServico.Filial.Endereco.Bairro;
            }

            // CPF DO CLIENTE
            if (ordemServico.Cliente.Cnpj.Length == 14)
            {
                cpfFormatado = Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (ordemServico.Cliente.Cnpj.Length == 11)
            {
                cpfFormatado = Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"000\.000\.000\-00");
            }

            // Cidade e Bairro do cliente
            string cidadeCliente = "";
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

            string status = "";
            if (ordemServico.Status.Equals("ENCERRADA") && ordemServico.DataServico != DateTime.Parse("1900-01-01 00:00:00"))
            {
                status = "DATA SERVIÇO: " + ordemServico.DataServico.ToShortDateString();
            }
            else if (ordemServico.Status.Equals("ABERTA") && ordemServico.DataServico != DateTime.Parse("1900-01-01 00:00:00"))
            {
                status = "DATA PREVISTA: " + ordemServico.DataServico.ToShortDateString() + " " + ordemServico.DataServico.ToShortTimeString();
            }

            ReportParameter[] parametros = new ReportParameter[]
            {
            new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia),
            new ReportParameter("OrdemServicoID", ordemServico.Id.ToString()),
            new ReportParameter("CNPJ", cnpjFormatado),
            new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa),
            new ReportParameter("Telefone", foneEmp),
            new ReportParameter("logo", Sessao.parametroSistema.Logo),
            new ReportParameter("Cliente", ordemServico.Cliente.RazaoSocial),
            new ReportParameter("CpfCliente", cpfFormatado),
            new ReportParameter("EnderecoCliente", enderecoCliente),
            new ReportParameter("TelefoneCliente", foneCliente),
            new ReportParameter("Observacoes", ordemServico.Observacoes),
            new ReportParameter("CidadeEmpresa", cidadeEmpresa),
            new ReportParameter("InscricaoEstadual", ordemServico.Filial.InscricaoEstadual),
            new ReportParameter("CidadeCliente", cidadeCliente),
            new ReportParameter("DataOrdemServico", ordemServico.DataAbertura.ToShortDateString()),
            new ReportParameter("DataServico", status)
            };

            this.reportViewer1.LocalReport.SetParameters(parametros);
        }

        private byte[] RenderizarRelatorioPDF()
        {
            var deviceInfo = @"
                   <DeviceInfo>
                        <EmbedFonts>None</EmbedFonts>
                        <DPI>300</DPI>
                        <RepeatColumnHeaders>False</RepeatColumnHeaders>
                        <HumanReadablePDF>True</HumanReadablePDF> <!-- Para PDFs mais legíveis -->
                        <ConsumeContainerWhitespace>True</ConsumeContainerWhitespace> <!-- Remove espaços em branco -->
                        <ShowHideToggle>False</ShowHideToggle> <!-- Oculta os botões de mostrar/ocultar detalhes -->
                        <PrintDpiX>300</PrintDpiX> <!-- Configuração de DPI para impressão -->
                        <PrintDpiY>300</PrintDpiY>
                        <PageWidth>9.5in</PageWidth> <!-- Largura da página -->
                        <PageHeight>11.3in</PageHeight> <!-- Altura da página -->
                        <MarginTop>0.2in</MarginTop> <!-- Margem superior -->
                        <MarginLeft>0.1in</MarginLeft> <!-- Margem esquerda -->
                        <MarginRight>0.1in</MarginRight> <!-- Margem direita -->
                        <MarginBottom>0.2in</MarginBottom> <!-- Margem inferior -->
                    </DeviceInfo>";

            byte[] bytes = this.reportViewer1.LocalReport.Render("PDF", deviceInfo);

            return bytes;
        }

        private string SalvarPDFEmTemp(byte[] bytes)
        {
            // Salvar o PDF em um arquivo temporáriodfsd
            string caminhoArquivoTemporario = Path.Combine(Path.GetTempPath(), "OS" + ordemServico.Id.ToString() + ".pdf");
            File.WriteAllBytes(caminhoArquivoTemporario, bytes);

            return caminhoArquivoTemporario;
        }
    
    private void FrmImpressaoOrdemServico_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gerarImpressaoOrdemServicoOtica()
        {
            
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            if (Sessao.parametroSistema.TipoImpressoraCondicional.Equals("TERMICA"))
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServicoTermicaCliente.rdlc";
            else
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServicoOtica.rdlc";

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

            Microsoft.Reporting.WinForms.ReportDataSource dsOrdemServicoExameX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdemServicoExameX.Name = "dsOrdemServicoExame";
            dsOrdemServicoExameX.Value = this.bindingSourceExame1;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdemServicoExameX);

            this.reportViewer1.LocalReport.DisplayName = "Ordem de Serviço " + ordemServico.Id;

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

            ReportParameter[] p = new ReportParameter[17];
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
            p[14] = (new ReportParameter("Via", ""));
            p[15] = (new ReportParameter("DataPrevista", ordemServico.DataServico.ToShortDateString()));
            p[16] = (new ReportParameter("Data", ordemServico.DataAbertura.ToShortDateString() + " " + ordemServico.DataAbertura.ToShortTimeString()));

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
                        ordemServicoProduto.Produto.Id.ToString() + " - " + ordemServicoProduto.DescricaoProduto, ordemServicoProduto.ValorUnitario, ordemServicoProduto.Desconto,
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

            OrdemServicoExameController ordemServicoExameController = new OrdemServicoExameController();
            IList<OrdemServicoExame> listaExames = ordemServicoExameController.selecionarExamesPorOrdemServico(ordemServico.Id);
            if (listaExames.Count > 0)
            {
                foreach (OrdemServicoExame ordemServicoExame in listaExames)
                {
                    string dependente = "";
                    if (ordemServicoExame.Dependente != null)
                        dependente = ordemServicoExame.Dependente.Nome;
                    dsOrdemServicoExame.Exame.AddExameRow(ordemServicoExame.Id, dependente, ordemServicoExame.Examinador, 
                        ordemServicoExame.LdEsferico, ordemServicoExame.LdCilindrico, ordemServicoExame.LdPosicao, 
                        ordemServicoExame.LdDp, ordemServicoExame.LdAltura, ordemServicoExame.LeEsferico, ordemServicoExame.LeCilindrico, 
                        ordemServicoExame.LePosicao, ordemServicoExame.LeDp, ordemServicoExame.LeAltura, ordemServicoExame.PdEsferico, 
                        ordemServicoExame.PdCilindrico, ordemServicoExame.PdPosicao, ordemServicoExame.PdDp, ordemServicoExame.PdAltura, 
                        ordemServicoExame.PeEsferico, ordemServicoExame.PeCilindrico, ordemServicoExame.PePosicao, ordemServicoExame.PeDp, 
                        ordemServicoExame.PeAltura, ordemServicoExame.Armacao, ordemServicoExame.Lente, ordemServicoExame.ProximoExame, 
                        ordemServicoExame.Adicao, ordemServicoExame.DataEntrega.ToShortDateString(), 
                        ordemServicoExame.DataExame.ToShortDateString(), ordemServicoExame.Examinador);
                }
            }

            this.reportViewer1.RefreshReport();
            //if (Sessao.parametroSistema.TipoImpressoraCondicional.Equals("TERMICA"))
            //{
            //    ImprimirDiretoNaImpressora(reportViewer1.LocalReport);
            //}         
        }



        private void ImprimirDiretoNaImpressora(LocalReport relatorio)
        {
            // Configurações de impressão
            PrinterSettings printerSettings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings(printerSettings);

            // Renderiza o relatório como uma string de comandos EMF
            Warning[] warnings;
            string deviceInfo = @"
<DeviceInfo>
    <OutputFormat>EMF</OutputFormat>
    <PageWidth>8.5in</PageWidth>
    <PageHeight>11in</PageHeight>
    <MarginTop>0.25in</MarginTop>
    <MarginLeft>0.25in</MarginLeft>
    <MarginRight>0.25in</MarginRight>
    <MarginBottom>0.25in</MarginBottom>
</DeviceInfo>";

            var streams = new List<Stream>();
            relatorio.Render("Image", deviceInfo, (name, fileNameExtension, encoding, mimeType, willSeek) =>
            {
                Stream stream = new MemoryStream();
                streams.Add(stream);
                return stream;
            }, out warnings);

            // Ajusta as streams para o início
            foreach (Stream stream in streams)
                stream.Position = 0;

            if (streams.Count > 0)
            {
                // Índice da página atual sendo impressa
                int currentPageIndex = 0;

                // Realiza a impressão
                PrintDocument printDoc = new PrintDocument
                {
                    PrinterSettings = printerSettings,
                    DefaultPageSettings = pageSettings
                };
                printDoc.PrintPage += (sender, ev) =>
                {
                    if (currentPageIndex >= 0 && currentPageIndex < streams.Count)
                    {
                        Metafile pageImage = new Metafile(streams[currentPageIndex]);

                        ev.Graphics.DrawImage(pageImage, ev.PageBounds);

                        currentPageIndex++; // Próxima página

                        ev.HasMorePages = currentPageIndex < streams.Count;
                    }
                    else
                    {
                        // Tratamento para o caso de um índice fora do intervalo
                        ev.HasMorePages = false; // Não há mais páginas a serem impressas
                    }
                };

                // Inicia a impressão
                printDoc.Print();

                // Libera os recursos das streams
                foreach (Stream stream in streams)
                    stream.Close();
            }
        }



    }
}
