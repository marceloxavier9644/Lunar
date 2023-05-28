using Lunar.Telas.Vendas;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lunar.Telas.TransferenciaEstoques
{
    public partial class FrmTransferenciaEstoqueLista : Form
    {
        private IList<TransferenciaEstoque> listaTransferencia;
        TransferenciaEstoqueController transferenciaEstoqueController = new TransferenciaEstoqueController();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmTransferenciaEstoqueLista()
        {
            InitializeComponent();
        }

        private void carregarLista()
        {
            listaTransferencia = transferenciaEstoqueController.selecionarTodasTransferencias();

            if (listaTransferencia.Count > 0)
            {
                sfDataPager1.DataSource = listaTransferencia;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                grid.AutoSizeController.Refresh();
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaTransferencia.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void FrmTransferenciaEstoqueLista_Load(object sender, EventArgs e)
        {
            carregarLista();
        }

        private void txtPesquisaProdutoPorCodigoUnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtPesquisaTransferenciaPorCodigoUnico.Texts, e);
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtPesquisaTransferenciaPorCodigoUnico.Texts))
                    PesquisarTransferenciaPorCodigo();
            }
        }
        private void PesquisarTransferenciaPorCodigo()
        {
            if (!String.IsNullOrEmpty(txtPesquisaTransferenciaPorCodigoUnico.Texts))
            {
                TransferenciaEstoque transferenciaEstoque = new TransferenciaEstoque();
                transferenciaEstoque.Id = int.Parse(txtPesquisaTransferenciaPorCodigoUnico.Texts);
                transferenciaEstoque = (TransferenciaEstoque)transferenciaEstoqueController.selecionar(transferenciaEstoque);
                listaTransferencia = new List<TransferenciaEstoque>();
                
                if (transferenciaEstoque != null)
                {if (transferenciaEstoque.Id > 0)
                    {
                        listaTransferencia.Add(transferenciaEstoque);
                        sfDataPager1.DataSource = listaTransferencia;
                        if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                            sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                        else
                            sfDataPager1.PageSize = 100;
                        grid.DataSource = sfDataPager1.PagedSource;
                    }
                }
                txtPesquisaTransferenciaPorCodigoUnico.Texts = "";

                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                grid.AutoSizeController.Refresh();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            bool transferencia = true;
            FrmVendas02 frm = new FrmVendas02(transferencia);
            frm.ShowDialog();
            carregarLista();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarLista();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja excluir esta transferencia?"))
                {
                    TransferenciaEstoque transferenciaEstoque = new TransferenciaEstoque();
                    transferenciaEstoque = (TransferenciaEstoque)grid.SelectedItem;
                    Controller.getInstance().excluir(transferenciaEstoque);
                    TransferenciaEstoqueProdutoController transferenciaEstoqueProdutoController = new TransferenciaEstoqueProdutoController();
                    IList<TransferenciaEstoqueProduto> listaItens = transferenciaEstoqueProdutoController.selecionarProdutosPorTransferencia(transferenciaEstoque.Id);
                    if(listaItens.Count > 0)
                    {
                        foreach(TransferenciaEstoqueProduto transferenciaEstoqueProduto in listaItens)
                        {
                            generica.atualizarEstoqueNaoConciliado(transferenciaEstoqueProduto.Produto, transferenciaEstoqueProduto.Quantidade, true, "TRANSFERENCIAEXCLUIDA", "EXCLUSAOTRANSFERENCIA " + transferenciaEstoque.Id, null, DateTime.Now, null);
                            Controller.getInstance().excluir(transferenciaEstoqueProduto);
                        }
                    }
                    GenericaDesktop.ShowInfo("Excluído com Sucesso, estoque reajustado!");
                    carregarLista();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                TransferenciaEstoque transferenciaEstoque = new TransferenciaEstoque();
                transferenciaEstoque = (TransferenciaEstoque)grid.SelectedItem;
                FrmImprimirTransferencia frmImprimirTransferencia = new FrmImprimirTransferencia(transferenciaEstoque);
                frmImprimirTransferencia.ShowDialog();
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
