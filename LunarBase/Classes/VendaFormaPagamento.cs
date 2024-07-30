using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Venda Pagamento")]
    public class VendaFormaPagamento : ObjetoPadrao
    {
        private int id;
        private Venda venda;
        private FormaPagamento formaPagamento;
        private bool cartao;
        private string autorizacaoCartao;
        private AdquirenteCartao adquirenteCartao;
        private int parcelamento;
        private BandeiraCartao bandeiraCartao;
        private decimal valorRecebido;
        private ContaBancaria contaBancaria;
        private string tipoCartao;
        private Parcelamento parcelamentoFk;
        private decimal troco;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Venda")]
        public virtual Venda Venda { get => venda; set => venda = value; }
        [Anotacao("Forma de Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Valor Recebido")]
        public virtual decimal ValorRecebido { get => valorRecebido; set => valorRecebido = value; }
        [Anotacao("Cartão")]
        public virtual bool Cartao { get => cartao; set => cartao = value; }
        [Anotacao("Autorização")]
        public virtual string AutorizacaoCartao { get => autorizacaoCartao; set => autorizacaoCartao = value; }
        [Anotacao("Adquirente")]
        public virtual AdquirenteCartao AdquirenteCartao { get => adquirenteCartao; set => adquirenteCartao = value; }
        [Anotacao("Parcelamento")]
        public virtual int Parcelamento { get => parcelamento; set => parcelamento = value; }
        [Anotacao("Bandeira")]
        public virtual BandeiraCartao BandeiraCartao { get => bandeiraCartao; set => bandeiraCartao = value; }
        [Anotacao("Conta Bancária")]
        public virtual ContaBancaria ContaBancaria { get => contaBancaria; set => contaBancaria = value; }
        [Anotacao("Tipo Cartão")]
        public virtual string TipoCartao { get => tipoCartao; set => tipoCartao = value; }
        [Anotacao("Parcelamento")]
        public virtual Parcelamento ParcelamentoFk { get => parcelamentoFk; set => parcelamentoFk = value; }
        public virtual decimal Troco { get => troco; set => troco = value; }
    }
}
