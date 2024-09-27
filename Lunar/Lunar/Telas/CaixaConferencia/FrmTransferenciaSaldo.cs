using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
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

namespace Lunar.Telas.CaixaConferencia
{
    public partial class FrmTransferenciaSaldo : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        ContaBancariaController contaBancariaController = new ContaBancariaController();
        CaixaDAO caixaDAO = new CaixaDAO();
        public FrmTransferenciaSaldo()
        {
            InitializeComponent();

            IList<ContaBancaria> listaContas = new List<ContaBancaria>();
            listaContas = contaBancariaController.selecionarTodasContas();

            comboOrigem.DataSource = new List<ContaBancaria>(listaContas);
            comboOrigem.DisplayMember = "Descricao";
            comboOrigem.ValueMember = "Id";

            comboDestino.DataSource = new List<ContaBancaria>(listaContas);
            comboDestino.DisplayMember = "Descricao";
            comboDestino.ValueMember = "Id";
        }

        private void comboOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ContaBancaria contaSelecionada = (ContaBancaria)comboOrigem.SelectedItem;
                decimal saldoConta = caixaDAO.SelecionarSaldoPorSqlNativo(
                    "SELECT SUM(CASE WHEN Tabela.Tipo = 'E' THEN Tabela.Valor WHEN Tabela.Tipo = 'S' THEN -Tabela.Valor ELSE 0 END) AS Saldo " +
                    "FROM Caixa Tabela WHERE Tabela.FlagExcluido <> true AND Tabela.Concluido = true AND Tabela.ContaBancaria = " + contaSelecionada.Id);
                
                txtSaldoAtual.Text = saldoConta.ToString("C");
            }
            catch
            {

            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validações iniciais
                if (comboOrigem.SelectedItem == null)
                {
                    GenericaDesktop.ShowAlerta("Selecione a conta de origem!");
                    return;
                }

                if (comboDestino.SelectedItem == null)
                {
                    GenericaDesktop.ShowAlerta("Selecione a conta de destino!");
                    return;
                }

                if (string.IsNullOrEmpty(txtValorTransferir.Text) || !decimal.TryParse(txtValorTransferir.Text, out decimal valorTransferir) || valorTransferir <= 0)
                {
                    GenericaDesktop.ShowAlerta("Insira um valor válido para transferir!");
                    return;
                }

                ContaBancaria contaOrigem = (ContaBancaria)comboOrigem.SelectedItem;
                ContaBancaria contaDestino = (ContaBancaria)comboDestino.SelectedItem;

                // Saída de valor (Conta Origem)
                Caixa caixaOrigem = new Caixa
                {
                    ContaBancaria = contaOrigem,
                    Conciliado = true,
                    Concluido = true,
                    DataLancamento = DateTime.Now,
                    Descricao = "TRANSFERÊNCIA ENVIADA PARA B." + contaDestino.Descricao + ": " + txtObservacoes.Text,
                    EmpresaFilial = Sessao.empresaFilialLogada,
                    FlagExcluido = false,
                    FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(new FormaPagamento { Id = 3 }),
                    IdOrigem = "",
                    Observacoes = txtObservacoes.Text,
                    Pessoa = null,
                    PlanoConta = null,
                    TabelaOrigem = "CAIXA",
                    Tipo = "S", // Saída
                    Usuario = Sessao.usuarioLogado,
                    Valor = valorTransferir
                };

                Controller.getInstance().salvar(caixaOrigem);

                // Entrada de valor (Conta Destino)
                Caixa caixaDestino = new Caixa
                {
                    ContaBancaria = contaDestino,
                    Conciliado = true,
                    Concluido = true,
                    DataLancamento = DateTime.Now,
                    Descricao = "TRANSFERÊNCIA RECEBIDA DO B." +contaOrigem.Descricao + ": " + txtObservacoes.Text,
                    EmpresaFilial = Sessao.empresaFilialLogada,
                    FlagExcluido = false,
                    FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(new FormaPagamento { Id = 3 }),
                    IdOrigem = "",
                    Observacoes = txtObservacoes.Text,
                    Pessoa = null,
                    PlanoConta = null,
                    TabelaOrigem = "CAIXA",
                    Tipo = "E", // Entrada
                    Usuario = Sessao.usuarioLogado,
                    Valor = valorTransferir
                };

                Controller.getInstance().salvar(caixaDestino);

                GenericaDesktop.ShowInfo("Transferência realizada com sucesso!");
                this.Close();
            }
            catch (Exception ex) 
            {
                GenericaDesktop.ShowAlerta("Ocorreu um erro durante a transferência: " + ex.Message);
            }
        }

        private void txtValorTransferir_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValorTransferir.Text, e);
        }
    }
}
