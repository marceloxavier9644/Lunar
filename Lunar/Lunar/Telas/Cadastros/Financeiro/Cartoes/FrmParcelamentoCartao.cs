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
        BandeiraCartao bandeiraCartao = new BandeiraCartao();
        BandeiraCartaoController bandeiraCartaoController = new BandeiraCartaoController();
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        public FrmParcelamentoCartao()
        {
            InitializeComponent();
        }

        private void FrmParcelamentoCartao_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void carregarListaAdquirente()
        {

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

        private bool validaPreenchimento()
        {
            return true;
        }
    }
}
