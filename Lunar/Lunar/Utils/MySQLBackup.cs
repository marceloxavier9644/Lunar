using LunarBase.Classes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils
{
    public class MySQLBackup
    {
        public static async Task ExportDatabase(string userName, string password, string serverAddress, string databaseName)
        {
            Logger logger = new Logger();
            try
            {
                // Abrir diálogo para o usuário selecionar a pasta onde salvar o backup
                string outputPath = GetBackupPath();
                if (string.IsNullOrEmpty(outputPath))
                {
                    Console.WriteLine("Operação de backup cancelada.");
                    return;
                }

                // Definir o nome do arquivo de backup
                string fileName = GetBackupFileName(outputPath, databaseName);
                string caminhoArquivo = Path.Combine(outputPath, fileName);

                // Caminho do mysqldump
                string caminhoMysqlDump = GetMySqlDumpPath();

                // Argumentos para o comando mysqldump
                string arguments = $"-h {serverAddress} -u {userName} -p{password} {databaseName}";
                if (string.IsNullOrEmpty(password))
                    arguments = $"-h {serverAddress} -u {userName} -p {databaseName}";

                ProcessStartInfo psi = new ProcessStartInfo(caminhoMysqlDump, arguments)
                {
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = psi;

                    // Inicializar um stream de arquivo para escrever a saída no arquivo de destino
                    using (StreamWriter outputFile = new StreamWriter(caminhoArquivo))
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
                                // Ignorar avisos específicos do mysqldump
                                if (!e.Data.Contains("Using a password on the command line interface can be insecure"))
                                {
                                    // Exibir outros erros
                                    MessageBox.Show($"Erro ao exportar banco de dados: {e.Data}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        };

                        // Mostrar a barra de progresso
                        using (Form progressForm = ShowProgressDialog())
                        {
                            process.Start();
                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();

                            // Aguardar a conclusão do processo em uma tarefa assíncrona
                            await Task.Run(() => process.WaitForExit());
                            progressForm.Close();
                        }

                        logger.WriteLog("Backup do banco de dados concluída com sucesso.", "LogExport");
                        MessageBox.Show("Backup concluído com sucesso!", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog($"Erro ao exportar banco de dados: {ex.Message}", "LogExport");
                MessageBox.Show($"Erro ao exportar banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetBackupFileName(string outputPath, string databaseName)
        {
            string fileName = $"{databaseName}_backup.sql";
            string caminhoArquivo = Path.Combine(outputPath, fileName);
            int count = 1;

            while (File.Exists(caminhoArquivo))
            {
                // Adiciona data e hora ao nome do arquivo
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                fileName = $"{databaseName}_backup_{timestamp}_{count}.sql";
                caminhoArquivo = Path.Combine(outputPath, fileName);
                count++;
            }

            return fileName;
        }

        private static string GetMySqlDumpPath()
        {
            // Caminho do mysqldump - altere conforme o caminho de instalação no sistema
            string caminhoMysqlDump = Path.Combine(@"C:\wamp64\bin\mysql\mysql5.7.36\bin", "mysqldump.exe");
            if (!File.Exists(caminhoMysqlDump))
            {
                caminhoMysqlDump = Path.Combine(@"C:\Lunar\MySql64\bin", "mysqldump.exe");
            }
            return caminhoMysqlDump;
        }

        private static Form ShowProgressDialog()
        {
            Form progressForm = new Form
            {
                Text = "Realizando Backup...",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Width = 400,
                Height = 180,
                BackColor = Color.FromArgb(191, 0, 239),
                MaximizeBox = false,
                MinimizeBox = false
            };

            ProgressBar progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Bottom,
                Height = 30,
                MarqueeAnimationSpeed = 30 // Velocidade da animação
            };
            progressForm.Controls.Add(progressBar);

            Label statusLabel = new Label
            {
                Text = "Lunar Software\n\nRealizando backup, por favor, aguarde...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(191, 0, 239),
                ForeColor = Color.White,
                Padding = new Padding(0, 0, 0, 25) 
            };
            progressForm.Controls.Add(statusLabel);

            progressForm.Show();

            return progressForm;
        }

        private static string GetBackupPath()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione a pasta onde deseja salvar o backup";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath;
                }
            }

            return null;
        }
    }
}
