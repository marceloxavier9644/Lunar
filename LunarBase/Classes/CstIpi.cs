using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("CST IPI")]
    public class CstIpi : ObjetoPadrao
    {
        private int id;
        private string cst;
        private string descricao;

        [Anotacao("ID")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("CST")]
        public virtual string Cst { get => cst; set => cst = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public override string ToString()
        {
            return cst;
        }
    }
}
