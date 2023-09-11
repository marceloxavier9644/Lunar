using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasPagar.Relatorios;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ContasPagar
{
    public partial class FrmContaPagarLista : Form
    {
        ContaPagar contaPagar = new ContaPagar();
        ContaPagarController contaPagarController = new ContaPagarController();
        IList<ContaPagar> listaContaPagar = new List<ContaPagar>();
        IList<ContaPagar> listaContaPagarCalculado = new List<ContaPagar>();
        bool passou = false;
        public FrmContaPagarLista()
        {
            InitializeComponent();
            txtVencimentoInicial.Value = DateTime.Now;
            txtVencimentoFinal.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pesquisarContaPagar()
        {
            try
            {
                grid.AllowEditing = true;
                if (radioPagas.Checked == true)
                    grid.AllowEditing = false;
                listaContaPagar = new List<ContaPagar>();
                listaContaPagarCalculado = new List<ContaPagar>();
                string sql = "From ContaPagar Tabela where Tabela.FlagExcluido <> true ";
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sql = sql + "and Tabela.Pessoa = " + txtCodCliente.Texts + " ";

                if (!String.IsNullOrEmpty(txtNumeroDocumento.Texts))
                    sql = sql + "and Tabela.NumeroDocumento like '%" + txtNumeroDocumento.Texts + "%' ";

                if (radioAbertas.Checked == true)
                    sql = sql + "and Tabela.Pago <> true ";
                else
                    sql = sql + "and Tabela.Pago = true ";

                if (chkAtivarVencimento.Checked == true)
                {
                    DateTime dataIni = DateTime.Parse(txtVencimentoInicial.Value.ToString());
                    DateTime dataFin = DateTime.Parse(txtVencimentoFinal.Value.ToString());
                    String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                    String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                    sql = sql + "and Tabela.DVenc BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                }

                string orderBy = " order by Tabela.DVenc";
                listaContaPagar = contaPagarController.selecionarContaPagarPorSql(sql + orderBy);
                if (listaContaPagar.Count > 0)
                {
                    sfDataPager1.DataSource = listaContaPagar;

                    if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                        sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                    else
                        sfDataPager1.PageSize = 100;
                    grid.DataSource = sfDataPager1.PagedSource;
                    sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                    this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    this.grid.AutoSizeController.Refresh();
                    grid.Refresh();
                    this.grid.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));

                    int w = Screen.PrimaryScreen.Bounds.Width;
                    int h = Screen.PrimaryScreen.Bounds.Height;
                    if (w == 1920 && h == 1080)
                    {
                        this.grid.View.Records.CollectionChanged += Records_CollectionChanged;
                    }
                    else if (w == 1366 && h == 768)
                    {
                        this.grid.View.Records.CollectionChanged += Records_CollectionChanged;
                    }
                    grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    this.grid.Columns["Pessoa.RazaoSocial"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                    grid.AutoSizeController.Refresh();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Nada encontrado :-|");
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }

                //Nao pagar de 2 fornecedores ao mesmo tempo 
                if (String.IsNullOrEmpty(txtCodCliente.Texts) || radioPagas.Checked == true)
                {
                    btnPagar.Enabled = false;
                    btnPagar.BackColor = Color.FromArgb(192, 192, 192);
                    lblCalculando.Text = "Selecione apenas 1 cliente/fornecedor para liberar os botões de pagamento";
                    lblCalculando.Visible = true;
                }
                else
                {
                    btnPagar.Enabled = true;
                    btnPagar.BackColor = Color.FromArgb(31, 30, 68);
                    lblCalculando.Text = "Aguarde, calculado totais...";
                    lblCalculando.Visible = false;
                }
                preencherSumario();
            }
            catch (Exception erro)
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

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaContaPagar.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void preencherSumario()
        {
            // this.grid.SummaryCalculationUnit = Syncfusion.Data.SummaryCalculationUnit.SelectedRows;
            //sumario no grid
            GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Name = "TableSummary";
            tableSummaryRow1.ShowSummaryInRow = true;
            tableSummaryRow1.TitleColumnCount = 3;
            tableSummaryRow1.Title = " Parcelas: {TotalNotas}                 Total {ValorTotal}";
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
            summaryColumn1.MappingName = "NumeroDocumento";

            GridSummaryColumn summaryColumn3 = new GridSummaryColumn();
            summaryColumn3.Name = "ValorTotal";
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
            summaryColumn5.MappingName = "ValorPago";

            if (radioPagas.Checked == true)
            {
                tableSummaryRow1.Title = " Parcelas: {TotalNotas}                 Total {ValorTotal}                 Total Pago {ValorTotalRecebido}";
                tableSummaryRow1.SummaryColumns.Add(summaryColumn5);
            }

            this.grid.TableSummaryRows.Clear();
            tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
            tableSummaryRow1.SummaryColumns.Add(summaryColumn3);
            tableSummaryRow2.SummaryColumns.Add(summaryColumn4);
            this.grid.TableSummaryRows.Add(tableSummaryRow1);
            this.grid.Style.TableSummaryRowStyle.Font = new GridFontInfo(new Font("Microsoft Sans Serif", 12f, FontStyle.Regular));
            this.grid.TableSummaryRows.Add(tableSummaryRow2);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarContaPagar();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as ContaPagar) != null)
                {
                    if ((e.RowData as ContaPagar).DVenc < DateTime.Now && radioAbertas.Checked == true)
                    {
                        e.Style.TextColor = Color.Red;
                    }
                    else if (radioPagas.Checked == true)
                    {
                        e.Style.TextColor = Color.Green;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnNovaFatura_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmNovaFaturaPagar uu = new FrmNovaFaturaPagar())
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
                    switch (uu.showModalNovo(ref listaContaPagar))
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
                        var conta = selectedItem as ContaPagar;
                        if (conta.Pago == false)
                        {
                            Controller.getInstance().excluir(conta);
                            i++;
                        }
                        else
                            GenericaDesktop.ShowAlerta("Não é possível excluir uma conta ja paga!");
                    }
                    if (i > 0)
                    {
                        GenericaDesktop.ShowInfo(i + " Contas a Pagar Excluída(s) com Sucesso!");
                        btnPesquisar.PerformClick();
                    }

                }
                else
                    GenericaDesktop.ShowAlerta("Selecione as contas que deseja excluir!");
            }
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarContaPagar();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Confirma o pagamento das parcelas selecionadas?"))
            {
                IList<ContaReceber> listaReceber = new List<ContaReceber>();
                IList<ContaPagar> listaPagar = new List<ContaPagar>();
                int i = 0;
                bool parcelasPagasSelecionadas = false;
                foreach (var selectedItem in grid.SelectedItems)
                {
                    var conta = selectedItem as ContaPagar;

                    if (conta.Pago == false)
                        listaPagar.Add(conta);
                    else
                    {
                        parcelasPagasSelecionadas = true;
                    }
                }
                if (parcelasPagasSelecionadas == true)
                    GenericaDesktop.ShowAlerta("Atenção: Você selecionou parcelas pagas");
                if (listaPagar.Count > 0)
                {
                    Form formBackground = new Form();
                    OrdemServico ordemServico = new OrdemServico();
                    IList<OrdemServico> listaOs = new List<OrdemServico>();
                    FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "CONTAPAGAR", false, false, listaOs);
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
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";

                    btnPesquisar.PerformClick();
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Nenhuma parcela em aberto selecionada!");
                }
            }
        }

        private void FrmContaPagarLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnPagar.PerformClick();
                    break;
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
                                txtNumeroDocumento.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtNumeroDocumento.Focus();
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
                btnPesquisaCliente.PerformClick();
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
                        pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                        if (pessoa != null)
                        {
                            txtCliente.Texts = pessoa.RazaoSocial;
                            txtCodCliente.Texts = pessoa.Id.ToString();
                            pesquisarContaPagar();
                        }
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Código inválido");
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    listaContaPagar = new List<ContaPagar>();
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();

                }
            }
        }

        private void txtNumeroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisarContaPagar();
            }
        }

        private void chkAtivarVencimento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAtivarVencimento.Checked == true)
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

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            if(listaContaPagar.Count > 0)
            {
                FrmRelatorioContaPagar frm = new FrmRelatorioContaPagar(listaContaPagar);
                frm.ShowDialog();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaPagar",
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

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            var conta = (ContaPagar)grid.SelectedItem;
            txtCliente.Texts = conta.Pessoa.RazaoSocial;
            txtCodCliente.Texts = conta.Pessoa.Id.ToString();
            btnPesquisar.PerformClick();
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
        private void grid_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
            if (e.Column.MappingName == "DVenc")
            {
                try
                {
                    DateTime dataValida = DateTime.Parse(e.NewValue.ToString());
                    e.IsValid = true;
                    contaPagar = new ContaPagar();
                    contaPagar = (ContaPagar)grid.SelectedItem;
                    GenericaDesktop.gravarLinhaLog("Alteração de Vencimento " + contaPagar.DVenc.ToShortDateString() + " para " + dataValida.ToShortDateString() + " Usuario: " + Sessao.usuarioLogado.Login + " - FORNECEDOR: " + contaPagar.Pessoa.RazaoSocial, "ALTERAÇÃO CONTA A PAGAR ID " + contaPagar.Id);
                    contaPagar.DVenc = dataValida;
                    Controller.getInstance().salvar(contaPagar);

                    GenericaDesktop.ShowInfo("Alterado com Sucesso");
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Data Inválida, digite no formato 00/00/0000";

                }
            }
            if (e.Column.MappingName == "ValorTotal")
            {
                try
                {
                    decimal valorFinal = decimal.Parse(e.NewValue.ToString());
                    e.IsValid = true;
                    contaPagar = new ContaPagar();
                    contaPagar = (ContaPagar)grid.SelectedItem;
                    GenericaDesktop.gravarLinhaLog("Alteração de Valor " + contaPagar.ValorTotal.ToString() + " para " + valorFinal.ToString() + " Usuario: " + Sessao.usuarioLogado.Login + " - FORNECEDOR: " + contaPagar.Pessoa.RazaoSocial, "ALTERAÇÃO CONTA A PAGAR ID " + contaPagar.Id);

                    contaPagar.ValorTotal = valorFinal;
                    Controller.getInstance().salvar(contaPagar);
                    GenericaDesktop.ShowInfo("Alterado com Sucesso");
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Valor Inválido";

                }
            }
            if (e.Column.MappingName == "Descricao")
            {
                try
                {
                    string descricao = e.NewValue.ToString();
                    e.IsValid = true;
                    contaPagar = new ContaPagar();
                    contaPagar = (ContaPagar)grid.SelectedItem;
                    GenericaDesktop.gravarLinhaLog("Alteração de Descrição " + contaPagar.Descricao + " para " + descricao + " Usuario: " + Sessao.usuarioLogado.Login + " - FORNECEDOR: " + contaPagar.Pessoa.RazaoSocial, "ALTERAÇÃO CONTA A PAGAR ID " + contaPagar.Id);
                    contaPagar.Descricao = descricao;
                    Controller.getInstance().salvar(contaPagar);

                    GenericaDesktop.ShowInfo("Alterado com Sucesso");
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Descrição Inválida!";

                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (grid.SelectedItems.Count == 1)
            {
                var conta = (ContaPagar)grid.SelectedItem;
                if (conta.Pago == false)
                {
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmNovaFaturaPagar uu = new FrmNovaFaturaPagar(conta))
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
                            switch (uu.showModalNovo(ref listaContaPagar))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    formBackground.Dispose();
                                    btnPesquisar.PerformClick();
                                    txtCliente.Focus();
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
                else
                {
                    GenericaDesktop.ShowAlerta("É Possível Editar Apenas Faturas em Aberto!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione Apenas 1 Fatura para Editar!");
            }
        }
    }
}
