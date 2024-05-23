using LunarBase.Classes;
using LunarBase.ControllerBO;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lunar.Utils.LunarChatIntegracao
{
    public class LunarChatMensagem
    {
      
        public class EnviarMensagemWhatsapp
        {
            Logger logger = new Logger();
            private static readonly HttpClient httpClient = new HttpClient();
            private ParametroSistema parametro;
            private ParametroSistemaController parametroSistemaController;

            public async Task SendMessageAsync(WebhookMessage message)
            {
                parametro = new ParametroSistema();
                parametroSistemaController = new ParametroSistemaController();
                parametro.Id = 1;
                parametro = (ParametroSistema)parametroSistemaController.selecionar(parametro);
                string webhookUrl = parametro.TokenWhats;

                if (!string.IsNullOrEmpty(webhookUrl))
                {
                    var json = JsonConvert.SerializeObject(message);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(webhookUrl, content);
                    logger.WriteLog($"JSON Enviado: {json}", "Logs");
                    logger.WriteLog($"Webhook URL: {webhookUrl}", "Logs");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully!");
                    }
                    else
                    {
                        MessageBox.Show($"Falha ao disparar mensagem. Status code: {response.StatusCode}");
                        var responseContent = await response.Content.ReadAsStringAsync();
                        logger.WriteLog($"Falha ao disparar whatsapp LunarChat: {responseContent}", "Logs");
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Esta função não está habilitada. Por favor, entre em contato com seu representante para mais informações.");
                }
            }

            public async Task<string> EnviarArquivoFtp(string caminhoPdf)
            {
                string ftpServerUrl = "ftp://ftp.lunarsoftware.com.br/anexo/";
                string ftpUsername = "lunarsoftware";
                string ftpPassword = "Aramxs@11";
                string filePath = caminhoPdf;

                try
                {
                    // Crie uma solicitação FTP
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServerUrl + Path.GetFileName(filePath));
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                    // Leia o arquivo local
                    byte[] fileContents;
                    using (FileStream sourceStream = File.OpenRead(filePath))
                    {
                        fileContents = new byte[sourceStream.Length];
                        await sourceStream.ReadAsync(fileContents, 0, fileContents.Length);
                    }

                    // Envie o arquivo para o servidor FTP
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
                    }

                    // Obtenha o link do arquivo no servidor FTP
                    string linkArquivo = ftpServerUrl + Path.GetFileName(filePath);
                    string linkSemFtp = linkArquivo.Substring(6);
                    linkSemFtp = "https://" + linkSemFtp;
                    return linkSemFtp;
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Erro no upload: {ex.Message}");
                    return null;
                }
            }
            public static string TratarTelefone(string ddd, string telefone)
            {
                if (telefone.Length == 8)
                {
                    telefone = "9" + telefone;
                }
                else if (telefone.Length != 9)
                {
                    GenericaDesktop.ShowAlerta("O número de telefone deve ter 8 ou 9 dígitos.");
                }
                string telefoneCompleto = "55" + ddd + telefone;

                return telefoneCompleto;
            }
        }
        public class WebhookMessage
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public string File { get; set; }
        }
    }
}
