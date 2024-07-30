using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class FormaPagamentoBO : BO
    {
        private FormaPagamentoDAO dao;

        public FormaPagamentoBO()
        {
            dao = new FormaPagamentoDAO();
        }

        public void salvar(ObjetoPadrao formaPagamento)
        {
            Boolean excluido = formaPagamento.FlagExcluido;

            if (valida((FormaPagamento)formaPagamento))
            {
                if (((FormaPagamento)formaPagamento).Id == 0)
                    dao.incluir((FormaPagamento)formaPagamento);
                else
                    dao.alterar((FormaPagamento)formaPagamento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((FormaPagamento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Forma de Pagamento não encontrado!");
            }

        }

        public void salvarSeNaoExistir(FormaPagamento formaPagamento)
        {
            try
            {
                FormaPagamento formaPagamentoAux = (FormaPagamento)dao.Selecionar(formaPagamento, ((FormaPagamento)formaPagamento).Id);
                if (formaPagamentoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(formaPagamento.Descricao))
                {
                    throw new Exception("O campo \"Forma de Pagamento\" é obrigatório!");
                }
                dao.incluir((FormaPagamento)formaPagamento);
            }
        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new FormaPagamentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Forma de Pagamento!" + e.Message);
            }

        }

        private Boolean valida(FormaPagamento formaPagamento)
        {
            if (string.IsNullOrWhiteSpace(formaPagamento.Descricao))
            {
                throw new Exception("O campo \"Forma de Pagamento\" é obrigatório!");
            }
            return true;
        }
    }
}