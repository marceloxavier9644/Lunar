using LunarBase.Classes;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Lunar.Utils.GeradorNFe2024
{
    public class GeradorNFCe
    {
        public static string GerarXMLNFCe(Nfe nfe, IList<NfeProduto> listaNfeProduto, IList<NfePagamento> listaNfePagamento)
        {
            string xmlNFCe = SerializeObjectToXml<Nfe>(nfe);
            return xmlNFCe;
        }

        // Método genérico para serializar um objeto para XML
        private static string SerializeObjectToXml<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();

            using (StringWriter writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, obj);
            }

            return sb.ToString();
        }
    }

  
}