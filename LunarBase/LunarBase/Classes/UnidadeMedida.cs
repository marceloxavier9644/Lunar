using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Unidade de Medida")]
    public class UnidadeMedida : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string sigla;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Unidade de Medida")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Unidade")]
        public virtual string Sigla { get => sigla; set => sigla = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
