using LunarAtualiza.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.ZAPZAP;
using MySql.Data.MySqlClient;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LunarAtualizador
{
    public partial class FrmAtualizador : Form
    {
        Logger logger = new Logger();
        string nomeDoComputador = "";
        string nomeServidorConfigurado = "";
        string idWhats = "";
        string tokenWhats = "";
        String ativarMensagemLembreteExame = "";
        String ativarMensagemPosVendas = "";
        String horarioLembreteExame = "";
        string horaLembreteExame = "";
        string minutoLembreteExame = "";
        string mensagemPosVenda = "";
        private BackgroundWorker backgroundWorkerEmail;
        String cnpjClient = "";
        bool abriuForm = false;
        FrmNotificacao frmNotificacao = new FrmNotificacao();
        private static FrmAtualizador instanciaAtualizador;
        private Timer timer;
        string servidorNuvem = "";
        string bancoNuvem = "";
        string usuarioNuvem = "";
        string senhaNuvem = "";
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
        string connectionStringLocal = "Server=localhost;Database=lunar;User ID=marcelo;Password=mx123;";
        public FrmAtualizador()
        {
            InitializeComponent();
            nomeDoComputador = Environment.MachineName;
            verificaConfiguracaoBancoLocal();
            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClientDownloadProgressChanged;
            instanciaAtualizador = this;
            //parametros para verificar integracao com dashboards
            lerParametrosSistema();
            string atualizaBanco = @"C:\Lunar\Atualizador\AtualizaBanco.txt";
            if (File.Exists(atualizaBanco))
            {
                ExibirFormulario();
                lblNovaVersaoLocalizada.Enabled = true;
                lblNovaVersaoLocalizada.Visible = true;
                lblNovaVersaoLocalizada.Text = "Aguarde, Atualizando Banco de Dados.... Não feche!!!";
                lblNovaVersaoLocalizada.ForeColor = Color.Red;
                AtualizarBancoDeDados();
                lblNovaVersaoLocalizada.Visible = false;
                lblNovaVersaoLocalizada.Text = "";
                File.Delete(atualizaBanco);
            }
            conferirHorarioMensagens();
            if (ativarMensagemLembreteExame.Equals("True"))
            {
                if (!String.IsNullOrEmpty(horarioLembreteExame))
                {
                    string formatoHorario = "HH:mm";

                    // Tenta fazer o parse da string para um objeto DateTime
                    if (DateTime.TryParseExact(horarioLembreteExame, formatoHorario, null, System.Globalization.DateTimeStyles.None, out DateTime horarioParsed))
                    {
                        // O parse foi bem-sucedido, então obtemos a hora e o minuto
                        horaLembreteExame = horarioParsed.Hour.ToString();
                        minutoLembreteExame = horarioParsed.Minute.ToString();
                    }
                }
            }

            //Timer automático
            timer1.Start();

            //timer atualiza banco de dados nuvem
            timerExportImport.Interval = 30 * 60 * 1000; // 30 minutos em milissegundos
            timerExportImport.Tick += timerExportImport_Tick;
            timerExportImport.Start();

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
                // Verifica se o formulário principal está aberto e fecha-o
                Form principalForm = Application.OpenForms.OfType<FrmAtualizador>().FirstOrDefault();
                if (principalForm != null)
                {
                    principalForm.Close();
                }

                // Finaliza o aplicativo
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
            atualizar();
        }

        private async void atualizar()
        {
            try
            {
                string arquivoAtu1 = @"C:\Lunar\Atu1.rar";

                // Deleta o arquivo Atu1.rar se existir
                if (File.Exists(arquivoAtu1))
                {
                    File.Delete(arquivoAtu1);
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções ao deletar o arquivo, se necessário
                Console.WriteLine($"Erro ao deletar o arquivo Atu1.rar: {ex.Message}");
            }

            if (File.Exists(@"C:\Lunar\Lunar.exe"))
            {
                // btnVerificarAtualização.Enabled = false;
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
                    //progressBarAdv1.Value = 0;
                    await BaixarEAtualizarAsync();
                    //}
                }
                else
                {
                    MessageBox.Show("Você já possui a versão mais recente.", "Sem Atualizações");
                    this.ControlBox = true;
                    btnVerificarAtualização.Enabled = true;
                }
            }
            else
            {
                //btnVerificarAtualização.Enabled = false;
                this.ControlBox = false;
                verificarSistemaAberto();
                progressBarAdv1.Visible = true;
                //progressBarAdv1.Value = 0;
                await BaixarEAtualizarAsync();
            }
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
            //progressBarAdv1.Value = ;

            await BaixarArquivosAsync(urls, destino);

            //SubstituirArquivos(destino);
            DescompactarArquivoRar();

            //Conecta no banco de dados do cliente e atualiza...
            lblNovaVersaoLocalizada.Visible = true;
            lblNovaVersaoLocalizada.Text = "Aguarde, ajuste no banco de dados... Não Feche!!!";
            lblNovaVersaoLocalizada.ForeColor = Color.Red;
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

        //private static void DescompactarArquivoRar()
        //{
        //    using (Stream stream = File.OpenRead(@"C:\Lunar\Atu1.rar"))
        //    {
        //        var reader = ReaderFactory.Open(stream);

        //        while (reader.MoveToNextEntry())
        //        {
        //            if (!reader.Entry.IsDirectory)
        //            {
        //                reader.WriteEntryToDirectory(@"C:\Lunar", new ExtractionOptions()
        //                {
        //                    ExtractFullPath = true,
        //                    Overwrite = true
        //                });
        //            }
        //        }
        //    }
        //    // Após a descompactação, execute o arquivo .bat
        //    string caminhoBat = @"C:\Lunar\Atualizador\AtuAtualizador.bat";
        //    Process.Start(caminhoBat);
        //}

        private static void DescompactarArquivoRar()
        {
            string pastaDestino = @"C:\Lunar";
            string arquivoRar = @"C:\Lunar\Atu1.rar";
            string arquivoBat = @"C:\Lunar\AtuAtualizador.bat";

            // Descompacta o arquivo RAR
            using (Stream stream = File.OpenRead(arquivoRar))
            {
                var reader = ReaderFactory.Open(stream);

                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        reader.WriteEntryToDirectory(pastaDestino, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }

            // Verifica se o arquivo AtuAtualizador.bat existe após a descompactação
            if (File.Exists(arquivoBat))
            {
                // Copia o arquivo AtuAtualizador.bat para a pasta C:\Lunar\Atualizador
                File.Copy(arquivoBat, Path.Combine(pastaDestino, "Atualizador", Path.GetFileName(arquivoBat)), true);
            }
            // Após a descompactação, execute o arquivo .bat
            string caminhoBat = @"C:\Lunar\Atualizador\AtuAtualizador.bat";
            if (File.Exists(caminhoBat))
                Process.Start(caminhoBat);
        }


        private void verificaConfiguracaoBancoLocal()
        {
            if (File.Exists(@"C:\Lunar\MXSystem.xml"))
            {
                DateTime dataXML = DateTime.Now.AddDays(-1000);
                String hd = "";
                cnpjClient = "";
                String serialRegistro = "";
                string verificaBloqueio = "100";
                try
                {
                    //XmlTextReader reader = new XmlTextReader(System.IO.Path.GetDirectoryName(@"C:\Lunar\MXSystem.xml"));
                    XmlTextReader reader = new XmlTextReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "C:\\Lunar\\MXSystem.xml"));
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

                    //reader = new XmlTextReader(System.IO.Path.GetDirectoryName(@"C:\Lunar\MXSystem.xml"));
                    reader = new XmlTextReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "C:\\Lunar\\MXSystem.xml"));
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

                // Ocultar o formulário da tela antes de minimizá-lo
                this.Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime agora = DateTime.Now;
                string caminhoPastaExportacao = "exportBD";
                string caminhoArquivo = Path.Combine(caminhoPastaExportacao, $"{"lunar"}_backup.sql");

                if (!Directory.Exists(caminhoPastaExportacao))
                {
                    Directory.CreateDirectory(caminhoPastaExportacao);
                }
                //ExportarParaNuvem("localhost", "marcelo", "mx123", "lunar", "mysql.lunarsoftware.com.br", "lunarsoftware01", "aranha1", "lunarsoftware01");
                //BancoDadosUtils.ExportDatabase("marcelo", "mx123", "lunar", caminhoArquivo);
                //BancoDadosUtils.ImportarBancoDados("mysql.lunarsoftware.com.br", "lunarsoftware01", "aranha1", "lunarsoftware01", caminhoArquivo);

                // Horários desejados para verificação (por exemplo, 9h e 15h)
                DateTime horarioVerificacao1 = new DateTime(agora.Year, agora.Month, agora.Day, 9, 0, 0);
                DateTime horarioVerificacao2 = new DateTime(agora.Year, agora.Month, agora.Day, 15, 30, 0);
                DateTime horarioVerificacao3 = new DateTime(agora.Year, agora.Month, agora.Day, 10, 5, 0);

                DateTime horarioVerificacao4LembreteExame = new DateTime(agora.Year, agora.Month, agora.Day, 12, 0, 0);

                if (ativarMensagemLembreteExame.Equals("True"))
                {
                    // Verifica se é hora de disparar o lembrete de exame
                    if (agora.Hour == int.Parse(horaLembreteExame) && agora.Minute == int.Parse(minutoLembreteExame))
                    {
                        dispararMensagens_passo01();
                    }
                }

                lblAgora.Text = agora.ToLongTimeString();

                // Verifica se é um dos horários desejados
                if (agora.TimeOfDay == horarioVerificacao1.TimeOfDay || agora.TimeOfDay == horarioVerificacao2.TimeOfDay || agora.TimeOfDay == horarioVerificacao3.TimeOfDay)
                {
                    if (!abriuForm)
                    {
                        if (File.Exists(@"C:\Lunar\Lunar.exe"))
                        {
                            string caminhoParaExe = @"C:\Lunar\Lunar.exe";
                            FileVersionInfo info = FileVersionInfo.GetVersionInfo(caminhoParaExe);
                            string versaoAtual = info.FileVersion;

                            if (VerificarNovaVersaoDisponivel(versaoAtual))
                            {
                                abriuForm = true;
                                frmNotificacao = new FrmNotificacao();
                                frmNotificacao.WindowState = FormWindowState.Normal;
                                frmNotificacao.BringToFront();
                                DialogResult result = frmNotificacao.ShowDialog();

                                if (result == DialogResult.OK)
                                {
                                    // Se o usuário confirmou a atualização, faça a atualização
                                    ExibirFormulario();
                                    atualizar();
                                }

                                abriuForm = false;
                            }
                        }
                    }
                }

                if (ativarMensagemPosVendas.Equals("True") && nomeDoComputador.Equals(nomeServidorConfigurado, StringComparison.OrdinalIgnoreCase))
                {
                    // Verifica novas mensagens a cada 10 minutos
                    if (agora.Minute % 10 == 0)
                    {
                        //logger.WriteLog("Este é o servidor confirmado, verificação de 10 minutos...");
                        consultaMensagens();
                        if (Sessao.MensagensAgendadas.Count > 0)
                        {
                            logger.WriteLog("Sessao.MensagensAgendadas.Count > 0...OK ---- " + Sessao.MensagensAgendadas.Count, "LogMensagem");
                            foreach (var mensagem in Sessao.MensagensAgendadas.ToList()) // Usar ToList para evitar exceção de modificação durante a iteração
                            {
                                if (agora >= mensagem.DataAgendamento)
                                {
                                    logger.WriteLog("Preparando Disparo de Mensagem ---- NOME: " + mensagem.NomeCliente + " MENSAGEM: " + mensagemPosVenda + " FLAGENVIADA: " + mensagem.FlagEnviada, "LogMensagem");
                                    if (!String.IsNullOrEmpty(mensagem.NomeCliente) && !String.IsNullOrEmpty(mensagemPosVenda) && !mensagem.FlagEnviada)
                                    {
                                        try
                                        {
                                            dispararMensagemPosVenda(mensagem.NomeCliente, mensagemPosVenda, mensagem.Pessoa);
                                            mensagem.FlagEnviada = true;
                                            Controller.getInstance().salvar(mensagem);
                                        }
                                        catch
                                        {
                                            mensagem.FlagEnviada = true;
                                            Controller.getInstance().salvar(mensagem);
                                        }
                                    }
                                }
                            }
                            Sessao.MensagensAgendadas.Clear();
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void consultaMensagens()
        {
            MensagemPosVenda msgPos = new MensagemPosVenda();
            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão

                    connection.Open();
                    Sessao.parametroSistema = new ParametroSistema();
                    string query = "SELECT MP.* FROM MensagemPosVenda MP INNER JOIN (SELECT Pessoa, MIN(DataAgendamento) AS MinDataAgendamento FROM MensagemPosVenda WHERE DATE(DataAgendamento) <= CURDATE() AND FlagEnviada = false GROUP BY Pessoa) AS PessoasUnicas ON MP.Pessoa = PessoasUnicas.Pessoa AND MP.DataAgendamento = PessoasUnicas.MinDataAgendamento WHERE DATE(MP.DataAgendamento) <= CURDATE() AND MP.FlagEnviada = false";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Faça algo com os resultados
                            while (reader.Read())
                            {
                                msgPos.Id = Convert.ToInt32(reader["Id"]);
                                msgPos.DataAgendamento = DateTime.Parse($"{reader["DataAgendamento"]}");
                                msgPos.NomeCliente = $"{reader["NomeCliente"]}";
                                msgPos.Pessoa = new Pessoa();
                                msgPos.Pessoa.Id = Convert.ToInt32(reader["Pessoa"]);
                                msgPos.Pessoa = (Pessoa)Controller.getInstance().selecionar(msgPos.Pessoa);
                                msgPos.FlagEnviada = false;
                                if (String.IsNullOrEmpty(msgPos.NomeCliente))
                                    msgPos.NomeCliente = msgPos.Pessoa.RazaoSocial;
                                Sessao.MensagensAgendadas.Add(msgPos);

                                logger.WriteLog("Mensagens agendadas: " + Sessao.MensagensAgendadas.Count, "LogMensagem");

                            }
                        }
                    }
                }
                catch
                {

                }
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
                    + " \r" + "Data Atualização: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(),
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

        private void dispararMensagens_passo01()
        {
            string msgLembreteExame = "";
            string diasAntes = "";
            nomeServidorConfigurado = "";


            string mensagemPosVenda = "";
            string tempoMensagemPosVenda = "";
            string minutoOuHoraPosVenda = "";

            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();
                    Sessao.parametroSistema = new ParametroSistema();
                    // Realize uma consulta simples (substitua "suaTabela" e "seuCampo" conforme necessário)
                    string query = "SELECT * FROM ParametroSistema";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Faça algo com os resultados
                            while (reader.Read())
                            {
                                nomeServidorConfigurado = $"{reader["nomeServidor"]}";
                                ativarMensagemPosVendas = $"{reader["ATIVARMENSAGEMPOSVENDAS"]}";
                                ativarMensagemLembreteExame = $"{reader["ATIVARMENSAGEMVENCIMENTOEXAME"]}";
                                mensagemPosVenda = $"{reader["MENSAGEMPOSVENDAS"]}";
                                
                                servidorNuvem = $"{reader["SERVIDORNUVEM"]}";
                                bancoNuvem = $"{reader["BANCONUVEM"]}";
                                usuarioNuvem = $"{reader["USUARIONUVEM"]}";
                                senhaNuvem = GenericaDesktop.Descriptografa($"{reader["SENHANUVEM"]}");
                            }
                        }
                        if (ativarMensagemLembreteExame.Equals("True"))
                        {
                            //Compara sem considerar maiusculo e minusculo
                            if (nomeDoComputador.Equals(nomeServidorConfigurado, StringComparison.OrdinalIgnoreCase))
                            {
                                query = "SELECT * FROM ParametroSistema";
                                using (MySqlCommand command2 = new MySqlCommand(query, connection))
                                {
                                    using (MySqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        // Faça algo com os resultados
                                        while (reader2.Read())
                                        {
                                            // Acesse os dados do banco de dados e faça o que for necessário
                                            msgLembreteExame = reader2["MensagemLembreteExame"].ToString();
                                            diasAntes = reader2["MENSAGEMLEMBREEXAMEQTDDIAS"].ToString();
                                        }
                                    }
                                }
                                listaExamesVencendo(int.Parse(diasAntes), msgLembreteExame);
                            }
                        }
                        if (ativarMensagemPosVendas.Equals("True"))
                        {
                            //Compara sem considerar maiusculo e minusculo
                            if (nomeDoComputador.Equals(nomeServidorConfigurado, StringComparison.OrdinalIgnoreCase))
                            {
                                query = "SELECT * FROM ParametroSistema";
                                using (MySqlCommand command2 = new MySqlCommand(query, connection))
                                {
                                    using (MySqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        // Faça algo com os resultados
                                        while (reader2.Read())
                                        {
                                            // Acesse os dados do banco de dados e faça o que for necessário
                                            mensagemPosVenda = reader2["MensagemPosVendas"].ToString();
                                            minutoOuHoraPosVenda = reader2["MensagemPosVendasDiasOuMinutos"].ToString();
                                            tempoMensagemPosVenda = reader2["MensagemPosVendasQtdDiasOuMinutos"].ToString();
                                        }
                                    }
                                }
                                //listaExamesVencendo(int.Parse(diasAntes), msgLembreteExame);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        private void lerParametrosSistema()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Realize uma consulta simples
                    string query = "SELECT * FROM ParametroSistema";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Verificar se há linhas retornadas
                            if (reader.HasRows)
                            {
                                // Ler os resultados
                                while (reader.Read())
                                {
                                    // Faça algo com os resultados
                                    nomeServidorConfigurado = reader["nomeServidor"].ToString();
                                    ativarMensagemPosVendas = reader["ATIVARMENSAGEMPOSVENDAS"].ToString();
                                    ativarMensagemLembreteExame = reader["ATIVARMENSAGEMVENCIMENTOEXAME"].ToString();
                                    mensagemPosVenda = reader["MENSAGEMPOSVENDAS"].ToString();

                                    servidorNuvem = $"{reader["SERVIDORNUVEM"]}";
                                    bancoNuvem = $"{reader["BANCONUVEM"]}";
                                    usuarioNuvem = $"{reader["USUARIONUVEM"]}";
                                    senhaNuvem = GenericaDesktop.Descriptografa($"{reader["SENHANUVEM"]}");
                                }
                            }
                            else
                            {
                                // Nenhuma linha retornada
                                Console.WriteLine("Nenhuma linha retornada pela consulta.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lidar com exceções
                    logger.WriteLog($"Erro ao executar a consulta a parametros do sistema: {ex.Message}", "Logs");
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                }
            }
        }
        private void listaExamesVencendo(int diasAntesDoVencimento, string mensagemLembrete)
        {
            string idOrdemServico = "";
            String ddd = "";
            String telefoneCliente = "";
            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Crie a consulta SQL
                    string query = @"
                           SELECT os.*, pessoatelefone.*, pessoa.*, exame.*
                            FROM OrdemServico os
                            JOIN OrdemServicoExame exame ON os.ID = exame.OrdemServico
                            JOIN Pessoa pessoa ON os.CLIENTE = pessoa.ID
                            JOIN PessoaTelefone pessoatelefone ON pessoa.ID = pessoatelefone.PESSOA
                            WHERE os.Status = 'Encerrada' AND os.MensagemVencimentoExameOtica <> true AND
                                  STR_TO_DATE(exame.ProximoExame, '%d/%m/%Y') = DATE_ADD(CURDATE(), INTERVAL @DiasAntesDoVencimento DAY)
                    ";

                    // Crie o comando e parâmetro
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DiasAntesDoVencimento", diasAntesDoVencimento);

                        // Execute a consulta
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Processar resultados
                                //Console.WriteLine($"Número da Ordem de Serviço: {reader["ID"]}");
                                idOrdemServico = $"{reader["Id"]}";
                                ddd = $"{reader["DDD"]}";
                                telefoneCliente = $"{reader["TELEFONE"]}";
                                String nomeCli = $"{reader["RazaoSocial"]}";
                                string proxVencimentoExame = $"{reader["ProximoExame"]}";
                                // Adicione outras informações conforme necessário
                                if (ValidarNumeroTelefone(ddd + telefoneCliente))
                                {
                                    if (telefoneCliente.Length == 8)
                                        telefoneCliente = "9" + telefoneCliente;
                                    string numeroLimpo = RemoverCaracteresEspeciais("55" + ddd + telefoneCliente);
                                    Zapi zapi = new Zapi();

                                    //Nome e primeiro sobrenome
                                    string[] partesNome = nomeCli.Split(' ');
                                    if (partesNome.Length >= 2)
                                        nomeCli = partesNome[0] + " " + partesNome[1];

                                    String mensagemAjustada = mensagemLembrete;
                                    if (mensagemAjustada.Contains("[NomeCliente]"))
                                        mensagemAjustada = mensagemAjustada.Replace("[NomeCliente]", nomeCli);
                                    if (mensagemAjustada.Contains("[DataProximoExame]"))
                                        mensagemAjustada = mensagemAjustada.Replace("[DataProximoExame]", proxVencimentoExame);

                                    var ret = zapi.zapi_EnviarTexto(numeroLimpo, mensagemAjustada, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                    if (ret != null)
                                    {
                                        AtualizarFlagMensagemEnviada(int.Parse(idOrdemServico));

                                        string nomeArquivo = $"Whatsapp_{DateTime.Now:yyyyMMdd_HHmm}.txt";
                                        if (!Directory.Exists("Logs"))
                                        {
                                            Directory.CreateDirectory("Logs");
                                        }
                                        string caminhoArquivo = Path.Combine("Logs", nomeArquivo);
                                        string conteudoLog = $"Data e Hora: {DateTime.Now}\nNúmero de Telefone: {telefoneCliente}\nTexto da Mensagem: {mensagemAjustada}\n\n";
                                        // Escrever o conteúdo no arquivo (append para adicionar ao conteúdo existente)
                                        File.AppendAllText(caminhoArquivo, conteudoLog);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Número de telefone inválido");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
        private void AtualizarFlagMensagemEnviada(int ordemServicoId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();

                    // Crie a consulta SQL para o UPDATE
                    string updateQuery = @"
                    UPDATE OrdemServico
                    SET MENSAGEMVENCIMENTOEXAMEOTICA = 1
                    WHERE ID = @OrdemServicoId;
                    ";

                    // Crie o comando e parâmetro
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@OrdemServicoId", ordemServicoId);

                        // Execute o UPDATE
                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao atualizar a flag: {ex.Message}");
                }
            }
        }
        private void btnWts_Click(object sender, EventArgs e)
        {
            dispararMensagens_passo01();
            consultaMensagens();
        }

        static bool ValidarNumeroTelefone(string numero)
        {
            // Expressão regular para validar DDD + número de telefone celular com pelo menos 10 dígitos
            string padrao = @"^\(?([1-9]{2})\)?[-.\s]?[6-9]\d{3,4}[-.\s]?\d{4}$";

            // Verificar se o número atende ao padrão
            return Regex.IsMatch(numero, padrao);
        }

        static string RemoverCaracteresEspeciais(string input)
        {
            // Remover caracteres especiais usando expressão regular
            return Regex.Replace(input, @"[^\d]", "");
        }


        private void conferirHorarioMensagens()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
            {
                try
                {
                    // Abra a conexão
                    connection.Open();
                    Sessao.parametroSistema = new ParametroSistema();
                    // Realize uma consulta simples (substitua "suaTabela" e "seuCampo" conforme necessário)
                    string query = "SELECT * FROM ParametroSistema";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Faça algo com os resultados
                            while (reader.Read())
                            {
                                ativarMensagemLembreteExame = $"{reader["ATIVARMENSAGEMVENCIMENTOEXAME"]}";
                                ativarMensagemPosVendas = $"{reader["ATIVARMENSAGEMPOSVENDAS"]}";
                                horarioLembreteExame = $"{reader["MENSAGEMLEMBREEXAMEHORARIO"]}";
                                mensagemPosVenda = $"{reader["MENSAGEMPOSVENDAS"]}";
                                nomeServidorConfigurado = $"{reader["nomeServidor"]}";
                                Sessao.parametroSistema.IdInstanciaWhats = reader["IdInstanciaWhats"].ToString();
                                Sessao.parametroSistema.TokenWhats = reader["TokenWhats"].ToString();
                                idWhats = Sessao.parametroSistema.IdInstanciaWhats;
                                tokenWhats = Sessao.parametroSistema.TokenWhats;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void dispararMensagemPosVenda(string nomeCliente, string mensagem, Pessoa pessoa)
        {
            Zapi zapi = new Zapi();
            if (pessoa.PessoaTelefone != null)
            {
                if (ValidarNumeroTelefone(pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone))
                {
                    if (pessoa.PessoaTelefone.Telefone.Length == 8)
                        pessoa.PessoaTelefone.Telefone = "9" + pessoa.PessoaTelefone.Telefone;
                    string numeroLimpo = RemoverCaracteresEspeciais("55" + pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone);
                    //Nome e primeiro sobrenome
                    string[] partesNome = nomeCliente.Split(' ');
                    if (partesNome.Length >= 2)
                        nomeCliente = partesNome[0] + " " + partesNome[1];

                    String mensagemAjustada = mensagem;
                    if (mensagemAjustada.Contains("[NomeCliente]"))
                        mensagemAjustada = mensagemAjustada.Replace("[NomeCliente]", nomeCliente);


                    var ret = zapi.zapi_EnviarTexto(numeroLimpo, mensagemAjustada, idWhats, tokenWhats);
                    if (ret != null)
                    {
                        //AtualizarFlagMensagemEnviada(int.Parse(idOrdemServico));
                        string nomeArquivo = $"Whatsapp_PosVenda_{DateTime.Now:yyyyMMdd_HHmm}.txt";
                        if (!Directory.Exists("Logs"))
                        {
                            Directory.CreateDirectory("Logs");
                        }
                        string caminhoArquivo = Path.Combine("Logs", nomeArquivo);
                        string conteudoLog = $"Data e Hora: {DateTime.Now}\nNúmero de Telefone: {numeroLimpo}\nTexto da Mensagem: {mensagemAjustada}\n\n";
                        // Escrever o conteúdo no arquivo (append para adicionar ao conteúdo existente)
                        File.AppendAllText(caminhoArquivo, conteudoLog);
                    }
                }
            }
        }

        private void timerExportImport_Tick(object sender, EventArgs e)
        {
            try
            {
                // Obter o caminho do arquivo de exportação
                string caminhoPastaExportacao = "exportBD";
                string caminhoArquivo = Path.Combine(caminhoPastaExportacao, $"{"lunar"}_backup.sql");

                // Verificar se a pasta de exportação existe e criá-la se não existir
                if (!Directory.Exists(caminhoPastaExportacao))
                {
                    Directory.CreateDirectory(caminhoPastaExportacao);
                }

                if (nomeDoComputador.Equals(nomeServidorConfigurado, StringComparison.OrdinalIgnoreCase))
                {
                    // Exportar o banco de dados local para um arquivo
                    BancoDadosUtils.ExportDatabase("marcelo", "mx123", "lunar", caminhoArquivo);

                    // Importar o banco de dados do arquivo para o servidor remoto
                    if (!String.IsNullOrEmpty(bancoNuvem) && !String.IsNullOrEmpty(servidorNuvem) && !String.IsNullOrEmpty(usuarioNuvem))
                        BancoDadosUtils.ImportarBancoDados(servidorNuvem, usuarioNuvem, senhaNuvem, bancoNuvem, caminhoArquivo);
                }
            }
            catch (Exception erro)
            {
                logger.WriteLog("ExportNuvem com erro: " + erro.Message, "LogMensagem");
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {// Obter o caminho do arquivo de exportação
            string caminhoPastaExportacao = "exportBD";
            string caminhoArquivo = Path.Combine(caminhoPastaExportacao, $"{"lunar"}_backup.sql");

            // Verificar se a pasta de exportação existe e criá-la se não existir
            if (!Directory.Exists(caminhoPastaExportacao))
            {
                Directory.CreateDirectory(caminhoPastaExportacao);
            }
            if (nomeDoComputador.Equals(nomeServidorConfigurado, StringComparison.OrdinalIgnoreCase))
            {
                // Exportar o banco de dados local para um arquivo
                BancoDadosUtils.ExportDatabase("marcelo", "mx123", "lunar", caminhoArquivo);

                // Importar o banco de dados do arquivo para o servidor remoto
                if (!String.IsNullOrEmpty(bancoNuvem) && !String.IsNullOrEmpty(servidorNuvem) && !String.IsNullOrEmpty(usuarioNuvem))
                    BancoDadosUtils.ImportarBancoDados(servidorNuvem, usuarioNuvem, senhaNuvem, bancoNuvem, caminhoArquivo);
            }
        }
    }
}
