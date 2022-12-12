using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCadastroTelefone : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        PessoaTelefone pessoaTelefone = new PessoaTelefone();
        public DialogResult showModal(ref PessoaTelefone pessoaTelefone)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                pessoaTelefone = this.pessoaTelefone;
            }
            return DialogResult;
        }
        public FrmCadastroTelefone()
        {
            InitializeComponent();
            txtDDD.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void set_Telefone()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtTelefone.Texts)) 
                { 
                    pessoaTelefone = new PessoaTelefone();
                    if (String.IsNullOrEmpty(txtCodigo.Texts))
                    {
                        pessoaTelefone.Id = 0;
                    }
                    else
                        pessoaTelefone.Id = int.Parse(txtCodigo.Texts);

                    pessoaTelefone.Ddd = txtDDD.Texts.Trim();
                    pessoaTelefone.Telefone = txtTelefone.Texts;
                    pessoaTelefone.Observacoes = txtObservacoes.Texts;
                    Sessao.pessoaTelefone = pessoaTelefone;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    GenericaDesktop.ShowErro(" O campo Telefone é obrigatório!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Telefone();
        }

        private void txtTelefone_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTelefone.Texts))
                txtTelefone.Texts = GenericaDesktop.MascaraTelefone(txtTelefone.Texts);
        }

        private void FrmCadastroTelefone_KeyDown(object sender, KeyEventArgs e)
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
    }
}
