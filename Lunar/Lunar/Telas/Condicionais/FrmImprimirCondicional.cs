using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
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
            if(Sessao.parametroSistema.TipoImpressoraCondicional != null)
            {
                if(Sessao.parametroSistema.TipoImpressoraCondicional.Equals("A4"))
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Condicionais.ReportCondicionalA4.rdlc";
                else
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Condicionais.ReportCondicional01.rdlc";
            }
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;

            Microsoft.Reporting.WinForms.ReportDataSource dsVendaX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaX.Name = "dsCondicional";
            dsVendaX.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaX);

            this.reportViewer1.LocalReport.DisplayName = "Condicional " + condicional.Id;
            string foneCliente = "";
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
            ReportParameter[] p = new ReportParameter[11];
            p[0] = (new ReportParameter("Empresa", condicional.Filial.NomeFantasia));
            p[1] = (new ReportParameter("CNPJ", cnpjFormatado));
            p[2] = (new ReportParameter("FoneEmpresa", foneEmp));
            p[3] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[4] = (new ReportParameter("Cliente", nomeCliente + " - " + cpfFormatado + " - " + foneCliente));
            p[5] = (new ReportParameter("EnderecoCliente", enderecoCliente));
            p[6] = (new ReportParameter("Vendedor", vendedor));
            p[7] = (new ReportParameter("Id", condicional.Id.ToString()));
            p[8] = (new ReportParameter("Data", condicional.Data.ToShortDateString() + " " + condicional.Data.ToShortTimeString()));
            p[9] = (new ReportParameter("logo", Sessao.parametroSistema.Logo));
            p[10] = (new ReportParameter("Observacoes", condicional.Observacoes));
            reportViewer1.LocalReport.SetParameters(p);

            IList<CondicionalProduto> listaProdutos = new List<CondicionalProduto>();
            CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
            listaProdutos = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);

            CondicionalDevolucaoController condicionalDevolucaoController = new CondicionalDevolucaoController(); 
            IList<CondicionalDevolucao> listaDevolucoes = condicionalDevolucaoController.selecionarProdutosDevolvidosPorCondicional(condicional.Id);

            // Dicionário para armazenar a quantidade total devolvida por produto
            Dictionary<string, double> devolucoesPorProduto = new Dictionary<string, double>();

            // Processando devoluções para criar o dicionário
            foreach (CondicionalDevolucao devolucao in listaDevolucoes)
            {
                string produtoId = devolucao.Produto.Id.ToString(); // Assumindo que CondicionalDevolucao tem uma propriedade ProdutoId
                if (devolucoesPorProduto.ContainsKey(produtoId))
                {
                    devolucoesPorProduto[produtoId] += devolucao.Quantidade;
                }
                else
                {
                    devolucoesPorProduto[produtoId] = devolucao.Quantidade;
                }
            }

            // Lista para armazenar produtos em aberto
            IList<CondicionalProduto> listaProdutosEmAberto = new List<CondicionalProduto>();

            // Calculando produtos em aberto
            foreach (CondicionalProduto condicionalProduto in listaProdutos)
            {
                string produtoId = condicionalProduto.Produto.Id.ToString();
                double quantidadeDevolvida = devolucoesPorProduto.ContainsKey(produtoId) ? devolucoesPorProduto[produtoId] : 0;
                double quantidadeEmAberto = condicionalProduto.Quantidade - quantidadeDevolvida;

                if (quantidadeEmAberto > 0)
                {
                    // Criando uma nova instância do produto com a quantidade atualizada
                    CondicionalProduto produtoEmAberto = new CondicionalProduto
                    {
                        Id = condicionalProduto.Id,
                        Produto = condicionalProduto.Produto,
                        ValorUnitario = condicionalProduto.ValorUnitario,
                        ValorTotal = condicionalProduto.ValorUnitario * decimal.Parse(quantidadeEmAberto.ToString()),
                        Quantidade = quantidadeEmAberto
                    };

                    listaProdutosEmAberto.Add(produtoEmAberto);
                }
            }

            // Processando a lista de produtos em aberto para adicionar ao dataset
            foreach (CondicionalProduto condicionalProduto in listaProdutosEmAberto)
            {
                string descricaoAbreviada = condicionalProduto.Produto.Descricao;
                if (descricaoAbreviada.Length > 33)
                    descricaoAbreviada = descricaoAbreviada.Substring(0, 33);

                dsCondicional.Condicional.AddCondicionalRow(
                    condicional.Id,
                    condicional.Cliente.RazaoSocial,
                    condicional.Filial.RazaoSocial,
                    condicionalProduto.Produto.Id.ToString(),
                    condicionalProduto.ValorUnitario,
                    condicionalProduto.ValorTotal,
                    descricaoAbreviada,
                    condicionalProduto.Quantidade);
            }
            this.reportViewer1.RefreshReport();
        }
        private string SalvarPDFEmTemp(byte[] bytes)
        {
            // Salvar o PDF em um arquivo temporáriodfsd
            string caminhoArquivoTemporario = Path.Combine(Path.GetTempPath(), "CO" + condicional.Id.ToString() + ".pdf");
            File.WriteAllBytes(caminhoArquivoTemporario, bytes);

            return caminhoArquivoTemporario;
        }
        public string GerarPDF(Condicional condicional)
        {
            this.condicional = condicional;
            try
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Lunar.Telas.Condicionais.ReportCondicionalA4.rdlc";
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                AdicionarFontesDeDados();
                DefinirParametrosRelatorio();
                carregarListaItensCondicionalParaPdf();
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
            Microsoft.Reporting.WinForms.ReportDataSource dsVendaX = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsVendaX.Name = "dsCondicional";
            dsVendaX.Value = this.bindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(dsVendaX);
        }
        private void DefinirParametrosRelatorio()
        {
            this.reportViewer1.LocalReport.DisplayName = "Condicional " + condicional.Id;
            string foneCliente = "";
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
            ReportParameter[] p = new ReportParameter[10];
            p[0] = (new ReportParameter("Empresa", condicional.Filial.NomeFantasia));
            p[1] = (new ReportParameter("CNPJ", cnpjFormatado));
            p[2] = (new ReportParameter("FoneEmpresa", foneEmp));
            p[3] = (new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa));
            p[4] = (new ReportParameter("Cliente", nomeCliente + " - " + cpfFormatado + " - " + foneCliente));
            p[5] = (new ReportParameter("EnderecoCliente", enderecoCliente));
            p[6] = (new ReportParameter("Vendedor", vendedor));
            p[7] = (new ReportParameter("Id", condicional.Id.ToString()));
            p[8] = (new ReportParameter("Data", condicional.Data.ToShortDateString() + " " + condicional.Data.ToShortTimeString()));
            p[9] = (new ReportParameter("logo", Sessao.parametroSistema.Logo));
            reportViewer1.LocalReport.SetParameters(p);
        }

        private byte[] RenderizarRelatorioPDF()
        {
            var deviceInfo = @"
           <DeviceInfo>
                <EmbedFonts>None</EmbedFonts>
                <DPI>300</DPI>
                <RepeatColumnHeaders>False</RepeatColumnHeaders>
                <HumanReadablePDF>True</HumanReadablePDF>
                <ConsumeContainerWhitespace>True</ConsumeContainerWhitespace>
                <ShowHideToggle>False</ShowHideToggle>
                <PrintDpiX>300</PrintDpiX>
                <PrintDpiY>300</PrintDpiY>
                <PageWidth>8.27in</PageWidth> <!-- Largura A4 -->
                <PageHeight>11.69in</PageHeight> <!-- Altura A4 -->
                <MarginTop>0.2in</MarginTop>
                <MarginLeft>0.6in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.2in</MarginBottom>
            </DeviceInfo>";

            byte[] bytes = this.reportViewer1.LocalReport.Render("PDF", deviceInfo);

            return bytes;
        }

        private void carregarListaItensCondicionalParaPdf()
        {
            IList<CondicionalProduto> listaProdutos = new List<CondicionalProduto>();
            CondicionalProdutoController condicionalProdutoController = new CondicionalProdutoController();
            listaProdutos = condicionalProdutoController.selecionarProdutosPorCondicional(condicional.Id);

            CondicionalDevolucaoController condicionalDevolucaoController = new CondicionalDevolucaoController();
            IList<CondicionalDevolucao> listaDevolucoes = condicionalDevolucaoController.selecionarProdutosDevolvidosPorCondicional(condicional.Id);

            // Dicionário para armazenar a quantidade total devolvida por produto
            Dictionary<string, double> devolucoesPorProduto = new Dictionary<string, double>();

            // Processando devoluções para criar o dicionário
            foreach (CondicionalDevolucao devolucao in listaDevolucoes)
            {
                string produtoId = devolucao.Produto.Id.ToString(); // Assumindo que CondicionalDevolucao tem uma propriedade ProdutoId
                if (devolucoesPorProduto.ContainsKey(produtoId))
                {
                    devolucoesPorProduto[produtoId] += devolucao.Quantidade;
                }
                else
                {
                    devolucoesPorProduto[produtoId] = devolucao.Quantidade;
                }
            }

            // Lista para armazenar produtos em aberto
            IList<CondicionalProduto> listaProdutosEmAberto = new List<CondicionalProduto>();

            // Calculando produtos em aberto
            foreach (CondicionalProduto condicionalProduto in listaProdutos)
            {
                string produtoId = condicionalProduto.Produto.Id.ToString();
                double quantidadeDevolvida = devolucoesPorProduto.ContainsKey(produtoId) ? devolucoesPorProduto[produtoId] : 0;
                double quantidadeEmAberto = condicionalProduto.Quantidade - quantidadeDevolvida;

                if (quantidadeEmAberto > 0)
                {
                    // Criando uma nova instância do produto com a quantidade atualizada
                    CondicionalProduto produtoEmAberto = new CondicionalProduto
                    {
                        Id = condicionalProduto.Id,
                        Produto = condicionalProduto.Produto,
                        ValorUnitario = condicionalProduto.ValorUnitario,
                        ValorTotal = condicionalProduto.ValorUnitario * decimal.Parse(quantidadeEmAberto.ToString()),
                        Quantidade = quantidadeEmAberto
                    };

                    listaProdutosEmAberto.Add(produtoEmAberto);
                }
            }

            // Processando a lista de produtos em aberto para adicionar ao dataset
            foreach (CondicionalProduto condicionalProduto in listaProdutosEmAberto)
            {
                string descricaoAbreviada = condicionalProduto.Produto.Descricao;
                if (descricaoAbreviada.Length > 33)
                    descricaoAbreviada = descricaoAbreviada.Substring(0, 33);

                dsCondicional.Condicional.AddCondicionalRow(
                    condicional.Id,
                    condicional.Cliente.RazaoSocial,
                    condicional.Filial.RazaoSocial,
                    condicionalProduto.Produto.Id.ToString(),
                    condicionalProduto.ValorUnitario,
                    condicionalProduto.ValorTotal,
                    descricaoAbreviada,
                    condicionalProduto.Quantidade);
            }
        }
    }
}
