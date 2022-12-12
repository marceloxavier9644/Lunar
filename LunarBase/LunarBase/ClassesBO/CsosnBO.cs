using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CsosnBO : BO
    {
        private CsosnDAO dao;

        public CsosnBO()
        {
            dao = new CsosnDAO();
        }

        public void salvar(ObjetoPadrao csosn)
        {
            Boolean excluido = csosn.FlagExcluido;

            if (valida((Csosn)csosn))
            {
                if (((Csosn)csosn).Id == 0)
                    dao.incluir((Csosn)csosn);
                else
                    dao.alterar((Csosn)csosn);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Csosn)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Csosn não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CsosnDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Csosn!" + e.Message);
            }

        }

        private Boolean valida(Csosn csosn)
        {
            if (string.IsNullOrWhiteSpace(csosn.Codigo))
            {
                throw new Exception("O campo \"CSOSN\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(csosn.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(Csosn csosn)
        {
            try
            {
                Csosn csosnAux = (Csosn)dao.Selecionar(csosn, ((Csosn)csosn).Id);
                if (csosnAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(csosn.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(csosn.Codigo))
                {
                    throw new Exception("O campo \"csosn\" é obrigatório!");
                }
                dao.incluir((Csosn)csosn);
            }
        }
        public IList<Csosn> selecionarTodosCSOSN()
        {
            try
            {
                return dao.selecionarTodosCSOSN();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CSOSN! Erro: " + e.Message);
            }
        }

        public IList<Csosn> selecionarCsosnPorCsosn(string csosn)
        {
            try
            {
                return dao.selecionarCsosnPorCsosn(csosn);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CSOSN! Erro: " + e.Message);
            }
        }
    }
}
