using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.GalaxyPay_API;
using Lunar.Utils.Grid_Class;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Lunar.Utils.GalaxyPay_API.RetornoPagamentoPix;
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.Vendas
{
    public partial class FrmConsultaVendas : Form
    {
        CaixaController caixaController = new CaixaController();
        EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
        string codStatusRet = "";
        String xmlStrEnvio = "";
        IList<Venda> listaVenda = new List<Venda>();
        Venda venda = new Venda();
        VendaController vendaController = new VendaController();
        VendaItensController vendaItensController = new VendaItensController();
        PessoaController pessoaController = new PessoaController();
        IList<NfeProduto> listaProdutosNFe = new List<NfeProduto>();
        Produto produto = new Produto();
        ProdutoController produtoController = new ProdutoController();
        Nfe nfe = new Nfe();
        string numeroNFCe = "";
        string arquivoContigencia = "";
        string nomeArquivoContigencia = "";
        GenericaDesktop generica = new GenericaDesktop();
        public FrmConsultaVendas()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtCliente.Texts))
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal(ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtCliente.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                pesquisarVendas();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            pesquisarVendas();
                            break;
                    }
                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void pesquisarVendas()
        {
            try
            {
                listaVenda = new List<Venda>();
                string sql = "From Venda Tabela where Tabela.Concluida = true and Tabela.EmpresaFilial = " + Sessao.empresaFilialLogada.Id + " and Tabela.FlagExcluido <> true and Tabela.Cancelado <> true ";
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts + " ";

                if (!String.IsNullOrEmpty(txtNumeroDocumento.Texts))
                    sql = sql + "and Tabela.Id = " + txtNumeroDocumento.Texts + " ";

                if (!String.IsNullOrEmpty(txtVendedor.Texts))
                    sql = sql + "and Tabela.Vendedor = " + txtVendedor.Texts + " ";

                if (chkAtivarData.Checked == true)
                {
                    DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
                    DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
                    String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                    String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                    sql = sql + "and Tabela.DataVenda BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                }
                
                string orderBy = " order by Tabela.DataVenda";
                listaVenda = vendaController.selecionarVendaPorSql(sql + orderBy);
                if (listaVenda.Count > 0)
                {
                    //calculaTotalNotas();
                    sfDataPager1.DataSource = listaVenda;
                    if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                        sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                    else
                        sfDataPager1.PageSize = 100;
                    grid.DataSource = sfDataPager1.PagedSource;
                    sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                    this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    this.grid.AutoSizeController.Refresh();
                    grid.Refresh();
                    this.grid.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));

                    GridSummary.PreencherSumario(grid, "ValorFinal");

                }
                else
                {
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaVenda.Skip(e.StartRowIndex).Take(e.PageSize));

        }

        private void btnPesquisaVender_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true"))
                {
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            {
                                txtVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            pesquisarVendas();
                            break;
                    }
                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarVendas();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarVendas();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                venda = new Venda();
                venda = (Venda)grid.SelectedItem;
                if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja cancelar a venda " + venda.Id.ToString() + "?"))
                {
                    if (venda.Nfe == null)
                    {
                        venda.Cancelado = true;

                        //Cancela Caixa
                        IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorOrigem("VENDA", venda.Id.ToString());
                        if(listaCaixa.Count > 0)
                        {
                            foreach(Caixa caixa in listaCaixa)
                            {
                                caixa.FlagExcluido = true;
                                Controller.getInstance().excluir(caixa);
                            }
                        }
                        //Cancela Conta Receber
                        ContaReceberController contaReceberController = new ContaReceberController();
                        IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorVenda(venda.Id);
                        if (listaReceber.Count > 0)
                        {
                            foreach (ContaReceber contaReceber in listaReceber)
                            {
                                contaReceber.FlagExcluido = true;
                                Controller.getInstance().excluir(contaReceber);
                            }
                        }

                        IList<VendaItens> listaProdutosVenda = vendaItensController.selecionarProdutosPorVenda(venda.Id);
                        foreach (VendaItens vendaItem in listaProdutosVenda)
                        {
                            Produto produto = new Produto();
                            Estoque estoque = new Estoque();
                            produto = vendaItem.Produto;
                            produto.EstoqueAuxiliar = produto.EstoqueAuxiliar + vendaItem.Quantidade;

                            estoque.Conciliado = false;
                            estoque.DataEntradaSaida = DateTime.Now;
                            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                            estoque.Entrada = true;
                            estoque.Origem = "VENDA";
                            estoque.Produto = produto;
                            estoque.Quantidade = vendaItem.Quantidade;
                            estoque.Saida = false;
                            estoque.Descricao = "CANCELAMENTO DA VENDA " + venda.Id.ToString();
                            Controller.getInstance().salvar(produto);
                            Controller.getInstance().salvar(estoque);
                        }
                        Controller.getInstance().salvar(venda);
                        GenericaDesktop.ShowInfo("Venda cancelada com sucesso");
                        pesquisarVendas();
                    }
                    else
                        GenericaDesktop.ShowAlerta("Atenção, esta venda possui nota fiscal vinculada, você deve cancelar a nota fiscal primeiro! No menu Depart.Fiscal / Monitoramento Fiscal");


                }
                else
                    GenericaDesktop.ShowAlerta("Clique na venda que deseja cancelar!");
            }
        }

        private void chkAtivarData_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAtivarData.Checked == true)
            {
                txtDataInicial.Enabled = true;
                txtDataFinal.Enabled = true;
            }
            else
            {
                txtDataInicial.Enabled = false;
                txtDataFinal.Enabled = false;
            }
        }

        private void btnGerarNFCe_Click(object sender, EventArgs e)
        {
            venda = new Venda();
            venda = (Venda)grid.SelectedItem;
            if (venda.Nfe == null)
            {
                if (GenericaDesktop.ShowConfirmacao("Gerar NFC-e da venda " + venda.Id.ToString() + "?"))
                {
                    //se nao tem cliente ja vem validado
                    bool validaCliente = true;
                    //enviarNFCe();
                    if (venda.Cliente != null)
                    {
                        Pessoa cli = new Pessoa();
                        cli = venda.Cliente;
                        validaCliente = validarClienteNFCe(cli);
                    }

                    ValidadorNotaSaida validador = new ValidadorNotaSaida();
                    if (validaCliente == true)
                    {
                        //Emitir NFCe pela nova classe
                        EmitirNFCe emitirNFCe = new EmitirNFCe();
                        carregarListaProdutos();
                        try
                        {
                            if (validador.validarProdutosNota(listaProdutosNFe))
                            {
                                //Concluir a venda antes de gerar a nota
                                numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFCe;
                                xmlStrEnvio = emitirNFCe.gerarXMLNfce(venda.ValorProdutos, venda.ValorFinal, venda.ValorDesconto, numeroNFCe, listaProdutosNFe, venda.Cliente, venda, null, null, "");
                                if (!String.IsNullOrEmpty(xmlStrEnvio))
                                {
                                    enviarXMLNFCeParaApi(xmlStrEnvio);
                                }
                                atualizarProximoNumeroNota();
                            }
                        }
                        catch (Exception erro)
                        {
                            GenericaDesktop.ShowErro(erro.Message);
                        }
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Essa venda já possui NFe gerada!");
            }
        }

        private bool validarClienteNFCe(Pessoa pessoa)
        {
            bool validacao = false;
            if (pessoa.Cnpj.Length == 11)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length < 11)
            {
                GenericaDesktop.ShowAlerta("Para NFCe o cliente selecionado deve ter CPF preenchido corretamente");
                validacao = false;
            }
            else if (pessoa.Cnpj.Length == 14)
            {
                GenericaDesktop.ShowAlerta("Em uma NFCe o cliente não pode ser pessoa jurídica, caso precise identificar a pessoa jurídica faça a emissão de uma NFe modelo 55");
                validacao = false;
            }
            if (!String.IsNullOrEmpty(pessoa.RazaoSocial))
                validacao = true;
            if (pessoa.EnderecoPrincipal == null)
            {
                GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo");
                validacao = false;
            }
            if (pessoa.EnderecoPrincipal != null)
            {
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Logradouro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NOME DA RUA)");
                    validacao = false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (BAIRRO)");
                    validacao = false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Numero))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NUMERO)");
                    validacao = false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Cep))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (CEP)");
                    validacao = false;
                }
            }
            return validacao;
        }

        private void carregarListaProdutos()
        {
            listaProdutosNFe = new List<NfeProduto>();
            IList<VendaItens> listaProdutoVenda = new List<VendaItens>();
            listaProdutoVenda = vendaItensController.selecionarProdutosPorVenda(venda.Id);
            int i = 0;
            foreach (var VendaItens in listaProdutoVenda)
            {
                i++;
                NfeProduto nfeProduto = new NfeProduto();
                produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(VendaItens.Produto.Id, Sessao.empresaFilialLogada);
                double quantidade = VendaItens.Quantidade;
                decimal descontoItem = VendaItens.ValorDesconto;
                produto.ValorVenda = VendaItens.ValorProduto;
                nfeProduto.Item = i.ToString();
                nfeProduto.Produto = produto;
                nfeProduto.QCom = quantidade.ToString();
                nfeProduto.Ncm = produto.Ncm;
                nfeProduto.Cest = produto.Cest;
                nfeProduto.Cfop = produto.CfopVenda;
                nfeProduto.CProd = produto.Id.ToString();
                nfeProduto.Nfe = null;
                nfeProduto.VProd = produto.ValorVenda * decimal.Parse(quantidade.ToString());
                nfeProduto.QTrib = quantidade;
                nfeProduto.VDesc = descontoItem;
                nfeProduto.DescricaoInterna = produto.Descricao;
                nfeProduto.XProd = produto.Descricao;
                nfeProduto.CodigoInterno = produto.Id.ToString();
                nfeProduto.CEan = "";
                nfeProduto.CEANTrib = "";
                nfeProduto.CfopEntrada = produto.CfopVenda;
                nfeProduto.AliqCofins = produto.PercentualCofins;
                nfeProduto.AliqIpi = produto.PercentualIpi;
                nfeProduto.AliqPis = produto.PercentualPis;
                nfeProduto.BaseCofins = 0;
                nfeProduto.BaseIpi = 0;
                nfeProduto.BasePis = 0;
                nfeProduto.VUnCom = produto.ValorVenda;
                nfeProduto.VUnTrib = produto.ValorVenda;
                nfeProduto.CodAnp = produto.CodAnp;
                nfeProduto.CodEnqIpi = produto.EnqIpi;
                nfeProduto.CodSeloIpi = produto.CodSeloIpi;
                nfeProduto.CstCofins = produto.CstCofins;
                nfeProduto.CstIcms = produto.CstIcms;
                nfeProduto.CstIpi = produto.CstIpi;
                nfeProduto.CstPis = produto.CstPis;
                nfeProduto.OrigemIcms = produto.OrigemIcms;
                nfeProduto.OutrosIcms = 0;
                nfeProduto.TPag = "";
                nfeProduto.UCom = produto.UnidadeMedida.Sigla;
                nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
                nfeProduto.ValorAcrescimo = 0;
                nfeProduto.ValorCofins = 0;
                nfeProduto.ValorDesconto = descontoItem;
                nfeProduto.ValorFinal = (produto.ValorVenda * decimal.Parse(quantidade.ToString())) - descontoItem;
                nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                nfeProduto.ValorIpi = 0;
                nfeProduto.ValorPis = 0;
                nfeProduto.ValorProduto = produto.ValorVenda;

                listaProdutosNFe.Add(nfeProduto);
            }
        }

        private void atualizarProximoNumeroNota()
        {
            //ATUALIZA NUMERO DA NOTA 
            ParametroSistema param = new ParametroSistema();
            param = Sessao.parametroSistema;
            if (nfe.Modelo.Equals("65"))
                param.ProximoNumeroNFCe = (int.Parse(nfe.NNf) + 1).ToString();
            if (nfe.Modelo.Equals("55"))
                param.ProximoNumeroNFe = (int.Parse(nfe.NNf) + 1).ToString();
            Controller.getInstance().salvar(param);
            Sessao.parametroSistema = param;
        }
        private void enviarXMLNFCeParaApi(string xmlNfce)
        {
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFCePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFCe);
            string caminhoXML = @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            codStatusRet = "";
            gravarXMLNaPasta(xmlNfce, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", false);
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string ambiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    ambiente = "1";
                String retorno = NSSuite.emitirNFCeSincrono(xmlNfce, "xml", Sessao.empresaFilialLogada.Cnpj, ambiente, caminhoXML, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 1;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);

                    //Estoque
                    GenericaDesktop generica = new GenericaDesktop();
                    foreach(NfeProduto nfeProduto in listaProdutosNFe)
                    {
                        generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), false, "VENDA", "NF EMITIDA EM CONSULTA VENDAS " + venda.Id.ToString(), nfe.Cliente, DateTime.Now, null);
                    }

                    //EnviaXML PAINEL LUNAR 
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    byte[] arquivo;
                    using (var stream = new FileStream(caminhoXML + nfe.Chave + "-procNFCe.xml", FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            arquivo = reader.ReadBytes((int)stream.Length);
                            var ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                            {
                                nfe.Nuvem = true;
                                Controller.getInstance().salvar(nfe);

                            }
                        }
                    }

                    armazenaXmlAutorizadoNoBanco();
                    GenericaDesktop.ShowInfo("Nota Fiscal autorizada!");

                    if (File.Exists(caminhoXML + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoXML + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                    else
                    {
                        if (nfe.Modelo.Equals("65"))
                        {
                            NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();
                            nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                            if (nFCeDownloadProc != null)
                            {
                                GenericaDesktop genericaDesktop = new GenericaDesktop();
                                genericaDesktop.gerarPDF3(nfe, nFCeDownloadProc.pdf, nfe.Chave, true);
                            }
                        }
                    }
                }

                //Falha conexao
                else if (retornoNFCe.motivo.Contains("timeout") || retornoNFCe.cStat.Equals("999"))
                {
                    //gerar em contigencia
                    gravarXMLNaPasta(xmlNfce, numeroNFCe, @"\XML\Tentativa\NFCe\", numeroNFCe, true);
                }

                else
                {
                    String erros = "";
                    if (retornoNFCe.erros != null)
                    {
                        for (int xx = 0; xx < retornoNFCe.erros.Count; xx++)
                        {
                            erros = erros + " " + retornoNFCe.erros[xx];
                        }
                    }
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 2;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            //Se nao tem internet gera em contigencia tb
            else
            {
                gravarXMLNaPasta(xmlStrEnvio, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", true);
            }
        }

        private void gravarXMLNaPasta(string xml, string numeroNFCe, string caminhoArmazenamento, string nomeArquivo, bool emiteContigencia)
        {
            if (!nomeArquivo.Contains(".xml"))
                nomeArquivo = nomeArquivo + ".xml";
            if (!Directory.Exists(caminhoArmazenamento))
            {
                Directory.CreateDirectory(caminhoArmazenamento);
            }
            if (!(caminhoArmazenamento.Length - 1).Equals(@"\"))
            {
                caminhoArmazenamento = caminhoArmazenamento + @"\";
            }
            string arquivo = caminhoArmazenamento + nomeArquivo;
            if (!File.Exists(arquivo))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
            if (emiteContigencia == true)
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.PastaRemessaNsCloud))
                {
                    arquivoContigencia = arquivo;
                    nomeArquivoContigencia = nomeArquivo;
                    File.Copy(arquivo, Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivo);

                    //dentro desse metodo retorna se a contigencia deu certo e grava o nfestatus.
                    aguardarParaLerRetornoContigencia(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Processados\nsConcluido\" + numeroNFCe + ".txt");
                }
                else
                    GenericaDesktop.ShowAlerta("Pasta de envio em contigência não foi configurada, " +
                        "favor solicite suporte a sua revenda autorizada e solicite a configuração, enquanto isso sua nota ficará " +
                        "na tela de gerenciamento de notas para você tentar reenviar a sefaz");
            }
        }

        private void armazenaXmlAutorizadoNoBanco()
        {
            NFCeDownloadProc nota = new NFCeDownloadProc();
            if (nfe.Modelo.Equals("65"))
            {
                nota = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

                if (nota.motivo.Contains("SUCESSO") || nota.motivo.Contains("sucesso") || nota.motivo.Contains("Sucesso"))
                {
                    Genericos genericosNF = new Genericos();
                    var nfe1 = Genericos.LoadFromXMLString<TNfeProc>(nota.nfeProc.xml);
                    genericosNF.gravarXMLNoBanco(nfe1, 0, "S", this.nfe.Id, false);

                    //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                    //string pastaDropbox = @"XML\NFCe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                    //string arquivo = nfe.Chave + "-procNFCe.xml";
                    //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                    //t.Wait();
                }
            }
            else if (nfe.Modelo.Equals("55"))
            {
                NFeDownloadProc55 nota55 = new NFeDownloadProc55();
                nota55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

                if (nota55.motivo.Contains("SUCESSO") || nota55.motivo.Contains("sucesso") || nota55.motivo.Contains("Sucesso"))
                {
                    Genericos genericosNF = new Genericos();
                    var notaLida55 = Genericos.LoadFromXMLString<TNfeProc>(nota55.xml);
                    genericosNF.gravarXMLNoBanco(notaLida55, 0, "S", this.nfe.Id, false);

                    string caminhoArquivo = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    string pastaArquivo = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas";
                    if (!File.Exists(caminhoArquivo))
                    {
                        gravarXMLNaPasta(nota55.xml, this.nfe.NNf, pastaArquivo, nfe.Chave + "-procNFe.xml", false);
                    }

                    //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    //string pastaDropbox = @"XML\NFe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                    //string arquivo = nfe.Chave + "-procNFe.xml";
                    //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                    //t.Wait();
                }
            }
        }
        private void abrirFormAguardar()
        {
            FrmAguarde uu = new FrmAguarde("5000", this.nfe);
            uu.ShowDialog();
        }

        private string lerTXT2(string caminhoArquivo)
        {
            //List<String> dadosLidos = new List<String>();
            string statusContigencia = "";
            string chave = "";
            System.IO.StreamReader arquivo = new System.IO.StreamReader(caminhoArquivo);
            string linha = "";
            while (true)
            {
                linha = arquivo.ReadLine();
                if (linha != null)
                {
                    string[] DadosColetados = linha.Split('|');
                    if (DadosColetados.Length > 4)
                    {
                        chave = DadosColetados[5].Replace("NFe", "");
                        statusContigencia = DadosColetados[4];
                    }
                }
                else
                    break;
            }
            if (nfe.Id > 0 && statusContigencia.Contains("Emitido em contingencia offline"))
            {
                nfe.Chave = chave;
                nfe.CodStatus = "";
                NfeStatus nfeStatus = new NfeStatus();
                nfeStatus.Id = 6;
                nfe.NfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                codStatusRet = "50";
                nfe.Status = "Emitido em contingencia offline";
                Controller.getInstance().salvar(nfe);
                GenericaDesktop.ShowInfo("Venda Concluida com Sucesso, Nota Fiscal gerada em Contigência!");
            }
            return chave;
        }
        private async void aguardarParaLerRetornoContigencia(string arquivoTXT)
        {
            await GenericaDesktop.VerificaProgramaContigenciaEstaEmExecucao();

            //Aguarda até gerar o arquivo txt na pasta de retorno ou em 10 segundos retorna com falha
            abrirFormAguardar();

            if (File.Exists(arquivoTXT))
            {
                string chaveContigencia = lerTXT2(arquivoTXT);
                if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                {
                    string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                    if (File.Exists(caminhoPDF))
                    {
                        NfeStatus nfeStatus1 = new NfeStatus();
                        nfeStatus1.Id = 6;
                        nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                        nfe.Status = retornoNFCe.motivo;
                        nfe.CodStatus = retornoNFCe.cStat;
                        if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                        {
                            nfe.Protocolo = retornoNFCe.nProt;
                            nfe.Chave = retornoNFCe.chNFe;
                        }
                        Controller.getInstance().salvar(nfe);
                        FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                        frmPDF.ShowDialog();
                        //System.Diagnostics.Process.Start(caminhoPDF);
                    }
                }
            }
            else
            {
                await Task.Delay(5000);
                if (File.Exists(arquivoTXT))
                {
                    string chaveContigencia = lerTXT2(arquivoTXT);
                    if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                    {
                        string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                        if (File.Exists(caminhoPDF))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 6;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retornoNFCe.motivo;
                            nfe.CodStatus = retornoNFCe.cStat;
                            if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                            {
                                nfe.Protocolo = retornoNFCe.nProt;
                                nfe.Chave = retornoNFCe.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                            frmPDF.ShowDialog();
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Falha ao gerar nota em contigência, será realizado uma nova tentativa!");
                    if (!File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivoContigencia))
                        File.Copy(arquivoContigencia, Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivoContigencia);

                    await Task.Delay(5000);
                    if (File.Exists(arquivoTXT))
                    {
                        string chaveContigencia = lerTXT2(arquivoTXT);
                        if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                        {
                            string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                            if (File.Exists(caminhoPDF))
                            {
                                NfeStatus nfeStatus1 = new NfeStatus();
                                nfeStatus1.Id = 6;
                                nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                                nfe.Status = retornoNFCe.motivo;
                                nfe.CodStatus = retornoNFCe.cStat;
                                if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                                {
                                    nfe.Protocolo = retornoNFCe.nProt;
                                    nfe.Chave = retornoNFCe.chNFe;
                                }
                                Controller.getInstance().salvar(nfe);
                                FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                                frmPDF.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        NfeStatus nfeStatus1 = new NfeStatus();
                        nfeStatus1.Id = 2;
                        nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                        nfe.Status = retornoNFCe.motivo;
                        nfe.CodStatus = retornoNFCe.cStat;
                        if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                        {
                            nfe.Protocolo = retornoNFCe.nProt;
                            nfe.Chave = retornoNFCe.chNFe;
                        }
                        Controller.getInstance().salvar(nfe);
                        GenericaDesktop.ShowAlerta("Venda Concluída, porém a nota fiscal nao teve retorno de autorização, verifique depois na tela de gerenciamento de notas!");
                    }
                }
            }
        }

        private void grid_Click(object sender, EventArgs e)
        {
            
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            tabControlAdv1.SelectedTab = tabProdutos;
        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(tabControlAdv1.SelectedTab == tabVenda)
                {
                    gridProduto.DataSource = null;
                }
                if (tabControlAdv1.SelectedTab == tabProdutos)
                {
                    gridProduto.DataSource = null;
                    venda = new Venda();
                    venda = (Venda)grid.SelectedItem;
                    IList<VendaItens> listaProdutosVenda = vendaItensController.selecionarProdutosPorVenda(venda.Id);
                    gridProduto.DataSource = listaProdutosVenda;
                }
            }
            catch
            {

            }
        }

        private void grid_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            venda = new Venda();
            venda = (Venda)grid.SelectedItem;
            IList<VendaItens> listaProdutosVenda = vendaItensController.selecionarProdutosPorVenda(venda.Id);
            gridProduto.DataSource = listaProdutosVenda;
        }

        private void btnGerarNfe_Click(object sender, EventArgs e)
        {
            venda = new Venda();
            venda = (Venda)grid.SelectedItem;
            if (venda.Nfe == null)
            {
                if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja gerar a nota fiscal - NFe modelo 55 ?"))
                {
                    //se nao tem cliente ja vem validado
                    bool validaCliente = false;
                    //enviarNFCe();
                    if (venda.Cliente != null)
                    {
                        Pessoa cli = new Pessoa();
                        cli = venda.Cliente;
                        validaCliente = validarClienteNFCe(cli);

                        ValidadorNotaSaida validador = new ValidadorNotaSaida();
                        if (validaCliente == true)
                        {
                            //Emitir NFe pela nova classe
                            EmitirNFe emitirNFe = new EmitirNFe();
                            carregarListaProdutos();
                            try
                            {
                                if (validador.validarProdutosNota(listaProdutosNFe))
                                {
                                    numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFe;
                                    xmlStrEnvio = emitirNFe.gerarXMLNfe(venda.ValorProdutos, venda.ValorFinal, venda.ValorDesconto, numeroNFCe, listaProdutosNFe, venda.Cliente, venda, false, "VENDA", null);
                                    if (!String.IsNullOrEmpty(xmlStrEnvio))
                                    {
                                        enviarXMLNFeParaApi(xmlStrEnvio);
                                    }
                                }
                            }
                            catch (Exception erro)
                            {
                                if (erro.Message.Contains("Unexpected character encountered while parsing value"))
                                {
                                    NfeController nfeController = new NfeController();
                                    nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFe);
                                    NfeStatus nfeStatus = new NfeStatus();
                                    nfeStatus.Id = 2;
                                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                                    Controller.getInstance().salvar(nfe);
                                    Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                                    Controller.getInstance().salvar(Sessao.parametroSistema);
                                    GenericaDesktop.ShowAlerta("Falha de comunicação com a sefaz, tente reenviar a nota pelo modulo de gerenciamento de notas");
                                }
                                else
                                    GenericaDesktop.ShowErro(erro.Message);
                            }
                        }
                        atualizarProximoNumeroNota();
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Para emitir NFe deve selecionar um cliente com dados válidos, tendo nome, cpf ou cnpj, endereço completo!");
                    }
                }
            }
        }

        private void enviarXMLNFeParaApi(string xmlNfe)
        {
            RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
            NfeStatus nfeStatus = new NfeStatus();
            string caminhoSalvarXml = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFe);

            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            codStatusRet = "";
            gravarXMLNaPasta(xmlNfe, nfe.NNf, @"\XML\Tentativa\NFe\", nfe.NNf + ".xml", false);
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string ambiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    ambiente = "1";
                String retorno = NSSuite.emitirNFeSincrono(xmlNfe, "xml", Sessao.empresaFilialLogada.Cnpj, "XP", ambiente, caminhoSalvarXml, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);
                //Armazenar nsNRec
                if (!String.IsNullOrEmpty(retornoNFCe.nsNRec))
                    nfe.NsNrec = retornoNFCe.nsNRec;

                if (!String.IsNullOrEmpty(retornoNFCe.cStat))
                    codStatusRet = retornoNFCe.cStat;
                else
                    codStatusRet = retornoNFCe.statusEnvio;

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    nfeStatus = new NfeStatus();
                    nfeStatus.Id = 1;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);

                    //Estoque
                    GenericaDesktop generica = new GenericaDesktop();
                    foreach (NfeProduto nfeProduto in listaProdutosNFe)
                    {
                        generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), false, "VENDA", "NF EMITIDA EM CONSULTA VENDAS " + venda.Id.ToString(), nfe.Cliente, DateTime.Now, null);
                    }
                    //EnviaXML PAINEL LUNAR 
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    byte[] arquivo;
                    using (var stream = new FileStream(caminhoSalvarXml + nfe.Chave + "-procNFe.xml", FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            arquivo = reader.ReadBytes((int)stream.Length);
                            var ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                            {
                                nfe.Nuvem = true;
                                Controller.getInstance().salvar(nfe);

                            }
                        }
                    }
                    armazenaXmlAutorizadoNoBanco();
                    GenericaDesktop.ShowInfo("Venda concluída com sucesso, nota autorizada!");
                    if (File.Exists(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                }

                //Erro interno ao processar a requisicao // geralmente falha sefaz, aguardamos 5 segundos e verificamos novo retorno
                else if (retornoNFCe.motivo.Contains("Documento") || retornoNFCe.motivo.Contains("Sefaz") || retornoNFCe.motivo.Contains("sefaz") || retornoNFCe.motivo.Contains("Erro interno ao processar a requisicao"))
                {
                    Thread.Sleep(5000);

                    //CONSULTA NSNREC
                    ConsStatusProcessamentoReq consStatusProcessamentoReq = new ConsStatusProcessamentoReq();
                    consStatusProcessamentoReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                    consStatusProcessamentoReq.nsNRec = nfe.NsNrec;
                    if (Sessao.parametroSistema.AmbienteProducao == true)
                        consStatusProcessamentoReq.tpAmb = "1";
                    else
                        consStatusProcessamentoReq.tpAmb = "2";


                    String retornoConsulta = NSSuite.consultarStatusProcessamento(nfe.Modelo, consStatusProcessamentoReq);
                    if (retornoConsulta != null)
                        retConsulta = JsonConvert.DeserializeObject<RetConsultaProcessamentoNF>(retornoConsulta);

                    if (retConsulta.xMotivo != null)
                    {
                        if (retConsulta.xMotivo.Contains("Autorizado o uso"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 1;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            armazenaXmlAutorizadoNoBanco();
                            generica.gravarXMLNaPasta(retConsulta.xml, retConsulta.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");
                            if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                            {
                                //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                frmPDF.ShowDialog();
                            }
                            else
                            {
                                NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                                nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                if (nFeDownloadProc55 != null)
                                {
                                    GenericaDesktop gen = new GenericaDesktop();
                                    gen.gerarPDF3(nfe, nFeDownloadProc55.pdf, nfe.Chave, true);
                                }
                            }
                            if (!String.IsNullOrEmpty(retConsulta.xMotivo))
                                GenericaDesktop.ShowInfo(" (" + retConsulta.cStat + ") > " + retConsulta.xMotivo);
                        }
                        else if (retConsulta.xMotivo.Contains("Sem retorno de status da sefaz"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 2;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                //nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                                Controller.getInstance().salvar(nfe);
                                Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                                Controller.getInstance().salvar(Sessao.parametroSistema);
                                GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: " + retConsulta.cStat + " " + retConsulta.xMotivo + ", na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                            }
                        }
                    }
                }
                //se a nota continua nao autorizada, verifica se teve erros
                if (String.IsNullOrEmpty(nfe.Chave) || nfe.Chave.Equals("123"))
                {
                    String erros = "";
                    if (retornoNFCe.erros != null)
                    {
                        for (int xx = 0; xx < retornoNFCe.erros.Count; xx++)
                        {
                            erros = erros + " " + retornoNFCe.erros[xx];
                        }
                    }
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 2;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    if (!String.IsNullOrEmpty(retConsulta.chNFe))
                    {
                        //nfe.Protocolo = retConsulta.nProt;
                        nfe.Chave = retConsulta.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                    Controller.getInstance().salvar(Sessao.parametroSistema);
                    GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: Verifique sua conexão com a internet, após normalizar acesse o menu de gerenciamento de notas para reenviar a mesma!");

            }
        }
        private void reimprimirTicket()
        {
            try
            {
                venda = new Venda();
                venda = (Venda)grid.SelectedItem;
                //Imprimir Ticket
                FrmImprimirTicketVenda frmImprimirTicket = new FrmImprimirTicketVenda(venda);
                frmImprimirTicket.ShowDialog();
            }
            catch
            {

            }
        }

        private void iconeTicket_Click(object sender, EventArgs e)
        {
            reimprimirTicket();
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            reimprimirTicket();
        }

        private async void btnVerificarPix_Click(object sender, EventArgs e)
        {
            try
            {
                venda = (Venda)grid.SelectedItem;
                if (!String.IsNullOrEmpty(venda.QrCodePix))
                {
                    if (venda == null)
                    {
                        GenericaDesktop.ShowAlerta("Selecione uma venda.");
                        return;
                    }

                    GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                    galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();

                    DateTime dataInicial = DateTime.Now;
                    DateTime dataFinal = DateTime.Now;
                    string myIdTransacao = "VENDA_" + venda.Id.ToString();

                    GalaxPayRetornoPix galaxyPay_RetornoPix = await galaxyPayApiIntegracao.GalaxyPay_ListarRetornoTransacoesPixAsync(
                        dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"), myIdTransacao);

                    if (galaxyPay_RetornoPix != null && galaxyPay_RetornoPix.Transactions != null)
                    {
                        foreach (var transaction in galaxyPay_RetornoPix.Transactions)
                        {
                            if (transaction.chargeMyId != null)
                            {
                                if (transaction.status.Equals("payedPix"))
                                {
                                    GenericaDesktop.ShowInfo("Pagamento Confirmado!");
                                }
                                else
                                {
                                    GenericaDesktop.ShowAlerta("O Pagamento ainda não foi Confirmado :-(");
                                }
                            }
                        }

                        if (galaxyPay_RetornoPix.totalQtdFoundInPage == 0)
                        {
                            GenericaDesktop.ShowAlerta("O Pagamento ainda não foi Confirmado :-(");
                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("O Pagamento ainda não foi Confirmado :-(");
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Essa venda não teve geração de QRCODE PIX");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro("Ocorreu um erro: " + ex.Message);
            }
        }

        private void FrmConsultaVendas_Load(object sender, EventArgs e)
        {
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                btnCancelar.Enabled = Sessao.permissoes.Contains("68");
            }
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Parse(txtDataInicial.Value.ToString());
            DateTime datafinal = DateTime.Parse(txtDataFinal.Value.ToString());
            String data1 = "";
            String data2 = "";
            if(chkAtivarData.Checked == true)
            {
                data1 = dataInicial.ToString("dd/MM/yyyy");
                data2 = datafinal.ToString("dd/MM/yyyy");
            }
            FrmRelatorioVendas frm = new FrmRelatorioVendas(data1, data2, listaVenda);
            frm.ShowDialog();
        }



    }
}
