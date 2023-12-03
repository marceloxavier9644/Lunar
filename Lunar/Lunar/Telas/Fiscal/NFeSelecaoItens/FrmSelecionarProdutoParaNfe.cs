using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Fiscal.NFeSelecaoItens
{
    public partial class FrmSelecionarProdutoParaNfe : Form
    {
        IList<NfeProduto> listaProdutosNFe = new List<NfeProduto>();
        bool showModal = false;
        private IList<Produto> listaProdutos;
        ProdutoController produtoController = new ProdutoController();
        Produto produto = new Produto();
        int posicaoItem = 1;
        decimal valorTotal = 0;
        decimal valorComDesconto = 0;
        bool inseridoDescontoItem = false;
        GenericaDesktop generica = new GenericaDesktop();
        bool editandoProduto = false;
        DataRow dataRowEdit;
        public DialogResult showModalNovo(ref DataSet dsProdutos)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                dsProdutos = this.dsProduto;
            }
            return DialogResult;
        }
        public FrmSelecionarProdutoParaNfe(DataSet dsProdutos)
        {
            InitializeComponent();
            if(dsProdutos.Tables.Count > 0)
                this.dsProduto = dsProdutos;
            gridProdutos.DataSource = dsProduto;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmSelecionarProdutoParaNfe_Load(object sender, EventArgs e)
        {

        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProdutoPorDescricao(txtPesquisaProduto.Texts.Trim());
            }
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

            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {
                foreach (Produto prod in listaProdutos)
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

        private void inserirItem(Produto produto)
        {
            try
            {
            
                string csosn = txtCsosn.Texts.Trim();
                if (csosn.Length == 4)
                    csosn = csosn.Substring(1, 3);
                System.Data.DataRow row;
                row = dsProduto.Tables[0].NewRow();
                if(editandoProduto == true)
                    dataRowEdit.Delete();

                row.SetField("item", posicaoItem);
                posicaoItem++;
                row.SetField("Codigo", produto.Id.ToString());
                row.SetField("Descricao", produto.Descricao);
                decimal valorUnitForm = decimal.Parse(txtValorUnitario.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Quantidade", txtQuantidade.Texts);
                decimal valorTotal = valorUnitForm * decimal.Parse(txtQuantidade.Texts)/* - decimal.Parse(txtDesconto.Texts)*/;
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("DescontoItem", string.Format("{0:0.00}", decimal.Parse(txtDesconto.Texts)));
                row.SetField("EstoqueAuxiliar", produto.EstoqueAuxiliar);
                row.SetField("Estoque", produto.Estoque);
                row.SetField("CfopVenda", txtCfop.Texts.Trim());
                row.SetField("CstIcms", csosn);
                produto.PercentualIcms = txtPercentualICMS.Texts;

                row.SetField("PercentualICMS", txtPercentualICMS.Texts);
                row.SetField("PercentualRed", txtPercentualRed.Texts);
                row.SetField("BaseCalcICMS", txtBaseCalcICMS.Texts);
                row.SetField("ValorICMS", txtValorICMS.Texts);
                row.SetField("PercentualPIS", txtPercentualPIS.Texts);
                row.SetField("BaseCalcPis", txtBaseCalcPIS.Texts);
                row.SetField("ValorPIS", txtValorPIS.Texts);
                row.SetField("PercentualICMSST", txtPercentualICMSST.Texts);
                row.SetField("BaseCalcICMSST", txtBaseCalcICMSST.Texts);
                row.SetField("ValorICMSST", txtValorICMSST.Texts);
                row.SetField("PercentualIPI", txtPercentualIPI.Texts);
                row.SetField("BaseCalcIPI", txtBaseCalcIPI.Texts);
                row.SetField("ValorIPI", txtValorIPI.Texts);
                row.SetField("PercentualCOFINS", txtPercentualCOFINS.Texts);
                row.SetField("BaseCalcCOFINS", txtBaseCalcCOFINS.Texts);
                row.SetField("ValorCOFINS", txtValorCOFINS.Texts);
                row.SetField("PercentualFCP", txtPercFCP.Texts);
                row.SetField("BaseCalcFCPST", txtBaseCalcFCPST.Texts);
                row.SetField("ValorFCPST", txtvalorFCPST.Texts);
                row.SetField("BaseCalcICMSSTRet", txtBaseCalcICMSSTRet.Texts);
                row.SetField("ValorICMSRet", txtValorICMSREt.Texts);
                row.SetField("PercentualFCPRet", txtPercentualFCPRet.Texts);
                row.SetField("PercentualSTCons", txtPercStConsumidor.Texts);
                row.SetField("vFrete", "0");
                row.SetField("vOutro", "0");
                row.SetField("vSeguro", "0");
                row.SetField("Observacao", txtObservacaoProduto.Texts);
                row.SetField("ValorIPI", txtValorIPI.Texts);
                row.SetField("ValorIpiDevolvido", txtValorIpiDevolvido.Texts);
                row.SetField("PercentualMercadoriaDevolvida", txtPercentualMercadoriaDevolvida.Texts);

                dsProduto.Tables[0].Rows.Add(row);
 
                txtPesquisaProduto.Texts = "";
                somaEnquantoDigita();
                txtCodProduto.Texts = "";
                txtQuantidade.Texts = "";
                txtValorUnitario.Texts = "";
                txtValorTotal.Texts = "";
                txtDesconto.Texts = "0,00";
                txtDescontoPercentual.Texts = "0";
                txtTotalComDesconto.Texts = "0,00";
                limparAliquotas();

                this.produto = new Produto();

                if (gridProdutos.View != null) 
                {
                    if (this.gridProdutos.View.Records.Count > 0)
                    {
                        gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        gridProdutos.AutoSizeController.Refresh();
                        ajustarNumeroItem();
                    } 
                }
    
                editandoProduto = false;
                txtPesquisaProduto.Focus();
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void limparAliquotas()
        {
            txtPercentualICMS.Texts = "0";
            txtPercentualRed.Texts = "0";
            txtBaseCalcICMS.Texts = "0,00";
            txtValorICMS.Texts = "0,00";
            txtBaseCalcPIS.Texts = "0,00";
            txtValorPIS.Texts = "0,00";
            txtPercentualICMSST.Texts = "0";
            txtBaseCalcICMSST.Texts = "0,00";
            txtValorICMSST.Texts = "0,00";
            txtPercentualIPI.Texts = "0";
            txtBaseCalcIPI.Texts = "0,00";
            txtValorIPI.Texts = "0,00";
            txtPercentualCOFINS.Texts = "0";
            txtBaseCalcCOFINS.Texts = "0,00";
            txtValorCOFINS.Texts = "0,00";
            txtPercFCP.Texts = "0";
            txtBaseCalcFCPST.Texts = "0,00";
            txtvalorFCPST.Texts = "0,00";
            txtBaseCalcICMSSTRet.Texts = "0,00";
            txtValorICMSREt.Texts = "0,00";
            txtPercentualFCPRet.Texts = "0";
            txtPercStConsumidor.Texts = "0";
            txtPercentualMercadoriaDevolvida.Texts = "0";
            txtValorIpiDevolvido.Texts = "0,00";
        }
        private void somaEnquantoDigita()
        {
            try
            {
                if (gridProdutos.RowCount > 0)
                {
                    valorTotal = 0;
                    valorComDesconto = 0;
                    double pecas = 0;
                    var records = gridProdutos.View.Records;
                    decimal descontoItem = 0;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;
                        if (!String.IsNullOrEmpty(dataRowView.Row[7].ToString()))
                            descontoItem = descontoItem + decimal.Parse(dataRowView.Row[7].ToString());
                        valorTotal = valorTotal + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());
                    }
                    if (inseridoDescontoItem == true)
                        txtDesconto.Texts = descontoItem.ToString("C2", CultureInfo.CurrentCulture);

                    txtValorTotal.Texts = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
                    //txtTotalItens.Texts = pecas + " Total";
                    if (!String.IsNullOrEmpty(txtDesconto.Texts))
                    {
                        valorComDesconto = valorTotal - decimal.Parse(txtDesconto.Texts.Replace("R$ ", ""));
                    }
                    txtTotalComDesconto.Texts = valorComDesconto.ToString("C2", CultureInfo.CurrentCulture);
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro na soma dos produtos " + erro.Message);
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            PesquisarProdutoPorDescricao(txtPesquisaProduto.Texts.Trim());
        }

        private void calculaTotaisProduto()
        {
            txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            txtTotalComDesconto.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts) - decimal.Parse(txtDesconto.Texts));
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

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
                txtValorUnitario.Focus();
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

        private void btnConfirmarProduto_Click(object sender, EventArgs e)
        {
            if(validaProduto())
                inserirItem(produto);
        }
        private bool validaProduto()
        {
            if (produto == null)
            {
                GenericaDesktop.ShowAlerta("Selecione o produto corretamente");
                return false;
            }
            if (String.IsNullOrEmpty(txtCfop.Texts))
            {
                GenericaDesktop.ShowAlerta("Preencha o CFOP corretamente");
                return false;
            }
            if (String.IsNullOrEmpty(txtCsosn.Texts))
            {
                GenericaDesktop.ShowAlerta("Preencha o CSOSN corretamente");
                return false;
            }
            return true;
        }
 
        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDescontoPercentual.Focus();
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
                    txtCfop.Focus();
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
                    if(produto.Id > 0)
                        GenericaDesktop.ShowAlerta(erro.Message);
                }
            }
        }

        private void txtDesconto_Leave(object sender, EventArgs e)
        {
            descontoEmValor();
        }

        private void txtDescontoPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDesconto.Focus();
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtCfop.Focus();
        }

        private void txtCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCsosn.Focus();
            }
        }

        private void btnPesquisaCFOP_Click(object sender, EventArgs e)
        {
            Object cfopOjeto = new Cfop();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Cfop",""))
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
                    switch (uu.showModal("Cfop", "", ref cfopOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCfop.Texts = ((Cfop)cfopOjeto).CfopNumero;
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

        private void txtCfop_Leave(object sender, EventArgs e)
        {
            confereCFOP();
        }

        private void confereCFOP()
        {
            CfopController cfopController = new CfopController();
            IList<Cfop> listaCFOP = cfopController.selecionarCfopPorCfop(txtCfop.Texts.Trim());
            if (listaCFOP.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CFOP não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CFOP correto para essa nota em específico!");
            }
        }

        private void txtCsosn_Leave(object sender, EventArgs e)
        {
            CsosnController cfopController = new CsosnController();
            string csosn = txtCsosn.Texts.Trim();
            if (csosn.Length == 4)
                csosn = csosn.Substring(1, 3);
            IList<Csosn> listaCsosn = cfopController.selecionarCsosnPorCsosn(csosn);
            if (listaCsosn.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CFOP não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CFOP correto para essa nota em específico!");
            }
        }

        private void txtCsosn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtObservacaoProduto.Focus();
        }

        private void txtObservacaoProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnConfirmarProduto.PerformClick();
        }

        private void txtPercentualICMS_Leave(object sender, EventArgs e)
        {
            calculoICMS();
        }


        private void corrigirAliquotas()
        {
            if (String.IsNullOrEmpty(txtPercentualICMS.Texts))
                txtPercentualICMS.Texts = "0";

            if (String.IsNullOrEmpty(txtPercentualRed.Texts))
                txtPercentualRed.Texts = "0";

            if (String.IsNullOrEmpty(txtBaseCalcICMS.Texts))
                txtBaseCalcICMS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorICMS.Texts))
                txtValorICMS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtBaseCalcPIS.Texts))
                txtBaseCalcPIS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorPIS.Texts))
                txtValorPIS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtPercentualICMSST.Texts))
                txtPercentualICMSST.Texts = "0";

            if (String.IsNullOrEmpty(txtBaseCalcICMSST.Texts))
                txtBaseCalcICMSST.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorICMSST.Texts))
                txtValorICMSST.Texts = "0,00";

            if (String.IsNullOrEmpty(txtPercentualIPI.Texts))
                txtPercentualIPI.Texts = "0";

            if (String.IsNullOrEmpty(txtBaseCalcIPI.Texts))
                txtBaseCalcIPI.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorIPI.Texts))
                txtValorIPI.Texts = "0,00";

            if (String.IsNullOrEmpty(txtPercentualCOFINS.Texts))
                txtPercentualCOFINS.Texts = "0";

            if (String.IsNullOrEmpty(txtBaseCalcCOFINS.Texts))
                txtBaseCalcCOFINS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorCOFINS.Texts))
                txtValorCOFINS.Texts = "0,00";

            if (String.IsNullOrEmpty(txtPercFCP.Texts))
                txtPercFCP.Texts = "0";

            if (String.IsNullOrEmpty(txtBaseCalcFCPST.Texts))
                txtBaseCalcFCPST.Texts = "0,00";

            if (String.IsNullOrEmpty(txtvalorFCPST.Texts))
                txtvalorFCPST.Texts = "0,00";

            if (String.IsNullOrEmpty(txtBaseCalcICMSSTRet.Texts))
                txtBaseCalcICMSSTRet.Texts = "0,00";

            if (String.IsNullOrEmpty(txtValorICMSREt.Texts))
                txtValorICMSREt.Texts = "0,00";

            if (String.IsNullOrEmpty(txtPercentualFCPRet.Texts))
                txtPercentualFCPRet.Texts = "0";

            if (String.IsNullOrEmpty(txtPercStConsumidor.Texts))
                txtPercStConsumidor.Texts = "0";

        }
        private void calculoICMS()
        {
            try
            {
                corrigirAliquotas();
                decimal valorCalc = decimal.Parse(txtBaseCalcICMS.Texts) * decimal.Parse(txtPercentualICMS.Texts) / 100;
                valorCalc = valorCalc - (decimal.Parse(txtPercentualRed.Texts) * decimal.Parse(txtValorICMS.Texts) / 100);
                txtValorICMS.Texts = valorCalc.ToString("N2");            
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Preencha os campos de ICMS corretamente!");
            }
        }

        private void calculoPIS()
        {
            try
            {
                corrigirAliquotas();
                decimal valorCalc = decimal.Parse(txtBaseCalcPIS.Texts) * decimal.Parse(txtPercentualPIS.Texts) / 100;
                txtValorPIS.Texts = valorCalc.ToString("N2");
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Preencha os campos de PIS corretamente!");
            }
        }

        private void calculoICMSST()
        {
            try
            {
                corrigirAliquotas();
                decimal valorCalc = decimal.Parse(txtBaseCalcICMSST.Texts) * decimal.Parse(txtPercentualICMSST.Texts) / 100 - decimal.Parse(txtValorICMS.Texts);
                txtValorICMSST.Texts = valorCalc.ToString("N2");
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Preencha os campos de ICMS ST corretamente!");
            }
        }

        private void calculoIPI()
        {
            try
            {
                corrigirAliquotas();
                decimal valorCalc = decimal.Parse(txtBaseCalcIPI.Texts) * decimal.Parse(txtPercentualIPI.Texts) / 100;
                txtValorIPI.Texts = valorCalc.ToString("N2");
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Preencha os campos de IPI corretamente!");
            }
        }

        private void calculoCOFINS()
        {
            try
            {
                corrigirAliquotas();
                decimal valorCalc = decimal.Parse(txtBaseCalcCOFINS.Texts) * decimal.Parse(txtPercentualCOFINS.Texts) / 100;
                txtValorCOFINS.Texts = valorCalc.ToString("N2");
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Preencha os campos de COFINS corretamente!");
            }
        }

        private void txtPercentualICMS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoICMS();
                txtPercentualRed.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualICMS.Texts, e);
        }

        private void txtPercentualRed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            {
                calculoICMS();
                txtBaseCalcICMS.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualRed.Texts, e);
        }

        private void txtPercentualRed_Leave(object sender, EventArgs e)
        {
            calculoICMS();
        }

        private void txtBaseCalcICMS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoICMS();
                txtPercentualPIS.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcICMS.Texts, e);
        }

        private void txtBaseCalcICMS_Leave(object sender, EventArgs e)
        {
            calculoICMS();
        }

        private void txtPercentualPIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoPIS();
                txtBaseCalcPIS.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualPIS.Texts, e);
        }

        private void txtPercentualPIS_Leave(object sender, EventArgs e)
        {
            calculoPIS();
        }

        private void txtBaseCalcPIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoPIS();
                txtPercentualICMSST.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcPIS.Texts, e);
        }

        private void txtBaseCalcPIS_Leave(object sender, EventArgs e)
        {
            calculoPIS();
        }

        private void txtPercentualICMSST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoICMSST();
                txtBaseCalcICMSST.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualICMSST.Texts, e);
        }

        private void txtPercentualICMSST_Leave(object sender, EventArgs e)
        {
            calculoICMSST();
        }

        private void txtBaseCalcICMSST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoICMSST();
                txtPercentualIPI.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcICMSST.Texts, e);
        }

        private void txtBaseCalcICMSST_Leave(object sender, EventArgs e)
        {
            calculoICMSST();
        }

        private void txtPercentualIPI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoIPI();
                txtBaseCalcIPI.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualIPI.Texts, e);
        }

        private void txtPercentualIPI_Leave(object sender, EventArgs e)
        {
            calculoIPI();
        }

        private void txtBaseCalcIPI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoIPI();
                txtPercentualCOFINS.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcIPI.Texts, e);
        }

        private void txtBaseCalcIPI_Leave(object sender, EventArgs e)
        {
            calculoIPI();
        }

        private void txtPercentualCOFINS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoCOFINS();
                txtBaseCalcCOFINS.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualCOFINS.Texts, e);
        }

        private void txtPercentualCOFINS_Leave(object sender, EventArgs e)
        {
            calculoCOFINS();
        }

        private void txtPercFCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBaseCalcFCPST.Focus();
            }
            generica.SoNumeroEVirgula(txtPercFCP.Texts, e);
        }

        private void txtBaseCalcFCPST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtvalorFCPST.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcFCPST.Texts, e);
        }

        private void txtvalorFCPST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConfirmarProduto.PerformClick();
            }
            generica.SoNumeroEVirgula(txtvalorFCPST.Texts, e);
        }

        private void txtBaseCalcICMSSTRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtValorICMSREt.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcICMSSTRet.Texts, e);
        }

        private void txtValorICMSREt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPercentualFCPRet.Focus();
            }
            generica.SoNumeroEVirgula(txtValorICMSREt.Texts, e);
        }

        private void txtPercentualFCPRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPercStConsumidor.Focus();
            }
            generica.SoNumeroEVirgula(txtPercentualFCPRet.Texts, e);
        }

        private void btnConfirmarProdutos_Click(object sender, EventArgs e)
        {
            if (produto != null)
            {
                if(produto.Id > 0)
                    inserirItem(produto);
            }
            if (showModal)
            {
                //carregarListaProdutos();
                DialogResult = DialogResult.OK;
            }
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void gridProdutos_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            dataRowEdit = dataRow;

            editandoProduto = true;
            txtCodProduto.Texts = dataRow["Codigo"].ToString();
            this.produto.Id = int.Parse(dataRow["Codigo"].ToString());
            this.produto = (Produto)Controller.getInstance().selecionar(produto);

            txtPesquisaProduto.Texts = dataRow["Descricao"].ToString();
            txtQuantidade.Texts = dataRow["Quantidade"].ToString();
            txtValorUnitario.Texts = dataRow["ValorUnitario"].ToString();
            txtValorTotal.Texts = dataRow["ValorTotal"].ToString();
            txtDesconto.Texts = dataRow["DescontoItem"].ToString();
            txtTotalComDesconto.Texts = (decimal.Parse(txtValorTotal.Texts) - decimal.Parse(txtDesconto.Texts)).ToString("N2");
            txtCfop.Texts = dataRow["CfopVenda"].ToString();
            txtCsosn.Texts = dataRow["CstIcms"].ToString();
            try { txtObservacaoProduto.Texts = dataRow["Observacao"].ToString(); } catch { };
            txtPercentualICMS.Texts = dataRow["PercentualICMS"].ToString();
            txtPercentualRed.Texts = dataRow["PercentualRed"].ToString();
            txtBaseCalcICMS.Texts = dataRow["BaseCalcICMS"].ToString();
            txtValorICMS.Texts = dataRow["ValorICMS"].ToString();
            txtPercentualPIS.Texts = dataRow["PercentualPIS"].ToString();
            txtBaseCalcPIS.Texts = dataRow["BaseCalcPis"].ToString();
            txtValorPIS.Texts = dataRow["ValorPIS"].ToString();
            txtPercentualICMSST.Texts = dataRow["PercentualICMSST"].ToString();
            txtBaseCalcICMSST.Texts = dataRow["BaseCalcICMSST"].ToString();
            txtValorICMSST.Texts = dataRow["ValorICMSST"].ToString();
            txtPercentualIPI.Texts = dataRow["PercentualIPI"].ToString();
            txtBaseCalcIPI.Texts = dataRow["BaseCalcIPI"].ToString();
            txtValorIPI.Texts = dataRow["ValorIPI"].ToString();

            txtPercentualCOFINS.Texts = dataRow["PercentualCOFINS"].ToString();
            txtBaseCalcCOFINS.Texts = dataRow["BaseCalcCOFINS"].ToString();
            txtValorCOFINS.Texts = dataRow["ValorCOFINS"].ToString();
            txtPercFCP.Texts = dataRow["PercentualFCP"].ToString();
            txtBaseCalcFCPST.Texts = dataRow["BaseCalcFCPST"].ToString();
            txtvalorFCPST.Texts = dataRow["ValorFCPST"].ToString();
            txtBaseCalcICMSSTRet.Texts = dataRow["BaseCalcICMSSTRet"].ToString();
            txtValorICMSREt.Texts = dataRow["ValorICMSRet"].ToString();
            txtPercentualFCPRet.Texts = dataRow["PercentualFCPRet"].ToString();
            txtPercStConsumidor.Texts = dataRow["PercentualSTCons"].ToString();
            txtValorIpiDevolvido.Texts = dataRow["ValorIpiDevolvido"].ToString();
            txtPercentualMercadoriaDevolvida.Texts = dataRow["PercentualMercadoriaDevolvida"].ToString();
        }

        //private void carregarListaProdutos()
        //{
        //    listaProdutosNFe = new List<NfeProduto>();
        //    var records = gridProdutos.View.Records;
        //    int i = 0;
        //    foreach (var record in records)
        //    {
        //        i++;
        //        var dataRowViewXXX = record.Data as DataRowView;
        //        NfeProduto nfeProduto = new NfeProduto();
        //        produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
        //        double quantidade = double.Parse(dataRowViewXXX.Row["Quantidade"].ToString());
        //        decimal descontoItem = decimal.Parse(dataRowViewXXX.Row["DescontoItem"].ToString());
        //        produto.ValorVenda = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
        //        nfeProduto.Item = i.ToString();
        //        nfeProduto.Produto = produto;
        //        nfeProduto.QCom = quantidade.ToString();
        //        nfeProduto.Ncm = produto.Ncm;
        //        nfeProduto.Cest = produto.Cest;
        //        nfeProduto.Cfop = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
        //        nfeProduto.CProd = produto.Id.ToString();
        //        nfeProduto.Nfe = null;//
        //        nfeProduto.VProd = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
        //        nfeProduto.QTrib = quantidade;
        //        nfeProduto.VDesc = descontoItem;
        //        nfeProduto.DescricaoInterna = produto.Descricao;
        //        nfeProduto.XProd = produto.Descricao;
        //        nfeProduto.CodigoInterno = produto.Id.ToString();
        //        nfeProduto.CEan = "";
        //        nfeProduto.CEANTrib = "";
        //        nfeProduto.CfopEntrada = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
        //        nfeProduto.AliqCofins = dataRowViewXXX.Row["PercentualCOFINS"].ToString().Trim();
        //        nfeProduto.AliqIpi = dataRowViewXXX.Row["PercentualIPI"].ToString().Trim();
        //        nfeProduto.AliqPis = dataRowViewXXX.Row["PercentualPIS"].ToString().Trim();
        //        nfeProduto.BaseCofins = decimal.Parse(dataRowViewXXX.Row["BaseCalcCOFINS"].ToString());
        //        nfeProduto.BaseIpi = decimal.Parse(dataRowViewXXX.Row["BaseCalcIPI"].ToString());
        //        nfeProduto.BasePis = decimal.Parse(dataRowViewXXX.Row["BaseCalcPis"].ToString());
        //        nfeProduto.PRedBC = dataRowViewXXX.Row["PercentualRed"].ToString().Trim();
        //        nfeProduto.PST = dataRowViewXXX.Row["PercentualICMSST"].ToString().Trim();
        //        nfeProduto.VBCST = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSST"].ToString().Trim());
        //        nfeProduto.VICMSSt = decimal.Parse(dataRowViewXXX.Row["ValorICMSST"].ToString().Trim());
        //        nfeProduto.PFCP = dataRowViewXXX.Row["PercentualFCP"].ToString().Trim();
        //        nfeProduto.VBCFCPST = decimal.Parse(dataRowViewXXX.Row["BaseCalcFCPST"].ToString().Trim());
        //        nfeProduto.VFCPST = decimal.Parse(dataRowViewXXX.Row["ValorFCPST"].ToString().Trim());
        //        nfeProduto.VBCSTRet = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSSTRet"].ToString().Trim());
        //        nfeProduto.VICMSSTRet = decimal.Parse(dataRowViewXXX.Row["ValorICMSRet"].ToString().Trim());
        //        nfeProduto.PFCPST = dataRowViewXXX.Row["PercentualFCPRet"].ToString().Trim();

        //        nfeProduto.CodAnp = produto.CodAnp;
        //        nfeProduto.CodEnqIpi = produto.EnqIpi;
        //        nfeProduto.CodSeloIpi = produto.CodSeloIpi;
        //        nfeProduto.CstCofins = produto.CstCofins;
        //        nfeProduto.CstIcms = dataRowViewXXX.Row["CstIcms"].ToString().Trim();
        //        nfeProduto.CstIpi = produto.CstIpi;
        //        nfeProduto.CstPis = produto.CstPis;
        //        string orig = "0";
        //        if (!String.IsNullOrEmpty(produto.OrigemIcms))
        //            orig = produto.OrigemIcms;
        //        nfeProduto.OrigemIcms = orig;
        //        nfeProduto.OutrosIcms = 0;
        //        nfeProduto.TPag = "";
        //        nfeProduto.UCom = produto.UnidadeMedida.Sigla;
        //        nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
        //        nfeProduto.ValorAcrescimo = 0;
        //        nfeProduto.ValorCofins = decimal.Parse(dataRowViewXXX.Row["ValorCOFINS"].ToString());
        //        nfeProduto.ValorDesconto = descontoItem;
        //        nfeProduto.ValorFinal = (decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString()) * decimal.Parse(quantidade.ToString())) - descontoItem;
        //        nfeProduto.UnidadeMedidaConvertida = produto.UnidadeMedida.Sigla;
        //        nfeProduto.ValorIpi = decimal.Parse(dataRowViewXXX.Row["ValorIPI"].ToString());
        //        nfeProduto.ValorPis = decimal.Parse(dataRowViewXXX.Row["ValorPIS"].ToString());
        //        nfeProduto.ValorProduto = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());

        //        listaProdutosNFe.Add(nfeProduto);
                
        //    }
        //}

        private void ajustarNumeroItem()
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["Item"].MappingName, i);
                i++;
            }
        }

        private void txtBaseCalcCOFINS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                calculoCOFINS();
                txtPercFCP.Focus();
            }
            generica.SoNumeroEVirgula(txtBaseCalcCOFINS.Texts, e);
        }

        private void txtBaseCalcCOFINS_Leave(object sender, EventArgs e)
        {
            calculoCOFINS();
        }

        private void txtPercStConsumidor_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtPercStConsumidor.Texts, e);
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if(gridProdutos.SelectedIndex >= 0)
            {
                var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
                var dataRow = (selectedItem as DataRowView).Row;
                dataRow.Delete();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro você deve clicar na linha do produto que deseja remover!");
            }
        }
    }
}
