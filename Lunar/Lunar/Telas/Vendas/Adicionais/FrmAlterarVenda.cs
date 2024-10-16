using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
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

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmAlterarVenda : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        PessoaController pessoaController = new PessoaController();
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
            if (grid.SelectedItem != null)
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
                                vendaFormaPagamentoSelecionada = new VendaFormaPagamento();
                                vendaFormaPagamentoSelecionada.FormaPagamento = new FormaPagamento();
                                vendaFormaPagamentoSelecionada.FormaPagamento.Descricao = txtFormaPagamento.Texts;
                                vendaFormaPagamentoSelecionada.FormaPagamento.Id = int.Parse(txtCodigoFormaPagamento.Texts);

                                var vendaFormaPagamentoSelecionadaNoGrid = grid.CurrentItem as VendaFormaPagamento;
                                vendaFormaPagamentoSelecionadaNoGrid.FormaPagamento = vendaFormaPagamentoSelecionada.FormaPagamento;
                                if (vendaFormaPagamentoSelecionada.FormaPagamento.Cartao == true)
                                    vendaFormaPagamentoSelecionadaNoGrid.Cartao = true;
                                else
                                    vendaFormaPagamentoSelecionadaNoGrid.Cartao = false;

                                Controller.getInstance().salvar(vendaFormaPagamentoSelecionadaNoGrid);

                                grid.View.Refresh();
                                txtFormaPagamento.Texts = "";
                                txtCodigoFormaPagamento.Texts = "";
                                vendaFormaPagamentoSelecionada = null;
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
                                generica.buscarAlertaCadastrado((Pessoa)pessoaObj);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            generica.buscarAlertaCadastrado((Pessoa)pessoaOjeto);
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
        private void PesquisarCliente(string valor)
        {
            IList<Pessoa> listaCliente = pessoaController.selecionarClientesComVariosFiltros(txtCliente.Texts);
            if (listaCliente.Count == 1)
            {
                foreach (Pessoa pessoa in listaCliente)
                {
                    txtCliente.Texts = pessoa.RazaoSocial;
                    txtCodCliente.Texts = pessoa.Id.ToString();
                    generica.buscarAlertaCadastrado(pessoa);
                }
            }
            else
                btnPesquisaCliente.PerformClick();
        }
        private void PesquisarClientePorCodigo(int valor)
        {
            Pessoa pessoa = new Pessoa();
            pessoa.Id = valor;
            pessoa = (Pessoa)pessoaController.selecionar(pessoa);
            if (pessoa != null)
            {
                txtCliente.Texts = pessoa.RazaoSocial;
                txtCodCliente.Texts = pessoa.Id.ToString();
                generica.buscarAlertaCadastrado(pessoa);     
            }
            else
                btnPesquisaCliente.PerformClick();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                PesquisarCliente(txtCliente.Texts.Trim());
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarClientePorCodigo(int.Parse(txtCliente.Texts.Trim()));
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            bool vendedorAlterado = false;
            bool clienteAlterado = false;
            if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
            {
                Pessoa vendedor = new Pessoa();
                vendedor.Id = int.Parse(txtCodVendedor.Texts);
                vendedor = (Pessoa)pessoaController.selecionar(vendedor);
                if (venda.Vendedor == null)
                {
                    venda.Vendedor = vendedor;
                    Controller.getInstance().salvar(venda);
                    vendedorAlterado = true;
                }
                else if (venda.Vendedor.Id != vendedor.Id)
                {
                    venda.Vendedor = vendedor;
                    Controller.getInstance().salvar(venda);
                    vendedorAlterado = true;
                }
            }
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa cliente = new Pessoa();
                cliente.Id = int.Parse(txtCodCliente.Texts);
                cliente = (Pessoa)pessoaController.selecionar(cliente);
                if (venda.Cliente == null)
                {
                    venda.Cliente = cliente;
                    Controller.getInstance().salvar(cliente);
                    clienteAlterado = true;
                }
                else if (venda.Cliente.Id != cliente.Id)
                {
                    venda.Cliente = cliente;
                    Controller.getInstance().salvar(venda);
                    clienteAlterado = true;
                }
                IList<ContaReceber> listaReceber = new List<ContaReceber>();
                ContaReceberController contaReceberController = new ContaReceberController();
                listaReceber = contaReceberController.selecionarContaReceberPorVenda(venda.Id);
                if(listaReceber.Count > 0)
                {
                    foreach(ContaReceber contaReceber in listaReceber)
                    {
                        if (contaReceber.BoletoGerado == true)
                        {
                            GenericaDesktop.ShowAlerta("A fatura a receber possui boleto gerado, não é possivel alterar a fatura a receber! Mas a venda está sendo alterada!");
                        }
                        else
                        {
                            contaReceber.Cliente = cliente;
                            Controller.getInstance().salvar(contaReceber);
                        }
                    }
                }
                IList<Caixa> listaCaixa = new List<Caixa>();
                CaixaController caixaController = new CaixaController();
                listaCaixa = caixaController.selecionarCaixaPorSql("From Caixa Tabela Where Tabela.TabelaOrigem = 'VENDA' and Tabela.IdOrigem = '"+venda.Id+"'");
                if (listaCaixa.Count > 0)
                {
                    foreach (Caixa caixa in listaCaixa)
                    {
                       if(caixa.Pessoa != null)
                        {
                            if (caixa.Descricao.Contains(caixa.Pessoa.RazaoSocial))
                                caixa.Descricao = caixa.Descricao.Replace(caixa.Pessoa.RazaoSocial, cliente.RazaoSocial);
                        }
                        caixa.Pessoa = cliente;
                        Controller.getInstance().salvar(caixa);
                    }
                }
            }
            Controller.getInstance().salvar(venda);
            GenericaDesktop.ShowInfo("Venda alterada com Sucesso!");
        }

        private void btnPesquisarVendedor_Click(object sender, EventArgs e)
        {
            pesquisaVendedor();
        }

        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true"))
                {
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            {
                                txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();

                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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

        private void btnRemoverNfe_Click(object sender, EventArgs e)
        {          
            if (venda.Nfe != null)
                venda.Nfe = null;
            if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir o vinculo ? "))
            {
                Controller.getInstance().salvar(venda);
                GenericaDesktop.ShowInfo("Removido com Sucesso!");
            }
        }
    }
}
