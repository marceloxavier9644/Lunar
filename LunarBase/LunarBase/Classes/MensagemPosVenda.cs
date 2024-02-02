using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Mensagem Pos Venda")]
    public class MensagemPosVenda : ObjetoPadrao
    {
        private int id;
        private DateTime dataAgendamento;
        private string nomeCliente;
        private Pessoa pessoa;
        private bool flagEnviada;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data")]
        public virtual DateTime DataAgendamento { get => dataAgendamento; set => dataAgendamento = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Enviada")]
        public virtual bool FlagEnviada { get => flagEnviada; set => flagEnviada = value; }
        [Anotacao("Nome Cliente")]
        public virtual string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
    }
}
