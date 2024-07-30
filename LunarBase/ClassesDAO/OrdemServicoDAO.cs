using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate.Transform;

namespace LunarBase.ClassesDAO
{
    public class OrdemServicoDAO : BaseDAO
    {
        public IList<OrdemServico> selecionarOrdemServicoPorSQL(string sql)
        {
            Session = Conexao.GetSession();
            IList<OrdemServico> retorno = Session.CreateSQLQuery(sql).AddEntity("f", typeof(OrdemServico)).List<OrdemServico>();
            return retorno;
        }

        public IList<OsComissao> selecionarOSComissaoPorSQL(string sql)
        {
            Session = Conexao.GetSession();
            IList<OsComissao> retorno = Session.CreateSQLQuery(sql).AddEntity("f", typeof(OsComissao)).List<OsComissao>();
            return retorno;
        }

        public IList<OrdemServico> selecionarTodasOS()
        {
            Session = Conexao.GetSession();
            IList<OrdemServico> retorno = Session.CreateQuery("From OrdemServico as Tabela where Tabela.FlagExcluido <> true").List<OrdemServico>();
            return retorno;
        }
        public OrdemServico selecionarOrdemServicoPorNfe(int idNfe)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from OrdemServico as Tabela where Tabela.Nfe = '" + idNfe + "' and Tabela.FlagExcluido <> true").UniqueResult<OrdemServico>();
        }

        public OrdemServico selecionarOrdemServicoPorID(int idOS)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from OrdemServico as Tabela where Tabela.Id = '" + idOS + "' and Tabela.FlagExcluido <> true").UniqueResult<OrdemServico>();
        }

       
    }
}
