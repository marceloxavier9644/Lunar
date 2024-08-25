using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class ProdutoItemControl : UserControl
    {
        public ProdutoItemControl()
        {
            InitializeComponent();
            this.Load += ProdutoItemControl_Load;
            btnRemover.Click += btnRemover_Click;
        }
        public int IdProduto
        {
            get => int.Parse(lblIdProduto.Text);
            set => lblIdProduto.Text = value.ToString();
        }
        public string NomeProduto
        {
            get => lblNomeProduto.Text;
            set => lblNomeProduto.Text = value;
        }
        public string Observacoes
        {
            get => lblObservacoes.Text;
            set => lblObservacoes.Text = value;
        }

        public string Item
        {
            get => lblItem.Text;
            set => lblItem.Text = value;
        }

        public string Quantidade
        {
            get => lblQuantidade.Text;
            set => lblQuantidade.Text = value;
        }
        public decimal PrecoUnitario
        {
            get
            {
                string texto = lblPrecoUnitario.Text.Replace("R$", "").Trim();
                return decimal.TryParse(texto, out decimal result) ? result : 0m;
            }
            set => lblPrecoUnitario.Text = $"R$ {value:F2}";
        }

        public decimal PrecoFinal
        {
            get
            {
                string texto = lblPrecoFinal.Text.Replace("R$", "").Trim();
                return decimal.TryParse(texto, out decimal result) ? result : 0m;
            }
            set => lblPrecoFinal.Text = $"R$ {value:F2}";
        }

        public decimal Desconto
        {
            get
            {
                string texto = lblDesconto.Text.Replace("R$", "").Trim();
                return decimal.TryParse(texto, out decimal result) ? result : 0m;
            }
            set => lblDesconto.Text = $"R$ {value:F2}";
        }

        // Método para ajustar a largura do controle
        public void AjustarLargura()
        {
            // Verifica se o controle está dentro de um FlowLayoutPanel
            if (this.Parent is FlowLayoutPanel panel)
            {
                // Ajusta a largura do controle para se adaptar ao painel
                this.Width = panel.ClientSize.Width - 10; // Ajusta com uma margem de 10 pixels
                this.Invalidate(); // Solicita a atualização da aparência do controle
            }
        }

        private void ProdutoItemControl_Paint(object sender, PaintEventArgs e)
        {
            // Desenhe a linha abaixo do produto
            using (Pen pen = new Pen(Color.FromArgb(211, 212, 216), 2)) // Cor e largura da linha
            {
                // Calcula a posição da linha
                int y = lblPrecoUnitario.Bottom + 5; // 5 pixels abaixo do preço líquido

                // Desenha a linha
                e.Graphics.DrawLine(pen, 0, y, this.Width, y);
            }
        }

        private void ProdutoItemControl_Load(object sender, System.EventArgs e)
        {
            AjustarLargura();
        }

        private void ProdutoItemControl_ParentChanged(object sender, System.EventArgs e)
        {
            AjustarLargura();
        }
        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Remove o controle do FlowLayoutPanel
            if (this.Parent is FlowLayoutPanel parentPanel)
            {
                parentPanel.Controls.Remove(this);

                // Atualiza o valor total
                FrmPdvFood frmPdvFood = parentPanel.FindForm() as FrmPdvFood;
                if (frmPdvFood != null)
                {
                    frmPdvFood.AtualizarValorTotal();
                }
                else
                {
                    MessageBox.Show("Formulário não encontrado. Não foi possível atualizar o valor total.");
                }
            }
            else
            {
                MessageBox.Show("O controle pai não é um FlowLayoutPanel.");
            }
        }


    }
}
