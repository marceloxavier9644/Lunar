using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmDinheiro : Form
    {
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        FormaPagamento fp = new FormaPagamento();
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        Venda venda = new Venda();
        GenericaDesktop generica = new GenericaDesktop();
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        IList<ContaPagar> listaPagar = new List<ContaPagar>();
        OrdemServico ordemServico = new OrdemServico();
        bool passou = false;
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
        public FrmDinheiro(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();
            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;

            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
        }

        public FrmDinheiro(decimal valorFaltante, IList<ContaReceber> listaReceber, OrdemServico ordemServico, IList<ContaPagar> listaPagar)
        {
            InitializeComponent();
            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;

            txtValor.TextAlign = HorizontalAlignment.Center;
            this.listaReceber = listaReceber;
            this.ordemServico = ordemServico;
            this.listaPagar = listaPagar;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            
            {
                vendaFormaPagamento = new VendaFormaPagamento();
                fp = new FormaPagamento();
                fp.Id = 1;

                //Venda
                if (!String.IsNullOrEmpty(txtValor.Texts) && venda.Id > 0)
                {
                    if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                        vendaFormaPagamento.ValorRecebido = decimal.Parse(txtValor.Texts);
                    else
                    vendaFormaPagamento.ValorRecebido = valorFaltante;
                    vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(fp);
                    vendaFormaPagamento.AdquirenteCartao = null;
                    vendaFormaPagamento.BandeiraCartao = null;
                    vendaFormaPagamento.AutorizacaoCartao = "";
                    vendaFormaPagamento.Cartao = false;
                    vendaFormaPagamento.AdquirenteCartao = null;
                    vendaFormaPagamento.Venda = venda;
                    vendaFormaPagamento.TipoCartao = "";
                    vendaFormaPagamento.ParcelamentoFk = null;
                    vendaFormaPagamento.Troco = decimal.Parse(txtValor.Texts) - this.valorFaltante;
                    Controller.getInstance().salvar(vendaFormaPagamento);

                    Caixa caixa = new Caixa();
                    caixa.Conciliado = false;
                    caixa.IdOrigem = venda.Id.ToString();
                    caixa.ContaBancaria = null;
                    caixa.DataLancamento = DateTime.Now;
                    caixa.Descricao = "RECEBIMENTO VENDA " + venda.Id + " EM DINHEIRO";
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
                //Recebendo Parcelas do contas a receber
                if(listaReceber != null && !String.IsNullOrEmpty(txtValor.Texts))
                {
                    if(listaReceber.Count > 0)
                    {
                        fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                        if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                            this.valor = decimal.Parse(txtValor.Texts);
                        else
                            this.valor = valorFaltante;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                //Pagando Parcelas do contas a pagar
                if (listaPagar != null && !String.IsNullOrEmpty(txtValor.Texts))
                {
                    if (listaPagar.Count > 0)
                    {
                        fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                        if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                            this.valor = decimal.Parse(txtValor.Texts);
                        else
                            this.valor = valorFaltante;
                        this.DialogResult = DialogResult.OK;
                    }
                }

                //Ordem de Serviço
                if (ordemServico.Id > 0)
                {
                    fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                    if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                        this.valor = decimal.Parse(txtValor.Texts);
                    else
                        this.valor = valorFaltante;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha na confirmação do pagamento em dinheiro: " + erro.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Ignore;
            //txtValor.Texts = "0,00";
            //this.Close();
        }

        private void txtValor__TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtValor.Texts) > this.valorFaltante)
                {
                    lblTroco.Visible = true;
                    lblTroco.Text = "Troco: " + (decimal.Parse(txtValor.Texts) - this.valorFaltante).ToString("C2", CultureInfo.CurrentCulture);

                    int x = (this.Size.Width - lblTroco.Width) / 2;
                    //int y = (this.Size.Height - lblTroco.Height) / 3;
                    lblTroco.Location = new Point(x, 272);
                }
                else
                {
                    lblTroco.Text = "Troco: ";
                    lblTroco.Visible = false;
                }
            }
            catch
            {

            }
        }

        private void FrmDinheiro_Load(object sender, EventArgs e)
        {

        }

        private void FrmDinheiro_Paint(object sender, PaintEventArgs e)
        {
            int x = (this.Size.Width - lblFaltante.Width) / 2;
            lblFaltante.Location = new Point(x, 67);
            if (passou == false)
            {
                txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
                txtValor.Focus();
                passou = true;
            }
        }

        private void FrmDinheiro_KeyDown(object sender, KeyEventArgs e)
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
