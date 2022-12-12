using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmSetorCadastro : Form
    {
        bool showModal = false;
        ProdutoSetor setor = new ProdutoSetor();

        public DialogResult showModalNovo(ref object setor)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                setor = this.setor;
            }
            return DialogResult;
        }
        public FrmSetorCadastro()
        {
            InitializeComponent();
        }
        public FrmSetorCadastro(ProdutoSetor setor)
        {
            InitializeComponent();
            InitializeComponent();
            this.setor = setor;
            get_Setor();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Setor()
        {
            try
            {
                setor = new ProdutoSetor();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    setor.Id = int.Parse(txtCodigo.Texts);
                else
                    setor.Id = 0;
                setor.Descricao = txtSetor.Texts;
                setor.EmpresaFilial = Sessao.empresaFilialLogada;

                Controller.getInstance().salvar(setor);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Setor()
        {
            txtCodigo.Texts = setor.Id.ToString();
            txtSetor.Texts = setor.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Setor();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmSetorCadastro_KeyDown(object sender, KeyEventArgs e)
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
