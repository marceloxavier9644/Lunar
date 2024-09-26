using iTextSharp.text.pdf.languages;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Pdf;
using Syncfusion.WinForms.DataGridConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Compras
{
    public partial class FrmPrecificacaoPorNotaCompra : Form
    {
        private Nfe nfe = new Nfe();
        IList<NfeProduto> listaProdutosNfe = new List<NfeProduto>();
        public FrmPrecificacaoPorNotaCompra(int idNotaFiscal)
        {
            InitializeComponent();

            if (idNotaFiscal > 0)
            {
                nfe = new Nfe();
                nfe.Id = idNotaFiscal;
                nfe = (Nfe)Controller.getInstance().selecionar(nfe);
                buscarProdutosNotaFiscal(nfe);
            }
        }

        private void buscarProdutosNotaFiscal(Nfe nfe)
        {
            listaProdutosNfe = new List<NfeProduto>();
            NfeProdutoController nfeProdutoController = new NfeProdutoController();
            listaProdutosNfe = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
            if(listaProdutosNfe.Count > 0)
            {
                double qtdProdutoNota = 0;
                foreach (NfeProduto nfeprod in listaProdutosNfe)
                {
                    qtdProdutoNota = qtdProdutoNota + nfeprod.QuantidadeEntrada;
                }
                foreach (NfeProduto nfeprod in listaProdutosNfe) 
                {
                    nfeprod.VUnCom = calcularCustoProduto(nfeprod, qtdProdutoNota);
                    nfeprod.Produto.Markup = CalcularMarkup(nfeprod.VUnCom, nfeprod.Produto.ValorVenda).ToString();
                }
                gridProdutos.DataSource = listaProdutosNfe;
                gridProdutos.Refresh();
            }
        }

        public decimal calcularCustoProduto(NfeProduto nfeProd, double quantidadeProdutoNota)
        {
            decimal ipiUnitario = nfeProd.ValorIpi / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal stUnitario = nfeProd.VICMSSt / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal valorUnitItem = nfeProd.VProd / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal custoFreteItem = nfe.VFrete / decimal.Parse(quantidadeProdutoNota.ToString());
            decimal totalUnitario = valorUnitItem + ipiUnitario + stUnitario + custoFreteItem;

            return totalUnitario;
        }
        public decimal CalcularMarkup(decimal precoCusto, decimal precoVenda)
        {
            if (precoCusto == 0)
                throw new DivideByZeroException("O preço de custo não pode ser zero.");

            decimal markup = ((precoVenda - precoCusto) / precoCusto) * 100;

            return Math.Round(markup, 5);
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void chkArredondarCentavos_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkArredondarCentavos.Checked == true)
            {
                txtCentavos.Enabled = true;
                txtCentavos.Text = "0,90";
                txtCentavos.Focus();
                txtCentavos.SelectAll();
            }
            else
            {
                txtCentavos.Enabled = false;
                txtCentavos.Text = "0,00";
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal markup = decimal.Parse(txtMarkup.Text);
                decimal centavosDesejados = decimal.Parse(txtCentavos.Text);
                bool arredondarCentavos = chkArredondarCentavos.Checked;

                foreach (var registro in listaProdutosNfe)
                {
                    decimal precoCusto = registro.VUnCom;
                    decimal novoPrecoVenda = precoCusto * (1 + (markup / 100));

                    // Se o checkbox estiver marcado, aplica o arredondamento
                    if (arredondarCentavos)
                    {
                        novoPrecoVenda = ArredondarParaCentavosDesejados(novoPrecoVenda, centavosDesejados);
                    }

                    // Atualiza o valor de venda e o markup do produto
                    registro.Produto.ValorVenda = novoPrecoVenda;
                    registro.Produto.Markup = CalcularMarkup(precoCusto, novoPrecoVenda).ToString();
                }

                // Atualiza o grid
                gridProdutos.DataSource = null;
                gridProdutos.DataSource = listaProdutosNfe;
                gridProdutos.Refresh();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o markup ou centavos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal ArredondarParaCentavosDesejados(decimal valorVenda, decimal centavosDesejados)
        {
            decimal parteInteira = Math.Floor(valorVenda);
            decimal valorFinal = parteInteira + centavosDesejados;

            return valorFinal;
        }

        private void gridProdutos_CurrentCellEndEdit(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellEndEditEventArgs e)
        {
            var registroProduto = gridProdutos.CurrentItem as NfeProduto;
            if (registroProduto == null) return;

            // Verifica se o campo alterado foi o Markup ou o ValorVenda
            if (e.DataColumn.GridColumn.MappingName == "Produto.Markup")
            {
                AtualizarValorVendaPorMarkup(registroProduto);
            }
            else if (e.DataColumn.GridColumn.MappingName == "Produto.ValorVenda")
            {
                AtualizarMarkupPorValorVenda(registroProduto);
            }

            // Atualiza o grid para refletir os novos valores
            gridProdutos.Refresh();
        }

        // Método para atualizar o preço de venda ao alterar o Markup
        private void AtualizarValorVendaPorMarkup(NfeProduto registroProduto)
        {
            try
            {
                // Verifica se o valor de custo é válido
                if (registroProduto.VUnCom > 0) // Supondo que VUnCom é o PrecoCusto
                {
                    // Converte o Markup para decimal e recalcula o valor de venda
                    decimal markup = decimal.Parse(registroProduto.Produto.Markup);
                    decimal novoPrecoVenda = registroProduto.VUnCom * (1 + (markup / 100));

                    // Atualiza o campo ValorVenda no objeto
                    registroProduto.Produto.ValorVenda = novoPrecoVenda;

                    // Se você estiver arredondando os centavos, aplique essa lógica aqui
                    if (chkArredondarCentavos.Checked)
                    {
                        registroProduto.Produto.ValorVenda = ArredondarParaCentavosDesejados(novoPrecoVenda, decimal.Parse(txtCentavos.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o valor de venda: " + ex.Message);
            }
        }

        // Método para atualizar o Markup ao alterar o ValorVenda
        private void AtualizarMarkupPorValorVenda(NfeProduto registroProduto)
        {
            try
            {
                // Verifica se o valor de custo é válido
                if (registroProduto.VUnCom > 0) // Supondo que VUnCom é o PrecoCusto
                {
                    // Converte o ValorVenda para decimal e recalcula o markup
                    decimal valorVenda = registroProduto.Produto.ValorVenda;
                    decimal novoMarkup = CalcularMarkup(registroProduto.VUnCom, valorVenda);

                    // Atualiza o campo Markup no objeto
                    registroProduto.Produto.Markup = novoMarkup.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular o markup: " + ex.Message);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            salvar();
            this.Close();
        }

        private void salvar()
        {
            try
            {
                foreach (var nfeProduto in listaProdutosNfe)
                {
                    Produto produtoAtualizado = nfeProduto.Produto;
                    produtoAtualizado.ValorCusto = nfeProduto.VUnCom;
                    Controller.getInstance().salvar(produtoAtualizado);
                }
                MessageBox.Show("Produtos atualizados e salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                // Criando opções de exportação para o PDF
                PdfExportingOptions pdfExportingOptions = new PdfExportingOptions
                {
                    AutoColumnWidth = true 
                };
                int maxDescricaoLength = 45;
                if (chkImprimirCusto.Checked == false)
                    maxDescricaoLength = 95;
                if (chkImprimirCusto.Checked == true && chkImprimirReferencia.Checked == true)
                    maxDescricaoLength = 20;
     
                foreach (var produto in listaProdutosNfe)
                {
                    if (produto.DescricaoInterna.Length > maxDescricaoLength)
                    {
                        produto.Produto.Descricao = produto.Produto.Descricao.Substring(0, maxDescricaoLength) + "...";
                        produto.DescricaoInterna = produto.Produto.Descricao.Substring(0, maxDescricaoLength) + "...";
                    }
                }

                pdfExportingOptions.ExcludeColumns.Add("CfopEntrada");
                if(chkImprimirCusto.Checked == false)
                {
                    pdfExportingOptions.ExcludeColumns.Add("VUnCom");
                    pdfExportingOptions.ExcludeColumns.Add("Produto.Markup");
                }
                if (chkImprimirReferencia.Checked == false)
                {
                    pdfExportingOptions.ExcludeColumns.Add("Produto.Referencia");
                }
                PdfDocument pdfDocument = gridProdutos.ExportToPdf(pdfExportingOptions);

                string tempDirectory = Path.GetTempPath();
                string tempFilePath = Path.Combine(tempDirectory, "ProdutosPrecificados.pdf");

                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    pdfDocument.Save(fileStream);
                }
                System.Diagnostics.Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar o PDF: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
