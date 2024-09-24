using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LunarBase.Utilidades.NFSe
{
    public class FocusNFSeClient
    {
        private static readonly HttpClient client = new HttpClient();
        private string apiKey;
        private string baseUrl;

        public FocusNFSeClient(string apiKey, bool homologacao)
        {
            this.apiKey = apiKey;
            this.baseUrl = homologacao ? "https://homologacao.focusnfe.com.br/v2" : "https://api.focusnfe.com.br/v2";
            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
        }

        // POST para criar uma NFSe
        public async Task<string> CriarNFSeAsync(string referencia, NFSeRequest nfse)
        {
            var url = $"{baseUrl}/nfse?ref={referencia}";
            var json = JsonConvert.SerializeObject(nfse);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // GET para consultar a NFSe pelo código de referência
        public async Task<string> ConsultarNFSeAsync(string referencia)
        {
            var url = $"{baseUrl}/nfse/{referencia}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // DELETE para cancelar uma NFSe
        public async Task<string> CancelarNFSeAsync(string referencia)
        {
            var url = $"{baseUrl}/nfse/{referencia}";
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
