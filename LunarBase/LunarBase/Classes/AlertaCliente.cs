using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Alerta Cliente")]
    public class AlertaCliente : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private DateTime data;
        private Pessoa pessoa;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Mensagem")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
