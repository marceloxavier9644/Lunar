using LunarBase.Anotations;
using LunarBase.Classes;

namespace LunarBase
{
    [Serializable]
    [Anotacao("Tamanho")]
    public class ProdutoTamanho : ObjetoPadrao
    {
        private int id;
        private String descricao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
