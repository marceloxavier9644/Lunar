using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class CondicionalDAO : BaseDAO
    {
        public IList<Condicional> selecionarCondicionalPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<Condicional> retorno = Session.CreateQuery(sql).List<Condicional>();
            return retorno;
        }
    }
}
