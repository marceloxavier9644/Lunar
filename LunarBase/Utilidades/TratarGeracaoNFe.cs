using LunarBase.Classes;
using LunarBase.Utilidades.NFe40Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades
{
    public class TratarGeracaoNFe
    {
        public TCodUfIBGE Retornar_cUFFilialLogada()
        {
            string uf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;

            if (uf.Equals("MG"))
                return TCodUfIBGE.Item31;
            else if (uf.Equals("RO"))
                return TCodUfIBGE.Item11;
            else if (uf.Equals("AC"))
                return TCodUfIBGE.Item12;
            else if (uf.Equals("AM"))
                return TCodUfIBGE.Item13;
            else if (uf.Equals("RR"))
                return TCodUfIBGE.Item14;
            else if (uf.Equals("PA"))
                return TCodUfIBGE.Item15;
            else if (uf.Equals("AP"))
                return TCodUfIBGE.Item16;
            else if (uf.Equals("TO"))
                return TCodUfIBGE.Item17;
            else if (uf.Equals("MA"))
                return TCodUfIBGE.Item21;
            else if (uf.Equals("PI"))
                return TCodUfIBGE.Item22;
            else if (uf.Equals("CE"))
                return TCodUfIBGE.Item23;
            else if (uf.Equals("RN"))
                return TCodUfIBGE.Item24;
            else if (uf.Equals("PB"))
                return TCodUfIBGE.Item25;
            else if (uf.Equals("PE"))
                return TCodUfIBGE.Item26;
            else if (uf.Equals("AL"))
                return TCodUfIBGE.Item27;
            else if (uf.Equals("SE"))
                return TCodUfIBGE.Item28;
            else if (uf.Equals("BA"))
                return TCodUfIBGE.Item29;
            else if (uf.Equals("ES"))
                return TCodUfIBGE.Item32;
            else if (uf.Equals("RJ"))
                return TCodUfIBGE.Item33;
            else if (uf.Equals("SP"))
                return TCodUfIBGE.Item35;
            else if (uf.Equals("PR"))
                return TCodUfIBGE.Item41;
            else if (uf.Equals("SC"))
                return TCodUfIBGE.Item42;
            else if (uf.Equals("RS"))
                return TCodUfIBGE.Item43;
            else if (uf.Equals("MS"))
                return TCodUfIBGE.Item50;
            else if (uf.Equals("MT"))
                return TCodUfIBGE.Item51;
            else if (uf.Equals("GO"))
                return TCodUfIBGE.Item52;
            else //df
                return TCodUfIBGE.Item53;
        }
        public TUf Retornar_TUf(string uf)
        {
            if (uf.Equals("MG"))
                return TUf.MG;
            else if (uf.Equals("RO"))
                return TUf.RO;
            else if (uf.Equals("AC"))
                return TUf.AC;
            else if (uf.Equals("AM"))
                return TUf.AM;
            else if (uf.Equals("RR"))
                return TUf.RR;
            else if (uf.Equals("PA"))
                return TUf.PA;
            else if (uf.Equals("AP"))
                return TUf.AP;
            else if (uf.Equals("TO"))
                return TUf.TO;
            else if (uf.Equals("MA"))
                return TUf.MA;
            else if (uf.Equals("PI"))
                return TUf.PI;
            else if (uf.Equals("CE"))
                return TUf.CE;
            else if (uf.Equals("RN"))
                return TUf.RN;
            else if (uf.Equals("PB"))
                return TUf.PB;
            else if (uf.Equals("PE"))
                return TUf.PE;
            else if (uf.Equals("AL"))
                return TUf.AL;
            else if (uf.Equals("SE"))
                return TUf.SE;
            else if (uf.Equals("BA"))
                return TUf.BA;
            else if (uf.Equals("ES"))
                return TUf.ES;
            else if (uf.Equals("RJ"))
                return TUf.RJ;
            else if (uf.Equals("SP"))
                return TUf.SP;
            else if (uf.Equals("PR"))
                return TUf.PR;
            else if (uf.Equals("SC"))
                return TUf.SC;
            else if (uf.Equals("RS"))
                return TUf.RS;
            else if (uf.Equals("MS"))
                return TUf.MS;
            else if (uf.Equals("MT"))
                return TUf.MT;
            else if (uf.Equals("GO"))
                return TUf.GO;
            else //df
                return TUf.DF;
        }
        public TUfEmi Retornar_TUfEmi(string uf)
        {
            if (uf.Equals("MG"))
                return TUfEmi.MG;
            else if (uf.Equals("RO"))
                return TUfEmi.RO;
            else if (uf.Equals("AC"))
                return TUfEmi.AC;
            else if (uf.Equals("AM"))
                return TUfEmi.AM;
            else if (uf.Equals("RR"))
                return TUfEmi.RR;
            else if (uf.Equals("PA"))
                return TUfEmi.PA;
            else if (uf.Equals("AP"))
                return TUfEmi.AP;
            else if (uf.Equals("TO"))
                return TUfEmi.TO;
            else if (uf.Equals("MA"))
                return TUfEmi.MA;
            else if (uf.Equals("PI"))
                return TUfEmi.PI;
            else if (uf.Equals("CE"))
                return TUfEmi.CE;
            else if (uf.Equals("RN"))
                return TUfEmi.RN;
            else if (uf.Equals("PB"))
                return TUfEmi.PB;
            else if (uf.Equals("PE"))
                return TUfEmi.PE;
            else if (uf.Equals("AL"))
                return TUfEmi.AL;
            else if (uf.Equals("SE"))
                return TUfEmi.SE;
            else if (uf.Equals("BA"))
                return TUfEmi.BA;
            else if (uf.Equals("ES"))
                return TUfEmi.ES;
            else if (uf.Equals("RJ"))
                return TUfEmi.RJ;
            else if (uf.Equals("SP"))
                return TUfEmi.SP;
            else if (uf.Equals("PR"))
                return TUfEmi.PR;
            else if (uf.Equals("SC"))
                return TUfEmi.SC;
            else if (uf.Equals("RS"))
                return TUfEmi.RS;
            else if (uf.Equals("MS"))
                return TUfEmi.MS;
            else if (uf.Equals("MT"))
                return TUfEmi.MT;
            else if (uf.Equals("GO"))
                return TUfEmi.GO;
            else //df
                return TUfEmi.DF;
        }

        public TNFeInfNFeDet[] gerarProdutos(int qtsItem, IList<VendaItens> listaProdutosVendidos)
        {
            TNFeInfNFeDet[] det = new TNFeInfNFeDet[1];
            for (int i = 0; i < qtsItem; i++)
            {
                det = new TNFeInfNFeDet[]
                                              {
                    new TNFeInfNFeDet
                    {
                        nItem = qtsItem+1.ToString(),
                        prod = new TNFeInfNFeDetProd
                        {
                            cEAN = "SEM GTIN",
                            cEANTrib = "SEM GTIN",
                            cProd = "123456789",
                            xProd = "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                            NCM = "22021000",
                            CEST = "0301100",
                            CFOP = "5102",
                            uCom = "UN",
                            qCom = "1.0000",
                            vUnCom = "3.00",
                            vProd = "3.00",
                            uTrib = "UN",
                            qTrib = "1.0000",
                            vUnTrib = "3.00",
                            indTot = TNFeInfNFeDetProdIndTot.Item1,
                            nItemPed = "0"
                        },
                        imposto = new TNFeInfNFeDetImposto
                        {
                            Items = new TNFeInfNFeDetImpostoICMS[1]
                            {
                                new TNFeInfNFeDetImpostoICMS
                                {
                                    Item = new TNFeInfNFeDetImpostoICMSICMSSN102
                                    {
                                        CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102,
                                        orig = Torig.Item0
                                    }
                                }
                            },
                            PIS = new TNFeInfNFeDetImpostoPIS
                            {
                                Item = new TNFeInfNFeDetImpostoPISPISOutr
                                {

                                    CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item99,
                                    ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC,ItemsChoiceType1.pPIS },
                                    Items = new string[] { "0.00", "0.00" },
                                    vPIS = "0.00"
                                }
                            },

                            COFINS = new TNFeInfNFeDetImpostoCOFINS
                            {
                                Item = new TNFeInfNFeDetImpostoCOFINSCOFINSOutr
                                {
                                    CST = TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item99,
                                    ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.vBC,ItemsChoiceType3.pCOFINS },
                                    Items = new string[] { "0.00", "0.00" },
                                    vCOFINS = "0.00"
                                }
                            },

                        }
                    }
                };
            }
           return det;
        }
    } 
}
