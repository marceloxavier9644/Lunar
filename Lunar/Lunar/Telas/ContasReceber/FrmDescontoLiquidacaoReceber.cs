using Lunar.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmDescontoLiquidacaoReceber : Form
    {
        decimal valorPrincipal = 0; 
        decimal valorMulta = 0;
        decimal valorJuro = 0; 
        GenericaDesktop generica = new GenericaDesktop();
        bool showModal = false;
        decimal retorno = 0;

        public DialogResult showModalNovo(ref decimal retorno)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                retorno = this.retorno;
            }
            return DialogResult;
        }
        public FrmDescontoLiquidacaoReceber(decimal valorPrincipal, decimal valorMulta, decimal valorJuro)
        {
            InitializeComponent();

            txtValorPrincipal.Texts = valorPrincipal.ToString("N2");
            txtMulta.Texts = valorMulta.ToString("N2");
            txtJuro.Texts = valorJuro.ToString("N2");
            txtTotalComDesconto.Texts = (valorPrincipal + valorMulta + valorJuro).ToString("N2");
            this.valorPrincipal = Math.Round(valorPrincipal, 2);
            this.valorMulta = Math.Round(valorMulta, 2);
            this.valorJuro = Math.Round(valorJuro, 2);
            if (valorMulta + valorJuro > 0)
                lblSomaMultaJuro.Visible = true;
            lblSomaMultaJuro.Text = "Soma de Multa e Juros " + (valorMulta + valorJuro).ToString("C2", CultureInfo.CurrentCulture);
            //txtDescontoEmValor.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void descontoEmPercentual()
        {
            try
            {
                decimal valorTotal = valorPrincipal + decimal.Parse(txtMulta.Texts) + decimal.Parse(txtJuro.Texts);
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDescontoPercentual.Texts))
                {
                    valorDesconto = valorTotal * decimal.Parse(txtDescontoPercentual.Texts) / 100;
                    if (valorDesconto > 0) 
                    { 
                        txtJuro.Enabled = false; 
                        txtMulta.Enabled = false;
                        txtJuro.Texts = valorJuro.ToString("N2");
                        txtMulta.Texts = valorMulta.ToString("N2");
                    }

                    txtDescontoEmValor.Texts = valorDesconto.ToString("N2");
                    txtTotalComDesconto.Texts = (valorTotal - valorDesconto).ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;
                    btnConfirmar.Select();
                    if (valorDesconto > valorTotal)
                        throw new Exception("Valor do desconto não pode ser maior que o valor das parcelas");
                }
            }
            catch (Exception erro)
            {
                zerarDescontos();
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        private void descontoEmValor()
        {
            try
            {
                decimal valorTotal = valorPrincipal + decimal.Parse(txtMulta.Texts) + decimal.Parse(txtJuro.Texts);
                decimal valorTotalOriginal = valorPrincipal + valorJuro + valorMulta;
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDescontoEmValor.Texts))
                {
                    valorDesconto = decimal.Parse(txtDescontoEmValor.Texts);
                    if (valorDesconto > 0)
                    {
                        txtJuro.Enabled = false;
                        txtMulta.Enabled = false;
                        txtJuro.Texts = valorJuro.ToString("N2");
                        txtMulta.Texts = valorMulta.ToString("N2");
                    }

                    txtDescontoPercentual.Texts = (valorDesconto * 100 / valorTotal).ToString("N2");
                    txtTotalComDesconto.Texts = (valorTotal - valorDesconto).ToString("N2");
                    txtDescontoEmValor.Texts = valorDesconto.ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;

                    if (valorDesconto > valorTotal)
                        throw new Exception("Valor do desconto não pode ser maior que o valor das parcelas");
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
            txtDescontoEmValor.Texts = "0";
            txtDescontoPercentual.Texts = "0";
            txtValorPrincipal.Texts = valorPrincipal.ToString("N2");
            txtMulta.Texts = valorMulta.ToString("N2");
            txtJuro.Texts = valorJuro.ToString("N2");
            txtTotalComDesconto.Texts = (valorPrincipal + valorMulta + valorJuro).ToString("N2");

        }

        private void txtDescontoPercentual_Leave(object sender, EventArgs e)
        {
            descontoEmPercentual();
        }

        private void txtDescontoEmValor_Leave(object sender, EventArgs e)
        {
            descontoEmValor();
        }

        private void txtMulta_Leave(object sender, EventArgs e)
        {
            alterarJuroMulta();
        }

        private void alterarJuroMulta()
        {
            try
            {
                decimal m = 0;
                decimal j = 0;
                if (!String.IsNullOrEmpty(txtMulta.Texts))
                {
                    m = decimal.Parse(txtMulta.Texts);
                    if (m < valorMulta || m > valorMulta)
                    {
                        txtMulta.Texts = decimal.Parse(txtMulta.Texts).ToString("N2");
                        txtDescontoPercentual.Enabled = false;
                        txtDescontoEmValor.Enabled = false;
                    }
                    else
                    {
                        txtDescontoPercentual.Enabled = true;
                        txtDescontoEmValor.Enabled = true;
                    }
                }
                if (!String.IsNullOrEmpty(txtJuro.Texts))
                {
                    j = decimal.Parse(txtJuro.Texts);
                    if (j < valorJuro || j > valorJuro)
                    {
                        txtJuro.Texts = decimal.Parse(txtJuro.Texts).ToString("N2");
                        txtDescontoPercentual.Enabled = false;
                        txtDescontoEmValor.Enabled = false;
                    }
                    else
                    {
                        txtDescontoPercentual.Enabled = true;
                        txtDescontoEmValor.Enabled = true;
                    }
                }
                //Se juro e multa ficou menor é um desconto, senao houve acrescimo
                if(m + j < valorJuro + valorMulta)
                {
                    txtDescontoEmValor.Texts = ((valorJuro + valorMulta) - (m + j)).ToString("N2");
                }
                if (m + j > 0)
                    lblSomaMultaJuro.Visible = true;
                lblSomaMultaJuro.Text = "Soma de Multa e Juros " + (m + j).ToString("C2", CultureInfo.CurrentCulture);
                txtTotalComDesconto.Texts = (valorPrincipal + m + j).ToString("N2");
            }
            catch
            {
                GenericaDesktop.ShowErro("Erro ao calcular desconto, preencha os campos corretamente!");
            }
        }

        private void txtJuro_Leave(object sender, EventArgs e)
        {
            alterarJuroMulta();
        }

        private void txtMulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtMulta.Texts, e);
            if(e.KeyChar == 13)
            {
                alterarJuroMulta();
            }
        }

        private void txtJuro_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtJuro.Texts, e);
            if (e.KeyChar == 13)
            {
                alterarJuroMulta();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.retorno = decimal.Parse(txtTotalComDesconto.Texts);
            retorno = Math.Round(retorno, 2);
            if (retorno == 0)
            {
                GenericaDesktop.ShowAlerta("Atenção, você deu desconto no valor total das parcelas!");
            }
            DialogResult = DialogResult.OK;
        }

        private void txtDescontoEmValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtJuro.Texts, e);
            if (e.KeyChar == 13)
            {
                descontoEmValor();
            }
        }

        private void txtDescontoPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtJuro.Texts, e);
            if (e.KeyChar == 13)
            {
                descontoEmPercentual();
            }
        }
    }
}
