using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.Vendas.Adicionais;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static LunarBase.ClassesDAO.ProdutoDAO;

namespace Lunar.Telas.Food
{
    public partial class FrmPdvFood : Form
    {
        GenericaDesktop generica = new GenericaDesktop();
        ProdutoController produtoController = new ProdutoController();
        IList<Produto> listaProdutos = new List<Produto>();
        Produto produto = new Produto();
        public FrmPdvFood()
        {
            InitializeComponent();
            selecionarGrupos();

            panel4.Paint += panel4_Paint;
            panel2.Paint += panel2_Paint;

            flowLayoutPanel2.Margin = new Padding(0); // Remove a margem externa
            flowLayoutPanel2.Padding = new Padding(0); // Remove o preenchimento interno
            flowLayoutPanel2.AutoScroll = true; // Adiciona barras de rolagem, se necessário

        }


        private void selecionarGrupos()
        {
            flowLayoutPanel1.Controls.Clear();
            ProdutoGrupoController produtoGrupoController = new ProdutoGrupoController();
            IList<ProdutoGrupo> listaGrupo = produtoGrupoController.selecionarTodosGruposFood();

            if (listaGrupo.Count > 0)
            {
                foreach (var grupo in listaGrupo)
                {
                    // Criar o botão
                    Button btn = new Button
                    {
                        Height = 60, // Altura fixa do botão
                        Width = 250,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        Font = new Font("Microsoft JhengHei", 12, FontStyle.Bold),
                        Margin = new Padding(5),
                        Tag = grupo, // Armazena o objeto ProdutoGrupo no botão
                        Cursor = Cursors.Hand // Define o cursor como 'Hand' quando o mouse passa sobre o botão
                    };

                    // Definir a cor da borda do botão
                    btn.FlatAppearance.BorderColor = Color.FromArgb(211, 212, 216);
                    btn.FlatAppearance.BorderSize = 1; // Espessura da borda

                    // Definir o texto do botão
                    btn.Text = "    " + grupo.Descricao; // Supondo que ProdutoGrupo tem uma propriedade Descricao

                    // Medir o tamanho do texto
                    Size textoSize = TextRenderer.MeasureText(btn.Text, btn.Font);

                    // Configurar a imagem do botão
                    if (!string.IsNullOrEmpty(grupo.CaminhoImagem) && File.Exists(grupo.CaminhoImagem))
                    {
                        try
                        {
                            Image img = Image.FromFile(grupo.CaminhoImagem);

                            // Ajusta o tamanho da imagem para a altura do botão
                            img = ResizeImage(img, btn.Height - 10); // Ajusta a altura da imagem
                            btn.Image = img;

                            // Ajusta o alinhamento da imagem e do texto
                            btn.ImageAlign = ContentAlignment.MiddleLeft; // Alinha a imagem à esquerda
                            btn.TextAlign = ContentAlignment.MiddleRight; // Alinha o texto à direita
                            btn.TextImageRelation = TextImageRelation.ImageBeforeText; // A imagem vem antes do texto

                            // Calcula o comprimento do botão baseado na imagem e no texto
                            int imagemLargura = btn.Image.Width + 10; // Margem adicional para a imagem
                                                                      //btn.Width = textoSize.Width + imagemLargura + 20; // Margem adicional para o texto e a imagem
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao carregar imagem: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Se não houver imagem, ajusta o comprimento apenas com base no texto
                        //btn.Width = textoSize.Width + 20; // Margem adicional para o texto
                        btn.Image = null; // Remove qualquer imagem, se presente
                        btn.ImageAlign = ContentAlignment.MiddleLeft; // Sem imagem, alinhe o texto à esquerda
                        btn.TextAlign = ContentAlignment.MiddleRight; // Alinha o texto à direita
                        btn.TextImageRelation = TextImageRelation.ImageBeforeText; // Imagem não está presente, então o texto fica alinhado à direita
                    }

                    btn.Click += Button_Click;
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
            else
            {
                MessageBox.Show("Nenhum grupo de produto encontrado.");
            }
        }


        private Image ResizeImage(Image image, int newHeight)
        {
            int newWidth = (int)(image.Width * ((float)newHeight / image.Height));
            var resizedImage = new Bitmap(image, new Size(newWidth, newHeight));
            return resizedImage;
        }


        private void Button_Click(object sender, EventArgs e)
        {
            // Obtém o botão que foi clicado
            Button btn = sender as Button;
            if (btn != null)
            {
                ProdutoGrupo grupoSelecionado = btn.Tag as ProdutoGrupo;
                if (grupoSelecionado != null)
                {
                    // Ação a ser tomada quando o botão é clicado
                    MessageBox.Show($"Grupo selecionado: {grupoSelecionado.Descricao}");
                }
            }
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Defina a cor da borda
                Color borderColor = Color.FromArgb(211, 212, 216); // RGB(211, 212, 216)
                int borderWidth = 2; // Largura da borda

                // Desenhe a borda
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Defina a cor da borda
                Color borderColor = Color.FromArgb(211, 212, 216); // RGB(211, 212, 216)
                int borderWidth = 2; // Largura da borda

                // Desenhe a borda
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                }
            }
        }
        private ProdutoGrade selecionarGrade(Produto produto)
        {
            using (var formBackground = new Form())
            {
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d; // Define a opacidade
                formBackground.BackColor = Color.Black; // Define a cor de fundo
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width; // Define a largura
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height; // Define a altura
                formBackground.WindowState = FormWindowState.Maximized; // Maximiza a janela
                formBackground.TopMost = true; // Garante que o formulário de fundo fique acima de outros
                formBackground.ShowInTaskbar = false; // Não mostra na barra de tarefas
                formBackground.Show(); // Exibe o formulário de fundo

                using (var frmSelecionarGrade = new FrmSelecionarGrade(produto))
                {
                    frmSelecionarGrade.StartPosition = FormStartPosition.CenterParent; // Centraliza em relação ao formulário de fundo
                    frmSelecionarGrade.FormBorderStyle = FormBorderStyle.FixedDialog; // Configura a borda do formulário
                    if (frmSelecionarGrade.ShowDialog(formBackground) == DialogResult.OK)
                    {
                        var gradeSelecionada = frmSelecionarGrade.GradeSelecionada;
                        if (gradeSelecionada != null)
                        {
                            return gradeSelecionada;
                        }
                    }
                }
                formBackground.Dispose();
                return null;
            }
        }
        private void PesquisarProdutoNovoMetodo(string valor)
        {
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            //Verifica se é codigo de barras
            if (IsNumericAndHasMoreThan7Digits(valor))
            {
                ProdutoDAO produtoDAO = new ProdutoDAO();
                //Consulta para casos que produtograde podem ser null
                //String sql = "SELECT p.Id AS ProdutoId, p.DESCRICAO AS ProdutoNome, pg.Id AS ProdutoGradeId, pg.Descricao AS DescricaoGrade, COALESCE(um.Descricao, p.UnidadeMedida) AS UnidadeMedida, COALESCE(pg.ValorVenda, p.ValorVenda) AS ValorVenda, COALESCE(pcb.CodigoBarras, (SELECT pcb2.CodigoBarras FROM ProdutoCodigoBarras pcb2 WHERE pcb2.Produto = p.Id LIMIT 1)) AS CodigoBarras FROM Produto p LEFT JOIN ProdutoGrade pg ON p.Id = pg.Produto LEFT JOIN ProdutoCodigoBarras pcb ON pg.Id = pcb.PRODUTOGRADE LEFT JOIN UnidadeMedida um ON pg.UnidadeMedida = um.Id WHERE (pcb.CodigoBarras = '" + valor+"' OR (pg.Id IS NULL AND pcb.CodigoBarras IS NULL AND p.Id IN (SELECT p2.Id FROM Produto p2 LEFT JOIN ProdutoCodigoBarras pcb2 ON p2.Id = pcb2.Produto WHERE pcb2.CodigoBarras = '"+valor+"'))) AND p.FlagExcluido = 0 AND (pg.FlagExcluido = 0 OR pg.FlagExcluido IS NULL) AND (pcb.FlagExcluido = 0 OR pcb.FlagExcluido IS NULL) AND (um.FlagExcluido = 0 OR um.FlagExcluido IS NULL);";

                //Consulta para casos que todos itens tenha uma grade
                String sql = "SELECT p.Id AS ProdutoId, p.DESCRICAO AS ProdutoNome, pg.Id AS ProdutoGradeId, pg.Descricao AS DescricaoGrade, COALESCE(um.Id, p.UnidadeMedida) AS UnidadeMedida, COALESCE(pg.ValorVenda, p.ValorVenda) AS ValorVenda, COALESCE(pcb.CodigoBarras, (SELECT pcb2.CodigoBarras FROM ProdutoCodigoBarras pcb2 WHERE pcb2.Produto = p.Id LIMIT 1)) AS CodigoBarras FROM Produto p JOIN ProdutoGrade pg ON p.Id = pg.Produto LEFT JOIN ProdutoCodigoBarras pcb ON pg.Id = pcb.ProdutoGrade LEFT JOIN UnidadeMedida um ON pg.UnidadeMedida = um.Id WHERE pcb.CodigoBarras = '" + valor + "' AND p.FlagExcluido = 0 AND pg.FlagExcluido = 0 AND (pcb.FlagExcluido = 0 OR pcb.FlagExcluido IS NULL) AND (um.FlagExcluido = 0 OR um.FlagExcluido IS NULL)";

                ProdutoGradeController produtoGradeController = new ProdutoGradeController();
                IList<ProdutoResult> lista = produtoDAO.selecionarProdutosPorSqlResult(sql);
                if (lista.Count == 1)
                {
                    desbloquearCamposValorQuantidade();
                    foreach (ProdutoResult prod in lista)
                    {
                        IList<ProdutoGrade> listaGrade = produtoGradeController.selecionarGradePorProduto(prod.ProdutoId);
                        if (Sessao.parametroSistema.SelecionarGradeEan == true && listaGrade.Count > 1)
                        {
                            Produto produtoSel = new Produto();
                            produtoSel.Id = prod.ProdutoId;
                            produtoSel = (Produto)Controller.getInstance().selecionar(produtoSel);
                            if (produtoSel.Grade == true)
                            {
                                ProdutoGrade produtoGrade = new ProdutoGrade();
                                produtoGrade = selecionarGrade(produtoSel);

                                if (produtoGrade != null)
                                {
                                    txtPesquisaProduto.Text = produtoSel.Descricao;
                                    txtQuantidade.Text = "1";
                                    txtValorUnitario.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                    txtValorTotal.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                    this.produto = produtoSel;
                                    this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                    //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;

                                    produto.GradePrincipal = produtoGrade;
                                    txtQuantidade.Focus();
                                    txtQuantidade.Select();
                                }
                            }
                        }
                        //1 Codigo de barras por grade, nao pergunta grade na tela de vendas se bipar o produto
                        else
                        {
                            txtPesquisaProduto.Text = prod.DescricaoGrade;
                            txtQuantidade.Text = "1";
                            txtValorUnitario.Text = string.Format("{0:0.00}", prod.ValorVenda);
                            txtValorTotal.Text = string.Format("{0:0.00}", prod.ValorVenda);
                            produto.Id = prod.ProdutoId;
                            produto = (Produto)Controller.getInstance().selecionar(produto);
                            UnidadeMedida unidadeMedida = new UnidadeMedida();
                            unidadeMedida.Id = prod.UnidadeMedida;
                            unidadeMedida = (UnidadeMedida)Controller.getInstance().selecionar(unidadeMedida);
                            //lblUnidadeMedida.Text = unidadeMedida.Sigla;
                            produto.UnidadeMedida = unidadeMedida;

                            ProdutoGrade prdgrade = new ProdutoGrade();
                            prdgrade.Id = int.Parse(prod.ProdutoGradeId.ToString());
                            prdgrade = (ProdutoGrade)Controller.getInstance().selecionar(prdgrade);
                            produto.GradePrincipal = prdgrade;


                            if (valorAux.Contains("*"))
                                txtQuantidade.Text = valorAux.Substring(0, valorAux.IndexOf("*"));
                            if (prod.CodigoBarras.Equals(valor.Trim()))
                                AdicionarProdutoAoFlowLayoutPanel(this.produto);
                            else
                            {
                                txtQuantidade.Focus();
                                txtQuantidade.Select();
                            }
                        }
                    }
                }

            }
            //Verifica se é Id do produto
            else if (ENumeroMenorQue5Digitos(valor))
            {
                listaProdutos = new List<Produto>();
                listaProdutos = produtoController.selecionarProdutosPorSql("From Produto Tabela Where Tabela.FlagExcluido = false and Tabela.Id = " + valor);
                if (listaProdutos.Count > 0)
                {
                    desbloquearCamposValorQuantidade();
                    foreach (Produto prod in listaProdutos)
                    {
                        if (prod.Veiculo == true)
                        {
                            FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(prod, false, true);
                            frmProdutoCadastro.ShowDialog();
                        }
                        if (prod.Grade == true)
                        {
                            ProdutoGrade produtoGrade = new ProdutoGrade();
                            produtoGrade = selecionarGrade(prod);

                            if (produtoGrade != null)
                            {
                                txtPesquisaProduto.Text = prod.Descricao;
                                txtQuantidade.Text = "1";
                                txtValorUnitario.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                txtValorTotal.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                this.produto = prod;
                                this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;

                                produto.GradePrincipal = produtoGrade;

                                //inserirItem(this.produto);
                                txtQuantidade.Focus();
                                txtQuantidade.Select();
                            }
                        }
                        else
                        {
                            txtPesquisaProduto.Text = prod.Descricao;
                            txtQuantidade.Text = "1";
                            txtValorUnitario.Text = string.Format("{0:0.00}", prod.ValorVenda);
                            txtValorTotal.Text = string.Format("{0:0.00}", prod.ValorVenda);
                            this.produto = prod;
                            if (valorAux.Contains("*"))
                                txtQuantidade.Text = valorAux.Substring(0, valorAux.IndexOf("*"));
                            if (prod.Ean.Equals(valor.Trim()))
                                AdicionarProdutoAoFlowLayoutPanel(this.produto);
                            else
                            {
                                txtQuantidade.Focus();
                                txtQuantidade.Select();
                            }
                        }
                    }
                }
                else
                {
                    //pode ser uma referencia, ai ele pesquisa por descricao e referencia
                    PesquisarProdutoPorDescricao(valor);
                }
            }
            //Pesquisa por descricao ou referencia
            else
            {
                PesquisarProdutoPorDescricao(valor);
            }
        }

        private void PesquisarProdutoPorDescricao(string valor)
        {
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {

                desbloquearCamposValorQuantidade();
                foreach (Produto prod in listaProdutos)
                {
                    if (prod.Veiculo == true)
                    {
                        FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(prod, false, true);
                        frmProdutoCadastro.ShowDialog();
                    }
                    if (prod.Grade == true)
                    {
                        ProdutoGrade produtoGrade = new ProdutoGrade();
                        produtoGrade = selecionarGrade(prod);

                        if (produtoGrade != null)
                        {
                            txtPesquisaProduto.Text = prod.Descricao;
                            txtQuantidade.Text = "1";
                            txtValorUnitario.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                            txtValorTotal.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                            this.produto = prod;
                            this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                            //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                            this.produto.GradePrincipal = produtoGrade;
                            //inserirItem(this.produto);
                            txtQuantidade.Focus();
                            txtQuantidade.Select();
                        }
                    }
                    else
                    {
                        txtPesquisaProduto.Text = prod.Descricao;
                        txtQuantidade.Text = "1";
                        txtValorUnitario.Text = string.Format("{0:0.00}", prod.ValorVenda);
                        txtValorTotal.Text = string.Format("{0:0.00}", prod.ValorVenda);
                        this.produto = prod;
                        if (valorAux.Contains("*"))
                            txtQuantidade.Text = valorAux.Substring(0, valorAux.IndexOf("*"));
                        if (prod.Ean.Equals(valor.Trim()))
                            AdicionarProdutoAoFlowLayoutPanel(this.produto);
                        else
                        {
                            txtQuantidade.Focus();
                            txtQuantidade.Select();
                        }
                    }
                }
            }
            else if (listaProdutos.Count > 1)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%'"))
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
                        switch (uu.showModal("Produto", "", ref produtoOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmProdutoCadastro form = new FrmProdutoCadastro();
                                if (form.showModalNovo(ref produtoOjeto, false) == DialogResult.OK)
                                {
                                    desbloquearCamposValorQuantidade();
                                    txtPesquisaProduto.Text = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidade.Text = "1";
                                    txtValorUnitario.Text = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotal.Text = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (this.produto.Grade == true)
                                    {
                                        ProdutoGrade produtoGrade = new ProdutoGrade();
                                        produtoGrade = selecionarGrade(produto);

                                        if (produtoGrade != null)
                                        {
                                            txtPesquisaProduto.Text = produto.Descricao;
                                            txtQuantidade.Text = "1";
                                            txtValorUnitario.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            txtValorTotal.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                            this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                            //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                            this.produto.GradePrincipal = produtoGrade;
                                            //inserirItem(this.produto);
                                            txtQuantidade.Focus();
                                            txtQuantidade.Select();
                                        }
                                    }
                                    else
                                    {
                                        if (valorAux.Contains("*"))
                                            txtQuantidade.Text = valorAux.Substring(0, valorAux.IndexOf("*"));
                                        if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                            AdicionarProdutoAoFlowLayoutPanel(this.produto);
                                        else
                                        {
                                            txtQuantidade.Focus();
                                            txtQuantidade.SelectAll();
                                        }
                                        if (((Produto)produtoOjeto).Veiculo == true)
                                        {
                                            FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                            frmProdutoCadastro.ShowDialog();
                                        }
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:

                                desbloquearCamposValorQuantidade();
                                txtPesquisaProduto.Text = ((Produto)produtoOjeto).Descricao;

                                Produto prod = ((Produto)produtoOjeto);
                                if (prod.Grade == true)
                                {
                                    ProdutoGrade produtoGrade = new ProdutoGrade();
                                    produtoGrade = selecionarGrade(prod);

                                    if (produtoGrade != null)
                                    {
                                        txtPesquisaProduto.Text = prod.Descricao;
                                        txtQuantidade.Text = "1";
                                        txtValorUnitario.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        txtValorTotal.Text = string.Format("{0:0.00}", produtoGrade.ValorVenda);
                                        this.produto = prod;
                                        this.produto.UnidadeMedida = produtoGrade.UnidadeMedida;
                                        //lblUnidadeMedida.Text = produtoGrade.UnidadeMedida.Sigla;
                                        this.produto.GradePrincipal = produtoGrade;
                                        //inserirItem(this.produto);
                                        txtQuantidade.Focus();
                                        txtQuantidade.Select();
                                    }
                                }
                                else
                                {

                                    txtQuantidade.Text = "1";
                                    txtValorUnitario.Text = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotal.Text = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidade.Text = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                        AdicionarProdutoAoFlowLayoutPanel(this.produto);
                                    else
                                    {
                                        txtQuantidade.Focus();
                                        txtQuantidade.SelectAll();
                                    }
                                    if (((Produto)produtoOjeto).Veiculo == true)
                                    {
                                        FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(((Produto)produtoOjeto), false, true);
                                        frmProdutoCadastro.ShowDialog();
                                    }
                                }
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
            else
            {
                bloquearCamposValorQuantidade();
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtPesquisaProduto.SelectAll();
            }
        }

        private void bloquearCamposValorQuantidade()
        {
            txtQuantidade.Enabled = false;
            txtValorUnitario.Enabled = false;
            //btnAdicionar.Enabled = false;
        }

        private void desbloquearCamposValorQuantidade()
        {
            txtQuantidade.Enabled = true;
            txtValorUnitario.Enabled = Sessao.permissoes.Contains("60");
            //btnAdicionar.Enabled = true;
        }

        public bool IsNumericAndHasMoreThan7Digits(string input)
        {
            // Verifica se a string não é nula ou vazia
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Verifica se a string é composta apenas por dígitos e tem mais de 7 caracteres
            return input.All(char.IsDigit) && input.Length > 7;
        }


        private int proximoNumeroItem = 1;
        private void AdicionarProdutoAoFlowLayoutPanel(Produto produto)
        {
            ProdutoItemControl itemControl = new ProdutoItemControl();

            itemControl.IdProduto = produto.Id;
            itemControl.NomeProduto = produto.Descricao.Length > 40
       ? produto.Descricao.Substring(0, 40)
       : produto.Descricao;
            itemControl.Quantidade = txtQuantidade.Text + "x";
            itemControl.PrecoUnitario = decimal.Parse(txtValorUnitario.Text);
            itemControl.Desconto = 0;
            itemControl.PrecoFinal = decimal.Parse(txtValorTotal.Text);
            //itemControl.Item = proximoNumeroItem++.ToString();

            flowLayoutPanel2.Controls.Add(itemControl);

            itemControl.AjustarLargura();
            AtualizarValorTotal();

            // Rolagem automática para o último item
            flowLayoutPanel2.VerticalScroll.Value = flowLayoutPanel2.VerticalScroll.Maximum;
            flowLayoutPanel2.PerformLayout(); // Força a atualização do layout
        }
        private void btnConfirmaProduto_Click(object sender, EventArgs e)
        {
            if(this.produto != null)
                AdicionarProdutoAoFlowLayoutPanel(this.produto);
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProdutoPorDescricao(txtPesquisaProduto.Text.Trim());
        }

        public bool ENumeroMenorQue5Digitos(string input)
        {
            // Verifica se a string não é nula ou vazia
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Verifica se a string é composta apenas por dígitos e tem mais de 7 caracteres
            return input.All(char.IsDigit) && input.Length <= 5;
        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProdutoNovoMetodo(txtPesquisaProduto.Text);
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtCodProduto.Text, e);
            if (e.KeyChar == 13)
            {
                PesquisarProdutoNovoMetodo(txtCodProduto.Text);
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            generica.SoNumero(txtQuantidade.Text, e);
        }

        private List<Produto> ResgatarProdutosDoFlowLayoutPanel()
        {
            List<Produto> listaProdutos = new List<Produto>();

            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is ProdutoItemControl itemControl)
                {
                    VendaItens vendaItem = new VendaItens
                    {
                        //Quantidade = itemControl.Quantidade,
                        //PrecoBruto = itemControl.PrecoBruto,
                        //Desconto = itemControl.Desconto,
                        //PrecoLiquido = itemControl.PrecoLiquido
                    };

                    listaProdutos.Add(produto);
                }
            }

            return listaProdutos;
        }

        private void flowLayoutPanel2_Layout(object sender, LayoutEventArgs e)
        {
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is ProdutoItemControl itemControl)
                {
                    itemControl.AjustarLargura();
                }
            }
        }

        public void AtualizarValorTotal()
        {
            decimal valorTotal = 0;

            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is ProdutoItemControl itemControl)
                {
                    valorTotal += itemControl.PrecoFinal;
                }
            }
            AtualizarNumeracaoItem();
            lblValorTotal.Text = $"TOTAL {valorTotal:C2}";
        }
        private void AtualizarNumeracaoItem()
        {
            int numero = 1;
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is ProdutoItemControl itemControl)
                {
                    itemControl.Item = "Item: " + numero++.ToString();
                }
            }
        }




    }
}
