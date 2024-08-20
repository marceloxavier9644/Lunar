using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDTO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Food
{
    public partial class FrmDetalheMesa : Form
    {
        AtendimentoMesa mesa = new AtendimentoMesa();
        public FrmDetalheMesa(int numeroMesa, int idMesa)
        {
            InitializeComponent();

            mesa.Id = idMesa;
            mesa = (AtendimentoMesa)Controller.getInstance().selecionar(mesa);
            if (mesa.AtendimentoId != null)
            {
                ajustarBotaoMesaOcupada();
                CarregarVinculo(int.Parse(mesa.AtendimentoId.ToString()));
                InicializarAtendimentoConta(int.Parse(mesa.AtendimentoId.ToString()));
            }
        }
        private void CriarBotoesParaVinculos(List<AtendimentoVinculoDto> vinculos)
        {
            // Limpar o Panel antes de adicionar novos botões
            panel1.Controls.Clear();

            // Tamanho e espaçamento dos botões
            int buttonWidth = 75;
            int buttonHeight = 63;
            int spacing = 10; // Espaçamento entre botões
            int startX = 10; // Posição inicial X

            // Definições de estilo do botão
            Font buttonFont = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular);
            Color buttonBackColor = Color.FromArgb(123, 19, 255);
            Color buttonForeColor = Color.White; // Cor da fonte

            // Calcular a posição Y inicial para centralizar verticalmente
            int totalHeight = buttonHeight;
            int panelHeight = panel1.ClientSize.Height;
            int startY = (panelHeight - totalHeight) / 2;

            // Adiciona os botões ao panel
            for (int i = 0; i < vinculos.Count; i++)
            {
                var vinculo = vinculos[i];

                Button button = new Button
                {
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Text = vinculo.IdMesa.ToString(), // Texto do botão como número da mesa
                    Location = new Point(startX + i * (buttonWidth + spacing), startY),
                    Font = buttonFont, // Define a fonte do botão
                    BackColor = buttonBackColor, // Define a cor de fundo do botão
                    ForeColor = buttonForeColor // Define a cor da fonte do botão
                };

                // Adicione um evento de clique, se necessário
                button.Click += (sender, e) =>
                {
                    // Ação ao clicar no botão, você pode personalizar conforme necessário
                    MessageBox.Show($"Mesa {vinculo.IdMesa}");
                };

                // Adiciona o botão ao panel
                panel1.Controls.Add(button);
            }
        }
        private async void CarregarVinculo(int idAtendimento)
        {
            var vinculos = await ApiServiceFood.GetListaVinculoPorAtendimento(idAtendimento);
            CriarBotoesParaVinculos(vinculos);
        }
        private void InicializarAtendimentoConta(int idAtendimento)
        {
            flowLayoutPanel1.Controls.Clear();

            AtendimentoContaController atendimentoContaController = new AtendimentoContaController();
            IList<AtendimentoConta> listaConta = atendimentoContaController.selecionarTodosAtendimentoConta(idAtendimento);
            if (listaConta.Count > 0)
            {
                foreach (var conta in listaConta)
                {
                    Button btn = new Button();
                    btn.Text = conta.NomeCliente; // Presumindo que NomeCliente seja uma propriedade de AtendimentoConta
                    btn.Size = new Size(200, 50);
                    if (conta.NomeCliente.Equals("GERAL"))
                    {
                        btn.BackColor = Color.FromArgb(123, 19, 255);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                           btn.BackColor = Color.White;
                        btn.ForeColor = Color.Black;
                    }
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Margin = new Padding(5);
                    btn.Font = new Font("Microsoft JhengHei", 12, FontStyle.Regular);

                    // Associa o objeto AtendimentoConta ao botão
                    btn.Tag = conta;

                    // Associa o evento de clique ao botão
                    btn.Click += new EventHandler(Button_Click);

                    // Adiciona o botão ao FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is AtendimentoConta conta)
            {

                MessageBox.Show($"Conta selecionada: {conta.NomeCliente}");
                FrmPdvFood frmPdvFood = new FrmPdvFood();
                frmPdvFood.ShowDialog();
            }
        }

        private void btnMaisPessoas_Click(object sender, EventArgs e)
        {
            int quantidade = int.Parse(txtQuantidadePessoas.Text);
            if (quantidade >= 0)
            {
                quantidade++;
                txtQuantidadePessoas.Text = quantidade.ToString();
            }
        }

        private void btnMenosPessoas_Click(object sender, EventArgs e)
        {
            int quantidade = int.Parse(txtQuantidadePessoas.Text);
            if (quantidade > 1)
            {
                quantidade--;
                txtQuantidadePessoas.Text = quantidade.ToString();
            }
        }

        private async void btnOcuparMesa_Click(object sender, EventArgs e)
        {
            ocuparMesaAsync();
        }

        private async void ocuparMesaAsync()
        {
            AtendimentoDto atendimentoDto = new AtendimentoDto
            {
                Id = 0,
                Data = DateTime.Now,
                Identificacao = "GERAL",
                Observacoes = ""
            };

            AtendimentoContaDto atendimentoContaDto = new AtendimentoContaDto
            {
                IdAtendimento = 0, // This will be atualizado na API
                NomeCliente = "GERAL",
                IdCliente = null,
                Id = 0
            };

            AtendimentoVinculoDto atendimentoVinculoDto = new AtendimentoVinculoDto
            {
                Id = 0,
                IdMesa = mesa.Id,
                IdConta = 0, // This will be atualizado na API
                IdAtendimento = 0, // This will be atualizado na API
                Operador = Sessao.usuarioLogado.Id.ToString()
            };

            AtendimentoMasterDto atendimentoMasterDto = new AtendimentoMasterDto
            {
                IdMesa = mesa.Id,
                Atendimento = atendimentoDto,
                AtendimentoConta = atendimentoContaDto,
                AtendimentoVinculo = atendimentoVinculoDto
            };

            string result = await ApiServiceFood.PostSalvarAtendimentoMaster(atendimentoMasterDto);
            var atendimentoResponse = JsonConvert.DeserializeObject<AtendimentoResponse>(result);
            CarregarVinculo(atendimentoResponse.AtendimentoId);
            InicializarAtendimentoConta(atendimentoResponse.AtendimentoId);

            ajustarBotaoMesaOcupada();
            GenericaDesktop.ShowInfo("Abertura de Mesa Realizada com Sucesso!");
        }

        private void ajustarBotaoMesaOcupada()
        {
            btnOcuparMesa.BackColor = Color.Wheat;
            btnOcuparMesa.Enabled = false;
            btnOcuparMesa.Text = "MESA OCUPADA";
        }
        public class AtendimentoResponse
        {
            public int AtendimentoId { get; set; }
            public int AtendimentoContaId { get; set; }
            public int MesaId { get; set; }
            public string Message { get; set; }
        }

        private void btnAdicionarConta_Click(object sender, EventArgs e)
        {
            int quantidadePessoa = 0;
            if (!String.IsNullOrEmpty(txtQuantidadePessoas.Text))
                quantidadePessoa = int.Parse(txtQuantidadePessoas.Text);

            Form formBackground = new Form();
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
            FrmAdicionarComanda fr = new FrmAdicionarComanda(true, mesa.Id.ToString(), quantidadePessoa, mesa.AtendimentoId.ToString());
            fr.Owner = formBackground;
            fr.ShowDialog();
            formBackground.Dispose();
            fr.Dispose();
            InicializarAtendimentoConta(int.Parse(mesa.AtendimentoId.ToString()));
        }

        private void FrmDetalheMesa_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F8:
                    btnOcuparMesa.PerformClick();
                    break;
                case Keys.F10:
                    btnReservarMesa.PerformClick();
                    break;
                case Keys.F11:
                    btnAdicionarConta.PerformClick();
                    break;
            }
        }

        private void btnReservarMesa_Click(object sender, EventArgs e)
        {

        }
    }
}
