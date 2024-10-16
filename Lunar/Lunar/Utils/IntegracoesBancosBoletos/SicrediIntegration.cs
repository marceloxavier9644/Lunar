using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.IntegracoesBancosBoletos;
using LunarBase.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Lunar.Utils.IntegracoesBancosBoletos.BoletoService;

public class SicrediIntegration
{
    private const string SandboxUrl = "https://api-parceiro.sicredi.com.br/sb/auth/openapi/token";
    private const string ProductionUrl = "https://api-parceiro.sicredi.com.br/auth/openapi/token";

    private const string SandboxBoletoUrl = "https://api-parceiro.sicredi.com.br/sb/cobranca/boleto/v1/boletos";
    private const string ProductionBoletoUrl = "https://api-parceiro.sicredi.com.br/cobranca/boleto/v1/boletos";

    private const string SandboxBoletoUrlPDF = "https://api-parceiro.sicredi.com.br/sb/cobranca/boleto/v1/boletos/pdf";
    private const string ProductionBoletoUrlPDF = "https://api-parceiro.sicredi.com.br/cobranca/boleto/v1/boletos/pdf";

    // URLs de consulta de boletos liquidados
    private const string SandboxBoletosLiquidadosUrl = "https://api-parceiro.sicredi.com.br/sb/cobranca/boleto/v1/boletos/liquidados/dia";
    private const string ProductionBoletosLiquidadosUrl = "https://api-parceiro.sicredi.com.br/cobranca/boleto/v1/boletos/liquidados/dia";

    private string _apiUrl;
    private string _boletoUrl;
    private string _boletoUrlPDF;
    private string _boletosLiquidadosUrl;
    private string _accessToken;
    private string _refreshToken;
    private DateTime _tokenExpiration;
    private bool _isProduction;

    //private const string xApiKey = "58ae06aa-759c-4e27-b9da-46be855eb3aa"; // Homologacao
    //private const string xApiKey = "eec7d157-7430-4c1d-a26d-8ad1bec34e66"; // Produção
    private string xApiKey;
    private readonly string _username; // Beneficiário + Cooperativa
    private readonly string _password; // Código gerado no Internet Banking

    private readonly string _cooperativa;
    private readonly string _posto;
    private readonly string _codigoBeneficiario;

    // Construtor para configurar o ambiente
    public SicrediIntegration(bool isProduction, string username, string password, string cooperativa, string posto, string codigoBeneficiario)
    {
        _isProduction = isProduction;
        xApiKey = isProduction ? "eec7d157-7430-4c1d-a26d-8ad1bec34e66" : "58ae06aa-759c-4e27-b9da-46be855eb3aa";
        _apiUrl = isProduction ? ProductionUrl : SandboxUrl;
        _boletoUrl = isProduction ? ProductionBoletoUrl : SandboxBoletoUrl;
        _boletoUrlPDF = isProduction ? ProductionBoletoUrlPDF : SandboxBoletoUrlPDF;
        _boletosLiquidadosUrl = isProduction ? ProductionBoletosLiquidadosUrl : SandboxBoletosLiquidadosUrl;
        _username = username;
        _password = password;
        _cooperativa = cooperativa;
        _posto = posto;
        _codigoBeneficiario = codigoBeneficiario;
    }

    // Método para obter o access_token
    public async Task<bool> AuthenticateAsync()
    {
        var client = new RestClient(_apiUrl);
        var request = new RestRequest(_apiUrl, Method.Post); // Método POST para autenticação

        request.AddHeader("x-api-key", xApiKey); // Chave de API
        request.AddHeader("context", "COBRANCA"); // Contexto fixo "COBRANCA"
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); // Tipo de conteúdo

        // Parâmetros para autenticação
        request.AddParameter("grant_type", "password");
        request.AddParameter("username", _username); // Certifique-se de que está seguindo o formato correto
        request.AddParameter("password", _password); // Código de acesso do Internet Banking
        request.AddParameter("scope", "cobranca"); // Escopo fixo "cobranca"

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            var jsonResponse = JObject.Parse(response.Content);
            _accessToken = jsonResponse["access_token"].ToString();
            _refreshToken = jsonResponse["refresh_token"].ToString();
            var expiresIn = int.Parse(jsonResponse["expires_in"].ToString());
            _tokenExpiration = DateTime.Now.AddSeconds(expiresIn);

            return true;
        }

        // Mostrar erro de autenticação
        Console.WriteLine($"Erro de autenticação: {response.StatusCode}");
        GenericaDesktop.ShowAlerta($"Resposta do servidor sicredi: {response.Content}");
        return false;
    }


    // Método para renovar o access_token com o refresh_token
    public async Task<bool> RefreshTokenAsync()
    {
        var client = new RestClient(_apiUrl);
        var request = new RestRequest(_apiUrl, Method.Post); // Método POST para renovação de token

        request.AddHeader("x-api-key", xApiKey);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", _refreshToken);

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            var jsonResponse = JObject.Parse(response.Content);
            _accessToken = jsonResponse["access_token"].ToString();
            var expiresIn = int.Parse(jsonResponse["expires_in"].ToString());
            _tokenExpiration = DateTime.Now.AddSeconds(expiresIn);

            return true;
        }

        Console.WriteLine($"Erro ao renovar token: {response.Content}");
        return false;
    }

    public async Task<BoletoResponse> CreateBoletoAsync(object boletoData)
    {
        if (DateTime.Now >= _tokenExpiration)
        {
            if (!await RefreshTokenAsync())
            {
                return null;
            }
        }

        var client = new RestClient(_boletoUrl); 
        var request = new RestRequest(_boletoUrl, Method.Post); 

        request.AddHeader("Authorization", $"Bearer {_accessToken}");
        request.AddHeader("x-api-key", xApiKey);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("cooperativa", _cooperativa); 
        request.AddHeader("posto", _posto);
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore, // Ignora valores nulos
            Formatting = Formatting.Indented // Gera o JSON formatado
        };

        string jsonBoleto = JsonConvert.SerializeObject(boletoData, settings);
        Logger logger = new Logger();
        logger.WriteLog(jsonBoleto, "Log");

        request.AddJsonBody(boletoData); 

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Boleto gerado com sucesso.");
            var jsonResponse = JObject.Parse(response.Content);
            var boletoResponse = jsonResponse.ToObject<BoletoResponse>();
            return boletoResponse;
        }
        GenericaDesktop.ShowErro($"Erro ao gerar boleto: {response.Content}");
        logger.WriteLog("Erro Boleto Sicredi: " + response.Content, "Log");
        return null;
    }


    public async Task<string[]> DownloadAndOpenBoletoPdfs(string[] linhasDigitaveis, bool imprimirNaTela)
    {
        // Usar uma lista temporária para armazenar os caminhos dos PDFs
        List<string> pdfPaths = new List<string>();
        int i = 1;
        foreach (var linhaDigitavel  in linhasDigitaveis)
        {
            // Remover espaços e caracteres inesperados
            string cleanedLinhaDigitavel = linhaDigitavel.Trim().Replace(" ", "").Replace("-", "");

            // Verificar se a linha digitável possui 47 dígitos
            if (cleanedLinhaDigitavel.Length != 47)
            {
                Console.WriteLine($"Linha digitável inválida: {cleanedLinhaDigitavel}. Deve ter 47 dígitos.");
                continue; // Ignorar esta linha digitável inválida
            }

            // Verificar se o access_token está expirado
            if (DateTime.Now >= _tokenExpiration)
            {
                if (!await RefreshTokenAsync())
                {
                    return null; // Falha na renovação do token
                }
            }

            // URL da API para obtenção do PDF do boleto
            string pdfBoletoUrl = $"{_boletoUrlPDF}?linhaDigitavel={cleanedLinhaDigitavel}";

            var client = new RestClient(pdfBoletoUrl);
            var request = new RestRequest(pdfBoletoUrl, Method.Get);

            // Headers obrigatórios
            request.AddHeader("x-api-key", xApiKey);
            request.AddHeader("Authorization", $"Bearer {_accessToken}");

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var pdfBytes = response.RawBytes;
                try
                {
                    string tempFilePath = Path.Combine(Path.GetTempPath(), $"Boleto" + i + "_" + cleanedLinhaDigitavel + ".pdf");
                    i++;
                    File.WriteAllBytes(tempFilePath, pdfBytes);
                    Console.WriteLine($"Boleto salvo com sucesso em {tempFilePath}");
                    pdfPaths.Add(tempFilePath); // Armazenar caminho do PDF
                }
                catch (Exception ex)
                {
                    GenericaDesktop.ShowAlerta($"Boleto foi gerado, mas deu erro ao baixar o pdf do boleto:\n\n {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Erro ao baixar o boleto: {response.Content}");
            }
        }

        // Verificar se há PDFs para combinar
        if (pdfPaths.Count > 1)
        {
            string combinedPdfPath = Path.Combine(Path.GetTempPath(), "Boleto.pdf");
            CombinePdfs(pdfPaths.ToArray(), combinedPdfPath);
            if (imprimirNaTela == true)
            {
                FrmPDF frmPDF = new FrmPDF(combinedPdfPath);
                frmPDF.ShowDialog();
            }
        }
        else if (pdfPaths.Count == 1)
        {
            foreach (string pdfPath in pdfPaths)
            {
                if (imprimirNaTela == true)
                {
                    FrmPDF frmPDF = new FrmPDF(pdfPath);
                    frmPDF.ShowDialog();
                }
            }
        }

        return pdfPaths.ToArray();
    }

    public void CombinePdfs(string[] pdfPaths, string outputFilePath)
    {
        using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputFilePath)))
        {
            PdfMerger merger = new PdfMerger(pdfDocument);

            foreach (string pdfPath in pdfPaths)
            {
                if (File.Exists(pdfPath)) // Verifique se o arquivo PDF existe
                {
                    using (PdfDocument sourceDocument = new PdfDocument(new PdfReader(pdfPath)))
                    {
                        if (sourceDocument.GetNumberOfPages() > 0) // Verifique se o PDF tem páginas
                        {
                            merger.Merge(sourceDocument, 1, sourceDocument.GetNumberOfPages());
                        }
                        else
                        {
                            Console.WriteLine($"O arquivo {pdfPath} não contém páginas.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"O arquivo {pdfPath} não existe.");
                }
            }
        } 
    }


    public async Task<List<BoletoLiquidado>> ConsultarBoletosLiquidadosPorDiaAsync(string dia)
    {
        // Ensure dia is in the correct format (DD/MM/YYYY) and length (10 characters)
        if (string.IsNullOrEmpty(dia) || dia.Length != 10)
        {
            Console.WriteLine("O parâmetro 'dia' deve estar no formato DD/MM/YYYY.");
            return null;
        }

        string url = $"{_boletosLiquidadosUrl}?codigoBeneficiario={_codigoBeneficiario}&dia={dia}";

        using (var client = new HttpClient())
        {
            try
            {
                // Adiciona os headers
                client.DefaultRequestHeaders.Add("x-api-key", xApiKey);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Add("cooperativa", _cooperativa);
                client.DefaultRequestHeaders.Add("posto", _posto);
                // Remove Content-Type for GET request
                // client.DefaultRequestHeaders.Add("Content-Type", "application/x-wwwformurlencoded"); // Remove this line

                // Faz a requisição GET
                HttpResponseMessage response = await client.GetAsync(url);

                // Verifica a resposta da API
                if (response.IsSuccessStatusCode)
                {
                    //string content = await response.Content.ReadAsStringAsync();
                    //return content;
                    string content = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<BoletoLiquidadoResponse>(content);
                    return responseObject?.Items;
                }
                else
                {
                    // Read and log the response content for debugging
                    string errorContent = await response.Content.ReadAsStringAsync();
                    GenericaDesktop.ShowAlerta($"Erro na consulta: {response.StatusCode}, Mensagem: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
                return null;
            }
        }
    }


}

