using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using Lunar.Utils.Sintegra;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using LunarBase.Utilidades.ZAPZAP;
using Newtonsoft.Json;
using Syncfusion.Data;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lunar.Utils.OrganizacaoNF.RetConsultaNfe;
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static Lunar.Utils.OrganizacaoNF.RetInutilizacao;
using static LunarBase.Utilidades.ClasseRetornoJson.CancelamentoNFCe;
using static LunarBase.Utilidades.ManifestoDownload;
using File = System.IO.File;
using Form = System.Windows.Forms.Form;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmControleNotas : Form
    {
        GeradorSintegra geradorSintegra = new GeradorSintegra();
        private IList<Nfe> lista;
        LunarBase.Classes.Nfe nfe = new LunarBase.Classes.Nfe();
        GenericaDesktop generica = new GenericaDesktop();
        NfeController nfeController = new NfeController();
        bool fazerPrimeiraBuscaNotas = true;
        String xmlStrEnvio = "";
        VendaController vendaController = new VendaController();
        Ftp ftp = new Ftp();
        DateTime data1 = DateTime.Parse("2022-11-01 00:00:00");
        DateTime data2 = DateTime.Parse("2022-11-30");

        public const string clientId = "1d0b3a89-5c98-49d7-a9da-de69b0516a20";
        public const string MsaReturnUrl = "urn:ietf:wg:oauth:2.0:oob";
        Zapi zap = new Zapi();
        SecureString securePwd = new SecureString();
        public FrmControleNotas()
        {
            InitializeComponent();

            obterVencimentoCertificado();

            if(!String.IsNullOrEmpty(Sessao.parametroSistema.IdInstanciaWhats) && !String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
            {
                string ret = zap.zapi_VerificarConexaoInstancia_ParaGerarQR(Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                if (ret != null)
                {
                    if (ret.Equals("True"))
                    {
                        btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.whatsapp_logo_icone;
                        btnEnviarWhats.Enabled = true;
                    }
                    else
                    {
                        btnEnviarWhats.Enabled = false;
                        btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.WhatsappCinza2_1;
                    }
                }
                else
                {
                    btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.WhatsappCinza2_1;
                    btnEnviarWhats.Enabled = false;
                }
            }
            else
            {
                btnEnviarWhats.BackgroundImage = Lunar.Properties.Resources.WhatsappCinza2_1;
                btnEnviarWhats.Enabled = false;
            }
            

            //SignInAsync(app);

            //Task<string> retorno = ftp.enviarArquivoParaNuvem(@"C:\XML\Tentativa\NFe\134.xml", "134.xml", @"XML\Teste\NFe\134.xml");
            //ftp.azure_CriarDiretorio(Sessao.empresaFilialLogada.Cnpj);
            //DropboxComandos drop = new DropboxComandos();

            //var task = Task.Run((Func<Task>)DropboxComandos.uploadArquivo);
            //task.Wait();

            //DropboxComandos.listarPastas();
            //var task = Task.Run((Func<Task>)DropboxComandos.listarPastas);
            //task.Wait();
            //Console.ReadKey();


        }

        public bool obterVencimentoCertificado()
        {
            try
            {
                string ret = generica.NS_ConsultaValidadeCertificado(Sessao.empresaFilialLogada.Cnpj);
                if (!String.IsNullOrEmpty(ret))
                {
                    DateTime dataVencimento = DateTime.Parse(ret);
                    TimeSpan date = Convert.ToDateTime(dataVencimento) - Convert.ToDateTime(DateTime.Now);
                    int totalDias = date.Days;

                    if (dataVencimento < DateTime.Now)
                        GenericaDesktop.ShowAlerta("Certificado digital vencido em " + dataVencimento.ToShortDateString());
                    else if (totalDias <= 30)
                        GenericaDesktop.ShowAlerta("Atenção: Certificado digital próximo do vencimento " + dataVencimento.ToUniversalTime());
                }
                return true;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buscaNotas()
        {
            //Insere Notas no Grid
            DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime ultimoDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ultimoDiaDoMes = ultimoDiaDoMes.AddMonths(1).AddDays(-1);

            txtDataInicial.Text = primeiroDiaDoMes.ToShortDateString();
            txtDataFinal.Text = ultimoDiaDoMes.ToShortDateString();

            selecionarNotasBancoDados(primeiroDiaDoMes.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), ultimoDiaDoMes.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
            calculaTotalNotas();

            if (GenericaDesktop.possuiConexaoInternet())
                reenviarNotasEmContigencia(primeiroDiaDoMes.AddMonths(-2).ToString("yyyy'-'MM'-'dd' '00':'00':'00"), ultimoDiaDoMes.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));

        }

        private void selecionarNotasBancoDados(string dataInicial, string dataFinal)
        {
            lista = nfeController.selecionarNotasEmitidasPorPeriodo(dataInicial, dataFinal);
            if (lista.Count > 0)
            {
                sfDataPager1.DataSource = lista;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;
                txtPesquisa.Focus();

                grid.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                this.grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.AutoSizeController.Refresh();
            }
        }

        private async void reenviarNotasEmContigencia(string dataInicial, string dataFinal)
        {
            if (GenericaDesktop.possuiConexaoInternet())
            {
                lblReenviando.Visible = true;
                lblReenviando.Text = "Reenviando Notas em Contigência";
                IList<Nfe> listaNotasContigencia = nfeController.selecionarNotasSaidaEmContigenciaPorPeriodo(dataInicial, dataFinal);
                //GenericaDesktop.ShowInfo("Sistema reenviando notas em contigência! " + listaNotasContigencia.Count);
                foreach (Nfe nfe in listaNotasContigencia)
                {
                    lblReenviando.Text = "Reenviando Notas em Contigência + (" + listaNotasContigencia.Count + ")";
                    String caminhoArquivoContigencia = Sessao.parametroSistema.PastaRemessaNsCloud + @"\Contingencias\nao_autorizadas\" + nfe.DataEmissao.Year + @"\" + nfe.DataEmissao.Month.ToString() + @"\NFe" + nfe.Chave + ".xml";
                    if (File.Exists(caminhoArquivoContigencia))
                    {
                        try
                        {
                            if (!File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nfe.Chave + "_CONTIGENCIA.xml"))
                            {
                                File.Copy(caminhoArquivoContigencia, Sessao.parametroSistema.PastaRemessaNsCloud + @"\Remessas\" + nfe.Chave + "_CONTIGENCIA.xml");
                                await Task.Delay(5000);

                                //lblReenviando.Anchor = AnchorStyles.Bottom;
                                lblReenviando.Visible = true;
                                lblReenviando.Text = "Reenviando Nota em Contigência Nº " + nfe.NNf;
                                NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota65(nfe.Chave);
                                if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da NF-e"))
                                {
                                    modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                                }
                            }
                            else
                            {
                                // await Task.Delay(5000);
                                NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota65(nfe.Chave);
                                if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da NF-e"))
                                {
                                    modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        //Se o arquivo nao autorizado nao existe na pasta, consulta status da nota.
                        if (!String.IsNullOrEmpty(nfe.Chave))
                        {
                            NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota65(nfe.Chave);
                            if (nFCeDownloadProc != null)
                            {
                                if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da NF-e"))
                                {
                                    modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                                }
                            }
                        }
                        else
                        {
                            NfeStatus nfeStatus = new NfeStatus();
                            nfeStatus.Id = 2;
                            nfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                            nfe.NfeStatus = nfeStatus;
                            Controller.getInstance().salvar(nfe);
                            GenericaDesktop.ShowAlerta("Tente reenviar a nota: " + nfe.NNf);
                        }
                    }
                }
                lblReenviando.Visible = false;
            }
        }

        private void modificarNotaParaAutorizado(NFCeDownloadProc nFCeDownloadProc, Nfe nfe)
        {
            NfeStatusController nfeStatusController = new NfeStatusController();
            NfeStatus nfeStatus = new NfeStatus();

            if (nFCeDownloadProc.status.Equals("100"))
            {
                if (nFCeDownloadProc.retEvento != null)
                    nfe.Protocolo = nFCeDownloadProc.retEvento.nProt;

                nfe.Status = nFCeDownloadProc.nfeProc.xMotivo;
                nfe.CodStatus = "100";
                nfe.DataLancamento = nFCeDownloadProc.nfeProc.dhRecbto;
                nfe.DhSaiEnt = nFCeDownloadProc.nfeProc.dhRecbto.ToString();
                nfe.DataEmissao = nFCeDownloadProc.nfeProc.dhRecbto;
                nfeStatus = new NfeStatus();
                nfeStatus.Id = 1;
                nfe.NfeStatus = (NfeStatus)nfeStatusController.selecionar(nfeStatus);
                Controller.getInstance().salvar(nfe);//teste

                NFCeDownloadProc nota = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                if (nota.motivo.Contains("SUCESSO") || nota.motivo.Contains("sucesso") || nota.motivo.Contains("Sucesso"))
                {
                    Controller.getInstance().salvar(nfe);
                    Genericos genericosNF = new Genericos();
                    var nfeX = Genericos.LoadFromXMLString<TNfeProc>(nota.nfeProc.xml);
                    genericosNF.gravarXMLNoBanco(nfeX, 0, "S", nfe.Id);
                    generica.gravarXMLNaPasta(nota.nfeProc.xml, nfe.Chave, @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFCe.xml");
                }
            }
        }


        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, lista.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void calculaTotalNotas()
        {
            try
            {
                //sumario no grid
                GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
                tableSummaryRow1.Name = "TableSummary";
                tableSummaryRow1.ShowSummaryInRow = true;
                tableSummaryRow1.Title = " Notas: {TotalNotas}";
                tableSummaryRow1.Position = VerticalPosition.Bottom;

                GridTableSummaryRow tableSummaryRow2 = new GridTableSummaryRow();
                tableSummaryRow2.Name = "TableSummary2";
                tableSummaryRow2.ShowSummaryInRow = true;
                tableSummaryRow2.Title = " Valor Total: {ValorTotal}";
                tableSummaryRow2.Position = VerticalPosition.Bottom;

                GridSummaryColumn summaryColumn1 = new GridSummaryColumn();
                summaryColumn1.Name = "TotalNotas";
                summaryColumn1.SummaryType = SummaryType.DoubleAggregate;
                summaryColumn1.Format = "{Count}";
                summaryColumn1.MappingName = "Chave";

                GridSummaryColumn summaryColumn2 = new GridSummaryColumn();
                summaryColumn2.Name = "ValorTotal";
                summaryColumn2.SummaryType = SummaryType.DoubleAggregate;
                summaryColumn2.Format = "{Sum:c}";
                summaryColumn2.MappingName = "VNf";

                //tableSummaryRow1.SummaryColumns.Clear();
                //tableSummaryRow2.SummaryColumns.Clear();
                this.grid.TableSummaryRows.Clear();

                tableSummaryRow1.SummaryColumns.Add(summaryColumn1);
                tableSummaryRow2.SummaryColumns.Add(summaryColumn2);

                this.grid.TableSummaryRows.Add(tableSummaryRow1);
                this.grid.TableSummaryRows.Add(tableSummaryRow2);
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro no calculo do valor total: " + erro.Message);
            }
        }
        private void pesquisaNotas()
        {
            try
            {
                string modelo = "";
                if (radio65.Checked == true)
                    modelo = "65";
                else if (radio55.Checked == true)
                    modelo = "55";

                string sqlAdicional = "";
                if (radioStatusAutorizada.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.NfeStatus = 1 ";
                else if (radioStatusNaoAutorizada.Checked == true)
                    sqlAdicional = sqlAdicional + "and (Tabela.NfeStatus = 2 or Tabela.NfeStatus is null) ";
                else if (radioStatusCancelada.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.NfeStatus = 4 ";
                else if (radioStatusInutilizada.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.NfeStatus = 5 ";
                else if (radioContigencia.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.NfeStatus = 6 ";

                if (radioEntrada.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.TipoOperacao = 'E' ";
                else if (radioSaida.Checked == true)
                    sqlAdicional = sqlAdicional + "and Tabela.TipoOperacao = 'S' ";
                else
                    sqlAdicional = sqlAdicional + "and (Tabela.TipoOperacao = 'S' or Tabela.TipoOperacao = 'E') ";

                DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
                DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
                String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
                String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");
                Pesquisar(dataInicial, dataFinal, txtPesquisa.Texts.Trim(), modelo, sqlAdicional);
            }
            catch
            {
                GenericaDesktop.ShowErro("Informe as datas corretamente, caso esteja correto solicite suporte para seu representante!");
            }
        }
        private void Pesquisar(string dataInicial, string dataFinal, string valorPesquisa, string modelo, string codStatus)
        {
            lista = nfeController.selecionarNotaSaidaComVariosFiltros(dataInicial, dataFinal, valorPesquisa, modelo, codStatus);

            sfDataPager1.DataSource = lista;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 100;
            grid.DataSource = sfDataPager1.PagedSource;

            if (lista.Count == 0)
            {
                //GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisa.Texts = "";
                txtPesquisa.PlaceholderText = "";
                txtPesquisa.Select();
            }
            calculaTotalNotas();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
            // options.AutoColumnWidth = true;
            options.ExportAllPages = true;
            options.AutoRowHeight = true;
            options.ExcludeColumns.Add("Status");

            var page = document.Pages.Add();
            var PDFGrid = grid.ExportToPdfGrid(grid.View, options);
            var format = new PdfGridLayoutFormat()
            {
                Layout = PdfLayoutType.Paginate,
                Break = PdfLayoutBreakType.FitPage
            };
            //Largura da coluna
            foreach (PdfGridCell headerCell in PDFGrid.Headers[0].Cells)
            {
                if (headerCell.Value.ToString() == grid.Columns[0].HeaderText)
                {
                    var index = PDFGrid.Headers[0].Cells.IndexOf(headerCell);
                    PDFGrid.Columns[index].Width = 50;
                }
            }

            PDFGrid.Draw(page, new PointF(), format);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "ListaNotasSaida",
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    document.Save(stream);
                }
                //Message box confirmation to view the created Pdf file.
                if (MessageBox.Show("Deseja abrir o arquivo Pdf?", "Pdf criado com sucesso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the Pdf file using the default Application.
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void imprimir()
        {
            btnImprimirNf.PerformClick();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaNotasSaida",
                FilterIndex = 3,
                Filter = "Excel 97 a 2003 Files(*.xls)|*.xls|Excel 2007 a 2010 Files(*.xlsx)|*.xlsx|Excel 2013 a 2022 Files(*.xlsx)|*.xlsx"
            };

            if (saveFilterDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFilterDialog.OpenFile())
                {

                    if (saveFilterDialog.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else if (saveFilterDialog.FilterIndex == 2)
                        workBook.Version = ExcelVersion.Excel2010;
                    else
                        workBook.Version = ExcelVersion.Excel2013;
                    workBook.SaveAs(stream);
                }

                //Message box confirmation to view the created workbook.
                if (MessageBox.Show(this.grid, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private void btnAtualizarNotas_Click(object sender, EventArgs e)
        {
            pesquisaNotas();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisaNotas();
        }

        private void txtRegistroPorPagina_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void txtPesquisa_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void txtDataInicial_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaNotas();
            }
        }

        private void FrmControleNotas_Paint(object sender, PaintEventArgs e)
        {
      
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as Nfe) != null)
                {
                    if ((e.RowData as Nfe).Status != null)
                    {
                        if ((e.RowData as Nfe).Status.Contains("Autorizado") || (e.RowData as Nfe).Status.Contains("AUTORIZAD"))
                            e.Style.TextColor = Color.Green;
                        if ((e.RowData as Nfe).Status.Contains("Preparando") || (e.RowData as Nfe).Status.Contains("Falha") || (e.RowData as Nfe).Status.Contains("FALHA"))
                            e.Style.TextColor = Color.FromArgb(255, 41, 41);
                        if ((e.RowData as Nfe).Status.Contains("Cancelada") || (e.RowData as Nfe).Status.Contains("CANCEL") || (e.RowData as Nfe).Status.Contains("cancela"))
                            e.Style.TextColor = Color.Orange;
                        if ((e.RowData as Nfe).Status.Contains("Inuti") || (e.RowData as Nfe).Status.Contains("INUTI") || (e.RowData as Nfe).Status.Contains("inuti"))
                            e.Style.TextColor = Color.FromArgb(59, 138, 255);
                    }
                    if ((e.RowData as Nfe).NfeStatus != null)
                    {
                        if ((e.RowData as Nfe).NfeStatus.Id == 6)
                            e.Style.TextColor = Color.FromArgb(153, 0, 153);
                    }

                        // }
                        //else
                        //    grid.View.GetPropertyAccessProvider().SetValue(grid.GetRecordAtRowIndex(e.RowIndex), grid.Columns["PrintCCe"].MappingName, "");
                    }
            }
            catch
            {

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grid.RowCount > 0 && grid.SelectedItem != null)
            //    {
            //        nfe = (Nfe)grid.SelectedItem;
            //        if (nfe.Modelo.Equals("65"))
            //        {
            //            if (File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf"))
            //                Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf");

            //            else if (nfe.NfeStatus.Id == 6)
            //            {
            //                if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
            //                {
            //                    string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + nfe.DataCadastro.Year + @"\" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\" + nfe.DataCadastro.Day.ToString().PadLeft(2, '0') + @"\" + nfe.Chave + ".pdf";
            //                    if (File.Exists(caminhoPDF))
            //                    {
            //                        System.Diagnostics.Process.Start(caminhoPDF);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (nfe.Modelo.Equals("65"))
            //                {
            //                    NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();
            //                    nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
            //                    if (nFCeDownloadProc != null)
            //                    {
            //                        gerarPDF2(nFCeDownloadProc.pdf, nfe.Chave, true);
            //                    }
            //                }
            //            }
            //        }
            //        else if(nfe.Modelo.Equals("55"))
            //        {
            //            if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
            //                Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
            //            else 
            //            {
            //                NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
            //                nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
            //                if (nFeDownloadProc55 != null)
            //                {
            //                    gerarPDF2(nFeDownloadProc55.pdf, nfe.Chave, true);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch(Exception erro)
            //{
            //    GenericaDesktop.ShowAlerta("Falha ao imprimir nota. " + erro.Message);
            //}
        }

        private void gerarPDF2(String pdf, String chave, bool imprimir)
        {
            if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
            {
                if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    frmPDF.ShowDialog();
                }

            }
            else if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }

        private void gerarPDF2Canceladas(String pdf, String chave, bool imprimir)
        {
            if (nfe.TipoOperacao == "S" && nfe.Modelo.Equals("65"))
            {
                if (!File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFCe.pdf");
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf");
                    frmPDF.ShowDialog();
                }

            }
            else if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf"))
                {
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + chave + "-CAN.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }

        private void gerarPDF2CCe(String pdf, String chave, bool imprimir)
        {
            if (nfe.Modelo.Equals("55"))
            {
                if (!File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\" + chave + "-CCE.pdf"))
                {
                    if (!Directory.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\"))
                        Directory.CreateDirectory(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\");
                    byte[] bytes = Convert.FromBase64String(pdf);
                    System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\" + chave + "-CCE.pdf", FileMode.CreateNew);
                    System.IO.BinaryWriter writer =
                        new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                }
                if (imprimir == true)
                {
                    FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\" + chave + "-CCE.pdf");
                    frmPDF.ShowDialog();
                    //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + chave + "-procNFe.pdf");
                }
            }
        }

        private void FrmControleNotas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    geradorSintegra.gerarSintegra(data1, data2, Sessao.empresaFilialLogada, @"SINTEGRA\" + data1.Year + data1.Month.ToString().PadLeft(2, '0'), false, "2000-01-01 00:00:00");
                    break;
                case Keys.F3:
                    imprimir();
                    break;
            }
        }

        private void radio65_CheckChanged(object sender, EventArgs e)
        {
            if (radio65.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radio55_CheckChanged(object sender, EventArgs e)
        {
            if (radio55.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioTodas_CheckChanged(object sender, EventArgs e)
        {
            if (radioTodas.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioStatusTodas_CheckChanged(object sender, EventArgs e)
        {
            if (radioStatusTodas.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioStatusNaoAutorizada_CheckChanged(object sender, EventArgs e)
        {
            if (radioStatusNaoAutorizada.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioStatusAutorizada_CheckChanged(object sender, EventArgs e)
        {
            if (radioStatusAutorizada.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string caminhoX = "";
            string motivo = "";
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                nfe = (Nfe)grid.SelectedItem;
                if (nfe.NfeStatus.Id == 1)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja realmente cancelar a nota selecionada?"))
                    {
                        Form formBackground = new Form();
                        using (FrmJustificativa uu = new FrmJustificativa("Justificativa de Cancelamento", "Justificativa", nfe))
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
                            switch (uu.showModal(ref motivo))
                            {
                                case DialogResult.Ignore:
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    break;
                            }

                            formBackground.Dispose();
                        }

                        if (!String.IsNullOrEmpty(motivo))
                        {
                            var retorno = generica.ns_CancelarNF(nfe, motivo);
                            if (retorno != null)
                            {
                                if (retorno.status.Equals("135") || retorno.status.Equals("200"))
                                {
                                    if (retorno.motivo != null)
                                    {
                                        if (retorno.motivo.Contains("NF-e cancelada com sucesso") || retorno.motivo.Contains("Evento registrado e vinculado a NF-e") || retorno.motivo.Contains("NFC-e cancelada com sucesso"))
                                        {
                                            nfe.Status = retorno.motivo;
                                            nfe.CodStatus = retorno.status;
                                            nfe.Cancelada = true;
                                            NfeStatus nfStatus = new NfeStatus();
                                            nfStatus.Id = 4;
                                            nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
                                            nfe.NfeStatus = nfStatus;
                                            Controller.getInstance().salvar(nfe);
                                            OrdemServico os = new OrdemServico();
                                            OrdemServicoController ordemServicoController = new OrdemServicoController();
                                            Venda vendaVinculada = new Venda();
                                            //se tem ordem de serviço com notas vinculadas o sistma deve anular o numero da nfe da o.s
                                            try
                                            {
                                                os = ordemServicoController.selecionarOrdemServicoPorNfe(nfe.Id);
                                                vendaVinculada = vendaController.selecionarVendaPorNF(nfe.Id);
                                                if (os != null)
                                                {
                                                    if (os.Id > 1)
                                                    {
                                                        os.Nfe = null;
                                                        Controller.getInstance().salvar(os);
                                                    }
                                                }
                                                if (vendaVinculada != null)
                                                {
                                                    if (vendaVinculada.Id > 1)
                                                    {
                                                        vendaVinculada.Nfe = null;
                                                        Controller.getInstance().salvar(vendaVinculada);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                GenericaDesktop.ShowAlerta("Ordem de serviço ainda vinculada a nota fiscal.");
                                            }
                                            if (nfe.Modelo.Equals("65"))
                                            {
                                                var nfceCancelada = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.Chave);
                                                if (nfceCancelada != null)
                                                {
                                                    string caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\";
                                                    string nomeArquivo = nfe.Chave + @"-CAN.xml";
                                                    caminhoX = caminhoXML + nomeArquivo;
                                                    generica.gravarXMLNaPasta(nfceCancelada.retEvento.xml, nfe.Chave, caminhoXML, nomeArquivo);
                                                }

                                                //EnviaXML PAINEL LUNAR 
                                                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                                byte[] arquivo;
                                                using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                                {
                                                    using (var reader = new BinaryReader(stream))
                                                    {
                                                        arquivo = reader.ReadBytes((int)stream.Length);
                                                        var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "CANCELADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                    }
                                                }
                                            }
                                            else if (nfe.Modelo.Equals("55"))
                                            {
                                                var nfceCancelada = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, true, false, "");
                                                if (nfceCancelada != null)
                                                {
                                                    string caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\";
                                                    string nomeArquivo = nfe.Chave + @"-CAN.xml";
                                                    caminhoX = caminhoXML + nomeArquivo;
                                                    generica.gravarXMLNaPasta(nfceCancelada.xml, nfe.Chave, caminhoXML, nomeArquivo);
                                                }
                                                //EnviaXML PAINEL LUNAR 
                                                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                                byte[] arquivo;
                                                using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                                {
                                                    using (var reader = new BinaryReader(stream))
                                                    {
                                                        arquivo = reader.ReadBytes((int)stream.Length);
                                                        var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "CANCELADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                    }
                                                }
                                            }

                                            //Atualiza Estoque Conciliado
                                            NfeProdutoController nfeProdutoController = new NfeProdutoController();
                                            IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                                            foreach (NfeProduto nfeProduto in listaProdutos)
                                            {
                                                if (nfe.MovimentaEstoque == true)
                                                    generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "CANCELAMENTO NF: " + nfe.NNf + " MOD: " + nfe.Modelo, nfe.Cliente, DateTime.Now, null);
                                                Venda venda = vendaController.selecionarVendaPorNF(nfe.Id);
                                                if (venda != null)
                                                {
                                                    if (venda.Id > 0)
                                                    {
                                                        venda.Nfe = null;
                                                        Controller.getInstance().salvar(venda);

                                                        if (GenericaDesktop.ShowConfirmacao("Deseja também cancelar a venda ligada a essa NF?"))
                                                        {
                                                            venda.Cancelado = true;
                                                            venda.Observacoes = "Venda Cancelada Junto com NF: " + nfe.NNf + "Mod: " + nfe.Modelo + " Operador: " + Sessao.usuarioLogado.Login;
                                                            venda.Nfe = null;
                                                            Controller.getInstance().salvar(venda);
                                                            generica.atualizarEstoqueNaoConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "CANCELAMENTO NF: " + nfe.NNf + " MOD: " + nfe.Modelo, nfe.Cliente, DateTime.Now, null);
                                                        }
                                                    }
                                                }
                                                else if (nfe.Modelo.Equals("55") && venda == null && nfe.MovimentaEstoque == true)
                                                {
                                                    //NFE EMITIDA AVULSO NA TELA DE NFE
                                                    generica.atualizarEstoqueNaoConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "CANCELAMENTO NF: " + nfe.NNf + " MOD: " + nfe.Modelo, nfe.Cliente, DateTime.Now, null);
                                                }
                                            }

                                            if (retorno.nfeProc != null)
                                                GenericaDesktop.ShowInfo(retorno.motivo + "\n" + retorno.nfeProc.xMotivo);
                                            else
                                                GenericaDesktop.ShowInfo(retorno.motivo);
                                        }
                                        pesquisaNotas();
                                    }
                                }
                                else if (retorno.status != "135")
                                {
                                    if (retorno.nfeProc != null)
                                        GenericaDesktop.ShowAlerta(retorno.motivo + "\n" + retorno.nfeProc.cStat + " " + retorno.nfeProc.xMotivo);
                                    else
                                        GenericaDesktop.ShowAlerta(retorno.motivo);
                                }
                            }
                            else
                            {
                                GenericaDesktop.ShowAlerta("Erro de retorno da Sefaz, tente cancelar mais tarde");
                            }
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Informe uma justificativa válida para cancelar a nota fiscal!");
                        }
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("É possível cancelar apenas notas autorizadas, a nota selecionada não foi autorizada pela sefaz, talvez o que precisa seja inutilizar!");
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro selecione a nota que deseja cancelar");
            }
        }

        private void radioStatusCancelada_CheckChanged(object sender, EventArgs e)
        {
            if (radioStatusCancelada.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioStatusInutilizada_CheckChanged(object sender, EventArgs e)
        {
            if (radioStatusInutilizada.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
            string motivo = "";
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                nfe = (Nfe)grid.SelectedItem;
                if(nfe.NfeStatus == null)
                {
                    if(nfe.Status.Equals("Preparando Envio..."))
                    {
                        NfeStatus nfeStatus = new NfeStatus();
                        nfeStatus.Id = 2;
                        nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                        nfe.NfeStatus = nfeStatus;
                        Controller.getInstance().salvar(nfe);
                    }
                }
                if (nfe.NfeStatus.Id != 1)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja realmente inutilizar o número " + nfe.NNf + " ?"))
                    {

                        Form formBackground = new Form();
                        using (FrmJustificativa uu = new FrmJustificativa("Justificativa de Inutilização", "Justificativa", nfe))
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
                            switch (uu.showModal(ref motivo))
                            {
                                case DialogResult.Ignore:
                                    formBackground.Dispose();
                                    break;
                                case DialogResult.OK:
                                    break;
                            }

                            formBackground.Dispose();
                        }

                        if (!String.IsNullOrEmpty(motivo))
                        {
                            if (nfe.Modelo.Equals("65"))
                            {
                                var retorno = generica.ns_InutilizarNFCe(nfe, motivo);
                                if (retorno.status.Equals("102"))
                                {
                                    nfe.Status = retorno.motivo;
                                    nfe.CodStatus = retorno.status;
                                    nfe.Cancelada = false;
                                    NfeStatus nfStatus = new NfeStatus();
                                    nfStatus.Id = 5;
                                    nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
                                    nfe.NfeStatus = nfStatus;
                                    nfe.Destinatario = "NF INUTILIZADA";
                                    nfe.IdInut = retorno.retInutNFe.idInut;
                                    Controller.getInstance().salvar(nfe);

                                    try
                                    {
                                        OrdemServicoController ordemServicoController = new OrdemServicoController();
                                        OrdemServico ordem = ordemServicoController.selecionarOrdemServicoPorNfe(nfe.Id);
                                        if (ordem != null)
                                        {
                                            if (ordem.Id > 0)
                                            {
                                                ordem.Nfe = null;
                                                Controller.getInstance().salvar(ordem);
                                            }
                                        }
                                        VendaController vendaController = new VendaController();
                                        Venda venda = vendaController.selecionarVendaPorNF(nfe.Id);
                                        if(venda != null)
                                        {
                                            if(venda.Id > 0)
                                            {
                                                venda.Nfe = null;
                                                Controller.getInstance().salvar(venda);
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    generica.gravarXMLNaPasta(retorno.retInutNFe.xml,
                                            nfe.Chave, @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\", nfe.NNf + @"-INU.xml");

                                    //EnviaXML PAINEL LUNAR 
                                    string caminhoX = @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.NNf + @"-INU.xml";
                                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                    byte[] arquivo;
                                    using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var reader = new BinaryReader(stream))
                                        {
                                            arquivo = reader.ReadBytes((int)stream.Length);
                                            var retor = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFCE", "INUTILIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                        }
                                    }
                                    //Atualiza Estoque Conciliado
                                    //NfeProdutoController nfeProdutoController = new NfeProdutoController();
                                    //IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                                    //foreach (NfeProduto nfeProduto in listaProdutos)
                                    //{
                                    //    generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "INUTILIZAÇÃO NF: " + nfe.NNf + " MOD: " + nfe.Modelo);
                                    //    if (GenericaDesktop.ShowConfirmacao("Deseja também cancelar a venda ligada a essa NF?"))
                                    //    {
                                    //        Venda venda = vendaController.selecionarVendaPorNF(nfe.Id);
                                    //        venda.Cancelado = true;
                                    //        venda.Observacoes = "Venda Cancelada Junto com NF: " + nfe.NNf + "Mod: " + nfe.Modelo + " Operador: " + Sessao.usuarioLogado.Login;
                                    //        Controller.getInstance().salvar(venda);
                                    //        generica.atualizarEstoqueNaoConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "INUTILIZAÇÃO NF: " + nfe.NNf + " MOD: " + nfe.Modelo);
                                    //    }
                                    //}

                                    if (retorno.retInutNFe != null)
                                        GenericaDesktop.ShowInfo(retorno.retInutNFe.xMotivo);
                                    else
                                        GenericaDesktop.ShowInfo(retorno.motivo);
                                }
                                else if (retorno.status != "102")
                                {
                                    if (retorno.retInutNFe != null)
                                        GenericaDesktop.ShowAlerta(retorno.motivo + "\n" + retorno.retInutNFe.cStat + " " + retorno.retInutNFe.xMotivo);
                                    else
                                        GenericaDesktop.ShowAlerta(retorno.motivo);
                                }
                            }
                            else if (nfe.Modelo.Equals("55"))
                            {
                                //var retorno = generica.ns_InutilizarNFe(nfe, "Numeração não utilizada - TESTE DE INTEGRACAO");
                                string caminhoInu = @"Fiscal\XML\NFe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\"; // " + nfe.NNf + @"-INU.xml

                                string tpAmbiente = "";
                                if (Sessao.parametroSistema.AmbienteProducao == true)
                                    tpAmbiente = "1";
                                else
                                    tpAmbiente = "2";
                                InutilizarReq inutilizarReq = new InutilizarReq();
                                inutilizarReq.ano = nfe.DataCadastro.Year.ToString().Substring(2, 2);
                                inutilizarReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                                inutilizarReq.cUF = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Ibge;
                                inutilizarReq.serie = nfe.Serie;
                                inutilizarReq.tpAmb = tpAmbiente;
                                inutilizarReq.nNFIni = nfe.NNf;
                                inutilizarReq.nNFFin = nfe.NNf;
                                inutilizarReq.xJust = motivo;
                                var ret = NSSuite.inutilizarNumeracaoESalvar(nfe.Modelo, inutilizarReq, caminhoInu, Sessao.empresaFilialLogada.Cnpj, false);
                                var retorno = JsonConvert.DeserializeObject<RetornoInutilizacao>(ret);
                                try
                                {
                                    if (retorno.status == 102 || retorno.status == 200)
                                    {
                                        if (retorno.retornoInutNFe != null)
                                        {
                                            nfe.Chave = retorno.retornoInutNFe.chave;
                                            nfe.Status = retorno.retornoInutNFe.xMotivo;
                                        }
                                        nfe.CodStatus = retorno.status.ToString();
                                        nfe.Cancelada = false;
                                        NfeStatus nfStatus = new NfeStatus();
                                        nfStatus.Id = 5;
                                        nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
                                        nfe.NfeStatus = nfStatus;
                                        nfe.Destinatario = "NF INUTILIZADA";
                                        nfe.IdInut = retorno.retornoInutNFe.idInut;
                                        Controller.getInstance().salvar(nfe);
                                        //remove o numero da nota da origem
                                        try
                                        {
                                            OrdemServicoController ordemServicoController = new OrdemServicoController();
                                            OrdemServico ordem = ordemServicoController.selecionarOrdemServicoPorNfe(nfe.Id);
                                            if (ordem != null)
                                            {
                                                if (ordem.Id > 0)
                                                {
                                                    ordem.Nfe = null;
                                                    Controller.getInstance().salvar(ordem);
                                                }
                                            }
                                            VendaController vendaController = new VendaController();
                                            Venda venda = vendaController.selecionarVendaPorNF(nfe.Id);
                                            if (venda != null)
                                            {
                                                if (venda.Id > 0)
                                                {
                                                    venda.Nfe = null;
                                                    Controller.getInstance().salvar(venda);
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        if (retorno.retornoInutNFe != null)
                                        {
                                            if (retorno.retornoInutNFe.xml != null)
                                                generica.gravarXMLNaPasta(retorno.retornoInutNFe.xml,
                                                        nfe.Chave, @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\", nfe.NNf + @"-INU.xml");

                                            string caminhoX = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.NNf + @"-INU.xml";
                                            LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                            byte[] arquivo;
                                            using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                            {
                                                using (var reader = new BinaryReader(stream))
                                                {
                                                    arquivo = reader.ReadBytes((int)stream.Length);
                                                    var retor = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFE", "INUTILIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                }
                                            }
                                        }

                                        //Atualiza Estoque Conciliado
                                        //NfeProdutoController nfeProdutoController = new NfeProdutoController();
                                        //IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                                        //foreach (NfeProduto nfeProduto in listaProdutos)
                                        //{
                                        //    generica.atualizarEstoqueConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "INUTILIZAÇÃO NF: " + nfe.NNf + " MOD: " + nfe.Modelo);
                                        //    if (GenericaDesktop.ShowConfirmacao("Deseja também cancelar a venda ligada a essa NF?"))
                                        //    {
                                        //        Venda venda = vendaController.selecionarVendaPorNF(nfe.Id);
                                        //        venda.Cancelado = true;
                                        //        venda.Observacoes = "Venda Cancelada Junto com NF: " + nfe.NNf + "Mod: " + nfe.Modelo + " Operador: " + Sessao.usuarioLogado.Login;
                                        //        Controller.getInstance().salvar(venda);
                                        //        generica.atualizarEstoqueNaoConciliado(nfeProduto.Produto, double.Parse(nfeProduto.QCom), true, "NFE", "INUTILIZAÇÃO NF: " + nfe.NNf + " MOD: " + nfe.Modelo);
                                        //    }
                                        //}

                                        if (retorno.retornoInutNFe != null)
                                            GenericaDesktop.ShowInfo(retorno.retornoInutNFe.xMotivo);
                                        else
                                            GenericaDesktop.ShowInfo(retorno.motivo);

                                        pesquisaNotas();
                                    }
                                    else
                                    {
                                        if (retorno.retornoInutNFe != null)
                                            GenericaDesktop.ShowAlerta(retorno.motivo + "\n" + retorno.retornoInutNFe.cStat + " " + retorno.retornoInutNFe.xMotivo);
                                        if (retorno.erro != null)
                                        {
                                            GenericaDesktop.ShowAlerta("Falha ao Inutilizar: \n" + retorno.erro.cStat + " " + retorno.erro.xMotivo + "\nCaso tenha venda vinculada a esta nota, cancele a venda na tela de cancelamento de vendas!");
                                            if (retorno.erro.cStat == 997 && retorno.erro.xMotivo.Contains("Anteriomente e Autorizada"))
                                            {
                                                nfe.Status = "Inutilizacao de numero homologado";
                                                nfe.CodStatus = retorno.status.ToString();
                                                nfe.Cancelada = false;
                                                NfeStatus nfStatus = new NfeStatus();
                                                nfStatus.Id = 5;
                                                nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
                                                nfe.NfeStatus = nfStatus;
                                                nfe.Destinatario = "NF INUTILIZADA";
                                                Controller.getInstance().salvar(nfe);

                                                if (!String.IsNullOrEmpty(nfe.Chave))
                                                {
                                                    var retornoInut = generica.ns_DownloadEventoInutilizacaoNFE(nfe);
                                                    if (retornoInut != null)
                                                    {
                                                        if (retornoInut.retornoInutNFe != null)
                                                        {
                                                            generica.gravarXMLNaPasta(retornoInut.retornoInutNFe.xml,
                                                                    nfe.Chave, @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\", nfe.NNf + @"-INU.xml");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    pesquisaNotas();
                                }
                                catch
                                {
                                    GenericaDesktop.ShowErro("Falha ao inutilizar documento, tente mais tarde!");
                                }
                            }
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Informe uma justificativa válida");
                        }
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Nota autorizada não pode ser inutilizada, nesse caso deve tentar cancelar!");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro selecione a nota que deseja inutilizar");
            }
        }

        private void radioContigencia_CheckChanged(object sender, EventArgs e)
        {
            if (radioContigencia.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private async void btnReenviar_Click(object sender, EventArgs e)
        {
            try
            {
                string retorno = "";
                nfe = (Nfe)grid.SelectedItem;
                if (chkSVCAN.Checked == true && nfe.Modelo.Equals("55"))
                {
                    nfe.TpEmis = "6";
                        Controller.getInstance().salvar(nfe);
                }
            
                if (nfe.NfeStatus == null)
                {
                    if (nfe.Status.Equals("Preparando Envio..."))
                    {
                        NfeStatus nfeStatus = new NfeStatus();
                        nfeStatus.Id = 2;
                        nfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                        nfe.NfeStatus = nfeStatus;
                        Controller.getInstance().salvar(nfe);
                    }
                }
                if (nfe.NfeStatus != null)
                {
                    if (nfe.NfeStatus.Id == 2)
                    {
                        decimal totalNotaSemDesconto = 0;
                        decimal totalDesconto = 0;
                        decimal totalNotaComDesconto = 0;

                        NfeProdutoController nfeProdutoController = new NfeProdutoController();
                        OrdemServicoController ordemServicoController = new OrdemServicoController();

                        Venda venda = null;
                        OrdemServico ordemServico = null;
                        if (nfe.NotaAgrupada == false)
                        {
                            venda = vendaController.selecionarVendaPorNF(nfe.Id);
                            ordemServico = ordemServicoController.selecionarOrdemServicoPorNfe(nfe.Id);
                        }

                        //Verifica valores dos produtos
                        IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                        foreach (NfeProduto nfeProduto in listaProdutos)
                        {
                            totalNotaSemDesconto = (totalNotaSemDesconto + nfeProduto.VUnCom) * decimal.Parse(nfeProduto.QCom);
                            totalDesconto = totalDesconto + nfeProduto.VDesc;
                            nfeProduto.Ncm = nfeProduto.Produto.Ncm;
                            nfeProduto.Cest = nfeProduto.Produto.Cest;
                            nfeProduto.Cfop = nfeProduto.Produto.CfopVenda;
                            nfeProduto.CodAnp = nfeProduto.Produto.CodAnp;
                            nfeProduto.CstIcms = nfeProduto.Produto.CstIcms;
                            nfeProduto.CstPis = nfeProduto.Produto.CstPis;
                            nfeProduto.CstCofins = nfeProduto.Produto.CstCofins;
                            nfeProduto.CstIpi = nfeProduto.Produto.CstIpi;
                     
                            Controller.getInstance().salvar(nfeProduto);
                        }
                      
                        totalNotaComDesconto = totalNotaSemDesconto - totalDesconto;
                        nfe.VNf = totalNotaComDesconto;
                        Controller.getInstance().salvar(nfe); 
                        //Reenvia nota
                        if (venda != null || ordemServico != null)
                        {
                            IList<NfeProduto> listaProdutosAtualizados = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                            //totalNotaComDesconto = totalNotaSemDesconto - totalDesconto;
                            if (venda != null || ordemServico != null)
                            {
                                if (nfe.Modelo.Equals("65"))
                                {
                                    EmitirNFCe emitirNFCe = new EmitirNFCe();
                                    if (venda != null)
                                        xmlStrEnvio = emitirNFCe.gerarXMLNfce(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, venda.Cliente, venda, null, null);
                                    else if (ordemServico != null)
                                    {
                                        //Essa configuração foi devido a nf ter a possibilidade de emissão da nota com cliente diferente da O.S
                                        if(nfe.Cliente == null)
                                            xmlStrEnvio = emitirNFCe.gerarXMLNfce(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, ordemServico.Cliente, null, ordemServico, null);
                                        else
                                            xmlStrEnvio = emitirNFCe.gerarXMLNfce(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, nfe.Cliente, null, ordemServico, null);
                                    }

                                    if (!String.IsNullOrEmpty(xmlStrEnvio))
                                    {
                                        retorno = emitirNFCe.ReenviarXMLApi(xmlStrEnvio, nfe);
                                    }
                                    if (retorno.Contains("Autorizada com Sucesso"))
                                    {
                                        nfe.Lancada = true;
                                        Controller.getInstance().salvar(nfe);
                                        string caminhoX = @"Fiscal\XML\NFCe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" +nfe.Chave + "-procNFCe.xml";
                                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                        byte[] arquivo;
                                        using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                        {
                                            using (var reader = new BinaryReader(stream))
                                            {
                                                arquivo = reader.ReadBytes((int)stream.Length);
                                                var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                            }
                                        }
                                        pesquisaNotas();
                                    }
                                }
                                else if (nfe.Modelo.Equals("55"))
                                {
                                    //CONSULTA NOTA
                                    ConsStatusProcessamentoReq consStatusProcessamentoReq = new ConsStatusProcessamentoReq();
                                    consStatusProcessamentoReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                                    consStatusProcessamentoReq.nsNRec = nfe.NsNrec;
                                    if (Sessao.parametroSistema.AmbienteProducao == true)
                                        consStatusProcessamentoReq.tpAmb = "1";
                                    else
                                        consStatusProcessamentoReq.tpAmb = "2";

                                    RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
                                    String retornoConsulta = NSSuite.consultarStatusProcessamento(nfe.Modelo, consStatusProcessamentoReq);
                                    if (retornoConsulta != null)
                                        retConsulta = JsonConvert.DeserializeObject<RetConsultaProcessamentoNF>(retornoConsulta);

                                    if (retConsulta.xMotivo != null)
                                    {
                                        if (retConsulta.xMotivo.Contains("Autorizado o uso"))
                                        {
                                            NfeStatusController nfeStatusController = new NfeStatusController();
                                            nfe.Protocolo = retConsulta.nProt;
                                            nfe.Chave = retConsulta.chNFe;
                                            nfe.CodStatus = retConsulta.cStat;
                                            nfe.Status = retConsulta.xMotivo;
                                            nfe.Lancada = true;
                                            NfeStatus nfeStatus = new NfeStatus();
                                            nfeStatus.Id = 1;
                                            nfe.NfeStatus = (NfeStatus)nfeStatusController.selecionar(nfeStatus);
                                            Genericos genericosNF = new Genericos();
                                            NFeDownloadProc55 nota = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                            var nfeRet = Genericos.LoadFromXMLString<TNfeProc>(nota.xml);
                                            genericosNF.gravarXMLNoBanco(nfeRet, 0, "S", nfe.Id);
                                            Controller.getInstance().salvar(nfe);
                                            generica.gravarXMLNaPasta(retConsulta.xml, retConsulta.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");
                                            GenericaDesktop.ShowInfo(retConsulta.xMotivo);

                                            string caminhoX = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                                            LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                            byte[] arquivo;
                                            using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                            {
                                                using (var reader = new BinaryReader(stream))
                                                {
                                                    arquivo = reader.ReadBytes((int)stream.Length);
                                                    var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                }
                                            }
                                            pesquisaNotas();
                                        }
                                        //Reenvia a nota
                                        else
                                        {

                                            EmitirNFe emitirNFe = new EmitirNFe();
                                            if(venda != null)
                                                xmlStrEnvio = emitirNFe.gerarXMLNfe(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, venda.Cliente, venda, true, nfe.NatOp, null);
                                            if (ordemServico != null)
                                                xmlStrEnvio = emitirNFe.gerarXMLNfe(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, ordemServico.Cliente, null, true, nfe.NatOp, ordemServico);
                                            if (!String.IsNullOrEmpty(xmlStrEnvio))
                                            {
                                                retorno = emitirNFe.ReenviarXMLApi(xmlStrEnvio, nfe);
                                            }
                                            if (retorno.Contains("Autorizada com Sucesso"))
                                            {
                                                nfe.Lancada = true;
                                                Controller.getInstance().salvar(nfe);
                                                string caminhoX = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                                                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                                byte[] arquivo;
                                                using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                                {
                                                    using (var reader = new BinaryReader(stream))
                                                    {
                                                        arquivo = reader.ReadBytes((int)stream.Length);
                                                        var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                    }
                                                }
                                                pesquisaNotas();
                                            }
                                            //Verifica o q esta acontecendo com a nota.
                               
                                        }
                                    }
                                    //Reenvia a nota
                                    else
                                    {

                                        EmitirNFe emitirNFe = new EmitirNFe();
                                        if(venda != null)
                                            xmlStrEnvio = emitirNFe.gerarXMLNfe(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, venda.Cliente, venda, true, nfe.NatOp, null);
                                        if (ordemServico != null)
                                            xmlStrEnvio = emitirNFe.gerarXMLNfe(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutosAtualizados, ordemServico.Cliente, null, true, nfe.NatOp, ordemServico);
                                        if (!String.IsNullOrEmpty(xmlStrEnvio))
                                        {
                                            retorno = emitirNFe.ReenviarXMLApi(xmlStrEnvio, nfe);
                                        }
                                        if (retorno.Contains("Autorizada com Sucesso"))
                                        {
                                            string caminhoX = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                                            LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                            byte[] arquivo;
                                            using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                            {
                                                using (var reader = new BinaryReader(stream))
                                                {
                                                    arquivo = reader.ReadBytes((int)stream.Length);
                                                    var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                }
                                            }
                                            pesquisaNotas();
                                        }
                                    }
                                    if(nfe.NfeStatus.Id == 2)
                                                btnConsultarNota.PerformClick();
                                }
                            }
                        }
                        //Se for uma nota gerada na tela de geracao de notas agrupadas
                        else if (venda == null && ordemServico == null && nfe.Modelo == "65")
                        {
                            FormaPagamento formaPag = new FormaPagamento();
                            NfePagamentoController nfePagamentoController = new NfePagamentoController();
                            IList<NfePagamento> listaPagamento = nfePagamentoController.selecionarPagamentoPorNfe(nfe.Id);
                            if (listaPagamento.Count > 0)
                            {
                                foreach (NfePagamento nfePagamento in listaPagamento)
                                {
                                    formaPag = nfePagamento.FormaPagamento;
                                }
                            }
                            else
                            {
                                formaPag.Id = 2;
                                formaPag = (FormaPagamento)Controller.getInstance().selecionar(formaPag);
                            }
                            EmitirNFCe emitirNFCe = new EmitirNFCe();
                            if (nfe.Cliente == null)
                                xmlStrEnvio = emitirNFCe.gerarXMLNfce(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutos, nfe.Cliente, null, null, formaPag);
                            else
                                xmlStrEnvio = emitirNFCe.gerarXMLNfce(totalNotaSemDesconto, totalNotaComDesconto, totalDesconto, nfe.NNf, listaProdutos, nfe.Cliente, null, null, formaPag);
                            if (!String.IsNullOrEmpty(xmlStrEnvio))
                            {
                                retorno = emitirNFCe.ReenviarXMLApi(xmlStrEnvio, nfe);
                            }
                            if (retorno.Contains("Autorizada com Sucesso"))
                            {
                                string caminhoX = @"Fiscal\XML\NFCe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                                LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                byte[] arquivo;
                                using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                {
                                    using (var reader = new BinaryReader(stream))
                                    {
                                        arquivo = reader.ReadBytes((int)stream.Length);
                                        var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                    }
                                }
                                pesquisaNotas();
                            }
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Dê 2 cliques na nota fiscal e selecione a opção de enviar nota!");
                        }
                    }
                    else if(nfe.NfeStatus.Id == 6)
                    {
                        DateTime primeiroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime ultimoDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        ultimoDiaDoMes = ultimoDiaDoMes.AddMonths(1).AddDays(-1);
                        reenviarNotasEmContigencia(primeiroDiaDoMes.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), ultimoDiaDoMes.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
                        btnPesquisar.PerformClick();
                    }
                    else
                        GenericaDesktop.ShowAlerta("Só é possível reenviar uma nota que esteja com status de rejeição!");
                    //Verifica se houve falha do sistema e confere na pasta de contigencia se a nota esta em contigencia
                    //else
                    //{
                    //    string ret = lerTXT2(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Processados\nsConcluido\" + nfe.NNf + ".txt");
                    //    await Task.Delay(3000);
                    //    if (ret != null)
                    //    {
                    //        if (nfe.Modelo.Equals("65"))
                    //        {
                    //            NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota65(nfe.Chave);
                    //            if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da NF-e"))
                    //            {
                    //                modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                    //            }
                    //        }
                    //        else if (nfe.Modelo.Equals("55"))
                    //        {
                    //            //NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota(nfe.Chave);
                    //            //if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da NF-e"))
                    //            //{
                    //            //    modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                    //            //}
                    //        }
                    //    }
                    //}
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Essa funcionalidade é apenas para notas com origem na tela de vendas, nesse caso você deve dar 2 cliques na nota, fazer os ajustes necessários e enviar a nota novamente!");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Falha ao reenviar nota: " + erro.Message);
            }
        }

        private string lerTXT2(string caminhoArquivo)
        {
            try
            {
                //List<String> dadosLidos = new List<String>();
                string statusContigencia = "";
                string chave = "";
                System.IO.StreamReader arquivo = new System.IO.StreamReader(caminhoArquivo);
                string linha = "";
                while (true)
                {
                    linha = arquivo.ReadLine();
                    if (linha != null)
                    {
                        string[] DadosColetados = linha.Split('|');
                        if (DadosColetados.Length > 4)
                        {
                            chave = DadosColetados[5].Replace("NFe", "");
                            statusContigencia = DadosColetados[4];
                        }
                    }
                    else
                        break;
                }
                if (nfe.Id > 0 && statusContigencia.Contains("Emitido em contingencia offline"))
                {
                    nfe.Chave = chave;
                    nfe.CodStatus = "";
                    NfeStatus nfeStatus = new NfeStatus();
                    nfeStatus.Id = 6;
                    nfe.NfeStatus = (NfeStatus)Controller.getInstance().selecionar(nfeStatus);
                    string codStatusRet = "50";
                    nfe.Status = "Emitido em contingencia offline";
                    Controller.getInstance().salvar(nfe);
                }
                return chave;
            }
            catch
            {
                return null;
            }
        }
        //private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    nfe = (Nfe)grid.SelectedItem;
        //    FrmProdutosNota frmProdutosNota = new FrmProdutosNota(nfe);
        //    frmProdutosNota.ShowDialog();
        //}

        private void grid_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            nfe = (Nfe)grid.SelectedItem;
            OrdemServicoController ordemServicoController = new OrdemServicoController();
            OrdemServico ordem = new OrdemServico();
            ordem = ordemServicoController.selecionarOrdemServicoPorNfe(nfe.Id);
            if (nfe.Modelo == "55" && ordem == null)
            {
                if (nfe.NfeStatus != null)
                {
                    if (nfe.NfeStatus.Id != 2)
                    {
                        Form formBackground = new Form();
                        FrmProdutosNota uu = new FrmProdutosNota(nfe);
                        formBackground.StartPosition = FormStartPosition.Manual;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        formBackground.Left = Top = 0;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        uu.ShowDialog();
                        uu.Dispose();
                        formBackground.Dispose();
                        pesquisaNotas();
                    }
                    else
                    {
                        Form formBackground = new Form();
                        FrmNfe uu = new FrmNfe(nfe);
                        formBackground.StartPosition = FormStartPosition.Manual;
                        formBackground.Opacity = .50d;
                        formBackground.BackColor = Color.Black;
                        formBackground.Left = Top = 0;
                        formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                        formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                        formBackground.WindowState = FormWindowState.Maximized;
                        formBackground.TopMost = false;
                        formBackground.Location = this.Location;
                        formBackground.ShowInTaskbar = false;
                        formBackground.Show();
                        uu.Owner = formBackground;
                        uu.ShowDialog();
                        uu.Dispose();
                        formBackground.Dispose();
                        pesquisaNotas();
                    }
                }
                else
                {
                    Form formBackground = new Form();
                    FrmNfe uu = new FrmNfe(nfe);
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    uu.ShowDialog();
                    uu.Dispose();
                    formBackground.Dispose();
                    pesquisaNotas();
                }
            }
            else
            {
                Form formBackground = new Form();
                FrmProdutosNota uu = new FrmProdutosNota(nfe);
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Left = Top = 0;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                uu.Dispose();
                formBackground.Dispose();
                pesquisaNotas();
            }
        }

        private void btnConsultarNota_Click(object sender, EventArgs e)
        {
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                try
                {
                    Genericos genericosNF = new Genericos();
                    //CONSULTA NOTA
                    nfe = (Nfe)grid.SelectedItem;
                    nfe.Chave = GenericaDesktop.RemoveCaracteres(nfe.Chave.Trim());
                    if (nfe.Chave.Length != 44)
                        nfe.Chave = "";
                    Controller.getInstance().salvar(nfe);
                    if (nfe.Status != "Autorizado o uso da NF-e" && nfe.Status != "Inutilizacao de Numero homologado" && !nfe.Status.Contains("cancelada com sucesso"))
                    {
                        //para modelo 55
                        if (!String.IsNullOrEmpty(nfe.NsNrec))
                        {
                            ConsStatusProcessamentoReq consStatusProcessamentoReq = new ConsStatusProcessamentoReq();
                            consStatusProcessamentoReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                            consStatusProcessamentoReq.nsNRec = nfe.NsNrec;
                            if (Sessao.parametroSistema.AmbienteProducao == true)
                                consStatusProcessamentoReq.tpAmb = "1";
                            else
                                consStatusProcessamentoReq.tpAmb = "2";

                            RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
                            String retornoConsulta = NSSuite.consultarStatusProcessamento(nfe.Modelo, consStatusProcessamentoReq);
                            if (retornoConsulta != null)
                                retConsulta = JsonConvert.DeserializeObject<RetConsultaProcessamentoNF>(retornoConsulta);
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                                nfe.Chave = retConsulta.chNFe;
                            if (!String.IsNullOrEmpty(retConsulta.xMotivo))
                                nfe.Status = retConsulta.xMotivo;

                            if (retConsulta.xMotivo != null)
                            {
                                if (retConsulta.xMotivo.Contains("Autorizado o uso"))
                                {
                                    NfeStatusController nfeStatusController = new NfeStatusController();
                                    nfe.Protocolo = retConsulta.nProt;
                                    nfe.Chave = retConsulta.chNFe;
                                    nfe.CodStatus = retConsulta.cStat;
                                    nfe.Status = retConsulta.xMotivo;
                                    nfe.Lancada = true;
                                    nfe.DataLancamento = retConsulta.dhRecbto;
                                    nfe.DhSaiEnt = retConsulta.dhRecbto.ToString();
                                    //Atualizar o estoque se for autorizada
                                    if (nfe.NfeStatus.Id != 1)
                                    {
                                        NfeProdutoController nfeProdutoController = new NfeProdutoController();
                                        IList<NfeProduto> listaProd = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                                        foreach (NfeProduto nfeProd in listaProd)
                                        {
                                            if (nfe.TipoOperacao == "S")
                                                generica.atualizarEstoqueConciliado(nfeProd.Produto, double.Parse(nfeProd.QCom), false, "CONSULTA_CONTROLE", "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NaturezaOperacao, nfe.Cliente, DateTime.Now, null);
                                            else if (nfe.TipoOperacao == "E")
                                                generica.atualizarEstoqueConciliado(nfeProd.Produto, double.Parse(nfeProd.QCom), true, "CONSULTA_CONTROLE", "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NaturezaOperacao, nfe.Cliente, DateTime.Now, null);

                                            //Estoque auxiliar deve ser validado na origem..
                                            //generica.atualizarEstoqueNaoConciliado(nfeProd.Produto, double.Parse(nfeProd.QCom), false, "CONSULTA_CONTROLE", "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NaturezaOperacao);
                                        }
                                    }
                                    NfeStatus nfeStatus = new NfeStatus();
                                    nfeStatus.Id = 1;
                                    nfe.NfeStatus = (NfeStatus)nfeStatusController.selecionar(nfeStatus);

                                    if (nfe.Modelo == "55")
                                    {
                                        NFeDownloadProc55 nota = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                        var nfeRet = Genericos.LoadFromXMLString<TNfeProc>(nota.xml);
                                        genericosNF.gravarXMLNoBanco(nfeRet, 0, "S", nfe.Id);
                                        Controller.getInstance().salvar(nfe);
                                        generica.gravarXMLNaPasta(retConsulta.xml, retConsulta.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");

                                        //EnviaXML PAINEL LUNAR 
                                        string caminhoNota = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                        string retorno = lunarApiNotas.consultaNotaApi(nfe.CnpjEmitente, nfe.Chave);
                                        if (retorno.Contains("NENHUMA_NOTA_LOCALIZADA"))
                                        {
                                            byte[] arquivo;
                                            using (var stream = new FileStream(caminhoNota, FileMode.Open, FileAccess.Read))
                                            {
                                                using (var reader = new BinaryReader(stream))
                                                {
                                                    arquivo = reader.ReadBytes((int)stream.Length);
                                                    var ret = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                }
                                            }
                                        }
                                    }
                                    GenericaDesktop.ShowInfo(retConsulta.xMotivo);
                                }
                                else if (retConsulta.xMotivo.Contains("Duplicidade de NF-e, com diferenca na Chave de Acesso"))
                                {
                                    try { nfe.Chave = retConsulta.xMotivo.Substring(71, 44); } catch { }
                                    nfe.NsNrec = "";
                                    Controller.getInstance().salvar(nfe);
                                    if (nfe.Modelo.Equals("55"))
                                    {
                                        NFeDownloadProc55 nota = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                        if (nota != null)
                                        {
                                            var nfeRet = Genericos.LoadFromXMLString<TNfeProc>(nota.xml);
                                            genericosNF.gravarXMLNoBanco(nfeRet, 0, "S", nfe.Id);
                                            generica.gravarXMLNaPasta(nota.xml, nota.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");
                                            btnImprimirNf.PerformClick();
                                        }
                                    }
                                }
                                else
                                {
                                    GenericaDesktop.ShowAlerta(retConsulta.xMotivo);
                                }
                            }
                            else
                            {
                                Controller.getInstance().salvar(nfe);
                                GenericaDesktop.ShowAlerta(retConsulta.motivo);
                            }
                        }
                        else if (!String.IsNullOrEmpty(nfe.Chave) && nfe.Modelo.Equals("65"))
                        {
                            NFCeDownloadProc nFCeDownloadProc = generica.NS_ConsultaStatusNota65(nfe.Chave);
                            if (nFCeDownloadProc.nfeProc.xMotivo != null)
                            {
                                if (nFCeDownloadProc.nfeProc.xMotivo.Contains("Autorizado o uso da"))
                                {
                                    //Atualizar o estoque se for autorizada
                                    if (nfe.NfeStatus.Id != 1)
                                    {
                                        NfeProdutoController nfeProdutoController = new NfeProdutoController();
                                        IList<NfeProduto> listaProd = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
                                        foreach (NfeProduto nfeProd in listaProd)
                                        {
                                            generica.atualizarEstoqueConciliado(nfeProd.Produto, double.Parse(nfeProd.QCom), false, "CONSULTA_CONTROLE", "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NaturezaOperacao, nfe.Cliente, DateTime.Now, null);
                                        }
                                    }

                                    modificarNotaParaAutorizado(nFCeDownloadProc, nfe);
                                    //EnviaXML PAINEL LUNAR 
                                    string caminhoNota = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                                    LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                    string retorno = lunarApiNotas.consultaNotaApi(nfe.CnpjEmitente, nfe.Chave);
                                    if (retorno.Contains("NENHUMA_NOTA_LOCALIZADA"))
                                    {
                                        byte[] arquivo;
                                        using (var stream = new FileStream(caminhoNota, FileMode.Open, FileAccess.Read))
                                        {
                                            using (var reader = new BinaryReader(stream))
                                            {
                                                arquivo = reader.ReadBytes((int)stream.Length);
                                                var ret = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                            }
                                        }
                                    }
                                    GenericaDesktop.ShowInfo(nFCeDownloadProc.nfeProc.xMotivo);
                                }
                            }
                        }
                        else if (!String.IsNullOrEmpty(nfe.Chave) && nfe.Modelo.Equals("55"))
                        {
                            string caminhoSalvarXml = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                            RetNota55 nfeStatuss = generica.NS_ConsultaStatusNota55(nfe.Chave);
                            if (nfeStatuss != null)
                            {
                                if (nfeStatuss.motivo != null)
                                {
                                    if(nfeStatuss.retConsSitNFe.xMotivo != null)
                                        GenericaDesktop.ShowInfo(nfeStatuss.retConsSitNFe.xMotivo);
                                    if (nfeStatuss.retConsSitNFe.xMotivo.Contains("Autorizado o uso da"))
                                    {
                                        NfeStatus nfeStatus = new NfeStatus();
                                        nfeStatus.Id = 1;
                                        nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                                        nfe.Status = nfeStatuss.retConsSitNFe.xMotivo;
                                        nfe.CodStatus = nfeStatuss.retConsSitNFe.cStat;
                                        nfe.DataLancamento = DateTime.Parse(nfeStatuss.retConsSitNFe.protNFe[0].infProt.dhRecbto.ToShortDateString());
                                        if (!String.IsNullOrEmpty(nfeStatuss.retConsSitNFe.protNFe[0].infProt.chNFe))
                                        {
                                            nfe.Protocolo = nfeStatuss.retConsSitNFe.protNFe[0].infProt.nProt;
                                            nfe.Chave = nfeStatuss.retConsSitNFe.protNFe[0].infProt.chNFe;
                                        }
                                        Controller.getInstance().salvar(nfe);
                                        armazenaXmlAutorizadoNoBanco();
                                        //ATUALIZAR ESTOQUE
                                        NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                                        if (radioSaida.Checked == true)
                                        {
                                            IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                                            foreach (NfeProduto nfeP in listaProd)
                                            {
                                                generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NatOp, nfe.Cliente, DateTime.Now, null);
                                                generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NatOp, nfe.Cliente, DateTime.Now, null);
                                            }
                                        }
                                        else
                                        {
                                            IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                                            foreach (NfeProduto nfeP in listaProd)
                                            {
                                                generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NatOp, nfe.Cliente, DateTime.Now, null);
                                                generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + nfe.NatOp, nfe.Cliente, DateTime.Now, null);

                                            }
                                        }

                                        // GenericaDesktop.ShowInfo("Nota autorizada!");
                                        //EnviaXML PAINEL LUNAR 
                                        try
                                        {

                                            LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                            byte[] arquivo;
                                            using (var stream = new FileStream(caminhoSalvarXml + nfe.Chave + "-procNFe.xml", FileMode.Open, FileAccess.Read))
                                            {
                                                using (var reader = new BinaryReader(stream))
                                                {
                                                    arquivo = reader.ReadBytes((int)stream.Length);
                                                    var ret = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                                    if (ret.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
                                                    {
                                                        nfe.Nuvem = true;
                                                        Controller.getInstance().salvar(nfe);
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }

                                        if (File.Exists(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf"))
                                        {
                                            FrmPDF frmPDF = new FrmPDF(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf");
                                            frmPDF.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                        else if (nfe.NfeStatus != null)
                        {
                            if (nfe.NfeStatus.Id != 1)
                                GenericaDesktop.ShowAlerta("Tente reenviar a nota para sefaz");

                            else if (nfe.NfeStatus.Id == 1)
                                GenericaDesktop.ShowInfo("Nota Fiscal Autorizada");
                            pesquisaNotas();
                        }
                        }
                        else if (nfe.Status.Contains("Inutilizacao"))
                        {
                            if (nfe.Modelo == "55")
                            {
                                var retorno = generica.ns_DownloadEventoInutilizacaoNFE(nfe);
                                if (retorno.status == 200)
                                {
                                    if (GenericaDesktop.ShowConfirmacao("Nota Inutilizada, deseja salvar o arquivo xml?"))
                                    {
                                        generica.gravarXMLNaPasta(retorno.retInut.xml,
                                        nfe.Chave, @"Fiscal\XML\NFe\" + retorno.retInut.dhRecbto.Year + "-" + retorno.retInut.dhRecbto.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\", nfe.NNf + @"-INU.xml");

                                        //EnviaXML PAINEL LUNAR 
                                        string caminhoX = @"Fiscal\XML\NFe\" + retorno.retInut.dhRecbto.Year + "-" + retorno.retInut.dhRecbto.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.NNf + @"-INU.xml";
                                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                                        byte[] arquivo;
                                        using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                        {
                                            using (var reader = new BinaryReader(stream))
                                            {
                                                arquivo = reader.ReadBytes((int)stream.Length);
                                                var retor = lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, "NFE", "INUTILIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                            }
                                        }
                                        GenericaDesktop.ShowInfo("XML Salvo em C:\\Lunar\\" + caminhoX);
                                    }
                                }
                            }
                        }
                        else
                            GenericaDesktop.ShowAlerta("Status Atual: " + nfe.Status);
                }
                catch (Exception erro)
                {
                    GenericaDesktop.ShowErro(erro.Message);
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione uma nota com o mouse e clique novamente em consultar!");
            }
        }

        private void armazenaXmlAutorizadoNoBanco()
        {
            NFeDownloadProc55 nota55 = new NFeDownloadProc55();
            nota55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

            if (nota55.motivo.Contains("SUCESSO") || nota55.motivo.Contains("sucesso") || nota55.motivo.Contains("Sucesso"))
            {
                Genericos genericosNF = new Genericos();
                var notaLida55 = Genericos.LoadFromXMLString<TNfeProc>(nota55.xml);
                string es = "S";
                if (radioEntrada.Checked == true)
                    es = "E";
                genericosNF.gravarXMLNoBanco(notaLida55, 0, es, this.nfe.Id);
            }
        }
        private void btnCartaCorrecao_Click(object sender, EventArgs e)
        {
            NfeCce nfeCce = new NfeCce();
            Form formBackground = new Form();
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                //CONSULTA NOTA
                nfe = (Nfe)grid.SelectedItem;
                if (nfe.Modelo.Equals("55") && nfe.NfeStatus.Id == 1)
                {
                    using (FrmCartaCorrecao uu = new FrmCartaCorrecao(nfe))
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
                        switch (uu.showModal(ref nfeCce))
                        {
                            case DialogResult.Ignore:
                                formBackground.Dispose();
                                break;
                            case DialogResult.OK:
                                break;
                        }

                        formBackground.Dispose();
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("É possível gerar carta de correção apenas de NFe modelo 55 e a nota deve ta autorizada!");
            }
        }


        private void btnImprimirCCe_Click(object sender, EventArgs e)
        {
            if(File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf"))
            {
                //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf");
                FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf");
                frmPDF.ShowDialog();
            }
            else
            {
                NfeCceController nfeCceController = new NfeCceController();
                IList<NfeCce> listaCCe = nfeCceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
                int sequencia = 1;
                if (listaCCe.Count > 1)
                    sequencia = listaCCe.Count;
                RetornoCancelamento retornoDownload = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, false, true, sequencia.ToString());
                if (retornoDownload.pdf != null)
                {
                    generica.gerarPDF2(retornoDownload.pdf, nfe.Chave, true, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-CCe");
                }
            }
        }


        private void grid_CellButtonClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellButtonClickEventArgs e)
        {
           // int index = grid.TableControl.ResolveToRecordIndex(grid.CurrentCell.RowIndex);
           // MessageBox.Show("Clicou no index " + index);
            nfe = (Nfe)grid.SelectedItem;
            if (nfe.Modelo.Equals("55"))
            {
                //NfeCceController nfecceController = new NfeCceController();
                //IList<NfeCce> listaCCe = nfecceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
                if (nfe.PossuiCartaCorrecao == true)
                {
                    if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf");
                        frmPDF.ShowDialog();
                        //System.Diagnostics.Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf");                       
                    }
                    else
                    {
                        NfeCceController nfeCceController = new NfeCceController();
                        IList<NfeCce> listaCCe = nfeCceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
                        int sequencia = 1;
                        if (listaCCe.Count > 1)
                            sequencia = listaCCe.Count;

                        RetornoCancelamento retornoDownload = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, false, true, sequencia.ToString());
                        if (retornoDownload.pdf != null)
                        {
                            generica.gerarPDF2(retornoDownload.pdf, nfe.Chave, true, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-CCe");
                        }
                        else
                        {
                            //Buscar em nosso servidor online
                            GenericaDesktop.ShowAlerta("Impressão não localizada");
                        }
                    }
                }
            }
        }


        private void btnImprimirNf_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.RowCount > 0 && grid.SelectedItem != null)
                {
                    nfe = (Nfe)grid.SelectedItem;
                    
                    if (nfe.Modelo.Equals("65"))
                    {
                        if (File.Exists(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf"))
                        {
                            //Process.Start(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf");
                            FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf");
                            frmPDF.ShowDialog();
                        }
                        else
                        {
                            if (nfe.Modelo.Equals("65"))
                            {
                                NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();
                                nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                if (nFCeDownloadProc != null)
                                {
                                    gerarPDF2(nFCeDownloadProc.pdf, nfe.Chave, true);
                                }
                            }
                        }
                        if (nfe.NfeStatus.Id == 6)
                        {
                            if (Directory.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs"))
                            {
                                string caminhoPDF = Sessao.parametroSistema.PastaRemessaNsCloud + @"\PDFs\" + Sessao.empresaFilialLogada.Cnpj.Trim() + @"\" + nfe.DataCadastro.Year + @"\" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\" + nfe.DataCadastro.Day.ToString().PadLeft(2, '0') + @"\" + nfe.Chave + ".pdf";
                                if (File.Exists(caminhoPDF))
                                {
                                    //System.Diagnostics.Process.Start(caminhoPDF);
                                    FrmPDF frmPDF = new FrmPDF(caminhoPDF);
                                    frmPDF.ShowDialog();
                                }
                            }
                        }
         
                    }
                    if (nfe.Modelo.Equals("55"))
                    {
                        if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                        {
                            FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                            frmPDF.ShowDialog();
                            //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                        }
                        else
                        {
                            NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                            nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                            if (nFeDownloadProc55 != null)
                            {
                                gerarPDF2(nFeDownloadProc55.pdf, nfe.Chave, true);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta("Falha ao imprimir nota. " + erro.Message);
            }
        }

        private void radioEntrada_CheckChanged(object sender, EventArgs e)
        {
            if (radioEntrada.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioSaida_CheckChanged(object sender, EventArgs e)
        {
            if (radioSaida.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void radioEntradaSaidaTodas_CheckChanged(object sender, EventArgs e)
        {
            if (radioEntradaSaidaTodas.Checked == true)
            {
                pesquisaNotas();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void iconEmail_Click(object sender, EventArgs e)
        {
            dispararEmail();
        }

        private void dispararEmail()
        {
            int sucesso = 0;
            int erro = 0;
            List<String> listaEmail = new List<string>();
            List<String> listaAnexo = new List<string>();
            string email = "";
            if (grid.RowCount > 0 && grid.SelectedItem != null)
            {
                nfe = (Nfe)grid.SelectedItem;

                Form formBackground = new Form();
                using (FrmJustificativa uu = new FrmJustificativa("Email NFe", "Digite um ou mais emails separados por ; (ponto e virgula)", nfe))
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
                    switch (uu.showModal(ref email))
                    {
                        case DialogResult.Ignore:
                            formBackground.Dispose();
                            break;
                        case DialogResult.OK:
                            string ultCaract = email.Substring(email.Length - 1, 1);
                            if (ultCaract.Equals(";"))
                                email = email.Substring(0,email.Length - 1);
                            
                            listaEmail = email.Split(';').ToList();
                            break;
                    }

                    formBackground.Dispose();
                }
                //generica.NS_EnviarEmailNF(nfe.Chave, listaEmail);

                if (nfe.NfeStatus.Id != 2 && nfe.NfeStatus.Id != 3)
                {
                    NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                    NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();

                    if (listaEmail != null)
                    {
                        if (nfe.Modelo.Equals("65"))
                        {
                            if (nfe.NfeStatus.Id == 1)
                            {
                                nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);

                                for(int i = 0; i < listaEmail.Count; i++)
                                {
                                    string caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                                    if (!File.Exists(caminhoXML)) 
                                    {
                                        generica.gravarXMLNaPasta(nFCeDownloadProc.nfeProc.xml, nfe.Chave, @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFCe.xml");
                                    }
                                    listaAnexo.Add(caminhoXML);
                                    gerarPDF2(nFCeDownloadProc.pdf, nfe.Chave, false);
                                    string caminhoPDF = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.pdf";
                                    listaAnexo.Add(caminhoPDF);

                                    bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal " + nfe.NNf + " " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf, "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                    if (enviado == true)
                                        sucesso++;
                                    if (enviado == false)
                                        erro++;
                                }
                                if(erro == 0)
                                    GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                else if(erro > 0)
                                    GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n"+erro + " Email com falha no envio!");
                            }
                            //cancelada
                            if (nfe.NfeStatus.Id == 4)
                            {
                                var nfceCancelada = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.Chave);
                                if (nfceCancelada != null)
                                {
                                    for (int i = 0; i < listaEmail.Count; i++)
                                    {
                                        string caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\";
                                        string nomeArquivo = nfe.Chave + @"-CAN.xml";
                                        if (!File.Exists(caminhoXML + nomeArquivo))
                                        {
                                            generica.gravarXMLNaPasta(nfceCancelada.retEvento.xml, nfe.Chave, caminhoXML, nomeArquivo);
                                        }
                                        listaAnexo.Add(caminhoXML + nomeArquivo);
                                        if (nfceCancelada.pdfCancelamento != null)
                                        {
                                            gerarPDF2Canceladas(nfceCancelada.pdfCancelamento, nfe.Chave, false);
                                            string caminhoPDF = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.pdf";
                                            listaAnexo.Add(caminhoPDF);
                                        }

                                        bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal Cancelada " + nfe.NNf + " " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf + " cancelada ", "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                        if (enviado == true)
                                            sucesso++;
                                        if (enviado == false)
                                            erro++;
                                    }
                                    if (erro == 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                    else if (erro > 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n" + erro + " Email com falha no envio!");
                                }
                            }
                            //INUTILIZADA
                            if (nfe.NfeStatus.Id == 5)
                            {
                                var nfceInut = generica.NS_DownloadNFCeInutilizada(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.IdInut);
                                if (nfceInut != null)
                                {
                                    for (int i = 0; i < listaEmail.Count; i++)
                                    {
                                        string caminhoXML = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\";
                                        string nomeArquivo = nfe.NNf + @"-INU.xml";
                                        if (!File.Exists(caminhoXML + nomeArquivo))
                                        {
                                            generica.gravarXMLNaPasta(nfceInut.retInut.xml, nfe.NNf, caminhoXML, nomeArquivo);
                                        }
                                        listaAnexo.Add(caminhoXML + nomeArquivo);

                                        bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal Inutilizada " + nfe.NNf + " " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf + " inutilizada ", "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                        if (enviado == true)
                                            sucesso++;
                                        if (enviado == false)
                                            erro++;
                                    }
                                    if (erro == 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                    else if (erro > 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n" + erro + " Email com falha no envio!");
                                }
                            }
                        }
                        if (nfe.Modelo.Equals("55"))
                        {
                            if (nfe.NfeStatus.Id == 1)
                            {
                                nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                if (nfe.PossuiCartaCorrecao == true)
                                {
                                    NfeCceController nfeCceController = new NfeCceController();
                                    IList<NfeCce> listaCCe = nfeCceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
                                    int sequencia = 1;
                                    if (listaCCe.Count > 1)
                                        sequencia = listaCCe.Count;
                                    var nfeCce = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, false, true, sequencia.ToString());
                                    gerarPDF2CCe(nfeCce.pdf, nfe.Chave, false);
                                    listaAnexo.Add(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\CartaCorrecao\" + nfe.Chave + "-CCE.pdf");
                                }

                                for (int i = 0; i < listaEmail.Count; i++)
                                {
                                    string caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                                    if (!File.Exists(caminhoXML))
                                    {
                                        generica.gravarXMLNaPasta(nFeDownloadProc55.xml, nfe.Chave, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");
                                    }
                                    listaAnexo.Add(caminhoXML);
                                    gerarPDF2(nFeDownloadProc55.pdf, nfe.Chave, false);
                                    string caminhoPDF = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf";
                                    listaAnexo.Add(caminhoPDF);

                                    bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal " + nfe.NNf + " " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf, "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                    if (enviado == true)
                                        sucesso++;
                                    if (enviado == false)
                                        erro++;
                                }
                                if (erro == 0)
                                    GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                else if (erro > 0)
                                    GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n" + erro + " Email com falha no envio!");

                            }
                            //cancelada
                            if (nfe.NfeStatus.Id == 4)
                            {
                                var nfceCancelada = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, true, false, "");

                                if (nfceCancelada != null)
                                {
                                    for (int i = 0; i < listaEmail.Count; i++)
                                    {
                                        string caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.xml";
                                        if (!File.Exists(caminhoXML))
                                        {
                                            generica.gravarXMLNaPasta(nfceCancelada.xml, nfe.Chave, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\", nfe.Chave + "-CAN.xml");
                                        }
                                        listaAnexo.Add(caminhoXML);

                                        gerarPDF2Canceladas(nfceCancelada.pdf, nfe.Chave, false);
                                        listaAnexo.Add(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.pdf");

                                        bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal " + nfe.NNf + " Cancelada " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf + " cancelada", "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                        if (enviado == true)
                                            sucesso++;
                                        if (enviado == false)
                                            erro++;
                                    }
                                    if (erro == 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                    else if (erro > 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n" + erro + " Email com falha no envio!");
                                }
                            }
                            //INUTILIZADA
                            if (nfe.NfeStatus.Id == 5)
                            {
                                var nfeInut = generica.ns_DownloadEventoInutilizacaoNFE(nfe);
                                if (nfeInut != null)
                                {
                                    for (int i = 0; i < listaEmail.Count; i++)
                                    {
                                        string caminhoXML = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.Chave + "-INU.xml";
                                        if (!File.Exists(caminhoXML))
                                        {
                                            generica.gravarXMLNaPasta(nfeInut.retInut.xml, nfe.Chave, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\", nfe.Chave + "-INU.xml");
                                        }
                                        listaAnexo.Add(caminhoXML);

                                        bool enviado = generica.enviarEmail(listaEmail[i], "Nota Fiscal " + nfe.NNf + " Inutilizada " + Sessao.empresaFilialLogada.NomeFantasia, "Olá, segue em anexo nota fiscal " + nfe.NNf + " inutilizada", "E-mail enviado automaticamente pelo sistema, caso não tenha feito esta solicitação favor desconsiderar.", listaAnexo);
                                        if (enviado == true)
                                            sucesso++;
                                        if (enviado == false)
                                            erro++;
                                    }
                                    //if (erro == 0)
                                    //    GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!");
                                    //else
                                    if (erro > 0)
                                        GenericaDesktop.ShowInfo(sucesso + " Email(s) enviado(s) com sucesso!\n" + erro + " Email com falha no envio!");
                                }
                            }
                        }
                    }
                }

            }
        }

        private void btnEnviarWhats_Click(object sender, EventArgs e)
        {
            string numero = "";
            nfe = (Nfe)grid.SelectedItem;
            if(nfe.NfeStatus.Id != 2 && nfe.NfeStatus.Id != 3) { 
            NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
            NFCeDownloadProc nFCeDownloadProc = new NFCeDownloadProc();
            
            Form formBackground = new Form();
            using (FrmJustificativa uu = new FrmJustificativa("Número de WhatsApp", "DDD + Numero (Ex. 11988884455)", nfe))
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
                switch (uu.showModal(ref numero))
                {
                    case DialogResult.Ignore:
                        formBackground.Dispose();
                        break;
                    case DialogResult.OK:
                        string primeiroCaract = numero.Substring(0, 2);
                        if (!primeiroCaract.Equals("55"))
                            numero = "55" + GenericaDesktop.RemoveCaracteres(numero.Trim());
                        break;
                }

                formBackground.Dispose();
            }
                if (!String.IsNullOrEmpty(numero))
                {
                    Zapi zapi = new Zapi();
                    if (nfe.Modelo.Equals("65"))
                    {
                        if (nfe.NfeStatus.Id == 1)
                        {
                            nFCeDownloadProc = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                

                            dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, segue Nota Fiscal da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                            dynamic ret = zapi.zapi_EnviarDocumento(numero, nFCeDownloadProc.pdf, Sessao.empresaFilialLogada.NomeFantasia + ": PDF NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "PDF");
                            dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nFCeDownloadProc.nfeProc.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                            if (!String.IsNullOrEmpty(ret) && !String.IsNullOrEmpty(ret2))
                                GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                        }
                        //cancelada
                        if (nfe.NfeStatus.Id == 4)
                        {
                            var nfceCancelada = generica.ConsultaNFCeEmitida(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.Chave);
                            if (nfceCancelada != null)
                            {
                                dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, cancelamento da Nota Fiscal " + nfe.NNf + " da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nfceCancelada.nfeProc.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML CANCELAMENTO NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                                if (!String.IsNullOrEmpty(ret0) && !String.IsNullOrEmpty(ret2))
                                    GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                            }
                        }
                        //INUTILIZADA
                        if (nfe.NfeStatus.Id == 5)
                        {
                            var nfceInut = generica.NS_DownloadNFCeInutilizada(Sessao.empresaFilialLogada.Cnpj.Trim(), nfe.IdInut);
                            if (nfceInut != null)
                            {
                                dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, segue inutilização da Nota Fiscal " + nfe.NNf + " da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nfceInut.retInut.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML INUT NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                                if (!String.IsNullOrEmpty(ret0) && !String.IsNullOrEmpty(ret2))
                                    GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                            }
                        }
                    }
                    if (nfe.Modelo.Equals("55"))
                    {
                        if (nfe.NfeStatus.Id == 1)
                        {
                            nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                            dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, segue Nota Fiscal da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                            dynamic ret = zapi.zapi_EnviarDocumento(numero, nFeDownloadProc55.pdf, Sessao.empresaFilialLogada.NomeFantasia + ": PDF NFe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "PDF");
                            dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nFeDownloadProc55.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML NFe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                            if(nfe.PossuiCartaCorrecao == true)
                            {
                                NfeCceController nfeCceController = new NfeCceController();
                                IList<NfeCce> listaCCe = nfeCceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
                                int sequencia = 1;
                                if (listaCCe.Count > 1)
                                    sequencia = listaCCe.Count;
                                var nfeCce = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, false, true, sequencia.ToString());
                                zapi.zapi_EnviarTexto(numero, "Ahhh, esta Nota Fiscal possui uma carta de correção, segue:", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                zapi.zapi_EnviarDocumento(numero, nfeCce.pdf, Sessao.empresaFilialLogada.NomeFantasia + ": PDF Carta Correção NFe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "PDF");
                            }
                            
                            if (!String.IsNullOrEmpty(ret) && !String.IsNullOrEmpty(ret2))
                                GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                        }
                        //cancelada
                        if (nfe.NfeStatus.Id == 4)
                        {
                            var nfceCancelada = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, true, false, "");

                            if (nfceCancelada != null)
                            {
                                dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, cancelamento da Nota Fiscal " + nfe.NNf + " da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nfceCancelada.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML CANCELAMENTO NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                                if (!String.IsNullOrEmpty(ret0) && !String.IsNullOrEmpty(ret2))
                                    GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                            }
                        }
                        //INUTILIZADA
                        if (nfe.NfeStatus.Id == 5)
                        {
                            var nfeInut = generica.ns_DownloadEventoInutilizacaoNFE(nfe);
                            if (nfeInut != null)
                            {
                                dynamic ret0 = zapi.zapi_EnviarTexto(numero, "Olá, inutilização da Nota Fiscal " + nfe.NNf + " da empresa " + Sessao.empresaFilialLogada.NomeFantasia + "\n\n*Essa é uma mensagem automática, caso não tenha feito essa solicitação desconsidere a mensagem* ", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
                                dynamic ret2 = zapi.zapi_EnviarDocumento(numero, Zapi.EncodeToBase64(nfeInut.retInut.xml), Sessao.empresaFilialLogada.NomeFantasia + ": XML INUT NFCe " + nfe.NNf, Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats, "xml");
                                if (!String.IsNullOrEmpty(ret0) && !String.IsNullOrEmpty(ret2))
                                    GenericaDesktop.ShowInfo("Mensagem enviada com sucesso");
                            }
                        }
                    }
                }
                
            }
            else
            {
                GenericaDesktop.ShowAlerta("Atenção, essa nota nao está autorizada!");
            }
        }

        private void iconPastaArquivo_Click(object sender, EventArgs e)
        {
            nfe = new Nfe();
            nfe = (Nfe)grid.SelectedItem;
            if(nfe != null)
            {
                if(nfe.Id > 0)
                {
                    string caminho = "";
                    if (nfe.Modelo == "65")
                    {
                        if (nfe.NfeStatus.Id == 1)
                            caminho = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                        if (nfe.NfeStatus.Id == 4)
                            caminho = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.xml";
                        if (nfe.NfeStatus.Id == 5)
                            caminho = @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.NNf + "-INU.xml";
                    }
                    else if(nfe.Modelo == "55")
                    {
                        if (nfe.NfeStatus.Id == 1)
                            caminho = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                        if (nfe.NfeStatus.Id == 4)
                            caminho = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Canceladas\" + nfe.Chave + "-CAN.xml";
                        if (nfe.NfeStatus.Id == 5)
                            caminho = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.Chave + "-INU.xml";
                    }
                    if (!String.IsNullOrEmpty(caminho))
                    {
                        bool isfile = System.IO.File.Exists(caminho);
                        if (isfile)
                        {
                            string argument = @"/select, " + caminho;
                            System.Diagnostics.Process.Start("explorer.exe", argument);
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Arquivo não encontrado pelo sistema, faça uma busca manual na pasta de arquivos do sistema!");
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Arquivo não encontrado!");
                       
                    
                }
            }
        }

        private void btnConferirXmlNuvem_Click(object sender, EventArgs e)
        {
            GenericaDesktop genericaDesktop = new GenericaDesktop();
            IList<Nfe> listaNotas = new List<Nfe>();
            DateTime dataInicial = DateTime.Parse(txtDataInicial.Value.ToString());
            DateTime dataFinal = DateTime.Parse(txtDataFinal.Value.ToString());
            listaNotas = nfeController.selecionarNotasEmitidasPorPeriodo(dataInicial.ToString("yyyy'-'MM'-'dd' '00':'00':'00"), dataFinal.ToString("yyyy'-'MM'-'dd' '23':'59':'59"));
            if(listaNotas.Count > 0)
            {
                foreach(Nfe nfe in listaNotas)
                {
                    if (nfe.Modelo.Equals("65") && nfe.Status.Contains("Autorizado o uso"))
                    {
                        //Verifica se nota existe no painel Lunar
                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                        string retorno = lunarApiNotas.consultaNotaApi(nfe.CnpjEmitente, nfe.Chave);
                        if (retorno.Contains("NENHUMA_NOTA_LOCALIZADA"))
                        {
                            //Entra na API, faz o download do XML e joga no painel Lunar
                            NFCeDownloadProc nfceDownload = genericaDesktop.ConsultaNFCeEmitida(nfe.EmpresaFilial.Cnpj, nfe.Chave.Trim());
                            if (nfceDownload.motivo.Contains("realizado com sucesso"))
                            {
                                generica.gravarXMLNaPasta(nfceDownload.nfeProc.xml, nfe.Chave, @"Fiscal\XML\NFCe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFCe.xml");
                                //EnviaXML PAINEL LUNAR 
                                string caminhoX = @"Fiscal\XML\NFCe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFCe.xml";
                                byte[] arquivo;
                                using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
                                {
                                    using (var reader = new BinaryReader(stream))
                                    {
                                        arquivo = reader.ReadBytes((int)stream.Length);
                                        var retor = lunarApiNotas.EnvioNotaParaNuvem(Sessao.empresaFilialLogada.Cnpj, nfe.Chave, "NFCE", "AUTORIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                                    }
                                }
                            }
                        }
                    }
                }
                GenericaDesktop.ShowInfo("Notas Enviadas para Nuvem com Sucesso!");
            }
        }

        private void FrmControleNotas_Load(object sender, EventArgs e)
        {
            if (fazerPrimeiraBuscaNotas == true)
            {
                buscaNotas();
                fazerPrimeiraBuscaNotas = false;
            }
        }
    }
}
