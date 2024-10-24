using LunarBase.Classes;
using LunarBase.ControllerBO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Lunar.Telas.VisualizadorPDF;
using System.Threading.Tasks;

namespace Lunar.Utils.IntegracoesBancosBoletos.BB
{
    public partial class FrmImprimirBoletoBB : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        BoletoConfig boletoConfig = new BoletoConfig();
        BoletoConfigController boletoConfigController = new BoletoConfigController();
        private IList<ContaReceber> listaReceber;
        public FrmImprimirBoletoBB(IList<ContaReceber> listaReceber)
        {
            InitializeComponent();
            this.listaReceber = listaReceber;
        }

        private async void FrmImprimirBoletoBB_Load(object sender, EventArgs e)
        {
          

            //// Apresentar o PDF combinado na tela
            //System.Diagnostics.Process.Start(combinedPdfPath);
            //await Task.Delay(2000); // Atraso de 1 segundo (ajuste conforme necessário)
            //this.Close();
        }

        public async Task<string> ImprimirBoletosAsync()
        {
            this.reportViewer1.LocalReport.DisplayName = "BoletoBB";
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string folderPath = "C:\\Lunar\\Boletos";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<string> pdfPaths = new List<string>();

            foreach (var conta in listaReceber)
            {
                boletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancariaUnica(conta.ContaBoleto);
                if (boletoConfig.ContaBancaria.Banco.Descricao.Contains("BB") || boletoConfig.ContaBancaria.Banco.Descricao.Contains("BANCO DO BRASIL"))
                {
                    BBApiService bbApiService = new BBApiService(boletoConfig.AmbienteProducao, boletoConfig.IdToken, boletoConfig.Token);
                    BbDetalheBoletoResponse bbDetalheBoletoResponse = await bbApiService.DetalharBoletoAsync(conta.NossoNumero, int.Parse(boletoConfig.Convenio));

                    ReportParameter[] p = new ReportParameter[22];
                    p[0] = (new ReportParameter("LinhaDigitavel", FormatarLinhaDigitavel(bbDetalheBoletoResponse.codigoLinhaDigitavel)));
                    p[1] = (new ReportParameter("NomePagador", bbDetalheBoletoResponse.nomeSacadoCobranca));
                    p[2] = (new ReportParameter("EnderecoPagador", bbDetalheBoletoResponse.textoEnderecoSacadoCobranca + " " + bbDetalheBoletoResponse.nomeBairroSacadoCobranca));
                    string cepFormatado = String.Format("{0:00000-000}", bbDetalheBoletoResponse.numeroCepSacadoCobranca);
                    p[3] = (new ReportParameter("CepPagador", cepFormatado + " " + bbDetalheBoletoResponse.nomeMunicipioSacadoCobranca));
                    string docFormatado = bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString().Length == 11 ?
                                          genericaDesktop.FormatarCPF(bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString()) :
                                          genericaDesktop.FormatarCNPJ(bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString());
                    p[4] = (new ReportParameter("CpfPagador", docFormatado));
                    p[5] = (new ReportParameter("DataVencimento", DateTime.Parse(bbDetalheBoletoResponse.dataVencimentoTituloCobranca).ToShortDateString()));

                    // BENEFICIARIO
                    p[6] = (new ReportParameter("NomeBeneficiario", conta.EmpresaFilial.RazaoSocial));
                    p[7] = (new ReportParameter("CnpjBeneficiario", genericaDesktop.FormatarCNPJ(conta.EmpresaFilial.Cnpj)));
                    string logradouroEmpresa = conta.EmpresaFilial.Endereco != null ?
                                               conta.EmpresaFilial.Endereco.Logradouro + ", " + conta.EmpresaFilial.Endereco.Numero + " " + conta.EmpresaFilial.Endereco.Complemento : "";
                    string bairroEmpresa = conta.EmpresaFilial.Endereco?.Bairro ?? "";
                    string cidadeEmpresa = conta.EmpresaFilial.Endereco?.Cidade?.Descricao + "-" + conta.EmpresaFilial.Endereco?.Cidade?.Estado?.Uf ?? "";
                    string cepEmpresa = conta.EmpresaFilial.Endereco != null ? String.Format("{0:00000-000}", conta.EmpresaFilial.Endereco.Cep) : "";
                    p[8] = (new ReportParameter("EnderecoBeneficiario", logradouroEmpresa + " " + bairroEmpresa));
                    p[9] = (new ReportParameter("CepBeneficiario", cepEmpresa + " " + cidadeEmpresa));

                    // Dados Boleto
                    p[10] = (new ReportParameter("NossoNumero", conta.NossoNumero));
                    p[11] = (new ReportParameter("AgenciaCodigo", boletoConfig.ContaBancaria.Agencia + "-" + boletoConfig.ContaBancaria.DvAgencia + "/" + boletoConfig.ContaBancaria.Conta + "-" + boletoConfig.ContaBancaria.DvConta));
                    p[12] = (new ReportParameter("NumeroDocumento", bbDetalheBoletoResponse.numeroContratoCobranca.ToString()));
                    p[13] = (new ReportParameter("ValorDocumento", bbDetalheBoletoResponse.valorAtualTituloCobranca.ToString("N2")));
                    p[14] = (new ReportParameter("DataProcessamento", DateTime.Parse(bbDetalheBoletoResponse.dataEmissaoTituloCobranca).ToShortDateString()));
                    p[15] = (new ReportParameter("Carteira", bbDetalheBoletoResponse.numeroCarteiraCobranca.ToString()));
                    p[16] = (new ReportParameter("Desconto", bbDetalheBoletoResponse.valorDescontoTitulo.ToString("N2")));
                    p[17] = (new ReportParameter("Juro", bbDetalheBoletoResponse.valorJuroMoraTitulo.ToString("N2")));
                    p[18] = (new ReportParameter("ValorCobrado", bbDetalheBoletoResponse.valorAtualTituloCobranca.ToString("N2")));
                    p[19] = new ReportParameter("Informacoes", bbDetalheBoletoResponse.textoCampoUtilizacaoCedente);
                    p[20] = (new ReportParameter("CodigoBarras", GerarEGuardarCodigoDeBarras25Intercalado(bbDetalheBoletoResponse.textoCodigoBarrasTituloCobranca)));
                    p[21] = conta.QrCode != null ? new ReportParameter("QrCode", GerarEGuardarQrCode(conta.QrCode)) : new ReportParameter("QrCode", "");

                    reportViewer1.LocalReport.SetParameters(p);

                    // Salvar cada boleto como PDF
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    string pdfPath = Path.Combine(folderPath, $"BoletoBB_{conta.NossoNumero}.pdf");
                    using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    // Adicionar o caminho do PDF à lista
                    pdfPaths.Add(pdfPath);
                }
            }
            // Combinar PDFs
            string combinedPdfPath = Path.Combine(folderPath, "BoletoBB_Combined.pdf");
            using (PdfDocument combinedDocument = new PdfDocument())
            {
                foreach (string pdfPath in pdfPaths)
                {
                    if (File.Exists(pdfPath))
                    {
                        // Abre o PDF existente
                        PdfDocument pdf = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import);
                        // Copia cada página para o documento combinado
                        for (int i = 0; i < pdf.PageCount; i++)
                        {
                            combinedDocument.AddPage(pdf.Pages[i]);
                        }
                        // Deletar o PDF individual após ser adicionado ao combinado
                        File.Delete(pdfPath);
                    }
                }
                // Salvar o documento combinado
                combinedDocument.Save(combinedPdfPath);
            }
            return combinedPdfPath;
        }

        public string FormatarLinhaDigitavel(string linhaDigitavel)
        {
            // Garantir que a linha digitável tenha 47 caracteres
            if (linhaDigitavel.Length != 47)
                throw new ArgumentException("A linha digitável deve conter exatamente 47 caracteres.");

            // Aplicar a formatação padrão da linha digitável
            string formatada = string.Format("{0}.{1} {2}.{3} {4}.{5} {6} {7}",
                linhaDigitavel.Substring(0, 5),   // 21290
                linhaDigitavel.Substring(5, 5),   // 00119
                linhaDigitavel.Substring(10, 5),  // 21100
                linhaDigitavel.Substring(15, 6),  // 012109
                linhaDigitavel.Substring(21, 5),  // 04475
                linhaDigitavel.Substring(26, 6),  // 617405
                linhaDigitavel.Substring(32, 1),  // 9 (Dígito Verificador)
                linhaDigitavel.Substring(33, 14)  // 75870000002000 (Valor e Data)
            );

            return formatada;
        }

        public string GerarEGuardarCodigoDeBarras25Intercalado(string codigoDeBarras)
        {
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.ITF,  // Formato "Interleaved 2 of 5"
                Options = new EncodingOptions
                {
                    Width = 1030,  // 103 mm convertido para pixels (assumindo 10 pixels por mm)
                    Height = 130,  // 13 mm convertido para pixels (assumindo 10 pixels por mm)
                    Margin = 5,
                    PureBarcode = true 
                }
            };

            // Gera o código de barras como uma imagem
            Image barcodeImage = barcodeWriter.Write(codigoDeBarras);

            // Obtém o caminho temporário para salvar a imagem
            string tempPath = Path.GetTempPath();
            string imagePath = Path.Combine(tempPath, $"{codigoDeBarras}_codigoBarras.png");

            // Salva a imagem no diretório temporário
            barcodeImage.Save(imagePath, ImageFormat.Png);

            // Retorna o caminho completo da imagem gerada
            return imagePath;
        }

        public string GerarEGuardarQrCode(string textoQrCode)
        {
            try
            {
                var qrCodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,  // Formato QR Code
                    Options = new EncodingOptions
                    {
                        Width = 300,  // Largura do QR Code em pixels
                        Height = 300, // Altura do QR Code em pixels
                        Margin = 1    // Definir margem pequena (opcional)
                    }
                };

                // Gera o QR Code como uma imagem
                Image qrCodeImage = qrCodeWriter.Write(textoQrCode);

                // Obtém o caminho temporário para salvar a imagem
                string tempPath = Path.GetTempPath();
                string imagePath = Path.Combine(tempPath, $"PixBB_qrCode.png");

                // Salva a imagem no diretório temporário
                qrCodeImage.Save(imagePath, ImageFormat.Png);

                // Retorna o caminho completo da imagem gerada
                return imagePath;
            }
            catch
            {
                return "";
            }
        }

    }
}
