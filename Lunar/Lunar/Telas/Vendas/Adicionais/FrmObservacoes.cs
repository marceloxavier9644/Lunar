using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmObservacoes : Form
    {
        public string Observacao { get; private set; }
        public FrmObservacoes(string observacoes)
        {
            InitializeComponent();
            txtObservacoes.Text = observacoes;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Observacao = txtObservacoes.Text;

            // Fecha o formulário com o DialogResult definido como OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
