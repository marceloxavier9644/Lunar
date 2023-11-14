using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lunar.Telas.FormaPagamentoRecebimento.Adicionais
{
    public partial class FrmCrediarioOrdemServico : Form
    {
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        GenericaDesktop generica = new GenericaDesktop();
        FormaPagamento formaPagamento = new FormaPagamento();
        OrdemServico ordemServico = new OrdemServico();
        IList<ContaReceber> listaCrediarioReceber = new List<ContaReceber>();
        FormaPagamento fp = new FormaPagamento();
        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref IList<ContaReceber> listaCrediarioReceber)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                listaCrediarioReceber = this.listaCrediarioReceber;
                valorRecebido = this.valor;
            }
            return DialogResult;
        }
        public FrmCrediarioOrdemServico(decimal valorFaltante, OrdemServico ordemServico)
        {
            InitializeComponent();

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("C2", CultureInfo.CurrentCulture);
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.ordemServico = ordemServico;

            this.gridParcelas.Columns[1].AllowEditing = true;
            this.gridParcelas.Columns[2].AllowEditing = true;
        }

        private void FrmCrediarioOrdemServico_Paint(object sender, PaintEventArgs e)
        {
            txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
            if (String.IsNullOrEmpty(txtValor.Texts))
            {
                txtValor.Focus();
                txtParcelas.Texts = "1";
            }
            txtDataVencimento.Value = DateTime.Now.AddMonths(1);
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
                    {
                        if(vencimento.Month == 2)
                        {
                            vencimento = DateTime.Parse((01 + "/03/" + vencimento.Year + " 00:00:00").ToString());
                        }
                        else
                        {
                            vencimento = DateTime.Parse((dia + "/" + vencimento.Month + "/" + vencimento.Year + " 00:00:00").ToString());
                        }
                    }
                        
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
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
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

        private void FrmCrediarioOrdemServico_KeyDown(object sender, KeyEventArgs e)
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
            fp = new FormaPagamento();
            fp.Id = 6;
            fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
            var records = gridParcelas.View.Records;
            this.valor = decimal.Parse(txtValor.Texts);
            if (validaValorGrid())
            {
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;

                    ContaReceber contaReceber = new ContaReceber();
                    contaReceber.Id = 0;

                    contaReceber.NomeCliente = ordemServico.Cliente.RazaoSocial;
                    contaReceber.CnpjCliente = ordemServico.Cliente.Cnpj;
                    contaReceber.Data = DateTime.Now;
                    contaReceber.Descricao = "O.S - " + ordemServico.Id;
                    contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                    contaReceber.ValorParcela = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                    contaReceber.ValorTotal = decimal.Parse(dataRowView.Row["VALOR"].ToString());
                    contaReceber.ValorTotalOrigem = decimal.Parse(txtValor.Texts);
                    contaReceber.Juro = 0;
                    contaReceber.Multa = 0;
                    if (ordemServico.Cliente.EnderecoPrincipal != null)
                        contaReceber.EnderecoCliente = ordemServico.Cliente.EnderecoPrincipal.Logradouro + ", " + ordemServico.Cliente.EnderecoPrincipal.Numero + " - " + ordemServico.Cliente.EnderecoPrincipal.Complemento;
                    else
                        contaReceber.EnderecoCliente = "";
                    contaReceber.FormaPagamento = fp;
                    contaReceber.Recebido = false;
                    contaReceber.Vencimento = DateTime.Parse(dataRowView.Row["VENCIMENTO"].ToString());
                    contaReceber.Venda = null;
                    contaReceber.OrdemServico = ordemServico;
                    contaReceber.Parcela = dataRowView.Row["PARCELA"].ToString();
                    contaReceber.VendaFormaPagamento = null;
                    contaReceber.Cliente = ordemServico.Cliente;
                    contaReceber.Origem = "ORDEMSERVICO";
                    contaReceber.Concluido = false;
                    listaCrediarioReceber.Add(contaReceber);
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                inserirParcelas();
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

        private void gridParcelas_CurrentCellEndEdit(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellEndEditEventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txtValor.Texts);
                decimal val = 0;
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
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta("Erro, confira valor ou outros valores ajustados! " + erro.Message);
            }
        }
    }
}
