using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Cartão Movimento")]
    public class CartaoMovimento : ObjetoPadrao, ICartaoMovimento
    {
        private int id;
        private string descricao;
        private FormaPagamento formaPagamento;
        private AdquirenteCartao adquirenteCartao;
        private Parcelamento parcelamento;
        private BandeiraCartao bandeiraCartao;
        private DateTime dataLancamento;
        private DateTime dataCredito;
        private double taxa;
        private bool venda;
        private bool recebimento;
        private EmpresaFilial empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Forma Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Adquirente Cartão")]
        public virtual AdquirenteCartao AdquirenteCartao { get => adquirenteCartao; set => adquirenteCartao = value; }
        [Anotacao("Parcelamento")]
        public virtual Parcelamento Parcelamento { get => parcelamento; set => parcelamento = value; }
        [Anotacao("Bandeira")]
        public virtual BandeiraCartao BandeiraCartao { get => bandeiraCartao; set => bandeiraCartao = value; }
        [Anotacao("Data de Lançamento")]
        public virtual DateTime DataLancamento { get => dataLancamento; set => dataLancamento = value; }
        [Anotacao("Data de Crédito")]
        public virtual DateTime DataCredito { get => dataCredito; set => dataCredito = value; }
        [Anotacao("Taxa")]
        public virtual double Taxa { get => taxa; set => taxa = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Venda")]
        public virtual bool Venda { get => venda; set => venda = value; }
        [Anotacao("Recebimento")]
        public virtual bool Recebimento { get => recebimento; set => recebimento = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
