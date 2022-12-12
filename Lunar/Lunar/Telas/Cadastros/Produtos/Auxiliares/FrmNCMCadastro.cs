using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmNCMCadastro : Form
    {
        bool showModal = false;
        Ncm ncm = new Ncm();
        GenericaDesktop generica = new GenericaDesktop();
        public DialogResult showModalNovo(ref object ncm)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                ncm = this.ncm;
            }
            return DialogResult;
        }

        public DialogResult showModalNCM(String ncm)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
             
            }
            return DialogResult;
        }
        public FrmNCMCadastro()
        {
            InitializeComponent();
        }
        public FrmNCMCadastro(Ncm ncm)
        {
            InitializeComponent();
            this.ncm = ncm;
            get_NCM();
            if (txtCodigo.Texts.Equals("0"))
            {
                txtCodigo.Texts = "";
                txtDescricao.Select();
            }

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void set_NCM()
        {
            try
            {
                ncm = new Ncm();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    ncm.Id = int.Parse(txtCodigo.Texts);
                else
                    ncm.Id = 0;
                ncm.NumeroNcm = txtNCM.Texts.Trim();
                ncm.DescricaoNcm = txtDescricao.Texts.Trim();
                Controller.getInstance().salvar(ncm);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_NCM()
        {
            txtCodigo.Texts = ncm.Id.ToString();
            txtNCM.Texts = ncm.NumeroNcm;
            txtDescricao.Texts = ncm.DescricaoNcm;
        }

        private void txtNCM_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtNCM.Texts, e);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_NCM();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmNCMCadastro_KeyDown(object sender, KeyEventArgs e)
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
    }
}
