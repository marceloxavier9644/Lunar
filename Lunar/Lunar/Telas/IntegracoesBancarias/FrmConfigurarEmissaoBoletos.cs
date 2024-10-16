using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.IntegracoesBancarias
{
    public partial class FrmConfigurarEmissaoBoletos : Form
    {
        BoletoConfigController boletoConfigController = new BoletoConfigController();

        public FrmConfigurarEmissaoBoletos()
        {
            InitializeComponent();
        }

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Object objeto = new ContaBancaria();

            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancaria.Text = "";
                    txtContaBancaria.Text = "";
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
                    switch (uu.showModal("ContaBancaria", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancaria.Text = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancaria.Text = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancaria.Text = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancaria.Text = ((ContaBancaria)objeto).Id.ToString();
                            string textoSicredi = "Usuário (Cód. Beneficiário + Cód. Cooperativa)";
                            autoLabel8.Text = "Usuário";
                            if (((ContaBancaria)objeto).Banco.Descricao.ToUpper().Contains("SICREDI"))
                            {
                                autoLabel8.Text = textoSicredi;
                                autoLabel9.Text = "Código Gerado";
                            }
                            IList<BoletoConfig> listaBoletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancaria(((ContaBancaria)objeto).Id);
                            if (listaBoletoConfig.Count > 0)
                            {
                                foreach(BoletoConfig bc in listaBoletoConfig)
                                {
                                    get_configuracaoBoleto(bc);
                                }

                            }
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

        private void set_configuracaoBoleto()
        {
            BoletoConfig boletoConfig = new BoletoConfig();

            ContaBancaria contaBancaria = new ContaBancaria();
            contaBancaria.Id = int.Parse(txtCodContaBancaria.Text);
            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
            boletoConfig.ContaBancaria = contaBancaria;
            IList<BoletoConfig> listaBoletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancaria(contaBancaria.Id);
            if(listaBoletoConfig.Count > 0)
            {
                foreach(BoletoConfig bc in listaBoletoConfig)
                {
                    boletoConfig = bc;
                }
            }
            boletoConfig.CodigoBeneficiario = txtCodigoBeneficiario.Text;
            boletoConfig.ContaPadrao = false;
            if (chkContaPadrao.Checked == true)
            {
                //Limpa todas outras padroes primeiro
                IList<BoletoConfig> listaBoletoConfigs = boletoConfigController.selecionarTodosBoletoConfig();
                if(listaBoletoConfigs.Count > 0)
                {
                    foreach(BoletoConfig boletoC in listaBoletoConfigs)
                    {
                        if(boletoC.ContaPadrao == true)
                            boletoC.ContaPadrao = false;
                        Controller.getInstance().salvar(boletoC);
                    }
                }
                //e marca essa como padrão
                boletoConfig.ContaPadrao = true;
            }
            boletoConfig.Cooperativa = txtCooperativa.Text.Trim();
            boletoConfig.Descricao = "CONFIG " + contaBancaria.Descricao;
            boletoConfig.IdentificacaoCliente = txtIdentificacaoCliente.Text.Trim();
            boletoConfig.IdToken = txtIdToken.Text.Trim();
            boletoConfig.JuroMensal = decimal.Parse(txtJuro.Text);
            boletoConfig.Multa = decimal.Parse(txtMulta.Text);
            boletoConfig.NumeroCarteira = txtNumeroCarteira.Text.Trim();
            boletoConfig.Posto = txtPosto.Text.Trim();
            boletoConfig.Senha = GenericaDesktop.Criptografa(txtSenha.Text);
            if(radioTradicional.Checked == true)
                boletoConfig.TipoBoleto = "NORMAL";
            else
                boletoConfig.TipoBoleto = "HIBRIDO";
            boletoConfig.Token = txtToken.Text;
            boletoConfig.Usuario = txtUsuario.Text.Trim();

            if (!String.IsNullOrEmpty(txtCodPlanoConta.Text))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoConta.Text);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                boletoConfig.PlanoContaTarifa = planoConta;
            }
            if (radioProducao.Checked == true)
                boletoConfig.AmbienteProducao = true;
            else
                boletoConfig.AmbienteProducao = false;

            Controller.getInstance().salvar(boletoConfig);
            GenericaDesktop.ShowInfo("Registro salvo com Sucesso!");
            this.Close();
        }

        private void get_configuracaoBoleto(BoletoConfig boletoConfig)
        {
            if (boletoConfig == null)
            {
                // Se o objeto boletoConfig for nulo, não faça nada ou exiba uma mensagem de erro.
                MessageBox.Show("Configuração do boleto não encontrada.");
                return;
            }

            // Preenchendo os campos do formulário com os dados de boletoConfig
            txtContaBancaria.Text = boletoConfig.ContaBancaria.Descricao;
            txtCodContaBancaria.Text = boletoConfig.ContaBancaria?.Id.ToString() ?? string.Empty; // Verifica se ContaBancaria é nula
            txtCodigoBeneficiario.Text = boletoConfig.CodigoBeneficiario;
            chkContaPadrao.Checked = boletoConfig.ContaPadrao;
            txtCooperativa.Text = boletoConfig.Cooperativa;
            txtIdentificacaoCliente.Text = boletoConfig.IdentificacaoCliente;
            txtIdToken.Text = boletoConfig.IdToken;
            txtJuro.Text = boletoConfig.JuroMensal.ToString("F2"); // Formata como decimal com 2 casas decimais
            txtMulta.Text = boletoConfig.Multa.ToString("F2"); // Formata como decimal com 2 casas decimais
            txtNumeroCarteira.Text = boletoConfig.NumeroCarteira;
            txtPosto.Text = boletoConfig.Posto;
            if (boletoConfig.PlanoContaTarifa != null)
            {
                txtPlanoContaTarifa.Text = boletoConfig.PlanoContaTarifa.Descricao;
                txtCodPlanoConta.Text = boletoConfig.PlanoContaTarifa.Id.ToString();
            }
                if (!String.IsNullOrEmpty(boletoConfig.Senha))
                txtSenha.Text = GenericaDesktop.Descriptografa(boletoConfig.Senha);
            else
                txtSenha.Text = "";

            // Definindo o tipo de boleto com base na configuração
            if (boletoConfig.TipoBoleto == "NORMAL")
            {
                radioTradicional.Checked = true;
            }
            else if (boletoConfig.TipoBoleto == "HIBRIDO")
            {
                radioHibrido.Checked = true; // Supondo que você tenha esse radio button no seu formulário
            }
            if (boletoConfig.AmbienteProducao == true)
                radioProducao.Checked = true;          
            else
                radioHomologacao.Checked = true; 
     

            txtToken.Text = boletoConfig.Token;
            txtUsuario.Text = boletoConfig.Usuario;
        }

        private void txtMulta_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMulta.Text))
            {
                decimal valorMulta = decimal.Parse(txtMulta.Text);
                txtMulta.Text = valorMulta.ToString("F2");
            }
            else
            {
                txtMulta.Text = "0";
            }
        }

        private void txtJuro_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtJuro.Text))
            {
                decimal valorJuro = decimal.Parse(txtJuro.Text);
                txtJuro.Text = valorJuro.ToString("F2");
            }
            else
            {
                txtJuro.Text = "0";
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if(ValidarDados())
                set_configuracaoBoleto();
        }


        private bool ValidarDados()
        {
            if (string.IsNullOrEmpty(txtCodContaBancaria.Text))
            {
                MessageBox.Show("A conta bancária é obrigatória.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtCodPlanoConta.Text))
            {
                MessageBox.Show("O Plano de Contas para tarifas dos boletos é obrigatório.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnPesquisaPlanoConta.PerformClick();
                return false;
            }

            ContaBancaria contaBancaria = new ContaBancaria
            {
                Id = int.Parse(txtCodContaBancaria.Text)
            };
            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);

            // Se a conta for do Sicredi, valida outros campos
            if (contaBancaria != null && contaBancaria.Banco.Descricao.Contains("SICREDI"))
            {
                if (string.IsNullOrEmpty(txtCodigoBeneficiario.Text))
                {
                    MessageBox.Show("O código do beneficiário é obrigatório para contas Sicredi.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoBeneficiario.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(txtCooperativa.Text))
                {
                    MessageBox.Show("A cooperativa do cliente é obrigatória para contas Sicredi.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCooperativa.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtPosto.Text))
                {
                    MessageBox.Show("O posto do cliente é obrigatório para contas Sicredi.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPosto.Focus();
                    return false;
                }
            }

            return true; 
        }

        private void btnPesquisaPlanoConta_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", "and Tabela.TipoConta = 'DESPESA' "))
                {
                    txtCodPlanoConta.Text = "";
                    txtPlanoContaTarifa.Text = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoContaTarifa.Text = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoConta.Text = ((PlanoConta)objeto).Id.ToString();
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
