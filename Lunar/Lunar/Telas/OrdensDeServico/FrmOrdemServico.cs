using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Cadastros.Produtos;
using Lunar.Telas.ContasReceber;
using Lunar.Telas.FormaPagamentoRecebimento;
using Lunar.Telas.OrdensDeServico.Servicos;
using Lunar.Telas.OrdensDeServico.TipoObjetos;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using Lunar.Utils.IntegracaoZAPI;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Lunar.Telas.OrdensDeServico
{
    public partial class FrmOrdemServico : Form
    {
        int idEdicaoProduto = 0;
        DataRow dataRowEdit;
        bool editando = false;
        PessoaDependenteController pessoaDependenteController = new PessoaDependenteController();
        GenericaDesktop genericaDesktop = new GenericaDesktop();
        PessoaController pessoaController = new PessoaController();
        Pessoa cliente = new Pessoa();
        ProdutoController produtoController = new ProdutoController();
        Produto produto = new Produto();
        ServicoController servicoController = new ServicoController();
        bool showModal = false;
        OrdemServico ordemServico = new OrdemServico();
        Servico servico = new Servico();
        IList<OrdemServicoProduto> listaProdutos = new List<OrdemServicoProduto>();
        IList<OrdemServicoServico> listaServicos = new List<OrdemServicoServico>();
        IList<OrdemServicoExame> listaExames = new List<OrdemServicoExame>();
        OrdemServicoController ordemServicoController = new OrdemServicoController();
        IList<Anexo> listaAnexo = new List<Anexo>();
        AnexoController anexoController = new AnexoController();
        Pessoa vendedor = new Pessoa();
        public DialogResult showModalNovo(ref object ordemServico)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                ordemServico = this.ordemServico;
            }
            return DialogResult;
        }
        public FrmOrdemServico()
        {
            InitializeComponent();
            txtDataAbertura.Value = DateTime.Now;
            if (Sessao.parametroSistema.TipoObjeto != null)
            {
                txtCodTipoObjeto.Texts = Sessao.parametroSistema.TipoObjeto.Id.ToString();
                txtTipoObjeto.Texts = Sessao.parametroSistema.TipoObjeto.Descricao;
            }
            this.FormBorderStyle = FormBorderStyle.None;
            txtNumeroOS.TextAlign = HorizontalAlignment.Center;
            txtNumeroOS.ForeColor = Color.Red;
            
            this.gridProdutos.DataSource = dsProdutos;
            this.gridServico.DataSource = dsServico;
            this.gridExames.DataSource = dsExame;
            this.gridAnexos.DataSource = dsAnexo;
            if (Sessao.empresaFilialLogada.Otica == true)
            {
                tabExames.TabVisible = true;
                tabOtica.TabVisible = true;
            }
            else
            {
                tabExames.TabVisible = false;
                tabOtica.TabVisible = false;
            }
            lblAutomatico.Visible = true;
            txtDataExame.Value = DateTime.Now;
            txtCliente.Focus();
        }

        public FrmOrdemServico(OrdemServico ordemServico)
        {
            InitializeComponent();
            this.ordemServico = ordemServico;
            if (ordemServico.TipoObjeto != null)
            {
                txtCodTipoObjeto.Texts = ordemServico.TipoObjeto.Id.ToString();
                txtTipoObjeto.Texts = ordemServico.TipoObjeto.Descricao;
            }
            this.FormBorderStyle = FormBorderStyle.None;
            txtNumeroOS.TextAlign = HorizontalAlignment.Center;
            txtNumeroOS.ForeColor = Color.Red;

            this.gridProdutos.DataSource = dsProdutos;
            this.gridServico.DataSource = dsServico;
            this.gridExames.DataSource = dsExame;
            this.gridAnexos.DataSource = dsAnexo;

            if (Sessao.empresaFilialLogada.Otica == true)
            {
                tabExames.TabVisible = true;
                tabOtica.TabVisible = true;
            }
            else
            {
                tabExames.TabVisible = false;
                tabOtica.TabVisible = false;
            }
            lblAutomatico.Visible = true;
            txtDataExame.Value = DateTime.Now;
            get_OrdemServico();
            if(ordemServico.Status == "ENCERRADA") 
            {
                lblInformativo.Visible = true;
                btnGravar.Enabled = false;
                btnGravarEncerrar.Enabled = false;
                txtCodProduto.Enabled = false;
                txtPesquisaProduto.Enabled = false;
                txtCodServico.Enabled = false;
                txtPesquisaServico.Enabled = false;
                btnPesquisaProduto.Enabled = false;
                btnPesquisaServico.Enabled = false;
                btnPesquisaCliente.Enabled = false;
                btnPesquisaDependente.Enabled = false;
                txtCliente.Enabled = false;
                txtCodCliente.Enabled = false;
                txtTipoObjeto.Enabled = false;
                txtCodTipoObjeto.Enabled = false;
                btnPesquisaTipoObjeto.Enabled = false;
                txtVendedor.Enabled = false;
                //btnPesquisaVendedor.Enabled = false;
                txtObservacoes.Enabled = false;

            }
                
        }

        private void get_OrdemServico()
        {
            txtNumeroOS.Texts = ordemServico.Id.ToString();
            lblAutomatico.Visible = false;
            txtCliente.Texts = ordemServico.Cliente.RazaoSocial;
            txtCodCliente.Texts = ordemServico.Cliente.Id.ToString();
            txtNumeroSerie.Texts = ordemServico.NumeroSerie;
            txtObservacoes.Texts = ordemServico.Observacoes;
            txtDataAbertura.Value = ordemServico.DataAbertura;
            if (ordemServico.Vendedor != null)
            {
                txtVendedor.Texts = ordemServico.Vendedor.RazaoSocial;
                vendedor = ordemServico.Vendedor;
            }
            OrdemServicoProdutoController ordemServicoProdutoController = new OrdemServicoProdutoController();
            IList<OrdemServicoProduto> listaOrdemProduto = ordemServicoProdutoController.selecionarProdutosPorOrdemServico(ordemServico.Id);
            if(listaOrdemProduto.Count > 0)
            {
                dsProdutos.Tables[0].Clear();
                foreach (OrdemServicoProduto ordemServicoProduto in listaOrdemProduto)
                {
                    System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                    row.SetField("Id", ordemServicoProduto.Id.ToString());
                    row.SetField("Codigo", ordemServicoProduto.Produto.Id.ToString());
                    row.SetField("Descricao", ordemServicoProduto.DescricaoProduto);
                    decimal valorUnitForm = ordemServicoProduto.ValorUnitario;
                    row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                    row.SetField("Quantidade", ordemServicoProduto.Quantidade.ToString());
                    decimal valorTotal = ordemServicoProduto.ValorTotal;
                    row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                    row.SetField("Desconto", string.Format("{0:0.00}", ordemServicoProduto.Desconto));
                    row.SetField("Acrescimo", string.Format("{0:0.00}", ordemServicoProduto.Acrescimo));
                    dsProdutos.Tables[0].Rows.Add(row);
                }
                if (this.gridProdutos.View != null)
                {
                    if (this.gridProdutos.View.Records.Count > 0)
                    {
                        gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridProdutos.AutoSizeController.Refresh();
                    }
                }
                somaEnquantoDigitaItens();
            }

            //Serviços 
            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServicos = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            if (listaServicos.Count > 0)
            {
                dsServico.Tables[0].Clear();
                foreach (OrdemServicoServico ordemServicoServico in listaServicos)
                {
                    System.Data.DataRow row = dsServico.Tables[0].NewRow();
                    row.SetField("Id", ordemServicoServico.Id.ToString());
                    row.SetField("Codigo", ordemServicoServico.Servico.Id.ToString());
                    row.SetField("Descricao", ordemServicoServico.DescricaoServico);
                    decimal valorUnitForm = ordemServicoServico.ValorUnitario;
                    row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                    row.SetField("Quantidade", ordemServicoServico.Quantidade.ToString());
                    decimal valorTotal = ordemServicoServico.ValorTotal;
                    row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                    row.SetField("Desconto", string.Format("{0:0.00}", ordemServicoServico.Desconto));
                    row.SetField("Acrescimo", string.Format("{0:0.00}", ordemServicoServico.Acrescimo));
                    dsServico.Tables[0].Rows.Add(row);
                }
                if (this.gridServico.View != null)
                {
                    if (this.gridServico.View.Records.Count > 0)
                    {
                        gridServico.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        this.gridServico.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridServico.AutoSizeController.Refresh();
                    }
                }
            }
            somaEnquantoDigitaServicos();

            //Exames 
            OrdemServicoExameController ordemServicoExameController = new OrdemServicoExameController();
            IList<OrdemServicoExame> listaExames = ordemServicoExameController.selecionarExamesPorOrdemServico(ordemServico.Id);
            if (listaExames.Count > 0)
            {
                dsExame.Tables[0].Clear();
                foreach (OrdemServicoExame ordemServicoExame in listaExames)
                {
                    System.Data.DataRow row = dsExame.Tables[0].NewRow();
                    if (ordemServicoExame.Dependente != null)
                    {
                        row.SetField("CodDependente", ordemServicoExame.Dependente.Id.ToString());
                        row.SetField("Dependente", ordemServicoExame.Dependente.Nome);
                    }
                    else
                    {
                        row.SetField("CodDependente", "");
                        row.SetField("Dependente", "");
                    }
                    row.SetField("Examinador", ordemServicoExame.Examinador);
                    row.SetField("DataExame", ordemServicoExame.DataExame.ToShortDateString());
                    row.SetField("LDEsferico", ordemServicoExame.LdEsferico);
                    row.SetField("LDCilindrico", ordemServicoExame.LdCilindrico);
                    row.SetField("LDPosicao", ordemServicoExame.LdPosicao);
                    row.SetField("LDDp", ordemServicoExame.LdDp);
                    row.SetField("LDAltura", ordemServicoExame.LdAltura);
                    row.SetField("LEEsferico", ordemServicoExame.LeEsferico);
                    row.SetField("LECilindrico", ordemServicoExame.LeCilindrico);
                    row.SetField("LEPosicao", ordemServicoExame.LePosicao);
                    row.SetField("LEDp", ordemServicoExame.LeDp);
                    row.SetField("LEAltura", ordemServicoExame.LeAltura);
                    row.SetField("PDEsferico", ordemServicoExame.PdEsferico);
                    row.SetField("PDCilindrico", ordemServicoExame.PdCilindrico);
                    row.SetField("PDPosicao", ordemServicoExame.PdPosicao);
                    row.SetField("PDDp", ordemServicoExame.PdDp);
                    row.SetField("PDAltura", ordemServicoExame.PdAltura);
                    row.SetField("PEEsferico", ordemServicoExame.PeEsferico);
                    row.SetField("PECilindrico", ordemServicoExame.PeCilindrico);
                    row.SetField("PEPosicao", ordemServicoExame.PePosicao);
                    row.SetField("PEDp", ordemServicoExame.PeDp);
                    row.SetField("PEAltura", ordemServicoExame.PeAltura);
                    row.SetField("Armacao", ordemServicoExame.Armacao);
                    row.SetField("Lentes", ordemServicoExame.Lente);
                    row.SetField("ProximoExame", ordemServicoExame.ProximoExame);
                    row.SetField("Adicao", ordemServicoExame.Adicao);
                    row.SetField("DataEntrega", ordemServicoExame.DataEntrega);
                    row.SetField("Id", ordemServicoExame.Id.ToString());
                    dsExame.Tables[0].Rows.Add(row);
                    if (this.gridExames.View != null)
                    {
                        if (this.gridExames.View.Records.Count > 0)
                        {
                            gridExames.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                            this.gridExames.Columns["Dependente"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                            gridExames.AutoSizeController.Refresh();
                        }
                    }
                }
                if(listaExames.Count == 1)
                {
                    gridExames.SelectedIndex = 0;
                    editarExame();
                    tabControlAdv1.SelectedTab = tabPrincipal;
                }
            }

            //Anexos
            AnexoController anexoController = new AnexoController();
            IList<Anexo> listaAnexos = anexoController.selecionarTodosAnexosPorOrdemServico(ordemServico.Id);
            if (listaAnexos.Count > 0)
            {
                dsAnexo.Tables[0].Clear();
                foreach (Anexo anexo in listaAnexos)
                {
                    System.Data.DataRow row = dsAnexo.Tables[0].NewRow();
                    row.SetField("Id", anexo.Id.ToString());
                    row.SetField("Codigo", anexo.Codigo);
                    row.SetField("Caminho", anexo.Caminho);
                    row.SetField("DataCadastro", anexo.DataCadastro.ToShortDateString());
                    dsAnexo.Tables[0].Rows.Add(row);
                }
            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            //txtCodCliente.Texts = "";
            // txtCliente.Texts = "";
            iconeExclamacao.Visible = false;
            txtCodDependente.Texts = "";
            txtDependente.Texts = "";
            Pessoa pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPessoa uu = new FrmPesquisaPessoa(txtCliente.Texts))
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
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
                    switch (uu.showModal(ref pessoaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmClienteCadastro form = new FrmClienteCadastro();
                            Object pessoaObj = new Pessoa();
                            if (form.showModalNovo(ref pessoaObj) == DialogResult.OK)
                            {
                                txtCliente.Texts = ((Pessoa)pessoaObj).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaObj).Id.ToString();
                                if (((Pessoa)pessoaObj).EscritorioCobranca == true)
                                {
                                    GenericaDesktop.ShowAlerta("Cliente Marcado que possui parcela em escritório de cobrança!");
                                    iconeExclamacao.Visible = true;
                                }
                                if (((Pessoa)pessoaObj).RegistradoSpc == true)
                                {
                                    GenericaDesktop.ShowAlerta("Cliente marcado que está registrado no SPC/Serasa pela sua empresa!");
                                    iconeExclamacao.Visible = true;
                                }
                                txtVendedor.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            if (((Pessoa)pessoaOjeto).EscritorioCobranca == true)
                            {
                                GenericaDesktop.ShowAlerta("Cliente Marcado que possui parcela em escritório de cobrança!");
                                iconeExclamacao.Visible = true;
                            }
                            if (((Pessoa)pessoaOjeto).RegistradoSpc == true)
                            {
                                GenericaDesktop.ShowAlerta("Cliente marcado que está registrado no SPC/Serasa pela sua empresa!");
                                iconeExclamacao.Visible = true;
                            }
                            txtVendedor.Focus();
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

        private void pesquisaCliente()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%'"))
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
                                txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                                txtTipoObjeto.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            txtTipoObjeto.Focus();
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

        private void pesquisaVendedor()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtVendedor.Texts + "%' and Tabela.Vendedor = true "))
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
                                txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                vendedor = ((Pessoa)pessoaOjeto);
                                txtObservacoes.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtVendedor.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            vendedor = ((Pessoa)pessoaOjeto);
                            txtObservacoes.Focus();
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

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCodDependente.Texts = "";
                txtDependente.Texts = "";
                try
                {
                    if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        cliente.Id = int.Parse(txtCodCliente.Texts);
                        cliente = (Pessoa)pessoaController.selecionar(cliente);
                        if (cliente != null)
                        {
                            txtCliente.Texts = cliente.RazaoSocial;
                            txtCodCliente.Texts = cliente.Id.ToString();
                            if (cliente.EscritorioCobranca == true)
                                GenericaDesktop.ShowAlerta("Cliente Marcado que possui parcela em escritório de cobrança!");
                            if (cliente.RegistradoSpc == true)
                                GenericaDesktop.ShowAlerta("Cliente marcado que está registrado no SPC/Serasa pela sua empresa!");
                            txtTipoObjeto.Focus();
                        }
                        else
                        {

                            txtCliente.Texts = "";
                            txtCodCliente.Texts = "";
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtCliente.Texts = "";
                    txtCodCliente.Texts = "";
                    GenericaDesktop.ShowAlerta("Cliente não encontrado");
                }
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCodDependente.Texts = "";
                txtDependente.Texts = "";
                btnPesquisaCliente.PerformClick();
            }
        }

        private void pesquisaTipoObjeto()
        {
            Object objeto = new TipoObjeto();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("TipoObjeto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + txtTipoObjeto.Texts + "%'"))
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
                    switch (uu.showModal("TipoObjeto", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmTipoObjetoCadastro form = new FrmTipoObjetoCadastro();
                            if (form.showModalNovo(ref objeto) == DialogResult.OK)
                            {
                                txtTipoObjeto.Texts = ((TipoObjeto)objeto).Descricao;
                                txtCodTipoObjeto.Texts = ((TipoObjeto)objeto).Id.ToString();
                                txtNumeroSerie.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtTipoObjeto.Texts = ((TipoObjeto)objeto).Descricao;
                            txtCodTipoObjeto.Texts = ((TipoObjeto)objeto).Id.ToString();
                            txtNumeroSerie.Focus();
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

        private void txtCodTipoObjeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodTipoObjeto.Texts))
                    {
                        TipoObjeto tipoObjeto = new TipoObjeto();
                        tipoObjeto.Id = int.Parse(txtCodTipoObjeto.Texts);
                        tipoObjeto = (TipoObjeto)Controller.getInstance().selecionar(tipoObjeto);
                        if (tipoObjeto != null)
                        {
                            txtTipoObjeto.Texts = tipoObjeto.Descricao;
                            txtCodTipoObjeto.Texts = tipoObjeto.Id.ToString();
                            txtNumeroSerie.Focus();
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Tipo de objeto não encontrado");
                            txtTipoObjeto.Texts = "";
                            txtCodTipoObjeto.Texts = "";
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtTipoObjeto.Texts = "";
                    txtCodTipoObjeto.Texts = "";
                    GenericaDesktop.ShowAlerta("Tipo de objeto não encontrado");
                }
            }
        }

        private void btnPesquisaTipoObjeto_Click(object sender, EventArgs e)
        {
            pesquisaTipoObjeto();
        }

        private void PesquisarProduto(string valor)
        {

            IList<Produto> listaProdutos = new List<Produto>();

            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            listaProdutos = produtoController.selecionarProdutosComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaProdutos.Count == 1)
            {
                foreach (Produto prod in listaProdutos)
                {
                    txtPesquisaProduto.Texts = prod.Descricao;
                    txtQuantidadeItem.Texts = "1";
                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtValorTotalItem.Texts = string.Format("{0:0.00}", prod.ValorVenda);
                    txtDescontoItem.Texts = string.Format("{0:0.00}", 0);
                    txtAcrescimoItem.Texts = string.Format("{0:0.00}", 0);
                    txtCodProduto.Texts = prod.Id.ToString();
                    this.produto = prod;
                    if (valorAux.Contains("*"))
                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                    if (prod.Ean.Equals(valor.Trim()))
                        inserirItem(this.produto);
                    else
                    {
                        txtQuantidadeItem.Focus();
                        txtQuantidadeItem.Select();
                    }
                }
            }
            else if (listaProdutos.Count > 1)
            {
                Object produtoOjeto = new Produto();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Produto", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao, ' ', Tabela.Ean, ' ', Tabela.Referencia, ' ', Tabela.Ncm) like '%" + valor + "%'"))
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
                        switch (uu.showModal("Produto", "", ref produtoOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmProdutoCadastro form = new FrmProdutoCadastro();
                                if (form.showModalNovo(ref produtoOjeto, false) == DialogResult.OK)
                                {
                                    txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                    txtQuantidadeItem.Texts = "1";
                                    txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                    txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                    this.produto = ((Produto)produtoOjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()))
                                        inserirItem(this.produto);
                                    else
                                    {
                                        txtQuantidadeItem.Focus();
                                        txtQuantidadeItem.SelectAll();
                                    }
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtPesquisaProduto.Texts = ((Produto)produtoOjeto).Descricao;
                                txtQuantidadeItem.Texts = "1";
                                txtValorUnitarioItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtValorTotalItem.Texts = string.Format("{0:0.00}", ((Produto)produtoOjeto).ValorVenda);
                                txtCodProduto.Texts = ((Produto)produtoOjeto).Id.ToString();
                                this.produto = ((Produto)produtoOjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidadeItem.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                if (((Produto)produtoOjeto).Ean.Equals(valor.Trim()) && !String.IsNullOrEmpty(valor))
                                    inserirItem(this.produto);
                                else
                                {
                                    txtQuantidadeItem.Focus();
                                    txtQuantidadeItem.SelectAll();
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
                // GenericaDesktop.ShowInfo("Função de pesquisa extra em desenvolvimento...");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Produto não encontrado");
                txtPesquisaProduto.SelectAll();
            }
        }



        private void inserirItem(Produto produto)
        {
            txtQuantidadeItem.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioItem.TextAlign = HorizontalAlignment.Center;
            txtValorTotalItem.TextAlign = HorizontalAlignment.Center;
            try
            {
                System.Data.DataRow row = dsProdutos.Tables[0].NewRow();
                row.SetField("Id", 0);
                if (editando == true)
                {
                    dataRowEdit.Delete();
                    if(idEdicaoProduto > 0)
                        row.SetField("Id", idEdicaoProduto);
                }
                idEdicaoProduto = 0;
                row.SetField("Codigo", produto.Id.ToString());
                row.SetField("Descricao", produto.Descricao);
                decimal valorUnitForm = decimal.Parse(txtValorUnitarioItem.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Quantidade", txtQuantidadeItem.Texts);
                decimal valorTotal = decimal.Parse(txtValorTotalItem.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("Desconto", string.Format("{0:0.00}", decimal.Parse(txtDescontoItem.Texts)));
                row.SetField("Acrescimo", string.Format("{0:0.00}", decimal.Parse(txtAcrescimoItem.Texts)));
                dsProdutos.Tables[0].Rows.Add(row);

                txtPesquisaProduto.Texts = "";
                somaEnquantoDigitaItens();
                txtQuantidadeItem.Texts = "1";
                txtValorUnitarioItem.Texts = "0,00";
                txtValorTotalItem.Texts = "0,00";
                txtDescontoItem.Texts = "0,00";
                txtAcrescimoItem.Texts = "0,00";
                txtCodProduto.Texts = "";
                txtPesquisaProduto.Focus();
                editando = false;
                this.produto = new Produto();

                if (this.gridProdutos.View != null)
                {
                    if (this.gridProdutos.View.Records.Count > 0)
                    {
                        gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                        this.gridProdutos.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridProdutos.AutoSizeController.Refresh();
                    }
                }

                txtPesquisaProduto.Focus();

            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o produto, quantidade e valor");
            }
        }

        private void somaEnquantoDigitaItens()
        {
            try
            {
                decimal valorTotalProdutos = 0;
                double pecas = 0;
                if (gridProdutos.View != null)
                {
                    var records = gridProdutos.View.Records;
                    decimal descontoItem = 0;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;

                        if (!String.IsNullOrEmpty(dataRowView.Row[0].ToString()))
                            descontoItem = descontoItem + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        valorTotalProdutos = valorTotalProdutos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());
                    }
                    txtValorTotalTodosProdutos.Texts = string.Format("{0:0.00}", valorTotalProdutos);
                    txtTotalGeralProdutoServico.Texts = string.Format("{0:0.00}", valorTotalProdutos + decimal.Parse(txtValorTotalTodosServicos.Texts));
                }
            }
            catch
            {

            }
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquisarProduto("");
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            verificarExamesAoFechar();
            this.Close();
        }

        private void verificarExamesAoFechar()
        {
            //Exames
            if (gridExames.View != null)
            {
                var recordsExames = gridExames.View.Records;
                if (recordsExames.Count > 0)
                {
                    foreach (var record in recordsExames)
                    {
                        var dataRowView = record.Data as DataRowView;

                        if (int.Parse(dataRowView.Row["Id"].ToString()) == 0)
                        { 
                            if(GenericaDesktop.ShowConfirmacao("Deseja gravar os exames inseridos/alterados? Caso clique em não você irá perder os exames não gravados!"))
                            {
                                set_OrdemServico();
                                this.Close();
                            }
                        }
                    }
                }
            }
        }

        private void txtTipoObjeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaTipoObjeto();
            }
        }

        private void btnAnalise_Click(object sender, EventArgs e)
        {
            //Converter imagem para base64
            byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\marce\OneDrive\Imagens\124216785.jpg");
            string base64 = Convert.ToBase64String(imageArray);
            Zapi zap = new Zapi();

           // string ret = zap.zapi_EnviarImagem("5538988069644", base64, "Teste de Imagem");
            //string ret2 = zap.zapi_EnviarImagem("38999947153", base64, "Teste de Imagem, disparo via Lunar Software");
            string ret20 = zap.zapi_EnviarImagem("5538999947153", base64, "Teste de Imagem, disparo via Lunar Software", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);
            string ret201 = zap.zapi_EnviarImagem("5538999795577", base64, "Teste de Imagem, disparo via Lunar Software", Sessao.parametroSistema.IdInstanciaWhats, Sessao.parametroSistema.TokenWhats);

            // string ret3 = zap.zapi_EnviarTexto("5538988069644", "Teste de Mensagem");
            // string ret4 = zap.zapi_EnviarTexto("5538998467679", "Teste de Mensagem");
            //Converter de volta para imagem
            //var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64String)));
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

        private void btnConfirmaItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodProduto.Texts))
            {
                produto = new Produto();
                produto.Id = int.Parse(txtCodProduto.Texts);
                produto = (Produto)Controller.getInstance().selecionar(produto);
                inserirItem(produto);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione um produto");
            }
        }

        private void panelPagamentoTotal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void maskedEditBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPesquisaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarProduto(txtPesquisaProduto.Texts);
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodProduto.Texts))
                    {
                        produto = new Produto();
                        txtPesquisaProduto.Texts = "";
                        produto.Id = int.Parse(txtCodProduto.Texts);
                        produto = (Produto)produtoController.selecionar(produto);
                        if (produto != null)
                        {
                            txtPesquisaProduto.Texts = produto.Descricao;
                            txtCodProduto.Texts = produto.Id.ToString();
                            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", produto.ValorVenda);
                            txtValorTotalItem.Texts = string.Format("{0:0.00}", produto.ValorVenda * decimal.Parse(txtQuantidadeItem.Texts));
                            calculaTotalItem();
                            txtQuantidadeItem.Focus();
                        }
                        else
                        {

                            txtCodProduto.Texts = "";
                            txtPesquisaProduto.Texts = "";
                            GenericaDesktop.ShowAlerta("Produto não encontrado");
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtPesquisaProduto.Texts = "";
                    txtCodProduto.Texts = "";
                    GenericaDesktop.ShowAlerta("Produto não encontrado");
                }
            }
        }

        private void calculaTotalItem()
        {
            try
            {
                if (String.IsNullOrEmpty(txtQuantidadeItem.Texts))
                    txtQuantidadeItem.Texts = "1";
                if (String.IsNullOrEmpty(txtDescontoItem.Texts))
                    txtDescontoItem.Texts = "0,00";
                if (String.IsNullOrEmpty(txtAcrescimoItem.Texts))
                    txtAcrescimoItem.Texts = "0,00";

                txtValorTotalItem.Texts = string.Format("{0:0.00}", ((decimal.Parse(txtValorUnitarioItem.Texts) * decimal.Parse(txtQuantidadeItem.Texts)) - decimal.Parse(txtDescontoItem.Texts)) + decimal.Parse(txtAcrescimoItem.Texts));
            }
            catch
            {

            }
        }

        private void calculaTotalServico()
        {
            try
            {
                if (String.IsNullOrEmpty(txtQuantidadeServico.Texts))
                    txtQuantidadeServico.Texts = "1";
                if (String.IsNullOrEmpty(txtDescontoServico.Texts))
                    txtDescontoServico.Texts = "0,00";
                if (String.IsNullOrEmpty(txtAcrescimoServico.Texts))
                    txtAcrescimoServico.Texts = "0,00";

                txtValorTotalServico.Texts = string.Format("{0:0.00}", ((decimal.Parse(txtValorUnitarioServico.Texts) * decimal.Parse(txtQuantidadeServico.Texts)) - decimal.Parse(txtDescontoServico.Texts)) + decimal.Parse(txtAcrescimoServico.Texts));
            }
            catch
            {

            }
        }

        private void txtQuantidadeItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtQuantidadeItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtValorUnitarioItem.Focus();
            }
        }

        private void txtValorUnitarioItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValorUnitarioItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDescontoItem.Focus();
            }
        }

        private void txtDescontoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoItem.Texts, e);
            if (e.KeyChar == 13)
            {
                txtAcrescimoItem.Focus();
            }
        }

        private void txtAcrescimoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtAcrescimoItem.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmaItem.Focus();
                btnConfirmaItem.PerformClick();
            }
        }

        private void txtQuantidadeItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantidadeItem.Texts.Substring(0, 1).Equals(","))
                    throw new Exception();
                double value = double.Parse(txtQuantidadeItem.Texts); 
            } 
            catch 
            { 
                GenericaDesktop.ShowAlerta("Quantidade Inválida!");txtQuantidadeItem.Focus(); 
            }
            calculaTotalItem();
        }

        private void txtValorUnitarioItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void txtDescontoItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void txtAcrescimoItem_Leave(object sender, EventArgs e)
        {
            calculaTotalItem();
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void PesquisarServico(string valor)
        
        {
            IList<Servico> listaServicos = new List<Servico>();
            
            txtQuantidadeServico.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioServico.TextAlign = HorizontalAlignment.Center;
            txtValorTotalServico.TextAlign = HorizontalAlignment.Center;

            String valorAux = "";
            valorAux = valor;

            if (valor.Contains("*"))
                valor = valor.Substring(valor.IndexOf("*") + 1);

            listaServicos = servicoController.selecionarServicoComVariosFiltros(valor, Sessao.empresaFilialLogada);
            if (listaServicos.Count == 1)
            {
                foreach (Servico servico in listaServicos)
                {
                    txtPesquisaServico.Texts = servico.Descricao;
                    txtQuantidadeServico.Texts = "1";
                    txtValorUnitarioServico.Texts = string.Format("{0:0.00}", servico.Valor);
                    txtValorTotalServico.Texts = string.Format("{0:0.00}", servico.Valor);
                    txtDescontoServico.Texts = string.Format("{0:0.00}", 0);
                    txtAcrescimoServico.Texts = string.Format("{0:0.00}", 0);
                    txtCodServico.Texts = servico.Id.ToString();
                    this.servico = servico;
                    if (valorAux.Contains("*"))
                        txtQuantidadeServico.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));

                    txtQuantidadeServico.Focus();
                    txtQuantidadeServico.Select();
                    
                }
            }
            else if (listaServicos.Count > 1)
            {
                Object servicoObjeto = new Servico();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Servico", "and CONCAT(Tabela.Id, ' ', Tabela.Descricao) like '%" + valor + "%'"))
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
                        switch (uu.showModal("Servico", "", ref servicoObjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmServicoCadastro form = new FrmServicoCadastro();
                                if (form.showModalNovo(ref servicoObjeto) == DialogResult.OK)
                                {
                                    txtPesquisaServico.Texts = ((Servico)servicoObjeto).Descricao;
                                    txtQuantidadeServico.Texts = "1";
                                    txtValorUnitarioServico.Texts = string.Format("{0:0.00}", ((Servico)servicoObjeto).Valor);
                                    txtValorTotalServico.Texts = string.Format("{0:0.00}", ((Servico)servicoObjeto).Valor);
                                    txtCodServico.Texts = ((Servico)servicoObjeto).Id.ToString();
                                    this.servico = ((Servico)servicoObjeto);
                                    if (valorAux.Contains("*"))
                                        txtQuantidadeServico.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                    txtQuantidadeServico.Focus();
                                    txtQuantidadeServico.SelectAll();
                                    
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtPesquisaServico.Texts = ((Servico)servicoObjeto).Descricao;
                                txtQuantidadeServico.Texts = "1";
                                txtValorUnitarioServico.Texts = string.Format("{0:0.00}", ((Servico)servicoObjeto).Valor);
                                txtValorTotalServico.Texts = string.Format("{0:0.00}", ((Servico)servicoObjeto).Valor);
                                txtCodServico.Texts = ((Servico)servicoObjeto).Id.ToString();
                                this.servico = ((Servico)servicoObjeto);
                                if (valorAux.Contains("*"))
                                    txtQuantidadeServico.Texts = valorAux.Substring(0, valorAux.IndexOf("*"));
                                txtQuantidadeServico.Focus();
                                txtQuantidadeServico.SelectAll();
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
                // GenericaDesktop.ShowInfo("Função de pesquisa extra em desenvolvimento...");
            }
            else
            {
                GenericaDesktop.ShowAlerta("Serviço não encontrado");
                txtPesquisaServico.SelectAll();
            }
        }

        private void btnPesquisaServico_Click(object sender, EventArgs e)
        {
            PesquisarServico("");
        }

        private void txtPesquisaServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisarServico(txtPesquisaServico.Texts);
            }
        }

        private void txtCodServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodServico.Texts))
                    {
                        servico = new Servico();
                        txtPesquisaServico.Texts = "";
                        servico.Id = int.Parse(txtCodServico.Texts);
                        servico = (Servico)servicoController.selecionar(servico);
                        if (servico != null)
                        {
                            txtPesquisaServico.Texts = servico.Descricao;
                            txtCodServico.Texts = servico.Id.ToString();
                            txtValorUnitarioServico.Texts = string.Format("{0:0.00}", servico.Valor);
                            txtValorTotalServico.Texts = string.Format("{0:0.00}", servico.Valor * decimal.Parse(txtQuantidadeServico.Texts));
                            calculaTotalServico();                          
                            txtQuantidadeServico.Focus();
                        }
                        else
                        {

                            txtCodServico.Texts = "";
                            txtPesquisaServico.Texts = "";
                            GenericaDesktop.ShowAlerta("Serviço não encontrado");
                        }
                    }
                }
                catch (Exception erro)
                {
                    txtCodServico.Texts = "";
                    txtPesquisaServico.Texts = "";
                    GenericaDesktop.ShowAlerta("Serviço não encontrado");
                }
            }
        }

        private void txtQuantidadeServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtQuantidadeServico.Texts, e);
            if (e.KeyChar == 13)
            {
                txtValorUnitarioServico.Focus();
            }
        }

        private void txtValorUnitarioServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtValorUnitarioServico.Texts, e);
            if (e.KeyChar == 13)
            {
                txtDescontoServico.Focus();
            }
        }

        private void txtDescontoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtDescontoServico.Texts, e);
            if (e.KeyChar == 13)
            {
                txtAcrescimoServico.Focus();
            }
        }

        private void txtAcrescimoServico_KeyPress(object sender, KeyPressEventArgs e)
        {
            genericaDesktop.SoNumeroEVirgula(txtAcrescimoServico.Texts, e);
            if (e.KeyChar == 13)
            {
                btnConfirmaServico.Focus();
                btnConfirmaServico.PerformClick();
            }
        }

        private void btnConfirmaServico_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodServico.Texts))
            {
                servico = new Servico();
                servico.Id = int.Parse(txtCodServico.Texts);
                servico = (Servico)Controller.getInstance().selecionar(servico);
                inserirServico(servico);
            }
            else
            {
                GenericaDesktop.ShowAlerta("Selecione um serviço");
            }
        }

        private void inserirServico(Servico servico)
        {
            int posicaoItem = gridServico.RowCount;
            txtQuantidadeServico.TextAlign = HorizontalAlignment.Center;
            txtValorUnitarioServico.TextAlign = HorizontalAlignment.Center;
            txtValorTotalServico.TextAlign = HorizontalAlignment.Center;
            try
            {
                System.Data.DataRow row = dsServico.Tables[0].NewRow();
                row.SetField("Id", "0");
                row.SetField("Codigo", servico.Id.ToString());
                row.SetField("Descricao", servico.Descricao);
                decimal valorUnitForm = decimal.Parse(txtValorUnitarioServico.Texts);
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Quantidade", txtQuantidadeServico.Texts);
                decimal valorTotal = decimal.Parse(txtValorTotalServico.Texts);
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("Desconto", string.Format("{0:0.00}", decimal.Parse(txtDescontoServico.Texts)));
                row.SetField("Acrescimo", string.Format("{0:0.00}", decimal.Parse(txtAcrescimoServico.Texts)));
                dsServico.Tables[0].Rows.Add(row);

                txtPesquisaServico.Texts = "";
                somaEnquantoDigitaServicos();
                txtQuantidadeServico.Texts = "1";
                txtValorUnitarioServico.Texts = "0,00";
                txtValorTotalServico.Texts = "0,00";
                txtDescontoServico.Texts = "0,00";
                txtAcrescimoServico.Texts = "0,00";
                txtCodServico.Texts = "";
                txtPesquisaServico.Focus();

                this.servico = new Servico();

                if (this.gridServico.View != null)
                {
                    if (this.gridServico.View.Records.Count > 0)
                    {
                        gridServico.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        this.gridServico.Columns["Descricao"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridServico.AutoSizeController.Refresh();
                    }
                }

                txtPesquisaServico.Focus();

            }
            catch
            {
                GenericaDesktop.ShowErro("Insira corretamente o serviço, quantidade e valor");
            }
        }

        private void txtQuantidadeServico_Leave(object sender, EventArgs e)
        {
            calculaTotalServico();
        }

        private void txtValorUnitarioServico_Leave(object sender, EventArgs e)
        {
            calculaTotalServico();
        }

        private void txtDescontoServico_Leave(object sender, EventArgs e)
        {
            calculaTotalServico();
        }

        private void txtAcrescimoServico_Leave(object sender, EventArgs e)
        {
            calculaTotalServico();
        }

        private void somaEnquantoDigitaServicos()
        {
            try
            {
                decimal valorTotalServicos = 0;
                double pecas = 0;
                if (gridServico.View != null)
                {
                    var records = gridServico.View.Records;
                    decimal descontoItem = 0;
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;

                        if (!String.IsNullOrEmpty(dataRowView.Row[0].ToString()))
                            descontoItem = descontoItem + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        valorTotalServicos = valorTotalServicos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                        pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());

                    }
                    txtValorTotalTodosServicos.Texts = string.Format("{0:0.00}", valorTotalServicos);
                    txtTotalGeralProdutoServico.Texts = string.Format("{0:0.00}", valorTotalServicos + decimal.Parse(txtValorTotalTodosProdutos.Texts));
                }
            }
            catch
            {

            }
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (gridProdutos.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o produto selecionado?"))
                {
                    var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if(id != "0")
                    {
                        OrdemServicoProduto ordemServicoProduto = new OrdemServicoProduto();
                        ordemServicoProduto.Id = int.Parse(id);
                        ordemServicoProduto = (OrdemServicoProduto)Controller.getInstance().selecionar(ordemServicoProduto);
                        if (ordemServicoProduto != null)
                            Controller.getInstance().excluir(ordemServicoProduto);
                    }
                    dsProdutos.Tables[0].Rows[gridProdutos.SelectedIndex].Delete();
                    somaEnquantoDigitaItens();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o produto que deseja excluir!");
        }

        private void btnExcluirServico_Click(object sender, EventArgs e)
        {
            if (gridServico.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o serviço selecionado?"))
                {
                    var selectedItem = this.gridServico.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        OrdemServicoServico ordemServicoServico = new OrdemServicoServico();
                        ordemServicoServico.Id = int.Parse(id);
                        ordemServicoServico = (OrdemServicoServico)Controller.getInstance().selecionar(ordemServicoServico);
                        if (ordemServicoServico != null)
                            Controller.getInstance().excluir(ordemServicoServico);
                    }
                    dsServico.Tables[0].Rows[gridServico.SelectedIndex].Delete();
                    somaEnquantoDigitaServicos();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o serviço que deseja excluir!");
        }

        private void btnPesquisaDependente_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Object pessoaDependenteOjeto = new PessoaDependente();
                Form formBackground = new Form();
                try
                {
                    using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("PessoaDependente", "and Tabela.Pessoa = " + txtCodCliente.Texts))
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
                        switch (uu.showModal("PessoaDependente", "", ref pessoaDependenteOjeto))
                        {
                            case DialogResult.Ignore:
                                uu.Dispose();
                                FrmClienteCadastro form = new FrmClienteCadastro();
                                if (form.showModalNovo(ref pessoaDependenteOjeto) == DialogResult.OK)
                                {
                                    txtDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Nome;
                                    txtCodDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Id.ToString();
                                    txtExaminador.Focus();
                                }
                                form.Dispose();
                                break;
                            case DialogResult.OK:
                                txtDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Nome;
                                txtCodDependente.Texts = ((PessoaDependente)pessoaDependenteOjeto).Id.ToString();
                                txtExaminador.Focus();
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
            else
            {
                GenericaDesktop.ShowAlerta("Você deve selecionar um cliente primeiro!");
                btnPesquisaCliente.PerformClick();
            }
        }
        private void pesquisarDependente()
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)pessoaController.selecionar(pessoa);
                if (pessoa != null)
                {
                    IList<PessoaDependente> listaDependente = pessoaDependenteController.selecionarDependentePorPessoa(pessoa.Id);
                    if(listaDependente.Count > 0)
                    {

                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Primeiro selecione um cliente corretamente");
                    btnPesquisaCliente.PerformClick();
                }

            }
            else
            {
                GenericaDesktop.ShowAlerta("Você deve selecionar um cliente primeiro!");
                btnPesquisaCliente.PerformClick();
            }
        }

        private void btnGravarExame_Click(object sender, EventArgs e)
        {
            inserirExame();
        }

        private void inserirExame()
        {
            try
            {
                //verifica_posicao_item();
                System.Data.DataRow row = dsExame.Tables[0].NewRow();
                int idEdit = -1;
                if (!String.IsNullOrEmpty(txtIdExame.Texts))
                    idEdit = int.Parse(txtIdExame.Texts);
                if(idEdit > 0) 
                { 
                    OrdemServicoExame ordemServicoExame = new OrdemServicoExame();
                    ordemServicoExame.Id = int.Parse(txtIdExame.Texts);
                    OrdemServicoExameController.getInstance().excluir(ordemServicoExame);
                    retornarExamesAposEditar();
                }
                else if(idEdit == 0)
                {
                    dsExame.Tables[0].Rows[gridExames.SelectedIndex].Delete();
                }
                row.SetField("Id", "0");
                row.SetField("CodDependente", txtCodDependente.Texts);
                row.SetField("Dependente", txtDependente.Texts);
                row.SetField("Examinador", txtExaminador.Texts);
                row.SetField("DataExame", txtDataExame.Value.ToString());
                row.SetField("LDEsferico", txtLDEsferico.Texts);
                row.SetField("LDCilindrico", txtLDCilindrico.Texts);
                row.SetField("LDPosicao", txtLDPosicao.Texts);
                row.SetField("LDDp", txtLDDp.Texts);
                row.SetField("LDAltura", txtLDAltura.Texts);
                row.SetField("LEEsferico", txtLEEsferico.Texts);
                row.SetField("LECilindrico", txtLECilindrico.Texts);
                row.SetField("LEPosicao", txtLEPosicao.Texts);
                row.SetField("LEDp", txtLEDp.Texts);
                row.SetField("LEAltura", txtLEAltura.Texts);
                row.SetField("PDEsferico", txtPDEsferico.Texts);
                row.SetField("PDCilindrico", txtPDCilindrico.Texts);
                row.SetField("PDPosicao", txtPDPosicao.Texts);
                row.SetField("PDDp", txtPDDp.Texts);
                row.SetField("PDAltura", txtPDAltura.Texts);
                row.SetField("PEEsferico", txtPEEsferico.Texts);
                row.SetField("PECilindrico", txtPECilindrico.Texts);
                row.SetField("PEPosicao", txtPEPosicao.Texts);
                row.SetField("PEDp", txtPEDp.Texts);
                row.SetField("PEAltura", txtPEAltura.Texts);
                row.SetField("Armacao", txtArmacao.Texts);
                row.SetField("Lentes", txtLentes.Texts);
                row.SetField("ProximoExame", txtProximoExame.Text);
                row.SetField("Adicao", txtAdicao.Texts);
                row.SetField("DataEntrega", txtDataEntrega.Text);
                dsExame.Tables[0].Rows.Add(row);
                limparDadosExame();
                if (this.gridExames.View != null)
                {
                    if (this.gridExames.View.Records.Count > 0)
                    {
                        gridExames.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                        //this.gridProdutos.ColumnSizer = GridLengthUnitType.AutoLastColumnFill;
                        this.gridExames.Columns["Dependente"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                        gridExames.AutoSizeController.Refresh();
                    }
                }

                tabControlAdv1.SelectedTab = tabExames;

            }
            catch
            {
                GenericaDesktop.ShowErro("Dados Inválidos!");
            }
        }

        private void limparDadosExame()
        {
            txtIdExame.Texts = "";
            txtDependente.Texts = "";
            txtCodDependente.Texts = "";
            txtExaminador.Texts = "";
            txtLDEsferico.Texts = "";
            txtLDCilindrico.Texts = "";
            txtLDPosicao.Texts = "";
            txtLDDp.Texts = "";
            txtLDAltura.Texts = "";
            txtLEEsferico.Texts = "";
            txtLECilindrico.Texts = "";
            txtLEPosicao.Texts = "";
            txtLEDp.Texts = "";
            txtLEAltura.Texts = "";
            txtPDEsferico.Texts = "";
            txtPDCilindrico.Texts = "";
            txtPDPosicao.Texts = "";
            txtPDDp.Texts = "";
            txtPDAltura.Texts = "";
            txtPEEsferico.Texts = "";
            txtPECilindrico.Texts = "";
            txtPEPosicao.Texts = "";
            txtPEDp.Texts = "";
            txtPEAltura.Texts = "";
            txtArmacao.Texts = "";
            txtLentes.Texts = "";
            txtProximoExame.Text = "";
            txtAdicao.Texts = "";
            txtDataEntrega.Text = "";
        }

        private void btnBaixo_Click(object sender, EventArgs e)
        {
            txtPDEsferico.Texts = txtLDEsferico.Texts;
            txtPDCilindrico.Texts = txtLDCilindrico.Texts;
            txtPDPosicao.Texts = txtLDPosicao.Texts;
            txtPDDp.Texts = txtLDDp.Texts;
            txtPDAltura.Texts = txtLDAltura.Texts;
            //PERTO
            txtPEEsferico.Texts = txtLEEsferico.Texts;
            txtPECilindrico.Texts = txtLECilindrico.Texts;
            txtPEPosicao.Texts = txtLEPosicao.Texts;
            txtPEDp.Texts = txtLEDp.Texts;
            txtPEAltura.Texts = txtLEAltura.Texts;
        }

        private void btnCima_Click(object sender, EventArgs e)
        {
            txtLDEsferico.Texts = txtPDEsferico.Texts;
            txtLDCilindrico.Texts = txtPDCilindrico.Texts;
            txtLDPosicao.Texts = txtPDPosicao.Texts;
            txtLDDp.Texts = txtPDDp.Texts;
            txtLDAltura.Texts = txtPDAltura.Texts;
            //PERTO
            txtLEEsferico.Texts = txtPEEsferico.Texts;
            txtLECilindrico.Texts = txtPECilindrico.Texts;
            txtLEPosicao.Texts = txtPEPosicao.Texts;
            txtLEDp.Texts = txtPEDp.Texts;
            txtLEAltura.Texts = txtPEAltura.Texts;
        }

        private void set_OrdemServico()
        {
            ordemServico = new OrdemServico();
            if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
            {
                ordemServico.Id = int.Parse(txtNumeroOS.Texts);
                ordemServico = (OrdemServico)Controller.getInstance().selecionar(ordemServico);
            }
            else 
            {
                ordemServico.Id = 0;
            }
            if (vendedor != null && !String.IsNullOrEmpty(txtVendedor.Texts))
                ordemServico.Vendedor = vendedor;
            else
                ordemServico.Vendedor = null;
            ordemServico.OperadorCadastro = Sessao.usuarioLogado.Id.ToString();
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                TipoObjeto tipoObjeto = new TipoObjeto();
                tipoObjeto.Id = int.Parse(txtCodTipoObjeto.Texts);
                tipoObjeto = (TipoObjeto)Controller.getInstance().selecionar(tipoObjeto);
                if (pessoa != null && tipoObjeto != null)
                {
                    ordemServico.Cliente = pessoa;
                    ordemServico.DataAbertura = DateTime.Parse(txtDataAbertura.Value.ToString());
                    ordemServico.DataEncerramento = DateTime.Parse("1900-01-01 00:00:00");
                    ordemServico.Filial = Sessao.empresaFilialLogada;
                    ordemServico.NumeroSerie = txtNumeroSerie.Texts;
                    ordemServico.Observacoes = txtObservacoes.Texts;
                    ordemServico.TipoObjeto = tipoObjeto;
                    ordemServico.Status = "ABERTA";
                    capturarProdutosServicosExamesAnexos();

                    ordemServicoController.salvarOrdemServicoComItensAdicionais(ordemServico, listaProdutos, listaServicos, listaExames, listaAnexo);
                    lblAutomatico.Visible = false;
                    txtNumeroOS.Texts = ordemServico.Id.ToString();
                    if (GenericaDesktop.ShowConfirmacao("Ordem de Serviço " + ordemServico.Id + " Registrada com Sucesso, deseja imprimir?"))
                    {
                        FrmImpressaoOrdemServico frmImprimirOrdem = new FrmImpressaoOrdemServico(ordemServico);
                        frmImprimirOrdem.ShowDialog();
                    }
                }
                else
                    GenericaDesktop.ShowErro("Você deve selecionar um cliente e um tipo de Objeto para realizar a Ordem de Serviço!");              
            }
        }

        private void capturarProdutosServicosExamesAnexos()
        {
            listaProdutos = new List<OrdemServicoProduto>();
            listaServicos = new List<OrdemServicoServico>();
            listaExames = new List<OrdemServicoExame>();
            listaAnexo = new List<Anexo>();

            decimal acrescimoProdutos = 0;
            decimal descontoProdutos = 0;
            decimal totalProdutos = 0;

            decimal acrescimoServicos = 0;
            decimal descontoServicos = 0;
            decimal totalServicos = 0;

            if (gridProdutos.View != null)
            {
                var records = gridProdutos.View.Records;
                if(records.Count > 0) {
                    foreach (var record in records)
                    {
                        var dataRowView = record.Data as DataRowView;
                        acrescimoProdutos = acrescimoProdutos + decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                        descontoProdutos = descontoProdutos + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        totalProdutos = totalProdutos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());

                        produto = new Produto();
                        OrdemServicoProduto ordemServicoProduto = new OrdemServicoProduto();
                        produto.Id = int.Parse(dataRowView.Row["Codigo"].ToString());
                        produto = (Produto)Controller.getInstance().selecionar(produto);
                        if (produto != null)
                        {
                            ordemServicoProduto = new OrdemServicoProduto();
                            if(int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                            {
                                ordemServicoProduto.Id = int.Parse(dataRowView.Row["Id"].ToString());
                            }
                            ordemServicoProduto.DescricaoProduto = dataRowView.Row["Descricao"].ToString();
                            ordemServicoProduto.Desconto = decimal.Parse(dataRowView.Row["Desconto"].ToString());
                            ordemServicoProduto.Acrescimo = decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                            ordemServicoProduto.OrdemServico = ordemServico;
                            ordemServicoProduto.Produto = produto;
                            ordemServicoProduto.Quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                            ordemServicoProduto.ValorUnitario = decimal.Parse(dataRowView.Row["ValorUnitario"].ToString());
                            ordemServicoProduto.ValorTotal = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                            listaProdutos.Add(ordemServicoProduto);
                        }
                    }
                }
            }

            if (gridServico.View != null)
            {
                var recordsServico = gridServico.View.Records;
                if (recordsServico.Count > 0)
                {
                    foreach (var record in recordsServico)
                    {
                        var dataRowView = record.Data as DataRowView;
                        acrescimoServicos = acrescimoServicos + decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                        descontoServicos = descontoServicos + decimal.Parse(dataRowView.Row["Desconto"].ToString());
                        totalServicos = totalServicos + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());

                        servico = new Servico();
                        OrdemServicoServico ordemServicoServico = new OrdemServicoServico();
                        servico.Id = int.Parse(dataRowView.Row["Codigo"].ToString());
                        servico = (Servico)Controller.getInstance().selecionar(servico);
                        if (servico != null)
                        {
                            ordemServicoServico = new OrdemServicoServico();
                            if (int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                            {
                                ordemServicoServico.Id = int.Parse(dataRowView.Row["Id"].ToString());
                            }
                            ordemServicoServico.DescricaoServico = dataRowView.Row["Descricao"].ToString();
                            ordemServicoServico.Desconto = decimal.Parse(dataRowView.Row["Desconto"].ToString());
                            ordemServicoServico.Acrescimo = decimal.Parse(dataRowView.Row["Acrescimo"].ToString());
                            ordemServicoServico.OrdemServico = ordemServico;
                            ordemServicoServico.Servico = servico;
                            ordemServicoServico.Quantidade = double.Parse(dataRowView.Row["Quantidade"].ToString());
                            ordemServicoServico.ValorUnitario = decimal.Parse(dataRowView.Row["ValorUnitario"].ToString());
                            ordemServicoServico.ValorTotal = decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                            listaServicos.Add(ordemServicoServico);
                        }
                    }
                }
            }

            ordemServico.ValorAcrescimo = acrescimoProdutos + acrescimoServicos;
            ordemServico.ValorDesconto = descontoProdutos + descontoServicos;
            ordemServico.ValorProduto = totalProdutos;
            ordemServico.ValorServico = totalServicos;
            ordemServico.ValorTotal = totalProdutos + totalServicos;

            //Exames
            if (gridExames.View != null)
            {
                var recordsExames = gridExames.View.Records;
                if (recordsExames.Count > 0)
                {
                    foreach (var record in recordsExames)
                    {
                        var dataRowView = record.Data as DataRowView;
                        OrdemServicoExame ordemServicoExame = new OrdemServicoExame();
                        PessoaDependente pessoaDependente = new PessoaDependente();
                        try
                        {
                            if (int.Parse(dataRowView.Row["CodDependente"].ToString()) > 0)
                            {
                                pessoaDependente.Id = int.Parse(dataRowView.Row["CodDependente"].ToString());
                                pessoaDependente = (PessoaDependente)Controller.getInstance().selecionar(pessoaDependente);
                                if (pessoaDependente != null)
                                    ordemServicoExame.Dependente = pessoaDependente;
                                else
                                    ordemServicoExame.Dependente = null;
                            }
                            else
                                ordemServicoExame.Dependente = null;
                        }
                        catch { ordemServicoExame.Dependente = null; }

                        if (int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                        {
                            ordemServicoExame.Id = int.Parse(dataRowView.Row["Id"].ToString());
                        }
                        ordemServicoExame.Examinador = dataRowView.Row["Examinador"].ToString();
                        ordemServicoExame.DataExame = DateTime.Parse(dataRowView.Row["DataExame"].ToString());
                        //Longe Direito
                        ordemServicoExame.LdEsferico = dataRowView.Row["LDEsferico"].ToString();
                        ordemServicoExame.LdCilindrico = dataRowView.Row["LDCilindrico"].ToString();
                        ordemServicoExame.LdPosicao = dataRowView.Row["LDPosicao"].ToString();
                        ordemServicoExame.LdDp = dataRowView.Row["LDDp"].ToString();
                        ordemServicoExame.LdAltura = dataRowView.Row["LDAltura"].ToString();
                        //Longe Esquerdo
                        ordemServicoExame.LeEsferico = dataRowView.Row["LEEsferico"].ToString();
                        ordemServicoExame.LeCilindrico = dataRowView.Row["LECilindrico"].ToString();
                        ordemServicoExame.LePosicao = dataRowView.Row["LEPosicao"].ToString();
                        ordemServicoExame.LeDp = dataRowView.Row["LEDp"].ToString();
                        ordemServicoExame.LeAltura = dataRowView.Row["LEAltura"].ToString();
                        //Perto Direito
                        ordemServicoExame.PdEsferico = dataRowView.Row["PDEsferico"].ToString();
                        ordemServicoExame.PdCilindrico = dataRowView.Row["PDCilindrico"].ToString();
                        ordemServicoExame.PdPosicao = dataRowView.Row["PDPosicao"].ToString();
                        ordemServicoExame.PdDp = dataRowView.Row["PDDp"].ToString();
                        ordemServicoExame.PdAltura = dataRowView.Row["PDAltura"].ToString();
                        //Perto Esquerdo
                        ordemServicoExame.PeEsferico = dataRowView.Row["PEEsferico"].ToString();
                        ordemServicoExame.PeCilindrico = dataRowView.Row["PECilindrico"].ToString();
                        ordemServicoExame.PePosicao = dataRowView.Row["PEPosicao"].ToString();
                        ordemServicoExame.PeDp = dataRowView.Row["PEDp"].ToString();
                        ordemServicoExame.PeAltura = dataRowView.Row["PEAltura"].ToString();

                        ordemServicoExame.Armacao = dataRowView.Row["Armacao"].ToString();
                        ordemServicoExame.Lente = dataRowView.Row["Lentes"].ToString();

                        if (dataRowView.Row["ProximoExame"].ToString() != "  /  /    ")
                            ordemServicoExame.ProximoExame = dataRowView.Row["ProximoExame"].ToString();
                        else
                            ordemServicoExame.ProximoExame = "";

                        ordemServicoExame.Adicao = dataRowView.Row["Adicao"].ToString();

                        if (dataRowView.Row["DataEntrega"].ToString() != "  /  /    ")
                            ordemServicoExame.DataEntrega = DateTime.Parse(dataRowView.Row["DataEntrega"].ToString());
                        else
                            ordemServicoExame.DataEntrega = DateTime.Parse("1900-01-01 00:00:00");

                        ordemServicoExame.OrdemServico = ordemServico;
                        ordemServicoExame.Pessoa = ordemServico.Cliente;

                        listaExames.Add(ordemServicoExame);
                    }
                }
            }

            //ListaAnexo
            if (gridAnexos.View != null)
            {
                var recordAnexo = gridAnexos.View.Records;
                if (recordAnexo.Count > 0)
                {
                    foreach (var record in recordAnexo)
                    {
                        var dataRowView = record.Data as DataRowView;
                        Anexo anexo = new Anexo();
                        anexo = new Anexo();
                        if (int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                        {
                            anexo.Id = int.Parse(dataRowView.Row["Id"].ToString());
                        }
                        anexo.Codigo = dataRowView.Row["Codigo"].ToString();
                        anexo.Caminho = dataRowView.Row["Caminho"].ToString();
                        anexo.Filial = Sessao.empresaFilialLogada;
                        anexo.OrdemServico = ordemServico;
                        listaAnexo.Add(anexo);
                    }
                }
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            set_OrdemServico();
            this.Close();
        }

        private void gridServico_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void gridExames_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnExcluirExame_Click(object sender, EventArgs e)
        {
            if (gridExames.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o exame selecionado?"))
                {
                    var selectedItem = this.gridExames.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        OrdemServicoExame ordemServicoExame = new OrdemServicoExame();
                        ordemServicoExame.Id = int.Parse(id);
                        ordemServicoExame = (OrdemServicoExame)Controller.getInstance().selecionar(ordemServicoExame);
                        if (ordemServicoExame != null)
                            Controller.getInstance().excluir(ordemServicoExame);
                    }
                    dsExame.Tables[0].Rows[gridExames.SelectedIndex].Delete();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o exame que deseja excluir!");
        }

        private void btnGravarEncerrar_Click(object sender, EventArgs e)
        {
            if (GenericaDesktop.ShowConfirmacao("Deseja realmente encerrar a O.S? "))
            {
                set_OrdemServico();
                if (!ordemServico.Status.Equals("ENCERRADA"))
                {
                    Form formBackground = new Form();
                    IList<ContaReceber> listaReceber = new List<ContaReceber>();
                    IList<ContaPagar> listaPagar = new List<ContaPagar>();
                    IList<OrdemServico> listaOs = new List<OrdemServico>();
                    FrmPagamentoRecebimento uu = new FrmPagamentoRecebimento(listaReceber, listaPagar, ordemServico, "ORDEMSERVICO", false, true, listaOs);
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
                    this.Close();
                }
            }
        }

        private void FrmOrdemServico_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if(GenericaDesktop.ShowConfirmacao("Deseja realmente sair sem salvar?"))
                        this.Close();
                    break;

                case Keys.F2:
                    tabControlAdv2.SelectedTab = tabPageAdv1;
                    txtPesquisaProduto.Focus();
                    break;
                case Keys.F3:
                    tabControlAdv2.SelectedTab = tabPageAdv2;
                    txtPesquisaServico.Focus();
                    break;
                case Keys.F5:
                    if (ordemServico.Status != "ENCERRADA")
                        btnGravar.PerformClick();
                    else GenericaDesktop.ShowAlerta("Não é possível alterar uma o.s encerrada!");
                    break;
            }
        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.CaminhoAnexo))
                {
                    OpenFileDialog file = new OpenFileDialog();
                    file.Filter = "Imagens (*.BMP;*.JPEG;*.JPG;*.GIF,*.PNG,*.TIFF,*.JFIF)|*.BMP;*.JPEG;*.JPG;*.GIF;*.PNG;*.TIFF;*.JFIF|" + "All files (*.*)|*.*";
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        string extensao = Path.GetExtension(file.FileName);
                        Random randNum = new Random();
                        string num = randNum.Next(1000, 1000000).ToString();
                        string caminhoNovo = Sessao.parametroSistema.CaminhoAnexo + @"\OS_"+txtCodCliente.Texts + "_" + num + extensao;
                        try 
                        { 
                            File.Copy(file.FileName, caminhoNovo, true);
                            inserirAnexo(caminhoNovo, num);
                            //Se Ordem de Serviço está encerrada ele ja grava a imagem
                            if (!String.IsNullOrEmpty(txtNumeroOS.Texts))
                            {
                                //ListaAnexo
                                if (gridAnexos.View != null)
                                {
                                    var recordAnexo = gridAnexos.View.Records;
                                    if (recordAnexo.Count > 0)
                                    {
                                        foreach (var record in recordAnexo)
                                        {
                                            var dataRowView = record.Data as DataRowView;
                                            Anexo anexo = new Anexo();
                                            anexo = new Anexo();
                                            if (int.Parse(dataRowView.Row["Id"].ToString()) > 0)
                                            {
                                                anexo.Id = int.Parse(dataRowView.Row["Id"].ToString());
                                            }
                                            anexo.Codigo = dataRowView.Row["Codigo"].ToString();
                                            anexo.Caminho = dataRowView.Row["Caminho"].ToString();
                                            anexo.Filial = Sessao.empresaFilialLogada;
                                            anexo.OrdemServico = ordemServico;
                                            Controller.getInstance().salvar(anexo);
                                        }
                                    }
                                }
                            }
                        } 
                        catch (Exception rrr)
                        {
                            GenericaDesktop.ShowErro("Falha ao transferir a imagem para a pasta de destino " + rrr.Message);
                        }
                        //parametro.Logo = txtCaminhoLogo.Text;
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Não foi configurado o caminho para salvar os arquivos em anexo, configure dentro do menu " +
                        "Utilitários/Parâmetros do Sistema/Configurações da Empresa/CaminhoAnexo e selecione uma pasta em rede local!");
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void inserirAnexo(string caminho, string codigo)
        {
            System.Data.DataRow row = dsAnexo.Tables[0].NewRow();
            row.SetField("Id", 0);
            row.SetField("Codigo", "OS_" + txtCodCliente.Texts + "_" + codigo);
            row.SetField("Caminho", caminho);
            dsAnexo.Tables[0].Rows.Add(row);
        }

        private void removerAnexo()
        {
            if (gridAnexos.SelectedItems.Count > 0)
            {
                if (GenericaDesktop.ShowConfirmacao("Deseja excluir o anexo selecionado?"))
                {
                    var selectedItem = this.gridAnexos.CurrentItem as DataRowView;
                    var dataRow = (selectedItem as DataRowView).Row;
                    string id = dataRow["Id"].ToString();
                    if (id != "0")
                    {
                        Anexo anexo = new Anexo();
                        anexo.Id = int.Parse(id);
                        anexo = (Anexo)Controller.getInstance().selecionar(anexo);
                        if (anexo != null)
                            Controller.getInstance().excluir(anexo);
                    }
                    dsAnexo.Tables[0].Rows[gridAnexos.SelectedIndex].Delete();
                }
            }
            else
                GenericaDesktop.ShowAlerta("Primeiro selecione o anexo que deseja excluir!");
        }

        private void btnExcluirAnexo_Click(object sender, EventArgs e)
        {
            removerAnexo();
        }

        private void gridAnexos_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            try
            {
                var selectedItem = this.gridAnexos.CurrentItem as DataRowView;
                var dataRow = (selectedItem as DataRowView).Row;
                string caminho = dataRow["Caminho"].ToString();
                System.Diagnostics.Process.Start(caminho);
            }
            catch
            {
                GenericaDesktop.ShowAlerta("Falha ao abrir o arquivo, verifique se o caminho de destino " +
                    "está disponível, caso seja um caminho em rede verifique se o outro computador está ligado!");
            }
        }

        private void gridAnexos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            vendedor = new Pessoa();
            pesquisaVendedor();
            if(ordemServico.Status == "ENCERRADA")
            {
                if(vendedor != null)
                {
                    if(vendedor.Id > 0)
                    {
                        ordemServico.Vendedor = vendedor;
                        Controller.getInstance().salvar(ordemServico);
                        GenericaDesktop.ShowInfo("Vendedor Ajustado com Sucesso!");
                        this.Close();
                    }
                }
            }
        }

        private void txtVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pesquisaVendedor();
            }
        }

        private void gridExames_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (gridExames.SelectedIndex >= 0)
            {
                editarExame();
            }
            else
                GenericaDesktop.ShowAlerta("Clique na linha do exame que deseja editar!");
        }

        private void editarExame()
        {
            OrdemServicoExame exame = new OrdemServicoExame();
            var selectedItem = this.gridExames.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            txtDependente.Texts = dataRow["Dependente"].ToString();
            txtCodDependente.Texts = dataRow["CodDependente"].ToString();
            txtExaminador.Texts = dataRow["Examinador"].ToString();
            try { txtDataExame.Value = DateTime.Parse(dataRow["DataExame"].ToString()); } catch { }
            txtLDEsferico.Texts = dataRow["LDEsferico"].ToString();
            txtLDCilindrico.Texts = dataRow["LDCilindrico"].ToString();
            txtLDPosicao.Texts = dataRow["LDPosicao"].ToString();
            txtLDDp.Texts = dataRow["LDDp"].ToString();
            txtLDAltura.Texts = dataRow["LDAltura"].ToString();
            txtLEEsferico.Texts = dataRow["LEEsferico"].ToString();
            txtLECilindrico.Texts = dataRow["LECilindrico"].ToString();
            txtLEPosicao.Texts = dataRow["LEPosicao"].ToString();
            txtLEDp.Texts = dataRow["LEDp"].ToString();
            txtLEAltura.Texts = dataRow["LEAltura"].ToString();
            txtPDEsferico.Texts = dataRow["PDEsferico"].ToString();
            txtPDCilindrico.Texts = dataRow["PDCilindrico"].ToString();
            txtPDPosicao.Texts = dataRow["PDPosicao"].ToString();
            txtPDDp.Texts = dataRow["PDDp"].ToString();
            txtPDAltura.Texts = dataRow["PDAltura"].ToString();
            txtPEEsferico.Texts = dataRow["PEEsferico"].ToString();
            txtPECilindrico.Texts = dataRow["PECilindrico"].ToString();
            txtPEPosicao.Texts = dataRow["PEPosicao"].ToString();
            txtPEDp.Texts = dataRow["PEDp"].ToString();
            txtPEAltura.Texts = dataRow["PEAltura"].ToString();
            txtArmacao.Texts = dataRow["Armacao"].ToString();
            txtLentes.Texts = dataRow["Lentes"].ToString();
            txtProximoExame.Text = dataRow["ProximoExame"].ToString();
            txtAdicao.Texts = dataRow["Adicao"].ToString();
            txtDataEntrega.Text = dataRow["DataEntrega"].ToString();
            txtIdExame.Texts = dataRow["Id"].ToString();
            tabControlAdv1.SelectedTab = tabOtica;
        }

        private void retornarExamesAposEditar()
        {
            //Exames 
            dsExame.Tables[0].Clear();
            OrdemServicoExameController ordemServicoExameController = new OrdemServicoExameController();
            IList<OrdemServicoExame> listaExames = ordemServicoExameController.selecionarExamesPorOrdemServico(ordemServico.Id);
            if (listaExames.Count > 0)
            {
                foreach (OrdemServicoExame ordemServicoExame in listaExames)
                {
                    System.Data.DataRow row = dsExame.Tables[0].NewRow();
                    if (ordemServicoExame.Dependente != null)
                    {
                        row.SetField("CodDependente", ordemServicoExame.Dependente.Id.ToString());
                        row.SetField("Dependente", ordemServicoExame.Dependente.Nome);
                    }
                    else
                    {
                        row.SetField("CodDependente", "");
                        row.SetField("Dependente", "");
                    }
                    row.SetField("Examinador", ordemServicoExame.Examinador);
                    row.SetField("DataExame", ordemServicoExame.DataExame.ToShortDateString());
                    row.SetField("LDEsferico", ordemServicoExame.LdEsferico);
                    row.SetField("LDCilindrico", ordemServicoExame.LdCilindrico);
                    row.SetField("LDPosicao", ordemServicoExame.LdPosicao);
                    row.SetField("LDDp", ordemServicoExame.LdDp);
                    row.SetField("LDAltura", ordemServicoExame.LdAltura);
                    row.SetField("LEEsferico", ordemServicoExame.LeEsferico);
                    row.SetField("LECilindrico", ordemServicoExame.LeCilindrico);
                    row.SetField("LEPosicao", ordemServicoExame.LePosicao);
                    row.SetField("LEDp", ordemServicoExame.LeDp);
                    row.SetField("LEAltura", ordemServicoExame.LeAltura);
                    row.SetField("PDEsferico", ordemServicoExame.PdEsferico);
                    row.SetField("PDCilindrico", ordemServicoExame.PdCilindrico);
                    row.SetField("PDPosicao", ordemServicoExame.PdPosicao);
                    row.SetField("PDDp", ordemServicoExame.PdDp);
                    row.SetField("PDAltura", ordemServicoExame.PdAltura);
                    row.SetField("PEEsferico", ordemServicoExame.PeEsferico);
                    row.SetField("PECilindrico", ordemServicoExame.PeCilindrico);
                    row.SetField("PEPosicao", ordemServicoExame.PePosicao);
                    row.SetField("PEDp", ordemServicoExame.PeDp);
                    row.SetField("PEAltura", ordemServicoExame.PeAltura);
                    row.SetField("Armacao", ordemServicoExame.Armacao);
                    row.SetField("Lentes", ordemServicoExame.Lente);
                    row.SetField("ProximoExame", ordemServicoExame.ProximoExame);
                    row.SetField("Adicao", ordemServicoExame.Adicao);
                    row.SetField("DataEntrega", ordemServicoExame.DataEntrega);
                    row.SetField("Id", ordemServicoExame.Id.ToString());
                    dsExame.Tables[0].Rows.Add(row);
                    if (this.gridExames.View != null)
                    {
                        if (this.gridExames.View.Records.Count > 0)
                        {
                            gridExames.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                            this.gridExames.Columns["Dependente"].AutoSizeColumnsMode = AutoSizeColumnsMode.AllCellsWithLastColumnFill;
                            gridExames.AutoSizeController.Refresh();
                        }
                    }
                }
            }
        }

        private void btnDadosCliente_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = int.Parse(txtCodCliente.Texts);
                pessoa = (Pessoa)Controller.getInstance().selecionar(pessoa);
                if(pessoa != null)
                {
                    if(pessoa.Id > 0)
                    {
                        conferirContaReceber(pessoa);
                        editarCadastro(pessoa);
                    }
                }
            }
        }
        private void editarCadastro(Pessoa pessoa)
        {
            Form formBackground = new Form();
            try
            {
                using (FrmClienteCadastro uu = new FrmClienteCadastro(pessoa))
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
                    //carregarLista();
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
        private void conferirContaReceber(Pessoa pessoa)
        {
            Form formBackground = new Form();
            ContaReceberController contaReceberController = new ContaReceberController();
            IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber Tabela Where Tabela.FlagExcluido <> true and Tabela.Recebido = false");
            if (listaReceber.Count > 0)
            {
                try
                {
                    using (FrmContaReceberLista uu = new FrmContaReceberLista(pessoa))
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
                        //carregarLista();
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

        private void txtVendedor_Leave(object sender, EventArgs e)
        {
            if(vendedor.Id == 0 && !String.IsNullOrEmpty(txtVendedor.Texts))
            {
                pesquisaVendedor();
            }
        }

        private void gridProdutos_CellDoubleClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            editando = true;
            var selectedItem = this.gridProdutos.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            dataRowEdit = dataRow;
            idEdicaoProduto = 0;
            try { idEdicaoProduto = int.Parse(dataRow["Id"].ToString()); } catch { }
            var descricao = dataRow["Descricao"].ToString();
            var quantidade = dataRow["Quantidade"].ToString();
            var valorUnitario = dataRow["ValorUnitario"].ToString();
            var valorTotal = dataRow["ValorTotal"].ToString();
            var codigo = dataRow["Codigo"].ToString();

           // indexEditando = this.gridProdutos.SelectedIndex;
            txtPesquisaProduto.Texts = descricao;
            txtCodProduto.Texts = codigo;
            txtQuantidadeItem.Texts = quantidade;
            txtValorUnitarioItem.Texts = string.Format("{0:0.00}", valorUnitario);
            txtValorTotalItem.Texts = string.Format("{0:0.00}", valorTotal);
            txtValorUnitarioItem.Focus();
            txtValorUnitarioItem.SelectAll();
        }
    }
}
