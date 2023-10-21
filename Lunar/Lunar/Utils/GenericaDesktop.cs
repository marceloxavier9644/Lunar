using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils.SintegrawsConsultas;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using static Lunar.Utils.OrganizacaoNF.RetCartaCorrecao;
using static Lunar.Utils.OrganizacaoNF.RetInutilizacao;
using static Lunar.Utils.OrganizacaoNF.RetornoJsonVariados;
using static LunarBase.Utilidades.ClasseRetornoJson.CancelamentoNFCe;
using static LunarBase.Utilidades.ClasseRetornoJson.InutilizacaoNFCe;
using static LunarBase.Utilidades.ManifestoDownload;
using static LunarBase.Utilidades.Ns_ConsultaCNPJ;
using Attachment = System.Net.Mail.Attachment;
using File = System.IO.File;

namespace Lunar.Utils
{
    public class GenericaDesktop
    {

        public static void ShowInfo(String mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowAlerta(String mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErro(String mensagem)
        {
            MessageBox.Show(mensagem, "Aconteceu algo errado...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowConfirmacao(String mensagem)
        {
            return MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public void ShowAutoClosingMessageBox(string message, string caption)
        {
            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, caption);
                if (hWnd.ToInt32() != 0) PostMessage(hWnd, WM_CLOSE, 0, 0);
            };
            timer.Enabled = true;
            MessageBox.Show(message, caption);
        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        public static string formatarFone(String fone)
        {
            try
            {
                string foneEmp = GenericaDesktop.RemoveCaracteres(fone);
                foneEmp = foneEmp.Trim();
                if (foneEmp.Length == 11)
                {
                    foneEmp = long.Parse(foneEmp).ToString(@"(00) 0 0000-0000");
                }
                else if (foneEmp.Length == 9)
                {
                    foneEmp = long.Parse(foneEmp).ToString(@"00000-0000");
                }
                else if (foneEmp.Length == 10)
                {
                    foneEmp = long.Parse(foneEmp).ToString(@"(00) 0000-0000");
                }
                return foneEmp;
            }
            catch
            {
                return fone;
            }
        }

        public void buscarAlertaCadastrado(Pessoa pessoa)
        {
            AlertaClienteController alertaClienteController = new AlertaClienteController();
            IList<AlertaCliente> listaAlerta = alertaClienteController.selecionarAlertaPorPessoa(pessoa.Id);
            if (listaAlerta.Count > 0)
            {
                String mensagem = "";
                foreach (AlertaCliente alerta in listaAlerta)
                {
                    if (!String.IsNullOrEmpty(mensagem))
                        mensagem = mensagem + "\n" + alerta.Data.ToShortDateString() + " - " + alerta.Descricao;
                    else
                        mensagem = alerta.Data.ToShortDateString() + " - " + alerta.Descricao;
                }
                GenericaDesktop.ShowAlerta(mensagem);
            }
        }

        public static bool validarCPFCNPJ(String cpfcnpj, Boolean aceitaVazio = false)
        {
            if (string.IsNullOrWhiteSpace(cpfcnpj))
                return aceitaVazio;
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;

                SoNumero = Regex.Replace(cpfcnpj, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }

                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                else return false;
            }
        }
        public static async Task<bool> VerificaProgramaContigenciaEstaEmExecucao()
        {
            Process[] localByName = Process.GetProcessesByName("javaw");
            if (localByName.Length > 0)
            {
                return true;
            }
            else
            {
                if (File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\NSNFCeClient.jar"))
                {
                    Process.Start(Sessao.parametroSistema.PastaRemessaNsCloud + @"\NSNFCeClient.jar");
                    await Task.Delay(2000);
                    return true;
                }
                else
                    return false;
            }
        }
        public bool ValidarEmail(String email)
        {
            bool emailValido = false;

            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;
        }

        public bool eNumero(String texto)
        {
            bool retorno = false;
            if (texto.All(char.IsDigit))
                retorno = true;
            else
                retorno = false;

                return retorno;
        }
        public string ConversaoCFOP(string cfop)
        {
            string cfopEntrada = "";
            if (cfop.Equals("5102"))
                cfopEntrada = "1102";
            else if (cfop.Equals("6102"))
                cfopEntrada = "2102";
            else if (cfop.Equals("5101"))
                cfopEntrada = "1102";
            else if (cfop.Equals("6101"))
                cfopEntrada = "2102";
            else if (cfop.Equals("5949"))
                cfopEntrada = "1949";
            else if (cfop.Equals("6949"))
                cfopEntrada = "2949";

            else if (cfop.Equals("5405"))
                cfopEntrada = "1403";
            else if (cfop.Equals("5403"))
                cfopEntrada = "1403";
            else if (cfop.Equals("5401"))
                cfopEntrada = "1403";
            else if (cfop.Equals("5404"))
                cfopEntrada = "1403";
            else if (cfop.Equals("6405"))
                cfopEntrada = "2403";
            else if (cfop.Equals("6403"))
                cfopEntrada = "2403";
            else if (cfop.Equals("6401"))
                cfopEntrada = "2403";
            else if (cfop.Equals("6404"))
                cfopEntrada = "2403";

            else if (cfop.Equals("5915"))
                cfopEntrada = "1915";
            else if (cfop.Equals("6915"))
                cfopEntrada = "2915";

            else if (cfop.Equals("5933"))
                cfopEntrada = "1933";
            else if (cfop.Equals("6933"))
                cfopEntrada = "2933";

            return cfopEntrada;
        }

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        public static Boolean possuiConexaoInternet()
        {
            Uri url = new Uri("http://www.google.com.br");
            WebRequest request = WebRequest.Create(url);
            WebResponse response;
            try
            {
                request.Proxy = null;
                request.Timeout = 2000;
                response = request.GetResponse();
                response.Close();
                request = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string SoNumero(string pgNumero, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(
                e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            return pgNumero;
        }

        public string SoNumeroEVirgula(string pgNumero, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8) && !(e.KeyChar == (char)44))
            {
                e.Handled = true;
            }
            return pgNumero;
        }

        public CNPJResponse consultaEmpresa(string cnpj)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://api-publica.speedio.com.br/buscarcnpj?cnpj=" + cnpj);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "ConsultaCNPJ";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var empresa = JsonConvert.DeserializeObject<CNPJResponse>(objResponse.ToString());
                streamDados.Close();
                resposta.Close();
                return empresa;
            }
        }

        public Ns_ConsultaCNPJ consultarCNPJ(string cnpj, string Uf)
        {
            var requisicaoWeb = WebRequest.CreateHttp("http://nfehml.ns.eti.br/util/conscad?X-AUTH-TOKEN=VFhUIElORk9STUFUSUNBT3JQSEQ=&CNPJCont=28145398000173&UF=MG&CNPJ=28145398000173");
            requisicaoWeb.Method = "POST";
            //requisicaoWeb.UserAgent = "ConsultaCNPJ";
            requisicaoWeb.ContentType = "application/json";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var empresa = JsonConvert.DeserializeObject<Ns_ConsultaCNPJ>(objResponse.ToString());
                streamDados.Close();
                resposta.Close();
                return empresa;
            }
        }

        public ConsultEmpresaNs consultarEmpresaPorCnpj_NS(string cnpjCont, string cnpj, string Uf)
        {
            try
            {
                String url = "https://nfe.ns.eti.br/util/conscad";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        CNPJCont = cnpjCont,
                        UF = Uf,
                        CNPJ = cnpj
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var empresa = JsonConvert.DeserializeObject<ConsultEmpresaNs>(result);
                    return empresa;
                }
            }
            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using (var streamReader = new StreamReader(webEx.Response.GetResponseStream()))
                    {
                        string errorResponse = streamReader.ReadToEnd();
                        //GenericaDesktop.ShowErro($"Erro de Requisição: {webEx.Message}\nResposta do Servidor: {errorResponse}");
                    }
                }
                else
                {
                    //GenericaDesktop.ShowErro($"Erro de Requisição: {webEx.Message}");
                }

                return null;
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }


        public SintegraConsultaCnpj consultaCNPJSintegraWS(String cnpj)
        {
            String token = "A9F4A8B9-F9EA-4B5E-8A77-4577C43139F4";
            String plugin = "ST";
            SintegraConsultaCnpj sintegra = new SintegraConsultaCnpj();

            using (HttpClient client = new HttpClient())
            {
                String url = "https://www.sintegraws.com.br/api/v1/execute-api.php?token=" + token + "&cnpj=" + cnpj + "&plugin=" + plugin;
                var response = client.GetAsync(url).Result;
                using (HttpContent content = response.Content)
                {
                    var result = content.ReadAsStringAsync();
                    string jsonRetorno = result.Result;
                    
                    sintegra = (SintegraConsultaCnpj)JsonConvert.DeserializeObject(jsonRetorno);
                    if (sintegra.code != null)
                    {
                        if (sintegra.code.Equals("0"))
                        {
                            return sintegra;
                            //Console.WriteLine("Nome empresarial: " + sintegra.nome_empresarial);
                        }
                        else
                        {
                            MessageBox.Show(jsonRetorno);
                            return null;
                            //Console.WriteLine("Erro: " + sintegra.message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(jsonRetorno);
                        return null;
                    }
                }
            }
        }

            public async Task<ManifestoDownload.Manifesto> ConsultaNotas_Manifesto(string CNPJ, DateTime dataInicial)
            {
            try
            {
                String url = "https://ddfe.ns.eti.br/dfe/bunch";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tipoAmbiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tipoAmbiente = "1";

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        CNPJInteressado = CNPJ,
                        dhInicial = dataInicial.ToShortDateString() + " 00:00:00-03:00",
                        dhFinal = dataInicial.AddMonths(2).ToShortDateString() + " 23:59:59-03:00",
                        modelo = "55",
                        tpAmb = tipoAmbiente,
                        apenasComXml = true,
                        incluirPDF = true,
                        cSitNFe = 1
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var notas = JsonConvert.DeserializeObject<ManifestoDownload.Manifesto>(result);
                    return notas;
                }
            }
            catch (Exception err)
            {
                //GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public NotaUnique ConsultaNotas_Manifesto_Unique(string CNPJ, string chave)
        {
            try
            {
                String url = "https://ddfe.ns.eti.br/dfe/unique";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        CNPJInteressado = CNPJ,
                        chave = chave,
                        //modelo = "55",
                        tpAmb = "1",
                        apenasComXml = true,
                        incluirPDF = true

                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var nota = JsonConvert.DeserializeObject<NotaUnique>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public string NS_EnviarEmailNF(string chave, List<String> emails)
        {
            try
            {
                String url = "https://nfe.ns.eti.br/util/resendemail";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tipoAmbiente = "1";
                if (Sessao.parametroSistema.AmbienteProducao != true)
                    tipoAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = chave,
                        tpAmb = tipoAmbiente,
                        anexarPDF = true,
                        anexarEvento = true,
                        email = emails
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var nota = JsonConvert.DeserializeObject<NotaUnique>(result);
                    return result;
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public string NS_ConsultaValidadeCertificado(string CNPJ)
        {
            try
            {
                String url = "https://painelapi.ns.eti.br/licenca/certificate/get";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        licencaCnpj = CNPJ,
                        projeto = 1
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(result);
                    string valorRetornado = "";
                    if (retorno.certificado != null)
                        valorRetornado = retorno.certificado.vencimento;
                    return valorRetornado;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public string NS_EnviarCertificadoDigitalParaNFe55(string CNPJ, string senha, string certificado)
        {
            try
            {
                String url = "https://painelapi.ns.eti.br/licenca/certificate/save";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        licencaCnpj = CNPJ,
                        projeto = 1,
                        password = senha,
                        file = certificado
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(result);
                    string valorRetornado = "";
                    if (retorno.msg != null)
                        valorRetornado = retorno.msg;
                    return valorRetornado;
                }
            }
            catch (WebException ex)
            {
                RetConsultaCertificadoNS retorno = new RetConsultaCertificadoNS();
                string valorRetornado = "";
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        valorRetornado = streamReader.ReadToEnd();
                        retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(valorRetornado);
                    }

                    switch (System.Convert.ToInt32(retorno.status))
                    {
                        case - 1:
                            {
                                valorRetornado = "Base64 do certificado enviada não é valida";
                                break;
                            }

                        case -2:
                            {
                                valorRetornado = "Senha do certificado inválida ou arquivo enviado inválido";
                                break;
                            }

                        case -3:
                            {
                                valorRetornado = "Licença não encontrada para o CNPJ e Projeto enviados";
                                break;
                            }

                        default:
                            {
                                valorRetornado = "Erro ao inserir certificado, tente novamente!";
                                break;
                            }
                    }
                }
                return valorRetornado;
            }
        }

        public string NS_EnviarCertificadoDigitalParaNFCe65(string CNPJ, string senha, string certificado)
        {
            try
            {
                String url = "https://painelapi.ns.eti.br/licenca/certificate/save";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        licencaCnpj = CNPJ,
                        projeto = 20,
                        password = senha,
                        file = certificado
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(result);
                    string valorRetornado = "";
                    if (retorno.msg != null)
                        valorRetornado = retorno.msg;
                    return valorRetornado;
                }
            }
            catch (WebException ex)
            {
                RetConsultaCertificadoNS retorno = new RetConsultaCertificadoNS();
                string valorRetornado = "";
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        valorRetornado = streamReader.ReadToEnd();
                        retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(valorRetornado);
                    }

                    switch (System.Convert.ToInt32(retorno.status))
                    {
                        case -1:
                            {
                                valorRetornado = "Base64 do certificado enviada não é valida";
                                break;
                            }

                        case -2:
                            {
                                valorRetornado = "Senha do certificado inválida ou arquivo enviado inválido";
                                break;
                            }

                        case -3:
                            {
                                valorRetornado = "Licença não encontrada para o CNPJ e Projeto enviados";
                                break;
                            }

                        default:
                            {
                                valorRetornado = "Erro ao inserir certificado, tente novamente!";
                                break;
                            }
                    }
                }
                return valorRetornado;
            }
        }

        public string NS_EnviarCertificadoDigitalParaDDFe(string CNPJ, string senha, string certificado)
        {
            try
            {
                String url = "https://painelapi.ns.eti.br/licenca/certificate/save";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        licencaCnpj = CNPJ,
                        projeto = 22,
                        password = senha,
                        file = certificado
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(result);
                    string valorRetornado = "";
                    if (retorno.msg != null)
                        valorRetornado = retorno.msg;
                    return valorRetornado;
                }
            }
            catch (WebException ex)
            {
                RetConsultaCertificadoNS retorno = new RetConsultaCertificadoNS();
                string valorRetornado = "";
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        valorRetornado = streamReader.ReadToEnd();
                        retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(valorRetornado);
                    }

                    switch (System.Convert.ToInt32(retorno.status))
                    {
                        case -1:
                            {
                                valorRetornado = "Base64 do certificado enviada não é valida";
                                break;
                            }

                        case -2:
                            {
                                valorRetornado = "Senha do certificado inválida ou arquivo enviado inválido";
                                break;
                            }

                        case -3:
                            {
                                valorRetornado = "Licença não encontrada para o CNPJ e Projeto enviados";
                                break;
                            }

                        default:
                            {
                                valorRetornado = "Erro ao inserir certificado, tente novamente!";
                                break;
                            }
                    }
                }
                return valorRetornado;
            }
        }

        public void gravarXMLNaPasta(string xmlString, string chave, string caminhoGravar, string nomeArquivo)
        {
            try
            {
                if (Directory.Exists(@caminhoGravar) && !File.Exists(@nomeArquivo))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);
                    doc.Save(@caminhoGravar + @nomeArquivo);
                }
                else if (!Directory.Exists(@caminhoGravar))
                {
                    Directory.CreateDirectory(@caminhoGravar);
                    if (!File.Exists(@caminhoGravar + @nomeArquivo))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlString);
                        doc.Save(@caminhoGravar + @nomeArquivo);
                    }
                }
            }catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao gravar o XML na Pasta: " + err.Message);
            }
        }
        public NFCeDownloadProc ConsultaNFCeEmitida(string CNPJ, string chave)
        {
            try
            {
                ImpressaoNFCe imp = new ImpressaoNFCe();
                imp.tipo = "PDF";
                   String url = "https://nfce.ns.eti.br/v1/nfce/get";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = chave,
                        tpAmb = tpAmbiente,
                        impressao = imp
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var nota = JsonConvert.DeserializeObject<NFCeDownloadProc>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }


        public RetornoInutilizacao NS_DownloadNFCeInutilizada(string CNPJ, string idInut)
        {
            try
            {
                ImpressaoNFCe imp = new ImpressaoNFCe();
                imp.tipo = "PDF";
                String url = "https://nfce.ns.eti.br/v1/nfce/get/inut";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        idInut = idInut,
                        tpAmb = tpAmbiente
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var nota = JsonConvert.DeserializeObject<RetornoInutilizacao>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }


        public NFeDownloadProc55 ConsultaNFeEmitida(string CNPJ, string chave)
        {
            try
            {
                ImpressaoNFCe imp = new ImpressaoNFCe();
                imp.tipo = "PDF";
                String url = "https://nfe.ns.eti.br/nfe/get";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = chave,
                        printCEAN = false,
                        tpAmb = tpAmbiente,
                        tpDown = "XP"
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var nota = JsonConvert.DeserializeObject<NFeDownloadProc55>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public NFCeDownloadProc NS_ConsultaStatusNota65(string chave)
        {
            try
            {
                String url = "https://nfce.ns.eti.br/v1/nfce/status";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = chave,
                        tpAmb = tpAmbiente
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var nota = JsonConvert.DeserializeObject<NFCeDownloadProc>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public RetornoCancelamento ns_CancelarNF(Nfe nfe, string justificativa)
        {
            try
            {
                String url = "https://nfce.ns.eti.br/v1/nfce/cancel";
                if (nfe.Modelo.Equals("55"))
                    url = "https://nfe.ns.eti.br/nfe/cancel";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = nfe.Chave,
                        tpAmb = tpAmbiente,
                        dhEvento = DateTime.Now.ToString("s") + "-03:00",
                        nProt = nfe.Protocolo,
                        xJust = justificativa
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<RetornoCancelamento>(result);
                    return nota;
                }
            }
            catch (WebException ex)
            {
                RetConsultaCertificadoNS retorno = new RetConsultaCertificadoNS();
                string valorRetornado = "";
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        valorRetornado = streamReader.ReadToEnd();
                        retorno = JsonConvert.DeserializeObject<RetConsultaCertificadoNS>(valorRetornado);
                    }
                }
                return null;
            }
        }

        public RetornoCancelamento ns_DownloadEventoCanceladoOuCCE55(Nfe nfe, bool cancelamento, bool cce, string nSeqEventoDesejadoCCE)
        {
            try
            {
                string tpEventoSolicitado = "";
                string nSeqEvento = "1";
                if (cancelamento == true)
                    tpEventoSolicitado = "CANC";
                else if (cce == true)
                {
                    nSeqEvento = nSeqEventoDesejadoCCE;
                    tpEventoSolicitado = "CCE";
                }
                String url = "https://nfe.ns.eti.br/nfe/get/event";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = nfe.Chave,
                        tpAmb = tpAmbiente,
                        tpDown = "XP",
                        tpEvento = tpEventoSolicitado,
                        nSeqEvento = nSeqEvento
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<RetornoCancelamento>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public Inutilizacao ns_InutilizarNFCe(Nfe nfe, string justificativa)
        {
            try
            {
                String url = "https://nfce.ns.eti.br/v1/nfce/inut";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        cUF = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Ibge,
                        tpAmb = tpAmbiente,
                        ano = nfe.DataCadastro.Year.ToString().Substring(2, 2),
                        CNPJ = Sessao.empresaFilialLogada.Cnpj,
                        serie = nfe.Serie,
                        nNFIni = nfe.NNf,
                        nNFFin = nfe.NNf,
                        xJust = justificativa
                    }) ;

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<Inutilizacao>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public Inutilizacao ns_InutilizarNFe(Nfe nfe, string justificativa)
        {
            try
            {
                String url = "https://nfe.ns.eti.br/nfe/inut";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        cUF = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Ibge,
                        tpAmb = tpAmbiente,
                        ano = nfe.DataCadastro.Year.ToString().Substring(2, 2),
                        CNPJ = Sessao.empresaFilialLogada.Cnpj,
                        serie = nfe.Serie,
                        nNFIni = int.Parse(nfe.NNf),
                        nNFFin = int.Parse(nfe.NNf),
                        xJust = justificativa
                    });

                    streamWriter.Write(json);
                    MessageBox.Show(json);
                }
               
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<Inutilizacao>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public RetornoInutilizacao ns_DownloadEventoInutilizacaoNFE(Nfe nfe)
        {
            try
            {
                String url = "https://nfe.ns.eti.br/nfe/get/inut";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chave = nfe.Chave,
                        tpAmb = tpAmbiente,
                        tpDown = "X"
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<RetornoInutilizacao>(result);
                    return nota;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public RetornoCartaoCorrecao ns_GerarCartaoCorrecao(Nfe nfe, string correcao, int sequenciaEvento)
        {
            try
            {
                String url = "https://nfe.ns.eti.br/nfe/cce";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");
                string tpAmbiente = "";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpAmbiente = "1";
                else
                    tpAmbiente = "2";
                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        chNFe = nfe.Chave,
                        tpAmb = tpAmbiente,
                        dhEvento = DateTime.Now.ToString("s") + "-03:00",
                        nSeqEvento = sequenciaEvento,
                        xCorrecao = correcao
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);

                    var nota = JsonConvert.DeserializeObject<RetornoCartaoCorrecao>(result);
                    return nota;
                }
            }
            catch (WebException ex)
            {
                string retorno = "";
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        retorno = streamReader.ReadToEnd();
                    }

                    switch (System.Convert.ToInt32(response.StatusCode))
                    {
                        case 401:
                            {
                                GenericaDesktop.ShowErro("Token não enviado ou inválido");
                                break;
                            }

                        case 403:
                            {
                                GenericaDesktop.ShowErro("Token sem permissão");
                                break;
                            }

                        case 404:
                            {
                                GenericaDesktop.ShowErro("Não encontrado, verifique o retorno para mais informações");
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
                return null;
            }
        }

        public dynamic anagu_LoginPIX()
        {
            try
            {
                String url = "https://pix.anagu.com.br/autenticacao/v1/login";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        senha = "123",
                        usuario = "marcelo.xs@hotmail.com"
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string respostaToken = retorno["token"].ToString();
                    return retorno;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }
        public void gerarPDF3(Nfe nfe, String pdf, String chave, bool imprimir)
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
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                }
            }
            else if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("55"))
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
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                    frmPDF.ShowDialog();
                }

            }
        }
        public void gerarPDF2(String pdf, String chave, bool imprimir, String caminhoSalvar, String nomeArquivo)
        {
            if (!Directory.Exists(caminhoSalvar))
                Directory.CreateDirectory(caminhoSalvar);
            if (!File.Exists(@caminhoSalvar + @"\" + nomeArquivo + ".pdf"))
            {
                byte[] bytes = Convert.FromBase64String(pdf);
                System.IO.FileStream stream = new FileStream(@caminhoSalvar + @"\" + nomeArquivo + ".pdf", FileMode.CreateNew);
                System.IO.BinaryWriter writer =
                    new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
            if (imprimir == true)
            {
               // Process.Start(@caminhoSalvar + @"\" + nomeArquivo + ".pdf");
                FrmPDF frmPDF = new FrmPDF(@caminhoSalvar + @"\" + nomeArquivo + ".pdf");
                frmPDF.ShowDialog();
            }
        }

        public dynamic anagu_GerarPIX(Pessoa cliente, Venda venda, string valor, string tokenAut)
        {
            try
            {
                string cnpj = Sessao.empresaFilialLogada.Cnpj;
                string nomeCliente = Sessao.empresaFilialLogada.RazaoSocial;
                if (cliente != null)
                {
                    if (!String.IsNullOrEmpty(cliente.Cnpj))
                    {
                        cnpj = cliente.Cnpj;
                    }
                    nomeCliente = cliente.RazaoSocial;
                }
                String url = "https://pix.anagu.com.br/pedidos/v1/pedido";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers["Authorization"] = "Bearer " + tokenAut;

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        cpfCNPJ = cnpj,
                        descricaoCobranca = "PIX REF. VENDA " + venda.Id,
                        idPedidoLoja = venda.Id,
                        nomeRazaoSocial = nomeCliente,
                        valor = valor
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string respostaToken = "";
                    if (retorno != null)
                        respostaToken = retorno["qrCode"].ToString();
                    return respostaToken;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao Gerar QRCODE do PIX: " + err.Message);
                return null;
            }
        }

        public dynamic anagu_SimularRecebimentoPix(string qrCode, string tokenAut)
        {
            try
            {
                String url = "https://pix.anagu.com.br/pedidos/v1/simularpagamento";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers["Authorization"] = "Bearer " + tokenAut;

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        pix = qrCode
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string pagamento = "";
                    if (retorno != null)
                        pagamento = retorno["texto"].ToString();
                    return pagamento;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao Gerar QRCODE do PIX: " + err.Message);
                return null;
            }
        }


        //public async Task<string> Anagu_gerarQrCodePix(Pessoa cliente, Venda venda, string valor, string tokenAut)
        //{
        //    string cnpj = Sessao.empresaFilialLogada.Cnpj;
        //    string nomeCliente = Sessao.empresaFilialLogada.RazaoSocial;
        //    if (cliente != null)
        //    {
        //        if (!String.IsNullOrEmpty(cliente.Cnpj))
        //        {
        //            cnpj = cliente.Cnpj;
        //        }
        //        nomeCliente = cliente.RazaoSocial;
        //    }
        //    using (var request = new HttpRequestMessage(HttpMethod.Post, "http://pix.anagu.com.br/pedidos/v1/pedido"))
        //    {
        //        HttpClient client = new HttpClient();
        //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAut);

        //        request.Content = JsonContent.Create(new
        //        {
        //            cpfCNPJ = cnpj,
        //            descricaoCobranca = "PIX REF. VENDA " + venda.Id,
        //            idPedidoLoja = venda.Id,
        //            nomeRazaoSocial = nomeCliente,
        //            valor = valor
        //        });

        //        var response = await client.SendAsync(request);

        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            var retorno = response.Content.ReadAsStringAsync().Result;
        //            JObject json = JObject.Parse(retorno);
        //            string retQr = json.GetValue("qrCode").ToString();
        //            return retQr;

        //            //MessageBox.Show(json.GetValue("qrCode").ToString());
        //            //var ret = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(retorno);
        //            //var resposta = ret["token"].ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Falha ao Gerar QRCODE: " + response.StatusCode + " " + response.ReasonPhrase);
        //            return null;
        //        }
        //    }
        //}
        public class ImpressaoNFCe
        {
            public string tipo { get; set; }
            public bool ecologica { get; set; }

        }

        public static string enviaConteudoParaAPI_NSTec(string conteudo, string url, string tpConteudo)
        {
            string retorno = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["X-AUTH-TOKEN"] = "VFhUIElORk9STUFUSUNBT3JQSEQ=";

            if (tpConteudo == "txt")
            {
                httpWebRequest.ContentType = "text/plain;charset=utf-8";
            }
            else if (tpConteudo == "xml")
            {
                httpWebRequest.ContentType = "application/xml;charset=utf-8";
            }
            else
            {
                httpWebRequest.ContentType = "application/json;charset=utf-8";
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(conteudo);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    retorno = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        retorno = streamReader.ReadToEnd();
                    }

                    switch (System.Convert.ToInt32(response.StatusCode))
                    {
                        case 401:
                            {
                                GenericaDesktop.ShowErro("Token não enviado ou inválido");
                                break;
                            }

                        case 403:
                            {
                                GenericaDesktop.ShowErro("Token sem permissão");
                                break;
                            }

                        case 404:
                            {
                                GenericaDesktop.ShowErro("Não encontrado, verifique o retorno para mais informações");
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }

            return retorno;
        }

        public ManifestacaoNS_retorno Ns_ConfirmaNotaManifesto(string CNPJ, string chave)
        {
            try
            {
                string json = "";
                Manifestacao manifesto = new Manifestacao();
                manifesto.tpEvento = 210200;
                String url = "https://ddfe.ns.eti.br/events/manif";
                var requisicaoWeb = (HttpWebRequest)WebRequest.Create(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    json = new JavaScriptSerializer().Serialize(new
                    {
                        CNPJInteressado = CNPJ,
                        chave = chave,
                        manifestacao = manifesto
                    });

                    streamWriter.Write(json);

                }
                var resultado = enviaConteudoParaAPI_NSTec(json, url, "JSON");
                if (resultado != null)
                {
                    ManifestacaoNS_retorno ret = JsonConvert.DeserializeObject<ManifestacaoNS_retorno>(resultado);
                    return ret;
                }
                else
                    return null;
            }
            catch (Exception err)
            {
                gravarLinhaLog("[ERRO_GRAVAR_LINHA_LOG]: GENERICADESKTOP/Ns_ConfirmaNotaManifesto " + err.Message, "EXCEPTION_CONFIRMARMANIFESTO");
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

       
    public ManifestacaoNS_retorno Ns_RejeitarNotaManifesto(string CNPJ, string chave)
        {
            try
            {
                Manifestacao manifesto = new Manifestacao();
                manifesto.tpEvento = 210220;
                String url = "https://ddfe.ns.eti.br/events/manif";
                var requisicaoWeb = (HttpWebRequest)WebRequest.Create(url);
                //var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("x-auth-token", "VFhUIElORk9STUFUSUNBT3JQSEQ=");

                using (var streamWriter = new
                        StreamWriter(requisicaoWeb.GetRequestStream())
                        )
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        CNPJInteressado = CNPJ,
                        chave = chave,
                        manifestacao = manifesto
                    });

                    streamWriter.Write(json);

                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var resultado = JsonConvert.DeserializeObject<ManifestacaoNS_retorno>(result);
                    return resultado;
                }
            }
            catch (Exception err)
            {
                gravarLinhaLog("[ERRO_GRAVAR_LINHA_LOG]: GENERICADESKTOP/Ns_RejeitarNotaManifesto " + err.Message, "EXCEPTION_REJEITARNOTA");
                GenericaDesktop.ShowErro(err.Message);
                return null;
            }
        }

        public class Manifestacao
        {
            public int tpEvento { get; set; }
        }


        public Ncm_Json.Rootobject consultaNCM()
        {
            try
            {
                using (var streamReader = new StreamReader("NCM.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<Ncm_Json.Rootobject>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON NCM: " + err.Message);
                return null;
            }
        }

        public BancosJson.Rootobject consultaBancos()
        {
            try
            {
                using (var streamReader = new StreamReader("BANCOS.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<BancosJson.Rootobject>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON BANCOS: " + err.Message);
                return null;
            }
        }

        public IList<Cest_Json.TabelaCest> consultaCEST()
        {
            try
            {
                using (var streamReader = new StreamReader("CEST.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<List<Cest_Json.TabelaCest>>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON CEST: " + err.Message);
                return null;
            }
        }

        public IList<CfopAux.Class1> consultarCFOP_JSON()
        {
            try
            {
                using (var streamReader = new StreamReader("CFOP.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<List<CfopAux.Class1>>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON CFOP: " + err.Message);
                return null;
            }
        }

        public CstAuxPisCofins.Rootobject consultarCSTPISCOFINS_JSON()
        {
            try
            {
                using (var streamReader = new StreamReader("CSTPISCOFINS.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<CstAuxPisCofins.Rootobject>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON CST PIS/COFINS: " + err.Message);
                return null;
            }
        }

        public IList<CstAuxIPI.CSTIPIAUX> consultarCSTIPI_JSON()
        {
            try
            {
                using (var streamReader = new StreamReader("CSTIPI.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<List<CstAuxIPI.CSTIPIAUX>>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON CST IPI: " + err.Message);
                return null;
            }
        }

        public IList<AnpAux.AnpAuxiliar> consultarANP_JSON()
        {
            try
            {
                using (var streamReader = new StreamReader("ANP.json"))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var json = JsonConvert.DeserializeObject<List<AnpAux.AnpAuxiliar>>(result);
                    return json;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro JSON ANP: " + err.Message);
                return null;
            }
        }


        public string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return String.Empty;

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static string RemoveCaracteres(string texto)
        {
            string ret;
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\\s]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            ret = rgx.Replace(texto, replacement);
            return ret;
        }
        public static string RemoveAcentos(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string Criptografa(String valor)
        {
            Byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
            String dadosCriptografados = Convert.ToBase64String(bytes);
            return dadosCriptografados;
        }

        public static string Descriptografa(String valor)
        {
            Byte[] bytes = Convert.FromBase64String(valor);
            String dadosDescriptografados = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
            return dadosDescriptografados;
        }

        public static string MascaraTelefone(string strNumero)
        {
            try
            {
                // por omissão tem 10 ou menos dígitos
                string strMascara = "{0:0 0000-0000}";
                // converter o texto em número
                long lngNumero = Convert.ToInt64(strNumero);

                if (strNumero.Length == 9)
                    strMascara = "{0:0 0000-0000}";
                if (strNumero.Length == 8)
                    strMascara = "{0:0000-0000}";

                return string.Format(strMascara, lngNumero);
            }
            catch
            {
                return strNumero;
            }
        }

        public static string MascaraCep(string cep)
        {
            try
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }
            catch
            {
                return "";
            }
        }

        public string FormatarCNPJ(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
        public string FormatarCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

      public void abrirNavegador()
        {
            string target = "http://www.google.com.br";
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }

        public static void gravarLinhaLog(string registro, string operacaoRealizada)
        {
            string caminho = @".\logs\";

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(@".\logs\" + operacaoRealizada +"_"+ DateTime.Now.ToString("yyyy-MM-dd") + ".log", true))
                {
                    outputFile.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + " - \n" + registro);
                }
            }

            catch (Exception ex)
            {
                gravarLinhaLog("[ERRO_GRAVAR_LINHA_LOG]: " + ex.Message, "EXCEPTION " + operacaoRealizada);
            }

        }

        public static void Leitura_Contingencia(string numeroNota, string caminhoArquivoContigencia_ret)
        {
            NfeController nfeController = new NfeController();
               caminhoArquivoContigencia_ret = caminhoArquivoContigencia_ret + "NFCe" + numeroNota + ".txt";
            String linha = "";
            string chave = "";
            // Abre o arquivo contingencia_ret para leitura EX.de caminho: "D:\NSNFCe\Processados\nsConcluido\contingencia_ret"
            StreamReader arquivo = new StreamReader(@caminhoArquivoContigencia_ret);

            int i = 0;
            while (true)
            {
                linha = arquivo.ReadLine();
                if (linha != null)
                {
                    string[] DadosColetados = linha.Split('|');
                    if (!String.IsNullOrEmpty(linha) && DadosColetados.Length > 4)
                    {
                        chave = DadosColetados[5].Substring(3, 44);
                        Nfe nfe = new Nfe();
                        nfe = nfeController.selecionarNotaPorChave(chave);
                        if(nfe != null)
                        {
                            nfe.Status = DadosColetados[4].ToString();
                            if (!String.IsNullOrEmpty(DadosColetados[1].ToString()))
                            {
                                nfe.Protocolo = DadosColetados[1].ToString();
                                if(DadosColetados.Length > 7)
                                    nfe.DataProtocolo = DateTime.Parse(DadosColetados[8].ToString());
                            }
                        }
                    }

                    //dadosLidos.Add(new Dados { data = DadosColetados[0], Hora = DadosColetados[1], Local = DadosColetados[2] });
                }
                else
                    break;
            }
        }

        public void AbrirPastaExplorer(string path)
        {
            bool isfile = System.IO.File.Exists(path);
            if (isfile)
            {
                string argument = @"/select, " + path;
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                bool isfolder = System.IO.Directory.Exists(path);
                if (isfolder)
                {
                    string argument = @"/select, " + path;
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }

        public void atualizarEstoqueConciliado(Produto produto, double quantidade, bool entrada, string origem, string descricao, Pessoa pessoa, DateTime dataAjuste, BalancoEstoque balancoEstoque)
        {
            Estoque estoque = new Estoque();
            EstoqueController estoqueController = new EstoqueController();
            estoque.Conciliado = true;
            estoque.DataEntradaSaida = dataAjuste;
            estoque.BalancoEstoque = balancoEstoque;
            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
            if (entrada == true)
                estoque.Entrada = true;
            else
                estoque.Saida = true;
            estoque.Pessoa = pessoa;
            estoque.Origem = origem;
            estoque.Produto = produto;
            estoque.Quantidade = quantidade;
            estoque.Descricao = descricao;
            if (entrada == true)
                produto.Estoque = produto.Estoque + quantidade;
            else
                produto.Estoque = produto.Estoque - quantidade;
            Controller.getInstance().salvar(estoque);
            Controller.getInstance().salvar(produto);
        }
        public void atualizarEstoqueNaoConciliado(Produto produto, double quantidade, bool entrada, string origem, string descricao, Pessoa pessoa, DateTime dataAjuste, BalancoEstoque balancoEstoque)
        {
            Estoque estoque = new Estoque();
            EstoqueController estoqueController = new EstoqueController();
            estoque.Conciliado = false;
            estoque.DataEntradaSaida = dataAjuste;
            estoque.BalancoEstoque = balancoEstoque;
            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
            if (entrada == true)
                estoque.Entrada = true;
            else
                estoque.Saida = true;
            estoque.Pessoa = pessoa;
            estoque.Origem = origem;
            estoque.Produto = produto;
            estoque.Quantidade = quantidade;
            estoque.Descricao = descricao;
            if (entrada == true)
                produto.EstoqueAuxiliar = produto.EstoqueAuxiliar + quantidade;
            else
                produto.EstoqueAuxiliar = produto.EstoqueAuxiliar - quantidade;
            Controller.getInstance().salvar(estoque);
            Controller.getInstance().salvar(produto);
        }

        public void conferirTabelaEstoqueEAtualizarProduto(Produto produto, EmpresaFilial filial)
        {
            //Nao Conciliado
            EstoqueDAO estoqueDAO = new EstoqueDAO();
            double qtdProd = estoqueDAO.calcularEstoqueNaoConciliadoPorProduto(produto.Id, filial);
            produto.EstoqueAuxiliar = qtdProd;
            //Conciliado
            double qtdProdC = estoqueDAO.calcularEstoqueConciliadoPorProduto(produto.Id, filial);
            produto.Estoque = qtdProdC;
            Controller.getInstance().salvar(produto);
        }

        public bool enviarEmail(String emailDestino, String assuntoEmail, String tituloCorpoEmail, String corpoEmail, List<String> anexo)
        {
            try
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.SenhaEmail) && !String.IsNullOrEmpty(Sessao.parametroSistema.PortaEmail) && !String.IsNullOrEmpty(Sessao.parametroSistema.ServidorEmail))
                {
                    MailMessage Email;
                    StringBuilder mensagemEmail = new StringBuilder();
                    Email = new MailMessage();
                    Email.To.Add(new MailAddress(emailDestino));
                    Email.From = new MailAddress(Sessao.parametroSistema.Email, Sessao.parametroSistema.NomeRemetenteEmail);
                    Email.Subject = (assuntoEmail);
                    Email.IsBodyHtml = true;
                    mensagemEmail.Append("<span style=\"font-weight: bold; font-size: 18px\">" + tituloCorpoEmail + "</span><br />");
                    mensagemEmail.Append("<br />" + corpoEmail + "<br />");
                    Email.Body = mensagemEmail.ToString();
                    if (anexo != null)
                    {
                        for(int i = 0; i < anexo.Count; i++)
                        {
                            Email.Attachments.Add(new Attachment(anexo[i]));
                        }
                    }
                        
                    SmtpClient cliente = new SmtpClient(Sessao.parametroSistema.ServidorEmail, int.Parse(Sessao.parametroSistema.PortaEmail));
                    using (cliente)
                    {
                        cliente.Credentials = new System.Net.NetworkCredential(Sessao.parametroSistema.Email, (GenericaDesktop.Descriptografa(Sessao.parametroSistema.SenhaEmail)));
                        if (Sessao.parametroSistema.AutenticacaoSsl == true)
                            cliente.EnableSsl = true;
                        else
                            cliente.EnableSsl = false;

                        cliente.Send(Email);
                    }
                    Email.Attachments.Clear();
                    return true;
                }
                else
                {
                    GenericaDesktop.ShowAlerta("E-mail não configurado no sistema, acesse o menu parâmetros do sistema na aba Email e configure!");
                    return false;
                }
            }
            catch (Exception erroEmail)
            {
                GenericaDesktop.ShowAlerta("Falha no teste de e-mail \n\n" + erroEmail.Message);
                return false;
            }
        }
    }
}
