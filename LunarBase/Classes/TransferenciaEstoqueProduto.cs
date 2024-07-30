using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("TransferenciaEstoqueProduto")]
    public class TransferenciaEstoqueProduto : ObjetoPadrao
    {
        private int id;
        private Produto produto;
        private double quantidade;
        private decimal valorUnitario;
        private decimal desconto;
        private decimal acrescimo;
        private decimal valorFinal;
        private TransferenciaEstoque transferenciaEstoque;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("ValorUnitario")]
        public virtual decimal ValorUnitario { get => valorUnitario; set => valorUnitario = value; }
        [Anotacao("Desconto")]
        public virtual decimal Desconto { get => desconto; set => desconto = value; }
        [Anotacao("Acrescimo")]
        public virtual decimal Acrescimo { get => acrescimo; set => acrescimo = value; }
        [Anotacao("ValorFinal")]
        public virtual decimal ValorFinal { get => valorFinal; set => valorFinal = value; }
        [Anotacao("Transferencia Estoque")]
        public virtual TransferenciaEstoque TransferenciaEstoque { get => transferenciaEstoque; set => transferenciaEstoque = value; }
    }
}
