using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.PesquisaPadrao
{
    public partial class FrmPesquisaPessoa : Form
    {
        private IList<Pessoa> listaClientes;
        PessoaController pessoaController = new PessoaController();
        Pessoa pessoa = new Pessoa();
        String valor = "";

        public DialogResult showModal(ref Pessoa objeto)
        {
            DialogResult = ShowDialog();
            try
            {
                objeto = (Pessoa)gridClient.SelectedItem;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return DialogResult;
        }
        public FrmPesquisaPessoa(String valor)
        {
            InitializeComponent();
            // this.Opacity = 0.0;
            this.valor = valor;
            txtPesquisa.Texts = valor;
        }
        private void carregarLista()
        {
            txtPesquisa.Texts = "";
            listaClientes = pessoaController.selecionarPessoasGrid();

            sfDataPager1.DataSource = listaClientes;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            gridClient.DataSource = sfDataPager1.PagedSource;

            txtPesquisa.Focus();
            txtPesquisa.Select();
            if (listaClientes.Count > 0)
                gridClient.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtPesquisa.Texts))
                {
                    if (GenericaDesktop.ShowConfirmacao("Sem digitar dados na pesquisa o sistema vai buscar todos clientes/fornecedores e pode demorar um tempo, deseja retornar todos?"))
                        PesquisarCliente(txtPesquisa.Texts.Trim());
                    else
                        txtPesquisa.Focus();
                }
                else
                    PesquisarCliente(txtPesquisa.Texts.Trim());
            }
        }

        private void PesquisarCliente(string valor)
        {
            listaClientes = pessoaController.selecionarPessoasComVariosFiltros(valor);
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;

            sfDataPager1.DataSource = listaClientes;
            gridClient.DataSource = sfDataPager1.PagedSource;

            if (listaClientes.Count == 0)
            {
                sfDataPager1.DataSource = null;
                gridClient.DataSource = null;
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisa.Texts = "";
                txtPesquisa.PlaceholderText = "";
                txtPesquisa.Select();
            }
            txtPesquisa.Select();
        }

        private void gridClient_CurrentCellKeyDown(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellKeyEventArgs e)
        {
            if (e.KeyEventArgs.KeyCode == Keys.Enter)
            {
                if (this.gridClient.SelectedItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                e.KeyEventArgs.SuppressKeyPress = true;
            }
        }
        private void selecionarObjeto()
        {
            if (gridClient.SelectedIndex >= 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do item que deseja selecionar!");
        }

        private void gridClient_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            selecionarObjeto();
        }

        private void gridClient_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
            if ((e.RowData as Pessoa) != null)
            {
                if ((e.RowData as Pessoa).RegistradoSpc == true || (e.RowData as Pessoa).EscritorioCobranca == true)
                {
                    e.Style.TextColor = Color.Red;
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            selecionarObjeto();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPesquisa.Texts))
            {
                if (GenericaDesktop.ShowConfirmacao("Sem digitar dados na pesquisa o sistema vai buscar todos clientes/fornecedores e pode demorar um tempo, deseja retornar todos?"))
                    PesquisarCliente(txtPesquisa.Texts.Trim());
                else
                    txtPesquisa.Focus();
            }
            else
                PesquisarCliente(txtPesquisa.Texts.Trim());
        }

        private void FrmPesquisaPessoa_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(valor))
                carregarLista();
            else
                btnPesquisar.PerformClick();
        }
    }
}
