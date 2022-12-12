using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class EstadoDAO : BaseDAO
    {
        public Estado selecionarEstadoPorUF(String uf)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Estado as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Uf = '" + uf +"'";
            return Session.CreateQuery(sql).UniqueResult<Estado>();

        }
    }
}
