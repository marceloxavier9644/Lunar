using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static LunarBase.Classes.Estoque;

namespace Lunar.Telas.Estoques
{
    public partial class FrmGerarInventario : Form
    {
        IList<Inventario> listaInventario = new List<Inventario>();
        public FrmGerarInventario()
        {
            InitializeComponent();
            txtDataInventario.Value = DateTime.Now;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            gerarInventario();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            gerarInventario();
        }

        private void gerarInventario()
        {
            DateTime data = DateTime.Parse(txtDataInventario.Value.ToString());
            String dataInventario = data.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

            EstoqueDAO estoqueDAO = new EstoqueDAO();
            IList<Estoque> lista = estoqueDAO.gerarInventarioPorData(Sessao.empresaFilialLogada, dataInventario);

            Inventario inventario = new Inventario();
            listaInventario = new List<Inventario>();
            foreach (Estoque estoque in lista)
            {
                if (radioSomenteRevenda.Checked == true)
                {
                    if (estoque.Produto.TipoProduto.Equals("REVENDA") && estoque.QuantidadeInventario > 0)
                    {
                        inventario = new Inventario();
                        inventario.quantidadeInventario = estoque.QuantidadeInventario;
                        inventario.codigo = estoque.Produto.Id.ToString() + estoque.Produto.IdComplementar;
                        inventario.codigoBarras = estoque.Produto.Ean;
                        inventario.csosn = estoque.Produto.CstIcms;
                        inventario.descricao = estoque.Produto.Descricao;
                        inventario.medida = estoque.Produto.UnidadeMedida.Sigla;
                        inventario.ncm = estoque.Produto.Ncm;
                        inventario.produto = estoque.Produto;
                        inventario.valorCusto = estoque.Produto.ValorCusto;
                        inventario.valorTotal = estoque.Produto.ValorCusto * decimal.Parse(estoque.QuantidadeInventario.ToString());
                        listaInventario.Add(inventario);
                    }
                }
                else if (radioRevendaEMateriaPrima.Checked == true)
                {
                    if ((estoque.Produto.TipoProduto.Equals("REVENDA") || estoque.Produto.TipoProduto.Equals("MATÉRIA PRIMA")) && estoque.QuantidadeInventario > 0)
                    {
                        inventario = new Inventario();
                        inventario.quantidadeInventario = estoque.QuantidadeInventario;
                        inventario.codigo = estoque.Produto.Id.ToString() + estoque.Produto.IdComplementar;
                        inventario.codigoBarras = estoque.Produto.Ean;
                        inventario.csosn = estoque.Produto.CstIcms;
                        inventario.descricao = estoque.Produto.Descricao;
                        inventario.medida = estoque.Produto.UnidadeMedida.Sigla;
                        inventario.ncm = estoque.Produto.Ncm;
                        inventario.produto = estoque.Produto;
                        inventario.valorCusto = estoque.Produto.ValorCusto;
                        inventario.valorTotal = estoque.Produto.ValorCusto * decimal.Parse(estoque.QuantidadeInventario.ToString());
                        listaInventario.Add(inventario);
                    }
                }
                else
                {
                    if (estoque.QuantidadeInventario > 0)
                    {
                        inventario = new Inventario();
                        inventario.quantidadeInventario = estoque.QuantidadeInventario;
                        inventario.codigo = estoque.Produto.Id.ToString() + estoque.Produto.IdComplementar;
                        inventario.codigoBarras = estoque.Produto.Ean;
                        inventario.csosn = estoque.Produto.CstIcms;
                        inventario.descricao = estoque.Produto.Descricao;
                        inventario.medida = estoque.Produto.UnidadeMedida.Sigla;
                        inventario.ncm = estoque.Produto.Ncm;
                        inventario.produto = estoque.Produto;
                        inventario.valorCusto = estoque.Produto.ValorCusto;
                        inventario.valorTotal = estoque.Produto.ValorCusto * decimal.Parse(estoque.QuantidadeInventario.ToString());
                        listaInventario.Add(inventario);
                    }
                }
            }
            if (listaInventario.Count > 0)
            {
                sfDataPager1.DataSource = listaInventario;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
               
                this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.AutoSizeController.Refresh();
                grid.Refresh();
                this.grid.MoveToCurrentCell(new Syncfusion.WinForms.GridCommon.ScrollAxis.RowColumnIndex(1, 0));
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaInventario.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            DateTime data = DateTime.Parse(txtDataInventario.Value.ToString());
            String dataInventario = data.ToString("dd/MM/yyyy");

            Form formBackground = new Form();
            FrmImprimirInventario uu = new FrmImprimirInventario(listaInventario, dataInventario, txtLivro.Texts);
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
    }
}
