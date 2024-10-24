using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasReceber.Reports;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.OrdensDeServico;
using Lunar.Telas.OrdensDeServico.DataSetOrdemServico;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.RelatoriosDiversos;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.GalaxyPay_API;
using Lunar.Utils.IntegracoesBancosBoletos;
using Lunar.Utils.IntegracoesBancosBoletos.BB;
using Lunar.Utils.LunarChatIntegracao;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.ProjectServer.Client;
using NHibernate.Criterion;
using Syncfusion.Data;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Protection.PlayReady;
using static Lunar.Utils.GalaxyPay_API.GalaxyPay_RetornoStatusBoletos;
using static Lunar.Utils.GalaxyPay_API.GalaxyPayApiIntegracao;
using static Lunar.Utils.IntegracoesBancosBoletos.BB.RetornoBoletoBB;
using static Lunar.Utils.LunarChatIntegracao.LunarChatMensagem;
using static LunarBase.Utilidades.ManifestoDownload;
using Exception = System.Exception;
using Task = System.Threading.Tasks.Task;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmContaReceberLista : Form
    {
        IList<ContaReceber> listaReceberBoleto = new List<ContaReceber>();
        BoletoConfigController boletoConfigController = new BoletoConfigController();
        private ToolTip toolTip1;
        ContaReceber contaReceber = new ContaReceber();
        ContaReceberController contaReceberController = new ContaReceberController();
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        IList<ContaReceber> listaContaReceberCalculado = new List<ContaReceber>();
        bool passou = false;
        decimal multa = 0;
        decimal juro = 0;
        decimal diasVencido = 0;
        GenericaDesktop generica = new GenericaDesktop();
        private List<string> listaFaturaGalaxyPay = new List<string>();
        public FrmContaReceberLista()
        {
            InitializeComponent();
            txtVencimentoInicial.Value = DateTime.Now;
            txtVencimentoFinal.Value = DateTime.Now;
            IList<BoletoConfig> listaBoletoConfig = new List<BoletoConfig>();
            listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
            if (Sessao.parametroSistema.IntegracaoGalaxyPay == true || listaBoletoConfig.Count > 0)
            {
                btnRetornoBoletos.Enabled = true;
            }
            iniciarToolTip();
        }
        public FrmContaReceberLista(Pessoa pessoa)
        {
            InitializeComponent();
            txtVencimentoInicial.Value = DateTime.Now;
            txtVencimentoFinal.Value = DateTime.Now;
            txtCliente.Texts = pessoa.RazaoSocial;
            txtCodCliente.Texts = pessoa.Id.ToString();
            radioAbertas.Checked = true;
            txtNumeroDocumento.Texts = "";
            chkAtivarVencimento.Checked = false;
            pesquisarContaReceber();
            if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
            {
                btnRetornoBoletos.Enabled = true;
            }
            iniciarToolTip();
        }

        private void iniciarToolTip()
        {
            toolTip1 = new ToolTip();

            // Configurações adicionais opcionais
            toolTip1.AutoPopDelay = 5000;  // Duração em milissegundos que o ToolTip ficará visível
            toolTip1.InitialDelay = 500;  // Tempo em milissegundos antes de aparecer
            toolTip1.ReshowDelay = 1000;    // Tempo entre ToolTips sucessivos
            toolTip1.ShowAlways = true;    // Mostra o ToolTip mesmo que o controle não tenha foco

            // Associa o ToolTip ao botão e define o texto
            toolTip1.SetToolTip(btnEmail, "Enviar Boleto e NFe por E-mail");
            toolTip1.SetToolTip(btnImprimirBoleto, "Imprimir Boleto Selecionado");
            toolTip1.SetToolTip(btnExtratoCliente, "Extrato do Cliente");
            toolTip1.SetToolTip(btnExportarPDF, "Gerar PDF");
            toolTip1.SetToolTip(btnEnviarWhats, "Enviar Boleto e NFe pelo WhatsApp");
        }
        private void pesquisarContaReceber()
        {
            try
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Multa))
                    multa = decimal.Parse(Sessao.parametroSistema.Multa);
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Juro))
                    juro = decimal.Parse(Sessao.parametroSistema.Juro);

                listaContaReceber = new List<ContaReceber>();
                listaContaReceberCalculado = new List<ContaReceber>();
                string sql = "Select * From ContaReceber Tabela ";
                if (chkApenasSPC.Checked == true || chkApenasEscritorioCobranca.Checked == true)
                {
                    sql = sql + "Inner Join Pessoa on Tabela.Cliente = Pessoa.Id ";
                }
                sql = sql + "where Tabela.Concluido = true and Tabela.FlagExcluido <> true ";
                if (chkApenasSPC.Checked == true)
                    sql = sql + "and Pessoa.RegistradoSpc = true ";
                if (chkApenasEscritorioCobranca.Checked == true)
                    sql = sql + "and Pessoa.EscritorioCobranca = true ";
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts + " ";

                if (!String.IsNullOrEmpty(txtNumeroDocumento.Texts))
                    sql = sql + "and Tabela.Documento like '%" + txtNumeroDocumento.Texts + "%' ";
            
                if (radioAbertas.Checked == true)
                    sql = sql + "and Tabela.Recebido <> true ";
                else
                    sql = sql + "and Tabela.Recebido = true ";

                if (chkAtivarVencimento.Checked == true)
                {
                    DateTime dataIni = DateTime.Parse(txtVencimentoInicial.Value.ToString());
                    DateTime dataFin = DateTime.Parse(txtVencimentoFinal.Value.ToString());
                    String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                    String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");
                    sql = sql + "and Tabela.Vencimento BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                }

                string orderBy = "order by Tabela.Vencimento";

                if (chkApenasEscritorioCobranca.Checked == true || chkApenasSPC.Checked == true)
                    orderBy = "order by Pessoa.Id, Tabela.Vencimento";
                string sqlx = (sql + orderBy);
                listaContaReceber = contaReceberController.selecionarContaReceberPorSqlNativo(sql + orderBy);
                if (listaContaReceber.Count > 0)
                {
                    calculaTotalNotas();
                    sfDataPager1.DataSource = listaContaReceberCalculado;

                    //sfDataPager1.DataSource = listaContaReceber;
                    if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                        sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                    else
                        sfDataPager1.PageSize = 100;
                    grid.DataSource = sfDataPager1.PagedSource;
                    sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
                    // grid.Focus();

                    //grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                    this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    this.grid.AutoSizeController.Refresh();
                    grid.Refresh();
                    this.grid.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));
                    //calculaTotalJurosGridVisto();
                    //preencherSumario();

                    int w = Screen.PrimaryScreen.Bounds.Width;
                    int h = Screen.PrimaryScreen.Bounds.Height;
                    if (w == 1920 && h == 1080)
                    {
                        this.grid.View.Records.CollectionChanged += Records_CollectionChanged;
                    }
                }
                else
                {
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }

                //Nao receber de 2 clientes ao mesmo tempo 
                if (String.IsNullOrEmpty(txtCodCliente.Texts) || radioPagas.Checked == true)
                {
                    btnReceber.Enabled = false;
                    btnParcial.Enabled = false;
                    btnReceber.BackColor = Color.FromArgb(192, 192, 192);
                    lblCalculando.Text = "Pesquise 1 cliente para liberar os botões de recebimento";
                    lblCalculando.Visible = true;
                }
                else
                {
                    btnReceber.Enabled = true;
                    btnParcial.Enabled = true;
                    btnReceber.BackColor = Color.FromArgb(31, 30, 68);
                    lblCalculando.Text = "Aguarde, calculado totais...";
                    lblCalculando.Visible = false;
                }
            }
            catch (System.Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void Records_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.grid.View.Records.Count > 0)
            {
                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                grid.AutoSizeController.Refresh();
            }
        }
        private void calculaTotalNotas()
        {
            try
            {
                lblCalculando.Text = "Aguarde, calculando juros...";
                lblCalculando.Visible = true;
                //var records = grid.View.Records;
                int i = 1;
                 foreach (ContaReceber contaReceber in listaContaReceber)
                {
                    var receber = contaReceber;
                    //var receber = (ContaReceber)record.Data;

                    if (receber.Vencimento < DateTime.Now)
                    {
                        //calcula juro e multa apenas se nao tiver pago
                        if (contaReceber.Recebido == false)
                        {
                            TimeSpan dataX = DateTime.Now - receber.Vencimento;
                            diasVencido = dataX.Days;
                            decimal multaCalculada = receber.ValorParcela * multa / 100;
                            receber.Multa = multaCalculada;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Multa"].MappingName, multaCalculada);
                            decimal juroCalculado = receber.ValorParcela * ((juro / 30) / 100) * diasVencido;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Juro"].MappingName, juroCalculado);
                            receber.Juro = juroCalculado;
                            decimal valorTotalCalculado = (receber.ValorParcela + multaCalculada + juroCalculado - (receber.ValorRecebimentoParcial));
                            // grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, valorTotalCalculado);
                            receber.ValorTotal = valorTotalCalculado;
                        }
                        else
                        {
                            //Se foi pago pega o q esta preenchido
                            receber.ValorTotal = (receber.ValorParcela + receber.Multa + receber.Juro);
                        }
                    }
                    else
                    {

                        decimal valorTotalCalculado = (receber.ValorParcela  - receber.ValorRecebimentoParcial);
                        receber.ValorTotal = valorTotalCalculado;
                    }
                    if (receber.ValorTotal == 0)
                    {
                        receber.ValorTotal = receber.ValorParcela;
                    }
                    //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, receber.ValorParcela);
                    i++;
                    listaContaReceberCalculado.Add(receber);
                }
                preencherSumario();
                lblCalculando.Visible = false;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
            }
        }

        //private void calculaTotalJurosGridVisto()
        //{
        //    try
        //    {
        //        var records = grid.View.Records;
        //        int i = 1;
        //        if (records.Count > 0)
        //        {
        //            foreach (var record in records)
        //            {
        //                //var receber = contaReceber;
        //                var receber = (ContaReceber)record.Data;
        //                //MessageBox.Show(records.Count.ToString() + " "  + receber.Documento + " Calculando...");
        //                if (receber.Vencimento < DateTime.Now)
        //                {
        //                    TimeSpan dataX = DateTime.Now - receber.Vencimento;
        //                    diasVencido = dataX.Days;
        //                    decimal multaCalculada = receber.ValorParcela * multa / 100;
        //                    //receber.Multa = multaCalculada;
        //                    grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Multa"].MappingName, multaCalculada);
        //                    decimal juroCalculado = receber.ValorParcela * ((juro / 30) / 100) * diasVencido;
        //                    grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Juro"].MappingName, juroCalculado);
        //                    //receber.Juro = juroCalculado;
        //                    decimal valorTotalCalculado = receber.ValorParcela + multaCalculada + juroCalculado;
        //                    grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, valorTotalCalculado);
        //                    //receber.ValorTotal = valorTotalCalculado;
        //                }
        //                else
        //                    grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, receber.ValorParcela);
        //                i++;
        //                //listaContaReceberCalculado.Add(receber);
        //            }
        //        }
        //    }
        //    catch (Exception erro)
        //    {
        //        GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
        //    }
        //}
        private void preencherSumario()
        {
            // this.grid.SummaryCalculationUnit = Syncfusion.Data.SummaryCalculationUnit.SelectedRows;
            //sumario no grid
            GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Name = "TableSummary";
            tableSummaryRow1.ShowSummaryInRow = true;
            tableSummaryRow1.TitleColumnCount = 3;
            tableSummaryRow1.Title = " Parcelas: {TotalNotas}               Total Sem Juros {ValorTotalSemJuro}                 Total Com Juros - Parciais {ValorTotalComJuro}";
            tableSummaryRow1.Position = VerticalPosition.Bottom;
            tableSummaryRow1.CalculationUnit = Syncfusion.Data.SummaryCalculationUnit.AllRows;

            GridTableSummaryRow tableSummaryRow2 = new GridTableSummaryRow();
            tableSummaryRow2.Name = "TableSummary2";
            tableSummaryRow2.ShowSummaryInRow = true;
            tableSummaryRow2.TitleColumnCount = 1;
            tableSummaryRow2.Title = " Total Selecionado: {ValorTotalComJuroSelecionado}";
            tableSummaryRow2.Position = VerticalPosition.Bottom;
            tableSummaryRow2.CalculationUnit = Syncfusion.Data.SummaryCalculationUnit.SelectedRows;

            GridSummaryColumn summaryColumn1 = new GridSummaryColumn();
            summaryColumn1.Name = "TotalNotas";
            summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn1.Format = "{Count}";
            summaryColumn1.MappingName = "Documento";

            GridSummaryColumn summaryColumn2 = new GridSummaryColumn();
            summaryColumn2.Name = "ValorTotalSemJuro";
            summaryColumn2.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn2.Format = "{Sum:c}";
            summaryColumn2.MappingName = "ValorParcela";

            GridSummaryColumn summaryColumn3 = new GridSummaryColumn();
            summaryColumn3.Name = "ValorTotalComJuro";
            summaryColumn3.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn3.Format = "{Sum:c}";
            summaryColumn3.MappingName = "ValorTotal";

            GridSummaryColumn summaryColumn4 = new GridSummaryColumn();
            summaryColumn4.Name = "ValorTotalComJuroSelecionado";
            summaryColumn4.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn4.Format = "{Sum:c}";
            summaryColumn4.MappingName = "ValorTotal";

            GridSummaryColumn summaryColumn5 = new GridSummaryColumn();
            summaryColumn5.Name = "ValorTotalRecebido";
            summaryColumn5.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn5.Format = "{Sum:c}";
            summaryColumn5.MappingName = "ValorRecebido";

            if (radioPagas.Checked == true)
            {
                tableSummaryRow1.Title = " Parcelas: {TotalNotas}               Total Sem Juros {ValorTotalSemJuro}                 Total Com Juros {ValorTotalComJuro}                 Total Recebido {ValorTotalRecebido}";
                tableSummaryRow1.SummaryColumns.Add(summaryColumn5);
            }

            this.grid.TableSummaryRows.Clear();
            tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
            tableSummaryRow1.SummaryColumns.Add(summaryColumn2);
            tableSummaryRow1.SummaryColumns.Add(summaryColumn3);
            tableSummaryRow2.SummaryColumns.Add(summaryColumn4);
            this.grid.TableSummaryRows.Add(tableSummaryRow1);
            this.grid.Style.TableSummaryRowStyle.Font = new GridFontInfo(new Font("Microsoft Sans Serif", 12f, FontStyle.Regular));
            this.grid.TableSummaryRows.Add(tableSummaryRow2);
        }
        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaContaReceberCalculado.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void FrmContaReceberLista_Load(object sender, EventArgs e)
        {
            if (passou == false)
            {
                passou = true;
                txtCliente.Focus();
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
                {
                    btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.whatsapp_logo_icone;
                    btnEnviarWhats.Enabled = true;
                }
                else
                {
                    btnEnviarWhats.Enabled = false;
                    btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.WhatsappCinza2_1;
                }
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as ContaReceber) != null)
                {
                    if ((e.RowData as ContaReceber).Vencimento < DateTime.Now && radioAbertas.Checked == true)
                    {
                        e.Style.TextColor = Color.Red;
                        //int pagina = sfDataPager1.SelectedPageIndex; 
                        //if(pagina > 0)
                        //    calculaTotalJurosGridVisto();
                    }
                    else if (radioPagas.Checked == true)
                    {
                        e.Style.TextColor = Color.Green;
                        //int pagina = sfDataPager1.SelectedPageIndex; 
                        //if(pagina > 0)
                        //    calculaTotalJurosGridVisto();
                    }
                }
            }
            catch
            {

            }
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Confirma o recebimento das parcelas selecionadas?"))
            {
                IList<ContaReceber> listaReceber = new List<ContaReceber>();
                IList<ContaPagar> listaPagar = new List<ContaPagar>();
                int i = 0;
                bool parcelasPagasSelecionadas = false;
                foreach (var selectedItem in grid.SelectedItems)
                {
                    var conta = selectedItem as ContaReceber;
                    if (conta.Recebido == false)
                        listaReceber.Add(conta);
                    else
                    {
                        parcelasPagasSelecionadas = true;
                    }
                }
                if(parcelasPagasSelecionadas == true)
                    GenericaDesktop.ShowAlerta("Atenção: Você selecionou parcelas pagas");
                if (listaReceber.Count > 0)
                {
                    Form formBackground = new Form();
                    OrdemServico ordemServico = new OrdemServico();
                    IList<OrdemServico> listaOs = new List<OrdemServico>();
                    FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "CONTARECEBER", false, false, listaOs);
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    uu.Dispose();
                    btnPesquisar.PerformClick();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Nenhuma parcela em aberto selecionada!");
                }
            }
        }
        PessoaController pessoaController = new PessoaController();
        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            IList<Pessoa> listaCliente = pessoaController.selecionarClientesComVariosFiltros(txtCliente.Texts);
            if (listaCliente.Count == 1)
            {
                foreach (Pessoa pessoa in listaCliente)
                {
                    txtCliente.Texts = pessoa.RazaoSocial;
                    txtCodCliente.Texts = pessoa.Id.ToString();
                    txtNumeroDocumento.Focus();
                    generica.buscarAlertaCadastrado(pessoa);
                    btnPesquisar.PerformClick();
                }
            }
            //se tem mais de um cliente ou nenhum o sistema entra na pesquisa
            else
            {
                Pessoa pessoaOjeto = new Pessoa();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtCliente.Texts))
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
                                    txtNumeroDocumento.Focus();
                                    generica.buscarAlertaCadastrado((Pessoa)pessoaObj);
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                txtNumeroDocumento.Focus();
                                btnPesquisar.PerformClick();
                                if (((Pessoa)pessoaOjeto).RegistradoSpc == true)
                                {
                                    GenericaDesktop.ShowAlerta("Cliente com marcação de registro no SPC/Serasa no cadastro!");
                                }
                                if (((Pessoa)pessoaOjeto).EscritorioCobranca == true)
                                {
                                    GenericaDesktop.ShowAlerta("Cliente com marcação de cobrança externa!");
                                }
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
        }

        private void chkAtivarVencimento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkAtivarVencimento.Checked == true)
                {
                    txtVencimentoInicial.Enabled = true;
                    txtVencimentoFinal.Enabled = true;
                }
                else
                {
                    txtVencimentoInicial.Enabled = false;
                    txtVencimentoFinal.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private void pesquisarContasReceberPorCodigoBarras()
        {
            pesquisarContaReceber();
        }

        private void txtNumeroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisarContaReceber();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarContaReceber();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarContaReceber();

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
                        pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                        if (pessoa != null)
                        {
                            txtCliente.Texts = pessoa.RazaoSocial;
                            txtCodCliente.Texts = pessoa.Id.ToString();
                            pesquisarContaReceber();
                            generica.buscarAlertaCadastrado(pessoa);
                        }
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Código inválido");
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    listaContaReceber = new List<ContaReceber>();
                    listaContaReceberCalculado = new List<ContaReceber>();
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
             
                }
            }
        }

        private void btnNovaFatura_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmNovaFatura uu = new FrmNovaFatura())
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
                    switch (uu.showModalNovo(ref listaContaReceberCalculado))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            formBackground.Dispose();
                            break;
                        case DialogResult.OK:
                            btnPesquisar.PerformClick();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja excluir as parcelas selecionadas?"))
            {
                int i = 0;
                OrdemServico ordem = new OrdemServico();
                if (grid.SelectedItems.Count > 0)
                {
                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var conta = selectedItem as ContaReceber;
                        if (conta.Recebido == false)
                        {
                            //Se possuir boleto cancela boleto na plataforma
                            if(conta.BoletoGerado == true)
                            {
                                IList<ContaReceber> listaBoletos = new List<ContaReceber>();
                                listaBoletos.Add(conta);
                                GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                                string retorno = galaxyPayApiIntegracao.GalaxyPay_CancelarBoletos(listaBoletos);
                            }
                            //Cancela conta a receber
                            Controller.getInstance().excluir(conta);
                            if (conta.OrdemServico != null)
                                ordem = conta.OrdemServico;
                            i++;
                        }
                        else
                            GenericaDesktop.ShowAlerta("Não é possível excluir uma conta ja recebida!");
                    }
                    if (i > 0)
                    {
                        GenericaDesktop.ShowInfo(i + " Contas a Receber Excluídas com Sucesso!");
                        btnPesquisar.PerformClick();
                    }
                    if(ordem != null)
                    {
                        if (ordem.Id > 0)
                        {
                            if (GenericaDesktop.ShowConfirmacao("Deseja reabrir a ordem de serviço ? " + ordem.Id + ", se houver registros no caixa, também serão excluidos! "))
                            {
                                ordem.Status = "ABERTA";
                                Controller.getInstance().salvar(ordem);
                                CaixaController caixaController = new CaixaController();
                                IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorOrigem("ORDEMSERVICO", ordem.Id.ToString());
                                if (listaCaixa.Count > 0)
                                {
                                    foreach (Caixa caixa in listaCaixa)
                                    {
                                        Controller.getInstance().excluir(caixa);
                                    }
                                }
                                GenericaDesktop.ShowInfo("Ordem de Serviço reaberta com Sucesso!");
                            }
                        }
                    }

                }
                else
                    GenericaDesktop.ShowAlerta("Selecione as contas que deseja excluir!");
            }
        }

        private void FrmContaReceberLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnReceber.PerformClick();
                    break;
            }
        }

        private void btnParcial_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0 && grid.SelectedItems.Count == 1)
            {
                contaReceber = (ContaReceber)grid.SelectedItem;
                decimal valorRetorno = 0;
                Form formBackground = new Form();
                using (FrmValorParcial uu = new FrmValorParcial(contaReceber.ValorTotal))
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
                    switch (uu.showModalNovo(ref valorRetorno))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            formBackground.Dispose();
                            break;
                        case DialogResult.OK:
                            uu.Dispose();
                            formBackground.Dispose();
                            contaReceber.ValorParcela = valorRetorno;
                            contaReceber.Juro = 0;
                            contaReceber.Multa = 0;
                            IList<ContaReceber> listaReceber = new List<ContaReceber>();
                            IList<ContaPagar> listaPagar = new List<ContaPagar>();
                            int i = 0;
                            bool parcelasPagasSelecionadas = false;
                            if (contaReceber.Recebido == false && valorRetorno > 0)
                                listaReceber.Add(contaReceber);
                            else
                                parcelasPagasSelecionadas = true;
                            if (parcelasPagasSelecionadas == true)
                                GenericaDesktop.ShowAlerta("Atenção: Você selecionou parcelas pagas");
                            if (listaReceber.Count > 0)
                            {
                                Form formBackground1 = new Form();
                                OrdemServico ordemServico = new OrdemServico();
                                IList<OrdemServico> listaOs = new List<OrdemServico>();
                                FrmPagamentoRecebimento uu1 = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "CONTARECEBER", true, false, listaOs);
                                formBackground1.StartPosition = FormStartPosition.Manual;
                                //formBackground.FormBorderStyle = FormBorderStyle.None;
                                formBackground1.Opacity = .50d;
                                formBackground1.BackColor = Color.Black;
                                //formBackground.Left = Top = 0;
                                formBackground1.Width = Screen.PrimaryScreen.WorkingArea.Width;
                                formBackground1.Height = Screen.PrimaryScreen.WorkingArea.Height;
                                formBackground1.WindowState = FormWindowState.Maximized;
                                formBackground1.TopMost = false;
                                formBackground1.Location = this.Location;
                                formBackground1.ShowInTaskbar = false;
                                formBackground1.Show();
                                uu1.Owner = formBackground1;
                                uu1.ShowDialog();
                                formBackground.Dispose();
                                uu1.Dispose();
                                btnPesquisar.PerformClick();
                            }
                            break;
                    }
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione apenas 1 parcela para receber parcial!");
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            int qtdPaginas = sfDataPager1.PageCount;
            if (qtdPaginas > 1)
            {
                if (GenericaDesktop.ShowConfirmacao("Você possui " + qtdPaginas + " páginas na pesquisa, deseja imprimir todas?"))
                {
                    txtRegistroPorPagina.Texts = (qtdPaginas * int.Parse(txtRegistroPorPagina.Texts)).ToString();
                    btnPesquisar.PerformClick();
                    exportPDF();
                }
            }
            else
            {
                exportPDF();
            }
        }

        private void exportPDF()
        {
            var options = new PdfExportingOptions();
            options.AutoColumnWidth = true;
            options.FitAllColumnsInOnePage = true;
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
            var page = document.Pages.Add();
            var PDFGrid = grid.ExportToPdfGrid(grid.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == grid.Columns[0].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ContasReceber",
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    document.Save(stream);
                }
                //Message box confirmation to view the created Pdf file.
                if (MessageBox.Show("Deseja abrir o arquivo Pdf?", "Pdf criado com sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the Pdf file using the default Application.
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int qtdPaginas = sfDataPager1.PageCount;
            if (qtdPaginas > 1)
            {
                if (GenericaDesktop.ShowConfirmacao("Você possui " + qtdPaginas + " páginas na pesquisa, deseja imprimir todas?"))
                {
                    txtRegistroPorPagina.Texts = (qtdPaginas * int.Parse(txtRegistroPorPagina.Texts)).ToString();
                    btnPesquisar.PerformClick();
                    exportarExcel();
                }
            }
            else
            {
                exportarExcel();
            }
        }
        private void exportarExcel()
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaReceber",
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.grid, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void limpar()
        {
            grid.DataSource = null;
            sfDataPager1.DataSource = null;
            grid.Refresh();
            txtCliente.Texts = "";
            txtCodCliente.Texts = "";
            chkAtivarVencimento.Checked = false;
            radioAbertas.Checked = true;
            txtNumeroDocumento.Texts = "";
            txtRegistroPorPagina.Texts = "100";
            txtCliente.Focus();
        }

        private void txtCliente_Click(object sender, EventArgs e)
        {
            txtCliente.SelectAll();
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            var conta = (ContaReceber)grid.SelectedItem;
            txtCliente.Texts = conta.Cliente.RazaoSocial;
            txtCodCliente.Texts = conta.Cliente.Id.ToString();
            btnPesquisar.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
            if (e.Column.MappingName == "Vencimento")
            {
                try
                {
                    DateTime dataValida = DateTime.Parse(e.NewValue.ToString());
                    e.IsValid = true;
                    contaReceber = new ContaReceber();
                    contaReceber = (ContaReceber)grid.SelectedItem;
                    GenericaDesktop.gravarLinhaLog("Alteração de Vencimento " + contaReceber.Vencimento.ToShortDateString() + " para " + dataValida.ToShortDateString() + " Usuario: " + Sessao.usuarioLogado.Login + " Cliente: " + contaReceber.Cliente.RazaoSocial, "ALTERAÇÃO CONTA A RECEBER ID " + contaReceber.Id);
                    contaReceber.Vencimento = dataValida;
                    Controller.getInstance().salvar(contaReceber);
                    GenericaDesktop.ShowInfo("Alterado com Sucesso");
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Data Inválida, digite no formato 00/00/0000";

                }
            }
        }

        private void btnExtratoCliente_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                FrmExtratoCliente frm = new FrmExtratoCliente(listaContaReceberCalculado);
                frm.ShowDialog();
            }
            else
                GenericaDesktop.ShowAlerta("Selecione um cliente para conseguir gerar o extrato!");
        }

        private async void btnAbater_Click(object sender, EventArgs e)
        {
            IList<BoletoConfig> lstBoletoConfig = new List<BoletoConfig>();
            DateTime dataInicial = DateTime.Now;
            DateTime dataFinal = DateTime.Now;
            Form formBackground = new Form();
            using (FrmSelecionarData uu = new FrmSelecionarData("RETORNOBOLETOS"))
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
                switch (uu.showModalNovo(ref dataInicial, ref dataFinal))
                {
                    case DialogResult.Ignore:
                        uu.Dispose();
                        break;
                    case DialogResult.OK:
                        BoletoConfigController boletoConfigController = new BoletoConfigController();
                        lstBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
                        if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
                        {
                            retornoCelCash(dataInicial, dataFinal);
                        }
                        if(lstBoletoConfig.Count > 0)
                        {
                            foreach (var boletoConfig in lstBoletoConfig)
                            {
                                if (boletoConfig.ContaBancaria.Banco.Descricao.ToUpper().Contains("SICREDI"))
                                {
                                    retornoSicredi(dataInicial, dataFinal, boletoConfig);
                                }
                                if (boletoConfig.ContaBancaria.Banco.Descricao.ToUpper().Contains("BB") || boletoConfig.ContaBancaria.Banco.Descricao.ToUpper().Contains("BANCO DO BRASIL"))
                                {
                                    retornoBancoBrasil(dataInicial, dataFinal, boletoConfig);
                                }
                            }
                        }
                        break;
                }
                formBackground.Dispose();
            }
        }

        private async void retornoBancoBrasil(DateTime dataInicial, DateTime dataFinal, BoletoConfig boletoConfig)
        {
            try
            {
                DateTime dataAtual = dataInicial;
                StringBuilder todasAsMensagens = new StringBuilder();

                BBApiService bBApiService = new BBApiService(boletoConfig.AmbienteProducao, boletoConfig.IdToken, boletoConfig.Token);
                RetornoListaBoletosBaixadosBB listaBaixaBB = await bBApiService.ListarBoletosBancoBrasilAsync(boletoConfig.ContaBancaria.Agencia, boletoConfig.ContaBancaria.Conta, dataInicial.ToString("dd.MM.yyyy"), dataFinal.ToString("dd.MM.yyyy"));

                // Exibe o resultado se houver boletos
                if (listaBaixaBB != null)
                {

                    // Agora você pode acessar as propriedades do objetoma retornoBoletos
                    //Console.WriteLine($"Indicador de Continuidade: {retornoBoletos.IndicadorContinuidade}");
                    foreach (var boleto in listaBaixaBB.Boletos)
                    {
                        string nossoNumero = boleto.NumeroBoletoBB;
                        IList<ContaReceber> ListaContaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber Tabela where Tabela.NossoNumero = '" + nossoNumero + "' and Tabela.FlagExcluido <> true");
                        if(ListaContaReceber.Count > 0)
                        {
                            contaReceber.Recebido = true;
                            contaReceber.ValorRecebido = boleto.ValorPago;
                            if(contaReceber.ValorRecebido != contaReceber.ValorParcela)
                            {
                                decimal diferenca = contaReceber.ValorRecebido - contaReceber.ValorParcela;
                                contaReceber.Juro = 0;
                                contaReceber.Multa = 0;
                                contaReceber.AcrescimoRecebidoBaixa = diferenca;
                            }
                            contaReceber.DataRecebimento = DateTime.Parse(boleto.DataMovimento);
                            contaReceber.DescricaoRecebimento = "RETORNO BANCO DO BRASIL, SOLICITADO EM " + DateTime.Now.ToShortDateString();
                            Controller.getInstance().salvar(contaReceber);

                            RetornoBanco retornoBanco = new RetornoBanco();
                            retornoBanco.AbatimentoLiquido = 0;
                            retornoBanco.MultaLiquida = 0;
                            retornoBanco.CodigoBeneficiario = "";
                            retornoBanco.ContaReceber = contaReceber;
                            retornoBanco.Cooperativa = "";
                            retornoBanco.CooperativaPostoBeneficiario = "";
                            retornoBanco.DataPagamento = DateTime.Parse(boleto.DataMovimento);
                            retornoBanco.DescontoLiquido = 0;
                            retornoBanco.Descricao = "RETORNO BANCO DO BRASIL, SOLICITADO EM " + DateTime.Now.ToShortDateString();
                            retornoBanco.JurosLiquido = 0;
                            retornoBanco.TipoLiquidacao = boleto.EstadoTituloCobranca;
                            retornoBanco.Valor = boleto.ValorOriginal;
                            retornoBanco.ValorLiquidado = boleto.ValorPago;
                            Controller.getInstance().salvar(retornoBanco);

                            Caixa caixa = new Caixa();
                            caixa.Cobrador = null;
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = contaReceber.ContaBoleto;
                            caixa.DataLancamento = contaReceber.DataRecebimento;
                            caixa.Descricao = "REC. DE BOLETO - BB " + contaReceber.Documento + " " + contaReceber.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = contaReceber.FormaPagamento;
                            caixa.IdOrigem = contaReceber.Id.ToString();
                            caixa.Pessoa = contaReceber.Cliente;
                            caixa.PlanoConta = contaReceber.PlanoConta;
                            caixa.TabelaOrigem = "CONTARECEBER";
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = contaReceber.ValorRecebido;
                            caixa.Observacoes = "RETORNO AUTOMÁTICO BANCO, PUXADO EM: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                            Controller.getInstance().salvar(caixa);

                            todasAsMensagens.AppendLine($"Boleto Liquidado, Nosso Número: {nossoNumero}, Data de Lançamento: {caixa.DataLancamento:dd/MM/yyyy}, Valor: {caixa.Valor:C}");
                        }
                        Console.WriteLine($"Número do Boleto: {boleto.NumeroBoletoBB}, Valor Pago: {boleto.ValorPago}");
                        
                    }
                }

                // Incrementa a data para o próximo dia
                dataAtual = dataAtual.AddDays(1);

                if (todasAsMensagens.Length > 0)
                {
                    GenericaDesktop.ShowInfo(todasAsMensagens.ToString());
                }
                else
                {
                    GenericaDesktop.ShowInfo("Nenhum boleto liquidado foi encontrado no período.");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        private async void retornoSicredi(DateTime dataInicial, DateTime dataFinal, BoletoConfig boletoConfig)
        {
            DateTime dataAtual = dataInicial;
            StringBuilder todasAsMensagens = new StringBuilder();
            while (dataAtual.Date <= dataFinal.Date)
            {
                // Chama o método para consultar boletos liquidados na dataAtual
                BoletoSicrediManager boletoSicrediManager = new BoletoSicrediManager(0, 0, boletoConfig.ContaBancaria, boletoConfig.AmbienteProducao, null);
                string resultado = await boletoSicrediManager.ConsultarBoletosLiquidadosPorDiaAsync(dataAtual);

                // Exibe o resultado se houver boletos
                if (!string.IsNullOrEmpty(resultado))
                {
                    todasAsMensagens.AppendLine($"Boletos Liquidados no dia {dataAtual.ToString("dd/MM/yyyy")}: {resultado}");
                }

                // Incrementa a data para o próximo dia
                dataAtual = dataAtual.AddDays(1);
            }

            if (todasAsMensagens.Length > 0)
            {
                GenericaDesktop.ShowInfo(todasAsMensagens.ToString());
            }
            else
            {
                GenericaDesktop.ShowInfo("Nenhum boleto liquidado foi encontrado no período.");
            }
        }
        private void retornoCelCash(DateTime dataInicial, DateTime dataFinal)
        {
            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
            GalaxyPay_Result retGalaxy = galaxyPayApiIntegracao.GalaxyPay_ListarTransacoes(dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"));
            if (retGalaxy != null)
            {
                listaContaReceber = retGalaxy.ContasRecebidas;
                if (listaContaReceber != null && listaContaReceber.Count > 0)
                {
                    calculaTotalNotas();
                    sfDataPager1.DataSource = listaContaReceberCalculado;

                    int pageSize;
                    if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts) && int.TryParse(txtRegistroPorPagina.Texts, out pageSize))
                        sfDataPager1.PageSize = pageSize;
                    else
                        sfDataPager1.PageSize = 100;

                    grid.DataSource = sfDataPager1.PagedSource;
                    sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                    this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    this.grid.AutoSizeController.Refresh();
                    grid.Refresh();

                    // Garantir que a célula atual está sendo atualizada corretamente
                    this.grid.BeginInvoke((Action)(() =>
                    {
                        this.grid.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));
                    }));

                    int w = Screen.PrimaryScreen.Bounds.Width;
                    int h = Screen.PrimaryScreen.Bounds.Height;
                    if (w == 1920 && h == 1080)
                    {
                        this.grid.View.Records.CollectionChanged += Records_CollectionChanged;
                    }
                }
            }
        }
        private void btnImprimirBoleto_Click(object sender, EventArgs e)
        {
            //gera boleto para o banco especifico
            gerarBoletoParcelasSelecionadas2();
        }
        private void selecionarContaEmissaoBoleto()
        {
            Object objeto = new ContaBancaria();

            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
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
                    switch (uu.showModal("ContaBancaria", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            contaSelecionada = ((ContaBancaria)objeto);
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

        ContaBancaria contaBoletoSelecionada = new ContaBancaria();
        ContaBancaria contaSelecionada = new ContaBancaria();

        //private async void gerarBoletoParcelasSelecionadas2()
        //{
        //    try
        //    {
        //        bool ambienteProducao = false;
        //        lblCalculando.Visible = true;
        //        lblCalculando.Text = "Gerando Boleto, Comunicando com o Banco...";
        //        btnImprimirBoleto.Enabled = false;
        //        Logger logger = new Logger();
        //        if (!String.IsNullOrEmpty(txtCodCliente.Texts))
        //        {
        //            int vendaId = 0;
        //            int osId = 0;
        //            // Inicializando array para boletos GalaxyPay
        //            string[] arrayFatura = new string[grid.SelectedItems.Count];

        //            // Verificar se há linhas selecionadas no grid
        //            var selectedRows = grid.SelectedItems;
        //            if (selectedRows.Count == 0)
        //            {
        //                MessageBox.Show("Selecione pelo menos uma parcela para gerar o boleto.");
        //                return;
        //            }

        //            List<ContaReceber> contasSelecionadas = new List<ContaReceber>();

        //            // Adicionar as contas selecionadas à lista
        //            foreach (var row in selectedRows)
        //            {
        //                ContaReceber contaReceber = row as ContaReceber;
        //                if (contaReceber != null)
        //                {
        //                    contasSelecionadas.Add(contaReceber);
        //                }
        //            }

        //            if (contasSelecionadas.Count > 0)
        //            {

        //                int iGalaxyPay = 0;
        //                List<string> linhasDigitaveisSicredi = new List<string>(); // Para armazenar as linhas digitáveis dos boletos Sicredi

        //                // HashSet para rastrear vendas e OS já processadas
        //                HashSet<int> vendasProcessadas = new HashSet<int>();
        //                HashSet<int> osProcessadas = new HashSet<int>();

        //                foreach (ContaReceber contaReceber in contasSelecionadas)
        //                {
        //                    vendaId = contaReceber.Venda?.Id ?? 0;
        //                    osId = contaReceber.OrdemServico?.Id ?? 0;
        //                    Console.WriteLine($"Processando conta: VendaId = {vendaId}, OsId = {osId}");
        //                    // Verificar se a venda ou OS já foi processada
        //                    if (vendasProcessadas.Contains(vendaId) || osProcessadas.Contains(osId))
        //                    {
        //                        // Se já foi processada, continuar para a próxima parcela
        //                        continue;
        //                    }

        //                    // Adiciona a venda e OS ao conjunto de processados
        //                    if (vendaId > 0)
        //                        vendasProcessadas.Add(vendaId);
        //                    if (osId > 0)
        //                        osProcessadas.Add(osId);


        //                    // Se a conta de boleto for null, tenta selecionar
        //                    if (contaReceber.ContaBoleto == null)
        //                    {
        //                        if (contaBoletoSelecionada.Id == 0)
        //                        {
        //                            selecionarContaEmissaoBoleto();
        //                            contaBoletoSelecionada = contaSelecionada; // Armazena a conta selecionada
        //                        }

        //                        // Se a conta selecionada não for nula e válida, atribui
        //                        if (contaBoletoSelecionada != null && contaBoletoSelecionada.Id > 0)
        //                        {
        //                            contaReceber.ContaBoleto = contaBoletoSelecionada;
        //                            Controller.getInstance().salvar(contaReceber);
        //                        }
        //                    }

        //                    // Verificar a plataforma para geração de boleto
        //                    IList<BoletoConfig> listaBoletoConfig = new List<BoletoConfig>();
        //                    listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
        //                    if (listaBoletoConfig.Count > 0) 
        //                    {
        //                        foreach (BoletoConfig boletoConfig in listaBoletoConfig)
        //                        {
        //                            if (boletoConfig.ContaBancaria.Banco.Descricao.Contains("SICREDI") && contaReceber.ContaBoleto.Id == boletoConfig.ContaBancaria.Id)
        //                            {
        //                                ambienteProducao = boletoConfig.AmbienteProducao;
        //                                if (contaReceber.BoletoGerado)
        //                                {
        //                                    // Adicionar linha digitável do boleto gerado à lista
        //                                    if (!string.IsNullOrEmpty(contaReceber.LinhaDigitavel))
        //                                    {
        //                                        linhasDigitaveisSicredi.Add(contaReceber.LinhaDigitavel);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    // Gerar o boleto Sicredi
        //                                    lblCalculando.Text = "Gerando Boleto Sicredi";
        //                                    BoletoSicrediManager boletoManager = new BoletoSicrediManager(vendaId, osId, contaReceber.ContaBoleto, boletoConfig.AmbienteProducao);
        //                                    bool sucesso = await boletoManager.GeraBoletosSicredi(contaReceber.Cliente, true);
        //                                    if (sucesso == true)
        //                                        lblCalculando.Text = "Boleto Sicredi Gerado com Sucesso!";
        //                                }
        //                            }
        //                            else if ((boletoConfig.ContaBancaria.Banco.Descricao.Contains("BB") || boletoConfig.ContaBancaria.Banco.Descricao.Contains("BANCO DO BRASIL")) && contaReceber.ContaBoleto.Id == boletoConfig.ContaBancaria.Id)
        //                            {
        //                                try
        //                                {
        //                                    BBApiService bbApiService = new BBApiService(boletoConfig.AmbienteProducao, boletoConfig.IdToken, boletoConfig.Token);
        //                                    RetornoBoletoBB response = await bbApiService.CriarBoletoBancoBrasilAsync(contaReceber.Cliente, contaReceber, boletoConfig);
        //                                    if (response != null)
        //                                    {
        //                                        GenericaDesktop.ShowInfo("Boleto gerado com sucesso!");
        //                                        contaReceber.BoletoGerado = true;
        //                                        contaReceber.LinhaDigitavel = response.linhaDigitavel;
        //                                        contaReceber.NossoNumero = response.numero;
        //                                        contaReceber.IdBoleto = "";
        //                                        contaReceber.QrCode = response.qrCode.emv;
        //                                        Controller.getInstance().salvar(contaReceber);


        //                                        BbDetalheBoletoResponse boletoDetalheRet = await bbApiService.DetalharBoletoAsync(response.numero, int.Parse(boletoConfig.Convenio));
        //                                        if (boletoDetalheRet != null)
        //                                        {
        //                                            FrmImprimirBoletoBB frmImprimirBoletoBB = new FrmImprimirBoletoBB(boletoDetalheRet, contaReceber.EmpresaFilial, response, boletoConfig);
        //                                            frmImprimirBoletoBB.ShowDialog();
        //                                        }
        //                                    }

        //                                }
        //                                catch (ApiException apiEx)
        //                                {
        //                                    // Exibir mensagens amigáveis para cada erro retornado pela API
        //                                    foreach (var erro in apiEx.ApiError.erros)
        //                                    {
        //                                        MessageBox.Show($"Código: {erro.codigo}, Mensagem: {erro.mensagem}");
        //                                    }
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    // Tratar outras exceções, se necessário
        //                                    MessageBox.Show($"Ocorreu um erro inesperado: {ex.Message}");
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (contaReceber.ContaBoleto.Banco.Descricao == "GALAXYPAY" || contaReceber.ContaBoleto.Banco.Descricao == "CELCASH" || contaReceber.ContaBoleto.Descricao == "CELCASH")
        //                    {
        //                        GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
        //                        if (contaReceber.BoletoGerado)
        //                        {
        //                            arrayFatura[iGalaxyPay] = contaReceber.Id.ToString();
        //                            iGalaxyPay++;
        //                        }
        //                        else
        //                        {
        //                            if (GenericaDesktop.ShowConfirmacao("Deseja gerar o boleto no CelCash?"))
        //                            {
        //                                string mensagem = "";
        //                                bool isValid = GenericaDesktop.ValidarClienteEmissaoBoleto(contaReceber.Cliente, out mensagem);
        //                                if (isValid)
        //                                {
        //                                    IList<ContaReceber> listaCrediario = new List<ContaReceber> { contaReceber };
        //                                    GerarBoletoGalaxyPay gerarBoleto = new GerarBoletoGalaxyPay();
        //                                    Pessoa clienteBoleto = contaReceber.Cliente;
        //                                    lblCalculando.Text = "Gerando Boleto CelCash";
        //                                    gerarBoleto.gerarBoletoAvulsoGalaxyPay(listaCrediario, clienteBoleto);
        //                                }
        //                                else
        //                                {
        //                                    GenericaDesktop.ShowErro(mensagem);
        //                                }
        //                            }
        //                        }

        //                        if (arrayFatura.Length > 0)
        //                        {
        //                            lblCalculando.Text = "Obtendo PDF Boleto CelCash";
        //                            string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
        //                            string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(arrayFatura);
        //                            if (!String.IsNullOrEmpty(link))
        //                            {
        //                                System.Diagnostics.Process.Start(link);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Plataforma de geração de boleto desconhecida.");
        //                    }
        //                }

        //                // Se houver boletos Sicredi já gerados, chamar o método para download

        //                if (linhasDigitaveisSicredi.Count > 0)
        //                {
        //                    BoletoSicrediManager boletoSicrediManager = new BoletoSicrediManager(vendaId, osId, contasSelecionadas.First().ContaBoleto, ambienteProducao);
        //                    string[] linhasDigitaveisArray = linhasDigitaveisSicredi.ToArray();
        //                    await boletoSicrediManager.BaixarBoletosExistentesSicredi(linhasDigitaveisArray, true);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            GenericaDesktop.ShowAlerta("Selecione apenas um cliente para emissão ou impressão de boletos, pesquisa no campo de pesquisar clientes!");
        //        }
        //        lblCalculando.Text = "Finalizado...";
        //        lblCalculando.Visible = false;
        //        btnImprimirBoleto.Enabled = true;
        //        btnPesquisar.PerformClick();
        //    }
        //    catch (Exception erro)
        //    {
        //        GenericaDesktop.ShowAlerta(erro.Message);
        //    }
        //}


        private void imprimirBoletoBB()
        {

        }

        private async void enviarBoletoENFPorWhatsApp()
        {
            try
            {
                string IDcliente = "";
                int idNotaFiscal = 0;

                if (grid.SelectedItems.Count == 1)
                {
                    List<string> listaFaturas = new List<string>();

                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var conta = selectedItem as ContaReceber;
                        contaReceber = conta;
                        if (conta.BoletoGerado == true)
                        {
                            string documento = conta.Documento;
                            IDcliente = conta.Cliente.Id.ToString();
                            IList<ContaReceber> contaReceberAssociado = new List<ContaReceber>();

                            if (conta.Venda != null)
                                contaReceberAssociado = ObterParcelasAssociadas(conta.Venda, null, IDcliente);

                            if (conta.OrdemServico != null)
                                contaReceberAssociado = ObterParcelasAssociadas(null, conta.OrdemServico, IDcliente);

                            foreach (var receber in contaReceberAssociado)
                            {
                                listaFaturas.Add(receber.Id.ToString());

                                if (receber.OrdemServico != null)
                                {
                                    if (receber.OrdemServico.Nfe != null)
                                        idNotaFiscal = receber.OrdemServico.Nfe.Id;
                                }
                                else if (receber.Venda != null)
                                {
                                    if (receber.Venda.Nfe != null)
                                        idNotaFiscal = receber.Venda.Nfe.Id;
                                }
                            }
                        }
                    }

                    // Coleta o PDF dos boletos
                    if (contaReceber.BoletoGerado == true)
                    {
                        GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                        string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                        string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(listaFaturas.ToArray());

                        // O método BaixarPDFAsync agora é aguardado assincronamente
                        string localBoleto = await BaixarPDFAsync(link, int.Parse(IDcliente));

                        // Coleta nota fiscal se houver
                        List<Tuple<string, string>> listaCaminhoNotasParaEnviarEmail = new List<Tuple<string, string>>();
                        if (idNotaFiscal > 0)
                        {
                            Nfe nfe = new Nfe();
                            nfe.Id = idNotaFiscal;
                            nfe = (Nfe)Controller.getInstance().selecionar(nfe);

                            var caminhos = coletarNotaFiscalPDFeXML(nfe);
                            listaCaminhoNotasParaEnviarEmail.Add(caminhos);
                        }
                        String assuntoEmail = "Boleto " + Sessao.empresaFilialLogada.NomeFantasia;
                        List<string> listaCaminhosFinal = new List<string>();
                        if (listaCaminhoNotasParaEnviarEmail.Count > 0)
                        {
                            assuntoEmail = "Boleto e Nota Fiscal " + Sessao.empresaFilialLogada.NomeFantasia;
                            foreach (var caminhoNota in listaCaminhoNotasParaEnviarEmail)
                            {
                                listaCaminhosFinal.Add(caminhoNota.Item1); // Adiciona o caminho do PDF da Nota Fiscal
                                listaCaminhosFinal.Add(caminhoNota.Item2); // Adiciona o caminho do XML da Nota Fiscal
                            }
                        }
                        listaCaminhosFinal.Add(localBoleto);

                        bool retornoMensagem = false;
                        if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
                        {
                            string telefoneCompleto = "";
                            if (contaReceber.Cliente.PessoaTelefone != null)
                            {
                                telefoneCompleto = EnviarMensagemWhatsapp.TratarTelefone(contaReceber.Cliente.PessoaTelefone.Ddd, contaReceber.Cliente.PessoaTelefone.Telefone);
                            }
                            FrmEnvioMensagem frmEnvioMensagem = new FrmEnvioMensagem(telefoneCompleto, capturarPrimeiroNomeParaMensagem(contaReceber.Cliente.RazaoSocial));
                            if (frmEnvioMensagem.ShowDialog() == DialogResult.OK)
                            {
                                string escolha = frmEnvioMensagem.GetEscolha();
                                string numero = frmEnvioMensagem.GetTelefone();
                                string nome = frmEnvioMensagem.GetNome();
                                string mensagem = frmEnvioMensagem.GetMensagem();
                                EnviarMensagemWhatsapp enviarMsg = new EnviarMensagemWhatsapp();
                                if (!String.IsNullOrEmpty(mensagem))
                                    await enviarMsg.SendMessageAsync(numero, mensagem);
                                foreach (string caminho in listaCaminhosFinal)
                                {
                                    await enviarMsg.SendMediaMessageAsync(numero, caminho);
                                }
                            }
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Essa parcela não possui boleto gerado pelo sistema Lunar!");
                }
                else if (grid.SelectedItems.Count > 1)
                {
                    GenericaDesktop.ShowAlerta("Selecione apenas 1 parcela da venda, o sistema irá capturar todas parcelas (da mesma venda) e nota fiscal para enviar no e-mail!");
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione pelo menos 1 parcela");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao enviar mensagem: " + erro.Message);
            }
        }


        private async void enviarBoletoENFPorWhatsAppNew()
        {
            try
            {
                String urlNFSe = "";
                List<string> listaCaminhosFinal = new List<string>();
                string IDcliente = "";
                int idNotaFiscal = 0;
                int vendaId = 0;
                int osId = 0;
                List<string> listaLinhasDigitaveis = new List<string>();
                IList<ContaReceber> contaReceberAssociado = new List<ContaReceber>();
                if (grid.SelectedItems.Count == 1)
                {
                    List<string> listaFaturas = new List<string>();

                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var conta = selectedItem as ContaReceber;
                        contaReceber = conta;
                        if (conta.BoletoGerado == true)
                        {
                            string documento = conta.Documento;
                            IDcliente = conta.Cliente.Id.ToString();
                            contaReceberAssociado = new List<ContaReceber>();

                            if (conta.Venda != null)
                            {
                                vendaId = conta.Venda.Id;
                                contaReceberAssociado = ObterParcelasAssociadas(conta.Venda, null, IDcliente);
                            }

                            if (conta.OrdemServico != null)
                            {
                                osId = conta.OrdemServico.Id;
                                contaReceberAssociado = ObterParcelasAssociadas(null, conta.OrdemServico, IDcliente);
                                if (conta.OrdemServico.Nfse != null)
                                {
                                    urlNFSe = conta.OrdemServico.Nfse.UrlDanfe;
                                    string caminhoNotaServico = await BaixarEPDFSalvarNaTempAsync(urlNFSe);
                                    listaCaminhosFinal.Add(caminhoNotaServico);
                                }
                            }

                            foreach (var receber in contaReceberAssociado)
                            {
                                listaFaturas.Add(receber.Id.ToString());

                                if (receber.OrdemServico != null)
                                {
                                    if (receber.OrdemServico.Nfe != null)
                                        idNotaFiscal = receber.OrdemServico.Nfe.Id;
                                }
                                else if (receber.Venda != null)
                                {
                                    if (receber.Venda.Nfe != null)
                                        idNotaFiscal = receber.Venda.Nfe.Id;
                                }
                                if (receber.ContaBoleto != null)
                                {
                                    if (receber.ContaBoleto.Banco.Descricao.ToUpper().Contains("SICREDI"))
                                    {
                                        listaLinhasDigitaveis.Add(contaReceber.LinhaDigitavel);
                                    }
                                }
                            }
                        }
                    }

                    // Coleta o PDF dos boletos
                    string localBoleto = ""; 
                    if (contaReceber.BoletoGerado == true) 
                    {
                        if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
                        {
                            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                            string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                            string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(listaFaturas.ToArray());

                            // O método BaixarPDFAsync agora é aguardado assincronamente
                            localBoleto = await BaixarPDFAsync(link, int.Parse(IDcliente));
                        }
                        IList<BoletoConfig> listaBoletoConfig = new List<BoletoConfig>();
                        listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
                        if (listaBoletoConfig.Count > 0)
                        {
                            BoletoSicrediManager boletoSicrediManager = new BoletoSicrediManager(vendaId, osId, contaReceber.ContaBoleto, true, contaReceber);
                            IList<string> listaCaminhosBoletos = new List<string>();
                            listaCaminhosBoletos = await boletoSicrediManager.BaixarBoletosExistentesSicredi(listaLinhasDigitaveis.ToArray(), false);
                            string outputFilePath = "";
                            if (listaCaminhosBoletos.Count > 0)
                            {
                                string[] pdfPaths = listaCaminhosBoletos.ToArray();
                                string tempDirectory = Path.GetTempPath();
                                outputFilePath = Path.Combine(tempDirectory, "Boleto.pdf");
                                CombinePdfs(pdfPaths, outputFilePath);
                                localBoleto = outputFilePath;
                            }
                        }

                        // Coleta nota fiscal se houver
                        List<Tuple<string, string>> listaCaminhoNotasParaEnviarEmail = new List<Tuple<string, string>>();
                        if (idNotaFiscal > 0)
                        {
                            Nfe nfe = new Nfe();
                            nfe.Id = idNotaFiscal;
                            nfe = (Nfe)Controller.getInstance().selecionar(nfe);

                            var caminhos = coletarNotaFiscalPDFeXML(nfe);
                            listaCaminhoNotasParaEnviarEmail.Add(caminhos);
                        }
                        String assuntoEmail = "Boleto " + Sessao.empresaFilialLogada.NomeFantasia;
    
                        if (listaCaminhoNotasParaEnviarEmail.Count > 0)
                        {
                            assuntoEmail = "Boleto e Nota Fiscal " + Sessao.empresaFilialLogada.NomeFantasia;
                            foreach (var caminhoNota in listaCaminhoNotasParaEnviarEmail)
                            {
                                listaCaminhosFinal.Add(caminhoNota.Item1); // Adiciona o caminho do PDF da Nota Fiscal
                                listaCaminhosFinal.Add(caminhoNota.Item2); // Adiciona o caminho do XML da Nota Fiscal
                            }
                        }
                        listaCaminhosFinal.Add(localBoleto);

                        bool retornoMensagem = false;
                        if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
                        {
                            string telefoneCompleto = "";
                            if (contaReceber.Cliente.PessoaTelefone != null)
                            {
                                telefoneCompleto = EnviarMensagemWhatsapp.TratarTelefone(contaReceber.Cliente.PessoaTelefone.Ddd, contaReceber.Cliente.PessoaTelefone.Telefone);
                            }
                            FrmEnvioMensagem frmEnvioMensagem = new FrmEnvioMensagem(telefoneCompleto, capturarPrimeiroNomeParaMensagem(contaReceber.Cliente.RazaoSocial), true);
                            if (frmEnvioMensagem.ShowDialog() == DialogResult.OK)
                            {
                                string escolha = frmEnvioMensagem.GetEscolha();
                                string numero = frmEnvioMensagem.GetTelefone();
                                string nome = frmEnvioMensagem.GetNome();
                                string mensagem = frmEnvioMensagem.GetMensagem();
                                EnviarMensagemWhatsapp enviarMsg = new EnviarMensagemWhatsapp();
                                if (!String.IsNullOrEmpty(mensagem))
                                    await enviarMsg.SendMessageAsync(numero, mensagem);
                                foreach (string caminho in listaCaminhosFinal)
                                {
                                    await enviarMsg.SendMediaMessageAsync(numero, caminho);
                                }
                            }
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Essa parcela não possui boleto gerado pelo sistema Lunar!");
                }
                else if (grid.SelectedItems.Count > 1)
                {
                    GenericaDesktop.ShowAlerta("Selecione apenas 1 parcela da venda, o sistema irá capturar todas parcelas (da mesma venda) e nota fiscal para enviar no e-mail!");
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione pelo menos 1 parcela");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao enviar mensagem: " + erro.Message);
            }
        }



        private string capturarPrimeiroNomeParaMensagem(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
            {
                return nomeCompleto;
            }

            string[] partes = nomeCompleto.Split(' ');
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            string nomeFormatado = "";

            if (partes.Length >= 2)
            {
                string primeiroNomeFormatado = textInfo.ToTitleCase(partes[0].ToLower());
                string segundoNomeFormatado = textInfo.ToTitleCase(partes[1].ToLower());
                nomeFormatado = $"{primeiroNomeFormatado} {segundoNomeFormatado}";
            }
            else
            {
                nomeFormatado = textInfo.ToTitleCase(nomeCompleto.ToLower());
            }

            return nomeFormatado;
        }
        private async void btnEmail_Click(object sender, EventArgs e)
        {
            string IDcliente = "";
            int idNotaFiscal = 0;
            string notaServicoPdf = "";
            IList<ContaReceber> contaReceberAssociado = new List<ContaReceber>();
            if (grid.SelectedItems.Count == 1)
            {
                List<string> listaFaturas = new List<string>();

                foreach (var selectedItem in grid.SelectedItems)
                {
                    var conta = selectedItem as ContaReceber;
                    contaReceber = conta;
                    if (conta.BoletoGerado == true)
                    {
                        string documento = conta.Documento;
                        IDcliente = conta.Cliente.Id.ToString();
                        contaReceberAssociado = new List<ContaReceber>();

                        if (conta.Venda != null)
                            contaReceberAssociado = ObterParcelasAssociadas(conta.Venda, null, IDcliente);

                        if (conta.OrdemServico != null)
                            contaReceberAssociado = ObterParcelasAssociadas(null, conta.OrdemServico, IDcliente);

                        foreach (var receber in contaReceberAssociado)
                        {
                            listaFaturas.Add(receber.Id.ToString());

                            if (receber.OrdemServico != null)
                            {
                                if (receber.OrdemServico.Nfe != null)
                                    idNotaFiscal = receber.OrdemServico.Nfe.Id;
                                if (receber.OrdemServico.Nfse != null)
                                    notaServicoPdf = receber.OrdemServico.Nfse.UrlDanfe;
                            }
                            else if (receber.Venda != null)
                            {
                                if (receber.Venda.Nfe != null)
                                    idNotaFiscal = receber.Venda.Nfe.Id;
                            }
                        }
                    }
                }
                
                if (!String.IsNullOrEmpty(contaReceber.Cliente.Email) && Sessao.parametroSistema.IntegracaoGalaxyPay == true)
                {
                    dispararEmailBoletosGalaxyPayCelCash(idNotaFiscal, listaFaturas, IDcliente, notaServicoPdf);
                }
                IList<BoletoConfig> listaBoletoConfig = new List<BoletoConfig>();
                listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
                if (!String.IsNullOrEmpty(contaReceber.Cliente.Email) && listaBoletoConfig.Count > 0)
                {
                    dispararEmailBoletosSicredi(idNotaFiscal, contaReceberAssociado, IDcliente, notaServicoPdf);
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione apenas 1 parcela da venda, o sistema irá capturar todas parcelas e nota fiscal para enviar no e-mail!");
            }
        }

        public async Task<string> BaixarEPDFSalvarNaTempAsync(string urlPdf)
        {
            try
            {
                // Cria uma instância do HttpClient para fazer a requisição
                using (HttpClient client = new HttpClient())
                {
                    // Faz o download do PDF
                    HttpResponseMessage response = await client.GetAsync(urlPdf);

                    // Verifica se o status da resposta é sucesso
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo do PDF como byte array
                        byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();

                        // Gera o caminho para o arquivo na pasta temporária
                        string tempPath = Path.Combine(Path.GetTempPath(), "NOTAFISCALSERVICO.pdf");

                        // Salva o PDF no local especificado
                        File.WriteAllBytes(tempPath, pdfBytes);

                        // Retorna o caminho onde o arquivo foi salvo
                        return tempPath;
                    }
                    else
                    {
                        throw new Exception($"Falha ao baixar o PDF. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao baixar o PDF: {ex.Message}");
                return null;
            }
        }
        private async void dispararEmailBoletosGalaxyPayCelCash(int idNotaFiscal, List<string> listaFaturas, string IDcliente, string urlNFSe)
        {
            List<string> listaCaminhosFinal = new List<string>();
            // Coleta o PDF dos boletos
            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
            string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
            string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(listaFaturas.ToArray());

            // O método BaixarPDFAsync agora é aguardado assincronamente
            string localBoleto = await BaixarPDFAsync(link, int.Parse(IDcliente));
            if (!String.IsNullOrEmpty(urlNFSe))
            {
                string caminhoNotaServico = await BaixarEPDFSalvarNaTempAsync(urlNFSe);
                listaCaminhosFinal.Add(caminhoNotaServico);
            }

            // Coleta nota fiscal se houver
            List<Tuple<string, string>> listaCaminhoNotasParaEnviarEmail = new List<Tuple<string, string>>();
            if (idNotaFiscal > 0)
            {
                Nfe nfe = new Nfe();
                nfe.Id = idNotaFiscal;
                nfe = (Nfe)Controller.getInstance().selecionar(nfe);

                var caminhos = coletarNotaFiscalPDFeXML(nfe);
                listaCaminhoNotasParaEnviarEmail.Add(caminhos);
            }
            String assuntoEmail = "Boleto " + Sessao.empresaFilialLogada.NomeFantasia;
           
            if (listaCaminhoNotasParaEnviarEmail.Count > 0)
            {
                assuntoEmail = "Boleto e Nota Fiscal " + Sessao.empresaFilialLogada.NomeFantasia;
                foreach (var caminhoNota in listaCaminhoNotasParaEnviarEmail)
                {
                    listaCaminhosFinal.Add(caminhoNota.Item1); // Adiciona o caminho do PDF da Nota Fiscal
                    listaCaminhosFinal.Add(caminhoNota.Item2); // Adiciona o caminho do XML da Nota Fiscal
                }
            }
            listaCaminhosFinal.Add(localBoleto);

            bool retornoEmail = false;
            if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.ServidorEmail))
                retornoEmail = generica.enviarEmail(contaReceber.Cliente.Email, assuntoEmail, "E-mail disparado via Lunar Software", "Prezado(a) Cliente, Segue em anexo.", listaCaminhosFinal);
            else
                retornoEmail = generica.enviarEmailPeloLunar(contaReceber.Cliente.Email, assuntoEmail, "E-mail disparado pelo sistema Lunar Software", "Prezado(a) Cliente, Segue em anexo.", listaCaminhosFinal);

            if (retornoEmail == true)
                GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
        }

        private async void dispararEmailBoletosSicredi(int idNotaFiscal, IList<ContaReceber> listaReceber, string IDcliente, string urlNotaServico)
        {
            List<string> listaCaminhosFinal = new List<string>();
            List<string> listaLinhasDigitaveis = new List<string>();
            int vendaId = 0;
            int osId = 0;
            ContaBancaria conta = new ContaBancaria();
            foreach(ContaReceber contaReceber in listaReceber)
            {
                listaLinhasDigitaveis.Add(contaReceber.LinhaDigitavel);
                if (contaReceber.Venda != null)
                    vendaId = contaReceber.Venda.Id;
                if (contaReceber.OrdemServico != null)
                    osId = contaReceber.OrdemServico.Id;
                conta = contaReceber.ContaBoleto;
            }
            // Inicializa o gerenciador do Sicredi
            BoletoSicrediManager boletoSicrediManager = new BoletoSicrediManager(vendaId, osId, conta, true, null);

            // Baixa os boletos do Sicredi (PDF)
            string caminhoBoleto = "";
            IList<string> listaCaminhosBoletos = new List<string>();
            listaCaminhosBoletos = await boletoSicrediManager.BaixarBoletosExistentesSicredi(listaLinhasDigitaveis.ToArray(), false);
            string outputFilePath = "";
            if (listaCaminhosBoletos.Count > 0)
            {
                string[] pdfPaths = listaCaminhosBoletos.ToArray();
                string tempDirectory = Path.GetTempPath();
                outputFilePath = Path.Combine(tempDirectory, "Boleto.pdf");
                CombinePdfs(pdfPaths, outputFilePath);
            }
            if (!String.IsNullOrEmpty(urlNotaServico))
            {
                string caminhoNotaServico = await BaixarEPDFSalvarNaTempAsync(urlNotaServico);
                listaCaminhosFinal.Add(caminhoNotaServico);
            }
            // Coleta nota fiscal se houver
            List<Tuple<string, string>> listaCaminhoNotasParaEnviarEmail = new List<Tuple<string, string>>();
            if (idNotaFiscal > 0)
            {
                Nfe nfe = new Nfe();
                nfe.Id = idNotaFiscal;
                nfe = (Nfe)Controller.getInstance().selecionar(nfe);

                var caminhos = coletarNotaFiscalPDFeXML(nfe);
                listaCaminhoNotasParaEnviarEmail.Add(caminhos);
            }

            // Define o assunto do e-mail
            String assuntoEmail = "Boleto " + Sessao.empresaFilialLogada.NomeFantasia;
           

            // Se houver nota fiscal, adicionar ao assunto e à lista de arquivos
            if (listaCaminhoNotasParaEnviarEmail.Count > 0)
            {
                assuntoEmail = "Boleto e Nota Fiscal " + Sessao.empresaFilialLogada.NomeFantasia;
                foreach (var caminhoNota in listaCaminhoNotasParaEnviarEmail)
                {
                    listaCaminhosFinal.Add(caminhoNota.Item1); // PDF da Nota Fiscal
                    listaCaminhosFinal.Add(caminhoNota.Item2); // XML da Nota Fiscal
                }
            }

            // Adiciona o caminho do boleto Sicredi ao e-mail
            listaCaminhosFinal.Add(outputFilePath);

            // Dispara o e-mail
            bool retornoEmail = false;
            if (!String.IsNullOrEmpty(Sessao.parametroSistema.Email) && !String.IsNullOrEmpty(Sessao.parametroSistema.ServidorEmail))
            {
                retornoEmail = generica.enviarEmail(contaReceber.Cliente.Email, assuntoEmail, "E-mail disparado via Lunar Software", "Prezado(a) Cliente, Segue em anexo.", listaCaminhosFinal);
            }
            else
            {
                retornoEmail = generica.enviarEmailPeloLunar(contaReceber.Cliente.Email, assuntoEmail, "E-mail disparado pelo sistema Lunar Software", "Prezado(a) Cliente, Segue em anexo.", listaCaminhosFinal);
            }

            // Notifica o usuário sobre o resultado
            if (retornoEmail == true)
            {
                GenericaDesktop.ShowInfo("E-mail enviado com sucesso!");
            }
            else
            {
                GenericaDesktop.ShowErro("Erro ao enviar o e-mail.");
            }
        }

        static async Task<string> BaixarPDFAsync(string url, int idCliente)
        {
            using (HttpClient client = new HttpClient())
            {
                // Fazer a requisição HTTP GET para obter o PDF
                byte[] pdfBytes = await client.GetByteArrayAsync(url);

                // Gerar um nome de arquivo único com poucos caracteres
                string nomeArquivo = "BOLETO_"+ idCliente + $"_{Guid.NewGuid().ToString().Substring(0, 8)}.pdf";

                // Definir o caminho para a pasta temporária
                string caminhoParaSalvar = Path.Combine(Path.GetTempPath(), nomeArquivo);

                // Salvar o PDF na pasta temporária
                File.WriteAllBytes(caminhoParaSalvar, pdfBytes);

                // Retornar o caminho completo onde o arquivo foi salvo
                return caminhoParaSalvar;
            }
        }
        public void CombinePdfs(string[] pdfPaths, string outputFilePath)
        {
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputFilePath)))
            {
                PdfMerger merger = new PdfMerger(pdfDocument);

                foreach (string pdfPath in pdfPaths)
                {
                    if (File.Exists(pdfPath)) // Verifique se o arquivo PDF existe
                    {
                        using (PdfDocument sourceDocument = new PdfDocument(new PdfReader(pdfPath)))
                        {
                            if (sourceDocument.GetNumberOfPages() > 0) // Verifique se o PDF tem páginas
                            {
                                merger.Merge(sourceDocument, 1, sourceDocument.GetNumberOfPages());
                            }
                            else
                            {
                                Console.WriteLine($"O arquivo {pdfPath} não contém páginas.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O arquivo {pdfPath} não existe.");
                    }
                }
            }
        }
        private IList<ContaReceber> ObterParcelasAssociadas(Venda venda, OrdemServico ordemServico, string cliente)
        {
            IList<ContaReceber> listaReceber = new List<ContaReceber>();
            ContaReceberController contaReceberController = new ContaReceberController();
            if (venda != null)
            {
                listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber " +
                    "Tabela Where Tabela.Cliente = " + cliente + " and Tabela.Venda = " + venda.Id + " and Tabela.Recebido = false");
            }
            else if (ordemServico != null)
            {
                listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber " +
                                 "Tabela Where Tabela.Cliente = " + cliente + " and Tabela.OrdemServico = " + ordemServico.Id + " and Tabela.Recebido = false");
            }
            
            return listaReceber;
        }

        private void gerarPDF2(Nfe nfe, String pdf, String chave, bool imprimir)
        {
            if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
            {
                if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    frmPDF.ShowDialog();
                }

            }
            else if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }

        private void gerarPDF2Canceladas(Nfe nfe, String pdf, String chave, bool imprimir)
        {
            if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
            {
                if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf");
                    frmPDF.ShowDialog();
                }

            }
            else if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }
        private Tuple<string, string> coletarNotaFiscalPDFeXML(Nfe nfe)
        {
            string caminhoXML = "";
            string caminhoPDF = "";

            NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
            NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();

            if (nfe.Modelo.Equals("65"))
            {
                if (nfe.NfeStatus.Id == 1)
                {
                    nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                    caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                    if (!File.Exists(caminhoXML))
                    {
                        generica.gravarXMLNaPasta(nFCeDownloadProc.nfeProc.xml, nfe.Chave, @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFCe.xml");
                    }
                    gerarPDF2(nfe, nFCeDownloadProc.pdf, nfe.Chave, false);
                    caminhoPDF = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf";
                }
                if (nfe.NfeStatus.Id == 4)
                {
                    var nfceCancelada = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.Chave);
                    if (nfceCancelada != null)
                    {
                        caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\";
                        string nomeArquivo = nfe.Chave + @"-CAN.xml";
                        if (!File.Exists(caminhoXML + nomeArquivo))
                        {
                            generica.gravarXMLNaPasta(nfceCancelada.retEvento.xml, nfe.Chave, caminhoXML, nomeArquivo);
                            caminhoXML = caminhoXML + nomeArquivo;
                        }
                        if (nfceCancelada.pdfCancelamento != null)
                        {
                            gerarPDF2Canceladas(nfe, nfceCancelada.pdfCancelamento, nfe.Chave, false);
                            caminhoPDF = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.pdf";
                        }
                    }
                }
            }
            if (nfe.Modelo.Equals("55"))
            {
                if (nfe.NfeStatus.Id == 1)
                {
                    caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    if (!File.Exists(caminhoXML))
                    {
                        nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                        generica.gravarXMLNaPasta(nFeDownloadProc55.xml, nfe.Chave, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");
                    }
                    gerarPDF2(nfe, nFeDownloadProc55.pdf, nfe.Chave, false);
                    caminhoPDF = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf";
                }
                if (nfe.NfeStatus.Id == 4)
                {
                    var nfceCancelada = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, true, false, "");
                    if (nfceCancelada != null)
                    {
                        caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.xml";
                        if (!File.Exists(caminhoXML))
                        {
                            generica.gravarXMLNaPasta(nfceCancelada.xml, nfe.Chave, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\", nfe.Chave + "-CAN.xml");
                        }
                        gerarPDF2Canceladas(nfe, nfceCancelada.pdf, nfe.Chave, false);
                        caminhoPDF = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.pdf";
                    }
                }
            }

            return Tuple.Create(caminhoXML, caminhoPDF);
        }

        private void btnEnviarWhats_Click(object sender, EventArgs e)
        {
            //enviarBoletoENFPorWhatsApp();
            enviarBoletoENFPorWhatsAppNew();
        }


        private async void gerarBoletoParcelasSelecionadas2()
        {
            try
            {
                lblCalculando.Visible = true;
                lblCalculando.Text = "Gerando Boleto, Comunicando com o Banco...";
                btnImprimirBoleto.Enabled = false;

                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                {
                    var contasSelecionadas = SelecionarContasSelecionadas();
                    if (contasSelecionadas.Count == 0)
                    {
                        MessageBox.Show("Selecione pelo menos uma parcela para gerar o boleto.");
                        return;
                    }

                    //Reseta as listas necessárias q vao ser alimentadas se os boletos ja estiverem emitidos
                    listaFaturaGalaxyPay = new List<string>();
                    listaReceberBoleto = new List<ContaReceber>();

                    // Processamento das contas selecionadas
                    await ProcessarContasSelecionadas(contasSelecionadas);
                    await ImprimirBoletosSelecionadosAsync();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Selecione apenas um cliente para emissão ou impressão de boletos.");
                }

                lblCalculando.Text = "Finalizado...";
                lblCalculando.Visible = false;
                btnImprimirBoleto.Enabled = true;
                btnPesquisar.PerformClick();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }

        // Função para selecionar as contas que o usuário marcou
        private List<ContaReceber> SelecionarContasSelecionadas()
        {
            var selectedRows = grid.SelectedItems;
            List<ContaReceber> contasSelecionadas = new List<ContaReceber>();

            foreach (var row in selectedRows)
            {
                ContaReceber contaReceber = row as ContaReceber;
                if (contaReceber != null)
                {
                    contasSelecionadas.Add(contaReceber);
                }
            }
            return contasSelecionadas;
        }

        // Função para processar as contas selecionadas
        private async Task ProcessarContasSelecionadas(List<ContaReceber> contasSelecionadas)
        {
            HashSet<int> vendasProcessadas = new HashSet<int>();
            HashSet<int> osProcessadas = new HashSet<int>();

            foreach (ContaReceber contaReceber in contasSelecionadas)
            {
                if (contaReceber.BoletoGerado == false)
                {
                    int vendaId = contaReceber.Venda?.Id ?? 0;
                    int osId = contaReceber.OrdemServico?.Id ?? 0;

                    // Se já foi processada, pular
                    if (vendasProcessadas.Contains(vendaId) || osProcessadas.Contains(osId))
                    {
                        continue;
                    }

                    vendasProcessadas.Add(vendaId);
                    osProcessadas.Add(osId);

                    // Seleciona a conta de boleto caso seja necessário
                    await SelecionarContaBoletoSeNecessario(contaReceber);

                    // Verificar a plataforma de boleto (BB, Sicredi, GalaxyPay)
                    await GerarBoletoPorBanco(contaReceber, vendaId, osId);
                }
                listaReceberBoleto.Add(contaReceber);
            }
        }

        // Função para selecionar a conta de boleto se for necessário
        private async Task SelecionarContaBoletoSeNecessario(ContaReceber contaReceber)
        {
            if (contaReceber.ContaBoleto == null)
            {
                if (contaBoletoSelecionada.Id == 0)
                {
                    selecionarContaEmissaoBoleto();
                    contaBoletoSelecionada = contaSelecionada;
                }

                if (contaBoletoSelecionada != null && contaBoletoSelecionada.Id > 0)
                {
                    contaReceber.ContaBoleto = contaBoletoSelecionada;
                    Controller.getInstance().salvar(contaReceber);
                }
            }
        }

        // Função para gerar boletos por banco
        private async Task GerarBoletoPorBanco(ContaReceber contaReceber, int vendaId, int osId)
        {
            IList<BoletoConfig> listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();

            foreach (BoletoConfig boletoConfig in listaBoletoConfig)
            {
                if (boletoConfig.ContaBancaria.Banco.Descricao.Contains("SICREDI") && contaReceber.ContaBoleto.Id == boletoConfig.ContaBancaria.Id)
                {
                    await GerarBoletoSicredi(contaReceber, vendaId, osId, boletoConfig);
                }
                else if (boletoConfig.ContaBancaria.Banco.Descricao.Contains("BANCO DO BRASIL") && contaReceber.ContaBoleto.Id == boletoConfig.ContaBancaria.Id) 
                {
                    await GerarBoletoBancoBrasil(contaReceber, boletoConfig);
                }
                else if ((boletoConfig.ContaBancaria.Banco.Descricao.Contains("GALAXYPAY") || boletoConfig.ContaBancaria.Banco.Descricao.Contains("CELCASH")) && contaReceber.ContaBoleto.Id == boletoConfig.ContaBancaria.Id)
                {
                    await GerarBoletoGalaxyPay(contaReceber);
                }
            }
        }

        // Função específica para gerar boleto Sicredi
        private async Task GerarBoletoSicredi(ContaReceber contaReceber, int vendaId, int osId, BoletoConfig boletoConfig)
        {
            // Verifique se o boleto já foi gerado
            if (contaReceber.BoletoGerado)
            {
                await imprimirBoletosSicrediAsync(listaReceberBoleto);
            }
            else
            {
                lblCalculando.Text = "Gerando Boleto Sicredi...";
                BoletoSicrediManager boletoManager = new BoletoSicrediManager(vendaId, osId, contaReceber.ContaBoleto, boletoConfig.AmbienteProducao, contaReceber);
                bool sucesso = await boletoManager.GeraBoletosSicredi(contaReceber.Cliente, true);
                if (sucesso)
                {
                    lblCalculando.Text = "Boleto Sicredi Gerado com Sucesso!";
                }
            }
        }

        // Função específica para gerar boleto Banco do Brasil
        private async Task GerarBoletoBancoBrasil(ContaReceber contaReceber, BoletoConfig boletoConfig)
        {
            try
            {
                BBApiService bbApiService = new BBApiService(boletoConfig.AmbienteProducao, boletoConfig.IdToken, boletoConfig.Token);
                RetornoBoletoBB response = await bbApiService.CriarBoletoBancoBrasilAsync(contaReceber.Cliente, contaReceber, boletoConfig);

                if (response != null)
                {
                    GenericaDesktop.ShowInfo("Boleto gerado com sucesso!");
                    contaReceber.BoletoGerado = true;
                    contaReceber.LinhaDigitavel = response.linhaDigitavel;
                    contaReceber.NossoNumero = response.numero;
                    contaReceber.CodigoBarras = response.codigoBarraNumerico;
                  
                    // Salvar no banco de dados
                    Controller.getInstance().salvar(contaReceber);
                    listaReceberBoleto.Add(contaReceber);
                }
            }
            catch (Exception ex)
            {
                // Tratar erro
                MessageBox.Show($"Ocorreu um erro ao gerar boleto BB: {ex.Message}");
            }
        }
        private async Task GerarBoletoGalaxyPay(ContaReceber contaReceber)
        {
            // Inicializando array para boletos GalaxyPay
            if (contaReceber.ContaBoleto.Banco.Descricao == "GALAXYPAY" || contaReceber.ContaBoleto.Banco.Descricao == "CELCASH" || contaReceber.ContaBoleto.Descricao == "CELCASH")
            {
                GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                if (contaReceber.BoletoGerado)
                {
                    listaFaturaGalaxyPay.Add(contaReceber.Id.ToString());
                }
                else if (GenericaDesktop.ShowConfirmacao("Deseja gerar o boleto no CelCash?"))
                {
                    string mensagem = "";
                    bool isValid = GenericaDesktop.ValidarClienteEmissaoBoleto(contaReceber.Cliente, out mensagem);
                    if (isValid)
                    {
                        IList<ContaReceber> listaCrediario = new List<ContaReceber> { contaReceber };
                        GerarBoletoGalaxyPay gerarBoleto = new GerarBoletoGalaxyPay();
                        Pessoa clienteBoleto = contaReceber.Cliente;
                        lblCalculando.Text = "Gerando Boleto CelCash";
                        gerarBoleto.gerarBoletoAvulsoGalaxyPay(listaCrediario, clienteBoleto, contaReceber.ContaBoleto);
                    }
                    else
                    {
                        GenericaDesktop.ShowErro(mensagem);
                    }
                }
                //Imprimir boletos que foram gerados anteriormente
                if (listaFaturaGalaxyPay.Count > 0)
                {
                    lblCalculando.Text = "Obtendo PDF Boleto CelCash";
                    string[] arrayFaturaGalaxyPay = listaFaturaGalaxyPay.ToArray();
                    string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                    string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(arrayFaturaGalaxyPay);
                    if (!String.IsNullOrEmpty(link))
                    {
                        System.Diagnostics.Process.Start(link);
                    }
                }
            }
        }


        private async Task imprimirBoletosBancoBrasilAsync(List<ContaReceber> contasSelecionadas)
        {
            if (contasSelecionadas == null || !contasSelecionadas.Any())
            {
                GenericaDesktop.ShowAlerta("Nenhum boleto disponível para impressão.");
                return;
            }

            var boletosBB = contasSelecionadas
                .Where(c => c.ContaBoleto.Banco.Descricao.Contains("BANCO DO BRASIL") || c.ContaBoleto.Banco.Descricao.Contains("BB"))
                .ToList();

            if (boletosBB.Any())
            {
                FrmImprimirBoletoBB frmImprimirBoletosBB = new FrmImprimirBoletoBB(boletosBB);
                string caminhoPDF = await frmImprimirBoletosBB.ImprimirBoletosAsync();
                FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                frmPDF.Show();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Nenhum boleto do Banco do Brasil disponível para impressão.");
            }
        }


        private async Task imprimirBoletosSicrediAsync(IList<ContaReceber> contasSelecionadas)
        {
            // Verifica se há contas selecionadas
            if (contasSelecionadas == null || !contasSelecionadas.Any())
            {
                GenericaDesktop.ShowAlerta("Nenhuma conta selecionada.");
                return;
            }

            // Filtra as contas que têm boletos gerados
            var contasComBoletosGerados = contasSelecionadas
                .Where(c => c.BoletoGerado)
                .ToList();

            // Verifica se há boletos gerados
            if (!contasComBoletosGerados.Any())
            {
                GenericaDesktop.ShowAlerta("Nenhum boleto disponível para impressão.");
                return;
            }

            BoletoConfig boletoConfig = null;

            // Inicializa a lista para as linhas digitáveis
            List<string> linhasDigitaveisSicredi = new List<string>();

            foreach (var conta in contasComBoletosGerados)
            {
                // Verifica se a conta é do SICREDI
                if (conta.ContaBoleto.Banco.Descricao.Contains("SICREDI"))
                {
                    // Adiciona a linha digitável à lista
                    linhasDigitaveisSicredi.Add(conta.LinhaDigitavel);

                    // Se a configuração do boleto ainda não foi inicializada, faz isso agora
                    if (boletoConfig == null)
                    {
                        boletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancariaUnica(conta.ContaBoleto);
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("A conta selecionada não é do Banco SICREDI.");
                    return;
                }
            }

            // Se houver linhas digitáveis para processar
            if (linhasDigitaveisSicredi.Count > 0)
            {
                BoletoSicrediManager boletoSicrediManager = new BoletoSicrediManager(
                    contasComBoletosGerados.First().Venda?.Id ?? 0, // Pega o ID da primeira conta para evitar repetição
                    contasComBoletosGerados.First().OrdemServico?.Id ?? 0, // Pega o ID da primeira conta para evitar repetição
                    contasComBoletosGerados.First().ContaBoleto,
                    boletoConfig.AmbienteProducao, null
                );

                // Converte a lista de linhas digitáveis para um array
                string[] linhasDigitaveisArray = linhasDigitaveisSicredi.ToArray();

                // Chama o método para baixar os boletos
                await boletoSicrediManager.BaixarBoletosExistentesSicredi(linhasDigitaveisArray, true);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Nenhuma linha digitável disponível para baixar.");
            }
        }

        private async Task imprimirBoletosCelCashGalaxyPayAsync(IList<ContaReceber> contasSelecionadas)
        {
            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
            foreach (ContaReceber cr in contasSelecionadas)
            {
                listaFaturaGalaxyPay.Add(cr.Id.ToString());
            }
            if (listaFaturaGalaxyPay.Count > 0)
            {
                lblCalculando.Text = "Obtendo PDF Boleto CelCash";
                string[] arrayFaturaGalaxyPay = listaFaturaGalaxyPay.ToArray();
                string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(arrayFaturaGalaxyPay);
                if (!String.IsNullOrEmpty(link))
                {
                    System.Diagnostics.Process.Start(link);
                }
            }
        }

        private async Task ImprimirBoletosSelecionadosAsync()
        {
            // Filtrar e imprimir boletos do Banco do Brasil
            var boletosBB = listaReceberBoleto
                .Where(c => c.ContaBoleto.Banco.Descricao.Contains("BANCO DO BRASIL") ||
                             c.ContaBoleto.Banco.Descricao.Contains("BB"))
                .ToList();

            if (boletosBB.Any())
            {
                await imprimirBoletosBancoBrasilAsync(boletosBB);
            }
             var boletosSicredi = listaReceberBoleto
                 .Where(c => c.ContaBoleto.Banco.Descricao.Contains("SICREDI"))
                 .ToList();
            
             if (boletosSicredi.Any())
             {
                 await imprimirBoletosSicrediAsync(boletosSicredi);
             }

            var boletosGalaxtPay = listaReceberBoleto
        .Where(c => c.ContaBoleto.Banco.Descricao.Contains("CELCASH") || c.ContaBoleto.Banco.Descricao.Contains("GALAXY"))
        .ToList();

            if (boletosSicredi.Any())
            {
                await imprimirBoletosCelCashGalaxyPayAsync(boletosGalaxtPay);
            }
        }

    }
}
