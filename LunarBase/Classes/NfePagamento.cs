using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Nfe Pagamento")]
    public class NfePagamento : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string origem;
        private FormaPagamento formaPagamento;
        private Nfe nfe;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descricao")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Origem")]
        public virtual string Origem { get => origem; set => origem = value; }
        [Anotacao("Forma de Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("NFe")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
