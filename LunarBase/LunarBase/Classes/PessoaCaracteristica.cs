using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Pessoa Caracteristicas")]
    public class PessoaCaracteristica : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private int ordenacao;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Característica")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Ordenação")]
        public virtual int Ordenacao { get => ordenacao; set => ordenacao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
