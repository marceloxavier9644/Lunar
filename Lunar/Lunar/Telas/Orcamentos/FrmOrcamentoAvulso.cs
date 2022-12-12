using Lunar.Utils;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Orcamentos
{
    public partial class FrmOrcamentoAvulso : Form
    {
        int indexEditando = -1;
        bool editando = false;
        GenericaDesktop generica = new GenericaDesktop();
        public FrmOrcamentoAvulso()
        {
            InitializeComponent();
            this.gridProdutos.DataSource = dsProduto.Tables["Prods"];
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            try { txtDDD.Texts = Sessao.empresaFilialLogada.DddPrincipal; } catch { txtDDD.Texts = ""; }
        }

        private void btnConfirmarProduto_Click(object sender, EventArgs e)
        {
            inserir();
        }

        private void inserir()
        {
            if (editando == false)
            {
                System.Data.DataRow row;
                row = dsProduto.Tables[0].NewRow();
                row.SetField("Descricao", txtDescricao.Texts);
                row.SetField("Quantidade", double.Parse(txtQuantidade.Texts));
                decimal valorUnitForm = decimal.Parse(txtValorUnitario.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                decimal valorTotal = decimal.Parse(txtValorTotal.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                dsProduto.Tables[0].Rows.Add(row);

                gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                gridProdutos.AutoSizeController.Refresh();

                txtDescricao.Texts = "";
                txtQuantidade.Texts = "1";
                txtValorUnitario.Texts = "0,00";
                txtValorTotal.Texts = "0,00";
                txtDescricao.Focus();
                somaEnquantoDigita();
            }
            else
            {
                System.Data.DataRow row;
                row = dsProduto.Tables[0].Rows[indexEditando];
                row.SetField("Descricao", txtDescricao.Texts);
                row.SetField("Quantidade", double.Parse(txtQuantidade.Texts));
                decimal valorUnitForm = decimal.Parse(txtValorUnitario.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                decimal valorTotal = decimal.Parse(txtValorTotal.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                //dsProduto.Tables[0].Rows.Add(row);

                gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                gridProdutos.AutoSizeController.Refresh();

                txtDescricao.Texts = "";
                txtQuantidade.Texts = "1";
                txtValorUnitario.Texts = "0,00";
                txtValorTotal.Texts = "0,00";
                txtDescricao.Focus();
                indexEditando = -1;
                editando = false;
                somaEnquantoDigita();
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                generica.SoNumeroEVirgula(txtQuantidade.Texts, e);
                if (e.KeyChar == 13)
                {
                    txtValorUnitario.Focus();
                    txtValorUnitario.Select();
                    txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
                }
            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuantidade.Texts))
                txtQuantidade.Texts = "1";
        }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                generica.SoNumeroEVirgula(txtValorUnitario.Texts, e);
                if (e.KeyChar == 13)
                {
                    if (txtValorUnitario.Enabled == true)
                    {
                        txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
                        inserir();
                    }
                }
            }
            catch
            {

            }
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            }
            catch
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class OrcamentoAvulso
        {
            public string descricao { get; set; }
            public double quantidade { get; set; }
            public decimal valorUnitario { get; set; }
            public decimal valorTotal { get; set; }
            public string cliente { get; set; }
            public string ddd { get; set; }
            public string fone { get; set; }
        }

        private void txtFone_Leave(object sender, EventArgs e)
        {
            try 
            {
                if (!String.IsNullOrEmpty(txtFone.Texts))
                    txtFone.Texts = GenericaDesktop.MascaraTelefone(txtFone.Texts);
            } 
            catch
            {

            }
        }

        private void txtFone_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtFone.Texts, e);
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedIndex >= 0)
            {
                //var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
                //var dataRow = (selectedItem as DataRowView).Row;
                //dataRow.Delete();

                dsProduto.Tables[0].Rows[gridProdutos.SelectedIndex].Delete();

                somaEnquantoDigita();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro você deve clicar na linha do produto que deseja remover!");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void imprimir()
        {
            IList<OrcamentoAvulso> listaOrcamento = new List<OrcamentoAvulso>();
            var records = gridProdutos.View.Records;
            decimal descontoItem = 0;
            foreach (var record in records)
            {
                OrcamentoAvulso orcamentoAvulso = new OrcamentoAvulso();
                var dataRowView = record.Data as DataRowView;
                orcamentoAvulso.descricao = dataRowView.Row["Descricao"].ToString();
                orcamentoAvulso.quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                orcamentoAvulso.valorUnitario = decimal.Parse(dataRowView.Row["ValorUnitario"].ToString());
                orcamentoAvulso.valorTotal = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                orcamentoAvulso.cliente = txtCliente.Texts;
                orcamentoAvulso.ddd = txtDDD.Texts;
                orcamentoAvulso.fone = txtFone.Texts;
                listaOrcamento.Add(orcamentoAvulso);
            }
            Form formBackground = new Form();
            try { txtDDD.Texts = GenericaDesktop.RemoveCaracteres(txtDDD.Texts); } catch { }
            string fone = "(" + txtDDD.Texts + ") " + txtFone.Texts;

            FrmImprimirOrcamentoAvulso uu = new FrmImprimirOrcamentoAvulso(listaOrcamento, txtCliente.Texts, fone, txtObservacoes.Texts);
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
            uu.ShowDialog();
            formBackground.Dispose();
            uu.Dispose();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void txtValorUnitario__TextChanged(object sender, EventArgs e)
        {
            try
            {

                txtValorTotal.Texts = string.Format("{0:0.00}", decimal.Parse(txtValorUnitario.Texts) * decimal.Parse(txtQuantidade.Texts));
            }
            catch
            {

            }
        }

        private void somaEnquantoDigita()
        {
            try
            {
                decimal valorTotal = 0;
                var records = gridProdutos.View.Records;
                decimal descontoItem = 0;
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    valorTotal = valorTotal + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                }
                lblTotal.Text = valorTotal.ToString("C2", CultureInfo.CurrentCulture);
            }
            catch
            {

            }
        }

        private void gridProdutos_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            editando = true;
            var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            var descricao = dataRow["Descricao"].ToString();
            var quantidade = dataRow["Quantidade"].ToString();
            var valorUnitario = dataRow["ValorUnitario"].ToString();
            var valorTotal = dataRow["ValorTotal"].ToString();

            indexEditando = this.gridProdutos.SelectedIndex;
            txtDescricao.Texts = descricao;
            txtQuantidade.Texts = quantidade;
            txtValorUnitario.Texts = string.Format("{0:0.00}", valorUnitario);
            txtValorTotal.Texts = string.Format("{0:0.00}", valorTotal);
            txtDescricao.Focus();
            txtDescricao.Select();
        }

        private void FrmOrcamentoAvulso_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F8:
                    btnImprimir.PerformClick();
                    break;
            }
        }
    }
}
