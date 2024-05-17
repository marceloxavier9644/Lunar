using Lunar.Telas.Cadastros.Cliente.PessoaAdicionais;
using Lunar.Telas.Estoques;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.SerproGov;
using Microsoft.SharePoint.Client.Utilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente
{
    public partial class FrmClienteLista : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        private IList<Pessoa> listaClientes;
        PessoaController pessoaController = new PessoaController();
        Pessoa pessoa = new Pessoa();
        bool passou = false;
        public FrmClienteLista()
        {
            InitializeComponent();
            this.Opacity = 0.0;
            carregarLista();
            
        }
        public DataTable selectProdutos()
        {
            try
            {
                MySqlConnection con = null;

                String sql = "SELECT * FROM Pessoa Where Pessoa.FlagExcluido <> True";
                con = new MySqlConnection(Sessao._conexaoMySQL);
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void carregarLista()
        {
            txtPesquisaCliente.Texts = "";
            listaClientes = pessoaController.selecionarTodasPessoasPaginando(0, 50, "");
            //ajustarLista();        
            //gridClient.DataSource = listaClientes;
            sfDataPager1.DataSource = new List<int>();
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 50;
            sfDataPager1.AllowOnDemandPaging = true;

            Int64 totalPessoas = pessoaController.totalTodasPessoasPaginando("");
            int totalPaginas = (int)Math.Ceiling(Double.Parse((totalPessoas / sfDataPager1.PageSize).ToString()));
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }

            sfDataPager1.PageCount = totalPaginas;

            gridClient.DataSource = listaClientes;
            txtPesquisaCliente.Focus();
        }
        private void ajustarLista()
        {
            dsCliente.Tables[0].Clear();
            foreach (Pessoa pessoa in listaClientes)
            {
                DataRow row = dsCliente.Tables[0].NewRow();
                row.SetField("Codigo", pessoa.Id);
                row.SetField("Nome", pessoa.RazaoSocial);
                row.SetField("Apelido", pessoa.NomeFantasia);
                row.SetField("CNPJ", pessoa.Cnpj);

                string logradouro = "";
                string numero = "";
                if (pessoa.EnderecoPrincipal != null)
                {
                    logradouro = pessoa.EnderecoPrincipal.Logradouro;
                    numero = pessoa.EnderecoPrincipal.Numero;
                }
                row.SetField("Endereco", logradouro);
                row.SetField("Numero", numero);

                string cidade = "";
                if (pessoa.EnderecoPrincipal.Cidade != null)
                    cidade = pessoa.EnderecoPrincipal.Cidade.Descricao;
                row.SetField("Cidade", cidade);

                string bairro = "";
                if (!String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                    bairro = pessoa.EnderecoPrincipal.Bairro;
                row.SetField("Bairro", bairro);
                
                string telefone = "";
                if (pessoa.PessoaTelefone != null)
                {
                    telefone = pessoa.PessoaTelefone.Ddd;
                    telefone = telefone + pessoa.PessoaTelefone.Telefone;
                }
                row.SetField("Telefone", GenericaDesktop.MascaraTelefone(telefone));
              

                dsCliente.Tables[0].Rows.Add(row);
            }
        }

        private void FrmClienteLista_Load(object sender, EventArgs e)
        {
            if (passou == false)
            {
                passou = true;
                txtPesquisaCliente.Focus();

                //Ajustar Permissoes de usuario
                if(Sessao.permissoes.Count > 0)
                {
                    // Habilitar ou desabilitar os controles com base nas permissões
                    btnNovo.Enabled = Sessao.permissoes.Contains("1");
                    btnEditar.Enabled = Sessao.permissoes.Contains("2");
                    btnExcluir.Enabled = Sessao.permissoes.Contains("3");
                    btnExportarPDF.Enabled = Sessao.permissoes.Contains("4");
                    btnMensagemAlerta.Enabled = Sessao.permissoes.Contains("6");
                    btnExportarExcel.Enabled = Sessao.permissoes.Contains("7");
                    btnAnaliseCliente.Enabled = Sessao.permissoes.Contains("8");
                }
            }
        }

        private void PesquisarCliente(string valor, int paginaAtual)
        {
            //paginacao.DataSource = listaClientes;
            if (!String.IsNullOrEmpty(txtRegistroPorPagina.Texts))
                sfDataPager1.PageSize = int.Parse(txtRegistroPorPagina.Texts);
            else
                sfDataPager1.PageSize = 50;

            Int64 totalPessoas = pessoaController.totalTodasPessoasPaginando(valor);
            double totalPaginas = (double)totalPessoas / sfDataPager1.PageSize;
            if (totalPaginas < 1)
            {
                totalPaginas = 1;
            }

            sfDataPager1.PageCount = (int)Math.Ceiling(totalPaginas);
            sfDataPager1.Refresh();


            listaClientes = pessoaController.selecionarTodasPessoasPaginando(paginaAtual * sfDataPager1.PageSize, sfDataPager1.PageSize, valor);
            gridClient.DataSource = listaClientes;

            if (listaClientes.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado!");
                txtPesquisaCliente.Texts = "";
                txtPesquisaCliente.PlaceholderText = "";
                txtPesquisaCliente.Select();
            }
        }

        private void txtPesquisaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (String.IsNullOrEmpty(txtPesquisaCliente.Texts))
                {
                    if (GenericaDesktop.ShowConfirmacao("Sem digitar dados na pesquisa o sistema vai buscar todos clientes/fornecedores e pode demorar um tempo, deseja retornar todos?"))
                        PesquisarCliente(txtPesquisaCliente.Texts.Trim(),0);
                    else
                        txtPesquisaCliente.Focus();
                }
                else
                    PesquisarCliente(txtPesquisaCliente.Texts.Trim(),0);

            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            abrirNovoCadastro();
        }

        private void abrirNovoCadastro()
        {
            Form formBackground = new Form();
            try
            {
                using (FrmClienteCadastro uu = new FrmClienteCadastro())
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    //carregarLista();
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
            //FrmClienteCadastro frm = new FrmClienteCadastro();
            //frm.ShowDialog();
        }

        private void gridClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void sfDataPager1_OnDemandLoading(object sender, Syncfusion.WinForms.DataPager.Events.OnDemandLoadingEventArgs e)
        {
           //sfDataPager1.LoadDynamicData(e.StartRowIndex, listaClientes.Skip(e.StartRowIndex).Take(e.PageSize));
        }

        private void gridClient_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
            if ((e.RowData as Pessoa) != null)
            {
                if ((e.RowData as Pessoa).RegistradoSpc == true || (e.RowData as Pessoa).EscritorioCobranca == true)
                {
                    e.Style.TextColor = Color.Red;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            selecionarClienteParaEditar();
        }

        private void editarCadastro(Pessoa pessoa)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmClienteCadastro uu = new FrmClienteCadastro(pessoa))
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
                    uu.ShowDialog();
                    formBackground.Dispose();
                    PesquisarCliente(pessoa.RazaoSocial, 0);
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

        private void selecionarClienteParaEditar()
        {
            if (gridClient.SelectedIndex >= 0)
            {
                pessoa = new Pessoa();
                pessoa = (Pessoa)gridClient.SelectedItem;
                //Verificar se tem alerta
                genericaDesktop.buscarAlertaCadastrado(pessoa);
                editarCadastro(pessoa);
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja editar!");
        }

        private void gridClient_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (!Sessao.permissoes.Contains("2"))
            {
                e.Cancel = true; // Isso impede o evento padrão de edição se a permissão "2" não estiver presente
                GenericaDesktop.ShowAlerta("Usuário sem Permissão para essa operação!");
            }
            else
                selecionarClienteParaEditar();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            if (gridClient.SelectedIndex >= 0)
            {
                pessoa = new Pessoa();
                pessoa = (Pessoa)gridClient.SelectedItem;
                FrmImprimirFichaSimplificadaCliente fr = new FrmImprimirFichaSimplificadaCliente(pessoa);
                fr.ShowDialog();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2013;
            options.ExportAllPages = true;
            var excelEngine = gridClient.ExportToExcel(gridClient.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];

            SaveFileDialog saveFilterDialog = new SaveFileDialog
            {
                FileName = "ListaClientes",
                FilterIndex =3,
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) 
                this.Opacity += 0.05;          
        }

        private void FrmClienteLista_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
            //carregarLista(); aqui deu erro, ficava carregando a todo momento
        }

        private void paginacao_PageIndexChanged(object sender, Syncfusion.WinForms.DataPager.Events.PageIndexChangedEventArgs e)
        {
            PesquisarCliente(txtPesquisaCliente.Texts.Trim(), e.NewPageIndex);
        }

        private void txtRegistroPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarCliente(txtPesquisaCliente.Texts.Trim(), 0);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridClient.SelectedIndex >= 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja excluir esta pessoa?"))
                {
                    pessoa = new Pessoa();
                    pessoa = (Pessoa)gridClient.SelectedItem;
                    ContaReceberController contaReceberController = new ContaReceberController();
                    IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber Tabela Where Tabela.Cliente = " + pessoa.Id.ToString() + " and FlagExcluido <> True");
                    if (listaReceber.Count > 0)
                    {
                        GenericaDesktop.ShowErro("Este cliente possui faturas em aberto, não é possível excluir!");
                    }
                    else
                    {
                        Controller.getInstance().excluir(pessoa);
                        GenericaDesktop.ShowInfo("Excluído com Sucesso!");
                        PesquisarCliente(txtPesquisaCliente.Texts.Trim(), 0);
                    }
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja excluir!");
        }

        private void btnAnaliseCliente_Click(object sender, EventArgs e)
        {
            if (gridClient.SelectedIndex >= 0)
            {
                pessoa = new Pessoa();
                pessoa = (Pessoa)gridClient.SelectedItem;
                Form formBackground = new Form();
                FrmAnaliseCliente uu = new FrmAnaliseCliente(pessoa);
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                formBackground.Dispose();
                uu.Dispose();
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja analisar!");
        }

        private void btnMensagemAlerta_Click(object sender, EventArgs e)
        {
            if (gridClient.SelectedIndex >= 0)
            {
                pessoa = new Pessoa();
                pessoa = (Pessoa)gridClient.SelectedItem;
                Form formBackground = new Form();
                FrmAlertaMensagemCliente uu = new FrmAlertaMensagemCliente(pessoa);
                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.Opacity = .50d;
                formBackground.BackColor = Color.Black;
                formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                formBackground.WindowState = FormWindowState.Maximized;
                formBackground.TopMost = false;
                formBackground.Location = this.Location;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();
                uu.Owner = formBackground;
                uu.ShowDialog();
                formBackground.Dispose();
                uu.Dispose();
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do cliente que deseja inserir mensagem!");
        }

        private async void btnMei_Click(object sender, EventArgs e)
        {
            //Chamada API Serpro e consulta do boleto.
            var serproApiService = new SerproApiService();
            var tokenResponse = await serproApiService.AutenticacaoSerpro();
            string kwt = tokenResponse.jwt_token;
            string access = tokenResponse.access_token;
            var amd = await serproApiService.ConsultarGuiaMeiAsync(tokenResponse.jwt_token, tokenResponse.access_token, "34061827000127");
        }
        static byte[] ReadStream(System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private async Task ConsultaDasMeiAsync(string base64CombinedKey)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();

                // URL do certificado no servidor FTP...no ftp nao preciso passar a pasta www
                string ftpUrl = "ftp://lunarsoftware@ftp.lunarsoftware.com.br/Lunarerp/txt.pfx";
                // Baixar o arquivo do certificado temporariamente
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential("lunarsoftware", "Aramxs@11");
                string tempFilePath = Path.GetTempFileName();
                ftpClient.DownloadFile(ftpUrl, tempFilePath);

                // Carregar o certificado a partir do arquivo temporário
                X509Certificate2 cert = new X509Certificate2(tempFilePath, "123456");
                handler.ClientCertificates.Add(cert);
                

                // Use o handler conforme necessário (por exemplo, para fazer requisições HTTP com o certificado)
                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64CombinedKey);
                client.DefaultRequestHeaders.Add("Role-Type", "TERCEIROS");

                // Configurar o payload
                var payload = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                // Fazer a chamada à API
                HttpResponseMessage response = await client.PostAsync("https://autenticacao.sapi.serpro.gov.br/authenticate", payload);

                // Verificar a resposta
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                    string accessToken = tokenResponse.AccessToken;
                    string jwtToken = tokenResponse.JwtToken;
                    await consultarDasMeia(jwtToken, accessToken);
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Erro ao chamar a API DAS MEI: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro geral
                GenericaDesktop.ShowErro("Erro: " + ex.Message);
            }
        }

       

        private async Task consultarDasMeia(string jwtToken, string accessToken)
        {
            var requestBody = new
            {
                contratante = new
                {
                    numero = "28145398000173",
                    tipo = 2
                },
                autorPedidoDados = new
                {
                    numero = "34061827000127",
                    tipo = 2
                },
                contribuinte = new
                {
                    numero = "34061827000127",
                    tipo = 2
                },
                pedidoDados = new
                {
                    idSistema = "PGMEI",
                    idServico = "GERARDASPDF21",
                    versaoSistema = "1.0",
                    dados = "{\"periodoApuracao\": \"202401\"}" // Dados adicionais no formato JSON
                }
            };

            var jsonRequest = System.Text.Json.JsonSerializer.Serialize(requestBody);
            string apiUrl = "https://gateway.apiserpro.serpro.gov.br/integra-contador/v1/Emitir";

            using (var httpClient = new HttpClient())
            {
                // Adiciona os tokens de acesso e JWT ao cabeçalho de autorização
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                httpClient.DefaultRequestHeaders.Add("jwt_token", jwtToken);

                // Adiciona o cabeçalho Accept para indicar o tipo de conteúdo esperado na resposta
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Converte o corpo da requisição para o formato JSON
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Faz a chamada à API usando o método POST
                var response = await httpClient.PostAsync(apiUrl, content);

                // Verifica se a chamada foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Desserializa o JSON da resposta
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    if (responseObject != null && responseObject.dados != null)
                    {
                        foreach (var item in responseObject.dados)
                        {
                            // Verifica se o objeto contém um campo "pdf"
                            if (item.pdf != null)
                            {
                                byte[] pdfBytes = Convert.FromBase64String(item.pdf.ToString());

                                string anoMes = DateTime.Now.ToString("yyyy-MM");
                                string folderPath = @"C:\Lunar\DASMEI";
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                string filePath = @"C:\Lunar\DASMEI\"+anoMes+".pdf"; // Substitua pelo caminho desejado
                                File.WriteAllBytes(filePath, pdfBytes);

                                Process.Start(filePath);
                            }
                        }
                    }
                }
                else
                {
                    var problemDetailsJson = await response.Content.ReadAsStringAsync();
                    var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(problemDetailsJson);

                    // Exibe os detalhes do erro
                    Console.WriteLine("Erro status: " + problemDetails.status);
                    Console.WriteLine("Erro title: " + problemDetails.title);
                    Console.WriteLine("Erro detail: " + problemDetails.detail);
                }
            }
        }
        public class ProblemDetails
        {
            public string type { get; set; }
            public string title { get; set; }
            public int status { get; set; }
            public string detail { get; set; }
            public string instance { get; set; }
        }
        public class TokenResponse
        {
            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("jwt_token")]
            public string JwtToken { get; set; }

            [JsonProperty("jwt_pucomex")]
            public string JwtPucomex { get; set; }
        }

    }
}
