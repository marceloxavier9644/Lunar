//using ns_nfe_core.src.emissao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LunarBase.Utilidades.LunarNFe
{
    public class LunarNFe
    {
        public static async Task emitirNfe()
        {
            /*configParceiro.token = "VFhUIElORk9STUFUSUNBT3JQSEQ=";
            var NFeXML = LayoutNFe.gerarNFeXML();
            var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "XP", true, @"NFe/Documentos/");

            // Verifica se houve sucesso na emissão
            if (retorno.statusEnvio == "200" || retorno.statusEnvio == "-6" || retorno.statusEnvio == "-7")
            {
                string statusEnvio = retorno.statusEnvio;
                string nsNRec = retorno.nsNRec;

                // Verifica se houve sucesso na consulta
                if (retorno.statusConsulta == "200")
                {
                    string statusConsulta = retorno.statusConsulta;
                    string motivo = retorno.motivo;
                    string xMotivo = retorno.xMotivo;

                    // Verifica se a nota foi autorizada
                    if (retorno.cStat == "100" || retorno.cStat == "150")
                    {
                        // Documento autorizado com sucesso
                        string cStat = retorno.cStat;
                        string chNFe = retorno.chNFe;
                        string nProt = retorno.nProt;
                        string statusDownload = retorno.statusDownload;

                        if (retorno.statusDownload == "200")
                        {
                            // Verifica de houve sucesso ao realizar o download da NFe
                            string xml = retorno.xml;
                            string json = retorno.json;
                            string pdf = retorno.pdf;
                        }
                        else
                        {
                            // Aqui você pode realizar um tratamento em caso de erro no download
                            statusDownload = retorno.statusDownload;
                            dynamic erros = retorno.erros;
                        }
                    }
                    else
                    {
                        // NFe não foi autorizada com sucesso ou retorno diferente de 100 / 150
                        motivo = retorno.motivo;
                        xMotivo = retorno.xMotivo;
                        dynamic erros = retorno.erros;
                    }
                }
                else
                {
                    // Consulta não foi realizada com sucesso ou com retorno diferente de 200
                    string motivo = retorno.motivo;
                    string xMotivo = retorno.xMotivo;
                    dynamic erros = retorno.erros;
                }
            }
            else
            {
                // NFe não foi enviada com sucesso
                string statusEnvio = retorno.statusEnvio;
                string motivo = retorno.motivo;
                string xMotivo = retorno.xMotivo;
                dynamic erros = retorno.erros;
            }*/
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            NFeAux nota = new NFeAux();
            nota.infNFe = new NFeInfNFe();
            nota.infNFe.dest = new NFeInfNFeDest();
            nota.infNFe.emit = new NFeInfNFeEmit();
            nota.infNFe.ide = new NFeInfNFeIde();
            nota.infNFe.det = new NFeInfNFeDet();
            nota.infNFe.infAdic = new NFeInfNFeInfAdic();
            nota.infNFe.pag = new NFeInfNFePag();
            nota.infNFe.total = new NFeInfNFeTotal();
            nota.infNFe.transp = new NFeInfNFeTransp();
            nota.infNFe.versao = "4.00";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NFeAux));
            XmlDocument xml = new XmlDocument();


            string utf8;
            using (StringWriter writer = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(writer, nota);
                utf8 = writer.ToString();
            }

            xml.LoadXml(utf8);
            return xml;
        }

        public static async Task AutorizarNFe()
        {
            //NfeInutilizacao 4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeInutilizacao4
            //NfeConsultaProtocolo    4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4
            //NfeStatusServico    4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4
            //NfeConsultaCadastro 4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/CadConsultaCadastro4
            //RecepcaoEvento  4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRecepcaoEvento4
            //NFeAutorizacao  4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4
            //NFeRetAutorizacao   4.00    https://nfe.fazenda.mg.gov.br/nfe2/services/NFeRetAutorizacao4
            try
            {
                //Configurando o certificado
                //X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                //store.Open(OpenFlags.ReadWrite);
                //var certificado = store.Certificates.Find(X509FindType.FindByThumbprint, "14024ee0e26d5fea39d4929c7ceb64d963d385f7", true)[0];
                var certificado = new X509Certificate2(@"TXTInformatica.pfx", "1234");
                var urlService = "https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(urlService);

                XmlDocument soapEnvelopeXml = new XmlDocument();

                /* String soap = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                               "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"+
                               "<soap:Header>" +
                               "<nfeCabecMsg xmlns=\"http://www.portalfiscal.inf.br/nfe\">" +
                               "<versaoDados>4.00</versaoDados>" +
                               "</nfeCabecMsg>" +
                               "</soap:Header>" +
                               "<soap:Body>" +
                               "<nfeDadosMsg>" +
                               "<ide><cNF>111</cNF><cUF>43</cUF><natOp>NFE CST 00</natOp><mod>55</mod><serie>20</serie><nNF>191</nNF><dhEmi>2017-07-19T17:01:00</dhEmi><fusoHorario>-03:00</fusoHorario><tpNf>1</tpNf><idDest>1</idDest><indFinal>0</indFinal><indPres>0</indPres><cMunFg>4321808</cMunFg><tpImp>1</tpImp><tpEmis>1</tpEmis><tpAmb>2</tpAmb><finNFe>1</finNFe><EmailArquivos>email@gmail.com.br</EmailArquivos></ide><emit><CNPJ_emit>99999999999999</CNPJ_emit><xNome>Migrate</xNome><xFant>EMPRESADETESTE</xFant><IE>1234567891</IE><CRT>3</CRT><enderEmit><xLgr>RUAAGOSTINHOCOSTI</xLgr><nro>1700</nro><xCpl>ANDAR02</xCpl><xBairro>BARRADOJACARE</xBairro><cMun>4321808</cMun><xMun>ENCANTADO</xMun><UF>RS</UF><CEP>95960000</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>5137513322</fone><fax>3335354830</fax></enderEmit></emit><dest><CNPJ_dest>99999999999999</CNPJ_dest><xNome_dest>NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xNome_dest><indIEDest>2</indIEDest><enderDest><nro_dest>67</nro_dest><xCpl_dest>CENTRO</xCpl_dest><xBairro_dest>KENNEDY</xBairro_dest><xEmail_dest>email@gmail.com</xEmail_dest><xLgr_dest>ROD BR 040 KM 688 AREA ESPECIAL 11</xLgr_dest><xPais_dest>Brasil</xPais_dest><cMun_dest>4309605</cMun_dest><xMun_dest>CONTAGEM</xMun_dest><UF_dest>RS</UF_dest><CEP_dest>32145900</CEP_dest><cPais_dest>1058</cPais_dest><fone_dest>5535481105</fone_dest></enderDest></dest><autXML><autXMLItem><CNPJ_aut>99999999999999</CNPJ_aut></autXMLItem></autXML><det><detItem><prod><cProd>45093842304843</cProd><cEAN/><xProd>MOTOCICLETA MARCA HONDA, TIPO CB 300 R ANO DE FABRICACAO 2009 ANO MODELO 2010</xProd><NCM>87112020</NCM><EXTIPI>87</EXTIPI><CFOP>5102</CFOP><uCOM>BOT</uCOM><qCOM>1.0000</qCOM><vUnCom>9975.2100</vUnCom><vProd>9975.21</vProd><cEANTrib/><uTrib>BOT</uTrib><qTrib>1.00</qTrib><vUnTrib>9975.21</vUnTrib><vFrete>0.00</vFrete><vSeg>0.00</vSeg><vDesc>0.00</vDesc><indTot>1</indTot><nTipoItem>1</nTipoItem><nRECOPI>123456</nRECOPI><veicProd><tpOp>1</tpOp><chassi>9C2NC4310AR033234</chassi><cCor>PRME</cCor><xCor>PRATA METALICO</xCor><pot>2600</pot><cilin>0291</cilin><PesoL>000000143</PesoL><PesoB>000000175</PesoB><nSerie>0AR033234</nSerie><tpComb>02</tpComb><nMotor>NC43E1A033234</nMotor><CMT>00179.000</CMT><dist>1402</dist><anoMod>2010</anoMod><anoFab>2009</anoFab><tpPint>M</tpPint><tpVeic>04</tpVeic><espVeic>01</espVeic><VIN>N</VIN><condVeic>1</condVeic><cMod>2710</cMod><cCorDENATRAN>04</cCorDENATRAN><lota>2</lota><tpRest>0</tpRest></veicProd></prod><imposto><ICMS><orig>0</orig><CST>40</CST><modBC>0</modBC><vBC>0.00</vBC><pICMS>0.00</pICMS><vICMS_icms>0.00</vICMS_icms><pRedBC>0.00</pRedBC><pBCOp>0.00</pBCOp><pFCP>0.00</pFCP><vFCP>0.00</vFCP><vBCFCP>0.00</vBCFCP><vBCFCPST>0.00</vBCFCPST><pFCPST>0.00</pFCPST><vFCPST>0.00</vFCPST><pST>0.00</pST><vBCFCPSTRet>0.00</vBCFCPSTRet><pFCPSTRet>0.00</pFCPSTRet><vFCPSTRet>0.00</vFCPSTRet></ICMS><PIS><CST_pis>07</CST_pis></PIS><COFINS><CST_cofins>07</CST_cofins></COFINS></imposto><infADProd>COR PRATA METALICO GASOLINA, COM 26,5HP, 291 CC COM 0KM, PROCEDENCIA NACIONAL. |CHASSI: 9C2NC4310AR033234 MOTOR : NC43E1A033234 CLASS.FISCAL: 87113000 CODIGO RENAVAM: 2710 NFE:124933/1 de:22/09/2009</infADProd></detItem></det><total><ICMStot><vBC_ttlnfe>00.00</vBC_ttlnfe><vICMS_ttlnfe>00.00</vICMS_ttlnfe><vICMSDeson_ttlnfe>0.00</vICMSDeson_ttlnfe><vBCST_ttlnfe>0.00</vBCST_ttlnfe><vST_ttlnfe>0.00</vST_ttlnfe><vProd_ttlnfe>9975.21</vProd_ttlnfe><vFrete_ttlnfe>0</vFrete_ttlnfe><vSeg_ttlnfe>0</vSeg_ttlnfe><vDesc_ttlnfe>0</vDesc_ttlnfe><vII_ttlnfe>0.00</vII_ttlnfe><vIPI_ttlnfe>0</vIPI_ttlnfe><vPIS_ttlnfe>0</vPIS_ttlnfe><vCOFINS_ttlnfe>0</vCOFINS_ttlnfe><vOutro>0</vOutro><vNF>9975.21</vNF><vFCP_ttlnfe>0.00</vFCP_ttlnfe><vFCPST_ttlnfe>0.00</vFCPST_ttlnfe><vFCPSTRet_ttlnfe>0.00</vFCPSTRet_ttlnfe><vIPIDevol_ttlnfe>0.00</vIPIDevol_ttlnfe></ICMStot></total><transp><modFrete>9</modFrete><veicTransp><placa>IDN1565</placa><UF_veictransp>RS</UF_veictransp><RNTC>00811004</RNTC></veicTransp><reboque><reboqueItem><placa_rebtransp>CHI488</placa_rebtransp><UF_rebtransp>RS</UF_rebtransp><RNTC_rebtransp>00811004</RNTC_rebtransp></reboqueItem><reboqueItem><placa_rebtransp>CH4985</placa_rebtransp><UF_rebtransp>RS</UF_rebtransp><RNTC_rebtransp>00811004</RNTC_rebtransp></reboqueItem></reboque></transp><pag><pagItem><tPag>01</tPag><vPag>9975.21</vPag></pagItem></pag><vTroco>0.00</vTroco><infAdic><infAdFisco>INFORMACOES DE INTERESSE DO FISCO</infAdFisco><infCpl>7898080640413 7898904418150 7898117960019 7898904418150 7898080640413 7898117960019 7891107101621 7891091018011 7898909484129 7891024113783 7896387016139 7896041172539 7896213000677 7896213002091 7891010503024 4992 2284 2282 2300 2286 2320 2297 2314 2287 Maecenas ipsum velit, consectetuer eu, lobortis ut, dictum at, dui. In rutrum. Sed ac dolor sit amet purus malesuada congue. In laoreet, magna id viverra tincidunt, sem odio bibendum justo, vel imperdiet sapien wisi sed libero. Suspendisse sagittis ultrices augue. Mauris metus. Nunc dapibus tortor vel mi dapibus sollicitudin. Etiam posuere lacus quis dolor. Praesent id justo in neque elementum ultrices. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. In convallis. Fusce suscipit libero eget elit. Praesent vitae arcu tempor neque lacinia pretium. Morbi imperdiet, mauris ac auctor dictum, nisl ligula egestas nulla, et sollicitudin sem purus in lacus.</infCpl></infAdic>" +
                               "</nfeDadosMsg>" +
                               "</soap:Body>" +
                               "</soap:Envelope>";*/

                String soap = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <NFe xmlns=""http://www.portalfiscal.inf.br/nfe"">
        <infNFe Id=""NFe35150300822602000124550010009923461099234656"" versao=""4.00"">
            <ide>
                <cUF>35</cUF>
                <cNF>09923465</cNF>
                <natOp>Venda prod. do estab.</natOp>
                <indPag>1</indPag>
                <mod>55</mod>
                <serie>1</serie>
                <nNF>992346</nNF>
                <dhEmi>2015-03-27T09:40:00-03:00</dhEmi>
                <dhSaiEnt>2015-03-27T09:40:00-03:00</dhSaiEnt>
                <tpNF>1</tpNF>
                <idDest>1</idDest>
                <cMunFG>3550308</cMunFG>
                <tpImp>1</tpImp>
                <tpEmis>1</tpEmis>
                <cDV>6</cDV>
                <tpAmb>2</tpAmb>
                <finNFe>1</finNFe>
                <indFinal>1</indFinal>
                <indPres>3</indPres>
                <procEmi>3</procEmi>
                <verProc>3.10.43</verProc>
            </ide>
            <emit>
                <CNPJ>00822602000124</CNPJ>
                <xNome>Plotag Sistemas e Suprimentos Ltda</xNome>
                <xFant>Plotag - Localhost</xFant>
                <enderEmit>
                    <xLgr>Rua Solon</xLgr>
                    <nro>558</nro>
                    <xBairro>Bom Retiro</xBairro>
                    <cMun>3550308</cMun>
                    <xMun>Sao Paulo</xMun>
                    <UF>SP</UF>
                    <CEP>01127010</CEP>
                    <cPais>1058</cPais>
                    <xPais>BRASIL</xPais>
                    <fone>1123587604</fone>
                </enderEmit>
                <IE>114489114119</IE>
                <CRT>1</CRT>
            </emit>
            <dest>
                <CNPJ>99999999000191</CNPJ>
                <xNome>NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xNome>
                <enderDest>
                    <xLgr>Rua Jaragua</xLgr>
                    <nro>774</nro>
                    <xBairro>Bom Retiro</xBairro>
                    <cMun>3550308</cMun>
                    <xMun>Sao Paulo</xMun>
                    <UF>SP</UF>
                    <CEP>01129000</CEP>
                    <cPais>1058</cPais>
                    <xPais>BRASIL</xPais>
                    <fone>33933501</fone>
                </enderDest>
                <indIEDest>9</indIEDest>
                <email>gui_calabria@yahoo.com.br</email>
            </dest>
            <det nItem=""1"">
                <prod>
                    <cProd>B17025056</cProd>
                    <cEAN/>
                    <xProd>PAPEL MAXPLOT- 170MX250MX56GRS 3""</xProd>
                    <NCM>48025599</NCM>
                    <CFOP>5101</CFOP>
                    <uCom>Rl</uCom>
                    <qCom>1.0000</qCom>
                    <vUnCom>138.3000</vUnCom>
                    <vProd>138.30</vProd>
                    <cEANTrib/>
                    <uTrib>RL</uTrib>
                    <qTrib>1.0000</qTrib>
                    <vUnTrib>138.3000</vUnTrib>
                    <indTot>1</indTot>
                </prod>
                <imposto>
                    <vTotTrib>41.49</vTotTrib>
                    <ICMS>
                        <ICMSSN101>
                            <orig>0</orig>
                            <CSOSN>101</CSOSN>
                            <pCredSN>2.5600</pCredSN>
                            <vCredICMSSN>3.54</vCredICMSSN>
                        </ICMSSN101>
                    </ICMS>
                    <IPI>
                        <clEnq>48025</clEnq>
                        <CNPJProd>00822602000124</CNPJProd>
                        <cEnq>599</cEnq>
                        <IPINT>
                            <CST>53</CST>
                        </IPINT>
                    </IPI>
                    <PIS>
                        <PISNT>
                            <CST>07</CST>
                        </PISNT>
                    </PIS>
                    <COFINS>
                        <COFINSNT>
                            <CST>07</CST>
                        </COFINSNT>
                    </COFINS>
                </imposto>
            </det>
            <det nItem=""2"">
                <prod>
                    <cProd>1070100752</cProd>
                    <cEAN/>
                    <xProd>PAPEL MAXPLOT- 1070X100MX75GRS 2""</xProd>
                    <NCM>48025599</NCM>
                    <CFOP>5101</CFOP>
                    <uCom>RL</uCom>
                    <qCom>1.0000</qCom>
                    <vUnCom>48.9100</vUnCom>
                    <vProd>48.91</vProd>
                    <cEANTrib/>
                    <uTrib>RL</uTrib>
                    <qTrib>1.0000</qTrib>
                    <vUnTrib>48.9100</vUnTrib>
                    <indTot>1</indTot>
                </prod>
                <imposto>
                    <vTotTrib>14.67</vTotTrib>
                    <ICMS>
                        <ICMSSN101>
                            <orig>0</orig>
                            <CSOSN>101</CSOSN>
                            <pCredSN>2.5600</pCredSN>
                            <vCredICMSSN>1.25</vCredICMSSN>
                        </ICMSSN101>
                    </ICMS>
                    <IPI>
                        <clEnq>48025</clEnq>
                        <CNPJProd>00822602000124</CNPJProd>
                        <cEnq>599</cEnq>
                        <IPINT>
                            <CST>53</CST>
                        </IPINT>
                    </IPI>
                    <PIS>
                        <PISNT>
                            <CST>07</CST>
                        </PISNT>
                    </PIS>
                    <COFINS>
                        <COFINSNT>
                            <CST>07</CST>
                        </COFINSNT>
                    </COFINS>
                </imposto>
            </det>
            <det nItem=""3"">
                <prod>
                    <cProd>B17025056</cProd>
                    <cEAN/>
                    <xProd>PAPEL MAXPLOT- 170MX250MX56GRS 3""</xProd>
                    <NCM>48025599</NCM>
                    <CFOP>5101</CFOP>
                    <uCom>Rl</uCom>
                    <qCom>1.0000</qCom>
                    <vUnCom>138.3000</vUnCom>
                    <vProd>138.30</vProd>
                    <cEANTrib/>
                    <uTrib>RL</uTrib>
                    <qTrib>1.0000</qTrib>
                    <vUnTrib>138.3000</vUnTrib>
                    <indTot>1</indTot>
                </prod>
                <imposto>
                    <vTotTrib>41.49</vTotTrib>
                    <ICMS>
                        <ICMSSN101>
                            <orig>0</orig>
                            <CSOSN>101</CSOSN>
                            <pCredSN>2.5600</pCredSN>
                            <vCredICMSSN>3.54</vCredICMSSN>
                        </ICMSSN101>
                    </ICMS>
                    <IPI>
                        <clEnq>48025</clEnq>
                        <CNPJProd>00822602000124</CNPJProd>
                        <cEnq>599</cEnq>
                        <IPINT>
                            <CST>53</CST>
                        </IPINT>
                    </IPI>
                    <PIS>
                        <PISNT>
                            <CST>07</CST>
                        </PISNT>
                    </PIS>
                    <COFINS>
                        <COFINSNT>
                            <CST>07</CST>
                        </COFINSNT>
                    </COFINS>
                </imposto>
            </det>
            <det nItem=""4"">
                <prod>
                    <cProd>B17040056</cProd>
                    <cEAN/>
                    <xProd>PAPEL MAXPLOT - 1.700X400MX 56 GRS 3""</xProd>
                    <NCM>48025599</NCM>
                    <CFOP>5101</CFOP>
                    <uCom>Rl</uCom>
                    <qCom>1.0000</qCom>
                    <vUnCom>214.5700</vUnCom>
                    <vProd>214.57</vProd>
                    <cEANTrib/>
                    <uTrib>Rl</uTrib>
                    <qTrib>1.0000</qTrib>
                    <vUnTrib>214.5700</vUnTrib>
                    <indTot>1</indTot>
                </prod>
                <imposto>
                    <vTotTrib>64.37</vTotTrib>
                    <ICMS>
                        <ICMSSN101>
                            <orig>0</orig>
                            <CSOSN>101</CSOSN>
                            <pCredSN>2.5600</pCredSN>
                            <vCredICMSSN>5.49</vCredICMSSN>
                        </ICMSSN101>
                    </ICMS>
                    <IPI>
                        <clEnq>48025</clEnq>
                        <CNPJProd>00822602000124</CNPJProd>
                        <cEnq>599</cEnq>
                        <IPINT>
                            <CST>53</CST>
                        </IPINT>
                    </IPI>
                    <PIS>
                        <PISNT>
                            <CST>07</CST>
                        </PISNT>
                    </PIS>
                    <COFINS>
                        <COFINSNT>
                            <CST>07</CST>
                        </COFINSNT>
                    </COFINS>
                </imposto>
            </det>
            <det nItem=""5"">
                <prod>
                    <cProd>B18525056</cProd>
                    <cEAN/>
                    <xProd>PAPEL MAXPLOT-1.85MX250MX56GRS 3""</xProd>
                    <NCM>48025599</NCM>
                    <CFOP>5101</CFOP>
                    <uCom>Rl</uCom>
                    <qCom>1.0000</qCom>
                    <vUnCom>149.8300</vUnCom>
                    <vProd>149.83</vProd>
                    <cEANTrib/>
                    <uTrib>RL</uTrib>
                    <qTrib>1.0000</qTrib>
                    <vUnTrib>149.8300</vUnTrib>
                    <indTot>1</indTot>
                </prod>
                <imposto>
                    <vTotTrib>44.95</vTotTrib>
                    <ICMS>
                        <ICMSSN101>
                            <orig>0</orig>
                            <CSOSN>101</CSOSN>
                            <pCredSN>2.5600</pCredSN>
                            <vCredICMSSN>3.84</vCredICMSSN>
                        </ICMSSN101>
                    </ICMS>
                    <IPI>
                        <clEnq>48025</clEnq>
                        <CNPJProd>00822602000124</CNPJProd>
                        <cEnq>599</cEnq>
                        <IPINT>
                            <CST>53</CST>
                        </IPINT>
                    </IPI>
                    <PIS>
                        <PISNT>
                            <CST>07</CST>
                        </PISNT>
                    </PIS>
                    <COFINS>
                        <COFINSNT>
                            <CST>07</CST>
                        </COFINSNT>
                    </COFINS>
                </imposto>
            </det>
            <total>
                <ICMSTot>
                    <vBC>0.00</vBC>
                    <vICMS>0.00</vICMS>
                    <vICMSDeson>0.00</vICMSDeson>
                    <vBCST>0.00</vBCST>
                    <vST>0.00</vST>
                    <vProd>689.91</vProd>
                    <vFrete>0.00</vFrete>
                    <vSeg>0.00</vSeg>
                    <vDesc>0.00</vDesc>
                    <vII>0.00</vII>
                    <vIPI>0.00</vIPI>
                    <vPIS>0.00</vPIS>
                    <vCOFINS>0.00</vCOFINS>
                    <vOutro>0.00</vOutro>
                    <vNF>689.91</vNF>
                    <vTotTrib>206.97</vTotTrib>
                </ICMSTot>
            </total>
            <transp>
                <modFrete>1</modFrete>
                <transporta>
                    <xNome>Cliente Retira</xNome>
                    <xEnder>Rua ,</xEnder>
                    <xMun>Sao Paulo</xMun>
                    <UF>SP</UF>
                </transporta>
                <vol>
                    <qVol>1</qVol>
                    <marca>S/m</marca>
                    <nVol>S/n</nVol>
                    <pesoL>0.000</pesoL>
                    <pesoB>0.000</pesoB>
                </vol>
            </transp>
            <cobr>
                <fat>
                    <nFat>992346</nFat>
                    <vOrig>689.91</vOrig>
                    <vLiq>689.91</vLiq>
                </fat>
                <dup>
                    <nDup>992346</nDup>
                    <dVenc>2015-04-24</dVenc>
                    <vDup>689.91</vDup>
                </dup>
            </cobr>
            <infAdic>
                <infCpl>""DOCUMENTO EMITIDO POR EMPRESA OPTANTE PELO SIMPLES NACIONAL;NAO GERA DIREITO A CREDITO FISCAL DE IPI"";""PERMITE O APROVEITAMENTO DE CREDITO DE ICMS NO VALOR DE: R$17,66 CORRESPONDENTE A ALIQUOTA DE 2.56%"";Vendedor:1 - Guilherme Kavedikado;Valor Aproximado dos Tributos : R$ 206,97. Fonte IBPT (Instituto Brasileiro de Planejamento Tributario)</infCpl>
            </infAdic>
        </infNFe>

</NFe>";
                  soapEnvelopeXml.LoadXml(soap);
                //webRequest.Headers.Add("SOAPAction", action);
                webRequest.ContentType = "text/xml; charset=UTF-8";
                webRequest.Method = "POST";
                webRequest.ClientCertificates.Add(certificado);

                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }


                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();

                string soapResult;
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }

                    System.IO.StreamWriter file = new System.IO.StreamWriter(@"NFe/Logs/xml.txt", true);
                    file.WriteLine(DateTime.Now.ToString() + ": " + soapResult);
                    file.Close();
                }


                /*
                #region Gerando XML Autorização
                XmlDocument xmlNFe = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlNFe.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlNFe.DocumentElement;
                xmlNFe.InsertBefore(xmlDeclaration, root);

                //NFe
                XmlElement elementNFe = xmlNFe.CreateElement("NFe");
                elementNFe.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                elementNFe.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                elementNFe.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe");
                elementNFe.SetAttribute("versao", "4.00");

                //infNFe
                XmlElement elementInfNFe = xmlNFe.CreateElement("infNFe");
                //elementInfNFe.SetAttribute("versao", "4.00");

                //ide
                XmlElement elementIde = xmlNFe.CreateElement("ide");

                    XmlElement elementIde_cUF = xmlNFe.CreateElement("cUF");
                    elementIde_cUF.InnerText = "31";
                    elementIde.AppendChild(elementIde_cUF);

                    XmlElement elementIde_cNF = xmlNFe.CreateElement("cNF");
                    elementIde.AppendChild(elementIde_cNF);

                    XmlElement elementIde_natOp = xmlNFe.CreateElement("natOp");
                    elementIde_natOp.InnerText = "VENDA A PRAZO - SEM VALOR FISCAL";
                    elementIde.AppendChild(elementIde_natOp);

                    XmlElement elementIde_mod = xmlNFe.CreateElement("mod");
                    elementIde_mod.InnerText = "55";
                    elementIde.AppendChild(elementIde_mod);

                    XmlElement elementIde_serie = xmlNFe.CreateElement("serie");
                    elementIde_serie.InnerText = "0";
                    elementIde.AppendChild(elementIde_serie);

                    XmlElement elementIde_nNF = xmlNFe.CreateElement("nNF");
                    elementIde_nNF.InnerText = "22528";
                    elementIde.AppendChild(elementIde_nNF);

                    XmlElement elementIde_dhEmi = xmlNFe.CreateElement("dhEmi");
                    elementIde_dhEmi.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")+"-03:00";
                    elementIde.AppendChild(elementIde_dhEmi);

                    XmlElement elementIde_tpNF = xmlNFe.CreateElement("tpNF");
                    elementIde_tpNF.InnerText = "1";
                    elementIde.AppendChild(elementIde_tpNF);

                    XmlElement elementIde_idDest = xmlNFe.CreateElement("idDest");
                    elementIde_idDest.InnerText = "1";
                    elementIde.AppendChild(elementIde_idDest);

                    XmlElement elementIde_cMunFG = xmlNFe.CreateElement("cMunFG");
                    elementIde_cMunFG.InnerText = "3170404";
                    elementIde.AppendChild(elementIde_cMunFG);

                    XmlElement elementIde_tpImp = xmlNFe.CreateElement("tpImp");
                    elementIde_tpImp.InnerText = "1";
                    elementIde.AppendChild(elementIde_tpImp);

                    XmlElement elementIde_tpEmis = xmlNFe.CreateElement("tpEmis");
                    elementIde_tpEmis.InnerText = "1";
                    elementIde.AppendChild(elementIde_tpEmis);

                    XmlElement elementIde_cDV = xmlNFe.CreateElement("cDV");
                    elementIde.AppendChild(elementIde_cDV);

                    //1 - Produção     2 - Homologação
                    XmlElement elementIde_tpAmb = xmlNFe.CreateElement("tpAmb");
                    elementIde_tpAmb.InnerText = "2";
                    elementIde.AppendChild(elementIde_tpAmb);

                    XmlElement elementIde_finNFe = xmlNFe.CreateElement("finNFe");
                    elementIde_finNFe.InnerText = "1";
                    elementIde.AppendChild(elementIde_finNFe);

                    XmlElement elementIde_indFinal = xmlNFe.CreateElement("indFinal");
                    elementIde_indFinal.InnerText = "0";
                    elementIde.AppendChild(elementIde_indFinal);

                    XmlElement elementIde_indPres = xmlNFe.CreateElement("indPres");
                    elementIde_indPres.InnerText = "1";
                    elementIde.AppendChild(elementIde_indPres);

                    XmlElement elementIde_procEmi = xmlNFe.CreateElement("procEmi");
                    elementIde_procEmi.InnerText = "0";
                    elementIde.AppendChild(elementIde_procEmi);

                    XmlElement elementIde_verProc = xmlNFe.CreateElement("verProc");
                    elementIde_verProc.InnerText = "4.00";
                    elementIde.AppendChild(elementIde_verProc);


                //emit
                XmlElement elementEmit = xmlNFe.CreateElement("emit");

                    XmlElement elementEmit_CNPJ = xmlNFe.CreateElement("CNPJ");
                    elementEmit_CNPJ.InnerText = "28145398000173";
                    elementEmit.AppendChild(elementEmit_CNPJ);

                    XmlElement elementEmit_xNome = xmlNFe.CreateElement("xNome");
                    elementEmit_xNome.InnerText = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                    elementEmit.AppendChild(elementEmit_xNome);

                    XmlElement elementEnderEmit = xmlNFe.CreateElement("enderEmit");

                        XmlElement elementEnderEmit_xLgr = xmlNFe.CreateElement("xLgr");
                        elementEnderEmit_xLgr.InnerText = "Rua Canabrava";
                        elementEnderEmit.AppendChild(elementEnderEmit_xLgr);

                        XmlElement elementEnderEmit_nro = xmlNFe.CreateElement("nro");
                        elementEnderEmit_nro.InnerText = "777";
                        elementEnderEmit.AppendChild(elementEnderEmit_nro);

                        XmlElement elementEnderEmit_xCpl = xmlNFe.CreateElement("xCpl");
                        elementEnderEmit_xCpl.InnerText = "Complemento";
                        elementEnderEmit.AppendChild(elementEnderEmit_xCpl);

                        XmlElement elementEnderEmit_xBairro = xmlNFe.CreateElement("xBairro");
                        elementEnderEmit_xBairro.InnerText = "Centro";
                        elementEnderEmit.AppendChild(elementEnderEmit_xBairro);

                        XmlElement elementEnderEmit_cMun = xmlNFe.CreateElement("cMun");
                        elementEnderEmit_cMun.InnerText = "3170404";
                        elementEnderEmit.AppendChild(elementEnderEmit_cMun);

                        XmlElement elementEnderEmit_xMun = xmlNFe.CreateElement("xMun");
                        elementEnderEmit_xMun.InnerText = "UNAÍ";
                        elementEnderEmit.AppendChild(elementEnderEmit_xMun);

                        XmlElement elementEnderEmit_UF = xmlNFe.CreateElement("UF");
                        elementEnderEmit_UF.InnerText = "MG";
                        elementEnderEmit.AppendChild(elementEnderEmit_UF);

                        XmlElement elementEnderEmit_CEP = xmlNFe.CreateElement("CEP");
                        elementEnderEmit_CEP.InnerText = "38610031";
                        elementEnderEmit.AppendChild(elementEnderEmit_CEP);

                        XmlElement elementEnderEmit_fone = xmlNFe.CreateElement("fone");
                        elementEnderEmit_fone.InnerText = "3836776224";
                        elementEnderEmit.AppendChild(elementEnderEmit_fone);

                elementEmit.AppendChild(elementEnderEmit);

                XmlElement elementEmit_IE = xmlNFe.CreateElement("IE");
                elementEmit_IE.InnerText = "0030011730013";
                elementEmit.AppendChild(elementEmit_IE);

                XmlElement elementEmit_CRT = xmlNFe.CreateElement("CRT");
                elementEmit_CRT.InnerText = "1";
                elementEmit.AppendChild(elementEmit_CRT);


                //dest
                XmlElement elementDest = xmlNFe.CreateElement("dest");

                    XmlElement elementDest_CNPJ = xmlNFe.CreateElement("CNPJ");
                    elementDest_CNPJ.InnerText = "28145398000173";
                    elementDest.AppendChild(elementDest_CNPJ);

                    XmlElement elementDest_xNome = xmlNFe.CreateElement("xNome");
                    elementDest_xNome.InnerText = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
                    elementDest.AppendChild(elementDest_xNome);

                    XmlElement elementEnderDest = xmlNFe.CreateElement("enderDest");

                        XmlElement elementEnderDest_xLgr = xmlNFe.CreateElement("xLgr");
                        elementEnderDest_xLgr.InnerText = "Rua Canabrava";
                        elementEnderDest.AppendChild(elementEnderDest_xLgr);

                        XmlElement elementEnderDest_nro = xmlNFe.CreateElement("nro");
                        elementEnderDest_nro.InnerText = "0";
                        elementEnderDest.AppendChild(elementEnderDest_nro);

                        XmlElement elementEnderDest_xBairro = xmlNFe.CreateElement("xBairro");
                        elementEnderDest_xBairro.InnerText = "Centro";
                        elementEnderDest.AppendChild(elementEnderDest_xBairro);

                        XmlElement elementEnderDest_cMun = xmlNFe.CreateElement("cMun");
                        elementEnderDest_cMun.InnerText = "3170404";
                        elementEnderDest.AppendChild(elementEnderDest_cMun);

                        XmlElement elementEnderDest_xMun = xmlNFe.CreateElement("xMun");
                        elementEnderDest_xMun.InnerText = "UNAÍ";
                        elementEnderDest.AppendChild(elementEnderDest_xMun);

                        XmlElement elementEnderDest_UF = xmlNFe.CreateElement("UF");
                        elementEnderDest_UF.InnerText = "MG";
                        elementEnderDest.AppendChild(elementEnderDest_UF);

                        XmlElement elementEnderDest_CEP = xmlNFe.CreateElement("CEP");
                        elementEnderDest_CEP.InnerText = "38610031";
                        elementEnderDest.AppendChild(elementEnderDest_CEP);

                        XmlElement elementEnderDest_cPais = xmlNFe.CreateElement("cPais");
                        elementEnderDest_cPais.InnerText = "1058";
                        elementEnderDest.AppendChild(elementEnderDest_cPais);

                        XmlElement elementEnderDest_xPais = xmlNFe.CreateElement("xPais");
                        elementEnderDest_xPais.InnerText = "BRASIL";
                        elementEnderDest.AppendChild(elementEnderDest_xPais);

                    elementDest.AppendChild(elementEnderDest);

                    XmlElement elementDest_indIEDest = xmlNFe.CreateElement("indIEDest");
                    elementDest_indIEDest.InnerText = "1";
                    elementDest.AppendChild(elementDest_indIEDest);

                    XmlElement elementDest_IE = xmlNFe.CreateElement("IE");
                    elementDest_IE.InnerText = "0030011730013";
                    elementDest.AppendChild(elementDest_IE);





                elementInfNFe.AppendChild(elementIde);
                elementInfNFe.AppendChild(elementEmit);
                elementInfNFe.AppendChild(elementDest);
                elementNFe.AppendChild(elementInfNFe);
                xmlNFe.AppendChild(elementNFe);

                //Log do XML da Nota
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"NFe/Logs/xml.txt", true);
                file.WriteLine(DateTime.Now.ToString() + ": " + xmlNFe.OuterXml.ToString());
                file.Close();
                #endregion
                */
                //"<?xml version="1.0" encoding="utf - 16"?>
                //<NFe xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.portalfiscal.inf.br/nfe">
                //  <infNFe versao="4.00">
                //      <det nItem="1">
                //          <prod>
                //              <cProd>123456789</cProd>
                //              <cEAN>SEM GTIN</cEAN>
                //              <xProd>COCA-COLA LT 250ML</xProd>
                //              <NCM>22021000</NCM>
                //              <CEST>0301100</CEST>
                //              <CFOP>5101</CFOP>
                //              <uCom>UN</uCom>
                //              <qCom>1.0000</qCom>
                //              <vUnCom>3.00</vUnCom>
                //              <vProd>3.00</vProd>
                //              <cEANTrib>SEM GTIN</cEANTrib>
                //              <uTrib>UN</uTrib>
                //              <qTrib>1.0000</qTrib>
                //              <vUnTrib>3.00</vUnTrib>
                //              <indTot>1</indTot>
                //              <nItemPed>0</nItemPed>
                //          </prod>
                //          <imposto>
                //              <vTotTrib>0.00</vTotTrib>
                //              <ICMS><ICMSSN102>
                //              <orig>0</orig>
                //              <CSOSN>102</CSOSN>
                //              </ICMSSN102>
                //              </ICMS>
                //              <PIS>
                //                  <PISAliq>
                //                      <CST>01</CST>
                //                      <vBC>3.00</vBC>
                //                      <pPIS>1.65</pPIS>
                //                      <vPIS>0.05</vPIS>
                //                  </PISAliq>
                //              </PIS>
                //              <COFINS>
                //                  <COFINSAliq>
                //                      <CST>01</CST>
                //                      <vBC>3.00</vBC>
                //                      <pCOFINS>7.00</pCOFINS>
                //                      <vCOFINS>0.21</vCOFINS>
                //                  </COFINSAliq>
                //              </COFINS>
                //          </imposto>
                //      </det>
                //      <total>
                //          <ICMSTot>
                //              <vBC>0</vBC>
                //              <vICMS>0</vICMS>
                //              <vICMSDeson>0.00</vICMSDeson>
                //              <vFCPUFDest>0.00</vFCPUFDest>
                //              <vICMSUFDest>0.00</vICMSUFDest>
                //              <vICMSUFRemet>0.00</vICMSUFRemet>
                //              <vFCP>0</vFCP>
                //              <vBCST>0</vBCST>
                //              <vST>0</vST>
                //              <vFCPST>0</vFCPST>
                //              <vFCPSTRet>0.00</vFCPSTRet>
                //              <vProd>3.00</vProd>
                //              <vFrete>0.00</vFrete>
                //              <vSeg>0.00</vSeg>
                //              <vDesc>0.00</vDesc>
                //              <vII>0.00</vII>
                //              <vIPI>0.00</vIPI>
                //              <vIPIDevol>0.00</vIPIDevol>
                //              <vPIS>0.05</vPIS>
                //              <vCOFINS>0.21</vCOFINS>
                //              <vOutro>0.00</vOutro>
                //              <vNF>3.00</vNF>
                //              <vTotTrib>0.00</vTotTrib>
                //          </ICMSTot>
                //      </total>
                //      <transp>
                //          <modFrete>9</modFrete>
                //      </transp>
                //      <pag>
                //          <detPag>
                //              <tPag>16</tPag>
                //              <vPag>5.00</vPag>
                //          </detPag>
                //          <vTroco>2.00</vTroco>
                //      </pag>
                //      <infAdic>
                //          <infCpl>TESTE DE EMISSAO UTILIZANDO ESTRUTURA XSD</infCpl>
                //      </infAdic>
                //   </infNFe>
                //</NFe>"

                /*
                #region Adicionando o XML à requisição
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeAutorizacao4");
                request.ClientCertificates.Add(certificado);
                byte[] bytes = Encoding.UTF8.GetBytes(xmlNFe.OuterXml.ToString());
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "application/xml";
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
                #endregion

                #region Realizando a solicitação de autorização da NFe
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamResponse = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(streamResponse,System.Text.Encoding.ASCII);
                var y = responseReader.ReadToEnd();

                var x = 1;
                #endregion
                */
            }
            catch (Exception e){
                var erro = e.Message;
                var y = 2;
            }

        }
    }
}
