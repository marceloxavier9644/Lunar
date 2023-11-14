using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasReceber.Reports;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.RelatoriosDiversos;
using Lunar.Utils;
using Lunar.Utils.GalaxyPay_API;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Lunar.Utils.GalaxyPay_API.GalaxyPay_RetornoStatusBoletos;
using Exception = System.Exception;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmContaReceberLista : Form
    {
        ContaReceber contaReceber = new ContaReceber();
        ContaReceberController ContaReceberController = new ContaReceberController();
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        IList<ContaReceber> listaContaReceberCalculado = new List<ContaReceber>();
        bool passou = false;
        decimal multa = 0;
        decimal juro = 0;
        decimal diasVencido = 0;
        GenericaDesktop generica = new GenericaDesktop();
        public FrmContaReceberLista()
        {
            InitializeComponent();
            txtVencimentoInicial.Value = DateTime.Now;
            txtVencimentoFinal.Value = DateTime.Now;
            if(Sessao.parametroSistema.IntegracaoGalaxyPay == true)
            {
                btnRetornoBoletos.Enabled = true;
            }
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
                //MessageBox.Show(sql + orderBy);
                listaContaReceber = ContaReceberController.selecionarContaReceberPorSqlNativo(sql + orderBy);
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

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
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
                            if(((Pessoa)pessoaOjeto).RegistradoSpc == true)
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
                if (grid.SelectedItems.Count > 0)
                {
                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var conta = selectedItem as ContaReceber;
                        if (conta.Recebido == false)
                        {
                            Controller.getInstance().excluir(conta);
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

        private void btnAbater_Click(object sender, EventArgs e)
        {
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
                        try
                        {
                            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                            GalaxyPay_RetornoStatus retGalaxy = galaxyPayApiIntegracao.GalaxyPay_ListarTransacoes(dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"));
                            if (retGalaxy != null)
                            {
                                if (retGalaxy.Transactions != null)
                                {
                                    if (retGalaxy.Transactions.Length > 0)
                                    {
                                        IList<ContaReceber> listaReceberRec = new List<ContaReceber>();
                                        for (int x = 0; x < retGalaxy.Transactions.Length; x++)
                                        {
                                            if (retGalaxy.Transactions[x].chargeMyId != null)
                                            {
                                                ContaReceber contaReceber = new ContaReceber();
                                                contaReceber.Id = int.Parse(retGalaxy.Transactions[x].chargeMyId);
                                                contaReceber = (ContaReceber)Controller.getInstance().selecionar(contaReceber);
                                                listaReceberRec.Add(contaReceber);
                                            }
                                            listaContaReceber = listaReceberRec;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            GenericaDesktop.ShowErro("Data Inválida");
                        }
                        break;
                }
                formBackground.Dispose();
                //OpenChildForm(() => new FrmComissaoRelatorio01(), btnRelatorios);
            }
        }

        private void btnImprimirBoleto_Click(object sender, EventArgs e)
        {
            if (grid.SelectedItems.Count == 1)
            {
                contaReceber = new ContaReceber();
                contaReceber = (ContaReceber)grid.SelectedItem;
                if (contaReceber.BoletoGerado == true)
                {
                    GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                    string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                    string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFUnico(contaReceber);
                    if (!String.IsNullOrEmpty(link))
                        System.Diagnostics.Process.Start(link);
                }
                else
                    GenericaDesktop.ShowAlerta("Fatura Não Possui Boleto Gerado");
            }
            else if (grid.SelectedItems.Count > 1)
            {
                string[] arrayFatura = new string[grid.SelectedItems.Count];
                int i = 0;
                foreach (var selectedItem in grid.SelectedItems)
                {
                    var conta = selectedItem as ContaReceber;
                    if(conta.BoletoGerado == true)
                    {
                        arrayFatura[i] = conta.Id.ToString();
                        i++;
                    }
                }
                GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
                string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();
                string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(arrayFatura);
                if (!String.IsNullOrEmpty(link))
                    System.Diagnostics.Process.Start(link);
            }

        }
    }
}
