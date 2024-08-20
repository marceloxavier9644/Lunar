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
    public partial class ProdutoItemControl : UserControl
    {
        public ProdutoItemControl()
        {
            InitializeComponent();
        }
        public string NomeProduto
        {
            get => lblNomeProduto.Text;
            set => lblNomeProduto.Text = value;
        }

        public int Quantidade
        {
            get => int.Parse(lblQuantidade.Text);
            set => lblQuantidade.Text = value.ToString();
        }

        public decimal Preco
        {
            get => decimal.Parse(lblPreco.Text);
            set => lblPreco.Text = $"R$ {value:F2}";
        }
    }
}
