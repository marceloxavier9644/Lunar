using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ContaReceberDAO : BaseDAO
    {
        public IList<ContaReceber> selecionarContaReceberPorVendaFormaPagamento(int idVendaFormaPagamento)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaReceber as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.VendaFormaPagamento = " + idVendaFormaPagamento;
            IList<ContaReceber> retorno = Session.CreateQuery(sql).List<ContaReceber>();
            return retorno;
        }

        public IList<ContaReceber> selecionarContaReceberPorVenda(int idVenda)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaReceber as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Venda = " + idVenda;
            IList<ContaReceber> retorno = Session.CreateQuery(sql).List<ContaReceber>();
            return retorno;
        }

        public IList<ContaReceber> selecionarContaReceberPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ContaReceber as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.OrdemServico = " + idOrdemServico;
            IList<ContaReceber> retorno = Session.CreateQuery(sql).List<ContaReceber>();
            return retorno;
        }

        public IList<ContaReceber> selecionarContaReceberPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<ContaReceber> retorno = Session.CreateQuery(sql).List<ContaReceber>();
            return retorno;
        }
        public IList<ContaReceber> selecionarContaReceberPorSqlNativo(string sql)
        {
            Session = Conexao.GetSession();
            IList<ContaReceber> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(ContaReceber)).List<ContaReceber>();
            return retorno;
        }
    }
}
