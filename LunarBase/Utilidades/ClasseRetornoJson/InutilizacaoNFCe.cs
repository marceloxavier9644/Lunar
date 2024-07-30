namespace LunarBase.Utilidades.ClasseRetornoJson
{
    public class InutilizacaoNFCe
    {
        public class Inutilizacao
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public Retinutnfe retInutNFe { get; set; }
            public Erro erro { get; set; }
        }

        public class Retinutnfe
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
            public string chave { get; set; }
            public string dhRecbto { get; set; }
            public string nProt { get; set; }
            public string idInut { get; set; }
            public string xml { get; set; }
            
        }

        public class Erro
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
        }


    }
}
