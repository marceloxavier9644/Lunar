using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class BalancoEstoqueDAO : BaseDAO
    {
        public IList<BalancoEstoque> selecionarTodosBalancoEstoque(EmpresaFilial empresaFilial)
        {
            Session = Conexao.GetSession();
            String sql = "FROM BalancoEstoque as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Filial = " + empresaFilial.Id;
            IList<BalancoEstoque> retorno = Session.CreateQuery(sql).List<BalancoEstoque>();
            return retorno;
        }

        public IList<BalancoEstoque> selecionarBalancoEstoquePorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<BalancoEstoque> retorno = Session.CreateQuery(sql).List<BalancoEstoque>();
            return retorno;
        }
    }
}
