using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Anotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class Anotacao : Attribute
    {
        private String vDescricao;

        public virtual String Descricao
        {
            get { return vDescricao; }
        }

        public Anotacao(String Descricao)
        {
            vDescricao = Descricao;
        }

        public override string ToString()
        {
            return vDescricao;
        }
    }
}
