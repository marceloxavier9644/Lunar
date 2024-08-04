using LunarBase.Classes;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
            string ip = "";
            string porta = "";
            if (!String.IsNullOrEmpty(Sessao.atendimentoConfig.IpServidor))
            {
                ip = Sessao.atendimentoConfig.IpServidor;
                porta = Sessao.atendimentoConfig.PortaApi;
            }
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("http://"+ip+":"+porta+ "/api/Mesas/DeletarTodas");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
