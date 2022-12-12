using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Bancos
{
    public partial class FrmContaBancaria : Form
    {
        ContaBancaria contaBancaria = new ContaBancaria();
        BancoController bancoController = new BancoController();
        public DialogResult showModal(ref object contaBancaria)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                contaBancaria = this.contaBancaria;
            }
            return DialogResult;
        }
        public FrmContaBancaria()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public FrmContaBancaria(ContaBancaria contaBancaria)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            get_ContaBancaria(contaBancaria);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmContaBancaria_KeyDown(object sender, KeyEventArgs e)
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

        private void set_ContaBancaria()
        {
            contaBancaria = new ContaBancaria();
            if (String.IsNullOrEmpty(txtCodigoConta.Texts))
            {
                contaBancaria.Id = 0;
            }
            else
                contaBancaria.Id = int.Parse(txtCodigoConta.Texts);
            contaBancaria.Descricao = txtDescricaoConta.Texts;
            contaBancaria.Agencia = txtAgencia.Texts;
            contaBancaria.DvAgencia = txtDVAgencia.Texts;
            contaBancaria.DvConta = txtDVConta.Texts;
            contaBancaria.Conta = txtConta.Texts;
            Banco banco = new Banco();
            banco.Id = int.Parse(txtCodBanco.Texts);
            contaBancaria.Banco = (Banco)bancoController.selecionar(banco);
            contaBancaria.Pix = txtChavePIX.Texts;
            contaBancaria.EmpresaFilial = Sessao.empresaFilialLogada;
            Controller.getInstance().salvar(contaBancaria);
            GenericaDesktop.ShowInfo("Registro salvo com sucesso");
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void get_ContaBancaria(ContaBancaria conta)
        {
            txtCodigoConta.Texts = conta.Id.ToString();
            txtDescricaoConta.Texts = conta.Descricao;
            txtAgencia.Texts = conta.Agencia;
            txtDVAgencia.Texts = conta.DvAgencia;
            txtBanco.Texts = conta.Banco.Descricao;
            txtCodBanco.Texts = conta.Banco.Id.ToString();
            txtConta.Texts = conta.Conta;
            txtDVConta.Texts = conta.DvConta;
            txtChavePIX.Texts = conta.Pix;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_ContaBancaria();
        }

        private void FrmContaBancaria_Paint(object sender, PaintEventArgs e)
        {
           // txtDescricaoConta.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnPesquisaBanco_Click(object sender, EventArgs e)
        {
            Object bancoObjeto = new Banco();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Banco", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.CodBanco) like '%" + txtBanco.Texts + "%'"))
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
                    switch (uu.showModal("Banco", "", ref bancoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmBancosCadastro form = new FrmBancosCadastro();
                            //if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            //{
                            //    txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            //    txtClienteAbaPagamento.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            //    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            //    txtPesquisaProduto.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtBanco.Texts = ((Banco)bancoObjeto).Descricao;
                            txtCodBanco.Texts = ((Banco)bancoObjeto).Id.ToString();
                            txtDescricaoConta.Focus();
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
