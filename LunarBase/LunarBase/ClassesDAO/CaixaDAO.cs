using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CaixaDAO : BaseDAO
    {
        public IList<Caixa> selecionarCaixaPorOrigem(string tabelaOrigem, string idOrigem)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Caixa as Tabela WHERE Tabela.TabelaOrigem = '" + tabelaOrigem + "' and Tabela.IdOrigem = '" + idOrigem + "' and Tabela.FlagExcluido <> true";
            IList<Caixa> retorno = Session.CreateQuery(sql).List<Caixa>();
            return retorno;
        }

        public IList<Caixa> selecionarCaixaPorUsuarioEDataCadastro(int idUsuario, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Caixa as Tabela WHERE Tabela.Usuario = '"+idUsuario+"' and Tabela.DataLancamento between '"+dataInicial+ " 00:00:00' and '" + dataFinal + " 23:59:59' and Tabela.FlagExcluido <> true";
            IList<Caixa> retorno = Session.CreateQuery(sql).List<Caixa>();
            return retorno;
        }

        public IList<Caixa> selecionarCaixaPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<Caixa> retorno = Session.CreateQuery(sql).List<Caixa>();
            return retorno;
        }
        public IList<Caixa> selecionarCaixaPorSqlNativo(string sql)
        {
            Session = Conexao.GetSession();
            IList<Caixa> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(Caixa)).List<Caixa>();
            return retorno;
        }

        public decimal SelecionarSaldoPorSqlNativo(string sql)
        {
            using (var session = Conexao.GetSession())
            {
                // Execute a consulta SQL e retorne o valor escalar
                decimal saldo = session.CreateSQLQuery(sql).UniqueResult<decimal>();

                return saldo;
            }
        }

    }
}
