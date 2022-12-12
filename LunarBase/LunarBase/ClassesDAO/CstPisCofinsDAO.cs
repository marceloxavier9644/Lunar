using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CstPisCofinsDAO : BaseDAO
    {
        public IList<CstPisCofins> selecionarTodosCST()
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstPisCofins as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<CstPisCofins> retorno = Session.CreateQuery(sql).List<CstPisCofins>();
            return retorno;
        }

        public IList<CstPisCofins> selecionarCstPorCst(string cst)
        {
            Session = Conexao.GetSession();
            String sql = "FROM CstPisCofins as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Cst Like '" + cst + "%'";
            IList<CstPisCofins> retorno = Session.CreateQuery(sql).List<CstPisCofins>();
            return retorno;
        }
    }
}
