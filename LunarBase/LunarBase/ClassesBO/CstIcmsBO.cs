using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CstIcmsBO : BO
    {
        private CstIcmsDAO dao;

        public CstIcmsBO()
        {
            dao = new CstIcmsDAO();
        }

        public void salvar(ObjetoPadrao cstIcms)
        {
            Boolean excluido = cstIcms.FlagExcluido;

            if (valida((CstIcms)cstIcms))
            {
                if (((CstIcms)cstIcms).Id == 0)
                    dao.incluir((CstIcms)cstIcms);
                else
                    dao.alterar((CstIcms)cstIcms);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CstIcms)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cst Icms não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CstIcmsDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cst Icms!" + e.Message);
            }

        }

        private Boolean valida(CstIcms cstIcms)
        {
            if (string.IsNullOrWhiteSpace(cstIcms.Codigo))
            {
                throw new Exception("O campo \"CST\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cstIcms.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(CstIcms cstIcms)
        {
            try
            {
                CstIcms cstIcmssAux = (CstIcms)dao.Selecionar(cstIcms, ((CstIcms)cstIcms).Id);
                if (cstIcmssAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(cstIcms.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(cstIcms.Codigo))
                {
                    throw new Exception("O campo \"CST\" é obrigatório!");
                }
                dao.incluir((CstIcms)cstIcms);
            }
        }
        public IList<CstIcms> selecionarTodosCstIcms()
        {
            try
            {
                return dao.selecionarTodosCstIcms();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST Icms! Erro: " + e.Message);
            }
        }

        public IList<CstIcms> selecionarCstIcmsPorCst(string cst)
        {
            try
            {
                return dao.selecionarCstIcmsPorCst(cst);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST Icms! Erro: " + e.Message);
            }
        }
    }
}