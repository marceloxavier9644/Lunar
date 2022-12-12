using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class OrdemServicoPagamentoBO : BO
    {
        private OrdemServicoPagamentoDAO dao;

        public OrdemServicoPagamentoBO()
        {
            dao = new OrdemServicoPagamentoDAO();
        }

        public void salvar(ObjetoPadrao ordemServicoPagamento)
        {
            Boolean excluido = ordemServicoPagamento.FlagExcluido;

            if (valida((OrdemServicoPagamento)ordemServicoPagamento))
            {
                if (((OrdemServicoPagamento)ordemServicoPagamento).Id == 0)
                    dao.incluir((OrdemServicoPagamento)ordemServicoPagamento);
                else
                    dao.alterar((OrdemServicoPagamento)ordemServicoPagamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrdemServicoPagamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Ordem Serviço Forma de Pagamento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrdemServicoPagamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Ordem Serviço Forma de Pagamento!" + e.Message);
            }

        }

        private Boolean valida(OrdemServicoPagamento ordemServicoPagamento)
        {
            if (ordemServicoPagamento.OrdemServico == null)
            {
                throw new Exception("O campo \"Ordem Serviço\" é obrigatório!");
            }
            if (ordemServicoPagamento.FormaPagamento == null)
            {
                throw new Exception("O campo \"Forma Pagamento\" é obrigatório!");
            }
            return true;
        }

        public IList<OrdemServicoPagamento> selecionarPagamentoPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarPagamentoPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ordem Serviço Pagamento! Erro: " + e.Message);
            }
        }
    }
}