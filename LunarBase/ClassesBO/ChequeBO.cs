using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ChequeBO : BO
    {
        private ChequeDAO dao;

        public ChequeBO()
        {
            dao = new ChequeDAO();
        }

        public void salvar(ObjetoPadrao cheque)
        {
            Boolean excluido = cheque.FlagExcluido;

            if (valida((Cheque)cheque))
            {
                if (((Cheque)cheque).Id == 0)
                    dao.incluir((Cheque)cheque);
                else
                    dao.alterar((Cheque)cheque);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Cheque)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cheque não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ChequeDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cheque!" + e.Message);
            }

        }

        private Boolean valida(Cheque cheque)
        {
            if (string.IsNullOrWhiteSpace(cheque.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(cheque.NumeroCheque))
            {
                throw new Exception("O campo \"Numero Cheque\" é obrigatório!");
            }
            return true;
        }

        public IList<Cheque> selecionarChequesPorVenda(int idVenda)
        {
            try
            {
                return dao.selecionarChequesPorVenda(idVenda);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Cheque! Erro: " + e.Message);
            }
        }
    }
}