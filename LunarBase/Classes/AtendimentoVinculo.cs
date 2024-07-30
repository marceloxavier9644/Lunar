using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class AtendimentoVinculo : ObjetoPadrao
    {
        private int id;
        private int contaId;
        private int atendimentoId;
        private int mesaId;

        public virtual int Id { get => id; set => id = value; }
        public virtual int ContaId { get => contaId; set => contaId = value; }
        public virtual int AtendimentoId { get => atendimentoId; set => atendimentoId = value; }
        public virtual int MesaId { get => mesaId; set => mesaId = value; }
    }
}
