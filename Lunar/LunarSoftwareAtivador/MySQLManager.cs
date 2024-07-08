using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LunarSoftwareAtivador
{
    public class MySQLManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // Construtor
        public MySQLManager()
        {
            Initialize();
        }

        // Inicializar parâmetros de conexão
        private void Initialize()
        {
            server = "mysql.lunarsoftware.com.br"; 
            database = "lunarsoftware"; 
            uid = "lunarsoftware"; 
            password = "p2NDx84b91Tg"; 
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        // Método para abrir conexão com o MySQL
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Tratar exceções de conexão aqui
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Método para fechar conexão com o MySQL
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Tratar exceções de fechamento de conexão aqui
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Método para consultar usuário por CNPJ
        public DataRow ConsultarUsuarioPorCNPJ(string cnpj)
        {
            string query = $"SELECT * FROM users WHERE cnpj = @cnpj and is_client = '1'";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@cnpj", cnpj);

                DataTable dataTable = new DataTable();
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro MySQL: " + ex.Message);
                    Console.WriteLine("Código de erro MySQL: " + ex.ErrorCode);
                    Console.WriteLine("StackTrace: " + ex.StackTrace);
                }
                finally
                {
                    this.CloseConnection();
                }

                // Retornar a primeira linha (supondo que só haja um resultado)
                return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
            }
            else
            {
                return null; // Retornar null se não conseguir abrir conexão
            }
        }
    }
}