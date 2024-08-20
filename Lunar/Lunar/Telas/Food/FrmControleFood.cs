using Lunar.Telas.ParametroDoSistema;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Panel = System.Windows.Forms.Panel;

namespace Lunar.Telas.Food
{
    public partial class FrmControleFood : Form
    {
        private Panel draggedPanel = null;
        private Point dragStartPoint;
        private ClientWebSocket webSocket;
        private CancellationTokenSource cts;
        public FrmControleFood()
        {
            InitializeComponent();
            //Confid Food
            retornarConfigPadrao();
            if (Sessao.atendimentoConfig != null)
            {
                ConfigureFlowLayoutPanel();
                loadMesas();
                StartWebSocketConnection();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Faça a configuração do sistema Lunar Food!");
                FrmConfigFood frm = new FrmConfigFood();
                frm.ShowDialog();
            }
        }
        private void textBoxPesquisar_Enter(object sender, EventArgs e)
        {
   
        }
        private void retornarConfigPadrao()
        {
            try
            {
                AtendimentoConfig atendimentoConfig = new AtendimentoConfig();
                atendimentoConfig.Id = 1;

                var resultado = Controller.getInstance().selecionar(atendimentoConfig);
                if (resultado != null && resultado is AtendimentoConfig)
                {
                    Sessao.atendimentoConfig = (AtendimentoConfig)resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao selecionar AtendimentoConfig: " + ex.Message);
            }
        }
        private async void loadMesas()
        {
            var statusMesas = new Dictionary<int, string>
            {
                { 1, "DISPONIVEL" },
                { 2, "OCUPADO" }
            };

            List<AtendimentoMesa> mesas = await ApiServiceFood.GetAtendimentoMesasAsync();
            if (mesas.Count > 0)
            {
                int numeroInicial = mesas.Min(m => int.Parse(m.Numero));
                int numeroFinal = mesas.Max(m => int.Parse(m.Numero));

                AddMesasEComandas(mesas);
            }
        }


        private void ConfigureFlowLayoutPanel()
        {
            // Configurando o FlowLayoutPanel para permitir rolagem
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
        }

        private void textBoxPesquisar_Leave(object sender, EventArgs e)
        {
        }
   
        private void btnNovoAtendimento_Click(object sender, EventArgs e)
        {
            using (var form = new FrmAdicionarComanda())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AddComanda(form.NumeroComanda, form.NomeCliente, form.Observacao,form.QuantidadePessoas, Color.FromArgb(255, 230, 230), true);
                }
            }
        }

        private void AddComanda(string numero, string nome, string observacao, string quantidadePessoas, Color corFundo, bool ocupado)
        {
            Panel comandaPanel = new Panel();
            comandaPanel.Size = new Size(200, 120); // Mantém a altura para incluir o rodapé
            comandaPanel.BackColor = corFundo;
            comandaPanel.BorderStyle = BorderStyle.FixedSingle;
            comandaPanel.Cursor = Cursors.Hand;
            comandaPanel.Tag = "COMANDA";

            Font labelFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            Label lblIdentificacao = new Label();
            lblIdentificacao.Text = $"COMANDA: {numero}";
            lblIdentificacao.Location = new Point(10, 10);
            lblIdentificacao.Size = new Size(180, 20); // Ajusta o tamanho do label
            lblIdentificacao.TextAlign = ContentAlignment.MiddleCenter;
            lblIdentificacao.Font = labelFont;

            Label lblNome = new Label();
            lblNome.Text = $"CLIENTE: {nome}";
            lblNome.Location = new Point(10, 35); // Ajusta a localização para minimizar espaço
            lblNome.Size = new Size(180, 20); // Ajusta o tamanho do label
            lblNome.AutoSize = false;
            lblNome.Font = labelFont;

            Label lblObservacao = new Label();
            lblObservacao.Text = $"OBS: {observacao}";
            lblObservacao.Location = new Point(10, 60); // Ajusta a localização para minimizar espaço
            lblObservacao.Size = new Size(180, 20); // Ajusta o tamanho do label
            lblObservacao.AutoSize = false;
            lblObservacao.Font = labelFont;

            comandaPanel.Controls.Add(lblIdentificacao);
            comandaPanel.Controls.Add(lblNome);
            comandaPanel.Controls.Add(lblObservacao);

            // Adicionar rodapé
            Panel footerPanel = new Panel();
            footerPanel.Size = new Size(200, 20);
            footerPanel.Location = new Point(0, 100); // Localização do rodapé ajustada
            footerPanel.BackColor = ocupado ? Color.FromArgb(255, 0, 0) : Color.FromArgb(123, 19, 255); // Cor do rodapé
            footerPanel.BorderStyle = BorderStyle.FixedSingle;

            Label lblFooter = new Label();
            lblFooter.Text = ocupado ? "Ocupado" : "Disponível";
            lblFooter.ForeColor = Color.White;
            lblFooter.AutoSize = false;
            lblFooter.Size = new Size(200, 20);
            lblFooter.Location = new Point(0, 0);
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            lblFooter.Font = labelFont;

            footerPanel.Controls.Add(lblFooter);
            comandaPanel.Controls.Add(footerPanel);

            flowLayoutPanel1.Controls.Add(comandaPanel);
        }

        //private Dictionary<int, Panel> mesasDic = new Dictionary<int, Panel>();
        //private void AddMesas(int numeroInicial, int numeroFinal)
        //{
        //    mesasDic.Clear(); // Limpa o dicionário para evitar duplicatas

        //    for (int i = numeroInicial; i <= numeroFinal; i++)
        //    {
        //        Panel mesaPanel = new Panel();
        //        mesaPanel.Size = new Size(200, 120);
        //        mesaPanel.BorderStyle = BorderStyle.FixedSingle;
        //        mesaPanel.Cursor = Cursors.Hand;
        //        mesaPanel.Tag = "MESA";

        //        Font labelFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

        //        Label lblMesa = new Label();
        //        lblMesa.Text = $"MESA {i}";
        //        lblMesa.AutoSize = false; // Permite definir a largura e a altura
        //        lblMesa.Size = new Size(200, 60); // Ajuste o tamanho conforme necessário
        //        lblMesa.Location = new Point(0, 10); // Ajuste a posição conforme necessário
        //        lblMesa.TextAlign = ContentAlignment.MiddleCenter; // Centraliza o texto
        //        lblMesa.Font = labelFont;

        //        mesaPanel.Controls.Add(lblMesa);

        //        // Adicionar rodapé
        //        Panel footerPanel = new Panel();
        //        footerPanel.Size = new Size(200, 20);
        //        footerPanel.Location = new Point(0, 100); // Ajuste conforme necessário
        //        footerPanel.BackColor = Color.FromArgb(123, 19, 255); // Disponível por padrão
        //        mesaPanel.BackColor = Color.FromArgb(242, 232, 255); // Superior - Disponível

        //        Label lblFooter = new Label();
        //        lblFooter.Text = "Disponível";
        //        lblFooter.ForeColor = Color.White;
        //        lblFooter.AutoSize = false;
        //        lblFooter.Size = new Size(200, 20); // O mesmo tamanho do footerPanel
        //        lblFooter.Location = new Point(0, 0); // Ajuste a localização para garantir que o texto fique centralizado
        //        lblFooter.TextAlign = ContentAlignment.MiddleCenter;
        //        lblFooter.Font = labelFont;

        //        footerPanel.Controls.Add(lblFooter);
        //        mesaPanel.Controls.Add(footerPanel);

        //        flowLayoutPanel1.Controls.Add(mesaPanel);

        //        // Adiciona a mesa ao dicionário
        //        mesasDic.Add(i, mesaPanel);
        //    }
        //}

        private Dictionary<int, Panel> mesasDic = new Dictionary<int, Panel>();
        private void Control_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            if (clickedControl != null)
            {
                // Propaga o clique para o pai
                Panel parentPanel = clickedControl.Parent as Panel;
                if (parentPanel != null)
                {
                    MesaPanel_Click(parentPanel, e);
                }
            }
        }

        private void AddMesasEComandas(List<AtendimentoMesa> mesas)
        {
            flowLayoutPanel1.Controls.Clear();
            mesasDic.Clear(); // Limpa o dicionário para evitar duplicatas

            foreach (var mesa in mesas)
            {
                Panel mesaPanel = new Panel();
                mesaPanel.Size = new Size(200, 120);
                mesaPanel.BorderStyle = BorderStyle.FixedSingle;
                mesaPanel.Cursor = Cursors.Hand;
                mesaPanel.Tag = mesa;
                mesaPanel.Click += MesaPanel_Click; // Associa o evento Click

                Font labelFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

                Label lblMesa = new Label();
                lblMesa.Text = mesa.Tipo + $": {mesa.Numero}";
                lblMesa.AutoSize = false;
                //lblMesa.Size = new Size(200, 60);
                //lblMesa.Location = new Point(0, 10);

                lblMesa.Size = new Size(180, 20); // Ajusta o tamanho do label
                lblMesa.Location = new Point(10, 10);
                if (mesa.Tipo.Equals("MESA"))
                {
                    lblMesa.Size = new Size(200, 60);
                    lblMesa.Location = new Point(0, 10);
                }  
     
                lblMesa.TextAlign = ContentAlignment.MiddleCenter;
                lblMesa.Font = labelFont;
                lblMesa.BackColor = Color.Transparent; // Torna o fundo do Label transparente
                lblMesa.Click += Control_Click; // Adiciona o evento Click ao Label

                Label lblNome = new Label();
                lblNome.Text = $"CLIENTE: {mesa.Nome}";
                lblMesa.AutoSize = false;
                lblNome.Location = new Point(10, 35); // Ajusta a localização para minimizar espaço
                lblNome.Size = new Size(180, 20); // Ajusta o tamanho do label
                lblNome.AutoSize = false;
                lblNome.Font = labelFont;

                Label lblObservacao = new Label();
                lblObservacao.Text = $"OBS: {mesa.Observacao}";
                lblObservacao.Location = new Point(10, 60); // Ajusta a localização para minimizar espaço
                lblObservacao.Size = new Size(180, 20); // Ajusta o tamanho do label
                lblObservacao.AutoSize = false;
                lblObservacao.Font = labelFont;

                mesaPanel.Controls.Add(lblMesa);
                if(!String.IsNullOrEmpty(mesa.Nome))
                    mesaPanel.Controls.Add(lblNome);
                if (!String.IsNullOrEmpty(mesa.Observacao))
                    mesaPanel.Controls.Add(lblObservacao);

                // Adicionar rodapé
                Panel footerPanel = new Panel();
                footerPanel.Size = new Size(200, 20);
                footerPanel.Location = new Point(0, 100);
                footerPanel.Click += Control_Click; // Adiciona o evento Click ao footer

                Label lblFooter = new Label();
                string status = mesa.Status;
                if (status == "OCUPADO")
                {
                    footerPanel.BackColor = Color.FromArgb(255, 0, 0); // Vermelho para ocupado
                    lblFooter.Text = "Ocupado";
                }
                else
                {
                    footerPanel.BackColor = Color.FromArgb(123, 19, 255); // Azul para disponível
                    lblFooter.Text = "Disponível";
                }

                lblFooter.ForeColor = Color.White;
                lblFooter.AutoSize = false;
                lblFooter.Size = new Size(200, 20);
                lblFooter.Location = new Point(0, 0);
                lblFooter.TextAlign = ContentAlignment.MiddleCenter;
                lblFooter.Font = labelFont;
                lblFooter.Click += Control_Click; // Adiciona o evento Click ao Label do footer

                footerPanel.Controls.Add(lblFooter);
                mesaPanel.Controls.Add(footerPanel);

                flowLayoutPanel1.Controls.Add(mesaPanel);

                // Adiciona a mesa ao dicionário
                mesasDic.Add(mesa.Id, mesaPanel); // Usa o ID do banco como chave
            }
        }

        private void MesaPanel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                AtendimentoMesa mesaInfo = clickedPanel.Tag as AtendimentoMesa;
                if (mesaInfo != null)
                {
                    int mesaNumero = int.Parse(mesaInfo.Numero);

                    Form formBackground = new Form();
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
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
                    FrmDetalheMesa fr = new FrmDetalheMesa(mesaNumero, mesaInfo.Id);
                    fr.Owner = formBackground;
                    fr.ShowDialog();
                    formBackground.Dispose();
                    fr.Dispose();

                    loadMesas();
                }
            }
        }
        private void UpdateMesaDisponibilidade(int numeroMesa, bool disponivel)
        {
            if (mesasDic.TryGetValue(numeroMesa, out Panel mesaPanel))
            {
                Panel footerPanel = (Panel)mesaPanel.Controls[1]; // Rodapé é o segundo controle adicionado (index 1)
                footerPanel.BackColor = disponivel ? Color.FromArgb(123, 19, 255) : Color.FromArgb(255, 0, 0);

                Label lblFooter = (Label)footerPanel.Controls[0];
                lblFooter.Text = disponivel ? "Disponível" : "Ocupado";

                mesaPanel.BackColor = disponivel ? Color.FromArgb(242, 232, 255) : Color.FromArgb(255, 230, 230);
            }
        }

        private void ajustarCoresBotoesFiltro(string botaoSelecionado)
        {
            if (botaoSelecionado.Equals("MESA"))
            {
                btnComandas.BackColor = Color.White;
                btnComandas.ForeColor = Color.Black;
                btnMesas.BackColor = Color.FromArgb(123, 19, 255);
                btnMesas.ForeColor = Color.White;
                btnTodas.BackColor = Color.White;
                btnTodas.ForeColor = Color.Black;
            }
            else if (botaoSelecionado.Equals("COMANDA"))
            {
                btnComandas.BackColor = Color.FromArgb(123, 19, 255);
                btnComandas.ForeColor = Color.White;
                btnMesas.BackColor = Color.White;
                btnMesas.ForeColor = Color.Black;
                btnTodas.BackColor = Color.White;
                btnTodas.ForeColor = Color.Black;
            }
            else
            {
                btnComandas.BackColor = Color.White;
                btnComandas.ForeColor = Color.Black;
                btnMesas.BackColor = Color.White;
                btnMesas.ForeColor = Color.Black;
                btnTodas.BackColor = Color.FromArgb(123, 19, 255);
                btnTodas.ForeColor = Color.White;
            }
        }
        private void btnMesas_Click(object sender, EventArgs e)
        {
            FilterItems("MESA", "");
            ajustarCoresBotoesFiltro("MESA");
        }

        private void btnComandas_Click(object sender, EventArgs e)
        {
            FilterItems("COMANDA", "");
            ajustarCoresBotoesFiltro("COMANDA");
        }

        private void btnTodas_Click(object sender, EventArgs e)
        {
            FilterItems("TODAS", "");
            ajustarCoresBotoesFiltro("TODAS");
        }

        private void FilterItems(string filter, string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
    
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                var atendimentoMesa = control.Tag as AtendimentoMesa;
                bool visible = false;

                if (filter == "TODAS")
                {
                    visible = true;
                }
                else if (filter == "MESA" && atendimentoMesa.Tipo == "MESA")
                {
                    visible = true;
                }
                else if (filter == "COMANDA" && atendimentoMesa.Tipo == "COMANDA")
                {
                    visible = true;
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // Procura pelo nome ou número dentro do painel
                    Label lbl = control.Controls.OfType<Label>().FirstOrDefault(l => l.Text.ToLower().Contains(searchTerm));
                    if (lbl == null)
                    {
                        visible = false;
                    }
                }

                control.Visible = visible;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        { 
            FilterItems("TODAS", txtPesquisar.Text);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
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
            FrmConfigFood fr = new FrmConfigFood();
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
            loadMesas();
        }

        private void FrmControleFood_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F2:
                    btnNovoAtendimento.PerformClick();
                    break;
            }
        }

        private static readonly HttpClient client = new HttpClient();
        public static async Task<List<AtendimentoMesa>> GetAtendimentoMesasAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:5130/api/ListaMesas");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<AtendimentoMesa> mesas = JsonConvert.DeserializeObject<List<AtendimentoMesa>>(responseBody);
                return mesas;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<AtendimentoMesa>();
            }
        }


        private async void StartWebSocketConnection()
        {
            cts = new CancellationTokenSource();
            webSocket = new ClientWebSocket();

            try
            {
                Uri serverUri = new Uri("ws://localhost:5130/ws"); 
                await webSocket.ConnectAsync(serverUri, cts.Token);
                MessageBox.Show("Conectado ao WebSocket.");

                _ = ReceiveMessagesAsync(webSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao WebSocket: {ex.Message}");
            }
        }

        private async Task ReceiveMessagesAsync(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];
            while (client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result;
                var receiveSegment = new ArraySegment<byte>(buffer);

                try
                {
                    result = await client.ReceiveAsync(receiveSegment, CancellationToken.None);
                    string messageReceived = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Atualizar a interface do usuário com a mensagem recebida
                    Invoke(new Action(() => UpdateUIWithMessage(messageReceived)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao receber mensagem: {ex.Message}");
                    break;
                }
            }
        }

        private void UpdateUIWithMessage(string message)
        {
            MessageBox.Show($"Mensagem recebida: {message}");

            try
            {
                //var mesas = JsonSerializer.Deserialize<List<AtendimentoMesa>>(message);
                // Atualize a FlowLayoutPanel ou outro controle com as novas informações
                // Exemplo:
                // UpdateFlowLayoutPanel(mesas);
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Erro ao processar a mensagem: {jsonEx.Message}");
            }
        }

        private void FrmControleFood_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts?.Cancel();
            webSocket?.Dispose();
        }

        private async void btnMessage_Click(object sender, EventArgs e)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                string messageToSend = "Mensagem de teste do Windows Forms";
                var buffer = Encoding.UTF8.GetBytes(messageToSend);
                var segment = new ArraySegment<byte>(buffer);

                try
                {
                    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao enviar mensagem: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("WebSocket não está conectado.");
            }
        }
    }
}
