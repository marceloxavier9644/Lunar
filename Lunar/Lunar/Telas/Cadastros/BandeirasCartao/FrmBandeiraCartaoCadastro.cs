using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.BandeirasCartao
{
    public partial class FrmBandeiraCartaoCadastro : Form
    {
        BandeiraCartao bandeiraCartao = new BandeiraCartao();
        public DialogResult showModal(ref BandeiraCartao bandeiraCartao)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                bandeiraCartao = this.bandeiraCartao;
            }
            return DialogResult;
        }
        public FrmBandeiraCartaoCadastro()
        {
            InitializeComponent();
        }
        public FrmBandeiraCartaoCadastro(BandeiraCartao bandeira)
        {
            InitializeComponent();
            get_BandeiraCartao(bandeira);
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBandeiraCartaoCadastro_KeyDown(object sender, KeyEventArgs e)
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

        private void set_BandeiraCartao()
        {
            if (!String.IsNullOrEmpty(txtBandeiraCartao.Texts))
            {
                bandeiraCartao = new BandeiraCartao();
                if (String.IsNullOrEmpty(txtCodigo.Texts))
                {
                    bandeiraCartao.Id = 0;
                    bandeiraCartao.CodigoSefaz = "99";
                }
                else
                    bandeiraCartao.Id = int.Parse(txtCodigo.Texts);

                bandeiraCartao.Descricao = txtBandeiraCartao.Texts;
                Controller.getInstance().salvar(bandeiraCartao);
                GenericaDesktop.ShowInfo("Registro salvo com sucesso");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                GenericaDesktop.ShowAlerta("O campo bandeira é obrigatório!");
            }
        }

        private void get_BandeiraCartao(BandeiraCartao bandeira)
        {
            txtCodigo.Texts = bandeira.Id.ToString();
            txtBandeiraCartao.Texts = bandeira.Descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            set_BandeiraCartao();
        }

        private void FrmBandeiraCartaoCadastro_Paint(object sender, PaintEventArgs e)
        {
            txtBandeiraCartao.Focus();
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
