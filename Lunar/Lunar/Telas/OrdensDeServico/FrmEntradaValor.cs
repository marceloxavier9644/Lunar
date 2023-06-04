using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmEntradaValor : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        OrdemServico ordemServico = new OrdemServico();
        public FrmEntradaValor(OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                generica.SoNumeroEVirgula(txtValor.Texts, e);
                if (e.KeyChar == 13)
                {
                    if (txtValor.Enabled == true)
                    {
                        txtValor.Texts = string.Format("{0:0.00}", decimal.Parse(txtValor.Texts));
                        btnConfirmar.PerformClick();
                    }
                }
            }
            catch
            {

            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Form formBackground = new Form();
                IList<ContaReceber> listaReceber = new List<ContaReceber>();
                IList<ContaPagar> listaPagar = new List<ContaPagar>();
                Sessao.valorSinalOrdemServico = decimal.Parse(txtValor.Texts);
                IList<OrdemServico> listaOs = new List<OrdemServico>();
                FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "ORDEMSERVICO_SINAL", false, false, listaOs);
                formBackground.StartPosition = FormStartPosition.Manual;
                //formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                //formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                formBackground.Dispose();
                uu.Dispose();
                this.Close();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
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

        private void FrmEntradaValor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                        this.Close();
                    break;

                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;
            }
        }
    }
}
