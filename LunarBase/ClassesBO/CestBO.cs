using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CestBO : BO
    {
        private CestDAO dao;

        public CestBO()
        {
            dao = new CestDAO();
        }

        public void salvar(ObjetoPadrao cest)
        {
            Boolean excluido = cest.FlagExcluido;

            if (valida((Cest)cest))
            {
                if (((Cest)cest).Id == 0)
                    dao.incluir((Cest)cest);
                else
                    dao.alterar((Cest)cest);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Cest)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cest não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CestDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cest!" + e.Message);
            }

        }

        private Boolean valida(Cest cest)
        {
            if (string.IsNullOrWhiteSpace(cest.DescricaoCest))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cest.NumeroCest))
            {
                throw new Exception("O campo \"Cest\" é obrigatório!");
            }
            return true;
        }

        public IList<Cest> selecionarTodosCEST()
        {
            try
            {
                return dao.selecionarTodosCEST();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cest! Erro: " + e.Message);
            }
        }
        public IList<Cest> selecionarCestPorNCM(String ncm)
        {
            try
            {
                return dao.selecionarCestPorNCM(ncm);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cest! Erro: " + e.Message);
            }
        }
    }
}