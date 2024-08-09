using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System.Linq;
using Lunar.Utils.Sintegra;
using System.Threading;
using Syncfusion.Windows.Forms.Tools;

namespace Lunar.Telas.ArquivosContabilidade
{
    public partial class FrmEnviarArquivosContabilidade : Form
    {
        DateTime data;
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        decimal SomaNfceXml = 0;
        decimal SomaNfeXml = 0;
        string arquivoSintegraCaminho = "";
        public FrmEnviarArquivosContabilidade()
        {
            InitializeComponent();
            data = DateTime.Today;
            data = data.AddMonths(-1);
            txtMes.Text = data.Month.ToString().PadLeft(2, '0');
            if (data.Month.Equals("12"))
                data = data.AddYears(-1);
            txtAno.Text = data.Year.ToString();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            progressBarAdv1.Visible = true;
            SetupWaitingGradientProgressBar();
            await ProcessarNotas();
            await envioArquivos();
            progressBarAdv1.Visible = false;
        }

        private async Task ProcessarNotas() // Ajustado para ser assíncrono e retornar Task
        {
            try
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Conferindo Notas Fiscais...";

                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);
                DateTime primeiroDia = new DateTime(ano, mes, 1);
                DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
                string dataInicial = $"{primeiroDia:dd/MM/yyyy}";
                string dataFinal = $"{ultimoDia:dd/MM/yyyy}";

                GenericaDesktop genericaDesktop = new GenericaDesktop();
                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                NfeController nfeController = new NfeController(); // Certifique-se de que você tem uma instância válida
                NotaService notaService = new NotaService(genericaDesktop, lunarApiNotas, nfeController);

                DateTime dataInicial1 = DateTime.Parse(dataInicial);
                DateTime dataFinal1 = DateTime.Parse(dataFinal);

                // Processa as notas de forma assíncrona
                await notaService.ProcessarNotasAsync(dataInicial1, dataFinal1);
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta("Erro: " + ex.Message);
            }
        }
        //private async void envioArquivos()
        //{
        //    gerarRelatorioNfeENfce();
        //    gerarSintegra();
        //    string localFilePath = txtPasta.Texts + @"\";
        //    string fileName = txtMes.Text + txtAno.Text + ".zip";
        //    if (!String.IsNullOrEmpty(txtPasta.Texts))
        //    {
        //        LunarApiNotas lunarApiNotas = new LunarApiNotas();
        //        var retor = await lunarApiNotas.coletarArquivosContabeisEConferir(Sessao.empresaFilialLogada.Cnpj, txtMes.Text, txtAno.Text, localFilePath);
        //        if (retor != null)
        //        {
        //            if (File.Exists(retor.ToString()))
        //            {
        //                string diretorioOriginal = Path.GetDirectoryName(retor);
        //                string caminhoNovoArquivo = Path.Combine(diretorioOriginal, "Arquivos.zip");
        //                await ReorganizarPastasAsync(retor, caminhoNovoArquivo);

        //                GenericaDesktop genericaDesktop = new GenericaDesktop();
        //                if (!String.IsNullOrEmpty(txtEmail.Text.Trim()))
        //                {
        //                    List<string> listaAnexo = new List<string>();
        //                    listaAnexo.Add(caminhoNovoArquivo);
        //                    if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.NomeRemetenteEmail))
        //                    {
        //                        bool ret = genericaDesktop.enviarEmail(txtEmail.Text.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
        //                        if (ret == true)
        //                            GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
        //                        else
        //                            GenericaDesktop.ShowAlerta("Falha ao enviar e-mail, verifique a configuração do seu e-mail de disparo em parâmetros do sistema!");
        //                    }
        //                    else
        //                    {
        //                        bool retorno = genericaDesktop.enviarEmailPeloLunar(txtEmail.Text.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
        //                        if (retorno == true)
        //                            GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
        //                        else
        //                            GenericaDesktop.ShowAlerta("Falha ao enviar e-mail, tente novamente!");
        //                    }

        //                }
        //                GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
        //            }
        //        }
        //        else
        //        {
        //            GenericaDesktop.ShowAlerta("Falha ao salvar/enviar arquivos? Informe o suporte.");
        //        }
        //    }
        //    else
        //        GenericaDesktop.ShowAlerta("Selecione uma pasta para salvar os arquivos");
        //}

        private async Task envioArquivos()
        {
            // Mostrar o ProgressBar e configurar o estilo de espera
            progressBarAdv1.Visible = true;

            try
            {
                // Simula uma operação demorada
                await Task.Delay(5000); // Remova ou substitua por suas operações reais
                lblInfo.Text = "Gerando Arquivos...";
                // Seu código para gerar e enviar arquivos
                await Task.Run(() => gerarRelatorioNfeENfce()); // Garante que a operação é executada em um thread separado
                await Task.Run(() => gerarSintegra()); // Garante que a operação é executada em um thread separado

                string localFilePath = txtPasta.Texts + @"\";
                string fileName = txtMes.Text + txtAno.Text + ".zip";

                if (!String.IsNullOrEmpty(txtPasta.Texts))
                {
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    var retor = await lunarApiNotas.coletarArquivosContabeisEConferir(Sessao.empresaFilialLogada.Cnpj, txtMes.Text, txtAno.Text, localFilePath);

                    if (retor != null)
                    {
                        if (File.Exists(retor.ToString()))
                        {
                            string diretorioOriginal = Path.GetDirectoryName(retor.ToString());
                            string caminhoNovoArquivo = Path.Combine(diretorioOriginal, "Arquivos.zip");
                            await ReorganizarPastasAsync(retor.ToString(), caminhoNovoArquivo, arquivoSintegraCaminho);

                            //MessageBox.Show("NFC-e: "+ SomaNfceXml.ToString("C2"));
                            //MessageBox.Show("NF-e: " + SomaNfeXml.ToString("C2"));

                            GenericaDesktop genericaDesktop = new GenericaDesktop();
                            if (!String.IsNullOrEmpty(txtEmail.Text.Trim()))
                            {
                                List<string> listaAnexo = new List<string> { caminhoNovoArquivo };

                                bool emailEnviado;
                                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.NomeRemetenteEmail))
                                {
                                    emailEnviado = genericaDesktop.enviarEmail(
                                        txtEmail.Text.Trim(),
                                        "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia,
                                        txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj,
                                        "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.",
                                        listaAnexo
                                    );
                                }
                                else
                                {
                                    emailEnviado = genericaDesktop.enviarEmailPeloLunar(
                                        txtEmail.Text.Trim(),
                                        "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia,
                                        txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj,
                                        "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.",
                                        listaAnexo
                                    );
                                }

                                GenericaDesktop.ShowInfo(emailEnviado ? "E-mail enviado com sucesso!" : "Falha ao enviar e-mail, verifique a configuração do seu e-mail de disparo em parâmetros do sistema!");
                            }
                            else
                            {
                                GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
                                GenericaDesktop generica = new GenericaDesktop();
                                generica.AbrirPastaExplorer(txtPasta.Texts);
                            }
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Falha ao salvar/enviar arquivos? Informe o suporte.");
                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Falha ao salvar/enviar arquivos? Informe o suporte.");
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Selecione uma pasta para salvar os arquivos");
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta("Erro: " + ex.Message);
            }
            finally
            {
                // Ocultar o ProgressBar ao final da operação
                progressBarAdv1.Visible = false;
                lblInfo.Visible = false;
            }
        }


        public async Task ReorganizarPastasAsync(string caminhoArquivoOriginal, string caminhoNovoArquivo, string caminhoSintegra)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string reorganizedDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            try
            {
                // Cria diretórios temporários
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(reorganizedDir);

                // Extrai o arquivo .zip para a pasta temporária
                ZipFile.ExtractToDirectory(caminhoArquivoOriginal, tempDir);

                // Cria subpastas "NFCE" e "NFE" no diretório reorganizado
                string nfceDir = Path.Combine(reorganizedDir, "NFCE");
                string nfeDir = Path.Combine(reorganizedDir, "NFE");
                Directory.CreateDirectory(nfceDir);
                Directory.CreateDirectory(nfeDir);

                // Obtém todos os arquivos .xml em todos os subdiretórios
                string[] xmlFiles = Directory.GetFiles(tempDir, "*.xml", SearchOption.AllDirectories);

                // Move todos os arquivos .xml para as subpastas "NFCE" e "NFE"
                foreach (string file in xmlFiles)
                {
                    string relativePath = file.Substring(tempDir.Length + 1); // Obtém o caminho relativo

                    string destDir = relativePath.Contains("NFCE") ? nfceDir :
                                     (relativePath.Contains("NFE") ? nfeDir : null);

                    if (destDir != null)
                    {
                        string destFile = Path.Combine(destDir, Path.GetFileName(file));
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile); // Se o arquivo existir, exclui o arquivo de destino
                        }
                        File.Move(file, destFile); // Move o arquivo para o destino
                    }
                }

                // Copia os arquivos adicionais para o diretório reorganizado
                string relatorioNfcePdf = Path.Combine(Path.GetDirectoryName(caminhoArquivoOriginal), "Relatorio NFC-e 65.pdf");
                string relatorioNfePdf = Path.Combine(Path.GetDirectoryName(caminhoArquivoOriginal), "Relatorio NF-e 55.pdf");
                string relatorioTxt = arquivoSintegraCaminho.Replace("/","");

                if (File.Exists(relatorioNfcePdf))
                {
                    File.Copy(relatorioNfcePdf, Path.Combine(reorganizedDir, Path.GetFileName(relatorioNfcePdf)), true);
                }

                if (File.Exists(relatorioNfePdf))
                {
                    File.Copy(relatorioNfePdf, Path.Combine(reorganizedDir, Path.GetFileName(relatorioNfePdf)), true);
                }

                if (!string.IsNullOrEmpty(relatorioTxt) && File.Exists(relatorioTxt))
                {
                    File.Copy(relatorioTxt, Path.Combine(reorganizedDir, Path.GetFileName(relatorioTxt)), true);
                }

                // Cria um novo arquivo .zip com a estrutura reorganizada e arquivos adicionais
                if (File.Exists(caminhoNovoArquivo))
                {
                    File.Delete(caminhoNovoArquivo);
                }

                ZipFile.CreateFromDirectory(reorganizedDir, caminhoNovoArquivo);

                // Soma NFCe no temp antes de deletar os arquivos que estão extraídos
                await somarNFCeXml(reorganizedDir);
                await somarNFeXml(reorganizedDir);
            }
            finally
            {
                // Garante que os diretórios temporários sejam excluídos
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
                if (Directory.Exists(reorganizedDir))
                {
                    Directory.Delete(reorganizedDir, true);
                }
                if (File.Exists(caminhoArquivoOriginal))
                {
                    File.Delete(caminhoArquivoOriginal);
                }
            }
        }

        //public async Task ReorganizarPastasAsync(string caminhoArquivoOriginal, string caminhoNovoArquivo)
        //{
        //    string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        //    string reorganizedDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        //    try
        //    {
        //        // Cria diretórios temporários
        //        Directory.CreateDirectory(tempDir);
        //        Directory.CreateDirectory(reorganizedDir);

        //        // Extrai o arquivo .zip para a pasta temporária
        //        ZipFile.ExtractToDirectory(caminhoArquivoOriginal, tempDir);

        //        // Cria subpastas "NFCE" e "NFE" no diretório reorganizado
        //        string nfceDir = Path.Combine(reorganizedDir, "NFCE");
        //        string nfeDir = Path.Combine(reorganizedDir, "NFE");
        //        Directory.CreateDirectory(nfceDir);
        //        Directory.CreateDirectory(nfeDir);

        //        // Obtém todos os arquivos .xml em todos os subdiretórios
        //        string[] xmlFiles = Directory.GetFiles(tempDir, "*.xml", SearchOption.AllDirectories);

        //        // Move todos os arquivos .xml para as subpastas "NFCE" e "NFE"
        //        foreach (string file in xmlFiles)
        //        {
        //            string relativePath = file.Substring(tempDir.Length + 1); // Obtém o caminho relativo

        //            string destDir = relativePath.Contains("NFCE") ? nfceDir :
        //                             (relativePath.Contains("NFE") ? nfeDir : null);

        //            if (destDir != null)
        //            {
        //                string destFile = Path.Combine(destDir, Path.GetFileName(file));
        //                if (File.Exists(destFile))
        //                {
        //                    File.Delete(destFile); // Se o arquivo existir, exclui o arquivo de destino
        //                }
        //                File.Move(file, destFile); // Move o arquivo para o destino
        //            }
        //        }

        //        // Cria um novo arquivo .zip com a estrutura reorganizada
        //        if (File.Exists(caminhoNovoArquivo))
        //        {
        //            File.Delete(caminhoNovoArquivo);
        //        }

        //        ZipFile.CreateFromDirectory(reorganizedDir, caminhoNovoArquivo);

        //        // Soma NFCe no temp antes de deletar os arquivos que estão extraídos
        //        await somarNFCeXml(reorganizedDir);
        //        await somarNFeXml(reorganizedDir);
        //    }
        //    finally
        //    {
        //        // Garante que os diretórios temporários sejam excluídos
        //        if (Directory.Exists(tempDir))
        //        {
        //            Directory.Delete(tempDir, true);
        //        }
        //        if (Directory.Exists(reorganizedDir))
        //        {
        //            Directory.Delete(reorganizedDir, true);
        //        }
        //        if (File.Exists(caminhoArquivoOriginal))
        //        {
        //            File.Delete(caminhoArquivoOriginal);
        //        }
        //    }
        //}

        public void GerarRelatorioPDF(string caminhoPDF, Dictionary<string, string> parametros, string modelo)
        {
            try
            {
                // Crie uma instância do LocalReport
                LocalReport relatorio = new LocalReport();

                // Carregue o relatório do recurso embutido
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lunar.Telas.ArquivosContabilidade.RelatorioFiscal.rdlc"))
                {
                    if (stream != null)
                    {
                        relatorio.LoadReportDefinition(stream);
                    }
                    else
                    {
                        throw new FileNotFoundException("Não foi possível encontrar o arquivo de relatório embutido.");
                    }
                }

                // Obtenha os dados
                NfeController nfeController = new NfeController();
                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);
                DateTime primeiroDia = new DateTime(ano, mes, 1);
                DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
                string dataInicial = $"{primeiroDia:yyyy/MM/dd}";
                string dataFinal = $"{ultimoDia:yyyy/MM/dd}";

                IList<Nfe> listaNotas = nfeController.selecionarNotasEmitidasPorPeriodo(dataInicial, dataFinal);

                // Filtrar notas pelo modelo
                IList<Nfe> notasDoModelo = listaNotas.Where(n => n.Modelo == modelo).ToList();

                // Verifique se há notas para o modelo
                if (notasDoModelo.Count == 0)
                {
                    MessageBox.Show($"Nenhuma nota encontrada para o modelo {modelo}.");
                    return;
                }

                // Crie e preencha o DataTable com dados
                DataTable dtNotas = new DataTable("dsFiscalXml");

                // Defina as colunas do DataTable
                dtNotas.Columns.Add("Numero", typeof(int));
                dtNotas.Columns.Add("Serie", typeof(string));
                dtNotas.Columns.Add("DataEmissao", typeof(DateTime));
                dtNotas.Columns.Add("Chave", typeof(string));
                dtNotas.Columns.Add("Status", typeof(string));
                dtNotas.Columns.Add("Valor", typeof(decimal));
                dtNotas.Columns.Add("ValorAutorizado", typeof(decimal)); // Adicione esta coluna
                dtNotas.Columns.Add("EntradaSaida", typeof(string));
                // Preencha o DataTable com dados
                foreach (Nfe nota in notasDoModelo)
                {
                    // Ajuste o status conforme as regras definidas
                    string statusFormatado;
                    if (nota.Status == "Autorizado o uso da NF-e")
                    {
                        statusFormatado = "AUTORIZADA";
                    }
                    else if (nota.Status == "NF-e cancelada com sucesso" || nota.Status == "NFC-e cancelada com sucesso")
                    {
                        statusFormatado = "CANCELADA";
                    }
                    else if (nota.Status == "Inutilizacao de Numero homologado")
                    {
                        statusFormatado = "INUTILIZADA";
                    }
                    else if (nota.Status.Contains("Preparando") || String.IsNullOrEmpty(nota.Status) || nota.Status.Length > 40)
                    {
                        statusFormatado = "REJEIÇÃO";
                    }
                    else
                    {
                        statusFormatado = nota.Status;
                    }

                    // Defina o valor condicionalmente
                    decimal valorAutorizado = statusFormatado == "AUTORIZADA" ? nota.VNf : 0;

                    dtNotas.Rows.Add(
                        nota.NNf,
                        nota.Serie,
                        DateTime.Parse(nota.DataEmissao.ToString()),
                        nota.Chave,
                        statusFormatado,
                        nota.VNf,
                        valorAutorizado,
                        nota.TipoOperacao
                    );
                }

                // Adicione o DataTable ao relatório
                relatorio.DataSources.Clear();
                relatorio.DataSources.Add(new ReportDataSource("dsFiscalXml", dtNotas));

                // Adicione os parâmetros, se houver
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        relatorio.SetParameters(new ReportParameter(parametro.Key, parametro.Value));
                    }
                }

                // Renderize o relatório para o formato PDF
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                byte[] bytes = relatorio.Render(
                    "PDF", null, out mimeType, out encoding,
                    out fileNameExtension, out streams, out warnings);

                // Salve o PDF no disco
                using (FileStream fs = new FileStream(caminhoPDF, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                // Log ou exiba a exceção para depuração
                MessageBox.Show("Erro ao gerar o relatório: " + ex.Message);
            }
        }
        //public void GerarRelatorioPDF(string caminhoPDF, Dictionary<string, string> parametros)
        //{
        //    try
        //    {
        //        // Crie uma instância do LocalReport
        //        LocalReport relatorio = new LocalReport();

        //        // Carregue o relatório do recurso embutido
        //        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lunar.Telas.ArquivosContabilidade.RelatorioFiscal.rdlc"))
        //        {
        //            if (stream != null)
        //            {
        //                relatorio.LoadReportDefinition(stream);
        //            }
        //            else
        //            {
        //                throw new FileNotFoundException("Não foi possível encontrar o arquivo de relatório embutido.");
        //            }
        //        }

        //        // Obtenha os dados
        //        NfeController nfeController = new NfeController();
        //        int mes = int.Parse(txtMes.Text);
        //        int ano = int.Parse(txtAno.Text);
        //        DateTime primeiroDia = new DateTime(ano, mes, 1);
        //        DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
        //        string dataInicial = $"{primeiroDia:yyyy/MM/dd}";
        //        string dataFinal = $"{ultimoDia:yyyy/MM/dd}";


        //        IList<Nfe> listaNotas = nfeController.selecionarNotasEmitidasPorPeriodo(dataInicial, dataFinal);

        //        // Crie e preencha o DataTable com dados
        //        DataTable dtNotas = new DataTable("dsFiscalXml");

        //        // Defina as colunas do DataTable
        //        dtNotas.Columns.Add("Numero", typeof(int));
        //        dtNotas.Columns.Add("Serie", typeof(string));
        //        dtNotas.Columns.Add("DataEmissao", typeof(DateTime)); 
        //        dtNotas.Columns.Add("Chave", typeof(string));
        //        dtNotas.Columns.Add("Status", typeof(string));
        //        dtNotas.Columns.Add("Valor", typeof(decimal));

        //        // Preencha o DataTable com dados
        //        foreach (Nfe nota in listaNotas)
        //        {
        //            // Ajuste o status conforme as regras definidas
        //            string statusFormatado;
        //            if (nota.Status == "Autorizado o uso da NF-e")
        //            {
        //                statusFormatado = "Autorizado o Uso";
        //            }
        //            else if (nota.Status == "NF-e cancelada com sucesso" || nota.Status == "NFC-e cancelada com sucesso")
        //            {
        //                statusFormatado = "Cancelada";
        //            }
        //            else
        //            {
        //                statusFormatado = nota.Status; // Mantém o status original se não se encaixar nas condições acima
        //            }

        //            // Adicione a linha ao DataTable
        //            dtNotas.Rows.Add(
        //                nota.NNf,
        //                nota.Serie,
        //                DateTime.Parse(nota.DataEmissao.ToString()), // Converta para DateTime
        //                nota.Chave,
        //                statusFormatado, // Use o status formatado
        //                nota.VNf
        //            );
        //        }

        //        // Adicione o DataTable ao relatório
        //        relatorio.DataSources.Clear();
        //        relatorio.DataSources.Add(new ReportDataSource("dsFiscalXml", dtNotas));

        //        // Adicione os parâmetros, se houver
        //        if (parametros != null)
        //        {
        //            foreach (var parametro in parametros)
        //            {
        //                relatorio.SetParameters(new ReportParameter(parametro.Key, parametro.Value));
        //            }
        //        }

        //        // Renderize o relatório para o formato PDF
        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;
        //        Warning[] warnings;
        //        string[] streams;

        //        byte[] bytes = relatorio.Render(
        //            "PDF", null, out mimeType, out encoding,
        //            out fileNameExtension, out streams, out warnings);

        //        // Salve o PDF no disco
        //        using (FileStream fs = new FileStream(caminhoPDF, FileMode.Create))
        //        {
        //            fs.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log ou exiba a exceção para depuração
        //        MessageBox.Show("Erro ao gerar o relatório: " + ex.Message);
        //    }
        //}


        private Task somarNFCeXml(string diretorioXml)
        {
            SomaNfceXml = 0;
            decimal somaVNF = 0;

            string[] xmlFiles = Directory.GetFiles(Path.Combine(diretorioXml, "NFCE"), "*.xml");

            foreach (string xmlFile in xmlFiles)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                // Verifica se o XML foi autorizado
                if (xmlDoc.InnerXml.Contains("<xMotivo>Autorizado o uso da NF-e</xMotivo>"))
                {
                    // Encontra o valor de vNF
                    XmlNodeList vnfNodes = xmlDoc.GetElementsByTagName("vNF");
                    if (vnfNodes.Count > 0)
                    {
                        string vNFValue = vnfNodes[0].InnerText;
                        if (decimal.TryParse(vNFValue.Replace(".", ","), out decimal valorNF))
                        {
                            somaVNF += valorNF;
                        }
                        else
                        {
                            Console.WriteLine($"Não foi possível converter o valor de vNF no arquivo: {xmlFile}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O arquivo XML não contém o nó vNF: {xmlFile}");
                    }
                }
            }
            SomaNfceXml = somaVNF;
            return Task.CompletedTask;
        }

        private Task somarNFeXml(string diretorioXml)
        {
            SomaNfeXml = 0;
            decimal somaVNF = 0;

            string[] xmlFiles = Directory.GetFiles(Path.Combine(diretorioXml, "NFE"), "*.xml");

            foreach (string xmlFile in xmlFiles)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                // Verifica se o XML foi autorizado
                if (xmlDoc.InnerXml.Contains("<xMotivo>Autorizado o uso da NF-e</xMotivo>"))
                {
                    // Encontra o valor de vNF
                    XmlNodeList vnfNodes = xmlDoc.GetElementsByTagName("vNF");
                    if (vnfNodes.Count > 0)
                    {
                        string vNFValue = vnfNodes[0].InnerText;
                        if (decimal.TryParse(vNFValue.Replace(".", ","), out decimal valorNF))
                        {
                            somaVNF += valorNF;
                        }
                        else
                        {
                            Console.WriteLine($"Não foi possível converter o valor de vNF no arquivo: {xmlFile}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O arquivo XML não contém o nó vNF: {xmlFile}");
                    }
                }
            }
            //MessageBox.Show($"Soma dos valores de NFe nos arquivos autorizados: {somaVNF.ToString("C2")}");
            SomaNfeXml = somaVNF;
            return Task.CompletedTask;
        }

        private void btnPesquisaPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtPasta.Texts = folder.SelectedPath;
            }
        }

        private void gerarRelatorioNfeENfce()
        {
            int mes = int.Parse(txtMes.Text);
            int ano = int.Parse(txtAno.Text);
            DateTime primeiroDia = new DateTime(ano, mes, 1);
            DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
            string periodo = "Período " + $"{primeiroDia:dd/MM/yyyy} a {ultimoDia:dd/MM/yyyy}";

            var parametros = new Dictionary<string, string>
            {
                { "Empresa", Sessao.empresaFilialLogada.RazaoSocial },
                { "Cnpj", genericaDesktop.FormatarCNPJ(Sessao.empresaFilialLogada.Cnpj) },
                { "Periodo", periodo },
                {"Modelo", "NFC-e MOD. 65" }
            };
            GerarRelatorioPDF(txtPasta.Texts + @"\Relatorio NFC-e 65.pdf", parametros, "65");

            //GERAR RELATORIO 55
            parametros = new Dictionary<string, string>
            {
                { "Empresa", Sessao.empresaFilialLogada.RazaoSocial },
                { "Cnpj", genericaDesktop.FormatarCNPJ(Sessao.empresaFilialLogada.Cnpj) },
                { "Periodo", periodo },
                {"Modelo", "NF-e MOD. 55" }
            };
            GerarRelatorioPDF(txtPasta.Texts + @"\Relatorio NF-e 55.pdf", parametros, "55");
        }
        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            
        }

        private void gerarSintegra()
        {
            bool reg74 = false;
            String dataInventario = "";
            DateTime data = DateTime.Parse(txtDataInventario.Value.ToString());
            dataInventario = data.ToString("yyyy'-'MM'-'dd' '23':'59':'59");
            if (chkRegistro74.Checked == true)
                reg74 = true;

            //data inicial e final
            int mes = int.Parse(txtMes.Text);
            int ano = int.Parse(txtAno.Text);
            DateTime primeiroDia = new DateTime(ano, mes, 1);
            DateTime ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
            string dataInicial = $"{primeiroDia:dd/MM/yyyy}";
            string dataFinal = $"{ultimoDia:dd/MM/yyyy}";
            arquivoSintegraCaminho = txtPasta.Texts + @"\Sintegra" + dataInicial + "_" + dataFinal + ".txt";
            GeradorSintegra geradorSintegra = new GeradorSintegra();
            geradorSintegra.gerarSintegra(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal), Sessao.empresaFilialLogada, txtPasta.Texts, reg74, dataInventario, false);
        }
        private void chkRegistro74_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRegistro74.Checked == true)
                    txtDataInventario.Enabled = true;
                else
                    txtDataInventario.Enabled = false;
            }
            catch
            {

            }
        }

        private void SetupWaitingGradientProgressBar()
        {
            progressBarAdv1.Visible = true;
        }


    }
}
