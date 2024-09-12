using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Financeiro.Cartoes
{
    public partial class FrmAdquirenteCartao : Form
    {
        AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        public DialogResult showModal(ref AdquirenteCartao adquirenteCartao)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                adquirenteCartao = this.adquirenteCartao;
            }
            return DialogResult;
        }
        public FrmAdquirenteCartao()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public FrmAdquirenteCartao(AdquirenteCartao adquirenteCartao)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.adquirenteCartao = adquirenteCartao;
            get_AdquirenteCartao(adquirenteCartao);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAdquirenteCartao_KeyDown(object sender, KeyEventArgs e)
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

        private void set_AdquirenteCartao()
        {
            if (!String.IsNullOrEmpty(txtAdquirente.Texts) && !String.IsNullOrEmpty(txtCNPJ.Texts) && !String.IsNullOrEmpty(txtCodContaBancaria.Texts))
            {
                adquirenteCartao = new AdquirenteCartao();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    adquirenteCartao.Id = 0;
                }
                else
                    adquirenteCartao.Id = int.Parse(txtCodigo.Texts);

                adquirenteCartao.Descricao = txtAdquirente.Texts;
                adquirenteCartao.Cnpj = GenericaDesktop.RemoveCaracteres(txtCNPJ.Texts);

                ContaBancaria contaBancaria = new ContaBancaria();
                contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts);
                contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                adquirenteCartao.ContaBancaria = contaBancaria;

                Controller.getInstance().salvar(adquirenteCartao);
                GenericaDesktop.ShowInfo("Registro salvo com sucesso");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                GenericaDesktop.ShowErro("Preencha todos os campos");
        }

        private void get_AdquirenteCartao(AdquirenteCartao adquirenteCartao)
        {
            txtCodigo.Texts = adquirenteCartao.Id.ToString();
            txtAdquirente.Texts = adquirenteCartao.Descricao;
            txtCNPJ.Texts = adquirenteCartao.Cnpj;
            if (adquirenteCartao.ContaBancaria != null)
            {
                txtContaBancaria.Texts = adquirenteCartao.ContaBancaria.Descricao;
                txtCodContaBancaria.Texts = adquirenteCartao.ContaBancaria.Id.ToString();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_AdquirenteCartao();
        }

        private void FrmAdquirenteCartao_Paint(object sender, PaintEventArgs e)
        {
            txtAdquirente.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void txtCNPJ_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCNPJ.Texts))
            {
                if (!GenericaDesktop.validarCPFCNPJ(txtCNPJ.Texts))
                {
                    GenericaDesktop.ShowAlerta("CNPJ Inválido");
                    txtCNPJ.Texts = "";
                    txtCNPJ.Focus();
                }
            }
        }

        private void txtAdquirente_Leave(object sender, EventArgs e)
        {
            txtCNPJ.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
        }

        private void FrmAdquirenteCartao_Load(object sender, EventArgs e)
        {
            //this.Opacity = 0.0;
            //timer1.Start();
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

  
        private void pesquisarConta()
        {
            Object objeto = new ContaBancaria();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancaria.Texts = "";
                    txtContaBancaria.Texts = "";
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
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
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
        private void btnPesquisaCon_Click(object sender, EventArgs e)
        {
            pesquisarConta();
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            pesquisarConta();
        }
    }
}
