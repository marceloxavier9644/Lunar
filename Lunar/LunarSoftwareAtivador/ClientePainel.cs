using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LunarSoftwareAtivador
{
    public class ClientePainel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public int IEFree { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Bairro { get; set; }
        public int IdRepresentante { get; set; }
    }
    public static class ClienteService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> CadastrarClienteAsync(ClientePainel cliente, string callbackUrl)
        {
            try
            {
                var dataObject = new
                {
                    name = cliente.Nome,
                    email = cliente.Email,
                    cnpj = cliente.CNPJ,
                    ie = cliente.IE,
                    ie_free = 0,
                    trading_name = cliente.NomeFantasia,
                    company_name = cliente.RazaoSocial,
                    cep = cliente.CEP,
                    address = cliente.Endereco,
                    address_number = cliente.Numero,
                    address_complement = cliente.Complemento,
                    uf = cliente.UF,
                    city = cliente.Cidade,
                    phone = cliente.Telefone,
                    callback_url_success = callbackUrl,
                    id_reseller = cliente.IdRepresentante,
                    is_admin = 0,
                    is_reseller = 0,
                    is_actived = 1,
                    is_client = 1,
                    id_bookkeeper = 1,
                    is_blocked = 0
                };

                var dataAux = new { data = dataObject }; // O objeto a ser enviado
                var json = JsonConvert.SerializeObject(dataAux); // Converte o objeto para JSON
                var content = new StringContent(json, Encoding.UTF8, "application/json"); //
                string url = "https://lunarsoftware.com.br/painel/client_register_lunar.php";

                OperatingSystem os = Environment.OSVersion;
                Version version = os.Version;
                if (os.Platform == PlatformID.Win32NT && version.Major == 6 && version.Minor == 1)
                {
                    url = "http://lunarsoftware.com.br/painel/cadastro-sucesso.html";
                }
                var response = await client.PostAsync(url, content);
                //var response = await client.PostAsync("http://localhost:3000/receive_post", content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro HTTP: {e.Message}");
                return false; // Erro no cadastro
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
                return false; // Erro geral
            }
        }
    }
}
