using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.RelatoriosDiversos
{
    public partial class FrmSelecionarData : Form
    {
        bool showModal = false;
        DateTime dataInicial;
        DateTime dataFinal;
        public DialogResult showModalNovo(ref DateTime dataInicial, ref DateTime dataFinal)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                dataInicial = this.dataInicial;
                dataFinal = this.dataFinal;
            }
            return DialogResult;
        }
        public FrmSelecionarData()
        {
            InitializeComponent();
            string primeiroDiaMesAnterior = string.Concat("01/", DateTime.Now.AddMonths(-1).ToString("MM/yyyy"));
            DateTime ultimoDiaMesAnterior = DateTime.Parse(string.Concat("01/", DateTime.Now.ToString("MM/yyyy")));
            txtDataInicial.Value = DateTime.Parse(primeiroDiaMesAnterior);
            txtDataFinal.Value = ultimoDiaMesAnterior.AddDays(-1);
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

        private void FrmSelecionarData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    confirmaItem();
                    break;
                case Keys.Enter:
                    confirmaItem();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void confirmaItem()
        {
            this.dataInicial = DateTime.Parse(txtDataInicial.Value.ToString());
            this.dataFinal = DateTime.Parse(txtDataFinal.Value.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            confirmaItem();
        }
    }
}
