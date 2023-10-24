using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PermissaoUsuarioBO : BO
    {
        private PermissaoUsuarioDAO dao;

        public PermissaoUsuarioBO()
        {
            dao = new PermissaoUsuarioDAO();
        }

        public void salvar(ObjetoPadrao permissaoUsuario)
        {
            Boolean excluido = permissaoUsuario.FlagExcluido;

            if (valida((PermissaoUsuario)permissaoUsuario))
            {
                if (((PermissaoUsuario)permissaoUsuario).Id == 0)
                    dao.incluir((PermissaoUsuario)permissaoUsuario);
                else
                    dao.alterar((PermissaoUsuario)permissaoUsuario);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PermissaoUsuario)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Permissao Usuario não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PermissaoUsuarioDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Permissao Usuario!" + e.Message);
            }

        }

        private Boolean valida(PermissaoUsuario permissaoUsuario)
        {
            if (string.IsNullOrWhiteSpace(permissaoUsuario.Permissoes))
            {
                throw new Exception("O campo \"Permissoes\" é obrigatório!");
            }
            return true;
        }
    }
}