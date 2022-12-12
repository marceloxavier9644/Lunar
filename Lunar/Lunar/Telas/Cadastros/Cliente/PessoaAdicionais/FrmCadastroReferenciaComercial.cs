using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCadastroReferenciaComercial : Form
    {
        PessoaReferenciaComercial pessoaReferenciaComercial = new PessoaReferenciaComercial();
        public DialogResult showModal(ref PessoaReferenciaComercial pessoaReferenciaComercial)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                pessoaReferenciaComercial = this.pessoaReferenciaComercial;
            }
            return DialogResult;
        }
        public FrmCadastroReferenciaComercial()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtEmpresa.Select();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_referenciaComercial()
        {
            if (!String.IsNullOrEmpty(txtEmpresa.Texts))
            {
                pessoaReferenciaComercial = new PessoaReferenciaComercial();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    pessoaReferenciaComercial.Id = 0;
                }
                else
                    pessoaReferenciaComercial.Id = int.Parse(txtCodigo.Texts);

                pessoaReferenciaComercial.Empresa = txtEmpresa.Texts;
                pessoaReferenciaComercial.Observacoes = txtObservacoes.Texts;
                pessoaReferenciaComercial.Telefone = txtTelefone.Texts;
                Sessao.pessoaReferenciaComercial = pessoaReferenciaComercial;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("O campo empresa é obrigatório!");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_referenciaComercial();
        }

        private void FrmCadastroReferenciaComercial_KeyDown(object sender, KeyEventArgs e)
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
