using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class PessoaCaracteristica : ObjetoPadrao
    {
        public int Id { get; set; }
        public Caracteristica Caracteristica { get; set; }
        public string Valor { get; set; }
    }
}
