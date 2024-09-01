using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class PessoaDAO : BaseDAO
    {
        public IList<Pessoa> selecionarTodosClientes()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.Cliente = true order by Tabela.RazaoSocial";
            IList<Pessoa> retorno = Session.CreateQuery(sql).List<Pessoa>();
            return retorno;
        }

        public IList<Pessoa> selecionarTodasPessoas()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                         "order by Tabela.RazaoSocial";
            IList<Pessoa> retorno = Session.CreateQuery(sql).List<Pessoa>();
            //IList<Pessoa> retorno = Session.CreateSQLQuery(sql).AddEntity(typeof(Pessoa)).List<Pessoa>();

            return retorno;
            //.SetMaxResults(5000)
        }

        public IList<Pessoa> selecionarPessoasGrid()
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                         "order by Tabela.Id Desc";
            IList<Pessoa> retorno = Session.CreateQuery(sql).SetMaxResults(100).List<Pessoa>();
            return retorno;
            //.SetMaxResults(5000)
        }

        public IList<Pessoa> selecionarTodasPessoasPaginando(int paginaAtual, int itensPorPagina, string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.RazaoSocial";
            IList<Pessoa> retorno = Session.CreateQuery(sql).SetFirstResult(paginaAtual).SetMaxResults(itensPorPagina).List<Pessoa>();
            return retorno;
        }

        public Int64 totalTodasPessoasPaginando(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "SELECT COUNT(*) FROM Pessoa as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', " +
                "Tabela.NomeFantasia) like '%" + valor + "%' and Tabela.FlagExcluido <> true ";
            return Session.CreateQuery(sql).UniqueResult<Int64>();
        }

        public IList<Pessoa> selecionarClientesComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + valor + "%' and Tabela.FlagExcluido <> true and " +
                         "Tabela.Cliente = true order by Tabela.RazaoSocial";
            IList<Pessoa> retorno = Session.CreateQuery(sql).List<Pessoa>();
            return retorno;
        }

        public IList<Pessoa> selecionarPessoasComVariosFiltros(string valor)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Pessoa as Tabela WHERE CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + valor + "%' and Tabela.FlagExcluido <> true " +
                         "order by Tabela.RazaoSocial";
            IList<Pessoa> retorno = Session.CreateQuery(sql).List<Pessoa>();
            return retorno;
        }

        public Pessoa selecionarPessoaPorCPFCNPJ(string cpfCNPJ)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Pessoa as Tabela where Tabela.Cnpj = '" + cpfCNPJ + "' and Tabela.FlagExcluido <> true").UniqueResult<Pessoa>();
        }

        public Pessoa selecionarPessoaPorCodigoImportado(string codigoImportacao)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Pessoa as Tabela where Tabela.CodigoImportacao = '" + codigoImportacao + "' and Tabela.FlagExcluido <> true").UniqueResult<Pessoa>();
        }
        public Pessoa selecionarPessoaPorCodigoImportadoEFornecedor(string codigoImportacao, bool fornecedor)
        {
            Session = Conexao.GetSession();
            return Session.CreateQuery("from Pessoa as Tabela where Tabela.CodigoImportacao = '" + codigoImportacao + "'and Tabela.Fornecedor = "+ fornecedor + " and Tabela.FlagExcluido <> true").UniqueResult<Pessoa>();
        }
    }
}
