﻿using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Empresas;
using Lunar.Telas.Orcamentos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient.Memcached;
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
    public partial class FrmMovimentoCaixa : Form
    {
        public FrmMovimentoCaixa()
        {
            InitializeComponent();
            txtDataFinal.Text = DateTime.Now.ToShortDateString();
            txtDataInicial.Text = DateTime.Now.ToShortDateString();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisarUsuario();
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

        private void txtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = int.Parse(txtCodUsuario.Texts);
                    usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                    if (usuario != null)
                    {
                        txtUsuario.Texts = usuario.Login;
                        txtCodUsuario.Texts = usuario.Id.ToString();
                        txtPlanoConta.Focus();
                    }
                }
            }
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

        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                pesquisaEmpresa();
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

        private void pesquisar()
        {
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

            CaixaController caixaController = new CaixaController();
            IList<Caixa> listaCaixa = new List<Caixa>();
            listaCaixa = caixaController.selecionarCaixaPorSql(sql); ;

            if (listaCaixa.Count > 0)
            {
                sfDataPager1.DataSource = listaCaixa;
                sfDataPager1.PageSize = 10000;
                grid.DataSource = sfDataPager1.PagedSource;
                //foreach (Caixa caixa in listaCaixa)
                //{
                //string tipoValor = "ENTRADA";
                //if (caixa.Tipo.Equals("S"))
                //{
                //    tipoValor = "SAÍDA";
                //    caixa.Valor = -(caixa.Valor);
                //}

                //  dsCaixa.Caixa.AddCaixaRow(caixa.Id.ToString(), caixa.Descricao, caixa.Valor, tipoValor, caixa.DataLancamento.ToShortDateString(), caixa.FormaPagamento.Id.ToString() + " - " + caixa.FormaPagamento.Descricao, caixa.FormaPagamento.Descricao, "", "", "", int.Parse(caixa.OperadorCadastro));
                //}
            }
            else
            {
                GenericaDesktop.ShowAlerta("Nenhum registro encontrado");
            }
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            pesquisar();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja excluir a movimentação de caixa selecionado?"))
            {
                int i = 0;
                if (grid.SelectedItems.Count > 0)
                {
                    foreach (var selectedItem in grid.SelectedItems)
                    {
                        var caixa = selectedItem as Caixa;

                        //Ordem Serviço
                        if (caixa.TabelaOrigem.Equals("ORDEMSERVICO"))
                        {
                            OrdemServico ordemServico = new OrdemServico();
                            ordemServico.Id = int.Parse(caixa.IdOrigem);
                            ordemServico = (OrdemServico)OrdemServicoController.getInstance().selecionar(ordemServico);
                            if (ordemServico != null)
                            {
                                if (ordemServico.Id > 0)
                                {

                                    OrdemServicoPagamentoController ordemServicoPagamentoController = new OrdemServicoPagamentoController();
                                    IList<OrdemServicoPagamento> listaPagamentoOS = ordemServicoPagamentoController.selecionarPagamentoPorOrdemServico(ordemServico.Id);
                                    if (listaPagamentoOS.Count > 0)
                                    {
                                        if (GenericaDesktop.ShowConfirmacao("A Ordem de Serviço " + ordemServico.Id.ToString() + " possui " + listaPagamentoOS.Count + " recebimento(s), deseja excluir?"))
                                        {
                                            foreach (OrdemServicoPagamento ordemServicoPagamento in listaPagamentoOS)
                                            {
                                                Controller.getInstance().excluir(ordemServicoPagamento);
                                                if (ordemServicoPagamento.FormaPagamento.Id == 8)
                                                {
                                                    CreditoCliente creditoCliente = new CreditoCliente();
                                                    creditoCliente.Cliente = ordemServicoPagamento.OrdemServico.Cliente;
                                                    creditoCliente.DataUtilizacao = DateTime.Now;
                                                    creditoCliente.EmpresaFilial = Sessao.empresaFilialLogada;
                                                    creditoCliente.Origem = "EXCLUSAO PAGAMENTO O.S " + ordemServico.Id.ToString();
                                                    creditoCliente.Valor = ordemServicoPagamento.ValorRecebido;
                                                    creditoCliente.ValorUtilizado = 0;
                                                    Controller.getInstance().salvar(creditoCliente);
                                                }
                                            }
                                            ordemServico.Status = "ABERTA";
                                            Controller.getInstance().salvar(ordemServico);
                                            CaixaController caixaController = new CaixaController();
                                            IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorSql("From Caixa Tabela Where Tabela.FlagExcluido <> true and Tabela.TabelaOrigem = 'OrdemServico' and Tabela.IdOrigem = " + ordemServico.Id);
                                            if (listaCaixa.Count > 0)
                                            {
                                                foreach (Caixa caixa1 in listaCaixa)
                                                {
                                                    Controller.getInstance().excluir(caixa1);
                                                }
                                            }
                                            GenericaDesktop.ShowInfo("Pagamento da Ordem de Serviço " + ordemServico.Id.ToString() + " excluído com Sucesso, ela foi reaberta!");
                                            btnPesquisar.PerformClick();
                                        }
                                    }
                                }
                            }
                        }

                        //CONTA A RECEBER
                        if (caixa.TabelaOrigem.Equals("CONTARECEBER"))
                        {
                            List<string> idReceber = new List<string>();
                            string original = caixa.IdOrigem;
                            char[] delimitadores = new char[] { '-' };
                            string[] strings = original.Split(delimitadores);
                            foreach (string s in strings)
                            {
                                idReceber.Add(s);
                            }
                            foreach (String st in idReceber)
                            {
                                ContaReceber contaReceber = new ContaReceber();
                                contaReceber.Id = int.Parse(st);
                                contaReceber = (ContaReceber)OrdemServicoController.getInstance().selecionar(contaReceber);
                                if (contaReceber != null)
                                {
                                    if (contaReceber.Id > 0)
                                    {
                                        if (contaReceber.ValorRecebimentoParcial == 0)
                                        {
                                            contaReceber.Recebido = false;
                                            contaReceber.ValorRecebido = 0;
                                            Controller.getInstance().salvar(contaReceber);

                                            CaixaController caixaController = new CaixaController();
                                            IList<Caixa> listaCaixa = caixaController.selecionarCaixaPorSql("From Caixa Tabela Where Tabela.FlagExcluido <> true and Tabela.TabelaOrigem = 'CONTARECEBER' and Tabela.IdOrigem = '" + original + "' and Tabela.Pessoa = " + contaReceber.Cliente.Id);
                                            if (listaCaixa.Count > 0)
                                            {
                                                foreach (Caixa caixa1 in listaCaixa)
                                                {
                                                    Controller.getInstance().excluir(caixa1);
                                                }
                                            }
                                        }
                                        else
                                            GenericaDesktop.ShowErro("Não é possível excluir caixa de parcela que possui recebimento parcial!");
                                    }
                                }
                            }
                            GenericaDesktop.ShowInfo("Movimentação Excluída com Sucesso!");
                        }

                        //DESPESA
                        if (caixa.TabelaOrigem.Equals("LANCAMENTODESPESA"))
                        {
                            Controller.getInstance().excluir(caixa);
                            GenericaDesktop.ShowInfo("Movimentação Excluída com Sucesso!");
                        }
                        //RECEITA
                        if (caixa.TabelaOrigem.Equals("LANCAMENTORECEITA"))
                        {
                            Controller.getInstance().excluir(caixa);
                            GenericaDesktop.ShowInfo("Movimentação Excluída com Sucesso!");
                        }
                    }
                    btnPesquisar.PerformClick();
                }
                else
                    GenericaDesktop.ShowAlerta("Selecione as contas que deseja excluir!");
            }
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;

            try
            {
                if ((e.RowData as Caixa) != null)
                {
                    if ((e.RowData as Caixa).Tipo == "S")
                    {
                        e.Style.TextColor = Color.Red;
                    }
                    else
                    {
                        e.Style.TextColor = Color.Blue;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnExcluir2_Click(object sender, EventArgs e)
        {
            btnExcluir.PerformClick();
        }

        private void btnDespesa_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmLancamentoCaixa uu = new FrmLancamentoCaixa("SAIDA");
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
            pesquisar();
        }

        private void btnReceita_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmLancamentoCaixa uu = new FrmLancamentoCaixa("ENTRADA");
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
            pesquisar();
        }
    }
}