
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Lunar.Utils;
using Newtonsoft.Json.Linq;
using OpenAC.Net.Core.Extensions;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using static Lunar.Utils.IntegracoesBancosBoletos.BoletoService;

public class SicrediIntegration
{
    private const string SandboxUrl = "https://api-parceiro.sicredi.com.br/sb/auth/openapi/token";
    private const string ProductionUrl = "https://api-parceiro.sicredi.com.br/auth/openapi/token";

    private const string SandboxBoletoUrl = "https://api-parceiro.sicredi.com.br/sb/cobranca/boleto/v1/boletos";
    private const string ProductionBoletoUrl = "https://api-parceiro.sicredi.com.br/cobranca/boletos";


    private string _apiUrl;
    private string _boletoUrl;
    private string _accessToken;
    private string _refreshToken;
    private DateTime _tokenExpiration;

    private const string xApiKey = "58ae06aa-759c-4e27-b9da-46be855eb3aa";
    private readonly string _username; // Beneficiário + Cooperativa
    private readonly string _password; // Código gerado no Internet Banking

    private readonly string _cooperativa;
    private readonly string _posto;

    // Construtor para configurar o ambiente
    public SicrediIntegration(bool isProduction, string username, string password, string cooperativa, string posto)
    {
        _apiUrl = isProduction ? ProductionUrl : SandboxUrl;
        _boletoUrl = isProduction ? ProductionBoletoUrl : SandboxBoletoUrl;
        _username = username;
        _password = password;
        _cooperativa = cooperativa;
        _posto = posto;
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
        Console.WriteLine($"Resposta do servidor: {response.Content}");
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

    // Método para enviar a requisição de geração de boleto
    public async Task<bool> CreateBoletoAsync(object boletoData)
    {
        // Verificar se o access_token está expirado
        if (DateTime.Now >= _tokenExpiration)
        {
            if (!await RefreshTokenAsync())
            {
                return false; // Falha na renovação do token
            }
        }

        var client = new RestClient(_boletoUrl); // Usando a URL de criação de boletos
        var request = new RestRequest(_boletoUrl, Method.Post); // Passando a URL do boleto aqui

        // Adicionando os cabeçalhos
        request.AddHeader("Authorization", $"Bearer {_accessToken}");
        request.AddHeader("x-api-key", xApiKey);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("cooperativa", _cooperativa); // Adiciona o cabeçalho cooperativa
        request.AddHeader("posto", _posto); // Adiciona o cabeçalho posto

        request.AddJsonBody(boletoData); // Adiciona o corpo da requisição com os dados do boleto

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Boleto gerado com sucesso.");
            var jsonResponse = JObject.Parse(response.Content);
            var boletoResponse = jsonResponse.ToObject<BoletoResponse>();
            CreateSicrediBoletoPdf(boletoResponse);
            return true;
        }

        GenericaDesktop.ShowErro($"Erro ao gerar boleto: {response.Content}");
        return false;
    }

    public void CreateSicrediBoletoPdf(BoletoResponse boletoResponse)
    {
        using (var stream = new MemoryStream())
        {
            // Cria o documento PDF
            using (var pdfWriter = new PdfWriter(stream))
            using (var pdf = new PdfDocument(pdfWriter))
            {
                Document document = new Document(pdf);

                // Adiciona o logo do Sicredi
                System.Drawing.Image logo = System.Drawing.Image.FromFile("caminho/para/sua/imagem/sicredi.png"); // Use o caminho adequado
                var logoImage = ImageDataFactory.Create(logo.ToByteArray());
                document.Add(new iText.Layout.Element.Image(logoImage).SetWidth(120)); // Defina a largura do logo conforme necessário

                // Adiciona título
                document.Add(new Paragraph("Boleto Bancário")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold());

                // Adiciona informações do boleto
                document.Add(new Paragraph($"TXID: {boletoResponse.Txid}"));
                document.Add(new Paragraph($"QR Code: {boletoResponse.QrCode}"));
                document.Add(new Paragraph($"Linha Digitável: {boletoResponse.LinhaDigitavel}"));
                document.Add(new Paragraph($"Código de Barras: {boletoResponse.CodigoBarras}"));
                document.Add(new Paragraph($"Cooperativa: {boletoResponse.Cooperativa}"));
                document.Add(new Paragraph($"Posto: {boletoResponse.Posto}"));
                document.Add(new Paragraph($"Nosso Número: {boletoResponse.NossoNumero}"));

                // Adiciona informações adicionais (se necessário)
                document.Add(new Paragraph("Pagável preferencialmente na rede bancária ou correspondentes autorizados"));

                // Fecha o documento
                document.Close();
            }

            // Salva o PDF em um arquivo
            File.WriteAllBytes("boleto_sicredi.pdf", stream.ToArray());
        }
    }

}
