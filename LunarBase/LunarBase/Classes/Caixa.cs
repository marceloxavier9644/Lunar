using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Caixa")]
    public class Caixa : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private decimal valor;
        private string tipo;
        private string tabelaOrigem;
        private string idOrigem;
        private DateTime dataLancamento;
        private bool conciliado;
        private FormaPagamento formaPagamento;
        private PlanoConta planoConta;
        private EmpresaFilial empresaFilial;
        private Usuario usuario;
        private ContaBancaria contaBancaria;
        private bool concluido;
        private Pessoa pessoa;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Valor")]
        public virtual decimal Valor { get => valor; set => valor = value; }
        [Anotacao("Tipo")]
        public virtual string Tipo { get => tipo; set => tipo = value; }
        [Anotacao("Tabela Origem")]
        public virtual string TabelaOrigem { get => tabelaOrigem; set => tabelaOrigem = value; }
        [Anotacao("ID Origem")]
        public virtual string IdOrigem { get => idOrigem; set => idOrigem = value; }
        [Anotacao("Data Lançamento")]
        public virtual DateTime DataLancamento { get => dataLancamento; set => dataLancamento = value; }
        [Anotacao("Conciliado")]
        public virtual bool Conciliado { get => conciliado; set => conciliado = value; }
        [Anotacao("Forma de Pagamento")]
        public virtual FormaPagamento FormaPagamento { get => formaPagamento; set => formaPagamento = value; }
        [Anotacao("Plano de Conta")]
        public virtual PlanoConta PlanoConta { get => planoConta; set => planoConta = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Usuário")]
        public virtual Usuario Usuario { get => usuario; set => usuario = value; }
        [Anotacao("Conta Bancaria")]
        public virtual ContaBancaria ContaBancaria { get => contaBancaria; set => contaBancaria = value; }
        [Anotacao("Concluido")]
        public virtual bool Concluido { get => concluido; set => concluido = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
