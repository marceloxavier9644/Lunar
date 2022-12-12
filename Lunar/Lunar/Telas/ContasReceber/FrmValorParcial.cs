using Lunar.Utils;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmValorParcial : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        bool showModal = false;
        decimal valor = 0;
        decimal valorParcelaOriginal = 0;
        public DialogResult showModalNovo(ref decimal valor)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                valor = this.valor;
            }
            return DialogResult;
        }
        public FrmValorParcial(decimal valorSelecionado)
        {
            InitializeComponent();
            valorParcelaOriginal = Math.Round(valorSelecionado, 2);
            lblValorParcela.Text = "Parcela Selecionada " + valorSelecionado.ToString("C2", CultureInfo.CurrentCulture);
            txtValorParcial.TextAlign = HorizontalAlignment.Center;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtValorParcial.Texts))
            {
                this.valor = decimal.Parse(txtValorParcial.Texts);
                if (this.valor < valorParcelaOriginal)
                {
                    if (showModal)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Valor da Parcial deve ser menor que o total da parcela original");
            }
            else
            {
                DialogResult = DialogResult.Ignore;
            }
        }

        private void txtValorParcial_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtValorParcial.Texts))
                {
                    decimal val = decimal.Parse(txtValorParcial.Texts);
                    txtValorParcial.Texts = string.Format("{0:0.00}", val);
                }
            }
            catch
            {

            }
        }

        private void txtValorParcial_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtValorParcial.Texts, e);
            if(e.KeyChar == 13)
            {
                btnConfirmar.PerformClick();
            }
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
