using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class AdquirenteCartaoDAO : BaseDAO
    {
        public IList<AdquirenteCartao> selecionarTodasAdquirentes()
        {
            Session = Conexao.GetSession();
            String sql = "FROM AdquirenteCartao as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<AdquirenteCartao> retorno = Session.CreateQuery(sql).List<AdquirenteCartao>();
            return retorno;
        }

        public IList<AdquirenteCartao> selecionarAdquirenteComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM AdquirenteCartao as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Cnpj) like '%" + valor + "%' and Tabela.FlagExcluido <> true order by Tabela.Descricao";
            IList<AdquirenteCartao> retorno = Session.CreateQuery(sql).List<AdquirenteCartao>();
            return retorno;
        }
    }
}
