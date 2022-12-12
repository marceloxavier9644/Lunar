using Lunar.Utils;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Lunar.Telas.Orcamentos.FrmOrcamentoAvulso;

namespace Lunar.Telas.Orcamentos
{
    public partial class FrmImprimirOrcamentoAvulso : Form
    {
        IList<OrcamentoAvulso> listaOrcamento = new List<OrcamentoAvulso>();
        String nomeCliente = "";
        String foneCliente = "";
        String observacoes;
        public FrmImprimirOrcamentoAvulso(IList<OrcamentoAvulso> lista, string nomeCliente, string foneCliente, string observacoes)
        {
            InitializeComponent();
            listaOrcamento = lista;
            this.nomeCliente = nomeCliente;
            this.foneCliente = foneCliente;
            this.observacoes = observacoes;
        }

        private void FrmImprimirOrcamentoAvulso_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            Microsoft.Reporting.WinForms.ReportDataSource dsOrdem = new Microsoft.Reporting.WinForms.ReportDataSource();
            dsOrdem.Name = "dsOrcamentoAvulso";
            dsOrdem.Value = this.bindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(dsOrdem);
            this.reportViewer1.LocalReport.DisplayName = "Orçamento " + nomeCliente;


            string foneEmp = GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.DddPrincipal + Sessao.empresaFilialLogada.TelefonePrincipal);
            foneEmp = foneEmp.Trim();
            if (foneEmp.Length == 11)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"(00) 0 0000-0000");
            }
            else if (foneEmp.Length == 9)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"00000-0000");
            }
            else if (foneEmp.Length == 10)
            {
                foneEmp = long.Parse(foneEmp).ToString(@"(00) 0000-0000");
            }

            string logo = "";
            if(Sessao.parametroSistema.Logo != null)
            {
                if (File.Exists(Sessao.parametroSistema.Logo))
                    logo = Sessao.parametroSistema.Logo;
            }

            ReportParameter[] p = new ReportParameter[10];
            p[0] = (new ReportParameter("RazaoSocial", Sessao.empresaFilialLogada.RazaoSocial));
            p[1] = (new ReportParameter("Fone", foneEmp));
            p[2] = (new ReportParameter("Endereco", Sessao.empresaFilialLogada.Endereco.Logradouro + ", " + Sessao.empresaFilialLogada.Endereco.Numero + " " + Sessao.empresaFilialLogada.Endereco.Complemento + " " + Sessao.empresaFilialLogada.Endereco.Bairro));
            p[3] = (new ReportParameter("Cidade", "CIDADE: " + Sessao.empresaFilialLogada.Endereco.Cidade.Descricao + "-" + Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf + " CEP: " + Sessao.empresaFilialLogada.Endereco.Cep));
            p[4] = (new ReportParameter("Email", Sessao.empresaFilialLogada.Email));
            p[5] = (new ReportParameter("Logo", logo));
            p[6] = (new ReportParameter("Cliente", nomeCliente));
            p[7] = (new ReportParameter("FoneCliente", foneCliente));
            p[8] = (new ReportParameter("Observacoes", observacoes));
            p[9] = (new ReportParameter("CidadeData", Sessao.empresaFilialLogada.Endereco.Cidade.Descricao + ", " + DateTime.Now.ToLongDateString().ToUpper()));
            reportViewer1.LocalReport.SetParameters(p);

            foreach (OrcamentoAvulso orc in listaOrcamento) 
            {
                dsOrcamentoAvulso.OrcamentoAvulso.AddOrcamentoAvulsoRow(orc.descricao, orc.quantidade, orc.valorUnitario, orc.valorTotal); 
            }
            this.reportViewer1.RefreshReport();
        }

        private void btnClose_Click(object sender, EventArgs e)
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
