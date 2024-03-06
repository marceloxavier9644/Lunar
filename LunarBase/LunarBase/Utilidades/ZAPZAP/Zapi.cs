using LunarBase.Classes;
using System.Drawing;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace LunarBase.Utilidades.ZAPZAP
{
    public class Zapi
    {
        private string ClientToken = "F33012d7f14374f1f91ce0084d0ddcca4S";
        //
        public dynamic zapi_EnviarImagem(string numeroTelefone, string imagemBase64, string textoTitulo, string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/send-image";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        phone = numeroTelefone,
                        image = "data:image/png;base64," + imagemBase64,
                        caption = textoTitulo
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                        idret = retorno["messageId"].ToString();
                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Enviar Whatsapp: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_EnviarDocumento(string numeroTelefone, string imagemBase64, string textoTitulo, string idInstancia, string token, string extensaoArquivo)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/send-document/" + extensaoArquivo;
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        phone = numeroTelefone,
                        document = "data:image/png;base64," + imagemBase64,
                        fileName = textoTitulo
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                        idret = retorno["messageId"].ToString();
                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Enviar Whatsapp: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_EnviarTexto(string numeroTelefone, string texto, string idInstancia, string token)
        {
            Logger logger = new Logger();
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/send-text";
      
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        phone = numeroTelefone,
                        message = texto
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        logger.WriteLog("Mensagem enviada","LogMensagem");
                        idret = retorno["messageId"].ToString();
                    }
                    return idret;
                }
            }
            catch (Exception err)
            {
                logger.WriteLog("Falha ao Enviar Whatsapp: " + err.Message, "LogMensagem");
                MessageBox.Show("Falha ao Enviar Whatsapp: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_RetornaQRCodeConexao(string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/qr-code";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        try { idret = retorno["value"].ToString(); } catch { idret = ""; }
                    }
                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Gerar QRCODE do WhatsApp, Verifique com seu representante sobre a ativação: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_RestaurarSessao(string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/restore-session";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                        idret = retorno["value"].ToString();
                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Restaurar Sessão do WhatsApp, Verifique com seu representante sobre a funcionalidade: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_VerificarConexaoInstancia(string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/status";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        if (retorno["connected"].ToString().Equals("True"))
                            MessageBox.Show("Conexão ativa");
                        else if (retorno["connected"].ToString().Equals("False"))
                            MessageBox.Show("Sem Conexão, gere o QRCODE e conecte");
                        else
                            MessageBox.Show("Atenção: Celular sem internet");
                    }

                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Visualizar Status do WhatsApp, verifique com seu representante sobre a funcionalidade: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_VerificarConexaoInstancia_ParaGerarQR(string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/status";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        idret = (retorno["connected"].ToString());
                    }

                    return idret;
                }
            }
            catch (Exception err)
            {
                //GenericaDesktop.ShowErro("Falha ao Visualizar Status do WhatsApp, verifique com seu representante sobre a funcionalidade: " + err.Message);
                return null;
            }
        }

        public dynamic zapi_DesconectarInstancia(string idInstancia, string token)
        {
            try
            {
                String url = "https://api.z-api.io/instances/" + idInstancia + "/token/" + token + "/disconnect";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Client-Token", ClientToken);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        if (retorno["value"].ToString().Equals("True"))
                            MessageBox.Show("Conexão Desativada com Sucesso");
                    }
                    return idret;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Falha ao Visualizar Status do WhatsApp, verifique com seu representante sobre a funcionalidade: " + err.Message);
                return null;
            }
        }

        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }
        //convert string para base64
        static public string EncodeToBase64(string texto)
        {
            try
            {
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
