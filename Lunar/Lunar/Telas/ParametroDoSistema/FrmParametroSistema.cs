﻿using Lunar.Telas.OrdensDeServico.TipoObjetos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.IntegracaoZAPI;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.ParametroDoSistema
{
    public partial class FrmParametroSistema : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        ParametroSistema parametro = new ParametroSistema();
        int tempoSegundo = 0;
        public FrmParametroSistema()
        {
            InitializeComponent();

            parametro.Id = 1;
            parametro = (ParametroSistema)Controller.getInstance().selecionar(parametro);
            getParametro();
        }

        private void getParametro()
        {
            txtSerieNFCe.Texts = parametro.SerieNFCe;
            txtSerieNFe.Texts = parametro.SerieNFe;
            txtProximoNFCe.Texts = parametro.ProximoNumeroNFCe;
            txtProximoNFe.Texts = parametro.ProximoNumeroNFe;

            if (parametro.AmbienteProducao == true)
                radioProducao.Checked = true;
            else
                radioHomologacao.Checked = true;
            txtCSC.Texts = parametro.CscNfce;
            txtTokenNFCe.Texts = parametro.TokenNfce;
            txtInformacaoAdicionalNFCe.Texts = parametro.InformacaoAdicionalNFCe;
            txtInformacaoAdicionalNFE.Texts = parametro.InformacaoAdicionalNFe;
            if(!String.IsNullOrEmpty(parametro.PastaRemessaNsCloud))
                txtPastaRemessaNsCloud.Texts = parametro.PastaRemessaNsCloud;
            if (parametro.AlertaEstoqueFiscal == true)
                chkAlertaEstoqueFiscal.Checked = true;
            else
                chkAlertaEstoqueFiscal.Checked = false;
            if (parametro.AlertaEstoqueGerencial == true)
                chkAlertaEstoqueGerencial.Checked = true;
            else
                chkAlertaEstoqueGerencial.Checked = false;
            txtMulta.Texts = parametro.Multa;
            txtJuro.Texts = parametro.Juro;
            if(parametro.TipoObjeto != null)
            {
                txtTipoObjeto.Texts = parametro.TipoObjeto.Descricao;
                txtCodTipoObjeto.Texts = parametro.TipoObjeto.Id.ToString();
            }
            if(parametro.IdInstanciaWhats != null)
            {
                txtIdWhatsapp.Texts = parametro.IdInstanciaWhats;
            }
            if(parametro.TokenWhats != null)
            {
                txtTokenWhatsapp.Texts = parametro.TokenWhats;
            }

            if (parametro.PlanoContaCompraImobilizado != null)
            {
                txtPlanoContaImobilizado.Texts = parametro.PlanoContaCompraImobilizado.Descricao;
                txtCodPlanoContaImobilizado.Texts = parametro.PlanoContaCompraImobilizado.Id.ToString();
            }
            if (parametro.PlanoContaCompraRevenda != null)
            {
                txtPlanoContaRevenda.Texts = parametro.PlanoContaCompraRevenda.Descricao;
                txtCodPlanoContaRevenda.Texts = parametro.PlanoContaCompraRevenda.Id.ToString();
            }
            if (parametro.PlanoContaUsoConsumo != null)
            {
                txtPlanoContaUsoConsumo.Texts = parametro.PlanoContaUsoConsumo.Descricao;
                txtCodPlanoContaUsoConsumo.Texts = parametro.PlanoContaUsoConsumo.Id.ToString();
            }
            if (parametro.PlanoContaVenda != null)
            {
                txtPlanoContaVendas.Texts = parametro.PlanoContaVenda.Descricao;
                txtCodPlanoContaVendas.Texts = parametro.PlanoContaVenda.Id.ToString();
            }
            if (parametro.Logo != null)
            {
                txtCaminhoLogo.Texts = parametro.Logo;
                if (File.Exists(parametro.Logo))
                {
                    pictureLogo.Visible = true;
                    pictureLogo.ImageLocation = parametro.Logo;
                }
            }
            if(!String.IsNullOrEmpty(parametro.LembreteVencimento))
                txtLembreteVencimento.Text = parametro.LembreteVencimento;
            txtDDDWhats.Texts = parametro.DddWhats;
            txtNumeroWhats.Texts = parametro.FoneWhats;

            txtServidorEmail.Text = parametro.ServidorEmail;
            txtPortaEmail.Text = parametro.PortaEmail;
            txtNomeEmpresaEmail.Text = parametro.NomeRemetenteEmail;
            txtEmail.Text = parametro.Email;
            if(!String.IsNullOrEmpty(parametro.SenhaEmail))
                txtSenhaEmail.Text = GenericaDesktop.Descriptografa(parametro.SenhaEmail);
            if (parametro.AutenticacaoSsl == true)
                chkAutenticacaoSSL.Checked = true;
            else
                chkAutenticacaoSSL.Checked = false;
            if (parametro.AutenticacaoTls == true)
                chkAutenticacaoTLS.Checked = true;
            else
                chkAutenticacaoTLS.Checked = false;
            if (!String.IsNullOrEmpty(parametro.CaminhoAnexo))
                txtCaminhoAnexos.Texts = parametro.CaminhoAnexo;
        }

        private void setParametro()
        {
            parametro.Id = 1;
            parametro.ProximoNumeroNFCe = txtProximoNFCe.Texts.Trim();
            parametro.ProximoNumeroNFe = txtProximoNFe.Texts.Trim();
            parametro.SerieNFCe = txtSerieNFCe.Texts.Trim();
            parametro.SerieNFe = txtSerieNFe.Texts.Trim();
            if (radioProducao.Checked == true)
                parametro.AmbienteProducao = true;
            else
                parametro.AmbienteProducao = false;
            parametro.CscNfce = txtCSC.Texts.Trim();
            parametro.TokenNfce = txtTokenNFCe.Texts.Trim();
            parametro.InformacaoAdicionalNFCe = txtInformacaoAdicionalNFCe.Texts.Trim();
            parametro.InformacaoAdicionalNFe = txtInformacaoAdicionalNFE.Texts.Trim();
            parametro.PastaRemessaNsCloud = txtPastaRemessaNsCloud.Texts.Trim();
            if (chkAlertaEstoqueFiscal.Checked == true)
                parametro.AlertaEstoqueFiscal = true;
            else
                parametro.AlertaEstoqueFiscal = false;
            if (chkAlertaEstoqueGerencial.Checked == true)
                parametro.AlertaEstoqueGerencial = true;
            else
                parametro.AlertaEstoqueGerencial = false;
            parametro.Multa = txtMulta.Texts;
            parametro.Juro = txtJuro.Texts;
            parametro.IdInstanciaWhats = txtIdWhatsapp.Texts;
            parametro.TokenWhats = txtTokenWhatsapp.Texts;
            parametro.LembreteVencimento = txtLembreteVencimento.Text;
            if (!String.IsNullOrEmpty(txtCodTipoObjeto.Texts))
            {
                TipoObjeto tipoObjeto = new TipoObjeto();
                tipoObjeto.Id = int.Parse(txtCodTipoObjeto.Texts);
                tipoObjeto = (TipoObjeto)Controller.getInstance().selecionar(tipoObjeto);
                if (tipoObjeto != null)
                    parametro.TipoObjeto = tipoObjeto;
                else
                    parametro.TipoObjeto = null;
            }

            if (!String.IsNullOrEmpty(txtCodPlanoContaImobilizado.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoContaImobilizado.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    parametro.PlanoContaCompraImobilizado = planoConta;
                else
                    parametro.PlanoContaCompraImobilizado = null;
            }
            if (!String.IsNullOrEmpty(txtCodPlanoContaRevenda.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoContaRevenda.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    parametro.PlanoContaCompraRevenda = planoConta;
                else
                    parametro.PlanoContaCompraRevenda = null;
            }
            if (!String.IsNullOrEmpty(txtCodPlanoContaUsoConsumo.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoContaUsoConsumo.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    parametro.PlanoContaUsoConsumo = planoConta;
                else
                    parametro.PlanoContaUsoConsumo = null;
            }
            if (!String.IsNullOrEmpty(txtCodPlanoContaVendas.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoContaVendas.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    parametro.PlanoContaVenda = planoConta;
                else
                    parametro.PlanoContaVenda = null;
            }
            parametro.Logo = txtCaminhoLogo.Texts;
            parametro.DddWhats = txtDDDWhats.Texts;
            parametro.FoneWhats = txtNumeroWhats.Texts;

            parametro.ServidorEmail = txtServidorEmail.Text;
            parametro.PortaEmail = txtPortaEmail.Text;
            parametro.NomeRemetenteEmail = txtNomeEmpresaEmail.Text;
            parametro.Email = txtEmail.Text;
            parametro.SenhaEmail = GenericaDesktop.Criptografa(txtSenhaEmail.Text);
            parametro.AutenticacaoSsl = false;
            if (chkAutenticacaoSSL.Checked == true)
                parametro.AutenticacaoSsl = true;

            parametro.AutenticacaoTls = false;
            if (chkAutenticacaoTLS.Checked == true)
                parametro.AutenticacaoTls = true;
            parametro.CaminhoAnexo = txtCaminhoAnexos.Texts;

            Sessao.parametroSistema = parametro;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                setParametro();
                Controller.getInstance().salvar(parametro);
                Sessao.parametroSistema = parametro;
                GenericaDesktop.ShowInfo("Registro salvo com Sucesso");
                this.Close();
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(GenericaDesktop.ShowConfirmacao("Deseja cancelar as modificações?"))
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmParametroSistema_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;
            }
        }

        private void txtMulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtMulta.Texts, e);
        }

        private void txtJuro_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtJuro.Texts, e);
        }

        private void pesquisaTipoObjeto()
        {
            Object objeto = new TipoObjeto();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("TipoObjeto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + txtTipoObjeto.Texts + "%'"))
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
                    switch (uu.showModal("TipoObjeto", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmTipoObjetoCadastro form = new FrmTipoObjetoCadastro();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtTipoObjeto.Texts = ((TipoObjeto)objeto).Descricao;
                                txtCodTipoObjeto.Texts = ((TipoObjeto)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtTipoObjeto.Texts = ((TipoObjeto)objeto).Descricao;
                            txtCodTipoObjeto.Texts = ((TipoObjeto)objeto).Id.ToString();
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

        private void btnPesquisaTipoObjeto_Click(object sender, EventArgs e)
        {
            txtTipoObjeto.Texts = "";
            txtCodTipoObjeto.Texts = "";
            pesquisaTipoObjeto();
        }

        private void txtCodTipoObjeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodTipoObjeto.Texts))
                    {
                        TipoObjeto tipoObjeto = new TipoObjeto();
                        tipoObjeto.Id = int.Parse(txtCodTipoObjeto.Texts);
                        tipoObjeto = (TipoObjeto)Controller.getInstance().selecionar(tipoObjeto);
                        if (tipoObjeto != null)
                        {
                            txtTipoObjeto.Texts = tipoObjeto.Descricao;
                            txtCodTipoObjeto.Texts = tipoObjeto.Id.ToString();
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Tipo de objeto não encontrado");
                            txtTipoObjeto.Texts = "";
                            txtCodTipoObjeto.Texts = "";
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtTipoObjeto.Texts = "";
                    txtCodTipoObjeto.Texts = "";
                    GenericaDesktop.ShowErro(erro.Message);
                }
            }
        }

        private void txtTipoObjeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaTipoObjeto();
            }
        }

        private void btnGerarQr_Click(object sender, EventArgs e)
        {
     
        }

        private void InitializeTimer()
        {
            tempoSegundo = 0;
            timer1.Enabled = true;
            //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tempoSegundo >= 7)
            {
                // Exit loop code.  
                timer1.Enabled = false;
                tempoSegundo = 0;
                gerarQrCodeWhatsapp();
            }
            else
            {
                tempoSegundo++;
                //label1.Text = "Segundos: " + tempoSegundo.ToString();
            }
        }

        private void btnGerarQr_Click_1(object sender, EventArgs e)
        {

        }

        private void chkAceitoContratoWhats_CheckStateChanged(object sender, EventArgs e)
        {
            if(chkAceitoContratoWhats.Checked == true)
            {
                if (!String.IsNullOrEmpty(txtTokenWhatsapp.Texts) && !String.IsNullOrEmpty(txtIdWhatsapp.Texts))
                {
                    picQRCode.Enabled = true;
                    picQRCode.Visible = true;
                    
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Antes de iniciar o uso você precisa dos dados de acesso, token e id (Solicite seu revendedor)");
                }
            }
            else
            {
                picQRCode.Enabled = false;
                picQRCode.Visible = false;
            }
        }

        private void gerarQrCodeWhatsapp()
        {
            try
            {
                if (!string.IsNullOrEmpty(Sessao.parametroSistema.IdInstanciaWhats) && !String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
                {
                    picQRCode.Visible = true;
                    Zapi zap = new Zapi();
                    string conexao = zap.zapi_VerificarConexaoInstancia_ParaGerarQR(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                    if (conexao.Equals("False"))
                    {
                        string retorno = zap.zapi_RetornaQRCodeConexao(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                        if (!String.IsNullOrEmpty(retorno))
                        {
                            picQRCode.Image = zap.GerarQRCode(190, 190, retorno);
                        }
                        InitializeTimer();
                    }
                    else
                        GenericaDesktop.ShowInfo("Número conectado");
                }
                else
                {
                    GenericaDesktop.ShowAlerta("ID e Token não configurado!");
                }
            }
            catch
            {

            }
        }

        private void zapi_restaurarSessao()
        {

        }

        private void btnConfirmaNumeroWhatsapp_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtDDDWhats.Texts) && !String.IsNullOrEmpty(txtNumeroWhats.Texts) && !String.IsNullOrEmpty(txtTokenWhatsapp.Texts) && !String.IsNullOrEmpty(txtIdWhatsapp.Texts))
            {
                if (txtDDDWhats.Texts.Length != 2)
                {
                    GenericaDesktop.ShowAlerta("Preencha o campo DDD com 2 dígitos");
                }
                else if (txtNumeroWhats.Texts.Length < 8 || txtNumeroWhats.Texts.Length > 9)
                {
                    GenericaDesktop.ShowAlerta("Preencha o campo Número do Whatsapp corretamente!");
                }
                else
                {
                    chkAceitoContratoWhats.Enabled = true;
                    chkEnvioAgradecimentoCompraWhats.Enabled = true;
                    chkEnvioAniversarioWhats.Enabled = true;
                    chkEnvioBoletoWhats.Enabled = true;
                    chkEnvioNFCeNFeWhats.Enabled = true;
                    setParametro();
                }
            }
            else
            {
                chkAceitoContratoWhats.Enabled = false;
                GenericaDesktop.ShowAlerta("Preencha o campo DDD, Número do Whatsapp, token e ID corretamente!");
            }
        }

        private void txtNumeroWhats_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtNumeroWhats.Texts, e);
        }

        private void txtDDDWhats_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtDDDWhats.Texts, e);
        }

        private void btnPesquisaPlanoContaVenda_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
                {
                    txtCodPlanoContaVendas.Texts = "";
                    txtPlanoContaVendas.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoContaVendas.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoContaVendas.Texts = ((PlanoConta)objeto).Id.ToString();
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

        private void btnPesquisaPlanoContaRevenda_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
                {
                    txtCodPlanoContaRevenda.Texts = "";
                    txtPlanoContaRevenda.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoContaRevenda.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoContaRevenda.Texts = ((PlanoConta)objeto).Id.ToString();
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

        private void btnPesquisaPlanoContaUsoConsumo_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
                {
                    txtCodPlanoContaUsoConsumo.Texts = "";
                    txtPlanoContaUsoConsumo.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoContaUsoConsumo.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoContaUsoConsumo.Texts = ((PlanoConta)objeto).Id.ToString();
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

        private void btnPesquisaPlanoContaImobilizado_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
                {
                    txtCodPlanoContaImobilizado.Texts = "";
                    txtPlanoContaImobilizado.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoContaImobilizado.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoContaImobilizado.Texts = ((PlanoConta)objeto).Id.ToString();
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

        private void btnPesquisaLogo_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Images (*.BMP;*.JPEG;*.JPG;*.GIF,*.PNG,*.TIFF,*.JFIF)|*.BMP;*.JPEG;*.JPG;*.GIF;*.PNG;*.TIFF;*.JFIF|" + "All files (*.*)|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureLogo.Visible = true;
                pictureLogo.ImageLocation = file.FileName;
                txtCaminhoLogo.Texts = file.FileName;
                parametro.Logo = txtCaminhoLogo.Text;
            }
        }

        private void btnGerarQR_Click_2(object sender, EventArgs e)
        {
            gerarQrCodeWhatsapp();
        }

        private void btnRestaurarSessaoWhats_Click(object sender, EventArgs e)
        {
            Zapi zapi = new Zapi();
            string ret = zapi.zapi_RestaurarSessao(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
            if (ret.Equals("True"))
            {
                GenericaDesktop.ShowInfo("Sessão restaurada com sucesso!");
            }
        }

        private void btnVerificarConexaoWhats_Click(object sender, EventArgs e)
        {
            Zapi zapi = new Zapi();
            zapi.zapi_VerificarConexaoInstancia(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
        }

        private void btnDesconectarWhats_Click(object sender, EventArgs e)
        {
            Zapi zapi = new Zapi();
            zapi.zapi_DesconectarInstancia(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnTesteEmail_Click(object sender, EventArgs e)
        {
            setParametro();
            Controller.getInstance().salvar(parametro);
            Sessao.parametroSistema = parametro;
            
            if(generica.enviarEmail(parametro.Email, "Teste de Email", Sessao.empresaFilialLogada.NomeFantasia, "CNPJ: " + Sessao.empresaFilialLogada.Cnpj + " >>>>>>" + "Seu teste foi realizado com sucesso", null))
                GenericaDesktop.ShowInfo("Conexão OK\n\nFoi enviado um email de teste para o seu email " + parametro.Email);        
            
        }

        private void btnCaminhoAnexos_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtCaminhoAnexos.Texts = folder.SelectedPath;
            }
        }
    }
}
