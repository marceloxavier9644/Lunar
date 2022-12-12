using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Campos")]
    public class ModuloCampo : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private Modulo modulo;
        private ModuloGrupo grupo;
        private Empresa empresa;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Campo")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Módulo")]
        public virtual Modulo Modulo { get => modulo; set => modulo = value; }
        [Anotacao("Grupo")]
        public virtual ModuloGrupo Grupo { get => grupo; set => grupo = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
