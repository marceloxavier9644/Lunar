using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunarAtualizador
{
    public partial class FrmNotificacao : Form
    {
        public bool Atualiza { get; private set; }
        public FrmNotificacao()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Atualiza = false;
            // Fecha o formulário com DialogResult.OK
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Atualiza = true;
            // Fecha o formulário com DialogResult.OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Atualiza = false;
            // Fecha o formulário com DialogResult.OK
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
