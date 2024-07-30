using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Banco")]
    public class Banco : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string codBanco;
        private string codIspb;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Banco")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Cód. Banco")]
        public virtual string CodBanco { get => codBanco; set => codBanco = value; }
        [Anotacao("Cód. ISPB")]
        public virtual string CodIspb { get => codIspb; set => codIspb = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
