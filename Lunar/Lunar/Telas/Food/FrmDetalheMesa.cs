using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDTO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
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
            button1.Text = numeroMesa.ToString();

            InitializeDynamicButtons();
            mesa.Id = idMesa;
            mesa = (AtendimentoMesa)Controller.getInstance().selecionar(mesa);
        }

        private void InitializeDynamicButtons()
        {
            // Exemplo de dados de pessoas
            string[] pessoas = { "GERAL" };

            // Cria botões dinamicamente
            foreach (var pessoa in pessoas)
            {
                Button btn = new Button();
                btn.Text = pessoa;
                btn.Size = new Size(200, 50);
                btn.BackColor = Color.FromArgb(123, 19, 255);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Margin = new Padding(5);
                btn.Click += new EventHandler(Button_Click);
                btn.Font = new Font("Microsoft JhengHei", 12, FontStyle.Regular);

                flowLayoutPanel1.Controls.Add(btn);
            }

            // Adiciona o botão especial "Ocupar Mesa [F8]"
            Button ocuparMesaBtn = new Button();
            ocuparMesaBtn.Text = "Adicionar Conta [F9]";
            ocuparMesaBtn.Size = new Size(200, 50);
            ocuparMesaBtn.BackColor = Color.White;
            ocuparMesaBtn.ForeColor = Color.FromArgb(123, 19, 255);
            ocuparMesaBtn.FlatStyle = FlatStyle.Flat;
            ocuparMesaBtn.Margin = new Padding(5);
            ocuparMesaBtn.Click += new EventHandler(AdicionarConta_Click);
            ocuparMesaBtn.Font = new Font("Microsoft JhengHei", 12, FontStyle.Regular);

            flowLayoutPanel1.Controls.Add(ocuparMesaBtn);
        }
        private void AdicionarConta_Click(object sender, EventArgs e)
        {
            // Ação ao clicar no botão "Ocupar Mesa [F8]"
            MessageBox.Show("Adicionar Conta [F9]");
        }
        // Evento de clique para expandir e mostrar mais detalhes
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Alterna entre expandir e recolher
                if (clickedButton.Height == 50)
                {
                    clickedButton.Height = 100; // Expande o botão
                    Label detailsLabel = new Label();
                    detailsLabel.Text = $"Detalhes de {clickedButton.Text}";
                    detailsLabel.Size = new Size(180, 50);
                    detailsLabel.Location = new Point(10, 50); // Posição abaixo do texto do botão
                    detailsLabel.ForeColor = Color.White;
                    detailsLabel.BackColor = Color.Transparent;
                    clickedButton.Controls.Add(detailsLabel);
                }
                else
                {
                    clickedButton.Height = 50; // Recolhe o botão
                    clickedButton.Controls.Clear(); // Remove detalhes
                }
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
            //mesa.Status = "OCUPADO";
            //Controller.getInstance().salvar(mesa);

            //AtendimentoDto atendimentoDto = new AtendimentoDto();
            //atendimentoDto.Id = 0;
            //atendimentoDto.Data = DateTime.Now;
            //atendimentoDto.Identificacao = "GERAL";
            //atendimentoDto.Observacoes = "";
            ////Salvar Atendimento
            //string idAtendimento = await ApiServiceFood.PostSalvarAtendimento(atendimentoDto);

            //AtendimentoContaDto atendimentoContaDto = new AtendimentoContaDto();
            //atendimentoContaDto.IdAtendimento = int.Parse(idAtendimento);
            //atendimentoContaDto.NomeCliente = "GERAL";
            //atendimentoContaDto.IdCliente = null;
            ////Salvar Atendimento Conta
            //string idAtendimentoConta = await ApiServiceFood.PostSalvarAtendimentoConta(atendimentoContaDto);


            //AtendimentoVinculoDto atendimentoVinculoDto = new AtendimentoVinculoDto();
            //atendimentoVinculoDto.Id = 0;
            //atendimentoVinculoDto.IdMesa = mesa.Id;
            //atendimentoVinculoDto.IdConta = int.Parse(idAtendimentoConta);
            //atendimentoVinculoDto.IdAtendimento = int.Parse(idAtendimento);
            //atendimentoVinculoDto.Operador = Sessao.usuarioLogado.Id.ToString();
            ////Salvar Atendimento Conta
            //string result2 = await ApiServiceFood.PostSalvarAtendimentoVinculo(atendimentoVinculoDto);

            //GenericaDesktop.ShowInfo("Abertura de Mesa Realizada com Sucesso!");
            ocuparMesaAsync();
        }

        private async void ocuparMesaAsync()
        {
            //mesa.Status = "OCUPADO";
            //Controller.getInstance().salvar(mesa);

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

            GenericaDesktop.ShowInfo("Abertura de Mesa Realizada com Sucesso!");
        }


    }
}
