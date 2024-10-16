using LunarBase.Classes;
using LunarBase.ConexaoBD;
using NHibernate.Transform;
using NHibernate;

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

        public Pessoa SelecionarPessoaPorID(string id)
        {
            using (var session = Conexao.GetSession())
            {
                // Utilizando consulta SQL nativa
                var sql = @"SELECT * FROM Pessoa 
                    WHERE ID = :id 
                    AND FLAGEXCLUIDO <> 1";

                // Executando a consulta nativa e mapeando o resultado para a classe Pessoa
                return session.CreateSQLQuery(sql)
                              .AddEntity(typeof(Pessoa)) // Mapeia o resultado para a entidade Pessoa
                              .SetParameter("id", id)    // Define o parâmetro ID
                              .UniqueResult<Pessoa>();   // Retorna o único resultado
            }
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

        public IList<Pessoa> selecionarClientesPorDataAniversarioSql(string sql)
        {
            Session = Conexao.GetSession();
            IList<Pessoa> retorno = Session.CreateQuery(sql).List<Pessoa>();
            return retorno;
        }

        public IList<PessoaConsulta> SelecionarPessoasSemComprasPorSQLNativo(string sql)
        {
            using (var session = Conexao.GetSession())
            {
                IList<PessoaConsulta> retorno = session.CreateSQLQuery(sql)
                    .AddScalar("ID", NHibernateUtil.Int32)
                    .AddScalar("RazaoSocial", NHibernateUtil.String)
                    .AddScalar("UltimaCompra", NHibernateUtil.DateTime)
                    .AddScalar("TotalCompras", NHibernateUtil.Decimal)
                    .AddScalar("Telefone", NHibernateUtil.String)
                    .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaConsulta)))
                    .List<PessoaConsulta>();

                return retorno;
            }
        }

        public IList<PessoaContatoTelefone> SelecionarContatoClientesParaExportarCsv(string sql)
        {
            using (var session = Conexao.GetSession())
            {
                IList<PessoaContatoTelefone> retorno = session.CreateSQLQuery(sql)
                    .AddScalar("Nome", NHibernateUtil.String)
                    .AddScalar("Telefone", NHibernateUtil.String)
                    .AddScalar("Email", NHibernateUtil.String)
                    .SetResultTransformer(Transformers.AliasToBean(typeof(PessoaContatoTelefone)))
                    .List<PessoaContatoTelefone>();

                return retorno;
            }
        }

        public class PessoaConsulta
        {
            public int ID { get; set; }
            public string RazaoSocial { get; set; }
            public DateTime? UltimaCompra { get; set; }
            public decimal? TotalCompras { get; set; }
            public string Telefone { get; set; } // Para armazenar o telefone formatado (ddd + telefone)
        }

        public class PessoaContatoTelefone
        {
            public string Nome { get; set; }
            public string Telefone { get; set; } 
            public string Email { get; set; }
        }
    }
}
