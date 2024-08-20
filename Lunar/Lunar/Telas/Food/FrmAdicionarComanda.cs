using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class FrmAdicionarComanda : Form
    {
        public string NumeroComanda { get; private set; }
        public string NomeCliente { get; private set; }
        public string Observacao { get; private set; }
        public string QuantidadePessoas { get; private set; }
        string atendimentoId = "";
        public FrmAdicionarComanda()
        {
            InitializeComponent();
        }
        public FrmAdicionarComanda(bool addConta, string numeroMesaPrincipal, int quantidadePessoa, string atendimentoId)
        {
            InitializeComponent();
            if(addConta == true)
            {
                txtNumeroComanda.Enabled = false;
                label2.Text = "Número Mesa";
                txtNumeroComanda.Text = numeroMesaPrincipal;
                txtQuantidadePessoas.Text = quantidadePessoa.ToString();
                txtNomeCliente.Focus();
                txtQuantidadePessoas.Enabled = false;
                this.atendimentoId = atendimentoId;
            }
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (label2.Text != "Número Mesa")
            {
                AtendimentoMesa atendimentoMesa = new AtendimentoMesa();
                atendimentoMesa.Numero = txtNumeroComanda.Text;
                atendimentoMesa.Status = "OCUPADO";
                atendimentoMesa.Tipo = "COMANDA";
                atendimentoMesa.Nome = txtNomeCliente.Text;
                atendimentoMesa.Observacao = txtObservacao.Text;
                Controller.getInstance().salvar(atendimentoMesa);

                NumeroComanda = txtNumeroComanda.Text;
                NomeCliente = txtNomeCliente.Text;
                Observacao = txtObservacao.Text;
                QuantidadePessoas = txtQuantidadePessoas.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            //Adicionar apenas uma conta em mesa ja aberta
            else if(label2.Text == "Número Mesa")
            {
                if (!String.IsNullOrEmpty(txtNomeCliente.Text) && int.Parse(atendimentoId) > 0)
                {
                    AtendimentoConta atendimentoConta = new AtendimentoConta();
                    atendimentoConta.AtendimentoId = int.Parse(atendimentoId);
                    atendimentoConta.Cliente = null;
                    atendimentoConta.NomeCliente = txtNomeCliente.Text;
                    atendimentoConta.OperadorCadastro = Sessao.usuarioLogado.Id.ToString();
                    Controller.getInstance().salvar(atendimentoConta);
                    this.Close();
                }
            }
        }

        private void FrmAdicionarComanda_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;
            }
        }
    }
}
