using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ContaReceberBO : BO
    {
        private ContaReceberDAO dao;

        public ContaReceberBO()
        {
            dao = new ContaReceberDAO();
        }

        public void salvar(ObjetoPadrao contaReceber)
        {
            Boolean excluido = contaReceber.FlagExcluido;

            if (valida((ContaReceber)contaReceber))
            {
                if (((ContaReceber)contaReceber).Id == 0)
                    dao.incluir((ContaReceber)contaReceber);
                else
                    dao.alterar((ContaReceber)contaReceber);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ContaReceber)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Conta Receber não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ContaReceberDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Conta Receber!" + e.Message);
            }

        }

        private Boolean valida(ContaReceber contaReceber)
        {
            if (string.IsNullOrWhiteSpace(contaReceber.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (contaReceber.ValorParcela <= 0)
            {
                throw new Exception("O campo \"Valor da Parcela\" é obrigatório ser maior que 0,00!");
            }
            return true;
        }
        public IList<ContaReceber> selecionarContaReceberPorVendaFormaPagamento(int idVendaFormaPagamento)
        {
            try
            {
                return dao.selecionarContaReceberPorVendaFormaPagamento(idVendaFormaPagamento);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta Receber! Erro: " + e.Message);
            }
        }

        public IList<ContaReceber> selecionarContaReceberPorVenda(int idVenda)
        {
            try
            {
                return dao.selecionarContaReceberPorVenda(idVenda);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta Receber! Erro: " + e.Message);
            }
        }

        public IList<ContaReceber> selecionarContaReceberPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarContaReceberPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta Receber! Erro: " + e.Message);
            }
        }

        public IList<ContaReceber> selecionarContaReceberPorSql(string sql)
        {
            try
            {
                return dao.selecionarContaReceberPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta Receber! Erro: " + e.Message);
            }
        }

        public IList<ContaReceber> selecionarContaReceberPorSqlNativo(string sql)
        {
            try
            {
                return dao.selecionarContaReceberPorSqlNativo(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta Receber! Erro: " + e.Message);
            }
        }
    }
}