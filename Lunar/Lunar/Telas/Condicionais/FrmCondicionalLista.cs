﻿using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasReceber;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Lunar.Telas.Condicionais
{
    public partial class FrmCondicionalLista : Form
    {
        Condicional condicional = new Condicional();
        CondicionalController condicionalController = new CondicionalController();
        IList<Condicional> listaCondicional = new List<Condicional>();
        bool passou = false;
        public FrmCondicionalLista()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pesquisarCondicional()
        {
            try
            {
                listaCondicional = new List<Condicional>();
                string sql = "From Condicional Tabela where Tabela.FlagExcluido <> true ";
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts + " ";

                if (!String.IsNullOrEmpty(txtNumeroDocumento.Texts))
                    sql = sql + "and Tabela.Id = " + txtNumeroDocumento.Texts +  " ";

                if (radioAbertas.Checked == true)
                    sql = sql + "and Tabela.Encerrado <> true ";
                else if(radioFaturadas.Checked == true)
                    sql = sql + "and Tabela.Encerrado = true ";

                if (chkAtivarData.Checked == true)
                {
                    DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
                    DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
                    String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                    String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                    sql = sql + "and Tabela.Data BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                }

                string orderBy = " order by Tabela.Data";
                listaCondicional = condicionalController.selecionarCondicionalPorSql(sql + orderBy);
                if (listaCondicional.Count > 0)
                {
                    sfDataPager1.DataSource = listaCondicional;

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
                    this.grid.Columns["Cliente.RazaoSocial"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                    grid.AutoSizeController.Refresh();
                }
                else
                {
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }
                preencherSumario();
            }
            catch
            {

            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaCondicional.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void Records_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.grid.View.Records.Count > 0)
            {
                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                grid.AutoSizeController.Refresh();
            }
        }

        private void preencherSumario()
        {
            // this.grid.SummaryCalculationUnit = Syncfusion.Data.SummaryCalculationUnit.SelectedRows;
            //sumario no grid
            GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Name = "TableSummary";
            tableSummaryRow1.ShowSummaryInRow = true;
            tableSummaryRow1.TitleColumnCount = 3;
            tableSummaryRow1.Title = " Condicionais: {TotalCond}                 Total {ValorTotal}";
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
            summaryColumn1.Name = "TotalCond";
            summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn1.Format = "{Count}";
            summaryColumn1.MappingName = "Id";

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
            summaryColumn5.Name = "ValorTotalFaturado";
            summaryColumn5.SummaryType = SummaryType.DoubleAggregate;
            summaryColumn5.Format = "{Sum:c}";
            summaryColumn5.MappingName = "ValorTotal";

            if (radioFaturadas.Checked == true)
            {
                tableSummaryRow1.Title = " Condicionais: {TotalNotas}                 Total: {ValorTotal}                 Total Faturado: {ValorTotalFaturado}";
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

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarCondicional();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarCondicional();
        }

        private void FrmCondicionalLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnNovo.PerformClick();
                    break;
            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%'"))
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            {
                                txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                txtCliente.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            pesquisarCondicional();
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
                GenericaDesktop generica = new GenericaDesktop();
                PessoaController pessoaController = new PessoaController();
                Pessoa pessoa = new Pessoa();
                if (generica.eNumero(txtCliente.Texts) && txtCliente.Texts.Length <= 5)
                {
                    pessoa.Id = int.Parse(txtCliente.Texts);
                    try
                    {
                        pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                        txtCliente.Texts = pessoa.RazaoSocial;
                        txtCodCliente.Texts = pessoa.Id.ToString();
                        btnPesquisar.PerformClick();
                    }
                    catch
                    {
                        pessoa = null;
                        txtCodCliente.Texts = "";
                        GenericaDesktop.ShowAlerta("Nenhuma pessoa/empresa encontrada");
                        txtCliente.SelectAll();
                    }
                }
                else
                {
                    IList<Pessoa> listaPessoa = pessoaController.selecionarPessoasComVariosFiltros(txtCliente.Texts);
                    if (listaPessoa.Count == 1)
                    {
                        foreach (Pessoa pess in listaPessoa)
                        {
                            pessoa = pess;
                            txtCliente.Texts = pess.RazaoSocial;
                            txtCodCliente.Texts = pess.Id.ToString();
                            btnPesquisar.PerformClick();
                        }
                    }
                    else if (listaPessoa.Count > 1)
                    {
                        btnPesquisaCliente.PerformClick();
                    }
                    else
                        GenericaDesktop.ShowAlerta("Nenhuma pessoa/empresa encontrada");
                }
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
                            pesquisarCondicional();
                        }
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Código inválido");
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                    listaCondicional = new List<Condicional>();
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
                txtCliente.Texts = "";
                txtCodCliente.Texts = "";
                chkAtivarData.Checked = false;
                radioTodas.Checked = true;
                pesquisarCondicional();
            }
        }

        private void chkAtivarData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAtivarData.Checked == true)
                {
                    txtDataInicial.Enabled = true;
                    txtDataFinal.Enabled = true;
                }
                else
                {
                    txtDataInicial.Enabled = false;
                    txtDataFinal.Enabled = false;
                }
            }
            catch
            {

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
            chkAtivarData.Checked = false;
            radioAbertas.Checked = true;
            txtNumeroDocumento.Texts = "";
            txtRegistroPorPagina.Texts = "100";
            txtCliente.Focus();
        }

        private void txtCliente_Click(object sender, EventArgs e)
        {
            txtCliente.SelectAll();
        }

        private void FrmCondicionalLista_Load(object sender, EventArgs e)
        {
            if (passou == false)
            {
                pesquisarCondicional();
                passou = true;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmCondicional uu = new FrmCondicional())
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    pesquisarCondicional();
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

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {

            if (grid.SelectedIndex >= 0)
            {
                condicional = new Condicional();
                condicional = (Condicional)grid.SelectedItem;
                editarCadastro(condicional);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha da condicional que deseja editar!");
        }
        private void editarCadastro(Condicional cond)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmCondicional uu = new FrmCondicional(cond))
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    pesquisarCondicional();
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

        private void radioAbertas_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                pesquisarCondicional();
            }
            catch
            {

            }
        }

        private void radioFaturadas_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                pesquisarCondicional();
            }
            catch
            {

            }
        }

        private void radioTodas_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                pesquisarCondicional();
            }
            catch
            {

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir esta condicional?"))
                {
                    condicional = new Condicional();
                    condicional = (Condicional)grid.SelectedItem;
                    //Imprimir Ticket
                    CondicionalController.getInstance().excluir(condicional);
                    GenericaDesktop.ShowInfo("Condicional Excluída com Sucesso!");
                }
            }
            catch(Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
            }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                condicional = new Condicional();
                condicional = (Condicional)grid.SelectedItem;
                Form formBackground = new Form();
                try
                {
                    using (FrmDevolucaoCondicional uu = new FrmDevolucaoCondicional(condicional))
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
                        uu.ShowDialog();
                        formBackground.Dispose();
                        pesquisarCondicional();
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                condicional = new Condicional();
                condicional = (Condicional)grid.SelectedItem;
                //Imprimir Ticket
                FrmImprimirCondicional frmImprimirTicket = new FrmImprimirCondicional(condicional);
                frmImprimirTicket.ShowDialog();
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro(err.Message);
            }
        }

        private void grid_Click(object sender, EventArgs e)
        {

        }
    }
}
