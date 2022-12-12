using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ClassesBO;
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
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static LunarBase.Utilidades.ManifestoDownload;
using Exception = System.Exception;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmOrdemServicoLista : Form
    {
        string arquivoContigencia = "";
        string nomeArquivoContigencia = "";
        GenericaDesktop generica = new GenericaDesktop();
        EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
        string codStatusRet = "";
        String xmlStrEnvio = "";
        string numeroNFCe = "";
        OrdemServico ordemServico = new OrdemServico();
        OrdemServicoController ordemServicoController = new OrdemServicoController();
        IList<OrdemServico> listaOrdemServico = new List<OrdemServico>();
        IList<NfeProduto> listaProdutosNFe = new List<NfeProduto>();
        bool passou = false;
        NfeProduto nfeProduto = new NfeProduto();
        decimal valorFinalNota = 0;
        decimal valorProdutosSemDesconto = 0;
        decimal valorDescontoProdutos = 0;
        Nfe nfe = new Nfe();
        public FrmOrdemServicoLista()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            object ordemServico = new OrdemServico();
            try
            {
                using (FrmOrdemServico uu = new FrmOrdemServico())
                {
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
                    uu.showModalNovo(ref ordemServico);
                    formBackground.Dispose();
                    btnPesquisar.PerformClick();
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

        private void pesquisarOrdemServico()
        {
            listaOrdemServico = new List<OrdemServico>();
          
            string sql = "Select * From OrdemServico Tabela ";

            if (!String.IsNullOrEmpty(txtCodDependente.Texts) || !txtDataEntregaInicial.Text.Equals("  /  /    "))
            {
                sql = sql + "INNER JOIN OrdemServicoExame Exame on Exame.OrdemServico = Tabela.Id ";
            }

            sql = sql + "where Tabela.FlagExcluido <> true ";

            if (!String.IsNullOrEmpty(txtCodDependente.Texts))
            {
                sql = sql + "and Exame.Dependente = " + txtCodDependente.Texts + " ";
            }

            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts + " ";

            if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
                sql = sql + "and Tabela.Id = " + txtNumeroOS.Texts + " ";

            if (radioAbertas.Checked == true)
                sql = sql + "and Tabela.Status = 'ABERTA' ";
            else if(radioEncerradas.Checked == true)
                sql = sql + "and Tabela.Status = 'ENCERRADA' ";

            if (chkAtivarDataAbertura.Checked == true)
            {
                DateTime dataIni = DateTime.Parse(txtDataAberturaInicial.Value.ToString());
                DateTime dataFin = DateTime.Parse(txtDataAberturaFinal.Value.ToString());
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                sql = sql + "and Tabela.DataAbertura BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
            }
            if (chkAtivarDataEncerramento.Checked == true)
            {
                DateTime dataIni = DateTime.Parse(txtDataEncerramentoInicial.Value.ToString());
                DateTime dataFin = DateTime.Parse(txtDataEncerramentoFinal.Value.ToString());
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                sql = sql + "and Tabela.DataEncerramento BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
            }

            if(!txtDataEntregaInicial.Text.Equals("  /  /    ") && !txtDataEntregaFinal.Text.Equals("  /  /    "))
            {
                DateTime dataIni = DateTime.Parse(txtDataEntregaInicial.Text);
                DateTime dataFin = DateTime.Parse(txtDataEntregaFinal.Text);
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                sql = sql + "and Exame.DataEntrega BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
            }
           

            string orderBy = " group by Tabela.Id order by Tabela.Id desc";
            //MessageBox.Show(sql + orderBy);
            listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL(sql + orderBy);
            if (listaOrdemServico.Count > 0)
            {
                sfDataPager1.DataSource = listaOrdemServico;
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
                //calculaTotalJurosGridVisto();
                //preencherSumario();

                //int w = Screen.PrimaryScreen.Bounds.Width;
                //int h = Screen.PrimaryScreen.Bounds.Height;
                //if (w == 1920 && h == 1080)
                //{
                //    this.grid.View.Records.CollectionChanged += Records_CollectionChanged;
                //}
            }
            else
            {
                grid.DataSource = null;
                sfDataPager1.DataSource = null;
                grid.Refresh();
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaOrdemServico.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            txtCodCliente.Texts = "";
            txtCliente.Texts = "";
            pesquisaCliente(false);
        }
        private void pesquisaCliente(bool ativaFiltroNome)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                String sqlAd = "";
                if (ativaFiltroNome == true)
                    sqlAd = "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%'";

                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", sqlAd))
                {
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
                                txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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

        private void pesquisaDependente(bool ativaFiltroNome)
        {
            Object pessoaDependenteOjeto = new PessoaDependente();
            Object pessoaObjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                String sqlAd = "";
                if(!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sqlAd = "and Tabela.Pessoa = " + txtCodCliente.Texts + " ";
                if (ativaFiltroNome == true)
                    sqlAd = sqlAd + "and CONCAT(Tabela.Id, ' ', Tabela.Nome, ' ', Tabela.Cpf) like '%" + txtDependente.Texts + "%'";

                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PessoaDependente", sqlAd))
                {
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
                    switch (uu.showModal("PessoaDependente", "", ref pessoaDependenteOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            Pessoa pessoa = new Pessoa();
                            pessoa.Id = int.Parse(txtCodCliente.Texts);
                            pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                            if (pessoa != null)
                            {
                                FrmClienteCadastro form = new FrmClienteCadastro(pessoa);
                                if (form.showModalNovo(ref pessoaObjeto) == DialogResult.OK)
                                {
                                    //txtDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Nome;
                                    //txtCodDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Id.ToString();
                                    btnPesquisaDependente.PerformClick();
                                }
                            }
                            break;
                        case DialogResult.OK:
                            txtDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Nome;
                            txtCodDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Id.ToString();
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

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaCliente(true);
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Pessoa cliente = new Pessoa();
                PessoaController pessoaController = new PessoaController();
                txtCliente.Texts = "";
                try
                {
                    if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        cliente.Id = int.Parse(txtCodCliente.Texts);
                        cliente = (Pessoa)pessoaController.selecionar(cliente);
                        if (cliente != null)
                        {
                            txtCliente.Texts = cliente.RazaoSocial;
                            txtCodCliente.Texts = cliente.Id.ToString();

                        }
                        else
                        {

                            txtCliente.Texts = "";
                            txtCodCliente.Texts = "";
                        }
                    }
                }
                catch
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
                    GenericaDesktop.ShowAlerta("Cliente não encontrado");
                }
            }
        }

        private void chkAtivarDataAbertura_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAtivarDataAbertura.Checked == true)
                {
                    txtDataAberturaInicial.Enabled = true;
                    txtDataAberturaFinal.Enabled = true;
                }
                else
                {
                    txtDataAberturaInicial.Enabled = false;
                    txtDataAberturaFinal.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void chkAtivarDataEncerramento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAtivarDataEncerramento.Checked == true)
                {
                    txtDataEncerramentoInicial.Enabled = true;
                    txtDataEncerramentoFinal.Enabled = true;
                }
                else
                {
                    txtDataEncerramentoInicial.Enabled = false;
                    txtDataEncerramentoFinal.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void txtNumeroOS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
                {
                    listaOrdemServico = new List<OrdemServico>();
                    string sql = "From OrdemServico Tabela where Tabela.FlagExcluido <> true and Tabela.Id = " + txtNumeroOS.Texts;
                    listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL(sql);
                    if(listaOrdemServico.Count > 0)
                    {
                        sfDataPager1.DataSource = listaOrdemServico;
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
                    }
                    else
                    {
                        grid.DataSource = null;
                        sfDataPager1.DataSource = null;
                        grid.Refresh();
                        GenericaDesktop.ShowAlerta("Ordem de Serviço não encontrada!");
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarOrdemServico();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarOrdemServico();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
           
            try
            {
                if ((e.RowData as OrdemServico) != null)
                {
                    if ((e.RowData as OrdemServico).Status.Equals("ABERTA"))
                    {
                        e.Style.TextColor = Color.Red;
                    }
                }
            }
            catch
            {

            }
        }

        private void FrmOrdemServicoLista_Load(object sender, EventArgs e)
        {
            if(passou == false)
            {
                pesquisarOrdemServico();
                passou = true;
            }
        }

        private void btnPesquisaDependente_Click(object sender, EventArgs e)
        {
            txtCodDependente.Texts = "";
            txtDependente.Texts = "";
            pesquisaDependente(false);
        }

        private void txtDependente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaDependente(true);
            }
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                editarCadastro(ordemServico);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja editar!");
        }
        private void editarCadastro(OrdemServico ordemServico)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmOrdemServico uu = new FrmOrdemServico(ordemServico))
                {
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    txtCodDependente.Texts = "";
                    txtDependente.Texts = "";
                    pesquisarOrdemServico();
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                FrmImpressaoOrdemServico frmImpressaoOrdemServico = new FrmImpressaoOrdemServico(ordemServico);
                frmImpressaoOrdemServico.ShowDialog();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Clique primeiro na Ordem de Serviço que deseja imprimir");
            }
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                if (!ordemServico.Status.Equals("ENCERRADA"))
                {
                    Form formBackground = new Form();
                    IList<ContaReceber> listaReceber = new List<ContaReceber>();
                    IList<ContaPagar> listaPagar = new List<ContaPagar>();
                    FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "ORDEMSERVICO", false, true);
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    uu.Dispose();
                    pesquisarOrdemServico();
                }
                else
                    GenericaDesktop.ShowAlerta("Clique na ordem que deseja encerrar!");
            }
            else
                GenericaDesktop.ShowAlerta("Ordem de Serviço já está encerrada!");


        }

        private void btnGerarNFCe_Click(object sender, EventArgs e)
        {
            valorFinalNota = 0;
            valorDescontoProdutos = 0;
            valorProdutosSemDesconto = 0;
            ordemServico = new OrdemServico();
            ordemServico = (OrdemServico)grid.SelectedItem;
            if (ordemServico.Nfe == null)
            {
                if (GenericaDesktop.ShowConfirmacao("Gerar NFC-e dos produtos da O.S " + ordemServico.Id.ToString() + "?"))
                {
                    //se nao tem cliente ja vem validado
                    bool validaCliente = true;
                    //enviarNFCe();
                    if (ordemServico.Cliente != null)
                    {
                        Pessoa cli = new Pessoa();
                        cli = ordemServico.Cliente;
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
                                //Concluir a O.S antes de gerar a nota
                                numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFCe;
                                xmlStrEnvio = emitirNFCe.gerarXMLNfce(valorProdutosSemDesconto, valorFinalNota, valorDescontoProdutos, numeroNFCe, listaProdutosNFe, ordemServico.Cliente, null, ordemServico);
                                if (!String.IsNullOrEmpty(xmlStrEnvio))
                                {
                                    enviarXMLNFCeParaApi(xmlStrEnvio);
                                }
                                atualizarProximoNumeroNota();
                            }
                        }
                        catch (Exception erro)
                        {
                            ordemServico.Nfe = null;
                            Controller.getInstance().salvar(ordemServico);
                            GenericaDesktop.ShowErro(erro.Message);
                        }
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Ordem de Serviço já possui NF gerada!");
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
                    foreach (NfeProduto nfeProduto in listaProdutosNFe)
                    {
                        generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), false, "ORDEMSERVICO", "NF EMITIDA EM ORDEM DE SERVICO " + ordemServico.Id.ToString(), nfe.Cliente, DateTime.Now, null);
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
                GenericaDesktop.ShowInfo("Nota Fiscal gerada em Contigência!");
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
                        GenericaDesktop.ShowAlerta("A nota fiscal nao teve retorno de autorização, verifique depois na tela de gerenciamento de notas!");
                    }
                }
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
                    genericosNF.gravarXMLNoBanco(nfe1, 0, "S", this.nfe.Id);

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
                    genericosNF.gravarXMLNoBanco(notaLida55, 0, "S", this.nfe.Id);

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

        private void carregarListaProdutos()
        {
            listaProdutosNFe = new List<NfeProduto>();
            IList<OrdemServicoProduto> listaProdutoOrdemServico = new List<OrdemServicoProduto>();
            ProdutoController produtoController = new ProdutoController();
            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            listaProdutoOrdemServico = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            int i = 0;
            foreach (var VendaItens in listaProdutoOrdemServico)
            {
                i++;
                nfeProduto = new NfeProduto();
                Produto produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(VendaItens.Produto.Id, Sessao.empresaFilialLogada);
                double quantidade = VendaItens.Quantidade;
                decimal descontoItem = VendaItens.Desconto;
                produto.ValorVenda = VendaItens.ValorUnitario;
                nfeProduto.Item = i.ToString();
                nfeProduto.Produto = produto;
                nfeProduto.QCom = quantidade.ToString();
                nfeProduto.Ncm = produto.Ncm;
                nfeProduto.Cest = produto.Cest;
                nfeProduto.Cfop = produto.CfopVenda;
                nfeProduto.CProd = produto.Id.ToString();
                nfeProduto.Nfe = null;
                nfeProduto.VProd = produto.ValorVenda;
                nfeProduto.QTrib = quantidade;
                nfeProduto.VDesc = descontoItem;
                nfeProduto.DescricaoInterna = produto.Descricao;
                nfeProduto.XProd = produto.Descricao;
                nfeProduto.CodigoInterno = produto.Id.ToString();
                nfeProduto.CEan = "";
                nfeProduto.CEANTrib = "";
                nfeProduto.CfopEntrada = "";
                nfeProduto.AliqCofins = produto.PercentualCofins;
                nfeProduto.AliqIpi = produto.PercentualIpi;
                nfeProduto.AliqPis = produto.PercentualPis;
                nfeProduto.BaseCofins = 0;
                nfeProduto.BaseIpi = 0;
                nfeProduto.BasePis = 0;
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

                valorFinalNota = valorFinalNota + nfeProduto.ValorFinal;
                valorProdutosSemDesconto = valorProdutosSemDesconto + nfeProduto.ValorProduto;
                valorDescontoProdutos = valorDescontoProdutos + nfeProduto.VDesc;

                listaProdutosNFe.Add(nfeProduto);
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

        private void btnGerarNFe_Click(object sender, EventArgs e)
        {
            valorFinalNota = 0;
            valorDescontoProdutos = 0;
            valorProdutosSemDesconto = 0;
            ordemServico = new OrdemServico();
            ordemServico = (OrdemServico)grid.SelectedItem;
            if (ordemServico.Nfe == null)
            {
                if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja gerar a nota fiscal - NFe modelo 55 ?"))
                {
                    //se nao tem cliente ja vem validado
                    bool validaCliente = false;
                    //enviarNFCe();
                    if (ordemServico.Cliente != null)
                    {
                        Pessoa cli = new Pessoa();
                        cli = ordemServico.Cliente;
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
                                    xmlStrEnvio = emitirNFe.gerarXMLNfe(valorProdutosSemDesconto, valorFinalNota, valorDescontoProdutos, numeroNFCe, listaProdutosNFe, ordemServico.Cliente, null, false, "VENDA", ordemServico);
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
            else
                GenericaDesktop.ShowAlerta("Nota Fiscal já foi gerada, consulta a tela de Monitoramento de Notas");
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
                        generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), false, "VENDA", "NF EMITIDA LISTA DE O.S " + ordemServico.Id.ToString(), ordemServico.Cliente, DateTime.Now, null);
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
                    GenericaDesktop.ShowInfo("Nota autorizada!");
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
                                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retConsulta.cStat + " " + retConsulta.xMotivo + ", na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
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
                    GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: Verifique sua conexão com a internet, após normalizar acesse o menu de gerenciamento de notas para reenviar a mesma!");

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                if(GenericaDesktop.ShowConfirmacao("Deseja realmente excluir a ordem de serviço?"))
                {
                    if (ordemServico.Nfe == null || ordemServico.Nfe.Cancelada == true)
                    {
                        ContaReceberController contaReceberController = new ContaReceberController();
                        IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber as Tabela Where Tabela.OrdemServico = " + ordemServico.Id);
                        if (listaReceber.Count > 0)
                        {
                            foreach (ContaReceber contaReceber in listaReceber)
                            {
                                Controller.getInstance().excluir(contaReceber);
                            }
                        }
                        CaixaController caixaController = new CaixaController();
                        IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorSql("From Caixa as Tabela Where Tabela.TabelaOrigem = 'ORDEMSERVICO' and Tabela.IdOrigem = '" + ordemServico.Id + "'");
                        if (listaCaixa.Count > 0)
                        {
                            foreach (Caixa caixa in listaCaixa)
                            {
                                Controller.getInstance().excluir(caixa);
                            }
                        }
                        Controller.getInstance().excluir(ordemServico);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso");
                        pesquisarOrdemServico();
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Ordem de Serviço com Nota Fiscal Vinculada, somente é possível excluir se a nota fiscal for cancelada!");
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Clique primeiro na Ordem de Serviço que deseja imprimir");
            }
        }
    }
}
