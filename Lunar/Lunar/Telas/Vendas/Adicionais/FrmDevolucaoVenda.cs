using Lunar.Telas.Cadastros.Cliente;
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
    public partial class FrmDevolucaoVenda : Form
    {
        Venda venda = new Venda();
        VendaItensController vendaItensController = new VendaItensController();
        public FrmDevolucaoVenda(Venda venda)
        {
            InitializeComponent();
            this.venda = venda;
            preencherCamposVenda();
            carregarProdutosVenda();
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
        private void carregarProdutosVenda()
        {
            IList<VendaItens> vendaItens = vendaItensController.selecionarProdutosPorVenda(venda.Id);
            gridProduto.DataSource = vendaItens;
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtCliente.Texts))
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
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
                    switch (uu.showModal(ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtCliente.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaObj).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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
