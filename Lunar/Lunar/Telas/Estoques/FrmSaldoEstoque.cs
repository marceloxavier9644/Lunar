using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Estoques
{
    public partial class FrmSaldoEstoque : Form
    {
        ProdutoController produtoController = new ProdutoController();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmSaldoEstoque()
        {
            InitializeComponent();
            gerarTiposDeProdutos();
        }

        private void iconPesquisar_Click(object sender, EventArgs e)
        {
            gerarRelatorio();
        }

        private void gerarTiposDeProdutos()
        {
            List<string> tiposDeProduto = new List<string>();
            tiposDeProduto.Add("REVENDA");
            tiposDeProduto.Add("USO E CONSUMO");
            tiposDeProduto.Add("MATÉRIA PRIMA");
            tiposDeProduto.Add("ATIVO IMOBILIZADO");
            comboTipoProduto.DataSource = tiposDeProduto;
            comboTipoProduto.SelectedIndex = -1;
        }

        private void gerarRelatorio() 
        {
            string sql = "From Produto Tabela where Tabela.FlagExcluido <> true ";
            if (!String.IsNullOrEmpty(txtcodGrupo.Texts))
                sql = sql + "and Tabela.Grupo = " + txtcodGrupo.Texts + " ";
            if (!String.IsNullOrEmpty(txtCodSubGrupo.Texts))
                sql = sql + "and Tabela.SubGrupo = " + txtCodSubGrupo.Texts + " ";
            if (!String.IsNullOrEmpty(txtCodSetor.Texts))
                sql = sql + "and Tabela.ProdutoSetor = " + txtCodSetor.Texts + " ";
            if (!String.IsNullOrEmpty(txtNCM.Texts))
                sql = sql + "and Tabela.Ncm = " + GenericaDesktop.RemoveCaracteres(txtNCM.Texts) + " ";
            if (!String.IsNullOrEmpty(comboTipoProduto.Text))
                sql = sql + "and Tabela.TipoProduto = " + comboTipoProduto.Text + " ";
            if (!String.IsNullOrEmpty(txtCodProduto.Text))
                sql = sql + "and Tabela.Id = " + txtCodProduto.Text + " ";
           
            sql = sql + "and Tabela.EmpresaFilial = " + Sessao.empresaFilialLogada.Id + " ";

            sql = sql + "order by Tabela.Id";


            IList<Produto> listaProduto = produtoController.selecionarProdutosPorSql(sql);
            if (listaProduto.Count > 0)
            {
                Form formBackground = new Form();
                object ordemServico = new OrdemServico();
                try
                {
                    using (FrmImprimirSaldoEstoque uu = new FrmImprimirSaldoEstoque(listaProduto))
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
                        uu.ShowDialog();
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

        private void FrmSaldoEstoque_Load(object sender, EventArgs e)
        {

  
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            gerarRelatorio();
        }
    }
}
