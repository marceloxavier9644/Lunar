using iTextSharp.text.pdf.languages;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Compras
{
    public partial class FrmPrecificacaoPorNotaCompra : Form
    {
        private Nfe nfe = new Nfe();
        public FrmPrecificacaoPorNotaCompra(int idNotaFiscal)
        {
            InitializeComponent();

            if (idNotaFiscal > 0)
            {
                nfe = new Nfe();
                nfe.Id = idNotaFiscal;
                nfe = (Nfe)Controller.getInstance().selecionar(nfe);
                buscarProdutosNotaFiscal(nfe);
            }
        }

        private void buscarProdutosNotaFiscal(Nfe nfe)
        {
            IList<NfeProduto> listaProdutosNfe = new List<NfeProduto>();
            NfeProdutoController nfeProdutoController = new NfeProdutoController();
            listaProdutosNfe = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
            if(listaProdutosNfe.Count > 0)
            {
                double qtdProdutoNota = 0;
                foreach (NfeProduto nfeprod in listaProdutosNfe)
                {
                    qtdProdutoNota = qtdProdutoNota + nfeprod.QuantidadeEntrada;
                }
                foreach (NfeProduto nfeprod in listaProdutosNfe) 
                {
                    nfeprod.VUnCom = calcularCustoProduto(nfeprod, qtdProdutoNota);
                }
                gridProdutos.DataSource = listaProdutosNfe;
                gridProdutos.Refresh();
            }
        }

        public decimal calcularCustoProduto(NfeProduto nfeProd, double quantidadeProdutoNota)
        {
            decimal ipiUnitario = nfeProd.ValorIpi / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal stUnitario = nfeProd.VICMSSt / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal valorUnitItem = nfeProd.VProd / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
            decimal custoFreteItem = nfe.VFrete / decimal.Parse(quantidadeProdutoNota.ToString());
            decimal totalUnitario = valorUnitItem + ipiUnitario + stUnitario + custoFreteItem;

            return totalUnitario;
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void chkTodos_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}
