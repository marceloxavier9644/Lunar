using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.FormaPagamentoRecebimento.Adicionais
{
    public partial class FrmListaPagarAbatimento : Form
    {
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        decimal valorFaltantePagar = 0;
        decimal valor = 0;
        bool showModal = false;
        ContaPagarController contaPagarController = new ContaPagarController();
        IList<ContaReceber> listaReceberAbatimento = new List<ContaReceber>();
        FormaPagamento fp = new FormaPagamento();
        public DialogResult showModalNovo(ref decimal valorRecebido, ref IList<ContaReceber> listaReceberAbatimento, ref FormaPagamento formaPagamento)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                valorRecebido = this.valor;
                listaReceberAbatimento = this.listaReceberAbatimento;
                formaPagamento = this.fp;
            }
            return DialogResult;
        }
        public FrmListaPagarAbatimento(IList<ContaReceber> listaReceber, decimal valorFaltantePagar, Pessoa pessoa)
        {
            InitializeComponent();
            this.listaReceber = listaReceber;
            this.valorFaltantePagar = valorFaltantePagar;
            calculaTotalNotas();
            fp.Id = 9;
            fp = (FormaPagamento)FormaPagamentoController.getInstance().selecionar(fp);
        }

        private void confirmar()
        {
            IList<ContaReceber> listaRece = new List<ContaReceber>();
            valor = 0;
            //Recebendo Parcelas do contas a receber
            if (listaReceber != null && valorFaltantePagar > 0)
            {
                if (listaReceber.Count > 0)
                {
                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var conta = selectedItem as ContaReceber;

                        if (conta.Recebido == false)
                        {
                            valor = valor + conta.ValorTotal;
                            listaRece.Add(conta);
                        }
                    }
                    this.listaReceberAbatimento = listaRece;
                    if (valorFaltantePagar >= valor)
                        this.DialogResult = DialogResult.OK;
                    else
                    {
                        GenericaDesktop.ShowErro("O valor a pagar ficou maior que o valor a receber, selecione um valor menor!");
                    }
                }
            }
        }

        private void calculaTotalNotas()
        {
            int diasVencido = 0;
            decimal multa = 0;
            decimal juro = 0;
            IList<ContaReceber> listaContaReceberCalculado = new List<ContaReceber>();
            try
            {
                //var records = grid.View.Records;
                int i = 1;
                foreach (ContaReceber contaReceber in listaReceber)
                {
                    var receber = contaReceber;
                    //var receber = (ContaReceber)record.Data;

                    if (receber.Vencimento < DateTime.Now)
                    {
                        //calcula juro e multa apenas se nao tiver pago
                        if (contaReceber.Recebido == false)
                        {
                            TimeSpan dataX = DateTime.Now - receber.Vencimento;
                            diasVencido = dataX.Days;
                            decimal multaCalculada = receber.ValorParcela * multa / 100;
                            receber.Multa = multaCalculada;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Multa"].MappingName, multaCalculada);
                            decimal juroCalculado = receber.ValorParcela * ((juro / 30) / 100) * diasVencido;
                            //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["Juro"].MappingName, juroCalculado);
                            receber.Juro = juroCalculado;
                            decimal valorTotalCalculado = (receber.ValorParcela + multaCalculada + juroCalculado - (receber.ValorRecebimentoParcial));
                            // grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, valorTotalCalculado);
                            receber.ValorTotal = valorTotalCalculado;
                        }
                        else
                        {
                            //Se foi pago pega o q esta preenchido
                            receber.ValorTotal = (receber.ValorParcela + receber.Multa + receber.Juro);
                        }
                    }
                    else
                    {

                        decimal valorTotalCalculado = (receber.ValorParcela - receber.ValorRecebimentoParcial);
                        receber.ValorTotal = valorTotalCalculado;
                    }
                    if (receber.ValorTotal == 0)
                    {
                        receber.ValorTotal = receber.ValorParcela;
                    }
                    //grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(i), grid.Columns["ValorTotal"].MappingName, receber.ValorParcela);
                    i++;
                    listaContaReceberCalculado.Add(receber);
                }
                grid.DataSource = listaContaReceberCalculado;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            confirmar();
        }

        private void FrmListaPagarAbatimento_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnPagar.PerformClick();
                    break;
            }
        }
    }
}
