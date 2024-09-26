using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate.Transform;

namespace LunarBase.ClassesDAO
{
    public class CidadeDAO : BaseDAO
    {
        public Cidade selecionarCidadePorDescricao(string descricao)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Cidade as Tabela where Tabela.Descricao = '" + descricao + "' and Tabela.FlagExcluido <> true").UniqueResult<Cidade>();
        }
        public Cidade selecionarCidadePorDescricaoEUf(string descricao, string uf)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Cidade as Tabela where Tabela.Descricao = '" + descricao + "' and Tabela.Estado.Uf = '" + uf + "' and Tabela.FlagExcluido <> true").UniqueResult<Cidade>();
        }

        public Cidade selecionarCidadePorDescricaoECodigoIBGE(string descricao, string codigoIBGE)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Cidade as Tabela where Tabela.Descricao = '" + descricao + "' and Tabela.Ibge = '" + codigoIBGE + "' and Tabela.FlagExcluido <> true").UniqueResult<Cidade>();
        }

        public Cidade selecionarCidadePorCodigoIBGE(string codigoIBGE)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Cidade as Tabela where Tabela.Ibge = '" + codigoIBGE + "' and Tabela.FlagExcluido <> true").UniqueResult<Cidade>();
        }

        public IList<Cidade> selecionarListaCidadePorDescricao(string descricao)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cidade as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Descricao Like '" + descricao + "%' order by Tabela.Descricao";
            IList<Cidade> retorno = Session.CreateQuery(sql).List<Cidade>();
            return retorno;
        }

        public IList<Cidade> selecionarTodasCidades()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cidade as Tabela WHERE Tabela.FlagExcluido <> true order by Tabela.Descricao";
            IList<Cidade> retorno = Session.CreateQuery(sql).List<Cidade>();
            return retorno;
        }
    }
}
