using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.IntegracoesEcommerce.NuvemShopIntegration
{
    public partial class FrmIntegracaoNuvemShop : Form
    {
        public FrmIntegracaoNuvemShop()
        {
            InitializeComponent();
        }

        private async void btnListaProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                string products = await NuvemshopApi.GetProductsAsync(txtAccesstoken.Text, int.Parse(txtIdLoja.Text), txtEmailLoja.Text);
                Console.WriteLine(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private async void btnNovoProduto_Click(object sender, EventArgs e)
        {
            try
            {
                string response = await NuvemshopApi.CreateProductAsync(txtAccesstoken.Text, int.Parse(txtIdLoja.Text), "Produto de Teste", txtEmailLoja.Text);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
