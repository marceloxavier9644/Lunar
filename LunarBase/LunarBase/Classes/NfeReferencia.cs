using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Notas Referenciadas")]
    public class NfeReferencia : ObjetoPadrao
    {
        private int id;
        private string chave;
        private Nfe nfeReferenciada;
        private Nfe nfe;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Chave")]
        public virtual string Chave { get => chave; set => chave = value; }
        [Anotacao("Nfe Referenciada")]
        public virtual Nfe NfeReferenciada { get => nfeReferenciada; set => nfeReferenciada = value; }
        [Anotacao("Nfe")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
        public override string ToString()
        {
            return chave;
        }
    }
}
