using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Tamanho")]
    public class Tamanho : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private int ordem;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Tamanho")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Ordem")]
        [OcultarEmGridsEPesquisas]
        public virtual int Ordem { get => ordem; set => ordem = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
