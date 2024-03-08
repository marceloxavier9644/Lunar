using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.OrdensDeServico.TipoObjetos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.ZAPZAP;
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

            if (String.IsNullOrEmpty(parametro.SerieNFCe))
                txtSerieNFCe.Enabled = true;
            if (String.IsNullOrEmpty(parametro.SerieNFe))
                txtSerieNFe.Enabled = true;
            if (String.IsNullOrEmpty(parametro.ProximoNumeroNFCe))
                txtProximoNFCe.Enabled = true;
            if (String.IsNullOrEmpty(parametro.ProximoNumeroNFe))
                txtProximoNFe.Enabled = true;

            if (parametro.AmbienteProducao == true)
                radioProducao.Checked = true;
            else
                radioHomologacao.Checked = true;

            if (parametro.ChequeContaReceber == true)
                chkChequeContaReceber.Checked = true;
            else
                chkChequeContaReceber.Checked = false;

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
            if(parametro.Comissao != null)
             txtComissaoPadrao.Texts = parametro.Comissao.ToString();

            if(parametro.ModeloEtiquetaPadrao != null)
                txtEtiquetaPadrao.Text = parametro.ModeloEtiquetaPadrao;
            if (parametro.ModeloEtiquetaNumeroOs != null)
                txtEtiquetaImprimirNumeroOs.Text = parametro.ModeloEtiquetaNumeroOs;
            if (parametro.TokenGalaxyPay != null)
                txtTokenGalaxyPay.Texts = parametro.TokenGalaxyPay;
            if (parametro.IdGalaxyPay != null)
                txtIdGalaxyPay.Texts = parametro.IdGalaxyPay;
            if (parametro.IntegracaoGalaxyPay == true)
                chkIntegracaoGalaxyPay.Checked = true;
            else
                chkIntegracaoGalaxyPay.Checked = false;
            if(parametro.ContaBancariaVinculadaApi != null)
            {
                txtContaBancariaVinculadaAPI.Texts = parametro.ContaBancariaVinculadaApi.Descricao;
                txtCodContaBancariaVinculadaAPI.Texts = parametro.ContaBancariaVinculadaApi.Id.ToString();
            }
            if(parametro.UsuarioWebServiceSpcBrasil != null)
            {
                txtUsuarioSPCBRASIL.Texts = parametro.UsuarioWebServiceSpcBrasil;
            }
            if(parametro.SenhaWebServiceSpcBrasil != null)
            {
                txtSenhaSpcBrasil.Texts = GenericaDesktop.Descriptografa(parametro.SenhaWebServiceSpcBrasil);
            }
            if(parametro.ConsultaPadraoSpcBrasil != null)
            {
                if (parametro.ConsultaPadraoSpcBrasil.Equals("12"))
                    radioSPC12.Checked = true;
                else if (parametro.ConsultaPadraoSpcBrasil.Equals("127"))
                    radioSPC127.Checked = true;
                else if (parametro.ConsultaPadraoSpcBrasil.Equals("128"))
                    radioSPC128.Checked = true;
            }
            if (parametro.AmbienteSpcBrasil != null)
            {
                if (parametro.AmbienteSpcBrasil.Equals("PRODUCAO"))
                {
                    chkProducaoSPC.Checked = true;
                    chkHomologacaoSPC.Checked = false;
                }
                else
                {
                    chkProducaoSPC.Checked = false;
                    chkHomologacaoSPC.Checked = true;
                }
            }

            //Whatsapp
            
            if (parametro.EnvioNotasPorWhats == true)
                chkEnvioNFCeNFeWhats.Checked = true;
            else
                chkEnvioNFCeNFeWhats.Checked = false;

            if (parametro.EnvioAgradecimentoCompra == true)
                chkEnvioAgradecimentoCompraWhats.Checked = true;
            else
                chkEnvioAgradecimentoCompraWhats.Checked = false;

            if (parametro.AtivarMensagemPosVendas == true)
                chkAtivarEnvioMensagemPosVenda.Checked = true;
            else
                chkAtivarEnvioMensagemPosVenda.Checked = false;

            if(!String.IsNullOrEmpty(parametro.MensagemPosVendasDiasOuMinutos))
                if (parametro.MensagemPosVendasDiasOuMinutos == "DIAS")
                    chkDiasPosVenda.Checked = true;
                else if (parametro.MensagemPosVendasDiasOuMinutos == "MINUTOS")
                    chkMinutosPosVenda.Checked = true;

            if (parametro.MensagemPosVendaAposFinalizarOs == true)
                chkMensagemAposEncerrarOS.Checked = true;
            else
                chkMensagemAposEncerrarOS.Checked = false;

            txtTempoEnvioMensagemPosVenda.Text = parametro.MensagemPosVendasQtdDiasOuMinutos;
            txtMensagemPosVenda.Text = parametro.MensagemPosVendas;

            if (parametro.AtivarMensagemVencimentoExame == true)
                chkAtivarMensagemLembreteVencimentoExameOftamotologico.Checked = true;
            else
                chkAtivarMensagemLembreteVencimentoExameOftamotologico.Checked = false;

            if (!String.IsNullOrEmpty(parametro.MensagemLembreteExameAntesOuDepois)) 
            {
                if (parametro.MensagemLembreteExameAntesOuDepois.Equals("ANTES"))
                    chkAntes.Checked = true;
                else
                    chkDepois.Checked = true;
            }

            txtDiasEnvioMensagemLembreteExame.Text = parametro.MensagemLembreteExameQtdDias;
            txtHorarioDisparoLembreteExame.Text = parametro.MensagemLembreteExameHorario;
            txtMensagemLembreteExame.Text = parametro.MensagemLembreteExame;
            if(!String.IsNullOrEmpty(parametro.NomeServidor))
                txtNomeServidor.Text = parametro.NomeServidor;

            //nuvem
            txtServidorNuvem.Text = parametro.ServidorNuvem;
            txtBancoNuvem.Text = parametro.BancoNuvem;
            txtUsuarioNuvem.Text = parametro.UsuarioNuvem;
            if(!String.IsNullOrEmpty(parametro.SenhaNuvem))
                txtSenhaNuvem.Text = GenericaDesktop.Descriptografa(parametro.SenhaNuvem);
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
            if (chkChequeContaReceber.Checked == true)
                parametro.ChequeContaReceber = true;
            else
                parametro.ChequeContaReceber = false;
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
            if (!String.IsNullOrEmpty(txtComissaoPadrao.Texts))
                parametro.Comissao = double.Parse(txtComissaoPadrao.Texts);
            else
                parametro.Comissao = 0;

            parametro.ModeloEtiquetaPadrao = txtEtiquetaPadrao.Text.Trim();
            parametro.ModeloEtiquetaNumeroOs = txtEtiquetaImprimirNumeroOs.Text.Trim();
            parametro.TokenGalaxyPay = txtTokenGalaxyPay.Texts.Trim();
            parametro.IdGalaxyPay = txtIdGalaxyPay.Texts.Trim();
            if (chkIntegracaoGalaxyPay.Checked == true)
                parametro.IntegracaoGalaxyPay = true;
            else
                parametro.IntegracaoGalaxyPay = false;
            if (!String.IsNullOrEmpty(txtCodContaBancariaVinculadaAPI.Texts))
            {
                ContaBancaria contaBancaria = new ContaBancaria();
                contaBancaria.Id = int.Parse(txtCodContaBancariaVinculadaAPI.Texts);
                contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                parametro.ContaBancariaVinculadaApi = contaBancaria;
            }
            else
                parametro.ContaBancariaVinculadaApi = null;

            parametro.UsuarioWebServiceSpcBrasil = txtUsuarioSPCBRASIL.Texts;
            parametro.SenhaWebServiceSpcBrasil = GenericaDesktop.Criptografa(txtSenhaSpcBrasil.Texts);
            if (radioSPC12.Checked == true)
                parametro.ConsultaPadraoSpcBrasil = "12";
            else if (radioSPC127.Checked == true)
                parametro.ConsultaPadraoSpcBrasil = "127";
            else if (radioSPC128.Checked == true)
                parametro.ConsultaPadraoSpcBrasil = "128";

            if (chkProducaoSPC.Checked == true)
                parametro.AmbienteSpcBrasil = "PRODUCAO";
            else
                parametro.AmbienteSpcBrasil = "HOMOLOGACAO";

            //Whatsapp
            if (chkEnvioNFCeNFeWhats.Checked == true)
                parametro.EnvioNotasPorWhats = true;
            else
                parametro.EnvioNotasPorWhats = false;

            if (chkEnvioAgradecimentoCompraWhats.Checked == true)
                parametro.EnvioAgradecimentoCompra = true;
            else
                parametro.EnvioAgradecimentoCompra = false;

            if (chkAtivarEnvioMensagemPosVenda.Checked == true)
                parametro.AtivarMensagemPosVendas = true;
            else
                parametro.AtivarMensagemPosVendas = false;

            if (chkDiasPosVenda.Checked == true)
                parametro.MensagemPosVendasDiasOuMinutos = "DIAS";
            else
                parametro.MensagemPosVendasDiasOuMinutos = "MINUTOS";

            if (chkMensagemAposEncerrarOS.Checked == true)
                parametro.MensagemPosVendaAposFinalizarOs = true;
            else
                parametro.MensagemPosVendaAposFinalizarOs = false;

            parametro.MensagemPosVendasQtdDiasOuMinutos = txtTempoEnvioMensagemPosVenda.Text;
            parametro.MensagemPosVendas = txtMensagemPosVenda.Text;

            if (chkAtivarMensagemLembreteVencimentoExameOftamotologico.Checked == true)
                parametro.AtivarMensagemVencimentoExame = true;
            if (chkAntes.Checked == true)
                parametro.MensagemLembreteExameAntesOuDepois = "ANTES";
            else
                parametro.MensagemLembreteExameAntesOuDepois = "DEPOIS";

            parametro.MensagemLembreteExameQtdDias = txtDiasEnvioMensagemLembreteExame.Text;
            parametro.MensagemLembreteExameHorario = txtHorarioDisparoLembreteExame.Text;
            parametro.MensagemLembreteExame = txtMensagemLembreteExame.Text;
            if (!String.IsNullOrEmpty(txtNomeServidor.Text))
                parametro.NomeServidor = txtNomeServidor.Text;


            //DASHBOARDS NUVEM
            parametro.ServidorNuvem = txtServidorNuvem.Text;
            parametro.BancoNuvem = txtBancoNuvem.Text;
            parametro.UsuarioNuvem = txtUsuarioNuvem.Text;
            parametro.SenhaNuvem = GenericaDesktop.Criptografa(txtSenhaNuvem.Text);

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

        private void chkLiberarAlteracaoNumeros_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkLiberarAlteracaoNumeros.Checked == true)
                {
                    txtSerieNFCe.Enabled = true;
                    txtSerieNFe.Enabled = true;
                    txtProximoNFCe.Enabled = true;
                    txtProximoNFe.Enabled = true;
                }
                else
                {
                    txtSerieNFCe.Enabled = false;
                    txtSerieNFe.Enabled = false;
                    txtProximoNFCe.Enabled = false;
                    txtProximoNFe.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void chkIntegracaoGalaxyPay_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIntegracaoGalaxyPay.Checked == true)
                {
                    txtTokenGalaxyPay.Enabled = true;
                    txtIdGalaxyPay.Enabled = true;
                }
                else
                {
                    txtTokenGalaxyPay.Enabled = false;
                    txtIdGalaxyPay.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Object objeto = new ContaBancaria();

            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancariaVinculadaAPI.Texts = "";
                    txtContaBancariaVinculadaAPI.Texts = "";
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
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancariaVinculadaAPI.Texts = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancariaVinculadaAPI.Texts = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancariaVinculadaAPI.Texts = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancariaVinculadaAPI.Texts = ((ContaBancaria)objeto).Id.ToString();
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

        private void chkHomologacaoSPC_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkHomologacaoSPC.Checked == true)
                    chkProducaoSPC.Checked = false;
            }
            catch
            {

            }
        }

        private void chkProducaoSPC_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkProducaoSPC.Checked == true)
                    chkHomologacaoSPC.Checked = false;
            }
            catch
            {

            }
        }

        private void chkEnvioAgradecimentoCompraWhats_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEnvioAgradecimentoCompraWhats.Checked == true)
                {
                    tabPosVenda.TabVisible = true;
                }
                else
                    tabPosVenda.TabVisible = false;
            }
            catch
            {

            }
        }

        private void chkDiasPosVenda_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDiasPosVenda.Checked == true)
                    chkMinutosPosVenda.Checked = false;
            }
            catch 
            { 
            }
        }

        private void chkMinutosPosVenda_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMinutosPosVenda.Checked == true)
                    chkDiasPosVenda.Checked = false;
            }
            catch
            {

            }
        }

        private void chkAntes_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAntes.Checked == true)
                    chkDepois.Checked = false;
            }
            catch
            {

            }
        }

        private void chkDepois_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDepois.Checked == true)
                    chkAntes.Checked = false;
            }
            catch
            {

            }
        }

        private void chkAtivarEnvioMensagemPosVenda_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAtivarEnvioMensagemPosVenda.Checked == true)
            {
                chkDiasPosVenda.Enabled = true;
                chkMinutosPosVenda.Enabled = true;
                txtMensagemPosVenda.Enabled = true;
                txtTempoEnvioMensagemPosVenda.Enabled = true;
                chkMensagemAposEncerrarOS.Enabled = true;
            }
            else
            {
                chkDiasPosVenda.Enabled = false;
                chkMinutosPosVenda.Enabled = false;
                txtMensagemPosVenda.Enabled = false;
                txtTempoEnvioMensagemPosVenda.Enabled = false;
                chkMensagemAposEncerrarOS.Enabled = false;
            }
        }

        private void chkAtivarMensagemLembreteVencimentoExameOftamotologico_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAtivarMensagemLembreteVencimentoExameOftamotologico.Checked == true)
            {
                txtDiasEnvioMensagemLembreteExame.Enabled = true;
                txtHorarioDisparoLembreteExame.Enabled = true;
                txtMensagemLembreteExame.Enabled = true;
            }
            else
            {
                txtDiasEnvioMensagemLembreteExame.Enabled = false;
                txtHorarioDisparoLembreteExame.Enabled = false;
                txtMensagemLembreteExame.Enabled = false;
            }
        }
    }
}
