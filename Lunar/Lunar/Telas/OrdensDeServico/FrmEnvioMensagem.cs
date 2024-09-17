using Lunar.Utils;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmEnvioMensagem : Form
    {
        String Escolha = "";
        String telefone = "";
        String nome = "";
        String mensagem = "";
        public FrmEnvioMensagem(String numeroTelefone, String nomeCliente)
        {
            InitializeComponent();
            txtNumeroCliente.Text = numeroTelefone;
            txtNomeCliente.Text = nomeCliente;
            nome = nomeCliente;
            txtMensagem.Text = "";
            txtMensagem.Focus();
        }
        public FrmEnvioMensagem(String numeroTelefone, String nomeCliente, bool apenasMsgLivre, string mensagemSugestao)
        {
            InitializeComponent();
            txtNumeroCliente.Text = numeroTelefone;
            txtNomeCliente.Text = nomeCliente;
            nome = nomeCliente;
            txtMensagem.Text = mensagemSugestao;
            txtMensagem.Focus();
            radioPdfOs.Visible = false;
            radioProdutoDisponivel.Visible = false;
            radioTecnicoCaminho.Visible = false;
        }

        public string GetEscolha()
        {
            return Escolha;
        }
        public string GetTelefone()
        {
            return telefone;
        }
        public string GetNome()
        {
            return nome;
        }
        public string GetMensagem()
        {
            return mensagem;
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            telefone = GenericaDesktop.RemoveCaracteres(txtNumeroCliente.Text.Trim());
            if (!telefone.StartsWith("55"))
            {
                telefone = "55" + telefone;
            }

            nome = txtNomeCliente.Text;
            mensagem = txtMensagem.Text;
            if (radioPdfOs.Checked == true)
            {
                Escolha = "Envio PDF";
            }
            else if (radioTecnicoCaminho.Checked == true)
            {
                Escolha = "Envio Técnico";
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioTecnicoCaminho_CheckedChanged(object sender, EventArgs e)
        {
            if(radioTecnicoCaminho.Checked == true)
            {
                txtMensagem.Text = "*" + Sessao.empresaFilialLogada.NomeFantasia + ":* " + "Olá" + ", " + nome + "! queremos agradecer pela " +
                    "preferência e confiança em nossos serviços. Estamos felizes em informar que nosso técnico/responsável pelo serviço " +
                    "solicitado está a caminho para atendê-lo. Conte conosco para proporcionar a melhor experiência possível. " +
                    "Obrigado pela sua escolha.";
            }
        }

        private void FrmEnvioMensagem_Load(object sender, EventArgs e)
        {
  
        }

        private void FrmEnvioMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnEnviar.PerformClick();
                    break;
            }
        }

        private void radioProdutoDisponivel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioProdutoDisponivel.Checked == true)
            {
                txtMensagem.Text = "*" + Sessao.empresaFilialLogada.NomeFantasia + ":* " + "Olá" + ", " + nome + "! queremos agradecer pela " +
                    "preferência e confiança em nossos Produtos/Serviços. Estamos felizes em informar que seu produto " +
                    "já está disponível para retirada em nossa loja. Conte conosco para proporcionar a melhor experiência possível.";
            }
        }

        private void radioPdfOs_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPdfOs.Checked == true)
            {
                txtMensagem.Text = "Olá, [nome]! Segue cópia da Ordem de Serviço. Data/Hora Envio: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ", clique e faça o download do PDF.";
            }
        }

        private void radioMensagemLivre_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMensagemLivre.Checked == true)
            {
                txtMensagem.Text = "";
            }
        }
    }
}
