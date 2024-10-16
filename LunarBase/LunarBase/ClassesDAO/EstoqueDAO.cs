using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class EstoqueDAO : BaseDAO
    {

        public double calcularEstoqueConciliadoPorProduto(int idProduto, EmpresaFilial empresaFilial)
        {
            string sql = "select (sum(if(prod.entrada= true,prod.quantidade,0)) - sum(if(prod.saida = true, prod.quantidade,0))) as Saldo from estoque prod where prod.Produto = " + idProduto + " and prod.Conciliado = true and prod.EmpresaFilial = " + empresaFilial.Id + " and prod.FLAGEXCLUIDO <> true group by prod.PRODUTO";

            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }
        public double calcularEstoqueNaoConciliadoPorProduto(int idProduto, EmpresaFilial empresaFilial)
        {
            string sql = "select (sum(if(prod.entrada= true,prod.quantidade,0)) - sum(if(prod.saida = true, prod.quantidade,0))) as Saldo from estoque prod where prod.Produto = " + idProduto + " and prod.Conciliado = false and prod.EmpresaFilial = " + empresaFilial.Id + " and prod.FLAGEXCLUIDO <> true group by prod.PRODUTO";

            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }

        public double calcularEstoqueConciliadoPorProdutoeData(int idProduto, EmpresaFilial empresaFilial, string data)
        {
            string sql = "select (sum(if(prod.entrada= true,prod.quantidade,0)) - sum(if(prod.saida = true, prod.quantidade,0))) as Saldo from estoque prod where prod.Produto = " + idProduto + " and prod.Conciliado = true and prod.DataEntradaSaida between '1900-01-01 00:00:00' and '" + data + " 23:59:59' and prod.EmpresaFilial = " + empresaFilial.Id + " and prod.FLAGEXCLUIDO <> true group by prod.PRODUTO";

            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }
        public double calcularEstoqueNaoConciliadoPorProdutoeData(int idProduto, EmpresaFilial empresaFilial, string data)
        {
            string sql = "select (sum(if(prod.entrada= true,prod.quantidade,0)) - sum(if(prod.saida = true, prod.quantidade,0))) as Saldo from estoque prod where prod.Produto = " + idProduto + " and prod.Conciliado = false and prod.DataEntradaSaida between '1900-01-01 00:00:00' and '" + data + " 23:59:59' and prod.EmpresaFilial = " + empresaFilial.Id + " and prod.FLAGEXCLUIDO <> true group by prod.PRODUTO";

            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }

        //public IList<Estoque> gerarInventarioPorData(EmpresaFilial filial, string dataInventario)
        //{
        //    Session = Conexao.GetSession();

        //    string sql3 = "SELECT Tabela.ID, Tabela.ORIGEM, Tabela.BALANCOESTOQUE,Tabela.PESSOA, Tabela.CONCILIADO, Tabela.ENTRADA, Tabela.SAIDA,Tabela.PRODUTO, " +
        //        "Tabela.EMPRESAFILIAL, Tabela.DATAENTRADASAIDA, Tabela.DESCRICAO,Tabela.QUANTIDADE,Tabela.DATACADASTRO, " +
        //        "Tabela.DATAALTERACAO, Tabela.DATAEXCLUSAO,Tabela.FLAGEXCLUIDO, Tabela.OPERADORCADASTRO, " +
        //        "Tabela.OPERADORALTERACAO,Tabela.OPERADOREXCLUSAO, SUM(IF(Tabela.Entrada = true and Tabela.DataEntradaSaida " +
        //        "Between '1900-01-01 00:00:00' and '" + dataInventario + "', Tabela.Quantidade, 0))" +
        //        " - SUM(IF(Tabela.Saida = true and Tabela.DataEntradaSaida Between '1900-01-01 00:00:00' " +
        //        "and '" + dataInventario + "', Tabela.Quantidade, 0)) AS QUANTIDADEINVENTARIO, " +
        //        "Tabela.Produto AS ProdutoSelecionado FROM Estoque as Tabela " +
        //        "WHERE Tabela.Conciliado = true and Tabela.DataEntradaSaida " +
        //        "Between '1900-01-01 00:00:00' and '" + dataInventario + "' and Tabela.EmpresaFilial = " + filial.Id + " GROUP BY Tabela.Produto";

        //    return Session.CreateSQLQuery(sql3).AddEntity(typeof(Estoque)).List<Estoque>();
        //}
        public IList<Estoque> gerarInventarioPorData(EmpresaFilial filial, string dataInventario)
        {
            Session = Conexao.GetSession();

            string sql3 = "SELECT " +
                "MAX(Tabela.ID) AS ID, " +
                "MAX(Tabela.ORIGEM) AS ORIGEM, " +
                "MAX(Tabela.BALANCOESTOQUE) AS BALANCOESTOQUE, " +
                "MAX(Tabela.PESSOA) AS PESSOA, " +
                "MAX(Tabela.CONCILIADO) AS CONCILIADO, " +
                "MAX(Tabela.ENTRADA) AS ENTRADA, " +
                "MAX(Tabela.SAIDA) AS SAIDA, " +
                "MAX(Tabela.PRODUTO) AS PRODUTO, " +
                "MAX(Tabela.EMPRESAFILIAL) AS EMPRESAFILIAL, " +
                "MAX(Tabela.DATAENTRADASAIDA) AS DATAENTRADASAIDA, " +
                "MAX(Tabela.DESCRICAO) AS DESCRICAO, " +
                "MAX(Tabela.QUANTIDADE) AS QUANTIDADE, " +
                "MAX(Tabela.DATACADASTRO) AS DATACADASTRO, " +
                "MAX(Tabela.DATAALTERACAO) AS DATAALTERACAO, " +
                "MAX(Tabela.DATAEXCLUSAO) AS DATAEXCLUSAO, " +
                "MAX(Tabela.FLAGEXCLUIDO) AS FLAGEXCLUIDO, " +
                "MAX(Tabela.OPERADORCADASTRO) AS OPERADORCADASTRO, " +
                "MAX(Tabela.OPERADORALTERACAO) AS OPERADORALTERACAO, " +
                "MAX(Tabela.OPERADOREXCLUSAO) AS OPERADOREXCLUSAO, " +
                "SUM(IF(Tabela.Entrada = true AND Tabela.DataEntradaSaida BETWEEN '1900-01-01 00:00:00' AND '" + dataInventario + "', Tabela.Quantidade, 0)) " +
                "- SUM(IF(Tabela.Saida = true AND Tabela.DataEntradaSaida BETWEEN '1900-01-01 00:00:00' AND '" + dataInventario + "', Tabela.Quantidade, 0)) AS QUANTIDADEINVENTARIO " +
                "FROM Estoque AS Tabela " +
                "WHERE Tabela.Conciliado = true " +
                "AND Tabela.DataEntradaSaida BETWEEN '1900-01-01 00:00:00' AND '" + dataInventario + "' " +
                "AND Tabela.EmpresaFilial = " + filial.Id + " " +
                "GROUP BY Tabela.Produto";

            return Session.CreateSQLQuery(sql3).AddEntity(typeof(Estoque)).List<Estoque>();
        }
        public IList<Estoque> selecionarEstoqueMovimentoPorProduto(EmpresaFilial filial, int idProduto, bool conciliado)
        {
            Session = Conexao.GetSession();
            string sql = "From Estoque as Tabela Where Tabela.Produto = " + idProduto + " and Tabela.FlagExcluido <> true and Tabela.Conciliado =  " + conciliado + " and Tabela.EmpresaFilial = " + filial.Id + " order by Tabela.DataEntradaSaida, Tabela.Entrada";
            IList<Estoque> retorno = Session.CreateQuery(sql).List<Estoque>();
            return retorno;
        }

        public IList<Estoque> selecionarEstoqueMovimentoPorBalanco(EmpresaFilial filial, bool conciliado, BalancoEstoque balancoEstoque)
        {
            Session = Conexao.GetSession();
            string sql = "From Estoque as Tabela Where Tabela.FlagExcluido <> true and Tabela.Conciliado =  " + conciliado + " and Tabela.EmpresaFilial = " + filial.Id + " and Tabela.BalancoEstoque = " + balancoEstoque.Id + " order by Tabela.DataEntradaSaida, Tabela.Entrada";
            IList<Estoque> retorno = Session.CreateQuery(sql).List<Estoque>();
            return retorno;
        }

        public IList<Estoque> selecionarEstoqueMovimentoPorProdutoEData(EmpresaFilial filial, int idProduto, bool conciliado, String dataInicial, String dataFinal)
        {
            Session = Conexao.GetSession();
            string sql = "From Estoque as Tabela Where Tabela.Produto = " + idProduto + " and Tabela.DataEntradaSaida between '" + dataInicial + " 00:00:00' and '" + dataFinal + " 23:59:59' and Tabela.FlagExcluido <> true and Tabela.Conciliado =  " + conciliado + " and Tabela.EmpresaFilial = " + filial.Id + " order by Tabela.DataEntradaSaida, Tabela.Entrada";
            IList<Estoque> retorno = Session.CreateQuery(sql).List<Estoque>();
            return retorno;
        }
    }
}
