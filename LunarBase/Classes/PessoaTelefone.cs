using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Telefone")]
    public class PessoaTelefone : ObjetoPadrao
    {
        private int id;
        private string ddd;
        private string telefone;
        private string observacoes;
        private Pessoa pessoa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("DDD")]
        public virtual string Ddd { get => ddd; set => ddd = value; }
        [Anotacao("Telefone")]
        public virtual string Telefone { get => telefone; set => telefone = value; }
        [Anotacao("Observacoes")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }

        public override string ToString()
        {
            return telefone;
        }
    }
}
