using LunarBase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lunar.Telas.OrdensDeServico.Servicos
{
    public class ItemServicoNfse
    {
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }
        [JsonPropertyName("servicos")]
        public Dictionary<string, string> Servicos { get; set; }
    }
    public class ListaItemServicoNfse
    {
        [JsonPropertyName("servicoCategoria")]
        public Dictionary<string, ItemServicoNfse> ServicoCategoria { get; set; }
    }
    public class ComboItemServico
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ComboItemServico(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value; // O que será exibido no ComboBox
        }
    }
}
