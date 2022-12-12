using System;

namespace Lunar.Utils.OrganizacaoNF
{
    public class RetConsultaProcessamento
    {

        public class RetConsultaProcessamentoNF
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string chNFe { get; set; }
            public string cStat { get; set; }
            public string nProt { get; set; }
            public string xMotivo { get; set; }
            public DateTime dhRecbto { get; set; }
            public string xml { get; set; }
            public Erro erro { get; set; }
        }

        public class Erro
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
        }

    }
}
