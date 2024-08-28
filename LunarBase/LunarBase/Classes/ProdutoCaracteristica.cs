using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class ProdutoCaracteristica : ObjetoPadrao
    {
        private int id;
        private Produto produto;
        private Caracteristica caracteristica;
        private string descricao;

        public virtual int Id { get => id; set => id = value; }
        public virtual Produto Produto { get => produto; set => produto = value; }
        public virtual Caracteristica Caracteristica { get => caracteristica; set => caracteristica = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
