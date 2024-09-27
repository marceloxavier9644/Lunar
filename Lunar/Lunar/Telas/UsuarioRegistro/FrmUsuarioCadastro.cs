using Lunar.Telas.Cadastros.Empresas;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.UsuarioRegistro
{
    public partial class FrmUsuarioCadastro : Form
    {
        string permissoesMarcadas = string.Empty;
        Usuario usuario = new Usuario();
        UsuarioController usuarioController = new UsuarioController();
        EmpresaFilial empresaFilial = new EmpresaFilial();
        GrupoUsuario grupoUsuario = new GrupoUsuario();
        bool showModal = false;
        public DialogResult showModalNovo(ref object usuario)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                usuario = this.usuario;
            }
            return DialogResult;
        }
        public FrmUsuarioCadastro()
        {
            InitializeComponent();
            txtEmpresa.Texts = Sessao.empresaFilialLogada.NomeFantasia;
            txtCodEmpresa.Texts = Sessao.empresaFilialLogada.Id.ToString();
            empresaFilial = Sessao.empresaFilialLogada;
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public FrmUsuarioCadastro(Usuario usuario)
        {
            InitializeComponent();

            this.usuario = usuario;
            get_Usuario(usuario);
            this.FormBorderStyle = FormBorderStyle.None;
            txtLogin.Focus();
            txtLogin.Select();
            lblAutomatico.Visible = false;
        }
        public FrmUsuarioCadastro(GrupoUsuario grupoUsuario)
        {
            InitializeComponent();
            txtEmpresa.Texts = Sessao.empresaFilialLogada.NomeFantasia;
            txtCodEmpresa.Texts = Sessao.empresaFilialLogada.Id.ToString();
            empresaFilial = Sessao.empresaFilialLogada;
            txtCodGrupo.Texts = grupoUsuario.Id.ToString();
            txtGrupo.Texts = grupoUsuario.Descricao;
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void get_Usuario(Usuario usuario)
        {
            txtID.Texts = usuario.Id.ToString();
            txtLogin.Texts = usuario.Login;
            txtSenha.Texts = GenericaDesktop.Descriptografa(usuario.Senha);
            txtEmail.Texts = usuario.Email;
            txtDDD.Texts = usuario.Ddd;
            txtTelefonePrincipal.Texts = usuario.Fone;
            if (usuario.Notificacoes == true)
                chkReceberNotificacoes.Checked = true;
            else
                chkReceberNotificacoes.Checked = false;
            if(usuario.GrupoUsuario != null)
            {
                txtGrupo.Texts = usuario.GrupoUsuario.Descricao;
                txtCodGrupo.Texts = usuario.GrupoUsuario.Id.ToString();
                grupoUsuario = usuario.GrupoUsuario;
            }
            if (usuario.EmpresaFilial != null)
            {
                txtEmpresa.Texts = usuario.EmpresaFilial.NomeFantasia;
                txtCodEmpresa.Texts = usuario.EmpresaFilial.Id.ToString();
                empresaFilial = usuario.EmpresaFilial;
            }

            if (!String.IsNullOrEmpty(usuario.NotificacoesSelecionadas))
            {
                string[] permissions = usuario.NotificacoesSelecionadas.Split(';');
                HashSet<string> notify = new HashSet<string>(permissions);

                foreach (Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage in tabControlAdv1.TabPages)
                {
                    foreach (string permissao in notify)
                    {
                        string nomeCheckbox = "chk" + permissao;

                        Control[] controles = tabPage.Controls.Find(nomeCheckbox, true);

                        if (controles.Length > 0 && controles[0] is CheckBox checkBox)
                        {
                            checkBox.Checked = true;
                        }
                    }
                }
            }
            tabControlAdv1.SelectedTab = tabPageAdv1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void set_Usuario()
        {
            usuario = new Usuario();
            if (!String.IsNullOrEmpty(txtID.Texts))
            {
                usuario.Id = int.Parse(txtID.Texts);
            }
            else
            {
                usuario.Id = 0;
            }
            usuario.Login = txtLogin.Texts.Trim();
            usuario.Senha = GenericaDesktop.Criptografa(txtSenha.Texts.Trim());
            usuario.Email = txtEmail.Texts.Trim();
            usuario.Ddd = txtDDD.Texts.Trim();
            if (!String.IsNullOrEmpty(txtTelefonePrincipal.Texts.Trim()))
                usuario.Fone = GenericaDesktop.RemoveCaracteres(txtTelefonePrincipal.Texts.Trim());
            else
                usuario.Fone = "";
            if (chkReceberNotificacoes.Checked == true)
                usuario.Notificacoes = true;
            else
                usuario.Notificacoes = false;
            if (!String.IsNullOrEmpty(txtCodGrupo.Texts))
            {
                grupoUsuario.Id = int.Parse(txtCodGrupo.Texts);
                usuario.GrupoUsuario = (GrupoUsuario)Controller.getInstance().selecionar(grupoUsuario);
            }
            if (!String.IsNullOrEmpty(txtCodEmpresa.Texts))
            {
                empresaFilial.Id = int.Parse(txtCodEmpresa.Texts);
                usuario.EmpresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
            }

            for (int i = 1; i <= 6; i++)
            {
                string checkBoxName = "chk" + i;
                CheckBox checkBox = tabControlAdv1.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        usuario.NotificacoesSelecionadas += ";" + i;
                    }
                }
            }
        }

        private void btnPesquisaEmpresa_Click(object sender, EventArgs e)
        {
            this.empresaFilial = new EmpresaFilial();
            Object empresa = new EmpresaFilial();
            txtEmpresa.Texts = "";
            txtCodEmpresa.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("EmpresaFilial", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("EmpresaFilial", "", ref empresa))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmCadastroEmpresas form = new FrmCadastroEmpresas();
                            if (form.showModalNovo(ref empresa) == DialogResult.OK)
                            {
                                txtEmpresa.Texts = ((EmpresaFilial)empresa).NomeFantasia;
                                txtCodEmpresa.Texts = ((EmpresaFilial)empresa).Id.ToString();
                                empresaFilial = ((EmpresaFilial)empresa);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtEmpresa.Texts = ((EmpresaFilial)empresa).NomeFantasia;
                            txtCodEmpresa.Texts = ((EmpresaFilial)empresa).Id.ToString();
                            empresaFilial = ((EmpresaFilial)empresa);
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodEmpresa.Texts) && !String.IsNullOrEmpty(txtCodGrupo.Texts) && !String.IsNullOrEmpty(txtLogin.Texts))
            {
                try
                {
                    set_Usuario();
                    usuarioController.salvar(usuario);
                    GenericaDesktop.ShowInfo("Registro salvo com sucesso");
                    this.Close();
                }
                catch (Exception ex)
                {
                    GenericaDesktop.ShowErro("Falha ao gravar usuário: " + ex.Message);
                }
            }
            else
                GenericaDesktop.ShowAlerta("Todos os campos devem ser preenchidos");
        }

        private void FrmUsuarioCadastro_KeyDown(object sender, KeyEventArgs e)
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
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnPesquisaGrupo_Click(object sender, EventArgs e)
        {
            this.grupoUsuario = new GrupoUsuario();
            Object grupo = new GrupoUsuario();
            txtGrupo.Texts = "";
            txtCodGrupo.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("GrupoUsuario", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("GrupoUsuario", "", ref grupo))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmGrupoUsuarioCadastro form = new FrmGrupoUsuarioCadastro();
                            if (form.showModalNovo(ref grupo) == DialogResult.OK)
                            {
                                txtGrupo.Texts = ((GrupoUsuario)grupo).Descricao;
                                txtCodGrupo.Texts = ((GrupoUsuario)grupo).Id.ToString();
                                grupoUsuario = ((GrupoUsuario)grupo);
                                txtEmpresa.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((GrupoUsuario)grupo).Descricao;
                            txtCodGrupo.Texts = ((GrupoUsuario)grupo).Id.ToString();
                            grupoUsuario = ((GrupoUsuario)grupo);
                            txtEmpresa.Focus();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.20;
        }

        private void FrmUsuarioCadastro_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            timer1.Start();
        }

        private void txtTelefonePrincipal_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTelefonePrincipal.Texts))
            {
                string tel = txtTelefonePrincipal.Texts;
                txtTelefonePrincipal.Texts = GenericaDesktop.formatarFone(tel);
            }
        }

        private void chkReceberNotificacoes_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkReceberNotificacoes.Checked == true)
                {
                    tabPageAdv2.TabVisible = true;
                    tabControlAdv1.SelectedTab = tabPageAdv2;
                }
                else
                {
                    tabPageAdv2.TabVisible = false;
                    tabControlAdv1.SelectedTab = tabPageAdv1;
                }
            }
            catch
            {

            }
        }
    }
}
