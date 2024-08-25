using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Constantes
{
    public static class LunarConstantes
    {        
        
        //Usuários - Padrão
        public const int USUARIO_SUPORTE = 1;
        //Grupo de Usuario - Padrão
        public const int GRUPOUSUARIO_ADMINISTRADOR = 1;

        //Parametro Sistema - Padrão
        public const int PARAMETROSISTEMA_UNICO = 1;

        //Unidade de Medida - Padrão
        public const int UNIDADEMEDIDA_UN = 1;
        public const int UNIDADEMEDIDA_PC = 2;
        public const int UNIDADEMEDIDA_KG = 3;
        public const int UNIDADEMEDIDA_MT = 4;
        public const int UNIDADEMEDIDA_CM = 5;
        public const int UNIDADEMEDIDA_CX = 6;
        public const int UNIDADEMEDIDA_LT = 7;
        public const int UNIDADEMEDIDA_JG = 8;
        public const int UNIDADEMEDIDA_PR = 9;
        public const int UNIDADEMEDIDA_PT = 10;
        public const int UNIDADEMEDIDA_GR = 11;
        public const int UNIDADEMEDIDA_RL = 12;

        //Grupo Fiscal - Padrão
        public const int GRUPOFISCAL_TRIBUTADO = 1;
        public const int GRUPOFISCAL_SUBSTITUICAOTRIBUTARIA = 2;

        //Origem ICMS - Padrão
        public const int ORIGEMICMS_0 = 1;
        public const int ORIGEMICMS_1 = 2;
        public const int ORIGEMICMS_2 = 3;
        public const int ORIGEMICMS_3 = 4;
        public const int ORIGEMICMS_4 = 5;
        public const int ORIGEMICMS_5 = 6;
        public const int ORIGEMICMS_6 = 7;
        public const int ORIGEMICMS_7 = 8;


        //CST ICMS - Padrão
        public const int CSTICMS_00 = 1;
        public const int CSTICMS_10 = 2;
        public const int CSTICMS_20 = 3;
        public const int CSTICMS_30 = 4;
        public const int CSTICMS_40 = 5;
        public const int CSTICMS_41 = 6;
        public const int CSTICMS_50 = 7;
        public const int CSTICMS_51 = 8;
        public const int CSTICMS_60 = 9;
        public const int CSTICMS_70 = 10;
        public const int CSTICMS_90 = 11;

        //CSOSN - Padrão
        public const int CSOSNICMS_101 = 1;
        public const int CSOSNICMS_102 = 2;
        public const int CSOSNICMS_103 = 3;
        public const int CSOSNICMS_201 = 4;
        public const int CSOSNICMS_202 = 5;
        public const int CSOSNICMS_203 = 6;
        public const int CSOSNICMS_300 = 7;
        public const int CSOSNICMS_400 = 8;
        public const int CSOSNICMS_500 = 9;
        public const int CSOSNICMS_900 = 10;

        //Regime Tributário Empresa - Padrão
        public const int REGIMEEMPRESA_SIMPLESNACIONAL = 1;
        public const int REGIMEEMPRESA_LUCROPRESUMIDO = 2;
        public const int REGIMEEMPRESA_LUCROREAL = 3;
        public const int REGIMEEMPRESA_SIMPLES_EXCESSO_RECEITA = 4;
        public const int REGIMEEMPRESA_MEI = 5;

        //Forma de Pagamento - Padrão
        public const int FORMAPAGAMENTO_DINHEIRO = 1;
        public const int FORMAPAGAMENTO_CARTAO = 2;
        public const int FORMAPAGAMENTO_PIX = 3;
        public const int FORMAPAGAMENTO_DEPOSITO = 4;
        public const int FORMAPAGAMENTO_BOLETO = 5;
        public const int FORMAPAGAMENTO_CREDIARIO = 6;
        public const int FORMAPAGAMENTO_CHEQUE = 7;
        public const int FORMAPAGAMENTO_CREDITOCLIENTE = 8;
        public const int FORMAPAGAMENTO_ABATIMENTO = 9;

        //Bandeira Cartão - Padrão
        public const int BANDEIRACARTAO_VISA = 1;
        public const int BANDEIRACARTAO_MASTERCARD = 2;
        public const int BANDEIRACARTAO_ELO= 3;
        public const int BANDEIRACARTAO_AMERICANEXPRESS = 4;
        public const int BANDEIRACARTAO_SOROCRED = 5;
        public const int BANDEIRACARTAO_HIPERCARD = 6;
        public const int BANDEIRACARTAO_AURA = 7;
        public const int BANDEIRACARTAO_DINERS = 8;
        public const int BANDEIRACARTAO_CABAL = 9;
        public const int BANDEIRACARTAO_JCB = 10;
        public const int BANDEIRACARTAO_OUTROS = 11;

        //Parcelamento - Padrão
        public const int PARCELAMENTO_DEBITO = 1;
        public const int PARCELAMENTO_CREDITO_A_VISTA = 2;
        public const int PARCELAMENTO_CREDITO_2X = 3;
        public const int PARCELAMENTO_CREDITO_3X = 4;
        public const int PARCELAMENTO_CREDITO_4X = 5;
        public const int PARCELAMENTO_CREDITO_5X = 6;
        public const int PARCELAMENTO_CREDITO_6X = 7;
        public const int PARCELAMENTO_CREDITO_7X = 8;
        public const int PARCELAMENTO_CREDITO_8X = 9;
        public const int PARCELAMENTO_CREDITO_9X = 10;
        public const int PARCELAMENTO_CREDITO_10X = 11;
        public const int PARCELAMENTO_CREDITO_11X = 12;
        public const int PARCELAMENTO_CREDITO_12X = 13;
        public const int PARCELAMENTO_CREDITO_13X = 14;
        public const int PARCELAMENTO_CREDITO_14X = 15;
        public const int PARCELAMENTO_CREDITO_15X = 16;
        public const int PARCELAMENTO_CREDITO_16X = 17;
        public const int PARCELAMENTO_CREDITO_17X = 18;
        public const int PARCELAMENTO_CREDITO_18X = 19;
        public const int PARCELAMENTO_CREDITO_19X = 20;
        public const int PARCELAMENTO_CREDITO_20X = 21;
        public const int PARCELAMENTO_CREDITO_21X = 22;
        public const int PARCELAMENTO_CREDITO_22X = 23;
        public const int PARCELAMENTO_CREDITO_23X = 24;
        public const int PARCELAMENTO_CREDITO_24X = 25;

        //Usuários - Padrão
        public const int NFESTATUS_AUTORIZADO = 1;
        public const int NFESTATUS_REJEITADO = 2;
        public const int NFESTATUS_ENVIANDO = 3;
        public const int NFESTATUS_CANCELADO = 4;
        public const int NFESTATUS_INUTILIZADO = 5;
        public const int NFESTATUS_CONTIGENCIA = 6;

        //Tamanho - Padrão
        public const int TAMANHO_P = 1;
        public const int TAMANHO_M = 2;
        public const int TAMANHO_G = 3;
        public const int TAMANHO_GG = 4;
    }
}
