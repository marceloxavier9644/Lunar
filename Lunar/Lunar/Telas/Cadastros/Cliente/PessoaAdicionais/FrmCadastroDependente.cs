using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCadastroDependente : Form
    {
        PessoaDependente pessoaDependente = new PessoaDependente();
        public DialogResult showModal(ref PessoaDependente pessoaDependente)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                pessoaDependente = this.pessoaDependente;
            }
            return DialogResult;
        }
        public FrmCadastroDependente()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtNome.Select();
        }

        public FrmCadastroDependente(PessoaDependente pessoaDependente)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtNome.Select();
            get_Dependente(pessoaDependente);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void get_Dependente(PessoaDependente pessoaDependente)
        {
            txtCodigo.Texts = pessoaDependente.Id.ToString();
            txtCPF.Texts = pessoaDependente.Cpf;
            txtDataNascimento.Text = pessoaDependente.DataNascimento.ToShortDateString();
            txtGrauParentesco.Texts = pessoaDependente.Parentesco;
            txtNome.Texts = pessoaDependente.Nome;
            txtObservacoes.Texts = pessoaDependente.Observacoes;
            txtTelefone.Texts = pessoaDependente.Telefone;
            txtTelefone.Focus();
            txtNome.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void set_PessoaDependente()
        {
            if (!String.IsNullOrEmpty(txtNome.Texts))
            {
                pessoaDependente = new PessoaDependente();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    pessoaDependente.Id = 0;
                }
                else
                {
                    pessoaDependente.Id = int.Parse(txtCodigo.Texts);
                    pessoaDependente = (PessoaDependente)PessoaDependenteController.getInstance().selecionar(pessoaDependente);
                }

                pessoaDependente.Nome = txtNome.Texts;
                pessoaDependente.Observacoes = txtObservacoes.Texts;
                pessoaDependente.Telefone = txtTelefone.Texts;
                pessoaDependente.Cpf = txtCPF.Texts;
                pessoaDependente.DataNascimento = DateTime.Parse(txtDataNascimento.Value.ToString());
                pessoaDependente.Parentesco = txtGrauParentesco.Texts;
                if(pessoaDependente.Pessoa != null)
                {
                    PessoaDependenteController.getInstance().salvar(pessoaDependente);
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("O campo nome é obrigatório!");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_PessoaDependente();
        }

        private void FrmCadastroDependente_KeyDown(object sender, KeyEventArgs e)
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
