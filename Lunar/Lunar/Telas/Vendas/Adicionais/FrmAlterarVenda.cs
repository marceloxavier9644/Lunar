using Lunar.Telas.PesquisaPadrao;
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

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmAlterarVenda : Form
    {
        VendaFormaPagamento vendaFormaPagamentoSelecionada = new VendaFormaPagamento();
        Venda venda = new Venda();
        VendaFormaPagamentoController vendaFormaPagamentoController = new VendaFormaPagamentoController();
        public FrmAlterarVenda(Venda venda)
        {
            InitializeComponent();
            this.venda = venda;
            preencherCamposVenda();
            carregarFormaPagamentoVenda();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preencherCamposVenda()
        {
            txtNumeroVenda.Texts = venda.Id.ToString();
            txtValorVenda.Texts = venda.ValorFinal.ToString("C");
            txtDataVenda.Texts = venda.DataVenda.ToShortDateString() + " " + venda.DataVenda.ToShortTimeString();
            if (venda.Cliente != null)
            {
                txtCliente.Texts = venda.Cliente.RazaoSocial;
                txtCodCliente.Texts = venda.Cliente.Id.ToString();
            }
            if (venda.Vendedor != null)
            {
                txtVendedor.Texts = venda.Vendedor.RazaoSocial;
                txtCodVendedor.Texts = venda.Vendedor.Id.ToString();
            }

        }
        private void carregarFormaPagamentoVenda()
        {
            IList<VendaFormaPagamento> vendaFormaPagamentos = vendaFormaPagamentoController.selecionarVendaFormaPagamentoPorVenda(venda.Id);
            grid.DataSource = vendaFormaPagamentos;
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            vendaFormaPagamentoSelecionada = (VendaFormaPagamento)grid.SelectedItem;

            if (vendaFormaPagamentoSelecionada != null)
            {
                txtFormaPagamento.Texts = vendaFormaPagamentoSelecionada.FormaPagamento.Descricao;
                txtCodigoFormaPagamento.Texts = vendaFormaPagamentoSelecionada.FormaPagamento.Id.ToString();
                btnPesquisarFormaPagamento.Enabled = true;
            }
        }

        private void btnPesquisarFormaPagamento_Click(object sender, EventArgs e)
        {
            Object formaPagamentoOjeto = new FormaPagamento();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("FormaPagamento", ""))
                {
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("FormaPagamento", "", ref formaPagamentoOjeto))
                    {
                        case DialogResult.Ignore:
                            break;
                        case DialogResult.OK:
                            txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                            txtCodigoFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Id.ToString();

                            vendaFormaPagamentoSelecionada.FormaPagamento.Descricao = txtFormaPagamento.Texts;
                            vendaFormaPagamentoSelecionada.FormaPagamento.Id = int.Parse(txtCodigoFormaPagamento.Texts);
                            grid.View.Refresh();
                            txtFormaPagamento.Texts = "";
                            txtCodigoFormaPagamento.Texts = "";
                            vendaFormaPagamentoSelecionada = null;
                            btnPesquisarFormaPagamento.Enabled = false;
                            break;
                    }
                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }
    }
}
