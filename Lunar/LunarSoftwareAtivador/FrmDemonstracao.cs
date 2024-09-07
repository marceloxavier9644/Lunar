using Lunar;
using Lunar.Telas.Cadastros.Cidades;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Utils;
using Lunar.Utils.LunarChatIntegracao;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using static LunarBase.Utilidades.Ns_ConsultaCNPJ;
using static LunarSoftwareAtivador.FrmAtivador;
using Button = System.Windows.Forms.Button;
using Cidade = LunarBase.Classes.Cidade;
using Endereco = LunarBase.Classes.Endereco;
using Estado = LunarBase.Classes.Estado;
using TextBox = System.Windows.Forms.TextBox;
using ToolTip = System.Windows.Forms.ToolTip;

namespace LunarSoftwareAtivador
{
    public partial class FrmDemonstracao : Form
    {
        string cnae = "";
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        Thread th;
        ToolTip toolTip = new ToolTip();
        private Button closeButton;
        private Color borderColor = Color.FromArgb(123, 19, 255);
        private Color closeButtonColor = Color.FromArgb(123, 19, 255);
        private MySQLManager dbManager;
        public FrmDemonstracao()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Remove a borda padrão
            this.DoubleBuffered = true; // Melhora a performance de desenho

            InitializeCloseButton();
            InitializeCustomPaint();

            dbManager = new MySQLManager();

            //Banco local Servidor
            Sessao.servidor = "localhost";
            Sessao.usuarioBanco = "marcelo";
            Sessao.senhaBanco = "mx123";
            Sessao.nomeBanco = "lunar";
            SetupDatabaseConfiguration();

            toolTip.ToolTipTitle = "Informação";
            toolTip.UseFading = true;
            toolTip.UseAnimation = true;
            toolTip.IsBalloon = true;
            toolTip.ShowAlways = true;
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.SetToolTip(this.txtCnpjRepresentante, "Digite o CNPJ do representante");


            ExecutionLimiter executionLimiter = new ExecutionLimiter();

            if (!executionLimiter.CanExecute())
            {
                GenericaDesktop.ShowAlerta("Falha ao efetuar registro da empresa, entre em contato com o suporte técnico! Erro: x85477");
                enviarMensagemPeloWhats_TentativaUsoInadequando();
                Application.Exit();
            }
            else
            {
                executionLimiter.IncrementExecutionCount();
                // Continue a inicialização do formulário principal
            }

            if (File.Exists("C:\\Lunar\\Terminal.txt"))
            {
                chkTerminal.Checked = true;
            }
            else
            {
                chkServidor.Checked = true;
            }
        }
        private async Task cadastrarEstadoCidades()
        {
            try
            {
                Generica generica = new Generica();
                EstadoController estadoController = new EstadoController();
                CidadeController cidadeController = new CidadeController();

                // Inicia a transação
                Conexao.IniciaTransacao();

                // Cadastrar Estados
                IList<ObjetoPadrao> lisEst = await Task.Run(() => estadoController.selecionarTodos(new Estado()));
                if (lisEst.Count == 0)
                {
                    var listaEstados = Estados.BuscarEstados();
                    foreach (Estados estados in listaEstados)
                    {
                        Estado estado = new Estado
                        {
                            Descricao = generica.RemoverAcentos(estados.Nome.ToUpper()),
                            Uf = estados.Sigla.ToUpper(),
                            Ibge = estados.Id.ToString()
                        };
                        Controller.getInstance().salvar(estado);
                    }
                }

                // Garantir que todos os estados foram cadastrados antes de continuar
                lisEst = await Task.Run(() => estadoController.selecionarTodos(new Estado()));
                if (lisEst.Count == 0)
                {
                    throw new Exception("Erro ao cadastrar estados. Verifique os dados ou a conexão com o banco de dados.");
                }

                // Cadastrar Cidades
                var listaMunicipios = Municipios.BuscarMunicipios();
                foreach (Municipios municipio in listaMunicipios)
                {
                    //lblStatus.Text = "Cadastro de cidades: " + generica.RemoverAcentos(municipio.Nome);

                    Cidade cidade = new Cidade
                    {
                        Descricao = generica.RemoverAcentos(municipio.Nome.ToUpper()),
                        Estado = await Task.Run(() => estadoController.selecionarEstadoPorUF(municipio.Microrregiao.Mesorregiao.Uf.Sigla)),
                        Ibge = municipio.Id.ToString()
                    };
                    await Task.Run(() => Controller.getInstance().salvar(cidade));
                }

                // Commit na transação
                Conexao.Commit();

                // Atualizações na UI
                //lblStatus.Text = "Verificando dados...";
                //lblStatus.Visible = false;

                // Fechar conexão de banco de dados
                Conexao.FechaConexaoBD();
            }
            catch (MySqlException mysqlEx)
            {
                Conexao.RollBack();
                MessageBox.Show($"Erro MySQL: {mysqlEx.Message}");
                // Log mysqlEx aqui
            }
            catch (Exception ex)
            {
                Conexao.RollBack();
                MessageBox.Show($"Erro geral: {ex.Message}");
                // Log ex aqui
            }
        }


        private void SetupDatabaseConfiguration()
        {
            Task.Run(async () =>
            {
                await ConfiguraBancoDadosAsync();
            });
        }
        private async Task ConfiguraBancoDadosAsync()
        {
            lblStatus.Text = "Inicio";
            ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
            Controller.getInstanceAtualiza();
            valoresPadraoBO.gerarValoresPadrao();
            lblStatus.Text = "Fim";
        }
        private void InitializeCloseButton()
        {
            closeButton = new Button();
            closeButton.BackColor = closeButtonColor;
            closeButton.Size = new Size(30, 30); // Tamanho do botão
            closeButton.Location = new Point(this.Width - 35, 5); // Posição na borda superior direita
            closeButton.FlatStyle = FlatStyle.Flat; // Estilo do botão
            closeButton.FlatAppearance.BorderSize = 0; // Remove a borda do botão
            closeButton.ForeColor = Color.White; // Cor do texto
            closeButton.Text = "X"; // Texto do botão
            closeButton.Font = new Font("Arial", 10, FontStyle.Bold); // Fonte do texto
            closeButton.Click += CloseButton_Click; // Evento de clique para fechar o formulário
            closeButton.Cursor = Cursors.Hand;
            this.Controls.Add(closeButton); // Adiciona o botão ao formulário
        }

        private void InitializeCustomPaint()
        {
            this.Paint += FrmDemonstracao_Paint;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Desenha a borda personalizada
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                borderColor, 2, ButtonBorderStyle.Solid,
                borderColor, 2, ButtonBorderStyle.Solid,
                borderColor, 2, ButtonBorderStyle.Solid,
                borderColor, 2, ButtonBorderStyle.Solid);
        }
        private void FrmDemonstracao_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                        borderColor, 2, ButtonBorderStyle.Solid,
                        borderColor, 2, ButtonBorderStyle.Solid,
                        borderColor, 2, ButtonBorderStyle.Solid,
                        borderColor, 2, ButtonBorderStyle.Solid);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário quando o botão de fechar é clicado
        }

        private void chkTermosUso_CheckedChanged(object sender, EventArgs e)
        {

        }

        private bool verificarSeJaPossuiCadastroNoPainel()
        {
            string cnpj = txtCnpj.Text.Trim();
            DataRow usuario = dbManager.ConsultarUsuarioPorCNPJ(cnpj);
            if (usuario != null)
                return true;
            else
                return false;
        }
        private async void txtCnpj_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCnpj.Text))
            {
                bool cnpjValido = GenericaDesktop.validarCPFCNPJ(txtCnpj.Text);
                if (cnpjValido == false)
                {
                    GenericaDesktop.ShowAlerta("CNPJ Inválido!");
                    txtCnpj.Text = "";
                }
                else
                {
                    lblLoading.Text = "Buscando dados, aguarde...";
                    lblLoading.Visible = true; // Mostra a mensagem de carregamento
                    lblLoading.Refresh(); // Atualiza a exibição do controle
                    this.UseWaitCursor = true; // Muda o cursor para "espera"
                    try
                    {
                        GenericaDesktop generica = new GenericaDesktop();
                        ConsultEmpresaNs empr = await Task.Run(() =>
                            generica.consultarEmpresaPorCnpj_NS("28145398000173", Generica.RemoveCaracteres(txtCnpj.Text.Trim()), "MG")
                        );

                        if (empr != null && empr.retConsCad.infCons.infCad != null)
                        {
                            txtRazaoSocial.Text = empr.retConsCad.infCons.infCad[0].xNome;
                            txtNomeFantasia.Text = empr.retConsCad.infCons.infCad[0].xFant;
                            txtCep.Text = empr.retConsCad.infCons.infCad[0].ender.CEP;
                            txtEndereco.Text = empr.retConsCad.infCons.infCad[0].ender.xLgr;
                            txtNumero.Text = empr.retConsCad.infCons.infCad[0].ender.nro;
                            txtComplemento.Text = empr.retConsCad.infCons.infCad[0].ender.xCpl;
                            txtBairro.Text = empr.retConsCad.infCons.infCad[0].ender.xBairro;
                            txtIe.Text = empr.retConsCad.infCons.infCad[0].IE;
                            cnae = empr.retConsCad.infCons.infCad[0].CNAE.ToString();

                            CidadeController cidadeController = new CidadeController();
                            Cidade cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(empr.retConsCad.infCons.infCad[0].ender.xMun, empr.retConsCad.infCons.infCad[0].ender.cMun);
                            if (cidade != null)
                            {
                                txtCidade.Text = cidade.Descricao;
                                txtUf.Text = cidade.Estado.Uf;
                            }
                            txtEmail.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        GenericaDesktop.ShowAlerta($"Erro ao buscar dados na receita: {ex.Message}");
                    }
                    finally
                    {
                        lblLoading.Visible = false; // Esconde a mensagem de carregamento
                        this.UseWaitCursor = false; // Restaura o cursor padrão
                    }
                }
            }
        }

        public void consultarRetornoAPI(string chavePainel)
        {
            string url = "https://lunarsoftware.com.br/painel/api/api-auth.php";

            OperatingSystem os = Environment.OSVersion;
            Version version = os.Version;

            if (os.Platform == PlatformID.Win32NT && version.Major == 6 && version.Minor == 1)
            {
                url = "http://lunarsoftware.com.br/painel/api/api-auth.php";
            }

            try
            {
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        cnpj = txtCnpj.Text,
                        key = chavePainel
                    });

                    streamWriter.Write(json);

                }
                FrmAtivador frm = new FrmAtivador();
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = JsonConvert.DeserializeObject<LunarClienteAtivo>(result);
                    if (!String.IsNullOrEmpty(retorno.licence_expiration) && retorno.is_blocked == false)
                    {
                        if (DateTime.Parse(retorno.licence_expiration) >= DateTime.Now)
                        {
                            Random rnd = new Random();
                            //se o numero for entre 100 e 499 nao está bloqueado
                            string block = frm.Criptografa(frm.Criptografa(rnd.Next(100, 499).ToString()));
                            //se o numero for entre 500 e 999 nao está bloqueado
                            if (retorno.is_blocked == true)
                                block = frm.Criptografa(frm.Criptografa(rnd.Next(500, 999).ToString()));

                            gerarXMLConfigPC(DateTime.Parse(retorno.licence_expiration), frm.GetHDDSerialNumber("C"), block, chavePainel);
                        }
                    }
                    if (retorno.msg.Equals("ERR_INVALID_USER"))
                    {
                        GenericaDesktop.ShowErro("Serial do Painel ou CNPJ Inválido!");
                    }
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
            }
        }
        public void gerarXMLConfigPC(DateTime validade, string serialHD, string block, string chavePainel)
        {
            try
            {
                string servidor = "localhost";
                if (chkTerminal.Checked == true)
                    servidor = txtServidor.Text.Trim();
                string dateToEncrypt = validade.ToShortDateString();
                var (key, iv) = CryptoUtils.GenerateKeyAndIV();
                File.WriteAllBytes("lunar.bin", key);
                File.WriteAllBytes("lunarSoftware.bin", iv);
                byte[] encryptedDate = CryptoUtils.EncryptDateString(dateToEncrypt, key, iv);
                //Console.WriteLine("Data Criptografada: " + Convert.ToBase64String(encryptedDate));

                FrmAtivador frm = new FrmAtivador();
                    XmlTextWriter xmlWriter = new XmlTextWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml", null);
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("MXSystem");
                    xmlWriter.WriteElementString("AppVersion", frm.Criptografa("Fé, saúde e trabalho"));
                    xmlWriter.WriteElementString("AppData", frm.Criptografa("lunar"));
                    xmlWriter.WriteElementString("AppTime", frm.Criptografa("Com DEUS tudo é possível"));
                    xmlWriter.WriteElementString("AppUser", Convert.ToBase64String(encryptedDate));
                    xmlWriter.WriteElementString("AppServ", frm.Criptografa(serialHD));
                    xmlWriter.WriteElementString("AppComon", block);
                    xmlWriter.WriteElementString("AppOper", frm.Criptografa(txtCnpj.Text));
                    xmlWriter.WriteElementString("AppClient", frm.Criptografa(chavePainel));
                    xmlWriter.WriteElementString("Servidor", servidor);
                    xmlWriter.WriteElementString("Usuario", "marcelo");
                    xmlWriter.WriteElementString("Senha", frm.Criptografa("mx123"));
                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Verifique as permissões da pasta do sistema\n" + erro.Message);
            }
        }
        private async Task enviarMensagemPeloWhats()
        {
            var client = new LunarChatMensagem.EnviarMensagemWhatsapp();
            await client.SendMessageAdminAsync("5538997425754", "Cadastro realizado no Lunar Software Demonstração. Responsável: " + txtResponsavel.Text + " Cel: " + txtCelular.Text + " CNPJ: " + txtCnpj.Text + " Fantasia: " + txtNomeFantasia.Text + " Segmento: " + txtSegmento.Text);
        }

        private void enviarMensagemPeloWhats_TentativaUsoInadequando()
        {
            var client = new LunarChatMensagem.EnviarMensagemWhatsapp();
            client.SendMessageAdminAsync("5538997425754", "Tentativa de ativar várias vezes. Responsável: " + txtResponsavel.Text + " Cel: " + txtCelular.Text + " CNPJ: " + txtCnpj.Text + " Fantasia: " + txtNomeFantasia.Text + " Segmento: " + txtSegmento.Text);
        }
        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (chkTermosUso.Checked == true)
            {
                btnConfirmar.Enabled = false;
                if (ValidarCampos())
                {
                    int idRepresentante = capturarRepresentante();
                    // Capturar os dados do formulário
                    ClientePainel cliente = new ClientePainel
                    {
                        Nome = txtRazaoSocial.Text,
                        Email = txtEmail.Text,
                        CNPJ = txtCnpj.Text,
                        IE = txtIe.Text,
                        IEFree = 0,
                        RazaoSocial = txtRazaoSocial.Text,
                        NomeFantasia = txtNomeFantasia.Text,
                        CEP = txtCep.Text,
                        Endereco = txtEndereco.Text,
                        Numero = txtNumero.Text,
                        Complemento = txtComplemento.Text,
                        UF = txtUf.Text,
                        Cidade = txtCidade.Text,
                        Telefone = txtCelular.Text,
                        IdRepresentante = idRepresentante,
                        Bairro = txtBairro.Text
                    };
                    await enviarMensagemPeloWhats();
                    // Cadastrar o cliente no painel online Lunar Software
                    bool cadastroSucesso = false;
                    if (verificarSeJaPossuiCadastroNoPainel())
                    {
                        cadastroLocalCasoTenhaCadastroOnlineJa();
                    }
                    else
                    {
                        string callbackUrl = "https://lunarsoftware.com.br/painel/cadastro-sucesso.html";
                        OperatingSystem os = Environment.OSVersion;
                        Version version = os.Version;

                        if (os.Platform == PlatformID.Win32NT && version.Major == 6 && version.Minor == 1)
                        {
                            callbackUrl = "http://lunarsoftware.com.br/painel/cadastro-sucesso.html";
                        }
                        cadastroSucesso = await ClienteService.CadastrarClienteAsync(cliente, callbackUrl);

                        if (cadastroSucesso)
                        {
                            string chave = retornarLicencaPainel();
                            if (!String.IsNullOrEmpty(chave))
                            {
                                //Gera arquivo de config
                                consultarRetornoAPI(chave);

                                Empresa empresa = new Empresa();
                                EmpresaFilial empresaFilial = new EmpresaFilial();
                                empresa.Id = 1;
                                empresa.Cnpj = cliente.CNPJ;
                                empresa.Responsavel = txtResponsavel.Text;
                                empresa.CpfResponsavel = txtCpfResponsavel.Text;
                                empresa.ValidadeLicenca = DateTime.Today.AddDays(7);
                                empresa.Contador = "";
                                empresa.RazaoSocial = txtRazaoSocial.Text;

                                empresa.CrcContador = "";
                                empresa.DataAbertura = DateTime.Now;
                                if (!string.IsNullOrEmpty(txtCelular.Text) && txtCelular.Text.Length >= 2)
                                {
                                    empresa.DddPrincipal = txtCelular.Text.Substring(0, 2);
                                    empresa.TelefonePrincipal = txtCelular.Text.Substring(2);
                                }
                                else
                                {
                                    empresa.DddPrincipal = string.Empty;
                                    empresa.TelefonePrincipal = txtCelular.Text;
                                }
                                empresa.TelefoneSecundario = "";
                                empresa.DddSecundario = "";
                                empresa.Email = txtEmail.Text;
                                empresa.EmailContador = "";
                                Endereco endereco = new Endereco();
                                endereco.Logradouro = txtEndereco.Text;
                                endereco.Numero = txtNumero.Text;
                                endereco.Pessoa = null;
                                endereco.EmpresaFilial = null;
                                endereco.Complemento = txtComplemento.Text;
                                if (!String.IsNullOrEmpty(txtCidade.Text))
                                {
                                    CidadeController cidadeController = new CidadeController();
                                    endereco.Cidade = new Cidade();
                                    endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEUf(txtCidade.Text, txtUf.Text);
                                }
                                endereco.Cep = GenericaDesktop.RemoveCaracteres(txtCep.Text);
                                endereco.Bairro = txtBairro.Text;
                                endereco.Pessoa = null;
                                endereco.EmpresaFilial = null;
                                Controller.getInstance().salvar(endereco);

                                empresa.Endereco = endereco;
                                if (endereco != null)
                                {
                                    if (endereco.Id > 0)
                                    {
                                        empresaFilial = new EmpresaFilial();
                                        empresaFilial.Id = 1;
                                        empresaFilial.Cnae = cnae;
                                        empresaFilial.Cnpj = GenericaDesktop.RemoveCaracteres(txtCnpj.Text);
                                        empresaFilial.DataAbertura = DateTime.Now;
                                        empresaFilial.DddPrincipal = empresa.DddPrincipal;
                                        empresaFilial.TelefonePrincipal = empresa.TelefonePrincipal;
                                        empresaFilial.DddSecundario = "";
                                        empresaFilial.TelefoneSecundario = "";
                                        empresaFilial.Email = txtEmail.Text;
                                        empresaFilial.InscricaoEstadual = txtIe.Text;
                                        empresaFilial.NomeFantasia = txtNomeFantasia.Text;
                                        empresaFilial.RazaoSocial = txtRazaoSocial.Text;
                                        empresaFilial.Endereco = endereco;
                                        empresaFilial.Empresa = empresa;
                                        empresaFilial.Otica = true;
                                        //empresaFilial.SenhaCertificado = GenericaDesktop.Criptografa(txtSenhaCertificado.Texts);

                                        RegimeEmpresa regimeTributario = new RegimeEmpresa();
                                        regimeTributario.Id = int.Parse("1");
                                        regimeTributario = (RegimeEmpresa)Controller.getInstance().selecionar(regimeTributario);
                                        empresaFilial.RegimeEmpresa = regimeTributario;
                                        Controller.getInstance().salvar(empresa);
                                        Controller.getInstance().salvar(empresaFilial);

                                        endereco.EmpresaFilial = empresaFilial;
                                        Controller.getInstance().salvar(endereco);
                                        Sessao.empresaFilialLogada = empresaFilial;
                                    }
                                }
                                GenericaDesktop.ShowInfo("Cadastro realizado com sucesso, agora cadastre um usuário para acessar o sistema!");
                                chamarFormCadastroUsuario();
                                this.Close();
                                th = new Thread(opennewform);
                                th.SetApartmentState(ApartmentState.STA);
                                th.Start();
                            }
                            else
                                GenericaDesktop.ShowErro("Cadastro não foi finalizado, entre em contato com a Lunar Software!");
                        }
                        else
                        {
                            GenericaDesktop.ShowErro("Falha ao cadastrar o cliente. Verifique os dados e tente novamente.");
                        }
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Você deve ler e aceitar os termos de uso para confirmar!");
            }
        }

        private void chamarFormCadastroUsuario()
        {
            EmpresaFilial empresaFilial = new EmpresaFilial();
            empresaFilial.Id = 1;
            empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
            Sessao.empresaFilialLogada = empresaFilial;

            GrupoUsuario grupoUsuario = new GrupoUsuario();
            grupoUsuario.Id = 1;
            grupoUsuario = (GrupoUsuario)Controller.getInstance().selecionar(grupoUsuario);

            Form formBackground = new Form();
            FrmUsuarioCadastro uu = new FrmUsuarioCadastro(grupoUsuario);
            formBackground.StartPosition = FormStartPosition.Manual;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
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
        private void cadastroLocalCasoTenhaCadastroOnlineJa()
        {
            string chave = retornarLicencaPainel();
            if (!String.IsNullOrEmpty(chave))
            {
                //Gera arquivo de config
                consultarRetornoAPI(chave);

                if (chkServidor.Checked == true)
                {
                    Empresa empresa = new Empresa();
                    EmpresaFilial empresaFilial = new EmpresaFilial();
                    empresa.Id = 1;
                    empresa.Cnpj = txtCnpj.Text;
                    empresa.Responsavel = txtResponsavel.Text;
                    empresa.CpfResponsavel = txtCpfResponsavel.Text;
                    empresa.ValidadeLicenca = DateTime.Today.AddDays(7);
                    empresa.Contador = "";
                    empresa.RazaoSocial = txtRazaoSocial.Text;

                    empresa.CrcContador = "";
                    empresa.DataAbertura = DateTime.Now;
                    if (!string.IsNullOrEmpty(txtCelular.Text) && txtCelular.Text.Length >= 2)
                    {
                        empresa.DddPrincipal = txtCelular.Text.Substring(0, 2);
                        empresa.TelefonePrincipal = txtCelular.Text.Substring(2);
                    }
                    else
                    {
                        empresa.DddPrincipal = string.Empty;
                        empresa.TelefonePrincipal = txtCelular.Text;
                    }
                    empresa.TelefoneSecundario = "";
                    empresa.DddSecundario = "";
                    empresa.Email = txtEmail.Text;
                    empresa.EmailContador = "";
                    Endereco endereco = new Endereco();
                    endereco.Logradouro = txtEndereco.Text;
                    endereco.Numero = txtNumero.Text;
                    endereco.Pessoa = null;
                    endereco.EmpresaFilial = null;
                    endereco.Complemento = txtComplemento.Text;
                    if (!String.IsNullOrEmpty(txtCidade.Text))
                    {
                        CidadeController cidadeController = new CidadeController();
                        endereco.Cidade = new Cidade();
                        endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEUf(txtCidade.Text, txtUf.Text);
                    }
                    endereco.Cep = GenericaDesktop.RemoveCaracteres(txtCep.Text);
                    endereco.Bairro = txtBairro.Text;
                    endereco.Pessoa = null;
                    endereco.EmpresaFilial = null;
                    Controller.getInstance().salvar(endereco);

                    empresa.Endereco = endereco;
                    if (endereco != null)
                    {
                        if (endereco.Id > 0)
                        {
                            empresaFilial = new EmpresaFilial();
                            empresaFilial.Id = 1;
                            empresaFilial.Cnae = cnae;
                            empresaFilial.Cnpj = GenericaDesktop.RemoveCaracteres(txtCnpj.Text);
                            empresaFilial.DataAbertura = DateTime.Now;
                            empresaFilial.DddPrincipal = empresa.DddPrincipal;
                            empresaFilial.TelefonePrincipal = empresa.TelefonePrincipal;
                            empresaFilial.DddSecundario = "";
                            empresaFilial.TelefoneSecundario = "";
                            empresaFilial.Email = txtEmail.Text;
                            empresaFilial.InscricaoEstadual = txtIe.Text;
                            empresaFilial.NomeFantasia = txtNomeFantasia.Text;
                            empresaFilial.RazaoSocial = txtRazaoSocial.Text;
                            empresaFilial.Endereco = endereco;
                            empresaFilial.Empresa = empresa;
                            empresaFilial.Otica = true;
                            //empresaFilial.SenhaCertificado = GenericaDesktop.Criptografa(txtSenhaCertificado.Texts);

                            RegimeEmpresa regimeTributario = new RegimeEmpresa();
                            regimeTributario.Id = int.Parse("1");
                            regimeTributario = (RegimeEmpresa)Controller.getInstance().selecionar(regimeTributario);
                            empresaFilial.RegimeEmpresa = regimeTributario;
                            Controller.getInstance().salvar(empresa);
                            Controller.getInstance().salvar(empresaFilial);

                            endereco.EmpresaFilial = empresaFilial;
                            Controller.getInstance().salvar(endereco);
                        }
                    }

                    GenericaDesktop.ShowInfo("Empresa já possui cadastro, cadastre um usuário para logar no sistema!");

                    chamarFormCadastroUsuario();
                    this.Close();
                    th = new Thread(opennewform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private bool ValidarCampos()
        {
            List<Control> campos = new List<Control>
            {
                txtRazaoSocial,
                txtCnpj,
                txtRazaoSocial,
                txtNomeFantasia
            };

            if (chkServidor.Checked)
            {
                campos.Add(txtCidade);
                campos.Add(txtCep);
                campos.Add(txtEndereco);
                campos.Add(txtCelular);
                campos.Add(txtResponsavel);
                campos.Add(txtSegmento);
            }

            foreach (Control campo in campos)
            {
                if (campo is TextBox || campo is MaskedTextBox)
                {
                    if (campo is TextBox && string.IsNullOrWhiteSpace((campo as TextBox).Text))
                    {
                        campo.Focus(); 
                        GenericaDesktop.ShowAlerta($"O campo '{campo.Name}' deve ser preenchido.");
                        return false; 
                    }
                    else if (campo is MaskedTextBox)
                    {
                        // Verificar MaskedTextBox
                        MaskedTextBox maskedTextBox = campo as MaskedTextBox;
                        // Verificar se o texto sem máscara está vazio
                        string textoSemMascara = maskedTextBox.Text.Replace(maskedTextBox.PromptChar.ToString(), "").Trim();
                        if (string.IsNullOrEmpty(textoSemMascara))
                        {
                            campo.Focus(); // Coloca o foco no campo vazio
                            GenericaDesktop.ShowAlerta($"O campo '{campo.Name}' deve ser preenchido.");
                            return false; // Retorna falso indicando que a validação falhou
                        }
                    }
                }
            }
            return true; 
        }
        private void opennewform()
        {
            Application.Run(new FrmLogin());
        }
        private void btnPesquisarCidade_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();
            txtUf.Text = "";
            txtCidade.Text = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaCidade uu = new FrmPesquisaCidade())
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
                    switch (uu.showModal(ref cidade))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCidade.Text = cidade.Descricao;
                            txtUf.Text = cidade.Estado.Uf;
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

        private void txtCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
               
            }
        }

        private string retornarLicencaPainel()
        {
            string cnpj = txtCnpj.Text.Trim();
            DataRow usuario = dbManager.ConsultarUsuarioPorCNPJ(cnpj);
            if (usuario != null)
            {
                string chave = usuario["licence_key"].ToString(); 
                return chave;
            }
            else
            {
                MessageBox.Show("Cliente não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void FrmDemonstracao_Load(object sender, EventArgs e)
        {

            // Chama o método para cadastrar estados e cidades de forma assíncrona
            //Task.Run(async () =>
            //{
            //    await cadastrarEstadoCidades();
            //});
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pesquisaCepAPIAsync();
        }
        private async Task pesquisaCepAPIAsync()
        {
            string cep = GenericaDesktop.RemoveCaracteres(txtCep.Text);

            string url = $"https://viacep.com.br/ws/{cep}/json/";

            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);
                EnderecoCep enderecoCep = JsonConvert.DeserializeObject<EnderecoCep>(json);
                GenericaDesktop generica = new GenericaDesktop();
                txtEndereco.Text = generica.RemoverAcentos(enderecoCep.Logradouro);
                txtComplemento.Text = enderecoCep.Complemento;
                Cidade cidade = new Cidade();
                CidadeController cidadeController = new CidadeController();
                cidade = cidadeController.selecionarCidadePorDescricao(generica.RemoverAcentos(enderecoCep.Localidade));
                if (cidade != null)
                {
                    txtCidade.Text = cidade.Descricao;
                    txtUf.Text = enderecoCep.Uf;
                    txtBairro.Text = enderecoCep.Bairro;
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            EmpresaFilial empresaFilial = new EmpresaFilial();
            empresaFilial.Id = 1;
            empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
            Sessao.empresaFilialLogada = empresaFilial;

            toolTip.Show("Digite o CNPJ do representante", txtCnpjRepresentante, txtCnpjRepresentante.Width / 2, txtCnpjRepresentante.Height / 2, 5000); // 0, -40 são as coordenadas de deslocamento do balão
           
            //Form formBackground = new Form();
            //FrmUsuarioCadastro uu = new FrmUsuarioCadastro();
            //formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.Opacity = .50d;
            //formBackground.BackColor = Color.Black;
            //formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //formBackground.WindowState = FormWindowState.Maximized;
            //formBackground.TopMost = false;
            //formBackground.Location = this.Location;
            //formBackground.ShowInTaskbar = false;
            //formBackground.Show();
            //uu.Owner = formBackground;
            //uu.ShowDialog();
            //formBackground.Dispose();
            //uu.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmTermosDeUso frmTermosDeUso = new FrmTermosDeUso();
            frmTermosDeUso.ShowDialog();
            chkTermosUso.Checked = true;
        }

        private void txtCnpjRepresentante_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCnpjRepresentante.Text))
            {
                GenericaDesktop.validarCPFCNPJ(txtCnpjRepresentante.Text);
            }
        }

        private int capturarRepresentante()
        {
            int idRepresentante = 2; // Padrao da nossa empresa no painel!
            try
            {
                string cnpj = txtCnpjRepresentante.Text;
                DataRow usuario = dbManager.ConsultarUsuarioPorCNPJ(cnpj);
                if (usuario != null)
                {
                    idRepresentante = int.Parse(usuario["id"].ToString());
                }
                return idRepresentante;
            }
            catch
            {
                return 0;
            }
        }

        private void txtIe_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumero(txtIe.Text,e);
        }

        private void FrmDemonstracao_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    lblStatus.Visible = true;
                    lblStatus.Text = "Aguarde atualização...";
                    Controller.getInstanceAtualiza();
                    Controller.getInstance().geraValoresPadrao();
                    InserirRegistrosPadroes();
                    lblStatus.Text = "Atualizado...";
                    break;
            }
        }

        private void InserirRegistrosPadroes()
        {
            cadastrarEstadoCidades();
            cadastrarNCM();
            cadastrarCEST();
            cadastrarCFOP();
            cadastrarCstPisCofins();
            cadastrarCstIPI();
            cadastrarANP();
            cadastrarBancos();
        }
        private void cadastrarNCM()
        {
            try
            {
                NcmController ncmController = new NcmController();
                IList<Ncm> listaNCMCadastrado = ncmController.selecionarTodosNCM();
                if (listaNCMCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    //List<Ncm_Json.Rootobject> ListaNCM = null;
                    var ListaNCM = generica.consultaNCM();
                    for (int i = 0; i < ListaNCM.Nomenclaturas.Length; i++)
                    {
                        Ncm ncm = new Ncm();

                        ncm.Id = 0;
                        ncm.NumeroNcm = GenericaDesktop.RemoveCaracteres(ListaNCM.Nomenclaturas[i].Codigo.ToString()).Trim();
                        ncm.DescricaoNcm = ListaNCM.Nomenclaturas[i].Descricao.ToUpper();
                        ncmController.salvar(ncm);

                        lblStatus.Text = "Cadastro de NCM: " + ncm.NumeroNcm;
                        int cal = i * 100 / ListaNCM.Nomenclaturas.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCEST()
        {
            try
            {
                CestController cestController = new CestController();
                IList<Cest> listaCESTCadastrado = cestController.selecionarTodosCest();
                if (listaCESTCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<Cest_Json.TabelaCest> ListaCEST = generica.consultaCEST();
                    int i = 0;
                    foreach (Cest_Json.TabelaCest cestSel in ListaCEST)
                    {
                        Cest cest = new Cest();

                        cest.Id = 0;
                        cest.NumeroCest = GenericaDesktop.RemoveCaracteres(cestSel.CEST.ToString()).Trim();
                        cest.DescricaoCest = cestSel.DESCRICAO.ToUpper();
                        cest.Item = cestSel.ITEM.ToString();
                        //String ncm = cestSel.NCM.ToString().Substring("", "ou"));
                        try { cest.Ncm = GenericaDesktop.RemoveCaracteres(cestSel.NCM.ToString()); } catch { }
                        cest.Segmento = cestSel.SEGMENTO.Trim();
                        cestController.salvar(cest);

                        lblStatus.Text = "Cadastro de CEST: " + cest.NumeroCest;
                        int cal = i * 100 / ListaCEST.Count;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarOrigemICMS()
        {
            try
            {
                OrigemIcmsController origemIcmsController = new OrigemIcmsController();

                IList<OrigemIcms> listaOrigemIcmsCadastrado = origemIcmsController.selecionarTodasOrigemIcms();
                if (listaOrigemIcmsCadastrado.Count < 7)
                {
                    lblStatus.Text = "Cadastro de Origem";
                    ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
                    valoresPadraoBO.gerarOrigemIcmsPadrao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cadastrarCSTICMS()
        {
            try
            {
                CstIcmsController cstIcmsController = new CstIcmsController();
                IList<CstIcms> listaCstIcmsCadastrado = cstIcmsController.selecionarTodosCstIcms();
                if (listaCstIcmsCadastrado.Count < 2)
                {
                    lblStatus.Text = "Cadastro de CST ICMS";
                    ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
                    valoresPadraoBO.gerarCstIcmsPadrao();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCSOSN()
        {
            try
            {
                CsosnController csosnController = new CsosnController();
                IList<Csosn> listaCsosnCadastrado = csosnController.selecionarTodosCSOSN();
                if (listaCsosnCadastrado.Count < 2)
                {
                    lblStatus.Text = "Cadastro de Csosn";
                    ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
                    valoresPadraoBO.gerarCSOSNPadrao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCFOP()
        {
            try
            {
                CfopController cfopController = new CfopController();
                IList<Cfop> listaCFOPCadastrado = cfopController.selecionarTodosCfop();
                if (listaCFOPCadastrado.Count < 50)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<CfopAux.Class1> listaCFOP = generica.consultarCFOP_JSON();
                    int i = 0;
                    foreach (CfopAux.Class1 cfopSel in listaCFOP)
                    {
                        Cfop cfop = new Cfop();

                        cfop.Id = 0;
                        cfop.CfopNumero = GenericaDesktop.RemoveCaracteres(cfopSel.CFOP.ToString()).Trim();
                        cfop.Descricao = cfopSel.Descrição.ToUpper();
                        IList<Cfop> listaCFOP2 = cfopController.selecionarCfopPorCfop(cfop.CfopNumero);
                        if (listaCFOP2.Count < 1)
                            cfopController.salvar(cfop);

                        lblStatus.Text = "Cadastro de CFOP: " + cfop.CfopNumero;
                        int cal = i * 100 / listaCFOP.Count;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCstPisCofins()
        {
            try
            {
                CstPisCofinsController cstPisCofinsController = new CstPisCofinsController();
                IList<CstPisCofins> listaCstCadastrado = cstPisCofinsController.selecionarTodosCST();
                if (listaCstCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    var listaCstA = generica.consultarCSTPISCOFINS_JSON();
                    for (int i = 0; i < listaCstA.CSTPISCOFINS.Length; i++)
                    {
                        CstPisCofins cstPisCofins = new CstPisCofins();
                        cstPisCofins.Id = 0;
                        cstPisCofins.Cst = listaCstA.CSTPISCOFINS[i].Código.ToString();
                        cstPisCofins.Descricao = listaCstA.CSTPISCOFINS[i].Descrição.ToUpper();
                        IList<CstPisCofins> listaCST2 = cstPisCofinsController.selecionarCstPorCst(cstPisCofins.Cst);
                        if (listaCST2.Count < 1)
                            cstPisCofinsController.salvar(cstPisCofins);

                        lblStatus.Text = "Cadastro de CST Pis/Cofins: " + cstPisCofins.Cst;
                        int cal = i * 100 / listaCstA.CSTPISCOFINS.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarCstIPI()
        {
            try
            {
                CstIpiController cstIpiController = new CstIpiController();
                IList<CstIpi> listaCstCadastrado = cstIpiController.selecionarTodosCST();
                if (listaCstCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<CstAuxIPI.CSTIPIAUX> listaCstA = generica.consultarCSTIPI_JSON();
                    int i = 0;
                    foreach (CstAuxIPI.CSTIPIAUX cstSel in listaCstA)
                    {
                        CstIpi cstIPI = new CstIpi();

                        cstIPI.Id = 0;
                        cstIPI.Cst = GenericaDesktop.RemoveCaracteres(cstSel.CST.ToString()).Trim();
                        cstIPI.Descricao = cstSel.DESCRICAO.ToUpper();
                        IList<CstIpi> listaCST2 = cstIpiController.selecionarCstPorCst(cstIPI.Cst);
                        if (listaCST2.Count < 1)
                            cstIpiController.salvar(cstIPI);

                        lblStatus.Text = "Cadastro de CST IPI: " + cstIPI.Cst;
                        int cal = i * 100 / listaCstA.Count;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarANP()
        {
            try
            {
                AnpController anpController = new AnpController();
                IList<Anp> listaAnpCadastrado = anpController.selecionarTodosCodigosANP();
                if (listaAnpCadastrado.Count < 10)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    IList<AnpAux.AnpAuxiliar> listaCstA = generica.consultarANP_JSON();
                    int i = 0;
                    foreach (AnpAux.AnpAuxiliar anpSel in listaCstA)
                    {
                        Anp anp = new Anp();

                        anp.Id = 0;
                        anp.Codigo = GenericaDesktop.RemoveCaracteres(anpSel.Código.ToString()).Trim();
                        anp.Descricao = anpSel.Produto.ToUpper();
                        IList<Anp> listaANP2 = anpController.selecionarCodigoAnpPorCodigo(anp.Codigo);
                        if (listaANP2.Count < 1)
                            anpController.salvar(anp);

                        lblStatus.Text = "Cadastro de Código ANP: " + anp.Codigo;
                        int cal = i * 100 / listaCstA.Count;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadastrarBancos()
        {
            try
            {
                BancoController bancoController = new BancoController();
                IList<Banco> listaBancos = bancoController.selecionarTodosBancos();
                if (listaBancos.Count < 2)
                {
                    GenericaDesktop generica = new GenericaDesktop();
                    var bancoJson = generica.consultaBancos();
                    for (int i = 0; i < bancoJson.Bancos.Length; i++)
                    {
                        Banco banco = new Banco();

                        banco.Id = 0;
                        banco.Descricao = bancoJson.Bancos[i].Descricao.ToUpper();
                        banco.CodBanco = bancoJson.Bancos[i].CodBanco.ToString();
                        banco.CodIspb = bancoJson.Bancos[i].CodIspb.ToString();
                        bancoController.salvar(banco);

                        lblStatus.Text = "Cadastro de Bancos: " + banco.Descricao;
                        int cal = i * 100 / bancoJson.Bancos.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkTerminal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkTerminal.Checked == true)
                {
                    chkServidor.Checked = false;
                    txtServidor.Visible = true;
                    lblServidor.Visible = true;

                    lblCelular.Visible = false;
                    lblCidade.Visible = false;
                    lblCpf.Visible = false;
                    lblFone.Visible = false;
                    lblNome.Visible = false;
                    lblSegmento.Visible = false;
                    lblUf.Visible = false;
                    txtCelular.Visible = false;
                    txtCidade.Visible = false;
                    txtCpfResponsavel.Visible = false;
                    txtFoneFixo.Visible = false;
                    txtResponsavel.Visible = false;
                    txtSegmento.Visible = false;
                    txtUf.Visible = false;
                    btnPesquisarCidade.Visible = false;
                    txtServidor.Focus();
                }
                else
                {
                    chkServidor.Checked = true;
                    txtServidor.Visible = false;
                    lblServidor.Visible = false;

                    lblCelular.Visible = true;
                    lblCidade.Visible = true;
                    lblCpf.Visible = true;
                    lblFone.Visible = true;
                    lblNome.Visible = true;
                    lblSegmento.Visible = true;
                    lblUf.Visible = true;
                    txtCelular.Visible = true;
                    txtCidade.Visible = true;
                    txtCpfResponsavel.Visible = true;
                    txtFoneFixo.Visible = true;
                    txtResponsavel.Visible = true;
                    txtSegmento.Visible = true;
                    txtUf.Visible = true;
                    btnPesquisarCidade.Visible = true;
                }
            }
            catch
            {

            }
        }

        private void chkServidor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkServidor.Checked == true)
                    chkTerminal.Checked = false;
            }
            catch
            {

            }
        }

        private void FrmDemonstracao_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!String.IsNullOrEmpty(txtRazaoSocial.Text))
                Process.Start(@"C:\Lunar\Lunar.exe");
        }
    }
}
