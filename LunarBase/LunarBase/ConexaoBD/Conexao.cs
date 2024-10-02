using LunarBase.Classes;
using LunarBase.Utilidades;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using System.Windows;

namespace LunarBase.ConexaoBD
{
    public class Conexao
    {
        public static ISessionFactory Factory;
        public static ISession Session;
        public static ITransaction Transaction;
        //conection
        //public static void IniciaTransacao()
        //{
        //    Transaction = GetSession().BeginTransaction();
        //}
        public static void IniciaTransacao()
        {
            var session = GetSession();
            if (session == null || !session.IsOpen)
            {
                throw new InvalidOperationException("A sessão NHibernate não está aberta.");
            }

            if (Transaction == null || !Transaction.IsActive)
            {
                Transaction = session.BeginTransaction();
            }
        }

        public static bool Commit()
        {
            try
            {
                Transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message + System.Environment.NewLine + e.StackTrace);
                throw new Exception("Erro ao fazer commit da transação." +
                           System.Environment.NewLine +
                           "Mensagem: " + e.Message +
                           System.Environment.NewLine +
                           "StackTrace: " + e.StackTrace);
            }
        }
        public static IStatelessSession GetStatelessSession()
        {
            return Factory.OpenStatelessSession();
        }
        public static void RollBack()
        {
            Transaction.Rollback();
        }

        public static void FechaConexaoBD()
        {
            GetSession().Close();
        }

        public Conexao(Boolean atualizaBanco = false)
        {
            try
            {
                if (Factory == null || atualizaBanco)
                {
                    String usuario = "";
                    String senha = "";
                    String servidor = "";
                    String bancoDados = "";

                    if (String.IsNullOrEmpty(servidor))
                    {
                        Logger logger = new Logger();
                        logger.WriteLog("Servidor 1: " + Sessao.servidor, "log");
                        if (!String.IsNullOrEmpty(Sessao.servidor))
                        {
                            servidor = Sessao.servidor;
                            usuario = Sessao.usuarioBanco;
                            senha = Sessao.senhaBanco;
                            bancoDados = Sessao.nomeBanco;
                            if (String.IsNullOrEmpty(bancoDados))
                                bancoDados = "lunar";
                        }
                        else
                        {
                            servidor = "localhost";
                            logger.WriteLog("Servidor 2: " + servidor, "log");
                            usuario = "marcelo";
                            senha = "mx123";
                            bancoDados = "lunar";
                        }
                    }

                    IDictionary<string, string> props = new Dictionary<string, string>();
                    String connectionString = null;

                    ////Normal
                    connectionString = "Server=" + servidor + ";Port=3306;Database=" + bancoDados + ";User ID=" + usuario + ";Password=" + senha + ";SslMode = none";
                    Sessao._conexaoMySQL = connectionString;
                    //SessaoVariaveis.stringConexao = connectionString;
                    props.Add("dialect", "NHibernate.Dialect.MySQL5InnoDBDialect");
                    props.Add("connection.driver_class", "NHibernate.Driver.MySqlDataDriver");
                    props.Add("query.substitutions", "true 1, false 0, yes 1, no 0");

                    string path = @System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    path = path.Replace("file:\\", "").Replace("file:\\", "");
                    props.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
                    props.Add("connection.connection_string", connectionString);
                    props.Add("show_sql", "true");
                    Configuration cfg = new Configuration();
                    cfg.AddProperties(props);

                    cfg.Configure(Path.Combine(path, "Configuracao.cfg.xml"));
                    if (atualizaBanco)
                        new SchemaUpdate(cfg).Execute(true, true);
                    Factory = cfg.BuildSessionFactory();
                }
            }
            catch (Exception Erro)
            {
                using (StreamWriter writer = new StreamWriter("erro.txt"))
                {
                    writer.Write("ERRO CONEXAO");
                    writer.WriteLine(Erro.Message);
                }
                //MessageBox.Show("Erro de conexão com o banco de dados " + System.Environment.NewLine + Erro.Message);
                throw new Exception("Erro de conexão! " + System.Environment.NewLine + Erro.Message);
            }

        }

        public static ISession GetSession()
        {
            try
            {
                if (Session == null || !Session.IsOpen)
                {
                    Session = Factory.OpenSession();
                }
                return Session;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar Session:" + e.Message);
            }
        }

    }
}
