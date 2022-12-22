using Lunar.Utils.Sintegra;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Sintegra
{
    public partial class FrmGerarSintegra : Form
    {
        GeradorSintegra geradorSintegra = new GeradorSintegra();
        public FrmGerarSintegra()
        {
            InitializeComponent();

            DateTime data = DateTime.Today;
            data = data.AddMonths(-1);
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);
            //DateTime com o último dia do mês
            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

            txtDataInicial.Value = primeiroDiaDoMes;
            txtDataFinal.Value = ultimoDiaDoMes;
            
            string month_name = data.ToString("MMM");
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string filePath = Path.Combine(desktop, "Sintegra" + month_name + ".txt");
            txtCaminho.Texts = desktop;
            txtDataInventario.Value = DateTime.Parse("31/12/" + data.AddYears(-1).Year);
        }

        private void chkRegistro74_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRegistro74.Checked == true)
                    txtDataInventario.Enabled = true;
                else
                    txtDataInventario.Enabled = false;
            }
            catch
            {

            }
        }

        private void btnCaminhoAnexos_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Texts = folder.SelectedPath;
            }
        }

        private void gerar()
        {
            bool reg74 = false;
            String dataInventario = "";
            DateTime data = DateTime.Parse(txtDataInventario.Value.ToString());
            dataInventario = data.ToString("yyyy'-'MM'-'dd' '23':'59':'59");
            if (chkRegistro74.Checked == true)
                reg74 = true;
            
            geradorSintegra.gerarSintegra(DateTime.Parse(txtDataInicial.Value.ToString()), DateTime.Parse(txtDataFinal.Value.ToString()), Sessao.empresaFilialLogada, txtCaminho.Texts, reg74, dataInventario);

        }
        private void btnGerar_Click(object sender, EventArgs e)
        {
            gerar();
        }

        private void FrmGerarSintegra_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    gerar();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
    }
}
