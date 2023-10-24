using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Permissão")]
    public class PermissaoUsuario : ObjetoPadrao
    {
        private int id;
        private string permissoes;
        private GrupoUsuario grupoUsuario;
        private EmpresaFilial empresaFilial;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Permissões")]
        public virtual string Permissoes { get => permissoes; set => permissoes = value; }
        [Anotacao("Grupo de Usuário")]
        public virtual GrupoUsuario GrupoUsuario { get => grupoUsuario; set => grupoUsuario = value; }
        [Anotacao("Empresa")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
    }
}
