using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("CCe")]
    public class NfeCce : ObjetoPadrao
    {
        private int id;
        private string correcao;
        private int sequencia;
        private string protocolo;
        private Nfe nfe;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Correção")]
        public virtual string Correcao { get => correcao; set => correcao = value; }
        [Anotacao("NFe")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
        [Anotacao("Sequência")]
        public virtual int Sequencia { get => sequencia; set => sequencia = value; }
        [Anotacao("Protocolo")]
        public virtual string Protocolo { get => protocolo; set => protocolo = value; }

        public override string ToString()
        {
            return correcao;
        }
    }
}
