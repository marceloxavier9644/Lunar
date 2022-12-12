using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

public class Genericos
{
    public static void salvarXML(string xml, string caminho, string nome, string tpEvento = "", string nSeqEvento = "")
    {
        string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + ".xml";
        string ConteudoSalvar = "";
        ConteudoSalvar = xml.Replace(@"\""", "");
        File.WriteAllText(localParaSalvar, ConteudoSalvar);
    }

    //transforma objeto xml em string xml
    public static string NFCeToXML(object NFCe)
    {
        //using (var stringwriter = new StringWriter())
        //{
        //    var serializer = new XmlSerializer(NFCe.GetType());
        //    serializer.Serialize(stringwriter, NFCe);
        //    return stringwriter.ToString();
        //}
        var serializer = new XmlSerializer(typeof(TNFe));
        var memorystream = new MemoryStream();
        Encoding utf8WithoutBom = new UTF8Encoding(false);
        var streamwriter = new StreamWriter(memorystream, utf8WithoutBom);
        serializer.Serialize(streamwriter, NFCe);
        memorystream.Seek(0, SeekOrigin.Begin);
        var streamreader = new StreamReader(memorystream, System.Text.Encoding.UTF8);
        var utf8encodedxml = streamreader.ReadToEnd();
        return utf8encodedxml;
    }

    public static T LoadFromXMLString<T>(string xmlText)
    {
        var stringReader = new System.IO.StringReader(xmlText);
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(stringReader);
    }
    public static void salvarJSON(string json, string caminho, string nome, string tpEvento = "", string nSeqEvento = "")
    {
        string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + ".json";
        File.WriteAllText(localParaSalvar, json);
    }

    public static void salvarPDF(string pdf, string caminho, string nome, string tpEvento = "", string nSeqEvento = "")
    {
        string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + ".pdf";
        byte[] bytes = Convert.FromBase64String(pdf);
        if (File.Exists(localParaSalvar))
            File.Delete(localParaSalvar);
        FileStream stream = new FileStream(localParaSalvar, FileMode.CreateNew);
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(bytes, 0, bytes.Length);
        writer.Close();
    }

    public static void gravarLinhaLog(string modelo, string conteudo)
    {
        string caminho = @".\log\";
        Console.Write(caminho);

        if (!Directory.Exists(caminho))
            Directory.CreateDirectory(caminho);

        string data = DateTime.Now.ToShortDateString();
        string hora = DateTime.Now.ToShortTimeString();
        string nomeArq = data.Replace("/", "") + "_" + modelo;

        using (StreamWriter outputFile = new StreamWriter(caminho + nomeArq + ".txt", true))
        {
            outputFile.WriteLine(data + " " + hora + " - " + conteudo);
        }
    }
    public static X509Certificate2 buscaCertificado(String cnpj)
    {
        X509Certificate2Collection lcerts;
        X509Store lStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

        lStore.Open(OpenFlags.ReadOnly);

        lcerts = lStore.Certificates;
        X509Certificate2 cert = null;
        foreach (X509Certificate2 elemento in lcerts)
        {
            if (elemento.Subject.Contains(cnpj))
            {
                cert = elemento;
                lStore.Close();
                return cert;
            }
        }
        lStore.Close();
        return cert;
    }

    public static string assinaXML(string XMLString, string RefUri, X509Certificate2 X509Cert)
    {
        XmlDocument XMLDoc;
        try
        {

            string _xnome = "";

            if (X509Cert != null)
            {
                _xnome = X509Cert.Subject.ToString();
            }

            X509Certificate2 _X509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);

            if (collection1.Count == 0)
            {
                throw new Exception("Problemas no certificado digital");
            }
            else
            {
                _X509Cert = collection1[0];
                string x;
                x = _X509Cert.GetKeyAlgorithm().ToString();
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;

                try
                {
                    doc.LoadXml(XMLString);
                    int qtdeRefUri = doc.GetElementsByTagName(RefUri).Count;

                    if (qtdeRefUri == 0)
                    {
                        throw new Exception("A tag de assinatura " + RefUri.Trim() + " inexiste");
                    }
                    else
                    {
                        if (qtdeRefUri > 1)
                        {
                            throw new Exception("A tag de assinatura " + RefUri.Trim() + " não é unica");
                        }
                        else
                        {
                            try
                            {

                                SignedXml signedXml = new SignedXml(doc);

                                signedXml.SigningKey = _X509Cert.PrivateKey;

                                Reference reference = new Reference();

                                XmlAttributeCollection _Uri = doc.GetElementsByTagName(RefUri).Item(0).Attributes;
                                foreach (XmlAttribute _atributo in _Uri)
                                {
                                    if (_atributo.Name == "Id")
                                    {
                                        reference.Uri = "#NFe" + _atributo.InnerText;
                                        reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
                                    }
                                }

                                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                reference.AddTransform(env);

                                XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                reference.AddTransform(c14);

                                signedXml.AddReference(reference);

                                KeyInfo keyInfo = new KeyInfo();

                                keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                signedXml.KeyInfo = keyInfo;
                                signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

                                signedXml.ComputeSignature();

                                XmlElement xmlDigitalSignature = signedXml.GetXml();

                                doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                XMLDoc = new XmlDocument();
                                XMLDoc.PreserveWhitespace = false;
                                XMLDoc = doc;
                                return XMLDoc.OuterXml;
                            }
                            catch (Exception caught)
                            {
                                throw new Exception("Erro: Ao assinar o documento - " + caught.Message);
                            }
                        }
                    }
                }
                catch (Exception caught)
                {
                    throw new Exception("Erro: XML mal formado - " + caught.Message);
                }
            }
        }
        catch (Exception caught)
        {
            throw new Exception("Erro: Problema ao acessar o certificado digital" + caught.Message);
        }
    }

    public static string ObterAssinatura(string XMLString, string RefUri, X509Certificate2 X509Cert)
    {
        X509Certificate2 x509Certificate = X509Cert;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.PreserveWhitespace = true;
        XmlDocument xmlDocument2 = xmlDocument;
        xmlDocument2.LoadXml(XMLString);
        SignedXml val = (SignedXml)(object)new SignedXml(xmlDocument2);
        val.SigningKey = (x509Certificate.PrivateKey);
        SignedXml val2 = val;
        Reference val3 = (Reference)(object)new Reference();


        string id = "";
        XmlAttributeCollection _Uri = xmlDocument2.GetElementsByTagName(RefUri).Item(0).Attributes;
        foreach (XmlAttribute _atributo in _Uri)
        {
            if (_atributo.Name == "Id")
            {
                id = _atributo.InnerText;
            }
        }

        val3.Uri = ("#NFe" + id);
        Reference val4 = val3;
        XmlDsigEnvelopedSignatureTransform val5 = (XmlDsigEnvelopedSignatureTransform)(object)new XmlDsigEnvelopedSignatureTransform();
        val4.AddTransform((Transform)(object)val5);
        XmlDsigC14NTransform val6 = (XmlDsigC14NTransform)(object)new XmlDsigC14NTransform();
        val4.AddTransform((Transform)(object)val6);
        val2.AddReference(val4);
        KeyInfo val7 = (KeyInfo)(object)new KeyInfo();
        val7.AddClause((KeyInfoClause)(object)new KeyInfoX509Data((X509Certificate)x509Certificate));
        val2.KeyInfo = (val7);
        val2.ComputeSignature();
        XmlElement xml = val2.GetXml();
        return xml.OuterXml;
    }



    public static string removeINT(string str)
    {
        var apenasDigitos = new Regex(@"[^\d]");
        return apenasDigitos.Replace(str, "");
    }
    public static string GerarChaveNFCe(TNFe NFCe, string tpEvento = "", string nSeqEvento = "")
    {
        string projeto = removeINT(NFCe.infNFe.ide.mod.ToString());
        string cUF = removeINT(NFCe.infNFe.ide.cUF.ToString());
        string dhEmi = NFCe.infNFe.ide.dhEmi.ToString();
        string serie = NFCe.infNFe.ide.serie.ToString();
        string nDF = NFCe.infNFe.ide.nNF.ToString();
        string tpEmis = removeINT(NFCe.infNFe.ide.tpEmis.ToString());
        string cnpjEmitente = NFCe.infNFe.emit.Item.ToString();


        for (int i = serie.Length; i < 3; i++)
            serie = "0" + serie;

        for (int i = nDF.Length; i < 9; i++)
            serie = "0" + serie;

        string[] auxAAMM = dhEmi.Split('T');
        DateTime dateTime = DateTime.Parse(auxAAMM[0]);
        string aamm = dateTime.ToString("yyMM");
        string chave43 = cUF + aamm + cnpjEmitente + projeto + serie + nDF + tpEmis + gerarCodigoCDF();
        string chave = tpEvento + chave43 + GerarDigitoVerificador(chave43) + nSeqEvento;
        return chave;
    }
    public static int gerarCodigoCDF()
    {
        int min = 10000000;
        int max = 99999999;
        Random random = new Random();
        return random.Next(min, max);
    }

    private static int GerarDigitoVerificador(string chave)
    {
        int soma = 0;
        int restoDivisao = -1;
        int digitoVerificador = -1;
        int pesoMultiplicacao = 2;

        for (int i = chave.Length - 1; i != -1; i--)
        {
            int ch = Convert.ToInt32(chave[i].ToString());
            soma += ch * pesoMultiplicacao;

            if (pesoMultiplicacao < 9)
                pesoMultiplicacao += 1;
            else
                pesoMultiplicacao = 2;
        }
        restoDivisao = soma % 11;
        if (restoDivisao == 0 || restoDivisao == 1)
            digitoVerificador = 0;
        else
            digitoVerificador = 11 - restoDivisao;

        return digitoVerificador;
    }
    string ApenasNumeros(string str)
    {
        var apenasDigitos = new Regex(@"[^\d]");
        return apenasDigitos.Replace(str, "");
    }

    //public void gerarPDF2(Nfe nfe, String pdf, String chave, bool imprimir)
    //{
    //    if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
    //    {
    //        if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf"))
    //        {
    //            byte[] bytes = Convert.FromBase64String(pdf);
    //            System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf", FileMode.CreateNew);
    //            System.IO.BinaryWriter writer =
    //                new BinaryWriter(stream);
    //            writer.Write(bytes, 0, bytes.Length);
    //            writer.Close();
    //        }
    //        if (imprimir == true)
    //        {
    //            FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
    //            frmPDF.ShowDialog();
    //            //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
    //        }
    //    }
    //    else if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("55"))
    //    {
    //        if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf"))
    //        {
    //            byte[] bytes = Convert.FromBase64String(pdf);
    //            System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf", FileMode.CreateNew);
    //            System.IO.BinaryWriter writer =
    //                new BinaryWriter(stream);
    //            writer.Write(bytes, 0, bytes.Length);
    //            writer.Close();
    //        }
    //        if (imprimir == true)
    //            Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
    //    }
    //}
    public void gravarXMLNoBanco(TNfeProc nota, int nsu, string tipoOperacao, int idNFeBanco)
    {
        Nfe nfe = new Nfe();       
        nfe.Id = idNFeBanco;
        if(nfe.Id > 0)
            nfe = (Nfe)NfeController.getInstance().selecionar(nfe);
        nfe.RazaoEmitente = nota.NFe.infNFe.emit.xNome.ToUpper();
        nfe.CnpjEmitente = nota.NFe.infNFe.emit.Item;
        nfe.CDv = nota.NFe.infNFe.ide.cDV;
        nfe.CMunFg = nota.NFe.infNFe.ide.cMunFG;
        nfe.CNf = nota.NFe.infNFe.ide.cNF;
        nfe.Conciliado = true;
        nfe.CUf = ApenasNumeros(nota.NFe.infNFe.ide.cUF.ToString());
        nfe.DhEmi = nota.NFe.infNFe.ide.dhEmi;
        if(nota.NFe.infNFe.ide.dhSaiEnt != null)
            nfe.DhSaiEnt = nota.NFe.infNFe.ide.dhSaiEnt;
        nfe.EmpresaFilial = Sessao.empresaFilialLogada;
        nfe.FinNfe = ApenasNumeros(nota.NFe.infNFe.ide.finNFe.ToString());
        nfe.IdDest = ApenasNumeros(nota.NFe.infNFe.ide.idDest.ToString());
        nfe.IndFinal = ApenasNumeros(nota.NFe.infNFe.ide.indFinal.ToString());
        nfe.IndIntermed = ApenasNumeros(nota.NFe.infNFe.ide.indIntermed.ToString());
        nfe.IndPres = ApenasNumeros(nota.NFe.infNFe.ide.indPres.ToString());
        if (nota.NFe.infNFe.infAdic != null)
            nfe.InfCpl = nota.NFe.infNFe.infAdic.infCpl;
        else
            nfe.InfCpl = "";
        nfe.Lancada = false;
        nfe.Modelo = ApenasNumeros(nota.NFe.infNFe.ide.mod.ToString());
        nfe.ModFrete = ApenasNumeros(nota.NFe.infNFe.transp.modFrete.ToString());
        nfe.NatOp = nota.NFe.infNFe.ide.natOp;
        nfe.NNf = nota.NFe.infNFe.ide.nNF;
        nfe.Nsu = nsu;
        nfe.ProcEmi = ApenasNumeros(nota.NFe.infNFe.ide.procEmi.ToString());
        nfe.Serie = nota.NFe.infNFe.ide.serie;
        nfe.TipoOperacao = tipoOperacao;
        nfe.TpAmb = ApenasNumeros(nota.NFe.infNFe.ide.tpAmb.ToString());
        nfe.TpEmis = ApenasNumeros(nota.NFe.infNFe.ide.tpEmis.ToString());
        nfe.TpImp = ApenasNumeros(nota.NFe.infNFe.ide.tpImp.ToString());
        nfe.TpNf = ApenasNumeros(nota.NFe.infNFe.ide.tpNF.ToString());

        nfe.VBc = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vBC.Replace(".", ","));
        nfe.VBcst = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vBCST.Replace(".", ","));
        nfe.VCofins = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vCOFINS.Replace(".", ","));
        nfe.VDesc = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vDesc.Replace(".", ","));
        nfe.VerProc = nota.NFe.infNFe.ide.verProc;
        nfe.VFcp = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vFCP.Replace(".", ","));
        nfe.VFcpst = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vFCPST.Replace(".", ","));
        nfe.VFcpstRet = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vFCPSTRet.Replace(".", ","));
        nfe.VFrete = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vFrete.Replace(".", ","));
        nfe.VIcms = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vICMS.Replace(".", ","));
        nfe.VIcmsDeson = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vICMSDeson.Replace(".", ","));
        nfe.VIi = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vII.Replace(".", ","));
        nfe.VIpi = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vIPI.Replace(".", ","));
        nfe.VIpiDevol = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vIPIDevol.Replace(".", ","));
        nfe.VNf = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vNF.Replace(".", ","));
        nfe.VOutro = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vOutro.Replace(".", ","));
        nfe.VPis = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vPIS.Replace(".", ","));
        nfe.VProd = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vProd.Replace(".", ","));
        nfe.VSeg = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vSeg.Replace(".", ","));
        nfe.VSt = decimal.Parse(nota.NFe.infNFe.total.ICMSTot.vST.Replace(".", ","));


        string ultimaDataNotaBaixada = nota.NFe.infNFe.ide.dhEmi.Replace("T", " ").Replace("-03:00", "");
        nfe.DataEmissao = DateTime.Parse(ultimaDataNotaBaixada);
        nfe.Manifesto = "";

        if (nota.protNFe.infProt.xMotivo.Contains("Autorizado o uso"))
        {
            nfe.CodStatus = "100";
            NfeStatus nfeStatus = new NfeStatus();
            nfeStatus.Id = 1;
            nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
            nfe.NfeStatus = nfeStatus;
            nfe.Status = nota.protNFe.infProt.xMotivo;
        }

        else
            nfe.CodStatus = "";
        nfe.Status = nota.protNFe.infProt.xMotivo;
        nfe.Chave = nota.protNFe.infProt.chNFe;
        nfe.Protocolo = nota.protNFe.infProt.nProt;
        if (nota.NFe.infNFe.dest != null)
        {
            nfe.Destinatario = nota.NFe.infNFe.dest.xNome;
            nfe.CnpjDestinatario = nota.NFe.infNFe.dest.Item;
        }
        else
        {
            nfe.Destinatario = "";
            nfe.CnpjDestinatario = "";
        }
        try { nfe.DataProtocolo = DateTime.Parse(nota.protNFe.infProt.dhRecbto); } catch { };
        
        //salvar produtos da nota de saida
        //for(int x = 0; x < nota.NFe.infNFe.det.Length; x++)
        //{
        //    nota.NFe.infNFe.det[x].prod
        //} 

        Pessoa pessoa = new Pessoa();
        PessoaController pessoaController = new PessoaController();
        //Se é entrada
        if (tipoOperacao.Equals("E"))
        {
            pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(nfe.CnpjEmitente);
            if (pessoa != null) 
            {
                nfe.Fornecedor = pessoa;
                nfe.Cliente = null;
            }
            else 
            {
                pessoa = cadastrarFornecedor(nfe, nota);
                if(pessoa != null)
                {
                    nfe.Fornecedor = pessoa;
                    nfe.Cliente = null;
                }
                else
                {
                    nfe.Fornecedor = null;
                    nfe.Cliente = null;
                }
            }
        }
        else if (tipoOperacao.Equals("S"))
        {
            if (!String.IsNullOrEmpty(nfe.Destinatario))
            {
                pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(nfe.CnpjDestinatario);
                if (pessoa != null)
                {
                    nfe.Fornecedor = null;
                    nfe.Cliente = pessoa;
                }
                else
                {
                    pessoa = cadastrarCliente(nfe, nota);
                    if (pessoa != null)
                    {
                        nfe.Cliente = pessoa;
                        nfe.Fornecedor = null;
                    }
                    else
                    {
                        nfe.Fornecedor = null;
                        nfe.Cliente = null;
                    }
                }
            }
        }
        Controller.getInstance().salvar(nfe);
    }

    //private Pessoa cadastrarTransportadora(Nfe nfe, TNfeProc notaXML)
    //{
    //    try
    //    {
    //        if (notaXML.NFe.infNFe.transp != null) 
    //        {
    //            PessoaController pessoaController = new PessoaController();
    //            Pessoa pessoa = new Pessoa();
    //            pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(notaXML.NFe.infNFe.transp.Items["CNPJ"].ToString);
    //            if (pessoa == null)
    //            {
    //                CidadeController cidadeController = new CidadeController();
    //                pessoa.Fornecedor = false;
    //                pessoa.Transportadora = true;
    //                pessoa.Id = 0;
    //                pessoa.Cliente = false;
    //                pessoa.Cnpj = notaXML.NFe.infNFe.transp.Items.ToString();
    //                pessoa.ContatoTrabalho = "";
    //                pessoa.TipoPessoa = "PJ";
    //                Endereco endereco = new Endereco();
    //                endereco.Id = 0;
    //                if (notaXML.NFe.infNFe.transp.retTransp..ToString() != null)
    //                    endereco.Bairro = notaXML.NFe.infNFe.emit.enderEmit.xBairro.ToUpper();
    //                endereco.Cep = notaXML.NFe.infNFe.emit.enderEmit.CEP;
    //                endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(notaXML.NFe.infNFe.emit.enderEmit.xMun, notaXML.NFe.infNFe.emit.enderEmit.cMun);
    //                if (endereco.Cidade == null)
    //                    endereco.Cidade = null;
    //                endereco.EmpresaFilial = Sessao.empresaFilialLogada;
    //                if (notaXML.NFe.infNFe.emit.enderEmit.xLgr != null)
    //                    endereco.Logradouro = notaXML.NFe.infNFe.emit.enderEmit.xLgr.ToUpper();
    //                endereco.Numero = notaXML.NFe.infNFe.emit.enderEmit.nro;
    //                endereco.Pessoa = null;
    //                if (notaXML.NFe.infNFe.emit.enderEmit.xCpl != null)
    //                    endereco.Complemento = notaXML.NFe.infNFe.emit.enderEmit.xCpl.ToUpper();
    //                Controller.getInstance().salvar(endereco);

    //                pessoa.EnderecoPrincipal = endereco;
    //                pessoa.FuncaoTrabalho = "";
    //                pessoa.Funcionario = false;
    //                pessoa.InscricaoEstadual = notaXML.NFe.infNFe.emit.IE;
    //                pessoa.LimiteCredito = 0;
    //                pessoa.LocalTrabalho = "";
    //                pessoa.Mae = "";
    //                if (notaXML.NFe.infNFe.emit.xFant != null)
    //                    pessoa.NomeFantasia = notaXML.NFe.infNFe.emit.xFant.ToUpper();
    //                pessoa.Observacoes = "";
    //                pessoa.Pai = "";
    //                pessoa.PessoaTelefone = null;
    //                if (notaXML.NFe.infNFe.emit.xNome != null)
    //                    pessoa.RazaoSocial = notaXML.NFe.infNFe.emit.xNome.ToUpper();
    //                pessoa.Rg = "";
    //                pessoa.SalarioTrabalho = "";
    //                pessoa.Sexo = "";
    //                pessoa.Vendedor = false;
    //                pessoa.Tecnico = false;
    //                pessoa.TelefoneTrabalho = "";
    //                pessoa.TempoTrabalho = "";
    //                Controller.getInstance().salvar(pessoa);
    //                endereco.Pessoa = pessoa;
    //                Controller.getInstance().salvar(endereco);
    //                return pessoa;
    //            }
    //        } 
    //    }
    //    catch (Exception e)
    //    {
    //        MessageBox.Show("Falha ao gravar transportadora! " + e.Message);
    //        return null;
    //    } 
    //}


    private Pessoa cadastrarFornecedor(Nfe nfe, TNfeProc notaXML)
    {
        try
        {
            PessoaController pessoaController = new PessoaController();
            Pessoa pessoa = new Pessoa();
            CidadeController cidadeController = new CidadeController();
            pessoa.Fornecedor = true;
            pessoa.Id = 0;
            pessoa.Cliente = false;
            pessoa.Cnpj = notaXML.NFe.infNFe.emit.Item;
            pessoa.ContatoTrabalho = "";
            pessoa.TipoPessoa = "PJ";
            Endereco endereco = new Endereco();
            endereco.Id = 0;
            if (notaXML.NFe.infNFe.emit.enderEmit.xBairro != null)
                endereco.Bairro = notaXML.NFe.infNFe.emit.enderEmit.xBairro.ToUpper();
            endereco.Cep = notaXML.NFe.infNFe.emit.enderEmit.CEP;
            endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(notaXML.NFe.infNFe.emit.enderEmit.xMun, notaXML.NFe.infNFe.emit.enderEmit.cMun);
            if (endereco.Cidade == null)
                endereco.Cidade = null;
            endereco.EmpresaFilial = Sessao.empresaFilialLogada;
            if (notaXML.NFe.infNFe.emit.enderEmit.xLgr != null)
                endereco.Logradouro = notaXML.NFe.infNFe.emit.enderEmit.xLgr.ToUpper();
            endereco.Numero = notaXML.NFe.infNFe.emit.enderEmit.nro;
            endereco.Pessoa = null;
            if (notaXML.NFe.infNFe.emit.enderEmit.xCpl != null)
                endereco.Complemento = notaXML.NFe.infNFe.emit.enderEmit.xCpl.ToUpper();
            Controller.getInstance().salvar(endereco);

            pessoa.EnderecoPrincipal = endereco;
            pessoa.FuncaoTrabalho = "";
            pessoa.Funcionario = false;
            pessoa.InscricaoEstadual = notaXML.NFe.infNFe.emit.IE;
            pessoa.LimiteCredito = 0;
            pessoa.LocalTrabalho = "";
            pessoa.Mae = "";
            if (notaXML.NFe.infNFe.emit.xFant != null)
                pessoa.NomeFantasia = notaXML.NFe.infNFe.emit.xFant.ToUpper();
            pessoa.Observacoes = "";
            pessoa.Pai = "";
            pessoa.PessoaTelefone = null;
            if (notaXML.NFe.infNFe.emit.xNome != null)
                pessoa.RazaoSocial = notaXML.NFe.infNFe.emit.xNome.ToUpper();
            pessoa.Rg = "";
            pessoa.SalarioTrabalho = "";
            pessoa.Sexo = "";
            pessoa.Vendedor = false;
            pessoa.Tecnico = false;
            pessoa.TelefoneTrabalho = "";
            pessoa.TempoTrabalho = "";
            Controller.getInstance().salvar(pessoa);
            endereco.Pessoa = pessoa;
            Controller.getInstance().salvar(endereco);
            return pessoa;
        }
        catch (Exception e) 
        {
            MessageBox.Show("Falha ao gravar fornecedor! " + e.Message);
            return null;
        }
    }

    private Pessoa cadastrarCliente(Nfe nfe, TNfeProc notaXML)
    {
        try
        {
            PessoaController pessoaController = new PessoaController();
            Pessoa pessoa = new Pessoa();
            CidadeController cidadeController = new CidadeController();
            pessoa.Fornecedor = false;
            pessoa.Id = 0;
            pessoa.Cliente = true;
            pessoa.Cnpj = notaXML.NFe.infNFe.emit.Item;
            pessoa.ContatoTrabalho = "";
            Endereco endereco = new Endereco();
            if (notaXML.NFe.infNFe.dest != null)
            {
                endereco.Id = 0;
                if (notaXML.NFe.infNFe.dest.enderDest.xBairro != null)
                    endereco.Bairro = notaXML.NFe.infNFe.dest.enderDest.xBairro.ToUpper();
                endereco.Cep = notaXML.NFe.infNFe.dest.enderDest.CEP;
                endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(notaXML.NFe.infNFe.dest.enderDest.xMun, notaXML.NFe.infNFe.dest.enderDest.cMun);
                if (endereco.Cidade == null)
                    endereco.Cidade = null;
                endereco.EmpresaFilial = Sessao.empresaFilialLogada;
                if (notaXML.NFe.infNFe.dest.enderDest.xLgr != null)
                    endereco.Logradouro = notaXML.NFe.infNFe.dest.enderDest.xLgr.ToUpper();
                endereco.Numero = notaXML.NFe.infNFe.dest.enderDest.nro;

                endereco.Pessoa = null;
                if (notaXML.NFe.infNFe.dest.enderDest.xCpl != null)
                    endereco.Complemento = notaXML.NFe.infNFe.dest.enderDest.xCpl.ToUpper();
                Controller.getInstance().salvar(endereco);
            }
      
            pessoa.EnderecoPrincipal = endereco;
            pessoa.FuncaoTrabalho = "";
            pessoa.Funcionario = false;
            if (notaXML.NFe.infNFe.dest.IE != null)
                pessoa.InscricaoEstadual = notaXML.NFe.infNFe.dest.IE;
            else
                pessoa.InscricaoEstadual = "";
            pessoa.LimiteCredito = 0;
            pessoa.LocalTrabalho = "";
            pessoa.Mae = "";
            if (notaXML.NFe.infNFe.dest.xNome != null)
                pessoa.NomeFantasia = notaXML.NFe.infNFe.dest.xNome.ToUpper();
            pessoa.Observacoes = "";
            pessoa.Pai = "";
            pessoa.PessoaTelefone = null;
            if (notaXML.NFe.infNFe.dest.xNome != null)
                pessoa.RazaoSocial = notaXML.NFe.infNFe.dest.xNome.ToUpper();
            pessoa.Rg = "";
            pessoa.SalarioTrabalho = "";
            pessoa.Sexo = "";
            pessoa.Vendedor = false;
            pessoa.Tecnico = false;
            pessoa.TelefoneTrabalho = "";
            pessoa.TempoTrabalho = "";
            pessoa.TipoPessoa = "";
            Controller.getInstance().salvar(pessoa);
            endereco.Pessoa = pessoa;
            Controller.getInstance().salvar(endereco);
            return pessoa;
        }
        catch (Exception e)
        {
            MessageBox.Show("Falha ao gravar cliente! " + e.Message);
            return null;
        }
    }
}
