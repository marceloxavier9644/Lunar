using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Produto Código Barras")]
    public class ProdutoCodigoBarras : ObjetoPadrao
    {
        private int id;
        private String codigoBarras;
        private Produto produto;
        private ProdutoGrade produtoGrade;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Codigo Barras")]
        public virtual string CodigoBarras { get => codigoBarras; set => codigoBarras = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Grade")]
        public virtual ProdutoGrade ProdutoGrade { get => produtoGrade; set => produtoGrade = value; }
    }
}
