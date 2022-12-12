using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Natureza Operação")]
    public class NaturezaOperacao : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private bool movimentaEstoque;
        private bool movimentaFinanceiro;
        private string entradaSaida;
        private string finalidadeNfe;


        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Movimenta Estoque")]
        public virtual bool MovimentaEstoque { get => movimentaEstoque; set => movimentaEstoque = value; }
        [Anotacao("Movimenta Financeiro")]
        public virtual bool MovimentaFinanceiro { get => movimentaFinanceiro; set => movimentaFinanceiro = value; }
     
        [Anotacao("E/S")]
        public virtual string EntradaSaida { get => entradaSaida; set => entradaSaida = value; }
        [Anotacao("Finalidade NFe")]
        public virtual string FinalidadeNfe { get => finalidadeNfe; set => finalidadeNfe = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
