using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Grade")]
    public class ProdutoGrade : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private UnidadeMedida unidadeMedida;
        private Produto produto;
        private decimal valorVenda;
        private double quantidadeMedida;
        private bool principal;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descricao")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Unidade de Medida")]
        public virtual UnidadeMedida UnidadeMedida { get => unidadeMedida; set => unidadeMedida = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Valor")]
        public virtual decimal ValorVenda { get => valorVenda; set => valorVenda = value; }
        [Anotacao("Quantidade Medida")]
        public virtual double QuantidadeMedida { get => quantidadeMedida; set => quantidadeMedida = value; }
        [Anotacao("Principal")]
        public virtual bool Principal { get => principal; set => principal = value; }
    }
}
