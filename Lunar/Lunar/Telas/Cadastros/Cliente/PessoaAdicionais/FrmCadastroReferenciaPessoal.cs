using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCadastroReferenciaPessoal : Form
    {
        PessoaReferenciaPessoal pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
        public DialogResult showModal(ref PessoaReferenciaPessoal pessoaReferenciaPessoal)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                pessoaReferenciaPessoal = this.pessoaReferenciaPessoal;
            }
            return DialogResult;
        }
        public FrmCadastroReferenciaPessoal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtNome.Select();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_referenciaPessoal()
        {
            if (!String.IsNullOrEmpty(txtNome.Texts) && !String.IsNullOrEmpty(txtTelefone.Texts))
            {
                pessoaReferenciaPessoal = new PessoaReferenciaPessoal();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    pessoaReferenciaPessoal.Id = 0;
                }
                else
                    pessoaReferenciaPessoal.Id = int.Parse(txtCodigo.Texts);

                pessoaReferenciaPessoal.Nome = txtNome.Texts;
                pessoaReferenciaPessoal.Grau = txtProximidade.Texts;
                pessoaReferenciaPessoal.Observacoes = txtObservacoes.Texts;
                pessoaReferenciaPessoal.Telefone = txtTelefone.Texts;
                Sessao.pessoaReferenciaPessoal = pessoaReferenciaPessoal;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("Os campos nome e telefone são obrigatórios!");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
           set_referenciaPessoal(); 
        }

        private void FrmCadastroReferenciaPessoal_KeyDown(object sender, KeyEventArgs e)
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

        private void txtTelefone_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTelefone.Texts))
                txtTelefone.Texts = GenericaDesktop.MascaraTelefone(txtTelefone.Texts);
        }
    }
}
