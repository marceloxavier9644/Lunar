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
    public partial class FrmAguarde2 : Form
    {
        public FrmAguarde2()
        {
            InitializeComponent();
        }
        public void SetMensagemAguarde(string mensagem)
        {
            lblMensagem.Text = mensagem;
        }
    }
}
