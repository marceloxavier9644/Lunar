using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using ISession = NHibernate.ISession;

namespace LunarApi
{
    public class InitializeNHibernate
    {
        private static ISessionFactory _sessionFactory;

        public static void Initialize()
        {
            try
            {
                var configuration = new Configuration();

                // Configuração direta no código para MySQL
                configuration.DataBaseIntegration(db =>
                {
                    db.Dialect<MySQLDialect>();
                    db.Driver<MySqlDataDriver>();
                    db.ConnectionString = "Server=localhost;Database=lunar;User ID=marcelo;Password=mx123;Charset=utf8;";
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                });

                configuration.AddAssembly("LunarApi"); // Adicione o assembly contendo suas entidades

                _sessionFactory = configuration.BuildSessionFactory();
                Console.WriteLine("NHibernate configurado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao configurar NHibernate: {ex.Message}");
                throw;
            }
        }

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                throw new InvalidOperationException("NHibernate não foi inicializado.");
            }

            return _sessionFactory.OpenSession();
        }
    }
}
