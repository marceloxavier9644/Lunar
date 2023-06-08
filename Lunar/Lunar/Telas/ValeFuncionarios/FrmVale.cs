using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Estoques;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.ValeFuncionarios
{
    public partial class FrmVale : Form
    {
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        public FrmVale()
        {
            InitializeComponent();
            txtDataMovimento.Value = DateTime.Now;
            string primeiroDiaMes = string.Concat("01/", DateTime.Now.AddMonths(1).ToString("MM/yyyy"));
            txtDataVencimento.Value = DateTime.Parse(primeiroDiaMes);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodFuncionario.Texts))
                {
                    PlanoConta planoConta = new PlanoConta();
                    Pessoa pessoa = new Pessoa();
                    pessoa.Id = int.Parse(txtCodFuncionario.Texts);
                    pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                    if (pessoa != null)
                    {
                        if (pessoa.Id > 0)
                        {
                            Caixa caixa = new Caixa();
                            caixa.Cobrador = null;
                            caixa.Conciliado = true;
                            caixa.Concluido = true;
                            caixa.ContaBancaria = null;
                            caixa.DataLancamento = DateTime.Parse(txtDataMovimento.Value.ToString());
                            caixa.TabelaOrigem = "VALE";
                            caixa.Tipo = "S";
                            caixa.Usuario = Sessao.usuarioLogado;
                            caixa.Valor = decimal.Parse(txtValor.Texts);
                            caixa.Descricao = txtObservacoes.Texts;
                            if (String.IsNullOrEmpty(caixa.Descricao))
                                caixa.Descricao = "VALE FUNCIONÁRIO: " + pessoa.RazaoSocial;
                            caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                            caixa.Pessoa = pessoa;
                            FormaPagamento formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 1;
                            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            caixa.FormaPagamento = formaPagamento;
                            if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
                            {
                                planoConta = new PlanoConta();
                                planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                                if (planoConta != null)
                                    caixa.PlanoConta = planoConta;
                                else
                                    caixa.PlanoConta = null;
                            }
                            else
                                caixa.PlanoConta = null;

                            

                            ContaReceber contaReceber = new ContaReceber();
                            contaReceber.AcrescimoRecebidoBaixa = 0;
                            contaReceber.CaixaRecebimento = "";
                            contaReceber.Cliente = pessoa;
                            contaReceber.CnpjCliente = pessoa.Cnpj;
                            contaReceber.Concluido = true;
                            contaReceber.Data = DateTime.Parse(txtDataMovimento.Value.ToString());
                            contaReceber.DataRecebimento = DateTime.Parse("0001-01-01 00:00:00");
                            contaReceber.DescontoRecebidoBaixa = 0;
                            contaReceber.Descricao = "ADIANTAMENTO FUNCIONÁRIO (VALE)";
                            contaReceber.DescricaoRecebimento = "";
                            Random randNum = new Random();
                            string num = randNum.Next(1000, 1000000).ToString();
                            contaReceber.Documento = "VALE_R" + num;
                            contaReceber.EmpresaFilial = Sessao.empresaFilialLogada;
                            contaReceber.EnderecoCliente = "";
                            formaPagamento = new FormaPagamento();
                            formaPagamento.Id = 6;
                            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                            contaReceber.FormaPagamento = formaPagamento;
                            contaReceber.Juro = 0;
                            contaReceber.Multa = 0;
                            contaReceber.NomeCliente = pessoa.RazaoSocial;
                            contaReceber.OrdemServico = null;
                            contaReceber.Origem = "VALE";
                            contaReceber.Parcela = "1";
                            contaReceber.PlanoConta = null;
                            if (planoConta!= null)
                            {
                                if(planoConta.Id > 0)
                                {
                                    contaReceber.PlanoConta = planoConta;
                                }
                            }
                            contaReceber.Recebido = false;
                            contaReceber.ValorParcela = decimal.Parse(txtValor.Texts);
                            contaReceber.ValorRecebido = 0;
                            contaReceber.ValorRecebimentoParcial = 0;
                            contaReceber.ValorTotal = decimal.Parse(txtValor.Texts);
                            contaReceber.ValorTotalOrigem = decimal.Parse(txtValor.Texts);
                            contaReceber.Vencimento = DateTime.Parse(txtDataVencimento.Value.ToString());
                            contaReceber.Venda = null;
                            contaReceber.VendaFormaPagamento = null;
                            caixa.IdOrigem = contaReceber.Documento;
                            Controller.getInstance().salvar(caixa);
                            Controller.getInstance().salvar(contaReceber);

                            GenericaDesktop.ShowInfo("Registrado com Sucesso!");
                            ImprimirRecibo(caixa);
                            this.Close();

                        }
                        else
                            GenericaDesktop.ShowAlerta("Selecione um funcionário corretamente!");
                    }
                    else
                        GenericaDesktop.ShowAlerta("Selecione um funcionário corretamente!");
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione um funcionário corretamente!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }
        private void ImprimirRecibo(Caixa caixa)
        {
            Form formBackground = new Form();
            FrmImpressaoVale uu = new FrmImpressaoVale(caixa);
            formBackground.StartPosition = FormStartPosition.Manual;
            //formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            //formBackground.Left = Top = 0;
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
        }

        private void btnPesquisaPlanoConta_Click(object sender, EventArgs e)
        {
            Object contaOjeto = new PlanoConta();
            txtPlanoConta.Texts = "";
            txtCodPlanoConta.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", "and Tabela.Descricao like '%" + txtPlanoConta.Texts + "%'"))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("PlanoConta", "", ref contaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoConta.Texts = ((PlanoConta)contaOjeto).Descricao;
                            txtCodPlanoConta.Texts = ((PlanoConta)contaOjeto).Id.ToString();
                            break;
                    }

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void btnPesquisaFuncionario_Click(object sender, EventArgs e)
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and Tabela.RazaoSocial like '%"+txtFuncionario.Texts+"%' and (Tabela.Funcionario = true or Tabela.Cobrador = true or Tabela.Vendedor = true) "))
                {
                    txtCodFuncionario.Texts = "";
                    txtFuncionario.Texts = "";
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    //formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("Pessoa", "and Tabela.RazaoSocial like '" + txtFuncionario.Texts + "' and (Tabela.Funcionario = true or Tabela.Cobrador = true or Tabela.Vendedor = true) ", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtFuncionario.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodFuncionario.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                txtDataMovimento.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtFuncionario.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodFuncionario.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtDataMovimento.Focus();
                            if (((Pessoa)pessoaOjeto).RegistradoSpc == true)
                            {
                                GenericaDesktop.ShowAlerta("Cliente com marcação de registro no SPC/Serasa no cadastro!");
                            }
                            if (((Pessoa)pessoaOjeto).EscritorioCobranca == true)
                            {
                                GenericaDesktop.ShowAlerta("Cliente com marcação de cobrança externa!");
                            }
                            txtObservacoes.Texts = "VALE FUNCIONARIO: " + (((Pessoa)pessoaOjeto).RazaoSocial);
                            break;
                    }
                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void txtFuncionario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaFuncionario.PerformClick();
            }
        }

        private void txtCodFuncionario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    Pessoa pessoa = new Pessoa();
                    if (!String.IsNullOrEmpty(txtCodFuncionario.Texts))
                    {
                        pessoa.Id = int.Parse(txtCodFuncionario.Texts);
                        pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                        if (pessoa != null)
                        {
                            if(pessoa.Id > 0)
                            {

                            }
                            if (pessoa.Funcionario == true || pessoa.Vendedor == true)
                            {
                                txtFuncionario.Texts = pessoa.RazaoSocial;
                                txtCodFuncionario.Texts = pessoa.Id.ToString();
                                if (pessoa.EscritorioCobranca == true)
                                    GenericaDesktop.ShowAlerta("Cliente Marcado que possui parcela em escritório de cobrança!");
                                if (pessoa.RegistradoSpc == true)
                                    GenericaDesktop.ShowAlerta("Cliente marcado que está registrado no SPC/Serasa pela sua empresa!");
                                txtDataMovimento.Focus();
                            }
                            else
                                GenericaDesktop.ShowAlerta("Este código não é de um funcionário!");
                        }
                        else
                        {

                            txtFuncionario.Texts = "";
                            txtCodFuncionario.Texts = "";
                        }
                    }
                }
                catch (Exception erro)
                {
                    GenericaDesktop.ShowAlerta(erro.Message);
                }
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValor.Texts, e);
            if (e.KeyChar == 13)
            {
                btnPesquisaPlanoConta.Focus();
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try {
                txtValor.Texts = string.Format("{0:0.00}", decimal.Parse(txtValor.Texts));
            }
            catch
            {
                GenericaDesktop.ShowErro("Valor inválido!");
                txtValor.Focus();
            }
        }
    }
}
