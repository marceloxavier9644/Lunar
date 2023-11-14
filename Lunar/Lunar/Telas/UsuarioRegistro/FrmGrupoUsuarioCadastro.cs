using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.UsuarioRegistro
{
    public partial class FrmGrupoUsuarioCadastro : Form
    {
        GrupoUsuario grupoUsuario = new GrupoUsuario();
        bool showModal = false;
        public DialogResult showModalNovo(ref object grupoUsuario)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                grupoUsuario = this.grupoUsuario;
            }
            return DialogResult;
        }
        public FrmGrupoUsuarioCadastro()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            grupoUsuario = new GrupoUsuario();
            if (!String.IsNullOrEmpty(txtCodGrupo.Texts))
                grupoUsuario.Id = int.Parse(txtCodGrupo.Texts);
            else
                grupoUsuario.Id = 0;
            grupoUsuario.Descricao = txtGrupo.Texts;
            grupoUsuario.Empresa = Sessao.empresaFilialLogada.Empresa;
            if(string.IsNullOrEmpty(txtCodGrupo.Texts))
                grupoUsuario.Permissoes = "";
            grupoUsuario.Supervisor = false;
            Controller.getInstance().salvar(grupoUsuario);
            txtCodGrupo.Texts = grupoUsuario.Id.ToString();
            GenericaDesktop.ShowInfo("Registro salvo com Sucesso!");
            if (showModal)
            {
                DialogResult = DialogResult.OK;
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
                            //FrmGrupoUsuarioCadastro form = new FrmGrupoUsuarioCadastro();
                            //if (form.showModalNovo(ref grupo) == DialogResult.OK)
                            //{
                            //    txtGrupo.Texts = ((GrupoUsuario)grupo).Descricao;
                            //    txtCodGrupo.Texts = ((GrupoUsuario)grupo).Id.ToString();
                            //    grupoUsuario = ((GrupoUsuario)grupo);
                            //    txtEmpresa.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((GrupoUsuario)grupo).Descricao;
                            txtCodGrupo.Texts = ((GrupoUsuario)grupo).Id.ToString();
                            grupoUsuario = ((GrupoUsuario)grupo);
                            
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmGrupoUsuarioCadastro_Load(object sender, EventArgs e)
        {

        }
    }
}
