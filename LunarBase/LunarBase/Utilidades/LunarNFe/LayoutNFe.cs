//using ns_nfe_core.src.emissao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades.LunarNFe
{
    public class LayoutNFe
    {
       /* public static TNFe gerarNFeXML()
        {
            TNFe NFe = new()
            {
                infNFe = new TNFeInfNFe
                {
                    versao = "4.00",
                    ide = new TNFeInfNFeIde
                    {
                        cUF = (TCodUfIBGE)Enum.Parse(typeof(TCodUfIBGE), "Item" + Convert.ToInt16("31")),
                        cNF = "",
                        natOp = "VENDA A PRAZO - SEM VALOR FISCAL",
                        mod = TMod.Item55,
                        serie = "0",
                        nNF = "22527",
                        dhEmi = DateTime.Now.ToString("s") + "-03:00",
                        tpNF = TNFeInfNFeIdeTpNF.Item1,
                        idDest = TNFeInfNFeIdeIdDest.Item1,
                        cMunFG = "3170404",
                        tpImp = TNFeInfNFeIdeTpImp.Item1,
                        tpEmis = TNFeInfNFeIdeTpEmis.Item1,
                        cDV = "",
                        tpAmb = TAmb.Item2,
                        finNFe = TFinNFe.Item1,
                        indFinal = TNFeInfNFeIdeIndFinal.Item0,
                        indPres = TNFeInfNFeIdeIndPres.Item1,
                        //indIntermed = TNFeInfNFeIdeIndIntermed.Item1,
                        procEmi = TProcEmi.Item0,
                        verProc = "4.00"
                    },
                    emit = new TNFeInfNFeEmit
                    {
                        ItemElementName = ItemChoiceType2.CNPJ,
                        Item = "28145398000173",
                        xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                        IE = "0030011730013",
                        CRT = TNFeInfNFeEmitCRT.Item1,
                        enderEmit = new TEnderEmi
                        {
                            xLgr = "Rua Canabrava",
                            nro = "777",
                            xCpl = "CX POSTAL 91",
                            xBairro = "Centro",
                            cMun = "3170404",
                            xMun = "UNAÍ",
                            UF = TUfEmi.MG,
                            CEP = "38610031",
                            fone = "005432200200"
                        }
                    },
                    dest = new TNFeInfNFeDest
                    {
                        ItemElementName = ItemChoiceType3.CNPJ,
                        xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                        Item = "28145398000173",
                        indIEDest = TNFeInfNFeDestIndIEDest.Item1,
                        IE = "0030011730013",
                        enderDest = new TEndereco
                        {
                            xLgr = "Rua Canabrava",
                            nro = "0",
                            xBairro = "Centro",
                            cMun = "3170404",
                            xMun = "UNAÍ",
                            UF = TUf.MG,
                            CEP = "38610031",
                            cPais = "1058",
                            xPais = "BRASIL"
                        }
                    },
                    det = new TNFeInfNFeDet[1]
                    {
                        new TNFeInfNFeDet
                        {
                            nItem = "1",
                            prod = new TNFeInfNFeDetProd
                            {
                                cEAN = "SEM GTIN",
                                cEANTrib = "SEM GTIN",
                                cProd = "123456789",
                                xProd = "COCA-COLA LT 250ML",
                                NCM = "22021000",
                                CEST = "0301100",
                                CFOP = "5101",
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
                                vTotTrib = "0.00",
                                Items = new TNFeInfNFeDetImpostoICMS[1]
                                {
                                    new TNFeInfNFeDetImpostoICMS
                                    {
                                        Item = new TNFeInfNFeDetImpostoICMSICMSSN102
                                        {
                                            orig = Torig.Item0,
                                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102
                                        }
                                    }
                                },
                                PIS = new TNFeInfNFeDetImpostoPIS
                                {
                                    Item = new TNFeInfNFeDetImpostoPISPISAliq
                                    {
                                        CST = TNFeInfNFeDetImpostoPISPISAliqCST.Item01,
                                        vBC = "3.00",
                                        pPIS = "1.65",
                                        vPIS = "0.05",
                                    }
                                },
                                COFINS = new TNFeInfNFeDetImpostoCOFINS
                                {
                                    Item = new TNFeInfNFeDetImpostoCOFINSCOFINSAliq
                                    {
                                        CST = TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item01,
                                        vBC = "3.00",
                                        pCOFINS = "7.00",
                                        vCOFINS = "0.21"
                                    }
                                }
                            }
                        }
                    },
                    total = new TNFeInfNFeTotal
                    {
                        ICMSTot = new TNFeInfNFeTotalICMSTot
                        {
                            vBC = "0",
                            vICMS = "0",
                            vICMSDeson = "0.00",
                            vFCPUFDest = "0.00",
                            vICMSUFDest = "0.00",
                            vICMSUFRemet = "0.00",
                            vFCP = "0",
                            vBCST = "0",
                            vST = "0",
                            vFCPST = "0",
                            vFCPSTRet = "0.00",
                            vProd = "3.00",
                            vFrete = "0.00",
                            vSeg = "0.00",
                            vDesc = "0.00",
                            vII = "0.00",
                            vIPI = "0.00",
                            vIPIDevol = "0.00",
                            vPIS = "0.05",
                            vCOFINS = "0.21",
                            vOutro = "0.00",
                            vNF = "3.00",
                            vTotTrib = "0.00"
                        }
                    },
                    transp = new TNFeInfNFeTransp
                    {
                        modFrete = TNFeInfNFeTranspModFrete.Item9
                    },
                    pag = new TNFeInfNFePag
                    {
                        detPag = new TNFeInfNFePagDetPag[1]
                        {
                            new TNFeInfNFePagDetPag
                            {
                                tPag = "16",
                                vPag = "5.00"
                            }
                        },
                        vTroco = "2.00"
                    },
                    infAdic = new TNFeInfNFeInfAdic
                    {
                        infCpl = "TESTE DE EMISSAO UTILIZANDO ESTRUTURA XSD"
                    }
                }
            };
            return NFe;
        }

        public static TNFe gerarLayoutNFe()
        {
            TNFe NFe = new();

            NFe.infNFe = new TNFeInfNFe();

            NFe.infNFe.ide = new TNFeInfNFeIde();
            NFe.infNFe.ide.cUF = (TCodUfIBGE)Enum.Parse(typeof(TCodUfIBGE), "Item" + Convert.ToInt16("43"));
            NFe.infNFe.ide.cNF = "";
            NFe.infNFe.ide.natOp = "VENDA A PRAZO - SEM VALOR FISCAL";
            NFe.infNFe.ide.mod = TMod.Item55;
            NFe.infNFe.ide.serie = "0";
            NFe.infNFe.ide.nNF = "22527";
            NFe.infNFe.ide.dhEmi = DateTime.Now.ToString("s") + "-03:00";
            NFe.infNFe.ide.tpNF = TNFeInfNFeIdeTpNF.Item1;
            NFe.infNFe.ide.idDest = TNFeInfNFeIdeIdDest.Item1;
            NFe.infNFe.ide.cMunFG = "4305108";
            NFe.infNFe.ide.tpImp = TNFeInfNFeIdeTpImp.Item1;
            NFe.infNFe.ide.tpEmis = TNFeInfNFeIdeTpEmis.Item1;
            NFe.infNFe.ide.cDV = "";
            NFe.infNFe.ide.tpAmb = TAmb.Item2;
            NFe.infNFe.ide.finNFe = TFinNFe.Item1;
            NFe.infNFe.ide.indFinal = TNFeInfNFeIdeIndFinal.Item0;
            NFe.infNFe.ide.indPres = TNFeInfNFeIdeIndPres.Item9;
            NFe.infNFe.ide.indIntermed = TNFeInfNFeIdeIndIntermed.Item0;
            NFe.infNFe.ide.procEmi = TProcEmi.Item0;
            NFe.infNFe.ide.verProc = "4.00";

            retu*/
    }
}
