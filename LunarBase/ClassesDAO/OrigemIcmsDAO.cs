using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class OrigemIcmsDAO : BaseDAO
    {
        public IList<OrigemIcms> selecionarTodasOrigemIcms()
        {
            Session = Conexao.GetSession();
            String sql = "FROM OrigemIcms as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<OrigemIcms> retorno = Session.CreateQuery(sql).List<OrigemIcms>();
            return retorno;
        }

        public OrigemIcms selecionarOrigemPorCodigoDeOrigem(string codOrigem)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("FROM OrigemIcms as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.CodOrigem = '" + codOrigem + "'").UniqueResult<OrigemIcms>();
        }
    }
}
