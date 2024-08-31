using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
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
            decimal saldo = caixaDAO.SelecionarSaldoPorSqlNativo(
                    "SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo " +
                    "FROM Caixa Tabela WHERE Tabela.Usuario = " + txtCodUsuario.Text + " AND Tabela.FormaPagamento = 1 " +
                    "AND Tabela.FLAGEXCLUIDO <> true");

            txtSaldoInicial.Text = saldo.ToString("N2");
        }
        private void FrmAbrirCaixa_Load(object sender, EventArgs e)
        {
            calcularSaldoInicial();
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            string dataInicial = DateTime.Parse(txtDataAbertura.Value.ToString()).ToString("yyyy-MM-dd");
            string dataFinal = DateTime.Parse(txtDataAbertura.Value.ToString()).ToString("yyyy-MM-dd");

            IList<CaixaAbertura> listaAbertura = caixaAberturaController.selecionarAberturaCaixaPorUsuarioEData(Sessao.usuarioLogado.Id, dataInicial, dataFinal);
            if (listaAbertura.Count <= 0)
            {
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
                Controller.getInstance().salvar(caixaAbertura);
                GenericaDesktop.ShowInfo("Caixa Aberto com Sucesso!");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Já existe um caixa nessa data");
            }
        }
    }
}
