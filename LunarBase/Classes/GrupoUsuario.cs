using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Grupo de Usuário")]
    public class GrupoUsuario : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string permissoes;
        private bool supervisor;
        private Empresa empresa = new Empresa();

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Grupo")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Empresa")]
        [OcultarEmGridsEPesquisas]
        public virtual Empresa Empresa { get => empresa; set => empresa = value; }
        [Anotacao("Permissões")]
        [OcultarEmGridsEPesquisas]
        public virtual string Permissoes { get => permissoes; set => permissoes = value; }
        [Anotacao("Supervisor")]
        public virtual bool Supervisor { get => supervisor; set => supervisor = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
}
