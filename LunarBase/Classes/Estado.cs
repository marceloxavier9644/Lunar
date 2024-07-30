using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Estado")]
    public class Estado : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string uf;
        private string ibge;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Estado")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("UF")]
        public virtual string Uf { get => uf; set => uf = value; }
        [Anotacao("IBGE")]
        public virtual string Ibge { get => ibge; set => ibge = value; }

        public override string ToString()
        {
            return uf;
        }
    }
}
