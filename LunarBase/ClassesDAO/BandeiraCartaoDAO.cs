using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class BandeiraCartaoDAO : BaseDAO
    {
        public IList<BandeiraCartao> selecionarTodasBandeiras()
        {
            Session = Conexao.GetSession();
            String sql = "FROM BandeiraCartao as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<BandeiraCartao> retorno = Session.CreateQuery(sql).List<BandeiraCartao>();
            return retorno;
        }

        public IList<BandeiraCartao> selecionarBandeiraCartaoComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM BandeiraCartao as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + valor + "%' and Tabela.FlagExcluido <> true order by Tabela.Descricao";
            IList<BandeiraCartao> retorno = Session.CreateQuery(sql).List<BandeiraCartao>();
            return retorno;
        }
    }
}
