using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HttpClient = System.Net.Http.HttpClient;
using HttpResponseMessage = System.Net.Http.HttpResponseMessage;

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
 
            public async Task<string> SendMessageAsync(string number, string body)
            {
                string _baseUri = "https://backend.lunarchat.com.br/api/messages/send";
                string _token = Sessao.parametroSistema.TokenWhats; // Certifique-se de que este token é válido

                var requestBody = new
                {
                    number = number,
                    body = body
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, _baseUri)
                    {
                        Content = content
                    };

                    // Adiciona o token Bearer ao cabeçalho de autorização
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    try
                    {
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        {
                            // Log detalhes do erro
                            string responseBy = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Failed to send message. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {responseBy}");
                            return null;
                        }

                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Request error: {e.Message}");
                        return null;
                    }
                }
            }

            public async Task<string> SendMessageAdminAsync(string number, string body)
            {
                string _baseUri = "https://backend.lunarchat.com.br/api/messages/send";
                string _token = "41f7ff53c1dc5cf4f3db1f331"; 

                var requestBody = new
                {
                    number = number,
                    body = body
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, _baseUri)
                    {
                        Content = content
                    };

                    // Adiciona o token Bearer ao cabeçalho de autorização
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    try
                    {
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        {
                            // Log detalhes do erro
                            string responseBy = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Failed to send message. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {responseBy}");
                            return null;
                        }

                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Request error: {e.Message}");
                        return null;
                    }
                }
            }

            public async Task<string> SendMediaMessageAsync(string number, string filePath)
            {
                string _baseUri = "https://backend.lunarchat.com.br/api/messages/send";
                string _token = Sessao.parametroSistema.TokenWhats; // Certifique-se de que este token é válido

                using (var httpClient = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(new StringContent(number), "number");

                        // Lê o arquivo em bytes
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        formData.Add(new ByteArrayContent(fileBytes), "medias", Path.GetFileName(filePath));

                        // Adiciona o token Bearer ao cabeçalho de autorização
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                        try
                        {
                            HttpResponseMessage response = await httpClient.PostAsync(_baseUri, formData);
                            if (!response.IsSuccessStatusCode)
                            {
                                // Log detalhes do erro
                                string responseBy = await response.Content.ReadAsStringAsync();
                                Console.WriteLine($"Failed to send media message. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {responseBy}");
                                return null;
                            }

                            string responseBody = await response.Content.ReadAsStringAsync();
                            return responseBody;
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine($"Request error: {e.Message}");
                            return null;
                        }
                    }
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

            public static string capturarNomeParaMensagem(string nomeCompleto)
            {
                if (string.IsNullOrWhiteSpace(nomeCompleto))
                {
                    return nomeCompleto;
                }

                string[] partes = nomeCompleto.Split(' ');
                TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
                string nomeFormatado = "";

                if (partes.Length >= 2)
                {
                    string primeiroNomeFormatado = textInfo.ToTitleCase(partes[0].ToLower());
                    string segundoNomeFormatado = textInfo.ToTitleCase(partes[1].ToLower());
                    nomeFormatado = $"{primeiroNomeFormatado} {segundoNomeFormatado}";
                }
                else
                {
                    nomeFormatado = textInfo.ToTitleCase(nomeCompleto.ToLower());
                }

                return nomeFormatado;
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

