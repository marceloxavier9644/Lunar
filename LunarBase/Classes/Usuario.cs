using LunarBase.Anotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Usuário")]
    public class Usuario : ObjetoPadrao
    {
        private int id;
        private string login;
        private string senha;
        private string email;
        private EmpresaFilial empresaFilial;
        private GrupoUsuario grupoUsuario;

        [Anotacao("Código")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Login")]
        public virtual string Login { get => login; set => login = value; }
        [Anotacao("Senha")]
        [OcultarEmGridsEPesquisas]
        public virtual string Senha { get => senha; set => senha = value; }
        [Anotacao("E-mail")]
        public virtual string Email { get => email; set => email = value; }
        [Anotacao("Filial de Trabalho")]
        public virtual EmpresaFilial EmpresaFilial { get => empresaFilial; set => empresaFilial = value; }
        [Anotacao("Grupo de Permissões")]
        public virtual GrupoUsuario GrupoUsuario { get => grupoUsuario; set => grupoUsuario = value; }

        public override string ToString()
        {
            return login;
        }
    }
}
