using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class FrmPdvFood : Form
    {
        public FrmPdvFood()
        {
            InitializeComponent();
            selecionarGrupos();

            panel4.Paint += panel4_Paint;
            panel2.Paint += panel2_Paint;
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

        private void AdicionarProdutoNoFlowPanel(int produtoId, string nomeProduto, int quantidade, decimal preco)
        {
            var produtoControl = new ProdutoItemControl
            {
                NomeProduto = nomeProduto,
                Quantidade = quantidade,
                Preco = preco
            };

            flowLayoutPanel1.Controls.Add(produtoControl);
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
    }
}
