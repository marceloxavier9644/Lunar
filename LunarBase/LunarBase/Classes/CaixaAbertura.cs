using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Caixa Abertura")]
    public class CaixaAbertura : ObjetoPadrao
    {
        private int id;
        private DateTime dataAbertura;
        private DateTime dataFechamento;
        private Usuario usuario;
        private decimal saldoInicial;
        private decimal saldoFinal;
        private decimal diferencaFechamento;
        private string status;
        private int idCaixaAnterior;
        private EmpresaFilial empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data Abertura")]
        public virtual DateTime DataAbertura { get => dataAbertura; set => dataAbertura = value; }
        [Anotacao("Data Fechamento")]
        public virtual DateTime DataFechamento { get => dataFechamento; set => dataFechamento = value; }
        [Anotacao("Usuário")]
        public virtual Usuario Usuario { get => usuario; set => usuario = value; }
        [Anotacao("Saldo Inicial")]
        public virtual decimal SaldoInicial { get => saldoInicial; set => saldoInicial = value; }
        [Anotacao("Saldo Final")]
        public virtual decimal SaldoFinal { get => saldoFinal; set => saldoFinal = value; }
        [Anotacao("Diferença")]
        public virtual decimal DiferencaFechamento { get => diferencaFechamento; set => diferencaFechamento = value; }
        [Anotacao("Status")]
        public virtual string Status { get => status; set => status = value; }
        [Anotacao("Caixa Anterior")]
        public virtual int IdCaixaAnterior { get => idCaixaAnterior; set => idCaixaAnterior = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }

    }
}
