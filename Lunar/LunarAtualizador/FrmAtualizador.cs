using LunarAtualiza.Utils;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using MySql.Data.MySqlClient;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LunarAtualizador
{
    public partial class FrmAtualizador : Form
    {
        private BackgroundWorker backgroundWorkerEmail;
        String cnpjClient = "";
        bool abriuForm = false;
        FrmNotificacao frmNotificacao = new FrmNotificacao();
        private static FrmAtualizador instanciaAtualizador;
        private Timer timer;
        public static FrmAtualizador InstanciaAtualizador
        {
            get
            {
                if (instanciaAtualizador == null || instanciaAtualizador.IsDisposed)
                {
                    instanciaAtualizador = new FrmAtualizador();
                }
                return instanciaAtualizador;
            }
        }
        NotifyIcon notifyIcon = new NotifyIcon();
        private string servidor = "";
        private string usuarioBanco = "";
        private string senhaBanco = "";
        private WebClient webClient;
        private string connectionString = "Server=mysql.lunarsoftware.com.br;Port=3306;Database=lunarsoftware02;User ID=lunarsoftware02;Password=aramxs11;SslMode = none";

        public FrmAtualizador()
        {
            InitializeComponent();
            verificaConfiguracaoBancoLocal();
            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClientDownloadProgressChanged;
            instanciaAtualizador = this;

            //Timer automático
            timer1.Start();
           
            Icon iconFromBitmap = CriarIconeTamanhoDesejado(LunarAtualizador.Properties.Resources.IconeLunarUpdate3, new Size(64, 64));
            notifyIcon = new NotifyIcon
            {
                Icon = iconFromBitmap,
                Text = "Atualizador Lunar Software",
                Visible = true,
                ContextMenuStrip = CriarMenu()
            };
            // Manipula o evento MouseDoubleClick para exibir o formulário quando o usuário dá dois cliques no ícone
            notifyIcon.MouseDoubleClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ExibirFormulario();
                }
            };
            Application.Run();
        }
        private Icon CriarIconeTamanhoDesejado(Bitmap bitmap, Size size)
        {
            // Redimensiona o bitmap para o tamanho desejado
            Bitmap resizedBitmap = new Bitmap(bitmap, size);

            // Cria um ícone a partir do bitmap redimensionado
            Icon icon = Icon.FromHandle(resizedBitmap.GetHicon());

            // Libera o bitmap original
            bitmap.Dispose();

            return icon;
        }
        private static void ExibirFormulario()
        {
            // Verifica se há uma instância válida antes de exibir o formulário
            if (instanciaAtualizador != null && !instanciaAtualizador.IsDisposed)
            {
                instanciaAtualizador.Show();
                instanciaAtualizador.WindowState = FormWindowState.Normal;
                instanciaAtualizador.BringToFront();
            }
        }
        private static ContextMenuStrip CriarMenu()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem itemMostrarMensagem = new ToolStripMenuItem("Abrir Atualizador");
            itemMostrarMensagem.Click += (sender, e) =>
            {
                ExibirFormulario();
            };
            contextMenuStrip.Items.Add(itemMostrarMensagem);

            ToolStripMenuItem itemSair = new ToolStripMenuItem("Sair");
            itemSair.Click += (sender, e) =>
            {
                Application.Exit();
            };
            contextMenuStrip.Items.Add(itemSair);

            return contextMenuStrip;
        }
        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Atualizar o valor da ProgressBar durante o download
            progressBarAdv1.Value = e.ProgressPercentage;
        }

        private async void btnVerificarAtualização_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Lunar\Lunar.exe"))
            {
                btnVerificarAtualização.Enabled = false;
                string caminhoParaExe = @"C:\Lunar\Lunar.exe";
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(caminhoParaExe);
                string versaoAtual = info.FileVersion;

                if (VerificarNovaVersaoDisponivel(versaoAtual))
                {
                    //DialogResult result = MessageBox.Show("Nova versão disponível. Deseja atualizar?", "Atualização Disponível", MessageBoxButtons.YesNo);

                    //if (result == DialogResult.Yes)
                    //{ 
                    this.ControlBox = false;
                    verificarSistemaAberto();
                    progressBarAdv1.Visible = true;
                    progressBarAdv1.Value = 5;
                    await BaixarEAtualizarAsync();
                    //}
                }
                else
                {
                    MessageBox.Show("Você já possui a versão mais recente.", "Sem Atualizações");
                }
            }
            else
                MessageBox.Show("Sistema não encontrado, o sistema lunar deve ta instalado na pasta C:\\Lunar");
        }
        public bool VerificarNovaVersaoDisponivel(string versaoAtual)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM atualizador WHERE Versao <> @VersaoAtual", connection);
                cmd.Parameters.AddWithValue("@VersaoAtual", versaoAtual);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Tratar a exceção conforme necessário (por exemplo, logar o erro)
                Console.WriteLine($"Erro ao verificar a versão: {ex.Message}");
                return false; // Ou lançar a exceção novamente, dependendo dos requisitos
            }
        }

        private async Task BaixarEAtualizarAsync()
        {
            lblNovaVersaoLocalizada.Text = "Aguarde, não feche o atualizador...";
            lblNovaVersaoLocalizada.Visible = true;

            string[] urls = ObterUrlsDosArquivos();
            string destino = ObterCaminhoDestino();

            progressBarAdv1.Minimum = 0;
            progressBarAdv1.Maximum = urls.Length;
            progressBarAdv1.Value = 5;

            await BaixarArquivosAsync(urls, destino);

            //SubstituirArquivos(destino);
            DescompactarArquivoRar();

            //Conecta no banco de dados do cliente e atualiza...
            lblNovaVersaoLocalizada.Visible = true;
            lblNovaVersaoLocalizada.Text = "Aguarde ajuste no banco de dados...";
            progressBarAdv1.Visible = false;
            progressBarAdv1.Value = 0;
            await Task.Run(() => AtualizarBancoDeDados());
            lblNovaVersaoLocalizada.Text = "Atualizado...";
            lblNovaVersaoLocalizada.Visible = false;
            //fim da atualização

            progressBarAdv1.Visible = false;
            EnviarEmailAposAtualizacaoAsync();
            MessageBox.Show("Atualização concluída com sucesso!", "Atualização Concluída");
            btnVerificarAtualização.Enabled = true;
            this.ControlBox = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        private void AtualizarBancoDeDados()
        {
            Controller.getInstanceAtualiza();
            Controller.getInstance().geraValoresPadrao();
        }

        private void verificarSistemaAberto()
        {
            string nomeProcesso = "Lunar";
            // Verifica se o processo está em execução
            if (Process.GetProcessesByName(nomeProcesso).Length > 0)
            {
                // O processo está em execução, então tentamos fechá-lo
                if (FecharProcesso(nomeProcesso))
                {
                    Console.WriteLine($"O processo {nomeProcesso} foi fechado com sucesso.");
                }
                else
                {
                    Console.WriteLine($"Não foi possível fechar o processo {nomeProcesso}.");
                    return; // Pode decidir não continuar com a atualização se não for possível fechar o processo.
                }
            }
            else
            {
                Console.WriteLine($"O processo {nomeProcesso} não está em execução.");
            }
        }
      
        private static bool FecharProcesso(string nomeProcesso)
        {
            try
            {
                // Obtém todos os processos com o mesmo nome
                Process[] processos = Process.GetProcessesByName(nomeProcesso);

                // Fecha cada instância do processo
                foreach (Process processo in processos)
                {
                    processo.CloseMainWindow(); // Tenta fechar a janela principal
                    processo.WaitForExit(5000); // Espera até 5 segundos para a finalização
                    if (!processo.HasExited)
                    {
                        processo.Kill(); // Se ainda estiver em execução, força o encerramento
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar fechar o processo {nomeProcesso}: {ex.Message}");
                return false;
            }
        }
        private async Task BaixarArquivosAsync(string[] urls, string destino)
        {
            for (int i = 0; i < urls.Length; i++)
            {
                string nomeArquivo = $"Atu{i + 1}.rar";

                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        // Baixar o arquivo como bytes
                        byte[] arquivoBytes = await httpClient.GetByteArrayAsync(urls[i]);

                        // Salvar o arquivo localmente
                        File.WriteAllBytes(Path.Combine(destino, nomeArquivo), arquivoBytes);

                        progressBarAdv1.Value = i + 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao baixar o arquivo {nomeArquivo}: {ex.Message}");
                    // Lógica para lidar com o erro, se necessário
                }
            }
        }

        private string[] ObterUrlsDosArquivos()
        {
            // Lógica para obter URLs dos arquivos
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT atualizador.Url FROM atualizador", connection);
            //cmd.Parameters.AddWithValue("@VersaoAtual", versaoAtual);

            string url = Convert.ToString(cmd.ExecuteScalar());
            return new string[] { url };
        }

        private string ObterCaminhoDestino()
        {
            // Lógica para obter o caminho de destino
            return @"C:\Lunar\";
        }

        private static void DescompactarArquivoRar()
        {
            using (Stream stream = File.OpenRead(@"C:\Lunar\Atu1.rar"))
            {
                var reader = ReaderFactory.Open(stream);

                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        reader.WriteEntryToDirectory(@"C:\Lunar", new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
        }

        private void verificaConfiguracaoBancoLocal()
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml"))
            {
                DateTime dataXML = DateTime.Now.AddDays(-1000);
                String hd = "";
                cnpjClient = "";
                String serialRegistro = "";
                string verificaBloqueio = "100";
                try
                {
                    XmlTextReader reader = new XmlTextReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppUser")
                            dataXML = DateTime.Parse(GenericaDesktop.Descriptografa(reader.ReadString()));
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppServ")
                            hd = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppOper")
                            cnpjClient = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppClient")
                            serialRegistro = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppComon")
                            verificaBloqueio = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Servidor")
                            servidor = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Usuario")
                            usuarioBanco = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Senha")
                            senhaBanco = GenericaDesktop.Descriptografa(reader.ReadString());
                    }
                    reader.Dispose();

                    Sessao.servidor = servidor;
                    Sessao.usuarioBanco = usuarioBanco;
                    Sessao.senhaBanco = senhaBanco;

                    reader = new XmlTextReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/MXSystem.xml");
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppUser")
                            dataXML = DateTime.Parse(GenericaDesktop.Descriptografa(reader.ReadString()));
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppServ")
                            hd = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppOper")
                            cnpjClient = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppClient")
                            serialRegistro = GenericaDesktop.Descriptografa(reader.ReadString());
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "AppComon")
                            verificaBloqueio = (GenericaDesktop.Descriptografa(reader.ReadString()));
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Servidor")
                            servidor = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Usuario")
                            usuarioBanco = reader.ReadString();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Senha")
                            senhaBanco = GenericaDesktop.Descriptografa(reader.ReadString());
                    }
                    Sessao.cnpjRegistro = cnpjClient;
                    Sessao.serialPainel = serialRegistro;
                    Sessao.servidor = servidor;
                    Sessao.usuarioBanco = usuarioBanco;
                    Sessao.senhaBanco = senhaBanco;
                    reader.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ocorreu uma falha ao validar a chave do sistema. Arquivo não Localizado!" + e.Message);
                    Environment.Exit(0);
                }
            }
        }

        private void FrmAtualizador_Load(object sender, EventArgs e)
        {

        }

        private void FrmAtualizador_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se o usuário clicou no botão de fechar, cancelar o fechamento e minimizar para a bandeja
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // Cancelar o fechamento
                this.WindowState = FormWindowState.Minimized;  // Minimizar para a bandeja do sistema
                this.ShowInTaskbar = false;  // Ocultar o formulário da barra de tarefas
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime agora = DateTime.Now;

            // Horários desejados para verificação (por exemplo, 9h e 15h)
            DateTime horarioVerificacao1 = new DateTime(agora.Year, agora.Month, agora.Day, 09, 00, 00);
            DateTime horarioVerificacao2 = new DateTime(agora.Year, agora.Month, agora.Day, 15, 30, 00);
            DateTime horarioVerificacao3 = new DateTime(agora.Year, agora.Month, agora.Day, 22, 52, 00);
          
            lblAgora.Text = agora.ToLongTimeString();
            // Verifica se é um dos horários desejados
            if (agora.ToLongTimeString() == horarioVerificacao1.ToLongTimeString() || agora.ToLongTimeString() == horarioVerificacao2.ToLongTimeString() || agora.ToLongTimeString() == horarioVerificacao3.ToLongTimeString())
            {

                if (abriuForm == false)
                {
                    abriuForm = true;
                    frmNotificacao = new FrmNotificacao();
                    frmNotificacao.WindowState = FormWindowState.Normal;
                    frmNotificacao.BringToFront();
                    if (frmNotificacao.ShowDialog() == DialogResult.OK)
                    {
                        bool atualiza = frmNotificacao.Atualiza;
                        //Atualizar o sistema aqui...
                        ExibirFormulario();
                        btnVerificarAtualização.PerformClick();
                        abriuForm = false;
                    }
                    else if (frmNotificacao.ShowDialog() == DialogResult.Cancel)
                    {
                        abriuForm = false;
                    }
                }
                // Executa a verificação de atualização
                //btnVerificarAtualização.PerformClick();
            }
        }
        private void InicializarBackgroundWorker()
        {
            backgroundWorkerEmail = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            // Adicione o evento DoWork necessário
            backgroundWorkerEmail.DoWork += BackgroundWorkerEmail_DoWork;
        }
        private void EnviarEmailAposAtualizacaoAsync()
        {
            // Certifique-se de inicializar o BackgroundWorker antes de usá-lo
            if (backgroundWorkerEmail == null)
            {
                InicializarBackgroundWorker();
            }

            // Verifique se o BackgroundWorker não está ocupado antes de iniciar a operação
            if (!backgroundWorkerEmail.IsBusy)
            {
                // Inicie o BackgroundWorker para enviar e-mails em segundo plano
                backgroundWorkerEmail.RunWorkerAsync();
            }
        }

        private void BackgroundWorkerEmail_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Configuração do cliente SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("txtinformaticabackup@gmail.com", "pqjwogipkqoxnzxc"),
                    EnableSsl = true
                };

                // Construir o e-mail
                MailMessage mensagem = new MailMessage
                {
                    From = new MailAddress("txtinformaticabackup@gmail.com"),
                    Subject = "Atualização Lunar",
                    Body = "Sistema Atualizado " + cnpjClient + " \r" + "Computador: " + Environment.MachineName
                    +" \r" + "Data Atualização: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(),
                    IsBodyHtml = false
                };

                // Adicionar destinatários (pode ser um array para vários destinatários)
                mensagem.To.Add("marcelo.xs@hotmail.com");
                mensagem.To.Add("contato@txtinformatica.com.br");

                // Enviar o e-mail
                smtpClient.Send(mensagem);

                // Relatar progresso (opcional)
                //backgroundWorkerEmail.ReportProgress(100);
            }
            catch (Exception ex)
            {
                // Relatar erro
                //MessageBox.Show($"Erro ao enviar e-mail: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
