using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate;
using static LunarBase.ClassesDAO.OrdemServicoDAO;

namespace LunarBase.ClassesDAO
{
    public class VendaDAO : BaseDAO
    {
        public Venda selecionarVendaPorNF(int idNfe)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Venda as Tabela where Tabela.Nfe = "+idNfe+" and Tabela.FlagExcluido <> true").UniqueResult<Venda>();
        }

        public IList<Venda> selecionarTop5VendaPorVendedores(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            string sql2 = "SELECT new Venda(Sum(Tabela.ValorFinal) as ValorFinal, Tabela.Vendedor as Vendedor, Tabela.Quantidade as Quantidade) " +
                         "FROM Venda Tabela " +
                         "WHERE Tabela.DataVenda between '" +dataInicial+"' and '"+dataFinal+"' and Tabela.EmpresaFilial = " + filial.Id + " and Tabela.Concluida = true and Tabela.Cancelado = false " + 
                         "GROUP BY Tabela.Vendedor " +
                         "ORDER BY Tabela.DataCadastro";

            IQuery query = Session.CreateQuery(sql2);
            query.SetMaxResults(5);
            return query.List<Venda>();
        }

        public IList<Venda> selecionarVendaPorPeriodo(EmpresaFilial empresaFilial, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Venda as Tabela WHERE Tabela.Cancelado = false and Tabela.FlagExcluido <> true and Tabela.Concluida = true and Tabela.DataVenda between '" + dataInicial + "' and '" + dataFinal + "' and Tabela.EmpresaFilial = " + empresaFilial.Id;
            IList<Venda> retorno = Session.CreateQuery(sql).List<Venda>();
            return retorno;
        }

        public IList<Venda> selecionarVendaPorSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<Venda> retorno = Session.CreateQuery(sql).List<Venda>();
            return retorno;
        }

        public decimal selecionarValorVendidoPorVendedor(int idCodVendedor, string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            return Session.CreateSQLQuery("SELECT sum(venda.VALORFINAL)as valor from venda inner join pessoa on pessoa.ID = venda.VENDEDOR where venda.DATAVENDA between '"+dataInicial+" 00:00:00' and '"+dataFinal+ " 23:59:59' and venda.FLAGEXCLUIDO <> true and venda.Cancelado <> true and venda.CONCLUIDA = true and venda.VENDEDOR = " + idCodVendedor).SetMaxResults(1).UniqueResult<decimal>();
        }

        public IList<ComissaoVenda> selecionarComissaoVendaPorSQL(string sql)
        {
            Session = Conexao.GetSession();
            IList<ComissaoVenda> retorno = Session.CreateSQLQuery(sql).AddEntity("v", typeof(ComissaoVenda)).List<ComissaoVenda>();
            return retorno;
        }

       
    }
}
