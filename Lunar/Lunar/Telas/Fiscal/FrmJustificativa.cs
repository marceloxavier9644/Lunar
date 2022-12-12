using LunarBase.Classes;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmJustificativa : Form
    {
        string justificativa = "";
        public DialogResult showModal(ref String justificativa)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                justificativa = this.justificativa;
            }
            return DialogResult;
        }
        public FrmJustificativa(string titulo, string nomeCampo, Nfe nfe)
        {
            InitializeComponent();
            lblTitulo.Text = titulo;
            lblCampo.Text = nomeCampo;
            //WhatsApp
            if (titulo.Contains("EMAIL") || titulo.Contains("Email") || titulo.Contains("email") || titulo.Contains("E-Mail") || titulo.Contains("E-mail"))
            {
                if(nfe.Cliente != null)
                {
                    txtCorrecao.Text = nfe.Cliente.Email;
                }
            }
            if (titulo.Contains("WhatsApp"))
            {
                if (nfe.Cliente != null)
                {
                    if(nfe.Cliente.PessoaTelefone != null)
                        txtCorrecao.Text = nfe.Cliente.PessoaTelefone.Ddd + nfe.Cliente.PessoaTelefone.Telefone;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCorrecao.Text.Trim()))
            {
                this.justificativa = txtCorrecao.Text.Trim();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Ignore;
            }
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

        private void FrmJustificativa_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    btnImprimir.PerformClick();
                    break;
            }
        }
    }
}
