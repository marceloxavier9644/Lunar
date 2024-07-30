using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades
{
    public class BancosJson
    {
        public class Rootobject
        {
            public Banco[] Bancos { get; set; }
        }

        public class Banco
        {
            public int CodBanco { get; set; }
            public string Descricao { get; set; }
            public int CodIspb { get; set; }
        }

    }
}
