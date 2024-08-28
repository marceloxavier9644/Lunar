using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lunar.Utils.LunarChatIntegracao.LunarChatMensagem;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmImprimirViasOtica : Form
    {
        OrdemServico ordemServico = new OrdemServico();
        bool imprimir = false;
        public FrmImprimirViasOtica(OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
            CarregarImpressoras();
            if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
            {
                btnEnviarWhatsapp.Enabled = true;
            }
            if (ordemServico.Cliente.PessoaTelefone != null)
                txtWhatsappCliente.Text = GenericaDesktop.formatarFone(ordemServico.Cliente.PessoaTelefone.Ddd + ordemServico.Cliente.PessoaTelefone.Telefone);
        }

        private void FrmImprimirViasOtica_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    btnEnviarWhatsapp.PerformClick();
                    break;
                case Keys.F8:
                    btnImprimir.PerformClick();
                    break;
            }
        }
        private void CarregarImpressoras()
        {
            comboImpressoras.Items.Clear();
            PrinterSettings.StringCollection impressoras = PrinterSettings.InstalledPrinters;
            string impressoraPadrao = new PrinterSettings().PrinterName;

            int defaultIndex = -1;

            foreach (string impressora in impressoras)
            {
                int index = comboImpressoras.Items.Add(impressora);
                if (impressora.Equals(impressoraPadrao, StringComparison.OrdinalIgnoreCase))
                {
                    defaultIndex = index;
                }
            }

            if (defaultIndex != -1)
            {
                comboImpressoras.SelectedIndex = defaultIndex;
            }
            else if (comboImpressoras.Items.Count > 0)
            {
                comboImpressoras.SelectedIndex = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtWhatsappCliente_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtWhatsappCliente.Text))
            {
                GenericaDesktop.RemoveCaracteres(txtWhatsappCliente.Text);
                txtWhatsappCliente.Text = GenericaDesktop.formatarFone(txtWhatsappCliente.Text);
            }
        }

        private void txtWhatsappLaboratorio_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtWhatsappLaboratorio.Text))
            {
                GenericaDesktop.RemoveCaracteres(txtWhatsappLaboratorio.Text);
                txtWhatsappLaboratorio.Text = GenericaDesktop.formatarFone(txtWhatsappLaboratorio.Text);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //GERA O PDF DE TODOS, SE MARCAR PRA IMPRIMIR, JA IMPRIME DIRETO, SENAO APENAS GERA O PDF
            imprimir = true;
            gerarImpressaoOrdemServicoOtica_Cliente();
            gerarImpressaoOrdemServicoOtica_Laboratorio();
            gerarImpressaoOrdemServicoOtica_Loja();
        }

        private void gerarImpressaoOrdemServicoOtica_Cliente()
        {
            limparDataSet();
            LocalReport localReport = new LocalReport();

            // Habilitar imagens externas
            localReport.EnableExternalImages = true;
            localReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServicoTermicaCliente.rdlc";

            localReport.DataSources.Add(new ReportDataSource("dsOrdemServico", this.bindingSourceOrdem));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoProduto", this.bindingSourceProd));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoServico", this.bindingSourceServico));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoPagamento", this.bindingSourcePagamento));
           
            // Definir os parâmetros do relatório
            ReportParameter[] reportParameters = new ReportParameter[12];
            reportParameters[0] = new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia);
            reportParameters[1] = new ReportParameter("OrdemServicoID", ordemServico.Id.ToString());

            // Formatando CNPJ da empresa
            string cnpjFormatado = ordemServico.Filial.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"000\.000\.000\-00");

            reportParameters[2] = new ReportParameter("CNPJ", cnpjFormatado);

            // Endereço da empresa
            string cidadeEmpresa = ordemServico.Filial.Endereco?.Cidade?.Descricao + "-" + ordemServico.Filial.Endereco?.Cidade?.Estado?.Uf;
            string logradouroEmpresa = ordemServico.Filial.Endereco?.Logradouro + ", " + ordemServico.Filial.Endereco?.Numero + " " + ordemServico.Filial.Endereco?.Complemento;
            string bairroEmpresa = ordemServico.Filial.Endereco?.Bairro;

            reportParameters[3] = new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa);

            // Telefone da empresa
            string foneEmp = GenericaDesktop.RemoveCaracteres(ordemServico.Filial.DddPrincipal + ordemServico.Filial.TelefonePrincipal).Trim();
            foneEmp = foneEmp.Length == 11 ? long.Parse(foneEmp).ToString(@"(00) 0 0000-0000")
                : foneEmp.Length == 10 ? long.Parse(foneEmp).ToString(@"(00) 0000-0000")
                : long.Parse(foneEmp).ToString(@"00000-0000");

            reportParameters[4] = new ReportParameter("Telefone", foneEmp);
            reportParameters[5] = new ReportParameter("logo", Sessao.parametroSistema.Logo);

            // Cliente
            string cpfFormatado = ordemServico.Cliente.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"000\.000\.000\-00");

            reportParameters[6] = new ReportParameter("Cliente", ordemServico.Cliente.RazaoSocial);
            reportParameters[7] = new ReportParameter("CpfCliente", cpfFormatado);

            // Endereço do cliente
            string cidadeCliente = ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Descricao + " - " + ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Estado?.Uf;
            string enderecoCliente = ordemServico.Cliente.EnderecoPrincipal?.Logradouro + ", " +
                ordemServico.Cliente.EnderecoPrincipal?.Numero + " " + ordemServico.Cliente.EnderecoPrincipal?.Complemento + " " + ordemServico.Cliente.EnderecoPrincipal?.Bairro;

            reportParameters[8] = new ReportParameter("EnderecoCliente", enderecoCliente);

            // Telefone do cliente
            string foneCliente = "";
            if (ordemServico.Cliente.PessoaTelefone != null)
            {
                foneCliente = GenericaDesktop.RemoveCaracteres(ordemServico.Cliente.PessoaTelefone.Ddd + ordemServico.Cliente.PessoaTelefone.Telefone).Trim();
                foneCliente = foneCliente.Length == 11 ? long.Parse(foneCliente).ToString(@"(00) 0 0000-0000")
                    : foneCliente.Length == 10 ? long.Parse(foneCliente).ToString(@"(00) 0000-0000")
                    : long.Parse(foneCliente).ToString(@"00000-0000");
            }

            reportParameters[9] = new ReportParameter("TelefoneCliente", foneCliente);
            reportParameters[10] = new ReportParameter("DataPrevista", ordemServico.DataServico.ToShortDateString());
            reportParameters[11] = new ReportParameter("Data", ordemServico.DataAbertura.ToShortDateString() + " " + ordemServico.DataAbertura.ToShortTimeString());

            // Configurando os parâmetros
            localReport.SetParameters(reportParameters);

            // Preenchendo os datasets
            dsOrdemServico.OrdemServico.AddOrdemServicoRow(ordemServico.Id, ordemServico.Cliente.RazaoSocial, ordemServico.Cliente.Id.ToString(),
                ordemServico.DataAbertura.ToShortDateString(), ordemServico.DataEncerramento.ToShortDateString(),
                ordemServico.ValorProduto, ordemServico.ValorServico, ordemServico.ValorTotal);

            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            IList<OrdemServicoProduto> listaProdutos = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
            {
                dsOrdemServicoProduto.OrdemServicoProduto.AddOrdemServicoProdutoRow(ordemServicoProduto.Id, ordemServicoProduto.Produto.Id.ToString(),
                    ordemServicoProduto.Produto.Id.ToString() + " - " + ordemServicoProduto.DescricaoProduto, ordemServicoProduto.ValorUnitario, ordemServicoProduto.Desconto,
                    ordemServicoProduto.Acrescimo, ordemServicoProduto.Quantidade, ordemServicoProduto.ValorTotal, ordemServico.Id);
            }

            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServicos = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoServico ordemServicoServico in listaServicos)
            {
                dsOrdemServicoServico.OrdemServicoServico.AddOrdemServicoServicoRow(ordemServicoServico.Id, ordemServicoServico.Servico.Id.ToString(),
                    ordemServicoServico.DescricaoServico, ordemServicoServico.ValorUnitario, ordemServicoServico.Desconto,
                    ordemServicoServico.Acrescimo, ordemServicoServico.Quantidade, ordemServicoServico.ValorTotal, ordemServico.Id);
            }

            OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
            IList<OrdemServicoPagamento> listaPagamento= ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoPagamento pag in listaPagamento)
            {
                dsOrdemServicoPagamento.OrdemServicoPagamento.AddOrdemServicoPagamentoRow(pag.DataRecebimento.ToShortDateString(), 
                    pag.ValorRecebido, pag.FormaPagamento.Descricao, pag.TipoCartao, pag.Parcelas);
            }
            byte[] bytes = localReport.Render("PDF");

            // Salvando o arquivo PDF
            string caminhoArquivo = Path.Combine(Path.GetTempPath(), "OrdemServico"+ordemServico.Id+".pdf");
            using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            if (chkCliente.Checked == true && imprimir == true)
                ImprimirPDF(caminhoArquivo, comboImpressoras.SelectedItem.ToString());
        }

        private void limparDataSet()
        {
            dsOrdemServico.Tables[0].Clear();
            dsOrdemServicoProduto.Tables[0].Clear();
            dsOrdemServicoExame.Tables[0].Clear();
            dsOrdemServicoPagamento.Tables[0].Clear();
            dsOrdemServicoServico.Tables[0].Clear();
        }
        private void gerarImpressaoOrdemServicoOtica_Laboratorio()
        {
            limparDataSet();

            LocalReport localReport = new LocalReport();

            // Habilitar imagens externas
            localReport.EnableExternalImages = true;
            localReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServicoTermicaLaboratorio.rdlc";

            localReport.DataSources.Add(new ReportDataSource("dsOrdemServico", this.bindingSourceOrdem));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoProduto", this.bindingSourceProd));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoExame", this.bindingSourceExame1));

            // Formatando CNPJ da empresa
            string cnpjFormatado = ordemServico.Filial.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"000\.000\.000\-00");

            // Endereço da empresa
            string cidadeEmpresa = ordemServico.Filial.Endereco?.Cidade?.Descricao + "-" + ordemServico.Filial.Endereco?.Cidade?.Estado?.Uf;
            string logradouroEmpresa = ordemServico.Filial.Endereco?.Logradouro + ", " + ordemServico.Filial.Endereco?.Numero + " " + ordemServico.Filial.Endereco?.Complemento;
            string bairroEmpresa = ordemServico.Filial.Endereco?.Bairro;

            // Telefone da empresa
            string foneEmp = GenericaDesktop.RemoveCaracteres(ordemServico.Filial.DddPrincipal + ordemServico.Filial.TelefonePrincipal).Trim();
            foneEmp = foneEmp.Length == 11 ? long.Parse(foneEmp).ToString(@"(00) 0 0000-0000")
                : foneEmp.Length == 10 ? long.Parse(foneEmp).ToString(@"(00) 0000-0000")
                : long.Parse(foneEmp).ToString(@"00000-0000");

            // Cliente
            string cpfFormatado = ordemServico.Cliente.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"000\.000\.000\-00");

            // Endereço do cliente
            string cidadeCliente = ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Descricao + " - " + ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Estado?.Uf;
            string enderecoCliente = ordemServico.Cliente.EnderecoPrincipal?.Logradouro + ", " +
                ordemServico.Cliente.EnderecoPrincipal?.Numero + " " + ordemServico.Cliente.EnderecoPrincipal?.Complemento + " " + ordemServico.Cliente.EnderecoPrincipal?.Bairro;

            // Telefone do cliente
            string foneCliente = "";
            if (ordemServico.Cliente.PessoaTelefone != null)
            {
                foneCliente = GenericaDesktop.RemoveCaracteres(ordemServico.Cliente.PessoaTelefone.Ddd + ordemServico.Cliente.PessoaTelefone.Telefone).Trim();
                foneCliente = foneCliente.Length == 11 ? long.Parse(foneCliente).ToString(@"(00) 0 0000-0000")
                    : foneCliente.Length == 10 ? long.Parse(foneCliente).ToString(@"(00) 0000-0000")
                    : long.Parse(foneCliente).ToString(@"00000-0000");
            }

            // Definir os parâmetros do relatório
            ReportParameter[] p = new ReportParameter[16];
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
            p[14] = (new ReportParameter("DataPrevista", ordemServico.DataServico.ToShortDateString()));
            p[15] = (new ReportParameter("Data", ordemServico.DataAbertura.ToShortDateString() + " " + ordemServico.DataAbertura.ToShortTimeString()));

            // Configurando os parâmetros
            localReport.SetParameters(p);

            // Preenchendo os datasets
            dsOrdemServico.OrdemServico.AddOrdemServicoRow(ordemServico.Id, ordemServico.Cliente.RazaoSocial, ordemServico.Cliente.Id.ToString(),
                ordemServico.DataAbertura.ToShortDateString(), ordemServico.DataEncerramento.ToShortDateString(),
                ordemServico.ValorProduto, ordemServico.ValorServico, ordemServico.ValorTotal);

            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            IList<OrdemServicoProduto> listaProdutos = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
            {
                dsOrdemServicoProduto.OrdemServicoProduto.AddOrdemServicoProdutoRow(ordemServicoProduto.Id, ordemServicoProduto.Produto.Id.ToString(),
                    ordemServicoProduto.Produto.Id.ToString() + " - " + ordemServicoProduto.DescricaoProduto, ordemServicoProduto.ValorUnitario, ordemServicoProduto.Desconto,
                    ordemServicoProduto.Acrescimo, ordemServicoProduto.Quantidade, ordemServicoProduto.ValorTotal, ordemServico.Id);
            }

            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServicos = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoServico ordemServicoServico in listaServicos)
            {
                dsOrdemServicoServico.OrdemServicoServico.AddOrdemServicoServicoRow(ordemServicoServico.Id, ordemServicoServico.Servico.Id.ToString(),
                    ordemServicoServico.DescricaoServico, ordemServicoServico.ValorUnitario, ordemServicoServico.Desconto,
                    ordemServicoServico.Acrescimo, ordemServicoServico.Quantidade, ordemServicoServico.ValorTotal, ordemServico.Id);
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

            byte[] bytes = localReport.Render("PDF");

            // Salvando o arquivo PDF
            string caminhoArquivo = Path.Combine(Path.GetTempPath(), "OrdemServico" + ordemServico.Id + "_LAB.pdf");
            using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            if (chkLaboratorio.Checked == true && imprimir == true)
                ImprimirPDF(caminhoArquivo, comboImpressoras.SelectedItem.ToString());
        }

        private void gerarImpressaoOrdemServicoOtica_Loja()
        {
            limparDataSet();
            LocalReport localReport = new LocalReport();

            // Habilitar imagens externas
            localReport.EnableExternalImages = true;
            localReport.ReportEmbeddedResource = "Lunar.Telas.OrdensDeServico.ReportOrdemServicoTermicaLoja.rdlc";

            localReport.DataSources.Add(new ReportDataSource("dsOrdemServico", this.bindingSourceOrdem));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoProduto", this.bindingSourceProd));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoServico", this.bindingSourceServico));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoPagamento", this.bindingSourcePagamento));
            localReport.DataSources.Add(new ReportDataSource("dsOrdemServicoExame", this.bindingSourceExame1));

            // Definir os parâmetros do relatório
            ReportParameter[] reportParameters = new ReportParameter[12];
            reportParameters[0] = new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia);
            reportParameters[1] = new ReportParameter("OrdemServicoID", ordemServico.Id.ToString());

            // Formatando CNPJ da empresa
            string cnpjFormatado = ordemServico.Filial.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Filial.Cnpj).ToString(@"000\.000\.000\-00");

            reportParameters[2] = new ReportParameter("CNPJ", cnpjFormatado);

            // Endereço da empresa
            string cidadeEmpresa = ordemServico.Filial.Endereco?.Cidade?.Descricao + "-" + ordemServico.Filial.Endereco?.Cidade?.Estado?.Uf;
            string logradouroEmpresa = ordemServico.Filial.Endereco?.Logradouro + ", " + ordemServico.Filial.Endereco?.Numero + " " + ordemServico.Filial.Endereco?.Complemento;
            string bairroEmpresa = ordemServico.Filial.Endereco?.Bairro;

            reportParameters[3] = new ReportParameter("EnderecoEmpresa", logradouroEmpresa + " " + bairroEmpresa);

            // Telefone da empresa
            string foneEmp = GenericaDesktop.RemoveCaracteres(ordemServico.Filial.DddPrincipal + ordemServico.Filial.TelefonePrincipal).Trim();
            foneEmp = foneEmp.Length == 11 ? long.Parse(foneEmp).ToString(@"(00) 0 0000-0000")
                : foneEmp.Length == 10 ? long.Parse(foneEmp).ToString(@"(00) 0000-0000")
                : long.Parse(foneEmp).ToString(@"00000-0000");

            reportParameters[4] = new ReportParameter("Telefone", foneEmp);
            reportParameters[5] = new ReportParameter("logo", Sessao.parametroSistema.Logo);

            // Cliente
            string cpfFormatado = ordemServico.Cliente.Cnpj.Length == 14
                ? Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(ordemServico.Cliente.Cnpj).ToString(@"000\.000\.000\-00");

            reportParameters[6] = new ReportParameter("Cliente", ordemServico.Cliente.RazaoSocial);
            reportParameters[7] = new ReportParameter("CpfCliente", cpfFormatado);

            // Endereço do cliente
            string cidadeCliente = ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Descricao + " - " + ordemServico.Cliente.EnderecoPrincipal?.Cidade?.Estado?.Uf;
            string enderecoCliente = ordemServico.Cliente.EnderecoPrincipal?.Logradouro + ", " +
                ordemServico.Cliente.EnderecoPrincipal?.Numero + " " + ordemServico.Cliente.EnderecoPrincipal?.Complemento + " " + ordemServico.Cliente.EnderecoPrincipal?.Bairro;

            reportParameters[8] = new ReportParameter("EnderecoCliente", enderecoCliente);

            // Telefone do cliente
            string foneCliente = "";
            if (ordemServico.Cliente.PessoaTelefone != null)
            {
                foneCliente = GenericaDesktop.RemoveCaracteres(ordemServico.Cliente.PessoaTelefone.Ddd + ordemServico.Cliente.PessoaTelefone.Telefone).Trim();
                foneCliente = foneCliente.Length == 11 ? long.Parse(foneCliente).ToString(@"(00) 0 0000-0000")
                    : foneCliente.Length == 10 ? long.Parse(foneCliente).ToString(@"(00) 0000-0000")
                    : long.Parse(foneCliente).ToString(@"00000-0000");
            }

            reportParameters[9] = new ReportParameter("TelefoneCliente", foneCliente);
            reportParameters[10] = new ReportParameter("DataPrevista", ordemServico.DataServico.ToShortDateString());
            reportParameters[11] = new ReportParameter("Data", ordemServico.DataAbertura.ToShortDateString() + " " + ordemServico.DataAbertura.ToShortTimeString());

            // Configurando os parâmetros
            localReport.SetParameters(reportParameters);

            // Preenchendo os datasets
            dsOrdemServico.OrdemServico.AddOrdemServicoRow(ordemServico.Id, ordemServico.Cliente.RazaoSocial, ordemServico.Cliente.Id.ToString(),
                ordemServico.DataAbertura.ToShortDateString(), ordemServico.DataEncerramento.ToShortDateString(),
                ordemServico.ValorProduto, ordemServico.ValorServico, ordemServico.ValorTotal);

            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            IList<OrdemServicoProduto> listaProdutos = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
            {
                dsOrdemServicoProduto.OrdemServicoProduto.AddOrdemServicoProdutoRow(ordemServicoProduto.Id, ordemServicoProduto.Produto.Id.ToString(),
                    ordemServicoProduto.Produto.Id.ToString() + " - " + ordemServicoProduto.DescricaoProduto, ordemServicoProduto.ValorUnitario, ordemServicoProduto.Desconto,
                    ordemServicoProduto.Acrescimo, ordemServicoProduto.Quantidade, ordemServicoProduto.ValorTotal, ordemServico.Id);
            }

            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServicos = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoServico ordemServicoServico in listaServicos)
            {
                dsOrdemServicoServico.OrdemServicoServico.AddOrdemServicoServicoRow(ordemServicoServico.Id, ordemServicoServico.Servico.Id.ToString(),
                    ordemServicoServico.DescricaoServico, ordemServicoServico.ValorUnitario, ordemServicoServico.Desconto,
                    ordemServicoServico.Acrescimo, ordemServicoServico.Quantidade, ordemServicoServico.ValorTotal, ordemServico.Id);
            }

            OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
            IList<OrdemServicoPagamento> listaPagamento = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
            foreach (OrdemServicoPagamento pag in listaPagamento)
            {
                dsOrdemServicoPagamento.OrdemServicoPagamento.AddOrdemServicoPagamentoRow(pag.DataRecebimento.ToShortDateString(),
                    pag.ValorRecebido, pag.FormaPagamento.Descricao, pag.TipoCartao, pag.Parcelas);
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

            byte[] bytes = localReport.Render("PDF");

            // Salvando o arquivo PDF
            string caminhoArquivo = Path.Combine(Path.GetTempPath(), "OrdemServico" + ordemServico.Id + "_LOJA.pdf");
            using (FileStream fs = new FileStream(caminhoArquivo, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            if(chkLoja.Checked == true && imprimir == true)
                ImprimirPDF(caminhoArquivo, comboImpressoras.SelectedItem.ToString());
        }


        private void ImprimirPDF(string caminhoArquivo, string nomeImpressora)
        {
            try
            {
                // Criar um objeto de impressão
                using (var printDocument = new PrintDocument())
                {
                    // Definir a impressora
                    printDocument.PrinterSettings.PrinterName = nomeImpressora;

                    // Configurar o evento de impressão
                    printDocument.PrintPage += (sender, e) =>
                    {
                        using (var pdfDocument = PdfDocument.Load(caminhoArquivo))
                        using (var pdfViewer = new PdfViewer())
                        {
                            // Obter a página atual para impressão
                            var page = pdfDocument.Render(0, 300, 300, true); // Renderiza a primeira página
                            e.Graphics.DrawImage(page, e.MarginBounds);
                        }
                    };
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao imprimir o PDF: " + ex.Message);
            }
        }

        private async void btnEnviarWhatsapp_Click(object sender, EventArgs e)
        {
            imprimir = false;
            if (chkCliente.Checked == true)
            {
                gerarImpressaoOrdemServicoOtica_Cliente();
                string caminhoArquivoCliente = Path.Combine(Path.GetTempPath(), "OrdemServico" + ordemServico.Id + ".pdf");

                string numeroWhatsapp = GenericaDesktop.RemoveCaracteres(txtWhatsappCliente.Text).Trim();
                if (!numeroWhatsapp.StartsWith("55"))
                {
                    numeroWhatsapp = "55" + numeroWhatsapp;
                }
                string razaoSocial = ordemServico.Cliente.RazaoSocial;
                string[] partesNome = razaoSocial.Split(' ');
                string primeiroNome = partesNome.Length > 0 ? partesNome[0] : string.Empty;

                await enviarPDfPeloWhats(ordemServico, numeroWhatsapp, primeiroNome, "", caminhoArquivoCliente); 
            }
            if (chkLaboratorio.Checked == true)
            {
                gerarImpressaoOrdemServicoOtica_Laboratorio();
                string caminhoArquivoLab = Path.Combine(Path.GetTempPath(), "OrdemServico" + ordemServico.Id + "_LAB.pdf");

                string numeroWhatsapp = GenericaDesktop.RemoveCaracteres(txtWhatsappLaboratorio.Text).Trim();
                if (!numeroWhatsapp.StartsWith("55"))
                {
                    numeroWhatsapp = "55" + numeroWhatsapp;
                }
                string razaoSocial = ordemServico.Cliente.RazaoSocial;
                string[] partesNome = razaoSocial.Split(' ');
                string primeiroNome = partesNome.Length > 0 ? partesNome[0] : string.Empty;

                await enviarPDfPeloWhats(ordemServico, numeroWhatsapp, primeiroNome, "*"+Sessao.empresaFilialLogada.NomeFantasia + ":* OS. " + ordemServico.Id, caminhoArquivoLab);
            }
        }

        private async Task enviarPDfPeloWhats(OrdemServico ordem, string numero, string nome, string mensagem, string caminhoPDF)
        {
            if (!String.IsNullOrEmpty(caminhoPDF))
            {
                var client = new EnviarMensagemWhatsapp();
                await client.SendMessageAsync(numero, mensagem);
                await client.SendMediaMessageAsync(numero, caminhoPDF);
            }
        }
    }
}
