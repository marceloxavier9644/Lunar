using Lunar.Utils;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace LunarSoftwareAtivador
{
    public partial class FrmAtivador : Form
    {
        String senha = "@lunar_software";
        public FrmAtivador()
        {
            InitializeComponent();
            txtData.Text = DateTime.Now.ToShortDateString();
            txtSerialHD.Text = GetHDDSerialNumber("C");
        }

        private void btnGerarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                consultarRetornoAPI();
                inserirRegistroWindows();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void inserirRegistroWindows()
        {
            if (txtSenha.Text.Equals(senha))
            {
                string drive = "C";
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                key.CreateSubKey("MXSystem");
                key = key.OpenSubKey("MXSystem", true);
                key.CreateSubKey("System");
                key = key.OpenSubKey("System", true);
                key.SetValue("Chave", Criptografa(Criptografa(Criptografa(disk["VolumeSerialNumber"].ToString()))));
                key.Close();
            }
            else
            {
                throw new Exception("Senha de ativação incorreta!");
            }
        }

        public string GetHDDSerialNumber(string drive)
        {
            try
            {
                if (drive == "" || drive == null)
                {
                    drive = "C";
                }
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                return disk["VolumeSerialNumber"].ToString();
            }
            catch (Exception erro)
            { 
                GenericaDesktop.ShowErro("Erro ao obter serial do disco: " + erro.Message);
                return "123456";
            }
        }

       
        private string Criptografa(String valor)
        {
            Byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
            String dadosCriptografados = Convert.ToBase64String(bytes);
            return dadosCriptografados;
        }

        private void consultarRetornoAPI()
        {
            if (txtSenha.Text.Equals(senha))
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
                            cnpj = txtCNPJ.Text,
                            key = txtSerial.Text
                        });

                        streamWriter.Write(json);

                    }
                    var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        streamReader.Close();
                        var retorno = JsonConvert.DeserializeObject<LunarClienteAtivo>(result);
                        if (!String.IsNullOrEmpty(retorno.licence_expiration) && retorno.is_blocked == false)
                        {
                            if(DateTime.Parse(retorno.licence_expiration) >= DateTime.Now)
                            {
                                Random rnd = new Random();
                                //se o numero for entre 100 e 499 nao está bloqueado
                                string block = Criptografa(Criptografa(rnd.Next(100, 499).ToString()));
                                //se o numero for entre 500 e 999 nao está bloqueado
                                if (retorno.is_blocked == true)
                                    block = Criptografa(Criptografa(rnd.Next(500, 999).ToString()));

                                gerarXMLConfigPC(DateTime.Parse(retorno.licence_expiration), txtSerialHD.Text, block);
                                
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
            else
            {
                throw new Exception("Senha de ativação incorreta!");
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

        private void gerarXMLConfigPC(DateTime validade, string serialHD, string block)
        {
            try
            {
                if (txtSenha.Text.Equals(senha))
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml", null);
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("MXSystem");
                    xmlWriter.WriteElementString("AppVersion", Criptografa("Fé, saúde e trabalho"));
                    xmlWriter.WriteElementString("AppData", Criptografa("Superação, foco e garra"));
                    xmlWriter.WriteElementString("AppTime", Criptografa("Com DEUS tudo é possível"));
                    xmlWriter.WriteElementString("AppUser", Criptografa(validade.ToShortDateString()));
                    xmlWriter.WriteElementString("AppServ", Criptografa(serialHD));
                    xmlWriter.WriteElementString("AppComon", block);
                    xmlWriter.WriteElementString("AppOper", Criptografa(txtCNPJ.Text));
                    xmlWriter.WriteElementString("AppClient", Criptografa(txtSerial.Text));
                    xmlWriter.WriteElementString("Servidor", txtNomeServidor.Text.Trim());
                    xmlWriter.WriteElementString("Usuario", txtUsuarioBanco.Text.Trim());
                    xmlWriter.WriteElementString("Senha", Criptografa(txtSenhaBanco.Text));
                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();
                    GenericaDesktop.ShowInfo("Chave do sistema gerada com sucesso! Validade Atual da Licenca: " + validade.ToShortDateString());
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Verifique as permissões da pasta do sistema\n" + erro.Message);
            }
        }
    }
}
