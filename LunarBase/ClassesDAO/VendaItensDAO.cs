using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;

namespace LunarBase.ClassesDAO
{
    public class VendaItensDAO : BaseDAO
    {
        public IList<VendaItens> selecionarProdutosVendidosPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            string sql2 = "SELECT new VendaItens(Sum(Tabela.Quantidade) as Quantidade, Tabela.DescricaoProduto, Tabela.Produto) " +
                         "FROM VendaItens Tabela " +
                         "GROUP BY Tabela.Produto " +
                         "ORDER BY Tabela.DataCadastro";

            IQuery query = Session.CreateQuery(sql2);
            query.SetMaxResults(3);
            return query.List<VendaItens>();
        }

        public IList<VendaItens> selecionarProdutosPorVenda(int idVenda)
        {
            Session = Conexao.GetSession();
            String sql = "FROM VendaItens as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Venda = " + idVenda;
            IList<VendaItens> retorno = Session.CreateQuery(sql).List<VendaItens>();
            return retorno;
        }
    }
}
