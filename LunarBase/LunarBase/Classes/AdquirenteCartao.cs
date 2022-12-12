using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Adquirente")]
    public class AdquirenteCartao : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string cnpj;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Adquirente")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("CNPJ")]
        public virtual string Cnpj { get => cnpj; set => cnpj = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
