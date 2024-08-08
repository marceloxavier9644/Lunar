using NHibernate;
using NHibernate.Cfg;
using ISession = NHibernate.ISession;

namespace LunarApi
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;
        private static ITransaction transaction;

        static NHibernateHelper()
        {
            try
            {
                var configuration = new Configuration();
                // Configuração programática do NHibernate
                configuration.DataBaseIntegration(db =>
                {
                    db.ConnectionString = "Server=localhost;Database=lunar;User ID=marcelo;Password=mx123;";
                    db.Dialect<NHibernate.Dialect.MySQLDialect>();
                    db.Driver<NHibernate.Driver.MySqlDataDriver>();
                });
                // Adicionar mapeamentos
                configuration.AddAssembly("LunarApi");

                sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao configurar NHibernate: " + ex.Message);
                throw;
            }
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        public static void BeginTransaction()
        {
            if (transaction != null && transaction.IsActive)
            {
                throw new InvalidOperationException("Já existe uma transação ativa.");
            }

            var session = OpenSession();
            transaction = session.BeginTransaction();
            Console.WriteLine("Transação NHibernate iniciada.");
        }

        public static void CommitTransaction()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Commit();
                Console.WriteLine("Transação NHibernate commitada.");
            }
        }

        public static void RollbackTransaction()
        {
            if (transaction != null && transaction.IsActive)
            {
                transaction.Rollback();
                Console.WriteLine("Transação NHibernate revertida.");
            }
        }

        public static void CloseSession()
        {
            var session = OpenSession();
            if (session != null && session.IsOpen)
            {
                session.Close();
                Console.WriteLine("Sessão NHibernate fechada.");
            }
        }
    }
}