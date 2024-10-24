using LunarBase.Anotations;
using Microsoft.SqlServer.Server;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Produto Insumo")]
    public class ProdutoInsumo : ObjetoPadrao
    {
        private int id;
        private Produto produto;
        private decimal custoUnitario;
        private double quantidade;
        private decimal custoTotal;
        private string idProdutoProduzido;
        private bool produzirNaVenda;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Custo Unitário")]
        public virtual decimal CustoUnitario { get => custoUnitario; set => custoUnitario = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Custo Total")]
        public virtual decimal CustoTotal { get => custoTotal; set => custoTotal = value; }
        [Anotacao("ID Produto Produzido")]
        public virtual string IdProdutoProduzido { get => idProdutoProduzido; set => idProdutoProduzido = value; }
        [Anotacao("Produzir na Venda")]
        public virtual bool ProduzirNaVenda { get => produzirNaVenda; set => produzirNaVenda = value; }
    }
}
