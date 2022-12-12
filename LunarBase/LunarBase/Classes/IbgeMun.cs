using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace LunarBase.Classes
{

    public class Regiao
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
    public class UF
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Regiao Regiao { get; set; }
    }

    public class Mesorregiao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public UF Uf { get; set; }
    }

    public class Estados
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Regiao Regiao { get; set; }

        public static List<Estados> BuscarEstados()
        {
            string url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);
            var est = JsonConvert.DeserializeObject<List<Estados>>(json);

            return est;
        }
    }

    public class Microrregiao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Mesorregiao Mesorregiao { get; set; }
    }

    public class Municipios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Microrregiao Microrregiao { get; set; }

        public static List<Municipios> BuscarMunicipios()
        {
            string url = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios";
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);
            var mun = JsonConvert.DeserializeObject<List<Municipios>>(json);

            return mun;
        }
    }



}
