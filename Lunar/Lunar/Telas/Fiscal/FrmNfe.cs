using Lunar.Telas.Cadastros.Cliente;
using Lunar.Telas.Fiscal.NFeSelecaoItens;
using Lunar.Telas.PesquisaPadrao;
using Lunar.Telas.VisualizadorPDF;
using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using LunarBase.Utilidades.NFe40Modelo;
using Newtonsoft.Json;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Lunar.Utils.OrganizacaoNF.RetConsultaProcessamento;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmNfe : Form
    {
        string numeroNFe = "";
        NaturezaOperacao naturezaOperacaoSelecionado = new NaturezaOperacao();
        Pessoa clienteSelecionado = new Pessoa();
        DataSet dsProdutos = new DataSet();
        GenericaDesktop generica = new GenericaDesktop();
        Produto produto = new Produto();
        IList<NfeProduto> listaProdutosNFe = new List<NfeProduto>();
        ProdutoController produtoController = new ProdutoController();
        Nfe nfe = new Nfe();
        int posicaoItem = 1;
        Nfe notaEditar = new Nfe();
        bool atualizaNumeroNota = false;
        public FrmNfe()
        {
            InitializeComponent();
            atualizaNumeroNota = true;
            preencherCombos();
            txtInformacoesAdicionais.Texts = Sessao.parametroSistema.InformacaoAdicionalNFe;
            gridNotaReferenciada.DataSource = dsReferencia;
            lblNumeroNfe.Text = "0";
            lblNumeroNfe.Visible = false;
            labelN.Visible = false;
            txtDataEmissao.Value = DateTime.Now;
            txtDataSaida.Value = DateTime.Now;
        }

        public FrmNfe(Nfe nfe)
        {
            InitializeComponent();
            //txtInformacoesAdicionais.Texts = Sessao.parametroSistema.InformacaoAdicionalNFe;
            atualizaNumeroNota = false;
            gridNotaReferenciada.DataSource = dsReferencia;
            preencherCombos();
            notaEditar = nfe;
            if(nfe.Cliente != null)
                clienteSelecionado = nfe.Cliente;
            if (nfe.NaturezaOperacao != null)
                naturezaOperacaoSelecionado = nfe.NaturezaOperacao;
            get_Nfe(nfe);
        }
        private void get_Nfe(Nfe nfe)
        {
            if (nfe.Cliente != null)
            {
                txtCliente.Texts = nfe.Cliente.RazaoSocial;
                txtCodCliente.Texts = nfe.Cliente.Id.ToString();
            }
            lblNumeroNfe.Text = nfe.NNf;
            txtDataEmissao.Value = nfe.DataEmissao;
            txtDataSaida.Value = nfe.DataSaida;
            if (nfe.TipoOperacao.Equals("E"))
                radioEntrada.Checked = true;
            else
                radioSaida.Checked = true;
            txtNaturezaOperacao.Texts = nfe.NatOp;
            if (nfe.NaturezaOperacao != null)
                naturezaOperacaoSelecionado = nfe.NaturezaOperacao;
            if (nfe.IndPres.Equals("1"))
                comboTipoVenda.SelectedIndex = 0;
            else if(nfe.IndPres.Equals("2"))
                comboTipoVenda.SelectedIndex = 1;
            else if (nfe.IndPres.Equals("3"))
                comboTipoVenda.SelectedIndex = 2;
            else if (nfe.IndPres.Equals("4"))
                comboTipoVenda.SelectedIndex = 3;
            else
                comboTipoVenda.SelectedIndex = 4;

            //frete
            if (nfe.ModFrete.Equals("0"))
                comboFrete.SelectedIndex = 0;
            else if (nfe.ModFrete.Equals("1"))
                comboFrete.SelectedIndex = 1;
            else if (nfe.ModFrete.Equals("2"))
                comboFrete.SelectedIndex = 2;
            else if (nfe.ModFrete.Equals("3"))
                comboFrete.SelectedIndex = 3;
            else if (nfe.ModFrete.Equals("4"))
                comboFrete.SelectedIndex = 4;
            else 
                comboFrete.SelectedIndex = 5;

            if (nfe.MovimentaEstoque == true)
                chkMovimentarEstoque.Checked = true;
            else
                chkMovimentarEstoque.Checked = false;
            if (nfe.MovimentaFinanceiro == true)
                chkGerarFinanceiro.Checked = true;
            else
                chkGerarFinanceiro.Checked = false;

            txtInformacoesAdicionais.Texts = nfe.InfCpl;

            if (nfe.Transportadora != null)
            {
                txtTransportadora.Texts = nfe.Transportadora.RazaoSocial;
                txtCodTransportadora.Texts = nfe.Transportadora.Id.ToString();
            }
            txtCodigoAntt.Texts = nfe.CodigoAntt;
            txtQtdVolume.Texts = nfe.Volume;
            txtEspecie.Texts = nfe.Especie;
            txtMarca.Texts = nfe.Marca;
            txtPlacaVeiculo.Texts = nfe.Placa;
            txtPesoBruto.Texts = nfe.PesoBruto;
            txtPesoLiquido.Texts = nfe.PesoLiquido;

            NfeProdutoController nfeProdutoController = new NfeProdutoController();
            IList<NfeProduto> listaProdutos = nfeProdutoController.selecionarProdutosPorNfe(nfe.Id);
            if(listaProdutos.Count > 0)
            {
                foreach (NfeProduto nfeProduto in listaProdutos)
                {
                    getItem(nfeProduto);
                }
                somaEnquantoDigita();
            }

            NfeReferenciaController nfeReferenciaController = new NfeReferenciaController();
            IList<NfeReferencia> listaReferencias = nfeReferenciaController.selecionarNotasReferenciadasPorNfe(nfe.Id);
            if(listaReferencias.Count > 0)
            {
                foreach (NfeReferencia nfeReferencia in listaReferencias)
                {
                    txtChaveReferencia.Texts = nfeReferencia.Chave;
                    inserirNotaReferenciada(nfeReferencia.NfeReferenciada);
                }
            }


        }

        private void getItem(NfeProduto nfeProduto)
        {
            try
            {
                string csosn = nfeProduto.CstIcms.Trim();
                if (csosn.Length == 4)
                    csosn = csosn.Substring(1, 3);

                System.Data.DataRow row = dsProduto.Tables[0].NewRow();
                row.SetField("item", posicaoItem);
                posicaoItem++;
                row.SetField("Codigo", nfeProduto.Produto.Id.ToString());
                row.SetField("Descricao", nfeProduto.Produto.Descricao);
                decimal valorUnitForm = nfeProduto.ValorProduto;
                row.SetField("ValorUnitario", string.Format("{0:0.00}", valorUnitForm));
                row.SetField("Quantidade", nfeProduto.QCom);
                decimal valorTotal = valorUnitForm * decimal.Parse(nfeProduto.QCom)/* - decimal.Parse(txtDesconto.Texts)*/;
                row.SetField("ValorTotal", string.Format("{0:0.00}", valorTotal));
                row.SetField("DescontoItem", string.Format("{0:0.00}", nfeProduto.VDesc));
                row.SetField("EstoqueAuxiliar", nfeProduto.Produto.EstoqueAuxiliar);
                row.SetField("Estoque", nfeProduto.Produto.Estoque);
                row.SetField("CfopVenda", nfeProduto.Cfop);
                row.SetField("CstIcms", csosn);

                row.SetField("PercentualICMS", nfeProduto.PICMS);
                row.SetField("PercentualRed", nfeProduto.PRedBC);
                row.SetField("BaseCalcICMS", nfeProduto.VBC.ToString("N2"));
                row.SetField("ValorICMS", nfeProduto.VICMS.ToString("N2"));
                row.SetField("PercentualPIS", nfeProduto.AliqPis);
                row.SetField("BaseCalcPis", nfeProduto.BasePis.ToString("N2"));
                row.SetField("ValorPIS", nfeProduto.ValorPis.ToString("N2"));
                row.SetField("PercentualICMSST", nfeProduto.PICMSST);
                row.SetField("BaseCalcICMSST", nfeProduto.VBCST.ToString("N2"));
                row.SetField("ValorICMSST", nfeProduto.VICMSSt.ToString("N2"));
                row.SetField("PercentualIPI", nfeProduto.AliqIpi);
                row.SetField("BaseCalcIPI", nfeProduto.BaseIpi.ToString("N2"));
                row.SetField("ValorIPI", nfeProduto.ValorIpi.ToString("N2"));
                row.SetField("PercentualCOFINS", nfeProduto.AliqCofins);
                row.SetField("BaseCalcCOFINS", nfeProduto.BaseCofins.ToString("N2"));
                row.SetField("ValorCOFINS", nfeProduto.ValorCofins.ToString("N2"));
                row.SetField("PercentualFCP", nfeProduto.PFCP);
                row.SetField("BaseCalcFCPST", nfeProduto.VBCFCPST.ToString("N2"));
                row.SetField("ValorFCPST", nfeProduto.VFCPST.ToString("N2"));
                row.SetField("BaseCalcICMSSTRet", nfeProduto.VBCSTRet.ToString("N2"));
                row.SetField("ValorICMSRet", nfeProduto.VICMSSTRet.ToString("N2"));
                row.SetField("PercentualFCPRet", nfeProduto.PFCPSTRet);
                row.SetField("PercentualSTCons", "");
                row.SetField("vFrete", nfeProduto.VFrete.ToString("N2"));
                row.SetField("vOutro", nfeProduto.VOutro.ToString("N2"));
                row.SetField("vSeguro", nfeProduto.VSeguro.ToString("N2"));
                dsProduto.Tables[0].Rows.Add(row);

                this.produto = new Produto();
                if (dsProduto.Tables.Count > 0)
                {
                    dsProdutos = dsProduto;
                    gridProdutos.DataSource = dsProdutos;
                    gridProdutos.DataMember = "Produto";
                    somaEnquantoDigita();
                }
                if (this.gridProdutos.View.Records.Count > 0)
                {
                    gridProdutos.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                    gridProdutos.AutoSizeController.Refresh();
                }
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowErro("Erro puxar itens da nota " + erro.Message);
            }
        }
        private async void preencherCombos()
        {
            List<string> listaTipoVenda = new List<string>();
            listaTipoVenda.Add("1 - Operação presencial");
            listaTipoVenda.Add("2 - Operação não presencial, pela Internet");
            listaTipoVenda.Add("3 - Operação não presencial, Teleatendimento");
            listaTipoVenda.Add("4 - NFCe em operação com entrega a domicílio");
            listaTipoVenda.Add("9 - Operação não presencial, outros");
            comboTipoVenda.DataSource = listaTipoVenda;
            comboTipoVenda.ShowToolTip = true;
            comboTipoVenda.SelectedIndex = 0;

            List<string> listaTipoFrete = new List<string>();
            listaTipoFrete.Add("0 – Contratação do Frete por conta do Remetente(CIF)");
            listaTipoFrete.Add("1 – Contratação do Frete por conta do Destinatário (FOB)");
            listaTipoFrete.Add("2 – Contratação do Frete por conta de Terceiros");
            listaTipoFrete.Add("3 – Transporte Próprio por conta do Remetente");
            listaTipoFrete.Add("4 – Transporte Próprio por conta do Destinatário");
            listaTipoFrete.Add("9 – Sem Ocorrência de Transporte");
            comboFrete.DataSource = listaTipoFrete;
            comboFrete.ShowToolTip = true;
            comboFrete.SelectedIndex = 5;
        }
        private void FrmNfe_Load(object sender, EventArgs e)
        {
            //preencherCombos();
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            if (h < 768)
            {
                GenericaDesktop.ShowAlerta("A resolução do seu monitor não atende os requisitos mínimos do sistema Lunar, " +
                    "verifique com um técnico a possibilidade da resolução de tela ficar em 1366x768 ou superior");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void btnPesquisaNaturezaOperacao_Click(object sender, EventArgs e)
        {
            Object naturezaOperacaoObjeto = new NaturezaOperacao();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("NaturezaOperacao", "and Tabela.Descricao like '%" + txtNaturezaOperacao.Texts + "%'"))
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
                    switch (uu.showModal("NaturezaOperacao", "", ref naturezaOperacaoObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmNaturezaOperacao form = new FrmNaturezaOperacao();
                            NaturezaOperacao natureza = new NaturezaOperacao();
                            if (form.showModal(ref natureza) == DialogResult.OK)
                            {
                                txtNaturezaOperacao.Texts = natureza.Descricao;
                                naturezaOperacaoSelecionado = natureza;
                                if (naturezaOperacaoSelecionado.EntradaSaida == "S")
                                    radioSaida.Checked = true;
                                else
                                    radioEntrada.Checked = true;
                                if (naturezaOperacaoSelecionado.MovimentaEstoque)
                                    chkMovimentarEstoque.Checked = true;
                                else
                                    chkMovimentarEstoque.Checked = false;

                                if (naturezaOperacaoSelecionado.MovimentaFinanceiro)
                                    chkGerarFinanceiro.Checked = true;
                                else
                                    chkGerarFinanceiro.Checked = false;
                                if (naturezaOperacaoSelecionado.FinalidadeNfe.Equals("4"))
                                {
                                    tabControlAdv1.SelectedTab = tabPageAdv3;
                                    GenericaDesktop.ShowInfo("Para notas de devolução você deve referenciar a nota de origem, clique na lupa para pesquisar ou digite a chave da nota!");
                                }
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtNaturezaOperacao.Texts = ((NaturezaOperacao)naturezaOperacaoObjeto).Descricao;
                            naturezaOperacaoSelecionado = (NaturezaOperacao)naturezaOperacaoObjeto;
                            if (naturezaOperacaoSelecionado.EntradaSaida == "S")
                                radioSaida.Checked = true;
                            else
                                radioEntrada.Checked = true;
                            if (naturezaOperacaoSelecionado.MovimentaEstoque)
                                chkMovimentarEstoque.Checked = true;
                            else
                                chkMovimentarEstoque.Checked = false;

                            if (naturezaOperacaoSelecionado.MovimentaFinanceiro)
                                chkGerarFinanceiro.Checked = true;
                            else
                                chkGerarFinanceiro.Checked = false;
                            if (naturezaOperacaoSelecionado.FinalidadeNfe.Equals("4"))
                            {
                                tabControlAdv1.SelectedTab = tabPageAdv3;
                                GenericaDesktop.ShowInfo("Para notas de devolução você deve referenciar a nota de origem, clique na lupa para pesquisar ou digite a chave da nota!");
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

        private void txtNaturezaOperacao_Leave(object sender, EventArgs e)
        {
            if(naturezaOperacaoSelecionado.Id == 0)
                btnPesquisaNaturezaOperacao.PerformClick();
        }

        private void txtNaturezaOperacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                btnPesquisaNaturezaOperacao.PerformClick();
            }
        }

        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
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
                                clienteSelecionado = (Pessoa)pessoaOjeto;
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            clienteSelecionado = (Pessoa)pessoaOjeto;
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
        //private void pesquisaCliente()
        //{
        //    Object pessoaOjeto = new Pessoa();
        //    Form formBackground = new Form();
        //    try
        //    {
        //        using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtCliente.Texts + "%' and Tabela.Cliente = true"))
        //        {
        //            formBackground.StartPosition = FormStartPosition.Manual;
        //            //formBackground.FormBorderStyle = FormBorderStyle.None;
        //            formBackground.Opacity = .50d;
        //            formBackground.BackColor = Color.Black;
        //            //formBackground.Left = Top = 0;
        //            formBackground.Width = Screen.PrimaryScreen.WorkingArea.Width;
        //            formBackground.Height = Screen.PrimaryScreen.WorkingArea.Height;
        //            formBackground.WindowState = FormWindowState.Maximized;
        //            formBackground.TopMost = false;
        //            formBackground.Location = this.Location;
        //            formBackground.ShowInTaskbar = false;
        //            formBackground.Show();
        //            uu.Owner = formBackground;
        //            switch (uu.showModal("Pessoa", "", ref pessoaOjeto))
        //            {
        //                case DialogResult.Ignore:
        //                    uu.Dispose();
        //                    FrmClienteCadastro form = new FrmClienteCadastro();
        //                    if (form.showModalNovo(ref pessoaOjeto) == DialogResult.OK)
        //                    {
        //                        txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                        txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                        clienteSelecionado = (Pessoa)pessoaOjeto;
        //                    }
        //                    form.Dispose();
        //                    break;
        //                case DialogResult.OK:
        //                    txtCliente.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
        //                    txtCodCliente.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
        //                    clienteSelecionado = (Pessoa)pessoaOjeto;
        //                    break;
        //            }

        //            formBackground.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        formBackground.Dispose();
        //    }
        //}

        private void pesquisaTransportadora()
        {
            Object pessoaOjeto = new Pessoa();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Pessoa", "and CONCAT(Tabela.Id, ' ', Tabela.RazaoSocial, ' ', Tabela.Email, ' ', Tabela.Cnpj, ' ', Tabela.NomeFantasia) like '%" + txtTransportadora.Texts + "%' and Tabela.Transportadora = true"))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    //formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .50d;
                    formBackground.BackColor = Color.Black;
                   // formBackground.Left = Top = 0;
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
                                txtTransportadora.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                                txtCodTransportadora.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtTransportadora.Texts = ((Pessoa)pessoaOjeto).RazaoSocial;
                            txtCodTransportadora.Texts = ((Pessoa)pessoaOjeto).Id.ToString();
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

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisaCliente.PerformClick();
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = int.Parse(txtCodCliente.Texts.Trim());
                        pessoa = (Pessoa)PessoaController.getInstance().selecionar(pessoa);
                        if (pessoa.Id > 0)
                        {
                            txtCliente.Texts = pessoa.RazaoSocial;
                            clienteSelecionado = pessoa;
                        }
                    }
                }
                catch (Exception erro)
                {
                    GenericaDesktop.ShowAlerta(erro.Message);
                    txtCodCliente.Texts = "";
                    txtCliente.Texts = "";
                }
            }
        }
        private void somaEnquantoDigita()
        {
            try
            {
                decimal valorTotal = 0;
                decimal valorComDesconto = 0;
                double pecas = 0;
                var records = gridProdutos.View.Records;
                decimal descontoItem = 0;
                foreach (var record in records)
                {
                    var dataRowView = record.Data as DataRowView;
                    
                    descontoItem = descontoItem + decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
                    valorTotal = valorTotal + decimal.Parse(dataRowView.Row["ValorTotal"].ToString());
                    pecas = pecas + double.Parse(dataRowView.Row["Quantidade"].ToString());
                }

                txtDesconto.Enabled = true;
                txtTotalProduto.Texts = valorTotal.ToString("C2", CultureInfo.CurrentCulture);

                valorComDesconto = valorTotal;
                if (!String.IsNullOrEmpty(txtDesconto.Texts))
                {
                    //Se tem desconto no total ele ratea o desconto
                    if (descontoItem != decimal.Parse(txtDesconto.Texts) && decimal.Parse(txtDesconto.Texts) > 0)
                    {
                        decimal descontoPercentualNaVenda = (decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                        ratearDescontoItens(descontoPercentualNaVenda);
                    }
                    else
                    {
                        txtDesconto.Texts = descontoItem.ToString("N2");
                    }
                    valorComDesconto = valorComDesconto - decimal.Parse(txtDesconto.Texts.Replace("R$ ", ""));
                }
                if (!String.IsNullOrEmpty(txtFrete.Texts))
                {
                    decimal fretePercentual = (decimal.Parse(txtFrete.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                    ratearFreteItens(fretePercentual);
                    valorComDesconto = valorComDesconto + decimal.Parse(txtFrete.Texts.Replace("R$ ", ""));
                }
                if (!String.IsNullOrEmpty(txtOutrasDepesas.Texts))
                {
                    decimal percentual = (decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                    ratearOutrosValoresItens(percentual);
                    valorComDesconto = valorComDesconto + decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", ""));
                }
                if (!String.IsNullOrEmpty(txtSeguro.Texts))
                {
                    decimal percentual = (decimal.Parse(txtSeguro.Texts.Replace("R$ ", "")) * 100 / valorTotal);
                    ratearSeguroItens(percentual);
                    valorComDesconto = valorComDesconto + decimal.Parse(txtSeguro.Texts.Replace("R$ ", ""));
                }
                txtTotalNota.Texts = valorComDesconto.ToString("C2", CultureInfo.CurrentCulture);
            }
            catch
            {
  
            }
        }

        private void ratearDescontoItens(decimal percentualDesconto)
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            decimal somarDescontoTotal = 0;
            decimal valorDescontoInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;

                decimal descontoItem = (decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) * percentualDesconto) / 100;
                descontoItem = Math.Round(descontoItem, 2);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["DescontoItem"].MappingName, descontoItem);
                i++;
                somarDescontoTotal = somarDescontoTotal + decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
                valorDescontoInformado = decimal.Parse(dataRowView.Row["DescontoItem"].ToString());
            }
            String descontoInformado = somarDescontoTotal.ToString("N2");
            string valorDescontoItensInf = somarDescontoTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (txtDesconto.Texts != valorDescontoItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")) - somarDescontoTotal;
                // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if (somarDescontoTotal > decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado - diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["DescontoItem"].MappingName, valorDescontoInformado);

                }
                else if (somarDescontoTotal < decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")))
                {
                    valorDescontoInformado = valorDescontoInformado + diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["DescontoItem"].MappingName, valorDescontoInformado);
                }
            }
        }

        private void ratearFreteItens(decimal percentualFrete)
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            decimal somarFreteTotal = 0;
            decimal valorFreteInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;

                decimal valorFreteItem = (decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) * percentualFrete) / 100;
                valorFreteItem = Math.Round(valorFreteItem, 2);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["vFrete"].MappingName, valorFreteItem);
                i++;
                somarFreteTotal = somarFreteTotal + decimal.Parse(dataRowView.Row["vFrete"].ToString());
                valorFreteInformado = decimal.Parse(dataRowView.Row["vFrete"].ToString());
            }
            String descontoInformado = somarFreteTotal.ToString("N2");
            string valorFreteItensInf = somarFreteTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (txtFrete.Texts != valorFreteItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(txtFrete.Texts.Replace("R$ ", "")) - somarFreteTotal;
                // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if (somarFreteTotal > decimal.Parse(txtFrete.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado - diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vFrete"].MappingName, valorFreteInformado);

                }
                else if (somarFreteTotal < decimal.Parse(txtFrete.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado + diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vFrete"].MappingName, valorFreteInformado);
                }
            }
        }

        private void ratearOutrosValoresItens(decimal percentualOutroValores)
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            decimal somarFreteTotal = 0;
            decimal valorFreteInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;

                decimal valorFreteItem = (decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) * percentualOutroValores) / 100;
                valorFreteItem = Math.Round(valorFreteItem, 2);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["vOutro"].MappingName, valorFreteItem);
                i++;
                somarFreteTotal = somarFreteTotal + decimal.Parse(dataRowView.Row["vOutro"].ToString());
                valorFreteInformado = decimal.Parse(dataRowView.Row["vOutro"].ToString());
            }
            String descontoInformado = somarFreteTotal.ToString("N2");
            string valorFreteItensInf = somarFreteTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (txtOutrasDepesas.Texts != valorFreteItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", "")) - somarFreteTotal;
                // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if (somarFreteTotal > decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado - diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vOutro"].MappingName, valorFreteInformado);

                }
                else if (somarFreteTotal < decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado + diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vOutro"].MappingName, valorFreteInformado);
                }
            }
        }

        private void ratearSeguroItens(decimal percentualSeguro)
        {
            var records = gridProdutos.View.Records;
            int i = 1;
            decimal somarFreteTotal = 0;
            decimal valorFreteInformado = 0;
            foreach (var record in records)
            {
                var dataRowView = record.Data as DataRowView;

                decimal valorFreteItem = (decimal.Parse(dataRowView.Row["ValorTotal"].ToString()) * percentualSeguro) / 100;
                valorFreteItem = Math.Round(valorFreteItem, 2);
                gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(i), gridProdutos.Columns["vSeguro"].MappingName, valorFreteItem);
                i++;
                somarFreteTotal = somarFreteTotal + decimal.Parse(dataRowView.Row["vSeguro"].ToString());
                valorFreteInformado = decimal.Parse(dataRowView.Row["vSeguro"].ToString());
            }
            String descontoInformado = somarFreteTotal.ToString("N2");
            string valorFreteItensInf = somarFreteTotal.ToString("C2", CultureInfo.CurrentCulture);
            if (txtSeguro.Texts != valorFreteItensInf)
            {
                //Se deu diferenca de centavos, ajustar no ultimo item
                decimal diferenca = decimal.Parse(txtSeguro.Texts.Replace("R$ ", "")) - somarFreteTotal;
                // MessageBox.Show("Diferença " + diferenca.ToString("N2"));
                if (somarFreteTotal > decimal.Parse(txtSeguro.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado - diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vSeguro"].MappingName, valorFreteInformado);

                }
                else if (somarFreteTotal < decimal.Parse(txtSeguro.Texts.Replace("R$ ", "")))
                {
                    valorFreteInformado = valorFreteInformado + diferenca;
                    int index = gridProdutos.RowCount - 1;
                    gridProdutos.View.GetPropertyAccessProvider().SetValue(gridProdutos.GetRecordAtRowIndex(index), gridProdutos.Columns["vSeguro"].MappingName, valorFreteInformado);
                }
            }
        }
        private void btnInserirProduto_Click(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            FrmSelecionarProdutoParaNfe uu = new FrmSelecionarProdutoParaNfe(dsProdutos);
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

            uu.showModalNovo(ref dsProdutos);
            if (dsProdutos.Tables.Count > 0)
            {
                gridProdutos.DataSource = dsProdutos;
                gridProdutos.DataMember = "Produto";
                somaEnquantoDigita();
            }

            formBackground.Dispose();
            uu.Dispose();
        }

        private void chkGerarFinanceiro_CheckStateChanged(object sender, EventArgs e)
        {
            //if (chkGerarFinanceiro.Checked == true)
            //    tabFinanceiro.TabVisible = true;
            //else
            //    tabFinanceiro.TabVisible = false;
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            btnInserirProduto.PerformClick();
        }

        private void gridProdutos_QueryRowStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                e.Style.BackColor = Color.WhiteSmoke;
            else
                e.Style.BackColor = Color.White;
        }

        private void gridProdutos_QueryCellStyle(object sender, Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventArgs e)
        {
            try
            {
                double quantidadeInformada = 0;
                double estoqueAtual = 0;
                if (e.Column.MappingName == "Quantidade")
                {
                    if(e.DisplayText != null)
                        quantidadeInformada = double.Parse(e.DisplayText.ToString());
                }
                if (e.Column.MappingName == "Estoque")
                {
                    if (e.DisplayText != null)
                    {
                        estoqueAtual = double.Parse(e.DisplayText.ToString());
                        if (estoqueAtual < quantidadeInformada)
                        {
                            e.Style.TextColor = Color.Red;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void txtFrete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                txtOutrasDepesas.Focus();
            }
            generica.SoNumeroEVirgula(txtFrete.Texts, e);
        }

        private void txtFrete_Leave(object sender, EventArgs e)
        {
            somaEnquantoDigita();
            txtFrete.Texts = decimal.Parse(txtFrete.Texts).ToString("N2");
        }

        private void txtOutrasDepesas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtSeguro.Focus();
            }
            generica.SoNumeroEVirgula(txtOutrasDepesas.Texts, e);
        }

        private void txtOutrasDepesas_Leave(object sender, EventArgs e)
        {
            somaEnquantoDigita();
            txtOutrasDepesas.Texts = decimal.Parse(txtOutrasDepesas.Texts).ToString("N2");
        }

        private void txtSeguro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEmitirNfe.Focus();
            }
            generica.SoNumeroEVirgula(txtSeguro.Texts, e);
        }

        private void txtSeguro_Leave(object sender, EventArgs e)
        {
            somaEnquantoDigita();
            txtSeguro.Texts = decimal.Parse(txtSeguro.Texts).ToString("N2");
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFrete.Focus();
            }
            generica.SoNumeroEVirgula(txtDesconto.Texts, e);
        }

        private void txtDesconto_Leave(object sender, EventArgs e)
        {
            somaEnquantoDigita();
            txtDesconto.Texts = decimal.Parse(txtDesconto.Texts).ToString("N2");
        }

        private void carregarListaProdutos()
        {
            if (gridProdutos.RowCount > 0)
            {
                listaProdutosNFe = new List<NfeProduto>();
                var records = gridProdutos.View.Records;
                int i = 0;
                foreach (var record in records)
                {
                    i++;
                    var dataRowViewXXX = record.Data as DataRowView;
                    NfeProduto nfeProduto = new NfeProduto();
                    produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
                    double quantidade = double.Parse(dataRowViewXXX.Row["Quantidade"].ToString());
                    decimal descontoItem = decimal.Parse(dataRowViewXXX.Row["DescontoItem"].ToString());
                    produto.ValorVenda = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    nfeProduto.Item = i.ToString();
                    nfeProduto.Produto = produto;
                    nfeProduto.QCom = quantidade.ToString();
                    nfeProduto.Ncm = produto.Ncm;
                    nfeProduto.Cest = produto.Cest;
                    nfeProduto.Cfop = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
                    nfeProduto.CProd = produto.Id.ToString();
                    nfeProduto.Nfe = null;//
                    nfeProduto.VProd = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    nfeProduto.QTrib = quantidade;
                    nfeProduto.VDesc = descontoItem;
                    nfeProduto.DescricaoInterna = produto.Descricao;
                    nfeProduto.XProd = produto.Descricao;
                    nfeProduto.CodigoInterno = produto.Id.ToString();
                    nfeProduto.CEan = "";
                    nfeProduto.CEANTrib = "";
                    nfeProduto.CfopEntrada = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
                    nfeProduto.AliqCofins = dataRowViewXXX.Row["PercentualCOFINS"].ToString().Trim();
                    nfeProduto.AliqIpi = dataRowViewXXX.Row["PercentualIPI"].ToString().Trim();
                    nfeProduto.AliqPis = dataRowViewXXX.Row["PercentualPIS"].ToString().Trim();
                    nfeProduto.BaseCofins = decimal.Parse(dataRowViewXXX.Row["BaseCalcCOFINS"].ToString());
                    nfeProduto.BaseIpi = decimal.Parse(dataRowViewXXX.Row["BaseCalcIPI"].ToString());
                    nfeProduto.BasePis = decimal.Parse(dataRowViewXXX.Row["BaseCalcPis"].ToString());
                    nfeProduto.PRedBC = dataRowViewXXX.Row["PercentualRed"].ToString().Trim();
                    nfeProduto.PST = dataRowViewXXX.Row["PercentualICMSST"].ToString().Trim();
                    nfeProduto.VBCST = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSST"].ToString().Trim());
                    nfeProduto.VICMSSt = decimal.Parse(dataRowViewXXX.Row["ValorICMSST"].ToString().Trim());
                    nfeProduto.PFCP = dataRowViewXXX.Row["PercentualFCP"].ToString().Trim();
                    nfeProduto.VBCFCPST = decimal.Parse(dataRowViewXXX.Row["BaseCalcFCPST"].ToString().Trim());
                    nfeProduto.VFCPST = decimal.Parse(dataRowViewXXX.Row["ValorFCPST"].ToString().Trim());
                    nfeProduto.VBCSTRet = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSSTRet"].ToString().Trim());
                    nfeProduto.VICMSSTRet = decimal.Parse(dataRowViewXXX.Row["ValorICMSRet"].ToString().Trim());
                    nfeProduto.PFCPST = dataRowViewXXX.Row["PercentualFCPRet"].ToString().Trim();
                    nfeProduto.VBC = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMS"].ToString().Trim());
                    nfeProduto.PICMS = dataRowViewXXX.Row["PercentualICMS"].ToString().Trim();
                    nfeProduto.VICMS = decimal.Parse(dataRowViewXXX.Row["ValorICMS"].ToString().Trim());

                    nfeProduto.CodAnp = produto.CodAnp;
                    nfeProduto.CodEnqIpi = produto.EnqIpi;
                    nfeProduto.CodSeloIpi = produto.CodSeloIpi;
                    nfeProduto.CstCofins = produto.CstCofins;
                    nfeProduto.CstIcms = dataRowViewXXX.Row["CstIcms"].ToString().Trim();
                    nfeProduto.CstIpi = produto.CstIpi;
                    nfeProduto.CstPis = produto.CstPis;
                    string orig = "0";
                    if (!String.IsNullOrEmpty(produto.OrigemIcms))
                        orig = produto.OrigemIcms;
                    nfeProduto.OrigemIcms = orig;
                    nfeProduto.OutrosIcms = 0;
                    nfeProduto.TPag = "";
                    nfeProduto.UCom = produto.UnidadeMedida.Sigla;
                    nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorAcrescimo = 0;
                    nfeProduto.ValorCofins = decimal.Parse(dataRowViewXXX.Row["ValorCOFINS"].ToString());
                    nfeProduto.ValorDesconto = descontoItem;
                    nfeProduto.ValorFinal = (decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString()) * decimal.Parse(quantidade.ToString())) - descontoItem;
                    nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorIpi = decimal.Parse(dataRowViewXXX.Row["ValorIPI"].ToString());
                    nfeProduto.ValorPis = decimal.Parse(dataRowViewXXX.Row["ValorPIS"].ToString());
                    nfeProduto.ValorProduto = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());

                    nfeProduto.VFrete = decimal.Parse(dataRowViewXXX.Row["vFrete"].ToString().Trim());
                    nfeProduto.VOutro = decimal.Parse(dataRowViewXXX.Row["vOutro"].ToString().Trim());
                    nfeProduto.VSeguro = decimal.Parse(dataRowViewXXX.Row["vSeguro"].ToString().Trim());

                    listaProdutosNFe.Add(nfeProduto);
                }
            }
            else
                GenericaDesktop.ShowAlerta("Você deve inserir pelo menos 1 produto");
        }

        private void carregarListaProdutosESalvar()
        {
            if (gridProdutos.RowCount > 0)
            {
                listaProdutosNFe = new List<NfeProduto>();
                var records = gridProdutos.View.Records;
                int i = 0;
                foreach (var record in records)
                {
                    i++;
                    var dataRowViewXXX = record.Data as DataRowView;
                    NfeProduto nfeProduto = new NfeProduto();
                    produto = produtoController.selecionarProdutoPorCodigoUnicoEFilial(int.Parse(dataRowViewXXX.Row["Codigo"].ToString()), Sessao.empresaFilialLogada);
                    double quantidade = double.Parse(dataRowViewXXX.Row["Quantidade"].ToString());
                    decimal descontoItem = decimal.Parse(dataRowViewXXX.Row["DescontoItem"].ToString());
                    produto.ValorVenda = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    nfeProduto.Item = i.ToString();
                    nfeProduto.Produto = produto;
                    nfeProduto.QCom = quantidade.ToString();
                    nfeProduto.Ncm = produto.Ncm;
                    nfeProduto.Cest = produto.Cest;
                    nfeProduto.Cfop = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
                    nfeProduto.CProd = produto.Id.ToString();
                    nfeProduto.Nfe = null;//
                    nfeProduto.VProd = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());
                    nfeProduto.QTrib = quantidade;
                    nfeProduto.VDesc = descontoItem;
                    nfeProduto.DescricaoInterna = produto.Descricao;
                    nfeProduto.XProd = produto.Descricao;
                    nfeProduto.CodigoInterno = produto.Id.ToString();
                    nfeProduto.CEan = "";
                    nfeProduto.CEANTrib = "";
                    nfeProduto.CfopEntrada = dataRowViewXXX.Row["CfopVenda"].ToString().Trim();
                    nfeProduto.AliqCofins = dataRowViewXXX.Row["PercentualCOFINS"].ToString().Trim();
                    nfeProduto.AliqIpi = dataRowViewXXX.Row["PercentualIPI"].ToString().Trim();
                    nfeProduto.AliqPis = dataRowViewXXX.Row["PercentualPIS"].ToString().Trim();
                    nfeProduto.BaseCofins = decimal.Parse(dataRowViewXXX.Row["BaseCalcCOFINS"].ToString());
                    nfeProduto.BaseIpi = decimal.Parse(dataRowViewXXX.Row["BaseCalcIPI"].ToString());
                    nfeProduto.BasePis = decimal.Parse(dataRowViewXXX.Row["BaseCalcPis"].ToString());
                    nfeProduto.PRedBC = dataRowViewXXX.Row["PercentualRed"].ToString().Trim();
                    nfeProduto.PST = dataRowViewXXX.Row["PercentualICMSST"].ToString().Trim();
                    nfeProduto.VBCST = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSST"].ToString().Trim());
                    nfeProduto.VICMSSt = decimal.Parse(dataRowViewXXX.Row["ValorICMSST"].ToString().Trim());
                    nfeProduto.PFCP = dataRowViewXXX.Row["PercentualFCP"].ToString().Trim();
                    nfeProduto.VBCFCPST = decimal.Parse(dataRowViewXXX.Row["BaseCalcFCPST"].ToString().Trim());
                    nfeProduto.VFCPST = decimal.Parse(dataRowViewXXX.Row["ValorFCPST"].ToString().Trim());
                    nfeProduto.VBCSTRet = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMSSTRet"].ToString().Trim());
                    nfeProduto.VICMSSTRet = decimal.Parse(dataRowViewXXX.Row["ValorICMSRet"].ToString().Trim());
                    nfeProduto.PFCPST = dataRowViewXXX.Row["PercentualFCPRet"].ToString().Trim();
                    nfeProduto.VBC = decimal.Parse(dataRowViewXXX.Row["BaseCalcICMS"].ToString().Trim());
                    nfeProduto.PICMS = dataRowViewXXX.Row["PercentualICMS"].ToString().Trim();
                    nfeProduto.VICMS = decimal.Parse(dataRowViewXXX.Row["ValorICMS"].ToString().Trim());

                    nfeProduto.CodAnp = produto.CodAnp;
                    nfeProduto.CodEnqIpi = produto.EnqIpi;
                    nfeProduto.CodSeloIpi = produto.CodSeloIpi;
                    nfeProduto.CstCofins = produto.CstCofins;
                    nfeProduto.CstIcms = dataRowViewXXX.Row["CstIcms"].ToString().Trim();
                    nfeProduto.CstIpi = produto.CstIpi;
                    nfeProduto.CstPis = produto.CstPis;
                    string orig = "0";
                    if (!String.IsNullOrEmpty(produto.OrigemIcms))
                        orig = produto.OrigemIcms;
                    nfeProduto.OrigemIcms = orig;
                    nfeProduto.OutrosIcms = 0;
                    nfeProduto.TPag = "";
                    nfeProduto.UCom = produto.UnidadeMedida.Sigla;
                    nfeProduto.UTrib = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorAcrescimo = 0;
                    nfeProduto.ValorCofins = decimal.Parse(dataRowViewXXX.Row["ValorCOFINS"].ToString());
                    nfeProduto.ValorDesconto = descontoItem;
                    nfeProduto.ValorFinal = (decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString()) * decimal.Parse(quantidade.ToString())) - descontoItem;
                    nfeProduto.UComConvertida = produto.UnidadeMedida.Sigla;
                    nfeProduto.ValorIpi = decimal.Parse(dataRowViewXXX.Row["ValorIPI"].ToString());
                    nfeProduto.ValorPis = decimal.Parse(dataRowViewXXX.Row["ValorPIS"].ToString());
                    nfeProduto.ValorProduto = decimal.Parse(dataRowViewXXX.Row["ValorUnitario"].ToString());

                    nfeProduto.VFrete = decimal.Parse(dataRowViewXXX.Row["vFrete"].ToString().Trim());
                    nfeProduto.VOutro = decimal.Parse(dataRowViewXXX.Row["vOutro"].ToString().Trim());
                    nfeProduto.VSeguro = decimal.Parse(dataRowViewXXX.Row["vSeguro"].ToString().Trim());
                    nfeProduto.Nfe = nfe;

                    Controller.getInstance().salvar(nfeProduto);
                }
            }
            else
                GenericaDesktop.ShowAlerta("Você deve inserir pelo menos 1 produto");
        }
        private void btnEmitirNfe_Click(object sender, EventArgs e)
        {
            bool validaCliente = false;
            //enviarNFCe();
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa cli = new Pessoa();
                cli.Id = int.Parse(txtCodCliente.Texts);
                cli = (Pessoa)Controller.getInstance().selecionar(cli);
                validaCliente = validarClienteNFCe(cli);

                ValidadorNotaSaida validador = new ValidadorNotaSaida();
                if (validaCliente == true)
                {
                    EmitirNfe2 emitirNFe2 = new EmitirNfe2();
                    carregarListaProdutos();
                    if (validador.validarProdutosNota(listaProdutosNFe))
                    {
                        if(lblNumeroNfe.Text == "0")
                            numeroNFe = Sessao.parametroSistema.ProximoNumeroNFe;
                        else
                            numeroNFe = lblNumeroNfe.Text;
                        TNFeInfNFeTranspModFrete frete = retornaFrete();
                        TNFeInfNFeIdeIndPres presenca = retornaPresencaCliente();
                        TNFeInfNFeIdeNFref[] tNFeInfNFeIdeNFref = new TNFeInfNFeIdeNFref[gridNotaReferenciada.RowCount];
                 
                
                        Frete fr = new Frete();
                        if (!frete.ToString().Equals("Item9"))
                        {
                            Pessoa pessoaTransporte = new Pessoa();
                            if (!String.IsNullOrEmpty(txtCodTransportadora.Texts))
                            {
                                pessoaTransporte.Id = int.Parse(txtCodTransportadora.Texts);
                                pessoaTransporte = (Pessoa)Controller.getInstance().selecionar(pessoaTransporte);
                                if (pessoaTransporte != null)
                                {
                                    fr.transportadora = pessoaTransporte;
                                }
                            }
                            else
                                fr.transportadora = null;

                            fr.codigoAntt = txtCodigoAntt.Texts.Trim();
                            fr.especie = txtEspecie.Texts.Trim();
                            fr.marca = txtMarca.Texts.Trim();
                            fr.pesoBruto = txtPesoBruto.Texts.Trim();
                            fr.pesoLiquido = txtPesoLiquido.Texts.Trim();
                            fr.placa = txtPlacaVeiculo.Texts.Trim();
                            fr.quantidadeVolume = txtQtdVolume.Texts.Trim();
                        }

                        decimal fret = 0;
                        decimal segur = 0;
                        decimal outr = 0;
                        
                   
                        if (!String.IsNullOrEmpty(txtFrete.Texts)) 
                            fret = decimal.Parse(txtFrete.Texts);
                        if (!String.IsNullOrEmpty(txtSeguro.Texts))
                            segur = decimal.Parse(txtSeguro.Texts);
                        if (!String.IsNullOrEmpty(txtOutrasDepesas.Texts))
                            outr = decimal.Parse(txtOutrasDepesas.Texts);
                        TNFeInfNFeIdeTpNF tpNF = new TNFeInfNFeIdeTpNF();
                        if (radioEntrada.Checked == true)
                            tpNF = TNFeInfNFeIdeTpNF.Item0;
                        else
                            tpNF = TNFeInfNFeIdeTpNF.Item1;

                        nfe = alimentaNfe();

                        NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                        NfeReferenciaDAO nfeReferenciaDAO = new NfeReferenciaDAO();
                        nfeProdutoDAO.excluirProdutosNfeParaAtualizar(nfe.Id.ToString());
                        nfeReferenciaDAO.excluirReferenciaNfeParaAtualizar(nfe.Id.ToString());

                        tNFeInfNFeIdeNFref = retornarNotasReferenciadas();

                        string xmlStrEnvio = emitirNFe2.gerarXMLNfe(decimal.Parse(txtTotalProduto.Texts.Replace("R$ ", "")), decimal.Parse(txtTotalNota.Texts.Replace("R$ ", "")), decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")), numeroNFe, listaProdutosNFe, cli, null, false, naturezaOperacaoSelecionado.Descricao, frete, presenca, tNFeInfNFeIdeNFref, fret, segur, outr, tpNF, fr, naturezaOperacaoSelecionado, nfe);
                        if (!String.IsNullOrEmpty(xmlStrEnvio))
                        {
                            enviarXMLNFeParaApi(xmlStrEnvio);
                            //limparCampos();
                        }
                        //somente atualiza o numero se for uma nota nova, se for reenvio ou edição nao atualiza
                        if(atualizaNumeroNota == true)
                            Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(numeroNFe) + 1).ToString();
                        Controller.getInstance().salvar(Sessao.parametroSistema);
                    }
                }
            }
        }

        private Nfe alimentaNfe()
        {
            nfe = new Nfe();
            NfeController nfeController = new NfeController();
            
            if(!String.IsNullOrEmpty(lblNumeroNfe.Text) && int.Parse(lblNumeroNfe.Text) > 0)
            {
                nfe.NNf = lblNumeroNfe.Text;
                nfe.Id = notaEditar.Id;
                nfe = nfeController.selecionarNFePorNumeroESerie(lblNumeroNfe.Text, Sessao.parametroSistema.SerieNFe);
            }
            else
            {
                nfe.NNf = Sessao.parametroSistema.ProximoNumeroNFe;
            }
            if (chkGerarFinanceiro.Checked == true)
                nfe.MovimentaFinanceiro = true;
            else
                nfe.MovimentaFinanceiro = false;

            if (chkMovimentarEstoque.Checked == true)
                nfe.MovimentaEstoque = true;
            else
                nfe.MovimentaEstoque = false;

            nfe.CDv = "";
            nfe.IndIntermed = "1";
            nfe.CMunFg = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            nfe.CNf = "";
            nfe.CnpjEmitente = Sessao.empresaFilialLogada.Cnpj;
            nfe.Conciliado = true;
            nfe.CUf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            nfe.DataLancamento = DateTime.Now;
            nfe.DataEmissao = DateTime.Now;
            nfe.DhEmi = DateTime.Parse(txtDataEmissao.Value.ToString()).ToShortDateString();
            nfe.DhSaiEnt = DateTime.Parse(txtDataSaida.Value.ToString()).ToShortDateString();
            nfe.EmpresaFilial = Sessao.empresaFilialLogada;
            nfe.Lancada = true;
            if (radioSaida.Checked == true)
                nfe.TipoOperacao = "S";
            else
                nfe.TipoOperacao = "E";
            nfe.Modelo = "55";
            nfe.NatOp = txtNaturezaOperacao.Texts;
            nfe.NaturezaOperacao = naturezaOperacaoSelecionado;
            nfe.RazaoEmitente = Sessao.empresaFilialLogada.RazaoSocial;

            //nao precisamos alimentar os valores totais agora, pois a soma nos itens vai armazenar essa informacao e gravar na nota posteriormente
            nfe.VBc = 0;
            nfe.VIcms = 0;
            nfe.VIcmsDeson = 0;
            nfe.VFcp = 0;
            nfe.VBcst = 0;
            nfe.VSt = 0;
            nfe.VFcpst = 0;
            nfe.VFcpstRet = 0;
            nfe.VFrete = decimal.Parse(txtFrete.Texts.Replace("R$ ", ""));
            nfe.VProd = decimal.Parse(txtTotalProduto.Texts.Replace("R$ ", ""));
            nfe.VSeg = decimal.Parse(txtSeguro.Texts.Replace("R$ ", ""));
            nfe.VDesc = decimal.Parse(txtDesconto.Texts.Replace("R$ ", ""));
            nfe.VIi = 0;
            nfe.VIpi = 0;
            nfe.VIpiDevol = 0;
            nfe.VPis = 0;
            nfe.VCofins = 0;
            nfe.VOutro = decimal.Parse(txtOutrasDepesas.Texts.Replace("R$ ", ""));
            nfe.VNf = decimal.Parse(txtTotalNota.Texts.Replace("R$ ", ""));
            nfe.VTotTrib = 0;

            nfe.Status = "Preparando Envio...";
            nfe.CodStatus = "0";
            nfe.Chave = "";
            nfe.Serie = Sessao.parametroSistema.SerieNFe;

            nfe.TpNf = "1";
            
            if(clienteSelecionado.EnderecoPrincipal != null)
            {
                if (clienteSelecionado.EnderecoPrincipal.Cidade.Estado.Id == Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Id)
                    nfe.IdDest = "1";
                else
                    nfe.IdDest = "2";
            }
            nfe.TpImp = "1";
            nfe.TpEmis = "1";
            if (Sessao.parametroSistema.AmbienteProducao == true)
                nfe.TpAmb = "1";
            else
                nfe.TpAmb = "2";
            nfe.FinNfe = naturezaOperacaoSelecionado.FinalidadeNfe;
            nfe.IndFinal = "1";
            nfe.IndPres = "1";
            nfe.InfCpl = txtInformacoesAdicionais.Texts.Trim();
            nfe.ProcEmi = "0";
            nfe.VerProc = "1.0|Lunar";
            nfe.ModFrete = comboFrete.Text.Substring(0,1);
            nfe.Manifesto = "";
            nfe.Protocolo = "";
            nfe.PossuiCartaCorrecao = false;
            if (clienteSelecionado != null)
            {
                nfe.Cliente = clienteSelecionado;
                nfe.Destinatario = clienteSelecionado.RazaoSocial;
                nfe.CnpjDestinatario = GenericaDesktop.RemoveCaracteres(clienteSelecionado.Cnpj);
            }
            nfe.CodigoAntt = txtCodigoAntt.Texts;
            nfe.DataSaida = DateTime.Parse(txtDataSaida.Value.ToString());
            if (!String.IsNullOrEmpty(txtCodTransportadora.Texts))
            {
                Pessoa transp = new Pessoa();
                transp.Id = int.Parse(txtCodTransportadora.Texts);
                transp = (Pessoa)Controller.getInstance().selecionar(transp);
                nfe.Transportadora = transp;
            }
            else
                nfe.Transportadora = null;
            nfe.Volume = txtQtdVolume.Texts;
            nfe.Xml = "";
            nfe.Placa = GenericaDesktop.RemoveCaracteres(txtPlacaVeiculo.Texts.Trim());
            nfe.Especie = txtEspecie.Texts;
            nfe.Marca = txtMarca.Texts;
            nfe.PesoBruto = txtPesoBruto.Texts;
            nfe.PesoLiquido = txtPesoLiquido.Texts;
            Controller.getInstance().salvar(nfe);
            lblNumeroNfe.Text = nfe.NNf;
            lblNumeroNfe.Visible = true;
            labelN.Visible = true;
            return nfe;
        }

        private TNFeInfNFeIdeNFref[] retornarNotasReferenciadas()
        {
            if (gridNotaReferenciada.RowCount > 0 && gridNotaReferenciada.View != null)
            {
                TNFeInfNFeIdeNFref[] tNFeInfNFeIdeNFref = new TNFeInfNFeIdeNFref[gridNotaReferenciada.RowCount];
                var records = gridNotaReferenciada.View.Records;
                int i = 0;
                foreach (var record in records)
                {
                    tNFeInfNFeIdeNFref[i] = new TNFeInfNFeIdeNFref();
                    var dataRowView = record.Data as DataRowView;
                    tNFeInfNFeIdeNFref[i].ItemElementName = ItemChoiceType1.refNFe;
                    tNFeInfNFeIdeNFref[i].Item = dataRowView.Row["Chave"].ToString();
                    i++;
                    if (!String.IsNullOrEmpty(dataRowView.Row["Chave"].ToString()))
                    {
                        NfeReferencia nfReferenciada = new NfeReferencia();
                        nfReferenciada.Chave = dataRowView.Row["Chave"].ToString();
                        nfReferenciada.Nfe = nfe;
                        NfeController nfeController = new NfeController();
                        nfReferenciada.NfeReferenciada = nfeController.selecionarNotaPorChave(nfReferenciada.Chave);
                        Controller.getInstance().salvar(nfReferenciada);
                    }
                }
                return tNFeInfNFeIdeNFref;
            }
            else
                return null;
        }
        private TNFeInfNFeTranspModFrete retornaFrete()
        {
            TNFeInfNFeTranspModFrete tipoFrete;
            string frete = comboFrete.SelectedValue.ToString().Substring(0, 1);
            if (frete.Equals("0"))
            {
                tipoFrete = TNFeInfNFeTranspModFrete.Item0;
            }
            else if (frete.Equals("1"))
            {
                tipoFrete = TNFeInfNFeTranspModFrete.Item1;
            }
            else if (frete.Equals("2"))
            {
                tipoFrete = TNFeInfNFeTranspModFrete.Item2;
            }
            else if (frete.Equals("3"))
            {
                tipoFrete = TNFeInfNFeTranspModFrete.Item3;
            }
            else if (frete.Equals("4"))
            {
                tipoFrete = TNFeInfNFeTranspModFrete.Item4;
            }
            else
                tipoFrete = TNFeInfNFeTranspModFrete.Item9;
            
            return tipoFrete;
        }

        private TNFeInfNFeIdeIndPres retornaPresencaCliente()
        {
            TNFeInfNFeIdeIndPres tipoPresenca;
            string presenca = comboTipoVenda.SelectedValue.ToString().Substring(0, 1);
            if (presenca.Equals("0"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item0;
            }
            else if (presenca.Equals("1"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item1;
            }
            else if (presenca.Equals("2"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item2;
            }
            else if (presenca.Equals("3"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item3;
            }
            else if (presenca.Equals("4"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item4;
            }
            else if (presenca.Equals("5"))
            {
                tipoPresenca = TNFeInfNFeIdeIndPres.Item5;
            }
            else
                tipoPresenca = TNFeInfNFeIdeIndPres.Item9;
                
            return tipoPresenca;
        }

        private void enviarXMLNFeParaApi(string xmlNfe)
        {
            RetConsultaProcessamentoNF retConsulta = new RetConsultaProcessamentoNF();
            NfeStatus nfeStatus = new NfeStatus();
            string caminhoSalvarXml = @"Fiscal\XML\NFe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
            NfeController nfeController = new NfeController();
            nfe = nfeController.selecionarNFePorNumeroESerie(numeroNFe, Sessao.parametroSistema.SerieNFe);

            EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();
            string codStatusRet = "";
            gravarXMLNaPasta(xmlNfe, nfe.NNf, @"\XML\Tentativa\NFe\", nfe.NNf + ".xml");
            if (GenericaDesktop.possuiConexaoInternet())
            {
                String retorno = NSSuite.emitirNFeSincrono(xmlNfe, "xml", Sessao.empresaFilialLogada.Cnpj, "XP", "2", caminhoSalvarXml, false, false);
                retornoNFCe = JsonConvert.DeserializeObject<EmitirSincronoRetNFCe>(retorno);
                //Armazenar nsNRec
                if (!String.IsNullOrEmpty(retornoNFCe.nsNRec))
                    nfe.NsNrec = retornoNFCe.nsNRec;

                if (!String.IsNullOrEmpty(retornoNFCe.cStat))
                    codStatusRet = retornoNFCe.cStat;
                else
                    codStatusRet = retornoNFCe.statusEnvio;

                if (retornoNFCe.motivo.Contains("autorizada com sucesso") || retornoNFCe.motivo.Contains("autorizado") || retornoNFCe.motivo.Contains("Autorizado"))
                {
                    nfeStatus = new NfeStatus();
                    nfeStatus.Id = 1;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    armazenaXmlAutorizadoNoBanco();
                    //ATUALIZAR ESTOQUE
                    if (chkMovimentarEstoque.Checked == true)
                    {
                        NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                        if (radioSaida.Checked == true)
                        {
                            IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                            foreach (NfeProduto nfeP in listaProd)
                            {
                                generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);
                                generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);

                            }
                        }
                        else
                        {
                            IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                            foreach (NfeProduto nfeP in listaProd)
                            {
                                generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);
                                generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);

                            }
                        }
                    }
                    GenericaDesktop.ShowInfo("Nota autorizada!");
                    //Grava em Nuvem
                    //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + " -" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.xml";
                    //string pastaDropbox = @"XML\NFe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                    //string arquivo = nfe.Chave + "-procNFe.xml";
                    //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                    //t.Wait();

                    if (File.Exists(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf"))
                    {
                        FrmPDF frmPDF = new FrmPDF(caminhoSalvarXml + nfe.Chave + "-procNFe.pdf");
                        frmPDF.ShowDialog();
                    }
                }
                //Erro interno ao processar a requisicao // geralmente falha sefaz, aguardamos 5 segundos e verificamos novo retorno
                else if (retornoNFCe.motivo.Contains("Documento") || retornoNFCe.motivo.Contains("Sefaz") || retornoNFCe.motivo.Contains("sefaz") || retornoNFCe.motivo.Contains("Erro interno ao processar a requisicao"))
                {
                    Thread.Sleep(5000);

                    //CONSULTA NSNREC
                    ConsStatusProcessamentoReq consStatusProcessamentoReq = new ConsStatusProcessamentoReq();
                    consStatusProcessamentoReq.CNPJ = Sessao.empresaFilialLogada.Cnpj;
                    consStatusProcessamentoReq.nsNRec = nfe.NsNrec;
                    if (Sessao.parametroSistema.AmbienteProducao == true)
                        consStatusProcessamentoReq.tpAmb = "1";
                    else
                        consStatusProcessamentoReq.tpAmb = "2";


                    String retornoConsulta = NSSuite.consultarStatusProcessamento(nfe.Modelo, consStatusProcessamentoReq);
                    if (retornoConsulta != null)
                        retConsulta = JsonConvert.DeserializeObject<RetConsultaProcessamentoNF>(retornoConsulta);

                    if (retConsulta.xMotivo != null)
                    {
                        if (retConsulta.xMotivo.Contains("Autorizado o uso"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 1;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                            }
                            Controller.getInstance().salvar(nfe);
                            armazenaXmlAutorizadoNoBanco();
                            generica.gravarXMLNaPasta(retConsulta.xml, retConsulta.chNFe, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-procNFe.xml");

                            //ATUALIZAR ESTOQUE
                            if (chkMovimentarEstoque.Checked == true)
                            {
                                NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                                if (radioSaida.Checked == true)
                                {
                                    IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                                    foreach (NfeProduto nfeP in listaProd)
                                    {
                                        generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);
                                        generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), false, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);

                                    }
                                }
                                else
                                {
                                    IList<NfeProduto> listaProd = nfeProdutoDAO.selecionarProdutosPorNfe(nfe.Id);
                                    foreach (NfeProduto nfeP in listaProd)
                                    {
                                        generica.atualizarEstoqueNaoConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);
                                        generica.atualizarEstoqueConciliado(nfeP.Produto, double.Parse(nfeP.QCom), true, "NFE " + nfe.Id.ToString(), "NFE: " + nfe.NNf + " MOD: " + nfe.Modelo + " - " + txtNaturezaOperacao.Texts, nfe.Cliente, DateTime.Now, null);

                                    }
                                }
                            }

                            //string origem = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + " -" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\"+ nfe.Chave + "-procNFe.xml";
                            //string pastaDropbox = @"XML\NFe\" + nfe.DataEmissao.Year + " - " + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\";
                            //string arquivo = nfe.Chave + "-procNFe.xml";
                            //var t = Task.Run(() => DropboxComandos.uploadArquivo(origem, pastaDropbox, arquivo));
                            //t.Wait();
                            if (File.Exists(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf"))
                            { 
                                //Process.Start(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                FrmPDF frmPDF = new FrmPDF(@"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-procNFe.pdf");
                                frmPDF.ShowDialog();
                            }
                               
                            else
                            {
                                NFeDownloadProc55 nFeDownloadProc55 = new NFeDownloadProc55();
                                nFeDownloadProc55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, nfe.Chave);
                                if (nFeDownloadProc55 != null)
                                {
                                    GenericaDesktop gen = new GenericaDesktop();
                                    gen.gerarPDF3(nfe, nFeDownloadProc55.pdf, nfe.Chave, true);
                                }
                            }
                            if (!String.IsNullOrEmpty(retConsulta.xMotivo))
                                GenericaDesktop.ShowInfo(" (" + retConsulta.cStat + ") > " + retConsulta.xMotivo);
                        }
                        else if (retConsulta.xMotivo.Contains("Sem retorno de status da sefaz"))
                        {
                            NfeStatus nfeStatus1 = new NfeStatus();
                            nfeStatus1.Id = 2;
                            nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                            nfe.Status = retConsulta.xMotivo;
                            nfe.CodStatus = retConsulta.cStat;
                            if (!String.IsNullOrEmpty(retConsulta.chNFe))
                            {
                                //nfe.Protocolo = retConsulta.nProt;
                                nfe.Chave = retConsulta.chNFe;
                                Controller.getInstance().salvar(nfe);
                                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retConsulta.cStat + " " + retConsulta.xMotivo + ", na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                            }
                        }
                    }
                }
                //se a nota continua nao autorizada, verifica se teve erros
                if (String.IsNullOrEmpty(nfe.Chave) || nfe.Chave.Equals("123"))
                {
                    String erros = "";
                    if (retornoNFCe.erros != null)
                    {
                        for (int xx = 0; xx < retornoNFCe.erros.Count; xx++)
                        {
                            erros = erros + " " + retornoNFCe.erros[xx];
                        }
                    }
                    NfeStatus nfeStatus1 = new NfeStatus();
                    nfeStatus1.Id = 2;
                    nfe.NfeStatus = (NfeStatus)NfeStatusController.getInstance().selecionar(nfeStatus1);
                    nfe.Status = retornoNFCe.motivo;
                    nfe.CodStatus = retornoNFCe.cStat;
                    if (!String.IsNullOrEmpty(retornoNFCe.chNFe))
                    {
                        nfe.Protocolo = retornoNFCe.nProt;
                        nfe.Chave = retornoNFCe.chNFe;
                    }
                    if (!String.IsNullOrEmpty(retConsulta.chNFe))
                    {
                        //nfe.Protocolo = retConsulta.nProt;
                        nfe.Chave = retConsulta.chNFe;
                    }
                    Controller.getInstance().salvar(nfe);
                    GenericaDesktop.ShowAlerta("Erro na Nota Fiscal, possívelmente falha na sefaz, tente reenviar a nota posteriormente: (" + retConsulta.cStat + " " + retConsulta.xMotivo + ") na tela de gerenciamento de notas você poderá reenviar a nota para sefaz!");
                    if(!String.IsNullOrEmpty(erros))
                        GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: " + retornoNFCe.cStat + " " + retornoNFCe.motivo + "\n\n" + erros);
                }
                this.Close();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Erro na Nota Fiscal: Verifique sua conexão com a internet, após normalizar acesse o menu de gerenciamento de notas para reenviar a mesma!");

            }
        }

        private void armazenaXmlAutorizadoNoBanco()
        {
            NFeDownloadProc55 nota55 = new NFeDownloadProc55();
            nota55 = generica.ConsultaNFeEmitida(Sessao.empresaFilialLogada.Cnpj, this.nfe.Chave);

            if (nota55.motivo.Contains("SUCESSO") || nota55.motivo.Contains("sucesso") || nota55.motivo.Contains("Sucesso"))
            {
                Genericos genericosNF = new Genericos();
                var notaLida55 = Genericos.LoadFromXMLString<TNfeProc>(nota55.xml);
                string es = "S";
                if (radioEntrada.Checked == true)
                    es = "E";
                genericosNF.gravarXMLNoBanco(notaLida55, 0, es, this.nfe.Id);
                //Ftp ftp = new Ftp();
                //string caminho = "/www/LunarERP/Fiscal/" + Sessao.empresaFilialLogada.Cnpj + "/XML/NFCe/Autorizadas/";
                //string caminhoArquivoLocal = @"C:\Desenvolvimento\Lunar\Lunar\Lunar\bin\Debug\Emissao\" + vendaConclusao.Nfe.Chave + "-procNFCe.xml";
                //ftp.enviarArquivoFtp(caminho, caminhoArquivoLocal);
            }          
        }

        private void gravarXMLNaPasta(string xml, string numeroNFe, string caminhoArmazenamento, string nomeArquivo)
        {
            if (!nomeArquivo.Contains(".xml"))
                nomeArquivo = nomeArquivo + ".xml";
            if (!Directory.Exists(caminhoArmazenamento))
            {
                Directory.CreateDirectory(caminhoArmazenamento);
            }
            string arquivo = caminhoArmazenamento + nomeArquivo;
            if (!File.Exists(arquivo))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
            else
            {
                File.Delete(arquivo);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false);
                using (XmlWriter writer = XmlWriter.Create(arquivo, settings))
                {
                    doc.Save(writer);
                    writer.Close();
                }
            }
        }

        private bool validarClienteNFCe(Pessoa pessoa)
        {

            bool validacao = false;
            if (String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                GenericaDesktop.ShowAlerta("Para NFe é obrigatório selecionar um cliente/Fornecedor");
                validacao = false;
            }
            if (pessoa.Cnpj.Length == 11)
            {
                validacao = true;
            }
            else if (pessoa.Cnpj.Length < 11)
            {
                GenericaDesktop.ShowAlerta("Para NFe o cliente selecionado deve ter CPF preenchido corretamente");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            else if (pessoa.Cnpj.Length == 14)
            {
                GenericaDesktop.ShowAlerta("Em uma NFe o cliente não pode ser pessoa jurídica, caso precise identificar a pessoa jurídica faça a emissão de uma NFe modelo 55");
                validacao = false;
            }
            if (!String.IsNullOrEmpty(pessoa.RazaoSocial))
                validacao = true;
            if (pessoa.EnderecoPrincipal == null)
            {
                GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo");
                validacao = false;
                abrirTelaEditarCliente(pessoa);
            }
            if (pessoa.EnderecoPrincipal != null)
            {
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Logradouro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NOME DA RUA)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Bairro))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (BAIRRO)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Numero))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (NUMERO)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
                if (String.IsNullOrEmpty(pessoa.EnderecoPrincipal.Cep))
                {
                    GenericaDesktop.ShowAlerta("O endereço do cliente deve ser preenchido completo (CEP)");
                    validacao = false;
                    abrirTelaEditarCliente(pessoa);
                }
            }
            if(naturezaOperacaoSelecionado == null)
            {
                GenericaDesktop.ShowAlerta("Selecione a Natureza da Operação");
                validacao = false;
            }
            else if(naturezaOperacaoSelecionado != null)
            {
                if(naturezaOperacaoSelecionado.Id == 0)
                {
                    GenericaDesktop.ShowAlerta("Selecione a Natureza da Operação");
                    validacao = false;
                }
                if(naturezaOperacaoSelecionado.FinalidadeNfe == null)
                {
                    GenericaDesktop.ShowAlerta("Natureza de operação sem Finalidade de NFe preenchida, ajuste a Natureza da Operação");
                    validacao = false;
                }
            }
            return validacao;
        }

        private void abrirTelaEditarCliente(Pessoa cliente)
        {
            PessoaController pessoaController = new PessoaController();
            FrmClienteCadastro frm = new FrmClienteCadastro(cliente);
            frm.ShowDialog();

            //if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            //{
            //    Pessoa pessoaCliente = new Pessoa();
            //    pessoaCliente.Id = int.Parse(txtCodCliente.Texts);
            //    pessoaCliente = (Pessoa)pessoaController.selecionar(pessoaCliente);
            //    if (pessoaCliente.EnderecoPrincipal != null)
            //    {
            //        pessoaCliente.EnderecoCliente = venda.Cliente.EnderecoPrincipal.Logradouro + ", " + venda.Cliente.EnderecoPrincipal.Numero + " - " + venda.Cliente.EnderecoPrincipal.Complemento;
            //    }
            //}
        }
        private void btnPesquisaTransportadora_Click(object sender, EventArgs e)
        {
            pesquisaTransportadora();
        }

        private void txtTransportadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                pesquisaTransportadora();
        }

        private void txtCodTransportadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(txtCodTransportadora.Texts))
                {
                    Pessoa transportadora = new Pessoa();
                    transportadora.Id = int.Parse(txtCodTransportadora.Texts);
                    transportadora = (Pessoa)Controller.getInstance().selecionar(transportadora);
                    if(transportadora.Id > 0)
                    {
                        txtTransportadora.Texts = transportadora.RazaoSocial;
                    }
                }
            }
        }

        private void txtChaveReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                if(txtChaveReferencia.Texts.Length == 44)
                {
                    inserirNotaReferenciada(null);
                }
                else
                {
                    pesquisarChaveReferencia();
                }
            }
        }

        private void pesquisarChaveReferencia()
        {
            Object nfeObjeto = new Nfe();
            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("Nfe", "and Tabela.Chave like '%" + txtChaveReferencia.Texts + "%' and Tabela.NfeStatus = 1"))
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
                    switch (uu.showModal("Nfe", "", ref nfeObjeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            formBackground.Dispose();
                            break;
                        case DialogResult.OK:
                            //txtChaveReferencia.Texts = ((Nfe)nfeObjeto).Chave;
                            inserirNotaReferenciada((Nfe)nfeObjeto);
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
    
        private void inserirNotaReferenciada(Nfe nfeSelecionada)
        {
            if (nfeSelecionada != null)
            {
                System.Data.DataRow row = dsReferencia.Tables[0].NewRow();
                row.SetField("Chave", nfeSelecionada.Chave);
                row.SetField("DataEmissao", nfeSelecionada.DataEmissao.ToShortDateString());
                row.SetField("Valor", nfeSelecionada.VNf.ToString("N2"));
                dsReferencia.Tables[0].Rows.Add(row);

                if(!txtInformacoesAdicionais.Texts.Contains("NFe Ref. " + nfeSelecionada.NNf))
                    txtInformacoesAdicionais.Texts = txtInformacoesAdicionais.Texts + " " + "NFe Ref. " + nfeSelecionada.NNf;
            }
            else
            {
                if (txtChaveReferencia.Texts.Trim().Length == 44)
                {
                    NfeController nfeController = new NfeController();
                    Nfe nf = new Nfe();
                    nf = nfeController.selecionarNotaPorChave(txtChaveReferencia.Texts.Trim());
                    if (nf is null)
                    {
                        if (GenericaDesktop.ShowConfirmacao("Nota não encontrada nesse sistema, caso seja uma nota antiga lançada em outro sistema clique em sim para continuar."))
                        {
                            System.Data.DataRow row = dsReferencia.Tables[0].NewRow();
                            row.SetField("Chave", txtChaveReferencia.Texts.Trim());
                            row.SetField("DataEmissao", "");
                            row.SetField("Valor", "");
                            dsReferencia.Tables[0].Rows.Add(row);

                            txtInformacoesAdicionais.Texts = txtInformacoesAdicionais.Texts + " " + "NFe Ref. " + txtChaveReferencia.Texts.Trim().Substring(25, 9);
                        }
                    }
                    else
                    {
                        nfeSelecionada = nf;
                        System.Data.DataRow row = dsReferencia.Tables[0].NewRow();
                        row.SetField("Chave", nfeSelecionada.Chave);
                        row.SetField("DataEmissao", nfeSelecionada.DataEmissao.ToShortDateString());
                        row.SetField("Valor", nfeSelecionada.VNf.ToString("N2"));
                        dsReferencia.Tables[0].Rows.Add(row);

                        txtInformacoesAdicionais.Texts = txtInformacoesAdicionais.Texts + " " + "NFe Ref. " + nfeSelecionada.NNf;
                    }
                }
                else
                    GenericaDesktop.ShowAlerta("Chave de nota fiscal eletrônica deve conter 44 dígitos, você digitou " + txtChaveReferencia.Texts.Trim().Length + " dígitos!");

            }
            txtChaveReferencia.Texts = "";
            txtChaveReferencia.Focus();
            if(gridNotaReferenciada.RowCount > 0)
            {
                gridNotaReferenciada.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCells;
                gridNotaReferenciada.AutoSizeController.ResetAutoSizeWidthForAllColumns();
                gridNotaReferenciada.AutoSizeController.Refresh();
            }
        }

        private void btnConfirmarReferencia_Click(object sender, EventArgs e)
        {
            inserirNotaReferenciada(null);
        }

        private void btnPesquisaReferencia_Click(object sender, EventArgs e)
        {
            pesquisarChaveReferencia();
        }

        private void btnPreVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                bool validaCliente = false;
                //enviarNFCe();
                if (!String.IsNullOrEmpty(txtCodCliente.Texts))
                {
                    Pessoa cli = new Pessoa();
                    cli.Id = int.Parse(txtCodCliente.Texts);
                    cli = (Pessoa)Controller.getInstance().selecionar(cli);
                    validaCliente = validarClienteNFCe(cli);

                    ValidadorNotaSaida validador = new ValidadorNotaSaida();
                    if (validaCliente == true)
                    {
                        EmitirNfe2 emitirNFe = new EmitirNfe2();
                        carregarListaProdutos();
                        if (validador.validarProdutosNota(listaProdutosNFe))
                        {
                            if (lblNumeroNfe.Text == "0")
                                numeroNFe = Sessao.parametroSistema.ProximoNumeroNFe;
                            else
                                numeroNFe = lblNumeroNfe.Text;
                            TNFeInfNFeTranspModFrete frete = retornaFrete();
                            TNFeInfNFeIdeIndPres presenca = retornaPresencaCliente();
                            TNFeInfNFeIdeNFref[] tNFeInfNFeIdeNFref = new TNFeInfNFeIdeNFref[gridNotaReferenciada.RowCount];
                           
                            Frete fr = new Frete();
                            if (!frete.ToString().Equals("Item9"))
                            {
                                Pessoa pessoaTransporte = new Pessoa();
                                if (!String.IsNullOrEmpty(txtCodTransportadora.Texts))
                                {
                                    pessoaTransporte.Id = int.Parse(txtCodTransportadora.Texts);
                                    pessoaTransporte = (Pessoa)Controller.getInstance().selecionar(pessoaTransporte);
                                    if (pessoaTransporte != null)
                                    {
                                        fr.transportadora = pessoaTransporte;
                                    }
                                }
                                else
                                    fr.transportadora = null;

                                fr.codigoAntt = txtCodigoAntt.Texts.Trim();
                                fr.especie = txtEspecie.Texts.Trim();
                                fr.marca = txtMarca.Texts.Trim();
                                fr.pesoBruto = txtPesoBruto.Texts.Trim();
                                fr.pesoLiquido = txtPesoLiquido.Texts.Trim();
                                fr.placa = txtPlacaVeiculo.Texts.Trim();
                                fr.quantidadeVolume = txtQtdVolume.Texts.Trim();
                            }

                            decimal fret = 0;
                            decimal segur = 0;
                            decimal outr = 0;
                            if (!String.IsNullOrEmpty(txtFrete.Texts))
                                fret = decimal.Parse(txtFrete.Texts);
                            if (!String.IsNullOrEmpty(txtSeguro.Texts))
                                segur = decimal.Parse(txtSeguro.Texts);
                            if (!String.IsNullOrEmpty(txtOutrasDepesas.Texts))
                                outr = decimal.Parse(txtOutrasDepesas.Texts);
                            TNFeInfNFeIdeTpNF tpNF = new TNFeInfNFeIdeTpNF();
                            if (radioEntrada.Checked == true)
                                tpNF = TNFeInfNFeIdeTpNF.Item0;
                            else
                                tpNF = TNFeInfNFeIdeTpNF.Item1;
                            nfe = alimentaNfe();

                            NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                            NfeReferenciaDAO nfeReferenciaDAO = new NfeReferenciaDAO();
                            nfeProdutoDAO.excluirProdutosNfeParaAtualizar(nfe.Id.ToString());
                            nfeReferenciaDAO.excluirReferenciaNfeParaAtualizar(nfe.Id.ToString());
                            
                            tNFeInfNFeIdeNFref = retornarNotasReferenciadas();

                            string xmlStrEnvio = emitirNFe.gerarXMLNfe(decimal.Parse(txtTotalProduto.Texts.Replace("R$ ", "")), decimal.Parse(txtTotalNota.Texts.Replace("R$ ", "")), decimal.Parse(txtDesconto.Texts.Replace("R$ ", "")), numeroNFe, listaProdutosNFe, cli, null, false, naturezaOperacaoSelecionado.Descricao, frete, presenca, tNFeInfNFeIdeNFref, fret, segur, outr, tpNF, fr, naturezaOperacaoSelecionado, nfe);
                            if (!String.IsNullOrEmpty(xmlStrEnvio))
                            {
                                EmitirSincronoRetNFCe retornoNFCe = new EmitirSincronoRetNFCe();

                                String retorno = NSSuite.previaDocumento("55", "xml", xmlStrEnvio);
                                if (!retorno.Contains("motivo") || !retorno.Contains("Motivo"))
                                {
                                    if (!Directory.Exists(@"Fiscal\XML\NFe\Temp"))
                                        Directory.CreateDirectory(@"Fiscal\XML\NFe\Temp");
                                    if (!File.Exists(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf"))
                                    {
                                        byte[] bytes = Convert.FromBase64String(retorno);
                                        System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf", FileMode.CreateNew);
                                        System.IO.BinaryWriter writer =
                                            new BinaryWriter(stream);
                                        writer.Write(bytes, 0, bytes.Length);
                                        writer.Close();
                                        Process.Start(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf");
                                    }
                                    else
                                    {
                                        File.Delete(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf");
                                        byte[] bytes = Convert.FromBase64String(retorno);
                                        System.IO.FileStream stream = new FileStream(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf", FileMode.CreateNew);
                                        System.IO.BinaryWriter writer =
                                            new BinaryWriter(stream);
                                        writer.Write(bytes, 0, bytes.Length);
                                        writer.Close();
                                        Process.Start(@"Fiscal\XML\NFe\Temp\" + numeroNFe + "-preNFe.pdf");
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Cliente inválido!");
                }
                else
                    GenericaDesktop.ShowAlerta("Cliente inválido!");
            }
            catch (Exception erro) 
            {
                GenericaDesktop.ShowErro(erro.Message);
            }
        }

        public class Frete
        {
            public Pessoa transportadora { get; set; }
            public string codigoAntt { get; set; }
            public string quantidadeVolume { get; set; }
            public string especie { get; set; }
            public string marca { get; set; }
            public string placa { get; set; }
            public string pesoBruto { get; set; }
            public string pesoLiquido { get; set; }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            bool validaCliente = false;
            //enviarNFCe();
            if (!String.IsNullOrEmpty(txtCodCliente.Texts))
            {
                Pessoa cli = new Pessoa();
                cli.Id = int.Parse(txtCodCliente.Texts);
                cli = (Pessoa)Controller.getInstance().selecionar(cli);
                validaCliente = validarClienteNFCe(cli);

                ValidadorNotaSaida validador = new ValidadorNotaSaida();
                if (validaCliente == true)
                {
                    carregarListaProdutos();
                    EmitirNfe2 emitirNFe2 = new EmitirNfe2();
                    if (validador.validarProdutosNota(listaProdutosNFe))
                    {
                        if (lblNumeroNfe.Text == "0")
                            numeroNFe = Sessao.parametroSistema.ProximoNumeroNFe;
                        else
                            numeroNFe = lblNumeroNfe.Text;
                        TNFeInfNFeTranspModFrete frete = retornaFrete();
                        TNFeInfNFeIdeIndPres presenca = retornaPresencaCliente();
                        TNFeInfNFeIdeNFref[] tNFeInfNFeIdeNFref = new TNFeInfNFeIdeNFref[gridNotaReferenciada.RowCount];
                        //tNFeInfNFeIdeNFref = retornarNotasReferenciadas();

                        Frete fr = new Frete();
                        if (!frete.ToString().Equals("Item9"))
                        {
                            Pessoa pessoaTransporte = new Pessoa();
                            if (!String.IsNullOrEmpty(txtCodTransportadora.Texts))
                            {
                                pessoaTransporte.Id = int.Parse(txtCodTransportadora.Texts);
                                pessoaTransporte = (Pessoa)Controller.getInstance().selecionar(pessoaTransporte);
                                if (pessoaTransporte != null)
                                {
                                    fr.transportadora = pessoaTransporte;
                                }
                            }
                            else
                                fr.transportadora = null;

                            fr.codigoAntt = txtCodigoAntt.Texts.Trim();
                            fr.especie = txtEspecie.Texts.Trim();
                            fr.marca = txtMarca.Texts.Trim();
                            fr.pesoBruto = txtPesoBruto.Texts.Trim();
                            fr.pesoLiquido = txtPesoLiquido.Texts.Trim();
                            fr.placa = txtPlacaVeiculo.Texts.Trim();
                            fr.quantidadeVolume = txtQtdVolume.Texts.Trim();
                        }

                        decimal fret = 0;
                        decimal segur = 0;
                        decimal outr = 0;

                        if (!String.IsNullOrEmpty(txtFrete.Texts))
                            fret = decimal.Parse(txtFrete.Texts);
                        if (!String.IsNullOrEmpty(txtSeguro.Texts))
                            segur = decimal.Parse(txtSeguro.Texts);
                        if (!String.IsNullOrEmpty(txtOutrasDepesas.Texts))
                            outr = decimal.Parse(txtOutrasDepesas.Texts);
                        TNFeInfNFeIdeTpNF tpNF = new TNFeInfNFeIdeTpNF();
                        if (radioEntrada.Checked == true)
                            tpNF = TNFeInfNFeIdeTpNF.Item0;
                        else
                            tpNF = TNFeInfNFeIdeTpNF.Item1;

                        nfe = alimentaNfe();

                        NfeProdutoDAO nfeProdutoDAO = new NfeProdutoDAO();
                        NfeReferenciaDAO nfeReferenciaDAO = new NfeReferenciaDAO();
                        nfeProdutoDAO.excluirProdutosNfeParaAtualizar(nfe.Id.ToString());
                        nfeReferenciaDAO.excluirReferenciaNfeParaAtualizar(nfe.Id.ToString());

                        tNFeInfNFeIdeNFref = retornarNotasReferenciadas();

                        carregarListaProdutosESalvar();
                        Sessao.parametroSistema.ProximoNumeroNFe = (int.Parse(numeroNFe) + 1).ToString();
                        Controller.getInstance().salvar(Sessao.parametroSistema);
                        GenericaDesktop.ShowInfo("Gravado com Sucesso!");
                        this.Close();
                    }
                }
            }
        }

        private void btnRemoverReferencia_Click(object sender, EventArgs e)
        {
            if (gridNotaReferenciada.SelectedIndex >= 0)
            {
                var selectedItem = this.gridNotaReferenciada.CurrentItem as DataRowView;
                var dataRow = (selectedItem as DataRowView).Row;
                dataRow.Delete();
            }
            else
            {
                GenericaDesktop.ShowAlerta("Primeiro você deve clicar na linha da nota referenciada que deseja remover!");
            }
        }
    } 
}

