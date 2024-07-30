using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ModuloGrupoBO : BO
    {
        private ModuloGrupoDAO dao;

        public ModuloGrupoBO()
        {
            dao = new ModuloGrupoDAO();
        }

        public void salvar(ObjetoPadrao moduloGrupo)
        {
            Boolean excluido = moduloGrupo.FlagExcluido;

            if (valida((ModuloGrupo)moduloGrupo))
            {
                if (((ModuloGrupo)moduloGrupo).Id == 0)
                    dao.incluir((ModuloGrupo)moduloGrupo);
                else
                    dao.alterar((ModuloGrupo)moduloGrupo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ModuloGrupo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Modulo Grupo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ModuloGrupoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Modulo Grupo!" + e.Message);
            }
        }

        private Boolean valida(ModuloGrupo moduloGrupo)
        {
            if (string.IsNullOrWhiteSpace(moduloGrupo.Descricao))
            {
                throw new Exception("O campo \"Grupo\" é obrigatório!");
            }
            return true;
        }
    }
}