using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Contacts.DataProvider;
using static Lunar.Telas.Fiscal.FrmNfe;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Utils.OrganizacaoNF
{

    public class EmitirNfe3
    {
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
        FormaPagamento formaPagamentoAvulso = new FormaPagamento();
        decimal valorJaRecebido = 0;
        TNFeInfNFeCobrDup[] duplicatas = null;
        TNFeInfNFeCobrDup[] duplicatasAux = null;
        TNFe tNfe = new TNFe();
        TNFeInfNFeIde ide = new TNFeInfNFeIde();
        TNFeInfNFeEmit emit = new TNFeInfNFeEmit();
        TNFeInfNFeDest dest = new TNFeInfNFeDest();
        TratarGeracaoNFe tratarCampo = new TratarGeracaoNFe();
        TNFeInfNFeDet[] det = null;
        TNFeInfNFeTotal totais = new TNFeInfNFeTotal();
        TNFeInfNFeTransp frete = new TNFeInfNFeTransp();
        TNFeInfNFeCobr cobr = new TNFeInfNFeCobr();
        TNFeInfNFePag pag = new TNFeInfNFePag();
        Frete freteTransporte = new Frete();
        TNFeInfNFePagDetPag[] detPag = null;
        TNFeInfNFePagDetPag[] detPag2 = null;
        Nfe nfe = new Nfe();
        Venda venda = new Venda();
        OrdemServico ordemServico = new OrdemServico();
        TNFeInfNFeIdeNFref[] notasReferenciadas = null;
        public string gerarXML(Nfe nfe, Frete freteTransporte, bool gerarFinanceiro, Venda venda, OrdemServico ordemServico, IList<NfeProduto> listaProdutos, TNFeInfNFeIdeNFref[] notasReferenciadas, FormaPagamento formaPagamentoDiferente, bool notaAgrupada)
        {
            formaPagamentoAvulso = formaPagamentoDiferente;
            //Gravar produtos em NFeProdutos
            decimal bcICMS = 0;
            decimal valorICMS = 0;
            foreach (NfeProduto nfeProduto in listaProdutos)
            {
                nfeProduto.Nfe = nfe;
                nfeProduto.DataEntrada = nfe.DataLancamento;
                nfeProduto.UComConvertida = nfeProduto.UCom;
                nfeProduto.QuantidadeEntrada = double.Parse(nfeProduto.QCom);
                Controller.getInstance().salvar(nfeProduto);
                bcICMS = nfeProduto.VBC + bcICMS;
                valorICMS = nfeProduto.VICMS + valorICMS;
            }
            if (bcICMS > 0)
            {
                nfe.VBc = bcICMS;
                nfe.VIcms = valorICMS;
                Controller.getInstance().salvar(nfe);
            }

            this.nfe = nfe;
            this.freteTransporte = freteTransporte;
            this.venda = venda;
            this.ordemServico = ordemServico;
            tNfe = new TNFe();
            tNfe.infNFe = new TNFeInfNFe();
            if (notasReferenciadas != null)
            {
                if (notasReferenciadas.Length > 0)
                {
                    if (notasReferenciadas[0] != null)
                        this.notasReferenciadas = notasReferenciadas;
                }
            }


            //Cabecalho da Nota
            gerarCabecalho();
            tNfe.infNFe.ide = this.ide;

            //Emitente da Nota
            gerarEmitente();
            tNfe.infNFe.emit = this.emit;

            //Destinatário da Nota
            gerarDestinatario();
            tNfe.infNFe.dest = this.dest;

            //Produtos da Nota
            gerarProdutos();
            tNfe.infNFe.det = det;

            //Totais da Nota
            gerarTotais();
            tNfe.infNFe.total = totais;

            //Frete da Nota
            gerarFrete();
            tNfe.infNFe.transp = frete;

            //Pagamento da Nota
            tNfe.infNFe.pag = new TNFeInfNFePag();
            if (gerarFinanceiro == true && venda != null)
            {
                gerarPagamento();
            }
            else if (gerarFinanceiro == true && notaAgrupada == true)
            {
                gerarPagamentoNotaAgrupada(formaPagamentoAvulso);
            }
            else
                tNfe.infNFe.pag.detPag = retornaPagamentoNulo();

            tNfe.infNFe.infAdic = new TNFeInfNFeInfAdic();
            tNfe.infNFe.infRespTec = new TInfRespTec();
            tNfe.infNFe.infAdic.infCpl = nfe.InfCpl;
            tNfe.infNFe.infRespTec.CNPJ = "28145398000173";
            tNfe.infNFe.infRespTec.xContato = "MARCELO XAVIER";
            tNfe.infNFe.infRespTec.email = "marcelo.xs@hotmail.com";
            tNfe.infNFe.infRespTec.fone = "3836769644";

            // string xml = Genericos.NFCeToXML(tNfe);
            var novoArquivo = @"C:\XML\Tentativa\NFe\" + nfe.NNf +
                                      "-procNfe.xml";
            string xml = Genericos.ClasseParaArquivoXml(tNfe, novoArquivo);
            return xml;
        }

        private void gerarCabecalho()
        {
            tNfe.infNFe.versao = "4.00";
            ide.cUF = tratarCampo.Retornar_cUFFilialLogada();
            ide.natOp = nfe.NatOp;
            ide.mod = TMod.Item55;
            ide.serie = Sessao.parametroSistema.SerieNFe;
            ide.nNF = nfe.NNf;
            ide.dhEmi = DateTime.Now.ToString("s") + "-03:00";
            if (nfe.TipoOperacao.Equals("E"))
                ide.tpNF = TNFeInfNFeIdeTpNF.Item0;
            else
                ide.tpNF = TNFeInfNFeIdeTpNF.Item1;

            ide.idDest = TNFeInfNFeIdeIdDest.Item1;
            if (nfe.Cliente.EnderecoPrincipal.Cidade.Estado.Uf != nfe.EmpresaFilial.Endereco.Cidade.Estado.Uf)
                ide.idDest = TNFeInfNFeIdeIdDest.Item2;
            ide.cMunFG = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            ide.tpImp = TNFeInfNFeIdeTpImp.Item1;
            if(nfe.TpEmis.Equals("6"))
                ide.tpEmis = TNFeInfNFeIdeTpEmis.Item6;
            else
                ide.tpEmis = TNFeInfNFeIdeTpEmis.Item1;
            if (Sessao.parametroSistema.AmbienteProducao == true)
                ide.tpAmb = TAmb.Item1;
            else
                ide.tpAmb = TAmb.Item2;
            if (nfe.FinNfe.Equals("1"))
                ide.finNFe = TFinNFe.Item1;
            else if (nfe.FinNfe.Equals("2"))
                ide.finNFe = TFinNFe.Item2;
            else if (nfe.FinNfe.Equals("3"))
                ide.finNFe = TFinNFe.Item3;
            else if (nfe.FinNfe.Equals("4"))
                ide.finNFe = TFinNFe.Item4;

            ide.indFinal = TNFeInfNFeIdeIndFinal.Item1;

            if (nfe.IndPres.Equals("0"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item0;
            else if (nfe.IndPres.Equals("2"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item2;
            else if (nfe.IndPres.Equals("3"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item3;
            else if (nfe.IndPres.Equals("4"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item4;
            else if (nfe.IndPres.Equals("5"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item5;
            else if (nfe.IndPres.Equals("9"))
                ide.indPres = TNFeInfNFeIdeIndPres.Item9;

            ide.indIntermed = TNFeInfNFeIdeIndIntermed.Item0;
            ide.procEmi = TProcEmi.Item0;
            ide.verProc = "4.00";
            if (notasReferenciadas != null)
            {
                if (notasReferenciadas.Length > 0)
                    ide.NFref = notasReferenciadas;
            }
        }

        private void gerarEmitente()
        {
            emit.ItemElementName = ItemChoiceType2.CNPJ;
            emit.Item = Sessao.empresaFilialLogada.Cnpj;
            if (Sessao.parametroSistema.AmbienteProducao == true)
                emit.xNome = Sessao.empresaFilialLogada.RazaoSocial;
            else
                emit.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            emit.IE = Sessao.empresaFilialLogada.InscricaoEstadual;
            //Simples nacional
            emit.CRT = TNFeInfNFeEmitCRT.Item1;
            emit.enderEmit = retornarEnderecoEmitente();
        }
        private TEnderEmi retornarEnderecoEmitente()
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

        private void gerarDestinatario()
        {
            if (GenericaDesktop.RemoveCaracteres(nfe.CnpjDestinatario).Length == 11)
                dest.ItemElementName = ItemChoiceType3.CPF;
            else if (GenericaDesktop.RemoveCaracteres(nfe.CnpjDestinatario).Length == 14)
                dest.ItemElementName = ItemChoiceType3.CNPJ;
            dest.Item = GenericaDesktop.RemoveCaracteres(nfe.CnpjDestinatario);
            if (Sessao.parametroSistema.AmbienteProducao == true)
                dest.xNome = nfe.Cliente.RazaoSocial;
            else
                dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            if (!String.IsNullOrEmpty(nfe.Cliente.InscricaoEstadual) && !nfe.Cliente.InscricaoEstadual.Equals("ISENTO"))
            {
                dest.indIEDest = TNFeInfNFeDestIndIEDest.Item1;
                dest.IE = GenericaDesktop.RemoveCaracteres(nfe.Cliente.InscricaoEstadual);
            }
            else if (nfe.Cliente.InscricaoEstadual.Equals("ISENTO"))
                dest.indIEDest = TNFeInfNFeDestIndIEDest.Item2;
            else
                dest.indIEDest = TNFeInfNFeDestIndIEDest.Item9;
            dest.enderDest = new TEndereco();

            if (nfe.Cliente.EnderecoPrincipal != null)
            {
                dest.enderDest.xLgr = nfe.Cliente.EnderecoPrincipal.Logradouro;
                dest.enderDest.nro = nfe.Cliente.EnderecoPrincipal.Numero;
                dest.enderDest.xBairro = nfe.Cliente.EnderecoPrincipal.Bairro;
                dest.enderDest.cMun = nfe.Cliente.EnderecoPrincipal.Cidade.Ibge;
                dest.enderDest.xMun = nfe.Cliente.EnderecoPrincipal.Cidade.Descricao;
                dest.enderDest.UF = tratarCampo.Retornar_TUf(nfe.Cliente.EnderecoPrincipal.Cidade.Estado.Uf);
                dest.enderDest.CEP = (GenericaDesktop.RemoveCaracteres(nfe.Cliente.EnderecoPrincipal.Cep));
                dest.enderDest.cPais = "1058";
                dest.enderDest.xPais = "Brasil";
            }
            else
                throw new Exception("Destinatário sem endereço configurado!");
        }

        private void gerarProdutos()
        {
            string dadosVeiculo = "";
            NfeProdutoController nfeProdutoController = new NfeProdutoController();
            IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
            det = new TNFeInfNFeDet[listaProdutos.Count];
            int i = 0;
            int y = 0;
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
                det[y] = new TNFeInfNFeDet();
                det[y].nItem = i.ToString();
                det[y].prod = new TNFeInfNFeDetProd();
                det[y].prod.cEAN = "SEM GTIN";
                det[y].prod.cEANTrib = "SEM GTIN";
                det[y].prod.cProd = produto.Produto.Id.ToString();
                det[y].prod.xProd = descricaoProduto;
                det[y].prod.NCM = produto.Ncm;
                det[y].prod.CEST = cest;
                det[y].prod.CFOP = produto.Cfop;
                det[y].prod.uCom = produto.Produto.UnidadeMedida.Sigla;
                det[y].prod.qCom = formatMoedaNf(decimal.Parse(produto.QCom));
                det[y].prod.vUnCom = formatMoedaNf(produto.VUnCom);
                det[y].prod.vProd = formatMoedaNf(produto.VUnCom * decimal.Parse(produto.QCom));
                det[y].prod.uTrib = produto.Produto.UnidadeMedida.Sigla;
                det[y].prod.qTrib = formatMoedaNf(decimal.Parse(produto.QCom));
                if(produto.VDesc > 0)
                    det[y].prod.vDesc = formatMoedaNf(produto.VDesc);
                det[y].prod.vUnTrib = formatMoedaNf(produto.VUnCom);
                det[y].prod.indTot = TNFeInfNFeDetProdIndTot.Item1;
                
                //Informacao adicional item
                if(!String.IsNullOrEmpty(produto.InfAdProd))
                    det[y].infAdProd = produto.InfAdProd;

                //Veiculo ou Bicicleta Elétrica
                if (produto.Produto.Veiculo == true)
                {
                    TNFeInfNFeDetProdVeicProd[] veic = new TNFeInfNFeDetProdVeicProd[1];
                    veic[0] = new TNFeInfNFeDetProdVeicProd();
                    if (!String.IsNullOrEmpty(produto.Produto.AnoVeiculo))
                        veic[0].anoFab = produto.Produto.AnoVeiculo.Trim();
                    if (!String.IsNullOrEmpty(produto.Produto.ModeloVeiculo))
                        veic[0].anoMod = produto.Produto.ModeloVeiculo;
                    if (!String.IsNullOrEmpty(produto.Produto.CorMontadora))
                        veic[0].cCor = produto.Produto.CorMontadora.Substring(0, 2);
                    if (!String.IsNullOrEmpty(produto.Produto.CorDenatran))
                        veic[0].cCorDENATRAN = produto.Produto.CorDenatran.Substring(0, 2);
                    if(!String.IsNullOrEmpty(produto.Produto.Chassi))
                        veic[0].chassi = produto.Produto.Chassi;
                    if (!String.IsNullOrEmpty(produto.Produto.CilindradaCc))
                        veic[0].cilin = produto.Produto.CilindradaCc;
                    if (!String.IsNullOrEmpty(produto.Produto.MarcaModelo))
                        veic[0].cMod = produto.Produto.MarcaModelo;
                    if (!String.IsNullOrEmpty(produto.Produto.CapacidadeTracao))
                        veic[0].CMT = produto.Produto.CapacidadeTracao;
                    if (!String.IsNullOrEmpty(produto.Produto.CondicaoProduto)) 
                    { 
                        if (produto.Produto.CondicaoProduto.Substring(0, 1).Equals("1"))
                            veic[0].condVeic = TNFeInfNFeDetProdVeicProdCondVeic.Item1;
                        else if (produto.Produto.CondicaoProduto.Substring(0, 1).Equals("2"))
                            veic[0].condVeic = TNFeInfNFeDetProdVeicProdCondVeic.Item2;
                        else
                            veic[0].condVeic = TNFeInfNFeDetProdVeicProdCondVeic.Item3;
                    }
                    if (!String.IsNullOrEmpty(produto.Produto.DistanciaEixo))
                        veic[0].dist = produto.Produto.DistanciaEixo;
                    if(!String.IsNullOrEmpty(produto.Produto.EspecieVeiculo))
                        veic[0].espVeic = produto.Produto.EspecieVeiculo.Substring(1, 1);
                    if (!String.IsNullOrEmpty(produto.Produto.LotacaoVeiculo))
                        veic[0].lota = produto.Produto.LotacaoVeiculo;
                    if (!String.IsNullOrEmpty(produto.Produto.NumeroMotor))
                        veic[0].nMotor = produto.Produto.NumeroMotor;
                    veic[0].nSerie = "0";
                    if (!String.IsNullOrEmpty(produto.Produto.PesoBrutoVeiculo))
                        veic[0].pesoB = formatPeso(decimal.Parse(produto.Produto.PesoBrutoVeiculo));
                    if (!String.IsNullOrEmpty(produto.Produto.PesoLiquidoVeiculo))
                        veic[0].pesoL = formatPeso(decimal.Parse(produto.Produto.PesoLiquidoVeiculo));
                    if (!String.IsNullOrEmpty(produto.Produto.PotenciaCv))
                        veic[0].pot = produto.Produto.PotenciaCv;
                    if(!String.IsNullOrEmpty(produto.Produto.Combustivel))
                        veic[0].tpComb = produto.Produto.Combustivel.Substring(0, 2);
                    //venda concessionária
                    veic[0].tpOp = TNFeInfNFeDetProdVeicProdTpOp.Item0;
                    if (!String.IsNullOrEmpty(produto.Produto.TipoPintura))
                        veic[0].tpPint = produto.Produto.TipoPintura.Substring(0, 1);
                    if (!String.IsNullOrEmpty(produto.Produto.RestricaoVeiculo))
                    {
                        if (produto.Produto.RestricaoVeiculo.Substring(0, 1).Equals("0"))
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item0;
                        else if (produto.Produto.RestricaoVeiculo.Substring(0, 1).Equals("1"))
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item1;
                        else if (produto.Produto.RestricaoVeiculo.Substring(0, 1).Equals("2"))
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item2;
                        else if (produto.Produto.RestricaoVeiculo.Substring(0, 1).Equals("3"))
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item3;
                        else if (produto.Produto.RestricaoVeiculo.Substring(0, 1).Equals("4"))
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item4;
                        else
                            veic[0].tpRest = TNFeInfNFeDetProdVeicProdTpRest.Item9;
                    }
                    if (!String.IsNullOrEmpty(produto.Produto.TipoVeiculo))
                        veic[0].tpVeic = produto.Produto.TipoVeiculo.Substring(0, 2);
                    if (!String.IsNullOrEmpty(produto.Produto.CondicaoVeiculo))
                    {
                        if (produto.Produto.CondicaoVeiculo.Substring(0, 1).Equals("N"))
                            veic[0].VIN = TNFeInfNFeDetProdVeicProdVIN.N;
                        else if (produto.Produto.CondicaoVeiculo.Substring(0, 1).Equals("R"))
                            veic[0].VIN = TNFeInfNFeDetProdVeicProdVIN.R;
                        else
                            veic[0].VIN = TNFeInfNFeDetProdVeicProdVIN.N;
                    }
                    if (!String.IsNullOrEmpty(produto.Produto.CorMontadora))
                        veic[0].xCor = produto.Produto.CorMontadora;
                    det[y].prod.Items = veic;
                    //det[y].infAdProd = veic[0].ToString();

                    string pesoliQ = "0";
                    string pesoBru = "0";
                    if (!String.IsNullOrEmpty(produto.Produto.PesoLiquidoVeiculo))
                        pesoliQ = produto.Produto.PesoLiquidoVeiculo;
                    if (!String.IsNullOrEmpty(produto.Produto.PesoBrutoVeiculo))
                        pesoBru = produto.Produto.PesoBrutoVeiculo;
                    //Funcao pra sair por baixo do produto
                    int caract = 0;
                    if (veic[0].xCor != null)
                        caract = veic[0].xCor.Length;
                    string codCorMontadora = "";
                    if (!String.IsNullOrEmpty(produto.Produto.CorMontadora))
                        codCorMontadora = produto.Produto.CorMontadora.Substring(0, 2);
                    dadosVeiculo = "TIPO DA OPERAÇÃO: " + "0 - OUTROS" + " " +
                    "CHASSI: " + produto.Produto.Chassi + " " +
                    "CÓD. COR: " + codCorMontadora + " " +
                    "NOME COR: " + produto.Produto.CorMontadora + " " +
                    "POT. MOTOR: " + produto.Produto.PotenciaCv + " " +
                    "CC: " + produto.Produto.CilindradaCc + " " +
                    "PESO LIQ: " + formatPeso(decimal.Parse(pesoliQ)) + " " +
                    "PESO BRUTO: " + formatPeso(decimal.Parse(pesoBru)) + " " +
                    //"Nº DE SÉRIE: 0 \n " +
                    "COMBUSTÍVEL: " + produto.Produto.Combustivel + " " +
                    "Nº MOTOR: " + produto.Produto.NumeroMotor + " " +
                    "CAP. MÁX. TRAÇÃO: " + produto.Produto.CapacidadeTracao + " " +
                    "DIST. EIXOS: " + produto.Produto.DistanciaEixo + " " +
                    "ANO MODELO: " + produto.Produto.ModeloVeiculo + " " +
                    "ANO FABRICAÇÃO: " + produto.Produto.AnoVeiculo + " " +
                    "TIPO PINTURA: " + produto.Produto.TipoPintura + " " +
                    "TIPO VEÍCULO: " + produto.Produto.TipoVeiculo + " " +
                    "ESP. VEÍCULO: " + produto.Produto.EspecieVeiculo + " " +
                    "VIN: " + produto.Produto.CondicaoVeiculo + " " +
                    "COND. DO VEÍCULO: " + produto.Produto.CondicaoProduto + " " +
                    "CÓD. MARCA MODELO: " + produto.Produto.MarcaModelo + " " +
                    "CÓD COR DENATRAN: " + produto.Produto.CorDenatran + " " +
                    "CAP. MÁX. LOTAÇÃO: " + produto.Produto.LotacaoVeiculo + " " +
                    "REST.: " + produto.Produto.RestricaoVeiculo;

                    if (dadosVeiculo.Length > 500)
                        dadosVeiculo = GenericaDesktop.RemoveAcentos(GenericaDesktop.RemoveCaracteres(dadosVeiculo.Substring(0, 500)));
                }
               
                //Fim cadastro veiculo ou Bicicleta Elétrica

                //Observacoes Item
                //TNFeInfNFeDetObsItem obsItem = new TNFeInfNFeDetObsItem();

                if (produto.VFrete > 0)
                    det[y].prod.vFrete = valorFreteItem;
                if(produto.VOutro > 0)
                    det[y].prod.vOutro = valorOutro;
                if (produto.VSeguro > 0)
                    det[y].prod.vSeg = valorSeguro;
                if (!String.IsNullOrEmpty(dadosVeiculo) && produto.Produto.Veiculo == true)
                {
                    det[y].infAdProd = dadosVeiculo;
                }
                det[y].imposto = new TNFeInfNFeDetImposto();
                det[y].imposto.Items = geraImpostoICMS_IPI(produto);
                det[y].imposto.PIS = geraImpostoPIS(produto.Produto)[0];
                det[y].imposto.COFINS = geraImpostoCofins(produto.Produto)[0];
                if(produto.VipiDevolvido > 0)
                {
                    det[y].impostoDevol = new TNFeInfNFeDetImpostoDevol();
                    det[y].impostoDevol.pDevol = "100";
                    det[y].impostoDevol.IPI = new TNFeInfNFeDetImpostoDevolIPI();
                    det[y].impostoDevol.IPI.vIPIDevol = formatMoedaNf(produto.VipiDevolvido);
                    nfe.InfCpl = nfe.InfCpl + " Vr. IPI Devol. " + formatMoedaNf(produto.VipiDevolvido);
                }
                y++;
            }  
        }

        private void gerarTotais()
        {
            totais.ICMSTot = new TNFeInfNFeTotalICMSTot();
            totais.ICMSTot.vBC = formatMoedaNf(nfe.VBc);
            totais.ICMSTot.vICMS = formatMoedaNf(nfe.VIcms);
            totais.ICMSTot.vICMSDeson = formatMoedaNf(nfe.VIcmsDeson);
            totais.ICMSTot.vFCPUFDest = "0.00";
            totais.ICMSTot.vICMSUFDest = "0.00";
            totais.ICMSTot.vICMSUFRemet = "0.00";
            totais.ICMSTot.vFCP = formatMoedaNf(nfe.VFcp);
            totais.ICMSTot.vBCST = formatMoedaNf(nfe.VBcst);
            totais.ICMSTot.vST = formatMoedaNf(nfe.VSt);
            totais.ICMSTot.vFCPST = formatMoedaNf(nfe.VFcpst);
            totais.ICMSTot.vFCPSTRet = formatMoedaNf(nfe.VFcpstRet);
            totais.ICMSTot.vProd = formatMoedaNf(nfe.VProd);
            totais.ICMSTot.vFrete = formatMoedaNf(nfe.VFrete);
            totais.ICMSTot.vSeg = formatMoedaNf(nfe.VSeg);
            totais.ICMSTot.vDesc = formatMoedaNf(nfe.VDesc);
            totais.ICMSTot.vII = formatMoedaNf(nfe.VIi);
            totais.ICMSTot.vIPI = formatMoedaNf(nfe.VIpi);
            totais.ICMSTot.vIPIDevol = formatMoedaNf(nfe.VIpiDevol);
            totais.ICMSTot.vPIS = formatMoedaNf(nfe.VPis);
            totais.ICMSTot.vCOFINS = formatMoedaNf(nfe.VCofins);
            totais.ICMSTot.vOutro = formatMoedaNf(nfe.VOutro);
            totais.ICMSTot.vNF = formatMoedaNf(nfe.VNf);
            totais.ICMSTot.vTotTrib = formatMoedaNf(nfe.VTotTrib);
        }

        private void gerarFrete()
        {
            if(nfe.ModFrete.Equals("9"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item9;

            else if (nfe.ModFrete.Equals("0"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item0;
            else if (nfe.ModFrete.Equals("1"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item1;
            else if (nfe.ModFrete.Equals("2"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item2;
            else if (nfe.ModFrete.Equals("3"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item3;
            else if (nfe.ModFrete.Equals("4"))
                frete.modFrete = TNFeInfNFeTranspModFrete.Item4;

            if (freteTransporte.transportadora != null && !frete.modFrete.ToString().Equals("Item9"))
            {
                frete.transporta = new TNFeInfNFeTranspTransporta();
                frete.vol = new TNFeInfNFeTranspVol[1];

                frete.transporta.IE = GenericaDesktop.RemoveCaracteres(freteTransporte.transportadora.InscricaoEstadual.Trim());
                frete.transporta.xNome = freteTransporte.transportadora.RazaoSocial;
                if (freteTransporte.transportadora.Cnpj.Length == 14)
                {
                    frete.transporta.ItemElementName = ItemChoiceType6.CNPJ;
                    frete.transporta.Item = freteTransporte.transportadora.Cnpj.Trim();
                }
                else if (freteTransporte.transportadora.Cnpj.Length == 11)
                {
                    frete.transporta.ItemElementName = ItemChoiceType6.CPF;
                    frete.transporta.Item = freteTransporte.transportadora.Cnpj.Trim();
                }

                if (freteTransporte.transportadora.EnderecoPrincipal != null)
                {
                    frete.transporta.UF = tratarCampo.Retornar_TUf(freteTransporte.transportadora.EnderecoPrincipal.Cidade.Estado.Uf);
                    frete.transporta.UFSpecified = true;
                    frete.transporta.xEnder = freteTransporte.transportadora.EnderecoPrincipal.Logradouro + " " + freteTransporte.transportadora.EnderecoPrincipal.Numero;
                    frete.transporta.xMun = freteTransporte.transportadora.EnderecoPrincipal.Cidade.Descricao;
                }
                else
                    frete.transporta.UFSpecified = false;
            }
            if (!String.IsNullOrEmpty(freteTransporte.quantidadeVolume))
            {
                frete.vol[0] = new TNFeInfNFeTranspVol();
                if (!String.IsNullOrEmpty(freteTransporte.especie))
                    frete.vol[0].esp = freteTransporte.especie;
                if (!String.IsNullOrEmpty(freteTransporte.marca))
                    frete.vol[0].marca = freteTransporte.marca;
                if (!String.IsNullOrEmpty(freteTransporte.quantidadeVolume))
                    frete.vol[0].qVol = freteTransporte.quantidadeVolume;
                if (!String.IsNullOrEmpty(freteTransporte.pesoLiquido))
                    frete.vol[0].pesoL = formatPeso(decimal.Parse(freteTransporte.pesoLiquido));
                if (!String.IsNullOrEmpty(freteTransporte.pesoBruto))
                    frete.vol[0].pesoB = formatPeso(decimal.Parse(freteTransporte.pesoBruto));
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
        private void gerarPagamentoNotaAgrupada(FormaPagamento formaPagamento)
        {
            valorJaRecebido = 0;
            cobr = new TNFeInfNFeCobr();
            cobr.fat = new TNFeInfNFeCobrFat();
            pag = new TNFeInfNFePag();
            cobr.fat.nFat = nfe.NNf;
            cobr.fat.vOrig = formatMoedaNf(nfe.VProd);
            cobr.fat.vDesc = formatMoedaNf(nfe.VDesc);
            cobr.fat.vLiq = formatMoedaNf(nfe.VNf);
            cobr.dup = null;
            pag.vTroco = formatMoedaNf(0);
            pag.detPag = retornaPagamentoNFeNotaAgrupada(formaPagamento);
            tNfe.infNFe.cobr = cobr;
            tNfe.infNFe.pag = pag;
            tNfe.infNFe.pag.detPag = pag.detPag;
        }
        private void gerarPagamento()
        {
            valorJaRecebido = 0;
            if (venda != null)
            {
                if (venda.Id > 0)
                {
                    VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
                    IList<VendaFormaPagamento> listaFormaPag = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);
                    decimal troco = 0;
                    foreach (VendaFormaPagamento vfp in listaFormaPag)
                    {
                        if (vfp.Troco > 0)
                            troco = troco + vfp.Troco;
                        if (vfp.ValorRecebido > 0 && (vfp.FormaPagamento.Caixa == true || vfp.FormaPagamento.Cartao == true || vfp.FormaPagamento.CreditoCliente == true || vfp.FormaPagamento.Banco))
                            valorJaRecebido = valorJaRecebido + vfp.ValorRecebido;
                    }
                    cobr.fat.nFat = venda.Id.ToString();
                    cobr.fat.vOrig = formatMoedaNf(venda.ValorProdutos);
                    cobr.fat.vDesc = formatMoedaNf(venda.ValorDesconto);
                    cobr.fat.vLiq = formatMoedaNf(venda.ValorFinal);
                    cobr.dup = gerarDuplicatasVenda(venda);
                    pag.vTroco = formatMoedaNf(troco);
                    pag.detPag = retornaPagamentoNFe(listaFormaPag);
                }
            }
        }

        private TNFeInfNFePagDetPag[] retornaPagamentoNFeNotaAgrupada(FormaPagamento formaPagamento)
        {
            int i = 0;
            try
            {
                detPag = new TNFeInfNFePagDetPag[1];
                detPag2 = new TNFeInfNFePagDetPag[1];
                    i++;
                    string creditoDebito = "";
                    TNFeInfNFePagDetPagIndPag indPagamento;
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
                                vPag = formatMoedaNf(nfe.VNf),
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
                                vPag = formatMoedaNf(nfe.VNf),
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
                                vPag = formatMoedaNf(nfe.VNf)
                            }
                       };
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
        private TNFeInfNFePagDetPag[] retornaPagamentoNFe(IList<VendaFormaPagamento> listaPagamento)
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
        private TNFeInfNFePagDetPag[] retornaPagamentoNulo()
        {
            TNFeInfNFePagDetPag[] pg = new TNFeInfNFePagDetPag[1];
            TNFeInfNFePagDetPag pag = new TNFeInfNFePagDetPag();
            pag.tPag = "90";
            pag.vPag = formatMoedaNf(0);
            pg[0] = pag;
            return pg;
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











        private object[] geraImpostoICMS_IPI(NfeProduto nfeProduto)
    {
        var itemsList = new List<object>();
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
                CST = TIpiIPITribCST.Item99,
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

                //var grupoII = new TNFeInfNFeDetImpostoII();

                //// Preencha os dados do Grupo II conforme necessário
                //grupoII.vBC = "0.00";
                //grupoII.vDespAdu = "0.00";
                //grupoII.vII = "0.00";
                //grupoII.vIOF = "0.00";
                itemsList.Add(ipi);
                //itemsList.Add(grupoII);
            }

            //recebe os items 
            TNFeInfNFeDetImposto imposto = new TNFeInfNFeDetImposto();

            //    imposto.Items = Array.Empty<object>();
            //    imposto.Items = imposto.Items.Append(new TNFeInfNFeDetImpostoICMS[]
            //{
            //            icms[0]
            //}).ToArray();

            //    if (nfeProduto.ValorIpi > 0)
            //    {
            //        imposto.Items = imposto.Items.Append(new TIpi[]
            //    {
            //                ipi
            //    }).ToArray();
            //    }
           
     
            itemsList.Add(icms[0]);
            imposto.Items = itemsList.ToArray();

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
    }
}
