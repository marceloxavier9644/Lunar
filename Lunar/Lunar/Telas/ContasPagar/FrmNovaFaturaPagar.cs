﻿using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Empresas;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.ContasPagar
{
    public partial class FrmNovaFaturaPagar : Form
    {
        EmpresaFilialController empresaFilialController = new EmpresaFilialController();
        GenericaDesktop generica = new GenericaDesktop();
        bool showModal = false;
        IList<ContaPagar> listaContaPagar = new List<ContaPagar>();
        public DialogResult showModalNovo(ref IList<ContaPagar> listaContaPagar)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                listaContaPagar = this.listaContaPagar;
            }
            return DialogResult;
        }
        public FrmNovaFaturaPagar()
        {
            InitializeComponent();
            txtPrimeiroVencimento.Value = DateTime.Now.AddMonths(1);
            txtEmpresa.Texts = Sessao.empresaFilialLogada.NomeFantasia;
            txtCodEmpresa.Texts = Sessao.empresaFilialLogada.Id.ToString();
            txtClienteFornecedor.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaClienteFornecedor_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                string sqlAdicional = "";
                if (!String.IsNullOrEmpty(txtClienteFornecedor.Texts) && String.IsNullOrEmpty(txtCodClienteFornecedor.Texts))
                    sqlAdicional = "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtClienteFornecedor.Texts + "%'";
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", sqlAdicional))
                {
                    txtCodClienteFornecedor.Texts = "";
                    txtClienteFornecedor.Texts = "";
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
                                txtClienteFornecedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodClienteFornecedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                txtNumeroDocumento.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtClienteFornecedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodClienteFornecedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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

        private void txtCodClienteFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodClienteFornecedor.Texts))
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = int.Parse(txtCodClienteFornecedor.Texts.Trim());
                        pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                        if (pessoa != null)
                        {
                            txtClienteFornecedor.Texts = pessoa.RazaoSocial;
                            txtCodClienteFornecedor.Texts = pessoa.Id.ToString();
                            txtNumeroDocumento.Focus();
                        }
                    }
                }
                catch
                {
                    GenericaDesktop.ShowAlerta("Código inválido");
                    txtCodClienteFornecedor.Texts = "";
                    txtClienteFornecedor.Texts = "";

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
                            if (((FormaPagamento)formaPagamentoOjeto).Banco == true || ((FormaPagamento)formaPagamentoOjeto).Caixa == true || ((FormaPagamento)formaPagamentoOjeto).Cartao == true || ((FormaPagamento)formaPagamentoOjeto).CreditoCliente == true)
                            {
                                String forma = ((FormaPagamento)formaPagamentoOjeto).Descricao;
                                txtFormaPagamento.Texts = "";
                                txtCodFormaPagamento.Texts = "";
                                GenericaDesktop.ShowAlerta("A forma de pagamento " + forma + " é inválida para esta operação, para gerar faturas a pagar deve ser informado apenas Crediário, Cheque ou Boleto");
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

        private void txtClienteFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaClienteFornecedor.PerformClick();
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
                inserirParcelas();
        }
        private bool validarCampos()
        {
            bool valido = true;
            if (String.IsNullOrEmpty(txtCodClienteFornecedor.Texts) && String.IsNullOrEmpty(txtClienteFornecedor.Texts))
            {
                GenericaDesktop.ShowAlerta("Selecione corretamente um cliente/fornecedor");
                txtClienteFornecedor.Focus();
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
                    ContaPagar contaPagar = new ContaPagar();
                    Pessoa cliente = new Pessoa();
                    cliente.Id = int.Parse(txtCodClienteFornecedor.Texts);
                    cliente = (Pessoa)Controller.getInstance().selecionar(cliente);
                    if (cliente != null)
                    {
                        contaPagar.Pessoa = cliente;
                        //contaPagar. = cliente.Cnpj;
                        contaPagar.DataOrigem = DateTime.Parse(txtDataEmissao.Value.ToString());
                        contaPagar.Descricao = "LANÇAMENTO DE CONTA A PAGAR";
                        if (!String.IsNullOrEmpty(txtDescricao.Texts))
                            contaPagar.Historico = txtDescricao.Texts;
                        contaPagar.NumeroDocumento = "CP" + txtNumeroDocumento.Texts + "/" + i;
                        EmpresaFilial empresaFilial = new EmpresaFilial();
                        if (!String.IsNullOrEmpty(txtCodEmpresa.Texts))
                        {
                            empresaFilial.Id = int.Parse(txtCodEmpresa.Texts);
                            empresaFilial = (EmpresaFilial)empresaFilialController.selecionar(empresaFilial);
                            contaPagar.EmpresaFilial = empresaFilial;
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Você não selecionou a sua empresa corretamente, a fatura será gravada para empresa logada " + Sessao.empresaFilialLogada.NomeFantasia);
                            contaPagar.EmpresaFilial = Sessao.empresaFilialLogada;
                        }

                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = int.Parse(txtCodFormaPagamento.Texts);
                        formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        contaPagar.FormaPagamento = formaPagamento;
                        contaPagar.Pago = false;
                        contaPagar.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                        contaPagar.DVenc = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                        contaPagar.NDup = i.ToString();

                        PlanoConta planoConta = new PlanoConta();
                        planoConta.Id = int.Parse(txtCodPlanoContas.Texts);
                        planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                        contaPagar.PlanoConta = planoConta;

                        Controller.getInstance().salvar(contaPagar);
                    }
                }
                GenericaDesktop.ShowInfo("Lançamento efetuado com sucesso");
                limparCampos();
            }
        }

        private void limparCampos()
        {
            txtCodClienteFornecedor.Texts = "";
            txtClienteFornecedor.Texts = "";
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

        private void btnPesquisaEmpresa_Click(object sender, EventArgs e)
        {
            Object objeto = new EmpresaFilial();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("EmpresaFilial", ""))
                {
                    txtCodEmpresa.Texts = "";
                    txtEmpresa.Texts = "";
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
                    switch (uu.showModal("EmpresaFilial", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmCadastroEmpresas form = new FrmCadastroEmpresas();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                                txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                            txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
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

        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EmpresaFilial empresaFilial = new EmpresaFilial();
                if (!String.IsNullOrEmpty(txtEmpresa.Texts))
                {
                    if (generica.eNumero(txtEmpresa.Texts))
                    {
                        empresaFilial.Id = int.Parse(txtEmpresa.Texts);
                        try
                        {
                            empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                            txtEmpresa.Texts = empresaFilial.NomeFantasia;
                            txtCodEmpresa.Texts = empresaFilial.Id.ToString();
                            txtClienteFornecedor.Focus();
                        }
                        catch
                        {
                            empresaFilial = null;
                            txtCodEmpresa.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhuma empresa encontrada");
                            txtEmpresa.SelectAll();
                        }
                    }
                    else if (empresaFilial == null || generica.eNumero(txtEmpresa.Texts) == false)
                    {
                        IList<EmpresaFilial> lista = empresaFilialController.selecionarEmpresaFilialComVariosFiltros(txtEmpresa.Texts);
                        if (lista.Count == 1)
                        {
                            foreach (EmpresaFilial emp in lista)
                            {
                                empresaFilial = emp;
                                txtEmpresa.Texts = emp.NomeFantasia;
                                txtCodEmpresa.Texts = emp.Id.ToString();
                                txtClienteFornecedor.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaEmpresa.PerformClick();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nenhuma empresa encontrada");
                    }
                }
                else
                {
                    btnPesquisaEmpresa.PerformClick();
                }
            }
        }
    }
}
