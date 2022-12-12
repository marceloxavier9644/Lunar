using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmGrupoFiscalCadastro : Form
    {
        bool showModal = false;
        GrupoFiscal grupoFiscal = new GrupoFiscal();

        public DialogResult showModalNovo(ref object grupoFiscal)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                grupoFiscal = this.grupoFiscal;
            }
            return DialogResult;
        }
        public FrmGrupoFiscalCadastro()
        {
            InitializeComponent();
            txtGrupoFiscal.Select();
        }
        public FrmGrupoFiscalCadastro(GrupoFiscal grupoFiscal)
        {
            InitializeComponent();
            this.grupoFiscal = grupoFiscal;
            get_GrupoFiscal();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void set_GrupoFiscal()
        {
            try
            {
                grupoFiscal = new GrupoFiscal();
                if (!String.IsNullOrEmpty(txtCodigo.Texts))
                    grupoFiscal.Id = int.Parse(txtCodigo.Texts);
                else
                    grupoFiscal.Id = 0;
                grupoFiscal.Descricao = txtGrupoFiscal.Texts;
                grupoFiscal.CsosnSaida = txtCSOSN.Texts;
                grupoFiscal.AliquotaIcms = txtAliquotaICMS.Texts;
                grupoFiscal.CfopSaidaEstadual = txtCFOPEstadual.Texts;
                grupoFiscal.CfopSaidaInterestadual = txtCFOPInterestadual.Texts;
                grupoFiscal.Empresa = Sessao.empresaFilialLogada.Empresa;

                Controller.getInstance().salvar(grupoFiscal);
                GenericaDesktop.ShowInfo("Registrado com sucesso");
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_GrupoFiscal()
        {
            txtCodigo.Texts = grupoFiscal.Id.ToString();
            txtGrupoFiscal.Texts = grupoFiscal.Descricao;
            txtAliquotaICMS.Texts = grupoFiscal.AliquotaIcms;
            txtCFOPEstadual.Texts = grupoFiscal.CfopSaidaEstadual;
            txtCFOPInterestadual.Texts = grupoFiscal.CfopSaidaInterestadual;
            txtCSOSN.Texts = grupoFiscal.CsosnSaida;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_GrupoFiscal();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmGrupoFiscalCadastro_KeyDown(object sender, KeyEventArgs e)
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
