using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.WinForms.DataGrid;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmSelecionarGrade : Form
    {
        public ProdutoGrade GradeSelecionada { get; private set; }
        ProdutoGradeController produtoGradeController = new ProdutoGradeController();
        public FrmSelecionarGrade(LunarBase.Classes.Produto produto)
        {
            InitializeComponent();
            IList<ProdutoGrade> listaGrade = produtoGradeController.selecionarGradePorProduto(produto.Id);
            sfDataGrid1.DataSource = listaGrade;
            lblCodProduto.Text = produto.Id.ToString();
            lblDescricaoProduto.Text = produto.Descricao;
            // Se houver pelo menos uma linha, selecione a primeira


        }

            private void FrmSelecionarGrade_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;

                case Keys.Enter:
                        e.Handled = true; 
                        e.SuppressKeyPress = true;
                        btnConfirmar.PerformClick();
                    break;
                   
            }
        }

        private void btnConfirmar_Click(object sender, System.EventArgs e)
        {
            if (sfDataGrid1.SelectedItems.Count > 0)
            {
                GradeSelecionada = (ProdutoGrade)sfDataGrid1.SelectedItems[0];
            }
            else
            {
                // Se nenhuma seleção, defina como null ou trate conforme necessário
                GradeSelecionada = null;
            }

            // Feche o formulário e retorne ao formulário anterior
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sfDataGrid1_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void sfDataGrid1_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            btnConfirmar.PerformClick();
        }

        private void FrmSelecionarGrade_Load(object sender, System.EventArgs e)
        {
            if (sfDataGrid1.RowCount > 0)
            {
                sfDataGrid1.Focus(); 
                sfDataGrid1.SelectedItems.Add(sfDataGrid1.View.Records[0].Data);
            }
        }
    }
}
