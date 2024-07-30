using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ProdutoGrupoDAO : BaseDAO
    {
        public IList<ProdutoGrupo> selecionarGrupoComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoGrupo as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Id) like '%" + valor + "%' and Tabela.FlagExcluido <> true" +
                         " order by Tabela.Descricao";
            IList<ProdutoGrupo> retorno = Session.CreateQuery(sql).List<ProdutoGrupo>();
            return retorno;
        }
    }
}
