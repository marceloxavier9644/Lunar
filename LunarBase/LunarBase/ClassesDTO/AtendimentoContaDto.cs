using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDTO
{
    public class AtendimentoContaDto
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public int IdAtendimento { get; set; }
        public string NomeCliente { get; set; }
    }
}
