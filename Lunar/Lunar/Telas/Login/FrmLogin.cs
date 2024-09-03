using Lunar.Telas.Login;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.LunarNFe;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace Lunar
{
    public partial class FrmLogin : Syncfusion.Windows.Forms.Office2007Form
    {
        Generica generica = new Generica();
       
        Thread th;
        private string servidor = "";
        private string usuarioBanco = "";
        private string senhaBanco = "";
        private string nomeBanco = "";
        string atualizaBanco = @"C:\Lunar\Atualizador\AtualizaBanco.txt";
        //UsuarioController usuarioController = new UsuarioController();
        Logger logger = new Logger();
        public FrmLogin()
        {
            InitializeComponent();
            logger.WriteLog("Loggin inicialize", "log");
            verificaLicencaSistema();
            logger.WriteLog("Loggin Licenca verificada", "log");
            if (File.Exists(@"C:\Lunar\Atualizador\LunarAtualizador.exe"))
                abrirAtualizador("LunarAtualizador", @"C:\Lunar\Atualizador\LunarAtualizador.exe");
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            try
            {
                if (File.Exists("C:\\Lunar\\LunarSoftwareAtivador.exe"))
                {
                    File.Delete("C:\\Lunar\\LunarSoftwareAtivador.exe");
                }
                if(Environment.MachineName.Equals("NoteMarcelo") || Environment.MachineName.Equals("NOTEMARCELO"))
                {
                    txtUsuario.Texts = "SUPORTE";
                    txtSenha.Texts = "ESTAC2UL";
                }
               
            }
            catch
            {

            }
        }

        private static void abrirAtualizador(string processName, string processPath)
        {
            try
            {
                // Check if the process is running
                var isRunning = Process.GetProcessesByName(processName).Any();

                // If the process is not running, start it
                if (!isRunning)
                {
                    try
                    {
                        Process.Start(processPath);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions here, such as logging the error
                        Console.WriteLine($"Failed to start process {processName}: {ex.Message}");
                    }
                }
            }
            catch
            {

            }
        }
        //EstadoController estadoController = new EstadoController();
        //CidadeController cidadeController = new CidadeController();
        //ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
        //UsuarioController usuarioController = new UsuarioController();
        private void ShowMessage(string message)
        {
            lblErrorMessage.Text = "    " + message;
            lblErrorMessage.Visible = true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Texts) || txtUsuario.Texts == txtUsuario.PlaceholderText)
            {
                ShowMessage("Insira seu nome de usuário");
                return;
            }

            Usuario usuario = new Usuario();
            usuario.Login = txtUsuario.Texts;
            usuario.Senha = GenericaDesktop.Criptografa(txtSenha.Texts);
            try
            {
                UsuarioController usuarioController = new UsuarioController();
                Usuario usuarioLogado = usuarioController.selecionarUsuarioPorUsuarioESenha(usuario);
                if (usuarioLogado != null && usuarioLogado.Id > 0)
                {
                    //gravarEmpresaPadraoDoComputador();
                    LunarBase.Utilidades.Sessao.usuarioLogado = usuarioLogado;
                    if (!String.IsNullOrEmpty(LunarBase.Utilidades.Sessao.usuarioLogado.GrupoUsuario.Permissoes))
                    {
                        string prm = GenericaDesktop.Descriptografa(Sessao.usuarioLogado.GrupoUsuario.Permissoes);
                        String[] permissions = prm.Split(';');
                        Sessao.permissoes = new HashSet<string>(permissions);
                    }
                    this.Close();
                    th = new Thread(opennewform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {

                }
            }
            catch
            {
                ShowMessage("Usuário ou senha incorreto");
                txtUsuario.PlaceholderText = "";
                txtSenha.Focus();
                txtUsuario.Texts = "";
                txtSenha.Texts = "";
                txtUsuario.Focus();
            }

        }

        private void opennewform()
        {
            Application.Run(new WelcomeForm(Sessao.usuarioLogado.Login));
        }

        private void MainForm_SessionClosed(object sender, FormClosedEventArgs e)
        {
            Logout();////Invoke the Close session method, when the main form has been closed session.
        }

        private void Logout()
        {
            this.Show();//Show the login form again.
            txtUsuario.Text = "";
            txtSenha.Text = "";
            lblErrorMessage.Visible = false;
        }

        private void atualizarBancoDados()
        {
            try
            {
                btnLogin.Enabled = false;
                progressBar1.Visible = true;
                progressBar1.Value = 30;
                progressBar1.Value = 70;
                ValoresPadraoBO valoresPadraoBO = new ValoresPadraoBO();
                valoresPadraoBO.gerarUsuarioPadrao();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new FrmRecuperarSenha();
            frm.ShowDialog();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //lblStatus.Visible = true;
            //lblStatus.Text = "Atualizando...";
            atualizarBancoDados();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            //lblStatus.Text = "Atualização de banco de dados";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnLogin.Enabled = true;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync();
        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            if (File.Exists(atualizaBanco))
            {
                lblStatus.Visible = true;
                lblStatus.Text = "Aguarde Atualização...";
                Controller.getInstanceAtualiza();
                Controller.getInstance().geraValoresPadrao();
                lblStatus.Text = "Atualização Concluída com Sucesso...";
                File.Delete(atualizaBanco);
            }
        }

        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    lblStatus.Visible = true;
                    lblStatus.Text = "Aguarde atualização...";
                    Controller.getInstanceAtualiza();
                    Controller.getInstance().geraValoresPadrao();
                    lblStatus.Text = "Atualizado...";
                    break;
                case Keys.F9:
                    //_ = LunarNFe.emitirNfe();
                    _ = LunarNFe.AutorizarNFe();
                    break;
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void atualizarLicenca(String cnpj, String chavePainel, String serialHD)
        {
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string url = "https://lunarsoftware.com.br/painel/api/api-auth.php";
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
                            cnpj = cnpj,
                            key = chavePainel
                        });

                        streamWriter.Write(json);

                    }
                    var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        streamReader.Close();
                        var retorno = JsonConvert.DeserializeObject<LunarClienteAtivo>(result);
                        if (!String.IsNullOrEmpty(retorno.licence_expiration))
                        {
                            Random rnd = new Random();
                            //se o numero for entre 100 e 499 nao está bloqueado
                            string block = Generica.Criptografa(rnd.Next(100, 499).ToString());
                            //se o numero for entre 500 e 999 nao está bloqueado
                            if (retorno.is_blocked == true)
                                block = Generica.Criptografa(rnd.Next(500, 999).ToString());
                            String hdReg = serialHD;//GetHDDSerialNumber("C");
                            gerarXMLConfigPC(DateTime.Parse(retorno.licence_expiration), hdReg, block, cnpj, chavePainel); 
                        }
                        if (retorno.msg.Equals("ERR_INVALID_USER"))
                        {
                            GenericaDesktop.ShowErro("Serial do Painel ou CNPJ Inválido!");
                            Environment.Exit(0);
                        }
                    }
                }
                catch (Exception err)
                {
                    Logger logger = new Logger();
                    logger.WriteLog(err.Message, "Log");
                    //GenericaDesktop.ShowErro(err.Message);
                }
            }
        }

        //public string GetHDDSerialNumber(string drive)
        //{
        //    if (drive == "" || drive == null)
        //    {
        //        drive = "C";
        //    }
        //    ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
        //    disk.Get();
        //    return disk["VolumeSerialNumber"].ToString();
        //}
        private void gerarXMLConfigPC(DateTime validade, string serialHD, string block, string cnpj, string serialPainel)
        {
            try
            {
                string dateToEncrypt = validade.ToShortDateString();
                var (key, iv) = CryptoUtils.GenerateKeyAndIV();
                File.WriteAllBytes("lunar.bin", key);
                File.WriteAllBytes("lunarSoftware.bin", iv);
                byte[] encryptedDate = CryptoUtils.EncryptDateString(dateToEncrypt, key, iv);

                XmlTextWriter xmlWriter = new XmlTextWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml", null);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("MXSystem");
                xmlWriter.WriteElementString("AppVersion", Generica.Criptografa("Fé, saúde e trabalho"));
                xmlWriter.WriteElementString("AppData", Generica.Criptografa(nomeBanco));
                xmlWriter.WriteElementString("AppTime", Generica.Criptografa("Com DEUS tudo é possível"));
                xmlWriter.WriteElementString("AppUser", Convert.ToBase64String(encryptedDate));
                xmlWriter.WriteElementString("AppServ", Generica.Criptografa(serialHD));
                xmlWriter.WriteElementString("AppComon", block);
                xmlWriter.WriteElementString("AppOper", Generica.Criptografa(cnpj));
                xmlWriter.WriteElementString("AppClient", Generica.Criptografa(serialPainel));
                xmlWriter.WriteElementString("Servidor", servidor);
                xmlWriter.WriteElementString("Usuario", usuarioBanco);
                xmlWriter.WriteElementString("Senha", Generica.Criptografa(senhaBanco));
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Verifique as permissões da pasta do sistema\n" + erro.Message);
            }
        }

        private void verificaLicencaSistema()
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml"))
            {
                string dataLicenca = "";
                byte[] storedKey = null;
                byte[] storedIV = null;

                string dataXML = DateTime.Now.AddDays(-1000).ToString();
                String hd = "";
                String cnpjClient = "";
                String serialRegistro = "";
                string verificaBloqueio = "100";
                try
                {
                    XmlTextReader reader = new XmlTextReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppUser")
                            dataXML = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppServ")
                            hd = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppData")
                            nomeBanco = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppOper")
                            cnpjClient = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppClient")
                            serialRegistro = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppComon")
                            verificaBloqueio = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Servidor")
                            servidor = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Usuario")
                            usuarioBanco = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Senha")
                            senhaBanco = GenericaDesktop.Descriptografa(reader.ReadString());
                    }
                    reader.Dispose();

                    Sessao.servidor = servidor;
                    Sessao.usuarioBanco = usuarioBanco;
                    Sessao.senhaBanco = senhaBanco;
                    Sessao.nomeBanco = nomeBanco;

                    atualizarLicenca(cnpjClient, serialRegistro, hd);

                    storedKey = File.ReadAllBytes("lunar.bin");
                    storedIV = File.ReadAllBytes("lunarSoftware.bin");

                    reader = new XmlTextReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppUser")
                            dataXML = reader.ReadString().ToString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppServ")
                            hd = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppOper")
                            cnpjClient = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppClient")
                            serialRegistro = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppComon")
                            verificaBloqueio = (GenericaDesktop.Descriptografa(reader.ReadString()));
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Servidor")
                            servidor = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Usuario")
                            usuarioBanco = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Senha")
                            senhaBanco = GenericaDesktop.Descriptografa(reader.ReadString());
                    }
                    Sessao.cnpjRegistro = cnpjClient;
                    Sessao.serialPainel = serialRegistro;
                    Sessao.servidor = servidor;
                    Sessao.usuarioBanco = usuarioBanco;
                    Sessao.senhaBanco = senhaBanco;
                    reader.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ocorreu uma falha ao validar a chave do sistema. Arquivo não Localizado!" + e.Message);
                    Environment.Exit(0);
                }
                if (double.Parse(verificaBloqueio) >= 100 && double.Parse(verificaBloqueio) <= 499)
                {
                    try
                    {
                        string drive = "C";
                        ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                        disk.Get();
                        String serialHD = disk["VolumeSerialNumber"].ToString();
                        RegistryKey chave = Registry.CurrentUser.OpenSubKey("Software", true);
                        chave = chave.OpenSubKey("MXSystem\\System", true);
                       // MessageBox.Show(GenericaDesktop.Descriptografa(GenericaDesktop.Descriptografa(GenericaDesktop.Descriptografa(chave.GetValue("Chave").ToString()))));
                        if (!serialHD.Equals(GenericaDesktop.Descriptografa(GenericaDesktop.Descriptografa(GenericaDesktop.Descriptografa(chave.GetValue("Chave").ToString())))))
                        {
                            GenericaDesktop.ShowAlerta("A chave de ativação do sistema não é válida!");
                            Environment.Exit(0);
                        }
                        if(serialHD != hd)
                        {
                            GenericaDesktop.ShowAlerta("A chave de ativação do sistema não é válida!");
                            Environment.Exit(0);
                        }
                    }
                    catch (Exception)
                    {
                        //GenericaDesktop.ShowAlerta("Ocorreu uma falha ao validar a chave do sistema. Entre em contato com o administrador!");
                        //Environment.Exit(0);
                    }

                    byte[] storedEncryptedDate = Convert.FromBase64String(dataXML);
                    dataLicenca = CryptoUtils.DecryptDateString(storedEncryptedDate, storedKey, storedIV);

                    if (DateTime.Parse(dataLicenca) < DateTime.Now)
                    {
                        GenericaDesktop.ShowAlerta("Sua licença expirou, entre em contato com seu revendedor");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("A data limite de uso do sistema foi atingida, por favor entre em contato com o seu revendedor para verificação!");
                    Environment.Exit(0);
                }
            }
            else
            {
                GenericaDesktop.ShowErro("Atenção, este sistema não foi ativado neste computador!");
                Environment.Exit(0);
            }
        }

        public class LunarClienteAtivo
        {
            public bool error { get; set; }
            public string msg { get; set; }
            public bool is_blocked { get; set; }
            public string date_blocked { get; set; }
            public string licence_expiration { get; set; }
            public int licence_computers { get; set; }
            public string client_name { get; set; }
            public Module[] modules { get; set; }
        }

        public class Module
        {
            public int id { get; set; }
            public bool is_selected { get; set; }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txtSenha.Focus();
                txtSenha.SelectAll();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtUsuario.Texts))
                    Login();
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsuario.Texts.Trim()))
            {
                UsuarioController usuarioController = new UsuarioController();
                IList<Usuario> listaUser = usuarioController.selecionarUsuarioComVariosFiltros(txtUsuario.Texts.Trim());
                if (listaUser.Count == 1)
                {
                    foreach (Usuario usuario in listaUser)
                    {
                        txtUsuario.Texts = usuario.Login;
                        txtSenha.Focus();
                    }
                }
                //else if (listaUser.Count == 0)
                //{
                //    GenericaDesktop.ShowAlerta("Usuário não encontrado!");
                //}
            }
        }


    }
}
