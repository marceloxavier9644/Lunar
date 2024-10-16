using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.ProdutoDAO;

namespace Lunar.Utils.PesquisasClass
{
    public class ProdutoPesquisaService
    {
        private ProdutoDAO produtoDAO;
        private ProdutoGradeController produtoGradeController;
        private ProdutoController produtoController;
        string valorPesquisa = "";
        public ProdutoPesquisaService()
        {
            produtoDAO = new ProdutoDAO();
            produtoGradeController = new ProdutoGradeController();
            produtoController = new ProdutoController();
        }

        public (Produto produto, ProdutoGrade grade) PesquisarProduto(string valor)
        {
            this.valorPesquisa = valor;

            //busca por codigo de barras
            IList<ProdutoResult> produtos = PesquisarProdutoPorCodigoDeBarras(valor);
            if (produtos.Count == 1)
            {
                ProdutoResult produtoSelecionado = produtos[0];
                Produto prod = (Produto)produtoController.selecionar(new Produto { Id = produtoSelecionado.ProdutoId });
                ProdutoGrade grade = SelecionarGrade(prod);
                return (prod, grade);
            }
            else if (produtos.Count > 1)
            {
                IList<Produto> listaProd = ConverterProdutoResultParaProdutoList(produtos);
                Produto produtoEscolhido = SelecionarProduto(listaProd);
                if (produtoEscolhido != null)
                {
                    ProdutoGrade gradeSelecionada = SelecionarGrade(produtoEscolhido);
                    return (produtoEscolhido, gradeSelecionada);
                }
            }  
            //Encerra a busca por codigo de barras

            //Pesquisa por ID
            else if (ENumeroMenorQue5Digitos(valor))
            {
                IList<Produto> produtosPorId = PesquisarProdutoPorId(Convert.ToInt32(valor));
                if (produtosPorId.Count > 0)
                {
                    return ManipularResultadoPesquisa(produtosPorId);
                }
            }
            //Pesquisa por Codigo de Balanca
            else if (valor.StartsWith("2") && valor.Substring(6, 1).Equals("0") && valor.Substring(7, 1).Equals("0"))
            {
                ProcessarCodigoBarras(valor);
                return (null, null);
            }
            //Pesquisa por Referencia
            else if (valor.StartsWith("/"))
            {
                var produtosPorReferencia = PesquisarProdutoPorReferencia(valor);
                return ManipularResultadoPesquisa(produtosPorReferencia);
            }
            //Pesquisa por Descricao
            else
            {
                IList<Produto> produtosPorDescricao = new List<Produto>();
                if (valor.Length > 3)
                    produtosPorDescricao = PesquisarProdutoPorDescricao(valor);

                //caso o usuario nao tenha inserido pelo menos 3 digitos o sistema vai entrar direto na tela de pesquisa e pesquisar por la, paginando
                //assim passamos uma lista zerada e o sistema entra la..

                return ManipularResultadoPesquisa(produtosPorDescricao);
            }

            return (null, null);
        }

        private IList<ProdutoResult> PesquisarProdutoPorCodigoDeBarras(string codigoDeBarras)
        {
            if (IsNumericAndHasMoreThan7Digits(codigoDeBarras))
            {
                string sql = "SELECT p.Id AS ProdutoId, p.DESCRICAO AS ProdutoNome, pg.Id AS ProdutoGradeId, pg.Descricao AS DescricaoGrade, " +
                             "COALESCE(um.Descricao, p.UnidadeMedida) AS UnidadeMedidaDesc, " +
                             "p.UnidadeMedida AS UnidadeMedida, " +
                             "COALESCE(pg.ValorVenda, p.ValorVenda) AS ValorVenda, " +
                             "COALESCE(pcb.CodigoBarras, (SELECT pcb2.CodigoBarras FROM ProdutoCodigoBarras pcb2 WHERE pcb2.Produto = p.Id LIMIT 1)) AS CodigoBarras " +
                             "FROM Produto p " +
                             "LEFT JOIN ProdutoGrade pg ON p.Id = pg.Produto " +
                             "LEFT JOIN ProdutoCodigoBarras pcb ON pg.Id = pcb.PRODUTOGRADE " +
                             "LEFT JOIN UnidadeMedida um ON pg.UnidadeMedida = um.Id " +
                             "WHERE pcb.CodigoBarras = '" + codigoDeBarras + "' AND p.FlagExcluido = 0 AND pg.FlagExcluido = 0";

                return produtoDAO.selecionarProdutosPorSqlResult(sql);
            }

            return new List<ProdutoResult>();
        }

        private IList<Produto> PesquisarProdutoPorId(int produtoId)
        {
            string sql = $"FROM Produto Tabela WHERE Tabela.FlagExcluido = false AND Tabela.Id = {produtoId}";
            return produtoDAO.selecionarProdutosPorSql(sql);
        }

        private IList<Produto> PesquisarProdutoPorDescricao(string valor)
        {
            string sql = $"FROM Produto Tabela WHERE Tabela.FlagExcluido = false AND Tabela.Descricao LIKE '%"+valor+"%'";
            return produtoDAO.selecionarProdutosPorSql(sql);
        }

        private IList<Produto> PesquisarProdutoPorReferencia(string valor)
        {
            string sql = $"FROM Produto Tabela WHERE Tabela.FlagExcluido = false AND Tabela.Referencia = '{valor.Replace("/", "")}'";
            return produtoDAO.selecionarProdutosPorSql(sql);
        }

        private (Produto produto, ProdutoGrade grade) ManipularResultadoPesquisa(IList<Produto> produtos)
        {
            if (produtos.Count == 1)
            {
                Produto produtoSelecionado = produtos[0];
                ProdutoGrade gradeSelecionada = SelecionarGrade(produtoSelecionado);
                return (produtoSelecionado, gradeSelecionada);
            }
            else if (produtos.Count > 1 && produtos.Count <= 100)
            {
                Produto produtoEscolhido = SelecionarProduto(produtos);
                if (produtoEscolhido != null)
                {
                    ProdutoGrade gradeSelecionada = SelecionarGrade(produtoEscolhido);
                    return (produtoEscolhido, gradeSelecionada);
                }
            }
            else if (produtos.Count > 100)
            {
                //joga apenas o nome pesquisa pra proxima tela controlar pesquisa paginando, this.valorPesquisa
                Produto produtoEscolhido = SelecionarProdutoListaGrande();
                if (produtoEscolhido != null)
                {
                    ProdutoGrade gradeSelecionada = SelecionarGrade(produtoEscolhido);
                    return (produtoEscolhido, gradeSelecionada);
                }
            }
            else if (produtos.Count ==0)
            {
                //joga apenas o nome pesquisa pra proxima tela controlar pesquisa paginando, this.valorPesquisa
                Produto produtoEscolhido = SelecionarProdutoListaGrande();
                if (produtoEscolhido != null)
                {
                    ProdutoGrade gradeSelecionada = SelecionarGrade(produtoEscolhido);
                    return (produtoEscolhido, gradeSelecionada);
                }
            }

            return (null, null);
        }

        private IList<Produto> ConverterProdutoResultParaProdutoList(IList<ProdutoResult> produtos)
        {
            IList<Produto> listaReturn = new List<Produto>();
            foreach (ProdutoResult prods in produtos)
            {
                Produto produto = new Produto
                {
                    Id = prods.ProdutoId
                };
                produto = (Produto)Controller.getInstance().selecionar(produto);
                listaReturn.Add(produto);
            }
            return listaReturn;
        }

        public ProdutoGrade SelecionarGrade(Produto produto)
        {
            IList<ProdutoGrade> grades = produtoGradeController.selecionarGradePorProduto(produto.Id);
            if (grades.Count <= 1)
            {
                return grades.FirstOrDefault();
            }

            using (var formBackground = new Form())
            {
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = true;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();

                using (var frmSelecionarGrade = new FrmSelecionarGrade(produto))
                {
                    frmSelecionarGrade.StartPosition = FormStartPosition.CenterParent;
                    frmSelecionarGrade.FormBorderStyle = FormBorderStyle.FixedDialog;
                    if (frmSelecionarGrade.ShowDialog(formBackground) == DialogResult.OK)
                    {
                        return frmSelecionarGrade.GradeSelecionada;
                    }
                }

                formBackground.Dispose();
                return null;
            }
        }

        public Produto SelecionarProduto(IList<Produto> produtos)
        {
            Produto produtoSelecionado = null;

            using (var formSelecionarProduto = new FrmPesquisaProduto(produtos))
            {
                if (formSelecionarProduto.showModal(ref produtoSelecionado) == DialogResult.OK)
                {
                    return produtoSelecionado;
                }
            }

            return null;
        }

        public Produto SelecionarProdutoListaGrande()
        {
            Produto produtoSelecionado = null;

            using (var formSelecionarProduto = new FrmPesquisaProduto(valorPesquisa))
            {
                if (formSelecionarProduto.showModal(ref produtoSelecionado) == DialogResult.OK)
                {
                    return produtoSelecionado;
                }
            }

            return null;
        }

        public string ProcessarCodigoBarras(string codigoBarras)
        {
            string codigoProdutoComZeros = codigoBarras.Substring(1, 4);
            string codigoProduto = codigoProdutoComZeros.TrimStart('0');
            string valorTotalString = codigoBarras.Substring(7, 5);
            string valorUnitarioString = codigoBarras.Substring(12, 5);
            double valorTotal = double.Parse(valorTotalString) / 100;
            double valorUnitario = double.Parse(valorUnitarioString) / 100;

            return $"{codigoProduto} - {valorUnitario:C2} - {valorTotal:C2}";
        }

        public bool IsNumericAndHasMoreThan7Digits(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(char.IsDigit) && input.Length > 7;
        }

        private bool ENumeroMenorQue5Digitos(string valor)
        {
            return int.TryParse(valor, out int numero) && numero < 10000; // Verifica se é numérico e menor que 10000
        }
    }
}
