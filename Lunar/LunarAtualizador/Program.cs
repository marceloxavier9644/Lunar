using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunarAtualizador
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmAtualizador());
            AdicionarAoRegistro("LunarAtualizador", @"C:\Lunar\LunarAtualizador.exe");
            using (Mutex mutex = new Mutex(true, "{A7756E11-7B0F-4BC3-8F3B-6ED4DBB0A6E0}"))
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmAtualizador());
                    mutex.ReleaseMutex();
                }
                else
                {
                    // Outra instância da aplicação já está em execução
                    MessageBox.Show("O aplicativo já está em execução.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        static void AdicionarAoRegistro(string nomeChave, string caminhoDoAplicativo)
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6) // Windows Vista ou superior
                {
                    using (RegistryKey chaveInicializacao = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                    {
                        if (chaveInicializacao == null)
                        {
                            // Se a chave não existir, você pode criar ela aqui
                            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                        }

                        // Adiciona a entrada ao Registro
                        chaveInicializacao.SetValue(nomeChave, caminhoDoAplicativo);
                    }
                }
                if(Environment.OSVersion.Version.Build >= 9000)
                {
                    //Windows 11
                    using (RegistryKey chaveInicializacao = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run", true))
                    {
                        if (chaveInicializacao == null)
                        {
                            // Se a chave não existir, você pode criar ela aqui
                            Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run");
                        }

                        // Adiciona a entrada ao Registro
                        chaveInicializacao.SetValue(nomeChave, caminhoDoAplicativo);
                    }
                }
                Console.WriteLine("Entrada adicionada com sucesso ao Registro.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar ao Registro: {ex.Message}");
            }
        }
    }
}
