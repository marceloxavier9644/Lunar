using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ProdutoCaracteristicaDAO : BaseDAO
    {
        public IList<ProdutoCaracteristica> selecionarProdutoCaracteristica(int idProduto)
        {
            Session = Conexao.GetSession();
            String sql = "FROM ProdutoCaracteristica as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Produto = '"+idProduto+ "' order by Tabela.Descricao";
            IList<ProdutoCaracteristica> retorno = Session.CreateQuery(sql).List<ProdutoCaracteristica>();
            return retorno;
        }
    }
}
