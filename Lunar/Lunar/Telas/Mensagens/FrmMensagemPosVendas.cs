using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.ZAPZAP;
using MySql.Data.MySqlClient;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Lunar.Telas.Mensagens
{
    public partial class FrmMensagemPosVendas : Form
    {
        Logger logger = new Logger();
        MensagemPosVendaController mensagemPosVendaController = new MensagemPosVendaController();
        IList<MensagemPosVenda> listaMensagens = new List<MensagemPosVenda>();
        public FrmMensagemPosVendas()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
            carregarLista();
        }
        private void carregarLista()
        {
            DateTime dataIni = DateTime.Parse(txtDataInicial.Value.ToString());
            DateTime dataFin = DateTime.Parse(txtDataFinal.Value.ToString());
            String dataInicial = dataIni.ToString("yyyy'-'MM'-'dd' '00':'00':'00");
            String dataFinal = dataFin.ToString("yyyy'-'MM'-'dd' '23':'59':'59");

            listaMensagens = mensagemPosVendaController.selecionarTodasMensagensNaoEnviadasPorPeriodo(dataInicial, dataFinal);

            if (listaMensagens.Count > 0)
            {
                sfDataPager1.DataSource = listaMensagens;
                if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                    sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
                else
                    sfDataPager1.PageSize = 100;
                grid.DataSource = sfDataPager1.PagedSource;
                sfDataPager1.OnDemandLoading += sfDataPager1_OnDemandLoading;

                grid.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                this.grid.Columns["Pessoa.RazaoSocial"].AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                grid.AutoSizeController.Refresh();
            }
        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
            sfDataPager1.LoadDynamicData(e.StartRowIndex, listaMensagens.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var options = new PdfExportingOptions();
            var document = new Syncfusion.Pdf.PdfDocument();
            document.PageSettings.Orientation = Syncfusion.Pdf.PdfPageOrientation.Landscape;
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
                FileName = "ListaMensagens",
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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = grid.ExportToExcel(grid.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaMensagens",
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

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grid.SelectedIndex >= 0)
            {
                MensagemPosVenda mensagemPosVenda = new MensagemPosVenda();
                mensagemPosVenda = (MensagemPosVenda)grid.SelectedItem;
                if (mensagemPosVenda.FlagEnviada == false)
                {
                    if (GenericaDesktop.ShowConfirmacao("Deseja excluir esta mensagem? "))
                    {
                        Controller.getInstance().excluir(mensagemPosVenda);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso");
                        carregarLista();
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Não é possível excluir uma mensagem que tenha sido enviada!");
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha da mensagem que deseja excluir!");
        }

        private void txtDataFinal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    carregarLista();
                    break;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarLista();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string connectionStringLocal = "Server=localhost;Database=lunar;User ID=marcelo;Password=mx123;";
            if (Sessao.parametroSistema.AtivarMensagemPosVendas.Equals("True") || Sessao.parametroSistema.AtivarMensagemPosVendas == true)
            {
                MensagemPosVenda msgPos = new MensagemPosVenda();
                using (MySqlConnection connection = new MySqlConnection(connectionStringLocal))
                {
                    try
                    {
                        // Abra a conexão

                        connection.Open();
                        //string query = "SELECT MP.* FROM MensagemPosVenda MP INNER JOIN (SELECT Pessoa, MIN(DataAgendamento) AS MinDataAgendamento FROM MensagemPosVenda WHERE DATE(DataAgendamento) <= CURDATE() AND FlagEnviada = false GROUP BY Pessoa) AS PessoasUnicas ON MP.Pessoa = PessoasUnicas.Pessoa AND MP.DataAgendamento = PessoasUnicas.MinDataAgendamento INNER JOIN Pessoa ON MP.Pessoa = Pessoa.Id INNER JOIN PessoaTelefone ON Pessoa.ID = PessoaTelefone.PESSOA WHERE DATE(MP.DataAgendamento) <= CURDATE() AND MP.FlagEnviada = false AND PessoaTelefone.ddd IS NOT NULL AND PessoaTelefone.Telefone IS NOT NULL";
                        string query = "SELECT DISTINCT MP.* FROM MensagemPosVenda MP INNER JOIN (SELECT Pessoa, MIN(DataAgendamento) AS MinDataAgendamento FROM MensagemPosVenda WHERE DATE(DataAgendamento) <= CURDATE() AND FlagEnviada = false GROUP BY Pessoa) AS PessoasUnicas ON MP.Pessoa = PessoasUnicas.Pessoa AND MP.DataAgendamento = PessoasUnicas.MinDataAgendamento INNER JOIN Pessoa ON MP.Pessoa = Pessoa.Id INNER JOIN (SELECT DISTINCT PessoaTelefone.PESSOA, PessoaTelefone.ddd, PessoaTelefone.Telefone FROM PessoaTelefone WHERE PessoaTelefone.ddd IS NOT NULL AND PessoaTelefone.Telefone IS NOT NULL) AS PessoaTelefoneDistinct ON Pessoa.ID = PessoaTelefoneDistinct.PESSOA WHERE DATE(MP.DataAgendamento) <= CURDATE() AND MP.FlagEnviada = false";


                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                // Faça algo com os resultados
                                while (reader.Read())
                                {
                                    msgPos.Id = Convert.ToInt32(reader["Id"]);
                                    msgPos.DataAgendamento = DateTime.Parse($"{reader["DataAgendamento"]}");
                                    msgPos.NomeCliente = $"{reader["NomeCliente"]}";
                                    msgPos.Pessoa = new Pessoa();
                                    msgPos.Pessoa.Id = Convert.ToInt32(reader["Pessoa"]);
                                    msgPos.Pessoa = (Pessoa)Controller.getInstance().selecionar(msgPos.Pessoa);
                                    msgPos.FlagEnviada = false;
                                    if (String.IsNullOrEmpty(msgPos.NomeCliente))
                                        msgPos.NomeCliente = msgPos.Pessoa.RazaoSocial;
                                    Sessao.MensagensAgendadas.Add(msgPos);

                                    logger.WriteLog("Mensagens agendadas: " + Sessao.MensagensAgendadas.Count, "LogMensagem");

                                }

                            }
                        }
                        enviar();
                    }
                    catch (Exception erro)
                    {
                        logger.WriteLog("Mensagens agendadas ERRO: " + erro.Message, "LogMensagem");
                    }
                }
            }
        }

        private async void enviar()
        {
            String mensagemPosVenda = Sessao.parametroSistema.MensagemPosVendas;
            DateTime agora = DateTime.Now;
            if (Sessao.MensagensAgendadas.Count > 0)
            {
                logger.WriteLog("Sessao.MensagensAgendadas.Count > 0...OK ---- " + Sessao.MensagensAgendadas.Count, "LogMensagem");
                foreach (var mensagem in Sessao.MensagensAgendadas.ToList()) // Usar ToList para evitar exceção de modificação durante a iteração
                {
                    if (agora >= mensagem.DataAgendamento)
                    {
                        logger.WriteLog("Preparando Disparo de Mensagem ---- NOME: " + mensagem.NomeCliente + " MENSAGEM: " + mensagemPosVenda + " FLAGENVIADA: " + mensagem.FlagEnviada, "LogMensagem");
                        if (!String.IsNullOrEmpty(mensagem.NomeCliente) && !String.IsNullOrEmpty(mensagemPosVenda) && !mensagem.FlagEnviada)
                        {
                            try
                            {
                                //Zapi zapi = new Zapi();
                                LunarChatAPI lunarChatAPI = new LunarChatAPI();
                                if (mensagem.Pessoa.PessoaTelefone != null)
                                {
                                    if (ValidarNumeroTelefone(mensagem.Pessoa.PessoaTelefone.Ddd + mensagem.Pessoa.PessoaTelefone.Telefone))
                                    {
                                        if (mensagem.Pessoa.PessoaTelefone.Telefone.Length == 8)
                                            mensagem.Pessoa.PessoaTelefone.Telefone = "9" + mensagem.Pessoa.PessoaTelefone.Telefone;
                                        string numeroLimpo = RemoverCaracteresEspeciais("55" + mensagem.Pessoa.PessoaTelefone.Ddd + mensagem.Pessoa.PessoaTelefone.Telefone);
                                        //Nome e primeiro sobrenome
                                        string[] partesNome = mensagem.Pessoa.RazaoSocial.Split(' ');
                                        if (partesNome.Length >= 2)
                                            mensagem.Pessoa.RazaoSocial = partesNome[0] + " " + partesNome[1];

                                        String mensagemAjustada = mensagemPosVenda;
                                        if (mensagemAjustada.Contains("[NomeCliente]"))
                                            mensagemAjustada = mensagemAjustada.Replace("[NomeCliente]", mensagem.Pessoa.RazaoSocial);


                                        var ret = await lunarChatAPI.SendMessageAsync(numeroLimpo, mensagemAjustada);
                                        if (ret != null)
                                        {
                                            //AtualizarFlagMensagemEnviada(int.Parse(idOrdemServico));
                                            string nomeArquivo = $"Whatsapp_PosVenda_{DateTime.Now:yyyyMMdd_HHmm}.txt";
                                            if (!Directory.Exists("Logs"))
                                            {
                                                Directory.CreateDirectory("Logs");
                                            }
                                            string caminhoArquivo = Path.Combine("Logs", nomeArquivo);
                                            string conteudoLog = $"Data e Hora: {DateTime.Now}\nNúmero de Telefone: {numeroLimpo}\nTexto da Mensagem: {mensagemAjustada}\n\n";
                                            // Escrever o conteúdo no arquivo (append para adicionar ao conteúdo existente)
                                            File.AppendAllText(caminhoArquivo, conteudoLog);
                                        }
                                    }
                                }
                                //dispararMensagemPosVenda(mensagem.NomeCliente, mensagemPosVenda, mensagem.Pessoa);
                                mensagem.FlagEnviada = true;
                                mensagem.DataAlteracao = DateTime.Now;
                                Controller.getInstance().salvar(mensagem);
                            }
                            catch
                            {
                                mensagem.FlagEnviada = true;
                                Controller.getInstance().salvar(mensagem);
                            }
                        }
                    }
                }
                Sessao.MensagensAgendadas.Clear();
            }
        }

        private async void excluirMensagensAnteriores()
        {
            MensagemPosVendaController mensagemPosVendaController = new MensagemPosVendaController();
            string data1 = DateTime.Now.AddYears(-5).ToString("yyyy/MM/dd");
            string data2 = DateTime.Now.ToString("yyyy/MM/dd");
            IList<MensagemPosVenda> lista = mensagemPosVendaController.selecionarTodasMensagensNaoEnviadasPorPeriodo(data1, data2);
        }
        static string RemoverCaracteresEspeciais(string input)
        {
            // Remover caracteres especiais usando expressão regular
            return Regex.Replace(input, @"[^\d]", "");
        }
        static bool ValidarNumeroTelefone(string numero)
        {
            // Expressão regular para validar DDD + número de telefone celular com pelo menos 10 dígitos
            string padrao = @"^\(?([1-9]{2})\)?[-.\s]?[6-9]\d{3,4}[-.\s]?\d{4}$";

            // Verificar se o número atende ao padrão
            return Regex.IsMatch(numero, padrao);
        }
    }
}
