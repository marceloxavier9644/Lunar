using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.IO;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.Adicionais
{
    public partial class FrmAguarde : Form
    {
        Nfe nf = new Nfe();
        public FrmAguarde(string tempoAguardar, Nfe nfe)
        {
            InitializeComponent();
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;
            nf = nfe;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Aguarde...";

            if (circularProgressBar1.Value < 100)
            {
                circularProgressBar1.Text = circularProgressBar1.Value.ToString();
                circularProgressBar1.Value++;
                if (File.Exists(Sessao.parametroSistema.PastaRemessaNsCloud + @"\Processados\nsConcluido\" + nf.NNf + ".txt") || Sessao.teveRetornoApi == true)
                {
                    circularProgressBar1.Value = 100;
                    circularProgressBar1.Text = circularProgressBar1.Value.ToString();
                    timer1.Stop();
                    this.Close();
                }
            }
            else
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void FrmAguarde_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
        }
    }
}
