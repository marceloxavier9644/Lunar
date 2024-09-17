using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils.Balancas
{
    public class BalancaService
    {
        public void GerarArquivoItens(List<ItemBalanca> itens)
        {
            string caminhoAreaDeTrabalho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string caminhoArquivo = Path.Combine(caminhoAreaDeTrabalho, "Itensmgv.txt");

            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                foreach (var item in itens)
                {
                    // Ajustar os campos de acordo com o comprimento especificado e preenchimento correto
                    string linha = item.CodigoDepartamento.PadLeft(2, '0') +  // 2 caracteres
                                   item.TipoProduto.PadLeft(1, '0') +  // 1 caractere
                                   item.CodigoItem.PadLeft(6, '0') +  // 6 caracteres
                                   item.Preco.PadLeft(6, '0') +  // 6 caracteres
                                   item.DiasValidade.PadLeft(3, '0') +  // 3 caracteres
                                   item.Descritivo1.PadRight(25) +  // 25 caracteres
                                   item.Descritivo2.PadRight(25) +  // 25 caracteres
                                   item.CodigoInfoExtra.PadLeft(6, '0') +  // 6 caracteres
                                   item.CodigoImagem.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoInfoNutricional.PadLeft(6, '0') +  // 6 caracteres
                                   item.DataValidade.PadLeft(1, '0') +  // 1 caractere
                                   item.DataEmbalagem.PadLeft(1, '0') +  // 1 caractere
                                   item.CodigoFornecedor.PadLeft(4, '0') +  // 4 caracteres
                                   item.Lote.PadRight(12) +  // 12 caracteres
                                   item.CodigoEANEspecial.PadLeft(11, '0') +  // 11 caracteres
                                   item.VersaoPreco.PadLeft(1, '0') +  // 1 caractere
                                   item.CodigoSom.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoTara.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoFracionador.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoCampoExtra1.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoCampoExtra2.PadLeft(4, '0') +  // 4 caracteres
                                   item.CodigoConservacao.PadLeft(4, '0') +  // 4 caracteres
                                   item.EANFornecedor.PadLeft(12, '0') +  // 12 caracteres
                                   item.PercentualGlaciamento.PadLeft(6, '0') +  // 6 caracteres
                                   "|DA|" +  // Marcação de início dos departamentos associados
                                   item.Descritivo3.PadRight(35) +  // 35 caracteres
                                   item.Descritivo4.PadRight(35) +  // 35 caracteres
                                   item.CodigoCampoExtra3.PadLeft(6, '0') +  // 6 caracteres
                                   item.CodigoCampoExtra4.PadLeft(6, '0') +  // 6 caracteres
                                   item.CodigoMidia.PadLeft(6, '0') +  // 6 caracteres
                                   item.PrecoPromocional.PadLeft(6, '0') +  // 6 caracteres
                                   item.SolicitacaoFornecedor.PadLeft(1, '0') +  // 1 caractere
                                   "|" + item.CodigoFornecedorAssociado.PadLeft(4, '0') + "|" +  // 4 caracteres para o código do fornecedor associado
                                   item.SolicitacaoTara.PadLeft(1, '0') +  // 1 caractere
                                   "|" + item.BalancasInativas + "|" +  // Sequência de balanças inativas
                                   item.CodigoEANEspecialG1.PadLeft(12, '0') +  // 12 caracteres
                                   item.PercentualGlaciamentoPG.PadLeft(4, '0');  // 4 caracteres

                    sw.WriteLine(linha);
                }
            }
        }


        // Método para simular a obtenção dos itens da base de dados
        public List<ItemBalanca> ObterItensDaBaseDeDados()
        {
            List<ItemBalanca> itens = new List<ItemBalanca>
        {
            new ItemBalanca
            {
                CodigoDepartamento = "01",
                TipoProduto = "0",
                CodigoItem = "123456",
                Preco = "010000", // Exemplo de preço (10,00)
                DiasValidade = "030",
                Descritivo1 = "Produto A",
                Descritivo2 = "Marca A",
                CodigoInfoExtra = "000001",
                CodigoImagem = "0010",
                CodigoInfoNutricional = "000005",
                DataValidade = "0",
                DataEmbalagem = "0",
                CodigoFornecedor = "1000",
                Lote = "000000000001",
                CodigoEANEspecial = "789123456789",
                VersaoPreco = "1",
                CodigoSom = "0010",
                CodigoTara = "0050",
                CodigoFracionador = "0010",
                CodigoCampoExtra1 = "0000",
                CodigoCampoExtra2 = "0000",
                CodigoConservacao = "0001",
                EANFornecedor = "789654321000",
                PercentualGlaciamento = "000500",
                Descritivo3 = "Extra Info 1",
                Descritivo4 = "Extra Info 2",
                CodigoCampoExtra3 = "000010",
                CodigoCampoExtra4 = "000020",
                CodigoMidia = "000000",
                PrecoPromocional = "009500",
                SolicitacaoFornecedor = "0",
                CodigoFornecedorAssociado = "1001",
                SolicitacaoTara = "0",
                BalancasInativas = "0",
                CodigoEANEspecialG1 = "789654321001",
                PercentualGlaciamentoPG = "0100"
            },
            // Adicione mais itens conforme necessário
        };

            return itens;
        }

        // Método para enviar o arquivo gerado para a balança via TCP/IP
        public void EnviarArquivoParaBalanca(string caminhoArquivo, string ipAddress, int port)
        {
            try
            {
                byte[] arquivoBytes = File.ReadAllBytes(caminhoArquivo);

                using (TcpClient client = new TcpClient(ipAddress, port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        stream.Write(arquivoBytes, 0, arquivoBytes.Length);
                        Console.WriteLine("Arquivo enviado com sucesso para a balança.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar arquivo para a balança: {ex.Message}");
            }
        }

        public bool TestarComunicacao(string ip, int porta)
        {
            try
            {
                // Criar um cliente TCP
                using (TcpClient client = new TcpClient())
                {
                    // Conectar ao IP e porta especificados
                    client.Connect(ip, porta);

                    // Verificar se a conexão foi estabelecida
                    if (client.Connected)
                    {
                        // Conexão estabelecida com sucesso
                        GenericaDesktop.ShowInfo("Conexão estabelecida com a balança.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibir erro de conexão
                GenericaDesktop.ShowAlerta($"Erro ao conectar com a balança: {ex.Message}");
            }

            return false;
        }
        public static string ObterIpLocal()
        {
            string ipLocal = null;

            try
            {
                foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) // IPv4
                    {
                        ipLocal = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter o IP local: " + ex.Message);
            }

            return ipLocal;
        }
        public async Task ScanBalancasNaRede(string faixaIp, int porta, int inicioRange, int fimRange, ProgressBar progressBar, CancellationToken cancellationToken)
        {
            int encontrada = 0;
            int totalIps = fimRange - inicioRange + 1;

            // Obtém o IP local do próprio computador
            string ipLocal = ObterIpLocal();

            // Configura o ProgressBar
            progressBar.Invoke((MethodInvoker)(() =>
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = totalIps;
                progressBar.Value = 0;
                progressBar.Visible = true;
            }));

            for (int i = inicioRange; i <= fimRange; i++)
            {
                cancellationToken.ThrowIfCancellationRequested(); // Verifica se o cancelamento foi solicitado

                string ip = faixaIp + i;

                // Verifica se o IP atual é o IP local
                if (ip == ipLocal)
                {
                    continue; // Pula o IP do próprio computador
                }

                // Testa a comunicação para cada IP
                bool balancaEncontrada = await TestarComunicacaoAsync(ip, porta);
                if (balancaEncontrada)
                {
                    encontrada++;
                    GenericaDesktop.ShowInfo($"Balança encontrada no IP: {ip}:{porta}");
                }

                // Atualiza o ProgressBar
                progressBar.Invoke((MethodInvoker)(() =>
                {
                    progressBar.Value = i - inicioRange + 1;
                }));
            }

            if (encontrada == 0)
                GenericaDesktop.ShowAlerta("Nenhuma balança encontrada na rede!");

            // Finaliza o ProgressBar quando o processo termina
            progressBar.Invoke((MethodInvoker)(() =>
            {
                progressBar.Value = totalIps;
                progressBar.Visible = false;
            }));
        }




        private async Task<bool> TestarComunicacaoAsync(string ip, int porta)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    // Tenta conectar com um timeout de 500ms
                    var result = client.BeginConnect(ip, porta, null, null);
                    var sucesso = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(500));

                    if (sucesso && client.Connected)
                    {
                        client.EndConnect(result);
                        return true; // Conexão bem-sucedida
                    }
                }
            }
            catch
            {
                // Ignorar erros de conexão
            }

            return false; // Conexão falhou
        }

    }
}
