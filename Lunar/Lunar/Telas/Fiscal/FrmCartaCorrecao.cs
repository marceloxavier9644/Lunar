using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Lunar.Utils.OrganizacaoNF.RetCartaCorrecao;
using static LunarBase.Utilidades.ClasseRetornoJson.CancelamentoNFCe;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmCartaCorrecao : Form
    {
        Nfe nfe = new Nfe();
        NfeCce nfeCce = new NfeCce();
        GenericaDesktop generica = new GenericaDesktop();
        public DialogResult showModal(ref NfeCce nfeCce)
        {
            DialogResult = ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                nfeCce = this.nfeCce;
            }
            return DialogResult;
        }
        public FrmCartaCorrecao(Nfe nfe)
        {
            InitializeComponent();
            this.nfe = nfe;
        }

        private void enviarCCe()
        {
            NfeCceController nfeCceController = new NfeCceController();
            IList<NfeCce> listaCCe = nfeCceController.selecionarCartaCorrecaoPorNfe(nfe.Id);
            int sequencia = 1;
            if (listaCCe.Count > 0)
                sequencia = listaCCe.Count + 1;

            RetornoCartaoCorrecao ret = generica.ns_GerarCartaoCorrecao(nfe, txtCorrecao.Texts.Trim(), sequencia);
            if (ret != null)
            {
                if (ret.status == 200)
                {
                    nfeCce.Correcao = txtCorrecao.Texts.Trim();
                    nfeCce.Nfe = nfe;
                    nfeCce.Sequencia = sequencia;
                    nfeCce.Protocolo = ret.retEvento.nProt;
                    Controller.getInstance().salvar(nfeCce);

                    //Informa na nfe que possui carta correcao
                    if (nfe.Id > 0)
                    {
                        nfe.PossuiCartaCorrecao = true;
                        Controller.getInstance().salvar(nfe);
                    }

                    RetornoCancelamento retornoDownload = generica.ns_DownloadEventoCanceladoOuCCE55(nfe, false, true, sequencia.ToString());
                    if (ret.retEvento != null)
                        GenericaDesktop.ShowInfo(ret.retEvento.xMotivo);

                    if (retornoDownload != null)
                    {
                        generica.gerarPDF2(retornoDownload.pdf, nfe.Chave, true, @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\", nfe.Chave + "-CCe");
                        //EnviaXML PAINEL LUNAR 
                        string caminhoPDF = @"Fiscal\XML\NFe\" + nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0') + @"\Autorizadas\" + nfe.Chave + "-CCe.pdf";
                        LunarApiNotas lunarApiNotas = new LunarApiNotas();
                        byte[] arquivo;
                        using (var stream = new FileStream(caminhoPDF, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new BinaryReader(stream))
                            {
                                arquivo = reader.ReadBytes((int)stream.Length);
                                var retor = lunarApiNotas.EnvioArquivoParaNuvem_PDF(nfe.CnpjEmitente, nfe.Chave, "NFE", "CARTAS DE CORRECAO", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                            }
                        }
                    }
                    else
                        GenericaDesktop.ShowAlerta("Falha ao baixar PDF/XML da carta de correção");
      

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    if (ret.retEvento != null)
                        GenericaDesktop.ShowAlerta(ret.retEvento.xMotivo);
                    else if (ret.erro != null)
                        GenericaDesktop.ShowAlerta(ret.erro.cStat + " " + ret.erro.xMotivo);
                }
            }
            else
            {
                GenericaDesktop.ShowErro("Falha de comunicação com a Sefaz, tente novamente mais tarde!");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            enviarCCe();
        }

        private void FrmCartaCorrecao_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    btnImprimir.PerformClick();
                    break;
            }
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
