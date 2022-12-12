using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Contas a Pagar")]
    public class ContaPagar : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private DateTime dataOrigem;
        private string numeroDocumento;
        private decimal valorTotal;
        private DateTime dVenc;
        private string nDup;
        private decimal vDup;
        private Pessoa pessoa = new Pessoa();
        private FormaPagamento formaPagamento;
        private Nfe nfe;
        private PlanoConta planoConta;
        private EmpresaFilial empresaFilial;
        private bool pago;
        private DateTime dataPagamento;
        private String descricaoPagamento;
        private String caixaPagamento;
        private decimal valorPago;
        private decimal descontoBaixa;
        private decimal acrescimoBaixa;
        private string historico;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Data Origem")]
        public virtual DateTime DataOrigem { get => dataOrigem; set => dataOrigem = value; }
        [Anotacao("Número do Documento")]
        public virtual string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Data de Vencimento")]
        public virtual DateTime DVenc { get => dVenc; set => dVenc = value; }
        [Anotacao("Número da Parcela")]
        public virtual string NDup { get => nDup; set => nDup = value; }
        [Anotacao("Valor Parcela")]
        public virtual decimal VDup { get => vDup; set => vDup = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Forma de Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Nota Fiscal")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
        [Anotacao("Plano de Contas")]
        public virtual PlanoConta PlanoConta { get => planoConta; set => planoConta = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Pago")]
        public virtual bool Pago { get => pago; set => pago = value; }
        [Anotacao("Data Pagamento")]
        public virtual DateTime DataPagamento { get => dataPagamento; set => dataPagamento = value; }
        [Anotacao("Descrição Pagamento")]
        public virtual string DescricaoPagamento { get => descricaoPagamento; set => descricaoPagamento = value; }
        [Anotacao("Caixa pagamento")]
        public virtual string CaixaPagamento { get => caixaPagamento; set => caixaPagamento = value; }
        [Anotacao("Valor Pago")]
        public virtual decimal ValorPago { get => valorPago; set => valorPago = value; }
        [Anotacao("Desconto Baixa")]
        public virtual decimal DescontoBaixa { get => descontoBaixa; set => descontoBaixa = value; }
        [Anotacao("Acrescimo Baixa")]
        public virtual decimal AcrescimoBaixa { get => acrescimoBaixa; set => acrescimoBaixa = value; }
        [Anotacao("Histórico")]
        public virtual string Historico { get => historico; set => historico = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
