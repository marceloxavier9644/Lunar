using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDTO
{
    public class AtendimentoVinculoDto
    {
        public int Id { get; set; }
        public int IdConta { get; set; }
        public int IdAtendimento { get; set; }
        public int IdMesa { get; set; }
        public string Operador { get; set; }
    }

}
