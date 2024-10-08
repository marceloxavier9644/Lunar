using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Empresas;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.CaixaConferencia.Reports
{
    public partial class FrmRelatorioCaixa : Form
    {
        public FrmRelatorioCaixa()
        {
            InitializeComponent();
            txtEmpresa.Texts = Sessao.empresaFilialLogada.RazaoSocial;
            txtCodEmpresa.Texts = Sessao.empresaFilialLogada.Id.ToString();
            txtDataFinal.Value = DateTime.Now;
            txtDataInicial.Value = DateTime.Now;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRelatorioCaixa_Load(object sender, EventArgs e)
        {

        }

        private void gerarRelatorio()
        {
            try
            {
                dsCaixa.Tables[0].Clear();
                this.reportViewer1.RefreshReport();
                Usuario usuario = new Usuario();
                PlanoConta planoConta = new PlanoConta();
                ContaBancaria contaBancaria = new ContaBancaria();
                String conta = "";
                String user = "";
                String plano = "";

                string sql = "From Caixa Tabela where Tabela.FlagExcluido <> true and Tabela.Concluido = true ";
                DateTime dataInicial = DateTime.Parse(txtDataInicial.Value.ToString());
                DateTime dataFinal = DateTime.Parse(txtDataFinal.Value.ToString());
                sql = sql + "and Tabela.DataLancamento between '" + dataInicial.ToString("yyyy-MM-dd") + " 00:00:00' and '" + dataFinal.ToString("yyyy-MM-dd") + " 23:59:59' ";
                if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
                {
                    planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                    planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                    if (planoConta != null)
                    {
                        plano = "PLANO DE CONTAS: " + planoConta.Descricao;
                        sql = sql + "and Tabela.PlanoConta = " + txtCodPlanoConta.Texts + " ";
                    }
                }
                if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
                {
                    usuario.Id = int.Parse(txtCodUsuario.Texts);
                    usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                    if (usuario != null)
                    {
                        user = "CAIXA: " + usuario.Login + "  ";
                        sql = sql + "and Tabela.Usuario = " + txtCodUsuario.Texts + " ";
                    }
                }
                if (!String.IsNullOrEmpty(txtCodContaBancaria.Texts))
                {
                    contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts);
                    contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                    if (contaBancaria != null)
                    {
                        conta = "CONTA BANCÁRIA: " + contaBancaria.Descricao + "  " + contaBancaria.Conta + "-" + contaBancaria.DvConta;
                        sql = sql + "and Tabela.ContaBancaria = " + txtCodContaBancaria.Texts + " ";
                    }
                }
                if (!String.IsNullOrEmpty(txtCodEmpresa.Texts))
                {
                    sql = sql + "and Tabela.EmpresaFilial = " + txtCodEmpresa.Texts + " ";
                }
                if (chkApenasCaixaFisico.Checked == true)
                {
                    sql = sql + "and Tabela.ContaBancaria is null ";
                }
                if (chkApenasContasReceber.Checked == true)
                {
                    sql = sql + "and Tabela.TabelaOrigem = 'CONTARECEBER' ";
                }
                if (chkApenasDespesas.Checked == true)
                {
                    sql = sql + "and Tabela.Tipo = 'S' and Tabela.TabelaOrigem <> 'DEPOSITO_BANCARIO' ";
                }
                if (chkApenasReceitas.Checked == true)
                {
                    sql = sql + "and Tabela.Tipo = 'E' and Tabela.TabelaOrigem <> 'DEPOSITO_BANCARIO' ";
                }
                if (!String.IsNullOrEmpty(txtCodCobrador.Texts))
                    sql = sql + "and Tabela.Cobrador = " + txtCodCobrador.Texts + " ";
                Microsoft.Reporting.WinForms.ReportDataSource dsOrdem = new Microsoft.Reporting.WinForms.ReportDataSource();
                //MessageBox.Show(sql);
                dsOrdem.Name = "dsCaixa";
                dsOrdem.Value = this.bindingSource1;
                this.reportViewer1.LocalReport.DataSources.Add(dsOrdem);

                string cobradorSelecionado = "";
                if (!String.IsNullOrEmpty(txtCodCobrador.Texts))
                    cobradorSelecionado = "RECEBIDO POR: " + txtCobrador.Texts;
                ReportParameter[] p = new ReportParameter[5];
                p[0] = (new ReportParameter("Empresa", Sessao.empresaFilialLogada.NomeFantasia));
                p[1] = (new ReportParameter("Data", DateTime.Parse(txtDataInicial.Value.ToString()).ToShortDateString() + " a " + DateTime.Parse(txtDataFinal.Value.ToString()).ToShortDateString() + "\n" + cobradorSelecionado));
                p[2] = (new ReportParameter("Usuario", user + " "));
                p[3] = (new ReportParameter("PlanoConta", plano));
                p[4] = (new ReportParameter("ContaBancaria", conta));
                reportViewer1.LocalReport.SetParameters(p);

                CaixaController caixaController = new CaixaController();
                IList<Caixa> listaCaixa = new List<Caixa>();
                listaCaixa = caixaController.selecionarCaixaPorSql(sql); ;

                //troco fixo
                TrocoFixoController trocoFixoController = new TrocoFixoController();
                IList<TrocoFixo> listaTroco = trocoFixoController.selecionarTodosTrocoFixoPorEmpresaFilial();
                decimal valorTroco = 0;
                if (listaTroco != null)
                {
                    if (listaTroco.Count > 0)
                    {
                        foreach (TrocoFixo troco in listaTroco)
                        {
                            if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
                            {
                                if (troco.Usuario.Id == int.Parse(txtCodUsuario.Texts))
                                {
                                    valorTroco = valorTroco + troco.Valor;
                                }
                            }
                            else
                            {
                                valorTroco = valorTroco + troco.Valor;
                            }
                        }
                        Caixa caixa = new Caixa();
                        caixa.Id = 1;
                        caixa.Cobrador = null;
                        caixa.Conciliado = true;
                        caixa.Concluido = true;
                        caixa.ContaBancaria = null;
                        caixa.DataLancamento = DateTime.Now;
                        caixa.Descricao = "TROCO FIXO";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FlagExcluido = false;
                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = 1;
                        formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        caixa.FormaPagamento = formaPagamento;
                        caixa.FormaPagamento = formaPagamento;
                        caixa.IdOrigem = "";
                        caixa.Pessoa = null;
                        caixa.PlanoConta = null;
                        caixa.TabelaOrigem = "TROCOFIXO";
                        caixa.Tipo = "E";
                        caixa.Usuario = null;
                        caixa.Valor = valorTroco;
                        caixa.OperadorCadastro = "1";
                        listaCaixa.Add(caixa);
                    }
                }

                //listaCaixa = caixaController.selecionarCaixaPorUsuarioEDataCadastro(Sessao.usuarioLogado.Id, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")); ;
                if (listaCaixa.Count > 0)
                {
                    foreach (Caixa caixa in listaCaixa)
                    {
                        string tipoValor = "ENTRADA";
                        if (caixa.Tipo.Equals("S"))
                        {
                            tipoValor = "SAÍDA";
                            caixa.Valor = -(caixa.Valor);
                        }

                        dsCaixa.Caixa.AddCaixaRow(caixa.Id.ToString(), caixa.Descricao, caixa.Valor, tipoValor, caixa.DataLancamento.ToShortDateString(), caixa.FormaPagamento.Id.ToString() + " - " + caixa.FormaPagamento.Descricao, caixa.FormaPagamento.Descricao, "", "", "", int.Parse(caixa.OperadorCadastro));
                    }
                    this.reportViewer1.Visible = true;
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    this.reportViewer1.Visible = false;
                    GenericaDesktop.ShowAlerta("Nenhum registro encontrado");
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void btnPesquisaUsuario_Click(object sender, EventArgs e)
        {
            Object objeto = new Usuario();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Usuario", "and Tabela.Id <> 1"))
                {
                    txtCodUsuario.Texts = "";
                    txtUsuario.Texts = "";
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.Left = Top = 0;
                    formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = false;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();
                    uu.Owner = formBackground;
                    switch (uu.showModal("Usuario", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmUsuarioCadastro form = new FrmUsuarioCadastro();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtUsuario.Texts = ((Usuario)objeto).Login;
                                txtCodUsuario.Texts = ((Usuario)objeto).Id.ToString();
                                txtPlanoConta.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtUsuario.Texts = ((Usuario)objeto).Login;
                            txtCodUsuario.Texts = ((Usuario)objeto).Id.ToString();
                            txtPlanoConta.Focus();
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

        private void pesquisarUsuario()
        {
            Object objeto = new Usuario();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Usuario", "and CONCAT(Tabela.Id, ' ', Tabela.Login, ' ', Tabela.Email) like '%" + txtUsuario.Texts + "%' and Tabela.Id <> 1"))
                {
                    txtCodUsuario.Texts = "";
                    txtUsuario.Texts = "";
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
                    switch (uu.showModal("Usuario", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmUsuarioCadastro form = new FrmUsuarioCadastro();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtUsuario.Texts = ((Usuario)objeto).Login;
                                txtCodUsuario.Texts = ((Usuario)objeto).Id.ToString();
                                txtPlanoConta.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtUsuario.Texts = ((Usuario)objeto).Login;
                            txtCodUsuario.Texts = ((Usuario)objeto).Id.ToString();
                            txtPlanoConta.Focus();
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

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                pesquisarUsuario();
            }
        }

        private void txtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = int.Parse(txtCodUsuario.Texts);
                    usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                    if(usuario != null)
                    {
                        txtUsuario.Texts = usuario.Login;
                        txtCodUsuario.Texts = usuario.Id.ToString();
                        txtPlanoConta.Focus();
                    }
                }
            }
        }

        private void btnPesquisaEmpresa_Click(object sender, EventArgs e)
        {
            Object objeto = new EmpresaFilial();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("EmpresaFilial", ""))
                {
                    txtCodEmpresa.Texts = "";
                    txtEmpresa.Texts = "";
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
                    switch (uu.showModal("EmpresaFilial", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmCadastroEmpresas form = new FrmCadastroEmpresas();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                                txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                            txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
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

        private void pesquisaEmpresa()
        {
            Object objeto = new EmpresaFilial();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("EmpresaFilial", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.NomeFantasia, ' ', Tabela.Cnpj) like '%" + txtEmpresa.Texts + "%'"))
                {
                    txtCodEmpresa.Texts = "";
                    txtEmpresa.Texts = "";
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
                    switch (uu.showModal("EmpresaFilial", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmCadastroEmpresas form = new FrmCadastroEmpresas();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                                txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                            txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
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

        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                pesquisaEmpresa();
        }

        private void txtCodEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtCodEmpresa.Texts))
                {
                    EmpresaFilial empresaFilial = new EmpresaFilial();
                    empresaFilial.Id = int.Parse(txtCodEmpresa.Texts);
                    empresaFilial = (EmpresaFilial)Controller.getInstance().selecionar(empresaFilial);
                    if (empresaFilial != null)
                    {
                        txtEmpresa.Texts = empresaFilial.RazaoSocial;
                        txtCodEmpresa.Texts = empresaFilial.Id.ToString();
                    }
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            gerarRelatorio();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            gerarRelatorio();
        }

        private void btnPesquisaPlanoConta_Click(object sender, EventArgs e)
        {
            Object objeto = new PlanoConta();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", ""))
                {
                    txtCodPlanoConta.Texts = "";
                    txtPlanoConta.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmPlano form = new FrmCadastroEmpresas();
                            //if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            //{
                            //    txtEmpresa.Texts = ((EmpresaFilial)objeto).RazaoSocial;
                            //    txtCodEmpresa.Texts = ((EmpresaFilial)objeto).Id.ToString();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtPlanoConta.Texts = ((PlanoConta)objeto).Descricao;
                            txtCodPlanoConta.Texts = ((PlanoConta)objeto).Id.ToString();
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

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Object objeto = new ContaBancaria();
         
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancaria.Texts = "";
                    txtContaBancaria.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
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

        private void chkApenasCaixaFisico_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkApenasCaixaFisico.Checked == true)
                {
                    txtCodContaBancaria.Texts = "";
                    txtContaBancaria.Texts = "";
                    gerarRelatorio();
                }
                else if (chkApenasCaixaFisico.Checked == false)
                    gerarRelatorio();
            }
            catch
            {

            }
        }

        private void chkApenasContasReceber_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkApenasContasReceber.Checked == true)
                {
                    chkApenasCaixaFisico.Checked = false;
                    gerarRelatorio();
                }
                else if (chkApenasContasReceber.Checked == false)
                    gerarRelatorio();
            }
            catch
            {

            }
        }

        private void btnPesquisaCobrador_Click(object sender, EventArgs e)
        {
            txtCobrador.Texts = "";
            txtCodCobrador.Texts = "";
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and Tabela.Cobrador = true"))
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
                    switch (uu.showModal("Pessoa", "and Tabela.Cobrador = true", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtCobrador.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCobrador.Texts = ((Pessoa)pessoaObj).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCobrador.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCobrador.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            gerarRelatorio();
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

        private void txtCobrador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtCobrador.Texts))
                {
                    Object pessoaOjeto = new Pessoa();
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and Tabela.Cobrador = true and Tabela.RazaoSocial like '%"+txtCobrador.Texts+"%'"))
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
                            switch (uu.showModal("Pessoa", "and Tabela.Cobrador = true and Tabela.RazaoSocial like '%" + txtCobrador.Texts + "%'", ref pessoaOjeto))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    FrmClienteCadastro form = new FrmClienteCadastro();
                                    Object pessoaObj = new Pessoa();
                                    if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                                    {
                                        txtCobrador.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                        txtCodCobrador.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                    }
                                    form.Dispose();
                                    break;
                                case DialogResult.OK:
                                    txtCobrador.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                    txtCodCobrador.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                    gerarRelatorio();
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
            }
        }

        private void txtCodCobrador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodCobrador.Texts))
                    {
                        Pessoa pessoaCobrador = new Pessoa();
                        pessoaCobrador.Id = int.Parse(txtCodCobrador.Texts);
                        pessoaCobrador = (Pessoa)PessoaController.getInstance().selecionar(pessoaCobrador);
                        if (pessoaCobrador != null)
                        {
                            if (pessoaCobrador.Id > 0)
                            {
                                txtCobrador.Texts = pessoaCobrador.RazaoSocial;
                                gerarRelatorio();
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Texts = "";
            txtCodUsuario.Texts = "";
            txtCodPlanoConta.Texts = "";
            txtPlanoConta.Texts = "";
            txtCodContaBancaria.Texts = "";
            txtContaBancaria.Texts = "";
            txtCodCobrador.Texts = "";
            txtCobrador.Texts = "";
            chkApenasCaixaFisico.Checked = true;
            chkApenasContasReceber.Checked = false;
            //txtCodEmpresa.Texts = "";
           //txtEmpresa.Texts = "";
        }

        private void chkApenasDespesas_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkApenasDespesas.Checked == true)
                {
                    chkApenasCaixaFisico.Checked = false;
                    gerarRelatorio();
                }
                else if (chkApenasDespesas.Checked == false)
                    gerarRelatorio();
            }
            catch
            {

            }
        }
    }
}
