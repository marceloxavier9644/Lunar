using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class AnpDAO : BaseDAO
    {
        public IList<Anp> selecionarTodosCodigosANP()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Anp as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Anp> retorno = Session.CreateQuery(sql).List<Anp>();
            return retorno;
        }

        public IList<Anp> selecionarCodigoAnpPorCodigo(string Anp)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Anp as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Codigo Like '" + Anp + "%'";
            IList<Anp> retorno = Session.CreateQuery(sql).List<Anp>();
            return retorno;
        }
    }
}
