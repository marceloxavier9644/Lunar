using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lunar.Utils.OrganizacaoNF.RetInutilizacao;

namespace Lunar.Telas.Fiscal
{
    public partial class FrmInutilizar : Form
    {
        Nfe nfe = new Nfe();
        NfeController nfeController = new NfeController();
        GenericaDesktop generica = new GenericaDesktop();
        public FrmInutilizar()
        {
            InitializeComponent();
            txtSerie.Text = Sessao.parametroSistema.SerieNFCe;
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
            Inutilizar();
            this.Close();
        }

        private bool validarCampos()
        {
            // Verifica se todos os campos estão preenchidos
            if (string.IsNullOrEmpty(txtNumeroInicial.Text) ||
                string.IsNullOrEmpty(txtNumeroFinal.Text) ||
                string.IsNullOrEmpty(txtSerie.Text) ||
                string.IsNullOrEmpty(txtJustificativa.Text))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos.");
                return false;
            }
            if (txtJustificativa.Text.Length < 15)
            {
                GenericaDesktop.ShowAlerta("A justificativa deve ter mais de 15 caracteres.");
                return false;
            }
            return true;
        }
        private async void Inutilizar()
        {
            if (validarCampos())
            {
                try
                {
                    int numeroInicial = int.Parse(txtNumeroInicial.Text);
                    int numeroFinal = int.Parse(txtNumeroFinal.Text);
                    int serie = int.Parse(txtSerie.Text);
                    string modelo = radio55.Checked ? "55" : "65";
                    var resultadosInutilizacao = new List<string>();

                    for (int i = numeroInicial; i <= numeroFinal; i++)
                    {
                        nfe = radio55.Checked
                            ? nfeController.selecionarNFePorNumeroESerie(i.ToString(), txtSerie.Text)
                            : nfeController.selecionarNFCePorNumeroESerie(i.ToString(), txtSerie.Text);

                        if (nfe != null && nfe.NfeStatus.Id != 1)
                        {
                            if (modelo == "55")
                            {
                                resultadosInutilizacao.Add(await InutilizarModelo55(i));
                            }
                            else
                            {
                                resultadosInutilizacao.Add(await inutilizarModelo65(i));
                            }
                        }
                        else
                        {
                            nfe = AlimentaObjetoNfe(i, serie, modelo);
                            if (modelo == "55")
                            {
                                resultadosInutilizacao.Add(await InutilizarModelo55(i));
                            }
                            else
                            {
                                resultadosInutilizacao.Add(await inutilizarModelo65(i));
                            }
                        }
                    }

                    // Display results at the end
                    string resumo = string.Join(Environment.NewLine, resultadosInutilizacao);
                    GenericaDesktop.ShowInfo(resumo);
                }
                catch (Exception ex)
                {
                    GenericaDesktop.ShowErro("Erro ao processar a inutilização: " + ex.Message);
                }
            }
        }

        private async Task<string> inutilizarModelo65(int numero)
        {
            var retorno = generica.ns_InutilizarNFCe(nfe, txtJustificativa.Text.Trim());
            string mensagem = "";

            if (retorno.status.Equals("102"))
            {
                nfe.IdInut = retorno.retInutNFe.idInut;
                await ProcessarInutilizacao(nfe, retorno.retInutNFe.xml, retorno.retInutNFe.xMotivo, "NFCe");
                string motivo = retorno.retInutNFe?.xMotivo ?? "Motivo não disponível";
                mensagem = $"Numeração {nfe.NNf} Inutilizada:\n{motivo}";
            }
            else
            {
                string motivo = retorno.retInutNFe?.xMotivo ?? "Motivo não disponível";
                if (motivo.Contains("Já existe pedido de Inutilização com a mesma faixa de inutilização"))
                {
                    RetornoInutilizacao retiInutFaixa = generica.NS_DownloadNFCeInutilizada(Sessao.empresaFilialLogada.Cnpj, nfe.IdInut);
                    if (retiInutFaixa.retInut != null)
                    {
                        await ProcessarInutilizacao(nfe, retiInutFaixa.retornoInutNFe?.xml, retiInutFaixa.retornoInutNFe?.xMotivo, "NFE");
                    }
                }
                mensagem = $"Falha ao inutilizar Número {nfe.NNf}:\n{retorno.motivo}\n{motivo}";
            }

            return mensagem + "\n\n"; // Adiciona duas quebras de linha no final para separar a próxima mensagem
        }

        private async Task<string> InutilizarModelo55(int numero)
        {
            string caminhoInu = @"Fiscal\XML\NFe\" + nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\";
            string tpAmbiente = Sessao.parametroSistema.AmbienteProducao ? "1" : "2";
            string mensagem = "";

            InutilizarReq inutilizarReq = new InutilizarReq
            {
                ano = nfe.DataCadastro.Year.ToString().Substring(2, 2),
                CNPJ = Sessao.empresaFilialLogada.Cnpj,
                cUF = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Ibge,
                serie = nfe.Serie,
                tpAmb = tpAmbiente,
                nNFIni = nfe.NNf,
                nNFFin = nfe.NNf,
                xJust = txtJustificativa.Text.Trim()
            };

            var ret = NSSuite.inutilizarNumeracaoESalvar(nfe.Modelo, inutilizarReq, caminhoInu, Sessao.empresaFilialLogada.Cnpj, false);
            var retorno = JsonConvert.DeserializeObject<RetornoInutilizacao>(ret);

            if (retorno.status == 102 || retorno.status == 200)
            {
                nfe.IdInut = retorno.retInut.idInut;
                await ProcessarInutilizacao(nfe, retorno.retornoInutNFe?.xml, retorno.retornoInutNFe?.xMotivo, "NFE");
                string motivo = retorno.retornoInutNFe?.xMotivo ?? retorno.motivo;
                mensagem = $"Númeração {nfe.NNf} Inutilizada:\n{motivo}";
            }
            else
            {
                string motivo = retorno.retInut?.xMotivo ?? "Motivo não disponível";
                if (motivo.Contains("Já existe pedido de Inutilização com a mesma faixa de inutilização"))
                {
                    RetornoInutilizacao retiInutFaixa = generica.ns_DownloadEventoInutilizacaoNFE(nfe);
                    if (retiInutFaixa.retInut != null)
                    {
                        await ProcessarInutilizacao(nfe, retiInutFaixa.retornoInutNFe?.xml, retiInutFaixa.retornoInutNFe?.xMotivo, "NFE");
                    }
                }
                mensagem = $"Falha ao inutilizar Número {nfe.NNf}:\n{retorno.motivo}\n{motivo}";
            }

            return mensagem + "\n\n"; // Adiciona duas quebras de linha no final para separar a próxima mensagem
        }

        private async Task ProcessarInutilizacao(Nfe nfe, string xml, string motivo, string tipoNota)
        {
            nfe.Status = motivo;
            nfe.Cancelada = false;
            nfe.CodStatus = "5"; // 5 is the code for "Inutilizada"
            nfe.Destinatario = "NF INUTILIZADA";
            NfeStatus nfStatus = new NfeStatus();
            nfStatus.Id = 5;
            nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
            nfe.NfeStatus = nfStatus;
            Controller.getInstance().salvar(nfe);

            if (!string.IsNullOrEmpty(xml))
            {
                string caminhoGravar = $@"Fiscal\XML\{tipoNota}\{DateTime.Now.Year}-{DateTime.Now.Month:00}\Inutilizadas\";
                string nomeArquivo = $"{nfe.NNf}-INU.xml";
                generica.gravarXMLNaPasta(xml, nfe.Chave, caminhoGravar, nomeArquivo);
            }

            await EnviarParaNuvem(nfe, tipoNota);
        }

        private async Task EnviarParaNuvem(Nfe nfe, string tipoNota)
        {
            string caminhoX = @"Fiscal\XML\NFCe\" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + @"\Inutilizadas\" + nfe.NNf + @"-INU.xml";
            LunarApiNotas lunarApiNotas = new LunarApiNotas();
            byte[] arquivo;

            using (var stream = new FileStream(caminhoX, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    arquivo = reader.ReadBytes((int)stream.Length);
                    await lunarApiNotas.EnvioNotaParaNuvem(nfe.CnpjEmitente, nfe.Chave, tipoNota, "INUTILIZADAS", nfe.DataEmissao.Month.ToString().PadLeft(2, '0'), nfe.DataEmissao.Year.ToString(), arquivo, nfe);
                }
            }
        }

        private void radio55_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(radio55.Checked == true)
                    txtSerie.Text = Sessao.parametroSistema.SerieNFe;
                
                else
                    txtSerie.Text = Sessao.parametroSistema.SerieNFCe;
            }
            catch
            {

            }
        }

        private Nfe AlimentaObjetoNfe(int numeroNF, int serie, string modelo)
        {
            nfe = new Nfe();
            nfe.CDv = "";
            nfe.IndIntermed = "1";
            nfe.CMunFg = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge;
            nfe.CNf = "";
            nfe.CnpjEmitente = Sessao.empresaFilialLogada.Cnpj;
            nfe.Conciliado = true;
            nfe.CUf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf;
            nfe.DataLancamento = DateTime.Now;
            nfe.DataEmissao = DateTime.Now;
            nfe.DhEmi = DateTime.Now.ToShortTimeString();
            nfe.EmpresaFilial = Sessao.empresaFilialLogada;
            nfe.Lancada = true;
            nfe.TipoOperacao = "S";
            nfe.Modelo = modelo;
            nfe.NatOp = "VENDA";
            nfe.RazaoEmitente = Sessao.empresaFilialLogada.RazaoSocial;
            nfe.VBc = 0;
            nfe.VIcms = 0;
            nfe.VIcmsDeson = 0;
            nfe.VFcp = 0;
            nfe.VBcst = 0;
            nfe.VSt = 0;
            nfe.VFcpst = 0;
            nfe.VFcpstRet = 0;
            nfe.VFrete = 0;
            nfe.VProd = 0;
            nfe.VSeg = 0;
            nfe.VDesc = 0;
            nfe.VIi = 0;
            nfe.VIpi = 0;
            nfe.VIpiDevol = 0;
            nfe.VPis = 0;
            nfe.VCofins = 0;
            nfe.VOutro = 0;
            nfe.VNf = 0;
            nfe.VTotTrib = 0;
            nfe.NNf = numeroNF.ToString();
            nfe.Status = "Preparando Envio...";
            nfe.CodStatus = "0";
            nfe.Chave = "";
            nfe.Serie = serie.ToString();
            nfe.DhSaiEnt = DateTime.Now.ToString();
            nfe.TpNf = "1";
            nfe.IdDest = "1";
            nfe.TpImp = "4";
            nfe.TpEmis = "1";
            NfeStatus nfStatus = new NfeStatus();
            nfStatus.Id = 2;
            nfStatus = (NfeStatus)Controller.getInstance().selecionar(nfStatus);
            nfe.NfeStatus = nfStatus;

            if (Sessao.parametroSistema.AmbienteProducao == true)
                nfe.TpAmb = "1";
            else
                nfe.TpAmb = "2";
            nfe.FinNfe = "1";
            nfe.IndFinal = "1";
            nfe.IndPres = "1";
            nfe.InfCpl = Sessao.parametroSistema.InformacaoAdicionalNFCe;
            nfe.ProcEmi = "0";
            nfe.VerProc = "1.0|Lunar";
            nfe.ModFrete = "9";
            nfe.Manifesto = "";
            nfe.Protocolo = "";
            nfe.Destinatario = "";
            nfe.CnpjDestinatario = "";
            nfe.PossuiCartaCorrecao = false;

            nfe.Cliente = null;
            nfe.Destinatario = "";
            nfe.CnpjDestinatario = "";
            
            if (nfe.NatOp.Contains("VENDA"))
            {
                nfe.MovimentaEstoque = true;
                nfe.MovimentaFinanceiro = true;
            }
            Controller.getInstance().salvar(nfe);
            return nfe;
        }

        private void FrmInutilizar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    Inutilizar();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
    }
}
