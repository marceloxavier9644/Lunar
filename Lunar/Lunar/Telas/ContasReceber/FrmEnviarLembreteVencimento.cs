using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Fiscal;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.ZAPZAP;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Interactivity;
using Syncfusion.WinForms.DataGrid.Styles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmEnviarLembreteVencimento : Form
    {
        decimal multa = 0;
        decimal juro = 0;
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        IList<ContaReceber> listaContaReceberCalculado = new List<ContaReceber>();
        ContaReceberController contaReceberController = new ContaReceberController();
        DataTable table;

        public FrmEnviarLembreteVencimento()
        {
            InitializeComponent();

            txtVencimentoInicial.Value = DateTime.Now.AddDays(1);
            txtVencimentoFinal.Value = DateTime.Now.AddDays(1);
            txtMensagem.Text = Sessao.parametroSistema.LembreteVencimento;
            this.grid.AllowDeleting = true;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        private void pesquisar()
        {
            try
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Multa))
                    multa = decimal.Parse(Sessao.parametroSistema.Multa);
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Juro))
                    juro = decimal.Parse(Sessao.parametroSistema.Juro);

                listaContaReceber = new List<ContaReceber>();
                listaContaReceberCalculado = new List<ContaReceber>();
                string sql = "From ContaReceber Tabela where Tabela.Concluido = true and Tabela.FlagExcluido <> true ";
                sql = sql + "and Tabela.Recebido <> true ";
                if (chkClienteLembreteVencimento.Checked == true)
                    sql = sql + "and Tabela.Cliente.ReceberLembrete = true ";
                DateTime dataIni = DateTime.Parse(txtVencimentoInicial.Value.ToString());
                DateTime dataFin = DateTime.Parse(txtVencimentoFinal.Value.ToString());
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                sql = sql + "and Tabela.Vencimento BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    sql = sql + "and Tabela.Cliente = " + txtCodCliente.Texts;
                string orderBy = " order by Tabela.Vencimento";
                listaContaReceber = contaReceberController.selecionarContaReceberPorSql(sql + orderBy);
                if (listaContaReceber.Count > 0)
                {
                    calculaTotalNotas();
                    sfDataPager1.DataSource = listaContaReceberCalculado;

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
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Nenhuma parcela encontrada");
                    listaContaReceberCalculado.Clear();
                    listaContaReceber.Clear();
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void calculaTotalNotas()
        {
            try
            {
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
                            int diasVencido = dataX.Days;
                            decimal multaCalculada = receber.ValorParcela * multa / 100;
                            receber.Multa = multaCalculada;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Multa"].MappingName, multaCalculada);
                            decimal juroCalculado = receber.ValorParcela * ((juro / 30) / 100) * diasVencido;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Juro"].MappingName, juroCalculado);
                            receber.Juro = juroCalculado;
                            decimal valorTotalCalculado = receber.ValorParcela + multaCalculada + juroCalculado;
                            // grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, valorTotalCalculado);
                            receber.ValorTotal = valorTotalCalculado;
                        }
                        else
                        {
                            //Se foi pago pega o q esta preenchido
                            receber.ValorTotal = receber.ValorParcela + receber.Multa + receber.Juro;
                        }
                    }
                    if (receber.ValorTotal == 0)
                        receber.ValorTotal = receber.ValorParcela;
                    //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, receber.ValorParcela);
                    i++;
                    listaContaReceberCalculado.Add(receber);
                }
                preencherSumario();
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
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
            tableSummaryRow1.Title = " Parcelas: {TotalNotas}               Total Sem Juros {ValorTotalSemJuro}                 Total Com Juros {ValorTotalComJuro}";
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

            //if (radioPagas.Checked == true)
            //{
            //    tableSummaryRow1.Title = " Parcelas: {TotalNotas}               Total Sem Juros {ValorTotalSemJuro}                 Total Com Juros {ValorTotalComJuro}                 Total Recebido {ValorTotalRecebido}";
            //    tableSummaryRow1.SummaryColumns.Add(summaryColumn5);
            //}

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

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

            if (GenericaDesktop.ShowConfirmacao("Remover as parcelas selecionadas?"))
            {
                if (grid.SelectedIndex >= 0)
                {
                    foreach (var selectedItem in this.grid.SelectedItems)
                    {
                        var index = grid.TableControl.ResolveToRowIndex(selectedItem);
                        deletarItem(index);
                    }
                        
                }
            }
        }

        private void deletarItem(int index)
        {
            grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(index), grid.Columns["Id"].MappingName, 0);

        }

        private void btnTesteMensagem_Click(object sender, EventArgs e)
        {
            LunarChatAPI lunarChatAPI = new LunarChatAPI();
            string numero = "";
            Form formBackground = new Form();
            Nfe nfe = new Nfe();
            using (FrmJustificativa uu = new FrmJustificativa("Número de WhatsApp", "DDD + Numero (Ex. 11988884455)", nfe))
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
                switch (uu.showModal(ref numero))
                {
                    case DialogResult.Ignore:
                        formBackground.Dispose();
                        break;
                    case DialogResult.OK:
                        string primeiroCaract = numero.Substring(0, 2);
                        if (!primeiroCaract.Equals("55"))
                            numero = "55" + GenericaDesktop.RemoveCaracteres(numero.Trim());
                        break;
                }
                formBackground.Dispose();
            }
            if (!String.IsNullOrEmpty(numero))
            {
                String nomeCliente = "Teste";

                string mensagemAjustada = txtMensagem.Text;
                    
                if (mensagemAjustada.Contains("[Cliente]"))
                    mensagemAjustada = mensagemAjustada.Replace("[Cliente]", nomeCliente);

                decimal valorAtualParcela = 120;
                string valorFormat = valorAtualParcela.ToString("C");
                if (mensagemAjustada.Contains("[ValorTotal]"))
                    mensagemAjustada = mensagemAjustada.Replace("[ValorTotal]", valorFormat);

                decimal valorPrincipal = 100;
                string valorPrincFormat = valorPrincipal.ToString("C");
                if (mensagemAjustada.Contains("[Valor]"))
                    mensagemAjustada = mensagemAjustada.Replace("[Valor]", valorPrincFormat);

                decimal valorJuroMulta = 20;
                string valorJuroMultaFormat = valorJuroMulta.ToString("C");
                if (mensagemAjustada.Contains("[ValorJuro]"))
                    mensagemAjustada = mensagemAjustada.Replace("[ValorJuro]", valorJuroMultaFormat);

                DateTime vencimento = DateTime.Now.AddDays(1);
                if (mensagemAjustada.Contains("[Vencimento]"))
                    mensagemAjustada = mensagemAjustada.Replace("[Vencimento]", vencimento.ToShortDateString());

                if (mensagemAjustada.Contains("[Empresa]"))
                    mensagemAjustada = mensagemAjustada.Replace("[Empresa]", Sessao.empresaFilialLogada.NomeFantasia);

                dynamic ret0 = lunarChatAPI.SendMessageAsync(numero, mensagemAjustada);
            }
        }

        private async Task enviarMensagem()
        {
            try
            {
                if (!Directory.Exists(@"TempLogLunar"))
                    Directory.CreateDirectory(@"TempLogLunar");
                string nomeArquivo = @"TempLogLunar\log_EnvioLembrete" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                StreamWriter writer = new StreamWriter(nomeArquivo);
                LunarChatAPI lunarChatAPI = new LunarChatAPI();
                var records = grid.View.Records;
                int sucesso = 0;
                int falha = 0;
                if (Sessao.parametroSistema.LembreteVencimento != txtMensagem.Text) 
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja gravar esta mensagem como lembrete padrão?"))
                    {
                        ParametroSistema parametroSistema = new ParametroSistema();
                        parametroSistema = Sessao.parametroSistema;
                        parametroSistema.LembreteVencimento = txtMensagem.Text;
                        Controller.getInstance().salvar(parametroSistema);
                    }
                 }

                foreach (var record in records)
                {
                    string numero = "";
                    var dataRowView = record.Data as DataRowView;
                    var conta = record.Data as ContaReceber;
                    String nomeCliente = conta.Cliente.RazaoSocial;

                    //Nao passa pelas linhas excluidas
                    if (conta.Id != 0)
                    {
                        string mensagemAjustada = txtMensagem.Text;

                        if (mensagemAjustada.Contains("[Cliente]"))
                            mensagemAjustada = mensagemAjustada.Replace("[Cliente]", nomeCliente);

                        decimal valorAtualParcela = conta.ValorTotal;
                        string valorFormat = valorAtualParcela.ToString("C");
                        if (mensagemAjustada.Contains("[ValorTotal]"))
                            mensagemAjustada = mensagemAjustada.Replace("[ValorTotal]", valorFormat);

                        decimal valorPrincipal = conta.ValorParcela;
                        string valorPrincFormat = valorPrincipal.ToString("C");
                        if (mensagemAjustada.Contains("[Valor]"))
                            mensagemAjustada = mensagemAjustada.Replace("[Valor]", valorPrincFormat);

                        decimal valorJuroMulta = conta.Juro + conta.Multa;
                        string valorJuroMultaFormat = valorJuroMulta.ToString("C");
                        if (mensagemAjustada.Contains("[ValorJuro]"))
                            mensagemAjustada = mensagemAjustada.Replace("[ValorJuro]", valorJuroMultaFormat);

                        DateTime vencimento = conta.Vencimento;
                        if (mensagemAjustada.Contains("[Vencimento]"))
                            mensagemAjustada = mensagemAjustada.Replace("[Vencimento]", vencimento.ToShortDateString());

                        if (mensagemAjustada.Contains("[Empresa]"))
                            mensagemAjustada = mensagemAjustada.Replace("[Empresa]", Sessao.empresaFilialLogada.NomeFantasia);

                        numero = conta.Cliente.PessoaTelefone.Ddd + conta.Cliente.PessoaTelefone.Telefone;
                        string primeiroCaract = numero.Substring(0, 2);
                        if (!primeiroCaract.Equals("55"))
                            numero = "55" + GenericaDesktop.RemoveCaracteres(numero.Trim());
                        else
                            numero = GenericaDesktop.RemoveCaracteres(numero.Trim());

                        string ret0 = await lunarChatAPI.SendMessageAsync(numero, mensagemAjustada);
                        if (!String.IsNullOrEmpty(ret0))
                        {
                            sucesso++;
                            writer.WriteLine("COD. Cliente: " + conta.Cliente.Id.ToString() + " - " + conta.Cliente.RazaoSocial);
                            writer.WriteLine("Fone: " + GenericaDesktop.formatarFone(numero.Replace("55", "")));
                            if (numero.Replace("55", "").Length < 11 || numero.Replace("55", "").Length > 11)
                            {
                                writer.WriteLine("SUCESSO, COM NÚMERO A VERIFICAR, CONFIRME NO APARELHO TELEFÔNICO");
                            }
                            else
                                writer.WriteLine("SUCESSO");
                            writer.WriteLine("");
                        }
                        else
                        {
                            falha++;
                            writer.WriteLine("COD. Cliente: " + conta.Cliente.Id.ToString() + " - " + conta.Cliente.RazaoSocial);
                            writer.WriteLine("Fone: " + GenericaDesktop.formatarFone(numero.Replace("55", "")));
                            writer.WriteLine("FALHA");
                            writer.WriteLine("");
                        }
                    }
                    writer.Close();
                    if (sucesso > 0)
                        GenericaDesktop.ShowInfo(sucesso + " mensagens disparadas com sucesso!");
                    else if (falha > 0)
                        GenericaDesktop.ShowAlerta(falha + " mensagens não foram entregues ao destinatário por falhas");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }
        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            if (e.RowType == RowType.DefaultRow)
            {
                if ((e.RowData as ContaReceber).Id == 0)
                {
                    e.Style.TextColor = Color.Red;
                    e.Style.Font.FontStyle = FontStyle.Strikeout;
                }
                if ((e.RowData as ContaReceber).Cliente.PessoaTelefone is null)
                {
                    e.Style.TextColor = Color.Red;
                    e.Style.Font.FontStyle = FontStyle.Strikeout;
                }
                else
                {
                    if (String.IsNullOrEmpty((e.RowData as ContaReceber).Cliente.PessoaTelefone.Ddd) && String.IsNullOrEmpty((e.RowData as ContaReceber).Cliente.PessoaTelefone.Telefone))
                    {
                        e.Style.TextColor = Color.Red;
                        e.Style.Font.FontStyle = FontStyle.Strikeout;
                    }
                }

            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%'"))
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
                                btnPesquisar.PerformClick();
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

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private async void btnEnviarMensagem_Click(object sender, EventArgs e)
        {
            try
            {
                await enviarMensagem();
            }
            catch (Exception ex)
            {
                // Lida com qualquer exceção que possa ocorrer
                GenericaDesktop.ShowAlerta("Ocorreu um erro ao enviar as mensagens: " + ex.Message);
            }
        }
    }
}
