using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmBoleto : Form
    {
        BoletoConfigController boletoConfigController = new BoletoConfigController();
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        GenericaDesktop generica = new GenericaDesktop();
        OrdemServico ordemservico = new OrdemServico();
        FormaPagamento fp = new FormaPagamento();
        IList<ContaReceber> listaBoletoReceber = new List<ContaReceber>();
        ContaBancaria conta = new ContaBancaria();
        public DialogResult showModalNovo(ref object vendaFormaPagamento)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                vendaFormaPagamento = this.vendaFormaPagamento;
            }
            return DialogResult;
        }

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref IList<ContaReceber> listaBoletoReceber, ref ContaBancaria contaBancaria)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                listaBoletoReceber = this.listaBoletoReceber;
                valorRecebido = this.valor;
                contaBancaria = this.conta;
            }
            return DialogResult;
        }
        public FrmBoleto(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;


            IList<BoletoConfig> listaBoletoConfig = new List<BoletoConfig>();
            listaBoletoConfig = boletoConfigController.selecionarTodosBoletoConfig();
            if(listaBoletoConfig.Count > 0)
            {
                foreach(BoletoConfig bol in listaBoletoConfig)
                {
                    if(bol.ContaPadrao == true)
                    {
                        txtContaBancaria.Texts = bol.ContaBancaria.Descricao;
                        txtCodContaBancaria.Texts = bol.ContaBancaria.Id.ToString();
                    }
                }
            }
            txtParcelas.Text = "1";
            txtParcelas.Focus();
            txtParcelas.SelectAll();
        }

        public FrmBoleto(decimal valorFaltante, OrdemServico ordemservico)
        {
            InitializeComponent();

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.ordemservico = ordemservico;
            txtParcelas.Text = "1";
            txtParcelas.Focus();
            txtParcelas.SelectAll();
        }

        private void FrmBoleto_Paint(object sender, PaintEventArgs e)
        {
            gridParcelas.Columns["VENCIMENTO"].AllowEditing = true;
            txtValor.Text = string.Format("{0:0.00}", valorFaltante);
            if (String.IsNullOrEmpty(txtValor.Text))
            {
                txtParcelas.Text = "1";
                txtParcelas.Focus();
                txtParcelas.SelectAll();
            }
            txtDataVencimento.Value = DateTime.Now.AddMonths(1);
        }
        private bool ValidarCampos()
        {
            // Verifica se o campo de parcelas está preenchido
            if (string.IsNullOrWhiteSpace(txtParcelas.Text))
            {
                MessageBox.Show("Por favor, preencha o campo de Parcelas.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtParcelas.Focus();
                return false;
            }

            // Verifica se o campo de código da conta bancária está preenchido
            if (string.IsNullOrWhiteSpace(txtCodContaBancaria.Texts))
            {
                MessageBox.Show("Por favor, preencha o campo de Código da Conta Bancária.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodContaBancaria.Focus();
                return false;
            }

            // Verifica se o campo de valor está preenchido e é um número válido
            if (string.IsNullOrWhiteSpace(txtValor.Text) || !decimal.TryParse(txtValor.Text, out _))
            {
                MessageBox.Show("Por favor, preencha o campo de Valor com um número válido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Focus();
                return false;
            }

            // Verifica se o grid de parcelas contém pelo menos uma linha
            if (gridParcelas.RowCount == 1)
            {
                btnConfirmaParcelas.PerformClick();
            }

            return true; // Todos os campos estão válidos
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();

            if (!String.IsNullOrEmpty(txtValor.Text) && ValidarCampos())
            {
                if (decimal.Parse(txtValor.Text) <= valorFaltante)
                {
                    if (venda != null)
                    {
                        if (venda.Id > 0)
                        {
                            this.valor = decimal.Parse(txtValor.Text);
                            FormaPagamento formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 5;
                            vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            vendaFormaPagamento.Parcelamento = int.Parse(txtParcelas.Text);
                            vendaFormaPagamento.ValorRecebido = valor;
                            vendaFormaPagamento.Cartao = false;
                            vendaFormaPagamento.AutorizacaoCartao = "";
                            vendaFormaPagamento.Venda = venda;
                            vendaFormaPagamento.TipoCartao = "";
                            vendaFormaPagamento.ParcelamentoFk = null;
                            ContaBancaria contaBancaria = new ContaBancaria();
                            contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts);
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            vendaFormaPagamento.ContaBancaria = contaBancaria;
                            Controller.getInstance().salvar(vendaFormaPagamento);

                            var records = gridParcelas.View.Records;
                            foreach (var record in records)
                            {
                                var dataRowView = record.Data as DataRowView;

                                ContaReceber contaReceber = new ContaReceber();
                                contaReceber.Id = 0;
                                contaReceber.ContaBoleto = contaBancaria;
                                contaReceber.NomeCliente = venda.Cliente.RazaoSocial;
                                contaReceber.CnpjCliente = venda.Cliente.Cnpj;
                                contaReceber.Data = DateTime.Now;
                                contaReceber.Descricao = "VENDA - " + venda.Id;
                                contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                                contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                contaReceber.ValorTotalOrigem = valor;
                                contaReceber.Juro = 0;
                                contaReceber.Multa = 0;
                                if (venda.Cliente.EnderecoPrincipal != null)
                                    contaReceber.EnderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " + venda.Cliente.EnderecoPrincipal.Numero + " - " + venda.Cliente.EnderecoPrincipal.Complemento;
                                else
                                    contaReceber.EnderecoCliente = "";
                                contaReceber.FormaPagamento = vendaFormaPagamento.FormaPagamento;
                                contaReceber.Recebido = false;
                                contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                contaReceber.Venda = venda;
                                contaReceber.Parcela = dataRowView.Row["PARCELA"].ToString();
                                contaReceber.VendaFormaPagamento = vendaFormaPagamento;
                                contaReceber.Cliente = venda.Cliente;
                                contaReceber.Origem = "VENDA";
                                contaReceber.Concluido = false;
                                Controller.getInstance().salvar(contaReceber);
                            }
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    if (ordemservico != null)
                    {
                        if (ordemservico.Id > 0)
                        {
                            this.valor = decimal.Parse(txtValor.Text);
                            FormaPagamento formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 5;
                            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            this.fp = formaPagamento;

                            ContaBancaria contaBancaria = new ContaBancaria();
                            if(!String.IsNullOrEmpty(txtCodContaBancaria.Texts))
                                contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts);
                            contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                            this.conta = contaBancaria;

                            listaBoletoReceber = new List<ContaReceber>();
                            var records = gridParcelas.View.Records;
                            foreach (var record in records)
                            {
                                var dataRowView = record.Data as DataRowView;

                                ContaReceber contaReceber = new ContaReceber();
                                contaReceber.Id = 0;
                                contaReceber.NomeCliente = ordemservico.Cliente.RazaoSocial;
                                contaReceber.CnpjCliente = ordemservico.Cliente.Cnpj;
                                contaReceber.Data = DateTime.Now;
                                contaReceber.Descricao = "ORDEMSERVICO - " + ordemservico.Id;
                                contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                                contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                contaReceber.ValorTotalOrigem = valor;
                                contaReceber.Juro = 0;
                                contaReceber.Multa = 0;
                                if (ordemservico.Cliente.EnderecoPrincipal != null)
                                    contaReceber.EnderecoCliente = ordemservico.Cliente.EnderecoPrincipal.Logradouro + ", " + ordemservico.Cliente.EnderecoPrincipal.Numero + " - " + ordemservico.Cliente.EnderecoPrincipal.Complemento;
                                else
                                    contaReceber.EnderecoCliente = "";
                                contaReceber.FormaPagamento = formaPagamento;
                                contaReceber.Recebido = false;
                                contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                contaReceber.Venda = null;
                                contaReceber.Parcela = dataRowView.Row["PARCELA"].ToString();
                                contaReceber.VendaFormaPagamento = null;
                                contaReceber.Cliente = ordemservico.Cliente;
                                contaReceber.Origem = "ORDEMSERVICO";
                                contaReceber.Concluido = false;
                                contaReceber.ContaBoleto = contaBancaria;
                                contaReceber.BoletoGerado = false; // vao ser gerados na proxima tela ao finalizar
                                contaReceber.IdBoleto = "0";
                                contaReceber.OrdemServico = ordemservico;
                                listaBoletoReceber.Add(contaReceber);
                            }
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor recebido no PIX não pode ser maior que o valor faltante!");
                    txtValor.Select();
                }

            }
        }

        private void inserirParcelas()
        {
            try
            {
                if (String.IsNullOrEmpty(txtParcelas.Text))
                    txtParcelas.Text = "1";
                dsParcelas.Tables[0].Clear();
                DateTime vencimento = DateTime.Parse(txtDataVencimento.Value.ToString());
                this.gridParcelas.DataSource = dsParcelas.Tables["Parcelas"];
                for (int i = 1; i <= int.Parse(txtParcelas.Text); i++)
                {
                    if (i > 1)
                        vencimento = vencimento.AddMonths(1);

                    int dia = DateTime.Parse(txtDataVencimento.Value.ToString()).Day;
                    if (vencimento.Day != dia)
                        vencimento = DateTime.Parse((dia + "/" + vencimento.Month + "/" + vencimento.Year + " 00:00:00").ToString());
                    System.Data.DataRow row = dsParcelas.Tables[0].NewRow();
                    row.SetField("PARCELA", i);
                    row.SetField("VENCIMENTO", vencimento.ToShortDateString());
                    decimal valorParcela = 0;
                    valorParcela = decimal.Parse(txtValor.Text) / decimal.Parse(txtParcelas.Text);
                    row.SetField("VALOR", string.Format("{0:0.00}", valorParcela));
                    dsParcelas.Tables[0].Rows.Add(row);
                }
                var records = gridParcelas.View.Records;

                //Ajustar centavos de diferença na última parcela
                decimal valorCalculo = 0;
                int linhas = 0;
                foreach (var record in records)
                {
                    linhas++;
                    var dataRowView = record.Data as DataRowView;
                    valorCalculo = valorCalculo + decimal.Parse(dataRowView.Row["Valor"].ToString());
                    if (linhas == records.Count)
                    {
                        if (valorCalculo > decimal.Parse(txtValor.Text))
                        {
                            decimal valorCorrecao = valorCalculo - decimal.Parse(txtValor.Text);
                            valorCorrecao = decimal.Parse(dataRowView.Row["Valor"].ToString()) - valorCorrecao;
                            gridParcelas.View.GetPropertyAccessProvider().SetValue(gridParcelas.GetRecordAtRowIndex(linhas), gridParcelas.Columns[2].MappingName, valorCorrecao);
                        }
                        else if (valorCalculo < decimal.Parse(txtValor.Text))
                        {
                            decimal valorCorrecao = decimal.Parse(txtValor.Text) - valorCalculo;
                            valorCorrecao = valorCorrecao + decimal.Parse(dataRowView.Row["Valor"].ToString());
                            gridParcelas.View.GetPropertyAccessProvider().SetValue(gridParcelas.GetRecordAtRowIndex(linhas), gridParcelas.Columns[2].MappingName, valorCorrecao);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void txtParcelas_Leave(object sender, EventArgs e)
        {
            inserirParcelas();
        }

        private void gridParcelas_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmaParcelas_Click(object sender, EventArgs e)
        {
            inserirParcelas();
        }

        private void FrmBoleto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;
            }
        }

        private void txtParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                inserirParcelas();
        }

        private void gridParcelas_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
            if (e.Column.MappingName == "VENCIMENTO")
            {
                try
                {
                    UpdateVencimentos();
                    DateTime dataValida = DateTime.Parse(e.NewValue.ToString());
                    e.IsValid = true;
                    //GenericaDesktop.ShowInfo("Alterado com Sucesso");
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Data Inválida, digite no formato 00/00/0000";

                }
            }
        }

        private void UpdateVencimentos()
        {
            try
            {
                DateTime baseDate = DateTime.Parse(txtDataVencimento.Value.ToString());
                foreach (DataRow row in dsParcelas.Tables[0].Rows)
                {
                    DateTime vencimento = DateTime.Parse(row["VENCIMENTO"].ToString());

                    if (vencimento != baseDate)
                    {
                        // Recalcule os vencimentos baseados na nova data
                        int monthsToAdd = (vencimento.Year - baseDate.Year) * 12 + vencimento.Month - baseDate.Month;
                        baseDate = baseDate.AddMonths(monthsToAdd);

                        // Atualize o vencimento na tabela
                        row["VENCIMENTO"] = baseDate.ToShortDateString();
                    }
                }

                // Atualize o grid
                gridParcelas.DataSource = dsParcelas.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar vencimentos: " + ex.Message);
            }
        }

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Object objeto = new ContaBancaria();

            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancaria.Texts = "";
                    txtContaBancaria.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
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
