using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Lunar.Telas.ContasReceber
{
    public partial class FrmNovaFatura : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        bool showModal = false;
        IList<ContaReceber> listaContaReceber = new List<ContaReceber>();
        public DialogResult showModalNovo(ref IList<ContaReceber> listaContaReceber)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                listaContaReceber = this.listaContaReceber;
            }
            return DialogResult;
        }
        public FrmNovaFatura()
        {
            InitializeComponent();
            txtPrimeiroVencimento.Value = DateTime.Now.AddMonths(1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                                generica.buscarAlertaCadastrado((Pessoa)pessoaObj);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtNumeroDocumento.Focus();
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

        private void txtValorTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorTotal.Texts));
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Valor Inválido, preencha apenas com números e virgula!");
            }
        }

        private void txtValorTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtValorTotal.Texts, e);
        }

        private void txtParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtParcelas.Texts, e);
        }

        private void txtIntervalo_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtIntervalo.Texts, e);
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
                            txtNumeroDocumento.Focus();
                            generica.buscarAlertaCadastrado(pessoa);
                        }
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Código inválido");
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";

                }
            }
        }

        private void btnPesquisarFormaPagamento_Click(object sender, EventArgs e)
        {
            Object formaPagamentoOjeto = new FormaPagamento();
            Form formBackground = new Form();
            try
            {
                string sqlAdicional = "";
                if (!String.IsNullOrEmpty(txtFormaPagamento.Texts) && String.IsNullOrEmpty(txtCodFormaPagamento.Texts))
                    sqlAdicional = "and Tabela.Descricao like '%" + txtFormaPagamento.Texts + "%'";
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("FormaPagamento", sqlAdicional))
                {
                    txtCodFormaPagamento.Texts = "";
                    txtFormaPagamento.Texts = "";
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
                    switch (uu.showModal("FormaPagamento", "", ref formaPagamentoOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmFormaCadastro form = new FrmClienteCadastro();
                            //if (form.showModalNovo(ref formaPagamentoOjeto) == DialogResult.OK)
                            //{
                            //    txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                            //    txtCodFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Id.ToString();
                            //    txtPlanoContas.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                            txtCodFormaPagamento.Texts = ((FormaPagamento)formaPagamentoOjeto).Id.ToString();
                            txtPlanoContas.Focus();
                            if(((FormaPagamento)formaPagamentoOjeto).Banco == true || ((FormaPagamento)formaPagamentoOjeto).Caixa == true || ((FormaPagamento)formaPagamentoOjeto).Cartao == true || ((FormaPagamento)formaPagamentoOjeto).CreditoCliente == true)
                            {
                                String forma = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                                txtFormaPagamento.Texts = "";
                                txtCodFormaPagamento.Texts = "";
                                GenericaDesktop.ShowAlerta("A forma de pagamento " + forma + " é inválida para esta operação, para gerar faturas a receber deve ser informado apenas Crediário, Cheque ou Boleto");
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

        private void btnPesquisaPlanoContas_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                string sqlAdicional = "";
                if (!String.IsNullOrEmpty(txtPlanoContas.Texts) && String.IsNullOrEmpty(txtCodPlanoContas.Texts))
                    sqlAdicional = "and Tabela.Descricao like '%" + txtPlanoContas.Texts + "%'";
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", sqlAdicional))
                {
                    txtCodPlanoContas.Texts = "";
                    txtPlanoContas.Texts = "";
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
                            break;
                        case DialogResult.OK:
                            txtPlanoContas.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoContas.Texts = ((PlanoConta)objeto).Id.ToString();
                            txtDescricao.Focus();
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

        private void txtFormaPagamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisarFormaPagamento.PerformClick();
            }
        }

        private void txtPlanoContas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaPlanoContas.PerformClick();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
           if(validarCampos())
                inserirParcelas();
        }

        private bool validarCampos()
        {
            bool valido = true;
            if (String.IsNullOrEmpty(txtCodCliente.Texts) && String.IsNullOrEmpty(txtCliente.Texts))
            {
                GenericaDesktop.ShowAlerta("Selecione corretamente um cliente");
                txtCliente.Focus();
                valido = false;
            }
            if (!String.IsNullOrEmpty(txtValorTotal.Texts) & valido == true) 
            {
                if (decimal.Parse(txtValorTotal.Texts) <= 0)
                {
                    GenericaDesktop.ShowAlerta("Preencha o valor total, deve ser maior que 0,00");
                    txtValorTotal.Focus();
                    valido = false;
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Preencha o valor total");
            }
            if (String.IsNullOrEmpty(txtParcelas.Texts) & valido == true)
            {
                GenericaDesktop.ShowAlerta("Preencha a quantidade de parcelas corretamente");
                txtParcelas.Focus();
                valido = false;
            }
            if (String.IsNullOrEmpty(txtIntervalo.Texts) & valido == true)
            {
                txtIntervalo.Texts = "30";
            }
            if (String.IsNullOrEmpty(txtCodFormaPagamento.Texts) && String.IsNullOrEmpty(txtFormaPagamento.Texts) & valido == true)
            {
                GenericaDesktop.ShowAlerta("Selecione corretamente a forma de pagamento");
                txtFormaPagamento.Focus();
                valido = false;
            }
            if (String.IsNullOrEmpty(txtCodPlanoContas.Texts) && String.IsNullOrEmpty(txtPlanoContas.Texts) & valido == true)
            {
                GenericaDesktop.ShowAlerta("Selecione corretamente o plano de contas");
                txtPlanoContas.Focus();
                valido = false;
            }
            return valido;
        }
        private void inserirParcelas()
        {
            try
            {
                if (String.IsNullOrEmpty(txtParcelas.Texts))
                    txtParcelas.Texts = "1";
                dsParcelas.Tables[0].Clear();
                DateTime vencimento = DateTime.Parse(txtPrimeiroVencimento.Value.ToString());
                this.gridParcelas.DataSource = dsParcelas.Tables["Parcelas"];
                for (int i = 1; i <= int.Parse(txtParcelas.Texts); i++)
                {
                    if (i > 1)
                        vencimento = vencimento.AddDays(int.Parse(txtIntervalo.Texts));
                    int dia = DateTime.Parse(txtPrimeiroVencimento.Value.ToString()).Day;
                    if (chkVencimentoFixo.Checked == true)
                    {
                        if (vencimento.Day != dia)
                            vencimento = DateTime.Parse((dia + "/" + vencimento.Month + "/" + vencimento.Year + " 00:00:00").ToString());
                    }
                    System.Data.DataRow row = dsParcelas.Tables[0].NewRow();
                    row.SetField("PARCELA", i);
                    row.SetField("VENCIMENTO", vencimento.ToShortDateString());
                    decimal valorParcela = 0;
                    valorParcela = decimal.Parse(txtValorTotal.Texts) / decimal.Parse(txtParcelas.Texts);
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
                        if (valorCalculo > decimal.Parse(txtValorTotal.Texts))
                        {
                            decimal valorCorrecao = valorCalculo - decimal.Parse(txtValorTotal.Texts);
                            valorCorrecao = decimal.Parse(dataRowView.Row["Valor"].ToString()) - valorCorrecao;
                            gridParcelas.View.GetPropertyAccessProvider().SetValue(gridParcelas.GetRecordAtRowIndex(linhas), gridParcelas.Columns[2].MappingName, valorCorrecao);
                        }
                        else if (valorCalculo < decimal.Parse(txtValorTotal.Texts))
                        {
                            decimal valorCorrecao = decimal.Parse(txtValorTotal.Texts) - valorCalculo;
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (gridParcelas.RowCount > 0)
            {
                int i = 0;
                var records = gridParcelas.View.Records;
                foreach (var record in records)
                {
                    i++;
                    var dataRowView = record.Data as DataRowView;
                    ContaReceber contaReceber = new ContaReceber();
                    Pessoa cliente = new Pessoa();
                    cliente.Id = int.Parse(txtCodCliente.Texts);
                    cliente = (Pessoa)Controller.getInstance().selecionar(cliente);
                    if (cliente != null)
                    {
                        contaReceber.Cliente = cliente;
                        contaReceber.CnpjCliente = cliente.Cnpj;
                        contaReceber.Data = DateTime.Parse(txtDataEmissao.Value.ToString());
                        contaReceber.Descricao = "LANÇAMENTO DE CONTA A RECEBER";
                        if (!String.IsNullOrEmpty(txtDescricao.Texts))
                            contaReceber.Descricao = txtDescricao.Texts;
                        contaReceber.Documento = "CR" + txtNumeroDocumento.Texts + "/" + i;
                        contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                        if (cliente.EnderecoPrincipal != null)
                        {
                            contaReceber.EnderecoCliente = cliente.EnderecoPrincipal.Logradouro + ", " + cliente.EnderecoPrincipal.Numero + " " + cliente.EnderecoPrincipal.Complemento;
                        }
                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = int.Parse(txtCodFormaPagamento.Texts);
                        formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        contaReceber.FormaPagamento = formaPagamento;
                        contaReceber.Juro = 0;
                        contaReceber.Multa = 0;
                        contaReceber.NomeCliente = cliente.RazaoSocial;
                        contaReceber.Origem = "LANÇAMENTO DE CONTA A RECEBER";
                        contaReceber.Recebido = false;
                        contaReceber.Venda = null;
                        contaReceber.VendaFormaPagamento = null;
                        contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                        contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                        contaReceber.Parcela = dataRowView.Row["PARCELA"].ToString();
                        contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                        contaReceber.Concluido = true;
                        PlanoConta planoConta = new PlanoConta();
                        planoConta.Id = int.Parse(txtCodPlanoContas.Texts);
                        planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                        contaReceber.PlanoConta = planoConta;

                        Controller.getInstance().salvar(contaReceber);
                    }
                }
                GenericaDesktop.ShowInfo("Lançamento efetuado com sucesso");
                limparCampos();
            }
        }

        private void limparCampos()
        {
            txtCodCliente.Texts = "";
            txtCliente.Texts = "";
            txtCodFormaPagamento.Texts = "";
            txtCodPlanoContas.Texts = "";
            txtDescricao.Texts = "";
            txtFormaPagamento.Texts = "";
            txtIntervalo.Texts = "30";
            txtNumeroDocumento.Texts = "";
            txtParcelas.Texts = "1";
            txtPlanoContas.Texts = "";
            txtValorTotal.Texts = "";
            dsParcelas.Tables[0].Clear();
        }
        private void txtDataEmissao_Leave(object sender, EventArgs e)
        {
            txtPrimeiroVencimento.Value = DateTime.Parse(txtDataEmissao.Value.ToString()).AddMonths(1);
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void gridParcelas_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
            if (e.Column.MappingName == "VENCIMENTO")
            {
                try
                {
                    DateTime dataValida = DateTime.Parse(e.NewValue.ToString());
                    e.IsValid = true;
                }
                catch
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Data Inválida, digite no formato 00/00/0000";
                }
            }
        }
    }
}
