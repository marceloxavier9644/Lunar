using Lunar.Telas.PesquisaPadrao;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            break;
                        case DialogResult.OK:
                            txtGrupo.Text = ((GrupoUsuario)grupo).Descricao;
                            txtCodGrupo.Text = ((GrupoUsuario)grupo).Id.ToString();
                            grupoUsuario = ((GrupoUsuario)grupo);
                            tabControlAdv1.Enabled = true;
                            btnSalvar.Enabled = true;
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
            foreach (Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage in tabControlAdv1.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)control;
                        checkBox.Checked = true;
                    }
                }
            }
        }

        private void btnDesmarcarTodosTab1_Click(object sender, EventArgs e)
        {
            foreach (Syncfusion.Windows.Forms.Tools.TabPageAdv tabPage in tabControlAdv1.TabPages)
            {
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


        private void set_permissoes()
        {
            permissao = new PermissaoUsuario();
            grupoUsuario = new GrupoUsuario();
            grupoUsuario.Id = int.Parse(txtCodGrupo.Text);
            grupoUsuario = (GrupoUsuario)Controller.getInstance().selecionar(grupoUsuario);
            capturarCheckAbaCadastrosClientesProdutos();


            //Salvo permissoes apenas para identificar o que tinha e o que foi alterado e por quem... 
            //e posteriormente armazeno as permissoes no grupoUsuario para realmente validar nas telas
            Controller.getInstance().salvar(permissao);
            grupoUsuario.Permissoes = permissao.Permissoes;
            Controller.getInstance().salvar(grupoUsuario);
        }

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
        private void FrmPermissoesUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_permissoes();
        }
    }
}
