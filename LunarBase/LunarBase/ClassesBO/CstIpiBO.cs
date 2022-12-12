using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CstIpiBO : BO
    {
        private CstIpiDAO dao;

        public CstIpiBO()
        {
            dao = new CstIpiDAO();
        }

        public void salvar(ObjetoPadrao cstIpi)
        {
            Boolean excluido = cstIpi.FlagExcluido;

            if (valida((CstIpi)cstIpi))
            {
                if (((CstIpi)cstIpi).Id == 0)
                    dao.incluir((CstIpi)cstIpi);
                else
                    dao.alterar((CstIpi)cstIpi);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CstIpi)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cst IPI não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CstIpiDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cst IPI!" + e.Message);
            }

        }

        private Boolean valida(CstIpi cstIpi)
        {
            if (string.IsNullOrWhiteSpace(cstIpi.Cst))
            {
                throw new Exception("O campo \"CST\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cstIpi.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<CstIpi> selecionarTodosCST()
        {
            try
            {
                return dao.selecionarTodosCST();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST IPI! Erro: " + e.Message);
            }
        }

        public IList<CstIpi> selecionarCstPorCst(string cst)
        {
            try
            {
                return dao.selecionarCstPorCst(cst);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST IPI! Erro: " + e.Message);
            }
        }
    }
}