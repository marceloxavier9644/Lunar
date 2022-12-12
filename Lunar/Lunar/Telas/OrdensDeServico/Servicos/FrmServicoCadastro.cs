using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico.Servicos
{
    public partial class FrmServicoCadastro : Form
    {
        bool showModal = false;
        Servico servico = new Servico();

        public DialogResult showModalNovo(ref object servico)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                servico = this.servico;
            }
            return DialogResult;
        }
        public FrmServicoCadastro()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public FrmServicoCadastro(Servico servico)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.servico = servico;
            get_Servico();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Servico()
        {
            try
            {
                servico = new Servico();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    servico.Id = int.Parse(txtCodigo.Texts);
                else
                    servico.Id = 0;
                servico.Descricao = txtDescricao.Texts;
                servico.Filial = Sessao.empresaFilialLogada;
                if (!String.IsNullOrEmpty(txtValor.Texts))
                    servico.Valor = decimal.Parse(txtValor.Texts);
                else
                    servico.Valor = 0;

                Controller.getInstance().salvar(servico);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
                txtDescricao.Texts = "";
                txtCodigo.Texts = "";
                txtValor.Texts = "";
                servico = new Servico();
                txtDescricao.Focus();
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Servico()
        {
            txtCodigo.Texts = servico.Id.ToString();
            txtDescricao.Texts = servico.Descricao;
            txtValor.Texts = string.Format("{0:0.00}",servico.Valor);
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValor.Texts = string.Format("{0:0.00}", decimal.Parse(txtValor.Texts));
            }
            catch
            {
           
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Servico();
            //if (showModal)
            //{
            //    DialogResult = DialogResult.OK;
            //}
        }

        private void FrmServicoCadastro_KeyDown(object sender, KeyEventArgs e)
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

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                btnSalvar.PerformClick();
            }
        }

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtValor.Focus();
            }
        }
    }
}
