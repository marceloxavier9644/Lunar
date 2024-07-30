using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Balanço Estoque Produto")]
    public class BalancoEstoqueProduto : ObjetoPadrao
    {
        private int id;
        private BalancoEstoque balancoEstoque;
        private Produto produto;
        private string descricaoProduto;
        private double quantidade;
        private String tipo; //e/s
        private bool conciliado;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Balanço de Estoque")]
        public virtual BalancoEstoque BalancoEstoque { get => balancoEstoque; set => balancoEstoque = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Descrição do Produto")]
        public virtual string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Tipo")]
        public virtual string Tipo { get => tipo; set => tipo = value; }
        [Anotacao("Conciliado")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Conciliado { get => conciliado; set => conciliado = value; }
    }
}
