using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class ParametroSistema : ObjetoPadrao
    {
        private int id;
        private string ultNsu;
        private string proximoNumeroNFCe;
        private string serieNFCe;
        private string proximoNumeroNFe;
        private string serieNFe;
        private bool ambienteProducao;
        private string tokenNfce;
        private string cscNfce;
        private string informacaoAdicionalNFCe;
        private String informacaoAdicionalNFe;
        private string pastaRemessaNsCloud;
        private bool alertaEstoqueFiscal;
        private bool alertaEstoqueGerencial;
        private string multa;
        private string juro;
        private TipoObjeto tipoObjeto;
        private string idInstanciaWhats;
        private string tokenWhats;
        private PlanoConta planoContaVenda;
        private PlanoConta planoContaCompraRevenda;
        private PlanoConta planoContaCompraImobilizado;
        private PlanoConta planoContaUsoConsumo;

        private PlanoConta planoContaAjusteCaixaEntrada;
        private PlanoConta planoContaAjusteCaixaSaida;
        private string tipoCaixa;

        private string logo;
        private string dddWhats;
        private string foneWhats;
        private string servidorEmail;
        private string portaEmail;
        private string nomeRemetenteEmail;
        private string email;
        private string senhaEmail;
        private bool autenticacaoSsl;
        private bool autenticacaoTls;
        private string lembreteVencimento;
        private string caminhoAnexo;
        private double comissao;
        private bool chequeContaReceber;
        private string modeloEtiquetaPadrao;
        private string modeloEtiquetaNumeroOs;
        private string tokenGalaxyPay;
        private string idGalaxyPay;
        private bool integracaoGalaxyPay;
        private ContaBancaria contaBancariaVinculadaApi;
        private string usuarioWebServiceSpcBrasil;
        private string senhaWebServiceSpcBrasil;
        private string consultaPadraoSpcBrasil;
        private string ambienteSpcBrasil;

        private string tipoImpressoraCondicional;
        private bool selecionarGradeEan;

        //WHATS
        private bool envioNotasPorWhats;
        private bool envioAgradecimentoCompra;
        private bool ativarMensagemPosVendas;
        private bool ativarMensagemVencimentoExame;
        private string mensagemPosVendasDiasOuMinutos;
        private string mensagemPosVendasQtdDiasOuMinutos;
        private string mensagemPosVendas;
        private string mensagemLembreteExameAntesOuDepois;
        private string mensagemLembreteExameQtdDias;
        private string mensagemLembreteExameHorario;
        private string mensagemLembreteExame;
        private string nomeServidor;
        private bool mensagemPosVendaAposFinalizarOs;

        //Servidor Nuvem para Dashboards
        private string servidorNuvem;
        private string bancoNuvem;
        private string usuarioNuvem;
        private string senhaNuvem;

        //Nota de Serviço
        private int numeroRps;
        private string serieRps;
        private string usuarioNfse;
        private string senhaNfse;

        private bool viaImpressaoOs;

        public virtual int Id { get => id; set => id = value; }
        public virtual string UltNsu { get => ultNsu; set => ultNsu = value; }
        public virtual string ProximoNumeroNFCe { get => proximoNumeroNFCe; set => proximoNumeroNFCe = value; }
        public virtual string SerieNFCe { get => serieNFCe; set => serieNFCe = value; }
        public virtual string ProximoNumeroNFe { get => proximoNumeroNFe; set => proximoNumeroNFe = value; }
        public virtual string SerieNFe { get => serieNFe; set => serieNFe = value; }
        public virtual bool AmbienteProducao { get => ambienteProducao; set => ambienteProducao = value; }
        public virtual string TokenNfce { get => tokenNfce; set => tokenNfce = value; }
        public virtual string CscNfce { get => cscNfce; set => cscNfce = value; }
        public virtual string InformacaoAdicionalNFCe { get => informacaoAdicionalNFCe; set => informacaoAdicionalNFCe = value; }
        public virtual string InformacaoAdicionalNFe { get => informacaoAdicionalNFe; set => informacaoAdicionalNFe = value; }
        public virtual string PastaRemessaNsCloud { get => pastaRemessaNsCloud; set => pastaRemessaNsCloud = value; }
        public virtual bool AlertaEstoqueFiscal { get => alertaEstoqueFiscal; set => alertaEstoqueFiscal = value; }
        public virtual bool AlertaEstoqueGerencial { get => alertaEstoqueGerencial; set => alertaEstoqueGerencial = value; }
        public virtual string Multa { get => multa; set => multa = value; }
        public virtual string Juro { get => juro; set => juro = value; }
        public virtual TipoObjeto TipoObjeto { get => tipoObjeto; set => tipoObjeto = value; }
        public virtual string IdInstanciaWhats { get => idInstanciaWhats; set => idInstanciaWhats = value; }
        public virtual string TokenWhats { get => tokenWhats; set => tokenWhats = value; }
        public virtual PlanoConta PlanoContaVenda { get => planoContaVenda; set => planoContaVenda = value; }
        public virtual PlanoConta PlanoContaCompraRevenda { get => planoContaCompraRevenda; set => planoContaCompraRevenda = value; }
        public virtual PlanoConta PlanoContaCompraImobilizado { get => planoContaCompraImobilizado; set => planoContaCompraImobilizado = value; }
        public virtual PlanoConta PlanoContaUsoConsumo { get => planoContaUsoConsumo; set => planoContaUsoConsumo = value; }
        public virtual string Logo { get => logo; set => logo = value; }
        public virtual string DddWhats { get => dddWhats; set => dddWhats = value; }
        public virtual string FoneWhats { get => foneWhats; set => foneWhats = value; }
        public virtual string ServidorEmail { get => servidorEmail; set => servidorEmail = value; }
        public virtual string PortaEmail { get => portaEmail; set => portaEmail = value; }
        public virtual string NomeRemetenteEmail { get => nomeRemetenteEmail; set => nomeRemetenteEmail = value; }
        public virtual string Email { get => email; set => email = value; }
        public virtual string SenhaEmail { get => senhaEmail; set => senhaEmail = value; }
        public virtual bool AutenticacaoSsl { get => autenticacaoSsl; set => autenticacaoSsl = value; }
        public virtual bool AutenticacaoTls { get => autenticacaoTls; set => autenticacaoTls = value; }
        public virtual string LembreteVencimento { get => lembreteVencimento; set => lembreteVencimento = value; }
        public virtual string CaminhoAnexo { get => caminhoAnexo; set => caminhoAnexo = value; }
        public virtual double Comissao { get => comissao; set => comissao = value; }
        public virtual bool ChequeContaReceber { get => chequeContaReceber; set => chequeContaReceber = value; }
        public virtual string ModeloEtiquetaPadrao { get => modeloEtiquetaPadrao; set => modeloEtiquetaPadrao = value; }
        public virtual string ModeloEtiquetaNumeroOs { get => modeloEtiquetaNumeroOs; set => modeloEtiquetaNumeroOs = value; }
        public virtual string TokenGalaxyPay { get => tokenGalaxyPay; set => tokenGalaxyPay = value; }
        public virtual string IdGalaxyPay { get => idGalaxyPay; set => idGalaxyPay = value; }
        public virtual bool IntegracaoGalaxyPay { get => integracaoGalaxyPay; set => integracaoGalaxyPay = value; }
        public virtual ContaBancaria ContaBancariaVinculadaApi { get => contaBancariaVinculadaApi; set => contaBancariaVinculadaApi = value; }
        public virtual string UsuarioWebServiceSpcBrasil { get => usuarioWebServiceSpcBrasil; set => usuarioWebServiceSpcBrasil = value; }
        public virtual string SenhaWebServiceSpcBrasil { get => senhaWebServiceSpcBrasil; set => senhaWebServiceSpcBrasil = value; }
        public virtual string ConsultaPadraoSpcBrasil { get => consultaPadraoSpcBrasil; set => consultaPadraoSpcBrasil = value; }
        public virtual string AmbienteSpcBrasil { get => ambienteSpcBrasil; set => ambienteSpcBrasil = value; }
        public virtual bool EnvioNotasPorWhats { get => envioNotasPorWhats; set => envioNotasPorWhats = value; }
        public virtual bool EnvioAgradecimentoCompra { get => envioAgradecimentoCompra; set => envioAgradecimentoCompra = value; }
        public virtual bool AtivarMensagemPosVendas { get => ativarMensagemPosVendas; set => ativarMensagemPosVendas = value; }
        public virtual bool AtivarMensagemVencimentoExame { get => ativarMensagemVencimentoExame; set => ativarMensagemVencimentoExame = value; }
        public virtual string MensagemPosVendasDiasOuMinutos { get => mensagemPosVendasDiasOuMinutos; set => mensagemPosVendasDiasOuMinutos = value; }
        public virtual string MensagemPosVendasQtdDiasOuMinutos { get => mensagemPosVendasQtdDiasOuMinutos; set => mensagemPosVendasQtdDiasOuMinutos = value; }
        public virtual string MensagemPosVendas { get => mensagemPosVendas; set => mensagemPosVendas = value; }
        public virtual string MensagemLembreteExameAntesOuDepois { get => mensagemLembreteExameAntesOuDepois; set => mensagemLembreteExameAntesOuDepois = value; }
        public virtual string MensagemLembreteExameQtdDias { get => mensagemLembreteExameQtdDias; set => mensagemLembreteExameQtdDias = value; }
        public virtual string MensagemLembreteExameHorario { get => mensagemLembreteExameHorario; set => mensagemLembreteExameHorario = value; }
        public virtual string MensagemLembreteExame { get => mensagemLembreteExame; set => mensagemLembreteExame = value; }
        public virtual string NomeServidor { get => nomeServidor; set => nomeServidor = value; }
        public virtual bool MensagemPosVendaAposFinalizarOs { get => mensagemPosVendaAposFinalizarOs; set => mensagemPosVendaAposFinalizarOs = value; }
        public virtual string ServidorNuvem { get => servidorNuvem; set => servidorNuvem = value; }
        public virtual string BancoNuvem { get => bancoNuvem; set => bancoNuvem = value; }
        public virtual string UsuarioNuvem { get => usuarioNuvem; set => usuarioNuvem = value; }
        public virtual string SenhaNuvem { get => senhaNuvem; set => senhaNuvem = value; }
        public virtual string TipoImpressoraCondicional { get => tipoImpressoraCondicional; set => tipoImpressoraCondicional = value; }
        public virtual bool SelecionarGradeEan { get => selecionarGradeEan; set => selecionarGradeEan = value; }
        public virtual bool ViaImpressaoOs { get => viaImpressaoOs; set => viaImpressaoOs = value; }
        public virtual PlanoConta PlanoContaAjusteCaixaEntrada { get => planoContaAjusteCaixaEntrada; set => planoContaAjusteCaixaEntrada = value; }
        public virtual PlanoConta PlanoContaAjusteCaixaSaida { get => planoContaAjusteCaixaSaida; set => planoContaAjusteCaixaSaida = value; }
        public virtual string TipoCaixa { get => tipoCaixa; set => tipoCaixa = value; }
        public virtual int NumeroRps { get => numeroRps; set => numeroRps = value; }
        public virtual string SerieRps { get => serieRps; set => serieRps = value; }
        public virtual string UsuarioNfse { get => usuarioNfse; set => usuarioNfse = value; }
        public virtual string SenhaNfse { get => senhaNfse; set => senhaNfse = value; }
    }
}
