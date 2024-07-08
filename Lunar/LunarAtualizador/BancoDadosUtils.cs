using LunarBase.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LunarAtualizador
{
    public class BancoDadosUtils
    {
        public static void ExportarBancoDados(string servidor, string usuario, string senha, string nomeBanco, string caminhoArquivo)
        {
            string connectionString = $"server={servidor};user={usuario};password={senha};database={nomeBanco}";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string script = ExportarBancoParaScript(connection);

                    File.WriteAllText(caminhoArquivo, script);

                    Console.WriteLine("Banco de dados exportado com sucesso para o arquivo: " + caminhoArquivo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao exportar banco de dados: " + ex.Message);
                }
            }
        }

        private static string ExportarBancoParaScript(MySqlConnection connection)
        {
            string script = "";
            List<string> tableNames = ObterNomesTabelas(connection);

            // Dividir a lista de tabelas em batches menores
            int batchSize = 10; // Defina o tamanho do batch
            for (int i = 0; i < tableNames.Count; i += batchSize)
            {
                List<string> batch = tableNames.Skip(i).Take(batchSize).ToList();
                foreach (string tableName in batch)
                {
                    if (tableName != "cidade")
                    {
                        script += ExportarTabelaParaScript(connection, tableName);
                    }
                    else
                    {
                        script += ExportarEstruturaTabela(connection, tableName);
                    }
                }
            }

            return script;
        }
        private static string ExportarEstruturaTabela(MySqlConnection connection, string tableName)
        {
            string script = $"-- Estrutura da Tabela: {tableName}\n";
            string structureQuery = $"SHOW CREATE TABLE {tableName}";

            try
            {
                using (MySqlCommand command = new MySqlCommand(structureQuery, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // O índice 1 contém a declaração CREATE TABLE
                        script += $"{reader.GetString(1)};\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exportar a estrutura da tabela {tableName}: {ex.Message}");
                // Adicione mais lógica de tratamento de exceção, se necessário
            }

            return script;
        }
        private static List<string> ObterNomesTabelas(MySqlConnection connection)
        {
            List<string> tableNames = new List<string>();
            string tablesQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = @databaseName";

            using (MySqlCommand command = new MySqlCommand(tablesQuery, connection))
            {
                command.Parameters.AddWithValue("@databaseName", connection.Database);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }

            return tableNames;
        }

        private static string ExportarTabelaParaScript(MySqlConnection connection, string tableName)
        {
            string script = $"-- Tabela: {tableName}\n";

            try
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"SELECT * FROM {tableName}";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            script += $"INSERT INTO {tableName} VALUES (";

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i > 0)
                                    script += ", ";

                                object value = reader.GetValue(i);
                                if (value == DBNull.Value)
                                    script += "NULL";
                                else if (value is string || value is DateTime)
                                    script += $"'{value}'";
                                else
                                    script += value;
                            }

                            script += ");\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exportar tabela {tableName}: {ex.Message}");
                // Adicione mais lógica de tratamento de exceção, se necessário
            }

            return script + "\n";
        }

        public static void ImportarBancoDados(string servidor, string usuario, string senha, string nomeBanco, string caminhoArquivo)
        {
            string connectionString = $"Server={servidor};Uid={usuario};Pwd={senha};Database={nomeBanco}";
            Logger logger = new Logger();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string script = File.ReadAllText(caminhoArquivo);
                    MySqlCommand command = new MySqlCommand(script, connection)
                    {
                        CommandTimeout = 600 // 10 minutos
                    };
                    command.ExecuteNonQuery();

                    logger.WriteLog("Banco de dados importado com sucesso para o servidor na nuvem.", "Logs");
                    Console.WriteLine("Banco de dados importado com sucesso para o servidor na nuvem.");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Erro ao importar banco de dados para nuvem: " + ex.Message, "Logs");
                Console.WriteLine("Erro ao importar banco de dados: " + ex.Message);
            }
        }

        //DUMP
        public static void ExportDatabase(string userName, string password, string databaseName, string outputPath)
        {
            try
            {
                string caminhoMysqlDump = Path.Combine(@"C:\wamp64\bin\mysql\mysql5.7.36\bin", "mysqldump.exe");
                string arguments = $"-u {userName} -p{password} {databaseName}";

                ProcessStartInfo psi = new ProcessStartInfo(caminhoMysqlDump, arguments);
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = psi;

                    // Inicializar um stream de arquivo para escrever a saída no arquivo de destino
                    using (StreamWriter outputFile = new StreamWriter(outputPath))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                            {
                                outputFile.WriteLine(e.Data);
                            }
                        };

                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                            {
                                Console.WriteLine($"Erro ao exportar banco de dados: {e.Data}");
                            }
                        };

                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        process.WaitForExit();

                        Console.WriteLine("Exportação do banco de dados concluída com sucesso.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exportar banco de dados: {ex.Message}");
            }
        }



    }
}
