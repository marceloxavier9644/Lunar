using Lunar.Telas.Vendas.Adicionais;
using Lunar.Utils;
using Lunar.Utils.GalaxyPay_API;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmPix : Form
    {
        bool passou = false;
        decimal valorFaltante = 0;
        bool showModal = false;
        decimal valor = 0;
        VendaFormaPagamento vendaFormaPagamento = new VendaFormaPagamento();
        Venda venda = new Venda();
        ContaBancariaController ContaBancariaController = new ContaBancariaController();
        bool contaNulo = false;
        GenericaDesktop generica = new GenericaDesktop();
        private FrmAguarde _FormCarregando;
        IList<ContaReceber> listaReceber = new List<ContaReceber>();
        DateTime dataPix = new DateTime();
        ContaBancaria contaBancaria = new ContaBancaria();
        FormaPagamento fp = new FormaPagamento();
        OrdemServico ordemServico = new OrdemServico();
        public DialogResult showModalNovo(ref object vendaFormaPagamento)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                vendaFormaPagamento = this.vendaFormaPagamento;
            }
            return DialogResult;
        }

        public DialogResult showModalReceber(ref FormaPagamento formaPagamento, ref decimal valorRecebido, ref ContaBancaria contaBancaria, ref DateTime dataPix)
        {
            showModal = true;
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                formaPagamento = this.fp;
                valorRecebido = this.valor;
                contaBancaria = this.contaBancaria;
                dataPix = this.dataPix;
            }
            return DialogResult;
        }
        public FrmPix(decimal valorFaltante, Venda venda)
        {
            InitializeComponent();
            IList<ContaBancaria> lista = ContaBancariaController.selecionarTodasContas();
            if (lista.Count > 0)
            {
                foreach (ContaBancaria contaBancaria in lista)
                {
                    DataRow drs = dsConta.Tables[0].NewRow();
                    drs.SetField("ID", contaBancaria.Id);
                    drs.SetField("CONTA", contaBancaria.Descricao);
                    dsConta.Tables[0].Rows.Add(drs);
                }
                comboBanco.DisplayMember = "CONTA";
                comboBanco.ValueMember = "ID";
                comboBanco.DataSource = dsConta.Tables[0];
                comboBanco.SelectedIndex = 0;
            }
            else
            {
                contaNulo = true;
                GenericaDesktop.ShowAlerta("Para utilizar a opção PIX você deve cadastrar a conta bancária da empresa!");
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("N2");
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
            this.venda = venda;
        }

        public FrmPix(decimal valorFaltante, IList<ContaReceber> listaReceber, OrdemServico ordemServico)
        {
            InitializeComponent();
            this.listaReceber = listaReceber;
            this.ordemServico = ordemServico;
            IList<ContaBancaria> lista = ContaBancariaController.selecionarTodasContas();
            if (lista.Count > 0)
            {
                foreach (ContaBancaria contaBancaria in lista)
                {
                    DataRow drs = dsConta.Tables[0].NewRow();
                    drs.SetField("ID", contaBancaria.Id);
                    drs.SetField("CONTA", contaBancaria.Descricao);
                    dsConta.Tables[0].Rows.Add(drs);
                }
                comboBanco.DisplayMember = "CONTA";
                comboBanco.ValueMember = "ID";
                comboBanco.DataSource = dsConta.Tables[0];
                comboBanco.SelectedIndex = 0;
            }
            else
            {
                contaNulo = true;
                GenericaDesktop.ShowAlerta("Para utilizar a opção PIX você deve cadastrar a conta bancária da empresa!");
            }

            lblFaltante.Text = "Valor Faltante: " + valorFaltante.ToString("N2");
            this.valorFaltante = valorFaltante;
            txtValor.TextAlign = HorizontalAlignment.Center;
        }

        private void FrmPix_Paint(object sender, PaintEventArgs e)
        {
            if (contaNulo == true)
                this.Close();
            if (passou == false)
            {
                txtValor.Texts = string.Format("{0:0.00}", valorFaltante);
                txtValor.Focus();
                passou = true;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            vendaFormaPagamento = new VendaFormaPagamento();
            if (!String.IsNullOrEmpty(txtValor.Texts))
            {
                if (decimal.Parse(txtValor.Texts) <= valorFaltante)
                {
                    if (venda.Id > 0)
                    {
                        this.valor = decimal.Parse(txtValor.Texts);
                        FormaPagamento formaPagamento = new FormaPagamento();
                        formaPagamento.Id = 3;
                        vendaFormaPagamento.FormaPagamento = (FormaPagamento)Controller.getInstance().selecionar(formaPagamento);
                        vendaFormaPagamento.Parcelamento = 0;
                        vendaFormaPagamento.ValorRecebido = valor;

                        ContaBancaria contaBancaria = new ContaBancaria();
                        contaBancaria.Id = int.Parse(comboBanco.SelectedValue.ToString());
                        vendaFormaPagamento.ContaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);

                        vendaFormaPagamento.Cartao = false;
                        vendaFormaPagamento.AutorizacaoCartao = "";
                        vendaFormaPagamento.Venda = venda;
                        vendaFormaPagamento.TipoCartao = "";
                        vendaFormaPagamento.ParcelamentoFk = null;
                        Controller.getInstance().salvar(vendaFormaPagamento);

                        Caixa caixa = new Caixa();
                        caixa.Conciliado = false;
                        caixa.IdOrigem = venda.Id.ToString();
                        caixa.ContaBancaria = contaBancaria;
                        caixa.DataLancamento = DateTime.Parse(txtData.Value.ToString());
                        caixa.Descricao = "RECEBIMENTO VENDA EM PIX";
                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                        caixa.FormaPagamento = vendaFormaPagamento.FormaPagamento;
                        if (venda.PlanoConta != null)
                            caixa.PlanoConta = venda.PlanoConta;
                        else
                            caixa.PlanoConta = null;
                        caixa.TabelaOrigem = "VENDA";
                        caixa.Tipo = "E";
                        caixa.Usuario = Sessao.usuarioLogado;
                        caixa.Valor = vendaFormaPagamento.ValorRecebido;
                        Controller.getInstance().salvar(caixa);

                        this.DialogResult = DialogResult.OK;
                    }
                    //Recebendo Parcelas//PAGANDO
                    else
                    {
                        this.fp.Id = 3;
                        this.fp = (FormaPagamento)Controller.getInstance().selecionar(fp);
                        this.contaBancaria.Id = int.Parse(comboBanco.SelectedValue.ToString());
                        this.contaBancaria = (ContaBancaria)Controller.getInstance().selecionar(contaBancaria);
                        this.valor = decimal.Parse(txtValor.Texts);
                        this.dataPix = DateTime.Parse(txtData.Value.ToString());
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    GenericaDesktop.ShowAlerta("O valor recebido no PIX não pode ser maior que o valor faltante!");
                    txtValor.Select();
                }

            }
            else
            {
                GenericaDesktop.ShowErro("Informe o valor recebido!");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            txtValor.Texts = "0,00";
            this.Close();
        }

        private void FrmPix_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F3:
                    btnPixQR.PerformClick();
                    break;
                case Keys.F5:
                    btnConfirmar.PerformClick();
                    break;
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
           // generica.SoNumeroEVirgula(txtValor.Texts, e);
        }
        private string formatMoedaNf(decimal valor)
        {
            try
            {
                string valorFormatado = String.Format("{0:0.00}", valor);
                return valorFormatado.Replace(",", ".");
            }
            catch
            {
                return valor.ToString();
            }
        }
        //private async void gerarQRCODE()
        //{
        //    if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
        //    {
        //        //GenericaDesktop.ShowAlerta("Funcionalidade não configurada!");
        //        String origem = "";
        //        String idOrigem = "";
        //        Pessoa pessoa = new Pessoa();
        //        if (venda != null)
        //        {
        //            if (venda.Id > 0)
        //            {
        //                idOrigem = venda.Id.ToString();
        //                origem = "VENDA";
        //                if (venda.Cliente != null)
        //                    pessoa = venda.Cliente;
        //            }
        //        }
        //        if (ordemServico != null)
        //        {
        //            if (ordemServico.Id > 0)
        //            {
        //                idOrigem = ordemServico.Id.ToString();
        //                origem = "ORDEMSERVICO";
        //                if (ordemServico.Cliente != null)
        //                    pessoa = ordemServico.Cliente;
        //            }
        //        }
        //        if (pessoa != null)
        //        {
        //            if (pessoa.Id > 0)
        //            {

        //            }
        //            else
        //                pessoa = null;
        //        }
        //        if (pessoa != null)
        //        {
        //            if (!String.IsNullOrEmpty(pessoa.Cnpj))
        //            {
        //                GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
        //                Task<string> linkGerado = galaxyPayApiIntegracao.GalaxyPay_GerarPix(origem, idOrigem, valorFaltante, pessoa);
        //                    if (!String.IsNullOrEmpty(linkGerado.ToString()))
        //                    {
        //                        picQRCode.Image = GerarQRCode(190, 190, linkGerado.ToString());
        //                //        //Simulacao de recebimento
        //                //         verificaRecebimento(retorno, retornoToken["token"]);
        //                    }
        //            }
        //            else
        //                GenericaDesktop.ShowAlerta("O Cliente deve possuir CPF ou CNPJ cadastrado");
        //        }
        //        else
        //            GenericaDesktop.ShowAlerta("Para gerar o QR CODE o sistema bancário exige os dados do cliente, selecione ou cadastre o cliente!");
        //    }
        //    else
        //        GenericaDesktop.ShowAlerta("Funcionalidade não configurada!");
        //}

        private async void gerarQRCODE()
        {
            if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
            {
                // Crie uma instância do formulário de "Aguarde..."
                FrmAguarde2 aguardeForm = new FrmAguarde2();

                try
                {
                    aguardeForm.SetMensagemAguarde("Aguarde... Processando...");

                    // Exiba o formulário como um diálogo modal em uma nova thread
                    Thread thread = new Thread(() =>
                    {
                        aguardeForm.ShowDialog();
                    });

                    thread.Start();

                    String origem = "";
                    String idOrigem = "";
                    Pessoa pessoa = new Pessoa();

                    if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
                    {
                        if (venda != null)
                        {
                            if (venda.Id > 0)
                            {
                                idOrigem = venda.Id.ToString();
                                origem = "VENDA";
                                if (venda.Cliente != null)
                                    pessoa = venda.Cliente;
                            }
                        }
                        if (ordemServico != null)
                        {
                            if (ordemServico.Id > 0)
                            {
                                idOrigem = ordemServico.Id.ToString();
                                origem = "ORDEMSERVICO";
                                if (ordemServico.Cliente != null)
                                    pessoa = ordemServico.Cliente;
                            }
                        }
                        if (pessoa != null)
                        {
                            if (pessoa.Id > 0)
                            {

                            }
                            else
                                pessoa = null;
                        }

                        if (pessoa != null)
                        {
                            if (!String.IsNullOrEmpty(pessoa.Cnpj))
                            {
                                GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();

                                // Inicie a tarefa assíncrona
                                string linkGerado = await galaxyPayApiIntegracao.GalaxyPay_GerarPix(origem, idOrigem, valorFaltante, pessoa);

                                // Aguarde a conclusão da tarefa assíncrona
                                if (!String.IsNullOrEmpty(linkGerado))
                                {
                                    picQRCode.Image = GerarQRCode(190, 190, linkGerado);
                                    txtCodigoQrCode.Texts = linkGerado;
                                    txtCodigoQrCode.Visible = true;
                                    if (origem.Equals("VENDA"))
                                    {
                                        venda.QrCodePix = linkGerado;
                                        Controller.getInstance().salvar(venda);
                                    }
                                    else if (origem.Equals("ORDEMSERVICO"))
                                    {
                                        ordemServico.QrCodePix = linkGerado;
                                        Controller.getInstance().salvar(ordemServico);
                                    }
                                }
                            }
                            else
                            {
                                GenericaDesktop.ShowAlerta("O Cliente deve possuir CPF ou CNPJ cadastrado");
                            }
                        }
                    }
                    else
                    {

                        GenericaDesktop.ShowAlerta("Funcionalidade não configurada! Para gerar o QR CODE é necessário configurar a integração bancária, entre em contato com o Suporte Técnico!");
                    }
                }
                catch (Exception ex)
                {
                    // Lide com exceções, se houver alguma
                    GenericaDesktop.ShowErro(ex.Message);
                }
                finally
                {
                    // Feche o formulário de "Aguarde..." usando Invoke
                    aguardeForm.Invoke(new Action(() =>
                    {
                        aguardeForm.Close();
                    }));
                }
            }
            else
            {
                GenericaDesktop.ShowAlerta("Funcionalidade não configurada!");
            }
        }


        private async Task verificaRecebimento(string qrCode, string token)
        {
            await Task.Delay(5000);
            //Verificar retorno
            string resultPagamento = generica.anagu_SimularRecebimentoPix(qrCode, token);
            if (resultPagamento.Contains("Pagamento efetuado com sucesso"))
            {
                picQRCode.Image = Lunar.Properties.Resources.confirmacao;
                await Task.Delay(2000);
                btnConfirmar.PerformClick();
            }
        }

        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private void MostrarTelaCarregando()
        {
            //_FormCarregando = new FrmAguarde("5000");
            //_FormCarregando.TopMost = true;
            //_FormCarregando.ShowDialog();
        }
        private void btnPixQR_Click(object sender, EventArgs e)
        {
            gerarQRCODE();
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            try
            {
                if(decimal.Parse(txtValor.Texts.Replace("R$ ", "")) > valorFaltante)
                {
                    GenericaDesktop.ShowAlerta("O valor não pode ser maior que o valor faltante para a forma de pagamento PIX");
                    txtValor.Texts = valorFaltante.ToString("N2");
                }
            }
            catch
            {

            }
        }

        private void txtValor_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            generica.SoNumeroEVirgula(txtValor.Texts, e);
            if (e.KeyChar == 13)
                btnConfirmar.PerformClick();
        }
    }
}
