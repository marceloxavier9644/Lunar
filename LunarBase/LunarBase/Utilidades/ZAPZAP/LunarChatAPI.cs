using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades.ZAPZAP
{
    public class LunarChatAPI
    {
        public async Task<string> SendMessageAsync(string number, string body)
        {
            string _baseUri = "https://backend.lunarchat.com.br/api/messages/send";
            string _token = Sessao.parametroSistema.TokenWhats; 

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
    }
}
