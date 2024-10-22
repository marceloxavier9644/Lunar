using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.IntegracoesBancosBoletos.BB
{
    public class BbBoletoRequest
    {
        public int numeroConvenio { get; set; }
        public int numeroCarteira { get; set; }
        public int numeroVariacaoCarteira { get; set; }
        public int codigoModalidade { get; set; }
        public string dataEmissao { get; set; }
        public string dataVencimento { get; set; }
        public decimal valorOriginal { get; set; }
        public decimal valorAbatimento { get; set; }
        public int quantidadeDiasProtesto { get; set; }
        public int quantidadeDiasNegativacao { get; set; }
        //public int orgaoNegativador { get; set; }
        public string indicadorAceiteTituloVencido { get; set; }
        public string numeroDiasLimiteRecebimento { get; set; }
        public string codigoAceite { get; set; }
        public string codigoTipoTitulo { get; set; }
        public string descricaoTipoTitulo { get; set; }
        public string indicadorPermissaoRecebimentoParcial { get; set; }
        public string numeroTituloBeneficiario { get; set; }
        public string textoCampoUtilizacaoBeneficiario { get; set; }
        public string numeroTituloCliente { get; set; }
        public string mensagemBloquetoOcorrencia { get; set; }
        public descontoRequest desconto { get; set; }
        public descontoRequest segundoDesconto { get; set; }
        public descontoRequest terceiroDesconto { get; set; }
        public jurosMoraRequest jurosMora { get; set; }
        public multaRequest multa { get; set; }
        public pagadorRequest pagador { get; set; }
        //public beneficiarioFinalRequest beneficiarioFinal { get; set; }
        public string indicadorPix { get; set; }
    }

    public class descontoRequest
    {
        public int tipo { get; set; }
        public string dataExpiracao { get; set; }
        public decimal porcentagem { get; set; }
        public decimal valor { get; set; }
    }

    public class jurosMoraRequest
    {
        public int tipo { get; set; }
        public decimal porcentagem { get; set; }
        public decimal valor { get; set; }
    }

    public class multaRequest
    {
        public int tipo { get; set; }
        public string data { get; set; }
        public decimal porcentagem { get; set; }
        public decimal valor { get; set; }
    }

    public class pagadorRequest
    {
        public int tipoInscricao { get; set; }
        public long numeroInscricao { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public long cep { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string uf { get; set; }
        public string telefone { get; set; }
        //public string email { get; set; }
    }

    public class beneficiarioFinalRequest
    {
        public int tipoInscricao { get; set; }
        public long numeroInscricao { get; set; }
        public string nome { get; set; }
    }


    public class BbDetalheBoletoResponse
    {
        public string codigoLinhaDigitavel { get; set; }
        public string textoEmailPagador { get; set; }
        public string textoMensagemBloquetoTitulo { get; set; }
        public int codigoTipoMulta { get; set; }
        public int codigoCanalPagamento { get; set; }
        public int numeroContratoCobranca { get; set; }
        public int codigoTipoInscricaoSacado { get; set; }
        public long numeroInscricaoSacadoCobranca { get; set; }
        public int codigoEstadoTituloCobranca { get; set; }
        public int codigoTipoTituloCobranca { get; set; }
        public int codigoModalidadeTitulo { get; set; }
        public string codigoAceiteTituloCobranca { get; set; }
        public int codigoPrefixoDependenciaCobrador { get; set; }
        public int codigoIndicadorEconomico { get; set; }
        public string numeroTituloCedenteCobranca { get; set; }
        public int codigoTipoJuroMora { get; set; }
        public string dataEmissaoTituloCobranca { get; set; }
        public string dataRegistroTituloCobranca { get; set; }
        public string dataVencimentoTituloCobranca { get; set; }
        public decimal valorOriginalTituloCobranca { get; set; }
        public decimal valorAtualTituloCobranca { get; set; }
        public decimal valorPagamentoParcialTitulo { get; set; }
        public decimal valorAbatimentoTituloCobranca { get; set; }
        public decimal percentualImpostoSobreOprFinanceirasTituloCobranca { get; set; }
        public decimal valorImpostoSobreOprFinanceirasTituloCobranca { get; set; }
        public decimal valorMoedaTituloCobranca { get; set; }
        public decimal percentualJuroMoraTitulo { get; set; }
        public decimal valorJuroMoraTitulo { get; set; }
        public decimal percentualMultaTitulo { get; set; }
        public decimal valorMultaTituloCobranca { get; set; }
        public int quantidadeParcelaTituloCobranca { get; set; }
        public string dataBaixaAutomaticoTitulo { get; set; }
        public string textoCampoUtilizacaoCedente { get; set; }
        public string indicadorCobrancaPartilhadoTitulo { get; set; }
        public string nomeSacadoCobranca { get; set; }
        public string textoEnderecoSacadoCobranca { get; set; }
        public string nomeBairroSacadoCobranca { get; set; }
        public string nomeMunicipioSacadoCobranca { get; set; }
        public string siglaUnidadeFederacaoSacadoCobranca { get; set; }
        public int numeroCepSacadoCobranca { get; set; }
        public decimal valorMoedaAbatimentoTitulo { get; set; }
        public string dataProtestoTituloCobranca { get; set; }
        public int codigoTipoInscricaoSacador { get; set; }
        public long numeroInscricaoSacadorAvalista { get; set; }
        public string nomeSacadorAvalistaTitulo { get; set; }
        public decimal percentualDescontoTitulo { get; set; }
        public string dataDescontoTitulo { get; set; }
        public decimal valorDescontoTitulo { get; set; }
        public int codigoDescontoTitulo { get; set; }
        public decimal percentualSegundoDescontoTitulo { get; set; }
        public string dataSegundoDescontoTitulo { get; set; }
        public decimal valorSegundoDescontoTitulo { get; set; }
        public int codigoSegundoDescontoTitulo { get; set; }
        public decimal percentualTerceiroDescontoTitulo { get; set; }
        public string dataTerceiroDescontoTitulo { get; set; }
        public decimal valorTerceiroDescontoTitulo { get; set; }
        public int codigoTerceiroDescontoTitulo { get; set; }
        public string dataMultaTitulo { get; set; }
        public int numeroCarteiraCobranca { get; set; }
        public int numeroVariacaoCarteiraCobranca { get; set; }
        public int quantidadeDiaProtesto { get; set; }
        public int quantidadeDiaPrazoLimiteRecebimento { get; set; }
        public string dataLimiteRecebimentoTitulo { get; set; }
        public string indicadorPermissaoRecebimentoParcial { get; set; }
        public string textoCodigoBarrasTituloCobranca { get; set; }
        public int codigoOcorrenciaCartorio { get; set; }
        public decimal valorImpostoSobreOprFinanceirasRecebidoTitulo { get; set; }
        public decimal valorAbatimentoTotal { get; set; }
        public decimal valorJuroMoraRecebido { get; set; }
        public decimal valorDescontoUtilizado { get; set; }
        public decimal valorPagoSacado { get; set; }
        public decimal valorCreditoCedente { get; set; }
        public int codigoTipoLiquidacao { get; set; }
        public string dataCreditoLiquidacao { get; set; }
        public string dataRecebimentoTitulo { get; set; }
        public int codigoPrefixoDependenciaRecebedor { get; set; }
        public int codigoNaturezaRecebimento { get; set; }
        public string numeroIdentidadeSacadoTituloCobranca { get; set; }
        public string codigoResponsavelAtualizacao { get; set; }
        public int codigoTipoBaixaTitulo { get; set; }
        public decimal valorMultaRecebido { get; set; }
    }
}
