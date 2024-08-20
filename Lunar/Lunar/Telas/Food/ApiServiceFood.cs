using iTextSharp.text;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDTO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Web.Http;
using HttpClient = System.Net.Http.HttpClient;
using HttpResponseMessage = System.Net.Http.HttpResponseMessage;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Lunar.Telas.Food
{
    public class ApiServiceFood
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<AtendimentoMesa>> GetAtendimentoMesasAsync()
        {
            string ip = "";
            string porta = "";
            if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
            {
                ip = Sessao.atendimentoConfig.IpServidor;
                porta = Sessao.atendimentoConfig.PortaApi;
            }

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://"+ip+":"+porta+"/api/Mesas/ListaMesas");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<AtendimentoMesa> mesas = JsonConvert.DeserializeObject<List<AtendimentoMesa>>(responseBody);
                return mesas;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<AtendimentoMesa>();
            }
        }

        //Deletar as Mesas anteriores
        public static async Task DeleteAllMesasAsync()
        {
            AtendimentoMesaController atendimentoMesaController = new AtendimentoMesaController();
            IList<AtendimentoMesa> listaMesa = atendimentoMesaController.selecionarTodasMesas();
            if (listaMesa.Count > 0)
            {
                string ip = "";
                string porta = "";
                if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
                {
                    ip = Sessao.atendimentoConfig.IpServidor;
                    porta = Sessao.atendimentoConfig.PortaApi;
                }
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync("http://" + ip + ":" + porta + "/api/Mesas/DeletarTodas");
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

        public static async Task<string> PostSalvarAtendimento(AtendimentoDto atendimentoDto)
        {
            string ip = "";
            string porta = "";
            if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
            {
                ip = Sessao.atendimentoConfig.IpServidor;
                porta = Sessao.atendimentoConfig.PortaApi;
            }
            string apiUrl = "http://" + ip + ":" + porta + "/api/Atendimento";

            try
            {
                // Serializa o objeto atendimento para JSON
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Converte propriedades para camelCase
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Ignora propriedades com valor null
                    WriteIndented = true // Formata o JSON para melhor leitura
                };
                string jsonString = JsonSerializer.Serialize(atendimentoDto, options);
                Console.WriteLine("JSON Enviado: " + jsonString);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Faz a requisição POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Disparar mensagem via WebSocket
                    //await WebSocketHandler.BroadcastMesaUpdate(atendimento.Id);
                    WebSocketHandler.SendMessageToAllAsync("Atendimento Criado - " + atendimentoDto.Id).Wait();

                    return responseBody;
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    throw new Exception("Erro ao salvar atendimento: " + response.ReasonPhrase + " - " + responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task<string> PostSalvarAtendimentoConta(AtendimentoContaDto atendimentoContaDto)
        {
            string ip = "";
            string porta = "";
            if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
            {
                ip = Sessao.atendimentoConfig.IpServidor;
                porta = Sessao.atendimentoConfig.PortaApi;
            }
            string apiUrl = "http://" + ip + ":" + porta + "/api/AtendimentoConta";

            try
            {
                // Serializa o objeto atendimento para JSON
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Converte propriedades para camelCase
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Ignora propriedades com valor null
                    WriteIndented = true // Formata o JSON para melhor leitura
                };
                string jsonString = JsonSerializer.Serialize(atendimentoContaDto, options);
                Console.WriteLine("JSON Enviado: " + jsonString);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Faz a requisição POST
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Disparar mensagem via WebSocket
                    //await WebSocketHandler.BroadcastMesaUpdate(atendimento.Id);

                    return responseBody;
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    throw new Exception("Erro ao salvar atendimento: " + response.ReasonPhrase + " - " + responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    public static async Task<string> PostSalvarAtendimentoVinculo(AtendimentoVinculoDto atendimentoVinculoDto)
    {
        string ip = "";
        string porta = "";
        if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
        {
            ip = Sessao.atendimentoConfig.IpServidor;
            porta = Sessao.atendimentoConfig.PortaApi;
        }
        string apiUrl = "http://" + ip + ":" + porta + "/api/AtendimentoVinculo";

        try
        {
            // Serializa o objeto atendimento para JSON
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Converte propriedades para camelCase
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Ignora propriedades com valor null
                WriteIndented = true // Formata o JSON para melhor leitura
            };
            string jsonString = JsonSerializer.Serialize(atendimentoVinculoDto, options);
            Console.WriteLine("JSON Enviado: " + jsonString);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Faz a requisição POST
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                // Disparar mensagem via WebSocket
                //await WebSocketHandler.BroadcastMesaUpdate(atendimento.Id);

                return responseBody;
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                throw new Exception("Erro ao salvar atendimento: " + response.ReasonPhrase + " - " + responseBody);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

        public static async Task<string> PostSalvarAtendimentoMaster(AtendimentoMasterDto atendimentoMasterDto)
        {
            string ip = "";
            string porta = "";
            if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
            {
                ip = Sessao.atendimentoConfig.IpServidor;
                porta = Sessao.atendimentoConfig.PortaApi;
            }

            var url = "http://" + ip + ":" + porta + "/api/SalvarAtendimentoMaster"; 
            var json = JsonConvert.SerializeObject(atendimentoMasterDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                // Log and handle the exception as needed
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }


        public static async Task<List<AtendimentoVinculoDto>> GetListaVinculoPorAtendimento(int idAtendimento)
        {
            try
            {
                // Defina a URL da API com o ID do atendimento
                string apiUrl = $"http://{Sessao.atendimentoConfig.IpServidor}:{Sessao.atendimentoConfig.PortaApi}/api/AtendimentoVinculo/{idAtendimento}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(apiUrl);
                    var vinculos = JsonConvert.DeserializeObject<List<AtendimentoVinculoDto>>(response);
                    return vinculos;
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro($"Erro ao carregar vínculos: {ex.Message}");
                return new List<AtendimentoVinculoDto>(); // Retorne uma lista vazia em caso de erro
            }
        }

    }
}
