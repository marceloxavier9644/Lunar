namespace LunarBase.Utilidades.ClasseRetornoJson
{
    public class CancelamentoNFCe
    {

        public class RetornoCancelamento
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public Retevento retEvento { get; set; }
            public NfeProcLocal nfeProc { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }

        }

        public class Retevento
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
            public string chNFe { get; set; }
            public DateTime dhRegEvento { get; set; }
            public string nProt { get; set; }
        }

        public class NfeProcLocal
        {
            public int cStat { get; set; }
            public string xMotivo { get; set; }
        }

    }
}
