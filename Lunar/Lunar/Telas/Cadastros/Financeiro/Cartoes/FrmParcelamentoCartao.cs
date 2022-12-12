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
            if (listaBandeiras.Count > 0)
            {
                foreach (BandeiraCartao bandeira in listaBandeiras)
                {
                    DataRow drs = dsBandeira.Tables[0].NewRow();
                    drs.SetField("Codigo", bandeira.Id);
                    drs.SetField("Bandeira", bandeira.Descricao);
                    dsBandeira.Tables[0].Rows.Add(drs);
                }
                comboBandeiraCartao.DisplayMember = "Bandeira";
                comboBandeiraCartao.ValueMember = "Codigo";
                comboBandeiraCartao.DataSource = dsBandeira.Tables[0];
                comboBandeiraCartao.SelectedIndex = 0;
            }
        }
    }
}
