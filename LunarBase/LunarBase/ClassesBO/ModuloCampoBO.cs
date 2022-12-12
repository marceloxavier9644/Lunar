using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ModuloCampoBO : BO
    {
        private ModuloCampoDAO dao;

        public ModuloCampoBO()
        {
            dao = new ModuloCampoDAO();
        }

        public void salvar(ObjetoPadrao moduloCampo)
        {
            Boolean excluido = moduloCampo.FlagExcluido;

            if (valida((ModuloCampo)moduloCampo))
            {
                if (((ModuloCampo)moduloCampo).Id == 0)
                    dao.incluir((ModuloCampo)moduloCampo);
                else
                    dao.alterar((ModuloCampo)moduloCampo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ModuloCampo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Modulo Campo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ModuloCampoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Modulo Campo!" + e.Message);
            }

        }

        private Boolean valida(ModuloCampo moduloCampo)
        {
            if (string.IsNullOrWhiteSpace(moduloCampo.Descricao))
            {
                throw new Exception("O campo \"NOME DO CAMPO\" é obrigatório!");
            }
            if (moduloCampo.Grupo == null)
            {
                throw new Exception("O campo \"GRUPO\" é obrigatório!");
            }
            if (moduloCampo.Modulo == null)
            {
                throw new Exception("O campo \"MÓDULO\" é obrigatório!");
            }
            return true;
        }
    }
}