using Lunar.Telas.Cadastros.Produtos.Auxiliares;
using Lunar.Telas.PesquisaPadrao;
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
        List<string> tiposDeProduto = new List<string>();
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
            tiposDeProduto = new List<string>();
            tiposDeProduto.Add("REVENDA");
            tiposDeProduto.Add("USO E CONSUMO");
            tiposDeProduto.Add("MATÉRIA PRIMA");
            tiposDeProduto.Add("ATIVO IMOBILIZADO");
            tiposDeProduto.Add("PRODUÇÃO PRÓPRIA");
            tiposDeProduto.Add("ADICIONAL/COMPLEMENTO");
            comboTipoProduto.DataSource = tiposDeProduto;
        }

        private void gerarRelatorio() 
        {
            string sql = "From Produto Tabela where Tabela.FlagExcluido <> true ";
           
            if (!String.IsNullOrEmpty(txtCodProduto.Text))
                sql = sql + "and Tabela.Id = " + txtCodProduto.Text + " ";
            if(chkPesaveis.Checked == true)
                sql = sql + "and Tabela.Pesavel = true ";
            if(radioEstoqueAuxiliar.Checked == true)
            {
                if(radioSomentePositivos.Checked == true)
                    sql = sql + "and Tabela.EstoqueAuxiliar > 0 ";
                else if (radioSomenteNegativos.Checked == true)
                    sql = sql + "and Tabela.EstoqueAuxiliar < 0 ";
            }
            if (radioEstoque.Checked == true)
            {
                if (radioSomentePositivos.Checked == true)
                    sql = sql + "and Tabela.Estoque > 0 ";
                else if (radioSomenteNegativos.Checked == true)
                    sql = sql + "and Tabela.Estoque < 0 ";
            }
            if(!String.IsNullOrEmpty(txtcodGrupo.Texts))
                sql = sql + "and Tabela.ProdutoGrupo = " + txtcodGrupo.Texts + " ";
            if (!String.IsNullOrEmpty(txtCodSubGrupo.Texts))
                sql = sql + "and Tabela.ProdutoSubGrupo = " + txtCodSubGrupo.Texts + " ";
            if (!String.IsNullOrEmpty(txtCodSetor.Texts))
                sql = sql + "and Tabela.ProdutoSetor = " + txtCodSetor.Texts + " ";
            if(!String.IsNullOrEmpty(comboTipoProduto.Text))
                sql = sql + "and Tabela.TipoProduto = '" + comboTipoProduto.Text + "' ";
            if (!String.IsNullOrEmpty(txtNCM.Texts))
                sql = sql + "and Tabela.Ncm = '" + GenericaDesktop.RemoveCaracteres(txtNCM.Texts.Trim()) + "' ";

            sql = sql + "and Tabela.EmpresaFilial = " + Sessao.empresaFilialLogada.Id + " ";

            sql = sql + "order by Tabela.Id";

            bool apresentarCusto = false;
            if (chkCusto.Checked == true)
                apresentarCusto = true;
            bool apresentarVenda = false;
            if (chkVenda.Checked == true)
                apresentarVenda = true;

            IList<Produto> listaProduto = produtoController.selecionarProdutosPorSql(sql);
            if (listaProduto.Count > 0)
            {
                Form formBackground = new Form();
                object ordemServico = new OrdemServico();
                try
                {
                    using (FrmImprimirSaldoEstoque uu = new FrmImprimirSaldoEstoque(listaProduto, apresentarCusto, apresentarVenda))
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

        private void btnPesquisaGrupo_Click(object sender, EventArgs e)
        {
            Object grupoObjeto = new ProdutoGrupo();
            txtGrupo.Texts = "";
            txtcodGrupo.Texts = "";
            txtCodSubGrupo.Texts = "";
            txtSubGrupo.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ProdutoGrupo", ""))
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
                    switch (uu.showModal("ProdutoGrupo", "", ref grupoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmGrupoProdutoCadastro form = new FrmGrupoProdutoCadastro();
                            if (form.showModalNovo(ref grupoObjeto) == DialogResult.OK)
                            {
                                txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                                txtcodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                     
                                txtSubGrupo.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                            txtcodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                         
                            txtSubGrupo.Focus();
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

        private void btnPesquisaSubGrupo_Click(object sender, EventArgs e)
        {
            Object subGrupoObjeto = new ProdutoSubGrupo();
            String sqlAdicionalGrupo = "";
            Sessao.grupoSelecionadoCadastroProduto = new ProdutoGrupo();
            txtSubGrupo.Texts = "";
            txtCodSubGrupo.Texts = "";
            if (!String.IsNullOrEmpty(txtcodGrupo.Texts))
            {
                ProdutoGrupo grupo = new ProdutoGrupo();
                grupo.Id = int.Parse(txtcodGrupo.Texts);
                Sessao.grupoSelecionadoCadastroProduto = (ProdutoGrupo)Controller.getInstance().selecionar(grupo);
                sqlAdicionalGrupo = "and Tabela.ProdutoGrupo = " + txtcodGrupo.Texts;
            }
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ProdutoSubGrupo", sqlAdicionalGrupo))
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
                    switch (uu.showModal("ProdutoSubGrupo", "", ref subGrupoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmSubGrupoCadastro form = new FrmSubGrupoCadastro();
                            if (form.showModalNovo(ref subGrupoObjeto) == DialogResult.OK)
                            {
                                txtSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Descricao;
                                txtCodSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Id.ToString();

                                txtGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Descricao;
                                txtcodGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Id.ToString();

                                txtSetor.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Descricao;
                            txtCodSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Id.ToString();

                            txtGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Descricao;
                            txtcodGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Id.ToString();

                            txtSetor.Focus();
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

        private void btnPesquisaSetor_Click(object sender, EventArgs e)
        {
            Object setorObjeto = new ProdutoSetor();
            txtSetor.Texts = "";
            txtCodSetor.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ProdutoSetor", ""))
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
                    switch (uu.showModal("ProdutoSetor", "", ref setorObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmSetorCadastro form = new FrmSetorCadastro();
                            if (form.showModalNovo(ref setorObjeto) == DialogResult.OK)
                            {
                                txtSetor.Texts = ((ProdutoSetor)setorObjeto).Descricao;
                                txtCodSetor.Texts = ((ProdutoSetor)setorObjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtSetor.Texts = ((ProdutoSetor)setorObjeto).Descricao;
                            txtCodSetor.Texts = ((ProdutoSetor)setorObjeto).Id.ToString();
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

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {

        }
    }
}
