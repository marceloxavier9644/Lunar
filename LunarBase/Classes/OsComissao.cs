using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class OsComissao
    {
        private int id;
        private string razaoSocial;
        private decimal valor;
        private OrdemServico ordemServico;

        public virtual string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        public virtual decimal Valor { get => valor; set => valor = value; }
        public virtual int Id { get => id; set => id = value; }
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
    }
}
