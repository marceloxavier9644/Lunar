using Lunar.Telas.Cadastros.Cidades;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmCadastroEnderecoAdicional : Form
    {
        EnderecoController enderecoController = new EnderecoController();
        CidadeController cidadeController = new CidadeController();
        Endereco endereco = new Endereco();
        Cidade cidade = new Cidade();
        GenericaDesktop generica = new GenericaDesktop();
        

        public DialogResult showModal(ref Endereco endereco)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                endereco = this.endereco;
            }
            return DialogResult;
        }
        public FrmCadastroEnderecoAdicional()
        {
            InitializeComponent();
            txtCEP.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }

        private void btnPesquisaCidade_Click(object sender, EventArgs e)
        {
            txtUF.Texts = "";
            txtCidade.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaCidade uu = new FrmPesquisaCidade())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    uu.Owner = formBackground;
                    switch (uu.showModal(ref cidade))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCidade.Texts = cidade.Descricao;
                            txtUF.Texts = cidade.Estado.Uf;
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

        private void txtCidade_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCidade.Texts))
                pesquisaCidadePorNome(txtCidade.Texts.Trim());
        }

        private void pesquisaCidadePorNome(string descricaoCidade)
        {
            try
            {
                if (!String.IsNullOrEmpty(descricaoCidade))
                {
                    cidade = new Cidade();
                    IList<Cidade> listaCidade = cidadeController.selecionarListaCidadePorDescricao(descricaoCidade);

                    if (listaCidade.Count == 1)
                    {
                        foreach (Cidade cid in listaCidade)
                        {
                            txtCidade.Texts = cid.Descricao;
                            txtUF.Texts = cid.Estado.Uf;
                        }
                    }
                    else if (listaCidade.Count > 1)
                    {
                        Form formBackground = new Form();
                        try
                        {
                            using (FrmPesquisaCidade uu = new FrmPesquisaCidade())
                            {
                                formBackground.StartPosition = FormStartPosition.Manual;
                                formBackground.FormBorderStyle = FormBorderStyle.None;
                                formBackground.Opacity = .50d;
                                formBackground.BackColor = Color.Black;
                                formBackground.WindowState = FormWindowState.Maximized;
                                formBackground.TopMost = true;
                                formBackground.Location = this.Location;
                                formBackground.ShowInTaskbar = false;
                                formBackground.Show();

                                uu.Owner = formBackground;
                                switch (uu.showModalComDescricao(ref cidade, txtCidade.Texts.Trim()))
                                {
                                    case DialogResult.Ignore:
                                        uu.Dispose();
                                        txtCidade.Texts = "";
                                        txtUF.Texts = "";
                                        break;
                                    case DialogResult.OK:
                                        txtCidade.Texts = cidade.Descricao;
                                        txtUF.Texts = cidade.Estado.Uf;
                                        break;
                                }

                                formBackground.Dispose();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    btnPesquisaCidade.PerformClick();
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Cidade não encontrada! " + erro.Message);
            }
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            pesquisarCEP();
        }

        private void pesquisarCEP()
        {
            if (!String.IsNullOrEmpty(txtCEP.Texts.Trim()))
            {
                try
                {
                    var ws = new WSCorreios.AtendeClienteClient();
                    var resposta = ws.consultaCEP(txtCEP.Texts, "marcelo.xs@hotmail.com", "@Aranhamxs11");
                    if (!String.IsNullOrEmpty(resposta.end))
                    {
                        txtEndereco.Texts = generica.RemoverAcentos(resposta.end);
                        txtComplemento.Texts = resposta.complemento2;
                        cidade = new Cidade();
                        cidade = cidadeController.selecionarCidadePorDescricao(generica.RemoverAcentos(resposta.cidade));
                        if (cidade != null)
                        {
                            txtCidade.Texts = cidade.Descricao;
                            txtUF.Texts = cidade.Estado.Uf;
                            txtBairro.Texts = resposta.bairro;
                        }
                        txtNumero.Focus();
                    }
                }
                catch
                {
                    txtEndereco.Texts = "";
                    txtNumero.Texts = "";
                    txtComplemento.Texts = "";
                    txtReferencia.Texts = "";
                }

            }
        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEndereco.Focus();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_Endereco();
        }

        private void set_Endereco()
        {
            try
            {
                endereco = new Endereco();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    endereco.Id = 0;
                }
                else
                    endereco.Id = int.Parse(txtCodigo.Texts);

                if (cidade != null && !String.IsNullOrEmpty(txtCEP.Texts))
                {
                    endereco.Cep = txtCEP.Texts;
                    endereco.Cidade = cidade;
                    endereco.Bairro = txtBairro.Texts.Trim();
                    endereco.Complemento = txtComplemento.Texts;
                    endereco.Logradouro = txtEndereco.Texts;
                    endereco.Numero = txtNumero.Texts;
                    endereco.Referencia = txtReferencia.Texts;
                    Sessao.endereco = endereco;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    GenericaDesktop.ShowErro("Cep, Cidade e bairro são campos obrigatórios!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCadastroEnderecoAdicional_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;
            }
        }
    }
}
