using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.Compras
{
    public partial class FrmImprimirEtiquetasOtica : Form
    {
        ProdutoController produtoController = new ProdutoController();
        Ean13 ean13 = new Ean13();
        Produto prod = new Produto();
        IList<Produto> listaProdutos = new List<Produto>();
        public FrmImprimirEtiquetasOtica(IList<Produto> listaProdutos)
        {
            InitializeComponent();
            this.listaProdutos = listaProdutos;
        }

        private void gerarEtiquetas()
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.DisplayName = "Etiquetas";
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            //IList<Produto> listaProdutos = produtoController.selecionarProdutosComVariosFiltros("LENTE", Sessao.empresaFilialLogada);
            if(listaProdutos.Count > 0)
            {
                foreach (Produto produto in listaProdutos)
                {
                    if (!String.IsNullOrEmpty(produto.Ean))
                    {
                        using (var bc = new BarcodeLib.Barcode())
                        {
                            for (int i = 0; i < produto.Estoque; i++)
                            {
                                dsEtiquetaOtica.Etiqueta.AddEtiquetaRow(produto.Id.ToString(), produto.Descricao, produto.ValorVenda, produto.Ean, produto.Referencia, ImageToByteArray(bc.Encode(BarcodeLib.TYPE.CODE128, produto.Ean)));
                            }
                        }
                    }
                    else
                    {
                        prod = new Produto();
                        prod = produto;
                        prod.Ean = gerarCodigoBarras();
                        while (String.IsNullOrEmpty(prod.Ean))
                        {
                            gerarCodigoBarras();
                        }
                        using (var bc = new BarcodeLib.Barcode())
                        {
                            for (int i = 0; i < produto.Estoque; i++)
                            {
                                dsEtiquetaOtica.Etiqueta.AddEtiquetaRow(produto.Id.ToString(), produto.Descricao, produto.ValorVenda, produto.Ean, produto.Referencia, ImageToByteArray(bc.Encode(BarcodeLib.TYPE.CODE128, prod.Ean)));
                            }
                        }
                    }
                }
                this.reportViewer1.RefreshReport();
            }
        }
        private void FrmImprimirEtiquetasOtica_Load(object sender, EventArgs e)
        {
            gerarEtiquetas();
        }

        public static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.RotateFlip(RotateFlipType.Rotate270FlipX);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
          
                return ms.ToArray();
            }
        }

        private string gerarCodigoBarras()
        {
            CreateEan13();
            IList<Produto> listaProd = produtoController.selecionarProdutosComVariosFiltros(ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit, Sessao.empresaFilialLogada);
            if (listaProd.Count == 0)
            {
                prod.Ean = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                Controller.getInstance().salvar(prod);
                return prod.Ean;
            }
            else
                return "";
            

        }
        private void CreateEan13()
        {
            ean13 = new Ean13();
            ean13.CountryCode = RandomNumber(10, 78).ToString();
            ean13.ManufacturerCode = RandomNumber(79000, 99000).ToString();
            ean13.ProductCode = RandomNumber(10000, 99000).ToString();
            ean13.ChecksumDigit = RandomNumber(1, 9).ToString();
            ean13.Scale = 1;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
