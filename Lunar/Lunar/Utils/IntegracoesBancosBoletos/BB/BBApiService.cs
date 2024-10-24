using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LunarBase.Classes;
using Newtonsoft.Json;
using static Lunar.Utils.IntegracoesBancosBoletos.BB.RetornoBoletoBB;

namespace Lunar.Utils.IntegracoesBancosBoletos.BB
{

    public class BBApiService
    {
        private readonly string _gwStringAppKey;
        private readonly string _gwAppKey;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _oauthUrl;
        private readonly string _apiUrl;
        private readonly bool _ambienteProducao;

        // Construtor que configura automaticamente URLs e chaves com base no ambiente (produção ou homologação)
        public BBApiService(bool ambienteProducao, string clientId, string clientSecret)
        {
            _ambienteProducao = ambienteProducao;
            _clientId = clientId;
            _clientSecret = clientSecret;

            // Verifica o ambiente e ajusta as chaves e URLs de acordo
            if (ambienteProducao == false)
            {
                _gwStringAppKey = "gw-app-key";
                _gwAppKey = "3c5612d0fd9924d051f1bc8e7b31e846"; // Homologação
                _oauthUrl = "https://oauth.hm.bb.com.br/oauth/token";
                _apiUrl = "https://api.hm.bb.com.br/cobrancas/v2";
            }
            else
            {
                _gwStringAppKey = "gw-dev-app-key";
                _gwAppKey = "3c5612d0fd9924d051f1bc8e7b31e846"; // Produção
                _oauthUrl = "https://oauth.bb.com.br/oauth/token";
                _apiUrl = "https://api.bb.com.br/cobrancas/v2";
            }
        }

        // Método para obter o token OAuth 2.0
        private async Task<string> GetOAuthTokenAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                var request = new HttpRequestMessage(HttpMethod.Post, _oauthUrl)
                {
                    Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded")
                };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                dynamic tokenData = JsonConvert.DeserializeObject(content);
                return tokenData.access_token;
            }
        }

        // Método para criar um novo boleto
        public async Task<RetornoBoletoBB> CriarBoletoAsync(BbBoletoRequest boletoRequest)
        {
            string token = await GetOAuthTokenAsync();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_gwStringAppKey, _gwAppKey);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string jsonContent = JsonConvert.SerializeObject(boletoRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync($"{_apiUrl}/boletos", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var retornoBoletoBB = new RetornoBoletoBB();

                        // Deserializar a resposta de erro
                        var apiErrorResponse = JsonConvert.DeserializeObject<RetornoBoletoBB>(errorContent);
                        retornoBoletoBB.erros = apiErrorResponse.erros; // Adiciona os erros à resposta

                        throw new ApiException($"Erro na requisição: {response.StatusCode}. Detalhes: {string.Join(", ", retornoBoletoBB.erros.Select(e => e.mensagem))}", retornoBoletoBB);
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var retornoBoleto = JsonConvert.DeserializeObject<RetornoBoletoBB>(result);
                    if(retornoBoleto != null)
                        Console.WriteLine($"Boleto criado com sucesso: {retornoBoleto.numero}");
                    return retornoBoleto;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Erro na comunicação com a API: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao criar o boleto: {ex.Message}", ex);
                }
            }
        }

        //Listagem ou retorno de boletos
        //Indicador de Situacao = Domínio: A - boletos em ser; B - boletos baixados, liquidados ou protestados
        public async Task<RetornoListaBoletosBaixadosBB> ListarBoletosBancoBrasilAsync(string agenciaSemDigitoBeneficiario, string contaSemDigitoBeneficiario, string dataInicialOcorrencia, string dataFinalOcorrencia)
        {
            if (_ambienteProducao == false)
            {
                // agência e conta para homologação
                agenciaSemDigitoBeneficiario = "452";
                contaSemDigitoBeneficiario = "123873";
            }

            // Boletos baixados, liquidados ou protestados
            string indicadorSituacao = "B";
            contaSemDigitoBeneficiario = contaSemDigitoBeneficiario.TrimStart('0');
            agenciaSemDigitoBeneficiario = agenciaSemDigitoBeneficiario.TrimStart('0');

            // Obtém o token OAuth
            string token = await GetOAuthTokenAsync();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_gwStringAppKey, _gwAppKey);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Constrói a URL com os parâmetros de consulta
                string url = $"{_apiUrl}/boletos?" +
                             $"indicadorSituacao={indicadorSituacao}&" +
                             $"agenciaBeneficiario={agenciaSemDigitoBeneficiario}&" +
                             $"contaBeneficiario={contaSemDigitoBeneficiario}&" +
                             $"dataInicioMovimento={dataInicialOcorrencia}&" +
                             $"dataFimMovimento={dataFinalOcorrencia}";

                try
                {
                    // Envia a requisição GET para a API
                    var response = await client.GetAsync(url);

                    // Verifica se a resposta foi bem-sucedida
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var apiError = JsonConvert.DeserializeObject<RetornoBoletoBB>(errorContent);
                        throw new ApiException($"Erro ao listar boletos: {response.StatusCode}", apiError);
                    }

                    // Retorna o resultado em formato JSON
                    var result = await response.Content.ReadAsStringAsync();
                    //string pattern = @"(\d{2})\.(\d{2})\.(\d{4})";
                    //string replacedResult = Regex.Replace(result, pattern, m => $"{m.Groups[1].Value}/{m.Groups[2].Value}/{m.Groups[3].Value}");
                    RetornoListaBoletosBaixadosBB retornoBoletos = JsonConvert.DeserializeObject<RetornoListaBoletosBaixadosBB>(result);

                    return retornoBoletos;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Erro na comunicação com a API: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao listar boletos: {ex.Message}", ex);
                }
            }
        }


        // Método para detalhar um boleto existente pelo número do título
        public async Task<BbDetalheBoletoResponse> DetalharBoletoAsync(string numeroTitulo, int numeroConvenio)
        {
            string token = await GetOAuthTokenAsync();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_gwStringAppKey, _gwAppKey); // Usa a chave correta (homologação ou produção)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{_apiUrl}/boletos/{numeroTitulo}?numeroConvenio={numeroConvenio}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                BbDetalheBoletoResponse boletoResponse = JsonConvert.DeserializeObject<BbDetalheBoletoResponse>(result);
                return boletoResponse;
            }
        }

        public async Task<RetornoBoletoBB> CriarBoletoBancoBrasilAsync(Pessoa cliente, ContaReceber contaReceber, BoletoConfig boletoConfig)
        {
            try
            {
                if (cliente == null) throw new ArgumentException("Dados do cliente são obrigatórios.");
                if (contaReceber == null) throw new ArgumentException("Dados da conta a receber são obrigatórios.");
                if (boletoConfig == null) throw new ArgumentException("Configuração do boleto é obrigatória.");
                
                string nossoNumero = "";
                if (_ambienteProducao == true)
                    nossoNumero = GerarNossoNumero(int.Parse(boletoConfig.Convenio), contaReceber.Id);
                else
                {
                    Random random = new Random();
                    int numeroAleatorio = random.Next(100000, 999999);
                    //numero aleatorio para evitar ter duplicidade de boleto no ambiente de homologacao
                    nossoNumero = GerarNossoNumero(int.Parse(boletoConfig.Convenio), numeroAleatorio + contaReceber.Id);
                }

                var (tipoInscricao, numeroInscricao) = ObterTipoENumeroInscricao(cliente);

                var boletoRequest = new BbBoletoRequest
                {
                    numeroConvenio = int.Parse(boletoConfig.Convenio),
                    numeroCarteira = int.Parse(boletoConfig.NumeroCarteira),
                    numeroVariacaoCarteira = int.Parse(boletoConfig.VariacaoCarteira),
                    codigoModalidade = 01,
                    dataEmissao = DateTime.Now.ToString("dd.MM.yyyy"),
                    dataVencimento = contaReceber.Vencimento.ToString("dd.MM.yyyy"),
                    valorOriginal = Math.Round(contaReceber.ValorParcela, 2),
                    valorAbatimento = 0,
                    quantidadeDiasProtesto = 15,
                    indicadorAceiteTituloVencido = "S",
                    numeroDiasLimiteRecebimento = "30",
                    codigoAceite = "A",
                    codigoTipoTitulo = "2",
                    descricaoTipoTitulo = "DM",
                    indicadorPermissaoRecebimentoParcial = "N",
                    numeroTituloBeneficiario = GenericaDesktop.RemoveCaracteres(contaReceber.Documento).PadLeft(15, '0'),
                    numeroTituloCliente = nossoNumero,
                    textoCampoUtilizacaoBeneficiario = "DOC: " + contaReceber.Documento,
                    mensagemBloquetoOcorrencia = (boletoConfig.MensagemBoleto?.Trim() ?? "") +
    (boletoConfig.JuroMensal > 0 ?
    $" Juros Taxa Mensal {boletoConfig.JuroMensal.ToString("F2")}%. " : "") +
    (boletoConfig.Multa > 0 ?
    $" Multa de {boletoConfig.Multa.ToString("F2")}% após o vencimento." : "").Trim(),
                    desconto = new descontoRequest
                    {
                        tipo = 0,
                        dataExpiracao = "",
                        porcentagem = Math.Round(decimal.Parse("0"), 0),
                        valor = Math.Round(decimal.Parse("0"), 0)
                    },
                    segundoDesconto = new descontoRequest
                    {
                        tipo = 0,
                        dataExpiracao = "",
                        porcentagem = Math.Round(decimal.Parse("0"), 0),
                        valor = Math.Round(decimal.Parse("0"), 0)
                    },
                    terceiroDesconto = new descontoRequest
                    {
                        tipo = 0,
                        dataExpiracao = "",
                        porcentagem = Math.Round(decimal.Parse("0"),0),
                        valor = Math.Round(decimal.Parse("0"), 0)
                    },
                    jurosMora = new jurosMoraRequest
                    {
                        tipo = 2,
                        porcentagem = Math.Round(boletoConfig.JuroMensal,2)
                    },
                    multa = new multaRequest
                    {
                        tipo = 2,
                        data = ProximoDiaUtil(contaReceber.Vencimento.AddDays(1)).ToString("dd.MM.yyyy"),
                        porcentagem = Math.Round(boletoConfig.Multa,2)
                    },
                    pagador = new pagadorRequest
                    {
                        tipoInscricao = tipoInscricao,
                        numeroInscricao = numeroInscricao,
                        nome = cliente.RazaoSocial,
                        endereco = cliente.EnderecoPrincipal.Logradouro + ", " + cliente.EnderecoPrincipal.Numero + " " + cliente.EnderecoPrincipal.Complemento,
                        cep = long.Parse(GenericaDesktop.RemoveCaracteres(cliente.EnderecoPrincipal.Cep)),
                        cidade = cliente.EnderecoPrincipal.Cidade.Descricao,
                        bairro = cliente.EnderecoPrincipal.Bairro,
                        uf = cliente.EnderecoPrincipal.Cidade.Estado.Uf,
                        telefone = cliente.PessoaTelefone?.Ddd != null && cliente.PessoaTelefone.Telefone != null
                                   ? $"{cliente.PessoaTelefone.Ddd}{cliente.PessoaTelefone.Telefone}"
                                   : string.Empty,
                        //Email = string.IsNullOrEmpty(cliente.Email) ? null : cliente.Email
                    },
                    indicadorPix = boletoConfig.TipoBoleto == "HIBRIDO" ? "S" : "N"
                };

                // Chama o método de criação de boleto na API
                RetornoBoletoBB boletoCriado = await CriarBoletoAsync(boletoRequest);
                if (boletoCriado != null)
                {
                    Console.WriteLine($"Boleto criado com sucesso: {boletoCriado.numero}");
                    return boletoCriado;
                }
                else
                    return null;
            }
            catch (ArgumentException argEx)
            {
                throw new Exception($"Erro na validação de dados: {argEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar o boleto: {ex.Message}");
            }
        }

        public string GerarNossoNumero(int numeroConvenio, int numeroTituloCliente)
        {
            // Formatar o número do convênio com 7 dígitos
            string convenioFormatado = numeroConvenio.ToString("D7");

            // Formatar o número do título do cliente com 10 dígitos
            string tituloFormatado = numeroTituloCliente.ToString("D10");

            // Concatenar e formatar o nosso número
            string nossoNumero = $"000{convenioFormatado}{tituloFormatado}";

            // Garantir que o nosso número tenha exatamente 20 dígitos
            if (nossoNumero.Length > 20)
            {
                throw new ArgumentException("O nosso número gerado ultrapassa 20 dígitos.");
            }

            return nossoNumero;
        }

        public bool EhDiaUtil(DateTime data)
        {
            // Verifica se o dia é um sábado ou domingo
            return data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday;
        }
        public DateTime ProximoDiaUtil(DateTime data)
        {
            DateTime proximoDia = data;

            // Incrementa até encontrar um dia útil
            while (!EhDiaUtil(proximoDia))
            {
                proximoDia = proximoDia.AddDays(1);
            }

            return proximoDia;
        }
        public (int tipoInscricao, long numeroInscricao) ObterTipoENumeroInscricao(Pessoa cliente)
        {
            // Verifica se o documento é um CPF ou CNPJ
            if (cliente.Cnpj.Length == 11) // CPF
            {
                // Remove o primeiro dígito se for 0
                string cpf = cliente.Cnpj.StartsWith("0") ? cliente.Cnpj.Substring(1) : cliente.Cnpj;
                return (1, long.Parse(cpf));
            }
            else if (cliente.Cnpj.Length == 14) // CNPJ
            {
                // Remove o primeiro dígito se for 0
                string cnpj = cliente.Cnpj.StartsWith("0") ? cliente.Cnpj.Substring(1) : cliente.Cnpj;
                return (2, long.Parse(cnpj));
            }
            else
            {
                throw new ArgumentException("Documento inválido. Deve ser CPF (11 dígitos) ou CNPJ (14 dígitos).");
            }
        }
    }
}