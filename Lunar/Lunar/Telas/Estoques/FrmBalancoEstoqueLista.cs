using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Lunar.Telas.Estoques
{
    public partial class FrmBalancoEstoqueLista : Form
    {
        BalancoEstoqueController balancoEstoqueController = new BalancoEstoqueController();
        IList<BalancoEstoque> listaBalanco = new List<BalancoEstoque>();
        BalancoEstoque balanco = new BalancoEstoque();
        public FrmBalancoEstoqueLista()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
            todosBalancos();
        }

        private void pesquisaBalanco()
        {
            try
            {
                listaBalanco = new List<BalancoEstoque>();
                string sql = "From BalancoEstoque Tabela where Tabela.FlagExcluido <> true ";

                if (radioEfetivado.Checked == true)
                    sql = sql + "and Tabela.Efetivado = true ";
                else if (radioEmDigitacao.Checked == true)
                    sql = sql + "and Tabela.Efetivado = false ";


                if (chkAtivarData.Checked == true)
                {
                    DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
                    DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
                    String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                    String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

                    sql = sql + "and Tabela.DataAjuste BETWEEN '" + dataInicial + "' and '" + dataFinal + "' ";
                }

                string orderBy = " order by Tabela.DataAjuste";
                listaBalanco = balancoEstoqueController.selecionarBalancoEstoquePorSql(sql + orderBy);
                if (listaBalanco.Count > 0)
                {
                    sfDataPager1.DataSource = listaBalanco;

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
                else
                {
                    grid.DataSource = null;
                    sfDataPager1.DataSource = null;
                    grid.Refresh();
                }
            }
            catch (Exception erro)
            {

            }
        }

        private void todosBalancos()
        {
            listaBalanco = balancoEstoqueController.selecionarTodosBalancoEstoque(Sessao.empresaFilialLogada);
            if (listaBalanco.Count > 0)
            {
                sfDataPager1.DataSource = listaBalanco;
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
            else
            {
                grid.DataSource = null;
                sfDataPager1.DataSource = null;
                grid.Refresh();
            }
        }
        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {

        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisaBalanco();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisaBalanco();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmBalancoEstoque uu = new FrmBalancoEstoque();
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
            btnPesquisar.PerformClick();
        }

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                BalancoEstoque balancoEstoque = new BalancoEstoque();
                balancoEstoque = (BalancoEstoque)grid.SelectedItem;
                if (balancoEstoque.Efetivado == false)
                    editarCadastro(balancoEstoque);
                else
                    GenericaDesktop.ShowAlerta("Este balanço já foi efetivado, não é possível editar!");
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja editar!");
        }

        private void editarCadastro(BalancoEstoque balancoEstoque)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmBalancoEstoque uu = new FrmBalancoEstoque(balancoEstoque))
                {
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
                    pesquisaBalanco();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            BalancoEstoque balancoEstoque = new BalancoEstoque();
            try
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir este balanço?"))
                {
                    if (grid.SelectedIndex >= 0)
                    {
                        GenericaDesktop generica = new GenericaDesktop();
                        balancoEstoque = (BalancoEstoque)grid.SelectedItem;
                        if (balancoEstoque.Efetivado == true)
                        {
                            if (GenericaDesktop.ShowConfirmacao("Balanço já foi efetivado, se excluir o estoque vai retornar as quantidades ajustadas, deseja realmente excluir?"))
                            {
                                EstoqueController estoqueController = new EstoqueController();
                                IList<Estoque> listaEstoque = estoqueController.selecionarEstoqueMovimentoPorBalanco(balancoEstoque.Filial, balancoEstoque.Conciliado, balancoEstoque);
                                if (listaEstoque.Count > 0)
                                {
                                    foreach (Estoque estoque in listaEstoque)
                                    {
                                        Controller.getInstance().excluir(estoque);
                                        generica.conferirTabelaEstoqueEAtualizarProduto(estoque.Produto, estoque.EmpresaFilial);
                                    }
                                }
                                Controller.getInstance().excluir(balancoEstoque);
                                GenericaDesktop.ShowInfo("Excluído com Sucesso");
                                pesquisaBalanco();
                            }
                        }
                        else
                        {
                            Controller.getInstance().excluir(balancoEstoque);
                            GenericaDesktop.ShowInfo("Excluído com Sucesso");
                            pesquisaBalanco();
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Clique na linha do balanço que deseja excluir!");
                }
            }
            catch
            {
                GenericaDesktop.ShowErro("Informe o suporte do seu sistema que houve um erro na exclusão do balanço de ID: " + balancoEstoque.Id.ToString());
            }
        }

        private void chkAtivarData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkAtivarData.Checked == true)
                {
                    txtDataInicial.Enabled = true;
                    txtDataFinal.Enabled = true;
                }
                else
                {
                    txtDataInicial.Enabled = false;
                    txtDataFinal.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                BalancoEstoque balancoEstoque = new BalancoEstoque();
                balancoEstoque = (BalancoEstoque)grid.SelectedItem;
                FrmImprimirComparativoBalanco frm = new FrmImprimirComparativoBalanco(balancoEstoque);
                frm.ShowDialog();
            }
        }

        private void btnEfetivar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                BalancoEstoque balancoEstoque = new BalancoEstoque();
                balancoEstoque = (BalancoEstoque)grid.SelectedItem;
                if (balancoEstoque.Efetivado == false)
                {
                    if (GenericaDesktop.ShowConfirmacao("Confirma a efetivação do balanço com a data em " + balancoEstoque.DataAjuste.ToShortDateString()))
                    {
                        balanco = balancoEstoque;
                        lblEfetivacao.Visible = true;
                        lblEfetivacao.Text = "Efetivando";
                        Thread th = new Thread(efetivarBalanco);
                        th.Start();
                        th.Join();
                        lblEfetivacao.Visible = true;
                        lblEfetivacao.Text = "Aguarde...";
                        Thread th2 = new Thread(ajustarQuantidadeProdutoPeloMovimentoEstoque);
                        th2.Start();
                        th2.Join();

                        GenericaDesktop.ShowInfo("Balanço efetivado com sucesso!");
                        pesquisaBalanco();
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Este balanço já foi efetivado anteriormente");
                
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do balanço que deseja efetivar!");
            
        }

        private void efetivarBalanco()
        {
            GenericaDesktop generica = new GenericaDesktop();
            BalancoEstoqueProdutoController balancoEstoqueProdutoController = new BalancoEstoqueProdutoController();
            IList<BalancoEstoqueProduto> listaProdutos = balancoEstoqueProdutoController.selecionarProdutosPorBalancoSemRepetirItem(balanco.Id);
            IList<Produto> listaProdutosParaZerar = balancoEstoqueProdutoController.selecionarProdutosParaZerarNaoContabilizados(balanco.Id);
            if (listaProdutos.Count > 0)
            {
                int contador = 0;
               
                foreach (BalancoEstoqueProduto balancoEstoqueProduto in listaProdutos)
                {
                    contador++;
                    lblEfetivacao.Text = "Efetivando " + contador + " de " + listaProdutos.Count;
                    //lblEfetivacao.Text = "Efetivando "+contador+" de " + listaProdutos.Count;
                    bool tipoAjusteEntradaOuSaida2 = true;
                    double quantidadeAjuste = 0;
                    double estoqData = calcularEstoqueData(balancoEstoqueProduto);
                  
                    if (balancoEstoqueProduto.Quantidade > estoqData)
                    {
                        quantidadeAjuste = balancoEstoqueProduto.Quantidade - estoqData;
                        tipoAjusteEntradaOuSaida2 = true;
                    }
                    else
                    {
                        quantidadeAjuste = estoqData - balancoEstoqueProduto.Quantidade;
                        tipoAjusteEntradaOuSaida2 = false;
                    }

                    if (balancoEstoqueProduto.Conciliado == false)
                        generica.atualizarEstoqueNaoConciliado(balancoEstoqueProduto.Produto, quantidadeAjuste, tipoAjusteEntradaOuSaida2, "BALANCOESTOQUE", "BALANCO ESTOQUE: " + balanco.Descricao + " USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, balanco.DataAjuste, balanco);

                    else
                        generica.atualizarEstoqueConciliado(balancoEstoqueProduto.Produto, quantidadeAjuste, tipoAjusteEntradaOuSaida2, "BALANCOESTOQUE", "BALANCO ESTOQUE: " + balanco.Descricao + " USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, balanco.DataAjuste, balanco);
                }
                contador = 0;
                if(listaProdutosParaZerar.Count > 0)
                {
                    foreach (Produto produto in listaProdutosParaZerar)
                    {
                        contador++;
                        lblEfetivacao.Text = "Zerando " + contador + " de " + listaProdutosParaZerar.Count;

                        bool tipoAjusteEntradaOuSaida2 = true;
                        double quantidadeAjuste = 0;
                        double estoqData = calcularEstoqueDataParaZerar(produto, balanco.DataAjuste, balanco.Conciliado);

                        if (estoqData > 0 || estoqData < 0)
                        {
                            if (0 > estoqData)
                            {
                                quantidadeAjuste = -(estoqData);
                                tipoAjusteEntradaOuSaida2 = true;
                            }
                            else
                            {
                                quantidadeAjuste = estoqData;
                                tipoAjusteEntradaOuSaida2 = false;
                            }

                            if (balanco.Conciliado == false)
                                generica.atualizarEstoqueNaoConciliado(produto, quantidadeAjuste, tipoAjusteEntradaOuSaida2, "BALANCOESTOQUE", "BALANCO ESTOQUE: " + balanco.Descricao + " USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, balanco.DataAjuste, balanco);

                            else
                                generica.atualizarEstoqueConciliado(produto, quantidadeAjuste, tipoAjusteEntradaOuSaida2, "BALANCOESTOQUE", "BALANCO ESTOQUE: " + balanco.Descricao + " USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, balanco.DataAjuste, balanco);
                        }
                    }
                }
                balanco.Efetivado = true;
                balanco.OperadorEfetivacao = Sessao.usuarioLogado;
                Controller.getInstance().salvar(balanco);
                lblEfetivacao.Visible = false;
            }
        }
        private double calcularEstoqueData(BalancoEstoqueProduto balancoEstoqueProduto)
        {
            try
            {
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                DateTime dataAjust = new DateTime();
                dataAjust = balancoEstoqueProduto.BalancoEstoque.DataAjuste;
                DateTime dataIni = balancoEstoqueProduto.BalancoEstoque.DataAjuste;
                String dat = dataIni.ToString("yyyy-MM-dd");
                double estoqueConciliado = estoqueDAO.calcularEstoqueConciliadoPorProdutoeData(balancoEstoqueProduto.Produto.Id, Sessao.empresaFilialLogada, dat);
                if (balancoEstoqueProduto.Conciliado == true)
                    return estoqueConciliado;
                else
                {
                    double estoqueNaoConciliado = estoqueDAO.calcularEstoqueNaoConciliadoPorProdutoeData(balancoEstoqueProduto.Produto.Id, Sessao.empresaFilialLogada, dat);
                    return estoqueNaoConciliado;
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Ocorreu um erro no calculo do estoque anterior do produto: (" + balancoEstoqueProduto.Produto.Id.ToString() + ") " + erro.Message);
                return 0;
            }
        }

        private double calcularEstoqueDataParaZerar(Produto produto, DateTime dataBalanco, bool conciliado)
        {
            try
            {
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                DateTime dataAjust = new DateTime();
                dataAjust = dataBalanco;
                DateTime dataIni = dataBalanco;
                String dat = dataIni.ToString("yyyy-MM-dd");
                double estoqueConciliado = estoqueDAO.calcularEstoqueConciliadoPorProdutoeData(produto.Id, Sessao.empresaFilialLogada, dat);
                if (conciliado == true)
                    return estoqueConciliado;
                else
                {
                    double estoqueNaoConciliado = estoqueDAO.calcularEstoqueNaoConciliadoPorProdutoeData(produto.Id, Sessao.empresaFilialLogada, dat);
                    return estoqueNaoConciliado;
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Ocorreu um erro no calculo do estoque anterior do produto: (" + produto.Id.ToString() + ") " + erro.Message);
                return 0;
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as BalancoEstoque) != null)
                {
                    if ((e.RowData as BalancoEstoque).Efetivado == true)
                    {
                       // e.Style.BackColor = Color.Green;
                       e.Style.TextColor = Color.Green;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnCancelarEfetivacao_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                BalancoEstoque balancoEstoque = new BalancoEstoque();
                balancoEstoque = (BalancoEstoque)grid.SelectedItem;
                if (balancoEstoque.Efetivado == true)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja cancelar a efetivação deste balanço? Obs. o estoque será modificado"))
                    {
                        GenericaDesktop generica = new GenericaDesktop();
                        EstoqueController estoqueController = new EstoqueController();
                        IList<Estoque> listaEstoque = estoqueController.selecionarEstoqueMovimentoPorBalanco(balancoEstoque.Filial, balancoEstoque.Conciliado, balancoEstoque);
                        if (listaEstoque.Count > 0)
                        {
                            foreach (Estoque estoque in listaEstoque)
                            {
                                Controller.getInstance().excluir(estoque);
                                generica.conferirTabelaEstoqueEAtualizarProduto(estoque.Produto, estoque.EmpresaFilial);
                            }
                            balancoEstoque.Efetivado = false;
                            Controller.getInstance().salvar(balancoEstoque);
                            GenericaDesktop.ShowInfo("Cancelado com sucesso");
                            pesquisaBalanco();
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Balanço selecionado não foi efetivado!");
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do balanço que deseja efetivar!");

        }

        private void FrmBalancoEstoqueLista_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    ajustarQuantidadeProdutoPeloMovimentoEstoque();
                    break;
            }
        }

        public void ajustarQuantidadeProdutoPeloMovimentoEstoque()
        {
            lblEfetivacao.Text = "Conferindo estoque, aguarde...";
            GenericaDesktop generica = new GenericaDesktop();
            ProdutoController produtoController = new ProdutoController();
            IList<Produto> listaProd = produtoController.selecionarTodosProdutorPorFilial(Sessao.empresaFilialLogada);
            if (listaProd.Count > 0)
            {
                foreach (Produto prodx in listaProd)
                {
                    double estoqueNaoConciliado = prodx.EstoqueAuxiliar;
                    double estoqueConciliado = prodx.Estoque;
                    generica.conferirTabelaEstoqueEAtualizarProduto(prodx, Sessao.empresaFilialLogada);
                    Produto prody = new Produto();
                    prody.Id = prodx.Id;
                    prody = (Produto)ProdutoController.getInstance().selecionar(prody);
                    if(prody.EstoqueAuxiliar != estoqueNaoConciliado || prody.Estoque != estoqueConciliado)
                    {
                        GenericaDesktop.gravarLinhaLog(prody.Id + " ...Produto Não Conciliado: " + estoqueNaoConciliado + " Para " + prody.EstoqueAuxiliar + "\n"
                            + prody.Id + " ...Produto Conciliado: " + estoqueConciliado + " Para " + prody.Estoque, "CONFERENCIA TABELA ESTOQUE");
                    }
                }
                lblEfetivacao.Visible = false;
                //GenericaDesktop.ShowInfo("Ajuste finalizado com sucesso!");
            }
        }
    }
}
