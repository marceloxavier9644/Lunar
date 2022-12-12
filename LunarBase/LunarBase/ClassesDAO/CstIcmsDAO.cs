using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CstIcmsDAO : BaseDAO
    {
       public IList<CstIcms> selecionarTodosCstIcms()
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstIcms as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<CstIcms> retorno = Session.CreateQuery(sql).List<CstIcms>();
            return retorno;
        }

        public IList<CstIcms> selecionarCstIcmsPorCst(string cst)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstIcms as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Codigo = '" + cst + "'";
            IList<CstIcms> retorno = Session.CreateQuery(sql).List<CstIcms>();
            return retorno;
        }
    }
}
