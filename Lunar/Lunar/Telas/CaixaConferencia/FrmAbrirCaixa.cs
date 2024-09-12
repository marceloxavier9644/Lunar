using Lunar.Telas.CaixaConferencia.ClassesAuxiliaresCaixa;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.CaixaConferencia
{
    public partial class FrmAbrirCaixa : Form
    {
        CaixaController caixaController = new CaixaController();
        CaixaDAO caixaDAO = new CaixaDAO();
        CaixaAberturaController caixaAberturaController = new CaixaAberturaController();
        public FrmAbrirCaixa()
        {
            InitializeComponent();
            txtUsuario.Text = Sessao.usuarioLogado.Login;
            txtCodUsuario.Text = Sessao.usuarioLogado.Id.ToString();

        }
        public class SaldoDTO
        {
            public decimal Saldo { get; set; }
        }
        private void calcularSaldoInicial()
        {
            decimal saldo = 0;

            CaixaService caixaService = new CaixaService();
            saldo = caixaService.ObterSaldo(DateTime.Parse(txtDataAbertura.Value.ToString()), Sessao.usuarioLogado.Id);

            txtSaldoInicial.Text = saldo.ToString("N2");
        }
        private void FrmAbrirCaixa_Load(object sender, EventArgs e)
        {
            calcularSaldoInicial();
            CaixaAberturaController caixaAberturaController = new CaixaAberturaController();
            IList<CaixaAbertura> listaCaixa = new List<CaixaAbertura>();
            if(Sessao.parametroSistema.TipoCaixa.Equals("INDIVIDUAL"))
                listaCaixa = caixaAberturaController.selecionarAberturaCaixaPorUsuario(Sessao.usuarioLogado.Id);
            else
                listaCaixa = caixaAberturaController.selecionarTodosCaixasAbertos();
            if (listaCaixa.Count > 0)
            {
                listBox1.DataSource = listaCaixa;
                listBox1.DisplayMember = "DataAbertura";
            }
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            string dataInicial = DateTime.Parse(txtDataAbertura.Value.ToString()).ToString("yyyy-MM-dd");
            string dataFinal = DateTime.Parse(txtDataAbertura.Value.ToString()).ToString("yyyy-MM-dd");
            IList<CaixaAbertura> listaAbertura = new List<CaixaAbertura>();
            if(Sessao.parametroSistema.TipoCaixa.Equals("INDIVIDUAL"))
                listaAbertura = caixaAberturaController.selecionarAberturaCaixaPorUsuarioEData(Sessao.usuarioLogado.Id, dataInicial, dataFinal);
            else
                listaAbertura = caixaAberturaController.selecionarAberturaCaixaPorData(dataInicial, dataFinal);
            if (listaAbertura.Count <= 0)
            {

                //Verificar se existe outro caixa logado e deslogar em datas anteriores
                CaixaAberturaController caixaAberturaController = new CaixaAberturaController();
                listaAbertura = caixaAberturaController.selecionarAberturaCaixaPorUsuario(Sessao.usuarioLogado.Id);
                if (listaAbertura.Count > 0)
                {
                    foreach (CaixaAbertura ca in listaAbertura)
                    {
                        ca.Logado = false;
                        Controller.getInstance().salvar(ca);
                    }
                }


                CaixaAbertura caixaAbertura = new CaixaAbertura();
                DateTime dataSelecionada = DateTime.Parse(txtDataAbertura.Value.ToString());
                TimeSpan horaAtual = DateTime.Now.TimeOfDay;
                DateTime dataComHoraAtual = dataSelecionada.Add(horaAtual);

                caixaAbertura.DataAbertura = dataComHoraAtual;
                caixaAbertura.Status = "ABERTO";
                caixaAbertura.EmpresaFilial = Sessao.empresaFilialLogada;
                caixaAbertura.SaldoInicial = decimal.Parse(txtSaldoInicial.Text);
                caixaAbertura.DataFechamento = DateTime.Parse("1900-01-01 00:00:00");
                caixaAbertura.SaldoFinal = 0;
                caixaAbertura.DiferencaFechamento = 0;
                caixaAbertura.IdCaixaAnterior = 0;
                caixaAbertura.Usuario = Sessao.usuarioLogado;
                caixaAbertura.Logado = true;
                Controller.getInstance().salvar(caixaAbertura);
                GenericaDesktop.ShowInfo("Caixa Aberto com Sucesso!");

                //Apresenta caixas abertos

                IList<CaixaAbertura> listaCaixa = caixaAberturaController.selecionarAberturaCaixaPorUsuario(Sessao.usuarioLogado.Id);
                if (listaCaixa.Count > 0)
                {
                    listBox1.DataSource = listaCaixa;
                    listBox1.DisplayMember = "DataAbertura";
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Já existe um caixa nessa data");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                CaixaAbertura caixaSelecionada = (CaixaAbertura)listBox1.SelectedItem;
                txtDataAbertura.Value = caixaSelecionada.DataAbertura;
                calcularSaldoInicial();
                //MessageBox.Show("Data de Abertura: " + caixaSelecionada.DataAbertura.ToShortDateString());
            }
        }

        private void btnAjustar_Click(object sender, EventArgs e)
        {
            CaixaAbertura caixaSelecionada = new CaixaAbertura();
            if (listBox1.SelectedItem != null)
            {
                caixaSelecionada = (CaixaAbertura)listBox1.SelectedItem;


                Form formBackground = new Form();
                FrmAjusteSaldoCaixa uu = new FrmAjusteSaldoCaixa(caixaSelecionada);
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
                calcularSaldoInicial();
            }
            else
                GenericaDesktop.ShowInfo("Abra o caixa que deseja ajustar e selecione ele ao lado!");
        }

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            CaixaAbertura caixaSelecionada = new CaixaAbertura();
            if (listBox1.SelectedItem != null)
            {
                caixaSelecionada = (CaixaAbertura)listBox1.SelectedItem;

                Form formBackground = new Form();
                FrmFecharCaixa uu = new FrmFecharCaixa(decimal.Parse(txtSaldoInicial.Text), caixaSelecionada);
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
                listBox1.DataSource = null;
                listBox1.Refresh();
                CaixaAberturaController caixaAberturaController = new CaixaAberturaController();
                IList<CaixaAbertura> listaCaixa = caixaAberturaController.selecionarAberturaCaixaPorUsuario(Sessao.usuarioLogado.Id);
                if (listaCaixa.Count > 0)
                {
                    listBox1.DataSource = listaCaixa;
                    listBox1.DisplayMember = "DataAbertura";
                    calcularSaldoInicial();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Selecione uma data de caixa!");
        }

        private void txtDataAbertura_Click(object sender, EventArgs e)
        {

        }

        private void txtDataAbertura_Leave(object sender, EventArgs e)
        {
            calcularSaldoInicial();
        }

        private void FrmAbrirCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;

                    case Keys.F8:
                        btnAbrirCaixa.PerformClick();
                        break;
                }
            }
        }

    }
}
