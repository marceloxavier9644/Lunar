using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CestDAO : BaseDAO
    {
        public IList<Cest> selecionarTodosCEST()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cest as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Cest> retorno = Session.CreateQuery(sql).List<Cest>();
            return retorno;
        }

        public IList<Cest> selecionarCestPorNCM(string ncm)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cest as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Ncm Like '" + ncm + "%'";
            IList<Cest> retorno = Session.CreateQuery(sql).List<Cest>();
            return retorno;
        }
    }
}
