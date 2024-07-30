using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Ordem Serviço Pagamento")]
    public class OrdemServicoPagamento : ObjetoPadrao
    {
        private int id;
        private DateTime dataRecebimento;
        private decimal valorRecebido;
        private string tipoCartao;
        private string autorizacaoCartao;
        private string parcelas;
        private bool cartao;
        private decimal troco;
        private Parcelamento parcelamentoCartao;
        private FormaPagamento formaPagamento;
        private OrdemServico ordemServico;
        private AdquirenteCartao adquirenteCartao;
        private BandeiraCartao bandeiraCartao;
        private ContaBancaria contaBancaria;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data Recebimento")]
        public virtual DateTime DataRecebimento { get => dataRecebimento; set => dataRecebimento = value; }
        [Anotacao("Valor Recebido")]
        public virtual decimal ValorRecebido { get => valorRecebido; set => valorRecebido = value; }
        [Anotacao("Tipo Cartão")]
        public virtual string TipoCartao { get => tipoCartao; set => tipoCartao = value; }
        [Anotacao("Autorização")]
        public virtual string AutorizacaoCartao { get => autorizacaoCartao; set => autorizacaoCartao = value; }
        [Anotacao("Parcelas")]
        public virtual string Parcelas { get => parcelas; set => parcelas = value; }
        [Anotacao("Cartão")]
        public virtual bool Cartao { get => cartao; set => cartao = value; }
        [Anotacao("Troco")]
        public virtual decimal Troco { get => troco; set => troco = value; }
        [Anotacao("Parcelas Cartão")]
        public virtual Parcelamento ParcelamentoCartao { get => parcelamentoCartao; set => parcelamentoCartao = value; }
        [Anotacao("Forma Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Ordem Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
        [Anotacao("Adquirente")]
        public virtual AdquirenteCartao AdquirenteCartao { get => adquirenteCartao; set => adquirenteCartao = value; }
        [Anotacao("Bandeira")]
        public virtual BandeiraCartao BandeiraCartao { get => bandeiraCartao; set => bandeiraCartao = value; }
        [Anotacao("Conta Bancária")]
        public virtual ContaBancaria ContaBancaria { get => contaBancaria; set => contaBancaria = value; }
    }
}
