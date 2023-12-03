using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.OrganizacaoNF
{
    public class RetConsultaNfe
    {

        public class RetNota55
        {
            public int status { get; set; }
            public string motivo { get; set; }
            public Retconssitnfe retConsSitNFe { get; set; }
        }

        public class Retconssitnfe
        {
            public string tpAmb { get; set; }
            public string verAplic { get; set; }
            public string cStat { get; set; }
            public string xMotivo { get; set; }
            public string cUF { get; set; }
            public DateTime dhRecbto { get; set; }
            public Protnfe[] protNFe { get; set; }
            public string versao { get; set; }
        }

        public class Protnfe
        {
            public Infprot infProt { get; set; }
            public string versao { get; set; }
        }

        public class Infprot
        {
            public string Id { get; set; }
            public string tpAmb { get; set; }
            public string verAplic { get; set; }
            public string chNFe { get; set; }
            public DateTime dhRecbto { get; set; }
            public string nProt { get; set; }
            public string digVal { get; set; }
            public string cStat { get; set; }
            public string xMotivo { get; set; }
        }

    }
}
