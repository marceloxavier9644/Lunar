using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class GrupoUsuarioBO : BO
    {
        private GrupoUsuarioDAO dao;

        public GrupoUsuarioBO()
        {
            dao = new GrupoUsuarioDAO();
        }

        public void salvar(ObjetoPadrao grupoUsuario)
        {
            Boolean excluido = grupoUsuario.FlagExcluido;

            if (valida((GrupoUsuario)grupoUsuario))
            {
                if (((GrupoUsuario)grupoUsuario).Id == 0)
                    dao.incluir((GrupoUsuario)grupoUsuario);
                else
                    dao.alterar((GrupoUsuario)grupoUsuario);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((GrupoUsuario)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Grupo Usuario não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new GrupoUsuarioDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Grupo Usuario!" + e.Message);
            }

        }

        private Boolean valida(GrupoUsuario grupoUsuario)
        {
            if (string.IsNullOrWhiteSpace(grupoUsuario.Descricao))
            {
                throw new Exception("O campo \"Nome do Grupo de Usuário\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(GrupoUsuario grupoUsuario)
        {
            try
            {
                GrupoUsuario grupoUsuarioAux = (GrupoUsuario)dao.Selecionar(grupoUsuario, ((GrupoUsuario)grupoUsuario).Id);
                if (grupoUsuarioAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(grupoUsuario.Descricao))
                {
                    throw new Exception("O campo \"Grupo\" é obrigatório!");
                }
              
                dao.incluir((GrupoUsuario)grupoUsuario);
            }
        }
    }
}