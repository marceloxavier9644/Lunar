﻿using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmCrediario : Form
    {
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        GenericaDesktop generica = new GenericaDesktop();
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
        public FrmCrediario(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
        }

        private void FrmCrediario_Paint(object sender, PaintEventArgs e)
        {
            txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
            if (String.IsNullOrEmpty(txtValor.Texts))
            {
                txtValor.Focus();
                txtParcelas.Texts = "1";
            }
            txtDataVencimento.Value = DateTime.Now.AddMonths(1);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();

            if (!String.IsNullOrEmpty(txtValor.Texts))
            {
                if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                {
                    this.valor = decimal.Parse(txtValor.Texts);
                    FormaPagamento formaPagamento = new FormaPagamento();
                    formaPagamento.Id = 6;
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
                        
                        ContaReceber contaReceber = new ContaReceber();
                        contaReceber.Id = 0;
                        
                        contaReceber.NomeCliente = venda.Cliente.RazaoSocial;
                        contaReceber.CnpjCliente = venda.Cliente.Cnpj;
                        contaReceber.Data = DateTime.Now;
                        contaReceber.Descricao = "VENDA - " + venda.Id;
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
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor recebido no PIX não pode ser maior que o valor faltante!");
                    txtValor.Select();
                }

            }
            else
            {
                GenericaDesktop.ShowErro("Informe o valor recebido!");
            }
        }

        private void inserirParcelas()
        {
            try
            {
                if (String.IsNullOrEmpty(txtParcelas.Texts))
                    txtParcelas.Texts = "1";
                dsParcelas.Tables[0].Clear();
                DateTime vencimento = DateTime.Parse(txtDataVencimento.Value.ToString());
                this.gridParcelas.DataSource = dsParcelas.Tables["Parcelas"];
                for (int i = 1; i <= int.Parse(txtParcelas.Texts); i++)
                {
                    if (i > 1)
                        vencimento = vencimento.AddMonths(1);
                    //vencimento = DateTime.Parse(txtDataVencimento.Value.ToString()).AddDays(i * 30 - 30);

                    int dia = DateTime.Parse(txtDataVencimento.Value.ToString()).Day;
                    //if (vencimento.Day == 31 || vencimento.Day == 30)
                    //{
                    //    vencimento = vencimento.AddDays(1);
                    //    if (vencimento.Day == 31)
                    //        vencimento = vencimento.AddDays(1);
                    //}
                    if (vencimento.Day != dia)
                        vencimento = DateTime.Parse((dia + "/" + vencimento.Month + "/" + vencimento.Year + " 00:00:00").ToString());
                    System.Data.DataRow row = dsParcelas.Tables[0].NewRow();
                    row.SetField("PARCELA", i);
                    row.SetField("VENCIMENTO", vencimento.ToShortDateString());
                    decimal valorParcela = 0;
                    valorParcela = decimal.Parse(txtValor.Texts) / decimal.Parse(txtParcelas.Texts);
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
                    if(linhas == records.Count)
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

        private void gridParcelas_CurrentCellEndEdit(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellEndEditEventArgs e)
        {
            try
            {
             //   MessageBox.Show("The editing is completed for the cell (" + e.DataRow.RowIndex + "," + e.DataColumn.ColumnIndex + ")");
            }
            catch
            {

            }
        }

        private void FrmCrediario_KeyDown(object sender, KeyEventArgs e)
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
    }
}
