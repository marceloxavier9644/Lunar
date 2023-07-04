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

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmHistoricoOS : Form
    {
        OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
        public FrmHistoricoOS(OrdemServico ordemServico)
        {
            InitializeComponent();

            IList<OrdemServicoPagamento> listaOrdemServicoPagamentos = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
            txtCliente.Texts = ordemServico.Cliente.RazaoSocial;
            txtCodCliente.Texts = ordemServico.Cliente.Id.ToString();
            txtData.Value = ordemServico.DataAbertura;
            txtDataEncerramento.Value = ordemServico.DataEncerramento;
            txtNumeroOS.Texts = ordemServico.Id.ToString();
            if(listaOrdemServicoPagamentos.Count > 0)
            {
                grid.DataSource = listaOrdemServicoPagamentos;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
