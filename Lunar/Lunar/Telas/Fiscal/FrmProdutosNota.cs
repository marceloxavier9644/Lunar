using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmProdutosNota : Form
    {
        ValidadorNotaSaida validadorNota = new ValidadorNotaSaida();
        NfeProdutoController nfeProdutoController = new NfeProdutoController();
        Nfe nfe = new Nfe();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        public FrmProdutosNota(Nfe nfe)
        {
            InitializeComponent();
            this.nfe = nfe;
            lblTitulo.Text = "Nota Fiscal " + nfe.NNf;
            IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
            grid.DataSource = listaProdutos;
            if(nfe.Cliente != null)
            {
                txtCodCliente.Texts = nfe.Cliente.Id.ToString();
                txtPesquisaCliente.Texts = nfe.Cliente.RazaoSocial;
            }
            if (nfe.CodStatus.Equals("100"))
            {
                txtCodCliente.Enabled = false;
                txtPesquisaCliente.Enabled = false;
                btnPesquisaCliente.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            pesquisaCliente();
        }
        private void pesquisaCliente()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtPesquisaCliente.Texts + "%'"))
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
                    switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
                            { 
                                if (validadorNota.validarClienteNota(((Pessoa)pessoaOjeto)))
                                {
                                    txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                    this.nfe.Cliente = ((Pessoa)pessoaOjeto);
                                    this.nfe.Destinatario = ((Pessoa)pessoaOjeto).RazaoSocial;
                                    this.nfe.CnpjDestinatario = ((Pessoa)pessoaOjeto).Cnpj;
                                    NfeController.getInstance().salvar(this.nfe);
                                }
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            if (validadorNota.validarClienteNota(((Pessoa)pessoaOjeto)))
                            {
                                txtPesquisaCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                this.nfe.Cliente = ((Pessoa)pessoaOjeto);
                                this.nfe.Destinatario = ((Pessoa)pessoaOjeto).RazaoSocial;
                                this.nfe.CnpjDestinatario = ((Pessoa)pessoaOjeto).Cnpj;
                                NfeController.getInstance().salvar(this.nfe);
                            }
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

        private void txtPesquisaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaCliente();
            }
        }
    }
}
