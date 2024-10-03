using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Contas a Receber")]
    public class ContaReceber : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string parcela;
        private DateTime data;
        private DateTime vencimento;
        private string nomeCliente;
        private string cnpjCliente;
        private string enderecoCliente;
        private decimal valorParcela;
        private decimal multa;
        private decimal juro;
        private decimal valorTotal;
        private bool recebido;
        private Pessoa cliente;
        private Venda venda;
        private OrdemServico ordemServico;
        private VendaFormaPagamento vendaFormaPagamento;
        private EmpresaFilial empresaFilial;
        private FormaPagamento formaPagamento;
        private PlanoConta planoConta;
        private string origem;
        private bool concluido;
        private string documento;
        private DateTime dataRecebimento;
        private string descricaoRecebimento;
        private string caixaRecebimento;
        private decimal valorRecebido;
        private decimal descontoRecebidoBaixa;
        private decimal acrescimoRecebidoBaixa;
        private decimal valorRecebimentoParcial;
        private decimal valorTotalOrigem;
        private bool boletoGerado;
        private string idBoleto;
        private string nossoNumero;
        private string linhaDigitavel;
        private string codigoBarras;
        private string txid;
        private string qrCode;
        private ContaBancaria contaBoleto;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Parcela")]
        public virtual string Parcela { get => parcela; set => parcela = value; }
        [Anotacao("Data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Anotacao("Vencimento")]
        public virtual DateTime Vencimento { get => vencimento; set => vencimento = value; }
        [Anotacao("Nome Cliente")]
        public virtual string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        [Anotacao("Cnpj Cliente")]
        public virtual string CnpjCliente { get => cnpjCliente; set => cnpjCliente = value; }
        [Anotacao("Endereço")]
        public virtual string EnderecoCliente { get => enderecoCliente; set => enderecoCliente = value; }
        [Anotacao("Valor Parcela")]
        public virtual decimal ValorParcela { get => valorParcela; set => valorParcela = value; }
        [Anotacao("Recebido?")]
        public virtual bool Recebido { get => recebido; set => recebido = value; }
        [Anotacao("Venda")]
        public virtual Venda Venda { get => venda; set => venda = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Forma Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("VendaFormaPagamento")]
        public virtual VendaFormaPagamento VendaFormaPagamento { get => vendaFormaPagamento; set => vendaFormaPagamento = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Origem")]
        public virtual string Origem { get => origem; set => origem = value; }
        [Anotacao("Concluido?")]
        public virtual bool Concluido { get => concluido; set => concluido = value; }
        [Anotacao("Multa")]
        public virtual decimal Multa { get => multa; set => multa = value; }
        [Anotacao("Juro")]
        public virtual decimal Juro { get => juro; set => juro = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Documento")]
        public virtual string Documento { get => documento; set => documento = value; }
        [Anotacao("Data Recebimento")]
        public virtual DateTime DataRecebimento { get => dataRecebimento; set => dataRecebimento = value; }
        [Anotacao("Descrição Recebimento")]
        public virtual string DescricaoRecebimento { get => descricaoRecebimento; set => descricaoRecebimento = value; }
        [Anotacao("Caixa Recebimento")]
        public virtual string CaixaRecebimento { get => caixaRecebimento; set => caixaRecebimento = value; }
        [Anotacao("Valor Recebido")]
        public virtual decimal ValorRecebido { get => valorRecebido; set => valorRecebido = value; }
        [Anotacao("Desconto Recebido")]
        public virtual decimal DescontoRecebidoBaixa { get => descontoRecebidoBaixa; set => descontoRecebidoBaixa = value; }
        [Anotacao("Acrescimo Recebido")]
        public virtual decimal AcrescimoRecebidoBaixa { get => acrescimoRecebidoBaixa; set => acrescimoRecebidoBaixa = value; }
        [Anotacao("Plano de Conta")]
        public virtual PlanoConta PlanoConta { get => planoConta; set => planoConta = value; }
        [Anotacao("Ordem de Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }
        [Anotacao("Valor Recebimento Parcial")]
        public virtual decimal ValorRecebimentoParcial { get => valorRecebimentoParcial; set => valorRecebimentoParcial = value; }
        [Anotacao("Valor Total Origem")]
        [OcultarEmGridsEPesquisas]
        public virtual decimal ValorTotalOrigem { get => valorTotalOrigem; set => valorTotalOrigem = value; }
        [Anotacao("Boleto Gerado")]
        [OcultarEmGridsEPesquisas]
        public virtual bool BoletoGerado { get => boletoGerado; set => boletoGerado = value; }
        [Anotacao("Boleto ID")]
        [OcultarEmGridsEPesquisas]
        public virtual string IdBoleto { get => idBoleto; set => idBoleto = value; }
        [Anotacao("Nosso Numero")]
        public virtual string NossoNumero { get => nossoNumero; set => nossoNumero = value; }
        [Anotacao("Linha Digitavel")]
        public virtual string LinhaDigitavel { get => linhaDigitavel; set => linhaDigitavel = value; }
        [Anotacao("Codigo Barras")]
        public virtual string CodigoBarras { get => codigoBarras; set => codigoBarras = value; }
        [Anotacao("Txid")]
        public virtual string Txid { get => txid; set => txid = value; }
        [Anotacao("QR Code")]
        public virtual string QrCode { get => qrCode; set => qrCode = value; }
        [Anotacao("Conta Boleto")]
        public virtual ContaBancaria ContaBoleto { get => contaBoleto; set => contaBoleto = value; }

        public override string ToString()
        {
            return descricao;
        }
        //plano de contas
        //
    }
}
