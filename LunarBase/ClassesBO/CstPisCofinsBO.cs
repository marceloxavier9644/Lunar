using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CstPisCofinsBO : BO
    {
        private CstPisCofinsDAO dao;

        public CstPisCofinsBO()
        {
            dao = new CstPisCofinsDAO();
        }

        public void salvar(ObjetoPadrao cstPisCofins)
        {
            Boolean excluido = cstPisCofins.FlagExcluido;

            if (valida((CstPisCofins)cstPisCofins))
            {
                if (((CstPisCofins)cstPisCofins).Id == 0)
                    dao.incluir((CstPisCofins)cstPisCofins);
                else
                    dao.alterar((CstPisCofins)cstPisCofins);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CstPisCofins)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cst Pis/Cofins não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CstPisCofinsDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cst Pis/Cofins!" + e.Message);
            }

        }

        private Boolean valida(CstPisCofins cstPisCofins)
        {
            if (string.IsNullOrWhiteSpace(cstPisCofins.Cst))
            {
                throw new Exception("O campo \"CST\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cstPisCofins.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }
        
        public IList<CstPisCofins> selecionarTodosCST()
        {
            try
            {
                return dao.selecionarTodosCST();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST PIS/Cofins! Erro: " + e.Message);
            }
        }

        public IList<CstPisCofins> selecionarCstPorCst(string cst)
        {
            try
            {
                return dao.selecionarCstPorCst(cst);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CST PIS/Cofins! Erro: " + e.Message);
            }
        }
    }
}