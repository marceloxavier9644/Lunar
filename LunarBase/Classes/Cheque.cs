using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Cheque")]
    public class Cheque : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private decimal valor;
        private string parcela;
        private DateTime vencimento;
        private string numeroCheque;
        private Banco banco;
        private string agencia;
        private string conta;
        private string dvConta;
        private string cnpj;
        private string razaoSocial;
        private Venda venda;
        private EmpresaFilial empresaFilial;
        private Pessoa cliente;
        private bool concluido;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Valor")]
        public virtual decimal Valor { get => valor; set => valor = value; }
        [Anotacao("Parcela")]
        public virtual string Parcela { get => parcela; set => parcela = value; }
        [Anotacao("Vencimento")]
        public virtual DateTime Vencimento { get => vencimento; set => vencimento = value; }
        [Anotacao("Numero Cheque")]
        public virtual string NumeroCheque { get => numeroCheque; set => numeroCheque = value; }
        [Anotacao("Banco")]
        public virtual Banco Banco { get => banco; set => banco = value; }
        [Anotacao("Agência")]
        public virtual string Agencia { get => agencia; set => agencia = value; }
        [Anotacao("Conta")]
        public virtual string Conta { get => conta; set => conta = value; }
        [Anotacao("Dv Conta")]
        public virtual string DvConta { get => dvConta; set => dvConta = value; }
        [Anotacao("CNPJ")]
        public virtual string Cnpj { get => cnpj; set => cnpj = value; }
        [Anotacao("Razão Social")]
        public virtual string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        [Anotacao("Venda")]
        public virtual Venda Venda { get => venda; set => venda = value; }
        [Anotacao("Empresa Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Concluido?")]
        public virtual bool Concluido { get => concluido; set => concluido = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
