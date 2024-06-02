using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Condicional")]
    public class Condicional : ObjetoPadrao
    {
        private int id;
        private DateTime data;
        private DateTime dataEncerramento;
        private DateTime dataPrevisao;
        private double qtdPeca;
        private decimal valorTotal;
        private Pessoa cliente;
        private EmpresaFilial filial;
        private string observacoes;
        private bool encerrado;
        private Pessoa vendedor;
        private decimal valorSaldo;
        private Venda venda;
        private double quantidadeDevolvida;
       

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Anotacao("Data Encerramento")]
        public virtual DateTime DataEncerramento { get => dataEncerramento; set => dataEncerramento = value; }
        [Anotacao("Qtd Peças")]
        public virtual double QtdPeca { get => qtdPeca; set => qtdPeca = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial Filial { get => filial; set => filial = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Encerrado")]
        public virtual bool Encerrado { get => encerrado; set => encerrado = value; }
        [Anotacao("Previsao Devolução")]
        public virtual DateTime DataPrevisao { get => dataPrevisao; set => dataPrevisao = value; }
        [Anotacao("Vendedor")]
        public virtual Pessoa Vendedor { get => vendedor; set => vendedor = value; }
        [Anotacao("Valor Saldo")]
        public virtual decimal ValorSaldo { get => valorSaldo; set => valorSaldo = value; }
        [Anotacao("Venda")]
        public virtual Venda Venda { get => venda; set => venda = value; }

        [Anotacao("Devolvido")]
        [OcultarEmGridsEPesquisas]
        public virtual double QuantidadeDevolvida { get => quantidadeDevolvida; set => quantidadeDevolvida = value; }


    }
}
