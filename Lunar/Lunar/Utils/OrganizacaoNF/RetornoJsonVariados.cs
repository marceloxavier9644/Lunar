using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.OrganizacaoNF
{
    public class RetornoJsonVariados
    {

        public class RetConsultaCertificadoNS
        {
            public int status { get; set; }
            public string msg { get; set; }
            public Certificado certificado { get; set; }
        }

        public class Certificado
        {
            public string vencimento { get; set; }
        }

    }
}
