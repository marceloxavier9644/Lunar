using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;

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

        public IList<OrdemServicoProduto> selecionarProdutosVendidosPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            string sql2 = "SELECT new OrdemServicoProduto(Sum(Tabela.Quantidade) as Quantidade, Tabela.DescricaoProduto, Tabela.Produto) " +
                         "FROM OrdemServicoProduto Tabela " +
                         "GROUP BY Tabela.Produto " +
                         "ORDER BY Tabela.DataCadastro";

            IQuery query = Session.CreateQuery(sql2);
            query.SetMaxResults(3);
            return query.List<OrdemServicoProduto>();
        }
    }
}
