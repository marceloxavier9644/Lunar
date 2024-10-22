using Lunar.Telas.Cadastros.Produtos.Auxiliares;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.VendaItensDAO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lunar.Telas.Vendas.Relatorios
{
    public partial class FrmVendaProdutos : Form
    {
        public FrmVendaProdutos()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
        }


        private void pesquisarProdutosVendidosGeral()
        {
            try
            {
                tabControlAdv1.SelectedTab = tabPageAdv1;
                VendaItensDAO vendaItensDAO = new VendaItensDAO();
                string sql = retornarSql();

                // Executando a consulta e preenchendo a lista completa
                IList<ProdutoVendido> listaCompleta = vendaItensDAO.SelecionarProdutosVendidosPorSqlNativo(sql);

                grid.DataSource = listaCompleta;
                OcultarColunasVazias(grid);
                // Exibe o botão de exportar
                btnExportarExcel.Visible = true;

                // Preenche o sumário
                GridSummary.PreencherSumario(grid, "ValorTotal", "Quantidade");
                chart1.Series.Clear();
                if (radioGrupo.Checked == false && radioMarca.Checked == false && radioVendedor.Checked == false)
                {
                    MostrarGraficoTop5ProdutosVendidos(listaCompleta);
                }

                // Exibe a mensagem de aviso se houver filtros aplicados
                if (!String.IsNullOrEmpty(txtCodGrupo.Texts) || !String.IsNullOrEmpty(txtCodMarca.Texts))
                {
                    lblInformacao.Visible = true;
                    lblInformacao.Text = "Atenção: Ao selecionar um grupo ou uma marca, apenas os produtos cadastrados\ncom esses" +
                        "critérios serão exibidos. Produtos que não possuem grupo ou marca\nno cadastro" +
                        "não aparecerão na lista.";
                }
                else
                {
                    lblInformacao.Visible = false; // Oculta a mensagem se não houver seleção
                }
            }
            catch (Exception erro)
            {
                
            }
        }

        private void MostrarGraficoTop5ProdutosVendidos(IList<ProdutoVendido> listaCompleta)
        {
          
            var topProdutos = listaCompleta
                .OrderByDescending(p => p.Quantidade) // Ordena pela quantidade vendida
                .Take(5) 
                .ToList();

            // Limpar séries existentes
            chart1.Series.Clear();

            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true; // O eixo X começa do zero
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true; // O eixo Y começa do zero
            // Criar uma nova série de colunas
            var series = new Series("Top 5 Produtos Vendidos")
            {
                ChartType = SeriesChartType.Column // Define o tipo de gráfico como coluna
            };

            // Adicionar dados à série
            foreach (var produto in topProdutos)
            {
                string descricaoTruncada = produto.Descricao.Length > 15
    ? produto.Descricao.Substring(0, 15) + "..." // Adiciona "..." se a descrição for maior que 15
    : produto.Descricao;
                series.Points.AddXY(produto.ID.ToString() + " - " + descricaoTruncada, produto.Quantidade);
            }

            // Adicionar a série ao gráfico
            chart1.Series.Add(series);

            // Configurações do eixo
            chart1.ChartAreas[0].AxisX.Title = "Produtos"; // Título do eixo X
            chart1.ChartAreas[0].AxisY.Title = "Quantidade Vendida"; // Título do eixo Y

            // Rotação dos rótulos do eixo X
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Rotaciona os rótulos em -45 graus
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = false; // Desabilita o ajuste automático
            chart1.ChartAreas[0].AxisX.Interval = 1; // Define o intervalo entre os rótulos

            // Aumente a largura do gráfico, se necessário
            chart1.Width = 600; // Ajuste conforme necessário
        }


        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidosGeral();
        }

        private string retornarSql()
        {
            string dataInicial = DateTime.Parse(txtDataInicial.Value.ToString()).ToString("yyyy-MM-dd 00:00:00");
            string dataFinal = DateTime.Parse(txtDataFinal.Value.ToString()).ToString("yyyy-MM-dd 23:59:59");

            string filtroGrupo = !string.IsNullOrEmpty(txtCodGrupo.Texts) ? " AND p.ProdutoGrupo = " + txtCodGrupo.Texts : "";
            string filtroMarca = !string.IsNullOrEmpty(txtCodMarca.Texts) ? " AND p.Marca = " + txtCodMarca.Texts : "";
            string filtroVendedor = !string.IsNullOrEmpty(txtCodVendedor.Texts) ? " AND v.Vendedor = " + txtCodVendedor.Texts : "";

            // Parte da consulta de venda
            string sqlVendas = @"
SELECT 
    p.ID, 
    p.Descricao, 
    g.Descricao AS Grupo,
    m.Descricao AS NomeMarca,
    SUM(vi.Quantidade) AS Quantidade, 
    SUM(vi.ValorFinal) AS ValorTotal 
FROM vendaitens vi 
INNER JOIN venda v ON vi.Venda = v.ID 
INNER JOIN produto p ON vi.Produto = p.ID 
LEFT JOIN produtogrupo g ON p.ProdutoGrupo = g.ID 
LEFT JOIN marca m ON p.Marca = m.ID 
WHERE v.DataVenda BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND v.Concluida = true 
AND v.Cancelado = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY p.ID, p.Descricao, g.Descricao, m.Descricao";

            // Parte da consulta de ordens de serviço
            string sqlOrdensServico = chkOrdemServico.Checked ? @"
UNION ALL 
SELECT 
    p.ID, 
    p.Descricao, 
    g.Descricao AS Grupo,
    m.Descricao AS NomeMarca,
    SUM(osp.Quantidade) AS Quantidade, 
    SUM(osp.VALORTOTAL) AS ValorTotal 
FROM ordemservicoproduto osp 
INNER JOIN ordemservico os ON osp.OrdemServico = os.ID 
INNER JOIN produto p ON osp.Produto = p.ID 
LEFT JOIN produtogrupo g ON p.ProdutoGrupo = g.ID 
LEFT JOIN marca m ON p.Marca = m.ID 
WHERE os.DATAENCERRAMENTO BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND os.STATUS = 'ENCERRADA' 
AND os.FLAGEXCLUIDO = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY p.ID, p.Descricao, g.Descricao, m.Descricao" : "";

            // Agrupamento por grupo
            if (radioGrupo.Checked)
            {
                sqlVendas = @"
SELECT 
    g.Descricao AS Grupo,
    SUM(vi.Quantidade) AS Quantidade, 
    SUM(vi.ValorFinal) AS ValorTotal 
FROM vendaitens vi 
INNER JOIN venda v ON vi.Venda = v.ID 
INNER JOIN produto p ON vi.Produto = p.ID 
LEFT JOIN produtogrupo g ON p.ProdutoGrupo = g.ID 
WHERE v.DataVenda BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND v.Concluida = true 
AND v.Cancelado = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY g.Descricao";

                if (chkOrdemServico.Checked)
                {
                    sqlOrdensServico = @"
UNION ALL 
SELECT 
    g.Descricao AS Grupo,
    SUM(osp.Quantidade) AS Quantidade, 
    SUM(osp.VALORTOTAL) AS ValorTotal 
FROM ordemservicoproduto osp 
INNER JOIN ordemservico os ON osp.OrdemServico = os.ID 
INNER JOIN produto p ON osp.Produto = p.ID 
LEFT JOIN produtogrupo g ON p.ProdutoGrupo = g.ID 
WHERE os.DATAENCERRAMENTO BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND os.STATUS = 'ENCERRADA' 
AND os.FLAGEXCLUIDO = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY g.Descricao";
                }
            }
            // Agrupamento por marca
            else if (radioMarca.Checked)
            {
                sqlVendas = @"
SELECT 
    m.Descricao AS NomeMarca,
    SUM(vi.Quantidade) AS Quantidade, 
    SUM(vi.ValorFinal) AS ValorTotal 
FROM vendaitens vi 
INNER JOIN venda v ON vi.Venda = v.ID 
INNER JOIN produto p ON vi.Produto = p.ID 
LEFT JOIN marca m ON p.Marca = m.ID 
WHERE v.DataVenda BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND v.Concluida = true 
AND v.Cancelado = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY m.Descricao";

                if (chkOrdemServico.Checked)
                {
                    sqlOrdensServico = @"
UNION ALL 
SELECT 
    m.Descricao AS NomeMarca,
    SUM(osp.Quantidade) AS Quantidade, 
    SUM(osp.VALORTOTAL) AS ValorTotal 
FROM ordemservicoproduto osp 
INNER JOIN ordemservico os ON osp.OrdemServico = os.ID 
INNER JOIN produto p ON osp.Produto = p.ID 
LEFT JOIN marca m ON p.Marca = m.ID 
WHERE os.DATAENCERRAMENTO BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND os.STATUS = 'ENCERRADA' 
AND os.FLAGEXCLUIDO = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY m.Descricao";
                }
            }
            // Agrupamento por vendedor
            else if (radioVendedor.Checked)
            {
                sqlVendas = @"
SELECT 
    v.Vendedor AS CodVendedor,
    SUM(vi.Quantidade) AS Quantidade, 
    SUM(vi.ValorFinal) AS ValorTotal 
FROM vendaitens vi 
INNER JOIN venda v ON vi.Venda = v.ID 
INNER JOIN produto p ON vi.Produto = p.ID 
WHERE v.DataVenda BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND v.Concluida = true 
AND v.Cancelado = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY v.Vendedor";

                if (chkOrdemServico.Checked)
                {
                    sqlOrdensServico = @"
UNION ALL 
SELECT 
    os.VENDEDOR AS CodVendedor,
    SUM(osp.Quantidade) AS Quantidade, 
    SUM(osp.VALORTOTAL) AS ValorTotal 
FROM ordemservicoproduto osp 
INNER JOIN ordemservico os ON osp.OrdemServico = os.ID 
INNER JOIN produto p ON osp.Produto = p.ID 
WHERE os.DATAENCERRAMENTO BETWEEN '" + dataInicial + @"' AND '" + dataFinal + @"' 
AND os.STATUS = 'ENCERRADA' 
AND os.FLAGEXCLUIDO = false" + filtroGrupo + filtroMarca + filtroVendedor + @"
GROUP BY os.VENDEDOR";
                }
            }

            // Combinar as duas partes da consulta
            string sql = sqlVendas + (chkOrdemServico.Checked ? sqlOrdensServico : "") + @"
ORDER BY Quantidade DESC;";

            return sql;
        }

        private async void OcultarColunasVazias(SfDataGrid dataGrid)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Primeiro, torna todas as colunas visíveis
                    foreach (var column in dataGrid.Columns)
                    {
                        column.Visible = true; // Garante que todas as colunas fiquem visíveis antes da nova verificação
                    }

                    // Se o radioMarca ou radioGrupo estiver marcado, ocultar a coluna Vendedor
                    if (radioMarca.Checked || radioGrupo.Checked)
                    {
                        var colunaVendedor = dataGrid.Columns["CodVendedor"];
                        if (colunaVendedor != null)
                        {
                            colunaVendedor.Visible = false;
                        }
                    }

                    // Oculta colunas que estão completamente vazias ou têm valores zero na coluna ID
                    foreach (var column in dataGrid.Columns)
                    {
                        bool isIdColumn = column.MappingName == "ID"; // Verifica se a coluna é a ID

                        // Verifica se há pelo menos um valor não vazio
                        var hasValue = dataGrid.View.Records
                            .Select(row => dataGrid.View.GetPropertyAccessProvider().GetValue(row.Data, column.MappingName))
                            .Any(cellValue =>
                                (isIdColumn && cellValue != null && Convert.ToInt32(cellValue) != 0) ||
                                (!isIdColumn && cellValue != null && !string.IsNullOrEmpty(cellValue.ToString())));

                        // Se não houver valores, oculta a coluna
                        if (!hasValue)
                        {
                            // Para atualizar a coluna, você precisa voltar para a thread da UI
                            this.Invoke(new Action(() => column.Visible = false));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // Registra ou exibe o erro
                GenericaDesktop.ShowAlerta("Erro ao ocultar colunas: " + ex.Message);
            }
        }



        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisarProdutosVendidosGeral();
        }

        private void btnPesquisaVender_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true"))
                {
                    txtCodVendedor.Texts = "";
                    txtVendedor.Texts = "";
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial.ToString();
                            txtCodVendedor.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            btnPesquisar.PerformClick();
                            break;
                    }
                    formBackground.Dispose();
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

        private void txtVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisaVender.PerformClick();
        }

        private void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisaVender.PerformClick();
        }

        private void txtDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnPesquisar.PerformClick();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }

        private void exportarExcel()
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ProdutosVendidos",
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.grid, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void btnPesquisaGrupo_Click(object sender, EventArgs e)
        {
            Object grupoObjeto = new ProdutoGrupo();
            txtGrupo.Texts = "";
            txtCodGrupo.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ProdutoGrupo", ""))
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
                    switch (uu.showModal("ProdutoGrupo", "", ref grupoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmGrupoProdutoCadastro form = new FrmGrupoProdutoCadastro();
                            if (form.showModalNovo(ref grupoObjeto) == DialogResult.OK)
                            {
                                txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                                txtCodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                            txtCodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                            btnPesquisar.PerformClick();
                            break;
                    }

                    formBackground.Dispose();
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

        private void btnPesquisaMarca_Click(object sender, EventArgs e)
        {
            Object marcaObjeto = new Marca();
            txtMarca.Texts = "";
            txtCodMarca.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Marca", ""))
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
                    switch (uu.showModal("Marca", "", ref marcaObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmMarcaCadastro form = new FrmMarcaCadastro();
                            if (form.showModalNovo(ref marcaObjeto) == DialogResult.OK)
                            {
                                txtMarca.Texts = ((Marca)marcaObjeto).Descricao;
                                txtCodMarca.Texts = ((Marca)marcaObjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtMarca.Texts = ((Marca)marcaObjeto).Descricao;
                            txtCodMarca.Texts = ((Marca)marcaObjeto).Id.ToString();
                            btnPesquisar.PerformClick();
                            break;
                    }

                    formBackground.Dispose();
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

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodGrupo.Texts = "";
            txtCodMarca.Texts = "";
            txtCodVendedor.Texts = "";
            txtGrupo.Texts = "";
            txtMarca.Texts = "";
            txtVendedor.Texts = "";
            radioGrupo.Checked = false;
            radioMarca.Checked = false;
            radioVendedor.Checked = false;
            btnPesquisar.PerformClick();
        }

        private void radioVendedor_CheckedChanged(object sender, EventArgs e)
        {
            if(radioVendedor.Checked == true)
                btnPesquisar.PerformClick();
        }

        private void radioGrupo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioGrupo.Checked == true)
                btnPesquisar.PerformClick();
        }

        private void radioMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMarca.Checked == true)
                btnPesquisar.PerformClick();
        }

        private void FrmVendaProdutos_Load(object sender, EventArgs e)
        {
            pesquisarProdutosVendidosGeral();
        }

        private void radioProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioProduto.Checked == true)
                btnPesquisar.PerformClick();
        }
    }
}
