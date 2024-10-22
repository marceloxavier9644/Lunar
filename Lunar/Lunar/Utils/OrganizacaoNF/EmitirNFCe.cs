using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using NHibernate.Classic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Utils.OrganizacaoNF
{
    public class EmitirNFCe
    {
        TratarGeracaoNFe tratarCampo = new TratarGeracaoNFe();
        ProdutoController produtoController = new ProdutoController();
        TNFeInfNFeDet[] det = null;
        TNFeInfNFeDet[] det2 = null;
        TNFeInfNFePagDetPag[] detPag = null;
        TNFeInfNFePagDetPag[] detPag2 = null;
        TNFeInfNFeTotal infNFeTotal;
        // NfeProduto nfeProduto = new NfeProduto();
        string xmlString = "";
        Pessoa cliente = null;
        Nfe objetoNfe = new Nfe();
        FormaPagamento formaPagamento = new FormaPagamento();
        string chave = "";
        public string gerarXMLNfce(decimal valorProdutos, decimal valorNota, decimal valorDescontoTotal, string numeroNfe, IList<NfeProduto> listProdutos, Pessoa cliente, Venda venda, OrdemServico ordemServico, FormaPagamento formaPagamento, string chave)
        {
            try
            {
                this.formaPagamento = formaPagamento;
                this.chave = chave;
                objetoNfe = new Nfe();
                if (cliente != null)
                {
                    if(cliente.Id > 0)
                        this.cliente = cliente;
                }
                objetoNfe = AlimentaObjetoNfe_1(valorProdutos, valorNota, valorDescontoTotal, numeroNfe);
                if (venda != null)
                {
                    venda.Nfe = objetoNfe;
                    if (valorNota != venda.ValorFinal)
                    {
                        objetoNfe.VNf = venda.ValorFinal;
                        objetoNfe = AlimentaObjetoNfe_1(venda.ValorProdutos, objetoNfe.VNf, venda.ValorDesconto, numeroNfe);
                        Controller.getInstance().salvar(objetoNfe);
                    }
                    Controller.getInstance().salvar(venda);
                }
                if (ordemServico != null)
                {
                    ordemServico.Nfe = objetoNfe;
                    Controller.getInstance().salvar(ordemServico);
                }
                infNFeTotal = NfTotais_2(objetoNfe);

                //Gravar produtos em NFeProdutos
                foreach (NfeProduto nfeProduto in listProdutos)
                {
                    nfeProduto.Nfe = objetoNfe;
                    nfeProduto.DataEntrada = objetoNfe.DataLancamento;
                    nfeProduto.UComConvertida = nfeProduto.UCom;
                    nfeProduto.QuantidadeEntrada = double.Parse(nfeProduto.QCom);
        
                    Controller.getInstance().salvar(nfeProduto);
                }

                TAmb tpamb = new TAmb();
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpamb = TAmb.Item1;
                else
                    tpamb = TAmb.Item2;
                //Aqui ja alimente o det2 por completo.
                geraProdutosNFCe_3(listProdutos);

                if (cliente != null)
                    xmlString = gerarNFCeComConsumidor_4(objetoNfe, tpamb, venda, ordemServico);
                else
                    xmlString = gerarNFCeSemConsumidor_4(objetoNfe, tpamb, venda, ordemServico);

                return xmlString;
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta("Falha ao gerar XML da Nota, informe ao representante do seu sistema. " + ex.Message);
                return null;
            }
        }
        public int CapturarCDV(string chaveAcesso)
        {
            if (string.IsNullOrEmpty(chaveAcesso) || chaveAcesso.Length != 44)
            {
                // Chave de acesso inválida
                return -1;
            }

            // Captura o último dígito da chave de acesso
            char cDV = chaveAcesso[43];

            // Converte o dígito para inteiro e retorna
            return int.Parse(cDV.ToString());
        }
        private Nfe AlimentaObjetoNfe_1(decimal valorProdutos, decimal valorNota, decimal valorDescontoTotal, string numeroNF)
        {
            NfeController nfeController = new NfeController();
            Nfe nfe = new Nfe();
            nfe = nfeController.selecionarNFCePorNumeroESerie(numeroNF, Sessao.parametroSistema.SerieNFCe);
            if (nfe == null) 
                nfe = new Nfe();
            //nfe.CDv = "";
            nfe.CDv = CapturarCDV(chave).ToString();
            nfe.IndIntermed = "1";
            nfe.CMunFg = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            nfe.CNf = "";
            nfe.CnpjEmitente = Sessao.empresaFilialLogada.Cnpj;
            nfe.Conciliado = true;
            nfe.CUf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            nfe.DataLancamento = DateTime.Now;
            nfe.DataEmissao = DateTime.Now;
            nfe.DhEmi = DateTime.Now.ToShortTimeString();
            nfe.EmpresaFilial = Sessao.empresaFilialLogada;
            nfe.Lancada = true;
            nfe.TipoOperacao = "S";
            nfe.Modelo = "65";
            nfe.NatOp = "VENDA";
            nfe.RazaoEmitente = Sessao.empresaFilialLogada.RazaoSocial;
            nfe.VBc = 0;
            nfe.VIcms = 0;
            nfe.VIcmsDeson = 0;
            nfe.VFcp = 0;
            nfe.VBcst = 0;
            nfe.VSt = 0;
            nfe.VFcpst = 0;
            nfe.VFcpstRet = 0;
            nfe.VFrete = 0;
            nfe.VProd = valorProdutos;
            nfe.VSeg = 0;
            nfe.VDesc = valorDescontoTotal;
            nfe.VIi = 0;
            nfe.VIpi = 0;
            nfe.VIpiDevol = 0;
            nfe.VPis = 0;
            nfe.VCofins = 0;
            nfe.VOutro = 0;
            nfe.VNf = valorNota;
            nfe.VTotTrib = 0;
            nfe.NNf = numeroNF;
            nfe.Status = "Preparando Envio...";
            nfe.CodStatus = "0";
            nfe.Chave = "";
            nfe.Serie = Sessao.parametroSistema.SerieNFCe;
            nfe.DhSaiEnt = DateTime.Now.ToString();
            nfe.TpNf = "1";
            nfe.IdDest = "1";
            nfe.TpImp = "4";
            nfe.TpEmis = "1";
            if (Sessao.parametroSistema.AmbienteProducao == true)
                nfe.TpAmb = "1";
            else
                nfe.TpAmb = "2";
            nfe.FinNfe = "1";
            nfe.IndFinal = "1";
            nfe.IndPres = "1";
            nfe.InfCpl = Sessao.parametroSistema.InformacaoAdicionalNFCe;
            nfe.ProcEmi = "0";
            nfe.VerProc = "1.0|Lunar";
            nfe.ModFrete = "9";
            nfe.Manifesto = "";
            nfe.Protocolo = "";
            nfe.Destinatario = "";
            nfe.CnpjDestinatario = "";
            nfe.PossuiCartaCorrecao = false;
            if (cliente != null)
            {
                nfe.Cliente = cliente;
                nfe.Destinatario = cliente.RazaoSocial;
                nfe.CnpjDestinatario = GenericaDesktop.RemoveCaracteres(cliente.Cnpj);
            }
            if (nfe.NatOp.Contains("VENDA"))
            {
                nfe.MovimentaEstoque = true;
                nfe.MovimentaFinanceiro = true;
            }
            Controller.getInstance().salvar(nfe);
            return nfe;
        }

        private TNFeInfNFeTotal NfTotais_2(Nfe nfe)
        {
            try
            {
                TNFeInfNFeTotal totalNota = new TNFeInfNFeTotal
                {
                    ICMSTot = new TNFeInfNFeTotalICMSTot
                    {
                        vBC = formatMoedaNf(nfe.VBc),
                        vICMS = formatMoedaNf(nfe.VIcms),
                        vICMSDeson = formatMoedaNf(nfe.VIcmsDeson),
                        vFCPUFDest = "0.00",
                        vICMSUFDest = "0.00",
                        vICMSUFRemet = "0.00",
                        vFCP = formatMoedaNf(nfe.VFcp),
                        vBCST = formatMoedaNf(nfe.VBcst),
                        vST = formatMoedaNf(nfe.VSt),
                        vFCPST = formatMoedaNf(nfe.VFcpst),
                        vFCPSTRet = formatMoedaNf(nfe.VFcpstRet),
                        vProd = formatMoedaNf(nfe.VProd),
                        vFrete = formatMoedaNf(nfe.VFrete),
                        vSeg = formatMoedaNf(nfe.VSeg),
                        vDesc = formatMoedaNf(nfe.VDesc),
                        vII = formatMoedaNf(nfe.VIi),
                        vIPI = formatMoedaNf(nfe.VIpi),
                        vIPIDevol = formatMoedaNf(nfe.VIpiDevol),
                        vPIS = formatMoedaNf(nfe.VPis),
                        vCOFINS = formatMoedaNf(nfe.VCofins),
                        vOutro = formatMoedaNf(nfe.VOutro),
                        vNF = formatMoedaNf(nfe.VNf),
                        vTotTrib = formatMoedaNf(nfe.VTotTrib),
                    }
                };
                return totalNota;
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowAlerta("Falha no calculo dos totais da nota fiscal" + err.Message);
                return null;
            }
        }

        private string formatMoedaNf(decimal valor)
        {
            try
            {
                string valorFormatado = String.Format("{0:0.00}", valor);
                return valorFormatado.Replace(",", ".");
            }
            catch
            {
                return valor.ToString();
            }
        }

        private string formatMoedaNf6Decimais(decimal valor)
        {
            try
            {
                string valorFormatado = String.Format("{0:0.000000}", valor);
                return valorFormatado.Replace(",", ".");
            }
            catch
            {
                return valor.ToString();
            }
        }

        private TNFeInfNFeDet[]geraProdutosNFCe_3(IList<NfeProduto> listaProdutos)
        {
            try
            {
                det = new TNFeInfNFeDet[listaProdutos.Count];
                det2 = new TNFeInfNFeDet[listaProdutos.Count];

                int i = 0;
                foreach (NfeProduto produto in listaProdutos)
                {
                    i++;
                    string cest = null;
                    if (!String.IsNullOrEmpty(produto.Cest))
                        cest = produto.Cest;
                    string descricaoProduto = produto.Produto.Descricao.Trim();
                    if (descricaoProduto.Length > 120)
                        descricaoProduto = descricaoProduto.Substring(0, 119);
                    if (Sessao.parametroSistema.AmbienteProducao == false)
                        descricaoProduto = "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                    if (produto.VDesc > 0)
                    {
                        det = new TNFeInfNFeDet[]
                        {
                            new TNFeInfNFeDet
                            {
                            nItem = i.ToString(),
                            prod = new TNFeInfNFeDetProd
                            {
                                cEAN = "SEM GTIN",
                                cEANTrib = "SEM GTIN",
                                cProd = produto.Produto.Id.ToString(),
                                xProd = descricaoProduto,
                                NCM = produto.Ncm,
                                CEST = cest,
                                CFOP = produto.Produto.CfopVenda,
                                uCom = produto.Produto.UnidadeMedida.Sigla,
                                qCom = formatMoedaNf(decimal.Parse(produto.QCom)),
                                vUnCom = formatMoedaNf6Decimais(produto.VUnCom),
                                vProd = formatMoedaNf(produto.VUnCom * decimal.Parse(produto.QCom)),
                                uTrib = produto.Produto.UnidadeMedida.Sigla,
                                qTrib = formatMoedaNf(decimal.Parse(produto.QCom)),
                                vDesc = formatMoedaNf(produto.VDesc),
                                vUnTrib = formatMoedaNf6Decimais(produto.VUnCom),

                                indTot = TNFeInfNFeDetProdIndTot.Item1
                                //nItemPed = "0"}}
                            },
                                imposto = new TNFeInfNFeDetImposto
                                {
                                    //ICMS
                                    Items = geraImpostoICMS(produto.Produto),
                                    PIS = geraImpostoPIS(produto.Produto)[0],
                                    COFINS = geraImpostoCofins(produto.Produto)[0],
                                }
                            }
                        };
                    }
                    //GERA PRODUTO SEM DESCONTO
                    else
                    {
                        det = new TNFeInfNFeDet[]
                    {
                    new TNFeInfNFeDet
                    {
                        nItem = i.ToString(),
                        prod = new TNFeInfNFeDetProd
                        {
                            cEAN = "SEM GTIN",
                            cEANTrib = "SEM GTIN",
                            cProd = produto.Produto.Id.ToString(),
                            xProd = descricaoProduto,
                            NCM = produto.Ncm,
                            CEST = cest,
                            CFOP = produto.Produto.CfopVenda,
                            uCom = produto.Produto.UnidadeMedida.Sigla,
                            qCom = formatMoedaNf(decimal.Parse(produto.QCom)),
                            vUnCom = formatMoedaNf6Decimais(produto.VUnCom),
                            vProd = formatMoedaNf(produto.VUnCom * decimal.Parse(produto.QCom)),
                            uTrib = produto.Produto.UnidadeMedida.Sigla,
                            qTrib = formatMoedaNf(decimal.Parse(produto.QCom)),
                            vUnTrib = formatMoedaNf6Decimais(produto.VUnCom),
                            indTot = TNFeInfNFeDetProdIndTot.Item1
                            //nItemPed = "0"}}
                        },
                        imposto = new TNFeInfNFeDetImposto
                        {
                            Items = geraImpostoICMS(produto.Produto),
                            PIS = geraImpostoPIS(produto.Produto)[0],
                            COFINS = geraImpostoCofins(produto.Produto)[0],
                        }
                    }
                    };
                    }
                    det2[i - 1] = det[0];
                }
                return det2;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao inserir itens na emissão de nota " + erro.Message);
                return null;
            }
        }

        private TNFeInfNFeDetImpostoICMS[] geraImpostoICMS(Produto produto)
        {
            ///Origem ICMS
            Torig orig = new Torig();
            if (produto.OrigemIcms == "0")
                orig = Torig.Item0;
            else if (produto.OrigemIcms == "1")
                orig = Torig.Item1;
            else if (produto.OrigemIcms == "2")
                orig = Torig.Item2;
            else if (produto.OrigemIcms == "3")
                orig = Torig.Item3;
            else if (produto.OrigemIcms == "4")
                orig = Torig.Item4;
            else if (produto.OrigemIcms == "5")
                orig = Torig.Item5;
            else if (produto.OrigemIcms == "6")
                orig = Torig.Item6;
            else if (produto.OrigemIcms == "7")
                orig = Torig.Item7;
            else if (produto.OrigemIcms == "8")
                orig = Torig.Item8;
            else
                orig = Torig.Item0;


            if (produto.CstIcms == "102")
            {
                TNFeInfNFeDetImpostoICMS[] Items = new TNFeInfNFeDetImpostoICMS[]
                {
                    new TNFeInfNFeDetImpostoICMS
                    {
                        Item = new TNFeInfNFeDetImpostoICMSICMSSN102
                        {
                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102,
                            orig = orig
                        }
                    }
                };
                return Items;
            }
            else if (produto.CstIcms == "101")
            {
                TNFeInfNFeDetImpostoICMS[] Items = new TNFeInfNFeDetImpostoICMS[]
                {
                    new TNFeInfNFeDetImpostoICMS
                    {
                        Item = new TNFeInfNFeDetImpostoICMSICMSSN101
                        {
                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN101CSOSN.Item101,
                            orig = orig,
                            pCredSN = "0.00",
                            vCredICMSSN = "0.00"
                        }
                    }
                };
                return Items;
            }

            else if (produto.CstIcms == "500")
            {
                TNFeInfNFeDetImpostoICMS[] Items = new TNFeInfNFeDetImpostoICMS[]
                {
                    new TNFeInfNFeDetImpostoICMS
                    {
                        Item = new TNFeInfNFeDetImpostoICMSICMSSN500
                        {
                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN500CSOSN.Item500,
                            orig = orig,
                            vBCSTRet = "0.00",
                            pST = "0.00",
                            vICMSSubstituto = "0.00",
                            vICMSSTRet = "0.00"
                        }
                    }
                };
                return Items;
            }
            else
            {
                throw new Exception("O Produto " + produto.Id.ToString() + " - " + produto.Descricao + " Está sem CST de ICMS ou está inválido, favor corrigir este produto em Cadastro de produtos, aba de tributação!");
            }
        }


        private TNFeInfNFeDetImpostoPIS[] geraImpostoPIS(Produto produto)
        {
            if (produto.CstPis == "99")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item99,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                };
                return Items;
            }
            else if (produto.CstPis == "98")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item98,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }

            else if (produto.CstPis == "75")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item75,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "74")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item74,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "73")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item73,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "72")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item72,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "71")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item71,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "70")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item70,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "67")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item67,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "66")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item66,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "65")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item65,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "64")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item64,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "63")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item63,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "62")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item62,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "61")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item61,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "60")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item60,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "56")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item56,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "55")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item55,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "54")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item54,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "53")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item53,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "52")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item52,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "51")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item51,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "50")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item50,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "49")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISOutr
                        {
                            CST = TNFeInfNFeDetImpostoPISPISOutrCST.Item49,
                            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.vBC, ItemsChoiceType1.pPIS },
                            Items = new string[] { "0.00", "0.00" },
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "04")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item04
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "05")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item05
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "06")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item06
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "07")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item07
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "08")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item08
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "09")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISNT
                        {
                            CST = TNFeInfNFeDetImpostoPISPISNTCST.Item09
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "03")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISQtde
                        {
                            CST = TNFeInfNFeDetImpostoPISPISQtdeCST.Item03,
                            qBCProd = "0.00",
                            vAliqProd = "0.00",
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "02")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISAliq
                        {
                            CST = TNFeInfNFeDetImpostoPISPISAliqCST.Item02,
                            pPIS = "0.00",
                            vBC = "0.00",
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else if (produto.CstPis == "01")
            {
                TNFeInfNFeDetImpostoPIS[] Items = new TNFeInfNFeDetImpostoPIS[]
                  {
                    new TNFeInfNFeDetImpostoPIS
                    {
                        Item = new TNFeInfNFeDetImpostoPISPISAliq
                        {
                            CST = TNFeInfNFeDetImpostoPISPISAliqCST.Item01,
                            pPIS = "0.00",
                            vBC = "0.00",
                            vPIS = "0.00"
                        }
                    }
                  };
                return Items;
            }
            else
            {
                throw new Exception("O Produto " + produto.Id.ToString() + " - " + produto.Descricao + " Está sem CST de PIS ou está inválido, favor corrigir este produto em Cadastro de produtos, aba de tributação!");
            }
        }

        private TNFeInfNFeDetImpostoCOFINS[] geraImpostoCofins(Produto produto)
        {
            if (produto.CstCofins == "99")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSOutr
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item99,
                            ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.vBC, ItemsChoiceType3.pCOFINS },
                            Items = new string[] { "0.00", "0.00" },
                            vCOFINS = "0.00"
                        }
                    }
                };
                return Items;
            }
            else if (produto.CstCofins == "98")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSOutr
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item98,
                            ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.vBC, ItemsChoiceType3.pCOFINS },
                            Items = new string[] { "0.00", "0.00" },
                            vCOFINS = "0.00"
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "04")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item04
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "05")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item05
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "06")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item06
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "07")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item07
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "08")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item08
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "09")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSNT
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item09
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "01")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSAliq
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item01,
                            pCOFINS = "0.00",
                            vBC = "0.00",
                            vCOFINS = "0.00"
                        }
                    }
                 };
                return Items;
            }
            else if (produto.CstCofins == "02")
            {
                TNFeInfNFeDetImpostoCOFINS[] Items = new TNFeInfNFeDetImpostoCOFINS[]
                 {
                    new TNFeInfNFeDetImpostoCOFINS
                    {
                        Item = new TNFeInfNFeDetImpostoCOFINSCOFINSAliq
                        {
                            CST = TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item02,
                            pCOFINS = "0.00",
                            vBC = "0.00",
                            vCOFINS = "0.00"
                        }
                    }
                 };
                return Items;
            }
            else
            {
                throw new Exception("O Produto " + produto.Id.ToString() + " - " + produto.Descricao + " Está sem CST de COFINS ou está inválido, favor corrigir este produto em Cadastro de produtos, aba de tributação!");
            }
        }

        private string gerarNFCeComConsumidor_4(Nfe nfe, TAmb tpAmbiente, Venda venda, OrdemServico ordemServico)
        {
            VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
            OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
            IList<VendaFormaPagamento> listaFormaPag = new List<VendaFormaPagamento>();
            if (venda != null)
                listaFormaPag = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);
            IList<OrdemServicoPagamento> listaFormaPagOs = new List<OrdemServicoPagamento>();
            if (ordemServico != null)
                listaFormaPagOs = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
            decimal troco = 0;
            string infComplementar = Sessao.parametroSistema.InformacaoAdicionalNFCe;
            if (String.IsNullOrEmpty(infComplementar))
                infComplementar = "OBRIGADO PELA PREFERENCIA, VOLTE SEMPRE!!!";
            if (listaFormaPag.Count > 0)
            {
                foreach (VendaFormaPagamento vfp in listaFormaPag)
                {
                    if (vfp.Troco > 0)
                        troco = troco + vfp.Troco;
                }
            }
            if(listaFormaPagOs.Count > 0)
            {
                foreach (OrdemServicoPagamento vfp in listaFormaPagOs)
                {
                    if (vfp.Troco > 0)
                        troco = troco + vfp.Troco;
                }
            }
            string razaoConsumidor = nfe.Destinatario;
            string cpfDestinatario = nfe.CnpjDestinatario;
            if (tpAmbiente == TAmb.Item2)
            {
                razaoConsumidor = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            }

            string razaoSocialEmissor = Sessao.empresaFilialLogada.RazaoSocial;
            if (tpAmbiente == TAmb.Item2)
            {
                razaoSocialEmissor = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            }

            tratarCampo = new TratarGeracaoNFe();
            TNFe NFCe = new TNFe
            {
                infNFe = new TNFeInfNFe
                {
                    Id = "Nfe" + chave,
                    versao = "4.00",
                    ide = new TNFeInfNFeIde
                    {
                        cUF = tratarCampo.Retornar_cUFFilialLogada(),

                        natOp = nfe.NatOp,
                        mod = TMod.Item65,
                        serie = Sessao.parametroSistema.SerieNFCe,
                        nNF = nfe.NNf,
                        dhEmi = DateTime.Now.ToString("s") + "-03:00",
                        tpNF = TNFeInfNFeIdeTpNF.Item1,
                        idDest = TNFeInfNFeIdeIdDest.Item1,
                        cMunFG = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                        tpImp = TNFeInfNFeIdeTpImp.Item4,
                        tpEmis = TNFeInfNFeIdeTpEmis.Item1,

                        tpAmb = tpAmbiente,

                        finNFe = TFinNFe.Item1,
                        indFinal = TNFeInfNFeIdeIndFinal.Item1,
                        indPres = TNFeInfNFeIdeIndPres.Item1,
                        indIntermed = TNFeInfNFeIdeIndIntermed.Item0,
                        procEmi = TProcEmi.Item0,
                        verProc = "4.00"
                    },
                    emit = new TNFeInfNFeEmit
                    {
                        ItemElementName = ItemChoiceType2.CNPJ,
                        Item = Sessao.empresaFilialLogada.Cnpj,
                        xNome = razaoSocialEmissor,
                        IE = Sessao.empresaFilialLogada.InscricaoEstadual,
                        //Simples nacional
                        CRT = TNFeInfNFeEmitCRT.Item1,
                        enderEmit = enderecoEmitente()
                    },
                    dest = new TNFeInfNFeDest
                    {
                        ItemElementName = ItemChoiceType3.CPF,
                        Item = cpfDestinatario,
                        xNome = razaoConsumidor,
                        indIEDest = retornarTipoIE(),
                        enderDest = new TEndereco
                        {
                            xLgr = nfe.Cliente.EnderecoPrincipal.Logradouro,
                            nro = nfe.Cliente.EnderecoPrincipal.Numero,
                            xBairro = nfe.Cliente.EnderecoPrincipal.Bairro,
                            //ajustar
                            cMun = nfe.Cliente.EnderecoPrincipal.Cidade.Ibge,
                            xMun = nfe.Cliente.EnderecoPrincipal.Cidade.Descricao,
                            UF = tratarCampo.Retornar_TUf(nfe.Cliente.EnderecoPrincipal.Cidade.Estado.Uf),
                            CEP = (GenericaDesktop.RemoveCaracteres(nfe.Cliente.EnderecoPrincipal.Cep)),
                            cPais = "1058",
                            xPais = "Brasil"
                        }
                    },
                    //Produtos
                    det = this.det2,
                    //Totais
                    total = infNFeTotal,

                    transp = new TNFeInfNFeTransp
                    {
                        modFrete = TNFeInfNFeTranspModFrete.Item9
                    },
                    pag = new TNFeInfNFePag
                    {
                        detPag = retornaPagamentoNFCe(listaFormaPag, listaFormaPagOs),
                        vTroco = troco > 0 ? formatMoedaNf(troco) : null
                    },
                    infAdic = new TNFeInfNFeInfAdic
                    {
                        infCpl = infComplementar
                    },
                    infRespTec = new TInfRespTec
                    {
                        CNPJ = "28145398000173",
                        xContato = "MARCELO XAVIER",
                        email = "marcelo.xs@hotmail.com",
                        fone = "3836769644"
                    }
                }
            };
            string NFCeXML = Genericos.NFCeToXML(NFCe);
            return NFCeXML;
        }
        private TNFeInfNFeDestIndIEDest retornarTipoIE()
        {
            if (cliente != null)
            {
                if (cliente.Id > 0)
                {
                    if (!String.IsNullOrEmpty(cliente.InscricaoEstadual) && !cliente.InscricaoEstadual.Equals("ISENTO"))
                    {
                        return TNFeInfNFeDestIndIEDest.Item1;
                    }
                    else if (cliente.InscricaoEstadual.Equals("ISENTO"))
                        return TNFeInfNFeDestIndIEDest.Item2;
                    else
                        return TNFeInfNFeDestIndIEDest.Item9;
                }
                else
                    return TNFeInfNFeDestIndIEDest.Item9;
            }
            else
                return TNFeInfNFeDestIndIEDest.Item9;
        }
        private string gerarNFCeSemConsumidor_4(Nfe nfe, TAmb tpAmbiente, Venda venda, OrdemServico ordemServico)
        {
            VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
            OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
            IList<VendaFormaPagamento> listaFormaPag = new List<VendaFormaPagamento>();
            if (venda != null)
                listaFormaPag = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);
            IList<OrdemServicoPagamento> listaFormaPagOs = new List<OrdemServicoPagamento>();
            if (ordemServico != null)
                listaFormaPagOs = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
            decimal troco = 0;
            string infComplementar = Sessao.parametroSistema.InformacaoAdicionalNFCe;
            if (String.IsNullOrEmpty(infComplementar))
                infComplementar = "OBRIGADO PELA PREFERENCIA, VOLTE SEMPRE!!!";

            if (venda != null)
            {
                foreach (VendaFormaPagamento vfp in listaFormaPag)
                {
                    if (vfp.Troco > 0)
                        troco = troco + vfp.Troco;
                }
            }
            if(ordemServico != null)
            {
                foreach (OrdemServicoPagamento vfp in listaFormaPagOs)
                {
                    if (vfp.Troco > 0)
                        troco = troco + vfp.Troco;
                }
            }
            string razaoConsumidor = nfe.Destinatario;
            string cpfDestinatario = nfe.CnpjDestinatario;
            if (tpAmbiente == TAmb.Item2)
            {
                razaoConsumidor = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            }

            string razaoSocialEmissor = Sessao.empresaFilialLogada.RazaoSocial;
            if (tpAmbiente == TAmb.Item2)
            {
                razaoSocialEmissor = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            }

            tratarCampo = new TratarGeracaoNFe();
            TNFe NFCe = new TNFe
            {
                infNFe = new TNFeInfNFe
                {
                    Id = "Nfe" + chave,
                    versao = "4.00",
                    ide = new TNFeInfNFeIde
                    {
                        cUF = tratarCampo.Retornar_cUFFilialLogada(),

                        natOp = nfe.NatOp,
                        mod = TMod.Item65,
                        serie = Sessao.parametroSistema.SerieNFCe,
                        nNF = nfe.NNf,
                        dhEmi = DateTime.Now.ToString("s") + "-03:00",
                        tpNF = TNFeInfNFeIdeTpNF.Item1,
                        idDest = TNFeInfNFeIdeIdDest.Item1,
                        cMunFG = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                        tpImp = TNFeInfNFeIdeTpImp.Item4,
                        tpEmis = TNFeInfNFeIdeTpEmis.Item1,

                        tpAmb = tpAmbiente,

                        finNFe = TFinNFe.Item1,
                        indFinal = TNFeInfNFeIdeIndFinal.Item1,
                        indPres = TNFeInfNFeIdeIndPres.Item1,
                        indIntermed = TNFeInfNFeIdeIndIntermed.Item0,
                        procEmi = TProcEmi.Item0,
                        verProc = "4.00"
                    },
                    emit = new TNFeInfNFeEmit
                    {
                        ItemElementName = ItemChoiceType2.CNPJ,
                        Item = Sessao.empresaFilialLogada.Cnpj,
                        xNome = razaoSocialEmissor,
                        IE = Sessao.empresaFilialLogada.InscricaoEstadual,
                        //Simples nacional
                        CRT = TNFeInfNFeEmitCRT.Item1,
                        enderEmit = enderecoEmitente()
                    },
                    //Produtos
                    det = this.det2,
                    //Totais
                    total = infNFeTotal,

                    transp = new TNFeInfNFeTransp
                    {
                        modFrete = TNFeInfNFeTranspModFrete.Item9
                    },
                    pag = new TNFeInfNFePag
                    {
                        detPag = retornaPagamentoNFCe(listaFormaPag, listaFormaPagOs),
                        vTroco = troco > 0 ? formatMoedaNf(troco) : null
                    },
                    infAdic = new TNFeInfNFeInfAdic
                    {
                        infCpl = infComplementar
                    },
                    infRespTec = new TInfRespTec
                    {
                        CNPJ = "28145398000173",
                        xContato = "MARCELO XAVIER",
                        email = "marcelo.xs@hotmail.com",
                        fone = "3836769644"
                    }
                }
            };
            string NFCeXML = Genericos.NFCeToXML(NFCe);
            return NFCeXML;
        }

        private TEnderEmi enderecoEmitente()
        {
            TEnderEmi enderecoEmitente = new TEnderEmi();
            String bairro = "";
            String numeroEndereco = "";
            if (!String.IsNullOrEmpty(Sessao.empresaFilialLogada.Endereco.Bairro))
                bairro = Sessao.empresaFilialLogada.Endereco.Bairro;
            else
                bairro = "NAO INFORMADO";
            if (!String.IsNullOrEmpty(Sessao.empresaFilialLogada.Endereco.Numero))
                numeroEndereco = Sessao.empresaFilialLogada.Endereco.Numero;
            else
                numeroEndereco = "SN";


            if (String.IsNullOrEmpty(Sessao.empresaFilialLogada.Endereco.Complemento))
            {
                string fone = GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.DddPrincipal) + GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.TelefonePrincipal);
                if (String.IsNullOrEmpty(fone))
                    fone = "0000000000"; 
                enderecoEmitente = new TEnderEmi
                {
                    xLgr = Sessao.empresaFilialLogada.Endereco.Logradouro,
                    nro = numeroEndereco,
                    xBairro = bairro,
                    cMun = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                    xMun = Sessao.empresaFilialLogada.Endereco.Cidade.Descricao,
                    UF = tratarCampo.Retornar_TUfEmi(Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf),
                    CEP = GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.Endereco.Cep),
                    fone = fone
                };
                return enderecoEmitente;
            }
            else
            {
                enderecoEmitente = new TEnderEmi
                {
                    xLgr = Sessao.empresaFilialLogada.Endereco.Logradouro,
                    nro = numeroEndereco,
                    xCpl = Sessao.empresaFilialLogada.Endereco.Complemento,
                    xBairro = bairro,
                    cMun = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                    xMun = Sessao.empresaFilialLogada.Endereco.Cidade.Descricao,
                    UF = tratarCampo.Retornar_TUfEmi(Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf),
                    CEP = GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.Endereco.Cep),
                    fone = GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.DddPrincipal) + GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.TelefonePrincipal)
                };
                return enderecoEmitente;
            }
        }

        private TNFeInfNFePagDetPag[] retornaPagamentoNFCe(IList<VendaFormaPagamento> listaPagamento, IList<OrdemServicoPagamento> listaPagamentoOs)
        {
            int i = 0;
            try
            {
                if (listaPagamento.Count > 0)
                {
                    detPag = new TNFeInfNFePagDetPag[listaPagamento.Count];
                    detPag2 = new TNFeInfNFePagDetPag[listaPagamento.Count];
                    foreach (VendaFormaPagamento vendaFormaPagamento in listaPagamento)
                    {
                        //Funcao para gerar o troco certinho na nfce
                        if (vendaFormaPagamento.Troco > 0 && vendaFormaPagamento.FormaPagamento.Descricao.Contains("DIN"))
                            vendaFormaPagamento.ValorRecebido = vendaFormaPagamento.ValorRecebido + vendaFormaPagamento.Troco;
                        i++;
                        string creditoDebito = "";
                        TNFeInfNFePagDetPagIndPag indPagamento;
                        if (vendaFormaPagamento.FormaPagamento.Boleto || vendaFormaPagamento.FormaPagamento.Cheque || vendaFormaPagamento.FormaPagamento.Crediario)
                            indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                        else
                            indPagamento = TNFeInfNFePagDetPagIndPag.Item0;

                        if (vendaFormaPagamento.FormaPagamento.Cartao == true)
                        {
                            AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                            AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
                            IList<AdquirenteCartao> listaAdquirentes = adquirenteCartaoController.selecionarTodasAdquirentes();
                            if (listaAdquirentes.Count > 0)
                            {
                                foreach (AdquirenteCartao adquirente in listaAdquirentes)
                                {
                                    adquirenteCartao = adquirente;
                                }
                            }

                            if (vendaFormaPagamento.TipoCartao.Contains("Crédito") || vendaFormaPagamento.TipoCartao.Contains("DITO"))
                            {
                                creditoDebito = "03";
                                indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                            }

                            //Debito
                            else
                            {
                                creditoDebito = "04";
                                indPagamento = TNFeInfNFePagDetPagIndPag.Item0;
                            }

                            detPag = new TNFeInfNFePagDetPag[]
                            {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                tPag = creditoDebito,
                                vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                                card = new TNFeInfNFePagDetPagCard
                                {
                                    tpIntegra = TNFeInfNFePagDetPagCardTpIntegra.Item2,
                                    CNPJ = adquirenteCartao.Cnpj,
                                    tBand = vendaFormaPagamento.BandeiraCartao.CodigoSefaz
                                }
                            }
                            };
                        }
                        else if (vendaFormaPagamento.FormaPagamento.CodigoSefaz.Equals("99"))
                        {
                            detPag = new TNFeInfNFePagDetPag[]
                                             {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                xPag = vendaFormaPagamento.FormaPagamento.Descricao,
                                tPag = vendaFormaPagamento.FormaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                            }
                            };
                        }
                        else if (vendaFormaPagamento.FormaPagamento.CodigoSefaz.Equals("17"))
                        {
                            //ajuste devido erro de troco, venda parece q tava arredondando valores com descontos
                            if (vendaFormaPagamento.ValorRecebido > vendaFormaPagamento.Venda.ValorFinal)
                                vendaFormaPagamento.ValorRecebido = vendaFormaPagamento.Venda.ValorFinal;
                            detPag = new TNFeInfNFePagDetPag[]
                                             {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                //xPag = vendaFormaPagamento.FormaPagamento.Descricao,
                                tPag = vendaFormaPagamento.FormaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                                card = new TNFeInfNFePagDetPagCard
                                {
                                    tpIntegra = TNFeInfNFePagDetPagCardTpIntegra.Item2
                                }
                            }
                            };
                        }
                        else
                        {
                            detPag = new TNFeInfNFePagDetPag[]
                            {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                tPag = vendaFormaPagamento.FormaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                            }
                            };
                        }
                        detPag2[i - 1] = detPag[0];
                    }
                    return detPag2;
                }
                //GERAR PAGAMENTO DE O.S
                else if (listaPagamentoOs.Count > 0)
                {
                    decimal valorRecebidoSomado = 0;
                    int pass = 0;
                    //detPag = new TNFeInfNFePagDetPag[listaPagamentoOs.Count];
                    //detPag2 = new TNFeInfNFePagDetPag[listaPagamentoOs.Count];
                    detPag = new TNFeInfNFePagDetPag[1];
                    detPag2 = new TNFeInfNFePagDetPag[1];
                    int v = 0;
                    OrdemServicoPagamento vendaFormaPagamento = new OrdemServicoPagamento();
                    foreach (OrdemServicoPagamento vendaFormaPagamentos in listaPagamentoOs)
                    {
                        v++;
                        if (v == listaPagamentoOs.Count)
                        {
                            //Utilizando esta funcao para gerar a nfce apenas com o valor do produto como recebido, senao fica complexo qdo tem produto e servico junto
                            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
                            IList<OrdemServicoProduto> listaprod = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(vendaFormaPagamentos.OrdemServico.Id);
                            decimal valorRecebidoProduto = 0;
                            foreach (OrdemServicoProduto osp in listaprod)
                            {
                                valorRecebidoProduto = valorRecebidoProduto + osp.ValorTotal;
                            }
                            vendaFormaPagamentos.ValorRecebido = valorRecebidoProduto;
                            vendaFormaPagamento = vendaFormaPagamentos;
                        }
                    }

                        //essa funcao aqui é apenas pra separar o valor do serviço, senao o valor de troco daria erro
                        //if (vendaFormaPagamento.OrdemServico.ValorProduto < vendaFormaPagamento.ValorRecebido && listaPagamentoOs.Count == 1)
                        //{
                        //    OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
                        //    IList<OrdemServicoProduto> listaprod = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(vendaFormaPagamento.OrdemServico.Id);
                        //    decimal valorRecebidoProduto = 0;
                        //    foreach(OrdemServicoProduto osp in listaprod)
                        //    {
                        //        valorRecebidoProduto = valorRecebidoProduto + osp.ValorTotal;
                        //    }
                        //    vendaFormaPagamento.ValorRecebido = valorRecebidoProduto;
                        //}
                        //Essa aqui é pra verificar sem tem mais de 1 forma de pagamento e se possui produto e serviço pra ratear o valor recebido em 
                        //cada forma
                        //else if(vendaFormaPagamento.OrdemServico.ValorProduto > vendaFormaPagamento.ValorRecebido)
                        //{
                        //if (vendaFormaPagamento.OrdemServico.ValorServico > 0 && listaPagamentoOs.Count > 1)
                        //{
                        //    pass++;
                        //    decimal percentual = ((vendaFormaPagamento.ValorRecebido / vendaFormaPagamento.OrdemServico.ValorTotal) * 100);
                        //    vendaFormaPagamento.ValorRecebido = ((percentual / 100) * vendaFormaPagamento.OrdemServico.ValorProduto);
                        //    valorRecebidoSomado = valorRecebidoSomado + vendaFormaPagamento.ValorRecebido;
                        //    if (pass == listaPagamentoOs.Count)
                        //    {
                        //        //Se é a ultima forma de pagamento deve arredonar os valores
                        //        if (valorRecebidoSomado < vendaFormaPagamento.OrdemServico.ValorProduto)
                        //        {
                        //            decimal diferenca = vendaFormaPagamento.OrdemServico.ValorProduto - valorRecebidoSomado;
                        //            vendaFormaPagamento.ValorRecebido = vendaFormaPagamento.ValorRecebido + diferenca;
                        //        }
                        //        else if (valorRecebidoSomado > vendaFormaPagamento.OrdemServico.ValorProduto)
                        //        {
                        //            decimal diferenca = valorRecebidoSomado - vendaFormaPagamento.OrdemServico.ValorProduto;
                        //            vendaFormaPagamento.ValorRecebido = vendaFormaPagamento.ValorRecebido - diferenca;
                        //        }
                        //    }
                        //}
                        //}

                        //Funcao para gerar o troco certinho na nfce
                        //if (vendaFormaPagamento.Troco > 0 && vendaFormaPagamento.FormaPagamento.Descricao.Contains("DIN"))
                        //    vendaFormaPagamento.ValorRecebido = vendaFormaPagamento.ValorRecebido + vendaFormaPagamento.Troco;

                        //i++;
                        //string creditoDebito = "";
                        //TNFeInfNFePagDetPagIndPag indPagamento;
                        //if (vendaFormaPagamento.FormaPagamento.Boleto || vendaFormaPagamento.FormaPagamento.Cheque || vendaFormaPagamento.FormaPagamento.Crediario)
                        //    indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                        //else
                        //    indPagamento = TNFeInfNFePagDetPagIndPag.Item0;

                        //if (vendaFormaPagamento.FormaPagamento.Cartao == true)
                        //{
                        //    AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                        //    AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
                        //    IList<AdquirenteCartao> listaAdquirentes = adquirenteCartaoController.selecionarTodasAdquirentes();
                        //    if (listaAdquirentes.Count > 0)
                        //    {
                        //        foreach (AdquirenteCartao adquirente in listaAdquirentes)
                        //        {
                        //            adquirenteCartao = adquirente;
                        //        }
                        //    }

                        //    if (vendaFormaPagamento.TipoCartao.Contains("Crédito") || vendaFormaPagamento.TipoCartao.Contains("DITO"))
                        //    {
                        //        creditoDebito = "03";
                        //        indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                        //    }

                        //    //Debito
                        //    else
                        //    {
                        //        creditoDebito = "04";
                        //        indPagamento = TNFeInfNFePagDetPagIndPag.Item0;
                        //    }

                        //    detPag = new TNFeInfNFePagDetPag[]
                        //    {
                        //    new TNFeInfNFePagDetPag
                        //    {
                        //        indPag = indPagamento,
                        //        tPag = creditoDebito,
                        //        vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                        //        card = new TNFeInfNFePagDetPagCard
                        //        {
                        //            tpIntegra = TNFeInfNFePagDetPagCardTpIntegra.Item2,
                        //            CNPJ = adquirenteCartao.Cnpj,
                        //            tBand = vendaFormaPagamento.BandeiraCartao.CodigoSefaz
                        //        }
                        //    }
                        //    };
                        //}
                        //else if (vendaFormaPagamento.FormaPagamento.CodigoSefaz.Equals("99"))
                        //{
                        //    detPag = new TNFeInfNFePagDetPag[]
                        //                     {
                        //    new TNFeInfNFePagDetPag
                        //    {
                        //        indPag = indPagamento,
                        //        xPag = vendaFormaPagamento.FormaPagamento.Descricao,
                        //        tPag = vendaFormaPagamento.FormaPagamento.CodigoSefaz,
                        //        vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                        //    }
                        //    };
                        //}
                        //else
                        //{
                        //    detPag = new TNFeInfNFePagDetPag[]
                        //    {
                        //    new TNFeInfNFePagDetPag
                        //    {
                        //        indPag = indPagamento,
                        //        tPag = vendaFormaPagamento.FormaPagamento.CodigoSefaz,
                        //        vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                        //    }
                        //    };
                        //}
                        detPag = new TNFeInfNFePagDetPag[]
                           {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = TNFeInfNFePagDetPagIndPag.Item1,
                                tPag = "99",
                                vPag = formatMoedaNf(vendaFormaPagamento.ValorRecebido),
                            }
                           };
                        detPag2[0] = detPag[0];
                        //detPag2[i - 1] = detPag[0];
                    //}
                    return detPag2;
                }
                //pagamento de nota agrupada
                else
                {
                    i = 0;
                    detPag = new TNFeInfNFePagDetPag[1];
                    detPag2 = new TNFeInfNFePagDetPag[1];
                    i++;
                    string creditoDebito = "";
                    TNFeInfNFePagDetPagIndPag indPagamento;
                    if (formaPagamento == null)
                    {
                        formaPagamento = new FormaPagamento();
                        formaPagamento.Id = 2;
                        formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                    }
                    
                    if (formaPagamento.Boleto || formaPagamento.Cheque || formaPagamento.Crediario)
                        indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                    else
                        indPagamento = TNFeInfNFePagDetPagIndPag.Item0;

                    if (formaPagamento.Cartao == true)
                    {
                        AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
                        IList<AdquirenteCartao> listaAdquirentes = adquirenteCartaoController.selecionarTodasAdquirentes();
                        if (listaAdquirentes.Count > 0)
                        {
                            foreach (AdquirenteCartao adquirente in listaAdquirentes)
                            {
                                adquirenteCartao = adquirente;
                            }
                        }

                        if (formaPagamento.Descricao.Contains("Crédito") || formaPagamento.Descricao.Equals("CARTAO") || formaPagamento.Descricao.Equals("CARTÃO"))
                        {
                            creditoDebito = "03";
                            indPagamento = TNFeInfNFePagDetPagIndPag.Item1;
                        }
                        //Debito
                        else
                        {
                            creditoDebito = "04";
                            indPagamento = TNFeInfNFePagDetPagIndPag.Item0;
                        }

                        detPag = new TNFeInfNFePagDetPag[]
                        {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                tPag = creditoDebito,
                                vPag = formatMoedaNf(objetoNfe.VNf),
                                card = new TNFeInfNFePagDetPagCard
                                {
                                    tpIntegra = TNFeInfNFePagDetPagCardTpIntegra.Item2,
                                    CNPJ = adquirenteCartao.Cnpj,
                                    tBand = "99"
                                }
                            }
                        };
                    }
                    else if (formaPagamento.CodigoSefaz.Equals("99"))
                    {
                        detPag = new TNFeInfNFePagDetPag[]
                                            {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                xPag = GenericaDesktop.RemoveCaracteres(formaPagamento.Descricao),
                                tPag = formaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(objetoNfe.VNf),
                            }
                        };
                    }
                    else if (formaPagamento.CodigoSefaz.Equals("17"))
                    {
                        detPag = new TNFeInfNFePagDetPag[]
                                            {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                xPag = GenericaDesktop.RemoveCaracteres(formaPagamento.Descricao),
                                tPag = formaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(objetoNfe.VNf),
                                card = new TNFeInfNFePagDetPagCard
                                {
                                    tpIntegra = TNFeInfNFePagDetPagCardTpIntegra.Item2
                                }
                            }
                        };
                    }
                    else
                    {
                        detPag = new TNFeInfNFePagDetPag[]
                        {
                            new TNFeInfNFePagDetPag
                            {
                                indPag = indPagamento,
                                tPag = formaPagamento.CodigoSefaz,
                                vPag = formatMoedaNf(objetoNfe.VNf)
                            }
                        };
                    }
                }
                detPag2[i - 1] = detPag[0];
                return detPag2;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao inserir pagamento no xml da nota fiscal" + erro.Message);
                return null;
            }
        }

        public string ReenviarXMLApi(string xmlNfce, Nfe nfe)
         {
            NfeStatus nfeStatus = new NfeStatus();
            NfeStatusController nfeStatusController = new NfeStatusController();
            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            GenericaDesktop generica = new GenericaDesktop();
            string codStatusRet = "";
            if (!Directory.Exists(@"\XML\Tentativa\NFCe"))
                Directory.CreateDirectory(@"\XML\Tentativa\NFCe");
            if (File.Exists(@"\XML\Tentativa\NFCe\" + nfe.NNf + ".xml"))
                File.Delete(@"\XML\Tentativa\NFCe\" + nfe.NNf + ".xml");
            gravarXMLNaPasta(xmlNfce, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", false);

            if (GenericaDesktop.possuiConexaoInternet())
            {
                string tipoAmbiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tipoAmbiente = "1";
                String retorno = NSSuite.emitirNFCeSincrono(xmlNfce, "xml", Sessao.empresaFilialLogada.Cnpj, tipoAmbiente, @"Fiscal\XML\NFCe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", true, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);

                if (!String.IsNullOrEmpty(retornoNFCe.cStat))
                    codStatusRet = retornoNFCe.cStat;
                else
                    codStatusRet = retornoNFCe.statusEnvio;

                String errosNaNota = "";
                if (retornoNFCe.erros != null)
                {
                    for (int xx = 0; xx < retornoNFCe.erros.Count; xx++)
                    {
                        errosNaNota = errosNaNota + "\n" + retornoNFCe.erros[xx];
                    }

                    GenericaDesktop.ShowAlerta("Erro retornado: " + errosNaNota);
                }
                
                nfe.Status = errosNaNota;
                nfe.CodStatus = codStatusRet;

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    nfe.Protocolo = retornoNFCe.nProt;
                    nfe.Chave = retornoNFCe.chNFe;
                    nfeStatus = new NfeStatus();
                    nfeStatus.Id = 1;
                    nfe.NfeStatus = (NfeStatus)nfeStatusController.selecionar(nfeStatus);
                    nfe.Status = "Autorizado o uso da NF-e";
                    nfe.Lancada = true;
                    Genericos genericosNF = new Genericos();
                    NFCeDownloadProc nota = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                    var nfeRet = Genericos.LoadFromXMLString<TNfeProc>(nota.nfeProc.xml);
                    genericosNF.gravarXMLNoBanco(nfeRet, 0, "S", nfe.Id, false);
                    Controller.getInstance().salvar(nfe);
                    return "Nota Autorizada com Sucesso!";
                }

                else
                {
                    if (retornoNFCe.motivo.Contains("Rejeição: Duplicidade de NF-e com diferença na Chave de Acesso"))
                    {
                        string motiv = retornoNFCe.motivo;
                        motiv = retornoNFCe.motivo.Replace("Rejeição: Duplicidade de NF-e com diferença na Chave de Acesso", "");
                        string possChave = motiv.Substring(8, 44).ToString();
                        nfe.Chave = possChave;
                        Controller.getInstance().salvar(nfe);
                        GenericaDesktop.ShowAlerta(retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n" + "Tente consultar a nota para possível autorização!");
                        return "";
                    }
                    else
                    {
                        Controller.getInstance().salvar(nfe);
                        GenericaDesktop.ShowAlerta(retornoNFCe.cStat + " " + retornoNFCe.motivo);
                        return "";
                    }
                }
            }
            //Se nao tem internet anula a operação de reenvio
            else
            {
                GenericaDesktop.ShowAlerta("Falha de conexão com a internet");
                return "";
            }
        }
        private void gravarXMLNaPasta(string xml, string numeroNFCe, string caminhoArmazenamento, string nomeArquivo, bool emiteContigencia)
        {
            if (!nomeArquivo.Contains(".xml"))
                nomeArquivo = nomeArquivo + ".xml";
            if (!Directory.Exists(caminhoArmazenamento))
            {
                Directory.CreateDirectory(caminhoArmazenamento);
            }
            string arquivo = caminhoArmazenamento + nomeArquivo;
            if (!File.Exists(arquivo))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
        }
    }
}
