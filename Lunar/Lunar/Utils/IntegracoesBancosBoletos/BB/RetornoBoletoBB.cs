using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lunar.Utils.IntegracoesBancosBoletos.BB
{
    public class RetornoBoletoBB
    {
        public Beneficiario beneficiario { get; set; }
        public QrCode qrCode { get; set; }
        public string numero { get; set; }
        public int numeroCarteira { get; set; }
        public int numeroVariacaoCarteira { get; set; }
        public long codigoCliente { get; set; }
        public string linhaDigitavel { get; set; }
        public string codigoBarraNumerico { get; set; }
        public long numeroContratoCobranca { get; set; }
        public string observacao { get; set; }
        public List<ErroApi> erros { get; set; } = new List<ErroApi>();
        public class Beneficiario
        {
            public int agencia { get; set; }
            public int contaCorrente { get; set; }
            public int tipoEndereco { get; set; }
            public string logradouro { get; set; }
            public string bairro { get; set; }
            public string cidade { get; set; }
            public int codigoCidade { get; set; }
            public string uf { get; set; }
            public string cep { get; set; }
            public string indicadorComprovacao { get; set; }
        }
        public class ErroApi
        {
            public string codigo { get; set; }
            public string ocorrencia { get; set; }
            public string mensagem { get; set; }
            public string versao { get; set; }
        }
        public class QrCode
        {
            public string url { get; set; }
            public string txId { get; set; }
            public string emv { get; set; }
        }
        public class ApiException : Exception
        {
            public RetornoBoletoBB ApiError { get; }

            public ApiException(string message, RetornoBoletoBB apiError) : base(message)
            {
                ApiError = apiError;
            }

        }

        public class RetornoListaBoletosBaixadosBB
        {
            [JsonProperty("indicadorContinuidade")]
            public string IndicadorContinuidade { get; set; }

            [JsonProperty("boletos")]
            public List<BoletoBaixadoBB> Boletos { get; set; }
        }
        public class BoletoBaixadoBB
        {
            [JsonProperty("numeroBoletoBB")]
            public string NumeroBoletoBB { get; set; }

            [JsonProperty("dataRegistro")]
            public string DataRegistro { get; set; }

            [JsonProperty("dataVencimento")]
            public string DataVencimento { get; set; }

            [JsonProperty("valorOriginal")]
            public decimal ValorOriginal { get; set; }

            [JsonProperty("carteiraConvenio")]
            public int CarteiraConvenio { get; set; }

            [JsonProperty("variacaoCarteiraConvenio")]
            public int VariacaoCarteiraConvenio { get; set; }

            [JsonProperty("codigoEstadoTituloCobranca")]
            public int CodigoEstadoTituloCobranca { get; set; }

            [JsonProperty("estadoTituloCobranca")]
            public string EstadoTituloCobranca { get; set; }

            [JsonProperty("contrato")]
            public long Contrato { get; set; }

            [JsonProperty("dataMovimento")]
            public string DataMovimento { get; set; }

            [JsonProperty("dataCredito")]
            public string DataCredito { get; set; }

            [JsonProperty("valorAtual")]
            public decimal ValorAtual { get; set; }

            [JsonProperty("valorPago")]
            public decimal ValorPago { get; set; }
        }
    }
}
