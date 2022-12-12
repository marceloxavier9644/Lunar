using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmNaturezaOperacao : Form
    {
        NaturezaOperacao naturezaOperacao = new NaturezaOperacao();
        NaturezaOperacaoController naturezaOperacaoController = new NaturezaOperacaoController();
        public DialogResult showModal(ref NaturezaOperacao naturezaOperacao)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                naturezaOperacao = this.naturezaOperacao;
            }
            return DialogResult;
        }
        public FrmNaturezaOperacao()
        {
            InitializeComponent();
            preencherCombo();
        }
        public FrmNaturezaOperacao(NaturezaOperacao naturezaOperacao)
        {
            InitializeComponent();
            preencherCombo(); 
            get_NaturezaOperacao(naturezaOperacao);
        }

        private void preencherCombo()
        {
            List<string> listaFinalidade = new List<string>();
            listaFinalidade.Add("1 - NF-e normal");
            listaFinalidade.Add("2 - NF-e complementar");
            listaFinalidade.Add("3 - NF-e de ajuste");
            listaFinalidade.Add("4 - Devolução de mercadoria");
            comboFinalidade.DataSource = listaFinalidade;
            comboFinalidade.ShowToolTip = true;
            comboFinalidade.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNaturezaOperacao_KeyDown(object sender, KeyEventArgs e)
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

        private void set_NaturezaOperacao()
        {
            naturezaOperacao = new NaturezaOperacao();
            if (String.IsNullOrEmpty(txtId.Texts))
            {
                naturezaOperacao.Id = 0;
            }
            else
                naturezaOperacao.Id = int.Parse(txtId.Texts);
            naturezaOperacao.Descricao = txtDescricao.Texts;
            if (radioEntrada.Checked == true)
                naturezaOperacao.EntradaSaida = "E";
            else
                naturezaOperacao.EntradaSaida = "S";
            string finalidade = comboFinalidade.SelectedValue.ToString().Substring(0, 1);
            naturezaOperacao.FinalidadeNfe = finalidade;
            if (chkMovimentaEstoque.Checked == true)
                naturezaOperacao.MovimentaEstoque = true;
            else
                naturezaOperacao.MovimentaEstoque = false;
            if (chkGerarFinanceiro.Checked == true)
                naturezaOperacao.MovimentaFinanceiro = true;
            else
                naturezaOperacao.MovimentaFinanceiro = false;

            Controller.getInstance().salvar(naturezaOperacao);
            GenericaDesktop.ShowInfo("Registro salvo com sucesso");
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void get_NaturezaOperacao(NaturezaOperacao naturezaOperacao)
        {
            txtId.Texts = naturezaOperacao.Id.ToString();
            txtDescricao.Texts = naturezaOperacao.Descricao;
            if (naturezaOperacao.FinalidadeNfe.Equals("1"))
                comboFinalidade.SelectedIndex = 0;
            else if (naturezaOperacao.FinalidadeNfe.Equals("2"))
                comboFinalidade.SelectedIndex = 1;
            else if (naturezaOperacao.FinalidadeNfe.Equals("3"))
                comboFinalidade.SelectedIndex = 2;
            else
                comboFinalidade.SelectedIndex = 3;
            if (naturezaOperacao.EntradaSaida.Equals("E"))
                radioEntrada.Checked = true;
            else
                radioSaida.Checked = true;
            if (naturezaOperacao.MovimentaEstoque == true)
                chkMovimentaEstoque.Checked = true;
            else
                chkMovimentaEstoque.Checked = false;
            if (naturezaOperacao.MovimentaFinanceiro == true)
                chkGerarFinanceiro.Checked = true;
            else
                chkGerarFinanceiro.Checked = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_NaturezaOperacao();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja cancelar o cadastro"))
            {
                this.Close();
            }
        }
    }
}
