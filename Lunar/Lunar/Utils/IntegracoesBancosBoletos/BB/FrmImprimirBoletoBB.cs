using LunarBase.Classes;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Utils.IntegracoesBancosBoletos.BB
{
    public partial class FrmImprimirBoletoBB : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        BbDetalheBoletoResponse bbDetalheBoletoResponse = new BbDetalheBoletoResponse();
        EmpresaFilial empresaFilial = new EmpresaFilial();
        RetornoBoletoBB response  = new RetornoBoletoBB();
        BoletoConfig boletoConfig = new BoletoConfig();
        public FrmImprimirBoletoBB(BbDetalheBoletoResponse bbDetalheBoletoResponse, EmpresaFilial empresaFilial, RetornoBoletoBB response, BoletoConfig boletoConfig)
        {
            InitializeComponent();
            this.bbDetalheBoletoResponse = bbDetalheBoletoResponse;
            this.empresaFilial = empresaFilial;
            this.response = response;
            this.boletoConfig = boletoConfig;
        }

        private void FrmImprimirBoletoBB_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DisplayName = "BoletoBB";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            ReportParameter[] p = new ReportParameter[15];
            p[0] = (new ReportParameter("LinhaDigitavel", FormatarLinhaDigitavel(bbDetalheBoletoResponse.codigoLinhaDigitavel)));
            p[1] = (new ReportParameter("NomePagador", bbDetalheBoletoResponse.nomeSacadoCobranca));
            p[2] = (new ReportParameter("EnderecoPagador", bbDetalheBoletoResponse.textoEnderecoSacadoCobranca + ", "
                + bbDetalheBoletoResponse.numeroCepSacadoCobranca + " " + bbDetalheBoletoResponse.nomeBairroSacadoCobranca));
            string cepFormatado = String.Format("{0:00000-000}", bbDetalheBoletoResponse.numeroCepSacadoCobranca);
            p[3] = (new ReportParameter("CepPagador", cepFormatado + " " + bbDetalheBoletoResponse.nomeMunicipioSacadoCobranca));
            string docFormatado = "";
            if (bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString().Length == 11)
                docFormatado = genericaDesktop.FormatarCPF(bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString());
            else
                docFormatado = genericaDesktop.FormatarCNPJ(bbDetalheBoletoResponse.numeroInscricaoSacadoCobranca.ToString());
            p[4] = (new ReportParameter("CpfPagador", docFormatado)); 
            p[5] = (new ReportParameter("DataVencimento", DateTime.Parse(bbDetalheBoletoResponse.dataVencimentoTituloCobranca).ToShortDateString()));

            //BENEFICIARIO
            p[6] = (new ReportParameter("NomeBeneficiario", empresaFilial.RazaoSocial));
            p[7] = (new ReportParameter("CnpjBeneficiario", genericaDesktop.FormatarCNPJ(empresaFilial.Cnpj)));
            string logradouroEmpresa = "";
            string bairroEmpresa = "";
            string cepEmpresa = "";
            string cidadeEmpresa = "";
            if (empresaFilial.Endereco != null)
            {
                cepEmpresa = String.Format("{0:00000-000}", empresaFilial.Endereco.Cep);
                logradouroEmpresa = empresaFilial.Endereco.Logradouro + ", " + empresaFilial.Endereco.Numero + " " + empresaFilial.Endereco.Complemento;
                bairroEmpresa = empresaFilial.Endereco.Bairro;
                cidadeEmpresa = empresaFilial.Endereco.Cidade.Descricao + "-" + empresaFilial.Endereco.Cidade.Estado.Uf;
            }
            p[8] = (new ReportParameter("EnderecoBeneficiario", logradouroEmpresa + " " + bairroEmpresa));
            p[9] = (new ReportParameter("CepBeneficiario", cepEmpresa + cidadeEmpresa));

            //Dados Boleto
            p[10] = (new ReportParameter("NossoNumero", response.numero));
            p[11] = (new ReportParameter("AgenciaCodigo", boletoConfig.ContaBancaria.Agencia + "-" + boletoConfig.ContaBancaria.DvAgencia + "/" + boletoConfig.ContaBancaria.Conta + "-" + boletoConfig.ContaBancaria.DvConta));
            p[12] = (new ReportParameter("NumeroDocumento", response.numeroContratoCobranca.ToString()));
            p[13] = (new ReportParameter("ValorDocumento", bbDetalheBoletoResponse.valorAtualTituloCobranca.ToString("N2")));
            p[14] = (new ReportParameter("DataProcessamento", DateTime.Parse(bbDetalheBoletoResponse.dataEmissaoTituloCobranca).ToShortDateString()));
            reportViewer1.LocalReport.SetParameters(p);

            this.reportViewer1.RefreshReport();
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
    }
}
