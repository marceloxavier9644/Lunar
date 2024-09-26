using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.PesquisasClass;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Lunar.Telas.Compras
{
    public partial class FrmDesmembrarItem : Form
    {
        decimal valorTotal = 0;
        private double quantidadeTotal = 0; // Quantidade total a ser inserida
        ProdutoPesquisaService produtoPesquisaService = new ProdutoPesquisaService();
        ProdutoController produtoController = new ProdutoController();
        bool editando = false;
        DataRow dataRowEdit;
        int idEdicaoProduto = 0;
        NfeProduto nfeProduto = new NfeProduto();
        public FrmDesmembrarItem(NfeProduto nfeProduto)
        {
            InitializeComponent();
            this.nfeProduto = nfeProduto;
            txtProdutoNota.Texts = nfeProduto.XProd;
            txtQuantidadeNota.Texts = nfeProduto.QuantidadeEntrada.ToString();
            quantidadeTotal = nfeProduto.QuantidadeEntrada;
            txtIdProdutoNf.Texts = nfeProduto.Id.ToString();
            txtNcmNota.Texts = nfeProduto.Ncm;
            txtUnidadeMedidaNota.Texts = nfeProduto.UCom;
            txtReferenciaNota.Texts = nfeProduto.CProd.ToString();
            txtCfopNota.Texts = nfeProduto.Cfop;
            txtValorUnitarioNota.Texts = string.Format("{0:0.00}", nfeProduto.VUnCom);
            txtValorTotalNota.Texts = string.Format("{0:0.00}", nfeProduto.VProd);
            valorTotal = nfeProduto.VProd;
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProduto("");
        }
        private void PesquisarProduto(string valor)
        {

            IList<Produto> listaProdutos = new List<Produto>();

            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            //txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            //txtValorTotal.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {
                foreach (Produto prod in listaProdutos)
                {
                    txtProduto.Texts = prod.Descricao;
                    txtQuantidade.Texts = "1";
                    txtValorUnitario.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtValorTotal.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtCodProduto.Texts = prod.Id.ToString();
                    if (valorAux.Contains("*"))
                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    if (prod.Ean.Equals(valor.Trim()))
                        inserirItem(prod);
                    else
                    {
                        txtQuantidade.Focus();
                        txtQuantidade.Select();
                    }
                }
            }
            else if (listaProdutos.Count > 1)
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
                                    txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidade.Texts = "1";
                                    txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    //this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                        inserirItem(((Produto)produtoOjeto));
                                    else
                                    {
                                        txtQuantidade.Focus();
                                        txtQuantidade.SelectAll();
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtQuantidade.Texts = "1";
                                txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                //this.produto = ((Produto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                    inserirItem(((Produto)produtoOjeto));
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
                txtProduto.SelectAll();
            }
        }

        private void inserirItem(Produto produto)
        {
            if (produto.Veiculo == true)
            {
                FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(produto, false, true);
                frmProdutoCadastro.ShowDialog();
            }
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;
            try
            {
                System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                row.SetField("Id", 0);
                if (editando == true)
                {
                    dataRowEdit.Delete();
                    if (idEdicaoProduto > 0)
                        row.SetField("Id", idEdicaoProduto);
                }
                idEdicaoProduto = 0;
                row.SetField("Codigo", produto.Id.ToString());
                row.SetField("Descricao", produto.Descricao);
                decimal valorUnitForm = decimal.Parse(txtValorUnitario.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00000}", valorUnitForm));
                row.SetField("Quantidade", txtQuantidade.Texts);
                decimal valorTotal = decimal.Parse(txtValorTotal.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00000}", valorTotal));
                row.SetField("Desconto", string.Format("{0:0.00000}", 0));
                row.SetField("Acrescimo", string.Format("{0:0.00000}", 0));
                dsProdutos.Tables[0].Rows.Add(row);

                txtProduto.Texts = "";
                somaEnquantoDigitaItens();
                txtQuantidade.Texts = "1";
                txtValorUnitario.Texts = "0,00";
                txtValorTotal.Texts = "0,00";
                txtCodProduto.Texts = "";
                txtProduto.Focus();
                editando = false;

                if (this.gridProdutos.View != null)
                {
                    if (this.gridProdutos.View.Records.Count > 0)
                    {
                        gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                        //this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridProdutos.AutoSizeController.Refresh();
                    }
                }

                txtProduto.Focus();

            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void RemoverItem()
        {
            try
            {
                // Verifica se existe uma linha selecionada no SfDataGrid
                if (gridProdutos.SelectedItems.Count > 0)
                {
                    // Pega o item selecionado (no SfDataGrid, SelectedItems retorna objetos de dados)
                    var rowSelecionada = gridProdutos.SelectedItems[0] as DataRowView;
                    if (rowSelecionada != null)
                    {
                        // Obtém o valor total do item que será removido
                        decimal valorTotalItemRemovido = Convert.ToDecimal(rowSelecionada["ValorTotal"]);

                        // Obtém a quantidade do item que será removido
                        int quantidadeItemRemovida = Convert.ToInt32(rowSelecionada["Quantidade"]);

                        // Subtrai o valor do item removido do valor acumulado
                        valorAcumulado -= valorTotalItemRemovido;

                        // Subtrai a quantidade removida da quantidade acumulada
                        quantidadeAcumulada -= quantidadeItemRemovida;

                        // Remove a linha do DataSet (baseado no dsProdutos, que é a origem dos dados do grid)
                        dsProdutos.Tables[0].Rows.Remove(rowSelecionada.Row);

                        // Atualiza o grid para refletir a remoção
                        gridProdutos.View.Refresh();

                        // Recalcula o valor restante e outras operações que dependam do valor acumulado
                        RecalcularValores();

                        GenericaDesktop.ShowAlerta("Item removido com sucesso!");
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Selecione um item para remover.");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro ao remover o item: " + erro.Message);
            }
        }


        private void RecalcularValores()
        {
            // Inicializa a variável valorAcumulado para recalcular o valor total dos itens
            valorAcumulado = 0;

            // Percorre todas as linhas da tabela para somar o valor total dos itens
            foreach (DataRow row in dsProdutos.Tables[0].Rows)
            {
                decimal valorTotalItem = Convert.ToDecimal(row["ValorTotal"]);
                valorAcumulado += valorTotalItem;
            }

            // Atualiza o display do valor total (se houver algum campo na interface que mostre isso)
            somaEnquantoDigitaItens();
        }
        private void somaEnquantoDigitaItens()
        {
            try
            {
                decimal valorTotalProdutos = 0;
                double pecas = 0;
                valorAcumulado = 0;
                if (gridProdutos.View != null)
                {
                    var records = gridProdutos.View.Records;
                    //decimal descontoItem = 0;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;

                        //if (!String.IsNullOrEmpty(dataRowView.Row[0].ToString()))
                        //    descontoItem = descontoItem + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        valorTotalProdutos = valorTotalProdutos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        valorAcumulado = valorAcumulado + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());
                    }
                    txtValorDesmembrado.Texts = string.Format("{0:0.00}", valorTotalProdutos);
                    decimal diferenca = valorTotalProdutos - decimal.Parse(txtValorTotalNota.Texts);
                    txtDiferenca.Texts = diferenca.ToString("C");
                }
            }
            catch
            {

            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Se a tecla pressionada for 'Enter'
            {
                string valorPesquisa = txtProduto.Texts.Trim(); // Captura o valor digitado no campo de texto

                ProdutoPesquisaService produtoService = new ProdutoPesquisaService();
                var resultado = produtoService.PesquisarProduto(valorPesquisa); // Chama o serviço de pesquisa

                // Aqui você vai verificar o retorno
                if (resultado.produto != null && resultado.grade != null)
                {
                    Produto produto = resultado.produto;
                    ProdutoGrade grade = resultado.grade;

                    // Preenche os campos da tela com os dados retornados
                    txtProduto.Texts = produto.Descricao;
                    txtQuantidade.Texts = "1";
                    txtValorUnitario.Texts = string.Format("{0:0.00}", grade.ValorVenda);
                    txtValorTotal.Texts = string.Format("{0:0.00}", grade.ValorVenda);
                    txtCodProduto.Texts = produto.Id.ToString();
                    txtQuantidade.Focus();
                    txtQuantidade.SelectAll();
                }
                else
                {
                    MessageBox.Show("Produto ou grade não encontrados."); // Mensagem de erro se não houver resultado
                }
            }
        }

        private void limparProduto()
        {
            txtCodProduto.Texts = "";
            txtProduto.Texts = "";
            txtQuantidade.Texts = "";
            txtValorUnitario.Texts = "0,00";
            txtValorTotal.Texts = "0,00";
            txtProduto.Focus();
            txtProduto.Select();
        }

        private decimal valorAcumulado = 0; // Valor acumulado até agora
        private double quantidadeAcumulada = 0; // Quantidade acumulada até agora
        private decimal SugerirValorParaInserir(double quantidadeInserida)
        {
            quantidadeAcumulada += quantidadeInserida;

            try
            {
                if (quantidadeTotal <= 0 || quantidadeInserida <= 0)
                {
                    limparProduto();
                    throw new ArgumentException("A quantidade total e a quantidade inserida devem ser maiores que zero.");
                }

                if (quantidadeAcumulada > quantidadeTotal)
                {
                    limparProduto();
                    throw new ArgumentException("A quantidade inserida excede a quantidade total disponível.");
                }

                decimal valorRestante = valorTotal - valorAcumulado;

                // Verifica se a quantidade acumulada é igual à quantidade total
                if (quantidadeAcumulada == quantidadeTotal)
                {
                    // Se for igual, divide o valor restante pela quantidade inserida
                    valorRestante = decimal.Parse(txtDiferenca.Texts.Replace("R$ ", ""));
                    valorRestante = Math.Abs(valorRestante);

                    decimal quantidadeParaRedistribuir = (decimal)quantidadeInserida; // Aqui estamos usando a quantidade que está sendo inserida
                    decimal valorUnitario = valorRestante / quantidadeParaRedistribuir; // Divide o valor restante pela quantidade inserida
                    return Math.Round(valorUnitario, 5);
                }

                // Se não é o último item a ser inserido
                if (quantidadeAcumulada < quantidadeTotal)
                {
                    // Calcula o valor unitário do item original
                    decimal valorUnitario = valorTotal / decimal.Parse(quantidadeTotal.ToString());
                    return Math.Round(valorUnitario, 2);
                }
                else // Se for o último item a ser inserido (caso não pegamos anteriormente)
                {
                    decimal valorUnitario = valorRestante / (decimal)quantidadeInserida;
                    return Math.Round(valorUnitario, 2);
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
                quantidadeAcumulada -= quantidadeInserida;
                limparProduto();
                return 0;
            }
        }




        private void btnConfirmarItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts))
            {
                decimal valorUnitario = decimal.Parse(txtValorUnitario.Texts);
                txtValorTotal.Texts = (valorUnitario * decimal.Parse(txtQuantidade.Texts)).ToString("N5");

                Produto produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)Controller.getInstance().selecionar(produto);
                inserirItem(produto);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione um produto");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txtValorUnitario.Focus();
                txtValorUnitario.SelectAll();
            }
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtQuantidade.Texts))
                txtValorUnitario.Texts = SugerirValorParaInserir(double.Parse(txtQuantidade.Texts)).ToString("N5");
        }

        private void txtValorUnitario__TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtValorUnitario.Texts) && !String.IsNullOrEmpty(txtQuantidade.Texts))
                {
                    decimal valorUnitario = decimal.Parse(txtValorUnitario.Texts);
                    txtValorTotal.Texts = (valorUnitario * decimal.Parse(txtQuantidade.Texts)).ToString("N5");
                }
            }
            catch
            {

            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            RemoverItem();
        }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtValorUnitario.Texts) && !String.IsNullOrEmpty(txtQuantidade.Texts))
                    {
                        btnConfirmarItem.PerformClick();
                    }
                }
                catch
                {

                }
            }
        }
    }
}
