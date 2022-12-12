using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Conta Recebida")]
    public class ContaRecebidaFormaPagamento : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private FormaPagamento formaPagamento;
        private ContaBancaria contaBancaria;
        private Parcelamento parcelamento;
        private BandeiraCartao bandeiraCartao;
        private ContaReceber contaReceber;
        private decimal valorOriginal;
        private decimal valorJuro;
        private decimal valorDesconto;
        private decimal valorAcrescimo;
        private decimal valorTotalRecebido;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Forma Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Conta Bancária")]
        public virtual ContaBancaria ContaBancaria { get => contaBancaria; set => contaBancaria = value; }
        [Anotacao("Parcelamento")]
        public virtual Parcelamento Parcelamento { get => parcelamento; set => parcelamento = value; }
        [Anotacao("Bandeira Cartão")]
        public virtual BandeiraCartao BandeiraCartao { get => bandeiraCartao; set => bandeiraCartao = value; }
        [Anotacao("Conta Receber")]
        public virtual ContaReceber ContaReceber { get => contaReceber; set => contaReceber = value; }
        [Anotacao("Valor Original")]
        public virtual decimal ValorOriginal { get => valorOriginal; set => valorOriginal = value; }
        [Anotacao("Valor Juro")]
        public virtual decimal ValorJuro { get => valorJuro; set => valorJuro = value; }
        [Anotacao("Valor Total Recebido")]
        public virtual decimal ValorTotalRecebido { get => valorTotalRecebido; set => valorTotalRecebido = value; }
        [Anotacao("Valor Desconto")]
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        [Anotacao("Valor Acréscimo")]
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
