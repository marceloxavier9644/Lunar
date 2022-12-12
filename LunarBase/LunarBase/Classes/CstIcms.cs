using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("CST ICMS")]
    public class CstIcms : ObjetoPadrao
    {
        private int id;
        private string codigo;
        private string descricao;

        [Anotacao("ID")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("CST")]
        public virtual string Codigo { get => codigo; set => codigo = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return codigo;
        }
    }
}
