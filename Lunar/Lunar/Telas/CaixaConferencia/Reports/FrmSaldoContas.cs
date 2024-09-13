using Lunar.Utils.Grid_Class;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.CaixaConferencia.Reports
{
    public partial class FrmSaldoContas : Form
    {
        CaixaController caixaController = new CaixaController();
        CaixaDAO caixaDAO = new CaixaDAO();
        ContaBancariaController contaBancariaController = new ContaBancariaController();


        public FrmSaldoContas()
        {
            InitializeComponent();
            ObterSaldo();
            apresentarInformacoes();

        }

        private void apresentarInformacoes()
        {
            lblInformacaoSaldo.Text = "Os saldos exibidos são calculados com base nos lançamentos registrados no sistema. O sistema não tem acesso direto " +
                          "ao saldo real do banco. Os valores apresentados aqui correspondem às entradas e saídas informadas " +
                          "pela sua empresa no sistema.";

            // Centraliza o texto dentro do label
            lblInformacaoSaldo.TextAlign = ContentAlignment.MiddleCenter;

            // Desativa o AutoSize para ajustar manualmente o tamanho do label
            lblInformacaoSaldo.AutoSize = false;

            // Define o tamanho e posicionamento do label
            lblInformacaoSaldo.Width = 550;  // Largura desejada
            lblInformacaoSaldo.Height = 60;  // Altura ajustada
            lblInformacaoSaldo.Anchor = AnchorStyles.Top;

            // Centraliza o label no form
            lblInformacaoSaldo.Left = (this.ClientSize.Width - lblInformacaoSaldo.Width) / 2;
        }
        
        private void ObterSaldo()
        {
            List<SaldoConta> listaSaldos = new List<SaldoConta>();

            decimal saldoDinheiro = caixaDAO.SelecionarSaldoPorSqlNativo("SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo FROM Caixa Tabela WHERE Tabela.FlagExcluido <> true AND Tabela.Concluido = true AND Tabela.ContaBancaria IS NULL AND Tabela.FormaPagamento = 1;\r\n");
            listaSaldos.Add(new SaldoConta("DINHEIRO", saldoDinheiro));

            //decimal saldoCheque = caixaDAO.SelecionarSaldoPorSqlNativo("SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo FROM Caixa Tabela WHERE Tabela.FlagExcluido <> true AND Tabela.Concluido = true AND Tabela.ContaBancaria IS NULL AND Tabela.FormaPagamento = 7;\r\n");
            //listaSaldos.Add(new SaldoConta("CHEQUE", saldoDinheiro));

            IList<ContaBancaria> listaContas = new List<ContaBancaria>();
            listaContas = contaBancariaController.selecionarTodasContas();
            if(listaContas.Count > 0)
            {
                foreach (ContaBancaria contaBancaria in listaContas)
                {
                    decimal saldoConta = caixaDAO.SelecionarSaldoPorSqlNativo(
                        "SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo " +
                        "FROM Caixa Tabela WHERE Tabela.FlagExcluido <> true AND Tabela.Concluido = true AND Tabela.ContaBancaria = " + contaBancaria.Id);

                    listaSaldos.Add(new SaldoConta(contaBancaria.Descricao, saldoConta));
                }
            }
            grid.DataSource = listaSaldos;
            GridSummary.PreencherSumario(grid, "Valor");
        }










        public class SaldoConta
        {
            public string Descricao { get; set; }
            public decimal Valor { get; set; }

            public SaldoConta(string descricao, decimal valor)
            {
                Descricao = descricao;
                Valor = valor;
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
