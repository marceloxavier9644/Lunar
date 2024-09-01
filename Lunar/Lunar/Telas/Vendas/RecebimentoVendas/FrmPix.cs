using Lunar.Telas.Cadastros.Bancos;
using Lunar.Telas.PesquisaPadrao;
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
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lunar.Utils.GalaxyPay_API.GalaxyPay_RetornoStatusBoletos;
using static Lunar.Utils.GalaxyPay_API.RetornoPagamentoPix;

namespace Lunar.Telas.Vendas.RecebimentoVendas
{
    public partial class FrmPix : Form
    {
        bool pagamentoConcluido = false;
        bool qrSucesso = false;
        GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
        String origem = "";
        String idOrigem = "";
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
                if (Sessao.parametroSistema.ContaBancariaVinculadaApi != null)
                {

                    txtCodContaBancaria.Texts = Sessao.parametroSistema.ContaBancariaVinculadaApi.Id.ToString();
                    txtContaBancaria.Texts = Sessao.parametroSistema.ContaBancariaVinculadaApi.Descricao;
                }
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
                if (Sessao.parametroSistema.ContaBancariaVinculadaApi != null)
                {
                    txtCodContaBancaria.Texts = Sessao.parametroSistema.ContaBancariaVinculadaApi.Id.ToString();
                    txtContaBancaria.Texts = Sessao.parametroSistema.ContaBancariaVinculadaApi.Descricao;
                }
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
                        contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts.ToString());
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
                        this.contaBancaria.Id = int.Parse(txtCodContaBancaria.Texts.ToString());
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

        private async Task gerarQRCODE()
        {
            qrSucesso = false;
            if (Sessao.parametroSistema.IntegracaoGalaxyPay == true)
            {
                // Crie uma instância do formulário de "Aguarde..."
                FrmAguarde2 aguardeForm = new FrmAguarde2();

                try
                {
                    aguardeForm.SetMensagemAguarde("Aguarde, Processando...");

                    // Exiba o formulário como um diálogo modal em uma nova thread
                    Thread thread = new Thread(() =>
                    {
                        aguardeForm.ShowDialog();
                    });

                    thread.Start();


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
                            {
                                //pessoa = null;
                                //faz um cadastro no nome da propria empresa
                                pessoa = new Pessoa();
                                pessoa.Cnpj = Sessao.empresaFilialLogada.Cnpj;
                                pessoa.RazaoSocial = Sessao.empresaFilialLogada.RazaoSocial;
                                pessoa.NomeFantasia = Sessao.empresaFilialLogada.NomeFantasia;
                                pessoa.Email = Sessao.empresaFilialLogada.Email;
                                pessoa.EnderecoPrincipal = Sessao.empresaFilialLogada.Endereco;
                            }
                        }

                        if (pessoa != null)
                        {
                            if (!String.IsNullOrEmpty(pessoa.Cnpj))
                            {
                                // Inicie a tarefa assíncrona
                                string linkGerado = await galaxyPayApiIntegracao.GalaxyPay_GerarPix(origem, idOrigem, valorFaltante, pessoa);

                                // Aguarde a conclusão da tarefa assíncrona
                                if (!String.IsNullOrEmpty(linkGerado))
                                {
                                    qrSucesso = true;
                                    picQRCode.Image = GerarQRCode(190, 190, linkGerado);
                                    txtCodigoQrCode.Texts = linkGerado;
                                    txtCodigoQrCode.Visible = true;
                                    btnCopiaQr.Visible = true;
                                    btnImprimirQr.Visible = true;
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
                        aguardeForm.BeginInvoke(new Action(() =>
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

        private async void btnPixQR_Click(object sender, EventArgs e)
        {
            txtData.Enabled = false;
            txtData.Value = DateTime.Now;
            await VerificarPagamentoPixAsync();
            if (pagamentoConcluido == true)
            {
                btnConfirmar.Enabled = true;
                btnConfirmar.PerformClick();                
            }
        }

        private async Task VerificarPagamentoPixAsync()
        {
            origem = "";
            idOrigem = "";
            txtValor.Enabled = false;
            btnConfirmar.Enabled = false;

            // Gere o QR code para pagamento Pix
            await gerarQRCODE();

            if (qrSucesso == true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));

                pagamentoConcluido = false;

                while (!pagamentoConcluido)
                {
                    // Espere um curto período antes de iniciar a verificação
                    // Verifique o status da transação Pix
                    GalaxPayRetornoPix galaxyPay_RetornoStatus = await galaxyPayApiIntegracao.GalaxyPay_ListarRetornoTransacoesPixAsync(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), origem + "_" + idOrigem);

                    // Aqui você pode verificar o status da transação e tomar medidas com base nele
                    // Por exemplo, se o status for "payedPix", o pagamento foi concluído com sucesso
                    // Você pode definir a lógica apropriada para processar o pagamento aqui

                    if (galaxyPay_RetornoStatus != null)
                    {
                        if (galaxyPay_RetornoStatus.totalQtdFoundInPage > 0)
                        {
                            foreach (var transaction in galaxyPay_RetornoStatus.Transactions)
                            {
                                if (transaction.status == "payedPix")
                                {
                                    // O pagamento Pix foi concluído
                                    GenericaDesktop.ShowInfo("Pagamento Confirmado!");
                                    pagamentoConcluido = true;
                                    break; // Saia do loop
                                }
                            }
                        }
                    }

                    if (!pagamentoConcluido)
                    {
                        // Aguarde um curto período antes de verificar novamente
                        await Task.Delay(TimeSpan.FromSeconds(4));
                    }
                }
            }

            // O pagamento Pix foi concluído, você pode executar a lógica adicional aqui
        }

        private void btnCopiaQr_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCodigoQrCode.Texts);
        }

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Object objeto = new ContaBancaria();

            Form formBackground = new Form();
            try
            {
                using (FrmPesquisaPadrao uu = new FrmPesquisaPadrao("ContaBancaria", ""))
                {
                    txtCodContaBancaria.Texts = "";
                    txtContaBancaria.Texts = "";
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
                    switch (uu.showModal("PlanoConta", "", ref objeto))
                    {
                        case DialogResult.Ignore:
                            uu.Dispose();
                            FrmContaBancaria form = new FrmContaBancaria();
                            if (form.showModal(ref objeto) == DialogResult.OK)
                            {
                                txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                                txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
                            }
                            form.Dispose();
                            break;
                        case DialogResult.OK:
                            txtContaBancaria.Texts = ((ContaBancaria)objeto).Descricao;
                            txtCodContaBancaria.Texts = ((ContaBancaria)objeto).Id.ToString();
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

        private void PrintQRCode(string valor)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                // Defina o tamanho da página de acordo com a largura da bobina térmica
                int pageWidth = e.PageBounds.Width;
                int pageHeight = e.PageBounds.Height;

                // Desenhar o QR Code a partir do PictureBox
                Image qrImage = picQRCode.Image;
                if (qrImage != null)
                {
                    // Define a posição e o tamanho do QR Code na página
                    int qrCodeX = 50;
                    int qrCodeY = 100; // Ajuste a posição conforme necessário

                    // Desenhar o QR Code na página
                    e.Graphics.DrawImage(qrImage, new Point(qrCodeX, qrCodeY)); // Ajuste a posição conforme necessário

                    // Defina a fonte para o texto do nome da loja
                    Font fontLoja = new Font("Arial", 14, FontStyle.Bold);
                    SolidBrush brushLoja = new SolidBrush(Color.Black);

                    // Calcule o tamanho do texto da loja
                    SizeF lojaTextSize = e.Graphics.MeasureString(Sessao.empresaFilialLogada.NomeFantasia, fontLoja);

                    // Calcule a posição do texto da loja acima do QR Code
                    float lojaTextX = (pageWidth - lojaTextSize.Width) / 2;
                    float lojaTextY = qrCodeY - lojaTextSize.Height - 10; // Ajuste a posição conforme necessário

                    // Defina a fonte para o texto do valor
                    Font fontValor = new Font("Arial", 16);
                    SolidBrush brushValor = new SolidBrush(Color.Black);

                    // Calcule o tamanho do texto
                    SizeF valorTextSize = e.Graphics.MeasureString(valor, fontValor);

                    // Calcule a posição do texto do valor centralizado abaixo do QR Code
                    float valorTextX = (pageWidth - valorTextSize.Width) / 2;
                    float valorTextY = qrCodeY + qrImage.Height + 10; // Ajuste a posição conforme necessário

                    // Desenhar o texto da loja
                    e.Graphics.DrawString(Sessao.empresaFilialLogada.NomeFantasia, fontLoja, brushLoja, new PointF(lojaTextX, lojaTextY));

                    // Desenhar o texto do valor
                    e.Graphics.DrawString(valor, fontValor, brushValor, new PointF(valorTextX, valorTextY));
                }
            };

            // Exibir a caixa de diálogo de impressão
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Imprimir diretamente sem mostrar a visualização
                printDocument.Print();
            }
        }

        private void btnImprimirQr_Click(object sender, EventArgs e)
        {
            string valor = txtValor.Texts; 
            PrintQRCode(valor);
        }
    }
}
