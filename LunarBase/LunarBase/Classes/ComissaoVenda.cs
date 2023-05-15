using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class ComissaoVenda
    {
        private int id;
        private decimal valorTotal;
        private int vendedor;
        private string nome;

        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        public virtual int Vendedor { get => vendedor; set => vendedor = value; }
        public virtual int Id { get => id; set => id = value; }
        public virtual string Nome { get => nome; set => nome = value; }
    }
}
