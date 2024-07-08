using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Ordem Serviço Produto")]
    public class OrdemServicoProduto : ObjetoPadrao
    {
        private int id;
        private string descricaoProduto;
        private decimal valorUnitario;
        private decimal desconto;
        private decimal acrescimo;
        private decimal valorTotal;
        private double quantidade;
        private Produto produto;
        private OrdemServico ordemServico;
        private Pessoa vendedor;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string DescricaoProduto { get => descricaoProduto; set => descricaoProduto = value; }
        [Anotacao("Valor Unitário")]
        public virtual decimal ValorUnitario { get => valorUnitario; set => valorUnitario = value; }
        [Anotacao("Desconto")]
        public virtual decimal Desconto { get => desconto; set => desconto = value; }
        [Anotacao("Acrescimo")]
        public virtual decimal Acrescimo { get => acrescimo; set => acrescimo = value; }
        [Anotacao("ValorTotal")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Ordem Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
        [Anotacao("Vendedor")]
        public virtual Pessoa Vendedor { get => vendedor; set => vendedor = value; }

        public override string ToString()
        {
            return descricaoProduto;
        }
    }
}
