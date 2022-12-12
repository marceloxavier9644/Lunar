using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos.Auxiliares
{
    public partial class FrmSubGrupoCadastro : Form
    {
        bool showModal = false;
        ProdutoGrupo grupo = new ProdutoGrupo();
        ProdutoSubGrupo subGrupo = new ProdutoSubGrupo();

        public DialogResult showModalNovo(ref object subGrupo)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                subGrupo = this.subGrupo;
            }
            return DialogResult;
        }
        public FrmSubGrupoCadastro()
        {
            InitializeComponent();
            if (Sessao.grupoSelecionadoCadastroProduto != null)
            {
                txtGrupo.Texts = Sessao.grupoSelecionadoCadastroProduto.Descricao;
                txtcodGrupo.Texts = Sessao.grupoSelecionadoCadastroProduto.Id.ToString();
                grupo = Sessao.grupoSelecionadoCadastroProduto;
                txtSubGrupo.Focus();
                txtSubGrupo.Select();
            }
            if (txtcodGrupo.Texts.Equals("0"))
                txtcodGrupo.Texts = "";
        }
        public FrmSubGrupoCadastro(ProdutoSubGrupo subGrupo)
        {
            InitializeComponent();
            this.subGrupo = subGrupo;
            get_SubGrupo();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_SubGrupo()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtcodGrupo.Texts))
                {
                    subGrupo = new ProdutoSubGrupo();
                    if (!String.IsNullOrEmpty(txtCodigo.Texts))
                        subGrupo.Id = int.Parse(txtCodigo.Texts);
                    else
                        subGrupo.Id = 0;
                    subGrupo.Descricao = txtSubGrupo.Texts;
                    ProdutoGrupo grupo = new ProdutoGrupo();
                    grupo.Id = int.Parse(txtcodGrupo.Texts);
                    subGrupo.ProdutoGrupo = (ProdutoGrupo)Controller.getInstance().selecionar(grupo);
                    subGrupo.Empresa = Sessao.empresaFilialLogada.Empresa;
                    Controller.getInstance().salvar(subGrupo);
                    GenericaDesktop.ShowInfo("Registrado com sucesso");
                }
                else
                {
                    GenericaDesktop.ShowErro("O Grupo é obrigatório!");
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void get_SubGrupo()
        {
            txtCodigo.Texts = subGrupo.Id.ToString();
            txtSubGrupo.Texts = subGrupo.Descricao;
            txtcodGrupo.Texts = subGrupo.ProdutoGrupo.Id.ToString();
            txtGrupo.Texts = subGrupo.ProdutoGrupo.Descricao;
        }

        private void FrmSubGrupoCadastro_KeyDown(object sender, KeyEventArgs e)
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_SubGrupo();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro?"))
            {
                this.Close();
            }
        }

        private void btnPesquisaGrupo_Click(object sender, EventArgs e)
        {
            Object grupoObjeto = new ProdutoGrupo();
            txtGrupo.Texts = "";
            txtcodGrupo.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ProdutoGrupo", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    //formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    uu.Owner = formBackground;
                    switch (uu.showModal("ProdutoGrupo", "", ref grupoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmGrupoProdutoCadastro form = new FrmGrupoProdutoCadastro();
                            if (form.showModalNovo(ref grupoObjeto) == DialogResult.OK)
                            {
                                txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                                txtcodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                                grupo = ((ProdutoGrupo)grupoObjeto);
                                txtSubGrupo.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                            txtcodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                            grupo = ((ProdutoGrupo)grupoObjeto);
                            txtSubGrupo.Focus();
                            break;
                    }

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }
    }
}
