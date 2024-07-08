using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Telas.IntegracoesEcommerce.NuvemShopIntegration
{
    public static class NuvemshopApi
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetProductsAsync(string accessToken, int storeId, string emailLoja)
        {
            string url = $"https://api.tiendanube.com/v1/{storeId}/products";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("User-Agent", "Lunar Software ERP (" + emailLoja+")");

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public static async Task<string> CreateProductAsync(string accessToken, int storeId, string productName, string emailLoja)
        {
            string url = $"https://api.tiendanube.com/v1/{storeId}/products";
            string jsonProduct = JsonConvert.SerializeObject(new { name = productName });
            StringContent content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //client.DefaultRequestHeaders.Add("User-Agent", "12068 (" + emailLoja + ")");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
