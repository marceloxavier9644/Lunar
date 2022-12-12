using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos
{
    public partial class FrmUnidadeMedidaCadastro : Form
    {
        bool showModal = false;
        UnidadeMedida unidadeMedida = new UnidadeMedida();

        public DialogResult showModalNovo(ref object unidadeMedida)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                unidadeMedida = this.unidadeMedida;
            }
            return DialogResult;
        }

        public FrmUnidadeMedidaCadastro()
        {
            InitializeComponent();
        }

        public FrmUnidadeMedidaCadastro(UnidadeMedida unidadeMedida)
        {
            InitializeComponent();
            this.unidadeMedida = unidadeMedida;
            get_UnidadeMedida();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void set_UnidadeMedida()
        {
            try
            {
                unidadeMedida = new UnidadeMedida();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    unidadeMedida.Id = int.Parse(txtCodigo.Texts);
                else
                    unidadeMedida.Id = 0;
                unidadeMedida.Descricao = txtUnidadeMedida.Texts;
                unidadeMedida.Sigla = txtSigla.Texts;
                unidadeMedida.Empresa = Sessao.empresaFilialLogada.Empresa;
                Controller.getInstance().salvar(unidadeMedida);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch(Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_UnidadeMedida()
        {
            txtCodigo.Texts = unidadeMedida.Id.ToString();
            txtUnidadeMedida.Texts = unidadeMedida.Descricao;
            txtSigla.Texts = unidadeMedida.Sigla;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_UnidadeMedida();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmUnidadeMedidaCadastro_KeyDown(object sender, KeyEventArgs e)
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
            if(GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro?"))
            {
                this.Close();
            }
        }
    }
}
