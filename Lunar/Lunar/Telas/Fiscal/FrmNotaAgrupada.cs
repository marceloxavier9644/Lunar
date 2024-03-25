using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Lunar.Telas.Fiscal.FrmNfe;
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmNotaAgrupada : Form
    {
        String xmlStrEnvio = "";
        EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
        string codStatusRet = "";
        string arquivoContigencia = "";
        string nomeArquivoContigencia = "";
        GenericaDesktop generica = new GenericaDesktop();
        decimal valorTotal = 0;
        Nfe nfe = new Nfe();
        string numeroNFe = "";
        Pessoa cli = new Pessoa();
        IList<NfeProduto> listaProdutosNFe = new List<NfeProduto>();
        OrdemServico ordem = new OrdemServico();
        Produto produto = new Produto();
        ProdutoController produtoController = new ProdutoController();
        OrdemServicoController ordemServicoController = new OrdemServicoController();
        VendaController vendaController = new VendaController();
        OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
        IList<OrdemServicoProduto> listaProdutos = new List<OrdemServicoProduto>();
        public FrmNotaAgrupada()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
            pesquisaOrdemServico();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if(radioOrdemServico.Checked == true)
                pesquisaOrdemServico();
        }

        private void pesquisaOrdemServico()
        {
            String dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd");
            String dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd");
            IList<OrdemServico> listaOS = ordemServicoController.selecionarOrdemServicoPorSQL("Select * From OrdemServico Tabela Where Tabela.FlagExcluido <> true and Tabela.DataEncerramento between '" + dataInicial + " 00:00:00' and '" + dataFinal + " 23:59:59' and Tabela.Nfe is null");
            gridOrdemServico.DataSource = listaOS;
            gridOrdemServico.Visible = true;
        }

        private void gridOrdemServico_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
            
        }

        private void pesquisarProdutosOrdemServico(OrdemServico ordemServico)
        {
            listaProdutos = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            gridProdutos.DataSource = listaProdutos;
        }

        private void gridOrdemServico_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (gridOrdemServico.RowCount > 0 && gridOrdemServico.SelectedItem != null)
            {
                listaProdutos = new List<OrdemServicoProduto>();
                ordem = new OrdemServico();
                ordem = (OrdemServico)gridOrdemServico.SelectedItem;
                lblOrdemSelecionada.Text = "Ordem de Serviço Selecionada: " + ordem.Id.ToString();
                pesquisarProdutosOrdemServico(ordem);
                somaEnquantoDigita();

                if(ordem.Cliente != null)
                {
                    txtCliente.Texts = ordem.Cliente.RazaoSocial;
                    txtCodCliente.Texts = ordem.Cliente.Id.ToString();
                }
                //FormaPagamento
                OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
                IList<OrdemServicoPagamento> listaOrdemPagamento = new List<OrdemServicoPagamento>();
                listaOrdemPagamento = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordem.Id);
                if(listaOrdemPagamento.Count > 0)
                {
                    foreach(OrdemServicoPagamento ordemServicoPagamento in listaOrdemPagamento)
                    {
                        txtFormaPagamento.Texts = ordemServicoPagamento.FormaPagamento.Descricao;
                        txtCodFormaPagamento.Texts = ordemServicoPagamento.FormaPagamento.Id.ToString();
                    }
                }
            }
            
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as OrdemServicoProduto) != null)
                {
                    if ((e.RowData as OrdemServicoProduto).Produto.Estoque < (e.RowData as OrdemServicoProduto).Quantidade)
                    {
                            e.Style.TextColor = Color.Red;
                    }
                }
            }
            catch
            {

            }
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProdutoPorDescricao(txtPesquisaProduto.Texts.Trim());
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            PesquisarProdutoPorDescricao("");
        }
        private void PesquisarProdutoPorDescricao(string valor)
        {
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);
            IList<Produto> listaProduto = new List<Produto>();
            listaProduto = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProduto.Count == 1)
            {
                foreach (Produto prod in listaProduto)
                {
                    txtPesquisaProduto.Texts = prod.Descricao;
                    txtCodProduto.Texts = prod.Id.ToString();
                    txtQuantidade.Texts = "1";
                    txtValorUnitario.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtValorTotal.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtTotalComDesconto.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    this.produto = prod;
                    if (valorAux.Contains("*"))
                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    //if (prod.Ean.Equals(valor.Trim()))
                    //    inserirItem(this.produto);
                    else
                    {
                        txtQuantidade.Focus();
                        txtQuantidade.Select();
                    }
                }
            }
            else if (listaProduto.Count > 1)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%'"))
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
                        switch (uu.showModal("Produto", "", ref produtoOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmProdutoCadastro form = new FrmProdutoCadastro();
                                if (form.showModalNovo(ref produtoOjeto, false) == DialogResult.OK)
                                {
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidade.Texts = "1";
                                    txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtTotalComDesconto.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    else
                                    {
                                        txtQuantidade.Focus();
                                        txtQuantidade.SelectAll();
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtQuantidade.Texts = "1";
                                txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtTotalComDesconto.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                this.produto = ((Produto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                else
                                {
                                    txtQuantidade.Focus();
                                    txtQuantidade.SelectAll();
                                }
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
                // GenericaDesktop.ShowInfo("Função de pesquisa extra em desenvolvimento...");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtPesquisaProduto.SelectAll();
            }
        }

        private void limparCamposProduto()
        {
            produto = new Produto();
            txtPesquisaProduto.Texts = "";
            txtCodProduto.Texts = "";
            txtQuantidade.Texts = "";
            txtValorUnitario.Texts = "";
            txtValorTotal.Texts = "";
            txtDesconto.Texts = "0,00";
            txtDescontoPercentual.Texts = "0";
            txtTotalComDesconto.Texts = "0,00";
            txtPesquisaProduto.Focus();
        }
        private void btnConfirmarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                Produto produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)produtoController.selecionar(produto);
                OrdemServicoProduto ordemServicoProduto = new OrdemServicoProduto();
                ordemServicoProduto.Acrescimo = 0;
                ordemServicoProduto.Desconto = decimal.Parse(txtDesconto.Texts);
                ordemServicoProduto.DescricaoProduto = txtPesquisaProduto.Texts;
                ordemServicoProduto.Produto = produto;
                ordemServicoProduto.Quantidade = double.Parse(txtQuantidade.Texts);
                ordemServicoProduto.ValorTotal = decimal.Parse(txtTotalComDesconto.Texts);
                ordemServicoProduto.ValorUnitario = decimal.Parse(txtValorUnitario.Texts);
                ordemServicoProduto.Produto = produto;
                ordemServicoProduto.OrdemServico = ordem;
                listaProdutos.Add(ordemServicoProduto);
                gridProdutos.DataSource = null;
                gridProdutos.DataSource = listaProdutos;
                gridProdutos.Refresh();
                limparCamposProduto();
                somaEnquantoDigita();
            }
            catch(Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodProduto.Texts))
                    {
                        produto.Id = int.Parse(txtCodProduto.Texts.Trim());
                        produto = (Produto)produtoController.selecionar(produto);
                        txtPesquisaProduto.Texts = produto.Descricao;
                        txtQuantidade.Focus();
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Produto não encontrado!");
                }
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtValorUnitario.Focus();
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            try
            {
                calculaTotaisProduto();
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade, valor e desconto");
            }
        }
        private void calculaTotaisProduto()
        {
            txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            txtTotalComDesconto.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts) - decimal.Parse(txtDesconto.Texts));
        }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDescontoPercentual.Focus();
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e)
        {
            try
            {
                calculaTotaisProduto();
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade, valor e desconto");
            }
        }

        private void txtDescontoPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDesconto.Focus();
        }

        private void txtDescontoPercentual_Leave(object sender, EventArgs e)
        {
            descontoEmPercentual();
        }

        private void descontoEmPercentual()
        {
            try
            {
                decimal valorOriginal = decimal.Parse(txtValorTotal.Texts);
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDescontoPercentual.Texts))
                {
                    valorDesconto = valorOriginal * decimal.Parse(txtDescontoPercentual.Texts) / 100;
                    txtDesconto.Texts = valorDesconto.ToString("N2");
                    txtTotalComDesconto.Texts = (valorOriginal - valorDesconto).ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;
                    txtDesconto.Focus();
                    if (valorDesconto > decimal.Parse(txtValorTotal.Texts))
                        throw new Exception("Valor do desconto não pode ser maior que o valor do produto");
                }
            }
            catch (Exception erro)
            {
                txtDesconto.Texts = "0,00";
                txtDescontoPercentual.Texts = "0";
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        private void txtDesconto_Leave(object sender, EventArgs e)
        {
            descontoEmValor();
        }
        private void descontoEmValor()
        {
            try
            {
                decimal valorOriginal = decimal.Parse(txtValorTotal.Texts);
                decimal valorDesconto = 0;
                if (!string.IsNullOrEmpty(txtDesconto.Texts))
                {
                    valorDesconto = decimal.Parse(txtDesconto.Texts);
                    txtDescontoPercentual.Texts = (valorDesconto * 100 / valorOriginal).ToString("N2");
                    txtTotalComDesconto.Texts = (valorOriginal - valorDesconto).ToString("N2");
                    txtDesconto.Texts = valorDesconto.ToString("N2");
                    txtDescontoPercentual.TextAlign = HorizontalAlignment.Center;
                    if (valorDesconto > decimal.Parse(txtValorTotal.Texts))
                        throw new Exception("Valor do desconto não pode ser maior que o valor do produto");
                }

            }
            catch (Exception erro)
            {
                txtDesconto.Texts = "0,00";
                txtDescontoPercentual.Texts = "0";
                if (produto != null)
                {
                    if (produto.Id > 0)
                        GenericaDesktop.ShowAlerta(erro.Message);
                }
            }
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnConfirmarProduto.PerformClick();
        }

        private void somaEnquantoDigita()
        {
            valorTotal = 0;
            try
            {
                if (gridProdutos.RowCount > 0)
                {
                    foreach(OrdemServicoProduto ordemServProduto in listaProdutos)
                    {
                        valorTotal = valorTotal + ordemServProduto.ValorTotal;
                    }
                    lblTotalSelecionado.Text = "Valor Total: " + valorTotal.ToString("C2", CultureInfo.CurrentCulture);
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro na soma dos produtos " + erro.Message);
            }
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedIndex >= 0)
            {
                listaProdutos.RemoveAt(gridProdutos.SelectedIndex);
                gridProdutos.DataSource = null;
                gridProdutos.DataSource = listaProdutos;
                gridProdutos.Refresh();
                limparCamposProduto();
                somaEnquantoDigita();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro você deve clicar na linha do produto que deseja remover!");
            }
 
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            pesquisaCliente("");
        }

        private void pesquisaCliente(string valor)
        {
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(valor))
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

        private void pesquisaFormaPagamento()
        {
            Object formaPagamentoOjeto = new FormaPagamento();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("FormaPagamento", ""))
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
                    switch (uu.showModal("FormaPagamento", "", ref formaPagamentoOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref formaPagamentoOjeto) == DialogResult.OK)
                            {
                                txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                                txtCodFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                            txtCodFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Id.ToString();
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

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaCliente(txtCliente.Texts);
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = int.Parse(txtCodCliente.Texts.Trim());
                        pessoa = (Pessoa)PessoaController.getInstance().selecionar(pessoa);
                        if (pessoa.Id > 0)
                        {
                            txtCliente.Texts = pessoa.RazaoSocial;
                        }
                    }
                }
                catch (Exception erro)
                {
                    GenericaDesktop.ShowAlerta(erro.Message);
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            pesquisaFormaPagamento();
        }
        private void abrirTelaEditarCliente(Pessoa cliente)
        {
            PessoaController pessoaController = new PessoaController();
            FrmClienteCadastro frm = new FrmClienteCadastro(cliente);
            frm.ShowDialog();
        }
        private bool validarClienteNFCe(Pessoa pessoa)
        {
            bool validacao = false;
            if (String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                GenericaDesktop.ShowAlerta("Para NFe é obrigatório selecionar um cliente/Fornecedor");
                validacao = false;
            }
            if (pessoa.Cnpj.Length == 11)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length < 11)
            {
                GenericaDesktop.ShowAlerta("Para NFe o cliente selecionado deve ter CPF preenchido corretamente");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
                if (String.IsNullOrEmpty(pessoa.Cnpj))
                    return validacao;
            }
            else if (pessoa.Cnpj.Length == 14)
            {
                //GenericaDesktop.ShowAlerta("Em uma NFe o cliente não pode ser pessoa jurídica, caso precise identificar a pessoa jurídica faça a emissão de uma NFe modelo 55");
                validacao = true;
            }
            if (!String.IsNullOrEmpty(pessoa.RazaoSocial))
                validacao = true;
            if (pessoa.EnderecoPrincipal == null)
            {
                GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            if (pessoa.EnderecoPrincipal != null)
            {
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Logradouro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NOME DA RUA)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (BAIRRO)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Numero))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NUMERO)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Cep))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (CEP)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
            }
            return validacao;
        }

        private void carregarListaProdutos()
        {
            if (gridProdutos.RowCount > 0)
            {
                listaProdutosNFe = new List<NfeProduto>();
               
                int i = 0;
                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
                {
                    i++;
                    NfeProduto nfeProduto = new NfeProduto();
                    produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(ordemServicoProduto.Produto.Id, Sessao.empresaFilialLogada);
                    double quantidade = ordemServicoProduto.Quantidade;
                    decimal descontoItem = decimal.Parse(ordemServicoProduto.Desconto.ToString());
                    produto.ValorVenda = ordemServicoProduto.ValorUnitario;
                    nfeProduto.Item = i.ToString();
                    nfeProduto.Produto = produto;
                    nfeProduto.QCom = quantidade.ToString();
                    nfeProduto.Ncm = produto.Ncm;
                    nfeProduto.Cest = produto.Cest;
                    nfeProduto.Cfop = produto.CfopVenda.Trim();
                    nfeProduto.VUnCom = ordemServicoProduto.ValorUnitario;
                    if (cli != null)
                    {
                        if (cli.Id > 0)
                        {
                            if(cli.EnderecoPrincipal != null)
                            {
                                if(cli.EnderecoPrincipal.Cidade != null)
                                {
                                    if(cli.EnderecoPrincipal.Cidade.Estado.Uf != Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf)
                                    {
                                        nfeProduto.Cfop = "6" + produto.CfopVenda.Substring(1, 3);
                                    }
                                    else
                                    {
                                        nfeProduto.Cfop = "5" + produto.CfopVenda.Substring(1, 3);
                                    }
                                }
                            }
                        }
                    }
                    nfeProduto.CProd = produto.Id.ToString();
                    nfeProduto.Nfe = null;//
                    nfeProduto.VProd = ordemServicoProduto.ValorUnitario * decimal.Parse(quantidade.ToString());
                    nfeProduto.QTrib = quantidade;
                    nfeProduto.VDesc = descontoItem;
                    nfeProduto.DescricaoInterna = produto.Descricao.Trim();
                    nfeProduto.XProd = produto.Descricao.Trim();
                    nfeProduto.CodigoInterno = produto.Id.ToString();
                    nfeProduto.CEan = "";
                    nfeProduto.CEANTrib = "";
                    nfeProduto.CfopEntrada = nfeProduto.Cfop;
                    nfeProduto.AliqCofins = "0";
                    nfeProduto.AliqIpi = "0";
                    nfeProduto.AliqPis = "0";
                    nfeProduto.BaseCofins = 0;
                    nfeProduto.BaseIpi = 0;
                    nfeProduto.BasePis = 0;
                    nfeProduto.PRedBC = "0";
                    nfeProduto.PST = "0";
                    nfeProduto.VBCST = 0;
                    nfeProduto.VICMSSt = 0;
                    nfeProduto.PFCP = "0";
                    nfeProduto.VBCFCPST = 0;
                    nfeProduto.VFCPST = 0;
                    nfeProduto.VBCSTRet = 0;
                    nfeProduto.VICMSSTRet = 0;
                    nfeProduto.PFCPST = "0";
                    nfeProduto.VBC = 0;
                    nfeProduto.PICMS = "0";
                    nfeProduto.VICMS = 0;

                    nfeProduto.CodAnp = produto.CodAnp;
                    nfeProduto.CodEnqIpi = produto.EnqIpi;
                    nfeProduto.CodSeloIpi = produto.CodSeloIpi;
                    nfeProduto.CstCofins = produto.CstCofins;
                    nfeProduto.CstIcms = produto.CstIcms;
                    nfeProduto.CstIpi = produto.CstIpi;
                    nfeProduto.CstPis = produto.CstPis;
                    string orig = "0";
                    if (!String.IsNullOrEmpty(produto.OrigemIcms))
                        orig = produto.OrigemIcms;
                    nfeProduto.OrigemIcms = orig;
                    nfeProduto.OutrosIcms = 0;
                    nfeProduto.TPag = "";
                    nfeProduto.UCom = produto.UnidadeMedida.Sigla;
                    nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorAcrescimo = 0;
                    nfeProduto.ValorCofins = 0;
                    nfeProduto.ValorDesconto = descontoItem;
                    nfeProduto.ValorFinal = ordemServicoProduto.ValorUnitario * decimal.Parse(quantidade.ToString()) - descontoItem;
                    nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorIpi = 0;
                    nfeProduto.ValorPis = 0;
                    nfeProduto.ValorProduto = 0;

                    nfeProduto.VFrete = 0;
                    nfeProduto.VOutro = 0;
                    nfeProduto.VSeguro = 0;
                    nfeProduto.VipiDevolvido = 0;
                    listaProdutosNFe.Add(nfeProduto);
                }
            }
            else
                GenericaDesktop.ShowAlerta("Você deve inserir pelo menos 1 produto");
        }

        private Nfe alimentaNfe()
        {
            nfe = new Nfe();
            NfeController nfeController = new NfeController();
            nfe.NNf = Sessao.parametroSistema.ProximoNumeroNFe;           
            nfe.CDv = "";
            nfe.IndIntermed = "1";
            nfe.CMunFg = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            nfe.CNf = "";
            nfe.CnpjEmitente = Sessao.empresaFilialLogada.Cnpj;
            nfe.Conciliado = true;
            nfe.CUf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            nfe.DataLancamento = DateTime.Now;
            nfe.DataEmissao = DateTime.Now;
            nfe.DhEmi = DateTime.Now.ToShortDateString();
            nfe.DhSaiEnt = DateTime.Now.ToShortDateString();
            nfe.EmpresaFilial = Sessao.empresaFilialLogada;
            nfe.Lancada = true;
            nfe.TipoOperacao = "S";
            nfe.Modelo = "55";
            nfe.NatOp = "VENDA";
            nfe.NaturezaOperacao = null;
            
            nfe.RazaoEmitente = Sessao.empresaFilialLogada.RazaoSocial;

            //nao precisamos alimentar os valores totais agora, pois a soma nos itens vai armazenar essa informacao e gravar na nota posteriormente
            nfe.VBc = 0;
            nfe.VIcms = 0;
            nfe.VIcmsDeson = 0;
            nfe.VFcp = 0;
            nfe.VBcst = 0;
            nfe.VSt = 0;
            nfe.VFcpst = 0;
            nfe.VFcpstRet = 0;
            nfe.VFrete = decimal.Parse("0");
            decimal somaProdutos = 0;
            decimal somaDesconto = 0;
            decimal totalComDesconto = 0;
            foreach (OrdemServicoProduto ordemServProduto in listaProdutos)
            {
                somaProdutos = ((somaProdutos + ordemServProduto.ValorTotal) - ordemServProduto.Desconto);
                somaDesconto = somaDesconto + ordemServProduto.Desconto;
                totalComDesconto = totalComDesconto + ordemServProduto.ValorTotal;
            }
            nfe.VProd = somaProdutos;
            nfe.VSeg = decimal.Parse("0");
            nfe.VDesc = somaDesconto;
            nfe.VIi = 0;
            nfe.VIpi = decimal.Parse("0");
            nfe.VIpiDevol = decimal.Parse("0");
            nfe.VPis = 0;
            nfe.VCofins = 0;
            nfe.VOutro = decimal.Parse("0");
            nfe.VNf = totalComDesconto;
            nfe.VTotTrib = 0;

            nfe.Status = "Preparando Envio...";
            nfe.CodStatus = "0";
            nfe.Chave = "";
            nfe.Serie = Sessao.parametroSistema.SerieNFe;

            nfe.TpNf = "1";

            if (cli.EnderecoPrincipal != null)
            {
                if (cli.EnderecoPrincipal.Cidade.Estado.Id == Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Id)
                    nfe.IdDest = "1";
                else
                    nfe.IdDest = "2";
            }
            nfe.TpImp = "1";
            nfe.TpEmis = "1";
            if (Sessao.parametroSistema.AmbienteProducao == true)
                nfe.TpAmb = "1";
            else
                nfe.TpAmb = "2";
            nfe.FinNfe = "1";
            nfe.IndFinal = "1";
            nfe.IndPres = "1";
            nfe.InfCpl = Sessao.parametroSistema.InformacaoAdicionalNFe;
            nfe.ProcEmi = "0";
            nfe.VerProc = "1.0|Lunar";
            nfe.ModFrete = "9";
            nfe.Manifesto = "";
            nfe.Protocolo = "";
            nfe.PossuiCartaCorrecao = false;
            if (cli != null)
            {
                nfe.Cliente = cli;
                nfe.Destinatario = cli.RazaoSocial;
                nfe.CnpjDestinatario = GenericaDesktop.RemoveCaracteres(cli.Cnpj);
            }
            nfe.CodigoAntt = "";
            nfe.DataSaida = DateTime.Now;
            nfe.Transportadora = null;
            nfe.Volume = "";
            nfe.Xml = "";
            nfe.Placa = "";
            nfe.Especie = "";
            nfe.Marca = "";
            nfe.PesoBruto = "";
            nfe.PesoLiquido = "";
            nfe.NotaAgrupada = true;
            Controller.getInstance().salvar(nfe);
            return nfe;
        }
        private void btnGerarNFe_Click(object sender, EventArgs e)
        {
            bool validaCliente = false;
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                cli = new Pessoa();
                cli.Id = int.Parse(txtCodCliente.Texts);
                cli = (Pessoa)Controller.getInstance().selecionar(cli);
                validaCliente = validarClienteNFCe(cli);

                ValidadorNotaSaida validador = new ValidadorNotaSaida();
                if (validaCliente == true)
                {
                    EmitirNfe2 emitirNFe2 = new EmitirNfe2();
                    carregarListaProdutos();
                    if (validador.validarProdutosNota(listaProdutosNFe))
                    {
                        ParametroSistema param = new ParametroSistema();
                        param.Id = 1;
                        param = (ParametroSistema)Controller.getInstance().selecionar(param);
                        Sessao.parametroSistema = param;
                        numeroNFe = Sessao.parametroSistema.ProximoNumeroNFe;

                        //Ja alimenta o proximo numero nos parametros
                        Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(numeroNFe) + 1).ToString();
                        Controller.getInstance().salvar(Sessao.parametroSistema);


                        TNFeInfNFeTranspModFrete frete = retornaFrete();
                        nfe = alimentaNfe();

                        NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                        nfeProdutoDAO.excluirProdutosNfeParaAtualizar(nfe.Id.ToString());
                        Frete fr = new Frete();
                        FormaPagamento forma = new FormaPagamento();
                        forma.Id = int.Parse(txtCodFormaPagamento.Texts);
                        forma = (FormaPagamento)FormaPagamentoController.getInstance().selecionar(forma);
                        EmitirNfe3 emitirNFe3 = new EmitirNfe3();
                        string xmlStrEnvio = emitirNFe3.gerarXML(nfe, fr, true, null, null, listaProdutosNFe, null, forma, true);
                        if(ordem != null)
                        {
                            if(ordem.Id > 0)
                            {
                                ordem.Nfe = nfe;
                                Controller.getInstance().salvar(ordem);
                            }
                        }
                        if (!String.IsNullOrEmpty(xmlStrEnvio))
                        {
                            enviarXMLNFeParaApi(xmlStrEnvio);
                        }
                        
                        listaProdutos = new List<OrdemServicoProduto>();
                        listaProdutosNFe = new List<NfeProduto>();
                        ordem = new OrdemServico();
                        gridProdutos.DataSource = null;
                        gridProdutos.Refresh();
                        btnPesquisar.PerformClick();
                    }
                }
            }
        }

        private TNFeInfNFeTranspModFrete retornaFrete()
        {
            TNFeInfNFeTranspModFrete tipoFrete = TNFeInfNFeTranspModFrete.Item9;
            return tipoFrete;
        }

        private void enviarXMLNFeParaApi(string xmlNfe)
        {
            RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
            NfeStatus nfeStatus = new NfeStatus();
            string caminhoSalvarXml = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFe, Sessao.parametroSistema.SerieNFe);

            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            string codStatusRet = "";
            gravarXMLNaPasta(xmlNfe, nfe.NNf, @"\XML\Tentativa\NFe\", nfe.NNf + ".xml");
            if (GenericaDesktop.possuiConexaoInternet())
            {
                String retorno = NSSuite.emitirNFeSincrono(xmlNfe, "xml", Sessao.empresaFilialLogada.Cnpj, "XP", "2", caminhoSalvarXml, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);
                //Armazenar nsNRec
                if (!String.IsNullOrEmpty(retornoNFCe.nsNRec))
                    nfe.NsNrec = retornoNFCe.nsNRec;

                if (!String.IsNullOrEmpty(retornoNFCe.cStat))
                    codStatusRet = retornoNFCe.cStat;
                else
                    codStatusRet = retornoNFCe.statusEnvio;

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    nfeStatus = new NfeStatus();
                    nfeStatus.Id = 1;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    nfe.Lancada = true;
                    Controller.getInstance().salvar(nfe);
                    armazenaXmlAutorizadoNoBanco();
                    //ATUALIZAR ESTOQUE
                    NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                    IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                    foreach (NfeProduto nfeP in listaProd)
                    {
                        generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + "VENDA", nfe.Cliente, DateTime.Now, null);
                        generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + "VENDA", nfe.Cliente, DateTime.Now, null);
                    }
                    GenericaDesktop.ShowInfo("Nota autorizada!");
                    //EnviaXML PAINEL LUNAR 
                    try
                    {
                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                        byte[] arquivo;
                        using (var stream = new FileStream(caminhoSalvarXml + nfe.Chave + "-procNFe.xml", FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new BinaryReader(stream))
                            {
                                arquivo = reader.ReadBytes((int)stream.Length);
                                var ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                                {
                                    nfe.Nuvem = true;
                                    Controller.getInstance().salvar(nfe);
                                }
                            }
                        }
                    }
                    catch
                    {

                    }

                    if (File.Exists(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                }
                //Erro interno ao processar a requisicao // geralmente falha sefaz, aguardamos 5 segundos e verificamos novo retorno
                else if (retornoNFCe.motivo.Contains("Documento") || retornoNFCe.motivo.Contains("Sefaz") || retornoNFCe.motivo.Contains("sefaz") || retornoNFCe.motivo.Contains("Erro interno ao processar a requisicao"))
                {
                    Thread.Sleep(5000);

                    //CONSULTA NSNREC
                    ConsStatusProcessamentoReq consStatusProcessamentoReq = new ConsStatusProcessamentoReq();
                    consStatusProcessamentoReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                    consStatusProcessamentoReq.nsNRec = nfe.NsNrec;
                    if (Sessao.parametroSistema.AmbienteProducao == true)
                        consStatusProcessamentoReq.tpAmb = "1";
                    else
                        consStatusProcessamentoReq.tpAmb = "2";


                    String retornoConsulta = NSSuite.consultarStatusProcessamento(nfe.Modelo, consStatusProcessamentoReq);
                    if (retornoConsulta != null)
                        retConsulta = JsonConvert.DeserializeObject<RetConsultaProcessamentoNF>(retornoConsulta);

                    if (retConsulta.xMotivo != null)
                    {
                        if (retConsulta.xMotivo.Contains("Autorizado o uso"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 1;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            armazenaXmlAutorizadoNoBanco();
                            generica.gravarXMLNaPasta(retConsulta.xml, retConsulta.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");

                            //ATUALIZAR ESTOQUE
                            NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                            IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                            foreach (NfeProduto nfeP in listaProd)
                            {
                                generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + "VENDA", nfe.Cliente, DateTime.Now, null);
                                generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + "VENDA", nfe.Cliente, DateTime.Now, null);

                            }

                            if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                            {
                                FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                frmPDF.ShowDialog();
                            }

                            else
                            {
                                NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                                nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                if (nFeDownloadProc55 != null)
                                {
                                    GenericaDesktop gen = new GenericaDesktop();
                                    gen.gerarPDF3(nfe, nFeDownloadProc55.pdf, nfe.Chave, true);
                                }
                            }
                            if (!String.IsNullOrEmpty(retConsulta.xMotivo))
                                GenericaDesktop.ShowInfo(" (" + retConsulta.cStat + ") > " + retConsulta.xMotivo);
                        }
                        else if (retConsulta.xMotivo.Contains("Sem retorno de status da sefaz"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 2;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                //nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                                Controller.getInstance().salvar(nfe);
                                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retConsulta.cStat + " " + retConsulta.xMotivo + ", na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                            }
                        }
                    }
                }
                String erros = "";
                if (retornoNFCe.erros != null)
                {

                    if (retornoNFCe.erros.Count > 0)
                    {
                        foreach (string msgErro in retornoNFCe.erros)
                        {
                            if (String.IsNullOrEmpty(erros))
                                erros = msgErro;
                            else
                                erros = erros + "\n" + msgErro;
                        }
                    }
                }
                if (String.IsNullOrEmpty(erros))
                    erros = retornoNFCe.motivo;
                try { GenericaDesktop.ShowAlerta(erros); } catch { };
                //se a nota continua nao autorizada, verifica se teve erros
                if (String.IsNullOrEmpty(nfe.Chave) || nfe.Chave.Equals("123"))
                {
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 2;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    if (!String.IsNullOrEmpty(retConsulta.chNFe))
                    {
                        //nfe.Protocolo = retConsulta.nProt;
                        nfe.Chave = retConsulta.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta("Corrija a nota e tente reenviar posteriormente: (" + retConsulta.cStat + " " + retConsulta.xMotivo + ") na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                }
                this.Close();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: Verifique sua conexão com a internet, após normalizar acesse o menu de gerenciamento de notas para reenviar a mesma!");
                this.Close();

            }
        }

        private void armazenaXmlAutorizadoNoBanco()
        {
            NFCeDownloadProc nota = new NFCeDownloadProc();
            if (nfe.Modelo.Equals("65"))
            {
                nota = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

                if (nota.motivo.Contains("SUCESSO") || nota.motivo.Contains("sucesso") || nota.motivo.Contains("Sucesso"))
                {
                    Genericos genericosNF = new Genericos();
                    var nfe1 = Genericos.LoadFromXMLString<TNfeProc>(nota.nfeProc.xml);
                    genericosNF.gravarXMLNoBanco(nfe1, 0, "S", this.nfe.Id);
                }
            }
            else if (nfe.Modelo.Equals("55"))
            {
                NFeDownloadProc55 nota55 = new NFeDownloadProc55();
                nota55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

                if (nota55.motivo.Contains("SUCESSO") || nota55.motivo.Contains("sucesso") || nota55.motivo.Contains("Sucesso"))
                {
                    Genericos genericosNF = new Genericos();
                    var notaLida55 = Genericos.LoadFromXMLString<TNfeProc>(nota55.xml);
                    genericosNF.gravarXMLNoBanco(notaLida55, 0, "S", this.nfe.Id);

                    string caminhoArquivo = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    string pastaArquivo = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas";
                    if (!File.Exists(caminhoArquivo))
                    {
                        gravarXMLNaPasta(nota55.xml, this.nfe.NNf, pastaArquivo, nfe.Chave + "-procNFe.xml", false);
                    }
                }
            }
        }

        private void gravarXMLNaPasta(string xml, string numeroNFe, string caminhoArmazenamento, string nomeArquivo)
        {
            if (!nomeArquivo.Contains(".xml"))
                nomeArquivo = nomeArquivo + ".xml";
            if (!Directory.Exists(caminhoArmazenamento))
            {
                Directory.CreateDirectory(caminhoArmazenamento);
            }
            string arquivo = caminhoArmazenamento + nomeArquivo;
            if (!File.Exists(arquivo))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
            else
            {
                File.Delete(arquivo);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
        }

        private void btnGerarNFCe_Click(object sender, EventArgs e)
        {
            ParametroSistema param = new ParametroSistema();
            param.Id = 1;
            param = (ParametroSistema)Controller.getInstance().selecionar(param);
            Sessao.parametroSistema = param;

            String xmlStrEnvio = "";
            nfe = new Nfe();
            bool validaCliente = true;
            if (!string.IsNullOrEmpty(txtCodCliente.Texts))
            {
                if (!string.IsNullOrEmpty(txtCodFormaPagamento.Texts))
                {
                    cli = new Pessoa();
                    cli.Id = int.Parse(txtCodCliente.Texts);
                    cli = (Pessoa)PessoaController.getInstance().selecionar(cli);
                    validaCliente = validarClienteNFCe(cli);
                    if (validaCliente == false && GenericaDesktop.ShowConfirmacao("Deseja emitir a nota sem identificar o consumidor?"))
                    {
                        cli = null;
                        validaCliente = true;
                    }
                    ValidadorNotaSaida validador = new ValidadorNotaSaida();
                    if (validaCliente == true)
                    {
                        //Emitir NFCe pela nova classe
                        EmitirNFCe emitirNFCe = new EmitirNFCe();
                        carregarListaProdutos();
                        try
                        {
                            if (validador.validarProdutosNota(listaProdutosNFe))
                            {
                                numeroNFe = Sessao.parametroSistema.ProximoNumeroNFCe;

                                //Ja alimenta o proximo numero nos parametros
                                Sessao.parametroSistema.ProximoNumeroNFCe = (int.Parse(numeroNFe) + 1).ToString();
                                Controller.getInstance().salvar(Sessao.parametroSistema);

                                FormaPagamento formaPagamento = new FormaPagamento();
                                formaPagamento.Id = int.Parse(txtCodFormaPagamento.Texts);
                                formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);

                                decimal valorProdutos = 0;
                                decimal valorDesconto = 0;
                                decimal valorTotalComDesconto = 0;
                                foreach (NfeProduto nfeProduto in listaProdutosNFe)
                                {
                                    if (nfeProduto.ValorProduto > 0)
                                        valorProdutos = valorProdutos + nfeProduto.ValorProduto;
                                    else
                                        valorProdutos = (valorProdutos + (nfeProduto.VUnCom * decimal.Parse(nfeProduto.QCom)));
                                    valorDesconto = valorDesconto + nfeProduto.ValorDesconto;
                                    valorTotalComDesconto = ((valorTotalComDesconto + nfeProduto.ValorFinal) - valorDesconto);
                                }
                                nfe.Modelo = "65";
                                nfe.VProd = valorProdutos;
                                nfe.VDesc = valorDesconto;
                                nfe.VNf = valorTotalComDesconto;
                                nfe.NotaAgrupada = true;
                                try { xmlStrEnvio = emitirNFCe.gerarXMLNfce(nfe.VProd, nfe.VNf, nfe.VDesc, numeroNFe, listaProdutosNFe, cli, null, null, formaPagamento); } catch (Exception err) { GenericaDesktop.ShowAlerta(err.Message); }

                                if (!String.IsNullOrEmpty(xmlStrEnvio))
                                {
                                    enviarXMLNFCeParaApi(xmlStrEnvio);
                                }
                                if (ordem != null)
                                {
                                    if (ordem.Id > 0)
                                    {
                                        if (ordem.ValorProduto == nfe.VNf)
                                        {
                                            ordem.Nfe = nfe;
                                            Controller.getInstance().salvar(ordem);
                                        }
                                    }
                                }
                                //Nfe pagamento
                                NfePagamento nfePagamento = new NfePagamento();
                                nfePagamento.Descricao = "PAGAMENTO NFE " + nfe.NNf;
                                nfePagamento.Origem = "NOTA FISCAL AGRUPADA";
                                nfePagamento.Nfe = nfe;
                                nfePagamento.FormaPagamento = formaPagamento;

                                Controller.getInstance().salvar(nfePagamento);

                                listaProdutos = new List<OrdemServicoProduto>();
                                listaProdutosNFe = new List<NfeProduto>();
                                ordem = new OrdemServico();
                                gridProdutos.DataSource = null;
                                gridProdutos.Refresh();
                                btnPesquisar.PerformClick();
                            }
                        }
                        catch (Exception erro)
                        {
                            ordem.Nfe = null;
                            Controller.getInstance().salvar(ordem);
                            GenericaDesktop.ShowErro(erro.Message);
                        }
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Preencha uma forma de Pagamento!");
            }
            else
                GenericaDesktop.ShowAlerta("Preencha um cliente!");
        }
        private void atualizarProximoNumeroNota()
        {
            //ATUALIZA NUMERO DA NOTA 
            ParametroSistema param = new ParametroSistema();
            param = Sessao.parametroSistema;
            if (nfe.Modelo.Equals("65"))
                param.ProximoNumeroNFCe = (int.Parse(nfe.NNf) + 1).ToString();
            if (nfe.Modelo.Equals("55"))
                param.ProximoNumeroNFe = (int.Parse(nfe.NNf) + 1).ToString();
            Controller.getInstance().salvar(param);
            Sessao.parametroSistema = param;
        }
        private void enviarXMLNFCeParaApi(string xmlNfce)
        {
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFCePorNumeroESerie(numeroNFe, Sessao.parametroSistema.SerieNFCe);
            string caminhoXML = @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            String codStatusRet = "";
            gravarXMLNaPasta(xmlNfce, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", false);
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string ambiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    ambiente = "1";
                String retorno = NSSuite.emitirNFCeSincrono(xmlNfce, "xml", Sessao.empresaFilialLogada.Cnpj, ambiente, caminhoXML, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 1;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    nfe.Lancada = true;
                    Controller.getInstance().salvar(nfe);

                    btnPesquisar.PerformClick();
                    //Estoque
                    GenericaDesktop generica = new GenericaDesktop();
                    foreach (NfeProduto nfeProduto in listaProdutosNFe)
                    {
                        generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), false, "ORDEMSERVICO", "NF EMITIDA AGRUPADA " + ordem.Id.ToString(), nfe.Cliente, DateTime.Now, null);
                    }

                    //EnviaXML PAINEL LUNAR 
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    byte[] arquivo;
                    using (var stream = new FileStream(caminhoXML + nfe.Chave + "-procNFCe.xml", FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            arquivo = reader.ReadBytes((int)stream.Length);
                            var ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                            {
                                nfe.Nuvem = true;
                                Controller.getInstance().salvar(nfe);
                            }
                            //enviar de novo
                            else
                            {
                                ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                                {
                                    nfe.Nuvem = true;
                                    Controller.getInstance().salvar(nfe);
                                }
                            }
                        }
                    }

                    armazenaXmlAutorizadoNoBanco();
                    GenericaDesktop.ShowInfo("Nota Fiscal autorizada!");

                    if (File.Exists(caminhoXML + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoXML + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                    else
                    {
                        if (nfe.Modelo.Equals("65"))
                        {
                            NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();
                            nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                            if (nFCeDownloadProc != null)
                            {
                                GenericaDesktop genericaDesktop = new GenericaDesktop();
                                genericaDesktop.gerarPDF3(nfe, nFCeDownloadProc.pdf, nfe.Chave, true);
                            }
                        }
                    }
                }
                //Falha conexao
                else if (retornoNFCe.motivo.Contains("timeout") || retornoNFCe.cStat.Equals("999"))
                {
                    //gerar em contigencia
                    gravarXMLNaPasta(xmlNfce, numeroNFe, @"\XML\Tentativa\NFCe\", numeroNFe, true);
                }

                else
                {
                    String erros = "";
                    if (retornoNFCe.erros != null)
                    {
                        for (int xx = 0; xx < retornoNFCe.erros.Count; xx++)
                        {
                            erros = erros + " " + retornoNFCe.erros[xx];
                        }
                    }
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 2;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            //Se nao tem internet gera em contigencia tb
            else
            {
                gravarXMLNaPasta(xmlStrEnvio, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", true);
            }
            if (retornoNFCe.motivo.Contains("Duplicidade de NF-e com diferença na Chave"))
            {
                //nfe.NNf = (int.Parse(nfe.NNf) + 1).ToString();
                nfe.Status = retornoNFCe.motivo;
                nfe.CodStatus = retornoNFCe.cStat;
                NfeStatus nfeStatus = new NfeStatus();
                nfeStatus.Id = 2;
                nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                if (nfeStatus != null)
                    nfe.NfeStatus = nfeStatus;

                Controller.getInstance().salvar(nfe);
                GenericaDesktop.ShowAlerta("Duplicidade de NF-e com diferença na Chave, reenvie a nota no painel de monitoramento fiscal");
            }
        }

        private void gravarXMLNaPasta(string xml, string numeroNFCe, string caminhoArmazenamento, string nomeArquivo, bool emiteContigencia)
        {
            if (!nomeArquivo.Contains(".xml"))
                nomeArquivo = nomeArquivo + ".xml";
            if (!Directory.Exists(caminhoArmazenamento))
            {
                Directory.CreateDirectory(caminhoArmazenamento);
            }
            if (!(caminhoArmazenamento.Length - 1).Equals(@"\"))
            {
                //caminhoArmazenamento = caminhoArmazenamento + @"\";

            }
            string arquivo = caminhoArmazenamento + nomeArquivo;
            if (!File.Exists(arquivo))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
            if (emiteContigencia == true)
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.PastaRemessaNsCloud))
                {
                    arquivoContigencia = arquivo;
                    nomeArquivoContigencia = nomeArquivo;
                    File.Copy(arquivo, Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivo);

                    //dentro desse metodo retorna se a contigencia deu certo e grava o nfestatus.
                    aguardarParaLerRetornoContigencia(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Processados\nsConcluido\" + numeroNFCe + ".txt");
                }
                else
                    GenericaDesktop.ShowAlerta("Pasta de envio em contigência não foi configurada, " +
                        "favor solicite suporte a sua revenda autorizada e solicite a configuração, enquanto isso sua nota ficará " +
                        "na tela de gerenciamento de notas para você tentar reenviar a sefaz");
            }
        }
        private void abrirFormAguardar()
        {
            FrmAguarde uu = new FrmAguarde("5000", this.nfe);
            uu.ShowDialog();
        }
        private string lerTXT2(string caminhoArquivo)
        {
            //List<String> dadosLidos = new List<String>();
            string statusContigencia = "";
            string chave = "";
            System.IO.StreamReader arquivo = new System.IO.StreamReader(caminhoArquivo);
            string linha = "";
            while (true)
            {
                linha = arquivo.ReadLine();
                if (linha != null)
                {
                    string[] DadosColetados = linha.Split('|');
                    if (DadosColetados.Length > 4)
                    {
                        chave = DadosColetados[5].Replace("NFe", "");
                        statusContigencia = DadosColetados[4];
                    }
                }
                else
                    break;
            }
            if (nfe.Id > 0 && statusContigencia.Contains("Emitido em contingencia offline"))
            {
                nfe.Chave = chave;
                nfe.CodStatus = "";
                NfeStatus nfeStatus = new NfeStatus();
                nfeStatus.Id = 6;
                nfe.NfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                codStatusRet = "50";
                nfe.Status = "Emitido em contingencia offline";
                Controller.getInstance().salvar(nfe);
                GenericaDesktop.ShowInfo("Nota Fiscal gerada em Contigência!");
            }
            return chave;
        }
        private async void aguardarParaLerRetornoContigencia(string arquivoTXT)
        {
            await GenericaDesktop.VerificaProgramaContigenciaEstaEmExecucao();

            //Aguarda até gerar o arquivo txt na pasta de retorno ou em 10 segundos retorna com falha
            abrirFormAguardar();

            if (File.Exists(arquivoTXT))
            {
                string chaveContigencia = lerTXT2(arquivoTXT);
                if (!String.IsNullOrEmpty(chaveContigencia))
                {
                    if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                    {
                        string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                        if (File.Exists(caminhoPDF))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 6;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retornoNFCe.motivo;
                            nfe.CodStatus = retornoNFCe.cStat;
                            if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                            {
                                nfe.Protocolo = retornoNFCe.nProt;
                                nfe.Chave = retornoNFCe.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                            frmPDF.ShowDialog();
                            //System.Diagnostics.Process.Start(caminhoPDF);
                        }
                    }
                }
                else
                {
                    NfeStatus nfeStatus = new NfeStatus();
                    nfeStatus.Id = 2;
                    nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                    nfe.NfeStatus = nfeStatus;
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta("Não foi possível ter retorno da nota em contigência, tente reenviar a nota mais tarde!");
                
                }
            }
            else
            {
                await Task.Delay(5000);
                if (File.Exists(arquivoTXT))
                {
                    string chaveContigencia = lerTXT2(arquivoTXT);
                    if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                    {
                        string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                        if (File.Exists(caminhoPDF))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 6;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retornoNFCe.motivo;
                            nfe.CodStatus = retornoNFCe.cStat;
                            if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                            {
                                nfe.Protocolo = retornoNFCe.nProt;
                                nfe.Chave = retornoNFCe.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                            frmPDF.ShowDialog();
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Falha ao gerar nota em contigência, será realizado uma nova tentativa!");
                    if (!File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivoContigencia))
                        File.Copy(arquivoContigencia, Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nomeArquivoContigencia);

                    await Task.Delay(5000);
                    if (File.Exists(arquivoTXT))
                    {
                        string chaveContigencia = lerTXT2(arquivoTXT);
                        if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                        {
                            string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\" + DateTime.Now.Day.ToString().PadLeft(2, '0') + @"\" + chaveContigencia + ".pdf";
                            if (File.Exists(caminhoPDF))
                            {
                                NfeStatus nfeStatus1 = new NfeStatus();
                                nfeStatus1.Id = 6;
                                nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                                nfe.Status = retornoNFCe.motivo;
                                nfe.CodStatus = retornoNFCe.cStat;
                                if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                                {
                                    nfe.Protocolo = retornoNFCe.nProt;
                                    nfe.Chave = retornoNFCe.chNFe;
                                }
                                Controller.getInstance().salvar(nfe);
                                FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                                frmPDF.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        NfeStatus nfeStatus1 = new NfeStatus();
                        nfeStatus1.Id = 2;
                        nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                        nfe.Status = retornoNFCe.motivo;
                        nfe.CodStatus = retornoNFCe.cStat;
                        if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                        {
                            nfe.Protocolo = retornoNFCe.nProt;
                            nfe.Chave = retornoNFCe.chNFe;
                        }
                        Controller.getInstance().salvar(nfe);
                        GenericaDesktop.ShowAlerta("A nota fiscal nao teve retorno de autorização, verifique depois na tela de gerenciamento de notas!");
                    }
                }
            }
        }

        private void FrmNotaAgrupada_Load(object sender, EventArgs e)
        {
            if (Sessao.permissoes.Count > 0)
            {
                // Habilitar ou desabilitar os controles com base nas permissões
                if (!Sessao.permissoes.Contains("69"))
                {
                    GenericaDesktop.ShowAlerta("Usuário sem permissão para operar nessa tela (69)!");
                    this.Close();
                }
            }
        }
    }
}
