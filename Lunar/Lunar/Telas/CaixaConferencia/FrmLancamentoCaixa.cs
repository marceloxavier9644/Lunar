using Lunar.Telas.Cadastros.Financeiro.PlanoContas.PlanosPorGrupos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.UsuarioRegistro;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.SharePoint.Client.WebParts;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.CaixaConferencia
{
    public partial class FrmLancamentoCaixa : Form
    {
        bool passou = false;
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        string tipoLancamento = "";
        public FrmLancamentoCaixa(String tipoLancamento)
        {
            InitializeComponent();
            txtUsuario.Texts = Sessao.usuarioLogado.Login;
            txtCodUsuario.Texts = Sessao.usuarioLogado.Id.ToString();
            txtDataMovimento.Text = DateTime.Now.ToShortDateString();
            this.tipoLancamento = tipoLancamento;
            //if(tipoLancamento.Equals("ENTRADA"))
            if (tipoLancamento.Equals("SAIDA"))
                tabPageAdv1.Text = "Lançamento de Despesa";
            //if (tipoLancamento.Equals("DEPOSITO"))
        }

        private void btnPesquisaUsuario1_Click(object sender, EventArgs e)
        {
            Object usuarioOjeto = new Usuario();
            txtUsuario.Texts = "";
            txtCodUsuario.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Usuario", "and Tabela.Login like '%" + txtUsuario.Texts + "%' and Tabela.Id > 1"))
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
                    switch (uu.showModal("Usuario", "", ref usuarioOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmUsuarioCadastro form = new FrmUsuarioCadastro();
                            if (form.showModalNovo(ref usuarioOjeto) == DialogResult.OK)
                            {
                                txtUsuario.Texts = ((Usuario)usuarioOjeto).Login;
                                txtCodUsuario.Texts = ((Usuario)usuarioOjeto).Id.ToString();
                                txtConta.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtUsuario.Texts = ((Usuario)usuarioOjeto).Login;
                            txtCodUsuario.Texts = ((Usuario)usuarioOjeto).Id.ToString();
                            txtConta.Focus();
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
            if (e.KeyChar == 13)
            {
                Object usuarioOjeto = new Usuario();
                //txtUsuario.Texts = "";
                //txtCodUsuario.Texts = "";
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Usuario", "and Tabela.Login like '%" + txtUsuario.Texts + "%' and Tabela.Id > 1"))
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
                        switch (uu.showModal("Usuario", "", ref usuarioOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmUsuarioCadastro form = new FrmUsuarioCadastro();
                                if (form.showModalNovo(ref usuarioOjeto) == DialogResult.OK)
                                {
                                    txtUsuario.Texts = ((Usuario)usuarioOjeto).Login;
                                    txtCodUsuario.Texts = ((Usuario)usuarioOjeto).Id.ToString();
                                    txtConta.Focus();
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtUsuario.Texts = ((Usuario)usuarioOjeto).Login;
                                txtCodUsuario.Texts = ((Usuario)usuarioOjeto).Id.ToString();
                                txtConta.Focus();
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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumero(txtCodUsuario.Texts, e);
            if (e.KeyChar == 13)
            {
                txtConta.Focus();
            }
        }

        private void txtCodUsuario_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
            {
                Usuario usuario = new Usuario();
                usuario.Id = int.Parse(txtCodUsuario.Texts);
                usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                if (usuario != null)
                {
                    if (usuario.Id > 0)
                    {
                        txtUsuario.Texts = usuario.Login;
                        txtConta.Focus();
                    }
                }
            }
        }

        private void btnPesquisaConta_Click(object sender, EventArgs e)
        {
            Object contaOjeto = new ContaBancaria();
            txtConta.Texts = "";
            txtCodConta.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", "and CONCAT(Tabela.Conta, ' ', Tabela.Descricao) like '%" + txtConta.Texts + "%'"))
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
                    switch (uu.showModal("ContaBancaria", "", ref contaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            //FrmContaBancaria form = new FrmContaBancaria();
                            //if (form.showModalNovo(ref contaOjeto) == DialogResult.OK)
                            //{
                            //    txtConta.Texts = ((ContaBancaria)contaOjeto).Descricao;
                            //    txtCodConta.Texts = ((ContaBancaria)contaOjeto).Id.ToString();
                            //    txtDataMovimento.Focus();
                            //}
                            //form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtConta.Texts = ((ContaBancaria)contaOjeto).Descricao;
                            txtCodConta.Texts = ((ContaBancaria)contaOjeto).Id.ToString();
                            txtDataMovimento.Focus();
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

        private void txtConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Object contaOjeto = new ContaBancaria();
                //txtConta.Texts = "";
                //txtCodConta.Texts = "";
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", "and CONCAT(Tabela.Conta, ' ', Tabela.Descricao) like '%" + txtConta.Texts + "%'"))
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
                        switch (uu.showModal("ContaBancaria", "", ref contaOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                //FrmContaBancaria form = new FrmContaBancaria();
                                //if (form.showModalNovo(ref contaOjeto) == DialogResult.OK)
                                //{
                                //    txtConta.Texts = ((ContaBancaria)contaOjeto).Descricao;
                                //    txtCodConta.Texts = ((ContaBancaria)contaOjeto).Id.ToString();
                                //    txtDataMovimento.Focus();
                                //}
                                //form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtConta.Texts = ((ContaBancaria)contaOjeto).Descricao;
                                txtCodConta.Texts = ((ContaBancaria)contaOjeto).Id.ToString();
                                txtDataMovimento.Focus();
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

        private void txtCodConta_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodConta.Texts))
            {
                ContaBancaria contaBancaria = new ContaBancaria();
                contaBancaria.Id = int.Parse(txtCodConta.Texts);
                contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                if (contaBancaria != null)
                {
                    if (contaBancaria.Id > 0)
                    {
                        txtConta.Texts = contaBancaria.Descricao;
                        txtConta.Focus();
                    }
                }
            }
        }

        private void txtCodConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumero(txtCodConta.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDataMovimento.Focus();
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try 
            { 
                txtValor.Texts = string.Format("{0:0.00}", decimal.Parse(txtValor.Texts)); 
            } 
            catch
            {
                GenericaDesktop.ShowAlerta("Valor Inválido");
                txtValor.Texts = "0,00";
                txtValor.Select();
            }
        }

        private void FrmLancamentoCaixa_Load(object sender, EventArgs e)
        {
        
        }

        private void FrmLancamentoCaixa_Paint(object sender, PaintEventArgs e)
        {
            if (passou == false)
            {
                txtValor.TextAlign = HorizontalAlignment.Center;
                txtValor.Focus();
                passou = true;
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValor.Texts, e);
        }

        private void set_Despesa()
        {
            Caixa caixa = new Caixa();
            caixa.Conciliado = true;
            caixa.Concluido = true;
            caixa.DataLancamento = DateTime.Parse(txtDataMovimento.Value.ToString());
            caixa.Descricao = "LANÇAMENTO DE DESPESA: " + txtDescricaoResumida.Texts;
            caixa.EmpresaFilial = Sessao.empresaFilialLogada;

            FormaPagamento formaPagamento = new FormaPagamento();
            formaPagamento.Id = 1;
            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
            caixa.FormaPagamento = formaPagamento;

            if (!String.IsNullOrEmpty(txtCodConta.Texts))
            {
                ContaBancaria contaBancaria = new ContaBancaria();
                contaBancaria.Id = int.Parse(txtCodConta.Texts);
                contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                if (contaBancaria != null)
                    caixa.ContaBancaria = contaBancaria;
                else
                    caixa.ContaBancaria = null;
            }
            else
                caixa.ContaBancaria = null;

            caixa.IdOrigem = "";
            caixa.Pessoa = null;
            if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    caixa.PlanoConta = planoConta;
                else
                    caixa.PlanoConta = null;
            }
            else
                caixa.PlanoConta = null;
            caixa.TabelaOrigem = "LANCAMENTODESPESA";
            caixa.Tipo = "S";

            if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
            {
                Usuario usuario = new Usuario();
                usuario.Id = int.Parse(txtCodUsuario.Texts);
                usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                if (usuario != null)
                    caixa.Usuario = usuario;
                else
                    caixa.Usuario = Sessao.usuarioLogado;
            }
            else
                caixa.Usuario = Sessao.usuarioLogado;
            caixa.Valor = decimal.Parse(txtValor.Texts);
            Controller.getInstance().salvar(caixa);
            GenericaDesktop.ShowInfo("Lançamento Efetuado com Sucesso!");
            this.Close();
        }

        private void set_Receita()
        {
            Caixa caixa = new Caixa();
            caixa.Conciliado = true;
            caixa.Concluido = true;
            caixa.DataLancamento = DateTime.Parse(txtDataMovimento.Value.ToString());
            caixa.Descricao = "LANÇAMENTO DE RECEITA: " + txtDescricaoResumida.Texts;
            caixa.EmpresaFilial = Sessao.empresaFilialLogada;

            FormaPagamento formaPagamento = new FormaPagamento();
            formaPagamento.Id = 1;
            formaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
            caixa.FormaPagamento = formaPagamento;

            caixa.IdOrigem = "";
            caixa.Pessoa = null;
            if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                    caixa.PlanoConta = planoConta;
                else
                    caixa.PlanoConta = null;
            }
            else
                caixa.PlanoConta = null;

            if (!String.IsNullOrEmpty(txtCodConta.Texts))
            {
                ContaBancaria contaBancaria = new ContaBancaria();
                contaBancaria.Id = int.Parse(txtCodConta.Texts);
                contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                if (contaBancaria != null)
                    caixa.ContaBancaria = contaBancaria;
                else
                    caixa.ContaBancaria = null;
            }
            else
                caixa.ContaBancaria = null;

            caixa.TabelaOrigem = "LANCAMENTORECEITA";
            caixa.Tipo = "E";

            if (!String.IsNullOrEmpty(txtCodUsuario.Texts))
            {
                Usuario usuario = new Usuario();
                usuario.Id = int.Parse(txtCodUsuario.Texts);
                usuario = (Usuario)Controller.getInstance().selecionar(usuario);
                if (usuario != null)
                    caixa.Usuario = usuario;
                else
                    caixa.Usuario = Sessao.usuarioLogado;
            }
            else
                caixa.Usuario = Sessao.usuarioLogado;
            caixa.Valor = decimal.Parse(txtValor.Texts);
            Controller.getInstance().salvar(caixa);
            GenericaDesktop.ShowInfo("Lançamento Efetuado com Sucesso!");
            this.Close();
        }

        private void btnPesquisaPlanoConta_Click(object sender, EventArgs e)
        {
            Object contaOjeto = new PlanoConta();
            txtPlanoContas.Texts = "";
            txtCodPlanoConta.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", "and Tabela.Descricao like '%" + txtPlanoContas.Texts + "%'"))
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
                            txtPlanoContas.Texts = ((PlanoConta)contaOjeto).Descricao;
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

        private void txtPlanoContas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Object contaOjeto = new PlanoConta();
               // txtPlanoContas.Texts = "";
               // txtCodPlanoConta.Texts = "";
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PlanoConta", "and Tabela.Descricao like '%" + txtPlanoContas.Texts + "%'"))
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
                                txtPlanoContas.Texts = ((PlanoConta)contaOjeto).Descricao;
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
        }

        private void txtCodPlanoConta_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodPlanoConta.Texts))
            {
                PlanoConta planoConta = new PlanoConta();
                planoConta.Id = int.Parse(txtCodPlanoConta.Texts);
                planoConta = (PlanoConta)Controller.getInstance().selecionar(planoConta);
                if (planoConta != null)
                {
                    if (planoConta.Id > 0)
                    {
                        txtPlanoContas.Texts = planoConta.Descricao;
                    }
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (tipoLancamento.Equals("SAIDA"))
                set_Despesa();
            if (tipoLancamento.Equals("ENTRADA"))
                set_Receita();
        }
    }
}
