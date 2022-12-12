using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmDeposito : Form
    {
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        ContaBancariaController ContaBancariaController = new ContaBancariaController();
        bool contaNulo = false;
        GenericaDesktop generica = new GenericaDesktop();
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        DateTime dataDeposito = new DateTime();
        ContaBancaria contaBancaria = new ContaBancaria();
        FormaPagamento fp = new FormaPagamento();
        OrdemServico ordemServico = new OrdemServico();
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

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref ContaBancaria contaBancaria, ref DateTime dataDeposito)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                valorRecebido = this.valor;
                contaBancaria = this.contaBancaria;
                dataDeposito = this.dataDeposito;
            }
            return DialogResult;
        }
        public FrmDeposito(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();

            IList<ContaBancaria> lista = ContaBancariaController.selecionarTodasContas();
            if (lista.Count > 0)
            {
                foreach (ContaBancaria contaBancaria in lista)
                {
                    DataRow drs = dsConta.Tables[0].NewRow();
                    drs.SetField("ID", contaBancaria.Id);
                    drs.SetField("CONTA", contaBancaria.Descricao);
                    dsConta.Tables[0].Rows.Add(drs);
                }
                comboBanco.DisplayMember = "CONTA";
                comboBanco.ValueMember = "ID";
                comboBanco.DataSource = dsConta.Tables[0];
                comboBanco.SelectedIndex = 0;
            }
            else
            {
                contaNulo = true;
                GenericaDesktop.ShowAlerta("Para utilizar a opção depósito/transferência você deve cadastrar a conta bancária da empresa!");
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
        }

        public FrmDeposito(decimal valorFaltante, IList<ContaReceber> listaReceber, OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
            this.listaReceber = listaReceber;
            IList<ContaBancaria> lista = ContaBancariaController.selecionarTodasContas();
            if (lista.Count > 0)
            {
                foreach (ContaBancaria contaBancaria in lista)
                {
                    DataRow drs = dsConta.Tables[0].NewRow();
                    drs.SetField("ID", contaBancaria.Id);
                    drs.SetField("CONTA", contaBancaria.Descricao);
                    dsConta.Tables[0].Rows.Add(drs);
                }
                comboBanco.DisplayMember = "CONTA";
                comboBanco.ValueMember = "ID";
                comboBanco.DataSource = dsConta.Tables[0];
                comboBanco.SelectedIndex = 0;
            }
            else
            {
                contaNulo = true;
                GenericaDesktop.ShowAlerta("Para utilizar a opção depósito/transferência você deve cadastrar a conta bancária da empresa!");
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
        }

        private void FrmDeposito_Paint(object sender, PaintEventArgs e)
        {
            if (contaNulo == true)
                this.Close();

            txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
            txtValor.Focus();
        }

        private void btnConfirmar_Click(object sender, System.EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();

            if (!String.IsNullOrEmpty(txtValor.Texts))
            {
                if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                {
                    if (venda.Id > 0)
                    {
                        this.valor = decimal.Parse(txtValor.Texts);
                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = 4;
                        vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        vendaFormaPagamento.Parcelamento = 0;
                        vendaFormaPagamento.ValorRecebido = valor;

                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(comboBanco.SelectedValue.ToString());
                        vendaFormaPagamento.ContaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        vendaFormaPagamento.TipoCartao = "";
                        vendaFormaPagamento.Cartao = false;
                        vendaFormaPagamento.AutorizacaoCartao = "";
                        vendaFormaPagamento.Venda = venda;
                        vendaFormaPagamento.ParcelamentoFk = null;
                        Controller.getInstance().salvar(vendaFormaPagamento);

                        Caixa caixa = new Caixa();
                        caixa.Conciliado = false;
                        caixa.IdOrigem = venda.Id.ToString();
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Parse(txtData.Value.ToString());
                        caixa.Descricao = "RECEBIMENTO VENDA VIA DEPOSITO/TRANSF.";
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
                        Controller.getInstance().salvar(caixa);

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        fp.Id = 4;
                        fp= (FormaPagamento)Controller.getInstance().selecionar(fp);
                       
                        contaBancaria.Id = int.Parse(comboBanco.SelectedValue.ToString());
                        contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);

                        this.dataDeposito = DateTime.Parse(txtData.Value.ToString());
                        this.valor = decimal.Parse(txtValor.Texts);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor recebido no DEPÓSITO não pode ser maior que o valor faltante!");
                    txtValor.Select();
                }

            }
            else
            {
                GenericaDesktop.ShowErro("Informe o valor recebido!");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            txtValor.Texts = "0,00";
            this.Close();
        }

        private void FrmDeposito_KeyDown(object sender, KeyEventArgs e)
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

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtValor.Texts, e);
        }
    }
}
