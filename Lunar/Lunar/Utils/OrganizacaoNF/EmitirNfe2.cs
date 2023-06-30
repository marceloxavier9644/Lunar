using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using static Lunar.Telas.Fiscal.FrmNfe;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Utils.OrganizacaoNF
{
    public class EmitirNfe2
    {
        decimal valorJaRecebido = 0;
        TratarGeracaoNFe tratarCampo = new TratarGeracaoNFe();
        ProdutoController produtoController = new ProdutoController();
        TNFeInfNFeDet[] det = null;
        TNFeInfNFeDet[] det2 = null;
        TNFeInfNFePagDetPag[] detPag = null;
        TNFeInfNFePagDetPag[] detPag2 = null;
        TNFeInfNFeTotal infNFeTotal;
        TNFeInfNFeCobrDup[] duplicatas = null;
        TNFeInfNFeCobrDup[] duplicatasAux = null;
        TNFeInfNFeCobrFat fat = new TNFeInfNFeCobrFat();
        // NfeProduto nfeProduto = new NfeProduto();
        string xmlString = "";
        Pessoa cliente = null;
        TNFeInfNFeTranspModFrete tipoFrete = new TNFeInfNFeTranspModFrete();
        TNFeInfNFeIdeIndPres tipoPresenca = new TNFeInfNFeIdeIndPres();
        TNFeInfNFeDetImpostoDevol impostoDevR = new TNFeInfNFeDetImpostoDevol();
        Nfe objetoNfe = new Nfe();
        TNFeInfNFeIdeNFref[] notasReferenciadas = null;
        decimal valorFrete;
        decimal valorSeguro;
        decimal valorOutrasDespesas;
        TNFeInfNFeIdeTpNF tpNf = new TNFeInfNFeIdeTpNF();
        Frete frete = new Frete();
        NaturezaOperacao natureza = new NaturezaOperacao();
        public string gerarXMLNfe(decimal valorProdutos, decimal valorNota, decimal valorDescontoTotal, string numeroNfe, IList<NfeProduto> listProdutos, Pessoa cliente, Venda venda, bool reenvio, string naturezaOperacao, TNFeInfNFeTranspModFrete tipoFrete, TNFeInfNFeIdeIndPres tipoPresenca, TNFeInfNFeIdeNFref[] notasReferenciadas, decimal valorFrete, decimal valorSeguro, decimal valorOutrasDespesas, TNFeInfNFeIdeTpNF tpNf, Frete frete, NaturezaOperacao natureza, Nfe nota)
        {
            try
            {
                if (natureza != null)
                    this.natureza = natureza;
                if (notasReferenciadas != null)
                {
                    if (notasReferenciadas.Length > 0)
                    {
                        if (notasReferenciadas[0] != null)
                            this.notasReferenciadas = notasReferenciadas;
                    }
                }
                if (frete != null)
                {
                    if (frete.transportadora != null)
                        this.frete = frete;
                }
                this.valorFrete = valorFrete;
                this.valorSeguro = valorSeguro;
                this.valorOutrasDespesas = valorOutrasDespesas;
                this.tipoPresenca = tipoPresenca;
                this.tipoFrete = tipoFrete;
                this.tpNf = tpNf;
                objetoNfe = new Nfe();
                if (cliente != null)
                    this.cliente = cliente;

                Nfe nfeAux = new Nfe();
                NfeController nfeController = new NfeController();
                nfeAux = nfeController.selecionarNFePorNumeroESerie(numeroNfe, Sessao.parametroSistema.SerieNFe);
                if (nfeAux != null)
                {
                    objetoNfe = nfeAux;
                    nota.Id = nfeAux.Id;
                }
                else if (nfeAux == null && venda != null)
                {
                    objetoNfe = AlimentaObjetoNfe_1(valorProdutos, valorNota, valorDescontoTotal, numeroNfe, reenvio, naturezaOperacao, nota.VIpiDevol);
                    venda.Nfe = objetoNfe;
                    Controller.getInstance().salvar(venda);
                }

                decimal bcICMS = 0;
                decimal valorICMS = 0;
                //Gravar produtos em NFeProdutos
                foreach (NfeProduto nfeProduto in listProdutos)
                {
                    nfeProduto.Nfe = objetoNfe;
                    nfeProduto.DataEntrada = objetoNfe.DataLancamento;
                    nfeProduto.UComConvertida = nfeProduto.UCom;
                    nfeProduto.QuantidadeEntrada = double.Parse(nfeProduto.QCom);
                    Controller.getInstance().salvar(nfeProduto);
                    bcICMS = nfeProduto.VBC + bcICMS;
                    valorICMS = nfeProduto.VICMS + valorICMS;
                }
                if (bcICMS > 0)
                {
                    objetoNfe.VBc = bcICMS;
                    objetoNfe.VIcms = valorICMS;
                    Controller.getInstance().salvar(objetoNfe);
                }

                infNFeTotal = NfTotais_2(objetoNfe);

                TAmb tpamb = new TAmb();
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    tpamb = TAmb.Item1;
                else
                    tpamb = TAmb.Item2;
                //Aqui ja alimente o det2 por completo.
                geraProdutosNFe_3(listProdutos);

                if (cliente != null && venda != null)
                    xmlString = gerarNFeComConsumidor_ComFinanceiro(objetoNfe, tpamb, venda);
                else
                    xmlString = gerarNFeComConsumidor_SemFinanceiro(objetoNfe, tpamb, null);

                return xmlString;
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta("Falha ao gerar XML da Nota, informe ao representante do seu sistema. " + ex.Message);
                return null;
            }
        }

        private Nfe AlimentaObjetoNfe_1(decimal valorProdutos, decimal valorNota, decimal valorDescontoTotal, string numeroNF, bool reenvio, string naturezaOperacao, decimal valorIpiDevolvido)
        {
            NfeController nfeController = new NfeController();
            Nfe nfe = new Nfe();
            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNF, Sessao.parametroSistema.SerieNFe);
            if (nfe == null)
                nfe = new Nfe();
            nfe.CDv = "";
            nfe.IndIntermed = "1";
            nfe.CMunFg = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            nfe.CNf = "";
            nfe.CnpjEmitente = Sessao.empresaFilialLogada.Cnpj;
            nfe.Conciliado = true;
            nfe.CUf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            if (reenvio == false)
            {
                nfe.DataLancamento = DateTime.Now;
                nfe.DataEmissao = DateTime.Now;
                nfe.DhEmi = DateTime.Now.ToShortTimeString();
            }
            nfe.EmpresaFilial = Sessao.empresaFilialLogada;
            nfe.Lancada = true;
            nfe.TipoOperacao = "S";
            nfe.Modelo = "55";
            nfe.NatOp = naturezaOperacao;
            nfe.NaturezaOperacao = natureza;
            nfe.RazaoEmitente = Sessao.empresaFilialLogada.RazaoSocial;
            nfe.VBc = 0;
            nfe.VIcms = 0;
            nfe.VIcmsDeson = 0;
            nfe.VFcp = 0;
            nfe.VBcst = 0;
            nfe.VSt = 0;
            nfe.VFcpst = 0;
            nfe.VFcpstRet = 0;
            nfe.VFrete = valorFrete;
            nfe.VProd = valorProdutos;
            nfe.VSeg = valorSeguro;
            nfe.VDesc = valorDescontoTotal;
            nfe.VIi = 0;
            nfe.VIpi = 0;
            nfe.VIpiDevol = valorIpiDevolvido;
            nfe.VPis = 0;
            nfe.VCofins = 0;
            nfe.VOutro = valorOutrasDespesas;
            nfe.VNf = valorNota;
            nfe.VTotTrib = 0;
            nfe.NNf = numeroNF;
            nfe.Status = "Preparando Envio...";
            nfe.CodStatus = "0";
            nfe.Chave = "";
            nfe.Serie = Sessao.parametroSistema.SerieNFe;
            nfe.DhSaiEnt = DateTime.Now.ToString();
            nfe.TpNf = "1";
            nfe.IdDest = "1";
            nfe.TpImp = "1";
            nfe.TpEmis = "1";
            if (Sessao.parametroSistema.AmbienteProducao == true)
                nfe.TpAmb = "1";
            else
                nfe.TpAmb = "2";
            nfe.FinNfe = natureza.FinalidadeNfe;
            nfe.IndFinal = "1";
            nfe.IndPres = "1";
            if (string.IsNullOrEmpty(nfe.InfCpl))
                nfe.InfCpl = Sessao.parametroSistema.InformacaoAdicionalNFe;
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
        private string formatPeso(decimal valor)
        {
            try
            {
                string valorFormatado = String.Format("{0:0.000}", valor);
                return valorFormatado.Replace(",", ".");
            }
            catch
            {
                return valor.ToString();
            }
        }
        private TNFeInfNFeCobrDup[] gerarDuplicatasVenda(Venda vendaNF)
        {
            try
            {
                int i = 0;
                int contadorParcelas = 0;
                bool fezParcelado = false;
                decimal valorJaPago = 0;

                IList<VendaFormaPagamento> listaPagamento = new List<VendaFormaPagamento>();
                VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
                ContaReceberController contaReceberController = new ContaReceberController();
                listaPagamento = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(vendaNF.Id);
                IList<ContaReceber> listaReceber = new List<ContaReceber>();
                if (listaPagamento.Count > 0)
                {
                    duplicatas = new TNFeInfNFeCobrDup[0];

                    //Conta qts parcelas tem todos recebimentos
                    foreach (VendaFormaPagamento vendaFormaPagamento in listaPagamento)
                    {
                        if (vendaFormaPagamento.Parcelamento == 0 || vendaFormaPagamento.Cartao)
                        {
                            vendaFormaPagamento.Parcelamento = 1;
                            valorJaPago = valorJaPago + vendaFormaPagamento.ValorRecebido;
                        }
                        contadorParcelas = vendaFormaPagamento.Parcelamento + contadorParcelas;
                    }
                    duplicatasAux = new TNFeInfNFeCobrDup[contadorParcelas];
                    //Lanca as duplicatas
                    foreach (VendaFormaPagamento vendaFormaPagamento in listaPagamento)
                    {
                        if (valorJaRecebido > 0)
                        {
                            i++;
                            duplicatas = new TNFeInfNFeCobrDup[]
                            {
                                new TNFeInfNFeCobrDup
                                {
                                    nDup = i.ToString().PadLeft(3, '0'),
                                    dVenc = DateTime.Now.ToString("yyyy-MM-dd"),
                                    vDup = formatMoedaNf(valorJaRecebido)
                                }
                            };
                            duplicatasAux[i - 1] = duplicatas[0];
                            valorJaRecebido = 0;
                        }

                        if (fezParcelado == false)
                        {
                            listaReceber = contaReceberController.selecionarContaReceberPorVendaFormaPagamento(vendaFormaPagamento.Id);
                            if (listaReceber.Count > 0)
                            {
                                fezParcelado = true;
                                foreach (ContaReceber contaReceber in listaReceber)
                                {
                                    i++;
                                    duplicatas = new TNFeInfNFeCobrDup[]
                                    {
                                        new TNFeInfNFeCobrDup
                                        {
                                            nDup = i.ToString().PadLeft(3, '0'),
                                            dVenc = contaReceber.Vencimento.ToString("yyyy-MM-dd"),
                                            vDup = formatMoedaNf(contaReceber.ValorParcela)
                                        }
                                    };
                                    duplicatasAux[i - 1] = duplicatas[0];
                                }
                            }
                        }
                        else
                            duplicatasAux = null;
                        //}
                    }
                }
                return duplicatasAux;
            }
            catch (Exception erroDuplicatas)
            {
                GenericaDesktop.ShowInfo(erroDuplicatas.Message);
                return null;
            }
        }


        private TNFeInfNFeDet[] geraProdutosNFe_3(IList<NfeProduto> listaProdutos)
        {
            try
            {
                det = new TNFeInfNFeDet[listaProdutos.Count];
                det2 = new TNFeInfNFeDet[listaProdutos.Count];

                int i = 0;
                foreach (NfeProduto produto in listaProdutos)
                {
                    string valorFreteItem = produto.VFrete.ToString();
                    if (valorFreteItem.Equals("0,00"))
                        valorFreteItem = null;
                    else
                        valorFreteItem = formatMoedaNf(produto.VFrete);

                    string valorOutro = produto.VOutro.ToString();
                    if (valorOutro.Equals("0,00"))
                        valorOutro = null;
                    else
                        valorOutro = formatMoedaNf(produto.VOutro);

                    string valorSeguro = produto.VSeguro.ToString();
                    if (valorSeguro.Equals("0,00"))
                        valorSeguro = null;
                    else
                        valorSeguro = formatMoedaNf(produto.VSeguro);

                    i++;
                    string cest = null;
                    if (!String.IsNullOrEmpty(produto.Cest))
                        cest = produto.Cest;
                    string descricaoProduto = produto.Produto.Descricao;
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
                                CFOP = produto.Cfop,
                                uCom = produto.Produto.UnidadeMedida.Sigla,
                                qCom = formatMoedaNf(decimal.Parse(produto.QCom)),
                                vUnCom = formatMoedaNf(produto.VProd),
                                vProd = formatMoedaNf(produto.VProd * decimal.Parse(produto.QCom)),
                                uTrib = produto.Produto.UnidadeMedida.Sigla,
                                qTrib = formatMoedaNf(decimal.Parse(produto.QCom)),
                                vDesc = formatMoedaNf(produto.VDesc),
                                vUnTrib = formatMoedaNf(produto.VProd),
                                indTot = TNFeInfNFeDetProdIndTot.Item1,
                                vFrete = valorFreteItem,
                                vOutro = valorOutro,
                                vSeg = valorSeguro
                                
                                //nItemPed = "0"}}
                            },
                                imposto = new TNFeInfNFeDetImposto
                                {
                                    //ICMS
                                    Items = geraImpostoICMS_IPI(produto),
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
                            CFOP = produto.Cfop,
                            uCom = produto.Produto.UnidadeMedida.Sigla,
                            qCom = formatMoedaNf(decimal.Parse(produto.QCom)),
                            vUnCom = formatMoedaNf(produto.VProd),
                            vProd = formatMoedaNf(produto.VProd * decimal.Parse(produto.QCom)),
                            uTrib = produto.Produto.UnidadeMedida.Sigla,
                            qTrib = formatMoedaNf(decimal.Parse(produto.QCom)),
                            vUnTrib = formatMoedaNf(produto.VProd),
                            indTot = TNFeInfNFeDetProdIndTot.Item1,
                            vFrete = valorFreteItem,
                            vOutro = valorOutro,
                            vSeg = valorSeguro
                            //nItemPed = "0"}}
                        },
                        imposto = new TNFeInfNFeDetImposto
                        {
                            Items = geraImpostoICMS_IPI(produto),
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

        private object[] geraImpostoICMS_IPI(NfeProduto nfeProduto)
        {
            TNFeInfNFeDetImpostoICMS[] icms = new TNFeInfNFeDetImpostoICMS[0];
            TIpi ipi = new TIpi();

            if (nfeProduto.PICMSST == null)
                nfeProduto.PICMSST = "0";
            if (nfeProduto.PICMS == null)
                nfeProduto.PICMS = "0";
            ///Origem ICMS
            Torig orig = new Torig();
            if (nfeProduto.Produto.OrigemIcms == "0")
                orig = Torig.Item0;
            else if (nfeProduto.Produto.OrigemIcms == "1")
                orig = Torig.Item1;
            else if (nfeProduto.Produto.OrigemIcms == "2")
                orig = Torig.Item2;
            else if (nfeProduto.Produto.OrigemIcms == "3")
                orig = Torig.Item3;
            else if (nfeProduto.Produto.OrigemIcms == "4")
                orig = Torig.Item4;
            else if (nfeProduto.Produto.OrigemIcms == "5")
                orig = Torig.Item5;
            else if (nfeProduto.Produto.OrigemIcms == "6")
                orig = Torig.Item6;
            else if (nfeProduto.Produto.OrigemIcms == "7")
                orig = Torig.Item7;
            else if (nfeProduto.Produto.OrigemIcms == "8")
                orig = Torig.Item8;
            else
                orig = Torig.Item0;

            if (nfeProduto.CstIcms == "102")
            {
                TNFeInfNFeDetImpostoICMS[] items = new TNFeInfNFeDetImpostoICMS[]
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
                icms = items;
            }
            else if (nfeProduto.CstIcms == "101")
            {
                TNFeInfNFeDetImpostoICMS[] items = new TNFeInfNFeDetImpostoICMS[]
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
                icms = items;
            }

            else if (nfeProduto.CstIcms == "500")
            {
                TNFeInfNFeDetImpostoICMS[] items = new TNFeInfNFeDetImpostoICMS[]
                {
                    new TNFeInfNFeDetImpostoICMS
                    {
                        Item = new TNFeInfNFeDetImpostoICMSICMSSN500
                        {
                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN500CSOSN.Item500,
                            orig = orig,
                            vBCSTRet = formatMoedaNf(nfeProduto.VBCSTRet),
                            pST = formatMoedaNf(decimal.Parse(nfeProduto.PST)),
                            vICMSSubstituto = formatMoedaNf(nfeProduto.VICMSSubstituto),
                            vICMSSTRet = formatMoedaNf(nfeProduto.VICMSSTRet)
                        }
                    }
                };
                icms = items;
            }
            else if (nfeProduto.CstIcms == "900")
            {
                TNFeInfNFeDetImpostoICMS[] items = new TNFeInfNFeDetImpostoICMS[]
                {
                    new TNFeInfNFeDetImpostoICMS
                    {
                        Item = new TNFeInfNFeDetImpostoICMSICMSSN900
                        {
                            orig = orig,
                            CSOSN = TNFeInfNFeDetImpostoICMSICMSSN900CSOSN.Item900,
                            modBC = TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item3,
                            vBC = formatMoedaNf(nfeProduto.VBC),
                            pICMS = formatMoedaNf(decimal.Parse(nfeProduto.PICMS)),
                            vICMS = formatMoedaNf(nfeProduto.VICMS),
                            modBCST = TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item0,
                            vBCST = formatMoedaNf(nfeProduto.VBCST),
                            pICMSST = formatMoedaNf(decimal.Parse(nfeProduto.PICMSST)),
                            vICMSST = formatMoedaNf(nfeProduto.VICMSSt)
                        }
                    }
                };
                icms = items;
            }
            else
            {
                throw new Exception("O Produto " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao + " > Gerou falha ao gerar ICMS, consulte o suporte ou contabilidade para conferir os campos preenchidos de icms, confira o CSOSN correto!");
            }

            if (nfeProduto.ValorIpi > 0)
            {
                TIpiIPITrib itemIpi = new TIpiIPITrib
                {
                    CST = TIpiIPITribCST.Item50,
                    ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.vBC, ItemsChoiceType.pIPI },
                    Items = new string[] { formatMoedaNf(nfeProduto.BaseIpi), formatMoedaNf(decimal.Parse(nfeProduto.AliqIpi)) },
                    vIPI = formatMoedaNf(nfeProduto.ValorIpi)
                };

                TIpi ip = new TIpi
                {
                    cEnq = nfeProduto.CodEnqIpi,
                    Item = itemIpi
                };
                ipi = ip;
            }

            //recebe os items 
            TNFeInfNFeDetImposto imposto = new TNFeInfNFeDetImposto();
            imposto.Items = new TNFeInfNFeDetImpostoICMS[]
            {
                icms[0]
            };

            if(nfeProduto.ValorIpi > 0)
            {
                imposto.Items = new TIpi[]
                {
                ipi
                }; 
            }
            return imposto.Items;
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

        private string gerarNFeComConsumidor_ComFinanceiro(Nfe nfe, TAmb tpAmbiente, Venda venda)
        {
            valorJaRecebido = 0;
            VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
            IList<VendaFormaPagamento> listaFormaPag = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);
            decimal troco = 0;
            string infComplementar = Sessao.parametroSistema.InformacaoAdicionalNFCe;
            if (String.IsNullOrEmpty(infComplementar))
                infComplementar = "OBRIGADO PELA PREFERENCIA, VOLTE SEMPRE!!!";
            foreach (VendaFormaPagamento vfp in listaFormaPag)
            {
                if (vfp.Troco > 0)
                    troco = troco + vfp.Troco;
                if (vfp.ValorRecebido > 0 && (vfp.FormaPagamento.Caixa == true || vfp.FormaPagamento.Cartao == true || vfp.FormaPagamento.CreditoCliente == true || vfp.FormaPagamento.Banco))
                    valorJaRecebido = valorJaRecebido + vfp.ValorRecebido;
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
                    versao = "4.00",
                    ide = new TNFeInfNFeIde
                    {
                        cUF = tratarCampo.Retornar_cUFFilialLogada(),

                        natOp = nfe.NatOp,
                        mod = TMod.Item55,
                        serie = Sessao.parametroSistema.SerieNFe,
                        nNF = nfe.NNf,
                        dhEmi = DateTime.Now.ToString("s") + "-03:00",
                        tpNF = tpNf,
                        idDest = TNFeInfNFeIdeIdDest.Item1,
                        cMunFG = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                        tpImp = TNFeInfNFeIdeTpImp.Item1,
                        tpEmis = TNFeInfNFeIdeTpEmis.Item1,
                        tpAmb = tpAmbiente,
                        finNFe = retornaFinalidadeNfe(),
                        indFinal = TNFeInfNFeIdeIndFinal.Item1,
                        indPres = tipoPresenca,
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
                    dest = retornarDestinatario(nfe, tpAmbiente),
                    //Produtos
                    det = this.det2,
                    //Totais
                    total = infNFeTotal,

                    transp = retornaFrete(),
                    cobr = new TNFeInfNFeCobr
                    {
                        fat = new TNFeInfNFeCobrFat
                        {
                            nFat = venda.Id.ToString(),
                            vOrig = formatMoedaNf(venda.ValorProdutos),
                            vDesc = formatMoedaNf(venda.ValorDesconto),
                            vLiq = formatMoedaNf(venda.ValorFinal)
                        },
                        dup = gerarDuplicatasVenda(venda)
                    },
                    pag = new TNFeInfNFePag
                    {
                        detPag = retornaPagamentoNFCe(listaFormaPag),
                        vTroco = formatMoedaNf(troco)
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
        private TNFeInfNFeDest retornarDestinatario(Nfe nfe, TAmb tpAmbiente)
        {
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

            if (String.IsNullOrEmpty(cliente.InscricaoEstadual))
            {
                TNFeInfNFeDest dest = new TNFeInfNFeDest
                {
                    ItemElementName = retornaTipoCliente(),
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

                };
                return dest;
            }
            else
            {
                TNFeInfNFeDest dest = new TNFeInfNFeDest
                {
                    ItemElementName = retornaTipoCliente(),
                    Item = cpfDestinatario,
                    xNome = razaoConsumidor,
                    indIEDest = retornarTipoIE(),
                    IE = GenericaDesktop.RemoveCaracteres(cliente.InscricaoEstadual),
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

                };
                return dest;
            }
        }
        private ItemChoiceType3 retornaTipoCliente()
        {
            if (cliente != null)
            {
                if (cliente.Id > 0)
                {
                    if (cliente.Cnpj.Length == 11)
                        return ItemChoiceType3.CPF;
                    else
                        return ItemChoiceType3.CNPJ;
                }
                else
                    return ItemChoiceType3.CPF;
            }
            else
                return ItemChoiceType3.CPF;
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
        private TFinNFe retornaFinalidadeNfe()
        {
            TFinNFe naturezaOperacao = new TFinNFe();
            if (natureza.FinalidadeNfe.Equals("1"))
                naturezaOperacao = TFinNFe.Item1;
            else if (natureza.FinalidadeNfe.Equals("2"))
                naturezaOperacao = TFinNFe.Item2;
            else if (natureza.FinalidadeNfe.Equals("3"))
                naturezaOperacao = TFinNFe.Item3;
            else
                naturezaOperacao = TFinNFe.Item4;

            return naturezaOperacao;
        }
        private TNFeInfNFeTransp retornaFrete()
        {
            TNFeInfNFeTransp transporte = new TNFeInfNFeTransp();
            transporte.modFrete = tipoFrete;

            if (frete.transportadora != null && !frete.ToString().Equals("Item9"))
            {
                transporte.transporta = new TNFeInfNFeTranspTransporta();
                transporte.vol = new TNFeInfNFeTranspVol[1];

                transporte.transporta.IE = GenericaDesktop.RemoveCaracteres(frete.transportadora.InscricaoEstadual.Trim());
                transporte.transporta.xNome = frete.transportadora.RazaoSocial;
                if (frete.transportadora.Cnpj.Length == 14)
                {
                    transporte.transporta.ItemElementName = ItemChoiceType6.CNPJ;
                    transporte.transporta.Item = frete.transportadora.Cnpj.Trim();
                }
                else if (frete.transportadora.Cnpj.Length == 11)
                {
                    transporte.transporta.ItemElementName = ItemChoiceType6.CPF;
                    transporte.transporta.Item = frete.transportadora.Cnpj.Trim();
                }

                if (frete.transportadora.EnderecoPrincipal != null) 
                {
                    transporte.transporta.UF = tratarCampo.Retornar_TUf(frete.transportadora.EnderecoPrincipal.Cidade.Estado.Uf);
                    transporte.transporta.UFSpecified = true;
                    transporte.transporta.xEnder = frete.transportadora.EnderecoPrincipal.Logradouro +" " + frete.transportadora.EnderecoPrincipal.Numero;
                    transporte.transporta.xMun = frete.transportadora.EnderecoPrincipal.Cidade.Descricao;
                }
                else
                    transporte.transporta.UFSpecified = false;
            }
            if (!String.IsNullOrEmpty(frete.quantidadeVolume))
            {
                transporte.vol[0] = new TNFeInfNFeTranspVol();
                if(!String.IsNullOrEmpty(frete.especie))
                    transporte.vol[0].esp = frete.especie;
                if (!String.IsNullOrEmpty(frete.marca))
                    transporte.vol[0].marca = frete.marca;
                if (!String.IsNullOrEmpty(frete.quantidadeVolume))
                    transporte.vol[0].qVol = frete.quantidadeVolume;
                if (!String.IsNullOrEmpty(frete.pesoLiquido))
                    transporte.vol[0].pesoL = formatPeso(decimal.Parse(frete.pesoLiquido));
                if (!String.IsNullOrEmpty(frete.pesoBruto))
                    transporte.vol[0].pesoB = formatPeso(decimal.Parse(frete.pesoBruto));
            }
            return transporte;
        }
        private string gerarNFeComConsumidor_SemFinanceiro(Nfe nfe, TAmb tpAmbiente, Venda venda)
        {
            string infComplementar = nfe.InfCpl;
            if (String.IsNullOrEmpty(infComplementar))
                infComplementar = "OBRIGADO PELA PREFERENCIA, VOLTE SEMPRE!!!";
            
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
                    versao = "4.00",
                    ide = new TNFeInfNFeIde
                    {
                        cUF = tratarCampo.Retornar_cUFFilialLogada(),
                        natOp = nfe.NatOp,
                        mod = TMod.Item55,
                        serie = Sessao.parametroSistema.SerieNFe,
                        nNF = nfe.NNf,
                        dhEmi = DateTime.Now.ToString("s") + "-03:00",
                        tpNF = tpNf,
                        idDest = TNFeInfNFeIdeIdDest.Item1,
                        cMunFG = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                        tpImp = TNFeInfNFeIdeTpImp.Item1,
                        tpEmis = TNFeInfNFeIdeTpEmis.Item1,
                        tpAmb = tpAmbiente,
                        finNFe = retornaFinalidadeNfe(),
                        indFinal = TNFeInfNFeIdeIndFinal.Item1,
                        indPres = tipoPresenca,
                        indIntermed = TNFeInfNFeIdeIndIntermed.Item0,
                        procEmi = TProcEmi.Item0,
                        verProc = "4.00",
                        NFref = notasReferenciadas
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
                    dest = retornarDestinatario(nfe, tpAmbiente),
                    //Produtos
                    det = this.det2,
                    //Totais
                    total = infNFeTotal,

                    transp = retornaFrete(),

                    pag = new TNFeInfNFePag
                    {
                        detPag = retornaPagamentoNulo()
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


        private TNFeInfNFePagDetPag[] retornaPagamentoNulo()
        {
            TNFeInfNFePagDetPag[] pg = new TNFeInfNFePagDetPag[1];
            TNFeInfNFePagDetPag pag = new TNFeInfNFePagDetPag();
            pag.tPag = "90";
            pag.vPag = formatMoedaNf(0);
            pg[0] = pag;
            return pg;
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

        private TNFeInfNFePagDetPag[] retornaPagamentoNFCe(IList<VendaFormaPagamento> listaPagamento)
        {
            int i = 0;
            try
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
                    if (vendaFormaPagamento.FormaPagamento.CodigoSefaz.Equals("99"))
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
            if (!Directory.Exists(@"\XML\Tentativa\NFe"))
                Directory.CreateDirectory(@"\XML\Tentativa\NFe");
            if (File.Exists(@"\XML\Tentativa\NFe\" + nfe.NNf + ".xml"))
                File.Delete(@"\XML\Tentativa\NFe\" + nfe.NNf + ".xml");
            gravarXMLNaPasta(xmlNfce, nfe.NNf, @"\XML\Tentativa\NFe\", nfe.NNf + ".xml", false);

            if (GenericaDesktop.possuiConexaoInternet())
            {
                String retorno = NSSuite.emitirNFeSincrono(xmlNfce, "xml", Sessao.empresaFilialLogada.Cnpj, "XP", "2", @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", true, false);
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
                    Genericos genericosNF = new Genericos();
                    NFeDownloadProc55 nota = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                    var nfeRet = Genericos.LoadFromXMLString<TNfeProc>(nota.xml);
                    genericosNF.gravarXMLNoBanco(nfeRet, 0, "S", nfe.Id);
                    Controller.getInstance().salvar(nfe);
                    return "Nota Autorizada com Sucesso!";
                }

                else
                {
                    if (!String.IsNullOrEmpty(retornoNFCe.nsNRec))
                        nfe.NsNrec = retornoNFCe.nsNRec;
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta(retornoNFCe.cStat + " " + retornoNFCe.motivo);
                    return "";
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
