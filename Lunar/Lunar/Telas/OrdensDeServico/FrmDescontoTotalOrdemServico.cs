using Lunar.Utils;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmDescontoTotalOrdemServico : Form
    {
        private enum UltimoCampoAlterado { Nenhum, Percentual, Valor }
        private UltimoCampoAlterado ultimoCampoAlteradoProduto = UltimoCampoAlterado.Nenhum;
        private UltimoCampoAlterado ultimoCampoAlteradoServico = UltimoCampoAlterado.Nenhum;
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        public FrmDescontoTotalOrdemServico(decimal totalProdutos, decimal totalServicos)
        {
            InitializeComponent();

            txtTotalProdutos.Texts = string.Format("{0:0.00}", totalProdutos);
            txtTotalServicos.Texts = string.Format("{0:0.00}", totalServicos);
            txtTotalGeralSemDesconto.Texts = string.Format("{0:0.00}", totalServicos + totalProdutos);

            txtTotalComDescontoProduto.Texts = string.Format("{0:0.00}", totalProdutos);
            txtTotalComDescontoServico.Texts = string.Format("{0:0.00}", totalServicos);

            if(totalServicos <= 0)
            {
                txtDescontoPercentualServico.Enabled = false;
                txtDescontoEmValorServico.Enabled = false;
            }
            if (totalProdutos <= 0)
            {
                txtDescontoPercentualProduto.Enabled = false;
                txtDescontoEmValorProduto.Enabled = false;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtDescontoPercentualProduto__TextChanged(object sender, EventArgs e)
        {
            ultimoCampoAlteradoProduto = UltimoCampoAlterado.Percentual;
            CalcularDescontoProdutos();
            CalcularTotalGeralComDesconto();
        }

        private void CalcularDescontoEmValorProduto()
        {
            decimal descontoEmValorProduto = decimal.Parse(txtTotalProdutos.Texts) - decimal.Parse(txtTotalComDescontoProduto.Texts);
            txtDescontoEmValorProduto.Texts = descontoEmValorProduto.ToString("N2");
        }

        private void txtDescontoEmValorProduto__TextChanged(object sender, EventArgs e)
        {
            ultimoCampoAlteradoProduto = UltimoCampoAlterado.Valor;
            CalcularDescontoProdutos();
            CalcularPercentualDescontoProduto(); // Novo método para calcular o percentual baseado no valor
            CalcularTotalGeralComDesconto();

        }

        private void txtDescontoPercentualServico__TextChanged(object sender, EventArgs e)
        {
            ultimoCampoAlteradoServico = UltimoCampoAlterado.Percentual;
            CalcularDescontoServicos();
            CalcularTotalGeralComDesconto();
        }

        private void txtDescontoEmValorServico__TextChanged(object sender, EventArgs e)
        {
            ultimoCampoAlteradoServico = UltimoCampoAlterado.Valor;
            CalcularDescontoServicos();
            CalcularPercentualDescontoServico(); // Novo método para calcular o percentual baseado no valor
            CalcularTotalGeralComDesconto();
        }

        private void CalcularPercentualDescontoProduto()
        {
            decimal totalProdutos = decimal.Parse(txtTotalProdutos.Texts);
            decimal descontoEmValorProduto = string.IsNullOrEmpty(txtDescontoEmValorProduto.Texts) ? 0 : decimal.Parse(txtDescontoEmValorProduto.Texts);

            if (totalProdutos != 0)
            {
                decimal descontoPercentualProduto = (descontoEmValorProduto / totalProdutos) * 100;
                txtDescontoPercentualProduto.Texts = descontoPercentualProduto.ToString("N2");
            }
        }

        private void CalcularPercentualDescontoServico()
        {
            decimal totalServicos = decimal.Parse(txtTotalServicos.Texts);
            decimal descontoEmValorServico = string.IsNullOrEmpty(txtDescontoEmValorServico.Texts) ? 0 : decimal.Parse(txtDescontoEmValorServico.Texts);

            if (totalServicos != 0)
            {
                decimal descontoPercentualServico = (descontoEmValorServico / totalServicos) * 100;
                txtDescontoPercentualServico.Texts = descontoPercentualServico.ToString("N2");
            }
        }

        private void CalcularDescontoProdutos()
        {
            decimal totalProdutos = decimal.Parse(txtTotalProdutos.Texts);
            decimal totalComDescontoProduto;

            if (ultimoCampoAlteradoProduto == UltimoCampoAlterado.Percentual)
            {
                decimal descontoPercentualProduto = string.IsNullOrEmpty(txtDescontoPercentualProduto.Texts) ? 0 : decimal.Parse(txtDescontoPercentualProduto.Texts);
                totalComDescontoProduto = totalProdutos - (totalProdutos * (descontoPercentualProduto / 100));
            }
            else if (ultimoCampoAlteradoProduto == UltimoCampoAlterado.Valor)
            {
                decimal descontoEmValorProduto = string.IsNullOrEmpty(txtDescontoEmValorProduto.Texts) ? 0 : decimal.Parse(txtDescontoEmValorProduto.Texts);
                totalComDescontoProduto = totalProdutos - descontoEmValorProduto;
            }
            else
            {
                totalComDescontoProduto = totalProdutos; // No discount applied
            }

            txtTotalComDescontoProduto.Texts = totalComDescontoProduto.ToString("N2");
        }

        private void CalcularDescontoServicos()
        {
            decimal totalServicos = decimal.Parse(txtTotalServicos.Texts);
            decimal totalComDescontoServico;

            if (ultimoCampoAlteradoServico == UltimoCampoAlterado.Percentual)
            {
                decimal descontoPercentualServico = string.IsNullOrEmpty(txtDescontoPercentualServico.Texts) ? 0 : decimal.Parse(txtDescontoPercentualServico.Texts);
                totalComDescontoServico = totalServicos - (totalServicos * (descontoPercentualServico / 100));
            }
            else if (ultimoCampoAlteradoServico == UltimoCampoAlterado.Valor)
            {
                decimal descontoEmValorServico = string.IsNullOrEmpty(txtDescontoEmValorServico.Texts) ? 0 : decimal.Parse(txtDescontoEmValorServico.Texts);
                totalComDescontoServico = totalServicos - descontoEmValorServico;
            }
            else
            {
                totalComDescontoServico = totalServicos; // No discount applied
            }

            txtTotalComDescontoServico.Texts = totalComDescontoServico.ToString("N2");
        }

        private void CalcularTotalGeralComDesconto()
        {
            decimal totalComDescontoProduto = decimal.Parse(txtTotalComDescontoProduto.Texts);
            decimal totalComDescontoServico = decimal.Parse(txtTotalComDescontoServico.Texts);

            decimal totalGeralComDesconto = totalComDescontoProduto + totalComDescontoServico;
            txtValorTotalComDesconto.Text = totalGeralComDesconto.ToString("N2");

            CalcularPercentualDescontoGeral();
            if (totalComDescontoProduto < 0)
            {
                GenericaDesktop.ShowAlerta("O valor final do produto não pode ser menor que zero!");
                txtDescontoPercentualProduto.Texts = "0";
                txtDescontoEmValorProduto.Texts = "0";
            }
            if (totalComDescontoServico < 0)
            {
                GenericaDesktop.ShowAlerta("O valor final do serviço não pode ser menor que zero!");
                txtDescontoPercentualServico.Texts = "0";
                txtDescontoEmValorServico.Texts = "0";
            }
        }

        private void TxtTotalGeralComDesconto__TextChanged(object sender, EventArgs e)
        {
          
        }

        private void CalcularPercentualDescontoGeral()
        {
            decimal descPercentual = 0;
            descPercentual = (decimal.Parse(txtValorTotalComDesconto.Text) / decimal.Parse(txtTotalGeralSemDesconto.Texts)  * 100) - 100;
            if (descPercentual != 0)
            {
                txtDescontoPercentualGeral.Text = descPercentual.ToString("N5") + "%";
            }
            else
            {
                txtDescontoPercentualGeral.Text = "0"; // Evita divisão por zero
            }
        }

        private void txtDescontoPercentualProduto_Leave(object sender, EventArgs e)
        {
            CalcularDescontoEmValorProduto(); // Calcula e atualiza o valor do desconto
        }

        private void txtDescontoPercentualServico_Leave(object sender, EventArgs e)
        {

            decimal descontoEmValorServico = decimal.Parse(txtTotalServicos.Texts) - decimal.Parse(txtTotalComDescontoServico.Texts);
            txtDescontoEmValorServico.Texts = descontoEmValorServico.ToString("N2");
        }

        private void txtDescontoPercentualProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoPercentualProduto.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDescontoEmValorProduto.Focus();
            }
        }

        private void txtDescontoEmValorProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoEmValorProduto.Texts, e);
            if (e.KeyChar == 13)
            {
                if (decimal.Parse(txtTotalServicos.Texts) > 0)
                {
                    txtDescontoPercentualServico.Focus();
                }
                else
                    btnConfirmar.PerformClick();
            }
        }

        private void txtDescontoPercentualServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoPercentualServico.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDescontoEmValorServico.Focus();
            }
        }

        private void txtDescontoEmValorServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoEmValorServico.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmar.PerformClick();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            CalcularDescontoFinal();
            this.Close();
        }

        private void CalcularDescontoFinal()
        {
            // Valida e obtem os valores de texto como decimais
            decimal totalProdutos = string.IsNullOrEmpty(txtTotalProdutos.Texts) ? 0 : decimal.Parse(txtTotalProdutos.Texts);
            decimal totalComDescontoProduto = string.IsNullOrEmpty(txtTotalComDescontoProduto.Texts) ? 0 : decimal.Parse(txtTotalComDescontoProduto.Texts);
            decimal totalServicos = string.IsNullOrEmpty(txtTotalServicos.Texts) ? 0 : decimal.Parse(txtTotalServicos.Texts);
            decimal totalComDescontoServico = string.IsNullOrEmpty(txtTotalComDescontoServico.Texts) ? 0 : decimal.Parse(txtTotalComDescontoServico.Texts);
            decimal totalGeralSemDesconto = string.IsNullOrEmpty(txtTotalGeralSemDesconto.Texts) ? 0 : decimal.Parse(txtTotalGeralSemDesconto.Texts);

            // Calcula o total e percentual de desconto para produtos
            decimal totalDescontoProduto = totalProdutos - totalComDescontoProduto;
            decimal percentualDescontoProduto = totalProdutos > 0 ? (totalDescontoProduto / totalProdutos) * 100 : 0;

            // Calcula o total e percentual de desconto para serviços
            decimal totalDescontoServico = totalServicos - totalComDescontoServico;
            decimal percentualDescontoServico = totalServicos > 0 ? (totalDescontoServico / totalServicos) * 100 : 0;

            // Calcula o desconto total
            decimal totalDescontoGeral = totalDescontoProduto + totalDescontoServico;
            decimal percentualDescontoGeral = totalGeralSemDesconto > 0 ? (totalDescontoGeral / totalGeralSemDesconto) * 100 : 0;

            // Passa os valores para o formulário anterior
            FrmOrdemServico frmOrdemServico = (FrmOrdemServico)this.Owner;
            frmOrdemServico.AtualizarDescontos(totalDescontoProduto, percentualDescontoProduto, totalDescontoServico, percentualDescontoServico, totalDescontoGeral, percentualDescontoGeral);
        }
    }
}