using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class UnidadeMedidaDAO : BaseDAO
    {
        public UnidadeMedida selecionarUnidadeMedidaPorSigla(string sigla)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from UnidadeMedida as Tabela where Tabela.Sigla = '" + sigla + "' and Tabela.FlagExcluido <> true").UniqueResult<UnidadeMedida>();
        }

        public IList<UnidadeMedida> selecionarUnidadeMedidaComVariosFiltros(string valor, EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM UnidadeMedida as Tabela WHERE CONCAT(Tabela.Sigla, ' ', Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " order by Tabela.Descricao";
            IList<UnidadeMedida> retorno = Session.CreateQuery(sql).List<UnidadeMedida>();
            return retorno;                      
        }
    }
}
