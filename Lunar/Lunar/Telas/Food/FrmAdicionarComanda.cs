using LunarBase.Classes;
using LunarBase.ControllerBO;
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
        public FrmAdicionarComanda()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
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
