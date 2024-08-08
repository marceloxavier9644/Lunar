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

namespace Lunar.Telas.ArquivosContabilidade
{
    public partial class FrmEnviarArquivosContabilidade : Form
    {
        DateTime data;
        GenericaDesktop genericaDesktop = new GenericaDesktop();
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

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            envioArquivos();
        }

        private async void envioArquivos()
        {
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
                        GenericaDesktop genericaDesktop = new GenericaDesktop();
                        if (!String.IsNullOrEmpty(txtEmail.Text.Trim()))
                        {
                            string diretorioOriginal = Path.GetDirectoryName(retor);
                            string caminhoNovoArquivo = Path.Combine(diretorioOriginal, "Arquivos.zip");
                            await ReorganizarPastasAsync(retor, caminhoNovoArquivo);

                            List<string> listaAnexo = new List<string>();
                            listaAnexo.Add(caminhoNovoArquivo);
                            if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.NomeRemetenteEmail))
                            {
                                bool ret = genericaDesktop.enviarEmail(txtEmail.Text.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
                                if (ret == true)
                                    GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
                                else
                                    GenericaDesktop.ShowAlerta("Falha ao enviar e-mail, verifique a configuração do seu e-mail de disparo em parâmetros do sistema!");
                            }
                            else
                            {
                                bool retorno = genericaDesktop.enviarEmailPeloLunar(txtEmail.Text.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Text + "/" + txtAno.Text + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
                                if (retorno == true)
                                    GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
                                else
                                    GenericaDesktop.ShowAlerta("Falha ao enviar e-mail, tente novamente!");
                            }

                        }
                        GenericaDesktop.ShowInfo("Arquivo salvo com sucesso!");
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Falha ao salvar/enviar arquivos? Informe o suporte.");
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione uma pasta para salvar os arquivos");
        }

        public async Task ReorganizarPastasAsync(string caminhoArquivoOriginal, string caminhoNovoArquivo)
        {
            string reorganizedDir = "";
            // Diretório temporário para extrair o conteúdo do zip original
            string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDir);

            try
            {
                // Extrai o conteúdo do arquivo .zip para a pasta temporária
                ZipFile.ExtractToDirectory(caminhoArquivoOriginal, tempDir);

                // Caminho do diretório reorganizado
                reorganizedDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(reorganizedDir);

                // Criar subpastas "NFCE" e "NFE" no diretório reorganizado
                string nfceDir = Path.Combine(reorganizedDir, "NFCE");
                string nfeDir = Path.Combine(reorganizedDir, "NFE");
                Directory.CreateDirectory(nfceDir);
                Directory.CreateDirectory(nfeDir);

                // Obtém todos os arquivos .xml em todos os subdiretórios
                string[] xmlFiles = Directory.GetFiles(tempDir, "*.xml", SearchOption.AllDirectories);

                // Move todos os arquivos .xml para as subpastas "NFCE" e "NFE"
                // Move todos os arquivos .xml para as subpastas "NFCE" e "NFE"
                foreach (string file in xmlFiles)
                {
                    string relativePath = file.Substring(tempDir.Length + 1); // Obtém o caminho relativo

                    if (relativePath.Contains("NFCE"))
                    {
                        string destFile = Path.Combine(nfceDir, Path.GetFileName(file));
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile); // Se o arquivo existir, exclui o arquivo de destino
                        }
                        File.Move(file, destFile); // Move o arquivo para o destino
                    }
                    else if (relativePath.Contains("NFE"))
                    {
                        string destFile = Path.Combine(nfeDir, Path.GetFileName(file));
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile); // Se o arquivo existir, exclui o arquivo de destino
                        }
                        File.Move(file, destFile); // Move o arquivo para o destino
                    }
                }

                // Cria um novo arquivo .zip com a estrutura reorganizada
                if (File.Exists(caminhoNovoArquivo))
                {
                    File.Delete(caminhoNovoArquivo);
                }

                ZipFile.CreateFromDirectory(reorganizedDir, caminhoNovoArquivo);
                //Soma NFCe no temp antes de deletar os arquivos q estao extraidos
                somarNFCeXml(reorganizedDir);
                somarNFeXml(reorganizedDir);
            }
            finally
            {
                // Limpa os diretórios temporários
                Directory.Delete(tempDir, true);
                Directory.Delete(reorganizedDir, true);
                
            }
        }
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

                // Preencha o DataTable com dados
                foreach (Nfe nota in notasDoModelo)
                {
                    // Ajuste o status conforme as regras definidas
                    string statusFormatado;
                    if (nota.Status == "Autorizado o uso da NF-e")
                    {
                        statusFormatado = "Autorizado o Uso";
                    }
                    else if (nota.Status == "NF-e cancelada com sucesso" || nota.Status == "NFC-e cancelada com sucesso")
                    {
                        statusFormatado = "Cancelada";
                    }
                    else if (nota.Status == "Inutilizacao de Numero homologado")
                    {
                        statusFormatado = "Inutilizada";
                    }
                    else
                    {
                        statusFormatado = nota.Status;
                    }

                    // Defina o valor condicionalmente
                    decimal valorAutorizado = statusFormatado == "Autorizado o Uso" ? nota.VNf : 0;

                    dtNotas.Rows.Add(
                        nota.NNf,
                        nota.Serie,
                        DateTime.Parse(nota.DataEmissao.ToString()),
                        nota.Chave,
                        statusFormatado,
                        nota.VNf,
                        valorAutorizado 
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


        private decimal somarNFCeXml(string diretorioXml)
        {
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
            //MessageBox.Show($"Soma dos valores de NFCe nos arquivos autorizados: {somaVNF.ToString("C2")}");
            return somaVNF;
        }

        private decimal somarNFeXml(string diretorioXml)
        {
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
            return somaVNF;
        }

        private void btnPesquisaPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtPasta.Texts = folder.SelectedPath;
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
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
                { "Periodo", periodo + "\nSAÍDA NFC-e MOD. 65"  }
            };
            GerarRelatorioPDF(txtPasta.Texts + @"\Relatorio NFC-e 65.pdf", parametros, "65");

            //GERAR RELATORIO 55
            parametros = new Dictionary<string, string>
            {
                { "Empresa", Sessao.empresaFilialLogada.RazaoSocial },
                { "Cnpj", genericaDesktop.FormatarCNPJ(Sessao.empresaFilialLogada.Cnpj) },
                { "Periodo", periodo + "\nEMISSÃO NF-e MOD. 55"  }
            };
            GerarRelatorioPDF(txtPasta.Texts + @"\Relatorio NF-e 55.pdf", parametros, "55");
        }
    }
}
