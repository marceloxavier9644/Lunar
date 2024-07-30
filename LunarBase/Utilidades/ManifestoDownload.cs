using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades
{
    public class ManifestoDownload
    {
        public class Manifesto
        {
            public int status { get; set; }
            public int ultNSU { get; set; }
            public Xml[] xmls { get; set; }
        }

        public class Xml
        {
            public int nsu { get; set; }
            public string chave { get; set; }
            public string emitCnpj { get; set; }
            public string emitRazao { get; set; }
            public int modelo { get; set; }
            public string xml { get; set; }
            public double vNF { get; set; }
            public DateTime dhEmis { get; set; }
            public string pdf { get; set; }
        }


        public class NotaUnique
        {
            public int status { get; set; }
            public bool listaDocs { get; set; }
            public int nsu { get; set; }
            public string chave { get; set; }
            public string emitCnpj { get; set; }
            public string emitRazao { get; set; }
            public int modelo { get; set; }
            public string vNF { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }
        }



        public class NFCeDownloadProc
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public Nfeproc nfeProc { get; set; }
            public Retevento retEvento { get; set; }
            public string pdf { get; set; }
            public string pdfCancelamento { get; set; }
        }

        public class Nfeproc
        {
            public string nProt { get; set; }
            public string digVal { get; set; }
            public string chNFe { get; set; }
            public string serie { get; set; }
            public string numero { get; set; }
            public string xMotivo { get; set; }
            public DateTime dhRecbto { get; set; }
            public string xml { get; set; }
        }

        public class Retevento
        {
            public string cStat { get; set; }
            public string xMotivo { get; set; }
            public string chNFeCanc { get; set; }
            public DateTime dhRegEvento { get; set; }
            public string nProt { get; set; }
            public string xml { get; set; }
        }


        public class NFeDownloadProc55
        {
            public int status { get; set; }
            public string motivo { get; set; }
            public string chNFe { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }
        }


    }
}
