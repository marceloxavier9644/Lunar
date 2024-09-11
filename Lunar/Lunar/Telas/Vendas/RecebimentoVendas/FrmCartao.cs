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
    public partial class FrmCartao : Form
    {
        bool passou = false;
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        BandeiraCartaoController bandeiraCartaoController = new BandeiraCartaoController();
        AdquirenteCartaoController adquirenteCartaoController = new AdquirenteCartaoController();
        Venda venda = new Venda();
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        FormaPagamento fp = new FormaPagamento();
        BandeiraCartao bandeiraCartao = new BandeiraCartao();
        Parcelamento parcelamento = new Parcelamento();
        String autorizacao = "";
        AdquirenteCartao adquirente = new AdquirenteCartao();
        OrdemServico ordemServico = new OrdemServico();
        String tipoCartao = "";
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

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref BandeiraCartao bandeiraCartao, ref Parcelamento parcelamento, ref string autorizacaoCartao, ref AdquirenteCartao adquirente, ref String tipoCartao)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                valorRecebido = this.valor;
                bandeiraCartao = this.bandeiraCartao;
                parcelamento = this.parcelamento;
                autorizacaoCartao = this.autorizacao;
                adquirente = this.adquirente;
                tipoCartao = this.tipoCartao;
            }
            return DialogResult;
        }
        public FrmCartao(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();

            radioCredito.Checked = true;
            IList<AdquirenteCartao> listaAdquirentes = adquirenteCartaoController.selecionarTodasAdquirentes();
            if (listaAdquirentes.Count > 0)
            {
                foreach (AdquirenteCartao adquirenteCartao in listaAdquirentes)
                {
                    DataRow drs = dsAdquirente.Tables[0].NewRow();
                    drs.SetField("Codigo", adquirenteCartao.Id);
                    drs.SetField("Adquirente", adquirenteCartao.Descricao);
                    dsAdquirente.Tables[0].Rows.Add(drs);
                }
                comboMaquininha.DisplayMember = "Adquirente";
                comboMaquininha.ValueMember = "Codigo";
                comboMaquininha.DataSource = dsAdquirente.Tables[0];
                comboMaquininha.SelectedIndex = 0;
            }



            IList<BandeiraCartao> listaBandeiras = bandeiraCartaoController.selecionarTodasBandeiras();
            if(listaBandeiras.Count > 0)
            {
                foreach(BandeiraCartao bandeira in listaBandeiras)
                {
                    DataRow drs = dsBandeira.Tables[0].NewRow();
                    drs.SetField("Codigo", bandeira.Id);
                    drs.SetField("Bandeira", bandeira.Descricao);
                    dsBandeira.Tables[0].Rows.Add(drs);
                }
                comboBandeiraCartao.DisplayMember = "Bandeira";
                comboBandeiraCartao.ValueMember = "Codigo";
                comboBandeiraCartao.DataSource = dsBandeira.Tables[0];
                comboBandeiraCartao.SelectedIndex = 0;
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;

            this.venda = venda;

            if (Sessao.caixaLogado != null)
            {
                if (Sessao.caixaLogado.Id > 0)
                    txtData.Value = Sessao.caixaLogado.DataAbertura;
            }
        }

        public FrmCartao(decimal valorFaltante, IList<ContaReceber> listaReceber, OrdemServico ordemServico)
        {
            InitializeComponent();

            radioCredito.Checked = true;
            this.ordemServico = ordemServico;
            IList<AdquirenteCartao> listaAdquirentes = adquirenteCartaoController.selecionarTodasAdquirentes();
            if (listaAdquirentes.Count > 0)
            {
                foreach (AdquirenteCartao adquirenteCartao in listaAdquirentes)
                {
                    DataRow drs = dsAdquirente.Tables[0].NewRow();
                    drs.SetField("Codigo", adquirenteCartao.Id);
                    drs.SetField("Adquirente", adquirenteCartao.Descricao);
                    dsAdquirente.Tables[0].Rows.Add(drs);
                }
                comboMaquininha.DisplayMember = "Adquirente";
                comboMaquininha.ValueMember = "Codigo";
                comboMaquininha.DataSource = dsAdquirente.Tables[0];
                comboMaquininha.SelectedIndex = 0;
            }
            IList<BandeiraCartao> listaBandeiras = bandeiraCartaoController.selecionarTodasBandeiras();
            if (listaBandeiras.Count > 0)
            {
                foreach (BandeiraCartao bandeira in listaBandeiras)
                {
                    DataRow drs = dsBandeira.Tables[0].NewRow();
                    drs.SetField("Codigo", bandeira.Id);
                    drs.SetField("Bandeira", bandeira.Descricao);
                    dsBandeira.Tables[0].Rows.Add(drs);
                }
                comboBandeiraCartao.DisplayMember = "Bandeira";
                comboBandeiraCartao.ValueMember = "Codigo";
                comboBandeiraCartao.DataSource = dsBandeira.Tables[0];
                comboBandeiraCartao.SelectedIndex = 0;
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;

            this.listaReceber = listaReceber;
            if (Sessao.caixaLogado != null)
            {
                if (Sessao.caixaLogado.Id > 0)
                    txtData.Value = Sessao.caixaLogado.DataAbertura;
            }
        }



        private void parcelas()
        {
            dsCondicao.Tables[0].Clear();
            IList<Parcelamento> lista = new List<Parcelamento>();
            ParcelamentoController parcelamentoController = new ParcelamentoController();
            
            if(radioCredito.Checked == true)
                lista = parcelamentoController.selecionarTodasCondicoesCredito();
            else
                lista = parcelamentoController.selecionarTodasCondicoesDebito();
            if (lista.Count > 0)
            {
                foreach (Parcelamento parcelamento in lista)
                {
                    DataRow drs = dsCondicao.Tables[0].NewRow();
                    drs.SetField("Codigo", parcelamento.Id);
                    drs.SetField("CondicaoRecebimento", parcelamento.Descricao);
                    dsCondicao.Tables[0].Rows.Add(drs);
                }
                comboCondicaoRecebimento.DisplayMember = "CondicaoRecebimento";
                comboCondicaoRecebimento.ValueMember = "Codigo";
                comboCondicaoRecebimento.DataSource = dsCondicao.Tables[0];
                comboCondicaoRecebimento.SelectedIndex = 0;
            }
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();

            if (!String.IsNullOrEmpty(txtValor.Texts))
            {
                if (venda.Id > 0)
                {
                    if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                    {
                        this.valor = decimal.Parse(txtValor.Texts);
                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = 2;
                        vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        parcelamento = new Parcelamento();
                        parcelamento.Id = int.Parse(comboCondicaoRecebimento.SelectedValue.ToString());
                        parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);
                        vendaFormaPagamento.ParcelamentoFk = parcelamento;
                        vendaFormaPagamento.Parcelamento = parcelamento.Parcelas;
                        vendaFormaPagamento.ValorRecebido = valor;

                        adquirente = new AdquirenteCartao();
                        adquirente.Id = int.Parse(comboMaquininha.SelectedValue.ToString());
                        vendaFormaPagamento.AdquirenteCartao = (AdquirenteCartao)Controller.getInstance().selecionar(adquirente);

                        bandeiraCartao = new BandeiraCartao();
                        bandeiraCartao.Id = int.Parse(comboBandeiraCartao.SelectedValue.ToString());
                        vendaFormaPagamento.BandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);

                        vendaFormaPagamento.Cartao = true;
                        tipoCartao = "";
                        if (radioDebito.Checked == true)
                            tipoCartao = "DÉBITO";
                        else
                            tipoCartao = "CRÉDITO";
                        vendaFormaPagamento.TipoCartao = tipoCartao;
                        vendaFormaPagamento.AutorizacaoCartao = txtAutorizacao.Texts;
                        vendaFormaPagamento.Venda = venda;
                        Controller.getInstance().salvar(vendaFormaPagamento);

                        Caixa caixa = new Caixa();
                        caixa.Conciliado = false;
                        caixa.IdOrigem = venda.Id.ToString();
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = "RECEBIMENTO VENDA " + venda.Id + " NO CARTÃO";
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

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("O valor recebido no cartão não pode ser maior que o valor faltante!");
                        txtValor.Select();
                    }

                }
                //Se nao for venda so retorna o valor para a tela anterior
                else
                {
                    if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                    {
                        adquirente = new AdquirenteCartao();
                        adquirente.Id = int.Parse(comboMaquininha.SelectedValue.ToString());
                        adquirente = (AdquirenteCartao)Controller.getInstance().selecionar(adquirente);

                        bandeiraCartao = new BandeiraCartao();
                        bandeiraCartao.Id = int.Parse(comboBandeiraCartao.SelectedValue.ToString());
                        bandeiraCartao = (BandeiraCartao)Controller.getInstance().selecionar(bandeiraCartao);

                        parcelamento = new Parcelamento();
                        parcelamento.Id = int.Parse(comboCondicaoRecebimento.SelectedValue.ToString());
                        parcelamento = (Parcelamento)Controller.getInstance().selecionar(parcelamento);

                        this.autorizacao = txtAutorizacao.Texts.Trim();

                        fp.Id = 2;
                        fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                        if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                            this.valor = decimal.Parse(txtValor.Texts);

                        tipoCartao = "";
                        if (radioDebito.Checked == true)
                            tipoCartao = "DÉBITO";
                        else
                            tipoCartao = "CRÉDITO";

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("O valor recebido no cartão não pode ser maior que o valor faltante!");
                        txtValor.Select();
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowErro("Informe o valor recebido!");
            }
        }

        private void FrmCartao_Load(object sender, EventArgs e)
        {
   
        }

        private void comboCondicaoRecebimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboCondicaoRecebimento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            txtValor.Texts = "0,00";
            this.Close();
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    comboMaquininha.Focus();
                    break;
            }
        }

        private void FrmCartao_Paint(object sender, PaintEventArgs e)
        {
            if (passou == false)
            {
                txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
                txtValor.Focus();
                passou = true;
            }
        }

        private void FrmCartao_KeyDown(object sender, KeyEventArgs e)
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

        private void txtValor_Leave(object sender, EventArgs e)
        {

        }

        private void radioDebito_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                parcelas();
            }
            catch
            {

            }
        }

        private void radioCredito_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                parcelas();
            }
            catch
            {

            }
        }
    }
}
