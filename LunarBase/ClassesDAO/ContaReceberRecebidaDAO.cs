using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ContaReceberRecebidaDAO : BaseDAO
    {
        public IList<ContaReceberRecebida> selecionarContaReceberRecebidaPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<ContaReceberRecebida> retorno = Session.CreateQuery(sql).List<ContaReceberRecebida>();
            return retorno;
        }
    }
}
