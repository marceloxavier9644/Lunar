using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Condicional Produto")]
    public class CondicionalProduto : ObjetoPadrao
    {
        private int id;
        private Produto produto;
        private string codigoProduto;
        private string descricaoProduto;
        private decimal desconto;
        private decimal acrescimo;
        private decimal valorUnitario;
        private double quantidade;
        private decimal valorTotal;
        private bool devolvido;
        private DateTime dataDevolucao;
        private Condicional condicional;

        [Anotacao("ID")]
        [OcultarEmGridsEPesquisas]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Valor Produto")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Devolvido")]
        [OcultarEmGridsEPesquisas]
        public virtual bool Devolvido { get => devolvido; set => devolvido = value; }
        [Anotacao("Data Devolução")]
        [OcultarEmGridsEPesquisas]
        public virtual DateTime DataDevolucao { get => dataDevolucao; set => dataDevolucao = value; }
        [Anotacao("Condicional")]
        public virtual Condicional Condicional { get => condicional; set => condicional = value; }
        [Anotacao("Código Produto")]
        public virtual string CodigoProduto { get => codigoProduto; set => codigoProduto = value; }
        [Anotacao("Descricao Produto")]
        public virtual string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }
        [Anotacao("Desconto")]
        public virtual decimal Desconto { get => desconto; set => desconto = value; }
        [Anotacao("Acréscimo")]
        public virtual decimal Acrescimo { get => acrescimo; set => acrescimo = value; }
        [Anotacao("Valor Unitário")]
        public virtual decimal ValorUnitario { get => valorUnitario; set => valorUnitario = value; }
    }
}
