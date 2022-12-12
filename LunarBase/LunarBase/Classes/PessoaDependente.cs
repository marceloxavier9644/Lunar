using LunarBase.Anotations;
using System.ComponentModel.DataAnnotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Dependentes")]
    public class PessoaDependente : ObjetoPadrao
    {
        private int id;
        private Pessoa pessoa;
        private string nome;
        private string cpf;
        private string telefone;
        private DateTime dataNascimento;
        private string parentesco;
        private string observacoes;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Nome")]
        public virtual string Nome { get => nome; set => nome = value; }
        [Anotacao("Data Nascimento")]
        public virtual DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        [Anotacao("Parentesco")]
        public virtual string Parentesco { get => parentesco; set => parentesco = value; }
        [Anotacao("Observacoes")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("CPF")]
        public virtual string Cpf { get => cpf; set => cpf = value; }
        [Anotacao("Fone")]
        public virtual string Telefone { get => telefone; set => telefone = value; }

        public override string ToString()
        {
            return nome;
        }
    }
}
