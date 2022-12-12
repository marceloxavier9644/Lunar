using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Módulo")]
    public class ModuloGrupo : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Módulo")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
