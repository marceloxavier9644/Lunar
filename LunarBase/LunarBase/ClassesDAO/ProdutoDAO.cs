using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.ClassesDAO
{
    public class ProdutoDAO : BaseDAO
    {
        public Produto selecionarProdutoPorCodigoUnicoEFilial(int idProduto, EmpresaFilial empresaFilial)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Produto as Tabela where Tabela.Id = '" + idProduto + "' and Tabela.EmpresaFilial = " + empresaFilial.Id + " and Tabela.FlagExcluido <> true").UniqueResult<Produto>();
        }

        public IList<Produto> selecionarTodosProdutorPorFilial(EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.EmpresaFilial = " + empresaLogada.Id + " order by Tabela.Descricao"; 
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarProdutosComVariosFiltros(string valor, EmpresaFilial empresaLogada)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE CONCAT(Tabela.Descricao, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.EmpresaFilial = " + empresaLogada.Id + " order by Tabela.Descricao";
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarProdutosPorSql(string sql)
        {
            using (var statelessSession = Conexao.GetStatelessSession())  // Utilizando StatelessSession
            {
                IList<Produto> retorno = statelessSession.CreateQuery(sql).List<Produto>();
                return retorno;
            }
        }

        public IList<ProdutoResult> selecionarProdutosPorSqlResult(string sql)
        {
            Session = Conexao.GetSession();

            IList<ProdutoResult> resultados = Session.CreateSQLQuery(sql)
                    .SetResultTransformer(Transformers.AliasToBean<ProdutoResult>())
                    .List<ProdutoResult>();
                return resultados;
            
        }
        public class ProdutoResult
        {
            public int ProdutoId { get; set; }
            public string ProdutoNome { get; set; }
            public int? ProdutoGradeId { get; set; }
            public string DescricaoGrade { get; set; }
            public int UnidadeMedida { get; set; }
            public decimal? ValorVenda { get; set; }
            public string CodigoBarras { get; set; }
            public string UnidadeMedidaDesc { get; set; }
        }
        public IList<Produto> selecionarProdutoPorCodigoBarras(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE Tabela.Ean = '" + valor + "' and Tabela.FlagExcluido <> true order by Tabela.Descricao";
            IList<Produto> retorno = Session.CreateQuery(sql).List<Produto>();
            return retorno;
        }

        public IList<Produto> selecionarTodosProdutosPaginando(int paginaAtual, int itensPorPagina, string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Produto as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Referencia, ' ', Tabela.Ean) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.Descricao";
            IList<Produto> retorno = Session.CreateQuery(sql).SetFirstResult(paginaAtual).SetMaxResults(itensPorPagina).List<Produto>();
            return retorno;
        }

        public Int64 totalTodosProdutosPaginando(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "SELECT COUNT(*) FROM Produto as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Referencia, ' ', Tabela.Ean) like '%" + valor + "%' and Tabela.FlagExcluido <> true ";
            return Session.CreateQuery(sql).UniqueResult<Int64>();
        }
    }
}
