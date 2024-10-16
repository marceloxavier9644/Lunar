using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.ProdutoDAO;

namespace Lunar.Telas.PesquisaPadrao
{
    public partial class FrmPesquisaProduto : Form
    {
        ProdutoController produtoController = new ProdutoController();
        private IList<Produto> listaProdutos;
        string valor = "";


        public DialogResult showModal(ref Produto objeto)
        {
            txtPesquisa.Focus();
            txtPesquisa.Select();
            DialogResult = ShowDialog();
            try
            {
                objeto = (Produto)grid.SelectedItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return DialogResult;
        }

        public FrmPesquisaProduto(string valor)
        {
            InitializeComponent();
            this.valor = valor;
            PesquisarProdutoPorDescricaoPaginando(valor,0);
            txtPesquisa.Focus();
            txtPesquisa.Select();
        }

        public FrmPesquisaProduto(IList<Produto> lista)
        {
            InitializeComponent();

            listaProdutos = new List<Produto>();
            listaProdutos = lista;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 50;
            sfDataPager1.AllowOnDemandPaging = true;

            Int64 totalProdutos = listaProdutos.Count;
            double totalPaginas = (double)totalProdutos / sfDataPager1.PageSize;
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }
            sfDataPager1.PageCount = (int)Math.Ceiling(totalPaginas);
            sfDataPager1.Refresh();

            //listaProdutos = produtoController.selecionarTodosProdutosPaginando(0 * sfDataPager1.PageSize, sfDataPager1.PageSize, valor);
            grid.DataSource = listaProdutos;
            grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
            this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
            if (listaProdutos.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
            }
            txtPesquisa.Texts = "";
            txtPesquisa.Select();
        }


        private void sfDataPager1_PageIndexChanged(object sender, Syncfusion.WinForms.DataPager.Events.PageIndexChangedEventArgs e)
        {
            PesquisarProdutoPorDescricaoPaginando(txtPesquisa.Texts.Trim(), e.NewPageIndex);
        }
        private void PesquisarProdutoPorDescricaoPaginando(string valor, int paginaAtual)
        {
            listaProdutos = new List<Produto>();

            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 50;
            sfDataPager1.AllowOnDemandPaging = true;

            Int64 totalProdutos = produtoController.totalTodosProdutosPaginando(valor);
            double totalPaginas = (double)totalProdutos / sfDataPager1.PageSize;
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }

            sfDataPager1.PageCount = (int)Math.Ceiling(totalPaginas);
            sfDataPager1.Refresh();


            listaProdutos = produtoController.selecionarTodosProdutosPaginando(paginaAtual * sfDataPager1.PageSize, sfDataPager1.PageSize, valor);
            grid.DataSource = listaProdutos;

      
            grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
            this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;



            if (listaProdutos.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisa.Texts = "";
                txtPesquisa.Select();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarObjeto();
        }
        private void selecionarObjeto()
        {
            if (grid.SelectedIndex >= 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do item que deseja selecionar!");
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                btnPesquisar.PerformClick();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesquisarProdutoPorDescricaoPaginando(txtPesquisa.Texts.Trim(), 0);
            txtPesquisa.Focus();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            selecionarObjeto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void grid_CurrentCellKeyDown(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellKeyEventArgs e)
        {
            if (e.KeyEventArgs.KeyCode == Keys.Enter)
            {
                if (this.grid.SelectedItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                e.KeyEventArgs.SuppressKeyPress = true;
            }
        }

        private void FrmPesquisaProduto_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F2:
                    this.DialogResult = DialogResult.Ignore;
                    break;
                case Keys.F5:
                    btnSelecionar.PerformClick();
                    break;
            }
        }
    }
}
