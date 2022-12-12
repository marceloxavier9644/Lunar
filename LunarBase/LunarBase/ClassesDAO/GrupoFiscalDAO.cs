using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class GrupoFiscalDAO : BaseDAO
    {
        public IList<GrupoFiscal> selecionarGrupoFiscalComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM GrupoFiscal as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " order by Tabela.Descricao";
            IList<GrupoFiscal> retorno = Session.CreateQuery(sql).List<GrupoFiscal>();
            return retorno;
        }
    }
}
