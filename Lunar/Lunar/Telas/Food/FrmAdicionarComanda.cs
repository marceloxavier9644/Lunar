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
            NumeroComanda = txtNumeroComanda.Text;
            NomeCliente = txtNomeCliente.Text;
            Observacao = txtObservacao.Text;
            QuantidadePessoas = txtQuantidadePessoas.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
