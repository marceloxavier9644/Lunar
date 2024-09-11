using Lunar.Telas.CaixaConferencia.ClassesAuxiliaresCaixa;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
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

namespace Lunar.Telas.CaixaConferencia
{
    public partial class FrmFecharCaixa : Form
    {
        CaixaAbertura caixaAbertura = new CaixaAbertura();
        decimal valorNoSistema = 0;
        public FrmFecharCaixa(decimal valorNoSistema, CaixaAbertura caixaAbertura)
        {
            InitializeComponent();
            lblValorSistema.Text = "Pelo Sistema você deve ter " + valorNoSistema.ToString("C");
            this.caixaAbertura = caixaAbertura;
            lblDataCaixaFechar.Text = "CAIXA " + caixaAbertura.DataAbertura.ToShortDateString();
            this.valorNoSistema = valorNoSistema;
        }

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtValorDinheiro.Text))
            {
                caixaAbertura.DataFechamento = DateTime.Now;
                caixaAbertura.EmpresaFilial = Sessao.empresaFilialLogada;
                caixaAbertura.SaldoFinal = valorNoSistema;
                caixaAbertura.DiferencaFechamento = decimal.Parse(txtValorDinheiro.Text) - caixaAbertura.SaldoFinal;
                caixaAbertura.Logado = false;
                caixaAbertura.Status = "FECHADO";
                Controller.getInstance().salvar(caixaAbertura);
                GenericaDesktop.ShowInfo("Caixa fechado com Sucesso!");
                this.Close();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Informe o valor em dinheiro do seu caixa, faça a contagem de todas notas e moedas!");
                txtValorDinheiro.Focus();
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtValorDinheiro.Text))
            {
                decimal diferenca = decimal.Parse(txtValorDinheiro.Text) - valorNoSistema;
                string diferencaString = Math.Abs(diferenca).ToString("C");
                lblDiferenca.Visible = true;
                if (diferenca < 0)
                {
                    lblDiferenca.Text = "Está FALTANDO em sua gaveta " + diferencaString + " em espécie!";
                    if (diferenca > 2 || diferenca < 2)
                        lblDiferenca.ForeColor = Color.Red;
                }
                else if (diferenca > 0)
                {
                    lblDiferenca.Text = "Está SOBRANDO em sua gaveta " + diferencaString + " em espécie!";
                    if (diferenca > 2 || diferenca < 2)
                        lblDiferenca.ForeColor = Color.Purple;
                }
                else
                {
                    lblDiferenca.Text = "Caixa em Dinheiro Correto!";
                    lblDiferenca.ForeColor = Color.Black;
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Informe o valor em dinheiro do seu caixa, faça a contagem de todas notas e moedas!");
            }
        }

        private void txtValorDinheiro_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtValorDinheiro.Text))
            {
                decimal valor = decimal.Parse(txtValorDinheiro.Text);
                txtValorDinheiro.Text = valor.ToString("N2");
            }
        }

        private void FrmFecharCaixa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnCalcular.PerformClick();
                btnFecharCaixa.PerformClick();
            }
        }
    }
}
