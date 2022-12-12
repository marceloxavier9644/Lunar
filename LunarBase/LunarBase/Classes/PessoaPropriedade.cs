using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Propriedades")]
    public class PessoaPropriedade : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string inscricaoEstadual;
        private Endereco endereco;
        private Pessoa pessoa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Inscrição Estadual")]
        public virtual string InscricaoEstadual { get => inscricaoEstadual; set => inscricaoEstadual = value; }
        [Anotacao("Endereço")]
        public virtual Endereco Endereco { get => endereco; set => endereco = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
