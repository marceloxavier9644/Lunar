using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.MeuCrediarioIntegracao
{
    public class MeuCrediario
    {

        private static readonly HttpClient client = new HttpClient();
        public async Task<string> EnviarRequisicaoAnaliseCreditoAsync(RequisicaoAnaliseCredito requisicao)
        {
            string url = "https://api.meucrediario.com.br/v1";
            string apiKey = "";

            var json = JsonConvert.SerializeObject(requisicao);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return $"Erro ao enviar requisição: {ex.Message}";
            }
        }



        public class Proposta
        {
            public string Identificador { get; set; }
            public decimal ValorEntrada { get; set; }
            public decimal ValorFinanciado { get; set; }
            public int QuantidadeParcelas { get; set; }
            public DateTime PrimeiroVencimento { get; set; }
        }

        public class Identificacao
        {
            public string CPF { get; set; }
            public string Nome { get; set; }
            public string RG { get; set; }
            public string RGOrgao { get; set; }
            public string RGOrgaoUF { get; set; }
            public DateTime RGDtEmis { get; set; }
            public DateTime Nascimento { get; set; }
            public string Sexo { get; set; }
            public string NomePai { get; set; }
            public string NomeMae { get; set; }
            public string EstadoCivil { get; set; }
        }

        public class Conjuge
        {
            public string Nome { get; set; }
            public DateTime Nascimento { get; set; }
            public string Telefone { get; set; }
        }

        public class EnderecoProposta
        {
            public string CEP { get; set; }
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public int AnosMoradia { get; set; }
        }

        public class Contato
        {
            public string TelResidencial { get; set; }
            public string TelCelular { get; set; }
            public string TelComercial { get; set; }
            public string Email { get; set; }
        }

        public class Trabalho
        {
            public int Ocupacao { get; set; }
            public string Empresa { get; set; }
            public int TempoEmpresa { get; set; }
            public decimal Salario { get; set; }
            public bool SalarioComprovado { get; set; }
        }

        public class Referencia
        {
            public string Nome { get; set; }
            public string Relacao { get; set; }
            public string TelResidencial { get; set; }
            public string TelCelular { get; set; }
            public string TelComercial { get; set; }
        }

        public class Social
        {
            public int Dependentes { get; set; }
            public string Moradia { get; set; }
            public string SituacaoCarro { get; set; }
            public string SituacaoMoto { get; set; }
            public string Escolaridade { get; set; }
            public List<Referencia> Referencias { get; set; }
        }

        public class ClienteProposta
        {
            public Identificacao Identificacao { get; set; }
            public Conjuge Conjuge { get; set; }
            public EnderecoProposta Endereco { get; set; }
            public Contato Contato { get; set; }
            public Trabalho Trabalho { get; set; }
            public Social Social { get; set; }
        }

        public class Parcela
        {
            public int Numero { get; set; }
            public string Status { get; set; }
            public decimal ValorVencimento { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime? DataUltimoPagamento { get; set; }
            public decimal TotalPago { get; set; }
            public decimal CapitalAberto { get; set; }
            public string FormaPagamento { get; set; }
        }

        public class Contrato
        {
            public string Numero { get; set; }
            public DateTime Data { get; set; }
            public string Hora { get; set; }
            public string Status { get; set; }
            public decimal ValorTotal { get; set; }
            public decimal ValorEntrada { get; set; }
            public decimal ValorFinanciado { get; set; }
            public List<Parcela> Parcelas { get; set; }
        }

        public class RequisicaoAnaliseCredito
        {
            public List<Proposta> Propostas { get; set; }
            public ClienteProposta Cliente { get; set; }
            public List<Contrato> Contratos { get; set; }
        }
    }
}
