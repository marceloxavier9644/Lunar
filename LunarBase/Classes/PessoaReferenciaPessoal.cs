using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Referência Pessoal")]
    public class PessoaReferenciaPessoal : ObjetoPadrao
    {
        private int id;
        private string nome;
        private string telefone;
        private string grau;
        private Pessoa pessoa;
        private string observacoes;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Nome")]
        public virtual string Nome { get => nome; set => nome = value; }
        [Anotacao("Telefone")]
        public virtual string Telefone { get => telefone; set => telefone = value; }
        [Anotacao("Grau")]
        public virtual string Grau { get => grau; set => grau = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }


        public override string ToString()
        {
            return nome;
        }
    }
}
