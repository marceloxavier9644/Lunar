using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ContaRecebidaFormaPagamentoBO : BO
    {
        private ContaRecebidaFormaPagamentoDAO dao;

        public ContaRecebidaFormaPagamentoBO()
        {
            dao = new ContaRecebidaFormaPagamentoDAO();
        }

        public void salvar(ObjetoPadrao contaRecebidaFormaPagamento)
        {
            Boolean excluido = contaRecebidaFormaPagamento.FlagExcluido;

            if (valida((ContaRecebidaFormaPagamento)contaRecebidaFormaPagamento))
            {
                if (((ContaRecebidaFormaPagamento)contaRecebidaFormaPagamento).Id == 0)
                    dao.incluir((ContaRecebidaFormaPagamento)contaRecebidaFormaPagamento);
                else
                    dao.alterar((ContaRecebidaFormaPagamento)contaRecebidaFormaPagamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ContaRecebidaFormaPagamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("ContaRecebidaFormaPagamento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ContaRecebidaFormaPagamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar ContaRecebidaFormaPagamento!" + e.Message);
            }

        }

        private Boolean valida(ContaRecebidaFormaPagamento contaRecebidaFormaPagamento)
        {
            if (string.IsNullOrWhiteSpace(contaRecebidaFormaPagamento.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (contaRecebidaFormaPagamento.FormaPagamento == null)
            {
                throw new Exception("O campo \"Forma Pagamento\" é obrigatório!");
            }
            if (contaRecebidaFormaPagamento.ContaReceber == null)
            {
                throw new Exception("O campo \"Conta Receber\" é obrigatório!");
            }
            return true;
        }

    
    }
}
