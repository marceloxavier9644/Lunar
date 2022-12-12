using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber.Reports
{
    public partial class FrmRecibo01 : Form
    {
        Pessoa cliente = new Pessoa();
        String descricaoRecibo = "";
        decimal valorRecibo = 0;
        public FrmRecibo01(String descricaoRecibo, decimal valorRecibo, Pessoa cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.descricaoRecibo = descricaoRecibo;
            this.valorRecibo = valorRecibo;
        }

        private void gerarReciboContaReceber()
        {
            this.reportViewer1.LocalReport.DisplayName = "Recibo " + cliente.RazaoSocial;              
            String cnpjFormatado = "";
            String cidadeEmpresa = "";

            //CNPJ DA EMPRESA
            if (Sessao.empresaFilialLogada.Cnpj.Length == 14)
            {
                cnpjFormatado = Convert.ToUInt64(Sessao.empresaFilialLogada.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (Sessao.empresaFilialLogada.Cnpj.Length == 11)
            {
                cnpjFormatado = Convert.ToUInt64(Sessao.empresaFilialLogada.Cnpj).ToString(@"000\.000\.000\-00");
            }
            else
            {
                cnpjFormatado = "";
            }
            //Cidade e Bairro da empresa
            if (Sessao.empresaFilialLogada.Endereco != null)
            {
                cidadeEmpresa = Sessao.empresaFilialLogada.Endereco.Cidade.Descricao + "-" + Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            }

            ReportParameter[] p = new ReportParameter[8];
            p[0] = (new ReportParameter("EMPRESAFANTASIA", Sessao.empresaFilialLogada.NomeFantasia));
            p[1] = (new ReportParameter("EMPRESACNPJ", cnpjFormatado));
            p[2] = (new ReportParameter("DESCRICAO", descricaoRecibo.Replace("RECEBIMENTO DE ", "").Replace(cliente.RazaoSocial + " ", "")));
            p[3] = (new ReportParameter("VALORNUMERICO", string.Format("{0:0.00}", valorRecibo)));
            p[4] = (new ReportParameter("CLIENTENOME", cliente.RazaoSocial));
            p[5] = (new ReportParameter("VALOREXTENSO", ConverterMoedaPorExtenso.toExtenso(valorRecibo)));
            p[6] = (new ReportParameter("CIDADE", cidadeEmpresa));
            p[7] = (new ReportParameter("DATALONGA", DateTime.Now.ToLongDateString().ToUpper()));
            reportViewer1.LocalReport.SetParameters(p);

            this.reportViewer1.RefreshReport();
        }

        private void FrmRecibo01_Load(object sender, EventArgs e)
        {
            gerarReciboContaReceber();
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
