using System;

namespace Lunar.Utils.OrganizacaoNF
{
    public class RetCartaCorrecao
    {

        public class RetornoCartaoCorrecao
        {
            public int status { get; set; }
            public string motivo { get; set; }
            public Retevento retEvento { get; set; }
            public Erro erro { get; set; }
        }

        public class Retevento
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
            public string chNFe { get; set; }
            public DateTime dhRegEvento { get; set; }
            public string nProt { get; set; }
        }
        public class Erro
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
        }
    }
}
