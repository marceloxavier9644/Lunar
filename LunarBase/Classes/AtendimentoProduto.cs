using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Produtos do Atendimento")]
    public class AtendimentoProduto : ObjetoPadrao
    {
        private int id;
        private Pessoa garcom;
        private Produto produto;
        private Servico servico;
        private decimal valorUnitario;
        private decimal desconto;
        private decimal acrescimo;
        private decimal valorTotal;
        private int atendimentoContaId;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Garçom")]
        public virtual Pessoa Garcom { get => garcom; set => garcom = value; }
        [Anotacao("Produto")]
        public virtual Produto Produto { get => produto; set => produto = value; }
        [Anotacao("Serviço")]
        public virtual Servico Servico { get => servico; set => servico = value; }
        [Anotacao("Valor Unitário")]
        public virtual decimal ValorUnitario { get => valorUnitario; set => valorUnitario = value; }
        [Anotacao("Desconto")]
        public virtual decimal Desconto { get => desconto; set => desconto = value; }
        [Anotacao("Acrescimo")]
        public virtual decimal Acrescimo { get => acrescimo; set => acrescimo = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Atendimento Conta ID")]
        public virtual int AtendimentoContaId { get => atendimentoContaId; set => atendimentoContaId = value; }
    }
}
