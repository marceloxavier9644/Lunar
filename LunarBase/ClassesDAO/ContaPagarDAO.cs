using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ContaPagarDAO : BaseDAO
    {
        public void excluirContaPagarNfe(string idNfe)
        {
            Session = Conexao.GetSession();
            String sql = "Update ContaPagar as Tabela Set Tabela.FlagExcluido = true WHERE Tabela.Nfe = " + idNfe + " and Tabela.FlagExcluido <> true";
            Session.CreateQuery(sql).ExecuteUpdate();
        }
        public IList<ContaPagar> selecionarContaPagarPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<ContaPagar> retorno = Session.CreateQuery(sql).List<ContaPagar>();
            return retorno;
        }
    }
}
