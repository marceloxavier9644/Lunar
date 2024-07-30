using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("ANP")]
    public class Anp : ObjetoPadrao
    {
        private int id;
        private string codigo;
        private string descricao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("ANP")]
        public virtual string Codigo { get => codigo; set => codigo = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return codigo;
        }
    }
}
