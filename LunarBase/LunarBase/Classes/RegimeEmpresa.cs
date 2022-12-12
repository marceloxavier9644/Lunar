using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Regime Tributário")]
    public class RegimeEmpresa : ObjetoPadrao
    {
        private int id;
        private string descricao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Regime Tributário")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
