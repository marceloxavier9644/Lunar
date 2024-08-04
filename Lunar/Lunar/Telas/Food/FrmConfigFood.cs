using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class FrmConfigFood : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        public FrmConfigFood()
        {
            InitializeComponent();
            retornarConfigPadrao();
        }

        private void retornarConfigPadrao()
        {
            try
            {
                AtendimentoConfig atendimentoConfig = new AtendimentoConfig();
                atendimentoConfig.Id = 1;

                var resultado = Controller.getInstance().selecionar(atendimentoConfig);
                if (resultado != null && resultado is AtendimentoConfig)
                {
                    Sessao.atendimentoConfig = (AtendimentoConfig)resultado;
                    txtIpServidor.Text = Sessao.atendimentoConfig.IpServidor;
                    txtPortaServidor.Text = Sessao.atendimentoConfig.PortaApi;
                    txtMesaInicial.Text = Sessao.atendimentoConfig.MesaInicial.ToString();
                    txtMesaFinal.Text = Sessao.atendimentoConfig.MesaFinal.ToString();
                }
                else
                    txtPortaServidor.Text = "5130";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao selecionar AtendimentoConfig: " + ex.Message);
            }
        }

        private void txtMesaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtMesaInicial.Text, e);
        }

        private void txtMesaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtMesaFinal.Text, e);
        }
        private bool ValidarCampos()
        {
            string mensagemErro = string.Empty;

            // Verificar se os campos estão preenchidos
            if (string.IsNullOrWhiteSpace(txtMesaInicial.Text) || string.IsNullOrWhiteSpace(txtMesaFinal.Text))
            {
                mensagemErro = "Ambos os campos devem ser preenchidos.";
            }
            // Verificar se os campos contêm apenas números
            else if (!int.TryParse(txtMesaInicial.Text, out int mesaInicial) || !int.TryParse(txtMesaFinal.Text, out int mesaFinal))
            {
                mensagemErro = "Os campos devem conter apenas números.";
            }
            // Verificar se o valor inicial é menor que o valor final
            else if (mesaInicial >= mesaFinal)
            {
                mensagemErro = "O valor inicial deve ser menor que o valor final.";
            }
            // Verificar se os campos estão preenchidos
            else if(string.IsNullOrWhiteSpace(txtIpServidor.Text) || string.IsNullOrWhiteSpace(txtPortaServidor.Text))
            {
                mensagemErro = "IP/Nome e Porta do Servidor Devem ser Preenchidos.";
            }

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                GenericaDesktop.ShowAlerta(mensagemErro);
                return false;
            }
            return true;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if(chkModificarMesa.Checked == true)
                    SalvarMesas();
                SalvarConfiguracoes();
                GenericaDesktop.ShowInfo("Mesas salvas com sucesso!");
                this.Close();
            }
        }
        private async void SalvarMesas()
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja realmente modificar as configurações das mesas? Caso tenha mesas com status ocupadas, elas serão encerradas!"))
            {
                await ApiServiceFood.DeleteAllMesasAsync();
                int mesaInicial = int.Parse(txtMesaInicial.Text);
                int mesaFinal = int.Parse(txtMesaFinal.Text);

                for (int i = mesaInicial; i <= mesaFinal; i++)
                {
                    AtendimentoMesa atendimentoMesa = new AtendimentoMesa();
                    atendimentoMesa.Numero = i.ToString();
                    atendimentoMesa.Tipo = "MESA";
                    atendimentoMesa.Status = "DISPONIVEL";
                    Controller.getInstance().salvar(atendimentoMesa);
                }
            }
        }

        private void SalvarConfiguracoes()
        {
            AtendimentoConfig atendimentoConfig = new AtendimentoConfig();
            atendimentoConfig.Id = 1;
            atendimentoConfig.MesaInicial = int.Parse(txtMesaInicial.Text);
            atendimentoConfig.MesaFinal = int.Parse(txtMesaFinal.Text);
            atendimentoConfig.IpServidor = txtIpServidor.Text;
            atendimentoConfig.PortaApi = txtPortaServidor.Text;
            Controller.getInstance().salvar(atendimentoConfig);
        }

        private void FrmConfigFood_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnGravar.PerformClick();
                    break;
            }
        }

        private void chkModificarMesa_CheckedChanged(object sender, EventArgs e)
        {
            if(chkModificarMesa.Checked == true)
            {
                txtMesaInicial.Enabled = true;
                txtMesaFinal.Enabled = true;
            }
            else
            {
                txtMesaInicial.Enabled = false;
                txtMesaFinal.Enabled = false;
            }
        }
    }
}
