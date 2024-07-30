using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Contas a Receber Recebidas")]
    public class ContaReceberRecebida : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private ContaReceber contaReceber;   
        private decimal valorRecebido;
        private decimal valorOriginal;
        private decimal valorOriginalComJuros;
        private decimal valorDesconto;
        private decimal valorAcrescimo;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descricao")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Conta Receber")]
        public virtual ContaReceber ContaReceber { get => contaReceber; set => contaReceber = value; }      
        [Anotacao("Valor Recebido")]
        public virtual decimal ValorRecebido { get => valorRecebido; set => valorRecebido = value; }
        [Anotacao("Valor Original")]
        public virtual decimal ValorOriginal { get => valorOriginal; set => valorOriginal = value; }
        [Anotacao("Valor Original com Juros")]
        public virtual decimal ValorOriginalComJuros { get => valorOriginalComJuros; set => valorOriginalComJuros = value; }
        [Anotacao("Valor Desconto")]
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        [Anotacao("Valor Acrescimo")]
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
