using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class OrdemServicoProdutoDAO : BaseDAO
    {
        public IList<OrdemServicoProduto> selecionarProdutosPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM OrdemServicoProduto as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.OrdemServico = " + idOrdemServico;
            IList<OrdemServicoProduto> retorno = Session.CreateQuery(sql).List<OrdemServicoProduto>();
            return retorno;
        }
    }
}
