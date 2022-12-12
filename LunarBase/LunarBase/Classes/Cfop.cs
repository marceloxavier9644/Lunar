using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("CFOP")]
    public class Cfop : ObjetoPadrao
    {
        private int id;
        private string cfopNumero;
        private string descricao;

        [Anotacao("ID")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("CFOP")]
        public virtual string CfopNumero { get => cfopNumero; set => cfopNumero = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return cfopNumero;
        }
    }
}
