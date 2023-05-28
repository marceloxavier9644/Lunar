using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.ContasReceber;
using Lunar.Telas.ContasReceber.Reports;
using Lunar.Telas.FormaPagamentoRecebimento.Adicionais;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.RecebimentoVendas;
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

namespace Lunar.Telas.FormaPagamentoRecebimento
{
    public partial class FrmPagamentoRecebimento : Form
    {
        decimal valorTotal = 0;
        decimal valorFaltante = 0;
        decimal valorRecebido = 0;
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        IList<ContaPagar> listaPagar = new List<ContaPagar>();
        IList<Cheque> listaCheque = new List<Cheque>();
        IList<ContaReceber> listaCrediario = new List<ContaReceber>();
        Pessoa clienteLista = new Pessoa();
        decimal valorOriginal = 0;
        decimal valorMulta = 0;
        decimal valorJuro = 0;
        decimal descontoRecebido = 0;
        decimal acrescimoRecebido = 0;
        OrdemServico ordemServico = new OrdemServico();
        string origem = "";
        decimal descontoPercentual = 0;
        bool parcial = false;
        Pessoa pessoaCobrador = new Pessoa();
        public FrmPagamentoRecebimento(IList<ContaReceber> listaReceber, IList<ContaPagar> listaPagar, OrdemServico ordemServico, string origem, bool parcial, bool ativarCrediario)
        {
            InitializeComponent();
            this.parcial = parcial;
            this.gridRecebimento.DataSource = dsRecebimento.Tables["Recebimento"];
            lblFormaPagamento.TextAlign = HorizontalAlignment.Center;
            rjTextBox1.TextAlign = HorizontalAlignment.Center;
            rjTextBox2.TextAlign = HorizontalAlignment.Center;
            this.origem = origem;
            if (listaReceber.Count > 0)
            {
                this.listaReceber = listaReceber;
                ajustarListaReceber();
                if (parcial == true)
                    btnDesconto.Enabled = false;
            }
            if (listaPagar.Count > 0)
            {
                this.listaPagar = listaPagar;
                ajustarListaPagar();
            }
            this.ordemServico = ordemServico;
            if (ordemServico.Id > 0)
                ajustarOrdemServico();

            if (ativarCrediario == true)
            {
                btnCrediario.Enabled = true;
                iconeCrediario.Enabled = true;
            }
            else
            {
                btnCrediario.Enabled = false;
                iconeCrediario.Enabled = false;
            }
        }

        public FrmPagamentoRecebimento(Nfe nfe, string origem)
        {
            InitializeComponent();
            this.gridRecebimento.DataSource = dsRecebimento.Tables["Recebimento"];
            lblFormaPagamento.TextAlign = HorizontalAlignment.Center;
            rjTextBox1.TextAlign = HorizontalAlignment.Center;
            rjTextBox2.TextAlign = HorizontalAlignment.Center;
            this.origem = origem;
            //if (nfe.Id > 0)
            //    ajustarNfe(nfe);

        }


        private void ajustarOrdemServico()
        {
            if (origem != "ORDEMSERVICO_SINAL")
            {
                System.Data.DataRow row = dsOrdemServico.Tables[0].NewRow();
                row.SetField("Id", ordemServico.Id.ToString());
                row.SetField("Documento", ordemServico.Id);
                row.SetField("Parcela", 1);
                decimal valorUnitForm = ordemServico.ValorTotal;
                row.SetField("ValorParcela", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Multa", 0);
                decimal valorTotal = ordemServico.ValorTotal;
                row.SetField("Juro", string.Format("{0:0.00}", 0));
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("FormaPagamento", string.Format("{0:0.00}", ""));
                row.SetField("Cliente", string.Format("{0:0.00}", ordemServico.Cliente.RazaoSocial));
                dsOrdemServico.Tables[0].Rows.Add(row);
                gridFaturas.DataSource = this.dsOrdemServico;

                valorTotal = 0;
                valorFaltante = 0;
                valorRecebido = 0;

                valorTotal = ordemServico.ValorTotal;
                clienteLista = ordemServico.Cliente;

                valorFaltante = valorTotal;
                if (valorFaltante == 0)
                    btnFinalizar.Enabled = true;
                valorTotal = Math.Round(valorTotal, 2);
                lblValorTotal.Text = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
                lblDesconto.Text = 0.ToString("C2", CultureInfo.CurrentCulture);
                lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
                lblValorRecebido.Text = valorRecebido.ToString("C2", CultureInfo.CurrentCulture);
            }
            else if (origem.Equals("ORDEMSERVICO_SINAL"))
            {
                System.Data.DataRow row = dsOrdemServico.Tables[0].NewRow();
                row.SetField("Id", ordemServico.Id.ToString());
                row.SetField("Documento", ordemServico.Id);
                row.SetField("Parcela", 1);
                decimal valorUnitForm = Sessao.valorSinalOrdemServico;
                row.SetField("ValorParcela", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Multa", 0);
                decimal valorTotal = Sessao.valorSinalOrdemServico;
                row.SetField("Juro", string.Format("{0:0.00}", 0));
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("FormaPagamento", string.Format("{0:0.00}", ""));
                row.SetField("Cliente", string.Format("{0:0.00}", ordemServico.Cliente.RazaoSocial));
                dsOrdemServico.Tables[0].Rows.Add(row);
                gridFaturas.DataSource = this.dsOrdemServico;

                //valorTotal = 0;
                valorFaltante = 0;
                valorRecebido = 0;


                clienteLista = ordemServico.Cliente;

                valorFaltante = valorTotal;
                valorTotal = Math.Round(valorTotal, 2);
                lblValorTotal.Text = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
                lblDesconto.Text = 0.ToString("C2", CultureInfo.CurrentCulture);
                lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
                lblValorRecebido.Text = valorRecebido.ToString("C2", CultureInfo.CurrentCulture);
            }
            verificarCreditoClienteNaAbertura();
        }
        private void ajustarListaReceber()
        {
            btnSelecionarCobrador.Visible = true;
            decimal valorTotalFormat = 0;
            decimal totalFormatado = 0;
            foreach (ContaReceber contaReceber1 in listaReceber)
            {
                System.Data.DataRow row = dsFatura.Tables[0].NewRow();
                row.SetField("Id", contaReceber1.Id.ToString());
                row.SetField("Documento", contaReceber1.Documento);
                row.SetField("Parcela", contaReceber1.Parcela);
                decimal valorUnitForm = contaReceber1.ValorParcela;
                valorUnitForm = Math.Round(valorUnitForm, 2);
                row.SetField("ValorParcela", string.Format("{0:0.00}", valorUnitForm));
                decimal valorMultaFormat = contaReceber1.Multa;
                valorMultaFormat = Math.Round(valorMultaFormat, 2);
                row.SetField("Multa", string.Format("{0:0.00}", valorMultaFormat));
                decimal valorJuroFormat = contaReceber1.Juro;
                valorJuroFormat = Math.Round(valorJuroFormat, 2);
                row.SetField("Juro", string.Format("{0:0.00}", valorJuroFormat));
                if(parcial == false)
                    valorTotalFormat = valorUnitForm + valorJuroFormat + valorMultaFormat -(contaReceber1.ValorRecebimentoParcial);
                else
                    valorTotalFormat = valorUnitForm + valorJuroFormat + valorMultaFormat;
                valorTotalFormat = Math.Round(valorTotalFormat, 2);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotalFormat));
                row.SetField("FormaPagamento", contaReceber1.FormaPagamento.Descricao);
                row.SetField("Cliente", contaReceber1.Cliente.RazaoSocial);
                row.SetField("DescontoAcrescimo", "");
                dsFatura.Tables[0].Rows.Add(row);
                clienteLista = contaReceber1.Cliente;
                totalFormatado = totalFormatado + valorTotalFormat;
            }
            totalFormatado = Math.Round(totalFormatado, 2);
            gridFaturas.DataSource = dsFatura;
            valorTotal = totalFormatado;
            valorFaltante = 0;
            valorRecebido = 0;
            if (ordemServico.Id > 0)
                valorTotal = ordemServico.ValorTotal;

            valorFaltante = valorTotal;
            valorTotal = Math.Round(valorTotal, 2);
            lblValorTotal.Text = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
            lblDesconto.Text = 0.ToString("C2", CultureInfo.CurrentCulture);
            lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            lblValorRecebido.Text = valorRecebido.ToString("C2", CultureInfo.CurrentCulture);
        }

        private void ajustarListaPagar()
        {
            btnCreditoCliente.Enabled = false;
            iconeCredito.Enabled = false;
            btnCheque.Enabled = false;
            iconeCheque.Enabled = false;
            decimal totalFormatado = 0;
            foreach (ContaPagar contaPagar in listaPagar)
            {
                System.Data.DataRow row = dsFatura.Tables[0].NewRow();
                row.SetField("Id", contaPagar.Id.ToString());
                row.SetField("Documento", contaPagar.NumeroDocumento);
                row.SetField("Parcela", contaPagar.NDup);
                decimal valor = contaPagar.ValorTotal;
                valor = Math.Round(valor, 2);
                row.SetField("ValorParcela", string.Format("{0:0.00}", valor));
                row.SetField("Multa", string.Format("{0:0.00}", 0));
                row.SetField("Juro", string.Format("{0:0.00}", 0));
                row.SetField("ValorTotal", string.Format("{0:0.00}", valor));
                row.SetField("FormaPagamento", contaPagar.FormaPagamento.Descricao);
                row.SetField("Cliente", contaPagar.Pessoa.RazaoSocial);
                row.SetField("DescontoAcrescimo", "");
                dsFatura.Tables[0].Rows.Add(row);
                clienteLista = contaPagar.Pessoa;
                totalFormatado = totalFormatado + valor;
            }
            totalFormatado = Math.Round(totalFormatado, 2);
            gridFaturas.DataSource = dsFatura;
            valorTotal = totalFormatado;
            valorFaltante = 0;
            valorRecebido = 0;
            if (ordemServico.Id > 0)
                valorTotal = ordemServico.ValorTotal;

            valorFaltante = valorTotal;
            valorTotal = Math.Round(valorTotal, 2);
            lblValorTotal.Text = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
            lblDesconto.Text = 0.ToString("C2", CultureInfo.CurrentCulture);
            lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            lblValorRecebido.Text = valorRecebido.ToString("C2", CultureInfo.CurrentCulture);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridPagamento_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnDinheiro_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDinheiro();
        }

        private void abrirFormPagamentoDinheiro()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                if (ordemServico.Id > 0 && origem != "ORDEMSERVICO_SINAL")
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                object vendaFormaPagamento = new VendaFormaPagamento();
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmDinheiro uu = new FrmDinheiro(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, ordemServico, listaPagar))
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
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                //MessageBox.Show("Forma Pagamento: " + formaPagamento.Descricao + " Valor: R$ " + valorRecebido);
                                inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }

        private void abrirFormPagamentoCartao()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmCartao uu = new FrmCartao(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, ordemServico))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //  formBackground.Left = Top = 10;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        BandeiraCartao bandeiraCartao = new BandeiraCartao();
                        Parcelamento parcelamento = new Parcelamento();
                        String autorizacao = "";
                        String tipoCartao = "";
                        AdquirenteCartao adquirente = new AdquirenteCartao();
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido, ref bandeiraCartao, ref parcelamento, ref autorizacao, ref adquirente, ref tipoCartao))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                //MessageBox.Show("Forma Pagamento: " + formaPagamento.Descricao + " Valor: R$ " + valorRecebido);
                                inserirFormaPagamentoGrid(formaPagamento, valorRecebido, bandeiraCartao, parcelamento, autorizacao, adquirente, null, DateTime.Parse("1900-01-01 00:00:00"), tipoCartao);
                                uu.Dispose();
                                formBackground.Dispose();
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }

        private void abrirFormPagamentoPix()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmPix uu = new FrmPix(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, ordemServico))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //  formBackground.Left = Top = 10;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        ContaBancaria contaBancaria = new ContaBancaria();
                        DateTime dataPix = new DateTime();
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido, ref contaBancaria, ref dataPix))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, contaBancaria, dataPix, "");
                                uu.Dispose();
                                formBackground.Dispose();
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }

        private void abrirFormPagamentoCheque()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmCheque uu = new FrmCheque(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, ordemServico))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //  formBackground.Left = Top = 10;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido, ref listaCheque))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
                                uu.Dispose();
                                formBackground.Dispose();
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }

        private void abrirFormPagamentoDepositoTransferencia()
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmDeposito uu = new FrmDeposito(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, ordemServico))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //  formBackground.Left = Top = 10;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        ContaBancaria contaBancaria = new ContaBancaria();
                        DateTime dataDeposito = new DateTime();
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido, ref contaBancaria, ref dataDeposito))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, contaBancaria, dataDeposito, "");
                                uu.Dispose();
                                formBackground.Dispose();
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }

        private void abrirFormCreditoCliente(decimal valorDisponivel)
        {
            if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
            {
                Sessao.vendasRecebimento_ValorRecebido = 0;
                Form formBackground = new Form();
                try
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    decimal valorRecebido = 0;
                    using (FrmCreditoCliente uu = new FrmCreditoCliente(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), listaReceber, valorDisponivel, ordemServico))
                    {
                        formBackground.StartPosition = FormStartPosition.Manual;
                        //formBackground.FormBorderStyle = FormBorderStyle.None;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        //  formBackground.Left = Top = 10;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                bool invalidar = false;
                                var records = gridRecebimento.View.Records;
                                foreach (var record in records)
                                {
                                    FormaPagamento form = new FormaPagamento();
                                    Caixa caixa = new Caixa();
                                    var dataRowView = record.Data as DataRowView;
                                    form = new FormaPagamento();
                                    form.Id = int.Parse(dataRowView.Row["Id"].ToString());
                                    form = (FormaPagamento)Controller.getInstance().selecionar(form);
                                    if (form.CreditoCliente == true)
                                        invalidar = true;
                                }
                                if (invalidar == false)
                                    inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
                                else
                                    GenericaDesktop.ShowErro("Já foi incluso crédito do cliente, para alterar o valor delete o crédito inserido primeiro!");
                                uu.Dispose();
                                formBackground.Dispose();
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
                GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
            }
        }
        private void inserirFormaPagamentoGrid(FormaPagamento formaPagamento, decimal valorRecebido, BandeiraCartao bandeiraCartao, Parcelamento parcelamento, string autorizacaoCartao, AdquirenteCartao adquirente, ContaBancaria contaBancaria, DateTime dataPix, String tipoCartao)
        {
            int idBandeira = 0;
            string bandeira = ""; ;
            int idParcela = 0;
            string parcela = "";
            string adqui = "";
            int idConta = 0;
            String dataPixRealizado = "";

            if (bandeiraCartao != null)
            {
                idBandeira = bandeiraCartao.Id;
                bandeira = bandeiraCartao.Descricao;
            }
            if (parcelamento != null)
            {
                idParcela = parcelamento.Id;
                parcela = parcelamento.Descricao;
            }
            if (adquirente != null)
                adqui = adquirente.Id.ToString();
            if (contaBancaria != null)
            {
                idConta = contaBancaria.Id;
            }
            if (dataPix != null && !dataPix.ToShortDateString().Equals("01/01/1900"))
                dataPixRealizado = dataPix.ToShortDateString();

            System.Data.DataRow row = dsRecebimento.Tables[0].NewRow();
            row.SetField("Id", formaPagamento.Id.ToString());
            row.SetField("FormaPagamento", formaPagamento.Descricao);
            row.SetField("Valor", string.Format("{0:0.00}", valorRecebido));
            row.SetField("IdBandeira", idBandeira);
            row.SetField("Bandeira", bandeira);
            row.SetField("IdParcelamento", idParcela);
            row.SetField("Parcelamento", parcela);
            row.SetField("AutorizacaoCartao", autorizacaoCartao);
            row.SetField("Adquirente", adqui);
            row.SetField("ContaBancaria", idConta);
            row.SetField("DataPix", dataPixRealizado);
            row.SetField("TipoCartao", tipoCartao);
            dsRecebimento.Tables[0].Rows.Add(row);

            calcularTotais();
        }

        private void calcularTotais()
        {
            if (gridRecebimento.RowCount > 0)
            {
                decimal somaValorRecebido = 0;
                var records = gridRecebimento.View.Records;
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    somaValorRecebido = somaValorRecebido + decimal.Parse(dataRowView.Row["Valor"].ToString());
                }
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                valorFaltante = ((valorTotal - somaValorRecebido) - descontoRecebido + acrescimoRecebido);

                if (descontoRecebido > 0)
                    lblDesconto.Text = descontoRecebido.ToString("C2", CultureInfo.CurrentCulture);
                else if (acrescimoRecebido > 0)
                    lblDesconto.Text = acrescimoRecebido.ToString("C2", CultureInfo.CurrentCulture);
                else
                    lblDesconto.Text = 0.ToString("C2", CultureInfo.CurrentCulture);
                valorFaltante = Math.Round(valorFaltante, 2);
                lblValorFaltante.Text = valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
                lblValorRecebido.Text = somaValorRecebido.ToString("C2", CultureInfo.CurrentCulture);
                if (somaValorRecebido > 0) btnDesconto.Enabled = false; else btnDesconto.Enabled = true;
                if (valorFaltante == 0)
                {
                    btnFinalizar.Enabled = true;
                }
                else
                {
                    btnFinalizar.Enabled = false;
                }
            }
        }

        private void btnExcluirRecebimento_Click(object sender, EventArgs e)
        {
            if (gridRecebimento.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o recebimento selecionado?"))
                {
                    dsRecebimento.Tables[0].Rows[gridRecebimento.SelectedIndex].Delete();
                    calcularTotais();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione o recebimento que deseja excluir para depois clicar no botão de excluir");
        }

        private void FrmPagamentoRecebimento_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnFinalizar.PerformClick();
                    break;

                case Keys.F6:
                    abrirFormPagamentoDinheiro();
                    break;
                case Keys.F7:
                    abrirFormPagamentoCartao();
                    break;
                case Keys.F8:
                    abrirFormPagamentoPix();
                    break;
                case Keys.F12:
                    abrirFormPagamentoCheque();
                    break;
                case Keys.F9:
                    abrirFormPagamentoDepositoTransferencia();
                    break;
                case Keys.F3:
                    verificarCreditoCliente();
                    break;
            }
        }

        private void IconeDinheiro_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDinheiro();
        }

        private void btnCartaoCredito_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCartao();
        }

        private void iconeCartao_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCartao();
        }

        private void btnPIX_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoPix();
        }

        private void iconePix_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoPix();
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCheque();
        }

        private void iconeCheque_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCheque();
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDepositoTransferencia();
        }

        private void iconeDeposito_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoDepositoTransferencia();
        }

        private void btnCreditoCliente_Click(object sender, EventArgs e)
        {
            verificarCreditoCliente();
        }

        private void verificarCreditoCliente()
        {
            if (ordemServico.Id > 0)
                valorTotal = ordemServico.ValorTotal;
            else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                valorTotal = Sessao.valorSinalOrdemServico;
            CreditoClienteController creditoClienteController = new CreditoClienteController();
            if (GenericaDesktop.ShowConfirmacao("O cliente é " + clienteLista.RazaoSocial + " CPF/CNPJ: " + clienteLista.Cnpj + "?"))
            {

                IList<CreditoCliente> listaCredito = creditoClienteController.selecionarCreditoPorCliente(clienteLista.Id);
                if (listaCredito.Count > 0)
                {
                    decimal valorDisponivel = 0;
                    foreach (CreditoCliente credito in listaCredito)
                    {
                        valorDisponivel = valorDisponivel + (credito.Valor - credito.ValorUtilizado);
                    }
                    if (valorDisponivel > 0)
                    {
                        if (GenericaDesktop.ShowConfirmacao("Cliente " + clienteLista.RazaoSocial + " possui " + valorDisponivel.ToString("C2", CultureInfo.CurrentCulture) + " deseja utilizar o crédito total?"))
                        {
                            bool invalidar = false;
                            var records = gridRecebimento.View.Records;
                            foreach (var record in records)
                            {
                                FormaPagamento formaPagamento = new FormaPagamento();
                                Caixa caixa = new Caixa();
                                var dataRowView = record.Data as DataRowView;
                                formaPagamento = new FormaPagamento();
                                formaPagamento.Id = int.Parse(dataRowView.Row["Id"].ToString());
                                formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                                if (formaPagamento.CreditoCliente == true)
                                    invalidar = true;
                            }
                            if (invalidar == true)
                                GenericaDesktop.ShowErro("Já foi incluso crédito do cliente, para alterar o valor delete o crédito inserido primeiro!");
                            else
                            {
                                FormaPagamento fp = new FormaPagamento();
                                fp.Id = 8;
                                fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                                inserirFormaPagamentoGrid(fp, valorDisponivel, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
                            }
                        }
                        else
                        {
                            abrirFormCreditoCliente(valorDisponivel);
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Cliente não possui crédito disponível");
                }
            }
            else
            {
                selecionarCliente();
                ////if(clienteLista != null)
                ////    abrirFormCreditoCliente();
            }
        }

        private void verificarCreditoClienteNaAbertura()
        {
            if (origem != "ORDEMSERVICO_SINAL")
            {
                if (ordemServico.Id > 0)
                    valorTotal = ordemServico.ValorTotal;
                else if (ordemServico.Id > 0 && origem == "ORDEMSERVICO_SINAL")
                    valorTotal = Sessao.valorSinalOrdemServico;
                CreditoClienteController creditoClienteController = new CreditoClienteController();
                IList<CreditoCliente> listaCredito = creditoClienteController.selecionarCreditoPorCliente(clienteLista.Id);
                if (listaCredito.Count > 0)
                {
                    decimal valorDisponivel = 0;
                    foreach (CreditoCliente credito in listaCredito)
                    {
                        valorDisponivel = valorDisponivel + (credito.Valor - credito.ValorUtilizado);
                    }
                    if (valorDisponivel > 0)
                    {
                        if (GenericaDesktop.ShowConfirmacao("Cliente " + clienteLista.RazaoSocial + " possui " + valorDisponivel.ToString("C2", CultureInfo.CurrentCulture) + " deseja utilizar o crédito total?"))
                        {
                            FormaPagamento fp = new FormaPagamento();
                            fp.Id = 8;
                            fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                            inserirFormaPagamentoGrid(fp, valorDisponivel, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
                            //btnCreditoCliente.Enabled = false;
                            //iconeCredito.Enabled = false;
                        }
                        else
                        {
                            abrirFormCreditoCliente(valorDisponivel);
                        }
                    }
                }
               
            }
        }

        private void selecionarCliente()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", ""))
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            {
                                clienteLista = ((Pessoa)pessoaOjeto);
                                verificarCreditoCliente();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            clienteLista = ((Pessoa)pessoaOjeto);
                            verificarCreditoCliente();
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


        private void iconeCredito_Click(object sender, EventArgs e)
        {
            verificarCreditoCliente();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if(listaReceber.Count > 0)
                concluirRecebimentoContaReceber();
            if (ordemServico.Id > 0 && origem != "ORDEMSERVICO_SINAL")
                concluirRecebimentoOrdemServico();
            if(ordemServico.Id > 0 && origem.Equals("ORDEMSERVICO_SINAL"))
                concluirRecebimentoSinalOs();
            if (listaPagar.Count > 0)
                concluirRecebimentoContaPagar();
        }

        private void concluirRecebimentoSinalOs()
        {
            try
            {
                IList<OrdemServicoPagamento> listaOrdemServicoPagamento = new List<OrdemServicoPagamento>();
                FormaPagamento formaPagamento = new FormaPagamento();
                //OrdemServicoPagamento ordemServicoPagamento = new OrdemServicoPagamento();
                decimal valorRecebido = 0;

                var records = gridRecebimento.View.Records;
                foreach (var record in records)
                {
                   // ordemServicoPagamento = new OrdemServicoPagamento();
                    Caixa caixa = new Caixa();
                    var dataRowView = record.Data as DataRowView;
                    formaPagamento = new FormaPagamento();
                    formaPagamento.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                    if (formaPagamento.Id > 0)
                    {
                        //ordemServicoPagamento.DataRecebimento = DateTime.Now;
                        //ordemServicoPagamento.FormaPagamento = formaPagamento;
                        //ordemServicoPagamento.OrdemServico = ordemServico;
                        //ordemServicoPagamento.Troco = 0;
                        //ordemServicoPagamento.ValorRecebido = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        valorRecebido = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        //ordemServicoPagamento.AdquirenteCartao = null;
                        //ordemServicoPagamento.AutorizacaoCartao = "";
                        //ordemServicoPagamento.BandeiraCartao = null;
                        //ordemServicoPagamento.Cartao = false;
                        //ordemServicoPagamento.ParcelamentoCartao = null;
                        //ordemServicoPagamento.Parcelas = "";
                        //ordemServicoPagamento.TipoCartao = "";
                        caixa.IdOrigem = ordemServico.Id.ToString();

                        //Dinheiro
                        if (formaPagamento.Id == 1)
                        {
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);
                        }
                        //Cartão
                        else if (formaPagamento.Id == 2)
                        {
                            BandeiraCartao bandeiraCartao = new BandeiraCartao();
                            bandeiraCartao.Id = int.Parse(dataRowView.Row["IdBandeira"].ToString());
                            bandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);

                            Parcelamento parcelamento = new Parcelamento();
                            parcelamento.Id = int.Parse(dataRowView.Row["IdParcelamento"].ToString());
                            parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);

                            AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                            adquirenteCartao.Id = int.Parse(dataRowView.Row["Adquirente"].ToString());
                            adquirenteCartao = (AdquirenteCartao)Controller.getInstance().selecionar(adquirenteCartao);
                            decimal valor = decimal.Parse(dataRowView.Row["Valor"].ToString());

                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);
                            //ordemServicoPagamento.AdquirenteCartao = adquirenteCartao;
                            //ordemServicoPagamento.AutorizacaoCartao = dataRowView.Row["AutorizacaoCartao"].ToString();
                            //ordemServicoPagamento.BandeiraCartao = bandeiraCartao;
                            //ordemServicoPagamento.Cartao = true;
                            //ordemServicoPagamento.ParcelamentoCartao = parcelamento;
                            //ordemServicoPagamento.Parcelas = parcelamento.Parcelas.ToString();
                            //ordemServicoPagamento.TipoCartao = dataRowView.Row["TipoCartao"].ToString();
                        }
                        //PIX
                        else if (formaPagamento.Id == 3)
                        {
                            ContaBancaria contaBancaria = new ContaBancaria();
                            contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            if (ordemServico.Id > 0)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = contaBancaria;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = ordemServico.Cliente;
                               // ordemServicoPagamento.ContaBancaria = contaBancaria;
                                Controller.getInstance().salvar(caixa);
                            }
                        }
                        //Depósito ou Transferência
                        else if (formaPagamento.Id == 4)
                        {
                            ContaBancaria contaBancaria = new ContaBancaria();
                            contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            if (ordemServico.Id > 0)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = contaBancaria;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = ordemServico.Cliente;
                                //ordemServicoPagamento.ContaBancaria = contaBancaria;
                                Controller.getInstance().salvar(caixa);
                            }
                        }
                        //Crediário
                        else if (formaPagamento.Id == 6)
                        {
                            if (listaCrediario.Count > 0)
                            {
                                int c = 0;
                                foreach (ContaReceber contaReceber in listaCrediario)
                                {
                                    c++;
                                    contaReceber.Concluido = true;
                                    contaReceber.Parcela = c.ToString();
                                    contaReceber.Documento = "OS" + ordemServico.Id + "/" + c;
                                    Controller.getInstance().salvar(contaReceber);
                                }
                            }
                        }
                        //Cheque
                        else if (formaPagamento.Id == 7)
                        {
                            if (listaCheque.Count > 0)
                            {
                                int c = 0;
                                foreach (Cheque cheque in listaCheque)
                                {
                                    c++;
                                    caixa.Conciliado = true;
                                    caixa.Concluido = true;
                                    caixa.ContaBancaria = null;
                                    caixa.DataLancamento = DateTime.Now;
                                    caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                    caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                    caixa.FormaPagamento = formaPagamento;
                                    caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                    caixa.TabelaOrigem = origem;
                                    caixa.Tipo = "E";
                                    caixa.Usuario = Sessao.usuarioLogado;
                                    caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                    caixa.Pessoa = ordemServico.Cliente;
                                    Controller.getInstance().salvar(caixa);
                                    Controller.getInstance().salvar(cheque);

                                    ContaReceber contaReceber = new ContaReceber();
                                    contaReceber.Id = 0;
                                    contaReceber.NomeCliente = ordemServico.Cliente.RazaoSocial;
                                    contaReceber.CnpjCliente = ordemServico.Cliente.Cnpj;
                                    contaReceber.Data = DateTime.Now;
                                    contaReceber.Descricao = "O.S - " + ordemServico.Id + " - CHEQUE";
                                    contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                                    contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    contaReceber.Juro = 0;
                                    contaReceber.Multa = 0;
                                    if (ordemServico.Cliente.EnderecoPrincipal != null)
                                        contaReceber.EnderecoCliente = ordemServico.Cliente.EnderecoPrincipal.Logradouro + ", " + ordemServico.Cliente.EnderecoPrincipal.Numero + " - " + ordemServico.Cliente.EnderecoPrincipal.Complemento;
                                    else
                                        contaReceber.EnderecoCliente = "";
                                    contaReceber.Documento = "OS" + ordemServico.Id + "/" + c;
                                    contaReceber.FormaPagamento = formaPagamento;
                                    contaReceber.Recebido = false;
                                    contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                    contaReceber.Venda = null;
                                    contaReceber.OrdemServico = ordemServico;
                                    contaReceber.Parcela = c.ToString();
                                    contaReceber.VendaFormaPagamento = null;
                                    contaReceber.Cliente = ordemServico.Cliente;
                                    contaReceber.Origem = "ORDEMSERVICO";
                                    contaReceber.Concluido = true;
                                    Controller.getInstance().salvar(contaReceber);
                                }
                            }
                        }
                        //Crédito Cliente
                        else if (formaPagamento.Id == 8)
                        {
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);

                            CreditoCliente creditoCliente = new CreditoCliente();
                            creditoCliente.Cliente = clienteLista;
                            creditoCliente.DataUtilizacao = DateTime.Now;
                            creditoCliente.EmpresaFilial = Sessao.empresaFilialLogada;
                            creditoCliente.Origem = origem;
                            creditoCliente.Valor = 0;
                            creditoCliente.ValorUtilizado = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            Controller.getInstance().salvar(creditoCliente);
                        }
                    }
                    //listaOrdemServicoPagamento.Add(ordemServicoPagamento);
                }
                foreach (OrdemServicoPagamento ordemServicoPagamento1 in listaOrdemServicoPagamento)
                {
                    Controller.getInstance().salvar(ordemServicoPagamento1);
                }
                //ordemServico.DataEncerramento = DateTime.Now;
                //ordemServico.Status = "ENCERRADA";
                //Controller.getInstance().salvar(ordemServico);
                //GenericaDesktop generica = new GenericaDesktop();
                //IList<OrdemServicoProduto> listaProdutoOS = new List<OrdemServicoProduto>();
                //OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
                //listaProdutoOS = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
                //foreach (OrdemServicoProduto ordemServicoProduto in listaProdutoOS)
                //{
                //    generica.atualizarEstoqueNaoConciliado(ordemServicoProduto.Produto, ordemServicoProduto.Quantidade, false, "O.S " + ordemServico.Id.ToString(), "O.S: " + ordemServico.Id + " CLI: " + ordemServico.Cliente.RazaoSocial, ordemServico.Cliente, DateTime.Now, null);
                //}
                CreditoCliente credito = new CreditoCliente();
                credito.Cliente = ordemServico.Cliente;
                credito.EmpresaFilial = Sessao.empresaFilialLogada;
                credito.Origem = "ORDEMSERVICO";
                credito.Valor = valorRecebido;
                credito.ValorUtilizado = 0;
                Controller.getInstance().salvar(credito);
                ordemServico.Entrada = true;
                Controller.getInstance().salvar(ordemServico);
                GenericaDesktop.ShowInfo("Entrada registrada com Sucesso!");
                this.Close();
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro ao encerrar a O.S " + ordemServico.Id + "\n\n" + err.Message);
            }
        }
        private void concluirRecebimentoOrdemServico()
        {
            try
            {
                IList<OrdemServicoPagamento> listaOrdemServicoPagamento = new List<OrdemServicoPagamento>();
                FormaPagamento formaPagamento = new FormaPagamento();
                OrdemServicoPagamento ordemServicoPagamento = new OrdemServicoPagamento();
                IList<ContaReceber> lis = new List<ContaReceber>();
                var records = gridRecebimento.View.Records;
                decimal somaTotalRecebido = 0;
                foreach (var record in records)
                {
                    ordemServicoPagamento = new OrdemServicoPagamento();
                    Caixa caixa = new Caixa();
                    var dataRowView = record.Data as DataRowView;
                    formaPagamento = new FormaPagamento();
                    formaPagamento.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                    somaTotalRecebido = somaTotalRecebido + decimal.Parse(dataRowView.Row["Valor"].ToString());
                    if (formaPagamento.Id > 0)
                    {
                        ordemServicoPagamento.DataRecebimento = DateTime.Now;
                        ordemServicoPagamento.FormaPagamento = formaPagamento;
                        ordemServicoPagamento.OrdemServico = ordemServico;
                        ordemServicoPagamento.Troco = 0;
                        ordemServicoPagamento.ValorRecebido = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        ordemServicoPagamento.AdquirenteCartao = null;
                        ordemServicoPagamento.AutorizacaoCartao = "";
                        ordemServicoPagamento.BandeiraCartao = null;
                        ordemServicoPagamento.Cartao = false;
                        ordemServicoPagamento.ParcelamentoCartao = null;
                        ordemServicoPagamento.Parcelas = "";
                        ordemServicoPagamento.TipoCartao = "";
                        caixa.IdOrigem = ordemServico.Id.ToString();

                        //Dinheiro
                        if (formaPagamento.Id == 1)
                        {
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);
                        }
                        //Cartão
                        else if (formaPagamento.Id == 2)
                        {
                            BandeiraCartao bandeiraCartao = new BandeiraCartao();
                            bandeiraCartao.Id = int.Parse(dataRowView.Row["IdBandeira"].ToString());
                            bandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);

                            Parcelamento parcelamento = new Parcelamento();
                            parcelamento.Id = int.Parse(dataRowView.Row["IdParcelamento"].ToString());
                            parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);

                            AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                            adquirenteCartao.Id = int.Parse(dataRowView.Row["Adquirente"].ToString());
                            adquirenteCartao = (AdquirenteCartao)Controller.getInstance().selecionar(adquirenteCartao);
                            decimal valor = decimal.Parse(dataRowView.Row["Valor"].ToString());

                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);
                            ordemServicoPagamento.AdquirenteCartao = adquirenteCartao;
                            ordemServicoPagamento.AutorizacaoCartao = dataRowView.Row["AutorizacaoCartao"].ToString();
                            ordemServicoPagamento.BandeiraCartao = bandeiraCartao;
                            ordemServicoPagamento.Cartao = true;
                            ordemServicoPagamento.ParcelamentoCartao = parcelamento;
                            ordemServicoPagamento.Parcelas = parcelamento.Parcelas.ToString();
                            ordemServicoPagamento.TipoCartao = dataRowView.Row["TipoCartao"].ToString();
                        }
                        //PIX
                        else if (formaPagamento.Id == 3)
                        {
                            ContaBancaria contaBancaria = new ContaBancaria();
                            contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            if (ordemServico.Id > 0)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = contaBancaria;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = ordemServico.Cliente;
                                ordemServicoPagamento.ContaBancaria = contaBancaria;
                                Controller.getInstance().salvar(caixa);
                            }
                        }
                        //Depósito ou Transferência
                        else if (formaPagamento.Id == 4)
                        {
                            ContaBancaria contaBancaria = new ContaBancaria();
                            contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            if (ordemServico.Id > 0)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = contaBancaria;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = ordemServico.Cliente;
                                ordemServicoPagamento.ContaBancaria = contaBancaria;
                                Controller.getInstance().salvar(caixa);
                            }
                        }
                        //Crediário
                        else if (formaPagamento.Id == 6)
                        {
                            if (listaCrediario.Count > 0)
                            {
                                int c = 0;
                                
                                foreach (ContaReceber contaReceber in listaCrediario)
                                { c++;
                                    contaReceber.Concluido = true;
                                    contaReceber.Parcela = c.ToString();
                                    contaReceber.Documento = "OS"+ordemServico.Id+"/"+c;
                                    Controller.getInstance().salvar(contaReceber);
                                    lis.Add(contaReceber);
                                }
                            }
                        }
                        //Cheque
                        else if (formaPagamento.Id == 7)
                        {
                            if (listaCheque.Count > 0)
                            {
                                int c = 0;
                                foreach (Cheque cheque in listaCheque)
                                {
                                    c++;
                                    caixa.Conciliado = true;
                                    caixa.Concluido = true;
                                    caixa.ContaBancaria = null;
                                    caixa.DataLancamento = DateTime.Now;
                                    caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                                    caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                    caixa.FormaPagamento = formaPagamento;
                                    caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                    caixa.TabelaOrigem = origem;
                                    caixa.Tipo = "E";
                                    caixa.Usuario = Sessao.usuarioLogado;
                                    caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                    caixa.Pessoa = ordemServico.Cliente;
                                    Controller.getInstance().salvar(caixa);
                                    Controller.getInstance().salvar(cheque);

                                    ContaReceber contaReceber = new ContaReceber();
                                    contaReceber.Id = 0;
                                    contaReceber.NomeCliente = ordemServico.Cliente.RazaoSocial;
                                    contaReceber.CnpjCliente = ordemServico.Cliente.Cnpj;
                                    contaReceber.Data = DateTime.Now;
                                    contaReceber.Descricao = "O.S - " + ordemServico.Id + " - CHEQUE";
                                    contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                                    contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    contaReceber.Juro = 0;
                                    contaReceber.Multa = 0;
                                    if (ordemServico.Cliente.EnderecoPrincipal != null)
                                        contaReceber.EnderecoCliente = ordemServico.Cliente.EnderecoPrincipal.Logradouro + ", " + ordemServico.Cliente.EnderecoPrincipal.Numero + " - " + ordemServico.Cliente.EnderecoPrincipal.Complemento;
                                    else
                                        contaReceber.EnderecoCliente = "";
                                    contaReceber.Documento = "OS" + ordemServico.Id + "/" + c;
                                    contaReceber.FormaPagamento = formaPagamento;
                                    contaReceber.Recebido = false;
                                    contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                    contaReceber.Venda = null;
                                    contaReceber.OrdemServico = ordemServico;
                                    contaReceber.Parcela = c.ToString();
                                    contaReceber.VendaFormaPagamento = null;
                                    contaReceber.Cliente = ordemServico.Cliente;
                                    contaReceber.Origem = "ORDEMSERVICO";
                                    contaReceber.Concluido = true;
                                    Controller.getInstance().salvar(contaReceber);
                                    lis.Add(contaReceber);
                                }
                            }
                        }
                        //Crédito Cliente
                        else if (formaPagamento.Id == 8)
                        {
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Now;
                            caixa.Descricao = "REC. " + origem + " " + ordemServico.Id.ToString() + " - " + ordemServico.Cliente.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.FormaPagamento = formaPagamento;
                            caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                            caixa.TabelaOrigem = origem;
                            caixa.Tipo = "E";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                            caixa.Pessoa = ordemServico.Cliente;
                            Controller.getInstance().salvar(caixa);

                            CreditoCliente creditoCliente = new CreditoCliente();
                            creditoCliente.Cliente = clienteLista;
                            creditoCliente.DataUtilizacao = DateTime.Now;
                            creditoCliente.EmpresaFilial = Sessao.empresaFilialLogada;
                            creditoCliente.Origem = origem;
                            creditoCliente.Valor = 0;
                            creditoCliente.ValorUtilizado = decimal.Parse(dataRowView.Row["Valor"].ToString());

                            Controller.getInstance().salvar(creditoCliente);
                        }
                    }
                    listaOrdemServicoPagamento.Add(ordemServicoPagamento);
                }
                foreach (OrdemServicoPagamento ordemServicoPagamento1 in listaOrdemServicoPagamento)
                {
                    Controller.getInstance().salvar(ordemServicoPagamento1);
                }
                ordemServico.DataEncerramento = DateTime.Now;
                ordemServico.Status = "ENCERRADA";
                ordemServico.OperadorEncerramento = Sessao.usuarioLogado.Id.ToString() + "-" + Sessao.usuarioLogado.Login;
                if (descontoRecebido > 0)
                {
                    ordemServico.ValorDesconto = descontoRecebido;
                    ordemServico.ValorTotal = somaTotalRecebido;
                    decimal descontoPercentualNaOS = ((descontoRecebido * 100) / (ordemServico.ValorTotal+descontoRecebido));
                    ratearDescontoItens(descontoPercentualNaOS);
                }
                if (acrescimoRecebido > 0)
                {
                    decimal valorOriginal = ordemServico.ValorTotal;
                    ordemServico.ValorAcrescimo = acrescimoRecebido;
                    ordemServico.ValorTotal = somaTotalRecebido;
                    //decimal acrescimoNaOS = ((acrescimoRecebido * 100) / (ordemServico.ValorTotal + acrescimoRecebido));
                    decimal acrescimoNaOS = Math.Abs(((valorOriginal - ordemServico.ValorTotal) / valorOriginal) * 100);
                    ratearDescontoItens(acrescimoNaOS);
                }
                Controller.getInstance().salvar(ordemServico);
                GenericaDesktop generica = new GenericaDesktop();
                IList<OrdemServicoProduto> listaProdutoOS = new List<OrdemServicoProduto>();
                OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
                listaProdutoOS = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutoOS)
                {
                    generica.atualizarEstoqueNaoConciliado(ordemServicoProduto.Produto, ordemServicoProduto.Quantidade, false, "O.S " + ordemServico.Id.ToString(), "O.S: " + ordemServico.Id + " CLI: " + ordemServico.Cliente.RazaoSocial, ordemServico.Cliente, DateTime.Now, null);
                }
                if (lis.Count > 0)
                {
                    FrmImprimirDuplicata frDup = new FrmImprimirDuplicata(ordemServico.Cliente, lis);
                    frDup.ShowDialog();
                }
                GenericaDesktop.ShowInfo("Ordem de Serviço encerrada com sucesso");
                this.Close();
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Erro ao encerrar a O.S " + ordemServico.Id + "\n\n" + err.Message);
            }
        }

        private void ratearDescontoItens(decimal percentualDesconto)
        {
            int i = 1;
            //decimal somarDescontoTotal = 0;
            decimal valorDescontoInformado = 0;
            IList<OrdemServicoProduto> listaProdutoOS = new List<OrdemServicoProduto>();
            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            listaProdutoOS = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            decimal somaDescCalc = 0;
            decimal somaValorItensComDesconto = 0;
            if (descontoRecebido > 0)
            {
                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutoOS)
                {
                    i++;
                    decimal descontoItem = ordemServicoProduto.ValorTotal * percentualDesconto / 100;
                    descontoItem = Math.Round(descontoItem, 2);
                    ordemServicoProduto.Desconto = descontoItem;
                    ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal - descontoItem;
                    somaDescCalc = somaDescCalc + descontoItem;
                    somaValorItensComDesconto = somaValorItensComDesconto + ordemServicoProduto.ValorTotal;
                    Controller.getInstance().salvar(ordemServicoProduto);
                }
            }
            else if(acrescimoRecebido > 0)
            {
                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutoOS)
                {
                    i++;
                    decimal acrescimoItem = ordemServicoProduto.ValorTotal * percentualDesconto / 100;
                    acrescimoItem = Math.Round(acrescimoItem, 2);
                    ordemServicoProduto.Acrescimo = acrescimoItem;
                    ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal + acrescimoItem;
                    somaDescCalc = somaDescCalc + acrescimoItem;
                    somaValorItensComDesconto = somaValorItensComDesconto + ordemServicoProduto.ValorTotal;
                    Controller.getInstance().salvar(ordemServicoProduto);
                }
            }


                if (ordemServico.ValorTotal != somaValorItensComDesconto)
                {
                    //Se deu diferenca de centavos, ajustar no ultimo item
                    decimal diferenca = ordemServico.ValorTotal - somaValorItensComDesconto;
                //MessageBox.Show("DIFERENÇA: R$ " + diferenca.ToString());

                int qtd = 0;
                foreach (OrdemServicoProduto ordemServicoProduto in listaProdutoOS)
                {
                    qtd++;
                    //se é o ultimo item da o.s ajustamos o valor do desconto
                    if(qtd == listaProdutoOS.Count)
                    {
                        if(somaValorItensComDesconto > ordemServico.ValorTotal)
                        {
                            if (descontoRecebido > 0)
                            {
                                ordemServicoProduto.Desconto = ordemServicoProduto.Desconto + diferenca;
                                ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal - diferenca;
                            }
                            else if(acrescimoRecebido > 0)
                            {
                                ordemServicoProduto.Acrescimo = ordemServicoProduto.Acrescimo + diferenca;
                                ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal - diferenca;
                            }
                            Controller.getInstance().salvar(ordemServicoProduto);
                        }
                        else if (somaValorItensComDesconto < ordemServico.ValorTotal)
                        {
                            if (descontoRecebido > 0)
                            {
                                ordemServicoProduto.Desconto = ordemServicoProduto.Desconto - diferenca;
                                ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal + diferenca;
                            }
                            else if (acrescimoRecebido > 0)
                            {
                                ordemServicoProduto.Acrescimo = ordemServicoProduto.Acrescimo - diferenca;
                                ordemServicoProduto.ValorTotal = ordemServicoProduto.ValorTotal + diferenca;
                            }
                            Controller.getInstance().salvar(ordemServicoProduto);
                        }
                    }
                }
            }
        }

        private void gridRecebimento_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnDesconto_Click(object sender, EventArgs e)
        {
            //valorOriginal = 0;
            //valorMulta = 0;
            //valorJuro = 0;
            //acrescimoRecebido = 0;
            //descontoRecebido = 0;
            //lblDescAcre.Text = "Desconto";
            if (listaReceber.Count > 0 || listaPagar.Count > 0)
            {
                descontoPercentual = 0;
                valorOriginal = 0;
                valorMulta = 0;
                valorJuro = 0;
                acrescimoRecebido = 0;
                descontoRecebido = 0;

                var records = gridFaturas.View.Records;
                int i = 1;
                decimal somarDescontoTotal = 0;
                decimal valorDescontoInformado = 0;
                foreach (var record in records)
                {
                    var dataRowV = record.Data as DataRowView;
                   
                    valorOriginal = valorOriginal + decimal.Parse(dataRowV.Row["ValorParcela"].ToString());
                    valorMulta = valorMulta + decimal.Parse(dataRowV.Row["Multa"].ToString());
                    valorJuro = valorJuro + decimal.Parse(dataRowV.Row["Juro"].ToString());
                }
            }
            if(ordemServico.Id > 0)
            {
                valorOriginal = ordemServico.ValorTotal;
                valorMulta = 0;
                valorJuro = 0;
                acrescimoRecebido = 0;
                descontoRecebido = 0;
            }
            FrmDescontoLiquidacaoReceber uu = new FrmDescontoLiquidacaoReceber(valorOriginal, valorMulta, valorJuro);
            Form formBackground = new Form();
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
            decimal retornoTotal = 0;
            
            switch (uu.showModalNovo(ref retornoTotal))
            {
                case DialogResult.Ignore:
                    break;
                case DialogResult.OK:
                    if (retornoTotal < (valorOriginal + valorJuro + valorMulta))
                    {
                        lblDescAcre.Text = "Desconto";
                        descontoRecebido = (valorOriginal + valorJuro + valorMulta) - retornoTotal;
                        descontoRecebido = Math.Round(descontoRecebido, 2);
                        if (ordemServico.Id > 0)
                            valorTotal = ordemServico.ValorTotal;
                        descontoPercentual = descontoRecebido * 100 / valorTotal;
                        calcularTotais();
                        if(listaReceber.Count > 0)
                            ratearDescontoOuAcrescimoParcelasReceber(descontoPercentual);
                        else if (ordemServico.Id > 0)
                            gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(1), gridFaturas.Columns[9].MappingName, descontoRecebido.ToString());

                    }
                    else if (retornoTotal > (valorOriginal + valorJuro + valorMulta))
                    {
                        lblDescAcre.Text = "Acréscimo";
                        acrescimoRecebido = retornoTotal - (valorOriginal + valorJuro + valorMulta);
                        acrescimoRecebido = Math.Round(acrescimoRecebido, 2);
                        calcularTotais();
                        decimal acrescimoPercentual = acrescimoRecebido * 100 / valorTotal;
                        if (listaReceber.Count > 0 || listaPagar.Count > 0)
                            ratearDescontoOuAcrescimoParcelasReceber(acrescimoPercentual);
                        else if (ordemServico.Id > 0)
                            gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(1), gridFaturas.Columns[9].MappingName, acrescimoRecebido.ToString());
                    }
                    else
                    {
                        acrescimoRecebido = 0;
                        descontoRecebido = 0;
                        if(ordemServico.Id > 0)
                            gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(1), gridFaturas.Columns[9].MappingName, 0);
                        else if (listaReceber.Count > 0)
                        {
                            var records = gridFaturas.View.Records;
                            int z = 1;
                            foreach (var record in records)
                            {
                                var dataRowV = record.Data as DataRowView;
                                gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(z), gridFaturas.Columns[9].MappingName, 0);
                                z++;
                            }
                        }
                    }
                    calcularTotais();
                    break;
            }
            uu.Dispose();
            formBackground.Dispose();
        }
        private void ratearDescontoOuAcrescimoParcelasReceber(decimal percentualDesconto)
        {
            //paramos nessa etapa, o datarow viwer vindo nulo, criar um dataset pra essa grid de faturas.
            var records = gridFaturas.View.Records;
            int i = 1;
            decimal somarDescontoTotal = 0;
            decimal valorDescontoInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                if (dataRowView != null)
                {
                    decimal descontoItem = (decimal.Parse(dataRowView.Row[6].ToString())) * percentualDesconto / 100;
                    descontoItem = Math.Round(descontoItem, 2);
                    gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(i), gridFaturas.Columns[9].MappingName, descontoItem);
                    i++;
                    somarDescontoTotal = somarDescontoTotal + decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                    valorDescontoInformado = decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                }
                
            }
            String descontoInformado = somarDescontoTotal.ToString("N2");
            string valorDescontoItensInf = somarDescontoTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (lblDesconto.Text != valorDescontoItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(lblDesconto.Text.Replace("R$ ", "")) - somarDescontoTotal;
                // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if (somarDescontoTotal > decimal.Parse(lblDesconto.Text.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado - diferenca;
                    int index = gridFaturas.RowCount - 1;
                    gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(index), gridFaturas.Columns[9].MappingName, valorDescontoInformado);

                }
                else if (somarDescontoTotal < decimal.Parse(lblDesconto.Text.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado + diferenca;
                    int index = gridFaturas.RowCount - 1;
                    gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(index), gridFaturas.Columns[9].MappingName, valorDescontoInformado);
                }
            }
        }

        

        private void concluirRecebimentoContaReceber()
        {
            //Envia lista de caixa, receber, cartoes, pix ou cheques para salvar junto
            IList<Caixa> listaRecebimentosCaixa = new List<Caixa>();
            IList<ContaRecebidaFormaPagamento> listaContaRecebidaPagamento = new List<ContaRecebidaFormaPagamento>();
            FormaPagamento formaPagamento = new FormaPagamento();
            String descricaoRecebimento = "";
            String parcelas = "";
            String idRecebido = "";

            foreach (ContaReceber contaReceber in listaReceber)
            {
                if (String.IsNullOrEmpty(parcelas))
                {
                    parcelas = contaReceber.Documento;
                    idRecebido = contaReceber.Id.ToString();
                }
                else
                {
                    parcelas = parcelas + " - " + contaReceber.Documento;
                    idRecebido = idRecebido + " - " + contaReceber.Id.ToString();
                }

                descricaoRecebimento = "REC. " +contaReceber.Cliente.RazaoSocial + " DOCS: " + parcelas;
                if(parcial == true)
                    descricaoRecebimento = "REC. PARCIAL " + contaReceber.Cliente.RazaoSocial + " DOC: " + parcelas;
            }
            

            var records = gridRecebimento.View.Records;
            decimal valorTotalRecebido = 0;
            int id = 0;
            foreach (var record in records)
            {
                Caixa caixa = new Caixa();
                var dataRowView = record.Data as DataRowView;
                formaPagamento = new FormaPagamento();
                formaPagamento.Id = int.Parse(dataRowView.Row["Id"].ToString());
                formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                valorTotalRecebido = valorTotalRecebido + decimal.Parse(dataRowView.Row["Valor"].ToString());
                if (formaPagamento.Id > 0)
                {
                    //Dinheiro
                    if (formaPagamento.Id == 1)
                    {
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoRecebimento + " (" +formaPagamento.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = idRecebido;
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        if(pessoaCobrador != null)
                        {
                            if (pessoaCobrador.Id > 0)
                                caixa.Cobrador = pessoaCobrador;
                        }
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Cartão
                    else if (formaPagamento.Id == 2)
                    {
                        BandeiraCartao bandeiraCartao = new BandeiraCartao();
                        bandeiraCartao.Id = int.Parse(dataRowView.Row["IdBandeira"].ToString());
                        bandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);
                        Parcelamento parcelamento = new Parcelamento();
                        parcelamento.Id = int.Parse(dataRowView.Row["IdParcelamento"].ToString());
                        parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);
                        AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                        adquirenteCartao.Id = int.Parse(dataRowView.Row["Adquirente"].ToString());
                        adquirenteCartao = (AdquirenteCartao)Controller.getInstance().selecionar(adquirenteCartao);
                        decimal valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoRecebimento + " (" + dataRowView.Row["Parcelamento"].ToString()+ " " + dataRowView.Row["Bandeira"].ToString() + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = idRecebido;
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        {
                            if (pessoaCobrador.Id > 0)
                                caixa.Cobrador = pessoaCobrador;
                        }
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //PIX
                    else if (formaPagamento.Id == 3)
                    {
                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                        contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoRecebimento + " (" + formaPagamento.Descricao + " " + contaBancaria.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = idRecebido;
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        {
                            if (pessoaCobrador.Id > 0)
                                caixa.Cobrador = pessoaCobrador;
                        }
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Depósito ou Transferência
                    else if (formaPagamento.Id == 4)
                    {
                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                        contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoRecebimento + " (" + formaPagamento.Descricao + " " + contaBancaria.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = idRecebido;
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        {
                            if (pessoaCobrador.Id > 0)
                                caixa.Cobrador = pessoaCobrador;
                        }
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Cheque
                    else if (formaPagamento.Id == 7)
                    {
                        if (listaCheque.Count > 0)
                        {
                            foreach (Cheque cheque in listaCheque)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = null;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = descricaoRecebimento;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.IdOrigem = idRecebido;
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = clienteLista;
                                {
                                    if (pessoaCobrador.Id > 0)
                                        caixa.Cobrador = pessoaCobrador;
                                }
                                Controller.getInstance().salvar(caixa);
                                Controller.getInstance().salvar(cheque);
                                listaRecebimentosCaixa.Add(caixa);
                            }
                        }
                    }
                    //Crédito Cliente
                    else if (formaPagamento.Id == 8)
                    {
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoRecebimento;
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = idRecebido;
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        {
                            if (pessoaCobrador.Id > 0)
                                caixa.Cobrador = pessoaCobrador;
                        }
                        Controller.getInstance().salvar(caixa);

                        CreditoCliente creditoCliente = new CreditoCliente();
                        creditoCliente.Cliente = clienteLista;
                        creditoCliente.DataUtilizacao = DateTime.Now;
                        creditoCliente.EmpresaFilial = Sessao.empresaFilialLogada;
                        creditoCliente.Origem = origem;
                        creditoCliente.Valor = 0;
                        creditoCliente.ValorUtilizado = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        Controller.getInstance().salvar(creditoCliente);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                }
            }
            if(listaRecebimentosCaixa.Count > 0)
            {
                string caixaID = "";
                foreach(Caixa caixa in listaRecebimentosCaixa)
                {
                    if (String.IsNullOrEmpty(caixaID))
                        caixaID = "USUARIO: " + Sessao.usuarioLogado + " - " + caixa.Id.ToString();
                    else
                        caixaID = caixaID + " - " + caixa.Id.ToString();
                }

                var records1 = gridFaturas.View.Records;
                int i = 1;
                foreach (var record in records1)
                {
                    var dataRowView = record.Data as DataRowView;
                    ContaReceber conta = new ContaReceber();
                    conta.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    conta = (ContaReceber)Controller.getInstance().selecionar(conta);

                    if (parcial == false)
                    {
                        conta.Multa = decimal.Parse(dataRowView.Row["Multa"].ToString());
                        conta.Juro = decimal.Parse(dataRowView.Row["Juro"].ToString());
                        conta.Recebido = true;
                        conta.DataRecebimento = DateTime.Now;
                        conta.DescricaoRecebimento = descricaoRecebimento;
                        conta.CaixaRecebimento = caixaID;
                        //conta.Multa = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        //Acréscimo

                        if (lblDescAcre.Text.Equals("Desconto"))
                        {
                            decimal descontoAcrescimo = 0;
                            if (String.IsNullOrEmpty(dataRowView.Row["DescontoAcrescimo"].ToString()))
                            {
                                gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(i), gridFaturas.Columns[9].MappingName, 0);
                            }
                            else
                            {
                                descontoAcrescimo = decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            }

                            conta.ValorRecebido = decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) - descontoAcrescimo;
                            conta.DescontoRecebidoBaixa = descontoAcrescimo;
                            conta.AcrescimoRecebidoBaixa = 0;
                        }
                        else if (lblDescAcre.Text.Equals("Acréscimo"))
                        {
                            if (String.IsNullOrEmpty(dataRowView.Row["DescontoAcrescimo"].ToString()))
                                gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(i), gridFaturas.Columns[9].MappingName, 0);
                            conta.ValorRecebido = decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) + decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            conta.AcrescimoRecebidoBaixa = decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            conta.DescontoRecebidoBaixa = 0;
                        }
                    }
                    else
                    {
                        //RECEBIMENTO PARCIAL
                        conta.Documento = conta.Documento + ".1";
                        //conta.ValorParcela = conta.ValorParcela - decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        conta.ValorRecebimentoParcial = conta.ValorRecebimentoParcial + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        ContaReceberRecebida crr = new ContaReceberRecebida();
                        crr.ContaReceber = conta;
                        crr.Descricao = "RECEBIMENTO PARCIAL " + conta.Documento + "/" + conta.Parcela;
                        crr.ValorAcrescimo = 0;
                        crr.ValorDesconto = 0;
                        crr.ValorOriginal = conta.ValorParcela;
                        crr.ValorOriginalComJuros = conta.ValorParcela + conta.Juro + conta.Multa;
                        crr.ValorRecebido = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        Controller.getInstance().salvar(crr);
                    }
                    Controller.getInstance().salvar(conta); 
                }
                GenericaDesktop.ShowInfo("Recebimento confirmado com sucesso");
                Form formBackground = new Form();
                OrdemServico ordemServico = new OrdemServico();
                FrmRecibo01 uu = new FrmRecibo01(descricaoRecebimento, valorTotalRecebido, clienteLista);
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
                this.Close();
            }
        }

        private void concluirRecebimentoContaPagar()
        {
            //Envia lista de caixa, receber, cartoes, pix ou cheques para salvar junto
            IList<Caixa> listaRecebimentosCaixa = new List<Caixa>();
            FormaPagamento formaPagamento = new FormaPagamento();
            String descricaoPagamento = "";
            String parcelas = "";

            foreach (ContaPagar contaPagar in listaPagar)
            {
                if (String.IsNullOrEmpty(parcelas))
                    parcelas = contaPagar.NumeroDocumento;
                else
                    parcelas = parcelas + " - " + contaPagar.NumeroDocumento;

                descricaoPagamento = "PAG. " + contaPagar.Pessoa.RazaoSocial + " DOCS: " + parcelas;
                //if (parcial == true)
                //    descricaoPagamento = "PAG. PARCIAL " + contaPagar.Pessoa.RazaoSocial + " DOC: " + parcelas;
            }


            var records = gridRecebimento.View.Records;
            decimal valorTotalRecebido = 0;
            int id = 0;
            foreach (var record in records)
            {
                Caixa caixa = new Caixa();
                var dataRowView = record.Data as DataRowView;
                formaPagamento = new FormaPagamento();
                formaPagamento.Id = int.Parse(dataRowView.Row["Id"].ToString());
                formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                valorTotalRecebido = valorTotalRecebido + decimal.Parse(dataRowView.Row["Valor"].ToString());
                if (formaPagamento.Id > 0)
                {
                    //Dinheiro
                    if (formaPagamento.Id == 1)
                    {
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoPagamento + " (" + formaPagamento.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaPagar;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = parcelas;
                        caixa.Tipo = "S";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Cartão
                    else if (formaPagamento.Id == 2)
                    {
                        BandeiraCartao bandeiraCartao = new BandeiraCartao();
                        bandeiraCartao.Id = int.Parse(dataRowView.Row["IdBandeira"].ToString());
                        bandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);
                        Parcelamento parcelamento = new Parcelamento();
                        parcelamento.Id = int.Parse(dataRowView.Row["IdParcelamento"].ToString());
                        parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);
                        AdquirenteCartao adquirenteCartao = new AdquirenteCartao();
                        adquirenteCartao.Id = int.Parse(dataRowView.Row["Adquirente"].ToString());
                        adquirenteCartao = (AdquirenteCartao)Controller.getInstance().selecionar(adquirenteCartao);
                        decimal valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoPagamento + " (" + dataRowView.Row["Parcelamento"].ToString() + " " + dataRowView.Row["Bandeira"].ToString() + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = parcelas;
                        caixa.Tipo = "S";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //PIX
                    else if (formaPagamento.Id == 3)
                    {
                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                        contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoPagamento + " (" + formaPagamento.Descricao + " " + contaBancaria.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = parcelas;
                        caixa.Tipo = "S";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Depósito ou Transferência
                    else if (formaPagamento.Id == 4)
                    {
                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(dataRowView.Row["ContaBancaria"].ToString());
                        contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoPagamento + " (" + formaPagamento.Descricao + " " + contaBancaria.Descricao + ")";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = parcelas;
                        caixa.Tipo = "S";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        Controller.getInstance().salvar(caixa);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                    //Cheque
                    else if (formaPagamento.Id == 7)
                    {
                        if (listaCheque.Count > 0)
                        {
                            foreach (Cheque cheque in listaCheque)
                            {
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = null;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = descricaoPagamento;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = formaPagamento;
                                caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                                caixa.TabelaOrigem = origem;
                                caixa.IdOrigem = parcelas;
                                caixa.Tipo = "S";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                                caixa.Pessoa = clienteLista;
                                Controller.getInstance().salvar(caixa);
                                Controller.getInstance().salvar(cheque);
                                listaRecebimentosCaixa.Add(caixa);
                            }
                        }
                    }
                    //Crédito Cliente
                    else if (formaPagamento.Id == 8)
                    {
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = descricaoPagamento;
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.PlanoConta = Sessao.planoContaRecebimentoContaReceber;
                        caixa.TabelaOrigem = origem;
                        caixa.IdOrigem = parcelas;
                        caixa.Tipo = "S";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        caixa.Pessoa = clienteLista;
                        Controller.getInstance().salvar(caixa);

                        CreditoCliente creditoCliente = new CreditoCliente();
                        creditoCliente.Cliente = clienteLista;
                        creditoCliente.DataUtilizacao = DateTime.Now;
                        creditoCliente.EmpresaFilial = Sessao.empresaFilialLogada;
                        creditoCliente.Origem = origem;
                        creditoCliente.Valor = 0;
                        creditoCliente.ValorUtilizado = decimal.Parse(dataRowView.Row["Valor"].ToString());
                        Controller.getInstance().salvar(creditoCliente);
                        listaRecebimentosCaixa.Add(caixa);
                    }
                }
            }
            if (listaRecebimentosCaixa.Count > 0)
            {
                string caixaID = "";
                foreach (Caixa caixa in listaRecebimentosCaixa)
                {
                    if (String.IsNullOrEmpty(caixaID))
                        caixaID = "USUARIO: " + Sessao.usuarioLogado + " - " + caixa.Id.ToString();
                    else
                        caixaID = caixaID + " - " + caixa.Id.ToString();
                }

                var records1 = gridFaturas.View.Records;
                int i = 1;
                foreach (var record in records1)
                {
                    var dataRowView = record.Data as DataRowView;
                    ContaPagar conta = new ContaPagar();
                    conta.Id = int.Parse(dataRowView.Row["Id"].ToString());
                    conta = (ContaPagar)Controller.getInstance().selecionar(conta);

                    if (parcial == false)
                    {
                        conta.Pago = true;
                        conta.DataPagamento = DateTime.Now;
                        conta.DescricaoPagamento = descricaoPagamento;
                        conta.CaixaPagamento = caixaID;
                        //Acréscimo

                        if (lblDescAcre.Text.Equals("Desconto"))
                        {
                            decimal descontoAcrescimo = 0;
                            if (String.IsNullOrEmpty(dataRowView.Row["DescontoAcrescimo"].ToString()))
                            {
                                gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(i), gridFaturas.Columns[9].MappingName, 0);
                            }
                            else
                            {
                                descontoAcrescimo = decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            }

                            conta.ValorPago = decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) - descontoAcrescimo;
                            conta.DescontoBaixa = descontoAcrescimo;
                            conta.AcrescimoBaixa = 0;
                        }
                        else if (lblDescAcre.Text.Equals("Acréscimo"))
                        {
                            if (String.IsNullOrEmpty(dataRowView.Row["DescontoAcrescimo"].ToString()))
                                gridFaturas.View.GetPropertyAccessProvider().SetValue(gridFaturas.GetRecordAtRowIndex(i), gridFaturas.Columns[9].MappingName, 0);
                            conta.ValorPago = decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) + decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            conta.AcrescimoBaixa = decimal.Parse(dataRowView.Row["DescontoAcrescimo"].ToString());
                            conta.DescontoBaixa = 0;
                        }
                    }
                    else
                    {
                        //RECEBIMENTO PARCIAL
                        conta.NumeroDocumento = conta.NumeroDocumento + ".1";
                        conta.ValorTotal = conta.ValorTotal - decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                    }
                    Controller.getInstance().salvar(conta);
                }
                GenericaDesktop.ShowInfo("Pagamento confirmado com sucesso");
                //Form formBackground = new Form();
                //OrdemServico ordemServico = new OrdemServico();
                //FrmRecibo01 uu = new FrmRecibo01(descricaoPagamento, valorTotalRecebido, clienteLista);
                //formBackground.StartPosition = FormStartPosition.Manual;
                ////formBackground.FormBorderStyle = FormBorderStyle.None;
                //formBackground.Opacity = .50d;
                //formBackground.BackColor = Color.Black;
                ////formBackground.Left = Top = 0;
                //formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                //formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                //formBackground.WindowState = FormWindowState.Maximized;
                //formBackground.TopMost = false;
                //formBackground.Location = this.Location;
                //formBackground.ShowInTaskbar = false;
                //formBackground.Show();
                //uu.Owner = formBackground;
                //uu.ShowDialog();
                //formBackground.Dispose();
                //uu.Dispose();
                this.Close();
            }
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

        private void btnCrediario_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCrediario();
        }
        private void abrirFormPagamentoCrediario()
        {
            var records = gridRecebimento.View.Records;
            bool jaPossuiCrediario = false;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                int idForma = int.Parse(dataRowView.Row["Id"].ToString());
                if (idForma == 6)
                    jaPossuiCrediario = true;
            }
            if(jaPossuiCrediario == false) 
            {
                listaCrediario = new List<ContaReceber>();
                if (decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")) > 0)
                {
                    if (ordemServico.Id > 0)
                    {
                        valorTotal = ordemServico.ValorTotal;
                        Sessao.vendasRecebimento_ValorRecebido = 0;
                        Form formBackground = new Form();
                        try
                        {
                            FormaPagamento formaPagamento = new FormaPagamento();
                            decimal valorRecebido = 0;
                            using (FrmCrediarioOrdemServico uu = new FrmCrediarioOrdemServico(decimal.Parse(lblValorFaltante.Text.Replace("R$ ", "")), ordemServico))
                            {
                                formBackground.StartPosition = FormStartPosition.Manual;
                                //formBackground.FormBorderStyle = FormBorderStyle.None;
                                formBackground.Opacity = .50d;
                                formBackground.BackColor = Color.Black;
                                //  formBackground.Left = Top = 10;
                                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                                formBackground.WindowState = FormWindowState.Maximized;
                                formBackground.TopMost = false;
                                formBackground.Location = this.Location;
                                formBackground.ShowInTaskbar = false;
                                formBackground.Show();
                                uu.Owner = formBackground;
                                switch (uu.showModalReceber(ref formaPagamento, ref valorRecebido, ref listaCrediario))
                                {
                                    case DialogResult.Ignore:
                                        uu.Dispose();
                                        formBackground.Dispose();
                                        break;
                                    case DialogResult.OK:
                                        inserirFormaPagamentoGrid(formaPagamento, valorRecebido, null, null, "", null, null, DateTime.Parse("1900-01-01 00:00:00"), "");
                                        uu.Dispose();
                                        formBackground.Dispose();
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
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor total ja foi recebido!");
                }
            }
            else
                GenericaDesktop.ShowAlerta("Ja foi registrado uma forma de pagamento como crediário, não é possível utilizar a msm forma 2x");
        }

        private void iconeCrediario_Click(object sender, EventArgs e)
        {
            abrirFormPagamentoCrediario();
        }

        private void btnSelecionarCobrador_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa","and Tabela.Cobrador = true"))
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
                    switch (uu.showModal("Pessoa", "and Tabela.Cobrador = true", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                pessoaCobrador = ((Pessoa)pessoaObj);
                                lblCobrador.Visible = true;
                                btnLimparCobrador.Visible = true;
                                lblCobrador.Text = "COBRADOR: " + pessoaCobrador.RazaoSocial; 
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            pessoaCobrador = ((Pessoa)pessoaOjeto);
                            lblCobrador.Visible = true;
                            btnLimparCobrador.Visible = true;
                            lblCobrador.Text = "COBRADOR: " + pessoaCobrador.RazaoSocial;
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

        private void btnLimparCobrador_Click(object sender, EventArgs e)
        {
            pessoaCobrador = new Pessoa();
            lblCobrador.Visible = false;
            btnLimparCobrador.Visible = false;
        }
    }
}
