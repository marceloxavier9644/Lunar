using System;

namespace Lunar.Utils.OrganizacaoNF
{
    public class RetInutilizacao
    {

        public class RetornoInutilizacao
        {
            public int status { get; set; }
            public string motivo { get; set; }
            public RetornoInutNFe retornoInutNFe { get; set; }
            public RetInut retInut { get; set; }
            public Erro erro { get; set; }
        }
    

    public class Erro
    {
        public int cStat { get; set; }
        public string xMotivo { get; set; }
    }
    public class RetornoInutNFe
        {
            public string cStat { get; set; }
            public string xMotivo { get; set; }
            public string chave { get; set; }
            public int tpAmb { get; set; }
            public DateTime dhRecbto { get; set; }
            public string nProt { get; set; }
            public string idInut { get; set; }
            public string xml { get; set; }
        }

        public class RetInut
        {
            public string cStat { get; set; }
            public string xMotivo { get; set; }
            public string chave { get; set; }
            public int tpAmb { get; set; }
            public DateTime dhRecbto { get; set; }
            public string nProt { get; set; }
            public string idInut { get; set; }
            public string xml { get; set; }
        }

    }
}
