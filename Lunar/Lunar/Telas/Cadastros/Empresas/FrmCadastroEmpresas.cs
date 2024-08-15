using Lunar.Telas.Cadastros.Cidades;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.SintegrawsConsultas;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static LunarBase.Utilidades.Ns_ConsultaCNPJ;

namespace Lunar.Telas.Cadastros.Empresas
{
    public partial class FrmCadastroEmpresas : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        Cidade cidade = new Cidade();
        CidadeController cidadeController = new CidadeController();
        Cidade cidadePrincipal = new Cidade();
        Empresa empresa = new Empresa();
        EmpresaFilial empresaFilial = new EmpresaFilial();
        EmpresaController empresaController = new EmpresaController();
        EmpresaFilialController empresaFilialController = new EmpresaFilialController();
        Endereco enderecoSelecionado = new Endereco();
        bool showModal = false;
        RegimeEmpresa regimeTributario = new RegimeEmpresa();
        string certificadoBase64 = "";
        bool puxandoCertificadoInstalado = false;
        X509Certificate2 cert = null;
        public DialogResult showModalNovo(ref object empresaFilial)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                empresaFilial = this.empresaFilial;
            }
            return DialogResult;
        }

        public FrmCadastroEmpresas()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            txtCNPJ.Focus();
            txtCNPJ.Select();
            
            regimeTributario =  new RegimeEmpresa();
            regimeTributario.Id = 1;
            regimeTributario = (RegimeEmpresa)Controller.getInstance().selecionar(regimeTributario);
            txtRegimeTributario.Texts = regimeTributario.Descricao;
            txtCodRegimeTributario.Texts = regimeTributario.Id.ToString();
            //obterVencimentoCertificado();
        }
        public FrmCadastroEmpresas(EmpresaFilial empresaFilial)
        {
            InitializeComponent();

            this.empresaFilial = empresaFilial;
            get_Filial(empresaFilial);
            this.FormBorderStyle = FormBorderStyle.None;
            txtCNPJ.Focus();
            txtCNPJ.Select();
            //obterVencimentoCertificado();
        }
        private void txtCNPJ_Leave(object sender, EventArgs e)
        {
          
        }

        private void get_Filial(EmpresaFilial empresaFilial)
        {
            txtID.Texts = empresaFilial.Id.ToString();
            get_DadosBasicos(empresaFilial);
            get_EnderecoPrincipal(empresaFilial);
        }
        private void get_DadosBasicos(EmpresaFilial empresaFilial)
        {
            //DADOS BASICOS
            if (empresaFilial.Cnpj.Length == 14)
                txtCNPJ.Texts = generica.FormatarCNPJ(GenericaDesktop.RemoveCaracteres(empresaFilial.Cnpj));
            else if (empresaFilial.Cnpj.Length == 11)
                txtCNPJ.Texts = generica.FormatarCPF(GenericaDesktop.RemoveCaracteres(empresaFilial.Cnpj));

            txtRazaoSocial.Texts = empresaFilial.RazaoSocial;
            txtNomeFantasia.Texts = empresaFilial.NomeFantasia;
            txtDataAbertura.Value = empresaFilial.DataAbertura;
            txtEmail.Texts = empresaFilial.Email;
            txtDDD.Texts = empresaFilial.DddPrincipal;
            txtTelefonePrincipal.Texts = GenericaDesktop.MascaraTelefone(GenericaDesktop.RemoveCaracteres(empresaFilial.TelefonePrincipal));
            txtInscricaoEstadual.Texts = empresaFilial.InscricaoEstadual;

            if (empresaFilial.RegimeEmpresa != null)
            {
                txtRegimeTributario.Texts = empresaFilial.RegimeEmpresa.Descricao;
                txtCodRegimeTributario.Texts = empresaFilial.RegimeEmpresa.Id.ToString();
            }
            txtNomeContador.Texts = empresaFilial.Empresa.Contador;
            txtCPFContador.Texts = empresaFilial.Empresa.CpfContador;
            txtCRCContador.Texts = empresaFilial.Empresa.CrcContador;

            txtNomeResponsavel.Texts = empresaFilial.Empresa.Responsavel;
            txtCPFResponsavel.Texts = empresaFilial.Empresa.CpfResponsavel;
            txtCargoResponsavel.Texts = empresaFilial.Empresa.FuncaoResponsavel;
            if(!String.IsNullOrEmpty(empresaFilial.SenhaCertificado))
                txtSenhaCertificado.Texts = GenericaDesktop.Descriptografa(empresaFilial.SenhaCertificado);
            if (empresaFilial.Otica == true)
                chkOtica.Checked = true;
            else
                chkOtica.Checked = false;
            txtEmailContabilidadeXml.Text = empresaFilial.EmailXml;
        }

        private void get_EnderecoPrincipal(EmpresaFilial empresaFilial)
        {
            if (empresaFilial.Endereco != null)
            {
                enderecoSelecionado = empresaFilial.Endereco;
                txtCEP.Texts = GenericaDesktop.MascaraCep(empresaFilial.Endereco.Cep);
                txtEndereco.Texts = empresaFilial.Endereco.Logradouro;
                txtNumero.Texts = empresaFilial.Endereco.Numero;
                txtComplemento.Texts = empresaFilial.Endereco.Complemento;
                txtReferencia.Texts = empresaFilial.Endereco.Referencia;
                txtBairro.Texts = empresaFilial.Endereco.Bairro;
                txtCidade.Texts = empresaFilial.Endereco.Cidade.Descricao;
                txtUF.Texts = empresaFilial.Endereco.Cidade.Estado.Uf;
                cidadePrincipal = empresaFilial.Endereco.Cidade;
            }
        }

        private void FrmCadastroEmpresas_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            timer1.Start();
        }

        private void set_Empresa()
        {
            Endereco endereco = new Endereco();
            try
            {
                if (enderecoSelecionado.Id > 0)
                {
                    endereco.Id = enderecoSelecionado.Id;
                }
                else
                {
                    endereco.Id = 0;
                }
                endereco.Logradouro = txtEndereco.Texts;
                endereco.Numero = txtNumero.Texts;
                endereco.Pessoa = null;
                endereco.EmpresaFilial = null;
                endereco.Referencia = txtReferencia.Texts;
                endereco.Complemento = txtComplemento.Texts;
                if (cidadePrincipal != null)
                    endereco.Cidade = cidadePrincipal;
                endereco.Cep = GenericaDesktop.RemoveCaracteres(txtCEP.Texts);
                endereco.Bairro = txtBairro.Texts;
                

                Controller.getInstance().salvar(endereco);
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro("Falha ao salvar endereço" + ex.Message);
            }

            try
            {
                if (endereco != null)
                {
                    if(endereco.Id > 0)
                    {
                        //Seleciona todas empresas e verifica se ja tem cadastro
                        IList<Empresa> listaEmpresa = empresaController.selecionarTodasEmpresas();
                        if (listaEmpresa.Count <= 0)
                        {
                            empresa = new Empresa();
                            empresa.Cnae = txtCNAE.Texts;
                            empresa.Cnpj = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts);
                            empresa.Contador = txtNomeContador.Texts;
                            empresa.CpfContador = txtCPFContador.Texts;
                            empresa.CpfResponsavel = GenericaDesktop.RemoveCaracteres(txtCPFResponsavel.Texts);
                            empresa.CrcContador = txtCRCContador.Texts;
                            empresa.DataAbertura = DateTime.Parse(txtDataAbertura.Value.ToString());
                            empresa.DddPrincipal = txtDDD.Texts;
                            empresa.TelefonePrincipal = GenericaDesktop.RemoveCaracteres(txtTelefonePrincipal.Texts);
                            empresa.RazaoSocial = txtRazaoSocial.Texts;
                            empresa.NomeFantasia = txtNomeFantasia.Texts;
                            empresa.Responsavel = txtNomeResponsavel.Texts;
                            empresa.FuncaoResponsavel = txtCargoResponsavel.Texts;
                            empresa.InscricaoEstadual = txtInscricaoEstadual.Texts;
                            empresa.Email = txtEmail.Texts;
                            empresa.EmailContador = "";
                            empresa.DddSecundario = "";
                            empresa.TelefoneSecundario = "";

                            //Valida licenca comparado validade com token criptografado
                            empresa.Token = GenericaDesktop.Criptografa(DateTime.Now.AddDays(14).ToShortDateString());
                            empresa.ValidadeLicenca = DateTime.Now.AddDays(14);
                            empresa.Endereco = endereco;
                            Controller.getInstance().salvar(empresa);
                        }
                        else
                        {
                            foreach (Empresa emp in listaEmpresa)
                            {
                                empresa = new Empresa();
                                emp.Responsavel = txtNomeResponsavel.Texts;
                                emp.FuncaoResponsavel = txtCargoResponsavel.Texts;
                                emp.CpfResponsavel = txtCPFResponsavel.Texts;
                                emp.Contador = txtNomeContador.Texts;
                                emp.CpfContador = txtCPFContador.Texts;
                                emp.CrcContador = txtCRCContador.Texts;
                                Controller.getInstance().salvar(emp);
                                empresa = emp;
                            }
                        }

                        empresaFilial = new EmpresaFilial();
                        // pesquisar filial pra ver se ja tem cadastro...
                        //se tiver executa o get filial....

                        if (!String.IsNullOrEmpty(txtID.Texts))
                        {
                            empresaFilial.Id = int.Parse(txtID.Texts);
                        }
                        else
                        {
                            empresaFilial.Id = 0;
                        }
                        empresaFilial.Cnae = txtCNAE.Texts;
                        empresaFilial.Cnpj = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts);
                        empresaFilial.DataAbertura = DateTime.Parse(txtDataAbertura.Value.ToString());
                        empresaFilial.DddPrincipal = txtDDD.Texts;
                        empresaFilial.TelefonePrincipal = txtTelefonePrincipal.Texts;
                        empresaFilial.DddSecundario = "";
                        empresaFilial.TelefoneSecundario = "";
                        empresaFilial.Email = txtEmail.Texts;
                        empresaFilial.InscricaoEstadual = txtInscricaoEstadual.Texts;
                        empresaFilial.NomeFantasia = txtNomeFantasia.Texts;
                        empresaFilial.RazaoSocial = txtRazaoSocial.Texts;
                        empresaFilial.Endereco = endereco;
                        empresaFilial.Empresa = empresa;
                        empresaFilial.EmailXml = txtEmailContabilidadeXml.Text.Trim();
                        if (chkOtica.Checked == true)
                            empresaFilial.Otica = true;
                        else
                            empresaFilial.Otica = false;
                        empresaFilial.SenhaCertificado = GenericaDesktop.Criptografa(txtSenhaCertificado.Texts);
                        if (!String.IsNullOrEmpty(txtCodRegimeTributario.Texts))
                        {
                            regimeTributario = new RegimeEmpresa();
                            regimeTributario.Id = int.Parse(txtCodRegimeTributario.Texts);
                            regimeTributario = (RegimeEmpresa)Controller.getInstance().selecionar(regimeTributario);
                            empresaFilial.RegimeEmpresa = regimeTributario;
                        }
                        else
                            empresaFilial.RegimeEmpresa = null;

                        Controller.getInstance().salvar(empresaFilial);

                        endereco.EmpresaFilial = empresaFilial;
                        Controller.getInstance().salvar(endereco);
                        if(empresaFilial.Id == Sessao.empresaFilialLogada.Id)
                            Sessao.empresaFilialLogada = empresaFilial;

                        GenericaDesktop.ShowInfo("Registro salvo com Sucesso!");
                        if (showModal)
                        {
                            DialogResult = DialogResult.OK;
                        }
                        this.Close();
                    } 
                }
            }
            catch (Exception ex) 
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Empresa();
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            pesquisarCEP();
        }

        private void pesquisarCEP()
        {
            if (!String.IsNullOrEmpty(GenericaDesktop.RemoveCaracteres(txtCEP.Texts.Trim())))
            {
                try
                {
                    var ws = new WSCorreios.AtendeClienteClient();
                    var resposta = ws.consultaCEP(txtCEP.Texts, "marcelo.xs@hotmail.com", "@Aranhamxs11");
                    if (!String.IsNullOrEmpty(resposta.end))
                    {
                        txtEndereco.Texts = generica.RemoverAcentos(resposta.end);
                        txtComplemento.Texts = resposta.complemento2;
                        cidade = new Cidade();
                        cidade = cidadeController.selecionarCidadePorDescricao(generica.RemoverAcentos(resposta.cidade));
                        if (cidade != null)
                        {
                            txtCidade.Texts = cidade.Descricao;
                            txtUF.Texts = cidade.Estado.Uf;
                            txtBairro.Texts = resposta.bairro;
                            cidadePrincipal = cidade;
                        }
                        txtNumero.Focus();
                    }
                }
                catch
                {
                    txtEndereco.Texts = "";
                    txtNumero.Texts = "";
                    txtComplemento.Texts = "";
                    txtReferencia.Texts = "";
                }

            }
        }

        private void txtTelefonePrincipal_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTelefonePrincipal.Texts))
                txtTelefonePrincipal.Texts = GenericaDesktop.MascaraTelefone(txtTelefonePrincipal.Texts);
        }

        private void txtTelefonePrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtTelefonePrincipal.Texts, e);
        }

        private void txtCPFResponsavel_Leave(object sender, EventArgs e)
        {
            if(txtCPFResponsavel.Texts.Length == 11)
            {
                GenericaDesktop generica = new GenericaDesktop();
                txtCPFResponsavel.Texts = generica.FormatarCPF(txtCPFResponsavel.Texts);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.20;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void FrmCadastroEmpresas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;
            }
        }

        private void btnPesquisaCidade_Click(object sender, EventArgs e)
        {
            this.cidade = new Cidade();
            Object cidadeObjeto = new Cidade();
            txtCidade.Texts = "";
            txtUF.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Cidade", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    uu.Owner = formBackground;
                    switch (uu.showModal("Cidade", "", ref cidadeObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmCidadeCadastro form = new FrmCidadeCadastro();
                            //if (form.showModalNovo(ref cidadeObjeto) == DialogResult.OK)
                            //{
                            //    txtCidade.Texts = ((Cidade)cidadeObjeto).Descricao;
                            //    txtUF.Texts = ((Cidade)cidadeObjeto).Estado.Uf;
                            //    cidade = ((Cidade)cidadeObjeto);
                            //    txtNomeResponsavel.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCidade.Texts = ((Cidade)cidadeObjeto).Descricao;
                            txtUF.Texts = ((Cidade)cidadeObjeto).Estado.Uf;
                            cidade = ((Cidade)cidadeObjeto);
                            txtNomeResponsavel.Focus();
                            cidadePrincipal = cidade;
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

        private void btnPesquisaRegimerTributario_Click(object sender, EventArgs e)
        {
            Object regimeEmpresaOjeto = new RegimeEmpresa();
            txtCodRegimeTributario.Texts = "";
            txtRegimeTributario.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("RegimeEmpresa", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("RegimeEmpresa", "", ref regimeEmpresaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtRegimeTributario.Texts = ((RegimeEmpresa)regimeEmpresaOjeto).Descricao;
                            txtCodRegimeTributario.Texts = ((RegimeEmpresa)regimeEmpresaOjeto).Id.ToString();
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public bool obterVencimentoCertificado()
        {
            try
            {
                if (GenericaDesktop.possuiConexaoInternet())
                {
                    string ret = generica.NS_ConsultaValidadeCertificado(Sessao.empresaFilialLogada.Cnpj);
                    if (!String.IsNullOrEmpty(ret))
                    {
                        DateTime dataVencimento = DateTime.Parse(ret);
                        TimeSpan date = Convert.ToDateTime(dataVencimento) - Convert.ToDateTime(DateTime.Now);
                        int totalDias = date.Days;
                        txtCertificado.Texts = "VENCIMENTO CERTIFICADO: " + ret;
                        if (dataVencimento < DateTime.Now)
                        {
                            GenericaDesktop.ShowAlerta("Certificado digital vencido em " + dataVencimento.ToShortDateString());
                            return false;
                        }
                        else if (totalDias <= 30)
                        {
                            GenericaDesktop.ShowAlerta("Atenção: Certificado digital próximo do vencimento " + dataVencimento.ToUniversalTime());
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
                return false;
            }
        }

        private void btnPesquisaCertificado_Click(object sender, EventArgs e)
        {
            puxandoCertificadoInstalado = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Certificado (*.PFX;*.P12;)|*.PFX;*.P12|" + "Todos Arquivos (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCertificado.Texts = openFileDialog1.FileName;

                var certContents = File.ReadAllBytes(openFileDialog1.FileName);
                certificadoBase64 = Convert.ToBase64String(certContents);

                if (String.IsNullOrEmpty(txtSenhaCertificado.Texts))
                {
                    txtSenhaCertificado.Focus();
                    GenericaDesktop.ShowInfo("Informe a senha do certificado e clique no botão confirmar!");
                }
                else
                    btnConfirmaCertificado.PerformClick();
            }
        }

        private void btnConfirmaCertificado_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtCertificado.Texts) && !String.IsNullOrEmpty(txtSenhaCertificado.Texts))
            {
                if (!txtCertificado.Texts.Substring(0, 4).Contains("VENC"))
                {
                    if (GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()).Length == 14)
                    {
                        string ret55 = generica.NS_EnviarCertificadoDigitalParaNFe55(GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()), txtSenhaCertificado.Texts, certificadoBase64);
                        string ret65 = generica.NS_EnviarCertificadoDigitalParaNFCe65(GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()), txtSenhaCertificado.Texts, certificadoBase64);
                        string retDDFe = generica.NS_EnviarCertificadoDigitalParaDDFe(GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()), txtSenhaCertificado.Texts, certificadoBase64);

                        if (ret55.Equals("Certificado salvo com sucesso") && ret65.Equals("Certificado salvo com sucesso") && retDDFe.Equals("Certificado salvo com sucesso"))
                        {
                            GenericaDesktop.ShowInfo("Certificado salvo com sucesso");
                            obterVencimentoCertificado();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Modelo de Nota 55: " + ret55 + "\n\nModelo de Nota 65: " + ret65 + "\n\nManifesto DDFe " + retDDFe);
                    }
                    else
                    {
                        GenericaDesktop.ShowErro("Informe um cnpj válido da empresa no campo cnpj");
                        tabControlAdv1.SelectedTab = tabPageAdv1;
                        txtCNPJ.Focus();
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione um certificado digital na lupa de pesquisa");

                
            }
        }

        private void btnPesquisaCnpj_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCNPJ.Texts))
                {
                    empresaFilial = new EmpresaFilial();
                    empresaFilial = empresaFilialController.selecionarEmpresaFilialPorCPFCNPJ(GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()));
                    if (empresaFilial == null)
                    {
                        if (Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()).Length == 14 && GenericaDesktop.validarCPFCNPJ(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim())))
                        {
                            ConsultEmpresaNs consulta = new ConsultEmpresaNs();
                            consulta = generica.consultarEmpresaPorCnpj_NS("28145398000173", Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()), "MG");
                            //SintegraConsultaCnpj consulta = new SintegraConsultaCnpj();
                           // consulta = generica.consultaCNPJSintegraWS(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()));
                            txtRazaoSocial.Texts = consulta.retConsCad.infCons.infCad[0].xNome;
                            txtNomeFantasia.Texts = consulta.retConsCad.infCons.infCad[0].xFant;
                            txtCEP.Texts = consulta.retConsCad.infCons.infCad[0].ender.CEP;
                            txtDataAbertura.Value = DateTime.Parse(consulta.retConsCad.infCons.infCad[0].dIniAtiv);
                            txtEndereco.Texts = consulta.retConsCad.infCons.infCad[0].ender.xLgr;
                            txtNumero.Texts = consulta.retConsCad.infCons.infCad[0].ender.nro;
                            txtComplemento.Texts = consulta.retConsCad.infCons.infCad[0].ender.xCpl;
                            txtBairro.Texts = consulta.retConsCad.infCons.infCad[0].ender.xBairro;
                            txtInscricaoEstadual.Texts = consulta.retConsCad.infCons.infCad[0].IE;
                            txtCNAE.Texts = consulta.retConsCad.infCons.infCad[0].CNAE.ToString();

                            cidade = new Cidade();
                            cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(consulta.retConsCad.infCons.infCad[0].ender.xMun, consulta.retConsCad.infCons.infCad[0].ender.cMun);
                            if (cidade != null)
                            {
                                txtCidade.Texts = cidade.Descricao;
                                txtUF.Texts = cidade.Estado.Uf;
                                cidadePrincipal = cidade;
                            }

                            txtEmail.Focus();
                        }
                        else if (txtCNPJ.Texts.Trim().Length == 11 && GenericaDesktop.validarCPFCNPJ(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim())))
                        {
                            txtCNPJ.Texts = generica.FormatarCPF(txtCNPJ.Texts);
                        }
                        else if (txtCNPJ.Texts.Trim().Length == 0)
                        {

                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Documento inválido!");
                            txtCNPJ.Texts = "";
                        }
                    }
                    else if (empresaFilial != null && String.IsNullOrEmpty(txtID.Texts))
                    {
                        GenericaDesktop.ShowAlerta("Empresa já possui cadastro no sistema!");
                        get_Filial(empresaFilial);
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Preencha o cnpj antes de clicar na pesquisa");
                }
            }
            catch
            {
                GenericaDesktop.ShowErro("Falha na consulta automática de dados do cnpj, preencha os dados manualmente");
            }
        }
    }
}
