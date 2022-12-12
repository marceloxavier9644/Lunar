using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class NaturezaOperacaoDAO : BaseDAO
    {
        public IList<NaturezaOperacao> selecionarTodasNaturezaOperacao()
        {
            Session = Conexao.GetSession();
            String sql = "FROM NaturezaOperacao as Tabela WHERE Tabela.FlagExcluido <> true";
            IList<NaturezaOperacao> retorno = Session.CreateQuery(sql).List<NaturezaOperacao>();
            return retorno;
        }

        public IList<NaturezaOperacao> selecionarNaturezaOperacaoVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM NaturezaOperacao as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.Id";
            IList<NaturezaOperacao> retorno = Session.CreateQuery(sql).List<NaturezaOperacao>();
            return retorno;
        }

    }
}
