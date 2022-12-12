using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico.TipoObjetos
{
    public partial class FrmTipoObjetoCadastro : Form
    {
        bool showModal = false;
        TipoObjeto tipoObjeto = new TipoObjeto();

        public DialogResult showModalNovo(ref object tipoObjeto)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                tipoObjeto = this.tipoObjeto;
            }
            return DialogResult;
        }
        public FrmTipoObjetoCadastro()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public FrmTipoObjetoCadastro(TipoObjeto tipoObjeto)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.tipoObjeto = tipoObjeto;
            get_TipoObjeto();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_TipoObjeto()
        {
            try
            {
                tipoObjeto = new TipoObjeto();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    tipoObjeto.Id = int.Parse(txtCodigo.Texts);
                else
                    tipoObjeto.Id = 0;
                tipoObjeto.Descricao = txtTipoObjeto.Texts;
              
                Controller.getInstance().salvar(tipoObjeto);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_TipoObjeto()
        {
            txtCodigo.Texts = tipoObjeto.Id.ToString();
            txtTipoObjeto.Texts = tipoObjeto.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_TipoObjeto();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmTipoObjetoCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro?"))
            {
                this.Close();
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
