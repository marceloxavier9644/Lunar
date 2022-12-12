namespace LunarBase.Utilidades
{
    public class Ns_ConsultaCNPJ
    {

        public class Rootobject
        {
            public int status { get; set; }
            public string motivo { get; set; }
            public Retconscad retConsCad { get; set; }
        }

        public class Retconscad
        {
            public Infcons infCons { get; set; }
            public string versao { get; set; }
        }

        public class Infcons
        {
            public string verAplic { get; set; }
            public int cStat { get; set; }
            public string xMotivo { get; set; }
            public string UF { get; set; }
            public string CNPJ { get; set; }
            public DateTime dhCons { get; set; }
            public int cUF { get; set; }
            public Infcad[] infCad { get; set; }
        }

        public class Infcad
        {
            public string IE { get; set; }
            public string CNPJ { get; set; }
            public string UF { get; set; }
            public int cSit { get; set; }
            public int indCredNFe { get; set; }
            public int indCredCTe { get; set; }
            public string xNome { get; set; }
            public string xFant { get; set; }
            public string xRegApur { get; set; }
            public int CNAE { get; set; }
            public string dIniAtiv { get; set; }
            public string dUltSit { get; set; }
            public string IEAtual { get; set; }
            public Ender ender { get; set; }
        }

        public class Ender
        {
            public string xLgr { get; set; }
            public string nro { get; set; }
            public string xCpl { get; set; }
            public string xBairro { get; set; }
            public string cMun { get; set; }
            public string xMun { get; set; }
            public string CEP { get; set; }
        }

    }
}
