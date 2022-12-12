namespace LunarBase.Utilidades
{
    public class Cest_Json
    {

        public class Rootobject
        {
            public TabelaCest[] tabelaCests { get; set; }
        }

        public class TabelaCest
        {
            public string CEST { get; set; }
            public object NCM { get; set; }
            public string SEGMENTO { get; set; }
            public float ITEM { get; set; }
            public string DESCRICAO { get; set; }
        }

    }
}
