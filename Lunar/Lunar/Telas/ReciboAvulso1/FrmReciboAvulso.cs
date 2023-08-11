using Lunar.Utils;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ReciboAvulso1
{
    public partial class FrmReciboAvulso : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        public FrmReciboAvulso()
        {
            InitializeComponent();
            txtEmitente.Text = Sessao.empresaFilialLogada.NomeFantasia;
            txtCpf.Text = Sessao.empresaFilialLogada.Cnpj;
            txtValor.Select();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtValor.Text, e);
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txtValor.Text);
                txtValor.Text = string.Format("{0:0.00}", valor);
            }
            catch
            {

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void imprimir()
        {
            FrmImprimirReciboAvulso frm = new FrmImprimirReciboAvulso(txtValor.Text, ConverterMoedaPorExtenso.toExtenso(decimal.Parse(txtValor.Text)), txtEmitente.Text, txtDestinatario.Text, txtCpf.Text, txtDataEmissao.Text, txtReferente.Text);
            frm.ShowDialog();
        }

        private void FrmReciboAvulso_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F8:
                    btnImprimir.PerformClick();
                    break;
            }
        }

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCpf.Text, e);
        }
    }
}
