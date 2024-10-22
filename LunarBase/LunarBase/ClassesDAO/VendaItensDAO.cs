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

        public IList<ProdutoVendido> SelecionarProdutosVendidosPorSqlNativo(string sql)
        {
            Session = Conexao.GetSession();

            // Executa a query nativa e projeta o resultado na classe ProdutoVendido
            var resultado = Session.CreateSQLQuery(sql)
                .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean<ProdutoVendido>())
                .List<ProdutoVendido>();

            return resultado;
        }

        public class ProdutoVendido
        {
            public int ID { get; set; }
            public string Descricao { get; set; }
            public double Quantidade { get; set; }
            public decimal ValorTotal { get; set; }
            public string NomeMarca { get; set; }
            public string Grupo { get; set; }
            public int CodVendedor { get; set; }
        }
    }
}
