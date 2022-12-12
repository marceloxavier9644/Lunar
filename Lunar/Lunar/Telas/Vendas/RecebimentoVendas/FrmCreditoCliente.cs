using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmCreditoCliente : Form
    {
        bool passou = false;
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        decimal creditoDisponivel = 0;
        OrdemServico ordemServico = new OrdemServico();
        GenericaDesktop generica = new GenericaDesktop();
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        FormaPagamento fp = new FormaPagamento();
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

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                valorRecebido = this.valor;
            }
            return DialogResult;
        }
        public FrmCreditoCliente(decimal valorFaltante, Venda venda, decimal creditoDisponivel)
        {
            InitializeComponent();          
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
            this.creditoDisponivel = creditoDisponivel;
        }
        public FrmCreditoCliente(decimal valorFaltante, IList<ContaReceber> listaReceber, decimal creditoDisponivel, OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            lblCreditoDisponivel.Text = "Disponível " + creditoDisponivel.ToString("C2", CultureInfo.CurrentCulture);
            this.creditoDisponivel = creditoDisponivel;
            this.listaReceber = listaReceber;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCreditoCliente_KeyDown(object sender, KeyEventArgs e)
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();
            if (!String.IsNullOrEmpty(txtValor.Texts))
            {
                if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                {
                    if (creditoDisponivel >= decimal.Parse(txtValor.Texts))
                    {
                        if (venda.Id > 0)
                        {
                            this.valor = decimal.Parse(txtValor.Texts);
                            FormaPagamento formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 8;
                            vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            vendaFormaPagamento.Parcelamento = 0;
                            vendaFormaPagamento.ValorRecebido = valor;
                            vendaFormaPagamento.ContaBancaria = null;
                            vendaFormaPagamento.Cartao = false;
                            vendaFormaPagamento.AutorizacaoCartao = "";
                            vendaFormaPagamento.Venda = venda;
                            vendaFormaPagamento.TipoCartao = "";
                            vendaFormaPagamento.ParcelamentoFk = null;
                            Controller.getInstance().salvar(vendaFormaPagamento);

                            this.DialogResult = DialogResult.OK;
                        }
                        //Recebendo Parcelas // Pagando
                        else
                        {
                            this.fp.Id = 8;
                            this.fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                            this.valor = decimal.Parse(txtValor.Texts);
                            this.DialogResult = DialogResult.OK;
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
                    GenericaDesktop.ShowAlerta("Você não pode utilizar um valor maior que o valor de crédito disponível, confira o valor digitado!");
                }

            }
            else
            {
                GenericaDesktop.ShowErro("Informe o valor recebido!");
            }
        }
    }
}
