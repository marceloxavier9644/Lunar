using Lunar.consultaSPCBrasil;
using Lunar.Telas.Cadastros.Cidades;
using Lunar.Telas.Cadastros.Cliente.PessoaAdicionais;
using Lunar.Telas.SPCs;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Utils;
using Lunar.Utils.SPCBrasilIntegracao;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.Utilidades.Ns_ConsultaCNPJ;
using Cidade = LunarBase.Classes.Cidade;
using Endereco = LunarBase.Classes.Endereco;

namespace Lunar.Telas.Cadastros.Cliente
{
    public partial class FrmClienteCadastro : Form
    {
        int posicaoItem = 0;
        PessoaController pessoaController = new PessoaController();
        GenericaDesktop generica = new GenericaDesktop();
        CNPJResponse cnpjResponse = new CNPJResponse();
        Ns_ConsultaCNPJ cnpjConsult = new Ns_ConsultaCNPJ();
        CidadeController cidadeController = new CidadeController();
        Cidade cidade = new Cidade();
        Cidade cidadePrincipal = new Cidade();
        Pessoa pessoa = new Pessoa();
        PessoaReferenciaPessoal pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
        PessoaReferenciaComercial pessoaReferenciaComercial = new PessoaReferenciaComercial();
        PessoaPropriedade pessoaPropriedade = new PessoaPropriedade();
        Endereco endereco = new Endereco();
        PessoaTelefoneController pessoaTelefoneController = new PessoaTelefoneController();
        PessoaTelefone pessoaTelefone = new PessoaTelefone();
        EnderecoController enderecoController = new EnderecoController();
        PessoaPropriedadeController pessoaPropriedadeController = new PessoaPropriedadeController();
        PessoaReferenciaPessoalController pessoaReferenciaPessoalController = new PessoaReferenciaPessoalController();
        PessoaReferenciaComercialController pessoaReferenciaComercialController = new PessoaReferenciaComercialController();

        bool showModal = false;
        public DialogResult showModalNovo(ref object pessoa)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                pessoa = this.pessoa;
            }
            return DialogResult;
        }
        public FrmClienteCadastro()
        {
            InitializeComponent();
            this.pessoa = new Pessoa();
        }

        public FrmClienteCadastro(Pessoa pessoa)
        {
            InitializeComponent();
            this.pessoa = pessoa;
            get_Pessoa(pessoa);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioPJ_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioPJ.Checked == true)
                    alterarPessoaFisicaParaJuridica();
                else
                    alterarPessoaJuridicaParaFisica();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }
        private void alterarPessoaFisicaParaJuridica()
        {
            lblCNPJ.Text = "CNPJ";
            lblRazaoSocial.Text = "Razão Social *";
            lblNomeFantasia.Text = "Nome Fantasia *";
            lblDataNascimento.Text = "Data Abertura *";
            lblInscricaoEstadual.Text = "Inscrição Estadual *";
            //txtInscricaoProdutor.PlaceholderText = "ISENTO";
            lblRG.Text = "Contato na Empresa";
            panelSexo.Visible = false;
            radioMasculino.Visible = false;
            radioFeminino.Visible = false;
            txtInscricaoProdutor.Size = new System.Drawing.Size(678, 39);
            txtInscricaoProdutor.Location = new System.Drawing.Point(12, 217);
            lblInscricaoEstadual.Location = new System.Drawing.Point(25, 204);
            txtObservacoes.Size = new System.Drawing.Size(982, 39);
            txtObservacoes.Location = new System.Drawing.Point(12, 269);
            autoLabel16.Location = new System.Drawing.Point(25, 255);
            tabDocumentos.TabVisible = false;
            //picFoto.Image = null;
            picFoto.BackgroundImage = Lunar.Properties.Resources.Empresa;
            txtCNPJ.Focus();
            if (pessoa != null)
            {
                if (pessoa.TipoPessoa != null)
                {
                    if (pessoa.TipoPessoa.Equals("F"))
                    {
                        if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                        {
                            GenericaDesktop.ShowAlerta("Atenção, você está editando o cliente/pessoa de código " + txtCodCliente.Texts);
                        }
                    }
                }
            }
        }
        private void alterarPessoaJuridicaParaFisica()
        {
            lblCNPJ.Text = "CPF";
            lblRazaoSocial.Text = "Nome";
            lblNomeFantasia.Text = "Apelido";
            lblDataNascimento.Text = "Data Nascimento";
            lblInscricaoEstadual.Text = "Inscrição Estadual Produtor Rural";
            lblRG.Text = "RG";
            panelSexo.Visible = true;
            radioMasculino.Visible = true;
            radioFeminino.Visible = true;
            txtInscricaoProdutor.Size = new System.Drawing.Size(338, 39);
            txtInscricaoProdutor.Location = new System.Drawing.Point(352, 217);
            lblInscricaoEstadual.Location = new System.Drawing.Point(365, 204);
            txtObservacoes.Size = new System.Drawing.Size(642, 39);
            txtObservacoes.Location = new System.Drawing.Point(352, 269);
            autoLabel16.Location = new System.Drawing.Point(365, 256);
            tabDocumentos.TabVisible = true;
            picFoto.BackgroundImage = Lunar.Properties.Resources.User1;
            txtCNPJ.Focus();
            if (pessoa != null)
            {
                if (pessoa.TipoPessoa != null)
                {
                    if (pessoa.TipoPessoa.Equals("J"))
                    {
                        if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                        {
                            GenericaDesktop.ShowAlerta("Atenção, você está editando o cliente/pessoa de código " + txtCodCliente.Texts);
                        }
                    }
                }
            }
        }

        private void txtCNPJ_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCNPJ.Texts))
                {
                    pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()));
                    if (pessoa == null)
                    {
                        if (Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()).Length == 14 && GenericaDesktop.validarCPFCNPJ(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim())))
                        {
                            if (radioPJ.Checked == false)
                                radioPJ.Checked = true;

                            //SintegraConsultaCnpj consulta = new SintegraConsultaCnpj();
                            //consulta = generica.consultaCNPJSintegraWS(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()));
                            ConsultEmpresaNs empr = new ConsultEmpresaNs();
                            empr = generica.consultarEmpresaPorCnpj_NS(Sessao.empresaFilialLogada.Cnpj, Generica.RemoveCaracteres(txtCNPJ.Texts.Trim()), "MG");
                            if (empr != null)
                            {
                                txtRazaoSocial.Texts = empr.retConsCad.infCons.infCad[0].xNome;
                                txtNomeFantasia.Texts = empr.retConsCad.infCons.infCad[0].xFant;
                                txtCEP.Texts = empr.retConsCad.infCons.infCad[0].ender.CEP;
                                // txtDataAbertura.Value = DateTime.Parse(consulta.data_inicio_atividade);
                                txtEndereco.Texts = empr.retConsCad.infCons.infCad[0].ender.xLgr;
                                txtNumero.Texts = empr.retConsCad.infCons.infCad[0].ender.nro;
                                txtComplemento.Texts = empr.retConsCad.infCons.infCad[0].ender.xCpl;
                                txtBairro.Texts = empr.retConsCad.infCons.infCad[0].ender.xBairro;
                                txtInscricaoProdutor.Texts = empr.retConsCad.infCons.infCad[0].IE;
                                //txtCNAE.Texts = consulta.cnae_principal.code;

                                cidade = new Cidade();
                                cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(empr.retConsCad.infCons.infCad[0].ender.xMun, empr.retConsCad.infCons.infCad[0].ender.cMun);
                                if (cidade != null)
                                {
                                    txtCidade.Texts = cidade.Descricao;
                                    txtUF.Texts = cidade.Estado.Uf;
                                    //cidadePrincipal = cidade;
                                }
                            }
                            txtDDD.Focus();
                        }
                        else if (txtCNPJ.Texts.Trim().Length == 11 && GenericaDesktop.validarCPFCNPJ(Generica.RemoveCaracteres(txtCNPJ.Texts.Trim())))
                        {
                            txtCNPJ.Texts = generica.FormatarCPF(txtCNPJ.Texts);
                            if (radioPJ.Checked == true)
                                radioPJ.Checked = false;
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
                    else if(pessoa != null && String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        GenericaDesktop.ShowAlerta("Cliente/Pessoa já possui cadastro no sistema!");
                        get_Pessoa(pessoa);
                    }
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void pesquisaCidadePorNome(string descricaoCidade)
        {
            try
            {
                if (!String.IsNullOrEmpty(descricaoCidade))
                {
                    cidade = new Cidade();
                    IList<Cidade> listaCidade = cidadeController.selecionarListaCidadePorDescricao(descricaoCidade);

                    if (listaCidade.Count == 1)
                    {
                        foreach (Cidade cid in listaCidade)
                        {
                            txtCidade.Texts = cid.Descricao;
                            txtUF.Texts = cid.Estado.Uf;
                            cidadePrincipal = cid;
                            txtInscricaoProdutor.Focus();
                        }
                    }
                    else if (listaCidade.Count > 1)
                    {
                        Form formBackground = new Form();
                        try
                        {
                            using (FrmPesquisaCidade uu = new FrmPesquisaCidade())
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
                                switch (uu.showModalComDescricao(ref cidade, txtCidade.Texts.Trim()))
                                {
                                    case DialogResult.Ignore:
                                        uu.Dispose();
                                        txtCidade.Texts = "";
                                        txtUF.Texts = "";
                                        break;
                                    case DialogResult.OK:
                                        txtCidade.Texts = cidade.Descricao;
                                        txtUF.Texts = cidade.Estado.Uf;
                                        cidadePrincipal = cidade;
                                        txtInscricaoProdutor.Focus();
                                        break;
                                }

                                formBackground.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    btnPesquisaCidade.PerformClick();
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Cidade não encontrada! " + erro.Message);
            } 
        }

        private void txtCidade_Leave(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtCidade.Texts))
                pesquisaCidadePorNome(txtCidade.Texts.Trim());
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            
        }

        private void pesquisarCEP()
        {
            if (!String.IsNullOrEmpty(GenericaDesktop.RemoveCaracteres(txtCEP.Texts.Trim())))
            {
                try
                {
                    var ws = new WSCorreios.AtendeClienteClient();

                    
                    //o login valido está dentro do metodo consultaCEP
                    var resposta = ws.consultaCEP(txtCEP.Texts, "24092991", "28145398");
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
                catch (System.ServiceModel.FaultException)
                {
                    // Trate a exceção de autenticação nula aqui
                    GenericaDesktop.gravarLinhaLog("Falha na autenticação com o serviço WSCorreios, verificar Usuario e Senha!", "WSCORREIOS");
                    //MessageBox.Show("Não foi possível autenticar com o serviço dos Correios. Verifique suas credenciais e tente novamente.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Capture outras exceções aqui
                    GenericaDesktop.gravarLinhaLog("Ocorreu um erro durante a consulta de CEP: " + ex.Message, "WSCORREIOS");
                    //MessageBox.Show("Ocorreu um erro durante a consulta de CEP. Por favor, tente novamente mais tarde.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private async Task pesquisaCepAPIAsync()
        {
            string cep = GenericaDesktop.RemoveCaracteres(txtCEP.Texts);

            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);
                EnderecoCep enderecoCep = JsonConvert.DeserializeObject<EnderecoCep>(json);

                txtEndereco.Texts = generica.RemoverAcentos(enderecoCep.Logradouro);
                txtComplemento.Texts = enderecoCep.Complemento;
                cidade = new Cidade();
                cidade = cidadeController.selecionarCidadePorDescricao(generica.RemoverAcentos(enderecoCep.Localidade));
                if (cidade != null)
                {
                    txtCidade.Texts = cidade.Descricao;
                    txtUF.Texts = enderecoCep.Uf;
                    txtBairro.Texts = enderecoCep.Bairro;
                    cidadePrincipal = cidade;
                }
                txtNumero.Focus();
            }
        }

        class EnderecoCep
        {
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
        }
        private void btnPesquisaCidade_Click(object sender, EventArgs e)
        {
            txtUF.Texts = "";
            txtCidade.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaCidade uu = new FrmPesquisaCidade())
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
                    switch (uu.showModal(ref cidade))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCidade.Texts = cidade.Descricao;
                            txtUF.Texts = cidade.Estado.Uf;
                            cidadePrincipal = cidade;
                            txtInscricaoProdutor.Focus();
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
            //FrmPesquisaCidade frm = new FrmPesquisaCidade();
            //frm.ShowDialog();
        }

        private void btnAdicionarReferenciaPessoal_Click(object sender, EventArgs e)
        {
            Sessao.pessoaReferenciaPessoal = null;
            pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroReferenciaPessoal uu = new FrmCadastroReferenciaPessoal())
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
                    switch (uu.showModal(ref pessoaReferenciaPessoal))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                           if(Sessao.pessoaReferenciaPessoal != null)
                            {
                                inserirReferenciaPessoalGrid(Sessao.pessoaReferenciaPessoal);
                            }
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

        private void inserirReferenciaPessoalGrid(PessoaReferenciaPessoal pessoaReferenciaPessoal)
        {
            DataRow row = dsReferenciaPessoal.Tables[0].NewRow();
            row.SetField("Codigo", pessoaReferenciaPessoal.Id);
            row.SetField("Nome", pessoaReferenciaPessoal.Nome);
            row.SetField("Telefone", pessoaReferenciaPessoal.Telefone);
            row.SetField("Proximidade", pessoaReferenciaPessoal.Grau);
            row.SetField("Observacoes", pessoaReferenciaPessoal.Observacoes);
            dsReferenciaPessoal.Tables[0].Rows.Add(row);
        }

      
        private void inserirReferenciaComercialGrid(PessoaReferenciaComercial pessoaReferenciaComercial)
        {
            DataRow row = dsReferenciaComercial.Tables[0].NewRow();
            row.SetField("Codigo", pessoaReferenciaComercial.Id);
            row.SetField("Empresa", pessoaReferenciaComercial.Empresa);
            row.SetField("Telefone", pessoaReferenciaComercial.Telefone);
            row.SetField("Observacoes", pessoaReferenciaComercial.Observacoes);
            dsReferenciaComercial.Tables[0].Rows.Add(row);
        }

       
        private void inserirTelefoneGrid(PessoaTelefone pessoaTelefone)
        {
            DataRow row = dsTelefone.Tables[0].NewRow();
            row.SetField("Codigo", pessoaTelefone.Id);
            row.SetField("DDD", pessoaTelefone.Ddd);
            row.SetField("Telefone", pessoaTelefone.Telefone);
            row.SetField("Observacoes", pessoaTelefone.Observacoes);
            dsTelefone.Tables[0].Rows.Add(row);
        }
        private void inserirDependenteGrid(PessoaDependente pessoaDependente)
        {
            DataRow row = dsDependente.Tables[0].NewRow();
            row.SetField("Codigo", pessoaDependente.Id);
            row.SetField("Nome", pessoaDependente.Nome);
            row.SetField("Telefone", pessoaDependente.Telefone);
            row.SetField("Observacoes", pessoaDependente.Observacoes);
            if(pessoaDependente.Cpf.Length == 11)
                row.SetField("Cpf", generica.FormatarCPF(pessoaDependente.Cpf));
            else
                row.SetField("Cpf", pessoaDependente.Cpf);
            row.SetField("DataNascimento", pessoaDependente.DataNascimento.ToShortDateString());
            row.SetField("Parentesco", pessoaDependente.Parentesco);
            dsDependente.Tables[0].Rows.Add(row);
        }
        private void FrmClienteCadastro_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            timer1.Start();
            if(!String.IsNullOrEmpty(Sessao.parametroSistema.ConsultaPadraoSpcBrasil) && !String.IsNullOrEmpty(Sessao.parametroSistema.UsuarioWebServiceSpcBrasil))
            {
                btnSPCBrasil.Visible = true;
                if (Sessao.permissoes.Count > 0)
                {
                    // Habilitar ou desabilitar os controles com base nas permissões
                    btnSPCBrasil.Enabled = Sessao.permissoes.Contains("5");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) 
                this.Opacity += 0.20;
        }

        private void btnAdicionarReferenciaComercial_Click(object sender, EventArgs e)
        {
            Sessao.pessoaReferenciaComercial = null;
            pessoaReferenciaComercial = new PessoaReferenciaComercial();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroReferenciaComercial uu = new FrmCadastroReferenciaComercial())
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
                    switch (uu.showModal(ref pessoaReferenciaComercial))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            if (Sessao.pessoaReferenciaComercial != null)
                            {
                                inserirReferenciaComercialGrid(Sessao.pessoaReferenciaComercial);
                            }
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

        private void txtTelefonePrincipal_Leave(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtTelefonePrincipal.Texts))
                txtTelefonePrincipal.Texts = GenericaDesktop.MascaraTelefone(txtTelefonePrincipal.Texts);
        }

        private void btnAddPropriedade_Click(object sender, EventArgs e)
        {
            Sessao.pessoaPropriedade = null;
            pessoaPropriedade = new PessoaPropriedade();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroPropriedade uu = new FrmCadastroPropriedade())
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
                    switch (uu.showModal(ref pessoaPropriedade))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            if (Sessao.pessoaPropriedade != null)
                            {
                                inserirPropriedadeGrid(Sessao.pessoaPropriedade);
                            }
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

        private void inserirPropriedadeGrid(PessoaPropriedade pessoaPropriedade)
        {
            DataRow row = dsPropriedade.Tables[0].NewRow();
            row.SetField("Codigo", pessoaPropriedade.Id);
            row.SetField("Propriedade", pessoaPropriedade.Descricao);
            row.SetField("InscricaoEstadual", pessoaPropriedade.InscricaoEstadual);
            row.SetField("Cep", pessoaPropriedade.Endereco.Cep);
            row.SetField("Endereco", pessoaPropriedade.Endereco.Logradouro);
            row.SetField("Numero", pessoaPropriedade.Endereco.Numero);
            row.SetField("Complemento", pessoaPropriedade.Endereco.Complemento);
            row.SetField("Referencia", pessoaPropriedade.Endereco.Referencia);
            row.SetField("CodCidade", pessoaPropriedade.Endereco.Cidade.Id.ToString());
            row.SetField("Cidade", pessoaPropriedade.Endereco.Cidade.Descricao);
            row.SetField("UF", pessoaPropriedade.Endereco.Cidade.Estado.Uf);
            row.SetField("Bairro", pessoaPropriedade.Endereco.Bairro);
            dsPropriedade.Tables[0].Rows.Add(row);
        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtInscricaoProdutor.Focus();
            }
        }

        private void btnAddEndereco_Click(object sender, EventArgs e)
        {
            Sessao.endereco = null;
            endereco = new Endereco();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroEnderecoAdicional uu = new FrmCadastroEnderecoAdicional())
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
                    switch (uu.showModal(ref endereco))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            if (Sessao.endereco != null)
                            {
                                inserirEnderecoGrid(Sessao.endereco);
                            }
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

        private void inserirEnderecoGrid(Endereco endereco)
        {
            DataRow row = dsEndereco.Tables[0].NewRow();
            row.SetField("Codigo", endereco.Id);
            string cep = "";
            if (endereco.Cep.Length == 8)
            {
                cep = endereco.Cep.Substring(0, 5);
                cep = cep + "-" + endereco.Cep.Substring(5, 3);
            }
            row.SetField("Cep", cep);
            row.SetField("Logradouro", endereco.Logradouro);
            row.SetField("Numero", endereco.Numero);
            row.SetField("Complemento", endereco.Complemento);
            row.SetField("Referencia", endereco.Referencia);
            row.SetField("CodCidade", endereco.Cidade.Id.ToString());
            row.SetField("Cidade", endereco.Cidade.Descricao);
            row.SetField("UF", endereco.Cidade.Estado.Uf);
            row.SetField("Bairro", endereco.Bairro);
            dsEndereco.Tables[0].Rows.Add(row);
        }

        //private void verifica_posicao_item_gridEndereco()
        //{
        //    //Percorre todo o grid, e preenche a posicao dos itens
        //    posicaoItem = 1;
        //    int linha = 0;
        //    for (int i = 0; i < gridEnderecoAdicional.RowCount; i++)
        //    {
        //        gridEnderecoAdicional.Rows[linha].Cells[0].Value = posicaoItem;
        //        linha++;
        //        posicaoItem++;
        //    }
        //}

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCEP.Texts, e);
        }

        private void txtCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCNPJ.Texts, e);
        }

        private void txtDDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtDDD.Texts, e);
        }

        private void txtTelefonePrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtTelefonePrincipal.Texts, e);
        }

        private void btnAddTelefone_Click(object sender, EventArgs e)
        {
            Sessao.pessoaTelefone = null;
            PessoaTelefone pessoaTelefone = new PessoaTelefone();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroTelefone uu = new FrmCadastroTelefone())
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
                    switch (uu.showModal(ref pessoaTelefone))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            if (Sessao.pessoaTelefone != null)
                            {
                                inserirTelefoneGrid(Sessao.pessoaTelefone);
                            }
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

        private void btnRemoverTelefone_Click(object sender, EventArgs e)
        {
            if (gridTelefoneAdicional.RowCount > 0)
            {
                if (int.Parse(gridTelefoneAdicional.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridTelefoneAdicional.Rows.RemoveAt(gridTelefoneAdicional.CurrentRow.Index);
                else
                {
                    pessoaTelefone = new PessoaTelefone();
                    pessoaTelefone.Id = int.Parse(gridTelefoneAdicional.CurrentRow.Cells[0].Value.ToString());
                    pessoaTelefone = (PessoaTelefone)pessoaTelefoneController.selecionar(pessoaTelefone);
                    if (pessoaTelefone != null)
                    {
                        gridTelefoneAdicional.Rows.RemoveAt(gridTelefoneAdicional.CurrentRow.Index);
                        pessoaTelefoneController.excluir(pessoaTelefone);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void btnRemoverEndereco_Click(object sender, EventArgs e)
        {
            if (gridEnderecoAdicional.RowCount > 0)
            {
                if (int.Parse(gridEnderecoAdicional.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridEnderecoAdicional.Rows.RemoveAt(gridEnderecoAdicional.CurrentRow.Index);
                else
                {
                    endereco = new Endereco();
                    endereco.Id = int.Parse(gridEnderecoAdicional.CurrentRow.Cells[0].Value.ToString());
                    endereco = (Endereco)enderecoController.selecionar(endereco);
                    if (endereco != null)
                    {
                        gridEnderecoAdicional.Rows.RemoveAt(gridEnderecoAdicional.CurrentRow.Index);
                        enderecoController.excluir(endereco);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void btnRemoverPropriedade_Click(object sender, EventArgs e)
        {
            if (gridPropriedadeRural.RowCount > 0)
            {
                if (int.Parse(gridPropriedadeRural.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridPropriedadeRural.Rows.RemoveAt(gridPropriedadeRural.CurrentRow.Index);
                else
                {
                    pessoaPropriedade = new PessoaPropriedade();
                    pessoaPropriedade.Id = int.Parse(gridPropriedadeRural.CurrentRow.Cells[0].Value.ToString());
                    pessoaPropriedade = (PessoaPropriedade)pessoaPropriedadeController.selecionar(pessoaPropriedade);
                    if (pessoaPropriedade != null)
                    {
                        gridPropriedadeRural.Rows.RemoveAt(gridPropriedadeRural.CurrentRow.Index);
                        pessoaPropriedadeController.excluir(pessoaPropriedade);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void btnRemoverReferenciaComercial_Click(object sender, EventArgs e)
        {
            if (gridReferenciaComercial.RowCount > 0)
            {
                if (int.Parse(gridReferenciaComercial.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridReferenciaComercial.Rows.RemoveAt(gridReferenciaComercial.CurrentRow.Index);
                else
                {
                    pessoaReferenciaComercial = new PessoaReferenciaComercial();
                    pessoaReferenciaComercial.Id = int.Parse(gridReferenciaComercial.CurrentRow.Cells[0].Value.ToString());
                    pessoaReferenciaComercial = (PessoaReferenciaComercial)pessoaReferenciaComercialController.selecionar(pessoaReferenciaComercial);
                    if (pessoaReferenciaComercial != null)
                    {
                        gridReferenciaComercial.Rows.RemoveAt(gridReferenciaComercial.CurrentRow.Index);
                        pessoaReferenciaComercialController.excluir(pessoaReferenciaComercial);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void btnRemoverReferenciaPessoal_Click(object sender, EventArgs e)
        {
            if (gridReferenciaPessoal.RowCount > 0)
            {
                if (int.Parse(gridReferenciaPessoal.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridReferenciaPessoal.Rows.RemoveAt(gridReferenciaPessoal.CurrentRow.Index);
                else
                {
                    pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
                    pessoaReferenciaPessoal.Id = int.Parse(gridReferenciaPessoal.CurrentRow.Cells[0].Value.ToString());
                    pessoaReferenciaPessoal = (PessoaReferenciaPessoal)pessoaReferenciaPessoalController.selecionar(pessoaReferenciaPessoal);
                    if (pessoaReferenciaPessoal != null)
                    {
                        gridReferenciaPessoal.Rows.RemoveAt(gridReferenciaPessoal.CurrentRow.Index);
                        pessoaReferenciaPessoalController.excluir(pessoaReferenciaPessoal);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void FrmClienteCadastro_KeyDown(object sender, KeyEventArgs e)
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Pessoa();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Pessoa()
        {
            pessoa = new Pessoa();
            try
            {
                IList<PessoaTelefone> listaTelefone = new List<PessoaTelefone>();
                IList<Endereco> listaEndereco = new List<Endereco>();
                IList<PessoaPropriedade> listaPropriedades = new List<PessoaPropriedade>();
                IList<PessoaReferenciaPessoal> listaReferenciaPessoal = new List<PessoaReferenciaPessoal>();
                IList<PessoaReferenciaComercial> listaReferenciaComercial = new List<PessoaReferenciaComercial>();

                if (String.IsNullOrEmpty(txtCodCliente.Texts))
                    pessoa.Id = 0;
                else
                {
                    pessoa.Id = int.Parse(txtCodCliente.Texts);
                    pessoa = (Pessoa)pessoaController.selecionar(pessoa);
                }

                if (pessoa != null)
                {
                    if(pessoa.Id > 0)
                        try { pessoa.DataCadastro = DateTime.Parse(txtDataCadastro.Texts); } catch { }
                }
                //Tipo de Pessoa e função
                pessoa.Cliente = false;
                if (chkCliente.Checked == true)
                    pessoa.Cliente = true;
                pessoa.Fornecedor = false;
                if (chkFornecedor.Checked == true)
                    pessoa.Fornecedor = true;
                pessoa.Funcionario = false;
                if (chkFuncionario.Checked == true)
                    pessoa.Funcionario = true;
                pessoa.Transportadora = false;
                if (chkTransportadora.Checked == true)
                    pessoa.Transportadora = true;
                pessoa.Vendedor = false;
                if (chkVendedor.Checked == true)
                    pessoa.Vendedor = true;
                pessoa.Tecnico = false;
                if (chkTecnico.Checked == true)
                    pessoa.Tecnico = true;
                pessoa.Cobrador = false;
                if (chkCobrador.Checked == true)
                    pessoa.Cobrador = true;

                if (radioPF.Checked == true)
                    pessoa.TipoPessoa = "PF";
                else
                    pessoa.TipoPessoa = "PJ";

                if (chkSPC.Checked == true)
                    pessoa.RegistradoSpc = true;
                else
                    pessoa.RegistradoSpc = false;

                if (chkEscritorioCobranca.Checked == true)
                    pessoa.EscritorioCobranca = true;
                else
                    pessoa.EscritorioCobranca = false;

                //Dados Básicos
                pessoa.Cnpj = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim());
                pessoa.Email = txtEmail.Texts;
                pessoa.InscricaoEstadual = GenericaDesktop.RemoveCaracteres(txtInscricaoProdutor.Texts.Trim());
                pessoa.Mae = txtMae.Texts;
                pessoa.NomeFantasia = txtNomeFantasia.Texts.Trim();
                if (String.IsNullOrEmpty(pessoa.NomeFantasia))
                    pessoa.NomeFantasia = txtRazaoSocial.Texts.Trim();
                pessoa.Pai = txtPai.Texts.Trim();
                pessoa.RazaoSocial = txtRazaoSocial.Texts.Trim();
                pessoa.Rg = txtRG.Texts.Trim();
                pessoa.Sexo = "";
                if (radioMasculino.Checked == true)
                    pessoa.Sexo = "M";
                else if (radioFeminino.Checked == true)
                    pessoa.Sexo = "F";

                pessoa.DataNascimento = DateTime.Parse(txtDataNascimento.Value.ToString());
                pessoa.Observacoes = txtObservacoes.Texts;

                //Trabalho
                pessoa.LocalTrabalho = txtLocalTrabalho.Texts;
                pessoa.FuncaoTrabalho = txtFuncao.Texts;
                pessoa.TelefoneTrabalho = txtTelefoneTrabalho.Texts;
                if (!String.IsNullOrEmpty(txtSalario.Texts))
                    pessoa.SalarioTrabalho = txtSalario.Texts.Replace("R$", "");
                else
                    pessoa.SalarioTrabalho = "";
                pessoa.TempoTrabalho = txtDesdeQuando.Texts;
                pessoa.ContatoTrabalho = txtContato.Texts;
                decimal limite = 0;
          
                if (!String.IsNullOrEmpty(txtLimiteCredito.Texts))
                    limite = decimal.Parse(txtLimiteCredito.Texts.Replace("R$", ""));
                pessoa.LimiteCredito = limite;

                if (chkLembreteVencimento.Checked == true)
                    pessoa.ReceberLembrete = true;
                else
                    pessoa.ReceberLembrete = false;

                if (!String.IsNullOrEmpty(txtComissao.Texts))
                {
                    pessoa.ComissaoVendedor = double.Parse(txtComissao.Texts);
                }
                else
                    pessoa.ComissaoVendedor = 0;

                //Telefones
                if (gridTelefoneAdicional.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridTelefoneAdicional.Rows)
                    {
                        pessoaTelefone = new PessoaTelefone();
                        pessoaTelefone.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            pessoaTelefone.Id = int.Parse(col.Cells[0].Value.ToString());
                        pessoaTelefone.Ddd = GenericaDesktop.RemoveCaracteres(col.Cells[1].Value.ToString().Trim());
                        pessoaTelefone.Telefone = GenericaDesktop.RemoveCaracteres(col.Cells[2].Value.ToString().Trim());
                        pessoaTelefone.Observacoes = col.Cells[3].Value.ToString().Trim();
                        pessoaTelefone.Pessoa = pessoa;

                        listaTelefone.Add(pessoaTelefone);
                    }
                }

                //Dependentes
                IList<PessoaDependente> listaDependentes = new List<PessoaDependente>();
                if (gridDependentes.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridDependentes.Rows)
                    {
                        PessoaDependente pessoaDependente = new PessoaDependente();
                        pessoaDependente.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            pessoaDependente.Id = int.Parse(col.Cells[0].Value.ToString());
                        pessoaDependente.Cpf = GenericaDesktop.RemoveCaracteres(col.Cells[2].Value.ToString().Trim());
                        pessoaDependente.Telefone = GenericaDesktop.RemoveCaracteres(col.Cells[5].Value.ToString().Trim());
                        if (!String.IsNullOrEmpty(col.Cells[3].Value.ToString()))
                            pessoaDependente.DataNascimento = DateTime.Parse(col.Cells[3].Value.ToString());
                        pessoaDependente.Nome = col.Cells[1].Value.ToString();
                        pessoaDependente.Observacoes = col.Cells[6].Value.ToString();
                        pessoaDependente.Parentesco = col.Cells[4].Value.ToString();
                        pessoaDependente.Pessoa = pessoa;

                        listaDependentes.Add(pessoaDependente);
                    }
                }

                if (gridEnderecoAdicional.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridEnderecoAdicional.Rows)
                    {
                        endereco = new Endereco();
                        endereco.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            endereco.Id = int.Parse(col.Cells[0].Value.ToString());
                        endereco.Cep = GenericaDesktop.RemoveCaracteres(col.Cells[1].Value.ToString().Trim());
                        endereco.Logradouro = col.Cells[2].Value.ToString().Trim();
                        endereco.Numero = col.Cells[3].Value.ToString().Trim();
                        endereco.Complemento = col.Cells[4].Value.ToString().Trim();
                        endereco.Referencia = col.Cells[5].Value.ToString().Trim();
                        endereco.Bairro = col.Cells[9].Value.ToString().Trim();
                        //Cidade
                        cidade = new Cidade();
                        cidade.Id = int.Parse(col.Cells[6].Value.ToString());
                        endereco.Cidade = (Cidade)cidadeController.selecionar(cidade);
                        endereco.Pessoa = pessoa;

                        listaEndereco.Add(endereco);
                    }
                }

                //Propriedades
                if (gridPropriedadeRural.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridPropriedadeRural.Rows)
                    {
                        pessoaPropriedade = new PessoaPropriedade();
                        endereco = new Endereco();
                        pessoaPropriedade.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            pessoaPropriedade.Id = int.Parse(col.Cells[0].Value.ToString());
                        pessoaPropriedade.Descricao = col.Cells[1].Value.ToString();
                        pessoaPropriedade.InscricaoEstadual = col.Cells[2].Value.ToString().Trim();
                        endereco.Cep = col.Cells[3].Value.ToString();
                        endereco.Logradouro = col.Cells[4].Value.ToString();
                        endereco.Numero = col.Cells[5].Value.ToString();
                        endereco.Complemento = col.Cells[6].Value.ToString();
                        endereco.Referencia = col.Cells[7].Value.ToString();
                        endereco.Bairro = col.Cells[11].Value.ToString();
                        //Cidade
                        cidade = new Cidade();
                        cidade.Id = int.Parse(col.Cells[8].Value.ToString());
                        endereco.Cidade = (Cidade)cidadeController.selecionar(cidade);
                        Controller.getInstance().salvar(endereco);
                        pessoaPropriedade.Endereco = endereco;
                        pessoaPropriedade.Pessoa = pessoa;

                        listaPropriedades.Add(pessoaPropriedade);
                    }
                }

                //Referência Pessoal
                if (gridReferenciaPessoal.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridReferenciaPessoal.Rows)
                    {
                        pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
                        pessoaReferenciaPessoal.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            pessoaReferenciaPessoal.Id = int.Parse(col.Cells[0].Value.ToString());
                        pessoaReferenciaPessoal.Nome = col.Cells[1].Value.ToString();
                        pessoaReferenciaPessoal.Telefone = col.Cells[2].Value.ToString();
                        pessoaReferenciaPessoal.Grau = col.Cells[3].Value.ToString();
                        pessoaReferenciaPessoal.Observacoes = col.Cells[4].Value.ToString();
                        pessoaReferenciaPessoal.Pessoa = pessoa;

                        listaReferenciaPessoal.Add(pessoaReferenciaPessoal);
                    }
                }

                //Referência Comercial
                if (gridReferenciaComercial.RowCount > 0)
                {
                    foreach (DataGridViewRow col in gridReferenciaComercial.Rows)
                    {
                        pessoaReferenciaComercial = new PessoaReferenciaComercial();
                        pessoaReferenciaComercial.Id = 0;
                        if (int.Parse(col.Cells[0].Value.ToString()) > 0)
                            pessoaReferenciaComercial.Id = int.Parse(col.Cells[0].Value.ToString());
                        pessoaReferenciaComercial.Empresa = col.Cells[1].Value.ToString();
                        pessoaReferenciaComercial.Telefone = col.Cells[2].Value.ToString();
                        pessoaReferenciaComercial.Observacoes = col.Cells[3].Value.ToString();
                        pessoaReferenciaComercial.Pessoa = pessoa;

                        listaReferenciaComercial.Add(pessoaReferenciaComercial);
                    }
                }
                //Salvar
                PessoaBO pessoaBO = new PessoaBO();
                pessoaBO.salvarPessoaComItensAdicionais(pessoa, listaTelefone, listaEndereco, listaPropriedades, listaReferenciaPessoal, listaReferenciaComercial, listaDependentes);

                //Endereços Principal
                if (!String.IsNullOrEmpty(txtEndereco.Texts))
                {
                    if (String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        endereco = new Endereco();
                        endereco.Cep = GenericaDesktop.RemoveCaracteres(txtCEP.Texts.Trim());
                        endereco.Logradouro = txtEndereco.Texts.Trim();
                        if (string.IsNullOrEmpty(txtNumero.Texts))
                            txtNumero.Texts = "1";
                        endereco.Numero = txtNumero.Texts.Trim();
                        endereco.Complemento = txtComplemento.Texts.Trim();
                        endereco.Referencia = txtReferencia.Texts.Trim();
                        endereco.Bairro = txtBairro.Texts.Trim();
                        endereco.Cidade = cidadePrincipal;
                        endereco.Pessoa = pessoa;
                        Controller.getInstance().salvar(endereco);
                        pessoa.EnderecoPrincipal = endereco;
                    }
                    //Verificando se quando está editando a pessoa está no mesmo endereço ou se foi atualizado
                    else
                    {
                        
                        endereco = new Endereco();
                        if (pessoa.EnderecoPrincipal != null)
                        {
                            endereco = (Endereco)enderecoController.selecionar(pessoa.EnderecoPrincipal);
                        }
                        endereco.Cep = GenericaDesktop.RemoveCaracteres(txtCEP.Texts.Trim());
                        endereco.Logradouro = txtEndereco.Texts.Trim();
                        endereco.Numero = txtNumero.Texts.Trim();
                        endereco.Complemento = txtComplemento.Texts.Trim();
                        endereco.Referencia = txtReferencia.Texts.Trim();
                        endereco.Bairro = txtBairro.Texts.Trim();
                        endereco.Cidade = cidadePrincipal;
                        endereco.Pessoa = pessoa;
                        Controller.getInstance().salvar(endereco);
                        pessoa.EnderecoPrincipal = endereco;
                    }
                }
                //Telefone Principal
                if (!String.IsNullOrEmpty(txtDDD.Texts) || !String.IsNullOrEmpty(txtTelefonePrincipal.Texts))
                {
                    if (String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        pessoaTelefone = new PessoaTelefone();
                        pessoaTelefone.Ddd = GenericaDesktop.RemoveCaracteres(txtDDD.Texts.Trim());
                        pessoaTelefone.Telefone = GenericaDesktop.RemoveCaracteres(txtTelefonePrincipal.Texts.Trim());
                        pessoaTelefone.Observacoes = "";
                        pessoaTelefone.Pessoa = pessoa;
                        Controller.getInstance().salvar(pessoaTelefone);
                        pessoa.PessoaTelefone = pessoaTelefone;
                    }
                    else
                    {
                        pessoaTelefone = new PessoaTelefone();
                        if(pessoa.PessoaTelefone != null)
                            pessoaTelefone = (PessoaTelefone)pessoaTelefoneController.selecionar(pessoa.PessoaTelefone);

                        pessoaTelefone.Ddd = GenericaDesktop.RemoveCaracteres(txtDDD.Texts.Trim());
                        pessoaTelefone.Telefone = GenericaDesktop.RemoveCaracteres(txtTelefonePrincipal.Texts.Trim());
                        pessoaTelefone.Observacoes = "";
                        pessoaTelefone.Pessoa = pessoa;
                        Controller.getInstance().salvar(pessoaTelefone);
                        pessoa.PessoaTelefone = pessoaTelefone;
                    }
                }
                //Salva aqui o endereco principal e o telefone principal
                Controller.getInstance().salvar(pessoa);
                GenericaDesktop.ShowInfo("Registro salvo com Sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
                this.Close();
            }
        }

        private void txtLimiteCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtLimiteCredito.Texts, e);
        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtSalario.Texts, e);
        }

        private void txtSalario_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txtSalario.Texts);
                txtSalario.Texts = valor.ToString("C2", CultureInfo.CurrentCulture);
            }
            catch
            {

            }
        }

        private void txtLimiteCredito_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txtLimiteCredito.Texts);
                txtLimiteCredito.Texts = valor.ToString("C2", CultureInfo.CurrentCulture);
            }
            catch
            {

            }
        }

        private void get_Pessoa(Pessoa pessoa)
        {
            txtCodCliente.Texts = pessoa.Id.ToString();
            //Checkbox
            get_TipoPessoa(pessoa);
            //DADOS BASICOS
            get_DadosBasicos(pessoa);
            //ENDERECO PRINCIPAL
            get_EnderecoPrincipal(pessoa);

            //Grids
            get_referenciaComercial(pessoa);
            get_referenciaPessoa(pessoa);
            get_Propriedades(pessoa);
            get_EnderecoAdicional(pessoa);
            get_TelefoneAdicional(pessoa);
            get_Dependentes(pessoa);

            txtCodCliente.TextAlign = HorizontalAlignment.Center;
            txtUsuarioCadastro.TextAlign = HorizontalAlignment.Center;
            txtDataCadastro.TextAlign = HorizontalAlignment.Center;
            txtUltimaAlteracao.TextAlign = HorizontalAlignment.Center;

        }
        private void get_EnderecoPrincipal(Pessoa pessoa)
        { 
            if(pessoa.EnderecoPrincipal != null)
            {
                txtCEP.Texts = GenericaDesktop.MascaraCep(pessoa.EnderecoPrincipal.Cep);
                txtEndereco.Texts = pessoa.EnderecoPrincipal.Logradouro;
                txtNumero.Texts = pessoa.EnderecoPrincipal.Numero;
                txtComplemento.Texts = pessoa.EnderecoPrincipal.Complemento;
                txtReferencia.Texts = pessoa.EnderecoPrincipal.Referencia;
                txtBairro.Texts = pessoa.EnderecoPrincipal.Bairro;
                txtCidade.Texts = pessoa.EnderecoPrincipal.Cidade.Descricao;
                txtUF.Texts = pessoa.EnderecoPrincipal.Cidade.Estado.Uf;
                cidadePrincipal = pessoa.EnderecoPrincipal.Cidade;
                endereco = pessoa.EnderecoPrincipal;
            }
        }

        private void get_DadosBasicos(Pessoa pessoa)
        {
            //DADOS BASICOS
            if (pessoa.Cnpj.Length == 14)
                txtCNPJ.Texts = generica.FormatarCNPJ(pessoa.Cnpj);
            else if (pessoa.Cnpj.Length == 11)
                txtCNPJ.Texts = generica.FormatarCPF(pessoa.Cnpj);

            txtRazaoSocial.Texts = pessoa.RazaoSocial;
            txtNomeFantasia.Texts = pessoa.NomeFantasia;
            txtDataNascimento.Value = pessoa.DataNascimento;

            if (txtDataNascimento.Value.ToString().Equals("01/01/0001 00:00:00"))
            {
                txtDataNascimento.Value = DateTime.Parse("01/01/1900 00:00:00");
            }
            txtEmail.Texts = pessoa.Email;
            if (pessoa.PessoaTelefone != null)
            {
                txtDDD.Texts = pessoa.PessoaTelefone.Ddd;
                txtTelefonePrincipal.Texts = GenericaDesktop.MascaraTelefone(pessoa.PessoaTelefone.Telefone);
            }

            txtInscricaoProdutor.Texts = pessoa.InscricaoEstadual;
            txtRG.Texts = pessoa.Rg;
            txtObservacoes.Texts = pessoa.Observacoes;
            txtPai.Texts = pessoa.Pai;
            txtMae.Texts = pessoa.Mae;
            txtLocalTrabalho.Texts = pessoa.LocalTrabalho;
            txtFuncao.Texts = pessoa.FuncaoTrabalho;
            txtComissao.Texts = pessoa.ComissaoVendedor.ToString();

            txtDataCadastro.Texts = pessoa.DataCadastro.ToShortDateString();
            if(pessoa.OperadorCadastro != null)
                txtUsuarioCadastro.Texts = pessoa.OperadorCadastro.ToString();
            if (pessoa.DataAlteracao != null)
            {
                txtUltimaAlteracao.Texts = pessoa.DataAlteracao.ToShortDateString();
                if (txtUltimaAlteracao.Texts.Equals("01/01/0001"))
                    txtUltimaAlteracao.Texts = "";
                else
                    txtUltimaAlteracao.Texts = txtUltimaAlteracao.Texts + " USU: " + pessoa.OperadorAlteracao;
            }

            else
                txtUltimaAlteracao.Texts = "";

            if (!String.IsNullOrEmpty(pessoa.TelefoneTrabalho))
                txtTelefoneTrabalho.Texts = GenericaDesktop.MascaraTelefone(pessoa.TelefoneTrabalho);
            else
                txtTelefoneTrabalho.Texts = "";
            try { decimal valor = decimal.Parse(pessoa.SalarioTrabalho); txtSalario.Texts = valor.ToString("C2", CultureInfo.CurrentCulture); } catch { }
            try { txtLimiteCredito.Texts = pessoa.LimiteCredito.ToString("C2", CultureInfo.CurrentCulture); } catch { }

            if (pessoa.ReceberLembrete == true)
                chkLembreteVencimento.Checked = true;
            else
                chkLembreteVencimento.Checked = false;
            
        }

        private void get_TipoPessoa(Pessoa pessoa)
        {
            if (pessoa.Cliente == true)
                chkCliente.Checked = true;
            else
                chkCliente.Checked = false;
            if (pessoa.Fornecedor == true)
                chkFornecedor.Checked = true;
            else
                chkFornecedor.Checked = false;
            if (pessoa.Funcionario == true)
                chkFuncionario.Checked = true;
            else
                chkFuncionario.Checked = false;
            if (pessoa.Transportadora == true)
                chkTransportadora.Checked = true;
            else
                chkTransportadora.Checked = false;
            if (pessoa.Vendedor == true)
                chkVendedor.Checked = true;
            else
                chkVendedor.Checked = false;
            if (pessoa.Tecnico == true)
                chkTecnico.Checked = true;
            else
                chkTecnico.Checked = false;
            if (pessoa.Cobrador == true)
                chkCobrador.Checked = true;
            else
                chkCobrador.Checked = false;

            if (pessoa.TipoPessoa.Equals("PF"))
                radioPF.Checked = true;
            else
                radioPJ.Checked = true;

            if (pessoa.Sexo == "M")
                radioMasculino.Checked = true;
            else if (pessoa.Sexo == "F")
                radioFeminino.Checked = true;
            else
            {
                radioFeminino.Checked = false;
                radioMasculino.Checked = false;
            }

            //spc
            if (pessoa.RegistradoSpc == true)
                chkSPC.Checked = true;
            else
                chkSPC.Checked = false;
            //Escritorio Cobranca
            if (pessoa.EscritorioCobranca == true)
                chkEscritorioCobranca.Checked = true;
            else
                chkEscritorioCobranca.Checked = false;

        }

        private void get_referenciaPessoa(Pessoa pessoa)
        {
            IList<PessoaReferenciaPessoal> listaReferencia = pessoaReferenciaPessoalController.selecionarReferenciaPessoalPorPessoa(pessoa.Id);
            if(listaReferencia.Count > 0)
            {
                foreach(PessoaReferenciaPessoal pessoaReferenciaPessoal in listaReferencia)
                {
                    inserirReferenciaPessoalGrid(pessoaReferenciaPessoal);
                }
            }
        }

        private void get_referenciaComercial(Pessoa pessoa)
        {
            IList<PessoaReferenciaComercial> listaReferencia = pessoaReferenciaComercialController.selecionarReferenciaComercialPorPessoa(pessoa.Id);
            if (listaReferencia.Count > 0)
            {
                foreach (PessoaReferenciaComercial pessoaReferenciaComercial in listaReferencia)
                {
                    inserirReferenciaComercialGrid(pessoaReferenciaComercial);
                }
            }
        }

        private void get_Propriedades(Pessoa pessoa)
        {
            IList<PessoaPropriedade> listaPropriedades = pessoaPropriedadeController.selecionarPropriedadesPorPessoa(pessoa.Id);
            if (listaPropriedades.Count > 0)
            {
                foreach (PessoaPropriedade pessoaPropriedade in listaPropriedades)
                {
                    inserirPropriedadeGrid(pessoaPropriedade);
                }
            }
        }

        private void get_EnderecoAdicional(Pessoa pessoa)
        {
            IList<Endereco> listaEndereco = enderecoController.selecionarEnderecoPorPessoa(pessoa.Id);
            if (listaEndereco.Count > 0)
            {
                foreach (Endereco endereco in listaEndereco)
                {
                    if (pessoa.EnderecoPrincipal != null)
                    {
                        if (endereco.Id != pessoa.EnderecoPrincipal.Id)
                            inserirEnderecoGrid(endereco);
                    }
                }
            }
        }

        private void get_TelefoneAdicional(Pessoa pessoa)
        {
            IList<PessoaTelefone> listaTelefones = pessoaTelefoneController.selecionarTelefonePorPessoa(pessoa.Id);
            if (listaTelefones.Count > 0)
            {
                foreach (PessoaTelefone telefone in listaTelefones)
                {
                    if (pessoa.PessoaTelefone != null)
                    {
                        if (telefone.Id != pessoa.PessoaTelefone.Id)
                            inserirTelefoneGrid(telefone);
                    }
                }
            }
        }

        private void get_Dependentes(Pessoa pessoa)
        {
            PessoaDependenteController pessoaDependenteController = new PessoaDependenteController();
            IList<PessoaDependente> listaDependentes = pessoaDependenteController.selecionarDependentePorPessoa(pessoa.Id);
            if (listaDependentes.Count > 0)
            {
                foreach (PessoaDependente pessoaDependente in listaDependentes)
                {
                        inserirDependenteGrid(pessoaDependente);
                }
            }
        }

        private void txtTelefoneTrabalho_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTelefoneTrabalho.Texts))
                txtTelefoneTrabalho.Texts = GenericaDesktop.MascaraTelefone(txtTelefoneTrabalho.Texts);
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

        private void btnNovoDependente_Click(object sender, EventArgs e)
        {
            PessoaDependente pessoaDependente = new PessoaDependente();
            Form formBackground = new Form();
            try
            {
                using (FrmCadastroDependente uu = new FrmCadastroDependente())
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
                    switch (uu.showModal(ref pessoaDependente))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                                inserirDependenteGrid(pessoaDependente);
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

        private void btnExcluirDependente_Click(object sender, EventArgs e)
        {
            if (gridDependentes.RowCount > 0)
            {
                if (int.Parse(gridDependentes.CurrentRow.Cells[0].Value.ToString()) == 0)
                    gridDependentes.Rows.RemoveAt(gridDependentes.CurrentRow.Index);
                else
                {
                    PessoaDependente pessoaDependente = new PessoaDependente();
                    pessoaDependente.Id = int.Parse(gridDependentes.CurrentRow.Cells[0].Value.ToString());
                    pessoaDependente = (PessoaDependente)PessoaDependenteController.getInstance().selecionar(pessoaDependente);
                    if (pessoaDependente != null)
                    {
                        gridDependentes.Rows.RemoveAt(gridDependentes.CurrentRow.Index);
                        PessoaDependenteController.getInstance().excluir(pessoaDependente);
                        GenericaDesktop.ShowInfo("Registro excluído com sucesso!");
                    }
                }
            }
        }

        private void btnEditarDependente_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(gridDependentes.CurrentRow.Cells[0].Value.ToString()) > 0)
                {
                    PessoaDependenteController pessoaDependenteController = new PessoaDependenteController();
                    PessoaDependente pessoaDependente = new PessoaDependente();
                    pessoaDependente.Id = int.Parse(gridDependentes.CurrentRow.Cells[0].Value.ToString());
                    pessoaDependente = (PessoaDependente)pessoaDependenteController.selecionar(pessoaDependente);
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmCadastroDependente uu = new FrmCadastroDependente(pessoaDependente))
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
                            switch (uu.showModal(ref pessoaDependente))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    break;
                                case DialogResult.OK:
                                    gridDependentes.Rows.RemoveAt(gridDependentes.CurrentRow.Index);
                                    inserirDependenteGrid(pessoaDependente);
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
                else
                {
                    GenericaDesktop.ShowAlerta("Para editar este cadastro você deve primeiro salvar o cadastro do cliente ou então remover o dependente e adicionar corretamente!");
                }
            }
            catch
            {

            }
        }

        private void chkVendedor_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkVendedor.Checked == true)
                {
                    lblComissao.Visible = true;
                    txtComissao.Visible = true;
                }
                else
                {
                    lblComissao.Visible = false;
                    txtComissao.Visible = false;
                }
            }
            catch
            {

            }
        }

        private void txtComissao_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtComissao.Texts, e);
        }

        private void btnPesquisaCep_Click(object sender, EventArgs e)
        {
            //pesquisarCEP();
            pesquisaCepAPIAsync();
        }

        private void btnSPCBrasil_Click(object sender, EventArgs e)
        {
            consultaSPC();
        }
        private async void consultaSPC()
        {
            // Exibir o formulário de aguarde
            FrmAguarde formAguarde = new FrmAguarde();
            formAguarde.Show();

            try
            {
                string usuario = Sessao.parametroSistema.UsuarioWebServiceSpcBrasil;
                string senha = GenericaDesktop.Descriptografa(Sessao.parametroSistema.SenhaWebServiceSpcBrasil);

                consultaSPCBrasil.consultaWebServiceClient client = new consultaWebServiceClient();
                client.ClientCredentials.UserName.UserName = usuario;
                client.ClientCredentials.UserName.Password = senha;
                if(Sessao.parametroSistema.AmbienteSpcBrasil.Equals("PRODUCAO"))
                    client.Endpoint.Address = new EndpointAddress("https://servicos.spc.org.br/spc/remoting/ws/consulta/consultaWebService");
                else
                    client.Endpoint.Address = new EndpointAddress("https://treina.spc.org.br:443/spc/remoting/ws/consulta/consultaWebService");

                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                binding.SendTimeout = TimeSpan.FromMinutes(5);
                binding.MaxReceivedMessageSize = 10485760;

                CustomMessageInspector messageInspector = new CustomMessageInspector();
                client.Endpoint.Behaviors.Add(new CustomEndpointBehavior(messageInspector));

                client.Endpoint.Binding = binding;
                Lunar.consultaSPCBrasil.FiltroConsulta filtroConsulta = new Lunar.consultaSPCBrasil.FiltroConsulta();
                
                //Pessoa fisica ou juridica
                if (GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()).Length == 11)
                {
                    filtroConsulta.tipoconsumidorSpecified = true;
                    filtroConsulta.tipoconsumidor = TipoPessoa.F;
                    filtroConsulta.documentoconsumidor = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim());
                    filtroConsulta.codigoproduto = Sessao.parametroSistema.ConsultaPadraoSpcBrasil;
                }
                else if (GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim()).Length == 14)
                {
                    filtroConsulta.tipoconsumidorSpecified = true;
                    filtroConsulta.tipoconsumidor = TipoPessoa.J;
                    filtroConsulta.documentoconsumidor = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts.Trim());
                    filtroConsulta.codigoproduto = "14";
                }

                    // Executar a consulta em uma thread separada usando async/await
                Lunar.consultaSPCBrasil.ResultadoConsulta res = await Task.Run(() => client.consultar(filtroConsulta));

                // Esconder o formulário de aguarde
                formAguarde.Close();

                FrmResultadoConsultaSPC frmResultadoConsultaSPC = new FrmResultadoConsultaSPC(res);
                frmResultadoConsultaSPC.ShowDialog();
            }
            catch (FaultException er)
            {
                // Esconder o formulário de aguarde em caso de erro
                formAguarde.Close();

                GenericaDesktop.ShowAlerta(er.Message);
            }
            catch (Exception erro)
            {
                // Esconder o formulário de aguarde em caso de erro
                formAguarde.Close();

                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }
    }
}
