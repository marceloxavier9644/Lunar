using static LunarBase.Utilidades.Ncm_Json;

namespace LunarBase.Utilidades
{
    public class Ncm_Json
    {
        public class Rootobject
        {
            public string Data_Ultima_Atualizacao_NCM { get; set; }
            public Nomenclatura[] Nomenclaturas { get; set; }
        }

        public class Nomenclatura
        {
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public string Data_Inicio { get; set; }
            public string Data_Fim { get; set; }
            public string Tipo_Ato { get; set; }
            public string Numero_Ato { get; set; }
            public string Ano_Ato { get; set; }
        }

    }
}
