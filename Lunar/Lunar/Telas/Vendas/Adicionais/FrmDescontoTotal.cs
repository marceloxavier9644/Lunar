using Lunar.Utils;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmDescontoTotal : Form
    {
        bool descontoItem = false;
        bool showModal = false;
        decimal totalVenda = 0;
        decimal totalItem = 0;
        GenericaDesktop generica = new GenericaDesktop();
        public DialogResult showModalNovo(ref decimal totalVenda)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                totalVenda = this.totalVenda;
            }
            return DialogResult;
        }

        public DialogResult showModalItem(ref decimal totalItem)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                totalItem = this.totalItem;
            }
            return DialogResult;
        }
        public FrmDescontoTotal(decimal totalVenda, bool descontoItem)
        {
            InitializeComponent();
            this.totalVenda = totalVenda;
            txtTotalVenda.Texts = totalVenda.ToString("C2", CultureInfo.CurrentCulture);
            txtTotalComDesconto.Texts = totalVenda.ToString("N2");
            txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;
            if (descontoItem == true)
            {
                this.descontoItem = descontoItem;
                lblTotalVendaOuItem.Text = "Total do Item";
            }
        }

        private void FrmDescontoTotal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;

                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void txtDescontoPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtDescontoPercentual.Texts, e);
            if (e.KeyChar == 13)
            {
                descontoEmPercentual();
            }
        }

        private void txtDescontoEmValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtDescontoEmValor.Texts, e); if (e.KeyChar == 13)
            {
                descontoEmValor();
            }
        }

        private void txtTotalComDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtTotalComDesconto.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmar.PerformClick();
            }
        }

        private void descontoEmValor()
        {
            try
            {
                decimal valorOriginal = totalVenda;
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDescontoEmValor.Texts))
                {
                    valorDesconto = decimal.Parse(txtDescontoEmValor.Texts);
                    txtDescontoPercentual.Texts = (valorDesconto * 100 / valorOriginal).ToString("N2");
                    txtTotalComDesconto.Texts = (valorOriginal - valorDesconto).ToString("N2");
                    txtDescontoEmValor.Texts = valorDesconto.ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;

                    if (valorDesconto > totalVenda)
                        throw new Exception("Valor do desconto não pode ser maior que o valor da venda");
                }

            }
            catch (Exception erro)
            {
                zerarDescontos();
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }
        private void txtDescontoEmValor_Leave(object sender, System.EventArgs e)
        {
            descontoEmValor();
        }

        private void descontoEmPercentual()
        {
            try
            {
                decimal valorOriginal = totalVenda;
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDescontoPercentual.Texts))
                {
                    valorDesconto = valorOriginal * decimal.Parse(txtDescontoPercentual.Texts) / 100;
                    txtDescontoEmValor.Texts = valorDesconto.ToString("N2");
                    txtTotalComDesconto.Texts = (valorOriginal - valorDesconto).ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;

                    if (valorDesconto > totalVenda)
                        throw new Exception("Valor do desconto não pode ser maior que o valor da venda");
                }
            }
            catch (Exception erro)
            {
                zerarDescontos();
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }
        private void txtDescontoPercentual_Leave(object sender, System.EventArgs e)
        {
            descontoEmPercentual();
        }

        private void btnConfirmar_Click(object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTotalComDesconto.Texts))
            {
                txtTotalComDesconto.Texts = (totalVenda - decimal.Parse(txtDescontoEmValor.Texts)).ToString("N2");

                if (descontoItem == true)
                    totalItem = decimal.Parse(txtDescontoEmValor.Texts);

                this.totalVenda = decimal.Parse(txtTotalComDesconto.Texts);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("Preencha Corretamente os Valores");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtDescontoPercentual_MouseClick(object sender, MouseEventArgs e)
        {
                txtDescontoPercentual.SelectAll();
        }

        private void txtDescontoEmValor_MouseClick(object sender, MouseEventArgs e)
        {
            txtDescontoEmValor.SelectAll();
        }

        private void txtTotalComDesconto_MouseClick(object sender, MouseEventArgs e)
        {
            txtTotalComDesconto.SelectAll();
        }

        private void txtTotalComDesconto_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal valorOriginal = totalVenda;
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtTotalComDesconto.Texts))
                {
                    txtDescontoEmValor.Texts = (valorOriginal - decimal.Parse(txtTotalComDesconto.Texts.Replace("R$ ", ""))).ToString("N2");
                    valorDesconto = decimal.Parse(txtDescontoEmValor.Texts);
                    txtDescontoPercentual.Texts = (valorDesconto * 100 / valorOriginal).ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;
                    if (valorDesconto > totalVenda)
                        throw new Exception("Valor do desconto não pode ser maior que o valor da venda");
                    if(decimal.Parse(txtTotalComDesconto.Texts) > totalVenda)
                        throw new Exception("O total não pode ser maior que o valor da venda");
                }
            }
            catch (Exception erro)
            {
                zerarDescontos();
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        private void zerarDescontos()
        {
            txtDescontoEmValor.Texts = "0,00";
            txtDescontoPercentual.Texts = "0";
            txtTotalComDesconto.Texts = totalVenda.ToString("N2");
        }
        private void FrmDescontoTotal_Load(object sender, EventArgs e)
        {

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
    }
}
