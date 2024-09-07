using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasReceber.Reports;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.LunarChatIntegracao;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using Syncfusion.Data;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Lunar.Utils.LunarChatIntegracao.LunarChatMensagem;
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
        bool produtoNegativo = false;
        Pessoa vendedor = new Pessoa();
        public FrmOrdemServicoLista()
        {
            InitializeComponent();
            txtDataAberturaFinal.Text = DateTime.Now.ToShortDateString();
            txtDataAberturaInicial.Text = DateTime.Now.ToShortDateString();
            txtDataEncerramentoFinal.Text = DateTime.Now.ToShortDateString();
            txtDataEncerramentoInicial.Text = DateTime.Now.ToShortDateString();

            if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
            {
                linkLabel1.Visible = false;
                btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.whatsapp_logo_icone;
                btnEnviarWhats.Enabled = true;
            }
            else
            {
                linkLabel1.Links.Add(0, linkLabel1.Text.Length, "https://www.lunarchat.com.br");
                linkLabel1.Visible = true;
                btnEnviarWhats.Enabled = false;
                btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.WhatsappCinza2_1;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void criarAgrupamentoGrid()
        {
        
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
                    //btnPesquisar.PerformClick();
                    txtNumeroOS.Texts = ((OrdemServico)ordemServico).Id.ToString();
                    pesquisarOrdemServicoPeloID();
                    txtNumeroOS.Texts = "";
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
            produtoNegativo = false;
            if(!String.IsNullOrEmpty(txtCodCliente.Texts) && radioAbertas.Checked == true)
                grid.SelectionMode = GridSelectionMode.Extended;
            else
                grid.SelectionMode = GridSelectionMode.Single;

            string sql = "Select * From ordemservico Tabela ";

            if (!String.IsNullOrEmpty(txtCodDependente.Texts) || !txtDataEntregaInicial.Text.Equals("  /  /    "))
            {
                sql = sql + "INNER JOIN ordemservicoexame Exame on Exame.OrdemServico = Tabela.Id ";
            }

            if(radioCanceladas.Checked == false)
                sql = sql + "where Tabela.FlagExcluido <> true ";
            else if (radioCanceladas.Checked == true)
                sql = sql + "where Tabela.FlagExcluido = true ";

            if (!String.IsNullOrEmpty(txtCodDependente.Texts))
            {
                sql = sql + "and Exame.Dependente = " + txtCodDependente.Texts + " ";
            }

            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts + " ";

            if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
                sql = sql + "and Tabela.Vendedor = " + txtCodVendedor.Texts + " ";

            if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
                sql = sql + "and Tabela.Id = " + txtNumeroOS.Texts + " ";

            if (radioAbertas.Checked == true)
                sql = sql + "and Tabela.Status = 'ABERTA' ";
            else if (radioEncerradas.Checked == true)
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
                preencherSumario();
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
                                txtNumeroOS.Focus();
                                generica.buscarAlertaCadastrado((Pessoa)pessoaObj);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtNumeroOS.Focus();
                            generica.buscarAlertaCadastrado((Pessoa)pessoaOjeto);
                            pesquisarOrdemServico();
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
        //private void pesquisaCliente(bool ativaFiltroNome)
        //{
        //    Object pessoaOjeto = new Pessoa();
        //    Form formBackground = new Form();
        //    try
        //    {
        //        String sqlAd = "";
        //        if (ativaFiltroNome == true)
        //            sqlAd = "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%'";

        //        using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", sqlAd))
        //        {
        //            formBackground.StartPosition = FormStartPosition.Manual;
        //            //formBackground.FormBorderStyle = FormBorderStyle.None;
        //            formBackground.Opacity = .50d;
        //            formBackground.BackColor = Color.Black;
        //            //formBackground.Left = Top = 0;
        //            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
        //            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
        //            formBackground.WindowState = FormWindowState.Maximized;
        //            formBackground.TopMost = false;
        //            formBackground.Location = this.Location;
        //            formBackground.ShowInTaskbar = false;
        //            formBackground.Show();
        //            uu.Owner = formBackground;
        //            switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
        //            {
        //                case DialogResult.Ignore:
        //                    uu.Dispose();
        //                    FrmClienteCadastro form = new FrmClienteCadastro();
        //                    if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
        //                    {
        //                        txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                        txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                    }
        //                    form.Dispose();
        //                    break;
        //                case DialogResult.OK:
        //                    txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                    break;
        //            }

        //            formBackground.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        formBackground.Dispose();
        //    }
        //}

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

        private void txtCliente_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private void txtCodCliente_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
                            generica.buscarAlertaCadastrado(cliente);

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

        private void txtNumeroOS_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisarOrdemServicoPeloID();
            }
        }

        private void pesquisarOrdemServicoPeloID()
        {
            if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
            {
                ordemServico = new OrdemServico();
                listaOrdemServico = new List<OrdemServico>();
                // string sql = "From ordemservico Tabela where Tabela.FlagExcluido <> true and Tabela.Id = " + txtNumeroOS.Texts;
                //listaOrdemServico = ordemServicoController.selecionarOrdemServicoPorSQL(sql);
                ordemServico = ordemServicoController.selecionarOrdemServicoPorID(int.Parse(txtNumeroOS.Texts));
                if (ordemServico != null)
                {
                    if (ordemServico.Id > 0)
                    {
                        listaOrdemServico.Add(ordemServico);
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
                    if ((e.RowData as OrdemServico).OperadorCadastro.Equals("1"))
                    {
                        (e.RowData as OrdemServico).OperadorCadastro = "";
                    }
                    if ((e.RowData as OrdemServico).FlagExcluido == true)
                    {
                        e.Style.TextColor = Color.DarkGoldenrod;
                        e.Style.Font.FontStyle = FontStyle.Strikeout;
                       (e.RowData as OrdemServico).Status = "CANCELADO/EXCLUÍDO";
                       
                    }
                }
  
            }
            catch
            {

            }
        }

        private void FrmOrdemServicoLista_Load(object sender, EventArgs e)
        {
            if (passou == false)
            {
                pesquisarOrdemServico();

                if (Sessao.permissoes.Count > 0)
                {
                    // Habilitar ou desabilitar os controles com base nas permissões
                    btnNovo.Enabled = Sessao.permissoes.Contains("30");
                    btnExcluir.Enabled = Sessao.permissoes.Contains("32");
                    if (!Sessao.permissoes.Contains("34"))
                    {
                        GenericaDesktop.ShowAlerta("Usuário sem permissão de operar esta tela!");
                        this.Close();
                    }
                    btnImprimir1.Enabled = Sessao.permissoes.Contains("35");
                    btnEncerrar.Enabled = Sessao.permissoes.Contains("37");
                    btnEntrada.Enabled = Sessao.permissoes.Contains("38");
                    btnExportarExcel.Enabled = Sessao.permissoes.Contains("39");
                    btnExportarPDF.Enabled = Sessao.permissoes.Contains("39");
                    btnGerarNFCe.Enabled = Sessao.permissoes.Contains("40");
                    btnGerarNFe.Enabled = Sessao.permissoes.Contains("41");
                }

                passou = true;
            }
        }

        private void btnPesquisaDependente_Click(object sender, EventArgs e)
        {
            txtCodDependente.Texts = "";
            txtDependente.Texts = "";
            pesquisaDependente(false);
        }

        private void txtDependente_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
                if (!Sessao.permissoes.Contains("31"))
                {
                    e.Cancel = true; // Isso impede o evento padrão de edição se a permissão "2" não estiver presente
                    GenericaDesktop.ShowAlerta("Usuário sem Permissão para essa operação!");
                }
                else
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    if (ordemServico.Status.Equals("ENCERRADA"))
                    {
                        if (Sessao.permissoes.Contains("33"))
                        {
                            editarCadastro(ordemServico);
                            btnPesquisar.PerformClick();
                        }
                    }
                    else
                    {
                        editarCadastro(ordemServico);
                        btnPesquisar.PerformClick();
                    }
                }
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
                    //txtCodCliente.Texts = "";
                    //txtCliente.Texts = "";
                    //txtCodDependente.Texts = "";
                    //txtDependente.Texts = "";
                    //txtNumeroOS.Texts = ordemServico.Id.ToString();
                    //pesquisarOrdemServicoPeloID();
                    //txtNumeroOS.Texts = "";
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
        private void imprimirOSComViasDiferentes()
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                FrmImprimirViasOtica frmImpressaoOrdemServico = new FrmImprimirViasOtica(ordemServico);
                frmImpressaoOrdemServico.ShowDialog();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Clique primeiro na Ordem de Serviço que deseja imprimir");
            }
        }
        private void imprimirOS()
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
                if (grid.SelectedItems.Count == 1)
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    if (!ordemServico.Status.Equals("ENCERRADA"))
                    {
                        Form formBackground = new Form();
                        IList<ContaReceber> listaReceber = new List<ContaReceber>();
                        IList<ContaPagar> listaPagar = new List<ContaPagar>();
                        IList<OrdemServico> listaOs = new List<OrdemServico>();
                        FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "ORDEMSERVICO", false, true, listaOs);
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
                        txtNumeroOS.Texts = ordemServico.Id.ToString();
                        pesquisarOrdemServicoPeloID();
                        txtNumeroOS.Texts = "";
                        // pesquisarOrdemServico();
                    }
                    else
                        GenericaDesktop.ShowAlerta("Ordem de Serviço já está encerrada!");
                }
                else if (grid.SelectedItems.Count > 1)
                {
                    string client = "";
                    string idCliente = "";
                    IList<OrdemServico> listaOS = new List<OrdemServico>();
                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var ord = selectedItem as OrdemServico;
                        listaOS.Add(ord);
                        client = ord.Cliente.RazaoSocial;
                        idCliente = ord.Cliente.Id.ToString();
                    }
                    Form formBackground = new Form();
                    IList<ContaReceber> listaReceber = new List<ContaReceber>();
                    IList<ContaPagar> listaPagar = new List<ContaPagar>();
                    //IList<OrdemServico> listaOs = new List<OrdemServico>();
                    ordemServico = new OrdemServico();
                    FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "ORDEMSERVICO", false, true, listaOS);
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
                    txtCliente.Texts = client;
                    txtCodCliente.Texts = idCliente;
                    radioEncerradas.Checked = true;
                    txtNumeroOS.Texts = "";
                    pesquisarOrdemServico();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na ordem que deseja encerrar!");
        }

        private void btnGerarNFCe_Click(object sender, EventArgs e)
        {  
            ParametroSistema param = new ParametroSistema();
            param.Id = 1;
            param = (ParametroSistema)Controller.getInstance().selecionar(param);
            Sessao.parametroSistema = param;

            Pessoa cliSel = new Pessoa();
            nfe = new Nfe();
            nfe.Modelo = "65";
            valorFinalNota = 0;
            valorDescontoProdutos = 0;
            valorProdutosSemDesconto = 0;
            ordemServico = new OrdemServico();
            ordemServico = (OrdemServico)grid.SelectedItem;
            if (ordemServico.Observacoes != "IMPORTADO ULTRA")
            {
                if (ordemServico.Status.Equals("ENCERRADA"))
                {
                    if (ordemServico.Nfe == null)
                    {
                        //if (GenericaDesktop.ShowConfirmacao("Gerar NFC-e dos produtos da O.S " + ordemServico.Id.ToString() + "?"))
                        //{
                        
                            //se nao tem cliente ja vem validado
                            bool validaCliente = true;
                            //enviarNFCe();
                            Pessoa cli = new Pessoa();
                            if (ordemServico.Cliente != null)
                            {
                                cli = ordemServico.Cliente;
                                validaCliente = validarClienteNFCe(cli);
                            }
                            if (validaCliente == false && GenericaDesktop.ShowConfirmacao("Deseja emitir a nota sem identificar o consumidor?"))
                            {
                                cli = null;
                                validaCliente = true;
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
                                        if (produtoNegativo == true)
                                        {
                                            if (!GenericaDesktop.ShowConfirmacao("Existe produto sem estoque fiscal, deseja continuar assim mesmo?"))
                                                throw new Exception("Emissão cancelada por \"Erro de Estoque\"");
                                        }
                                        //Concluir a O.S antes de gerar a nota
                                        numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFCe;
                                        NfeController nfeController = new NfeController();
                                        Nfe notaTeste = nfeController.selecionarNFCePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFCe);
                                        if (notaTeste != null)
                                        {
                                            if (notaTeste.Id > 0)
                                            {
                                                int novoNumero = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFCe) + 1);
                                                numeroNFCe = novoNumero.ToString();
                                                Sessao.parametroSistema.ProximoNumeroNFCe = numeroNFCe;
                                                Controller.getInstance().salvar(Sessao.parametroSistema);
                                                Logger logger = new Logger();
                                                logger.WriteLog("Nota " + notaTeste.NNf + " já existe, verifique a numeração de notas! Novo nº que será feito o teste: " + numeroNFCe, "Log");
                                                btnGerarNFCe.PerformClick();
                                                //GenericaDesktop.ShowAlerta("Nota " + notaTeste.NNf + " já existe, verifique a numeração de notas!");
                                            }
                                        }
                                        else // Emite nova nota
                                        {
                                            atualizarProximoNumeroNota();

                                        //GeradorChave geradorChave = new GeradorChave();
                                        //nfe.Chave = geradorChave.GerarChaveAcesso(31, DateTime.Now, Sessao.empresaFilialLogada.Cnpj, "65", Sessao.parametroSistema.SerieNFCe, int.Parse(numeroNFCe));
                                      
                                        try { xmlStrEnvio = emitirNFCe.gerarXMLNfce(valorProdutosSemDesconto, valorFinalNota, valorDescontoProdutos, numeroNFCe, listaProdutosNFe, cli, null, ordemServico, null, nfe.Chave); } catch (Exception err) { GenericaDesktop.ShowAlerta(err.Message); }

                                        //gravarXMLNaPasta(xmlStrEnvio, nfe.NNf, @"C:\Lunar\Unimake\UniNFe\28145398000173\Envio", nfe.Chave + "-nfe.xml", false);
                                        gravarXMLNaPasta(xmlStrEnvio, nfe.NNf, @"C:\XML", nfe.Chave + "-nfe.xml", false);
                                        //se nota foi emitida sem identificar o cliente o sistema apos emitir seta o cliente na o.s
                                        if (!String.IsNullOrEmpty(xmlStrEnvio))
                                            {
                                                enviarXMLNFCeParaApi(xmlStrEnvio);
                                            }
                                            
                                        }
                                    }
                                }
                                catch (Exception erro)
                                {
                                    ordemServico.Nfe = null;
                                    Controller.getInstance().salvar(ordemServico);
                                    GenericaDesktop.ShowErro(erro.Message);
                                }
                            }
                        //}
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Ordem de Serviço já possui NF gerada!");
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Para gerar o documento fiscal a Ordem de Serviço deve ta encerrada/faturada!");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Não é possível gerar nota de ordem de serviço gerada pelo outro sistema!");
            }
        }

        private void atualizarProximoNumeroNota()
        {
            try
            {
                //ATUALIZA NUMERO DA NOTA 
                ParametroSistema param = new ParametroSistema();
                param = Sessao.parametroSistema;

                if (nfe.Modelo.Equals("65"))
                    param.ProximoNumeroNFCe = (int.Parse(numeroNFCe) + 1).ToString();
                if (nfe.Modelo.Equals("55"))
                    param.ProximoNumeroNFe = (int.Parse(nfe.NNf) + 1).ToString();
                Controller.getInstance().salvar(param);
                Sessao.parametroSistema = param;
            }
            catch
            {

            }
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
                    nfe.Lancada = true;
                    Controller.getInstance().salvar(nfe);

                    //selecionar O.S na tela com o numero da NF! 
                    limpar();
                    txtNumeroOS.Texts = ordemServico.Id.ToString();
                    radioTodas.Checked = true;
                    btnPesquisar.PerformClick();
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
                        System.Diagnostics.Process.Start(caminhoXML + nfe.Chave + "-procNFe.pdf");
                        //FrmPDF frmPDF = new FrmPDF(caminhoXML + nfe.Chave + "-procNFe.pdf");
                        //frmPDF.ShowDialog();
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
            if (retornoNFCe.motivo.Contains("Duplicidade de NF-e com diferença na Chave"))
            {
                //nfe.NNf = (int.Parse(nfe.NNf) + 1).ToString();
                nfe.Status = retornoNFCe.motivo;
                nfe.CodStatus = retornoNFCe.cStat;
                NfeStatus nfeStatus = new NfeStatus();
                nfeStatus.Id = 2;
                nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                if (nfeStatus != null)
                    nfe.NfeStatus = nfeStatus;

                Controller.getInstance().salvar(nfe);
                GenericaDesktop.ShowAlerta("Duplicidade de NF-e com diferença na Chave, reenvie a nota no painel de monitoramento fiscal");
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
                double quantidadeMedida = 1;
                if (VendaItens.ProdutoGrade != null)
                {
                    quantidadeMedida = VendaItens.ProdutoGrade.QuantidadeMedida;
                    quantidade = VendaItens.Quantidade * VendaItens.ProdutoGrade.QuantidadeMedida;
                }
                decimal descontoItem = VendaItens.Desconto;
                produto.ValorVenda = VendaItens.ValorUnitario;

                decimal valorUnit = (produto.ValorVenda * decimal.Parse(VendaItens.Quantidade.ToString())) / (decimal.Parse(quantidadeMedida.ToString()) * decimal.Parse(VendaItens.Quantidade.ToString()));

                nfeProduto.Item = i.ToString();
                nfeProduto.Produto = produto;
                nfeProduto.QCom = quantidade.ToString();
                nfeProduto.Ncm = produto.Ncm;
                nfeProduto.Cest = produto.Cest;
                nfeProduto.Cfop = produto.CfopVenda;
                nfeProduto.CProd = produto.Id.ToString();
                nfeProduto.Nfe = null;
                nfeProduto.VProd = VendaItens.ValorUnitario * decimal.Parse(VendaItens.Quantidade.ToString());
                nfeProduto.QTrib = quantidade;
                nfeProduto.VDesc = descontoItem;
                nfeProduto.DescricaoInterna = produto.Descricao.Trim();
                nfeProduto.XProd = produto.Descricao.Trim();
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
                nfeProduto.ValorFinal = (produto.ValorVenda * decimal.Parse(VendaItens.Quantidade.ToString())) - descontoItem;
                nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                nfeProduto.ValorIpi = 0;
                nfeProduto.ValorPis = 0;
                nfeProduto.ValorProduto = valorUnit;
                nfeProduto.VUnCom = valorUnit;

                valorFinalNota += nfeProduto.ValorFinal;
                valorProdutosSemDesconto += nfeProduto.ValorProduto * decimal.Parse(quantidade.ToString());
                valorDescontoProdutos = valorDescontoProdutos + nfeProduto.VDesc;
                if(produto.Estoque < VendaItens.Quantidade * quantidadeMedida)
                {
                    produtoNegativo = true;
                }
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
                return false;
            }
            //else if (pessoa.Cnpj.Length == 14)
            //{
            //    GenericaDesktop.ShowAlerta("Em uma NFCe o cliente não pode ser pessoa jurídica, caso precise identificar a pessoa jurídica faça a emissão de uma NFe modelo 55");
            //    validacao = false;
            //    return false;
            //}
            if (!String.IsNullOrEmpty(pessoa.RazaoSocial))
                validacao = true;
            if (pessoa.EnderecoPrincipal == null)
            {
                GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo");
                validacao = false;
                return false;
            }
            if (pessoa.EnderecoPrincipal != null)
            {
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Logradouro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NOME DA RUA)");
                    validacao = false;
                    return false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (BAIRRO)");
                    validacao = false;
                    return false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Numero))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NUMERO)");
                    validacao = false;
                    return false;
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Cep))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (CEP)");
                    validacao = false;
                    return false;
                }
            }
            return validacao;
        }

        private void btnGerarNFe_Click(object sender, EventArgs e)
        {
            ParametroSistema param = new ParametroSistema();
            param.Id = 1;
            param = (ParametroSistema)Controller.getInstance().selecionar(param);
            Sessao.parametroSistema = param;

            valorFinalNota = 0;
            valorDescontoProdutos = 0;
            valorProdutosSemDesconto = 0;
            ordemServico = new OrdemServico();
            ordemServico = (OrdemServico)grid.SelectedItem;
            //if (ordemServico.Observacoes != "IMPORTADO ULTRA")
            //{
                if (ordemServico.Status.Equals("ENCERRADA"))
                {
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
                                            Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(numeroNFCe) + 1).ToString();
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
                else
                    GenericaDesktop.ShowAlerta("Para gerar o documento fiscal a Ordem de Serviço deve ta encerrada/faturada!");
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
                    nfe.Lancada = true;
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
                            GenericaDesktop.ShowAlerta("O.S Já foi encerrada e gerado faturas a receber, será excluído as parcelas do contas a receber!");
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

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                if (ordemServico.Status == "ABERTA")
                {
                    Form formBackground = new Form();
                    FrmEntradaValor uu = new FrmEntradaValor(ordemServico);
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
                }
                else
                    GenericaDesktop.ShowAlerta("Função válida apenas para O.S em aberto!");
            }
        }

        private void btnImprimir1_Click(object sender, EventArgs e)
        {
            if (Sessao.parametroSistema.ViaImpressaoOs == true)
            {
                imprimirOSComViasDiferentes();
            }
            else
                imprimirOS();
        }

        private void btnImprimir1_DropDowItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string btnClicado = e.ClickedItem.Text;
            if (btnClicado.Equals("Imprimir Duplicata(s)"))
            {
                if (grid.SelectedIndex >= 0)
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    ContaReceberController contaReceberController = new ContaReceberController();
                    if (ordemServico != null)
                    {
                        IList<ContaReceber> lis = contaReceberController.selecionarContaReceberPorSql("From ContaReceber as Tabela Where Tabela.OrdemServico = " + ordemServico.Id + " and Tabela.FlagExcluido <> True");
                        if (lis.Count > 0)
                        {
                            FrmImprimirDuplicata frDup = new FrmImprimirDuplicata(ordemServico.Cliente, lis);
                            frDup.ShowDialog();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Não foi encontrado duplicata(s) para essa ordem de serviço!");
                        
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Clique primeiro na Ordem de Serviço que deseja imprimir as duplicatas");
                }
            }
            else if (btnClicado.Equals("Imprimir Nota Fiscal"))
            {
                imprimirNF();
            }
            else if (btnClicado.Equals("Imprimir Nº O.S"))
            {
                if (grid.SelectedIndex >= 0)
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    FrmImprimirNumeroOS frm = new FrmImprimirNumeroOS(ordemServico.Id.ToString() + " ", ordemServico.Cliente.RazaoSocial);
                    frm.ShowDialog();
                }
                else
                    GenericaDesktop.ShowAlerta("Clique primeiro na linha da O.S que deseja imprimir!");
            }
            else if (btnClicado.Equals("Imprimir Duplicata Gráfica"))
            {
                if (grid.SelectedIndex >= 0)
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    ContaReceberController contaReceberController = new ContaReceberController();
                    if (ordemServico != null)
                    {
                        IList<ContaReceber> lis = contaReceberController.selecionarContaReceberPorSql("From ContaReceber as Tabela Where Tabela.OrdemServico = " + ordemServico.Id + " and Tabela.FlagExcluido <> True");
                        if (lis.Count > 0)
                        {
                            FrmImprimirDuplicataDaGrafica frDup = new FrmImprimirDuplicataDaGrafica(ordemServico.Cliente, lis);
                            frDup.ShowDialog();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Não foi encontrado duplicata(s) para essa ordem de serviço!");

                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Clique primeiro na Ordem de Serviço que deseja imprimir as duplicatas");
                }
            }
        }

        private void imprimirNF()
        {
            try
            {
                if (grid.RowCount > 0 && grid.SelectedItem != null)
                {
                    ordemServico = new OrdemServico();
                    ordemServico = (OrdemServico)grid.SelectedItem;
                    if (ordemServico.Nfe != null)
                    {
                        nfe = ordemServico.Nfe;
                        if (nfe.Status.Contains("Autorizado o uso"))
                        {
                            if (nfe.Modelo.Equals("65"))
                            {
                                if (File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf"))
                                {
                                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf");
                                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf");
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
                                            gerarPDF2(nFCeDownloadProc.pdf, nfe.Chave, true);
                                        }
                                    }
                                }
                                if (nfe.NfeStatus.Id == 6)
                                {
                                    if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                                    {
                                        string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + nfe.DataCadastro.Year + @"\" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\" + nfe.DataCadastro.Day.ToString().PadLeft(2, '0') + @"\" + nfe.Chave + ".pdf";
                                        if (File.Exists(caminhoPDF))
                                        {
                                            //System.Diagnostics.Process.Start(caminhoPDF);
                                            FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                                            frmPDF.ShowDialog();
                                        }
                                    }
                                }
                    
                            }
                            if (nfe.Modelo.Equals("55"))
                            {
                                if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                                {
                                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                    frmPDF.ShowDialog();
                                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                }
                                else
                                {
                                    NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                                    nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                    if (nFeDownloadProc55 != null)
                                    {
                                        gerarPDF2(nFeDownloadProc55.pdf, nfe.Chave, true);
                                    }
                                }
                            }
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nota Fiscal não está autorizada, verifique o erro da nota na tela de Monitoramento Fiscal");
                    }
                    else
                        GenericaDesktop.ShowAlerta("O.S sem nota vinculada!");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta("Falha ao imprimir nota. " + erro.Message);
            } 
        }

        private void gerarPDF2(String pdf, String chave, bool imprimir)
        {
            if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
            {
                if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    frmPDF.ShowDialog();
                }

            }
            else if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }

        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true "))
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
                                txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                vendedor = ((Pessoa)pessoaOjeto);
                                btnPesquisar.PerformClick();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            vendedor = ((Pessoa)pessoaOjeto);
                            btnPesquisar.PerformClick();
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

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            txtCodVendedor.Texts = "";
            txtVendedor.Texts = "";
            vendedor = new Pessoa();
            pesquisaVendedor();
        }

        private void txtVendedor_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaVendedor();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void limpar()
        {
            grid.DataSource = null;
            sfDataPager1.DataSource = null;
            grid.Refresh();
            txtCliente.Texts = "";
            txtCodCliente.Texts = "";
            chkAtivarDataAbertura.Checked = false;
            chkAtivarDataEncerramento.Checked = false;
            radioAbertas.Checked = true;
            txtNumeroOS.Texts = "";
            txtCodVendedor.Texts = "";
            txtDataEntregaInicial.Text = "";
            txtDataEntregaFinal.Text = "";
            txtVendedor.Texts = "";
            txtCodDependente.Texts = "";
            txtDependente.Texts = "";
            txtRegistroPorPagina.Texts = "100";
            txtCliente.Focus();
            grid.SelectionMode = GridSelectionMode.Single;
        }

        private void preencherSumario()
        {
            GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Name = "TableSummary";
            tableSummaryRow1.ShowSummaryInRow = true;
            tableSummaryRow1.TitleColumnCount = 3;
            String vendedorSelecionado = "";
            String dataSelecionada1 = "";
            String dataSelecionada2 = "";
            if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
                vendedorSelecionado = txtVendedor.Texts;
            if (chkAtivarDataAbertura.Checked == true)
            {
                dataSelecionada1 = DateTime.Parse(txtDataAberturaInicial.Value.ToString()).ToShortDateString();
                dataSelecionada2 = " A " + DateTime.Parse(txtDataAberturaFinal.Value.ToString()).ToShortDateString();
            }
            tableSummaryRow1.Title = " QTD O.S: {TotalNotas}         Total: {ValorTotalSemJuro}       " + vendedorSelecionado + "    " + dataSelecionada1 + dataSelecionada2;
            tableSummaryRow1.Position = VerticalPosition.Bottom;
            //tableSummaryRow1.CalculationUnit = Syncfusion.Data.SummaryCalculationUnit.AllRows;

            GridSummaryColumn summaryColumn1 = new GridSummaryColumn();
            summaryColumn1.Name = "TotalNotas";
            summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn1.Format = "{Count}";
            summaryColumn1.MappingName = "Id";

            GridSummaryColumn summaryColumn2 = new GridSummaryColumn();
            summaryColumn2.Name = "ValorTotalSemJuro";
            summaryColumn2.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn2.Format = "{Sum:c}";
            summaryColumn2.MappingName = "ValorTotal";


            this.grid.TableSummaryRows.Clear();
            tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
            tableSummaryRow1.SummaryColumns.Add(summaryColumn2);
            this.grid.TableSummaryRows.Add(tableSummaryRow1);
            this.grid.Style.TableSummaryRowStyle.Font = new GridFontInfo(new Font("Microsoft Sans Serif", 12f, FontStyle.Regular));
            //this.grid.TableSummaryRows.Add(tableSummaryRow2);
            //if(chkAtivarDataAbertura.Checked ==  true)
            //    this.grid.TableSummaryRows.Add(tableSummaryRow3);

            this.grid.Style.TableSummaryRowStyle.BackColor = Color.LightSkyBlue;
            this.grid.Style.TableSummaryRowStyle.Borders.All = new GridBorder(Color.Black, GridBorderWeight.Medium);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            options.ExportAllPages = true;
            options.ExcludeColumns.Add("Entrada");
            options.ExcludeColumns.Add("Nfe.NNf");
            options.ExcludeColumns.Add("Nfe.Modelo");
            options.ExcludeColumns.Add("ValorProduto");
            options.ExcludeColumns.Add("ValoServico");
            options.DefaultColumnWidth = 12;
          
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            workBook.Worksheets[0].UsedRange.BorderInside(ExcelLineStyle.Hair, ExcelKnownColors.Black);
            workBook.Worksheets[0].UsedRange.BorderAround(ExcelLineStyle.Hair, ExcelKnownColors.Black);
          
            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "OrdensDeServiço",
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.grid, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            //options.ExportAllPages = true;
            options.ExcludeColumns.Add("Entrada");
            options.ExcludeColumns.Add("Nfe.NNf");
            options.ExcludeColumns.Add("Nfe.Modelo");
            options.ExcludeColumns.Add("ValorProduto");
            options.ExcludeColumns.Add("ValorServico");
            options.ExcludeColumns.Add("OperadorCadastro");
            options.ExcludeColumns.Add("Cliente.Id");
            options.ExcludeColumns.Add("OperadorEncerramento");
            options.AutoColumnWidth = true;
            options.FitAllColumnsInOnePage = true;

            int qtdPaginas = sfDataPager1.PageCount;
            if (qtdPaginas > 1)
            {
                if (GenericaDesktop.ShowConfirmacao("Você possui " + qtdPaginas + " páginas na pesquisa, deseja imprimir todas?"))
                {
                    txtRegistroPorPagina.Texts = (qtdPaginas * int.Parse(txtRegistroPorPagina.Texts)).ToString();
                    btnPesquisar.PerformClick();
                    btnExportarPDF.PerformClick();
                }
                else
                {
                    salvarPDF(options);
                }
            }
            else
            {
                salvarPDF(options);
            }
        }

        private void salvarPDF(PdfExportingOptions options)
        {
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Portrait;
            var page = document.Pages.Add();
            var PDFGrid = grid.ExportToPdfGrid(grid.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == grid.Columns[0].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "OrdensDeServiço",
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    document.Save(stream);
                }
                //Message box confirmation to view the created Pdf file.
                if (MessageBox.Show("Deseja abrir o arquivo Pdf?", "Pdf criado com sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the Pdf file using the default Application.
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void FrmOrdemServicoLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F2:
                    abrirHistorico();
                    break;
            }
        }

        private void abrirHistorico()
        {
            if (grid.SelectedIndex >= 0)
            {
                OrdemServico ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                if (ordemServico.Status.Equals("ENCERRADA"))
                {
                    Form formBackground = new Form();
                    try
                    {
                        //FrmImprimirEtiquetasOtica frm = new FrmImprimirEtiquetasOtica();

                        FrmHistoricoOS uu = new FrmHistoricoOS(ordemServico);
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
                else
                    GenericaDesktop.ShowAlerta("É possível ver histórico de pagamento apenas de Ordem de Serviço encerrada!");
            }
        }

        private async void btnEnviarWhats_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                ordemServico = new OrdemServico();
                ordemServico = (OrdemServico)grid.SelectedItem;
                string telefoneCompleto = "";
                if (ordemServico.Cliente.PessoaTelefone != null)
                {
                    telefoneCompleto = EnviarMensagemWhatsapp.TratarTelefone(ordemServico.Cliente.PessoaTelefone.Ddd, ordemServico.Cliente.PessoaTelefone.Telefone);
                }
                FrmEnvioMensagem frmEnvioMensagem = new FrmEnvioMensagem(telefoneCompleto, capturarNomeParaMensagem(ordemServico.Cliente.RazaoSocial));
                if (frmEnvioMensagem.ShowDialog() == DialogResult.OK)
                {
                    string escolha = frmEnvioMensagem.GetEscolha();
                    string numero = frmEnvioMensagem.GetTelefone();
                    string nome = frmEnvioMensagem.GetNome();
                    string mensagem = frmEnvioMensagem.GetMensagem();
                    //Ajusta nome
                    if (mensagem.Contains("[nome]"))
                    {
                        mensagem = mensagem.Replace("[nome]", capturarNomeParaMensagem(nome));
                    }
                    if (escolha.Equals("Envio PDF"))
                    {
                        await enviarPDfPeloWhats(ordemServico, numero, nome, mensagem);
                    }
                    else if (escolha.Equals("Envio Técnico"))
                    {
                        await enviarMensagemPeloWhats(ordemServico, numero, nome, mensagem);
                    }
                    else
                    {
                        await enviarMensagemPeloWhats(ordemServico, numero, nome, mensagem);
                    }
                }
            }
        }

        private async Task enviarPDfPeloWhats(OrdemServico ordem, string numero, string nome, string mensagem)
        {
            string caminhoPDF = "";

            if (grid.SelectedIndex >= 0)
            {
                FrmImpressaoOrdemServico frmImpressaoOrdemServico = new FrmImpressaoOrdemServico(ordem);
                caminhoPDF = frmImpressaoOrdemServico.GerarPDF(ordem);
            }

            if (!String.IsNullOrEmpty(caminhoPDF))
            {
                var client = new EnviarMensagemWhatsapp();
                await client.SendMessageAsync(numero, mensagem);
                await client.SendMediaMessageAsync(numero, caminhoPDF); 
            }
        }

        private async Task enviarMensagemPeloWhats(OrdemServico ordem, string numero, string nome, string mensagem)
        {
            if (grid.SelectedIndex >= 0)
            {
                var client = new EnviarMensagemWhatsapp();
                await client.SendMessageAsync(numero, mensagem);
            }
        }

        private string capturarNomeParaMensagem(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
            {
                return nomeCompleto;
            }

            string[] partes = nomeCompleto.Split(' ');
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            string nomeFormatado = "";

            if (partes.Length >= 2)
            {
                string primeiroNomeFormatado = textInfo.ToTitleCase(partes[0].ToLower());
                string segundoNomeFormatado = textInfo.ToTitleCase(partes[1].ToLower());
                nomeFormatado = $"{primeiroNomeFormatado} {segundoNomeFormatado}";
            }
            else
            {
                nomeFormatado = textInfo.ToTitleCase(nomeCompleto.ToLower());
            }

            return nomeFormatado;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = e.Link.LinkData as string;

            if (!string.IsNullOrEmpty(url))
            {
                // Abrir a URL no navegador padrão
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }
}
