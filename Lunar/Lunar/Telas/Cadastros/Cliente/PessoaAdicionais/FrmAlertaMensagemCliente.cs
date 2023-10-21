using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Cliente.PessoaAdicionais
{
    public partial class FrmAlertaMensagemCliente : Form
    {
        Pessoa pessoa = new Pessoa();
        GenericaDesktop generica = new GenericaDesktop();
        AlertaClienteController alertaController = new AlertaClienteController();
        public FrmAlertaMensagemCliente(Pessoa pessoa)
        {
            InitializeComponent();
            this.pessoa = pessoa;
            txtCodCliente.Texts = pessoa.Id.ToString();
            txtCliente.Texts = pessoa.RazaoSocial;
            if (pessoa.Cnpj != null)
            {
                if (pessoa.Cnpj.Length == 11)
                    txtCPF.Texts = generica.FormatarCPF(pessoa.Cnpj);
                else if (pessoa.Cnpj.Length == 14)
                    txtCPF.Texts = generica.FormatarCNPJ(pessoa.Cnpj);
            }
            buscarAlertaCadastrado();
            txtDescricao.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buscarAlertaCadastrado()
        {
            IList<AlertaCliente> listaAlerta = alertaController.selecionarAlertaPorPessoa(pessoa.Id);
            if(listaAlerta.Count > 0)
            {
                gridAlerta.DataSource = listaAlerta;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDescricao.Texts))
            {
                AlertaCliente alerta = new AlertaCliente();
                if (String.IsNullOrEmpty(txtID.Texts))
                    alerta.Id = 0;
                else
                    alerta.Id = int.Parse(txtID.Texts);
                alerta.Data = DateTime.Now;
                alerta.Descricao = txtDescricao.Texts;
                alerta.Pessoa = pessoa;
                Controller.getInstance().salvar(alerta);
                buscarAlertaCadastrado();
                limpaCampos();
                GenericaDesktop.ShowInfo("Registro Salvo com Sucesso!");
            }
            else
                GenericaDesktop.ShowAlerta("Digite uma mensagem para gravar!");
        }

        private void limpaCampos()
        {
            txtID.Texts = "";
            txtDescricao.Texts = "";
        }

        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if(GenericaDesktop.ShowConfirmacao("Deseja gravar a mensagem?"))
                    btnConfirmar.PerformClick();
            }
        }

        private void gridAlerta_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (gridAlerta.SelectedIndex >= 0)
            {
                AlertaCliente alerta = new AlertaCliente();
                alerta = (AlertaCliente)gridAlerta.SelectedItem;
                txtDescricao.Texts = alerta.Descricao;
                txtID.Texts = alerta.Id.ToString();
                txtDescricao.Focus();
                txtDescricao.Select();
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha da mensagem que deseja editar!");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (gridAlerta.SelectedIndex >= 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja realmente excluir a mensagem selecionada?"))
                {
                    AlertaCliente alerta = new AlertaCliente();
                    alerta = (AlertaCliente)gridAlerta.SelectedItem;
                    Controller.getInstance().excluir(alerta);
                    buscarAlertaCadastrado();
                    GenericaDesktop.ShowInfo("Excluído com Sucesso!");
                }
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha da mensagem que deseja excluir!");
        }

        private void btnFechar2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridAlerta_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }
    }
}
