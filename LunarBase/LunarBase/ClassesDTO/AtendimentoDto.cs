using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDTO
{
    public class AtendimentoDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Identificacao { get; set; }
        public string Observacoes { get; set; }

    }
}
