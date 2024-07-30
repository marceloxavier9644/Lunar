using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LunarBase.Utilidades.SerproGov
{
    public class SerproApiService
    {
        private readonly HttpClient _httpClient;
        private string consumerKey = "fRKLVyzFa8yb7Bniz9zY1KS3vvoa";
        private string consumerSecret = "oC7GfMaAGTtj6JSYbWL6y7Bjcpsa";

        public SerproApiService()
        {
            // Configurar o certificado
            HttpClientHandler handler = new HttpClientHandler();

            // URL do certificado no servidor FTP
            string ftpUrl = "ftp://lunarsoftware@ftp.lunarsoftware.com.br/Lunarerp/txt.pfx";

            // Baixar o arquivo do certificado temporariamente
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential("lunarsoftware", "Aramxs@11");
            string tempFilePath = Path.GetTempFileName();
            ftpClient.DownloadFile(ftpUrl, tempFilePath);

            // Carregar o certificado a partir do arquivo temporário
            X509Certificate2 cert = new X509Certificate2(tempFilePath, "123456");
            handler.ClientCertificates.Add(cert);

            // Criar o cliente HTTP com o handler configurado
            _httpClient = new HttpClient(handler);

            // Configurar o cabeçalho de autorização
            string combinedKey = consumerKey + ":" + consumerSecret;
            byte[] bytes = Encoding.UTF8.GetBytes(combinedKey);
            string base64CombinedKey = Convert.ToBase64String(bytes);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64CombinedKey);
            _httpClient.DefaultRequestHeaders.Add("Role-Type", "TERCEIROS");
        }

        public async Task<TokenResponse> AutenticacaoSerpro()
        {
            try
            {
                // Configurar a requisição HTTP
                var requestContent = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await _httpClient.PostAsync("https://autenticacao.sapi.serpro.gov.br/authenticate", requestContent);

                // Verificar a resposta
                if (response.IsSuccessStatusCode)
                {
                    // Ler o conteúdo da resposta como string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Verificar se a resposta está vazia
                    if (string.IsNullOrWhiteSpace(responseContent))
                    {
                        throw new Exception("A resposta da API está vazia.");
                    }

                    // Desserializar a resposta JSON para um objeto TokenResponse
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                    // Verificar se o objeto tokenResponse não é nulo
                    if (tokenResponse == null)
                    {
                        throw new Exception("Não foi possível desserializar a resposta da API.");
                    }

                    // Retornar o token
                    return tokenResponse;
                }
                else
                {
                    // Lançar uma exceção se a resposta da API indicar um erro
                    throw new Exception($"Erro ao chamar a API DAS MEI: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Capturar e relançar exceções para facilitar a depuração
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public async Task<string> EmitirGuiaMeiAsync(string jwtToken, string accessToken, string cnpjContribuinte, string anoReferencia, string mesReferencia)
        {
            // Construa o corpo da solicitação
            var requestBody = new
            {
                contratante = new { numero = "28145398000173", tipo = 2 },
                autorPedidoDados = new { numero = cnpjContribuinte, tipo = 2 },
                contribuinte = new { numero = cnpjContribuinte, tipo = 2 },
                pedidoDados = new { idSistema = "PGMEI", idServico = "GERARDASPDF21", versaoSistema = "1.0", dados = "{\"periodoApuracao\": \"" + anoReferencia + mesReferencia.PadLeft(2, '0') + "\"}" }
            };

            // Converte o corpo da solicitação em JSON
            var jsonRequest = JsonConvert.SerializeObject(requestBody);

            // Configurar a requisição HTTP
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Add("jwt_token", jwtToken);
            var response = await _httpClient.PostAsync("https://gateway.apiserpro.serpro.gov.br/integra-contador/v1/Emitir", requestContent);

            // Verificar a resposta
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                throw new Exception($"Erro ao chamar a API DAS MEI: {response.StatusCode}");
            }
        }

        public async Task<string> ConsultarGuiaMeiAsync(string jwtToken, string accessToken, string cnpjContribuinte)
        {
            try
            {
                // Construa o corpo da solicitação
                var requestBody = new
                {
                    contratante = new { numero = "00000000000100", tipo = 2 },
                    autorPedidoDados = new { numero = "00000000000100", tipo = 2 },
                    contribuinte = new { numero = "00000000000100", tipo = 2 },
                    pedidoDados = new { idSistema = "PGMEI", idServico = "DIVIDAATIVA24", versaoSistema = "1.0", dados = "{\"anoCalendario\": \"2020\"}" }
                };

                // Converte o corpo da solicitação em JSON
                var jsonRequest = JsonConvert.SerializeObject(requestBody);

                // Configurar a requisição HTTP
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Configurar o cliente HTTP com o token de acesso e JWT
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "06aef429-a981-3ec5-a1f8-71d38d86481e");//accessToken
                //client.DefaultRequestHeaders.Add("jwt_token", jwtToken);

                // Enviar a solicitação POST
                var response = await client.PostAsync("https://gateway.apiserpro.serpro.gov.br/integra-contador-trial/v1/Consultar", requestContent);

                // Verificar a resposta
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new Exception($"Erro ao chamar a API DAS MEI: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }




        public class PdfService
        {
            public void SalvarPdf(string pdfBase64)
            {
                // Decodificar o PDF da string Base64
                byte[] pdfBytes = Convert.FromBase64String(pdfBase64);

                // Salvar o PDF em um arquivo
                string anoMes = DateTime.Now.ToString("yyyy-MM");
                string folderPath = @"C:\Lunar\DASMEI";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, $"{anoMes}.pdf");
                File.WriteAllBytes(filePath, pdfBytes);

                // Abrir o PDF no visualizador padrão
                System.Diagnostics.Process.Start(filePath);
            }
        }

        public class TokenResponse
        {
            public int expires_in { get; set; }
            public string scope { get; set; }
            public string token_type { get; set; }
            public string access_token { get; set; }
            public string jwt_token { get; set; }
            public string jwt_pucomex { get; set; }
        }
    }
}