using static LunarBase.ClassesDAO.EstoqueDAO;

namespace LunarBase.Classes
{
    public class Estoque : ObjetoPadrao
    {
        private int id;
        private double quantidade;
        private string origem;
        private bool conciliado;
        private bool entrada;
        private bool saida;
        private Produto produto;
        private EmpresaFilial empresaFilial;
        private DateTime dataEntradaSaida;
        private string descricao;
        private double quantidadeInventario;
        private Pessoa pessoa;
        private BalancoEstoque balancoEstoque;

        public virtual int Id { get => id; set => id = value; }
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        public virtual string Origem { get => origem; set => origem = value; }
        public virtual bool Conciliado { get => conciliado; set => conciliado = value; }
        public virtual bool Entrada { get => entrada; set => entrada = value; }
        public virtual bool Saida { get => saida; set => saida = value; }
        public virtual Produto Produto { get => produto; set => produto = value; }
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        public virtual DateTime DataEntradaSaida { get => dataEntradaSaida; set => dataEntradaSaida = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public virtual double QuantidadeInventario { get => quantidadeInventario; set => quantidadeInventario = value; }
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        public virtual BalancoEstoque BalancoEstoque { get => balancoEstoque; set => balancoEstoque = value; }

        //public virtual EstoqueX EstoqueX { get => estoqueX; set => estoqueX = value; }




        public Estoque()
        {
        }
        public Estoque(double quantidadeInventario, Produto produto)
        {
            this.quantidade = quantidadeInventario;
            this.produto = produto;
        }

        public class Inventario
        {
            public string ncm { get; set; }
            public string codigo { get; set; }
            public string descricao { get; set; }
            public string codigoBarras { get; set; }
            public string csosn { get; set; }
            public string medida { get; set; }
            public double quantidadeInventario { get; set; }
            public decimal valorCusto { get; set; }
            public decimal valorTotal { get; set; }
            public Produto produto { get; set; }

        }
    }
}
