using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Conta")]
    public class AtendimentoConta : ObjetoPadrao
    {
        private int id;
        private Pessoa cliente;
        private string nomeCliente;
        private int atendimentoId;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Nome Cliente")]
        public virtual string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        [Anotacao("Atendimento Id")]
        public virtual int AtendimentoId { get => atendimentoId; set => atendimentoId = value; }
    }
}
