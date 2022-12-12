using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Login
{
    public partial class FrmRecuperarSenha : Form
    {
        public FrmRecuperarSenha()
        {
            InitializeComponent();
            lblMessage.Text = "";
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            GenericaDesktop generica = new GenericaDesktop();
            UsuarioController usuarioController = new UsuarioController();
            if (string.IsNullOrWhiteSpace(txtUser.Text) == false)
            {
                Usuario usuario = new Usuario();
                if (!String.IsNullOrEmpty(txtAdmin.Text) && !String.IsNullOrEmpty(txtSenha.Text))
                {
                    usuario.Login = txtAdmin.Text;
                    usuario.Senha = GenericaDesktop.Criptografa(txtSenha.Text);
                    try { usuario = usuarioController.selecionarUsuarioPorUsuarioESenha(usuario); } catch { GenericaDesktop.ShowErro("Administrador não encontrado"); }
                }
                if (usuario.Id > 0 && usuario.GrupoUsuario.Supervisor == true)
                {
                    IList<Usuario> listaUser = usuarioController.selecionarUsuarioComVariosFiltros(txtUser.Text);
                    if (listaUser.Count == 1)
                    {
                        foreach (Usuario user in listaUser)
                        {
                            user.Senha = "";
                            Controller.getInstance().salvar(user);
                            GenericaDesktop.ShowInfo("Atenção, o usuario " + txtUser.Text + " teve sua senha resetada, está totalmente em branco! Acesse o sistema com seu usuário e deixe a senha em branco, logo após logar o sistema irá solicitar uma nova senha!");
                        }
                    }
                    else if(listaUser.Count > 1)
                    {
                        lblMessage.Text = "Como este nome foi encontrado mais de um usuário, digite o login ou email exato!";
                        lblMessage.ForeColor = Color.IndianRed;
                    }
                    else
                    {
                        lblMessage.Text = "Usuário digitado não encontrado ou administrador sem permissão";
                        lblMessage.ForeColor = Color.IndianRed;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Por favor, digite seu nome de usuário ou e-mail";
                lblMessage.ForeColor = Color.IndianRed;
            }
        }
    }
}
