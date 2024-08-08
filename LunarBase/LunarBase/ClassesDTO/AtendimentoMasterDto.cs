using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDTO
{
    public class AtendimentoMasterDto
    {
        public int IdMesa { get; set; }
        public AtendimentoDto Atendimento { get; set; }
        public AtendimentoContaDto AtendimentoConta { get; set; }
        public AtendimentoVinculoDto AtendimentoVinculo { get; set; }
    }
}
