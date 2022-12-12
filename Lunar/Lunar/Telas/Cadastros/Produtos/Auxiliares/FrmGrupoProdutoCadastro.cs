using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmGrupoProdutoCadastro : Form
    {
        bool showModal = false;
        ProdutoGrupo grupo = new ProdutoGrupo();

        public DialogResult showModalNovo(ref object grupo)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                grupo = this.grupo;
            }
            return DialogResult;
        }
        public FrmGrupoProdutoCadastro()
        {
            InitializeComponent();
        }
        public FrmGrupoProdutoCadastro(ProdutoGrupo grupo)
        {
            InitializeComponent();
            this.grupo = grupo;
            get_Grupo();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Grupo()
        {
            try
            {
                grupo = new ProdutoGrupo();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    grupo.Id = int.Parse(txtCodigo.Texts);
                else
                    grupo.Id = 0;
                grupo.Descricao = txtGrupo.Texts;
                grupo.Empresa = Sessao.empresaFilialLogada.Empresa;

                Controller.getInstance().salvar(grupo);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Grupo()
        {
            txtCodigo.Texts = grupo.Id.ToString();
            txtGrupo.Texts = grupo.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Grupo();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmGrupoProdutoCadastro_KeyDown(object sender, KeyEventArgs e)
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
