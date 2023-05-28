using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.ContasReceber.Reports;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Telas.Vendas.RecebimentoVendas;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
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
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.Vendas
{
    public partial class FrmVendas02 : Form
    {
       
        string arquivoContigencia = "";
        string nomeArquivoContigencia = "";
        string numeroNFCe = "";
        IList<NfeProduto> listaProdutosNFe;
        bool produtoExcluido = false;
        int indexExcluido = -1;
        decimal valorTotal = 0;
        decimal valorComDesconto = 0;
        int posicaoItem = 1;
        private IList<Produto> listaProdutos;
        CaixaController caixaController = new CaixaController();
        ProdutoController produtoController = new ProdutoController();
        Produto produto = new Produto();
        GenericaDesktop generica = new GenericaDesktop();
        GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
        GridSummaryColumn summaryColumn1 = new GridSummaryColumn();
        PessoaController pessoaController = new PessoaController();
        ContaReceberController contaReceberController = new ContaReceberController();
        Venda venda = new Venda();
        CreditoClienteController creditoClienteController = new CreditoClienteController();
        ChequeController chequeController = new ChequeController();
        string codStatusRet = "";
        Nfe nfe = new Nfe();
        bool inseridoDescontoTotal = false;
        bool inseridoDescontoItem = false;
        TNFe nfGerando = new TNFe();
        String xmlStrEnvio = "";
        EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
        double pecas = 0;
        Pessoa cli = new Pessoa();
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        public FrmVendas02()
        {
            InitializeComponent();
            bloquearCamposValorQuantidade();
        }
        private void txtDinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                abrirFormPagamentoDinheiro();
                //teste
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            //Finalização na segunda tela - com pagamento
            concluirVenda(venda, false);
        }

        private void btnFinalizar01_Click(object sender, EventArgs e)
        {
            nfe = new Nfe();
            set_venda();
        }

        private void set_venda()
        {
            //Maior que um pq o header é contado como uma linha
            if (gridProdutos.RowCount > 1)
            {
                try
                {
                    venda = new Venda();

                    //Essa função é utilizada se o usuário clicar em voltar na aba 2 - Pagamento.
                    if (lblNumeroVenda.Text != "Nº Venda: 0")
                    {
                        string num = lblNumeroVenda.Text.Substring(lblNumeroVenda.Text.IndexOf(":") + 2);
                        venda.Id = int.Parse(num);
                        venda = (Venda)VendaController.getInstance().selecionar(venda);
                        calcularValorFaltantePagamento();
                    }

                    venda.Nfe = null;
                    venda.Cancelado = false;
                    venda.Concluida = false;
                    if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        Pessoa pessoaCliente = new Pessoa();
                        pessoaCliente.Id = int.Parse(txtCodCliente.Texts);
                        venda.Cliente = (Pessoa)pessoaController.selecionar(pessoaCliente);
                        venda.NomeCliente = venda.Cliente.RazaoSocial;
                        if (venda.Cliente.EnderecoPrincipal != null)
                        {
                            venda.EnderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " + venda.Cliente.EnderecoPrincipal.Numero + " - " + venda.Cliente.EnderecoPrincipal.Complemento;
                        }
                    }
                    else
                    {
                        venda.Cliente = null;
                        venda.EnderecoCliente = "";
                        venda.NomeCliente = "CONSUMIDOR DIVERSOS";
                    }
                    venda.DataVenda = DateTime.Now;
                    venda.EmpresaFilial = Sessao.empresaFilialLogada;
                    venda.NomeComputador = Environment.MachineName;
                    venda.Observacoes = "";
                    venda.PlanoConta = Sessao.parametroSistema.PlanoContaVenda;
                    venda.ValorAcrescimo = 0;
                    venda.ValorDesconto = valorTotal - valorComDesconto;
                    venda.ValorProdutos = valorTotal;
                    venda.ValorFinal = valorComDesconto;
                    if (!String.IsNullOrEmpty(lblIdDependente.Text) && !lblIdDependente.Text.Equals("ID Dependente"))
                    {
                        PessoaDependente pessoaDependente = new PessoaDependente();
                        pessoaDependente.Id = int.Parse(lblIdDependente.Text);
                        pessoaDependente = (PessoaDependente)Controller.getInstance().selecionar(pessoaDependente);
                        venda.PessoaDependente = pessoaDependente;
                    }
                    else
                        venda.PessoaDependente = null;

                    if (!String.IsNullOrEmpty(txtCodVendedor.Texts))
                    {
                        Pessoa pessoaVendedor = new Pessoa();
                        pessoaVendedor.Id = int.Parse(txtCodVendedor.Texts);
                        venda.Vendedor = (Pessoa)pessoaController.selecionar(pessoaVendedor);

                        txtVendedorSelecionado.Texts = venda.Vendedor.NomeFantasia;
                    }
                    else
                    {
                        venda.Vendedor = null;
                    }

                    Controller.getInstance().salvar(venda);
                    //Muda a visualizacao para aba de recebimento/pagamento
                    lblValorVenda.Text = venda.ValorFinal.ToString("C2", CultureInfo.CurrentCulture);
                    lblValorFaltante.Text = venda.ValorFinal.ToString("C2", CultureInfo.CurrentCulture);
                    lblValorRecebido.Text = "R$ 0,00";
                    lblNumeroVenda.Text = "Nº Venda: " + venda.Id.ToString();
                    tabControlAdv1.SelectedTab = tabPagamento;
                    calcularValorFaltantePagamento();
                }
                catch (Exception ex)
                {
                    GenericaDesktop.ShowErro(ex.Message);
                }
            }
        }

        private void txtDinheiro_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDinheiro();
        }

        private void abrirFormPagamentoDinheiro()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                object vendaFormaPagamento = new VendaFormaPagamento();
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    using (FrmDinheiro uu = new FrmDinheiro(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
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
                        switch (uu.showModalNovo(ref vendaFormaPagamento))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                break;
                        }
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
            else
            {
                GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
            }
        }

        private void abrirFormPagamentoCartao()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                IList<AdquirenteCartao> listaAdquirenteCartao = adquirenteCartaoController.selecionarTodasAdquirentes();
                if (listaAdquirenteCartao.Count > 0)
                {
                    object vendaForma = new VendaFormaPagamento();
                    Sessao.vendasRecebimento_ValorRecebido = 0;
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmCartao uu = new FrmCartao(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
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
                            switch (uu.showModalNovo(ref vendaForma))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    inserirFormaPagamento((VendaFormaPagamento)vendaForma);
                                    break;
                            }
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
                else
                {
                    GenericaDesktop.ShowAlerta("Nenhuma maquininha cadastrada, acesse utilitários/Cartões/Adquirente Cartão para fazer o cadastro");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
            }
        }


        private void abrirFormPagamentoDeposito()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                object vendaFormaPagamento = new VendaFormaPagamento();
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    using (FrmDeposito uu = new FrmDeposito(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
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
                        switch (uu.showModalNovo(ref vendaFormaPagamento))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                break;
                        }
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
            else
            {
                GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
            }
        }

        private void abrirFormPagamentoPIX()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                object vendaFormaPagamento = new VendaFormaPagamento();
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    using (FrmPix uu = new FrmPix(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
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
                        switch (uu.showModalNovo(ref vendaFormaPagamento))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                break;
                        }
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
            else
            {
                GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
            }
        }

        private void abrirFormPagamentoCrediario()
        {
            if (venda.Cliente != null)
            {
                if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
                {
                    object vendaFormaPagamento = new VendaFormaPagamento();
                    Sessao.vendasRecebimento_ValorRecebido = 0;
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmCrediario uu = new FrmCrediario(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
                        {
                            formBackground.StartPosition = FormStartPosition.Manual;
                            //formBackground.FormBorderStyle = FormBorderStyle.None;
                            formBackground.Opacity = .50d;
                            formBackground.BackColor = Color.Black;
                           // formBackground.Left = Top = 0;
                            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                            formBackground.WindowState = FormWindowState.Maximized;
                            formBackground.TopMost = false;
                            formBackground.Location = this.Location;
                            formBackground.ShowInTaskbar = false;
                            formBackground.Show();
                            uu.Owner = formBackground;
                            switch (uu.showModalNovo(ref vendaFormaPagamento))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                    break;
                            }
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
                else
                {
                    GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Para utilizar a forma de recebimento Crediário você " +
                    "precisa selecionar um cliente cadastrado, caso o cliente não tenha cadastro faça o cadastro do mesmo!");
                btnSelecionaClienteTela2.PerformClick();
            }
        }

        private void abrirFormPagamentoBoleto()
        {
            if (venda.Cliente != null)
            {
                if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
                {
                    object vendaFormaPagamento = new VendaFormaPagamento();
                    Sessao.vendasRecebimento_ValorRecebido = 0;
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmBoleto uu = new FrmBoleto(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
                        {
                            formBackground.StartPosition = FormStartPosition.Manual;
                            //formBackground.FormBorderStyle = FormBorderStyle.None;
                            formBackground.Opacity = .50d;
                            formBackground.BackColor = Color.Black;
                           // formBackground.Left = Top = 0;
                            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                            formBackground.WindowState = FormWindowState.Maximized;
                            formBackground.TopMost = false;
                            formBackground.Location = this.Location;
                            formBackground.ShowInTaskbar = false;
                            formBackground.Show();
                            uu.Owner = formBackground;
                            switch (uu.showModalNovo(ref vendaFormaPagamento))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                    break;
                            }
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
                else
                {
                    GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Para utilizar a forma de recebimento Boleto você " +
                    "precisa selecionar um cliente cadastrado, caso o cliente não tenha cadastro faça o cadastro do mesmo!");
                btnSelecionaClienteTela2.PerformClick();
            }
        }

        private void abrirFormPagamentoCheque()
        {
            if (venda.Cliente != null)
            {
                if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
                {
                    object vendaFormaPagamento = new VendaFormaPagamento();
                    Sessao.vendasRecebimento_ValorRecebido = 0;
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmCheque uu = new FrmCheque(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), venda))
                        {
                            formBackground.StartPosition = FormStartPosition.Manual;
                            //formBackground.FormBorderStyle = FormBorderStyle.None;
                            formBackground.Opacity = .50d;
                            formBackground.BackColor = Color.Black;
                           // formBackground.Left = Top = 0;
                            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                            formBackground.WindowState = FormWindowState.Maximized;
                            formBackground.TopMost = false;
                            formBackground.Location = this.Location;
                            formBackground.ShowInTaskbar = false;
                            formBackground.Show();
                            uu.Owner = formBackground;
                            switch (uu.showModalNovo(ref vendaFormaPagamento))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    inserirFormaPagamento((VendaFormaPagamento)vendaFormaPagamento);
                                    break;
                            }
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
                else
                {
                    GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Para utilizar a forma de recebimento Cheque você " +
                    "precisa selecionar um cliente cadastrado, caso o cliente não tenha cadastro faça o cadastro do mesmo!");
                btnSelecionaClienteTela2.PerformClick();
            }
        }

        private void abrirFormPagamentoCreditoCliente()
        {
            if (venda.Cliente != null)
            {
                if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
                {
                    IList<CreditoCliente> listaCredito = creditoClienteController.selecionarCreditoPorCliente(int.Parse(txtCodCliente.Texts));
                    if (listaCredito.Count > 0)
                    {
                        decimal valorSaldo = 0;
                        foreach (CreditoCliente credito in listaCredito)
                        {
                            valorSaldo = valorSaldo + (credito.Valor - credito.ValorUtilizado);
                        }
                        if (GenericaDesktop.ShowConfirmacao("Cliente com R$ " + string.Format("{0:0.00}", valorSaldo) + " deseja utilizar o saldo total?"))
                        {
                            MessageBox.Show("Em desenvolvimento");
                        }
                        //Selecionar o valor do crédito a utilizar
                        else
                        {

                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Cliente não possui crédito disponível");
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor total da venda ja foi recebido!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Para utilizar a forma de recebimento Crédito você " +
                    "precisa selecionar um cliente cadastrado!");
                btnSelecionaClienteTela2.PerformClick();
            }
        }
        private void inserirFormaPagamento(VendaFormaPagamento vendaFormaPagamento)
        {
            try
            {
                String bandeira = "";
                String aut = "";
                String condicao = "";
                String adquirente = "";
                String forma = vendaFormaPagamento.FormaPagamento.Descricao;

                if (vendaFormaPagamento.FormaPagamento.Cartao == true)
                {
                    bandeira = " " + vendaFormaPagamento.BandeiraCartao.Descricao;
                    if (!String.IsNullOrEmpty(vendaFormaPagamento.AutorizacaoCartao))
                        aut = " AUT: " + vendaFormaPagamento.AutorizacaoCartao;
                    if(vendaFormaPagamento.ParcelamentoFk != null)
                    {
                        if (vendaFormaPagamento.ParcelamentoFk.Debito == true)
                            condicao = " DÉBITO";
                        else
                            condicao = " " + vendaFormaPagamento.Parcelamento.ToString() + "X";
                    }

                        
                    adquirente = (" - ") + vendaFormaPagamento.AdquirenteCartao.Descricao;
                }
                if (vendaFormaPagamento.FormaPagamento.Crediario == true)
                {
                    condicao = " " + vendaFormaPagamento.Parcelamento.ToString() + "X";
                }
                //verifica_posicao_item_FormaPagamento();
                System.Data.DataRow row = dsPagamento.Tables[0].NewRow();
                row.SetField("Codigo", vendaFormaPagamento.FormaPagamento.Id.ToString());
                row.SetField("FormaPagamento", vendaFormaPagamento.FormaPagamento.Descricao + bandeira + condicao + aut + adquirente);
                row.SetField("ValorRecebido", string.Format("{0:0.00}", vendaFormaPagamento.ValorRecebido));
                row.SetField("VendaForma", vendaFormaPagamento.Id);
                dsPagamento.Tables[0].Rows.Add(row);

                calcularValorFaltantePagamento();
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowErro(ex.Message);
            }
        }

        private void calcularValorFaltantePagamento()
        {
            decimal valorRecebido = 0;
            decimal valorFaltante = 0;

            var records = gridRecebimento.View.Records;

            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                valorRecebido = valorRecebido + decimal.Parse(dataRowView.Row["ValorRecebido"].ToString());
            }

            lblValorRecebido.Text = valorRecebido.ToString("C2", CultureInfo.CurrentCulture);
            valorFaltante = decimal.Parse(lblValorVenda.Text.Replace("R$ ", "")) - valorRecebido;
            lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);

            if (valorFaltante == 0)
            {
                btnGerarNFCe.Enabled = true;
                btnGerarNfe.Enabled = true;
                btnFinalizarTela2.Enabled = true;
            }
            else
            {
                btnFinalizarTela2.Enabled = false;
                btnGerarNFCe.Enabled = false;
                btnGerarNfe.Enabled = false;
            }
        }

        private void txtCartao_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCartao();
        }

        private void btnDinheiro_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDinheiro();
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //if (!String.IsNullOrEmpty(txtPesquisaProduto.Texts))
                PesquisarProdutoPorDescricao(txtPesquisaProduto.Texts.Trim());
            }
        }

        private void bloquearCamposValorQuantidade()
        {
            txtQuantidade.Enabled = false;
            txtValorUnitario.Enabled = false;
            btnAdicionar.Enabled = false;
        }

        private void desbloquearCamposValorQuantidade()
        {
            txtQuantidade.Enabled = true;
            txtValorUnitario.Enabled = true;
            btnAdicionar.Enabled = true;
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
                desbloquearCamposValorQuantidade();
                foreach (Produto prod in listaProdutos)
                {
                    txtPesquisaProduto.Texts = prod.Descricao;
                    txtQuantidade.Texts = "1";
                    txtValorUnitario.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtValorTotal.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    this.produto = prod;
                    if (valorAux.Contains("*"))
                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    if (prod.Ean.Equals(valor.Trim()))
                        inserirItem(this.produto);
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
                                    desbloquearCamposValorQuantidade();
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidade.Texts = "1";
                                    txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                        inserirItem(this.produto);
                                    else
                                    {
                                        txtQuantidade.Focus();
                                        txtQuantidade.SelectAll();
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                desbloquearCamposValorQuantidade();
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtQuantidade.Texts = "1";
                                txtValorUnitario.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtValorTotal.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                this.produto = ((Produto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidade.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                    inserirItem(this.produto);
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
                bloquearCamposValorQuantidade();
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtPesquisaProduto.SelectAll();
            }
        }
        private void inserirItem(Produto produto)
        {
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;
            try
            {
                //verifica_posicao_item();
                System.Data.DataRow row = dsProduto.Tables[0].NewRow();
                row.SetField("item", posicaoItem);
                posicaoItem++;

                row.SetField("Codigo", produto.Id.ToString());
                row.SetField("Descricao", produto.Descricao);
                decimal valorUnitForm = decimal.Parse(txtValorUnitario.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Quantidade", txtQuantidade.Texts);
                decimal valorTotal = valorUnitForm * decimal.Parse(txtQuantidade.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("DescontoItem", string.Format("{0:0.00}", 0));
                row.SetField("EstoqueAuxiliar", produto.EstoqueAuxiliar);
                row.SetField("Estoque", produto.Estoque);
                row.SetField("CfopVenda", produto.CfopVenda);
                row.SetField("CstIcms", produto.CstIcms);
                dsProduto.Tables[0].Rows.Add(row);
                txtPesquisaProduto.Texts = "";
                somaEnquantoDigita();
                txtQuantidade.Texts = "";
                txtValorUnitario.Texts = "";
                txtValorTotal.Texts = "";

                this.produto = new Produto();

                if (this.gridProdutos.View.Records.Count > 0)
                {
                    gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    //this.datagrid.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                    //this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                    gridProdutos.AutoSizeController.Refresh();
                }

                bloquearCamposValorQuantidade();
                txtPesquisaProduto.Focus();
                //txtPesquisaProduto.Select();
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void txtPesquisaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private void pesquisaCliente()
        {
            lblPropriedade.Text = "";
            lblInscricaoEstadual.Text = "";
            lblIdPropriedade.Text = "";
            lblPropriedade.Visible = false;
            lblInscricaoEstadual.Visible = false;
            lblIdPropriedade.Visible = false;
            //dependente
            lblIdDependente.Text = "";
            lblDependente.Text = "";
            lblDependente.Visible = false;

            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtPesquisaCliente.Texts + "%'"))
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
                                txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtClienteAbaPagamento.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                verificarPropriedadesCliente(((Pessoa)pessoaOjeto));
                                txtPesquisaProduto.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtClienteAbaPagamento.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            verificarPropriedadesCliente(((Pessoa)pessoaOjeto));
                            txtPesquisaProduto.Focus();
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

        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtPesquisaCliente.Texts + "%' and Tabela.Vendedor = true"))
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
                                txtNomeVendedor.Texts = ((Pessoa)pessoaOjeto).NomeFantasia;
                                txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                txtVendedorSelecionado.Texts = ((Pessoa)pessoaOjeto).NomeFantasia;
                                txtPesquisaProduto.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtNomeVendedor.Texts = ((Pessoa)pessoaOjeto).NomeFantasia;
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtVendedorSelecionado.Texts = ((Pessoa)pessoaOjeto).NomeFantasia;
                            txtPesquisaProduto.Focus();
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

        private void FrmVendas02_Load(object sender, EventArgs e)
        {
            this.gridProdutos.DataSource = dsProduto.Tables["Produto"];
            this.gridRecebimento.DataSource = dsPagamento.Tables["Pagamento"];
            //this.gridProdutos.Style.RowHeaderStyle.BackColor = Color.FromArgb(0, 192, 0);
            //this.gridProdutos.Style.RowHeaderStyle.BackColor = Color.White;
            //this.gridProdutos.ShowRowHeader = true;
            //this.gridProdutos.Style.RowHeaderStyle.SelectionBackColor = Color.White;
            //this.gridProdutos.Style.RowHeaderStyle.SelectionMarkerColor = Color.Red;


            //this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
            txtPesquisaProduto.Focus();

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            if (w == 1920 && h == 1080)
            {
               // MessageBox.Show("Full HD");
                this.panelPagamento.Location = new Point(this.panel6.Size.Width / 3, this.panelPagamento.Location.Y);
                this.panel4.Location = new Point(this.panel6.Size.Width / 2, this.panel4.Location.Y);
            }
            else
            {
               // MessageBox.Show("Não é Full HD");
                this.panelPagamento.Location = new Point(this.panel6.Size.Width / 9, this.panelPagamento.Location.Y);
                this.panel4.Location = new Point(this.panel6.Size.Width / 4, this.panel4.Location.Y);
                // lblNumeroVenda.Location = new Point(this.panel6.Size.Width / 3, this.lblNumeroVenda.Location.Y);
                //teste
            }

            lblFormaPagamento.TextAlign = HorizontalAlignment.Center;
            lblFormaPagamento.BorderColor = System.Drawing.Color.White;
            lblFormaPagamento.Enabled = false;


        }

        private void FrmVendas02_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabControlAdv1.SelectedTab == tabVenda)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;

                    case Keys.F2:
                        btnPesquisaCliente.PerformClick();
                        break;

                    case Keys.F1:
                        this.TopMost = false;
                        generica.abrirNavegador();
                        break;

                    case Keys.F5:
                        btnFinalizar01.PerformClick();
                        break;

                    case Keys.F6:
                        btnDescontoItem.PerformClick();
                        break;

                    case Keys.Delete:
                        if (tabControlAdv1.SelectedTab == tabVenda)
                        {
                            btnExcluirItem.PerformClick();
                        }
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        btnGerarNfe.PerformClick();
                        break;
                    case Keys.F2:
                        btnGerarNFCe.PerformClick();
                        break;
                    case Keys.F6:
                        btnDinheiro.PerformClick();
                        break;

                    case Keys.F7:
                        btnCartaoCredito.PerformClick();
                        break;

                    case Keys.F8:
                        btnPIX.PerformClick();
                        break;

                    case Keys.F9:
                        btnDeposito.PerformClick();
                        break;

                    case Keys.F10:
                        btnCrediario.PerformClick();
                        break;
                    case Keys.F11:
                        btnCheque.PerformClick();
                        break;
                    case Keys.F12:
                        btnCreditoCliente.PerformClick();
                        break;
                }
            }
            //if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.D)
            //{
            //    if(tabControlAdv1.SelectedTab == tabPagamento)
            //        btnDinheiro.PerformClick();
            //}
        }


        private void verifica_posicao_item_FormaPagamento()
        {
            //   Percorre todo o grid, e preenche a posicao dos itens
            posicaoItem = 1;
            int linha = 0;

            var records = gridProdutos.View.Records;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                if (dataRowView != null)
                {
                    dataRowView.Row["item"] = posicaoItem;
                    linha++;
                    posicaoItem++;
                }
            }
        }

        private void somaEnquantoDigita()
        {
            try
            {
                valorTotal = 0;
                valorComDesconto = 0;
                pecas = 0;
                var records = gridProdutos.View.Records;
                decimal descontoItem = 0;
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    //     e.Style.Font.FontStyle = FontStyle.Strikeout;
                    if (dataRowView != null && !dataRowView["ItemExcluido"].ToString().Equals("True"))
                    {
                        if(!String.IsNullOrEmpty(dataRowView.Row[7].ToString()))
                            descontoItem = descontoItem + decimal.Parse(dataRowView.Row[7].ToString());
                        valorTotal = valorTotal + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());
                    }
                    else
                    {

                    }
                }
                if(inseridoDescontoItem == true)
                    txtDesconto.Texts = descontoItem.ToString("C2", CultureInfo.CurrentCulture);

               // decimal descontoPercentualNaVenda = 0;
                //if(inseridoDescontoTotal == true)
                //{
                //    //Ratear desconto nos produtos
                //    descontoPercentualNaVenda = (decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                //    MessageBox.Show("O desconto percentual foi de " + descontoPercentualNaVenda);
                //}

                txtValorTotalProdutos.Texts = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
                txtTotalItens.Texts = pecas + " Total";
                if(!String.IsNullOrEmpty(txtDesconto.Texts))
                {
                    valorComDesconto = valorTotal - decimal.Parse(txtDesconto.Texts.Replace("R$ ", ""));
                }
                txtTotalComDesconto.Texts = valorComDesconto.ToString("C2", CultureInfo.CurrentCulture);
            }
            catch
            {

            }
        }

        private void totalizadorProdutos()
        {
            this.gridProdutos.SummaryCalculationUnit = Syncfusion.Data.SummaryCalculationUnit.Mixed;
            tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Title = "Total : {ValorTotal}";
            tableSummaryRow1.Name = "tableSummary";
            tableSummaryRow1.ShowSummaryInRow = false;
            tableSummaryRow1.Position = VerticalPosition.Bottom;

            summaryColumn1 = new GridSummaryColumn();
            summaryColumn1.Name = "ValorTotalGrid";
            summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn1.Format = "Total : {Sum:c}";
            summaryColumn1.MappingName = "ValorTotal";

            tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
            this.gridProdutos.TableSummaryRows.Add(tableSummaryRow1);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            inserirItem(this.produto);
        }


        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                generica.SoNumeroEVirgula(txtQuantidade.Texts, e);
                if (e.KeyChar == 13)
                {
                    if (txtValorUnitario.Enabled == true)
                    {
                        txtValorUnitario.Focus();
                        txtValorUnitario.Select();
                        txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
                    }
                    else
                        inserirItem(produto);
                }
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void txtValorUnitario__TextChanged(object sender, EventArgs e)
        {
            try
            {

                txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            }
            catch
            {

            }
        }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                generica.SoNumeroEVirgula(txtValorUnitario.Texts, e);
                if (e.KeyChar == 13)
                {
                    if (txtValorUnitario.Enabled == true)
                    {
                        txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
                        inserirItem(produto);
                    }
                }
            }
            catch
            {

            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtPesquisaCliente.Texts))
                {
                    txtPesquisaCliente.Texts = "";
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
                                txtPesquisaCliente.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                txtNomeVendedor.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtNomeVendedor.Focus();
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

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            //Marcar linha excluida
            if (e.RowIndex.Equals(indexExcluido) && produtoExcluido == true)
            {
                e.Style.Font.FontStyle = FontStyle.Strikeout;
                e.Style.TextColor = Color.Red;
                produtoExcluido = false;
                indexExcluido = -1;
            }
            var dataRowView = e.RowData as DataRowView;
            if (dataRowView != null)
            {
                var dataRow = dataRowView.Row;
                double estoqueAuxiliar = double.Parse(dataRow["EstoqueAuxiliar"].ToString());
                double estoque = double.Parse(dataRow["Estoque"].ToString());
                if (estoqueAuxiliar <= 0 && Sessao.parametroSistema.AlertaEstoqueGerencial == true)
                    e.Style.TextColor = Color.Red;
                if (estoque <= 0 && Sessao.parametroSistema.AlertaEstoqueFiscal == true)
                    e.Style.TextColor = Color.Red;
            }
        }


        private void verificarPropriedadesCliente(Pessoa cliente)
        {
            lblPropriedade.Text = "";
            lblInscricaoEstadual.Text = "";
            lblIdPropriedade.Text = "";
            lblPropriedade.Visible = false;
            lblInscricaoEstadual.Visible = false;
            lblIdPropriedade.Visible = false;

            PessoaPropriedadeController pessoaPropriedadeController = new PessoaPropriedadeController();
            IList<PessoaPropriedade> listaPropriedades = new List<PessoaPropriedade>();
            listaPropriedades = pessoaPropriedadeController.selecionarPropriedadesPorPessoa(cliente.Id);
            if(listaPropriedades.Count > 0)
            {
                //GenericaDesktop.ShowInfo("Cliente possui propriedades cadastradas, selecione a propriedade para ativar a inscrição estadual e endereço corretamente!");
                Object pessoaPropriedadeOjeto = new PessoaPropriedade();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PessoaPropriedade", "and Tabela.Pessoa = " + cliente.Id))
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
                        switch (uu.showModal("PessoaPropriedade", "", ref pessoaPropriedadeOjeto))
                        {
                            case DialogResult.Ignore:
                                lblPropriedade.Text = "";
                                lblInscricaoEstadual.Text = "";
                                lblIdPropriedade.Text = "";
                                lblPropriedade.Visible = false;
                                lblInscricaoEstadual.Visible = false;
                                lblIdPropriedade.Visible = false;
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                lblPropriedade.Visible = true;
                                lblInscricaoEstadual.Visible = true;
                                lblPropriedade.Text = "Propriedade: " + ((PessoaPropriedade)pessoaPropriedadeOjeto).Descricao;
                                lblInscricaoEstadual.Text = "Inscrição Estadual: " + GenericaDesktop.RemoveCaracteres(((PessoaPropriedade)pessoaPropriedadeOjeto).InscricaoEstadual);
                                lblIdPropriedade.Text = ((PessoaPropriedade)pessoaPropriedadeOjeto).Id.ToString();

                                if (String.IsNullOrEmpty(((PessoaPropriedade)pessoaPropriedadeOjeto).InscricaoEstadual))
                                    GenericaDesktop.ShowAlerta("Propriedade sem informação de inscrição estadual, caso emita uma NFe não será destacado a IE desta propriedade!");
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

        private void selecionarDependente(Pessoa cliente)
        {
            lblIdDependente.Text = "";
            lblDependente.Text = "";
            lblDependente.Visible = false;
            PessoaDependenteController pessoaDependenteController = new PessoaDependenteController();
            IList<PessoaDependente> listaDependentes = new List<PessoaDependente>();
            listaDependentes = pessoaDependenteController.selecionarDependentePorPessoa(cliente.Id);
            if (listaDependentes.Count > 0)
            {
                //GenericaDesktop.ShowInfo("Cliente possui propriedades cadastradas, selecione a propriedade para ativar a inscrição estadual e endereço corretamente!");
                Object pessoaDependenteOjeto = new PessoaDependente();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PessoaDependente", "and Tabela.Pessoa = " + cliente.Id))
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
                        switch (uu.showModal("PessoaDependente", "", ref pessoaDependenteOjeto))
                        {
                            case DialogResult.Ignore:
                                lblDependente.Text = "";
                                lblIdDependente.Text = "";
                                lblDependente.Visible = false;
                                lblIdDependente.Visible = false;
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                lblDependente.Visible = true;
                                string dependente = ((PessoaDependente)pessoaDependenteOjeto).Nome;
                                if (dependente.Length > 24)
                                    dependente = dependente.Substring(0, 24) + "...";
                                lblDependente.Text = "Dependente: " + dependente;
                                lblIdDependente.Text = ((PessoaDependente)pessoaDependenteOjeto).Id.ToString();
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                        }
                        uu.Dispose();
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

        //private void verificaEstoqueGrid()
        //{
        //    var records = gridProdutos.View.Records;
        //    foreach (var record in records)
        //    {
        //        var dataRowViewXXX = record.Data as DataRowView;
        //        if (dataRowViewXXX != null && !dataRowViewXXX["ItemExcluido"].ToString().Equals("True"))
        //        {
        //            NfeProduto nfeProduto = new NfeProduto();
        //            produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);


        //        }
        //    }
        //}

        private void deletarItem(int index)
        {
            //Ao deletar o sistema vai no querystyle do grid e modifica a cor, observar la tb.
            gridProdutos.SelectedIndex = index;
            produtoExcluido = true;
            indexExcluido = index;

            // aqui eu precisava marcar o checkbox ou entao no metodo soma_enquanto_digita nao somar as linhas riscadas...
            //as linhas riscadas tem a funcao FontStyle.Strikeout;
            gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns[6].MappingName, true);
            gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns[7].MappingName, 0);

            //System.Data.DataRow row = dsProduto.Tables[0].Rows[index - 1];
            //row.SetField("ItemExcluido", true);
            //verifica_posicao_item();
            somaEnquantoDigita();
            gridProdutos.SelectedIndex = -1;
        }
        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedIndex >= 0)
            {
                deletarItem(gridProdutos.SelectedIndex + 1);
                if (!String.IsNullOrEmpty(txtDesconto.Texts))
                {
                    if (decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) > 0)
                        GenericaDesktop.ShowAlerta("Atenção, você ja inseriu desconto na venda, confira novamente se o desconto está correto!");

                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Clique no item que deseja excluir");
            }
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar esta venda?"))
            {
                //Venda venda = new Venda();
                posicaoItem = 1;
                txtPesquisaCliente.Texts = "";
                txtPesquisaProduto.Texts = "";
                dsProduto.Tables[0].Clear();
                txtQuantidade.Texts = "1";
                txtValorUnitario.Texts = "0,00";
                txtValorTotal.Texts = "0,00";
                txtValorTotalProdutos.Texts = "R$ 0,00";
                txtTotalItens.Texts = "0 Itens";
            }
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            }
            catch
            {

            }
        }

        private void btnDadosCliente_Click(object sender, EventArgs e)
        {

        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuantidade.Texts))
                txtQuantidade.Texts = "1";
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            pesquisaVendedor();
        }

        private void btnSelecionaClienteTela2_Click(object sender, EventArgs e)
        {
            pesquisaCliente();
            if (!String.IsNullOrEmpty(txtClienteAbaPagamento.Texts))
            {
                Pessoa pessoaCliente = new Pessoa();
                pessoaCliente.Id = int.Parse(txtCodCliente.Texts);
                pessoaCliente = (Pessoa)pessoaController.selecionar(pessoaCliente);
                venda.Cliente = pessoaCliente;
                venda.NomeCliente = pessoaCliente.RazaoSocial;
                if (venda.Cliente.EnderecoPrincipal != null)
                {
                    venda.EnderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " + venda.Cliente.EnderecoPrincipal.Numero + " - " + venda.Cliente.EnderecoPrincipal.Complemento;
                }
                Controller.getInstance().salvar(venda);
            }
        }

        private void btnSelecionaVendedorTela2_Click(object sender, EventArgs e)
        {
            pesquisaVendedor();
            if (!String.IsNullOrEmpty(txtVendedorSelecionado.Texts))
            {
                Pessoa pessoaVendedor = new Pessoa();
                pessoaVendedor.Id = int.Parse(txtCodVendedor.Texts);
                pessoaVendedor = (Pessoa)pessoaController.selecionar(pessoaVendedor);
                venda.Vendedor = pessoaVendedor;
                Controller.getInstance().salvar(venda);
            }
        }


        //Cores Ao Passar Mouse no pagamento
        //private void btnDinheiro_MouseHover(object sender, EventArgs e)
        //{
        //    btnDinheiro.BorderColor = Color.FromArgb(0, 192, 0);
        //}

        //private void btnDinheiro_MouseLeave(object sender, EventArgs e)
        //{
        //    btnDinheiro.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnCartaoCredito_MouseHover(object sender, EventArgs e)
        //{
        //    btnCartaoCredito.BorderColor = Color.FromArgb(0, 0, 192);
        //}

        //private void btnCartaoCredito_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCartaoCredito.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnPIX_MouseHover(object sender, EventArgs e)
        //{
        //    btnPIX.BorderColor = Color.FromArgb(64, 64, 64);
        //}

        //private void btnPIX_MouseLeave(object sender, EventArgs e)
        //{
        //    btnPIX.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnDeposito_MouseHover(object sender, EventArgs e)
        //{
        //    btnDeposito.BorderColor = Color.Olive;
        //}

        //private void btnDeposito_MouseLeave(object sender, EventArgs e)
        //{
        //    btnDeposito.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnCrediario_MouseHover(object sender, EventArgs e)
        //{
        //    btnCrediario.BorderColor = Color.Black;
        //}

        //private void btnCrediario_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCrediario.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnCheque_MouseHover(object sender, EventArgs e)
        //{
        //    btnCheque.BorderColor = Color.FromArgb(255, 128, 0);
        //}

        //private void btnCheque_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCheque.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void btnCreditoCliente_MouseHover(object sender, EventArgs e)
        //{
        //    btnCreditoCliente.BorderColor = Color.Purple;
        //}

        //private void btnCreditoCliente_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCreditoCliente.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void IconeDinheiro_MouseHover(object sender, EventArgs e)
        //{
        //    btnDinheiro.BorderColor = Color.FromArgb(0, 192, 0);
        //}

        //private void IconeDinheiro_MouseLeave(object sender, EventArgs e)
        //{
        //    btnDinheiro.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconeCartao_MouseHover(object sender, EventArgs e)
        //{
        //    btnCartaoCredito.BorderColor = Color.FromArgb(0, 0, 192);
        //}

        //private void iconeCartao_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCartaoCredito.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconePix_MouseHover(object sender, EventArgs e)
        //{
        //    btnPIX.BorderColor = Color.FromArgb(64, 64, 64);
        //}

        //private void iconePix_MouseLeave(object sender, EventArgs e)
        //{
        //    btnPIX.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconeDeposito_MouseHover(object sender, EventArgs e)
        //{
        //    btnDeposito.BorderColor = Color.Olive;
        //}

        //private void iconeDeposito_MouseLeave(object sender, EventArgs e)
        //{
        //    btnDeposito.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconeCrediario_MouseHover(object sender, EventArgs e)
        //{
        //    btnCrediario.BorderColor = Color.Black;
        //}

        //private void iconeCrediario_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCrediario.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconeCheque_MouseHover(object sender, EventArgs e)
        //{
        //    btnCheque.BorderColor = Color.FromArgb(255, 128, 0);
        //}

        //private void iconeCheque_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCheque.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        //private void iconeCredito_MouseHover(object sender, EventArgs e)
        //{
        //    btnCreditoCliente.BorderColor = Color.Purple;
        //}

        //private void iconeCredito_MouseLeave(object sender, EventArgs e)
        //{
        //    btnCreditoCliente.BorderColor = Color.FromArgb(229, 229, 229);
        //}

        private void btnCartaoCredito_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCartao();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja retornar a tela anterior?"))
            {
                tabControlAdv1.SelectedTab = tabVenda;
            }
        }
        private void gridPagamento_QueryRowStyle(object sender, QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnExcluirRecebimento_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluirRecebimento_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (gridRecebimento.RowCount > 0 && gridRecebimento.SelectedIndex >= 0)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir o recebimento selecionado?"))
                    {
                        System.Data.DataRow row = dsPagamento.Tables[0].Rows[gridRecebimento.SelectedIndex];
                        VendaFormaPagamento vfp = new VendaFormaPagamento();

                        vfp.Id = int.Parse(row.ItemArray[3].ToString());
                        vfp = (VendaFormaPagamento)Controller.getInstance().selecionar(vfp);

 
                        //Somente se for forma de pagamento que tem conta a receber
                        if (vfp.FormaPagamento.Crediario == true || vfp.FormaPagamento.Cheque == true || vfp.FormaPagamento.Boleto == true)
                        {
                            ContaReceber contaReceber = new ContaReceber();
                            IList<ContaReceber> listaContasReceber = contaReceberController.selecionarContaReceberPorVendaFormaPagamento(vfp.Id);
                            if (listaContasReceber.Count > 0)
                            {
                                foreach (ContaReceber contRec in listaContasReceber)
                                {
                                    contRec.Descricao = contRec.Descricao + " - EXCL. DURANTE VENDA";
                                    Controller.getInstance().salvar(contRec);
                                    Controller.getInstance().excluir(contRec);
                                }
                            }
                        }
                        //Somente se for forma de pagamento cheque
                        if (vfp.FormaPagamento.Cheque == true)
                        {
                            Cheque cheque = new Cheque();
                            IList<Cheque> listaCheques = chequeController.selecionarChequesPorVenda(venda.Id);
                            if (listaCheques.Count > 0)
                            {
                                foreach (Cheque chequeSel in listaCheques)
                                {
                                    chequeSel.Descricao = chequeSel.Descricao + " - EXCL. DURANTE VENDA";
                                    Controller.getInstance().salvar(chequeSel);
                                    Controller.getInstance().excluir(chequeSel);
                                }
                            }
                        }

                        if (vfp.FormaPagamento.Caixa == true)
                        {
                            IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorOrigem("Venda", venda.Id.ToString());
                            if (listaCaixa.Count > 0)
                            {
                                foreach (Caixa caixa in listaCaixa)
                                {
                                    caixa.Descricao = caixa.Descricao + " - EXCL. DURANTE VENDA";
                                    Controller.getInstance().salvar(caixa);
                                    Controller.getInstance().excluir(caixa);
                                }
                            }
                        }

                        Controller.getInstance().excluir(vfp);
                        row.Delete();
                        calcularValorFaltantePagamento();
                    }
                }
            }
            catch (Exception erro)
            {

                GenericaDesktop.ShowErro("Erro ao excluir recebimento, solicite o suporte e informe o erro " + erro.Message);
            }
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDeposito();
        }

        private void btnPIX_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoPIX();
        }

        private void btnCrediario_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCrediario();
        }

        private void btnCreditoCliente_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCreditoCliente();
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCheque();
        }

        private void btnGerarNFCe_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(abrirAguarde2);
            th.Start();
            gerarNFCe();
            th.Join();
        }

        private void abrirAguarde2()
        {
            FrmAguarde frm = new FrmAguarde("5000", nfe);
            frm.ShowDialog();
        }
        private void gerarNFCe()
        {
            Sessao.teveRetornoApi = false;
            //se nao tem cliente ja vem validado
            bool validaCliente = true;
            //enviarNFCe();
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa cli = new Pessoa();
                cli.Id = int.Parse(txtCodCliente.Texts);
                cli = (Pessoa)pessoaController.selecionar(cli);
                validaCliente = validarClienteNFCe(cli);
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
                        //Concluir a venda antes de gerar a nota
                        concluirVenda(venda, true);
                        numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFCe;
                        Nfe nfConferencia = new Nfe();
                        NfeController nfeController = new NfeController();
                        nfConferencia = nfeController.selecionarUltimoNumeroNota("65");
                        if (nfConferencia != null)
                        {
                            if (nfConferencia.Id > 0)
                            {
                                if (numeroNFCe != (int.Parse(nfConferencia.NNf) + 1).ToString())
                                    numeroNFCe = (int.Parse(nfConferencia.NNf.ToString()) + 1).ToString();
                            }
                        }
                        xmlStrEnvio = emitirNFCe.gerarXMLNfce(valorTotal, valorComDesconto, decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")), numeroNFCe, listaProdutosNFe, venda.Cliente, venda, null);
                        if (!String.IsNullOrEmpty(xmlStrEnvio))
                        {
                            enviarXMLNFCeParaApi(xmlStrEnvio);
                            limparCampos();
                        }
                        atualizarProximoNumeroNota();
                    }
                }
                catch (Exception erro)
                {
                    Sessao.teveRetornoApi = true;
                    GenericaDesktop.ShowErro(erro.Message);
                }
            }
        }


        private void carregarListaProdutos()
        {
            listaProdutosNFe = new List<NfeProduto>();
            var records = gridProdutos.View.Records;
            int i = 0;
            foreach (var record in records)
            {
                i++;
                var dataRowViewXXX = record.Data as DataRowView;
                if (dataRowViewXXX != null && !dataRowViewXXX["ItemExcluido"].ToString().Equals("True"))
                {
                    NfeProduto nfeProduto = new NfeProduto();
                    produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
                    double quantidade = double.Parse(dataRowViewXXX.Row["Quantidade"].ToString());
                    decimal descontoItem = decimal.Parse(dataRowViewXXX.Row["DescontoItem"].ToString());
                    produto.ValorVenda = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    nfeProduto.Item = i.ToString();
                    nfeProduto.Produto = produto;
                    nfeProduto.QCom = quantidade.ToString();
                    nfeProduto.Ncm = produto.Ncm;
                    nfeProduto.Cest = produto.Cest;
                    nfeProduto.Cfop = produto.CfopVenda;
                    nfeProduto.CProd = produto.Id.ToString();
                    nfeProduto.Nfe = null;
                    nfeProduto.VProd = produto.ValorVenda;
                    nfeProduto.QTrib = quantidade;
                    nfeProduto.VDesc = descontoItem;
                    nfeProduto.DescricaoInterna = produto.Descricao;
                    nfeProduto.XProd = produto.Descricao;
                    nfeProduto.CodigoInterno = produto.Id.ToString();
                    nfeProduto.CEan = "";
                    nfeProduto.CEANTrib = "";
                    nfeProduto.CfopEntrada = "";
                    nfeProduto.AliqCofins = produto.PercentualCofins;
                    nfeProduto.AliqIpi = produto.PercentualIpi;
                    nfeProduto.AliqPis = produto.PercentualPis;
                    nfeProduto.BaseCofins = 0;
                    nfeProduto.BaseIpi = 0;
                    nfeProduto.BasePis = 0;
                    nfeProduto.CodAnp = produto.CodAnp;
                    nfeProduto.CodEnqIpi = produto.EnqIpi;
                    nfeProduto.CodSeloIpi = produto.CodSeloIpi;
                    nfeProduto.CstCofins = produto.CstCofins;
                    nfeProduto.CstIcms = produto.CstIcms;
                    nfeProduto.CstIpi = produto.CstIpi;
                    nfeProduto.CstPis = produto.CstPis;
                    nfeProduto.OrigemIcms = produto.OrigemIcms;
                    nfeProduto.OutrosIcms = 0;
                    nfeProduto.TPag = "";
                    nfeProduto.UCom = produto.UnidadeMedida.Sigla;
                    nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorAcrescimo = 0;
                    nfeProduto.ValorCofins = 0;
                    nfeProduto.ValorDesconto = descontoItem;
                    nfeProduto.ValorFinal = (produto.ValorVenda * decimal.Parse(quantidade.ToString())) - descontoItem;
                    nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorIpi = 0;
                    nfeProduto.ValorPis = 0;
                    nfeProduto.ValorProduto = produto.ValorVenda;

                    listaProdutosNFe.Add(nfeProduto);
                }
            } 
        }

        private void enviarXMLNFCeParaApi(string xmlNfce)
        {
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFCePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFCe);
            string caminhoXML = @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            codStatusRet = "";
            gravarXMLNaPasta(xmlNfce, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", false);
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string ambiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    ambiente = "1";
                String retorno = NSSuite.emitirNFCeSincrono(xmlNfce, "xml", Sessao.empresaFilialLogada.Cnpj, ambiente, caminhoXML, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);
                Sessao.teveRetornoApi = true;
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
                    Controller.getInstance().salvar(nfe);
                    
                    //Estoque
                    AtualizaEstoque(true, "NF SAÍDA: " + nfe.NNf + " MOD: " + nfe.Modelo);
                    armazenaXmlAutorizadoNoBanco();
                    GenericaDesktop.ShowInfo("Venda concluída com Sucesso, nota autorizada!");
                   
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

                    //EnviaXML PAINEL LUNAR 
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    byte[] arquivo;
                    using (var stream = new FileStream(caminhoXML + nfe.Chave + "-procNFCe.xml", FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            arquivo = reader.ReadBytes((int)stream.Length);
                            var ret = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                            {
                                nfe.Nuvem = true;
                                Controller.getInstance().salvar(nfe);
                                
                            }
                        }
                    }   
                }

                //Falha conexao
                else if (retornoNFCe.motivo.Contains("timeout") || retornoNFCe.cStat.Equals("999"))
                {
                    //gerar em contigencia
                    Sessao.teveRetornoApi = true;
                    gravarXMLNaPasta(xmlNfce, numeroNFCe, @"\XML\Tentativa\NFCe\", numeroNFCe, true);
                }

                else
                {
                    Sessao.teveRetornoApi = true;
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
                    GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            //Se nao tem internet gera em contigencia tb
            else
            {
                Sessao.teveRetornoApi = true;
                gravarXMLNaPasta(xmlStrEnvio, nfe.NNf, @"\XML\Tentativa\NFCe\", nfe.NNf + ".xml", true);
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
            if(!(caminhoArmazenamento.Length - 1).Equals(@"\"))
            {
                caminhoArmazenamento = caminhoArmazenamento + @"\"; 
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
            if(emiteContigencia == true)
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
                    GenericaDesktop.ShowAlerta("Venda Concluída, porém a pasta de envio em contigência não foi configurada, " +
                        "favor solicite suporte a sua revenda autorizada e solicite a configuração, enquanto isso sua nota ficará " +
                        "na tela de gerenciamento de notas para você tentar reenviar a sefaz");          
            }
        }

        private void abrirFormAguardar()
        {
            FrmAguarde uu = new FrmAguarde("5000", this.nfe);
            uu.ShowDialog();
        }
        private async void aguardarParaLerRetornoContigencia(string arquivoTXT)
        {
            await GenericaDesktop.VerificaProgramaContigenciaEstaEmExecucao();
            
            //Aguarda até gerar o arquivo txt na pasta de retorno ou em 10 segundos retorna com falha
            abrirFormAguardar();

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
                        //System.Diagnostics.Process.Start(caminhoPDF);
                    }
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
                        GenericaDesktop.ShowAlerta("Venda Concluída, porém a nota fiscal nao teve retorno de autorização, verifique depois na tela de gerenciamento de notas!");
                    }
                }
            }
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
                    if(DadosColetados.Length > 4)
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
                GenericaDesktop.ShowInfo("Venda Concluida com Sucesso, Nota Fiscal gerada em Contigência!");
            }
            return chave;
        }

        private void salvarProdutosVenda()
        {
            IList<VendaItens> listaProdutosVenda = new List<VendaItens>();
            var records = gridProdutos.View.Records;
            //int i = 0;
            foreach (var record in records)
            {
                var dataRowViewXXX = record.Data as DataRowView;
                if (dataRowViewXXX != null && !dataRowViewXXX["ItemExcluido"].ToString().Equals("True"))
                {
                    produto = new Produto();
                    VendaItens vendaItens = new VendaItens();
                    produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
                    double quantidade = double.Parse(dataRowViewXXX.Row["Quantidade"].ToString());
                    decimal descontoItem = decimal.Parse(dataRowViewXXX.Row["DescontoItem"].ToString());
                    produto.ValorVenda = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    vendaItens.Produto = produto;
                    vendaItens.Quantidade = quantidade;
                    vendaItens.Ncm = produto.Ncm;
                    vendaItens.Cest = produto.Cest;
                    vendaItens.Cfop = produto.CfopVenda;
                    vendaItens.DescricaoProduto = produto.Descricao;

                    vendaItens.ValorProduto = produto.ValorVenda;
                    vendaItens.AliqCofins = produto.PercentualCofins;
                    vendaItens.AliqIcms = produto.PercentualIcms;
                    vendaItens.AliqIpi = produto.PercentualIpi;
                    vendaItens.AliqPis = produto.PercentualPis;

                    vendaItens.CodAnp = produto.CodAnp;
                    vendaItens.CodEnqIpi = produto.EnqIpi;
                    vendaItens.CodSeloIpi = produto.CodSeloIpi;
                    vendaItens.CstCofins = produto.CstCofins;
                    vendaItens.CstIcms = produto.CstIcms;
                    vendaItens.CstIpi = produto.CstIpi;
                    vendaItens.CstPis = produto.CstPis;
                    vendaItens.EmpresaFilial = Sessao.empresaFilialLogada;
                    vendaItens.OrigemIcms = produto.OrigemIcms;
                    vendaItens.OutrosIcms = 0;
                    vendaItens.PercGlp = produto.PercGlp;
                    vendaItens.PercGni = produto.PercGni;
                    vendaItens.PercGnn = produto.PercGnn;
                    try { vendaItens.UnidadeMedida = produto.UnidadeMedida.Sigla; } catch { vendaItens.UnidadeMedida = ""; }
                    vendaItens.ValorAcrescimo = 0;
                    vendaItens.ValorCofins = 0;
                    vendaItens.ValorDesconto = descontoItem;
                    vendaItens.ValorFinal = (produto.ValorVenda * decimal.Parse(quantidade.ToString())) - descontoItem;
                    try { vendaItens.ValorIcms = vendaItens.ValorFinal / decimal.Parse(quantidade.ToString()) * decimal.Parse(produto.PercentualIcms); } catch { vendaItens.ValorIcms = 0; }
                    vendaItens.ValorIpi = 0;
                    vendaItens.ValorIsentoCofins = 0;
                    vendaItens.ValorIsentoIcms = 0;
                    vendaItens.ValorIsentoPis = 0;
                    vendaItens.ValorPartida = produto.ValorPartida;
                    vendaItens.ValorPis = 0;
                    vendaItens.Venda = venda;

                    Controller.getInstance().salvar(vendaItens);  
                }
            }
        }

        private void btnDescontoGeral_Click(object sender, EventArgs e)
        {
            decimal descontoEmItens = 0;
            var records = gridProdutos.View.Records;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                if(!String.IsNullOrEmpty(dataRowView.Row[7].ToString()))
                    descontoEmItens = descontoEmItens + decimal.Parse(dataRowView.Row[7].ToString());
            }
            if (descontoEmItens <= 0)
            {
                decimal valorOriginal = valorTotal;
                Form formBackground = new Form();
                try
                {
                    using (FrmDescontoTotal uu = new FrmDescontoTotal(valorOriginal, false))
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
                        switch (uu.showModalNovo(ref valorOriginal))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                break;
                            case DialogResult.OK:
                                try
                                {
                                    txtDesconto.Texts = (valorTotal - valorOriginal).ToString("C2", CultureInfo.CurrentCulture);
                                    txtTotalComDesconto.Texts = valorOriginal.ToString("C2", CultureInfo.CurrentCulture);
                                    valorComDesconto = valorOriginal;
                                    inseridoDescontoItem = false;
                                    inseridoDescontoTotal = true;
                                    decimal descontoPercentualNaVenda = (decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                                    ratearDescontoItens(descontoPercentualNaVenda);
                                }
                                catch
                                {
                                    GenericaDesktop.ShowErro("Valor Inválido");
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
            }
            else
            {
                if (GenericaDesktop.ShowConfirmacao("Você possui desconto em itens avulsos, ao adicionar desconto no total da venda é desconsiderado o desconto em itens, deseja continuar?"))
                {       
                    var records2 = gridProdutos.View.Records;
                    int i = 1;
                    foreach (var record in records2)
                    {
                        var dataRowView = record.Data as DataRowView;
                        gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns[7].MappingName, 0);
                        i++;
                    }

                    decimal valorOriginal = valorTotal;
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmDescontoTotal uu = new FrmDescontoTotal(valorOriginal, false))
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
                            switch (uu.showModalNovo(ref valorOriginal))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    break;
                                case DialogResult.OK:
                                    try
                                    {
                                        txtDesconto.Texts = (valorTotal - valorOriginal).ToString("C2", CultureInfo.CurrentCulture);
                                        txtTotalComDesconto.Texts = valorOriginal.ToString("C2", CultureInfo.CurrentCulture);
                                        valorComDesconto = valorOriginal;
                                        inseridoDescontoItem = false;
                                        inseridoDescontoTotal = true;
                                        decimal descontoPercentualNaVenda = (decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                                        ratearDescontoItens(descontoPercentualNaVenda);
                                    }
                                    catch
                                    {
                                        GenericaDesktop.ShowErro("Valor Inválido");
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
                }
            }
        }

        private void ratearDescontoItens(decimal percentualDesconto)
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            decimal somarDescontoTotal = 0;
            decimal valorDescontoInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                //if (!String.IsNullOrEmpty(dataRowView.Row[7].ToString()))
                //    somaValorTotalDescontoItens = somaValorTotalDescontoItens + decimal.Parse(dataRowView.Row[7].ToString());

                decimal descontoItem = (decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) * percentualDesconto) / 100;
                descontoItem = Math.Round(descontoItem, 2);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns[7].MappingName, descontoItem);
                i++;
                somarDescontoTotal = somarDescontoTotal + decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
                valorDescontoInformado = decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
            }
            String descontoInformado = somarDescontoTotal.ToString("N2");
            string valorDescontoItensInf = somarDescontoTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (txtDesconto.Texts != valorDescontoItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) - somarDescontoTotal;
               // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if(somarDescontoTotal > decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado - diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns[7].MappingName, valorDescontoInformado);

                }
                else if (somarDescontoTotal < decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado + diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns[7].MappingName, valorDescontoInformado);
                }
            }
        }

        private void btnDescontoItem_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedIndex >= 0)
            {
                var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
                var dataRow = (selectedItem as DataRowView).Row;
                decimal valorOriginal = decimal.Parse(dataRow["ValorTotal"].ToString());
                decimal somaValorTotalDescontoItens = 0;

                Form formBackground = new Form();
                try
                {
                    using (FrmDescontoTotal uu = new FrmDescontoTotal(valorOriginal, true))
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
                        switch (uu.showModalItem(ref valorOriginal))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                break;
                            case DialogResult.OK:
                                try
                                {
                                    if (valorOriginal > 0)
                                    {
                                        decimal valorDesconto = decimal.Parse(valorOriginal.ToString());
                                        gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns[7].MappingName, valorDesconto);
                                        var records = gridProdutos.View.Records;
                                        foreach (var record in records)
                                        {
                                            var dataRowView = record.Data as DataRowView;
                                            if(!String.IsNullOrEmpty(dataRowView.Row[7].ToString()))
                                                somaValorTotalDescontoItens = somaValorTotalDescontoItens + decimal.Parse(dataRowView.Row[7].ToString());
                                        }
                                        txtDesconto.Texts = somaValorTotalDescontoItens.ToString("C2", CultureInfo.CurrentCulture);
                                        somaEnquantoDigita();
                                        inseridoDescontoItem = true;
                                        inseridoDescontoTotal = false;
                                    }
                                }
                                catch
                                {
                                    GenericaDesktop.ShowErro("Valor Inválido");
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
            }
            else
                GenericaDesktop.ShowAlerta("Clique em um item para depois inserir o desconto");
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

                    //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                    //string pastaDropbox = @"XML\NFCe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                    //string arquivo = nfe.Chave + "-procNFCe.xml";
                    //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                    //t.Wait();
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

                    //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    //string pastaDropbox = @"XML\NFe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                    //string arquivo = nfe.Chave + "-procNFe.xml";
                    //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                    //t.Wait();
                }
            }
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
        private void concluirVenda(Venda vendaConclusao, bool temNota)
        {
            try
            {
                IList<ContaReceber> lisRec = new List<ContaReceber>();
                //Salva na tabela vendaItens
                salvarProdutosVenda();
                //Nota nao foi gerada para ser gerada posteriormente agrupada!
                //if (temNota == false)
                //{
                    AtualizaEstoque(false, "VENDA: <" + vendaConclusao.Id + "> " + txtClienteAbaPagamento.Texts);
                //}
                //else
                //    AtualizaEstoque(true);

                vendaConclusao.Concluida = true;
                Controller.getInstance().salvar(vendaConclusao);

                ContaReceberController contaReceberController = new ContaReceberController();
                IList<ContaReceber> listaContaReceber = contaReceberController.selecionarContaReceberPorVenda(vendaConclusao.Id);
                if (listaContaReceber.Count > 0)
                {
                    foreach (ContaReceber cr in listaContaReceber)
                    {
                        cr.Concluido = true;
                        cr.Documento = "V" + vendaConclusao.Id + "/" + cr.Parcela;
                        Controller.getInstance().salvar(cr);
                        lisRec.Add(cr);
                    }
                }

                CaixaController caixaController = new CaixaController();
                IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorOrigem("Venda", vendaConclusao.Id.ToString());
                if (listaCaixa.Count > 0)
                {
                    foreach (Caixa cx in listaCaixa)
                    {
                        cx.Concluido = true;
                        Controller.getInstance().salvar(cx);
                    }
                }
                if(lisRec.Count > 0)
                {
                    FrmImprimirDuplicata frDup = new FrmImprimirDuplicata(vendaConclusao.Cliente, lisRec);
                    frDup.ShowDialog();
                }
                if (temNota == false)
                {
                    //Imprimir Ticket
                    FrmImprimirTicketVenda frmImprimirTicket = new FrmImprimirTicketVenda(venda);
                    frmImprimirTicket.ShowDialog();
                    GenericaDesktop.ShowInfo("Venda Registrada com Sucesso");
                    limparCampos();
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao concluir venda: " + erro.Message);
            }    
        }

        private void AtualizaEstoque(bool conciliado, string descricaoEstoque)
        {
            var records = gridProdutos.View.Records;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                //     e.Style.Font.FontStyle = FontStyle.Strikeout;
                if (dataRowView != null && !dataRowView["ItemExcluido"].ToString().Equals("True"))
                {
                    Produto prod = new Produto();
                    Estoque estoque = new Estoque();
                    estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                    estoque.Entrada = false;
                    estoque.Origem = "VENDA " + venda.Id.ToString();
                    estoque.Descricao = descricaoEstoque;
                    estoque.Produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowView.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
                    prod = estoque.Produto;
                    estoque.Saida = true;
                    estoque.DataEntradaSaida = DateTime.Now;
                    estoque.Quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                    estoque.Pessoa = venda.Cliente;
                    if (conciliado == true)
                    {
                        prod.Estoque = prod.Estoque - estoque.Quantidade;
                        estoque.Conciliado = true;
                    }
                    else
                    {
                        prod.EstoqueAuxiliar = prod.EstoqueAuxiliar - estoque.Quantidade;
                        estoque.Conciliado = false;
                    }
                
                    Controller.getInstance().salvar(estoque);
                    Controller.getInstance().salvar(prod);
                    //prod = estoque.Produto;
                }
            }
        }
       private void limparCampos()
        {
            tabControlAdv1.SelectedTab = tabVenda;
            venda = new Venda();

            //nfe nova não, estava causando falha na contigencia, passava aqui antes de gerar.
            //nfe = new Nfe();
            Sessao.vendasRecebimento_ValorRecebido = 0;

            //Se nao tiver essa informacao o sistema entende q está editando a venda ja feita, por isso esse lblnumerodavenda é importante
            lblNumeroVenda.Text = "Nº Venda: 0";
            txtVendedorSelecionado.Texts = "";
            txtCodVendedor.Texts = "";
            txtNomeVendedor.Texts = "";
            txtClienteAbaPagamento.Texts = "";
            txtCodCliente.Texts = "";
            txtPesquisaCliente.Texts = "";

            txtTotalComDesconto.Texts = "R$ 0,00";
            txtDesconto.Texts = "R$ 0,00";
            produto = new Produto();
            dsPagamento.Tables[0].Clear();
            dsProduto.Tables[0].Clear();
            gridRecebimento.Refresh();
            gridProdutos.Refresh();
            produtoExcluido = false;
            indexExcluido = -1;
            valorTotal = 0;
            valorComDesconto = 0;
            posicaoItem = 1;
            inseridoDescontoItem = false;
            inseridoDescontoTotal = false;
            btnGerarNFCe.Enabled = false;
            btnGerarNfe.Enabled = false;
            somaEnquantoDigita();
            txtPesquisaProduto.Focus();
        }

        private void iconeCartao_Click(object sender, EventArgs e)
        {
            btnCartaoCredito.PerformClick();
        }

        private void IconeDinheiro_Click(object sender, EventArgs e)
        {
            btnDinheiro.PerformClick();
        }

        private void iconePix_Click(object sender, EventArgs e)
        {
            btnPIX.PerformClick();
        }

        private void iconeDeposito_Click(object sender, EventArgs e)
        {
            btnDeposito.PerformClick();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            btnBoleto.PerformClick();
        }

        private void iconeCrediario_Click(object sender, EventArgs e)
        {
            btnCrediario.PerformClick();
        }

        private void iconeCheque_Click(object sender, EventArgs e)
        {
            btnCheque.PerformClick();
        }

        private void iconeCredito_Click(object sender, EventArgs e)
        {
            btnCreditoCliente.PerformClick();
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProdutoPorDescricao(txtPesquisaProduto.Texts.Trim());
        }

        private void btnImprimirRecibo_Click(object sender, EventArgs e)
        {
           // GenericaDesktop.Leitura_Contingencia("2056", @"\\txt_note\Processados\nsConcluido\");
        }

        private void btnBoleto_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoBoleto();
        }

        private bool validarClienteNFCe(Pessoa pessoa)
        {
            bool validacao = false;
            if (pessoa.Cnpj.Length == 11)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length < 11)
            {
                GenericaDesktop.ShowAlerta("Para NFCe o cliente selecionado deve ter CPF preenchido corretamente");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            else if (pessoa.Cnpj.Length == 14)
            {
                GenericaDesktop.ShowAlerta("Em uma NFCe o cliente não pode ser pessoa jurídica, caso precise identificar a pessoa jurídica faça a emissão de uma NFe modelo 55");
                validacao = false;
            }
            if (!String.IsNullOrEmpty(pessoa.RazaoSocial))
                validacao = true;
            if (pessoa.EnderecoPrincipal == null)
            {
                GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            if(pessoa.EnderecoPrincipal != null)
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

        private bool validarClienteNFe(Pessoa pessoa)
        {
            bool validacao = false;
            if (pessoa.Cnpj.Length == 11)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length < 11)
            {
                GenericaDesktop.ShowAlerta("Para NFe o cliente selecionado deve ter CPF preenchido corretamente");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            else if (pessoa.Cnpj.Length == 14)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length > 14)
            {
                GenericaDesktop.ShowAlerta("Para NFe o cliente selecionado deve ter CNPJ preenchido corretamente");
                validacao = false;
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

        private void abrirTelaEditarCliente(Pessoa cliente)
        {
            FrmClienteCadastro frm = new FrmClienteCadastro(cliente);
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(txtClienteAbaPagamento.Texts))
            {
                Pessoa pessoaCliente = new Pessoa();
                pessoaCliente.Id = int.Parse(txtCodCliente.Texts);
                pessoaCliente = (Pessoa)pessoaController.selecionar(pessoaCliente);
                venda.Cliente = pessoaCliente;
                venda.NomeCliente = pessoaCliente.RazaoSocial;
                if (venda.Cliente.EnderecoPrincipal != null)
                {
                    venda.EnderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " + venda.Cliente.EnderecoPrincipal.Numero + " - " + venda.Cliente.EnderecoPrincipal.Complemento;
                }
                Controller.getInstance().salvar(venda);
            }
        }

        private void gridProdutos_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
          
        }

        private void btnGerarNfe_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(abrirAguarde2);
            th.Start();
            gerarNFE();
            th.Join();
        }

        private void gerarNFE()
        {
            Sessao.teveRetornoApi = false;
            //se nao tem cliente ja vem validado
            bool validaCliente = false;
            //enviarNFCe();
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                cli.Id = int.Parse(txtCodCliente.Texts);
                cli = (Pessoa)pessoaController.selecionar(cli);

                if (!String.IsNullOrEmpty(lblIdPropriedade.Text))
                {
                    cli.InscricaoEstadual = lblInscricaoEstadual.Text.Replace("Inscrição Estadual: ", "");
                    PessoaPropriedade pessoaPropriedade = new PessoaPropriedade();
                    pessoaPropriedade.Id = int.Parse(lblIdPropriedade.Text);
                    pessoaPropriedade = (PessoaPropriedade)Controller.getInstance().selecionar(pessoaPropriedade);
                    cli.EnderecoPrincipal = pessoaPropriedade.Endereco;
                }


                validaCliente = validarClienteNFe(cli);

                ValidadorNotaSaida validador = new ValidadorNotaSaida();
                if (validaCliente == true)
                {
                    //Emitir NFe pela nova classe
                    EmitirNFe emitirNFe = new EmitirNFe();
                    carregarListaProdutos();
                    try
                    {
                        if (validador.validarProdutosNota(listaProdutosNFe))
                        {
                            concluirVenda(venda, true);
                            numeroNFCe = Sessao.parametroSistema.ProximoNumeroNFe;
                            Nfe nfConferencia = new Nfe();
                            NfeController nfeController = new NfeController();
                            nfConferencia = nfeController.selecionarUltimoNumeroNota("55");
                            if (nfConferencia != null)
                            {
                                if (nfConferencia.Id > 0)
                                {
                                    if (numeroNFCe != (int.Parse(nfConferencia.NNf) + 1).ToString())
                                        numeroNFCe = (int.Parse(nfConferencia.NNf.ToString()) + 1).ToString();
                                }
                            }
                            xmlStrEnvio = emitirNFe.gerarXMLNfe(valorTotal, valorComDesconto, decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")), numeroNFCe, listaProdutosNFe, cli, venda, false, "VENDA", null);
                            if (!String.IsNullOrEmpty(xmlStrEnvio))
                            {
                                enviarXMLNFeParaApi(xmlStrEnvio);
                                limparCampos();
                            }
                        }
                    }
                    catch (Exception erro)
                    {
                        if (erro.Message.Contains("Unexpected character encountered while parsing value"))
                        {
                            NfeController nfeController = new NfeController();
                            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFe);
                            NfeStatus nfeStatus = new NfeStatus();
                            nfeStatus.Id = 2;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                            Controller.getInstance().salvar(nfe);
                            Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                            Controller.getInstance().salvar(Sessao.parametroSistema);
                            GenericaDesktop.ShowAlerta("Venda concluída com falha de comunicação com a sefaz, tente reenviar a nota pelo modulo de gerenciamento de notas");
                            limparCampos();
                        }
                        else
                            GenericaDesktop.ShowErro(erro.Message);
                    }
                }
                atualizarProximoNumeroNota();
                limparCampos();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Para emitir NFe deve selecionar um cliente com dados válidos, tendo nome, cpf ou cnpj, endereço completo!");
            }
        }

        private void enviarXMLNFeParaApi(string xmlNfe)
        {
            RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
            NfeStatus nfeStatus = new NfeStatus();
            string caminhoSalvarXml = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFCe, Sessao.parametroSistema.SerieNFe);

            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            codStatusRet = "";
            gravarXMLNaPasta(xmlNfe, nfe.NNf, @"\XML\Tentativa\NFe\", nfe.NNf + ".xml", false);
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string ambiente = "2";
                if (Sessao.parametroSistema.AmbienteProducao == true)
                    ambiente = "1";
                String retorno = NSSuite.emitirNFeSincrono(xmlNfe, "xml", Sessao.empresaFilialLogada.Cnpj, "XP", ambiente, caminhoSalvarXml, false, false);
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
                    Sessao.teveRetornoApi = true;
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
                    Controller.getInstance().salvar(nfe);
                    
                    //Estoque
                    AtualizaEstoque(true, "NF SAÍDA: " + nfe.NNf + " MOD: " + nfe.Modelo);
                    armazenaXmlAutorizadoNoBanco();
                    GenericaDesktop.ShowInfo("Venda concluída com sucesso, nota autorizada!");
                    if (File.Exists(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                    //EnviaXML PAINEL LUNAR 
                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                    byte[] arquivo;
                    using (var stream = new FileStream(caminhoSalvarXml + nfe.Chave + "-procNFe.xml", FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            arquivo = reader.ReadBytes((int)stream.Length);
                            var ret = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                            {
                                nfe.Nuvem = true;
                                Controller.getInstance().salvar(nfe);
                            }
                        }
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
                        Sessao.teveRetornoApi = true;
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
                            if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                            {
                                //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
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
                            if(!String.IsNullOrEmpty(retConsulta.xMotivo))
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
                                Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                                Controller.getInstance().salvar(Sessao.parametroSistema);
                                GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: " + retConsulta.cStat + " " + retConsulta.xMotivo + ", na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                            }
                        }
                    }
                }
                //se a nota continua nao autorizada, verifica se teve erros
                if(String.IsNullOrEmpty(nfe.Chave) || nfe.Chave.Equals("123"))
                {
                    Sessao.teveRetornoApi = true;
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
                    if (!String.IsNullOrEmpty(retConsulta.chNFe))
                    {
                        //nfe.Protocolo = retConsulta.nProt;
                        nfe.Chave = retConsulta.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(Sessao.parametroSistema.ProximoNumeroNFe) + 1).ToString();
                    Controller.getInstance().salvar(Sessao.parametroSistema);
                    GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
            }
            else
            {
                Sessao.teveRetornoApi = true;
                GenericaDesktop.ShowAlerta("Venda concluida com Erro na Nota Fiscal: Verifique sua conexão com a internet, após normalizar acesse o menu de gerenciamento de notas para reenviar a mesma!");
            }
        }

        private void btnCondicional_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Confirma a geração da condicional?"))
            {
                if (!String.IsNullOrEmpty(txtCodCliente.Texts) && gridProdutos.RowCount > 0)
                {
                    Condicional condicional = new Condicional();
                    Pessoa cliente = new Pessoa();
                    cliente.Id = int.Parse(txtCodCliente.Texts);
                    cliente = (Pessoa)Controller.getInstance().selecionar(cliente);
                    condicional.Cliente = cliente;
                    condicional.Data = DateTime.Now;
                    condicional.Encerrado = false;
                    condicional.Filial = Sessao.empresaFilialLogada;
                    condicional.Observacoes = "";
                    condicional.QtdPeca = pecas;
                    condicional.DataCadastro = DateTime.Now;
                    condicional.OperadorCadastro = Sessao.usuarioLogado.Id.ToString();
                    condicional.ValorTotal = 0; // vai receber a soma nos produtos abaixo
                    condicional.DataPrevisao = DateTime.Now.AddDays(1);

                    IList<CondicionalProduto> listaItensCondicional = new List<CondicionalProduto>();
                    var records = gridProdutos.View.Records;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;
                        if (dataRowView != null && !dataRowView["ItemExcluido"].ToString().Equals("True"))
                        {
                            CondicionalProduto condicionalProduto = new CondicionalProduto();
                            condicionalProduto.Condicional = condicional;
                            condicionalProduto.Devolvido = false;

                            Produto prod = new Produto();
                            prod.Id = int.Parse(dataRowView.Row["Codigo"].ToString());
                            prod = (Produto)Controller.getInstance().selecionar(prod);
                            condicionalProduto.Produto = prod;
                            condicionalProduto.Quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                            decimal descontoItem = decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
                            descontoItem = descontoItem / decimal.Parse(condicionalProduto.Quantidade.ToString());
                            descontoItem = Math.Round(descontoItem, 2);
                            condicionalProduto.ValorUnitario = decimal.Parse(dataRowView.Row["ValorUnitario"].ToString()) - descontoItem;
                            condicional.ValorTotal = condicional.ValorTotal + condicionalProduto.ValorUnitario;
                            listaItensCondicional.Add(condicionalProduto);
                        }
                    }
                    CondicionalController condicionalController = new CondicionalController();
                    condicionalController.salvarCondicionalComProdutos(condicional, listaItensCondicional);
                    limparCampos();
                    GenericaDesktop.ShowInfo("Condicional realizada com sucesso");
                    //Imprimir condicional
                }
                else
                    GenericaDesktop.ShowAlerta("Para realizar uma condicional coloque o cliente e produtos corretamente!");
            }
        }

        private void btnSelecionarPropriedades_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                verificarPropriedadesCliente(pessoa);
            }
            else 
                GenericaDesktop.ShowAlerta("Selecione um cliente primeiro");
            
        }

        private void btnSelecionarDependente_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                selecionarDependente(pessoa);
            }
            else
                GenericaDesktop.ShowAlerta("Selecione um cliente primeiro");
        }
    }
}
