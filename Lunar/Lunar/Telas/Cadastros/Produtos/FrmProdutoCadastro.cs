using Lunar.Telas.Cadastros.Produtos.Auxiliares;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Utils;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lunar.Telas.Cadastros.Produtos
{
    public partial class FrmProdutoCadastro : Form
    {
        bool showModal = false;
        public DialogResult showModalNovo(ref object produto, bool notaFiscal)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                produto = this.produto;
            }
            return DialogResult;
        }
        MarcaController marcaController = new MarcaController();
        UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
        CsosnController csosnController = new CsosnController();
        Csosn csosn = new Csosn();
        CstIcms cstIcms = new CstIcms();
        CstIcmsController cstIcmsController = new CstIcmsController();
        Produto produto = new Produto();
        Marca marca = new Marca();
        UnidadeMedida unidadeMedida = new UnidadeMedida();
        ProdutoGrupo grupo = new ProdutoGrupo();
        ProdutoSubGrupo subGrupo = new ProdutoSubGrupo();
        GrupoFiscal grupoFiscal = new GrupoFiscal();
        ProdutoSetor setor = new ProdutoSetor();
        private Ean13 ean13 = null;
        CestController cestController = new CestController();
        NcmController ncmController = new NcmController();
        ProdutoController produtoController = new ProdutoController();
        GenericaDesktop generica = new GenericaDesktop();
        OrigemIcms origem = new OrigemIcms();
        OrigemIcmsController origemIcmsController = new OrigemIcmsController();
        EstoqueDAO estoqueDAO = new EstoqueDAO();
        ProdutoGrupoController produtoGrupoController = new ProdutoGrupoController();
        ProdutoSubGrupoController produtoSubGrupoController = new ProdutoSubGrupoController();
        ProdutoSetorController produtoSetorController = new ProdutoSetorController();
        GrupoFiscalController grupoFiscalController = new GrupoFiscalController();
        bool inicio = false;

        public FrmProdutoCadastro()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            iniciarTributosPadroes();
            gerarCombustivel();
            gerarCoresVeiculos();
            gerarDadosVeiculos();
            gerarTiposDeProdutos();
            //aba veiculo nao apresenta se nao marcar o checkbox veiculo
            tabPageAdv3.TabVisible = false;
        }

        public FrmProdutoCadastro(Produto produto, bool inserindoNotaFiscal)
        {
            InitializeComponent();

            gerarCombustivel();
            gerarCoresVeiculos();
            gerarDadosVeiculos();
            gerarTiposDeProdutos();
            //aba veiculo nao apresenta se nao marcar o checkbox veiculo
            tabPageAdv3.TabVisible = false;

            this.produto = produto;
            get_Produto(produto);
            this.FormBorderStyle = FormBorderStyle.None;
            txtDescricao.Focus();
            txtDescricao.Select();

            if (inserindoNotaFiscal == true)
            {
                txtValorVenda.BorderColor = Color.Red;
                txtEstoque.ReadOnly = true;
                txtEstoqueAuxiliar.ReadOnly = true;

            }
            iniciarTributosPadroes();

        }

        public FrmProdutoCadastro(Produto produto, bool inserindoNotaFiscal, bool veiculo)
        {
            InitializeComponent();

            gerarCombustivel();
            gerarCoresVeiculos();
            gerarDadosVeiculos();
            gerarTiposDeProdutos();
            //aba veiculo nao apresenta se nao marcar o checkbox veiculo
            tabPageAdv3.TabVisible = false;

            this.produto = produto;
            get_Produto(produto);
            this.FormBorderStyle = FormBorderStyle.None;
            txtDescricao.Focus();
            txtDescricao.Select();

            if (inserindoNotaFiscal == true)
            {
                txtValorVenda.BorderColor = Color.Red;
                txtEstoque.ReadOnly = true;
                txtEstoqueAuxiliar.ReadOnly = true;
            }
            iniciarTributosPadroes();
            if(veiculo == true)
            {
                tabControlAdv2.SelectedTab = tabPageAdv3;
            }
        }

        private void gerarDadosVeiculos()
        {
            List<string> listaCambio = new List<string>();
            listaCambio.Add("0-NÃO APLICÁVEL");
            listaCambio.Add("1-MANUAL");
            listaCambio.Add("2-AUTOMÁTICO");
            listaCambio.Add("3-AUTOMATIZADO");
            listaCambio.Add("4-CVT");
            comboTipoCambio.DataSource = listaCambio;
            comboTipoCambio.SelectedIndex = -1;

            List<string> listaEspecie = new List<string>();
            listaEspecie.Add("01-PASSAGEIRO");
            listaEspecie.Add("02-CARGA");
            listaEspecie.Add("03-MISTO");
            listaEspecie.Add("04-TRAÇÃO");
            listaEspecie.Add("05-ESPECIAL");
            comboEspecie.DataSource = listaEspecie;
            comboEspecie.SelectedIndex = -1;

            List<string> listaTipoVeiculo = new List<string>();
            listaTipoVeiculo.Add("02-CICLOMOTOR");
            listaTipoVeiculo.Add("03-MOTONETA");
            listaTipoVeiculo.Add("04-MOTOCICLETA");
            listaTipoVeiculo.Add("05-TRICICLO");
            listaTipoVeiculo.Add("06-AUTOMÓVEL");
            listaTipoVeiculo.Add("07-MICRO-ÔNIBUS");
            listaTipoVeiculo.Add("08-ÔNIBUS");
            listaTipoVeiculo.Add("10-REBOQUE");
            listaTipoVeiculo.Add("11-SEMIRREBOQUE");
            listaTipoVeiculo.Add("13-CAMIONETA");
            listaTipoVeiculo.Add("14-CAMINHÃO");
            listaTipoVeiculo.Add("15-CARROÇA");
            listaTipoVeiculo.Add("17-CAMINHÃO TRATOR");
            listaTipoVeiculo.Add("18-TRATOR DE RODAS");
            listaTipoVeiculo.Add("19-TRATOR DE ESTEIRAS");
            listaTipoVeiculo.Add("20-TRATOR MISTO");
            listaTipoVeiculo.Add("21-QUADRICICLO");
            listaTipoVeiculo.Add("22-CHASSI/PLATAFORMA");
            listaTipoVeiculo.Add("23-CAMINHONETE");
            listaTipoVeiculo.Add("25-UTILITÁRIO");
            listaTipoVeiculo.Add("26-MOTOR-CASA");
            comboTipoVeiculo.DataSource = listaTipoVeiculo;
            comboTipoVeiculo.SelectedIndex = -1;

            List<string> listaCondicaoVeiculo = new List<string>();
            listaCondicaoVeiculo.Add("R-REMARCADO");
            listaCondicaoVeiculo.Add("N-NORMAL");
            comboCondicaoVeiculo.DataSource = listaCondicaoVeiculo;
            comboCondicaoVeiculo.SelectedIndex = -1;

            List<string> listaCondicao = new List<string>();
            listaCondicao.Add("1-ACABADO");
            listaCondicao.Add("2-INACABADO");
            listaCondicao.Add("3-SEMI-ACABADO");
            comboCondicaoProduto.DataSource = listaCondicao;
            comboCondicaoProduto.SelectedIndex = -1;

            List<string> listaRestricao = new List<string>();
            listaRestricao.Add("0-NÃO HÁ");
            listaRestricao.Add("1-ALIENACAO FIDUCIARIA");
            listaRestricao.Add("2-ARRENDAMENTO MERCANTIL");
            listaRestricao.Add("3-RESERVA DE DOMINIO");
            listaRestricao.Add("4-PENHOR DE VEICULOS");
            listaRestricao.Add("9-OUTRAS");
            comboRestricaoVeiculo.DataSource = listaRestricao;
            comboRestricaoVeiculo.SelectedIndex = -1;
        }
        private void gerarCombustivel()
        {
            List<string> lista = new List<string>();
            lista.Add("01-ÁLCOOL");
            lista.Add("02-GASOLINA");
            lista.Add("03-DIESEL");
            lista.Add("04-GASOGÊNIO");
            lista.Add("05-GÁS METANO");
            lista.Add("06-ELÉTRICO/FONTE INTERNA");
            lista.Add("07-ELÉTRICO/FONTE EXTERNA");
            lista.Add("08-GASOLINA/GÁS NATURAL COMBUSTÍVEL");
            lista.Add("09-ÁLCOOL/GÁS NATURAL COMBUSTÍVEL");
            lista.Add("10-DIESEL/GÁS NATURAL COMBUSTÍVEL");
            lista.Add("11-VIDE/CAMPO/OBSERVAÇÃO");
            lista.Add("12-ÁLCOOL/GNV");
            lista.Add("13-GASOLINA/GNV");
            lista.Add("14-DIESEL/GNV");
            lista.Add("15-GNV");
            lista.Add("16-ÁLCOOL/GASOLINA");
            lista.Add("17-GASOLINA/ÁLCOOL/GÁS NATURAL");
            lista.Add("18-GASOLINA/ELÉTRICO");
            comboCombustivel.DataSource = lista;
            comboCombustivel.SelectedIndex = -1;
        }
        private void gerarCoresVeiculos()
        {
            List<string> tiposDeProduto = new List<string>();
            tiposDeProduto.Add("01-AMARELO");
            tiposDeProduto.Add("02-AZUL");
            tiposDeProduto.Add("03-BEGE");
            tiposDeProduto.Add("04-BRANCA");
            tiposDeProduto.Add("05-CINZA");
            tiposDeProduto.Add("06-DOURADA");
            tiposDeProduto.Add("07-GRENÁ");
            tiposDeProduto.Add("08-LARANJA");
            tiposDeProduto.Add("09-MARROM");
            tiposDeProduto.Add("10-PRATA");
            tiposDeProduto.Add("11-PRETA");
            tiposDeProduto.Add("12-ROSA");
            tiposDeProduto.Add("13-ROXA");
            tiposDeProduto.Add("14-VERDE");
            tiposDeProduto.Add("15-VERMELHA");
            tiposDeProduto.Add("16-FANTASIA");
            comboCorDenatran.DataSource = tiposDeProduto;
            comboCorDenatran.SelectedIndex = -1;

            comboCorMontadora.DataSource = tiposDeProduto;
            comboCorMontadora.SelectedIndex = -1;
        }
        private void gerarTiposDeProdutos()
        {
            List<string> tiposDeProduto = new List<string>();
            tiposDeProduto.Add("REVENDA");
            tiposDeProduto.Add("USO E CONSUMO");
            tiposDeProduto.Add("MATÉRIA PRIMA");
            tiposDeProduto.Add("ATIVO IMOBILIZADO");
            comboTipoProduto.DataSource = tiposDeProduto;
            comboTipoProduto.SelectedIndex = 0;
            //MessageBox.Show(tiposDeProduto.Count.ToString());
        }

        private void get_Produto(Produto produto)
        {
            txtID.Texts = produto.Id.ToString();
            if (txtID.Texts.Equals("0"))
                txtID.Texts = "";

            txtDescricao.Texts = produto.Descricao;
            txtReferenciaProduto.Texts = produto.Referencia;
            if (produto.UnidadeMedida != null)
            {
                txtCodigoUnidadeMedida.Texts = produto.UnidadeMedida.Id.ToString();
                txtUnidadeMedida.Texts = produto.UnidadeMedida.Descricao;
            }
            if(produto.Marca != null)
            {
                txtCodMarca.Texts = produto.Marca.Id.ToString();
                txtMarca.Texts = produto.Marca.Descricao;
            }
            if (produto.ProdutoGrupo != null)
            {
                txtcodGrupo.Texts = produto.ProdutoGrupo.Id.ToString();
                txtGrupo.Texts = produto.ProdutoGrupo.Descricao;
            }
            if (produto.ProdutoSubGrupo != null)
            {
                txtCodSubGrupo.Texts = produto.ProdutoSubGrupo.Id.ToString();
                txtSubGrupo.Texts = produto.ProdutoSubGrupo.Descricao;
            }
            if (produto.ProdutoSetor != null)
            {
                txtCodSetor.Texts = produto.ProdutoSetor.Id.ToString();
                txtSetor.Texts = produto.ProdutoSetor.Descricao;
            }
            if (produto.GrupoFiscal != null)
            {
                txtCodGrupoFiscal.Texts = produto.GrupoFiscal.Id.ToString();
                txtGrupoFiscal.Texts = produto.GrupoFiscal.Descricao;
            }

            txtCodBarras.Texts = produto.Ean;
            txtNCM.Texts = produto.Ncm;
            txtCEST.Texts = produto.Cest;

            if(!String.IsNullOrEmpty(produto.TipoProduto))
                comboTipoProduto.Text = produto.TipoProduto;
            else
                comboTipoProduto.SelectedIndex = 0;

            txtEstoqueAuxiliar.Texts = produto.EstoqueAuxiliar.ToString();
            txtEstoque.Texts = produto.Estoque.ToString();
            txtValorCusto.Texts = produto.ValorCusto.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
            txtValorVenda.Texts = produto.ValorVenda.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
            txtObs.Texts = produto.Observacoes;

            //Tributacoes
            txtCSOSN.Texts = produto.CstIcms;
            if (Sessao.empresaFilialLogada.RegimeEmpresa != null)
            {
                if (!String.IsNullOrWhiteSpace(produto.CstIcms))
                {
                    if (Sessao.empresaFilialLogada.RegimeEmpresa.Id == 1 || Sessao.empresaFilialLogada.RegimeEmpresa.Id == 4 || Sessao.empresaFilialLogada.RegimeEmpresa.Id == 5)
                    {
                        IList<Csosn> listacsosn = csosnController.selecionarCsosnPorCsosn(produto.CstIcms);
                        if (listacsosn.Count == 1)
                        {
                            foreach (Csosn csosnSel in listacsosn)
                            {
                                txtDescricaoCSOSN.Texts = csosnSel.Descricao;
                            }
                        }
                    }
                }
                else if (Sessao.empresaFilialLogada.RegimeEmpresa.Id == 2 || Sessao.empresaFilialLogada.RegimeEmpresa.Id == 3)
                {
                    IList<CstIcms> listacstIcm = cstIcmsController.selecionarCstIcmsPorCst(produto.CstIcms);
                    if (listacstIcm.Count == 1)
                    {
                        foreach (CstIcms cstSel in listacstIcm)
                        {
                            txtDescricaoCSOSN.Texts = cstSel.Descricao;
                        }
                    }
                }
            }

            txtAliquotaICMS.Texts = produto.PercentualIcms;
            txtCFOP.Texts = produto.CfopVenda;
            txtCodOrigem.Texts = produto.OrigemIcms;
            if (!String.IsNullOrWhiteSpace(produto.OrigemIcms))
            {
                origem = origemIcmsController.selecionarOrigemPorCodigoDeOrigem(txtCodOrigem.Texts);
                txtOrigem.Texts = origem.Descricao;
            }


            txtCodIPI.Texts = produto.CstIpi;
            txtPercentualIPI.Texts = produto.PercentualIpi;
            txtCodEnqIPI.Texts = produto.EnqIpi;
            txtCodSeloIPI.Texts = produto.CodSeloIpi;

            txtCodPIS.Texts = produto.CstPis;
            txtPercentualPIS.Texts = produto.PercentualPis;

            txtCodCofins.Texts = produto.CstCofins;
            txtPercentualCofins.Texts = produto.PercentualCofins;

            txtCodANP.Texts = produto.CodAnp;
            
            if(produto.PercGlp > 0)
                txtPercGLP.Texts = produto.PercGlp.ToString();
            if (produto.PercGnn > 0) 
                txtPercGnn.Texts = produto.PercGnn.ToString();
            if (produto.PercGni > 0) 
                txtPercGni.Texts = produto.PercGni.ToString();
            if (produto.ValorPartida > 0)
                txtValorPartida.Texts = produto.ValorPartida.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");


            //Veiculo
            if (produto.Veiculo == true)
            {
                tabPageAdv3.TabVisible = true;
                chkVeiculo.Checked = true;
            }
            else
            {
                chkVeiculo.Checked = false;
                tabPageAdv3.TabVisible = false;
            }
            comboCorMontadora.Text = produto.CorMontadora;
            comboCorDenatran.Text = produto.CorDenatran;
            txtTipoPintura.Texts = produto.TipoPintura;

            txtPotenciaCv.Texts = produto.PotenciaCv;
            txtCilindradaCc.Texts = produto.CilindradaCc;
            txtNumeroMotor.Texts =  produto.NumeroMotor;
            comboCombustivel.Text = produto.Combustivel;
            comboTipoCambio.Text = produto.TipoCambio;
            comboTipoEntrada.Text = produto.TipoEntrada;
            txtAnoFabricacao.Texts = produto.AnoVeiculo;
            txtAnoModelo.Texts = produto.ModeloVeiculo;
            txtMarcaModelo.Texts = produto.MarcaModelo;
            comboEspecie.Text = produto.EspecieVeiculo;
            txtLotacao.Texts = produto.LotacaoVeiculo;
            comboTipoVeiculo.Text = produto.TipoVeiculo;
            txtPlaca.Texts = produto.Placa;
            txtRenavam.Texts = produto.Renavam;
            txtChassi.Texts = produto.Chassi;
            comboCondicaoVeiculo.Text = produto.CondicaoVeiculo;
            txtDistanciaEixos.Texts = produto.DistanciaEixo;
            txtCapacidadeMaximaTracao.Texts = produto.CapacidadeTracao;
            txtPesoLiquidoVeiculo.Texts = produto.PesoLiquidoVeiculo;
            txtPesoBrutoVeiculo.Texts = produto.PesoBrutoVeiculo;
            comboCondicaoProduto.Text = produto.CondicaoProduto;
            comboRestricaoVeiculo.Text = produto.RestricaoVeiculo;
            txtKmEntrada.Texts = produto.KmEntrada;
            if (produto.VeiculoNovo == true)
                chkVeiculoNovo.Checked = true;
            else
                chkVeiculoNovo.Checked = false;
        }

        private void btnPesquisaUnidadeMedida_Click(object sender, EventArgs e)
        {
            Object unidadeMedidaOjeto = new UnidadeMedida();
            txtUnidadeMedida.Texts = "";
            txtCodigoUnidadeMedida.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("UnidadeMedida", ""))
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
                    switch (uu.showModal("UnidadeMedida", "", ref unidadeMedidaOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmUnidadeMedidaCadastro form = new FrmUnidadeMedidaCadastro();
                            if (form.showModalNovo(ref unidadeMedidaOjeto) == DialogResult.OK)
                            {
                                txtUnidadeMedida.Texts = ((UnidadeMedida)unidadeMedidaOjeto).Descricao;
                                txtCodigoUnidadeMedida.Texts = ((UnidadeMedida)unidadeMedidaOjeto).Id.ToString();
                                unidadeMedida = ((UnidadeMedida)unidadeMedidaOjeto);
                                txtMarca.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtUnidadeMedida.Texts = ((UnidadeMedida)unidadeMedidaOjeto).Descricao;
                            txtCodigoUnidadeMedida.Texts = ((UnidadeMedida)unidadeMedidaOjeto).Id.ToString();
                            unidadeMedida = ((UnidadeMedida)unidadeMedidaOjeto);
                            txtMarca.Focus();
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

        private void btnFechar1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaMarca_Click(object sender, EventArgs e)
        {
            Object marcaObjeto = new Marca();
            txtMarca.Texts = "";
            txtCodMarca.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Marca", ""))
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
                    switch (uu.showModal("Marca", "", ref marcaObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmMarcaCadastro form = new FrmMarcaCadastro();
                            if (form.showModalNovo(ref marcaObjeto) == DialogResult.OK)
                            {
                                txtMarca.Texts = ((Marca)marcaObjeto).Descricao;
                                txtCodMarca.Texts = ((Marca)marcaObjeto).Id.ToString();
                                marca = ((Marca)marcaObjeto);
                                txtGrupo.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtMarca.Texts = ((Marca)marcaObjeto).Descricao;
                            txtCodMarca.Texts = ((Marca)marcaObjeto).Id.ToString();
                            marca = ((Marca)marcaObjeto);
                            txtGrupo.Focus();
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
                                grupo = ((ProdutoGrupo)grupoObjeto);
                                txtSubGrupo.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Descricao;
                            txtcodGrupo.Texts = ((ProdutoGrupo)grupoObjeto).Id.ToString();
                            grupo = ((ProdutoGrupo)grupoObjeto);
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
                grupo = new ProdutoGrupo();
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
                                subGrupo = ((ProdutoSubGrupo)subGrupoObjeto);

                                txtGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Descricao;
                                txtcodGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).ProdutoGrupo.Id.ToString();

                                txtSetor.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Descricao;
                            txtCodSubGrupo.Texts = ((ProdutoSubGrupo)subGrupoObjeto).Id.ToString();
                            subGrupo = ((ProdutoSubGrupo)subGrupoObjeto);

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

        private void btnGerarCodigoBarras_Click(object sender, EventArgs e)
        {
            //System.Drawing.Graphics g = this.picBarcode.CreateGraphics();

            //g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.SystemColors.Control),
            //    new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));

            CreateEan13();
            ean13.Scale = 1;
            //ean13.DrawEan13Barcode(g, new System.Drawing.Point(0, 0));

            //Verificar se codigo de barras ja existe
            IList<Produto> listaProd = produtoController.selecionarProdutosComVariosFiltros(ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit, Sessao.empresaFilialLogada);
            if(listaProd.Count == 0)
                txtCodBarras.Texts = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
            else if (listaProd.Count > 0)
            {
                //Se o primeiro codigo ja existe o sistema gera outro 
                CreateEan13();
                ean13.Scale = 1;
                IList<Produto> listaProd2 = produtoController.selecionarProdutosComVariosFiltros(ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit, Sessao.empresaFilialLogada);
                if (listaProd2.Count == 0)
                    txtCodBarras.Texts = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                else
                    GenericaDesktop.ShowAlerta("Codigo de barras já está em uso, tente gerar outro!");
            }

            //g.Dispose();
        }

        private void CreateEan13()
        {
            ean13 = new Ean13();
            ean13.CountryCode = RandomNumber(10, 78).ToString();
            ean13.ManufacturerCode = RandomNumber(79000, 99000).ToString();
            ean13.ProductCode = RandomNumber(10000, 99000).ToString();
            ean13.ChecksumDigit = RandomNumber(1, 9).ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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
                                setor = ((ProdutoSetor)setorObjeto);
                                txtCodBarras.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtSetor.Texts = ((ProdutoSetor)setorObjeto).Descricao;
                            txtCodSetor.Texts = ((ProdutoSetor)setorObjeto).Id.ToString();
                            setor = ((ProdutoSetor)setorObjeto);
                            txtCodBarras.Focus();
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

        private void btnPesquisaGrupoFiscal_Click(object sender, EventArgs e)
        {
            Object grupoFiscalObjeto = new GrupoFiscal();
            txtSetor.Texts = "";
            txtCodSetor.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("GrupoFiscal", ""))
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
                    switch (uu.showModal("GrupoFiscal", "", ref grupoFiscalObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmGrupoFiscalCadastro form = new FrmGrupoFiscalCadastro();
                            if (form.showModalNovo(ref grupoFiscalObjeto) == DialogResult.OK)
                            {
                                get_GrupoFiscal((GrupoFiscal)grupoFiscalObjeto);
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            get_GrupoFiscal((GrupoFiscal)grupoFiscalObjeto);
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

        private void get_GrupoFiscal(GrupoFiscal grupoFiscal)
        {
            txtGrupoFiscal.Texts = grupoFiscal.Descricao;
            txtCodGrupoFiscal.Texts = grupoFiscal.Id.ToString();
            txtCSOSN.Texts = grupoFiscal.CsosnSaida;
            txtCFOP.Texts = grupoFiscal.CfopSaidaEstadual;
            txtAliquotaICMS.Texts = grupoFiscal.AliquotaIcms;
            txtCodOrigem.Texts = "0";
            origem = origemIcmsController.selecionarOrigemPorCodigoDeOrigem("0");
            txtOrigem.Texts = origem.Descricao;
            this.grupoFiscal = grupoFiscal;
            txtNCM.Focus();
        }

        private void btnPesquisaNCM_Click(object sender, EventArgs e)
        {
            Object ncmObjeto = new Ncm();
            txtNCM.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Ncm", ""))
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
                    switch (uu.showModal("Ncm", "", ref ncmObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmNCMCadastro form = new FrmNCMCadastro();
                            if (form.showModalNovo(ref ncmObjeto) == DialogResult.OK)
                            {
                                txtNCM.Texts = ((Ncm)ncmObjeto).NumeroNcm;
                                txtCEST.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtNCM.Texts = ((Ncm)ncmObjeto).NumeroNcm;
                            txtCEST.Focus();
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

        private void btnPesquisaCEST_Click(object sender, EventArgs e)
        {
            Object cestObjeto = new Cest();
            txtCEST.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Cest", ""))
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
                    switch (uu.showModal("Cest", "", ref cestObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmCESTCadastro form = new FrmCESTCadastro();
                            if (form.showModalNovo(ref cestObjeto) == DialogResult.OK)
                            {
                                txtCEST.Texts = ((Cest)cestObjeto).NumeroCest;
                                txtEstoque.Focus();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCEST.Texts = ((Cest)cestObjeto).NumeroCest;
                            txtEstoque.Focus();
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

        private void verificarNCM()
        {
            if (txtNCM.Texts.Trim().Length == 8)
            {
                IList<Ncm> listaNCM = ncmController.selecionarNCMPorNCM(txtNCM.Texts.Trim());
                if (listaNCM.Count > 0)
                {
                    IList<Cest> listaCest = cestController.selecionarCestPorNCM(txtNCM.Texts.Trim());
                    if (listaCest.Count == 1)
                    {
                        foreach (Cest cestSelecionado in listaCest)
                        {
                            txtCEST.Texts = cestSelecionado.NumeroCest;
                        }
                    }
                    else if (listaCest.Count > 1)
                    {
                        Object cestObjeto = new Cest();
                        txtCEST.Texts = "";
                        Form formBackground = new Form();
                        try
                        {
                            using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Cest", "and Tabela.NCM like '%" + txtNCM.Texts + "%'"))
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
                                switch (uu.showModal("Cest", "", ref cestObjeto))
                                {
                                    case DialogResult.Ignore:
                                        uu.Dispose();
                                        FrmCESTCadastro form = new FrmCESTCadastro();
                                        if (form.showModalNovo(ref cestObjeto) == DialogResult.OK)
                                        {
                                            txtCEST.Texts = ((Cest)cestObjeto).NumeroCest;
                                            txtEstoque.Focus();
                                        }
                                        form.Dispose();
                                        break;
                                    case DialogResult.OK:
                                        txtCEST.Texts = ((Cest)cestObjeto).NumeroCest;
                                        txtEstoque.Focus();
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
                else
                {
                    if (GenericaDesktop.ShowConfirmacao("NCM não cadastrado, deseja cadastrar?"))
                    {
                        Object ncmObjeto = new Ncm();
                        Ncm nc = new Ncm();
                        nc.NumeroNcm = txtNCM.Texts;
                        FrmNCMCadastro form = new FrmNCMCadastro(nc);
                        if (form.showModalNovo(ref ncmObjeto) == DialogResult.OK)
                        {
                            txtNCM.Texts = ((Ncm)ncmObjeto).NumeroNcm;
                            txtCEST.Focus();
                        }
                    }
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("NCM deve conter 8 dígitos e não deve ser colocado ponto ou caracteres especiais!");
                txtNCM.Texts = "";
                
            }
        }

        private void txtNCM_Leave(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtNCM.Texts) && txtNCM.Texts.Length == 8)
            {
                verificarNCM();
            }
        }

        private void txtNCM_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCEST_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmProdutoCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;

                case Keys.F1:
                    this.TopMost = false;
                    generica.abrirNavegador();
                    break;
            }
        }

        private void FrmProdutoCadastro_Paint(object sender, PaintEventArgs e)
        {
            if (inicio == false)
            {
                inicio = true;
                this.TopMost = false;
                txtDescricao.Focus();
                if (String.IsNullOrEmpty(txtCodOrigem.Texts))
                    txtCodOrigem.Texts = "0";
                atualizaDescricaoCsts();
            }
        }

        private void atualizaDescricaoCsts()
        {
            CstPisCofinsController cstPisCofinsController = new CstPisCofinsController();
            CstIpiController cstIpiController = new CstIpiController();
            

            //IPI
            if (!String.IsNullOrEmpty(txtCodIPI.Texts))
            {
                IList<CstIpi> listaIpi = cstIpiController.selecionarCstPorCst(txtCodIPI.Texts);
                foreach (CstIpi ipi in listaIpi)
                {
                    txtDescricaoIPI.Texts = ipi.Descricao;
                }
            }

            //PIS
            if (!String.IsNullOrEmpty(txtCodPIS.Texts))
            {
                IList<CstPisCofins> listapis = cstPisCofinsController.selecionarCstPorCst(txtCodPIS.Texts);
                foreach (CstPisCofins pis in listapis)
                {
                    txtDescricaoPIS.Texts = pis.Descricao;
                }
            }

            //Cofins
            if (!String.IsNullOrEmpty(txtCodCofins.Texts))
            {
                IList<CstPisCofins> listaCofins = cstPisCofinsController.selecionarCstPorCst(txtCodCofins.Texts);
                foreach (CstPisCofins cofins in listaCofins)
                {
                    txtDescricaoCofins.Texts = cofins.Descricao;
                }
            }

            //Origem
            if (!String.IsNullOrEmpty(txtCodOrigem.Texts))
            {
                origem = new OrigemIcms();
                origem = origemIcmsController.selecionarOrigemPorCodigoDeOrigem(txtCodOrigem.Texts);
                if (origem != null)
                    txtOrigem.Texts = origem.Descricao;
            }
        }

        private void comboTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { txtEstoque.Focus(); } catch { }    
        }

        private void set_Produto()
        {
            try
            {
                double quantidadeAjusteConciliado = 0;
                double quantidadeAjusteNaoConciliado = 0;

                produto = new Produto();
                if (!String.IsNullOrEmpty(txtID.Texts))
                {
                    produto.Id = int.Parse(txtID.Texts);
                    produto = (Produto)produtoController.selecionar(produto);
                }
                else
                    produto.Id = 0;
                produto.Descricao = txtDescricao.Texts.Trim();
                produto.Referencia = txtReferenciaProduto.Texts;

                //Unidade Medida
                if (!String.IsNullOrEmpty(txtCodigoUnidadeMedida.Texts))
                {
                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = int.Parse(txtCodigoUnidadeMedida.Texts);
                    unidadeMedida = (UnidadeMedida)Controller.getInstance().selecionar(unidadeMedida);
                    produto.UnidadeMedida = unidadeMedida;
                }
                else
                    produto.UnidadeMedida = null;

                //Marca
                if (!String.IsNullOrEmpty(txtCodMarca.Texts))
                {
                    marca = new Marca();
                    marca.Id = int.Parse(txtCodMarca.Texts);
                    marca = (Marca)Controller.getInstance().selecionar(marca);
                    produto.Marca = marca;
                }
                else
                    produto.Marca = null;

                //Grupo de Produto
                if (!String.IsNullOrEmpty(txtcodGrupo.Texts))
                {
                    grupo = new ProdutoGrupo();
                    grupo.Id = int.Parse(txtcodGrupo.Texts);
                    grupo = (ProdutoGrupo)Controller.getInstance().selecionar(grupo);
                    produto.ProdutoGrupo = grupo;
                }
                else
                    produto.ProdutoGrupo = null;

                //SubGrupo de Produto
                if (!String.IsNullOrEmpty(txtCodSubGrupo.Texts))
                {
                    subGrupo = new ProdutoSubGrupo();
                    subGrupo.Id = int.Parse(txtCodSubGrupo.Texts);
                    subGrupo = (ProdutoSubGrupo)Controller.getInstance().selecionar(subGrupo);
                    produto.ProdutoSubGrupo = subGrupo;
                }
                else
                    produto.ProdutoSubGrupo = null;

                //Setor Localizacao
                if (!String.IsNullOrEmpty(txtCodSetor.Texts))
                {
                    setor = new ProdutoSetor();
                    setor.Id = int.Parse(txtCodSetor.Texts);
                    setor = (ProdutoSetor)Controller.getInstance().selecionar(setor);
                    produto.ProdutoSetor = setor;
                }
                else
                    produto.ProdutoSetor = null;

                //Grupo Fiscal
                if (!String.IsNullOrEmpty(txtCodGrupoFiscal.Texts))
                {
                    grupoFiscal = new GrupoFiscal();
                    grupoFiscal.Id = int.Parse(txtCodGrupoFiscal.Texts);
                    grupoFiscal = (GrupoFiscal)Controller.getInstance().selecionar(grupoFiscal);
                    produto.GrupoFiscal = grupoFiscal;
                }
                else
                    produto.GrupoFiscal = null;

                produto.Ean = txtCodBarras.Texts.Trim();
                produto.Ncm = txtNCM.Texts;
                produto.Cest = txtCEST.Texts;
                produto.TipoProduto = comboTipoProduto.Text;
                produto.IdComplementar = "";
                //Custo
                if (!String.IsNullOrEmpty(txtValorCusto.Texts))
                {
                    if (decimal.Parse(txtValorCusto.Texts) > 0)
                        produto.ValorCusto = decimal.Parse(txtValorCusto.Texts);
                }
                //Venda
                if (!String.IsNullOrEmpty(txtValorVenda.Texts))
                {
                    if (decimal.Parse(txtValorVenda.Texts) > 0)
                        produto.ValorVenda = decimal.Parse(txtValorVenda.Texts);
                }

                produto.Observacoes = txtObservacoes.Texts;
                if (chkControlaEstoque.Checked == true)
                    produto.ControlaEstoque = true;
                if (chkVenderLojaVirtual.Checked == true)
                    produto.Ecommerce = true;
                if (chkProdutoComGrade.Checked == true)
                    produto.Grade = true;
                if (chkProdutoPesavel.Checked == true)
                    produto.Pesavel = true;

                produto.CstIcms = txtCSOSN.Texts;
                produto.PercentualIcms = txtAliquotaICMS.Texts;
                produto.CfopVenda = txtCFOP.Texts;
                produto.OrigemIcms = txtCodOrigem.Texts;

                produto.CstIpi = txtCodIPI.Texts;
                produto.PercentualIpi = txtPercentualIPI.Texts;
                produto.EnqIpi = txtCodEnqIPI.Texts;
                produto.CodSeloIpi = txtCodSeloIPI.Texts;

                produto.CstPis = txtCodPIS.Texts;
                produto.PercentualPis = txtPercentualPIS.Texts;
                produto.CstCofins = txtCodCofins.Texts;
                produto.PercentualCofins = txtPercentualCofins.Texts;
                produto.CodAnp = txtCodANP.Texts;
                if (!String.IsNullOrEmpty(txtCodANP.Texts) && txtCodANP.Texts.Length > 3)
                {
                    if(!String.IsNullOrEmpty(txtPercGLP.Texts))
                        produto.PercGlp = double.Parse(txtPercGLP.Texts);
                    if (!String.IsNullOrEmpty(txtPercGnn.Texts)) 
                        produto.PercGnn = double.Parse(txtPercGnn.Texts);
                    if (!String.IsNullOrEmpty(txtPercGni.Texts)) 
                        produto.PercGni = double.Parse(txtPercGni.Texts);
                    if (!String.IsNullOrEmpty(txtValorPartida.Texts)) 
                        produto.ValorPartida = decimal.Parse(txtValorPartida.Texts);
                }
                else
                {
                    produto.PercGlp = 0;
                    produto.PercGnn = 0;
                    produto.PercGni = 0;
                    produto.ValorPartida = 0;
                }

                produto.Empresa = Sessao.empresaFilialLogada.Empresa;
                produto.EmpresaFilial = Sessao.empresaFilialLogada;

                //Veiculo
                if (chkVeiculo.Checked == true)
                    produto.Veiculo = true;
                else
                    produto.Veiculo = false;
                produto.CorMontadora = comboCorMontadora.Text;
                produto.CorDenatran = comboCorDenatran.Text;
                produto.TipoPintura = txtTipoPintura.Texts;
                produto.PotenciaCv = txtPotenciaCv.Texts;
                produto.CilindradaCc = txtCilindradaCc.Texts;
                produto.NumeroMotor = txtNumeroMotor.Texts;
                produto.Combustivel = comboCombustivel.Text;
                produto.TipoCambio = comboTipoCambio.Text;
                produto.TipoEntrada = comboTipoEntrada.Text;
                produto.AnoVeiculo = txtAnoFabricacao.Texts;
                produto.ModeloVeiculo = txtAnoModelo.Texts;
                produto.MarcaModelo = txtMarcaModelo.Texts;
                produto.EspecieVeiculo = comboEspecie.Text;
                produto.LotacaoVeiculo = txtLotacao.Texts;
                produto.TipoVeiculo = comboTipoVeiculo.Text;
                produto.Placa = txtPlaca.Texts;
                produto.Renavam = txtRenavam.Texts;
                produto.Chassi = txtChassi.Texts;
                produto.CondicaoVeiculo = comboCondicaoVeiculo.Text;
                produto.DistanciaEixo = txtDistanciaEixos.Texts;
                produto.CapacidadeTracao = txtCapacidadeMaximaTracao.Texts;
                produto.PesoLiquidoVeiculo = txtPesoLiquidoVeiculo.Texts;
                produto.PesoBrutoVeiculo = txtPesoBrutoVeiculo.Texts;
                produto.CondicaoProduto = comboCondicaoProduto.Text;
                produto.RestricaoVeiculo = comboRestricaoVeiculo.Text;
                produto.KmEntrada = txtKmEntrada.Texts;
                if(chkVeiculoNovo.Checked == true)
                    produto.VeiculoNovo = true;
                else
                    produto.VeiculoNovo = false;

                //SALVA NA TABELA ESTOQUE O NOVO SALDO PRODUTO CASO O PRODUTO SEJA UM NOVO CADASTRO
                if (String.IsNullOrEmpty(txtID.Texts))
                {
                    //Se produto novo e tem estoque conciliado ja insere
                    if (!String.IsNullOrEmpty(txtEstoque.Texts))
                    {
                        if (double.Parse(txtEstoque.Texts) > 0)
                        {
                            //Ajusta estoque conciliado para novo produto
                            quantidadeAjusteConciliado = 0;
                            double estoque = estoqueDAO.calcularEstoqueConciliadoPorProduto(produto.Id, Sessao.empresaFilialLogada);
                            quantidadeAjusteConciliado = double.Parse(txtEstoque.Texts) - (estoque);
                        }
                    }
                    if (!String.IsNullOrEmpty(txtEstoqueAuxiliar.Texts))
                    {
                        //Se produto novo e tem estoque auxiliar ja insere
                        if (double.Parse(txtEstoqueAuxiliar.Texts) > 0)
                        {
                            //Ajusta estoque conciliado para novo produto
                            quantidadeAjusteNaoConciliado = 0;
                            double estoque = estoqueDAO.calcularEstoqueNaoConciliadoPorProduto(produto.Id, Sessao.empresaFilialLogada);
                            quantidadeAjusteNaoConciliado = double.Parse(txtEstoqueAuxiliar.Texts) - (estoque);
                        }
                    }
                }
                //CASO ESTEJA EDITANDO O PRODUTO
                else
                {
                    if (produto.Estoque != double.Parse(txtEstoque.Texts))
                    {
                        if (GenericaDesktop.ShowConfirmacao("Atenção: Você está alterando o estoque contábil que deve ser movimentado através de entrada " +
                            "de notas fiscais ou venda com notas fiscais, tenha certeza do que está fazendo para evitar problemas com o fisco, " +
                            "caso tenha dúvida consulte a sua contabilidade sobre ajuste de estoque fiscal. Caso permaneça com a alteração o sistema " +
                            "irá gravar um ajuste fiscal com a data de hoje, deseja continuar?"))
                        {
                            //Ajusta estoque conciliado com autorizacao do cliente!
                            quantidadeAjusteConciliado = 0;
                            double estoque = estoqueDAO.calcularEstoqueConciliadoPorProduto(produto.Id, Sessao.empresaFilialLogada);
                            quantidadeAjusteConciliado = double.Parse(txtEstoque.Texts) - (estoque);
                        }
                    }

                    if (produto.EstoqueAuxiliar != double.Parse(txtEstoqueAuxiliar.Texts))
                    {
                        if (GenericaDesktop.ShowConfirmacao("Tem certeza que deseja atualizar o estoque auxiliar deste produto?"))
                        {
                            //Ajusta estoque conciliado com autorizacao do cliente!
                            quantidadeAjusteNaoConciliado = 0;
                            double estoque = estoqueDAO.calcularEstoqueNaoConciliadoPorProduto(produto.Id, Sessao.empresaFilialLogada);
                            quantidadeAjusteNaoConciliado = double.Parse(txtEstoqueAuxiliar.Texts) - (estoque);
                        }
                    } 
                }

                //Define se é uma entrada ou saida de estoque
                bool tipoAjusteEntradaOuSaida = true;
                if (quantidadeAjusteConciliado < 0)
                    tipoAjusteEntradaOuSaida = false;
                //Define se é uma entrada ou saida de estoque
                bool tipoAjusteEntradaOuSaida2 = true;
                if (quantidadeAjusteNaoConciliado < 0)
                    tipoAjusteEntradaOuSaida2 = false;
                
                Controller.getInstance().salvar(produto);
                //Ajusta o estoque
                if (quantidadeAjusteConciliado > 0 || quantidadeAjusteConciliado < 0)
                    generica.atualizarEstoqueConciliado(produto, double.Parse(GenericaDesktop.RemoveCaracteres(quantidadeAjusteConciliado.ToString())), tipoAjusteEntradaOuSaida, "PRODUTO", "AJUSTE DE ESTOQUE NO CADASTRO, USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, DateTime.Now, null);
                if (quantidadeAjusteNaoConciliado > 0 || quantidadeAjusteNaoConciliado < 0)
                    generica.atualizarEstoqueNaoConciliado(produto, double.Parse(GenericaDesktop.RemoveCaracteres(quantidadeAjusteNaoConciliado.ToString())), tipoAjusteEntradaOuSaida2, "PRODUTO", "AJUSTE DE ESTOQUE NO CADASTRO, USUARIO: " + Sessao.usuarioLogado.Id + " - " + Sessao.usuarioLogado.Login, null, DateTime.Now, null);

                GenericaDesktop.ShowInfo("Registrado com sucesso!");
                get_Produto(produto);
            }
            catch (Exception ex) 
            {
                GenericaDesktop.ShowErro("Erro ao gravar produto \n" + ex.Message);
            }
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            set_Produto();
            if (showModal)
            {
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void txtCSOSN_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCSOSN.Texts))
            {
                CsosnController csosnController = new CsosnController();
                IList<Csosn> listaCSOSN = csosnController.selecionarCsosnPorCsosn(txtCSOSN.Texts.Trim());
                if(listaCSOSN.Count == 1)
                {
                    foreach(Csosn csosnSel in listaCSOSN)
                    {
                        txtDescricaoCSOSN.Texts = csosnSel.Descricao;
                    }
                }
                else if (listaCSOSN.Count == 0)
                {
                    CstIcmsController cstIcmsController = new CstIcmsController();
                    IList<CstIcms> listaCstICMS = cstIcmsController.selecionarCstIcmsPorCst(txtCSOSN.Texts.Trim());
                    if (listaCstICMS.Count == 1)
                    {
                        foreach (Csosn csosnSel in listaCSOSN)
                        {
                            txtDescricaoCSOSN.Texts = csosnSel.Descricao;
                        }
                    }
                    else
                    {
                        GenericaDesktop.ShowAlerta("CST/CSOSN Inválido, caso não tenha o número correto consulte a sua contabilidade qual deve ser o CSOSN de venda deste produto!");
                        txtDescricaoCSOSN.Texts = "";
                    }
                }
            }
        }

        private void btnPesquisaCSOSN_Click(object sender, EventArgs e)
        {
            if (Sessao.empresaFilialLogada.RegimeEmpresa != null)
            {
                if (Sessao.empresaFilialLogada.RegimeEmpresa.Id == 1 || Sessao.empresaFilialLogada.RegimeEmpresa.Id == 4 || Sessao.empresaFilialLogada.RegimeEmpresa.Id == 5)
                {
                    Object csosnOjeto = new Csosn();
                    txtCSOSN.Texts = "";
                    txtDescricaoCSOSN.Texts = "";
                    Form formBackground = new Form();
                    try
                    {
                        using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Csosn", ""))
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
                            switch (uu.showModal("Csosn", "", ref csosnOjeto))
                            {
                                case DialogResult.Ignore:
                                    uu.Dispose();
                                    break;
                                case DialogResult.OK:
                                    txtCSOSN.Texts = ((Csosn)csosnOjeto).Codigo;
                                    txtDescricaoCSOSN.Texts = ((Csosn)csosnOjeto).Descricao;
                                    txtCFOP.Focus();
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

                //Se empresa não é simples nacional pesquisa cst
                else
                {

                }
            }
        }

        private void txtOrigem_Click(object sender, EventArgs e)
        {
            pesquisarOrigem();
        }

        private void pesquisarOrigem()
        {
            Object origemOjeto = new OrigemIcms();
            txtOrigem.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("OrigemIcms", ""))
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
                    switch (uu.showModal("OrigemIcms", "", ref origemOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtOrigem.Texts = ((OrigemIcms)origemOjeto).Descricao;
                            txtCodOrigem.Texts = ((OrigemIcms)origemOjeto).CodOrigem;
                            txtCodIPI.Focus();
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

        private void btnPesquisarOrigem_Click(object sender, EventArgs e)
        {
            pesquisarOrigem();
        }

        private void btnPesquisaIPI_Click(object sender, EventArgs e)
        {
            Object ipiOjeto = new CstIpi();
            txtOrigem.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("CstIpi", ""))
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
                    switch (uu.showModal("CstIpi", "", ref ipiOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtDescricaoIPI.Texts = ((CstIpi)ipiOjeto).Descricao;
                            txtCodIPI.Texts = ((CstIpi)ipiOjeto).Cst;
                            txtPercentualIPI.Focus();
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

        private void btnPesquisaPis_Click(object sender, EventArgs e)
        {
            Object cstPisCofinsOjeto = new CstPisCofins();
            txtOrigem.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("CstPisCofins", ""))
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
                    switch (uu.showModal("CstPisCofins", "", ref cstPisCofinsOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtDescricaoPIS.Texts = ((CstPisCofins)cstPisCofinsOjeto).Descricao;
                            txtCodPIS.Texts = ((CstPisCofins)cstPisCofinsOjeto).Cst;
                            txtPercentualPIS.Focus();
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

        private void btnPesquisaCofins_Click(object sender, EventArgs e)
        {
            Object cstPisCofinsOjeto = new CstPisCofins();
            txtOrigem.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("CstPisCofins", ""))
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
                    switch (uu.showModal("CstPisCofins", "", ref cstPisCofinsOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtDescricaoCofins.Texts = ((CstPisCofins)cstPisCofinsOjeto).Descricao;
                            txtCodCofins.Texts = ((CstPisCofins)cstPisCofinsOjeto).Cst;
                            txtPercentualCofins.Focus();
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

        private void txtCodANP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodANP.Texts))
                {
                    AnpController anpController = new AnpController();
                    IList<Anp> listaANP = anpController.selecionarCodigoAnpPorCodigo(txtCodANP.Texts);
                    if (listaANP.Count > 0)
                    {
                        foreach (Anp anp in listaANP)
                        {
                            txtDescricaoANP.Texts = anp.Descricao;
                        }
                    }
                    else
                    {
                        txtDescricaoANP.Texts = "";
                        GenericaDesktop.ShowAlerta("Código ANP não encontrado!");
                    }
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        private void btnPesquisaCFOP_Click(object sender, EventArgs e)
        {
            Object cfopOjeto = new Cfop();
            txtOrigem.Texts = "";
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Cfop", "and (Tabela.CfopNumero Like '5%' or Tabela.CfopNumero Like '6%')"))
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
                    switch (uu.showModal("Cfop", "", ref cfopOjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCFOP.Texts = ((Cfop)cfopOjeto).CfopNumero;
                            txtCodOrigem.Focus();
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

        private void iniciarTributosPadroes()
        {
            if(String.IsNullOrEmpty(txtCodIPI.Texts))
                txtCodIPI.Texts = "99";
            txtCodIPI.TextAlign = HorizontalAlignment.Center;

            if (String.IsNullOrEmpty(txtCodEnqIPI.Texts))
                txtCodEnqIPI.Texts = "999";
            txtCodEnqIPI.TextAlign = HorizontalAlignment.Center;

            if (String.IsNullOrEmpty(txtCodPIS.Texts))
                txtCodPIS.Texts = "99";
            txtCodPIS.TextAlign = HorizontalAlignment.Center;

            if (String.IsNullOrEmpty(txtCodCofins.Texts))
                txtCodCofins.Texts = "99";
            txtCodCofins.TextAlign = HorizontalAlignment.Center;

            //Somente ate inserir a origem vindo da nf de compra
            txtCodOrigem.Texts = "0";
            txtCodOrigem.TextAlign = HorizontalAlignment.Center;
            txtPercentualCofins.TextAlign = HorizontalAlignment.Center;  
            txtPercentualIPI.TextAlign = HorizontalAlignment.Center;
            txtAliquotaICMS.TextAlign = HorizontalAlignment.Center;
            txtCSOSN.TextAlign = HorizontalAlignment.Center;
            txtCFOP.TextAlign = HorizontalAlignment.Center;

            if (!String.IsNullOrWhiteSpace(produto.OrigemIcms))
            {
                origem = origemIcmsController.selecionarOrigemPorCodigoDeOrigem(txtCodOrigem.Texts);
                txtOrigem.Texts = origem.Descricao;
            }
        }
        private void txtValorVenda_Leave(object sender, EventArgs e)
        {
            txtValorVenda.BorderColor = Color.FromArgb(229, 229, 229);
        }

        private void txtCFOP_Leave(object sender, EventArgs e)
        {
            CfopController cfopController = new CfopController();
            IList<Cfop> listaCFOP = cfopController.selecionarCfopPorCfop(txtCFOP.Texts.Trim());
            if(listaCFOP.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CFOP não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CFOP correto para venda deste produto!");
            }
        }

        private void txtCodIPI_Leave(object sender, EventArgs e)
        {
            CstIpiController cstIpiController = new CstIpiController();
            IList<CstIpi> listaIPI = cstIpiController.selecionarCstPorCst(txtCodIPI.Texts.Trim());
            if (listaIPI.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CST de IPI não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CST IPI correto para venda deste produto!");
            }
            else if (listaIPI.Count == 1)
            {
                foreach(CstIpi ipi in listaIPI)
                {
                    txtDescricaoIPI.Texts = ipi.Descricao;
                }
            }
        }

        private void txtCodPIS_Leave(object sender, EventArgs e)
        {
            CstPisCofinsController cstPisCofinsController = new CstPisCofinsController();
            IList<CstPisCofins> listaPis = cstPisCofinsController.selecionarCstPorCst(txtCodPIS.Texts.Trim());
            if (listaPis.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CST de PIS não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CST PIS correto para venda deste produto!");
            }
            else if (listaPis.Count == 1)
            {
                foreach (CstPisCofins pis in listaPis)
                {
                    txtDescricaoPIS.Texts = pis.Descricao;
                }
            }
        }

        private void txtCodCofins_Leave(object sender, EventArgs e)
        {
            CstPisCofinsController cstPisCofinsController = new CstPisCofinsController();
            IList<CstPisCofins> listaCofins = cstPisCofinsController.selecionarCstPorCst(txtCodPIS.Texts.Trim());
            if (listaCofins.Count == 0)
            {
                GenericaDesktop.ShowAlerta("Este CST de COFINS não está cadastrado no sistema, caso tenha certeza que está correto mantenha o mesmo, caso tenha dúvida consulte a sua contabilidade qual o CST COFINS correto para venda deste produto!");
            }
            else if (listaCofins.Count == 1)
            {
                foreach (CstPisCofins cofins in listaCofins)
                {
                    txtDescricaoCofins.Texts = cofins.Descricao;
                }
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtUnidadeMedida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                unidadeMedida = new UnidadeMedida();
                if (!String.IsNullOrEmpty(txtUnidadeMedida.Texts))
                {
                    if (generica.eNumero(txtUnidadeMedida.Texts))
                    {
                        unidadeMedida.Id = int.Parse(txtUnidadeMedida.Texts);
                        try
                        {
                            unidadeMedida = (UnidadeMedida)Controller.getInstance().selecionar(unidadeMedida);
                            txtUnidadeMedida.Texts = unidadeMedida.Descricao;
                            txtCodigoUnidadeMedida.Texts = unidadeMedida.Id.ToString();
                            txtMarca.Focus();
                        }
                        catch
                        {
                            unidadeMedida = null;
                            txtCodigoUnidadeMedida.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhuma unidade de medida encontrada");
                            txtUnidadeMedida.SelectAll();
                        }
                    }
                    if (unidadeMedida == null || generica.eNumero(txtUnidadeMedida.Texts) == false)
                    {
                        IList<UnidadeMedida> listaUnidadeMedida = unidadeMedidaController.selecionarUnidadeMedidaComVariosFiltros(txtUnidadeMedida.Texts, Sessao.empresaFilialLogada);
                        if (listaUnidadeMedida.Count == 1)
                        {
                            foreach (UnidadeMedida uni in listaUnidadeMedida)
                            {
                                unidadeMedida = uni;
                                txtUnidadeMedida.Texts = uni.Descricao;
                                txtCodigoUnidadeMedida.Texts = uni.Id.ToString();
                                txtMarca.Focus();
                            }
                        }
                        else if (listaUnidadeMedida.Count > 1)
                        {
                            btnPesquisaUnidadeMedida.PerformClick();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nenhuma unidade de medida encontrada");
                    }
                }
                else
                {
                    btnPesquisaUnidadeMedida.PerformClick();
                }
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                marca = new Marca();
                if (!String.IsNullOrEmpty(txtMarca.Texts))
                {
                    if (generica.eNumero(txtMarca.Texts))
                    {
                        marca.Id = int.Parse(txtMarca.Texts);
                        try
                        {
                            marca = (Marca)Controller.getInstance().selecionar(marca);
                            txtMarca.Texts = marca.Descricao;
                            txtCodMarca.Texts = marca.Id.ToString();
                            txtGrupo.Focus();
                        }
                        catch
                        {
                            marca = null;
                            txtCodMarca.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhuma marca encontrada");
                            txtMarca.SelectAll();
                        }
                    }
                    else if (marca == null || generica.eNumero(txtMarca.Texts) == false)
                    {
                        IList<Marca> lista = marcaController.selecionarMarcaComVariosFiltros(txtMarca.Texts);
                        if (lista.Count == 1)
                        {
                            foreach (Marca mar in lista)
                            {
                                marca = mar;
                                txtMarca.Texts = mar.Descricao;
                                txtCodMarca.Texts = mar.Id.ToString();
                                txtGrupo.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaMarca.PerformClick();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nenhuma marca encontrada");
                    }
                }
                else
                {
                    btnPesquisaMarca.PerformClick();
                }
            }
        }

        private void txtGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                grupo = new ProdutoGrupo();
                if (!String.IsNullOrEmpty(txtGrupo.Texts))
                {
                    if (generica.eNumero(txtGrupo.Texts))
                    {
                        grupo.Id = int.Parse(txtGrupo.Texts);
                        try
                        {
                            grupo = (ProdutoGrupo)Controller.getInstance().selecionar(grupo);
                            txtGrupo.Texts = grupo.Descricao;
                            txtcodGrupo.Texts = grupo.Id.ToString();
                            txtSubGrupo.Focus();
                            if (!String.IsNullOrEmpty(txtCodSubGrupo.Texts))
                            {
                                subGrupo = new ProdutoSubGrupo();
                                subGrupo.Id = int.Parse(txtCodSubGrupo.Texts);
                                subGrupo = (ProdutoSubGrupo)Controller.getInstance().selecionar(subGrupo);
                                if (subGrupo.ProdutoGrupo.Id != grupo.Id)
                                {
                                    txtSubGrupo.Texts = "";
                                    txtCodSubGrupo.Texts = "";
                                }
                            }
                        }
                        catch
                        {
                            grupo = null;
                            txtcodGrupo.Texts = "";
                            txtCodSubGrupo.Texts = "";
                            txtSubGrupo.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhum grupo encontrado");
                            txtGrupo.SelectAll();
                        }
                    }
                    else if (grupo == null || generica.eNumero(txtGrupo.Texts) == false)
                    {
                        IList<ProdutoGrupo> lista = produtoGrupoController.selecionarGrupoComVariosFiltros(txtGrupo.Texts);
                        if (lista.Count == 1)
                        {
                            foreach (ProdutoGrupo grup in lista)
                            {
                                grupo = grup;
                                txtGrupo.Texts = grup.Descricao;
                                txtcodGrupo.Texts = grup.Id.ToString();
                                txtSubGrupo.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaGrupo.PerformClick();
                        }
                        else
                        {
                            txtCodSubGrupo.Texts = "";
                            txtSubGrupo.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhum grupo encontrado");
                        }
                    }
                }
                else
                {
                    btnPesquisaGrupo.PerformClick();
                }
            }
        }

        private void txtSubGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                subGrupo = new ProdutoSubGrupo();
                if (!String.IsNullOrEmpty(txtSubGrupo.Texts))
                {
                    if (generica.eNumero(txtSubGrupo.Texts))
                    {
                        subGrupo.Id = int.Parse(txtSubGrupo.Texts);
                        try
                        {
                            subGrupo = (ProdutoSubGrupo)Controller.getInstance().selecionar(subGrupo);
                            txtSubGrupo.Texts = subGrupo.Descricao;
                            txtCodSubGrupo.Texts = subGrupo.Id.ToString();
                            txtGrupo.Texts = subGrupo.ProdutoGrupo.Descricao;
                            txtcodGrupo.Texts = subGrupo.ProdutoGrupo.Id.ToString();
                            grupo = subGrupo.ProdutoGrupo;
                            txtSetor.Focus();
                            if (!String.IsNullOrEmpty(txtcodGrupo.Texts))
                            {
                                grupo = new ProdutoGrupo();
                                grupo.Id = int.Parse(txtcodGrupo.Texts);
                                grupo = (ProdutoGrupo)Controller.getInstance().selecionar(grupo);
                                if (grupo.Id != subGrupo.ProdutoGrupo.Id)
                                {
                                    txtGrupo.Texts = grupo.Descricao;
                                    txtcodGrupo.Texts = grupo.Id.ToString();
                                }
                            }
                        }
                        catch
                        {
                            subGrupo = null;
                            txtCodSubGrupo.Texts = "";
                            txtSubGrupo.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhum subGrupo encontrado");
                            txtSubGrupo.SelectAll();
                        }
                    }
                    else if (subGrupo == null || generica.eNumero(txtSubGrupo.Texts) == false)
                    {
                        IList<ProdutoSubGrupo> lista = produtoSubGrupoController.selecionarProdutoSubGrupoComVariosFiltros(txtSubGrupo.Texts);
                        if (lista.Count == 1)
                        {
                            foreach (ProdutoSubGrupo sub in lista)
                            {
                                subGrupo = sub;
                                txtSubGrupo.Texts = sub.Descricao;
                                txtCodSubGrupo.Texts = sub.Id.ToString();

                                txtGrupo.Texts = sub.ProdutoGrupo.Descricao;
                                txtcodGrupo.Texts = sub.ProdutoGrupo.Id.ToString();
                                grupo = sub.ProdutoGrupo;
                                txtSetor.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaSubGrupo.PerformClick();
                        }
                        else
                        {
                            GenericaDesktop.ShowAlerta("Nenhum subGrupo encontrado");
                        }
                    }
                }
                else
                {
                    btnPesquisaSubGrupo.PerformClick();
                }
            }
        }

        private void txtSetor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                setor = new ProdutoSetor();
                if (!String.IsNullOrEmpty(txtSetor.Texts))
                {
                    if (generica.eNumero(txtSetor.Texts))
                    {
                        setor.Id = int.Parse(txtSetor.Texts);
                        try
                        {
                            setor = (ProdutoSetor)Controller.getInstance().selecionar(setor);
                            txtSetor.Texts = setor.Descricao;
                            txtCodSetor.Texts = setor.Id.ToString();
                            txtCodBarras.Focus();
                        }
                        catch
                        {
                            setor = null;
                            txtCodSetor.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhum setor encontrado");
                            txtSetor.SelectAll();
                        }
                    }
                    else if (setor == null || generica.eNumero(txtSetor.Texts) == false)
                    {
                        IList<ProdutoSetor> lista = produtoSetorController.selecionarProdutoSetorComVariosFiltros(txtSetor.Texts, Sessao.empresaFilialLogada);
                        if (lista.Count == 1)
                        {
                            foreach (ProdutoSetor set in lista)
                            {
                                setor = set;
                                txtSetor.Texts = set.Descricao;
                                txtCodSetor.Texts = set.Id.ToString();
                                txtCodBarras.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaSetor.PerformClick();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nenhum setor encontrado");
                    }
                }
                else
                {
                    btnPesquisaSetor.PerformClick();
                }
            }
        }

        private void txtGrupoFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                grupoFiscal = new GrupoFiscal();
                if (!String.IsNullOrEmpty(txtGrupoFiscal.Texts))
                {
                    if (generica.eNumero(txtGrupoFiscal.Texts))
                    {
                        grupoFiscal.Id = int.Parse(txtGrupoFiscal.Texts);
                        try
                        {
                            grupoFiscal = (GrupoFiscal)Controller.getInstance().selecionar(grupoFiscal);
                            txtGrupoFiscal.Texts = grupoFiscal.Descricao;
                            txtCodGrupoFiscal.Texts = grupoFiscal.Id.ToString();
                            txtNCM.Focus();
                        }
                        catch
                        {
                            grupoFiscal = null;
                            txtCodGrupoFiscal.Texts = "";
                            GenericaDesktop.ShowAlerta("Nenhum grupo fiscal encontrado");
                            txtGrupoFiscal.SelectAll();
                        }
                    }
                    else if (grupoFiscal == null || generica.eNumero(txtGrupoFiscal.Texts) == false)
                    {
                        IList<GrupoFiscal> lista = grupoFiscalController.selecionarGrupoFiscalComVariosFiltros(txtGrupoFiscal.Texts);
                        if (lista.Count == 1)
                        {
                            foreach (GrupoFiscal gf in lista)
                            {
                                grupoFiscal = gf;
                                txtGrupoFiscal.Texts = gf.Descricao;
                                txtCodGrupoFiscal.Texts = gf.Id.ToString();
                                txtNCM.Focus();
                            }
                        }
                        else if (lista.Count > 1)
                        {
                            btnPesquisaGrupoFiscal.PerformClick();
                        }
                        else
                            GenericaDesktop.ShowAlerta("Nenhum grupo fiscal encontrado");
                    }
                }
                else
                {
                    btnPesquisaGrupoFiscal.PerformClick();
                }
            }
        }

        private void chkVeiculo_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkVeiculo.Checked == true)
                    tabPageAdv3.TabVisible = true;
                else
                    tabPageAdv3.TabVisible = false;
            }
            catch
            {

            }
        }
    }
}
