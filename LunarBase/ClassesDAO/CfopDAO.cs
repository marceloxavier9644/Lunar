using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CfopDAO : BaseDAO
    {
        public IList<Cfop> selecionarTodosCfop()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cfop as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Cfop> retorno = Session.CreateQuery(sql).List<Cfop>();
            return retorno;
        }

        public IList<Cfop> selecionarCfopPorCfop(string cfop)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cfop as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.CfopNumero = '" + cfop + "'";
            IList<Cfop> retorno = Session.CreateQuery(sql).List<Cfop>();
            return retorno;
        }
    }
}
