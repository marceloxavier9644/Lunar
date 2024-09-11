using Lunar.Telas.OrdensDeServico;
using Lunar.Utils;
using Lunar.WSCorreios;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.SharePoint.Client.Utilities;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lunar.Utils.LunarChatIntegracao.LunarChatMensagem;
using Exception = System.Exception;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmAniversariantes : Form
    {
        public FrmAniversariantes()
        {
            InitializeComponent();
            txtDataInicial.Value = DateTime.Now;
            txtDataFinal.Value = DateTime.Now;
            pesquisarClientesPorDataNascimento();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            pesquisarClientesPorDataNascimento();
        }

        private void pesquisarClientesPorDataNascimento()
        {

            PessoaController pessoaController = new PessoaController();
            DateTime dataInicial = DateTime.Parse(txtDataInicial.Value.ToString());
            DateTime dataFinal = DateTime.Parse(txtDataFinal.Value.ToString());

            // Captura o dia e o mês da data inicial e final
            int diaInicial = dataInicial.Day;
            int mesInicial = dataInicial.Month;
            int diaFinal = dataFinal.Day;
            int mesFinal = dataFinal.Month;

            IList<Pessoa> listaPessoa = new List<Pessoa>();

            // Caso o intervalo seja no mesmo mês
            if (mesInicial == mesFinal)
            {
                // Busca os aniversariantes dentro do mesmo mês e intervalo de dias
                string sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                             "AND Tabela.Cliente = true " +
                             "AND MONTH(Tabela.DataNascimento) = " + mesInicial + " " +
                             "AND DAY(Tabela.DataNascimento) BETWEEN " + diaInicial + " AND " + diaFinal + " " +
                             "ORDER BY Tabela.RazaoSocial";

                // Executa a consulta com SQL para aniversariantes no intervalo
                listaPessoa = pessoaController.selecionarClientesPorDataAniversarioSql(sql);
            }
            else
            {
                // Intervalo abrange mais de um mês, então iteramos sobre os meses
                for (int mes = mesInicial; mes <= mesFinal; mes++)
                {
                    string sql = "";

                    // Para o primeiro mês, limitar pelos dias a partir da dataInicial
                    if (mes == mesInicial)
                    {
                        sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                              "AND Tabela.Cliente = true " +
                              "AND MONTH(Tabela.DataNascimento) = " + mes + " " +
                              "AND DAY(Tabela.DataNascimento) >= " + diaInicial + " " +
                              "ORDER BY Tabela.RazaoSocial";
                    }
                    // Para o último mês, limitar pelos dias até a dataFinal
                    else if (mes == mesFinal)
                    {
                        sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                              "AND Tabela.Cliente = true " +
                              "AND MONTH(Tabela.DataNascimento) = " + mes + " " +
                              "AND DAY(Tabela.DataNascimento) <= " + diaFinal + " " +
                              "ORDER BY Tabela.RazaoSocial";
                    }
                    // Para os meses intermediários, buscar todos os aniversários
                    else
                    {
                        sql = "FROM Pessoa as Tabela WHERE Tabela.FlagExcluido <> true " +
                              "AND Tabela.Cliente = true " +
                              "AND MONTH(Tabela.DataNascimento) = " + mes + " " +
                              "ORDER BY Tabela.RazaoSocial";
                    }

                    // Executa a consulta para cada mês e agrega os resultados na lista final
                    var pessoasNoMes = pessoaController.selecionarClientesPorDataAniversarioSql(sql);
                    listaPessoa = listaPessoa.Concat(pessoasNoMes).ToList();
                }
            }
            gridClient.DataSource = listaPessoa;
            gridClient.Refresh();
        }

        private async void btnEnviarWhats_Click(object sender, EventArgs e)
        {
            string telefoneCompleto = "";
            string nomeCliente = "";
            if (!String.IsNullOrEmpty(Sessao.parametroSistema.TokenWhats))
            {
                if (gridClient.SelectedIndex >= 0)
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa = (Pessoa)gridClient.SelectedItem;
                    nomeCliente = pessoa.RazaoSocial.Split(' ')[0];
                    if (pessoa.PessoaTelefone != null)
                    {
                        telefoneCompleto = EnviarMensagemWhatsapp.TratarTelefone(pessoa.PessoaTelefone.Ddd, pessoa.PessoaTelefone.Telefone);
                    }
                    string mensagemSugestao = $"Olá {nomeCliente}, a {Sessao.empresaFilialLogada.NomeFantasia} deseja a você um feliz aniversário! Que o seu dia seja cheio de alegria e realizações.";

                    FrmEnvioMensagem frmEnvioMensagem = new FrmEnvioMensagem(telefoneCompleto, nomeCliente, true, mensagemSugestao);
                    if (frmEnvioMensagem.ShowDialog() == DialogResult.OK)
                    {
                        string escolha = frmEnvioMensagem.GetEscolha();
                        string numero = frmEnvioMensagem.GetTelefone();
                        string nome = frmEnvioMensagem.GetNome();
                        string mensagem = frmEnvioMensagem.GetMensagem();
                        if (pessoa.DataNascimento.Day == DateTime.Now.Day && pessoa.DataNascimento.Month == DateTime.Now.Month)
                            await enviarMensagemPeloWhats(null, numero, nome, mensagem);
                        else
                        {
                            if (GenericaDesktop.ShowConfirmacao("O aniversário do(a) " + nomeCliente + " não é hoje, deseja realmente enviar uma mensagem?"))
                            {
                                await enviarMensagemPeloWhats(null, numero, nome, mensagem);
                            }
                        }
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione um cliente!");
            }
            else
            {
                if (gridClient.SelectedIndex >= 0)
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa = (Pessoa)gridClient.SelectedItem;
                    if (pessoa.PessoaTelefone != null)
                    {
                        telefoneCompleto = EnviarMensagemWhatsapp.TratarTelefone(pessoa.PessoaTelefone.Ddd, pessoa.PessoaTelefone.Telefone);

                        string primeiroNome = pessoa.RazaoSocial.Split(' ')[0];
                        string mensagem = $"Olá {primeiroNome}, a {Sessao.empresaFilialLogada.NomeFantasia} deseja a você um feliz aniversário! Que o seu dia seja cheio de alegria e realizações.";
                        string mensagemCodificada = HttpUtility.UrlKeyValueEncode(mensagem);
                        string url = $"https://web.whatsapp.com/send?phone={telefoneCompleto}&text={mensagemCodificada}";
                        if (pessoa.DataNascimento.Day == DateTime.Now.Day && pessoa.DataNascimento.Month == DateTime.Now.Month)
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true
                            });
                        }
                        else
                        {
                            if (GenericaDesktop.ShowConfirmacao("O aniversário do(a) " + nomeCliente + " não é hoje, deseja realmente enviar uma mensagem?"))
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = url,
                                    UseShellExecute = true
                                });
                            }
                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("Cliente sem telefone!");
                    }
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            options.ExportAllPages = true;
            options.DefaultColumnWidth = 12;

            var excelEngine = gridClient.ExportToExcel(gridClient.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            workBook.Worksheets[0].UsedRange.BorderInside(ExcelLineStyle.Hair, ExcelKnownColors.Black);
            workBook.Worksheets[0].UsedRange.BorderAround(ExcelLineStyle.Hair, ExcelKnownColors.Black);

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "Aniversariantes",
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
                if (MessageBox.Show(this.gridClient, "Deseja abrir o arquivo no Excel?", "Arquivo criado com sucesso",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                    System.Diagnostics.Process.Start(saveFilterDialog.FileName);
                }
            }
        }

        private async Task enviarMensagemPeloWhats(OrdemServico ordem, string numero, string nome, string mensagem)
        {
            if (gridClient.SelectedIndex >= 0)
            {
                var client = new EnviarMensagemWhatsapp();
                await client.SendMessageAsync(numero, mensagem);
            }
        }

        private void gridClient_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;


        }
        public int CalcularIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade--;
            }
            return idade;
        }
        private void gridClient_QueryCellStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventArgs e)
        {
            try
            {
                // Ajusta o índice da linha para acessar o registro correto
                int adjustedRowIndex = e.RowIndex - 1;

                // Verifica se o índice ajustado está dentro do intervalo dos registros
                if (adjustedRowIndex >= 0 && adjustedRowIndex < gridClient.View.Records.Count)
                {
                    var record = gridClient.View.Records[adjustedRowIndex];

                    // Verifique se o registro não é nulo e se o mapeamento de dados é válido
                    if (record != null && record.Data is Pessoa pessoa)
                    {
                        if (e.Column.MappingName == "Idade")
                        {
                            // Ajusta o estilo baseado na data de nascimento
                            if (pessoa.DataNascimento.ToShortDateString() == "01/01/1900")
                            {
                                e.Style.TextColor = Color.Red;
                                e.Style.BackColor = Color.White;
                            }
                            else
                            {
                                e.Style.BackColor = adjustedRowIndex % 2 == 0 ? Color.WhiteSmoke : Color.White;
                                e.Style.TextColor = Color.Black;
                            }

                            // Exibe a idade
                            int idade = CalcularIdade(pessoa.DataNascimento);
                            // Certifique-se de que DisplayText é suportado
                            e.DisplayText = idade.ToString();
                        }
                    }
                    else
                    {
                        // Log para depuração
                        Console.WriteLine($"Record or Pessoa is null at adjusted RowIndex {adjustedRowIndex}.");
                    }
                }
                else
                {
                    // Log para depuração
                    Console.WriteLine($"Adjusted RowIndex {adjustedRowIndex} is out of range.");
                }
            }
            catch (Exception ex)
            {
                // Log de erro
                Console.WriteLine($"Exception: {ex.Message}");
            }
        } 


    }
}
