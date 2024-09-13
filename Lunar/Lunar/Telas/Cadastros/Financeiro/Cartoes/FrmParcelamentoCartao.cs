using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Financeiro.ParcelamentoCartoes
{
    public partial class FrmParcelamentoCartao : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        BandeiraCartao bandeiraCartao = new BandeiraCartao();
        BandeiraCartaoController bandeiraCartaoController = new BandeiraCartaoController();
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        public FrmParcelamentoCartao()
        {
            InitializeComponent();
            carregarListaAdquirentes();
        }

        private void FrmParcelamentoCartao_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            carregarListaBandeiras();
            carregarListaAdquirentes();
        }

        private void carregarListaBandeiras()
        {
            IList<BandeiraCartao> listaBandeiras = bandeiraCartaoController.selecionarTodasBandeiras();

            if (listaBandeiras != null && listaBandeiras.Count > 0)
            {
                comboBandeiraCartao.DisplayMember = "Descricao"; // Propriedade que será exibida no ComboBox
                comboBandeiraCartao.ValueMember = "Id"; // Propriedade que será usada como valor
                comboBandeiraCartao.DataSource = listaBandeiras; // Definindo a lista diretamente como DataSource
                comboBandeiraCartao.SelectedIndex = 0; // Seleciona o primeiro item
            }
        }

        private void carregarListaAdquirentes()
        {
            IList<AdquirenteCartao> listaAdquirente = adquirenteCartaoController.selecionarTodasAdquirentes();

            if (listaAdquirente != null && listaAdquirente.Count > 0)
            {
                comboAdquirente.DisplayMember = "Descricao"; // Propriedade que será exibida no ComboBox
                comboAdquirente.ValueMember = "Id"; // Propriedade que será usada como valor
                comboAdquirente.DataSource = listaAdquirente; // Definindo a lista diretamente como DataSource
                comboAdquirente.SelectedIndex = 0; // Seleciona o primeiro item
            }
        }

        private void chkAtivarAntecipacao_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkAtivarAntecipacao.Checked == true)
                {
                    txtDiasRecebimentoAntecipacao.Visible = true;
                    txtTaxaAntecipacao.Visible = true;
                }
                else
                {
                    txtDiasRecebimentoAntecipacao.Texts = "0";
                    txtTaxaAntecipacao.Texts = "0";
                    txtDiasRecebimentoAntecipacao.Visible = false;
                    txtTaxaAntecipacao.Visible = false;
                }
            }
            catch
            {

            }
        }

        private void txtQtdParcela_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtQtdParcela.Texts, e);  
        }

        private void txtTaxa_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtTaxa.Texts, e);
        }

        private void txtTarifaAdicional_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtTarifaAdicional.Texts, e);
        }

        private void txtAdicionalPorPacela_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtAdicionalPorPacela.Texts, e);
        }

        private void txtDiasRecebimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtDiasRecebimento.Texts, e);  
        }

        private void txtDiasRecebimentoAntecipacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtDiasRecebimentoAntecipacao.Texts, e); 
        }

        private void txtTaxaAntecipacao_KeyPress(object sender, KeyPressEventArgs e)
        {

            generica.SoNumeroEVirgula(txtTaxaAntecipacao.Texts, e);
            
        }

        private bool validarPreenchimento()
        {
            if (comboAdquirente.SelectedItem == null)
            {
                return false;
            }
            else if (comboBandeiraCartao.SelectedItem == null)
            {
                return false;
            }
            else if (String.IsNullOrEmpty(txtQtdParcela.Texts))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(txtTaxa.Texts))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(txtDiasRecebimento.Texts))
            {
                return false;
            }
            else if (radioDebito.Checked != true && radioCredito.Checked != true)
            {
                return false;
            }
            return true;
        }

        private void set_TaxaCartao()
        {
            if (validarPreenchimento())
            {

            }
        }
    }
}
