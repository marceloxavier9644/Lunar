using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Tabela de Preço")]
    public class TabelaPreco : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Tabela de Preço")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Emoresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
