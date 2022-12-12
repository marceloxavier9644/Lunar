using Lunar.Utils;
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
    public partial class FrmExcluirItem : Form
    {
        bool showModal = false;
        int item = 0;
        public DialogResult showModalNovo(ref int item)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                item = this.item;
            }
            return DialogResult;
        }
        public FrmExcluirItem()
        {
            InitializeComponent();

        }
        
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            confirmaItem();
        }

        private void FrmExcluirItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    confirmaItem();
                    break;

                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void confirmaItem()
        {
            if (!String.IsNullOrEmpty(txtValor.Texts)) 
            {
                this.item = int.Parse(txtValor.Texts);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("Digite o Nº do Item!");
            }
        }

        private void FrmExcluirItem_Load(object sender, EventArgs e)
        {

        }

        private void FrmExcluirItem_Paint(object sender, PaintEventArgs e)
        {
            txtValor.TextAlign = HorizontalAlignment.Center;
            txtValor.Focus();
            txtValor.Select();
            this.TopMost = false;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                confirmaItem();
            }
        }
    }
}
