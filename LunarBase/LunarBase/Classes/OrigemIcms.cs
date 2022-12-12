using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Origem")]
    public class OrigemIcms : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string codOrigem;

        [Anotacao("Código")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Código Origem")]
        public virtual string CodOrigem { get => codOrigem; set => codOrigem = value; }
        [Anotacao("Origem")]
        public virtual string Descricao { get => descricao; set => descricao = value; }


        public override string ToString()
        {
            return descricao;
        }
    }
}
