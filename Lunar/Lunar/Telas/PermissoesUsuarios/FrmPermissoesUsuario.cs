using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.PermissoesUsuarios
{
    public partial class FrmPermissoesUsuario : Form
    {
        PermissaoUsuario permissao = new PermissaoUsuario();
        GrupoUsuario grupoUsuario = new GrupoUsuario();
        public FrmPermissoesUsuario()
        {
            InitializeComponent();
        }

        private void btnPesquisaGrupo_Click(object sender, EventArgs e)
        {
            tabControlAdv1.Enabled = false;
            btnSalvar.Enabled = false;
            this.grupoUsuario = new GrupoUsuario();
            Object grupo = new GrupoUsuario();
            txtGrupo.Text = "";
            txtCodGrupo.Text = "";
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
                                txtGrupo.Text = ((GrupoUsuario)grupo).Descricao;
                                txtCodGrupo.Text = ((GrupoUsuario)grupo).Id.ToString();
                                grupoUsuario = ((GrupoUsuario)grupo);
                                tabControlAdv1.Enabled = true;
                                btnSalvar.Enabled = true;
                                if (!String.IsNullOrEmpty(grupoUsuario.Permissoes))
                                    get_PermissoesPorGrupo(grupoUsuario);
                                break;
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Text = ((GrupoUsuario)grupo).Descricao;
                            txtCodGrupo.Text = ((GrupoUsuario)grupo).Id.ToString();
                            grupoUsuario = ((GrupoUsuario)grupo);
                            tabControlAdv1.Enabled = true;
                            btnSalvar.Enabled = true;
                            if(!String.IsNullOrEmpty(grupoUsuario.Permissoes))
                                get_PermissoesPorGrupo(grupoUsuario);
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

        private void btnMarcarTodosTab1_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[0];
            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = true;
                }
            }
        }

        private void btnDesmarcarTodosTab1_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[0];
            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }
            }
        }


        private void set_permissoes()
        {
            permissao = new PermissaoUsuario();
            grupoUsuario = new GrupoUsuario();
            grupoUsuario.Id = int.Parse(txtCodGrupo.Text);
            grupoUsuario = (GrupoUsuario)Controller.getInstance().selecionar(grupoUsuario);
            capturarCheckAbaCadastrosClientesProdutos();
            capturarCheckAbaOrdemServico();
            capturarCheckAbaVendas();
            capturarCheckAbaUtilitarios();

            //Salvo permissoes apenas para identificar o que tinha e o que foi alterado e por quem... 
            //e posteriormente armazeno as permissoes no grupoUsuario para realmente validar nas telas
            permissao.GrupoUsuario = grupoUsuario;
            permissao.EmpresaFilial = Sessao.empresaFilialLogada;
            permissao.Permissoes = GenericaDesktop.Criptografa(permissao.Permissoes);
            Controller.getInstance().salvar(permissao);

            //Aqui ja esta criptografado
            grupoUsuario.Permissoes = permissao.Permissoes;
            Controller.getInstance().salvar(grupoUsuario);

            if (txtCodGrupo.Text.Equals(Sessao.usuarioLogado.GrupoUsuario.Id))
            {
                Sessao.usuarioLogado.GrupoUsuario.Permissoes = grupoUsuario.Permissoes;
                if (!String.IsNullOrEmpty(LunarBase.Utilidades.Sessao.usuarioLogado.GrupoUsuario.Permissoes))
                {
                    LunarBase.Utilidades.Sessao.usuarioLogado.GrupoUsuario.Permissoes = GenericaDesktop.Descriptografa(Sessao.usuarioLogado.GrupoUsuario.Permissoes);
                    string prm = GenericaDesktop.Descriptografa(Sessao.usuarioLogado.GrupoUsuario.Permissoes);
                    String[] permissions = prm.Split(';');
                    Sessao.permissoes = new HashSet<string>(permissions);
                }
            }

            GenericaDesktop.ShowInfo("Registro salvo com Sucesso, caso necessário feche o sistema e abra novamente para validar as funções!!");
        }

        //Cadastros de Clientes e Produtos
        private void capturarCheckAbaCadastrosClientesProdutos()
        {
            for (int i = 1; i <= 17; i++)
            {
                string checkBoxName = "chk" + i;

                // Use o método Controls.Find para encontrar o CheckBox com o nome específico.
                CheckBox checkBox = tabControlAdv1.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        permissao.Permissoes += ";" + i;
                    }
                }
            }
        }

        //Ordens de Serviço
        private void capturarCheckAbaOrdemServico()
        {
            for (int i = 30; i <= 45; i++)
            {
                string checkBoxName = "chk" + i;

                // Use o método Controls.Find para encontrar o CheckBox com o nome específico.
                CheckBox checkBox = tabControlAdv1.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        permissao.Permissoes += ";" + i;
                    }
                }
            }
        }

        //Vendas
        private void capturarCheckAbaVendas()
        {
            for (int i = 60; i <= 72; i++)
            {
                string checkBoxName = "chk" + i;

                // Use o método Controls.Find para encontrar o CheckBox com o nome específico.
                CheckBox checkBox = tabControlAdv1.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        permissao.Permissoes += ";" + i;
                    }
                }
            }
        }

        //Utilitários
        private void capturarCheckAbaUtilitarios()
        {
            for (int i = 100; i <= 108; i++)
            {
                string checkBoxName = "chk" + i;

                // Use o método Controls.Find para encontrar o CheckBox com o nome específico.
                CheckBox checkBox = tabControlAdv1.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        permissao.Permissoes += ";" + i;
                    }
                }
            }
        }

        private void FrmPermissoesUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_permissoes();
        }

        private void get_PermissoesPorGrupo(GrupoUsuario grupo)
        {
            string prm = GenericaDesktop.Descriptografa(grupo.Permissoes);
            string[] permissions = prm.Split(';');
            HashSet<string> permissoesGrupo = new HashSet<string>(permissions);

            MarcarCheckboxesComBaseEmPermissoes(permissoesGrupo, tabControlAdv1);
        }

        private void MarcarCheckboxesComBaseEmPermissoes(HashSet<string> permissoes, TabControlAdv tabControl)
        {
            foreach (Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage in tabControl.TabPages)
            {
                foreach (string permissao in permissoes)
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

        private void btnMarcarTodosTabOrdemServico_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[1];
            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = true;
                }
            }
        }

        private void btnDesmarcarTodosTabOrdemServico_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[1];

            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }
            }
        }

        private void btnMarcarTodosTabVendas_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[2];
            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = true;
                }
            }
        }

        private void btnDesmarcarTodosTabVendas_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[2];

            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }
            }
        }

        private void btnMarcarTodosTabUtilitarios_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[3];
            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = true;
                }
            }
        }

        private void btnDesmarcarTodosTabUtilitarios_Click(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage = tabControlAdv1.TabPages[3];

            foreach (Control control in tabPage.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }
            }
        }
    }
}
