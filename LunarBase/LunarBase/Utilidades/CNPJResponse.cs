using Newtonsoft.Json;

namespace LunarBase.Utilidades
{
    public class CNPJResponse
    {
        [JsonProperty("NOME FANTASIA")]
        public string nomeFantasia { get; set; }
        [JsonProperty("RAZAO SOCIAL")]
        public string razaoSocial { get; set; }
        [JsonProperty("CNPJ")]
        public string cnpj { get; set; }
        [JsonProperty("STATUS")]
        public string status { get; set; }
        [JsonProperty("CNAE PRINCIPAL DESCRICAO")]
        public string cnaePrincipalDescricao { get; set; }
        [JsonProperty("CNAE PRINCIPAL CODIGO")]
        public string cnaePrincipalCodigo { get; set; }
        [JsonProperty("CEP")]
        public string cep { get; set; }
        [JsonProperty("DATA ABERTURA")]
        public string dataAbertura { get; set; }
        [JsonProperty("DDD")]
        public string ddd { get; set; }
        [JsonProperty("TELEFONE")]
        public string telefone { get; set; }
        [JsonProperty("EMAIL")]
        public string email { get; set; }
        [JsonProperty("TIPO LOGRADOURO")]
        public string tipoLogradouro { get; set; }
        [JsonProperty("LOGRADOURO")]
        public string logradouro { get; set; }
        [JsonProperty("NUMERO")]
        public string numero { get; set; }
        [JsonProperty("COMPLEMENTO")]
        public string complemento { get; set; }
        [JsonProperty("BAIRRO")]
        public string bairro { get; set; }
        [JsonProperty("MUNICIPIO")]
        public string municipio { get; set; }
        [JsonProperty("UF")]
        public string uf { get; set; }

    }
}
