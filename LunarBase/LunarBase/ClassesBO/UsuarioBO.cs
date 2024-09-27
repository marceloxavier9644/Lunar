using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class UsuarioBO : BO
    {
        private UsuarioDAO dao;

        public UsuarioBO()
        {
            dao = new UsuarioDAO();
        }

        public void salvar(ObjetoPadrao usuario)
        {
            Boolean excluido = usuario.FlagExcluido;

            if (valida((Usuario)usuario))
            {
                if (((Usuario)usuario).Id == 0)
                    dao.incluir((Usuario)usuario);
                else
                    dao.alterar((Usuario)usuario);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Usuario)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Usuario não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new UsuarioDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Usuario!" + e.Message);
            }

        }

        private Boolean valida(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Login))
            {
                throw new Exception("O campo \"Login\" é obrigatório!");
            }
            if (usuario.GrupoUsuario == null)
            {
                throw new Exception("O campo \"Grupo de Usuário\" é obrigatório!");
            }
            return true;
        }
        public void salvarSeNaoExistir(Usuario usuario)
        {
            try
            {
                Usuario usuarioAux = (Usuario)dao.Selecionar(usuario, ((Usuario)usuario).Id);
                if (usuarioAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(usuario.Login))
                {
                    throw new Exception("O campo \"Login\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(usuario.Senha))
                {
                    throw new Exception("O campo \"Senha\" é obrigatório!");
                }
                dao.incluir((Usuario)usuario);
            }
        }

        public Usuario selecionarUsuarioPorUsuarioESenha(Usuario usuario)
        {
            try
            {
                usuario = dao.selecionarPorUsuarioESenha(usuario);
                if (usuario.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception("Usuário não encontrado!" + e.ToString());
            }
        }

        public IList<Usuario> selecionarTodosUsuarios()
        {
            try
            {
                return dao.selecionarTodosUsuarios();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Usuario! Erro: " + e.Message);
            }
        }

        public IList<Usuario> selecionarTodosUsuariosComNotificoes()
        {
            try
            {
                return dao.selecionarTodosUsuariosComNotificoes();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Usuario! Erro: " + e.Message);
            }
        }

        public IList<Usuario> selecionarUsuarioComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarUsuarioComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Usuario! Erro: " + e.Message);
            }
        }
    }
}