using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CsosnDAO : BaseDAO
    {
        public IList<Csosn> selecionarTodosCSOSN()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Csosn as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Csosn> retorno = Session.CreateQuery(sql).List<Csosn>();
            return retorno;
        }

        public IList<Csosn> selecionarCsosnPorCsosn(string csosn)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Csosn as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Codigo = '"+ csosn + "'";
            IList<Csosn> retorno = Session.CreateQuery(sql).List<Csosn>();
            return retorno;
        }
    }
}
