using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmCheque : Form
    {
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        GenericaDesktop generica = new GenericaDesktop();
        FormaPagamento fp = new FormaPagamento();
        IList<Cheque> listaCheque = new List<Cheque>();
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        OrdemServico ordemServico = new OrdemServico();
        IList<ContaPagar> listaPagar = new List<ContaPagar>();
        ContaBancaria contaBancaria1 = new ContaBancaria();
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

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref IList<Cheque> listaCheque)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                listaCheque = this.listaCheque;
                valorRecebido = this.valor;
            }
            return DialogResult;
        }
        public DialogResult showModalPagar(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref IList<Cheque> listaCheque, ref ContaBancaria contaBancaria)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                listaCheque = this.listaCheque;
                valorRecebido = this.valor;
                contaBancaria = this.contaBancaria1;
            }
            return DialogResult;
        }
        public FrmCheque(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();
            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
        }

        public FrmCheque(decimal valorFaltante, IList<ContaReceber> listaReceber, OrdemServico ordemServico, IList<ContaPagar> listaPagar)
        {
            InitializeComponent();
            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.listaReceber = listaReceber;
            this.ordemServico = ordemServico;
            this.listaPagar = listaPagar;
        }

        private void FrmCheque_Paint(object sender, PaintEventArgs e)
        {
            if (String.IsNullOrEmpty(txtValor.Texts))
            {
                txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
                txtValor.Focus();
                txtParcelas.Texts = "1";
                txtDataVencimento.Value = DateTime.Now.AddMonths(1);

                if (venda.Id > 0)
                {
                    txtCpf.Texts = venda.Cliente.Cnpj;
                    txtRazaoSocial.Texts = venda.Cliente.RazaoSocial;
                }
                else
                {
                    txtCpf.Enabled = true;
                    txtRazaoSocial.Enabled = true;
                }
                if (listaPagar.Count > 0)
                {
                    txtDataVencimento.Value = DateTime.Now;
                    txtDataVencimento.Enabled = false;
                    txtAgencia.Enabled = false;
                    txtConta.Enabled = false;
                    txtDvConta.Enabled = false;
                    btnPesquisaBanco.Enabled = false;
                    txtCpf.Enabled = false;
                    txtRazaoSocial.Enabled = false;
                    txtCodBanco.Enabled = false;
                    txtBanco.Enabled = false;
                    ContaBancariaController contaBancariaController = new ContaBancariaController();
                    IList<ContaBancaria> listaConta = new List<ContaBancaria>();
                    listaConta = contaBancariaController.selecionarTodasContasPorFilial(Sessao.empresaFilialLogada.Id);
                    if (listaConta.Count == 1)
                    {
                        foreach(ContaBancaria contaBancaria in listaConta)
                        {
                            txtAgencia.Texts = contaBancaria.Agencia;
                            txtConta.Texts = contaBancaria.Conta;
                            txtDvConta.Texts = contaBancaria.DvConta;
                            txtBanco.Texts = contaBancaria.Banco.Descricao;
                            txtCodBanco.Texts = contaBancaria.Banco.Id.ToString();
                            txtCpf.Texts = Sessao.empresaFilialLogada.Cnpj;
                            txtRazaoSocial.Texts = Sessao.empresaFilialLogada.RazaoSocial;
                            contaBancaria1 = contaBancaria;
                        }
                    }
                    else if (listaConta.Count > 1)
                    {
                        Object contaObjeto = new ContaBancaria();
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
                                switch (uu.showModal("ContaBancaria", "", ref contaObjeto))
                                {
                                    case DialogResult.Ignore:
                                        uu.Dispose();
                                        break;
                                    case DialogResult.OK:
                                        txtAgencia.Texts = ((ContaBancaria)contaObjeto).Agencia;
                                        txtConta.Texts = ((ContaBancaria)contaObjeto).Conta;
                                        txtDvConta.Texts = ((ContaBancaria)contaObjeto).DvConta;
                                        txtBanco.Texts = ((ContaBancaria)contaObjeto).Banco.Descricao;
                                        txtCodBanco.Texts = ((ContaBancaria)contaObjeto).Banco.Id.ToString();
                                        txtCpf.Texts = Sessao.empresaFilialLogada.Cnpj;
                                        txtRazaoSocial.Texts = Sessao.empresaFilialLogada.RazaoSocial;
                                        contaBancaria1 = ((ContaBancaria)contaObjeto);
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
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();
            if (validaValorGrid())
            {
                if (!String.IsNullOrEmpty(txtValor.Texts) && !String.IsNullOrEmpty(txtCodBanco.Texts))
                {
                    if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                    {
                        if (venda.Id > 0)
                        {
                            this.valor = decimal.Parse(txtValor.Texts);
                            FormaPagamento formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 7;
                            vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            vendaFormaPagamento.Parcelamento = int.Parse(txtParcelas.Texts);
                            vendaFormaPagamento.ValorRecebido = valor;
                            vendaFormaPagamento.Cartao = false;
                            vendaFormaPagamento.AutorizacaoCartao = "";
                            vendaFormaPagamento.Venda = venda;
                            vendaFormaPagamento.TipoCartao = "";
                            vendaFormaPagamento.ParcelamentoFk = null;
                            Controller.getInstance().salvar(vendaFormaPagamento);

                            var records = gridParcelas.View.Records;
                            foreach (var record in records)
                            {
                                var dataRowView = record.Data as DataRowView;

                                Cheque cheque = new Cheque();
                                cheque.Descricao = "CHEQUE DA VENDA " + venda.Id;
                                cheque.Agencia = txtAgencia.Texts;
                                cheque.Cnpj = txtCpf.Texts;
                                cheque.Conta = txtConta.Texts;
                                cheque.DvConta = txtDvConta.Texts; 
                                cheque.NumeroCheque = dataRowView.Row["NUMEROCHEQUE"].ToString();
                                cheque.Parcela = dataRowView.Row["PARCELA"].ToString();
                                cheque.RazaoSocial = txtRazaoSocial.Texts;
                                cheque.Valor = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                cheque.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                Banco banco = new Banco();
                                banco.Id = int.Parse(txtCodBanco.Texts);
                                banco = (Banco)Controller.getInstance().selecionar(banco);
                                cheque.Banco = banco;
                                cheque.EmpresaFilial = Sessao.empresaFilialLogada;
                                cheque.Venda = venda;
                                cheque.Cliente = venda.Cliente;
                                cheque.Concluido = false;
                                Controller.getInstance().salvar(cheque);
                                Controller.getInstance().salvar(banco);

                                    ContaReceber contaReceber = new ContaReceber();
                                    contaReceber.Id = 0;
                                    contaReceber.NomeCliente = venda.Cliente.RazaoSocial;
                                    contaReceber.CnpjCliente = venda.Cliente.Cnpj;
                                    contaReceber.Data = DateTime.Now;
                                    contaReceber.Descricao = "VENDA - " + venda.Id + " - CHEQUE";
                                    contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                                    contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
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
                                

                                Caixa caixa = new Caixa();
                                caixa.Conciliado = false;
                                caixa.IdOrigem = venda.Id.ToString();
                                caixa.ContaBancaria = null;
                                caixa.DataLancamento = DateTime.Now;
                                caixa.Descricao = "RECEBIMENTO VENDA " + venda.Id + " NO CHEQUE";
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = vendaFormaPagamento.FormaPagamento;
                                if (venda.PlanoConta != null)
                                    caixa.PlanoConta = venda.PlanoConta;
                                else
                                    caixa.PlanoConta = null;
                                caixa.TabelaOrigem = "VENDA";
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = vendaFormaPagamento.ValorRecebido;
                                caixa.Pessoa = null;
                                if (venda.Cliente != null)
                                    caixa.Pessoa = venda.Cliente;
                                caixa.ContaBancaria = null;
                                caixa.Concluido = false;
                                Controller.getInstance().salvar(caixa);

                            }
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.valor = decimal.Parse(txtValor.Texts);
                            Pessoa pessoa = new Pessoa();
                            PessoaController pessoaController = new PessoaController();
                            pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(GenericaDesktop.RemoveCaracteres(txtCpf.Texts.Trim()));
                            if (pessoa != null)
                            {
                                if (pessoa.Id > 0)
                                {
                                    fp.Id = 7;
                                    fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                                    var records = gridParcelas.View.Records;
                                    foreach (var record in records)
                                    {
                                        var dataRowView = record.Data as DataRowView;

                                        Cheque cheque = new Cheque();
                                        cheque.Descricao = "CHEQUE RECEBIMENTO";
                                        cheque.Agencia = txtAgencia.Texts;
                                        cheque.Cnpj = txtCpf.Texts;
                                        cheque.Conta = txtConta.Texts;
                                        cheque.DvConta = txtDvConta.Texts;
                                        cheque.NumeroCheque = dataRowView.Row["NUMEROCHEQUE"].ToString();
                                        cheque.Parcela = dataRowView.Row["PARCELA"].ToString();
                                        cheque.RazaoSocial = txtRazaoSocial.Texts;
                                        cheque.Valor = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                        cheque.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                        Banco banco = new Banco();
                                        banco.Id = int.Parse(txtCodBanco.Texts);
                                        banco = (Banco)Controller.getInstance().selecionar(banco);
                                        cheque.Banco = banco;
                                        cheque.EmpresaFilial = Sessao.empresaFilialLogada;
                                        cheque.Venda = null;

                                        cheque.Cliente = pessoa;
                                        cheque.Concluido = false;
                                        listaCheque.Add(cheque);
                                    }
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    GenericaDesktop.ShowAlerta("Digite CPF/CNPJ de um cliente já cadastrado no sistema ou cadastre um novo cliente");
                                    selecionarCliente();
                                }
                            }
                            else if (Sessao.empresaFilialLogada.Cnpj.Equals(GenericaDesktop.RemoveCaracteres(txtCpf.Texts)))
                            {
                                fp.Id = 7;
                                fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                                var records = gridParcelas.View.Records;
                                foreach (var record in records)
                                {
                                    var dataRowView = record.Data as DataRowView;

                                    Cheque cheque = new Cheque();
                                    cheque.Descricao = "CHEQUE PAGAMENTO";
                                    cheque.Agencia = txtAgencia.Texts;
                                    cheque.Cnpj = txtCpf.Texts;
                                    cheque.Conta = txtConta.Texts;
                                    cheque.DvConta = txtDvConta.Texts;
                                    cheque.NumeroCheque = dataRowView.Row["NUMEROCHEQUE"].ToString();
                                    cheque.Parcela = dataRowView.Row["PARCELA"].ToString();
                                    cheque.RazaoSocial = txtRazaoSocial.Texts;
                                    cheque.Valor = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                                    cheque.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                                    Banco banco = new Banco();
                                    banco.Id = int.Parse(txtCodBanco.Texts);
                                    banco = (Banco)Controller.getInstance().selecionar(banco);
                                    cheque.Banco = banco;
                                    cheque.EmpresaFilial = Sessao.empresaFilialLogada;
                                    cheque.Venda = null;

                                    cheque.Cliente = null;
                                    cheque.Concluido = false;
                                    listaCheque.Add(cheque);
                                }
                                this.DialogResult = DialogResult.OK;
                            }
                            else
                            {
                                GenericaDesktop.ShowAlerta("Digite CPF/CNPJ de um cliente já cadastrado no sistema ou cadastre um novo cliente");
                                selecionarCliente();
                            }
                        }
                            
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("O valor recebido no PIX não pode ser maior que o valor faltante!");
                        txtValor.Select();
                    }
                }
                else
                {
                    GenericaDesktop.ShowErro("Os campos valor recebido e banco são obrigatórios");
                }
            }
            else
            {
                GenericaDesktop.ShowErro("A soma dos cheques parcelados ficou diferente do valor recebido, confira os valores de cada cheque!");
            }
        }

        private void selecionarCliente()
        {
            Pessoa pessoaOjeto = new Pessoa();
            Object pessoaOjeto1 = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(""))
                {
                    txtCpf.Texts = "";
                    txtRazaoSocial.Texts = "";
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
                            if (form.showModalNovo(ref pessoaOjeto1) == DialogResult.OK)
                            {
                                txtRazaoSocial.Texts = ((Pessoa)pessoaOjeto1).RazaoSocial;
                                txtCpf.Texts = ((Pessoa)pessoaOjeto1).Cnpj.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtRazaoSocial.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCpf.Texts = ((Pessoa)pessoaOjeto).Cnpj.ToString();
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
        private bool validaValorGrid()
        {
            decimal valor = 0;
            var records = gridParcelas.View.Records;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;
                valor = valor + decimal.Parse(dataRowView.Row["VALOR"].ToString());
            }
            if (valor != decimal.Parse(txtValor.Texts))
                return false;
            else
                return true;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaBanco_Click(object sender, EventArgs e)
        {
            Object bancoObjeto = new Banco();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Banco", ""))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("Banco", "", ref bancoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmBanco form = new FrmClienteCadastro();
                            //if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            //{
                            //    txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            //    txtClienteAbaPagamento.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            //    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            //    txtPesquisaProduto.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtBanco.Texts = ((Banco)bancoObjeto).Descricao;
                            txtCodBanco.Texts = ((Banco)bancoObjeto).Id.ToString();
                            txtAgencia.Focus();
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

        private void inserirParcelas()
        {
            try
            {
                Int64 numeroCheque = 0;
                if (!String.IsNullOrEmpty(txtNumeroCheque.Texts))
                    numeroCheque = Int64.Parse(txtNumeroCheque.Texts);

                if (String.IsNullOrEmpty(txtParcelas.Texts))
                    txtParcelas.Texts = "1";
                dsParcelas.Tables[0].Clear();
                DateTime vencimento = DateTime.Parse(txtDataVencimento.Value.ToString());
                this.gridParcelas.DataSource = dsParcelas.Tables["Parcelas"];
                for (int i = 1; i <= int.Parse(txtParcelas.Texts); i++)
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
                    valorParcela = decimal.Parse(txtValor.Texts) / decimal.Parse(txtParcelas.Texts);
                    row.SetField("VALOR", string.Format("{0:0.00}", valorParcela));
                    row.SetField("NUMEROCHEQUE", numeroCheque);
                    numeroCheque++;
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
                        if (valorCalculo > decimal.Parse(txtValor.Texts))
                        {
                            decimal valorCorrecao = valorCalculo - decimal.Parse(txtValor.Texts);
                            valorCorrecao = decimal.Parse(dataRowView.Row["Valor"].ToString()) - valorCorrecao;
                            gridParcelas.View.GetPropertyAccessProvider().SetValue(gridParcelas.GetRecordAtRowIndex(linhas), gridParcelas.Columns[2].MappingName, valorCorrecao);
                        }
                        else if (valorCalculo < decimal.Parse(txtValor.Texts))
                        {
                            decimal valorCorrecao = decimal.Parse(txtValor.Texts) - valorCalculo;
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

        private void btnConfirmarCheques_Click(object sender, EventArgs e)
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

        private void FrmCheque_KeyDown(object sender, KeyEventArgs e)
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

        private void txtBanco_Click(object sender, EventArgs e)
        {
            btnPesquisaBanco.PerformClick();
        }

        private void txtCpf_Leave(object sender, EventArgs e)
        {
            pesquisarClientePorCNPJ();
        }

        private void pesquisarClientePorCNPJ()
        {
            PessoaController pessoaController = new PessoaController();
            Pessoa pessoa = new Pessoa();
            pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(GenericaDesktop.RemoveCaracteres(txtCpf.Texts.Trim()));
            if (pessoa != null)
            {
                if (pessoa.Id > 0)
                {
                    txtRazaoSocial.Texts = pessoa.RazaoSocial;
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Cliente não encontrado, para receber com cheque você precisa selecionar um cliente com cadastro!");
                selecionarCliente();
            }
        }
    }
}
