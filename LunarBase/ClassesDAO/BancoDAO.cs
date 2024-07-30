using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class BancoDAO : BaseDAO
    {
        public IList<Banco> selecionarTodosBancos()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Banco as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<Banco> retorno = Session.CreateQuery(sql).List<Banco>();
            return retorno;
        }

        public IList<Banco> selecionarBancosComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Banco as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.CodBanco, ' ', Tabela.CodIspb) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.Descricao";
            IList<Banco> retorno = Session.CreateQuery(sql).List<Banco>();
            return retorno;
        }
    }
}
