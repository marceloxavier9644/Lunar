using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Bandeira Cartão")]
    public class BandeiraCartao : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string codigoSefaz;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Bandeira")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Código Sefaz")]
        public virtual string CodigoSefaz { get => codigoSefaz; set => codigoSefaz = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
