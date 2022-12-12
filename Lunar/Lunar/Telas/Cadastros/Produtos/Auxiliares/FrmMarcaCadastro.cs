using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmMarcaCadastro : Form
    {
        bool showModal = false;
        Marca marca = new Marca();

        public DialogResult showModalNovo(ref object marca)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                marca = this.marca;
            }
            return DialogResult;
        }
        public FrmMarcaCadastro()
        {
            InitializeComponent();
        }
        public FrmMarcaCadastro(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            get_Marca();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Marca()
        {
            try
            {
                marca = new Marca();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    marca.Id = int.Parse(txtCodigo.Texts);
                else
                    marca.Id = 0;
                marca.Descricao = txtMarca.Texts;
                marca.Empresa = Sessao.empresaFilialLogada.Empresa;
               
                Controller.getInstance().salvar(marca);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_Marca()
        {
            txtCodigo.Texts = marca.Id.ToString();
            txtMarca.Texts = marca.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Marca();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmMarcaCadastro_KeyDown(object sender, KeyEventArgs e)
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
