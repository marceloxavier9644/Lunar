using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfePagamentoBO : BO
    {
        private NfePagamentoDAO dao;

        public NfePagamentoBO()
        {
            dao = new NfePagamentoDAO();
        }

        public void salvar(ObjetoPadrao nfePagamento)
        {
            Boolean excluido = nfePagamento.FlagExcluido;

            if (valida((NfePagamento)nfePagamento))
            {
                if (((NfePagamento)nfePagamento).Id == 0)
                    dao.incluir((NfePagamento)nfePagamento);
                else
                    dao.alterar((NfePagamento)nfePagamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NfePagamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfe Pagamento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfePagamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Nfe Pagamento!" + e.Message);
            }

        }

        private Boolean valida(NfePagamento nfePagamento)
        {
            if (string.IsNullOrWhiteSpace(nfePagamento.Descricao))
            {
                throw new Exception("O campo \"Descrição em Nfe Pagamento\" é obrigatório!");
            }
            if (nfePagamento.FormaPagamento == null)
            {
                throw new Exception("O campo \"Forma de Pagamento em Nfe Pagamento\" é obrigatório!");
            }
            return true;
        }
        public IList<NfePagamento> selecionarPagamentoPorNfe(int idNfe)
        {
            try
            {
                return dao.selecionarPagamentoPorNfe(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Pagamento por Nfe! Erro: " + e.Message);
            }
        }
    }
}