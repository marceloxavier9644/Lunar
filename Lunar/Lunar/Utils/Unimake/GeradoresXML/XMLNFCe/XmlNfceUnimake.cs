using LunarBase.Classes;
using LunarBase.Utilidades;
using System.Xml;

namespace Lunar.Utils.Unimake.GeradoresXML.XMLNFCe
{
    public class XmlNfceUnimake
    {
        public static void GerarXmlConsSitNFe(string caminhoSalvarXml, string chaveNFCe)
        {
            // Cria um novo XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Cria o elemento raiz <consSitNFe> com o namespace correto
            XmlElement consSitNFe = xmlDoc.CreateElement("consSitNFe", "http://www.portalfiscal.inf.br/nfe");
            xmlDoc.AppendChild(consSitNFe);

            // Adiciona o atributo 'versao' ao elemento raiz
            XmlAttribute versaoAttr = xmlDoc.CreateAttribute("versao");
            versaoAttr.Value = "4.00";
            consSitNFe.Attributes.Append(versaoAttr);

            // Adiciona os elementos filhos dentro de <consSitNFe>
            string ambiente = Sessao.parametroSistema.AmbienteProducao ? "1" : "2";
            consSitNFe.AppendChild(CreateElement(xmlDoc, "tpAmb", ambiente));
            consSitNFe.AppendChild(CreateElement(xmlDoc, "xServ", "CONSULTAR"));
            consSitNFe.AppendChild(CreateElement(xmlDoc, "tpEmis", "1"));
            consSitNFe.AppendChild(CreateElement(xmlDoc, "chNFe", chaveNFCe));

            // Salva o XML na pasta especificada
            xmlDoc.Save(caminhoSalvarXml);
            xmlDoc.Save("C:\\XML\\Uninfe.xml");

            Logger logger = new Logger();
            logger.WriteLog("XML CONSULTA NFCE GERADO " + caminhoSalvarXml, "LOGUNIMAKE");
        }

        static XmlElement CreateElement(XmlDocument xmlDoc, string name, string value)
        {
            XmlElement element = xmlDoc.CreateElement(name, "http://www.portalfiscal.inf.br/nfe");
            element.InnerText = value;
            return element;
        }
    }
}
