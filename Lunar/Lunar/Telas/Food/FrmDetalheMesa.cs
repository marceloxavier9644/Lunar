using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class FrmDetalheMesa : Form
    {
        public FrmDetalheMesa(int numeroMesa, int idMesa)
        {
            InitializeComponent();
            button1.Text = numeroMesa.ToString();

            InitializeDynamicButtons();
        }

        private void InitializeDynamicButtons()
        {
            // Exemplo de dados de pessoas
            string[] pessoas = { "GERAL", "João", "Maria", "Ana", "Carlos", "Marcelo", "Alane", "Miguel", "Arthur" };

            // Cria botões dinamicamente
            foreach (var pessoa in pessoas)
            {
                Button btn = new Button();
                btn.Text = pessoa;
                btn.Size = new Size(200, 50);
                btn.BackColor = Color.FromArgb(123, 19, 255);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Margin = new Padding(5);
                btn.Click += new EventHandler(Button_Click);
                btn.Font = new Font("Microsoft JhengHei", 12, FontStyle.Regular);

                flowLayoutPanel1.Controls.Add(btn);
            }

            // Adiciona o botão especial "Ocupar Mesa [F8]"
            Button ocuparMesaBtn = new Button();
            ocuparMesaBtn.Text = "Adicionar Conta [F9]";
            ocuparMesaBtn.Size = new Size(200, 50);
            ocuparMesaBtn.BackColor = Color.White;
            ocuparMesaBtn.ForeColor = Color.FromArgb(123, 19, 255);
            ocuparMesaBtn.FlatStyle = FlatStyle.Flat;
            ocuparMesaBtn.Margin = new Padding(5);
            ocuparMesaBtn.Click += new EventHandler(AdicionarConta_Click);
            ocuparMesaBtn.Font = new Font("Microsoft JhengHei", 12, FontStyle.Regular);

            flowLayoutPanel1.Controls.Add(ocuparMesaBtn);
        }
        private void AdicionarConta_Click(object sender, EventArgs e)
        {
            // Ação ao clicar no botão "Ocupar Mesa [F8]"
            MessageBox.Show("Adicionar Conta [F9]");
        }
        // Evento de clique para expandir e mostrar mais detalhes
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Alterna entre expandir e recolher
                if (clickedButton.Height == 50)
                {
                    clickedButton.Height = 100; // Expande o botão
                    Label detailsLabel = new Label();
                    detailsLabel.Text = $"Detalhes de {clickedButton.Text}";
                    detailsLabel.Size = new Size(180, 50);
                    detailsLabel.Location = new Point(10, 50); // Posição abaixo do texto do botão
                    detailsLabel.ForeColor = Color.White;
                    detailsLabel.BackColor = Color.Transparent;
                    clickedButton.Controls.Add(detailsLabel);
                }
                else
                {
                    clickedButton.Height = 50; // Recolhe o botão
                    clickedButton.Controls.Clear(); // Remove detalhes
                }
            }
        }

        private void btnMaisPessoas_Click(object sender, EventArgs e)
        {
            int quantidade = int.Parse(txtQuantidadePessoas.Text);
            if (quantidade >= 0)
            {
                quantidade++;
                txtQuantidadePessoas.Text = quantidade.ToString();
            }
        }

        private void btnMenosPessoas_Click(object sender, EventArgs e)
        {
            int quantidade = int.Parse(txtQuantidadePessoas.Text);
            if (quantidade > 1)
            {
                quantidade--;
                txtQuantidadePessoas.Text = quantidade.ToString();
            }
        }
    }
}
