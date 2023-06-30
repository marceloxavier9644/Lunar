using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Syncfusion.Windows.Forms.CellGrid.ScrollAxis;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LunarBase.Utilidades.ManifestoDownload;
using Exception = System.Exception;
using Nfe = LunarBase.Classes.Nfe;

namespace Lunar.Telas.Compras
{
	public partial class FrmLancarNotaFiscalCompra : Form
	{
		Dictionary<RowColumnIndex, Color> colorDict = new Dictionary<RowColumnIndex, Color>();
		Dictionary<RowColumnIndex, Color> colorDict2 = new Dictionary<RowColumnIndex, Color>();
		IList<NfeProduto> listaProdutos = new List<NfeProduto>();
		IList<TNFeInfNFeDet> lista;
		Nfe nfe = new Nfe();
		TNfeProc notaXML = new TNfeProc();
		NfeProduto nfeProd = new NfeProduto();
		GenericaDesktop generica = new GenericaDesktop();
		Produto prod = new Produto();
		bool verificado = false;
		ProdutoController produtoController = new ProdutoController();
		ContaPagar contaPagar = new ContaPagar();
		IList<ContaPagar> listaContaPagar = new List<ContaPagar>();
		public FrmLancarNotaFiscalCompra(TNfeProc notaFiscal, Nfe nfBanco)
		{
			InitializeComponent();
			notaXML = notaFiscal;
			nfe = nfBanco;

			alimentarInformacoes();

			txtChave.Enabled = true;
			txtChave.ReadOnly = true;
			txtFornecedor.ReadOnly = true;
			txtCNPJFornecedor.ReadOnly = true;
			gridProdutos.AllowEditing = true;
			txtEmpresaLogada.Texts = Sessao.empresaFilialLogada.NomeFantasia;


			verificarProdutosCadastrados();
		
		}

		private void alimentarInformacoes()
		{
			if(Sessao.parametroSistema.PlanoContaCompraRevenda != null)
            {
				txtPlanoContas.Texts = Sessao.parametroSistema.PlanoContaCompraRevenda.Descricao;
				txtCodPlanoContas.Texts = Sessao.parametroSistema.PlanoContaCompraRevenda.Id.ToString();
			}

			txtDataEmissao.Text = nfe.DataEmissao.ToShortDateString();
			txtDataLancamento.Value = DateTime.Parse(txtDataEmissao.Text);
			txtChave.Texts = nfe.Chave;
			txtCidadeFornecedor.Texts = notaXML.NFe.infNFe.emit.enderEmit.xMun;
			txtFornecedor.Texts = notaXML.NFe.infNFe.emit.xNome;
			if (notaXML.NFe.infNFe.emit.Item.Length == 14)
				txtCNPJFornecedor.Texts = generica.FormatarCNPJ(notaXML.NFe.infNFe.emit.Item);
			else if (notaXML.NFe.infNFe.emit.Item.Length == 11)
				txtCNPJFornecedor.Texts = generica.FormatarCPF(notaXML.NFe.infNFe.emit.Item);
			txtNumeroNota.Texts = nfe.NNf;
			txtSerie.Texts = nfe.Serie;
			txtUFFornecedor.Texts = notaXML.NFe.infNFe.emit.enderEmit.UF.ToString();
			txtValorTotal.Texts = nfe.VNf.ToString("C2", CultureInfo.CurrentCulture);

			txtFrete.Texts = nfe.VFrete.ToString("C2", CultureInfo.CurrentCulture);
			txtDesconto.Texts = nfe.VDesc.ToString("C2", CultureInfo.CurrentCulture);
			txtICMS.Texts = nfe.VIcms.ToString("C2", CultureInfo.CurrentCulture);
			txtIPI.Texts = nfe.VIpi.ToString("C2", CultureInfo.CurrentCulture);
			txtICMSSt.Texts = nfe.VSt.ToString("C2", CultureInfo.CurrentCulture);

			//TNFeInfNFeDetProd detprod;
			//for (int k = 0; k < notaXML.NFe.infNFe.det.Length; k++)
			//{
			//    detprod = notaXML.NFe.infNFe.det[k].prod;

			//}
			listaProdutos = new List<NfeProduto>();
			for (int k = 0; k < notaXML.NFe.infNFe.det.Length; k++)
			{
				double qtd = 0;
				nfeProd = new NfeProduto();
				nfeProd.CProd = notaXML.NFe.infNFe.det[k].prod.cProd;
				string cfopConvertido = generica.ConversaoCFOP(notaXML.NFe.infNFe.det[k].prod.CFOP);
				nfeProd.CfopEntrada = cfopConvertido; // FAZER TABELA DE CONVERSAO
				nfeProd.CodigoInterno = "";
				nfeProd.DescricaoInterna = "";
				qtd = double.Parse(nfeProd.XProd = notaXML.NFe.infNFe.det[k].prod.qCom.Replace(".", ","));
				nfeProd.QCom = qtd.ToString();
				nfeProd.QuantidadeEntrada = double.Parse(nfeProd.QCom);
				nfeProd.VProd = decimal.Parse(notaXML.NFe.infNFe.det[k].prod.vProd.Replace(".", ","));
                if(notaXML.NFe.infNFe.det[k].prod.vDesc != null) 
					nfeProd.VDesc = decimal.Parse(notaXML.NFe.infNFe.det[k].prod.vDesc.Replace(".", ","));
                nfeProd.XProd = notaXML.NFe.infNFe.det[k].prod.xProd;
				nfeProd.Ncm = notaXML.NFe.infNFe.det[k].prod.NCM;
				nfeProd.Cfop = notaXML.NFe.infNFe.det[k].prod.CFOP;
				nfeProd.Cest = notaXML.NFe.infNFe.det[k].prod.CEST;
				nfeProd.VUnCom = decimal.Parse(notaXML.NFe.infNFe.det[k].prod.vUnCom.Replace(".", ","));
				nfeProd.CEANTrib = notaXML.NFe.infNFe.det[k].prod.cEANTrib;
				nfeProd.CEan = notaXML.NFe.infNFe.det[k].prod.cEAN;
				nfeProd.UCom = notaXML.NFe.infNFe.det[k].prod.uCom;
				nfeProd.Item = notaXML.NFe.infNFe.det[k].nItem;
                //notaXML.NFe.infNFe.det[k].imposto.Items[].

                coletarPIS(k);
				coletarIPI(k);
				coletarCofins(k);
				coletarICMSProdutos(k);

                listaProdutos.Add(nfeProd);
			}

			listaContaPagar = new List<ContaPagar>();
			if (notaXML.NFe.infNFe.cobr != null)
			{
				if (notaXML.NFe.infNFe.cobr.dup != null)
				{
					for (int k = 0; k < notaXML.NFe.infNFe.cobr.dup.Length; k++)
					{
						contaPagar = new ContaPagar();
						contaPagar.NDup = notaXML.NFe.infNFe.cobr.dup[k].nDup;
						contaPagar.DataOrigem = nfe.DataEmissao;
						contaPagar.Descricao = "COMPRA NFE: " + nfe.NNf;
						contaPagar.DVenc = DateTime.Parse(notaXML.NFe.infNFe.cobr.dup[k].dVenc);
						contaPagar.EmpresaFilial = Sessao.empresaFilialLogada;
						contaPagar.FormaPagamento = null;
						contaPagar.Nfe = nfe;
						contaPagar.NumeroDocumento = nfe.NNf;
						contaPagar.Pago = false;
						contaPagar.Pessoa = null;
						contaPagar.PlanoConta = null;
						contaPagar.ValorTotal = nfe.VNf;
						contaPagar.VDup = decimal.Parse(notaXML.NFe.infNFe.cobr.dup[k].vDup.Replace(".", ","));

						listaContaPagar.Add(contaPagar);
					}
				}
				FormaPagamento formaPagamento = new FormaPagamento();
				if (listaContaPagar.Count > 1)
				{
					radioNaoPagas.Checked = true;
					formaPagamento.Id = 5;
					formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
					txtFormaPagamento.Texts = formaPagamento.Descricao;
					txtCodFormaPagamento.Texts = formaPagamento.Id.ToString();
				}
				else
				{
					radioPagas.Checked = true;
					formaPagamento.Id = 3;
					formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
					txtFormaPagamento.Texts = formaPagamento.Descricao;
					txtCodFormaPagamento.Texts = formaPagamento.Id.ToString();
				}
			}

			//GRID DE PRODUTOS
			sfDataPager1.DataSource = listaProdutos;
			sfDataPager1.PageSize = 999;
			gridProdutos.DataSource = sfDataPager1.PagedSource;
			sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
			gridProdutos.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
			this.gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
			this.gridProdutos.AutoSizeController.Refresh();

			//GRID CONTAS A PAGAR
			sfDataPager2.DataSource = listaContaPagar;
			sfDataPager2.PageSize = 999;
			gridPagamento.DataSource = sfDataPager2.PagedSource;
			sfDataPager2.OnDemandLoading += sfDataPager2_OnDemandLoading;
			gridPagamento.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
			this.gridPagamento.AutoSizeController.ResetAutoSizeWidthForAllColumns();
			this.gridPagamento.AutoSizeController.Refresh();
		}

		private void coletarCofins(int k)
        {
			//COFINSAliq
			if (notaXML.NFe.infNFe.det[k].imposto.COFINS.Item.ToString().Contains("TNFeInfNFeDetImpostoCOFINSCOFINSAliq"))
			{
				var cofins = (TNFeInfNFeDetImpostoCOFINSCOFINSAliq)notaXML.NFe.infNFe.det[k].imposto.COFINS.Item;
				nfeProd.AliqCofins = cofins.pCOFINS;
				nfeProd.BaseCofins = decimal.Parse(cofins.vBC.Replace(".", ","));
				//TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST cstco = new TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST();
				nfeProd.CstCofins = cofins.CST.ToString().Replace("Item", "");
				if (cofins.vCOFINS != null)
					nfeProd.ValorCofins = decimal.Parse(cofins.vCOFINS.Replace(".", ","));
				nfeProd.ValorIsentoCofins = 0;
			}
			//COFINSCOFINSNT
			if (notaXML.NFe.infNFe.det[k].imposto.COFINS.Item.ToString().Contains("TNFeInfNFeDetImpostoCOFINSCOFINSNT"))
			{
				var cofins = (TNFeInfNFeDetImpostoCOFINSCOFINSNT)notaXML.NFe.infNFe.det[k].imposto.COFINS.Item;
				nfeProd.CstCofins = cofins.CST.ToString().Replace("Item", "");
				nfeProd.ValorIsentoCofins = 0;
			}
			//COFINSOutr
			if (notaXML.NFe.infNFe.det[k].imposto.COFINS.Item.ToString().Contains("TNFeInfNFeDetImpostoCOFINSCOFINSOutr"))
			{
				var cofins = (TNFeInfNFeDetImpostoCOFINSCOFINSOutr)notaXML.NFe.infNFe.det[k].imposto.COFINS.Item;
				nfeProd.CstCofins = cofins.CST.ToString().Replace("Item", "");
				if (cofins.vCOFINS != null)
					nfeProd.ValorCofins = decimal.Parse(cofins.vCOFINS.Replace(".", ","));

				for (int u = 0; u < cofins.Items.Length; u++)
				{
					if (cofins.ItemsElementName[u].ToString().Equals("qBCProd"))
						nfeProd.QBCProdCofins = cofins.Items[u];
					if (cofins.ItemsElementName[u].ToString().Equals("pCOFINS"))
						nfeProd.AliqCofins = cofins.Items[u];
					if (cofins.ItemsElementName[u].ToString().Equals("vAliqProd"))
						nfeProd.VAliqProdCofins = decimal.Parse(cofins.Items[u].Replace(".", ","));
					if (cofins.ItemsElementName[u].ToString().Equals("vBC"))
						nfeProd.BaseCofins = decimal.Parse(cofins.Items[u].Replace(".", ","));
				}
			}
			//COFINSCOFINSQTD
			if (notaXML.NFe.infNFe.det[k].imposto.COFINS.Item.ToString().Contains("TNFeInfNFeDetImpostoCOFINSCOFINSQtde"))
			{
				var cofins = (TNFeInfNFeDetImpostoCOFINSCOFINSQtde)notaXML.NFe.infNFe.det[k].imposto.COFINS.Item;
				nfeProd.CstCofins = cofins.CST.ToString().Replace("Item", "");
				nfeProd.QBCProdCofins = cofins.qBCProd;
				nfeProd.VAliqProdCofins = decimal.Parse(cofins.vAliqProd.Replace(".", ","));
				nfeProd.ValorCofins = decimal.Parse(cofins.vCOFINS.Replace(".", ","));
				nfeProd.ValorIsentoCofins = 0;
			}

			//TNFeInfNFeDetImpostoCOFINSST
			if (notaXML.NFe.infNFe.det[k].imposto.COFINSST != null)
			{
				if (notaXML.NFe.infNFe.det[k].imposto.COFINSST.ToString().Contains("TNFeInfNFeDetImpostoCOFINSST"))
				{
					var cofins = (TNFeInfNFeDetImpostoCOFINSST)notaXML.NFe.infNFe.det[k].imposto.COFINSST;
					nfeProd.IndSomaCOFINSST = cofins.indSomaCOFINSST.ToString().Replace("Item", "");
					if (cofins.vCOFINS != null)
						nfeProd.ValorCofins = decimal.Parse(cofins.vCOFINS.Replace(".", ","));

					for (int u = 0; u < cofins.Items.Length; u++)
					{
						if (cofins.ItemsElementName[u].ToString().Equals("qBCProd"))
							nfeProd.QBCProdCofins = cofins.Items[u];
						if (cofins.ItemsElementName[u].ToString().Equals("pCOFINS"))
							nfeProd.AliqCofins = cofins.Items[u];
						if (cofins.ItemsElementName[u].ToString().Equals("vAliqProd"))
							nfeProd.VAliqProdCofins = decimal.Parse(cofins.Items[u].Replace(".", ","));
						if (cofins.ItemsElementName[u].ToString().Equals("vBC"))
							nfeProd.BaseCofins = decimal.Parse(cofins.Items[u].Replace(".", ","));
					}
				}
			}
		}

		private void coletarPIS(int k)
		{
			//PISAliq
			if (notaXML.NFe.infNFe.det[k].imposto.PIS.Item.ToString().Contains("TNFeInfNFeDetImpostoPISPISAliq"))
			{
				var pis = (TNFeInfNFeDetImpostoPISPISAliq)notaXML.NFe.infNFe.det[k].imposto.PIS.Item;
				nfeProd.AliqPis = pis.pPIS;
				nfeProd.BasePis = decimal.Parse(pis.vBC.Replace(".", ","));
				//TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST cstco = new TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST();
				nfeProd.CstPis = pis.CST.ToString().Replace("Item", "");
				if (pis.vPIS != null)
					nfeProd.ValorPis = decimal.Parse(pis.vPIS.Replace(".", ","));
				nfeProd.ValorIsentoPis = 0;
			}
			//PISNT
			if (notaXML.NFe.infNFe.det[k].imposto.PIS.Item.ToString().Contains("TNFeInfNFeDetImpostoPISPISNT"))
			{
				var pis = (TNFeInfNFeDetImpostoPISPISNT)notaXML.NFe.infNFe.det[k].imposto.PIS.Item;
				nfeProd.CstPis = pis.CST.ToString().Replace("Item", "");
				nfeProd.ValorIsentoPis = 0;
			}
			//PISOutr
			if (notaXML.NFe.infNFe.det[k].imposto.PIS.Item.ToString().Contains("TNFeInfNFeDetImpostoPISPISOutr"))
			{
				var pis = (TNFeInfNFeDetImpostoPISPISOutr)notaXML.NFe.infNFe.det[k].imposto.PIS.Item;
				nfeProd.CstPis = pis.CST.ToString().Replace("Item", "");
				if (pis.vPIS != null)
					nfeProd.ValorPis = decimal.Parse(pis.vPIS.Replace(".", ","));

				for (int u = 0; u < pis.Items.Length; u++)
				{
					if (pis.ItemsElementName[u].ToString().Equals("qBCProd"))
						nfeProd.QBCProdPis = pis.Items[u];
					if (pis.ItemsElementName[u].ToString().Equals("pPIS"))
						nfeProd.AliqPis = pis.Items[u];
					if (pis.ItemsElementName[u].ToString().Equals("vAliqProd"))
						nfeProd.VAliqProdPis = decimal.Parse(pis.Items[u].Replace(".", ","));
					if (pis.ItemsElementName[u].ToString().Equals("vBC"))
						nfeProd.BasePis = decimal.Parse(pis.Items[u].Replace(".", ","));
				}
			}
			//PISQtde
			if (notaXML.NFe.infNFe.det[k].imposto.COFINS.Item.ToString().Contains("TNFeInfNFeDetImpostoPISPISQtde"))
			{
				var pis = (TNFeInfNFeDetImpostoPISPISQtde)notaXML.NFe.infNFe.det[k].imposto.PIS.Item;
				nfeProd.CstPis = pis.CST.ToString().Replace("Item", "");
				nfeProd.QBCProdPis = pis.qBCProd;
				nfeProd.VAliqProdPis = decimal.Parse(pis.vAliqProd.Replace(".", ","));
				nfeProd.ValorPis = decimal.Parse(pis.vPIS.Replace(".", ","));
				nfeProd.ValorIsentoPis = 0;
			}

			//TNFeInfNFeDetImpostoPISST
			if (notaXML.NFe.infNFe.det[k].imposto.PISST != null)
			{
				if (notaXML.NFe.infNFe.det[k].imposto.PISST.ToString().Contains("TNFeInfNFeDetImpostoPISST"))
				{
					var pis = (TNFeInfNFeDetImpostoPISST)notaXML.NFe.infNFe.det[k].imposto.PISST;
					nfeProd.IndSomaPISST = pis.indSomaPISST.ToString().Replace("Item", "");
					if (pis.vPIS != null)
						nfeProd.ValorPis = decimal.Parse(pis.vPIS.Replace(".", ","));

					for (int u = 0; u < pis.Items.Length; u++)
					{
						if (pis.ItemsElementName[u].ToString().Equals("qBCProd"))
							nfeProd.QBCProdPis = pis.Items[u];
						if (pis.ItemsElementName[u].ToString().Equals("pPIS"))
							nfeProd.AliqPis = pis.Items[u];
						if (pis.ItemsElementName[u].ToString().Equals("vAliqProd"))
							nfeProd.VAliqProdPis = decimal.Parse(pis.Items[u].Replace(".", ","));
						if (pis.ItemsElementName[u].ToString().Equals("vBC"))
							nfeProd.BasePis = decimal.Parse(pis.Items[u].Replace(".", ","));
					}
				}
			}
		}

		private void coletarIPI(int k)
		{
			//ipi
			if (notaXML.NFe.infNFe.det[k].imposto.Items.Length > 1)
			{
				for (int h = 0; h < notaXML.NFe.infNFe.det[k].imposto.Items.Length; h++)
				{
					//EM ITEMS[2] DEFINE QUE É IPI - OBS RESTANTE
					if (notaXML.NFe.infNFe.det[k].imposto.Items[h].ToString().Contains("TIpi"))
					{
						var ipi = (TIpi)notaXML.NFe.infNFe.det[k].imposto.Items[h];

						if(ipi.cEnq != null)
							nfeProd.CodEnqIpi = ipi.cEnq.ToString().Replace("Item", "");
						if (ipi.cSelo != null)
							nfeProd.CodSeloIpi = ipi.cSelo.ToString().Replace("Item", "");
						if (ipi.qSelo != null)
							nfeProd.QSeloIpi = ipi.qSelo.ToString().Replace("Item", "");
						if (ipi.CNPJProd != null)
							nfeProd.CnpjProdIpi = ipi.CNPJProd.ToString().Replace("Item", "");

						if (ipi.Item != null)
						{

							if (ipi.Item.ToString().Contains("TIpiIPINT"))
							{
								var ipiInt = (TIpiIPINT)ipi.Item;
								nfeProd.CstIpi = ipiInt.CST.ToString().Replace("Item", "");
							}
							if (ipi.Item.ToString().Contains("TIpiIPITrib"))
							{
								var ipiTrib = (TIpiIPITrib)ipi.Item;

								nfeProd.CstIpi = ipiTrib.CST.ToString().Replace("Item", "");
								if (ipiTrib.vIPI != null)
									nfeProd.ValorIpi = decimal.Parse(ipiTrib.vIPI.Replace(".", ","));

								for (int u = 0; u < ipiTrib.Items.Length; u++)
								{
									if (ipiTrib.ItemsElementName[u].ToString().Equals("pIPI"))
										nfeProd.AliqIpi = ipiTrib.Items[u];
									if (ipiTrib.ItemsElementName[u].ToString().Equals("qUnid"))
										nfeProd.QUnidIpi = ipiTrib.Items[u];
									if (ipiTrib.ItemsElementName[u].ToString().Equals("vBC"))
										nfeProd.BaseIpi = decimal.Parse(ipiTrib.Items[u].Replace(".", ","));
									if (ipiTrib.ItemsElementName[u].ToString().Equals("vUnid"))
										nfeProd.VUnidIpi = decimal.Parse(ipiTrib.Items[u].Replace(".", ","));
								}
							}
						}
					}
				}
			}
		}


		private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
		{
			sfDataPager1.LoadDynamicData(e.StartRowIndex, listaProdutos.Skip(e.StartRowIndex).Take(e.PageSize));
		}

		private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
		{
			if (e.RowIndex % 2 == 0)
				e.Style.BackColor = Color.WhiteSmoke;
			else
				e.Style.BackColor = Color.White;

			var rowColumnIndex = new RowColumnIndex(e.RowIndex, 0);
			if (colorDict.ContainsKey(rowColumnIndex))
			{
				e.Style.BackColor = colorDict[rowColumnIndex];
				//e.Style.TextColor = colorDict2[rowColumnIndex];
				this.gridProdutos.Style.SelectionStyle.BackColor = Color.Transparent;
				//this.gridProdutos.Style.SelectionStyle.TextColor = Color.DarkBlue;
			}
		}

		void SetCellBackgroundColor(RowColumnIndex rowColumnIndex, Color color)
		{
			if (!colorDict.ContainsKey(rowColumnIndex))
			{
				colorDict.Add(rowColumnIndex, color);
			}
			else
			{
				colorDict[rowColumnIndex] = color;
			}
		}

		void SetCellTextColor(RowColumnIndex rowColumnIndex, Color color)
		{
			if (!colorDict2.ContainsKey(rowColumnIndex))
			{
				colorDict2.Add(rowColumnIndex, color);
			}
			else
			{
				colorDict2[rowColumnIndex] = color;
			}
		}

		private void btnFechar_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		// Ao salvar a nota gravar fornecedor.
		// produtos 
		//incluir como lancada em nfe 

		private void coletarICMSProdutos(int k)
        {
			//ICMS
			if (notaXML.NFe.infNFe.det[k].imposto.Items[0] != null)
			{
				//EM ITEMS[0] DEFINE QUE É ICMS - OBS RESTANTE
				var icms = (TNFeInfNFeDetImpostoICMS)notaXML.NFe.infNFe.det[k].imposto.Items[0];


				//ICMS00
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS00"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).orig.ToString().Replace("Item","");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).CST.ToString().Replace("Item","");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).modBC.ToString().Replace("Item", "");
				    nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).vBC.Replace(".", ","));
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).vICMS.Replace(".", ","));
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).pFCP;
					if (((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).vFCP != null)
						nfeProd.VFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS00)icms.Item).vFCP.Replace(".", ","));
				}

				//ICMS10
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS10"))
				{
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBC != null)
						nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBC.Replace(".", ","));
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).orig.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMS.Replace(".", ","));
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.MotDesICMSST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).motDesICMSST.ToString().Replace("Item", "");
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pFCP;
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pFCPST;
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMSSTDeson != null)
						nfeProd.VICMSSTDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMSSTDeson.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vFCPST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vFCP != null)
						nfeProd.VFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vFCP.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCFCPST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCFCP != null)
						nfeProd.VBCFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).vBCFCP.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMS10)icms.Item).pICMSST;
				}

				//ICMS20
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS20"))
				{
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vBC != null)
						nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vBC.Replace(".", ","));
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).orig.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vICMS.Replace(".", ","));
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).modBC.ToString().Replace("Item", "");
					
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).pFCP;
					if (((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vFCP != null)
						nfeProd.VFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vFCP.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vBCFCP != null)
						nfeProd.VBCFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vBCFCP.Replace(".", ","));

					nfeProd.PRedBC = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).pRedBC;
					if (((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vICMSDeson != null)
						nfeProd.VICMSDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).vICMSDeson.Replace(".", ","));
					nfeProd.MotDesIcms = ((TNFeInfNFeDetImpostoICMSICMS20)icms.Item).motDesICMS.ToString().Replace("Item", "");
				}

				//ICMS30
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS30"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vFCPST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vICMSDeson != null)
						nfeProd.VICMSDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).vICMSDeson.Replace(".", ","));
					nfeProd.MotDesIcms = ((TNFeInfNFeDetImpostoICMSICMS30)icms.Item).motDesICMS.ToString().Replace("Item", "");
				}

				//ICMS40
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS40"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS40)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS40)icms.Item).CST.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS40)icms.Item).vICMSDeson != null)
						nfeProd.VICMSDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS40)icms.Item).vICMSDeson.Replace(".", ","));
					nfeProd.MotDesIcms = ((TNFeInfNFeDetImpostoICMSICMS40)icms.Item).motDesICMS.ToString().Replace("Item", "");
				}

				//ICMS51
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS51"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.ModBCSpecified = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).modBCSpecified;
					nfeProd.PRedBC = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).pRedBC;
					if(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item) != null)
						nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vBC.Replace(".", ","));
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMSOp != null)
						nfeProd.VICMSOp = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMSOp.Replace(".", ","));
					nfeProd.PDif = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).pDif;
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMSDif != null)
						nfeProd.VICMSDif = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMSDif.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vICMS.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vBCFCP != null)
						nfeProd.VBCFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vBCFCP.Replace(".", ","));
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).pFCP;
					nfeProd.PFCPDif = ((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).pFCPDif;
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vFCPDif != null)
						nfeProd.VFCPDif = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vFCPDif.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vFCPEfet != null)
						nfeProd.VFCPEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS51)icms.Item).vFCPEfet.Replace(".", ","));
				}

				//ICMS60
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS60"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).CST.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCSTRet != null)
						nfeProd.VBCSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSSubstituto != null)
						nfeProd.VICMSSubstituto = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSSubstituto.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSSTRet != null)
						nfeProd.VICMSSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCFCPSTRet != null)
						nfeProd.VBCFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCFCPSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vFCPSTRet != null)
						nfeProd.VFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vFCPSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCEfet != null)
						nfeProd.VBCEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vBCEfet.Replace(".", ",")); 
					if (((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSEfet != null)
						nfeProd.VICMSEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).vICMSEfet.Replace(".", ","));
					nfeProd.PST = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).pST;
					nfeProd.PFCPSTRet = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).pFCPSTRet;
					nfeProd.PRedBCEfet = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).pRedBCEfet;
					nfeProd.PICMSEfet = ((TNFeInfNFeDetImpostoICMSICMS60)icms.Item).pICMSEfet;
				}

				//ICMS70
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS70"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pRedBCST;
					nfeProd.PRedBC = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pRedBC;
					nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBC.Replace(".", ","));
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMS.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCFCP != null)
						nfeProd.VBCFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCFCP.Replace(".", ","));
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pFCP;
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vFCP != null)
						nfeProd.VFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vFCP.Replace(".", ","));
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vFCPST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSDeson != null)
						nfeProd.VICMSDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSDeson.Replace(".", ","));
					nfeProd.MotDesIcms = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).motDesICMS.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSSTDeson != null)
						nfeProd.VICMSSTDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).vICMSSTDeson.Replace(".", ","));
					nfeProd.MotDesICMSST = ((TNFeInfNFeDetImpostoICMSICMS70)icms.Item).motDesICMSST.ToString().Replace("Item", "");
				}

				//ICMS90
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMS90"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.PRedBC = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pRedBC;
					nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBC.Replace(".", ","));
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMS.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCFCP != null)
						nfeProd.VBCFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCFCP.Replace(".", ","));
					nfeProd.PFCP = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pFCP;
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vFCP != null)
						nfeProd.VFCP = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vFCP.Replace(".", ","));
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vFCPST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSDeson != null)
						nfeProd.VICMSDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSDeson.Replace(".", ","));
					nfeProd.MotDesIcms = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).motDesICMS.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSSTDeson != null)
						nfeProd.VICMSSTDeson = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).vICMSSTDeson.Replace(".", ","));
					nfeProd.MotDesICMSST = ((TNFeInfNFeDetImpostoICMSICMS90)icms.Item).motDesICMSST.ToString().Replace("Item", "");
				}

				//ICMSPart
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSPart"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).CST.ToString().Replace("Item", "");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.PRedBC = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pRedBC;
					nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vBC.Replace(".", ","));
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vICMS.Replace(".", ","));
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).vFCPST.Replace(".", ","));
					nfeProd.PBCOp = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).pBCOp;
					nfeProd.UFST = ((TNFeInfNFeDetImpostoICMSICMSPart)icms.Item).UFST.ToString();
				}

				//CSOSN101
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN101"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN101)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN101)icms.Item).CSOSN.ToString().Replace("Item", "");
					nfeProd.PCredSN = ((TNFeInfNFeDetImpostoICMSICMSSN101)icms.Item).pCredSN.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMSSN101)icms.Item).vCredICMSSN != null)
						nfeProd.VCredICMSSN = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN101)icms.Item).vCredICMSSN.Replace(".", ","));
				}
				//CSOSN102
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN102"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN102)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN102)icms.Item).CSOSN.ToString().Replace("Item", "");
				}
				//CSOSN201
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN201"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).CSOSN.ToString().Replace("Item", "");
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vFCPST.Replace(".", ","));
					nfeProd.PCredSN = ((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).pCredSN.ToString().Replace("Item", "");
					if(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vCredICMSSN != null)
						nfeProd.VCredICMSSN = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN201)icms.Item).vCredICMSSN.Replace(".", ","));
				}

				//CSOSN202
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN202"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).CSOSN.ToString().Replace("Item", "");
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN202)icms.Item).vFCPST.Replace(".", ","));
				}

				//CSOSN500
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN500"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).CSOSN.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCSTRet != null)
						nfeProd.VBCSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCSTRet.Replace(".", ","));
					nfeProd.PST = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).pST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSSubstituto != null)
						nfeProd.VICMSSubstituto = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSSubstituto.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSSTRet != null)
						nfeProd.VICMSSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCFCPSTRet != null)
						nfeProd.VBCFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCFCPSTRet.Replace(".", ","));
					nfeProd.PFCPSTRet = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).pFCPSTRet;
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vFCPSTRet != null)
						nfeProd.VFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vFCPSTRet.Replace(".", ","));
					nfeProd.PRedBCEfet = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).pRedBCEfet;
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCEfet != null)
						nfeProd.VBCEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vBCEfet.Replace(".", ","));
					nfeProd.PICMSEfet = ((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).pICMSEfet;
					if (((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSEfet != null)
						nfeProd.VICMSEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN500)icms.Item).vICMSEfet.Replace(".", ","));
				}

				//CSOSN900
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSSN900"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).CSOSN.ToString().Replace("Item", "");
					nfeProd.ModBC = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).modBC.ToString().Replace("Item", "");
					nfeProd.VBC = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vBC.Replace(".", ","));
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pRedBCST;
					nfeProd.PICMS = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pICMS;
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vICMS != null)
						nfeProd.VICMS = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vICMS.Replace(".", ","));
					nfeProd.ModBCST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).modBCST.ToString().Replace("Item", "");
					nfeProd.PMVAST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pMVAST;
					nfeProd.PRedBCST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pRedBCST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vBCST != null)
						nfeProd.VBCST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vBCST.Replace(".", ","));
					nfeProd.PICMSST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pICMSST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vICMSST != null)
						nfeProd.VICMSSt = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vICMSST.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vBCFCPST != null)
						nfeProd.VBCFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vBCFCPST.Replace(".", ","));
					nfeProd.PFCPST = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pFCPST;
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vFCPST != null)
						nfeProd.VFCPST = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vFCPST.Replace(".", ","));
					nfeProd.PCredSN = ((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).pCredSN.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vCredICMSSN != null)
						nfeProd.VCredICMSSN = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSSN900)icms.Item).vCredICMSSN.Replace(".", ","));
				}

				//ICMSICMSST
				if (icms.Item.ToString().Contains("TNFeInfNFeDetImpostoICMSICMSST"))
				{
					nfeProd.OrigemIcms = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).orig.ToString().Replace("Item", "");
					nfeProd.CstIcms = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).CST.ToString().Replace("Item", "");
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCSTRet != null)
						nfeProd.VBCSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCSTRet.Replace(".", ","));
					nfeProd.PST = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).pST;
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSubstituto != null)
						nfeProd.VICMSSubstituto = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSubstituto.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSTRet != null)
						nfeProd.VICMSSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSTRet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCFCPSTRet != null)
						nfeProd.VBCFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCFCPSTRet.Replace(".", ","));
					nfeProd.PFCPSTRet = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).pFCPSTRet;
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vFCPSTRet != null)
						nfeProd.VFCPSTRet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vFCPSTRet.Replace(".", ","));
					nfeProd.PRedBCEfet = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).pRedBCEfet;
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCEfet != null)
						nfeProd.VBCEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCEfet.Replace(".", ","));
					nfeProd.PICMSEfet = ((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).pICMSEfet;
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSEfet != null)
						nfeProd.VICMSEfet = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSEfet.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCSTDest != null)
						nfeProd.VBCSTDest = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vBCSTDest.Replace(".", ","));
					if (((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSTDest != null)
						nfeProd.VICMSSTDest = decimal.Parse(((TNFeInfNFeDetImpostoICMSICMSST)icms.Item).vICMSSTDest.Replace(".", ","));
				}
			}
		}

        private void gridProdutos_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
			//if (gridProdutos.SelectedIndex >= 0)
			//{
			//	nfeProd = new NfeProduto();
			//	nfeProd = (NfeProduto)gridProdutos.SelectedItem;
			//	//decimal totalUnitario = calcularCustoProduto();
			//}
		}

        private void gridProdutos_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
			if (gridProdutos.SelectedIndex >= 0)
			{
				nfeProd = new NfeProduto();
				nfeProd = (NfeProduto)gridProdutos.SelectedItem;

				decimal totalUnitario = calcularCustoProduto();
			}
		}

		private decimal calcularCustoProduto()
        {
			decimal ipiUnitario = nfeProd.ValorIpi / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
			decimal stUnitario = nfeProd.VICMSSt / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());
			decimal valorUnitItem = nfeProd.VProd / decimal.Parse(nfeProd.QuantidadeEntrada.ToString());

			double quantidadeProdutoNota = 0;
			var records = gridProdutos.View.Records;
			foreach (var record in records)
			{
				//var dataRowView = record.Data as DataRowView;
				if (record != null)
				{
					nfeProd = new NfeProduto();
					nfeProd = (NfeProduto)record.Data;

					quantidadeProdutoNota = quantidadeProdutoNota + nfeProd.QuantidadeEntrada;
				}	
			}
			decimal custoFreteItem = nfe.VFrete / decimal.Parse(quantidadeProdutoNota.ToString());
			decimal totalUnitario = valorUnitItem + ipiUnitario + stUnitario + custoFreteItem;

			return totalUnitario;
		}

		public static void ShowToolTip(out ToolTip toolTip, Control control, string title, string message)

		{

			toolTip = new ToolTip();

			toolTip.IsBalloon = false;

			//toolTip.ToolTipIcon = ToolTipIcon.Info;

			toolTip.ToolTipTitle = title;

			toolTip.Show(string.Empty, control, 0);

			toolTip.Show(message, control, control.Width / 2, control.Height, 1500);

		}

		private void btnCopiarChave_MouseHover(object sender, EventArgs e)
        {
			ShowToolTip(out toolTip1, btnCopiarChave, "Chave NFe", "Copiar chave para área de transferência");
		}

        private void btnCopiarChave_Click(object sender, EventArgs e)
		{
			toolTip1.RemoveAll();
			Clipboard.SetText(txtChave.Texts.Trim());
			ShowToolTip(out toolTip1, btnCopiarChave, "Chave NFe", "Chave copiada com Sucesso");
		}


        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
			if (gridProdutos.SelectedIndex >= 0)
			{
				produtoAjusteNovoCadastro();
				abrirTelaParaCadastroProduto();
            }
            else
            {
				GenericaDesktop.ShowAlerta("Clique em um produto da nota que deseja cadastrar!");
            }
		}
		private void validarCFOPEditado()
        {
			//this.gridProdutos.ValidationMode = GridValidationMode.InEdit;
			this.gridProdutos.Columns["CfopEntrada"].ValidationMode = GridValidationMode.InEdit;
		}
		private void abrirTelaParaCadastroProduto()
        {
			Object produtoObjeto = new Produto();
			Form formBackground = new Form();
			try
			{
				using (FrmProdutoCadastro uu = new FrmProdutoCadastro(prod, true))
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
					switch (uu.showModalNovo(ref produtoObjeto, true))
					{
						case DialogResult.Ignore:
							//uu.Dispose();
							//formBackground.Dispose();
							break;
						case DialogResult.OK:
							prod = (Produto)produtoObjeto;
							gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["CodigoInterno"].MappingName, prod.Id.ToString());
							gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["DescricaoInterna"].MappingName, prod.Descricao);
							gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["ProdutoAssociado"].MappingName, "OK");
							gridProdutos.Refresh();
                            verificarProdutosCadastrados();
                            //foreach (var selectedItem in gridProdutos.SelectedItems)
                            //{
                            //    var index = gridProdutos.TableControl.ResolveToRowIndex(selectedItem);
                            //    SetCellBackgroundColor(new RowColumnIndex(index, 0), Color.FromArgb(153, 255, 153));
                            //}
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

		private async void verificarProdutosCadastrados()
        {
			await Task.Delay(500);
			IList<Produto> listaProds = new List<Produto>();
			ProdutoController produtoController = new ProdutoController();
			prod = new Produto();
			bool selecionadoPeloSistema = false;
			for (int i =0; i < gridProdutos.RowCount; i++)
			{
				var index = i;
				var produtoSelecionado = (NfeProduto)gridProdutos.GetRecordAtRowIndex(i);
			
				if (produtoSelecionado != null)
				{
					//Se nao tem produto ainda selecionado pelo usuario o sistema verifica se existe ja cadastro no sistema...
					if (String.IsNullOrEmpty(produtoSelecionado.ProdutoAssociado))
					{
						if (!String.IsNullOrEmpty(produtoSelecionado.CEANTrib))
						{
							listaProds = produtoController.selecionarProdutoPorCodigoBarras(produtoSelecionado.CEANTrib);
							if (listaProds.Count > 0)
							{
								foreach (Produto produto in listaProds)
								{
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["CodigoInterno"].MappingName, produto.Id.ToString());
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["DescricaoInterna"].MappingName, produto.Descricao);
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["ProdutoAssociado"].MappingName, "OK");
									produtoSelecionado.Produto = produto;
								}
								selecionadoPeloSistema = true;
							}
						}
						//Se o produto continua sem associação, segue a busca
						if(String.IsNullOrEmpty(produtoSelecionado.ProdutoAssociado))
						{
							//Faz uma busca na tabela nfeProduto para verificar se em algum momento este fornecedor ja enviou essa mesma referencia
							NfeProdutoController nfeProdutoController = new NfeProdutoController();
							IList<NfeProduto> listaProdsFornecedor = nfeProdutoController.selecionarProdutoPorCNPJeReferencia(nfe.CnpjEmitente, produtoSelecionado.CProd);
							if (listaProdsFornecedor.Count > 0)
							{
								foreach (NfeProduto nfeProduto in listaProdsFornecedor)
								{
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["CodigoInterno"].MappingName, nfeProduto.Produto.Id.ToString());
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["DescricaoInterna"].MappingName, nfeProduto.Produto.Descricao);
									gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["ProdutoAssociado"].MappingName, "OK");
									produtoSelecionado.Produto = nfeProduto.Produto;
								}
								selecionadoPeloSistema = true;
							}

						}
					}
					if (!String.IsNullOrEmpty(produtoSelecionado.ProdutoAssociado))
					{
						//SetCellTextColor(new RowColumnIndex(index, 0), Color.FromArgb(0, 0, 255));
						if(selecionadoPeloSistema == true)
							SetCellBackgroundColor(new RowColumnIndex(index, 0), Color.FromArgb(240, 255, 240));
						else
							SetCellBackgroundColor(new RowColumnIndex(index, 0), Color.FromArgb(154, 205, 50));
					}
				}
			}
			gridProdutos.Refresh();
		}
		private void produtoAjusteNovoCadastro()
		{
			if (gridProdutos.SelectedIndex >= 0)
			{
				nfeProd = new NfeProduto();
				nfeProd = (NfeProduto)gridProdutos.SelectedItem;
			}
			prod = new Produto();
			prod.Cest = nfeProd.Cest;
			prod.Ncm = nfeProd.Ncm;
			if (nfeProd.CfopEntrada.Substring(0, 2).Contains("11"))
				prod.CfopVenda = "5102";
			else if (nfeProd.CfopEntrada.Substring(0, 2).Contains("14"))
				prod.CfopVenda = "5405";
			else if (nfeProd.CfopEntrada.Substring(0, 2).Contains("21"))
				prod.CfopVenda = "5102";
			else if (nfeProd.CfopEntrada.Substring(0, 2).Contains("24"))
				prod.CfopVenda = "5405";

			prod.CodAnp = nfeProd.CodAnp;
			prod.ControlaEstoque = true;
			prod.Descricao = nfeProd.XProd;
			if (!nfeProd.CEANTrib.Contains("GTIN"))
				prod.Ean = nfeProd.CEANTrib;
			else
				prod.Ean = "";
	
			prod.EmpresaFilial = Sessao.empresaFilialLogada;
			
			//GRUPO FISCAL
			GrupoFiscal grupoFiscal = new GrupoFiscal();
			GrupoFiscalController grupoFiscalController = new GrupoFiscalController();
			if (prod.CfopVenda != null)
			{
				if (prod.CfopVenda.Substring(0, 2).Contains("51"))
				{
					grupoFiscal.Id = 1;
					prod.CstIcms = "102";
					if (Sessao.empresaFilialLogada.Endereco != null)
					{
						if (Sessao.empresaFilialLogada.Endereco.Cidade != null)
						{
							if (Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf == "MG")
								prod.PercentualIcms = "18";
						}
					}
				}
				else if (prod.CfopVenda.Substring(0, 2).Contains("54"))
				{
					grupoFiscal.Id = 2;
					prod.CstIcms = "500";
					//Colocar origem do fornecedor
				}
			}
			prod.OrigemIcms = "0";

			if (grupoFiscal.Id > 0)
			{
				grupoFiscal = (GrupoFiscal)grupoFiscalController.selecionar(grupoFiscal);
					prod.GrupoFiscal = grupoFiscal;
			}
			else grupoFiscal = null;

			prod.Referencia = nfeProd.CProd;

			//Unidade de Medida
			UnidadeMedida unidadeMedida = new UnidadeMedida();
			UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
			unidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaPorSigla(nfeProd.UCom);
			if (unidadeMedida != null)
				prod.UnidadeMedida = unidadeMedida;
			else 
				prod.UnidadeMedida = null;

			prod.ValorCusto = calcularCustoProduto();
			prod.ValorVenda = ((prod.ValorCusto / 100 * 80) + prod.ValorCusto);
		}

        private void FrmLancarNotaFiscalCompra_Paint(object sender, PaintEventArgs e)
        {
			if(verificado == false)
            {
				verificarProdutosCadastrados();
			}
        }

        private void btnSelecionarProduto_Click(object sender, EventArgs e)
        {
			if (gridProdutos.SelectedIndex >= 0)
			{
				Object produtoObjeto = new Produto();
				Form formBackground = new Form();
				try
				{
					using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", ""))
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
						switch (uu.showModal("Produto", "", ref produtoObjeto))
						{
							case DialogResult.Ignore:
								uu.Dispose();
								formBackground.Dispose();
								break;
							case DialogResult.OK:
								prod = (Produto)produtoObjeto;
								gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["CodigoInterno"].MappingName, prod.Id.ToString());
								gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["DescricaoInterna"].MappingName, prod.Descricao);
								gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(gridProdutos.SelectedIndex + 1), gridProdutos.Columns["ProdutoAssociado"].MappingName, "OK");
								gridProdutos.Refresh();
								if(string.IsNullOrEmpty(prod.Ncm))
								{
                                    NfeProduto nnfeProd = (NfeProduto)gridProdutos.SelectedItem;
									prod.Ncm = nnfeProd.Ncm;
									Controller.getInstance().salvar(prod);
                                }
								verificarProdutosCadastrados();
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
				GenericaDesktop.ShowAlerta("Clique no produto da nota que deseja selecionar");
		}

        private void gridProdutos_CurrentCellValidating(object sender, Syncfusion.WinForms.DataGrid.Events.CurrentCellValidatingEventArgs e)
        {
			if (e.Column.MappingName == "QuantidadeEntrada")
            {
				try 
				{
					if (double.Parse(e.NewValue.ToString()) > 0)
					{
						nfeProd = (NfeProduto)gridProdutos.SelectedItem;
						nfeProd.QuantidadeEntrada = double.Parse(e.NewValue.ToString());
					}
					else
					{
						e.IsValid = false;
						e.ErrorMessage = "Quantidade Inválida";
					}
				}
				catch
                {
					e.IsValid = false;
					e.ErrorMessage = "Quantidade Inválida";
				}

			}
			if (e.Column.MappingName == "CfopEntrada")
            {
				Cfop cfop = new Cfop();
				CfopController cfopController = new CfopController();
				IList<Cfop> listaCFOP = cfopController.selecionarCfopPorCfop(e.NewValue.ToString());
				if(listaCFOP.Count <= 0)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "CFOP Inexistente na tabela de CFOP do Sistema";
					desabilitarTodosBotoes();

				}
                else if (listaCFOP.Count > 0)
                {
					foreach(Cfop cfopSel in listaCFOP)
                    {
						if (int.Parse(cfopSel.CfopNumero.Substring(0, 1)) > 2 || int.Parse(cfopSel.CfopNumero.Substring(0, 1)) <= 0)
						{
							e.IsValid = false;
							e.ErrorMessage = "CFOP de Entrada deve sempre iniciar com 1 ou 2, em caso de dúvida consulte o o cfop correto com sua contabilidade";
							desabilitarTodosBotoes();
						}
						else
							habilitarTodosBotoes();
                    }
				}

            }
			//if (e.NewValue.ToString().Equals("10004"))
			//{
			//	e.IsValid = false;
			//	e.ErrorMessage = "OrderID 10004 cannot be passed";
			//}
		}

		private void desabilitarTodosBotoes()
		{
			btnCadastrarProduto.Enabled = false;
			btnConfirmarLancamento.Enabled = false;
			btnEditarProduto.Enabled = false;
			btnSelecionarProduto.Enabled = false;
			btnCopiarChave.Enabled = false;
		}
		private void habilitarTodosBotoes()
		{
			btnCadastrarProduto.Enabled = true;
			btnConfirmarLancamento.Enabled = true;
			btnEditarProduto.Enabled = true;
			btnSelecionarProduto.Enabled = true;
			btnCopiarChave.Enabled = true;
		}

        private void btnConfirmarLancamento_Click(object sender, EventArgs e)
        {
			if (DateTime.Parse(txtDataLancamento.Value.ToString()).Month != DateTime.Now.Month && DateTime.Parse(txtDataLancamento.Value.ToString()) < DateTime.Now)
			{
				GenericaDesktop.ShowAlerta("Atenção, você deixou a data de lançamento da nota na " +
					"data de " + DateTime.Parse(txtDataLancamento.Value.ToString()).ToShortDateString() + " caso " +
					"já tenha enviado os arquivos desse período para a contabilidade verifique se deve alterar essa data de lançamento!");
				if(GenericaDesktop.ShowConfirmacao("Deseja continuar assim mesmo?"))
                {
					if (validarLancamentoNota())
					{
						GenericaDesktop.ShowInfo("Nota lançada com sucesso");
						this.Close();
					}
				}
			}
			else
			{
				if (validarLancamentoNota())
				{
					GenericaDesktop.ShowInfo("Nota lançada com sucesso");
					this.Close();
				}
			}
		}

		private bool validarLancamentoNota()
		{
			bool pagamentoConferido = false;
			bool valoresConferidos = true;
			bool produtosValidos = true;
			PessoaController pessoaController = new PessoaController();
			Pessoa pessoa = new Pessoa();
			pessoa = pessoaController.selecionarPessoaPorCPFCNPJ(notaXML.NFe.infNFe.emit.Item);
			if(pessoa is null)
            {
				cadastrarFornecedor();
			}

			//VERIFICAR SE FOI CONFIRMADO OS BOLETOS
			if(listaContaPagar.Count > 0)
            {
				decimal somaContas = 0;
				var recordsPagamento = gridPagamento.View.Records;
				foreach (var record in recordsPagamento)
				{
					//var dataRowView = record.Data as DataRowView;
					if (record != null)
					{
						contaPagar = new ContaPagar();
						contaPagar = (ContaPagar)record.Data;
						if (contaPagar != null)
						{
							somaContas = somaContas + contaPagar.VDup;
						}
					}
				}
				if (somaContas == nfe.VNf)
					valoresConferidos = true;
				else
					valoresConferidos = false;

				if(valoresConferidos == true)
                {
					if (chkConfirmacaoPagamento.Checked == true)
                    {
						pagamentoConferido = true;
					}
					else
					{
						tabControlAdv1.SelectedTab = tab2;
						chkConfirmacaoPagamento.ForeColor = Color.Red;
						GenericaDesktop.ShowAlerta("Na aba pagamento você deve marcar o quadrinho (checkbox) confirmando que conferiu as parcelas da nota fiscal");
						gridPagamento.Style.BorderColor = Color.Black;
						return false;
					}
                }
                else
                {
					tabControlAdv1.SelectedTab = tab2;
					gridPagamento.Style.BorderColor = Color.Red;
					GenericaDesktop.ShowAlerta("Atenção: A soma das parcelas não bate com o valor total da nota fiscal, o total da nota é " + nfe.VNf.ToString("C2", CultureInfo.CurrentCulture) + " e o total das parcelas informado na aba pagamento está " + somaContas.ToString("C2", CultureInfo.CurrentCulture));
					gridPagamento.Style.BorderColor = Color.LightGray;
					return false;
                }
			}

			//VERIFICAR SE TODOS PRODUTOS DO GRID FORAM CADASTRADOS
			var records = gridProdutos.View.Records;
			foreach (var record in records)
			{
				//var dataRowView = record.Data as DataRowView;
				if (record != null)
				{
					nfeProd = new NfeProduto();
					nfeProd = (NfeProduto)record.Data;
					if (nfeProd != null)
					{
						if (String.IsNullOrWhiteSpace(nfeProd.CodigoInterno) && !String.IsNullOrEmpty(nfeProd.XProd) && String.IsNullOrEmpty(nfeProd.CfopEntrada))
						{
							produtosValidos = false;
						}
					}
				}
			}
			if (produtosValidos == true)
			{
				nfe.Lancada = true;
				nfe.DataLancamento = DateTime.Parse(txtDataLancamento.Value.ToString());
				Controller.getInstance().salvar(nfe);

				//grava os produtos
				var records1 = gridProdutos.View.Records;
				int cont = 0;
				decimal calcFreteTotal = 0;
				NfeProdutoDAO nfeprodDAO = new NfeProdutoDAO();
				nfeprodDAO.excluirProdutosNfeParaAtualizar(nfe.Id.ToString());
				foreach (var record in records1)
				{
					cont++;
					//var dataRowView = record.Data as DataRowView;
					if (record != null)
					{
						nfeProd = new NfeProduto();
						nfeProd = (NfeProduto)record.Data;

						if (nfeProd != null && !String.IsNullOrEmpty(nfeProd.XProd))
						{
							nfeProd.Nfe = nfe;
							nfeProd.DataEntrada = DateTime.Parse(txtDataLancamento.Value.ToString());
							nfeProd.Produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(nfeProd.CodigoInterno), Sessao.empresaFilialLogada);
							nfeProd.UComConvertida = nfeProd.Produto.UnidadeMedida.Sigla;

							//Ratear Frete
							if (nfe.VFrete > 0)
							{
								//Fórmula: (Valor do produto / Valor total dos produtos) x Valor do frete = Valor do rateio
								decimal valorFretePorItem = (nfeProd.VProd / nfeProd.Nfe.VProd) * nfe.VFrete;
								calcFreteTotal = calcFreteTotal + valorFretePorItem;
								nfeProd.VFrete = valorFretePorItem;
								if (cont == records1.Count)
								{
									if (calcFreteTotal > nfeProd.Nfe.VFrete)
									{
										nfeProd.VFrete = (calcFreteTotal - nfeProd.Nfe.VFrete);
									}
									else if (calcFreteTotal < nfeProd.Nfe.VFrete)
									{
										nfeProd.VFrete = nfeProd.Nfe.VFrete - calcFreteTotal;
									}
								}
							}
							if (nfeProd.VBC < nfeProd.VProd)
								nfeProd.OutrosIcms = nfeProd.VProd - nfeProd.VBC;
							else
								nfeProd.OutrosIcms = nfeProd.VBC;
							//nfeProd.OutrosIcms = nfeProd.VProd - nfeProd.VDesc + (nfeProd.VICMSSt + nfeProd.ValorIpi + nfeProd.VFrete); 
							//ESTOQUE PRODUTO
							Produto produtoSelecionado = new Produto();
							produtoSelecionado = nfeProd.Produto;
							produtoSelecionado.Estoque = produtoSelecionado.Estoque + nfeProd.QuantidadeEntrada;
							produtoSelecionado.EstoqueAuxiliar = produtoSelecionado.EstoqueAuxiliar + nfeProd.QuantidadeEntrada;
							Controller.getInstance().salvar(produtoSelecionado);

							//MOVIMENTO ESTOQUE CONCILIADO
							Estoque estoque = new Estoque();
							estoque.Produto = produtoSelecionado;
							estoque.EmpresaFilial = Sessao.empresaFilialLogada;
							estoque.Entrada = true;
							estoque.Saida = false;
							estoque.DataEntradaSaida = DateTime.Parse(txtDataLancamento.Value.ToString());
							estoque.Quantidade = nfeProd.QuantidadeEntrada;
							estoque.Origem = "NOTA FISCAL " + nfe.NNf + " CNPJ: " + nfe.CnpjEmitente;
							estoque.Descricao = "NOTA DE ENTRADA: " + nfe.NNf;
							estoque.Conciliado = true;
							if (nfe.Fornecedor != null)
								estoque.Pessoa = nfe.Fornecedor;
                            Controller.getInstance().salvar(estoque);

                            //MOVIMENTO ESTOQUE NAO CONCILIADO
                            estoque = new Estoque();
                            estoque.Produto = produtoSelecionado;
                            estoque.EmpresaFilial = Sessao.empresaFilialLogada;
                            estoque.Entrada = true;
                            estoque.Saida = false;
                            estoque.DataEntradaSaida = DateTime.Parse(txtDataLancamento.Value.ToString());
                            estoque.Quantidade = nfeProd.QuantidadeEntrada;
                            estoque.Origem = "NOTA FISCAL " + nfe.NNf + " CNPJ: " + nfe.CnpjEmitente;
                            estoque.Descricao = "NOTA DE ENTRADA: " + nfe.NNf;
                            estoque.Conciliado = false;
                            if (nfe.Fornecedor != null)
                                estoque.Pessoa = nfe.Fornecedor;
                            Controller.getInstance().salvar(estoque);

                            Controller.getInstance().salvar(nfeProd);
						}
					}
				}
				//gravas as contas pagar
				if (radioDesconsiderar.Checked == false)
				{
					var records2 = gridPagamento.View.Records;
					if (records2.Count > 0)
					{
						int a = 0;
						foreach (var record in records2)
						{
							//var dataRowView = record.Data as DataRowView;
							if (record != null)
							{
								a++;
                                contaPagar = new ContaPagar();
								contaPagar = (ContaPagar)record.Data;
								if (contaPagar != null)
								{
									contaPagar.Nfe = nfe;
									contaPagar.DataOrigem = DateTime.Parse(txtDataEmissao.Value.ToString());
									contaPagar.Descricao = "COMPRA PRODUTOS - NF: " + nfe.NNf + " PARC. " + a + " DE " + records2.Count;

									contaPagar.EmpresaFilial = Sessao.empresaFilialLogada;
									FormaPagamento formaPagamento = new FormaPagamento();
									formaPagamento.Id = int.Parse(txtCodFormaPagamento.Texts);
									formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
									contaPagar.FormaPagamento = formaPagamento;

									if (!String.IsNullOrEmpty(txtCodPlanoContas.Texts))
									{
										PlanoConta planoConta = new PlanoConta();
										planoConta.Id = int.Parse(txtCodPlanoContas.Texts);
										planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
										contaPagar.PlanoConta = planoConta;
									}

									contaPagar.Pessoa = pessoa;
									//if (radioNaoPagas.Checked == true)
									contaPagar.Pago = false;
									//else
									//{
									//	contaPagar.Pago = true;
									//	contaPagar.DescricaoPagamento = "PAG. COMPRA NF " + nfe.NNf + " " + nfe.Fornecedor.RazaoSocial;
									//	contaPagar.DescontoBaixa = 0;
									//	contaPagar.AcrescimoBaixa = 0;
									//	contaPagar.DataPagamento = DateTime.Now;
									//	contaPagar.CaixaPagamento = Sessao.usuarioLogado.Id.ToString() + " - " + Sessao.usuarioLogado.Login;
									//	contaPagar.ValorPago = contaPagar.ValorTotal;

									//	Caixa caixa = new Caixa();
									//	caixa.Conciliado = true;
									//	caixa.Concluido = true;
									//	caixa.ContaBancaria = 
									//}
									if (radioPagas.Checked == true)
										GenericaDesktop.ShowInfo("Acesse o menu contas a pagar para registrar o pagamento da(s) parcela(s)");
									Controller.getInstance().salvar(contaPagar);
								}
							}
						}
					}
					return true;
				}
				return true;
			}
			else
			{
				GenericaDesktop.ShowErro("Existem produtos sem cadastrar ou selecionar, favor conferir!");
				return false;
			}
		}
		private void cadastrarFornecedor()
		{
			PessoaController pessoaController = new PessoaController();
			Pessoa pessoa = new Pessoa();
			CidadeController cidadeController = new CidadeController();
			pessoa.Fornecedor = true;
			pessoa.Id = 0;
			pessoa.Cliente = false;
			pessoa.Cnpj = notaXML.NFe.infNFe.emit.Item;
			pessoa.ContatoTrabalho = "";
			Endereco endereco = new Endereco();
			endereco.Id = 0;
			if (notaXML.NFe.infNFe.emit.enderEmit.xBairro != null)
				endereco.Bairro = notaXML.NFe.infNFe.emit.enderEmit.xBairro.ToUpper();
			endereco.Cep = notaXML.NFe.infNFe.emit.enderEmit.CEP;
			endereco.Cidade = cidadeController.selecionarCidadePorDescricaoEIBGE(notaXML.NFe.infNFe.emit.enderEmit.xMun, notaXML.NFe.infNFe.emit.enderEmit.cMun);
			if (endereco.Cidade == null)
				endereco.Cidade = null;
			endereco.EmpresaFilial = Sessao.empresaFilialLogada;
			if (notaXML.NFe.infNFe.emit.enderEmit.xLgr != null)
				endereco.Logradouro = notaXML.NFe.infNFe.emit.enderEmit.xLgr.ToUpper();
			endereco.Numero = notaXML.NFe.infNFe.emit.enderEmit.nro;
			endereco.Pessoa = null;
			if(notaXML.NFe.infNFe.emit.enderEmit.xCpl != null)
				endereco.Complemento = notaXML.NFe.infNFe.emit.enderEmit.xCpl.ToUpper();
			Controller.getInstance().salvar(endereco);

			pessoa.EnderecoPrincipal = endereco;
			pessoa.FuncaoTrabalho = "";
			pessoa.Funcionario = false;
			pessoa.InscricaoEstadual = notaXML.NFe.infNFe.emit.IE;
			pessoa.LimiteCredito = 0;
			pessoa.LocalTrabalho = "";
			pessoa.Mae = "";
			pessoa.Email = "";
			if (notaXML.NFe.infNFe.emit.xFant != null)
				pessoa.NomeFantasia = notaXML.NFe.infNFe.emit.xFant.ToUpper();
			else if (notaXML.NFe.infNFe.emit.xNome != null)
				pessoa.NomeFantasia = notaXML.NFe.infNFe.emit.xNome.ToUpper();
			else
				pessoa.NomeFantasia = "";

            pessoa.Observacoes = "";
			pessoa.Pai = "";
			pessoa.PessoaTelefone = null;
			if (notaXML.NFe.infNFe.emit.xNome != null)
				pessoa.RazaoSocial = notaXML.NFe.infNFe.emit.xNome.ToUpper();
			pessoa.Rg = "";
			pessoa.SalarioTrabalho = "";
			pessoa.Sexo = "";
			pessoa.Vendedor = false;
			pessoa.Tecnico = false;
			pessoa.TelefoneTrabalho = "";
			pessoa.TempoTrabalho = "";
			pessoa.TipoPessoa = "";
			Controller.getInstance().salvar(pessoa);
			endereco.Pessoa = pessoa;
			Controller.getInstance().salvar(endereco);
		}

        private void sfDataPager2_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
			sfDataPager2.LoadDynamicData(e.StartRowIndex, listaContaPagar.Skip(e.StartRowIndex).Take(e.PageSize));
		}

        private void gridPagamento_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
			if (e.RowIndex % 2 == 0)
				e.Style.BackColor = Color.WhiteSmoke;
			else
				e.Style.BackColor = Color.White;
		}

        private void btnPesquisaFormaPagamento_Click(object sender, EventArgs e)
        {
			Object formaPagamentoObjeto = new FormaPagamento();
			Form formBackground = new Form();
			try
			{
				using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("FormaPagamento", ""))
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
					switch (uu.showModal("FormaPagamento", "", ref formaPagamentoObjeto))
					{
						case DialogResult.Ignore:
							uu.Dispose();
							break;
						case DialogResult.OK:
							txtFormaPagamento.Texts = ((FormaPagamento)formaPagamentoObjeto).Descricao;
							txtCodFormaPagamento.Texts = ((FormaPagamento)formaPagamentoObjeto).Id.ToString();
							btnPesquisarPlanoContas.Focus();
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

        private void radioNaoPagas_CheckChanged(object sender, EventArgs e)
        {
            try
            {
				if(radioNaoPagas.Checked == true)
                {
					FormaPagamento formaPagamento = new FormaPagamento();
					radioNaoPagas.Checked = true;
					formaPagamento.Id = 5;
					formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
					txtFormaPagamento.Texts = formaPagamento.Descricao;
					txtCodFormaPagamento.Texts = formaPagamento.Id.ToString();
				}
				else if(radioPagas.Checked == true)
				{
					FormaPagamento formaPagamento = new FormaPagamento();
					radioPagas.Checked = true;
					formaPagamento.Id = 3;
					formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
					txtFormaPagamento.Texts = formaPagamento.Descricao;
					txtCodFormaPagamento.Texts = formaPagamento.Id.ToString();
				}
				else
				{
                    txtFormaPagamento.Texts = "";
                    txtCodFormaPagamento.Texts = "";
					txtCodPlanoContas.Texts = "";
					txtPlanoContas.Texts = "";
                }
            }
            catch
            {

            }
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
			if(gridProdutos.SelectedIndex >= 0)
            {
				NfeProdutoController nfeProdutoController = new NfeProdutoController();
				nfeProd = new NfeProduto();
				nfeProd = (NfeProduto)gridProdutos.SelectedItem;
				if (!String.IsNullOrEmpty(nfeProd.ProdutoAssociado))
				{
					if (nfeProd.Produto == null)
						nfeProd.Produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(nfeProd.CodigoInterno), Sessao.empresaFilialLogada);

					FrmProdutoCadastro frmProdutoCadastro = new FrmProdutoCadastro(nfeProd.Produto, false);
					frmProdutoCadastro.Show();
				}
				else
					GenericaDesktop.ShowAlerta("O produto cadastrado no sistema já deve ter sido selecionado para conseguir " +
						"editar o mesmo, aparentemente você está visualizando apenas os produtos da nota fiscal, sem substituir " +
						"por um produto do seu próprio cadastro!");
			}
        }

        private void btnPesquisarPlanoContas_Click(object sender, EventArgs e)
        {
			Object objeto = new PlanoConta();
			Form formBackground = new Form();
			try
			{
				using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
				{
					txtCodPlanoContas.Texts = "";
					txtPlanoContas.Texts = "";
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
					switch (uu.showModal("PlanoConta", "", ref objeto))
					{
						case DialogResult.Ignore:
							uu.Dispose();
							break;
						case DialogResult.OK:
							txtPlanoContas.Texts = ((PlanoConta)objeto).Descricao;
							txtCodPlanoContas.Texts = ((PlanoConta)objeto).Id.ToString();
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
    }	
}

