using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using static LunarBase.Utilidades.QRCODE_NFCe.ExtinfNFeSupl;

namespace LunarBase.Utilidades.QRCODE_NFCe
{
    public static class Conversao
    {
        public static string VersaoServicoParaString(this ServicoNFe servicoNFe, VersaoServico? versaoServico)
        {

            if (servicoNFe == ServicoNFe.NfeConsultaCadastro && versaoServico != VersaoServico.Versao100)
            {
                return "2.00";
            }

            if (servicoNFe == ServicoNFe.RecepcaoEventoCancelmento
                || servicoNFe == ServicoNFe.RecepcaoEventoCartaCorrecao
                || servicoNFe == ServicoNFe.RecepcaoEventoManifestacaoDestinatario
                || servicoNFe == ServicoNFe.RecepcaoEventoEpec)
            {
                return "1.00";
            }

            switch (versaoServico)
            {
                case VersaoServico.Versao100:
                    switch (servicoNFe)
                    {
                        case ServicoNFe.NFeDistribuicaoDFe:
                            return "1.01";
                    }
                    return "1.00";
                case VersaoServico.Versao200:
                    switch (servicoNFe)
                    {
                        case ServicoNFe.NfeConsultaProtocolo:
                            return "2.01";
                    }
                    return "2.00";
                case VersaoServico.Versao310:
                    return "3.10";
                case VersaoServico.Versao400:
                    return "4.00";
            }
            return "";
        }

        public static string TpAmbParaString(this TipoAmbiente tpAmb)
        {
            switch (tpAmb)
            {
                case TipoAmbiente.Homologacao:
                    return "Homologação";
                case TipoAmbiente.Producao:
                    return "Produção";
                default:
                    throw new ArgumentOutOfRangeException("tpAmb", tpAmb, null);
            }
        }

        public static string VersaoServicoParaString(this VersaoServico versao)
        {
            switch (versao)
            {
                case VersaoServico.Versao100:
                    return "1.00";
                case VersaoServico.Versao200:
                    return "2.00";
                case VersaoServico.Versao310:
                    return "3.10";
                case VersaoServico.Versao400:
                    return "4.00";
            }
            return null;
        }

        public static VersaoServico StringParaVersaoServico(string versaoServico)
        {
            switch (versaoServico)
            {
                case "1.00":
                    return VersaoServico.Versao100;
                case "2.00":
                    return VersaoServico.Versao200;
                case "3.10":
                    return VersaoServico.Versao310;
                case "4.00":
                    return VersaoServico.Versao400;
                default:
                    throw new ArgumentOutOfRangeException("versaoServico", versaoServico, null);
            }
        }

        public static string TipoEmissaoParaString(this TipoEmissao tipoEmissao)
        {
            var s = Enum.GetName(typeof(TipoEmissao), tipoEmissao);
            return s != null ? s.Substring(2) : "";
        }

        public static string CrtParaString(this CRT crt)
        {
            switch (crt)
            {
                case CRT.SimplesNacional:
                    return "Simples Nacional";
                case CRT.SimplesNacionalExcessoSublimite:
                    return "Simples Nacional - sublimite excedido";
                case CRT.RegimeNormal:
                    return "Normal";
                default:
                    throw new ArgumentOutOfRangeException("crt", crt, null);
            }
        }

        public static string ModeloDocumentoParaString(this ModeloDocumento modelo)
        {
            switch (modelo)
            {
                case ModeloDocumento.NFe:
                    return "NF-e";
                case ModeloDocumento.NFCe:
                    return "NFC-e";
                case ModeloDocumento.MDFe:
                    return "MDF-e";
            }
            return null;
        }

        public static int ModeloDocumentoParaInt(this ModeloDocumento modelo)
        {
            return (int)modelo;
        }

        public static string CsticmsParaString(this Csticms csticms)
        {
            switch (csticms)
            {
                case Csticms.Cst00:
                    return "00";
                case Csticms.Cst10:
                case Csticms.CstPart10:
                    return "10";
                case Csticms.Cst20:
                    return "20";
                case Csticms.Cst30:
                    return "30";
                case Csticms.Cst40:
                    return "40";
                case Csticms.Cst41:
                case Csticms.CstRep41:
                    return "41";
                case Csticms.Cst50:
                    return "50";
                case Csticms.Cst51:
                    return "51";
                case Csticms.Cst60:
                case Csticms.CstRep60:
                    return "60";
                case Csticms.Cst70:
                    return "70";
                case Csticms.Cst90:
                case Csticms.CstPart90:
                    return "90";
                default:
                    throw new ArgumentOutOfRangeException("csticms", csticms, null);
            }
        }

        public static string CsosnicmsParaString(this Csosnicms csosnicms)
        {
            return ((int)csosnicms).ToString();
        }

        public static string OrigemMercadoriaParaString(this OrigemMercadoria origemMercadoria)
        {
            return ((int)origemMercadoria).ToString();
        }

        /// <summary>
        /// Obtém uma <see cref="string"/> SHA1, no formato hexadecimal da <see cref="string"/> passada no parâmero        
        /// </summary>
        public static string ObterHexSha1DeString(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(bytes);

            return ObterHexDeByteArray(hashBytes);
        }

        /// <summary>
        /// Obtém uma string Hexadecimal do array de bytes passado no parâmetro
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ObterHexDeByteArray(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Obtém uma string Hexadecimal de uma string passada no parâmetro
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ObterHexDeString(string s)
        {
            var hex = "";
            foreach (var c in s)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public enum ServicoNFe
        {
            /// <summary>
            ///     serviço destinado à recepção de mensagem do Evento de Cancelamento da NF-e
            /// </summary>
            RecepcaoEventoCancelmento,

            /// <summary>
            ///     serviço destinado à recepção de mensagem do Evento de Carta de Correção da NF-e
            /// </summary>
            RecepcaoEventoCartaCorrecao,

            /// <summary>
            ///     serviço destinado à recepção de mensagem do Evento EPEC da NF-e
            /// </summary>
            RecepcaoEventoEpec,

            /// <summary>
            ///     serviço destinado à recepção de mensagem do Evento de Manifestação do destinatário da NF-e
            /// </summary>
            RecepcaoEventoManifestacaoDestinatario,

            /// <summary>
            ///     serviço destinado à recepção de mensagens de lote de NF-e versão 2.0
            /// </summary>
            NfeRecepcao,

            /// <summary>
            ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 2.0
            /// </summary>
            NfeRetRecepcao,

            /// <summary>
            ///     Serviço para consultar o cadastro de contribuintes do ICMS da unidade federada
            /// </summary>
            NfeConsultaCadastro,

            /// <summary>
            ///     serviço destinado ao atendimento de solicitações de inutilização de numeração
            /// </summary>
            NfeInutilizacao,

            /// <summary>
            ///     serviço destinado ao atendimento de solicitações de consulta da situação atual da NF-e
            ///     na Base de Dados do Portal da Secretaria de Fazenda Estadual
            /// </summary>
            NfeConsultaProtocolo,

            /// <summary>
            ///     serviço destinado à consulta do status do serviço prestado pelo Portal da Secretaria de Fazenda Estadual
            /// </summary>
            NfeStatusServico,

            /// <summary>
            ///     serviço destinado à recepção de mensagens de lote de NF-e versão 3.10
            /// </summary>
            NFeAutorizacao,

            /// <summary>
            ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 3.10
            /// </summary>
            NFeRetAutorizacao,

            /// <summary>
            ///     Distribui documentos e informações de interesse do ator da NF-e
            /// </summary>
            NFeDistribuicaoDFe,

            /// <summary>
            ///     “Serviço de Consulta da Relação de Documentos Destinados” para um determinado CNPJ
            ///     de destinatário informado na NF-e.
            /// </summary>
            NfeConsultaDest,

            /// <summary>
            ///     Serviço destinado ao atendimento de solicitações de download de Notas Fiscais Eletrônicas por seus destinatários
            /// </summary>
            NfeDownloadNF,

            /// <summary>
            ///     Serviço destinado a administração do CSC.
            /// </summary>
            NfceAdministracaoCSC
        }

        public enum TipoEmissao
        {
            /// <summary>
            /// 1 - Emissão normal (não em contingência)
            /// </summary>
            [Description("Normal")]
            [XmlEnum("1")]
            teNormal = 1,

            /// <summary>
            /// 2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança
            /// </summary>
            [Description("Contingência FS-IA")]
            [XmlEnum("2")]
            teFSIA = 2,

            /// <summary>
            /// 3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)
            /// </summary>
            [Description("Contingência SCAN")]
            [XmlEnum("3")]
            teSCAN = 3,

            /// <summary>
            /// 4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)
            /// </summary>
            [Description("Contingência DPEC")]
            [XmlEnum("4")]
            teEPEC = 4,

            /// <summary>
            /// 5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança
            /// </summary>
            [Description("Contingência FS-DA")]
            [XmlEnum("5")]
            teFSDA = 5,

            /// <summary>
            /// 6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)
            /// </summary>
            [Description("Contingência SVC-AN")]
            [XmlEnum("6")]
            teSVCAN = 6,

            /// <summary>
            /// 7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)
            /// </summary>
            [Description("Contingência SVC-RS")]
            [XmlEnum("7")]
            teSVCRS = 7,

            /// <summary>
            /// 9 - Contingência off-line da NFC-e
            /// </summary>
            [Description("Contingência off-line")]
            [XmlEnum("9")]
            teOffLine = 9
        }

        public enum CRT
        {
            /// <summary>
            /// 1 – Simples Nacional
            /// </summary>
            [Description("Simples Nacional")]
            [XmlEnum("1")]
            SimplesNacional = 1,

            /// <summary>
            /// 2 – Simples Nacional – excesso de sublimite de receita bruta
            /// </summary>
            [Description("Simples Nacional – excesso de sublimite de receita bruta")]
            [XmlEnum("2")]
            SimplesNacionalExcessoSublimite = 2,

            /// <summary>
            /// 3 – Regime Normal
            /// </summary>
            [Description("Regime Normal")]
            [XmlEnum("3")]
            RegimeNormal = 3
        }

        public enum ModeloDocumento
        {
            [XmlEnum("55")]
            NFe = 55,
            [XmlEnum("58")]
            MDFe = 58,
            [XmlEnum("65")]
            NFCe = 65,
            [XmlEnum("57")]
            CTe = 57,
            [XmlEnum("67")]
            CTeOS = 67
        }

        public enum Csticms
        {
            /// <summary>
            /// 00 - Tributada integralmente
            /// </summary>
            [Description("Tributada integralmente")]
            [XmlEnum("00")]
            Cst00,

            /// <summary>
            /// 10 - Tributada e com cobrança do ICMS por substituição tributária
            /// </summary>
            [Description("Tributada e com cobrança do ICMS por substituição tributária")]
            [XmlEnum("10")]
            Cst10,

            /// <summary>
            /// 10 - Tributada e com cobrança do ICMS por substituição tributária
            /// </summary>
            [Description("Tributada e com cobrança do ICMS por substituição tributária")]
            [XmlEnum("10")]
            CstPart10,

            /// <summary>
            /// 20 - Com redução de base de cálculo
            /// </summary>
            [Description("Com redução de base de cálculo")]
            [XmlEnum("20")]
            Cst20,

            /// <summary>
            /// 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária
            /// </summary>
            [Description("Isenta ou não tributada e com cobrança do ICMS por substituição tributária")]
            [XmlEnum("30")]
            Cst30,

            /// <summary>
            /// 40 - Isenta
            /// </summary>
            [Description("Isenta")]
            [XmlEnum("40")]
            Cst40,

            /// <summary>
            /// 41 - Não tributada
            /// </summary>
            [Description("Não tributada")]
            [XmlEnum("41")]
            Cst41,

            /// <summary>
            /// 41 - Não tributada
            /// </summary>
            [Description("Não tributada")]
            [XmlEnum("41")]
            CstRep41,

            /// <summary>
            /// 50 - Suspensão
            /// </summary>
            [Description("Suspensão")]
            [XmlEnum("50")]
            Cst50,

            /// <summary>
            /// 51 - Diferimento
            /// </summary>
            [Description("Diferimento")]
            [XmlEnum("51")]
            Cst51,

            /// <summary>
            /// 60 - ICMS cobrado anteriormente por substituição tributária
            /// </summary>
            [Description("ICMS cobrado anteriormente por substituição tributária")]
            [XmlEnum("60")]
            Cst60,

            /// <summary>
            /// 60 - ICMS cobrado anteriormente por substituição tributária
            /// </summary>
            [XmlEnum("60")] CstRep60,

            /// <summary>
            /// 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária
            /// </summary>
            [Description("Com redução de base de cálculo e cobrança do ICMS por substituição tributária")]
            [XmlEnum("70")]
            Cst70,

            /// <summary>
            /// 90 - Outras
            /// </summary>
            [Description("Outras")]
            [XmlEnum("90")]
            Cst90,

            /// <summary>
            /// 90 - Outras
            /// </summary>
            [Description("Outras")]
            [XmlEnum("90")]
            CstPart90
        }

        public enum Csosnicms
        {
            /// <summary>
            /// 101 - Tributada pelo Simples Nacional com permissão de crédito
            /// </summary>
            [Description("Tributada pelo Simples Nacional com permissão de crédito")]
            [XmlEnum("101")]
            Csosn101 = 101,

            /// <summary>
            /// 102 - Tributada pelo Simples Nacional sem permissão de crédito
            /// </summary>
            [Description("Tributada pelo Simples Nacional sem permissão de crédito")]
            [XmlEnum("102")]
            Csosn102 = 102,

            /// <summary>
            /// 103 – Isenção do ICMS  no Simples Nacional para faixa de receita bruta
            /// </summary>
            [Description("Isenção do ICMS  no Simples Nacional para faixa de receita bruta")]
            [XmlEnum("103")]
            Csosn103 = 103,

            /// <summary>
            /// 201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária
            /// </summary>
            [Description("Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária")]
            [XmlEnum("201")]
            Csosn201 = 201,

            /// <summary>
            /// 202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária
            /// </summary>
            [Description("Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária")]
            [XmlEnum("202")]
            Csosn202 = 202,

            /// <summary>
            /// 203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária
            /// </summary>
            [Description("Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária")]
            [XmlEnum("203")]
            Csosn203 = 203,

            /// <summary>
            /// 300 – Imune
            /// </summary>
            [Description("Imune")]
            [XmlEnum("300")]
            Csosn300 = 300,

            /// <summary>
            /// 400 – Não tributada pelo Simples Nacional
            /// </summary>
            [Description("Não tributada pelo Simples Nacional")]
            [XmlEnum("400")]
            Csosn400 = 400,

            /// <summary>
            /// 500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação
            /// </summary>
            [Description("ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação")]
            [XmlEnum("500")]
            Csosn500 = 500,

            /// <summary>
            /// 900 - Outros
            /// </summary>
            [Description("Outros")]
            [XmlEnum("900")]
            Csosn900 = 900
        }

        public enum OrigemMercadoria
        {
            /// <summary>
            /// 0-Nacional exceto as indicadas nos códigos 3, 4, 5 e 8
            /// </summary>
            [Description("Nacional exceto as indicadas nos códigos 3, 4, 5 e 8")]
            [XmlEnum("0")]
            OmNacional = 0,

            /// <summary>
            /// 1-Estrangeira - Importação direta
            /// </summary>
            [Description("Estrangeira - Importação direta")]
            [XmlEnum("1")]
            OmEstrangeiraImportacaoDireta = 1,

            /// <summary>
            /// 2-Estrangeira - Adquirida no mercado interno
            /// </summary>
            [Description("Estrangeira - Adquirida no mercado interno")]
            [XmlEnum("2")]
            OmEstrangeiraAdquiridaBrasil = 2,

            /// <summary>
            /// 3-Nacional, conteudo superior 40% e inferior ou igual a 70%
            /// </summary>
            [Description("Nacional, conteudo superior 40% e inferior ou igual a 70%")]
            [XmlEnum("3")]
            OmNacionalConteudoImportacaoSuperior40 = 3,

            /// <summary>
            /// 4-Nacional, processos produtivos básicos
            /// </summary>
            [Description("Nacional, processos produtivos básicos")]
            [XmlEnum("4")]
            OmNacionalProcessosBasicos = 4,

            /// <summary>
            /// 5-Nacional, conteudo inferior 40%
            /// </summary>
            [Description("Nacional, conteudo inferior 40%")]
            [XmlEnum("5")]
            OmNacionalConteudoImportacaoInferiorIgual40 = 5,

            /// <summary>
            /// 6-Estrangeira - Importação direta, com similar nacional, lista CAMEX
            /// </summary>
            [Description("Estrangeira - Importação direta, com similar nacional, lista CAMEX")]
            [XmlEnum("6")]
            OmEstrangeiraImportacaoDiretaSemSimilar = 6,

            /// <summary>
            /// 7-Estrangeira - mercado interno, sem simular,lista CAMEX
            /// </summary>
            [Description("Estrangeira - mercado interno, sem simular,lista CAMEX")]
            [XmlEnum("7")]
            OmEstrangeiraAdquiridaBrasilSemSimilar = 7,

            /// <summary>
            /// 8-Nacional, Conteúdo de Importação superior a 70%
            /// </summary>
            [Description("Nacional, Conteúdo de Importação superior a 70%")]
            [XmlEnum("8")]
            OmNacionalConteudoImportacaoSuperior70 = 8
        }
    }
}
