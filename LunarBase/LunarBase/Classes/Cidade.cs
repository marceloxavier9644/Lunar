using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Cidade")]
    public class Cidade : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private Estado estado;
        private String ibge;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Cidade")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Estado")]
        public virtual Estado Estado { get => estado; set => estado = value; }
        [Anotacao("IBGE")]
        public virtual string Ibge { get => ibge; set => ibge = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
