using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Vendas")]
    public class Venda : ObjetoPadrao
    {
        private int id;
        private DateTime dataVenda;
        private decimal valorProdutos;
        private decimal valorDesconto;
        private decimal valorAcrescimo;
        private decimal valorFinal;
        private string enderecoCliente;
        private string nomeCliente;   
        private Pessoa cliente;
        private EmpresaFilial empresaFilial;
        private Pessoa vendedor;
        private PlanoConta planoConta;
        private string observacoes;
        private bool cancelado;
        private string nomeComputador;
        private bool concluida;
        private Nfe nfe;
        private int quantidade;
        private PessoaDependente pessoaDependente;
        private string qrCodePix;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data da Venda")]
        public virtual DateTime DataVenda { get => dataVenda; set => dataVenda = value; }
        [Anotacao("Valor Produtos")]
        public virtual decimal ValorProdutos { get => valorProdutos; set => valorProdutos = value; }
        [Anotacao("Desconto")]
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        [Anotacao("Acrescimo")]
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }
        [Anotacao("Valot Final")]
        public virtual decimal ValorFinal { get => valorFinal; set => valorFinal = value; }
        [Anotacao("Endereço")]
        public virtual string EnderecoCliente { get => enderecoCliente; set => enderecoCliente = value; }
        [Anotacao("Nome Cliente")]
        public virtual string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Vendedor")]
        public virtual Pessoa Vendedor { get => vendedor; set => vendedor = value; }
        [Anotacao("Plano de Contas")]
        public virtual PlanoConta PlanoConta { get => planoConta; set => planoConta = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Cancelado?")]
        public virtual bool Cancelado { get => cancelado; set => cancelado = value; }
        [Anotacao("Computador")]
        public virtual string NomeComputador { get => nomeComputador; set => nomeComputador = value; }
        [Anotacao("Concluída?")]
        public virtual bool Concluida { get => concluida; set => concluida = value; }
        [Anotacao("NFe?")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
        [Anotacao("Quantidade")]
        [OcultarEmGridsEPesquisas]
        public virtual int Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Dependente")]
        [OcultarEmGridsEPesquisas]
        public virtual PessoaDependente PessoaDependente { get => pessoaDependente; set => pessoaDependente = value; }
        [Anotacao("QR CODE")]
        [OcultarEmGridsEPesquisas]
        public virtual string QrCodePix { get => qrCodePix; set => qrCodePix = value; }

        //Pre venda
        //Condicional

        //Contrutor para conseguir puxar SUM na consulta DAO
        public Venda()
        {
        }

        public Venda(decimal valorFinal, Pessoa vendedor, int quantidade)
        {
            this.quantidade = quantidade;
            this.valorFinal = valorFinal;
            this.vendedor = vendedor;
        }
    }
}
