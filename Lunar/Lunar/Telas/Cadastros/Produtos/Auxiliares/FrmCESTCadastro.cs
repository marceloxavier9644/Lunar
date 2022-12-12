using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmCESTCadastro : Form
    {
        bool showModal = false;
        Cest cest = new Cest();
        GenericaDesktop generica = new GenericaDesktop();
        public DialogResult showModalNovo(ref object cest)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                cest = this.cest;
            }
            return DialogResult;
        }
        public FrmCESTCadastro()
        {
            InitializeComponent();
        }
        public FrmCESTCadastro(Cest cest)
        {
            InitializeComponent();
            this.cest = cest;
            get_Cest();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void set_Cest()
        {
            try
            {
                cest = new Cest();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    cest.Id = int.Parse(txtCodigo.Texts);
                else
                    cest.Id = 0;
                cest.NumeroCest = txtCEST.Texts.Trim();
                cest.DescricaoCest = txtDescricao.Texts.Trim();
                cest.Segmento = "";
                cest.Item = "";
                cest.Ncm = txtNCM.Texts;
                Controller.getInstance().salvar(cest);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Cest()
        {
            txtCodigo.Texts = cest.Id.ToString();
            txtCEST.Texts = cest.NumeroCest;
            txtDescricao.Texts = cest.DescricaoCest;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Cest();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void txtCEST_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCEST.Texts, e);
        }

        private void FrmCESTCadastro_KeyDown(object sender, KeyEventArgs e)
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

        private void txtNCM_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtNCM.Texts, e);
        }
    }
}
