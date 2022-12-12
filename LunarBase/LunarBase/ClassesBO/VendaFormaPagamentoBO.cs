using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class VendaFormaPagamentoBO : BO
    {
        private VendaFormaPagamentoDAO dao;

        public VendaFormaPagamentoBO()
        {
            dao = new VendaFormaPagamentoDAO();
        }

        public void salvar(ObjetoPadrao vendaFormaPagamento)
        {
            Boolean excluido = vendaFormaPagamento.FlagExcluido;

            if (valida((VendaFormaPagamento)vendaFormaPagamento))
            {
                if (((VendaFormaPagamento)vendaFormaPagamento).Id == 0)
                    dao.incluir((VendaFormaPagamento)vendaFormaPagamento);
                else
                    dao.alterar((VendaFormaPagamento)vendaFormaPagamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((VendaFormaPagamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Venda Forma de Pagamento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new VendaFormaPagamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Venda Forma de Pagamento!" + e.Message);
            }

        }

        private Boolean valida(VendaFormaPagamento vendaFormaPagamento)
        {
            if (vendaFormaPagamento.Venda == null)
            {
                throw new Exception("O campo \"Venda\" é obrigatório!");
            }
            if (vendaFormaPagamento.FormaPagamento == null)
            {
                throw new Exception("O campo \"Forma Pagamento\" é obrigatório!");
            }
            return true;
        }

        public IList<VendaFormaPagamento> selecionarVendaFormaPagamentoPorVenda(int idVenda)
        {
            try
            {
                return dao.selecionarVendaFormaPagamentoPorVenda(idVenda);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Venda Forma Pagamento! Erro: " + e.Message);
            }
        }
    }
}