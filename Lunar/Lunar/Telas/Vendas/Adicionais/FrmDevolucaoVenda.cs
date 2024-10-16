using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid;
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

            // Configurar a coluna 'Quantidade' para permitir edição
            gridProduto.Columns["QuantidadeDevolvida"].AllowEditing = true;
        }
        private void removerProdutoSelecionado()
        {
            // Verificar se há uma linha selecionada
            if (gridProduto.SelectedItem != null)
            {
                // Cast do item selecionado para o tipo correto (VendaItens)
                VendaItens itemSelecionado = gridProduto.SelectedItem as VendaItens;

                if (itemSelecionado != null)
                {
                    // Remover o item da lista de vendaItens
                    IList<VendaItens> vendaItens = (IList<VendaItens>)gridProduto.DataSource;
                    vendaItens.Remove(itemSelecionado);

                    // Atualizar o DataSource do grid com a lista atualizada
                    gridProduto.DataSource = null;
                    gridProduto.DataSource = vendaItens;
                    GridSummary.PreencherSumario(gridProduto, "ValorDevolvido");
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para remover.");
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            confirmarDevolucao();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            removerProdutoSelecionado();
        }

        private void confirmarDevolucao()
        {
            IList<VendaItens> vendaItens = (IList<VendaItens>)gridProduto.DataSource;

            if (vendaItens != null && vendaItens.Count > 0)
            {
                foreach (var item in vendaItens)
                {
                    if (item.QuantidadeDevolvida > 0)
                    {
                        item.DataDevolucao = DateTime.Now;
                        decimal valorTotalItem = decimal.Parse(item.Quantidade.ToString()) * item.ValorProduto;
                        decimal valorUnitarioLiquido = (valorTotalItem - item.ValorDesconto + item.ValorAcrescimo) / decimal.Parse(item.Quantidade.ToString());

                        // Calcular o valor total da devolução
                        decimal valorTotalDevolucao = decimal.Parse(item.QuantidadeDevolvida.ToString()) * valorUnitarioLiquido;
                        vendaItensController.salvar(item);
                    }
                }
                GenericaDesktop.ShowInfo("Devolução confirmada com sucesso!");
                this.Close();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Não há itens para devolução.");
            }
        }
        private void gridProduto_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
            if (e.Column.MappingName == "QuantidadeDevolvida")
            {
                // Recuperar o item atual da linha
                var item = e.RowData as VendaItens;

                if (item != null)
                {
                    double quantidadeDevolvida;

                    // Verificar se o novo valor é nulo antes de tentar a conversão
                    if (e.NewValue == null || !double.TryParse(e.NewValue.ToString(), out quantidadeDevolvida))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "A quantidade a ser devolvida deve ser um número válido.";
                    }
                    // Verificar se a quantidade devolvida é maior que zero
                    else if (quantidadeDevolvida <= 0)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "A quantidade a ser devolvida deve ser maior que 0.";
                    }
                    // Verificar se a quantidade devolvida não é maior que a quantidade total vendida
                    else if (quantidadeDevolvida > item.Quantidade)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "A quantidade a ser devolvida não pode ser maior que a quantidade vendida.";
                    }
                    // Verificar se a quantidade devolvida não é menor que a quantidade devolvida anterior
                    else if (quantidadeDevolvida < item.QuantidadeDevolvida)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = $"A quantidade devolvida não pode ser menor que {item.QuantidadeDevolvida} que foi devolvida anteriormente!";
                    }
                    else
                    {
                        // QuantidadeDevolvida válida, calcular o valor devolvido
                        item.QuantidadeDevolvida = quantidadeDevolvida;  // Atualizar a quantidade

                        // Calcular o valor total da devolução
                        decimal valorTotalItem = decimal.Parse(item.Quantidade.ToString()) * item.ValorProduto;
                        decimal valorUnitarioLiquido = (valorTotalItem - item.ValorDesconto + item.ValorAcrescimo) / decimal.Parse(item.Quantidade.ToString());
                        decimal valorTotalDevolucao = decimal.Parse(item.QuantidadeDevolvida.ToString()) * valorUnitarioLiquido;

                        // Atualizar o campo ValorDevolvido
                        item.ValorDevolvido = valorTotalDevolucao;

                        // Forçar a atualização do grid (atualizar a célula)
                        gridProduto.TableControl.Refresh();
                        gridProduto.View.Refresh();
                    }
                }

                // Se houve um erro, exibir o erro diretamente na célula correspondente
                if (!e.IsValid)
                {
                    gridProduto.ShowErrorIcon = true;  // Ativar o ícone de erro
                }
            }

            // Atualizar o sumário após a validação e cálculo do ValorDevolvido
            GridSummary.PreencherSumario(gridProduto, "ValorDevolvido");
        }


    }
}
