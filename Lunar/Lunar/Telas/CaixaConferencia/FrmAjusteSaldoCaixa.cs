using Lunar.Telas.CaixaConferencia.ClassesAuxiliaresCaixa;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.CaixaConferencia
{
    public partial class FrmAjusteSaldoCaixa : Form
    {
        CaixaDAO caixaDAO = new CaixaDAO();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        decimal valorCaixaAtual = 0;
        decimal valorAjustar = 0;
        string tipoMovimento = "";
        FormaPagamento formaPagamento = new FormaPagamento();
        CaixaAbertura caixaAbertura = new CaixaAbertura();
        public FrmAjusteSaldoCaixa(CaixaAbertura caixaAbertura)
        {
            InitializeComponent();
            this.caixaAbertura = caixaAbertura;
        }

        private bool verificaSeOCaixaEHoje()
        {
            if (this.caixaAbertura.DataAbertura.Date == DateTime.Now.Date)
                return true;
            else
                return false;
        }
        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            if (verificaSeOCaixaEHoje() != true)
            {
                if(!GenericaDesktop.ShowConfirmacao("Ao atualizar o saldo do caixa na data selecionada, o sistema ajustará automaticamente os saldos dos caixas posteriores. Isso significa que todos os caixas após essa data serão afetados pela alteração, refletindo o novo saldo a partir do caixa que está sendo atualizado."))
                    return;
            }
                
            if (!String.IsNullOrEmpty(txtSaldoInicial.Text))
            {
                decimal valorUsuario = decimal.Parse(txtSaldoInicial.Text);
                valorAjustar = valorCaixaAtual - valorUsuario;
                
                if (valorAjustar < 0)
                {
                    valorAjustar = Math.Abs(valorAjustar);
                    Caixa caixa = new Caixa();
                    caixa.Pessoa = null;
                    caixa.FormaPagamento = formaPagamento;
                    caixa.DataLancamento = caixaAbertura.DataAbertura;
                    caixa.ContaBancaria = null;
                    caixa.Descricao = "AJUSTE SALDO PELO USUÁRIO: " + Sessao.usuarioLogado.Login;
                    caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                    caixa.IdOrigem = "";
                    caixa.TabelaOrigem = "CAIXA";
                    caixa.Cobrador = null;
                    caixa.Valor = valorAjustar;
                    caixa.Conciliado = true;
                    caixa.Concluido = true;
                    caixa.PlanoConta = Sessao.parametroSistema.PlanoContaAjusteCaixaSaida;
                    caixa.Observacoes = txtObservacoes.Text;
                    caixa.Tipo = "E";
                    caixa.Usuario = Sessao.usuarioLogado;
                    Controller.getInstance().salvar(caixa);
                    GenericaDesktop.ShowInfo("Caixa Ajustado com Sucesso! (+)");
                    this.Close();
                }
                else if (valorAjustar > 0)
                {
                    Caixa caixa = new Caixa();
                    caixa.Pessoa = null;
                    caixa.FormaPagamento = formaPagamento;
                    caixa.DataLancamento = caixaAbertura.DataAbertura;
                    caixa.ContaBancaria = null;
                    caixa.Descricao = "AJUSTE SALDO PELO USUÁRIO: " + Sessao.usuarioLogado.Login;
                    caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                    caixa.IdOrigem = "";
                    caixa.TabelaOrigem = "CAIXA";
                    caixa.Cobrador = null;
                    caixa.Valor = valorAjustar;
                    caixa.Conciliado = true;
                    caixa.Concluido = true;
                    caixa.PlanoConta = Sessao.parametroSistema.PlanoContaAjusteCaixaSaida;
                    caixa.Observacoes = txtObservacoes.Text;
                    caixa.Tipo = "S";
                    caixa.Usuario = Sessao.usuarioLogado;
                    Controller.getInstance().salvar(caixa);
                    GenericaDesktop.ShowInfo("Caixa Ajustado com Sucesso! (-)");
                    this.Close();
                }
                else
                {
                    GenericaDesktop.ShowInfo("Você informou o mesmo valor do seu caixa atual, não é necessário ajustar!");
                    this.Close();
                }
            }

        }

        private void txtSaldoInicial_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txtSaldoInicial.Text);
                txtSaldoInicial.Text = valor.ToString("N2");
            }
            catch
            {

            }
        }

        private void txtSaldoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtSaldoInicial.Text, e);
        }
        private void calcularSaldoInicial()
        {
            decimal saldo = 0;

            CaixaService caixaService = new CaixaService();
            saldo = caixaService.ObterSaldo(caixaAbertura.DataAbertura, Sessao.usuarioLogado.Id);
            valorCaixaAtual = saldo;
        }

        private void FrmAjusteSaldoCaixa_Load(object sender, EventArgs e)
        {
            calcularSaldoInicial();
            formaPagamento.Id = 1;
            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);

        }

        private void FrmAjusteSaldoCaixa_KeyDown(object sender, KeyEventArgs e)
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
