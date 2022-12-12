using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class NcmDAO : BaseDAO
    {
        public IList<Ncm> selecionarTodosNCM()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Ncm as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Ncm> retorno = Session.CreateQuery(sql).List<Ncm>();
            return retorno;
        }

        public IList<Ncm> selecionarNCMPorNCM(String ncm)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Ncm as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.NumeroNcm like '%" + ncm + "%'";
            IList<Ncm> retorno = Session.CreateQuery(sql).List<Ncm>();
            return retorno;
        }
    }
}
