using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;

namespace LunarBase.ClassesDAO
{
    public class PlanoContaDAO : BaseDAO
    {
        public IList<PlanoConta> selecionarPlanoContaComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM PlanoConta as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + valor + "%' and Tabela.FlagExcluido <> true order by Tabela.Classificacao";
            IList<PlanoConta> retorno = Session.CreateQuery(sql).List<PlanoConta>();
            return retorno;
        }

        public IList<PlanoConta> selecionarTodosPlanosConta()
        {
            Session = Conexao.GetSession();
            String sql = "FROM PlanoConta as Tabela WHERE Tabela.FlagExcluido <> true order by Tabela.Classificacao";
            IList<PlanoConta> retorno = Session.CreateQuery(sql).List<PlanoConta>();
            return retorno;
        }
        public IList<PlanoConta> selecionarPlanoContaPeloIdPai(string idPai, string idAcima)
        {
            String sqlAdd = "";
            if (!String.IsNullOrEmpty(idAcima))
            {
                sqlAdd = " and Tabela.IdAcima = '" + idAcima + "' ";
            }
            Session = Conexao.GetSession();
            string sql2 = "from PlanoConta as Tabela Where Tabela.IdPai = '" + idPai +"'" + sqlAdd + "order by Tabela.Classificacao DESC";

            IQuery query = Session.CreateQuery(sql2);
            query.SetMaxResults(1);
            return query.List<PlanoConta>();
        }

        public IList<PlanoConta> selecionarPlanoContaPelaClassificacao(string classificacao)
        {
            Session = Conexao.GetSession();
            string sql2 = "from PlanoConta as Tabela Where Tabela.Classificacao = '" + classificacao + "' order by Tabela.Classificacao DESC";

            IQuery query = Session.CreateQuery(sql2);
            query.SetMaxResults(1);
            return query.List<PlanoConta>();
        }
    }
}
