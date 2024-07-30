using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class BalancoEstoqueProdutoDAO : BaseDAO
    {
        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalanco(int idBalancoEstoque)
        {
            Session = Conexao.GetSession();
            String sql = "FROM BalancoEstoqueProduto as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.BalancoEstoque = " + idBalancoEstoque;
            IList<BalancoEstoqueProduto> retorno = Session.CreateQuery(sql).List<BalancoEstoqueProduto>();
            return retorno;
        }

        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalancoSemRepetirItem(int idBalancoEstoque)
        {
            Session = Conexao.GetSession();
            String sql = "SELECT sum(Tabela.QUANTIDADE) as QUANTIDADE, `ID`, `DESCRICAOPRODUTO`, `TIPO`, `CONCILIADO`, `BALANCOESTOQUE`, `PRODUTO`, `DATACADASTRO`, `DATAALTERACAO`, `DATAEXCLUSAO`, `FLAGEXCLUIDO`, `OPERADORCADASTRO`, `OPERADORALTERACAO`, `OPERADOREXCLUSAO` FROM BalancoEstoqueProduto as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.BalancoEstoque = "+ idBalancoEstoque + " group by Tabela.Produto";
            IList<BalancoEstoqueProduto> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(BalancoEstoqueProduto)).List<BalancoEstoqueProduto>();
            return retorno;
        }
        public IList<Produto> selecionarProdutosParaZerarNaoContabilizados(int idBalancoEstoque)
        {
            Session = Conexao.GetSession();
            string sql = "SELECT * FROM Produto p WHERE p.FlagExcluido <> true and p.Id NOT IN (SELECT be.Produto FROM BalancoEstoqueProduto as be where be.BalancoEstoque = "+ idBalancoEstoque + " and be.FlagExcluido <> true)";
            IList<Produto> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(Produto)).List<Produto>();
            return retorno;
        }

        //Acredito q esse abaixo nao sera utilizado, foi feito antes da criação do sql acima.
        public double calcularQuantidadeDoMesmoProduto(BalancoEstoqueProduto balancoEstoqueProduto)
        {
            string sql = "select sum(tabela.Quantidade) as Saldo from BalancoEstoqueProduto tabela where tabela.Produto = " + balancoEstoqueProduto.Produto.Id + " and tabela.BalancoEstoque = " + balancoEstoqueProduto.BalancoEstoque.Id + " and tabela.FLAGEXCLUIDO <> true group by tabela.PRODUTO";

            Session = Conexao.GetSession();
            return Session.CreateSQLQuery(sql).UniqueResult<double>();
        }
    }
}
