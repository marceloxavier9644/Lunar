using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CfopBO : BO
    {
        private CfopDAO dao;

        public CfopBO()
        {
            dao = new CfopDAO();
        }

        public void salvar(ObjetoPadrao cfop)
        {
            Boolean excluido = cfop.FlagExcluido;

            if (valida((Cfop)cfop))
            {
                if (((Cfop)cfop).Id == 0)
                    dao.incluir((Cfop)cfop);
                else
                    dao.alterar((Cfop)cfop);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Cfop)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cfop não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CfopDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cfop!" + e.Message);
            }

        }

        private Boolean valida(Cfop cfop)
        {
            if (string.IsNullOrWhiteSpace(cfop.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cfop.CfopNumero))
            {
                throw new Exception("O campo \"CFOP\" é obrigatório!");
            }
            return true;
        }

        public IList<Cfop> selecionarTodosCfop()
        {
            try
            {
                return dao.selecionarTodosCfop();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Cfop! Erro: " + e.Message);
            }
        }
        public IList<Cfop> selecionarCfopPorCfop(String cfop)
        {
            try
            {
                return dao.selecionarCfopPorCfop(cfop);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Cfop! Erro: " + e.Message);
            }
        }
    }
}