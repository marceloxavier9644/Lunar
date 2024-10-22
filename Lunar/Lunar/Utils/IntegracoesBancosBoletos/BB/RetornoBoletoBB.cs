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
    }
}
