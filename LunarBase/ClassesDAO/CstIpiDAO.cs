using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CstIpiDAO : BaseDAO
    {
        public IList<CstIpi> selecionarTodosCST()
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstIpi as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<CstIpi> retorno = Session.CreateQuery(sql).List<CstIpi>();
            return retorno;
        }

        public IList<CstIpi> selecionarCstPorCst(string cst)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstIpi as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Cst Like '" + cst + "%'";
            IList<CstIpi> retorno = Session.CreateQuery(sql).List<CstIpi>();
            return retorno;
        }
    }
}
