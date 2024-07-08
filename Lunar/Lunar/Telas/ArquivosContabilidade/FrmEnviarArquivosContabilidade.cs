using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lunar.Telas.ArquivosContabilidade
{
    public partial class FrmEnviarArquivosContabilidade : Form
    {
        DateTime data;
        public FrmEnviarArquivosContabilidade()
        {
            InitializeComponent();
            data = DateTime.Today;
            data = data.AddMonths(-1);
            txtMes.Texts = data.Month.ToString().PadLeft(2, '0');
            if (data.Month.Equals("12"))
                data = data.AddYears(-1);
            txtAno.Texts = data.Year.ToString();
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
            string fileName = txtMes.Texts + txtAno.Texts + ".zip";
            if (!String.IsNullOrEmpty(txtPasta.Texts))
            {
                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                var retor = await lunarApiNotas.coletarArquivosContabeisEConferir(Sessao.empresaFilialLogada.Cnpj, txtMes.Texts, txtAno.Texts, localFilePath);
                if (retor != null)
                {
                    if (File.Exists(retor.ToString()))
                    {
                        GenericaDesktop genericaDesktop = new GenericaDesktop();
                        if (!String.IsNullOrEmpty(txtEmail.Texts.Trim()))
                        {
                            string diretorioOriginal = Path.GetDirectoryName(retor);
                            string caminhoNovoArquivo = Path.Combine(diretorioOriginal, "Arquivos.zip");
                            await ReorganizarPastasAsync(retor, caminhoNovoArquivo);

                            List<string> listaAnexo = new List<string>();
                            listaAnexo.Add(caminhoNovoArquivo);
                            if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.NomeRemetenteEmail))
                            {
                                bool ret = genericaDesktop.enviarEmail(txtEmail.Texts.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Texts + "/" + txtAno.Texts + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
                                if (ret == true)
                                    GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
                                else
                                    GenericaDesktop.ShowAlerta("Falha ao enviar e-mail, verifique a configuração do seu e-mail de disparo em parâmetros do sistema!");
                            }
                            else
                            {
                                bool retorno = genericaDesktop.enviarEmailPeloLunar(txtEmail.Texts.Trim(), "Arquivos Fiscais " + Sessao.empresaFilialLogada.NomeFantasia, txtMes.Texts + "/" + txtAno.Texts + "    " + Sessao.empresaFilialLogada.NomeFantasia + " CNPJ: " + Sessao.empresaFilialLogada.Cnpj, "Olá, segue arquivos em anexo. Este e-mail foi disparado pelo sistema Lunar Software, qualquer dúvida entre em contato com o responsável da empresa.", listaAnexo);
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
                    GenericaDesktop.ShowAlerta("Arquivo vazio! Você emitiu notas no mês informado? Informe o suporte.");
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
            MessageBox.Show($"Soma dos valores de NFCe nos arquivos autorizados: {somaVNF.ToString("C2")}");
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
            MessageBox.Show($"Soma dos valores de NFe nos arquivos autorizados: {somaVNF.ToString("C2")}");
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
    }
}
