using LunarBase.Classes;
using LunarBase.Constantes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;

namespace LunarBase.ClassesBO
{
    public class ValoresPadraoBO
    {
        public bool valorPadraoIncluido = false;
        private ObjetoPadrao objeto = new ObjetoPadrao();
        UsuarioController usuarioController = new UsuarioController();
        GrupoUsuarioController grupoUsuarioController = new GrupoUsuarioController();
        GrupoUsuario grupoUsuario = new GrupoUsuario();
        UnidadeMedida unidadeMedida = new UnidadeMedida();
        UnidadeMedidaController unidadeMedidaController = new UnidadeMedidaController();
        Marca marca = new Marca();
        MarcaController marcaController = new MarcaController();
        GrupoFiscal grupoFiscal = new GrupoFiscal();
        GrupoFiscalController grupoFiscalController = new GrupoFiscalController();
        OrigemIcms origemIcms = new OrigemIcms();
        OrigemIcmsController origemIcmsController = new OrigemIcmsController();
        CstIcms cstICMS = new CstIcms();
        CstIcmsController cstIcmsController = new CstIcmsController();
        Csosn csosn = new Csosn();
        CsosnController csosnController = new CsosnController();
        RegimeEmpresa regimeEmpresa = new RegimeEmpresa();
        RegimeEmpresaController regimeEmpresaController = new RegimeEmpresaController();
        FormaPagamento formaPagamento = new FormaPagamento();
        FormaPagamentoController formaPagamentoController = new FormaPagamentoController();
        BandeiraCartao bandeiraCartao = new BandeiraCartao();
        BandeiraCartaoController bandeiraCartaoController = new BandeiraCartaoController();
        Parcelamento parcelamento = new Parcelamento();
        ParcelamentoController parcelamentoController = new ParcelamentoController();
        ParametroSistema parametro = new ParametroSistema();
        ParametroSistemaController parametroController = new ParametroSistemaController();
         PlanoConta planoConta = new PlanoConta();
        PlanoContaController planoContaController = new PlanoContaController();
        NfeStatus nfeStatus = new NfeStatus();
        NfeStatusController nfeStatusController = new NfeStatusController();
        NaturezaOperacao naturezaOperacao = new NaturezaOperacao();
        NaturezaOperacaoController naturezaOperacaoController = new NaturezaOperacaoController();
        public bool gerarValoresPadrao()
        {
            try
            {
                gerarEmpresaPadrao();
                gerarGrupoUsuarioPadrao();
                gerarUsuarioPadrao();
                gerarUnidadeMedidaPadrao();
                gerarGrupoFiscalPadrao();
                gerarOrigemIcmsPadrao();
                gerarCstIcmsPadrao();
                gerarCSOSNPadrao();
                gerarRegimeTributarioPadrao();
                gerarEmpresaFilialPadrao();
                gerarFormaPagamentoPadrao();
                gerarBandeiraCartaoPadrao();
                gerarParametroSistema();
                gerarParcelamentoPadrao();
                gerarNfeStatusPadrao();
                gerarNaturezaOperacaoPadrao();
                gerarPlanoContaPadrao();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void gerarUsuarioPadrao()
        {
            Usuario usuario = new Usuario();
            try
            {
                GrupoUsuario grupoUsuario = new GrupoUsuario();
                grupoUsuario.Id = 1;
                grupoUsuario = (GrupoUsuario)grupoUsuarioController.selecionar(grupoUsuario);
                usuario.Id = LunarConstantes.USUARIO_SUPORTE;
                usuario.Login = "SUPORTE";
                usuario.Senha = Generica.Criptografa("ESTAC2UL");
                usuario.Email = "contato@txtinformatica.com.br";
                usuario.EmpresaFilial = null;
                if(grupoUsuario != null)
                    usuario.GrupoUsuario = grupoUsuario;
                else
                    usuario.GrupoUsuario = null;
                usuarioController.salvarSeNaoExistir(usuario);
            }
            catch
            {
                usuario.Id = 0;
                Controller.getInstance().salvar(usuario);
            }
        }

        public void gerarNfeStatusPadrao()
        {
            try
            {
                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_AUTORIZADO;
                nfeStatus.CodStatus = "1";
                nfeStatus.Status = "AUTORIZADO";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);

                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_REJEITADO;
                nfeStatus.CodStatus = "2";
                nfeStatus.Status = "REJEITADO";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);

                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_ENVIANDO;
                nfeStatus.CodStatus = "3";
                nfeStatus.Status = "ENVIANDO";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);

                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_CANCELADO;
                nfeStatus.CodStatus = "4";
                nfeStatus.Status = "CANCELADO";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);

                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_INUTILIZADO;
                nfeStatus.CodStatus = "5";
                nfeStatus.Status = "INUTILIZADO";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);

                nfeStatus = new NfeStatus();
                nfeStatus.Id = LunarConstantes.NFESTATUS_CONTIGENCIA;
                nfeStatus.CodStatus = "6";
                nfeStatus.Status = "CONTIGENCIA";
                nfeStatusController.salvarSeNaoExistir(nfeStatus);
            }
            catch
            {
                nfeStatus.Id = 0;
                Controller.getInstance().salvar(nfeStatus);
            }
        }
        public void gerarEmpresaFilialPadrao()
        {
            EmpresaFilial empresa = new EmpresaFilial();
            RegimeEmpresaController regimeEmpresaController = new RegimeEmpresaController();
            try
            {
                empresa.Id = 1;
                empresa.Cnae = "";
                empresa.Cnpj = "00000000000000";
                empresa.DataAbertura = DateTime.Now;
                empresa.DddPrincipal = "";
                empresa.DddSecundario = "";
                empresa.Email = "";
                empresa.Endereco = null;;
                empresa.InscricaoEstadual = "";
                empresa.NomeFantasia = "DEMONSTRACAO";
                empresa.RazaoSocial = "DEMONSTRACAO";
                empresa.TelefonePrincipal = "";
                empresa.TelefoneSecundario = "";
                empresa.SenhaCertificado = "";
                RegimeEmpresa regime = new RegimeEmpresa();
                regime.Id = 1;
                empresa.RegimeEmpresa = (RegimeEmpresa)regimeEmpresaController.selecionar(regime);

                Empresa empresaGeral = new Empresa();
                empresaGeral.Id = 1;
                empresa.Empresa = (Empresa)regimeEmpresaController.selecionar(empresaGeral);

                EmpresaFilialController empresaFilialController = new EmpresaFilialController();
                empresaFilialController.salvarSeNaoExistir(empresa);
            }
            catch
            {
                grupoUsuario.Id = 0;
                Controller.getInstance().salvar(empresa);

            }
        }
        public void gerarEmpresaPadrao()
        {
            Empresa empresa = new Empresa();
            try
            {
                empresa.Id = 1;
                empresa.Cnae = "";
                empresa.Cnpj = "00000000000000";
                empresa.Contador = "";
                empresa.CpfContador = "";
                empresa.CpfResponsavel = "";
                empresa.CrcContador = "";
                empresa.DataAbertura = DateTime.Now;
                empresa.DddPrincipal = "";
                empresa.DddSecundario = "";
                empresa.Email = "";
                empresa.EmailContador = "";
                empresa.Endereco = null;
                empresa.FuncaoResponsavel = "PROPRIETÁRIO";
                empresa.InscricaoEstadual = "";
                empresa.NomeFantasia = "DEMONSTRACAO";
                empresa.RazaoSocial = "DEMONSTRACAO";
                empresa.Responsavel = "";
                empresa.TelefonePrincipal = "";
                empresa.TelefoneSecundario = "";
                empresa.Token = "";
                empresa.ValidadeLicenca = DateTime.Now.AddDays(10);
                
                EmpresaController empresaController = new EmpresaController();
                empresaController.salvarSeNaoExistir(empresa);
            }
            catch
            {
                grupoUsuario.Id = 0;
                Controller.getInstance().salvar(empresa);

            }
        }

        public void gerarGrupoUsuarioPadrao()
        {
            grupoUsuario = new GrupoUsuario();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    grupoUsuario.Id = LunarConstantes.GRUPOUSUARIO_ADMINISTRADOR;
                    grupoUsuario.Descricao = "ADMINISTRADOR";
                    grupoUsuario.Permissoes = Generica.Criptografa("TODAS");
                    grupoUsuario.Supervisor = true;
                    grupoUsuario.Empresa = empresa;
                    grupoUsuarioController.salvarSeNaoExistir(grupoUsuario);
                }
            }
            catch
            {
                grupoUsuario.Id = 0;
                Controller.getInstance().salvar(grupoUsuario);
                
            }
        }

        public void gerarPlanoContaPadrao()
        {
            planoConta = new PlanoConta();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    planoConta.Id = 1;
                    planoConta.Descricao = "RECEITA";
                    planoConta.Classificacao = "1";
                    planoConta.IdPai = "";
                    planoConta.IdAcima = "";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "RECEITA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 2;
                    planoConta.Descricao = "DESPESA";
                    planoConta.Classificacao = "2";
                    planoConta.IdPai = "";
                    planoConta.IdAcima = "";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 3;
                    planoConta.Descricao = "VENDAS";
                    planoConta.Classificacao = "1.01";
                    planoConta.IdPai = "1";
                    planoConta.IdAcima = "1";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "RECEITA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 4;
                    planoConta.Descricao = "SERVICOS";
                    planoConta.Classificacao = "1.02";
                    planoConta.IdPai = "1";
                    planoConta.IdAcima = "1";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "RECEITA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 5;
                    planoConta.Descricao = "MENSALIDADES";
                    planoConta.Classificacao = "1.03";
                    planoConta.IdPai = "1";
                    planoConta.IdAcima = "1";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "RECEITA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 6;
                    planoConta.Descricao = "COMPRA FORNECEDORES";
                    planoConta.Classificacao = "2.01";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 7;
                    planoConta.Descricao = "COMPRA USO E CONSUMO";
                    planoConta.Classificacao = "2.02";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 8;
                    planoConta.Descricao = "COMPRA IMOBILIZADO";
                    planoConta.Classificacao = "2.03";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "VARIAVEL";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 9;
                    planoConta.Descricao = "ALUGUEL";
                    planoConta.Classificacao = "2.04";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 10;
                    planoConta.Descricao = "ENERGIA ELETRICA";
                    planoConta.Classificacao = "2.05";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 11;
                    planoConta.Descricao = "AGUA";
                    planoConta.Classificacao = "2.06";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 12;
                    planoConta.Descricao = "TELEFONIA";
                    planoConta.Classificacao = "2.07";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 13;
                    planoConta.Descricao = "INTERNET";
                    planoConta.Classificacao = "2.08";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 14;
                    planoConta.Descricao = "SALARIOS";
                    planoConta.Classificacao = "2.09";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 15;
                    planoConta.Descricao = "EMPRESTIMOS";
                    planoConta.Classificacao = "2.10";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);

                    planoConta = new PlanoConta();
                    planoConta.Id = 16;
                    planoConta.Descricao = "MARKETING";
                    planoConta.Classificacao = "2.11";
                    planoConta.IdPai = "2";
                    planoConta.IdAcima = "2";
                    planoConta.Tipo = "FIXO";
                    planoConta.TipoConta = "DESPESA";
                    planoContaController.salvarSeNaoExistir(planoConta);
                }
            }
            catch
            {
                planoConta.Id = 0;
                Controller.getInstance().salvar(planoConta);

            }
        }

        public void gerarUnidadeMedidaPadrao()
        {

            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_UN;
                    unidadeMedida.Descricao = "UNIDADE";
                    unidadeMedida.Sigla = "UN";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_PC;
                    unidadeMedida.Descricao = "PEÇA";
                    unidadeMedida.Sigla = "PC";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_KG;
                    unidadeMedida.Descricao = "KILO";
                    unidadeMedida.Sigla = "KG";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_MT;
                    unidadeMedida.Descricao = "METRO";
                    unidadeMedida.Sigla = "MT";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_CM;
                    unidadeMedida.Descricao = "CENTIMETRO";
                    unidadeMedida.Sigla = "CM";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_CX;
                    unidadeMedida.Descricao = "CAIXA";
                    unidadeMedida.Sigla = "CX";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_LT;
                    unidadeMedida.Descricao = "LITRO";
                    unidadeMedida.Sigla = "LT";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_JG;
                    unidadeMedida.Descricao = "JOGO";
                    unidadeMedida.Sigla = "JG";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_PR;
                    unidadeMedida.Descricao = "PAR";
                    unidadeMedida.Sigla = "PR";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_PT;
                    unidadeMedida.Descricao = "PACOTE";
                    unidadeMedida.Sigla = "PT";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_GR;
                    unidadeMedida.Descricao = "GRAMA";
                    unidadeMedida.Sigla = "GR";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);

                    unidadeMedida = new UnidadeMedida();
                    unidadeMedida.Id = LunarConstantes.UNIDADEMEDIDA_RL;
                    unidadeMedida.Descricao = "ROLO";
                    unidadeMedida.Sigla = "RL";
                    unidadeMedida.Empresa = empresa;
                    unidadeMedidaController.salvarSeNaoExistir(unidadeMedida);
                }
            }
            catch 
            {
                unidadeMedida.Id = 0;
                Controller.getInstance().salvar(unidadeMedida);
            }
        }
        public void gerarGrupoFiscalPadrao()
        {
            grupoFiscal = new GrupoFiscal();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    grupoFiscal.Id = LunarConstantes.GRUPOFISCAL_TRIBUTADO;
                    grupoFiscal.Descricao = "TRIBUTADO";
                    grupoFiscal.CfopSaidaEstadual = "5102";
                    grupoFiscal.CfopSaidaInterestadual = "6102";
                    grupoFiscal.CsosnSaida = "102";
                    String aliquotaICMS = "18";
                    grupoFiscal.AliquotaIcms = aliquotaICMS;
                    grupoFiscal.Empresa = empresa;
                    grupoFiscalController.salvarSeNaoExistir(grupoFiscal);

                    grupoFiscal.Id = LunarConstantes.GRUPOFISCAL_SUBSTITUICAOTRIBUTARIA;
                    grupoFiscal.Descricao = "ST - SUBSTITUIÇÃO TRIBUTÁRIA";
                    grupoFiscal.CfopSaidaEstadual = "5405";
                    grupoFiscal.CfopSaidaInterestadual = "6403";
                    grupoFiscal.CsosnSaida = "500";
                    grupoFiscal.AliquotaIcms = "";
                    grupoFiscal.Empresa = empresa;
                    grupoFiscalController.salvarSeNaoExistir(grupoFiscal);
                }
            }
            catch
            {
                grupoFiscal.Id = 0;
                Controller.getInstance().salvar(grupoFiscal);
            }
        }

        public void gerarOrigemIcmsPadrao()
        {
            origemIcms = new OrigemIcms();
            try
            {
                origemIcms.Id = LunarConstantes.ORIGEMICMS_0;
                origemIcms.CodOrigem = "0";
                origemIcms.Descricao = "Nacional, exceto as indicadas nos códigos 3 a 5".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_1;
                origemIcms.CodOrigem = "1";
                origemIcms.Descricao = "Estrangeira - Importação direta, exceto a indicada no código 6".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_2;
                origemIcms.CodOrigem = "2";
                origemIcms.Descricao = "Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_3;
                origemIcms.CodOrigem = "3";
                origemIcms.Descricao = "Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40%".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_4;
                origemIcms.CodOrigem = "4";
                origemIcms.Descricao = "Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto - Lei nº 288 / 67 e as Leis nºs 8.248 / 91, 8.387 / 91, 10.176 / 01 e 11.484 / 07".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_5;
                origemIcms.CodOrigem = "5";
                origemIcms.Descricao = "Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_6;
                origemIcms.CodOrigem = "6";
                origemIcms.Descricao = "Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);

                origemIcms.Id = LunarConstantes.ORIGEMICMS_7;
                origemIcms.CodOrigem = "7";
                origemIcms.Descricao = "Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução CAMEX".ToString().ToUpper();
                origemIcmsController.salvarSeNaoExistir(origemIcms);
            }
            catch
            {
                origemIcms.Id = 0;
                Controller.getInstance().salvar(origemIcms);
            }
        }

        public void gerarCstIcmsPadrao()
        {
            cstICMS = new CstIcms();
            try
            {
                cstICMS.Id = LunarConstantes.CSTICMS_00;
                cstICMS.Codigo = "00";
                cstICMS.Descricao = "Tributada integralmente".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_10;
                cstICMS.Codigo = "10";
                cstICMS.Descricao = "Tributada e com cobrança do ICMS por substituição tributária".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_20;
                cstICMS.Codigo = "20";
                cstICMS.Descricao = "Com redução da BC".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_30;
                cstICMS.Codigo = "30";
                cstICMS.Descricao = "Isenta / não tributada e com cobrança do ICMS por substituição tributária".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_40;
                cstICMS.Codigo = "40";
                cstICMS.Descricao = "Isenta".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_41;
                cstICMS.Codigo = "41";
                cstICMS.Descricao = "Não tributada".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_50;
                cstICMS.Codigo = "50";
                cstICMS.Descricao = "Com suspensão".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_51;
                cstICMS.Codigo = "51";
                cstICMS.Descricao = "Com diferimento".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_60;
                cstICMS.Codigo = "60";
                cstICMS.Descricao = "ICMS cobrado anteriormente por substituição tributária".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_70;
                cstICMS.Codigo = "70";
                cstICMS.Descricao = "Com redução da BC e cobrança do ICMS por substituição tributária".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);

                cstICMS.Id = LunarConstantes.CSTICMS_90;
                cstICMS.Codigo = "90";
                cstICMS.Descricao = "Outras".ToString().ToUpper();
                cstIcmsController.salvarSeNaoExistir(cstICMS);
            }
            catch
            {
                cstICMS.Id = 0;
                Controller.getInstance().salvar(cstICMS);
            }
        }

        public void gerarCSOSNPadrao()
        {
            csosn = new Csosn();
            try
            {
                csosn.Id = LunarConstantes.CSOSNICMS_101;
                csosn.Codigo = "101";
                csosn.Descricao = "Tributada pelo Simples Nacional com permissão de crédito".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_102;
                csosn.Codigo = "102";
                csosn.Descricao = "Tributada pelo Simples Nacional sem permissão de crédito".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_103;
                csosn.Codigo = "103";
                csosn.Descricao = "Isenção do ICMS no Simples Nacional para faixa de receita bruta".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_201;
                csosn.Codigo = "201";
                csosn.Descricao = "Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por substituição tributária".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_202;
                csosn.Codigo = "202";
                csosn.Descricao = "Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por substituição tributária".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_203;
                csosn.Codigo = "203";
                csosn.Descricao = "Isenção do ICMS no Simples Nacional para faixa de receita bruta e com cobrança do ICMS por substituição tributária".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_300;
                csosn.Codigo = "300";
                csosn.Descricao = "Imune".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_400;
                csosn.Codigo = "400";
                csosn.Descricao = "Não tributada pelo Simples Nacional".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_500;
                csosn.Codigo = "500";
                csosn.Descricao = "ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);

                csosn.Id = LunarConstantes.CSOSNICMS_900;
                csosn.Codigo = "900";
                csosn.Descricao = "Outros".ToString().ToUpper();
                csosnController.salvarSeNaoExistir(csosn);
            }
            catch
            {
                csosn.Id = 0;
                Controller.getInstance().salvar(csosn);
            }
        }

        public void gerarRegimeTributarioPadrao()
        {

            try
            {
                regimeEmpresa = new RegimeEmpresa();
                regimeEmpresa.Id = LunarConstantes.REGIMEEMPRESA_SIMPLESNACIONAL;
                regimeEmpresa.Descricao = "SIMPLES NACIONAL";
                regimeEmpresaController.salvarSeNaoExistir(regimeEmpresa);

                regimeEmpresa = new RegimeEmpresa();
                regimeEmpresa.Id = LunarConstantes.REGIMEEMPRESA_LUCROPRESUMIDO;
                regimeEmpresa.Descricao = "LUCRO PRESUMIDO";
                regimeEmpresaController.salvarSeNaoExistir(regimeEmpresa);

                regimeEmpresa = new RegimeEmpresa();
                regimeEmpresa.Id = LunarConstantes.REGIMEEMPRESA_LUCROREAL;
                regimeEmpresa.Descricao = "LUCRO REAL";
                regimeEmpresaController.salvarSeNaoExistir(regimeEmpresa);

                regimeEmpresa = new RegimeEmpresa();
                regimeEmpresa.Id = LunarConstantes.REGIMEEMPRESA_SIMPLES_EXCESSO_RECEITA;
                regimeEmpresa.Descricao = "SIMPLES, EXCESSO RECEITA";
                regimeEmpresaController.salvarSeNaoExistir(regimeEmpresa);

                regimeEmpresa = new RegimeEmpresa();
                regimeEmpresa.Id = LunarConstantes.REGIMEEMPRESA_MEI;
                regimeEmpresa.Descricao = "MEI - MICROEMPREENDEDOR INDIVIDUAL";
                regimeEmpresaController.salvarSeNaoExistir(regimeEmpresa);
            }
            catch
            {
                unidadeMedida.Id = 0;
                Controller.getInstance().salvar(unidadeMedida);
            }
        }

        public void gerarFormaPagamentoPadrao()
        {

            try
            {
                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_DINHEIRO;
                formaPagamento.Descricao = "DINHEIRO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = true;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "01";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_CARTAO;
                formaPagamento.Descricao = "CARTÃO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = true;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "03";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_PIX;
                formaPagamento.Descricao = "PIX";
                formaPagamento.Banco = true;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "17";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_DEPOSITO;
                formaPagamento.Descricao = "DEPÓSITO";
                formaPagamento.Banco = true;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "16";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_BOLETO;
                formaPagamento.Descricao = "BOLETO BANCÁRIO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = true;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "15";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_CREDIARIO;
                formaPagamento.Descricao = "CREDIÁRIO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = true;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "99";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_CHEQUE;
                formaPagamento.Descricao = "CHEQUE";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = true;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "02";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);


                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_CREDITOCLIENTE;
                formaPagamento.Descricao = "CRÉDITO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = true;
                formaPagamento.CodigoSefaz = "05";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);

                formaPagamento = new FormaPagamento();
                formaPagamento.Id = LunarConstantes.FORMAPAGAMENTO_ABATIMENTO;
                formaPagamento.Descricao = "ABATIMENTO";
                formaPagamento.Banco = false;
                formaPagamento.Boleto = false;
                formaPagamento.Caixa = false;
                formaPagamento.Cartao = false;
                formaPagamento.Cheque = false;
                formaPagamento.Crediario = false;
                formaPagamento.CreditoCliente = false;
                formaPagamento.CodigoSefaz = "05";
                formaPagamentoController.salvarSeNaoExistir(formaPagamento);
            }
            catch
            {
                formaPagamento.Id = 0;
                Controller.getInstance().salvar(formaPagamento);
            }
        }

        public void gerarBandeiraCartaoPadrao()
        {
            bandeiraCartao = new BandeiraCartao();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_VISA;
                    bandeiraCartao.Descricao = "VISA";
                    bandeiraCartao.CodigoSefaz = "01";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_MASTERCARD;
                    bandeiraCartao.Descricao = "MASTER CARD";
                    bandeiraCartao.CodigoSefaz = "02";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_ELO;
                    bandeiraCartao.Descricao = "ELO";
                    bandeiraCartao.CodigoSefaz = "06";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_AMERICANEXPRESS;
                    bandeiraCartao.Descricao = "AMERICAN EXPRESS";
                    bandeiraCartao.CodigoSefaz = "03";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_SOROCRED;
                    bandeiraCartao.Descricao = "SOROCRED";
                    bandeiraCartao.CodigoSefaz = "04";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);


                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_HIPERCARD;
                    bandeiraCartao.Descricao = "HIPER CARD";
                    bandeiraCartao.CodigoSefaz = "07";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_AURA;
                    bandeiraCartao.Descricao = "AURA";
                    bandeiraCartao.CodigoSefaz = "08";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_DINERS;
                    bandeiraCartao.Descricao = "DINERS";
                    bandeiraCartao.CodigoSefaz = "05";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_CABAL;
                    bandeiraCartao.Descricao = "CABAL";
                    bandeiraCartao.CodigoSefaz = "09";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_JCB;
                    bandeiraCartao.Descricao = "JCB";
                    bandeiraCartao.CodigoSefaz = "18";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);

                    bandeiraCartao = new BandeiraCartao();
                    bandeiraCartao.Id = LunarConstantes.BANDEIRACARTAO_OUTROS;
                    bandeiraCartao.Descricao = "OUTROS";
                    bandeiraCartao.CodigoSefaz = "99";
                    bandeiraCartaoController.salvarSeNaoExistir(bandeiraCartao);
                }
            }
            catch
            {
                bandeiraCartao.Id = 0;
                Controller.getInstance().salvar(bandeiraCartao);
            }
        }

        public void gerarParcelamentoPadrao()
        {
            parcelamento = new Parcelamento();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_DEBITO;
                    parcelamento.Descricao = "DÉBITO";
                    parcelamento.DiasRecebimento = 1;
                    parcelamento.Parcelas = 1;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("1,69");
                    parcelamento.Debito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_A_VISTA;
                    parcelamento.Descricao = "CRÉDITO A VISTA - 1X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 1;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,69");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_2X;
                    parcelamento.Descricao = "CRÉDITO 2X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 2;
                    parcelamento.Tarifa = 0;
                    parcelamento.Credito = true;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_3X;
                    parcelamento.Descricao = "CRÉDITO 3X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 3;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_4X;
                    parcelamento.Descricao = "CRÉDITO 4X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 4;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_5X;
                    parcelamento.Descricao = "CRÉDITO 5X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 5;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_6X;
                    parcelamento.Descricao = "CRÉDITO 6X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 6;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_7X;
                    parcelamento.Descricao = "CRÉDITO 7X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 7;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_8X;
                    parcelamento.Descricao = "CRÉDITO 8X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 8;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_9X;
                    parcelamento.Descricao = "CRÉDITO 9X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 9;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_10X;
                    parcelamento.Descricao = "CRÉDITO 10X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 10;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_11X;
                    parcelamento.Descricao = "CRÉDITO 11X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 11;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_12X;
                    parcelamento.Descricao = "CRÉDITO 12X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 12;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_13X;
                    parcelamento.Descricao = "CRÉDITO 13X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 13;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_14X;
                    parcelamento.Descricao = "CRÉDITO 14X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 14;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_15X;
                    parcelamento.Descricao = "CRÉDITO 15X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 15;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_16X;
                    parcelamento.Descricao = "CRÉDITO 16X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 16;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_17X;
                    parcelamento.Descricao = "CRÉDITO 17X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 17;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_18X;
                    parcelamento.Descricao = "CRÉDITO 18X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 18;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_19X;
                    parcelamento.Descricao = "CRÉDITO 19X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 19;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_20X;
                    parcelamento.Descricao = "CRÉDITO 20X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 20;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_21X;
                    parcelamento.Descricao = "CRÉDITO 21X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 21;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_22X;
                    parcelamento.Descricao = "CRÉDITO 22X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 22;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_23X;
                    parcelamento.Descricao = "CRÉDITO 23X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 23;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);

                    parcelamento = new Parcelamento();
                    parcelamento.Id = LunarConstantes.PARCELAMENTO_CREDITO_24X;
                    parcelamento.Descricao = "CRÉDITO 24X";
                    parcelamento.DiasRecebimento = 30;
                    parcelamento.Parcelas = 24;
                    parcelamento.Tarifa = 0;
                    parcelamento.Taxa = double.Parse("2,89");
                    parcelamento.Credito = true;
                    parcelamento.TaxaAntecipacao = 0;
                    parcelamentoController.salvarSeNaoExistir(parcelamento);
                }
            }
            catch
            {
                grupoFiscal.Id = 0;
                Controller.getInstance().salvar(grupoFiscal);
            }
        }

        public void gerarParametroSistema()
        {
            parametro = new ParametroSistema();
            try
            {
                Empresa empresa = new Empresa();
                empresa.Id = 1;
                EmpresaController empresaController = new EmpresaController();
                empresa = (Empresa)empresaController.selecionar(empresa);
                if (empresa != null)
                {
                    parametro.Id = LunarConstantes.PARAMETROSISTEMA_UNICO;
                    parametro.UltNsu = "0";
                    parametro.SerieNFCe = "1";
                    parametro.SerieNFe = "1";
                    parametro.ProximoNumeroNFCe = "1";
                    parametro.ProximoNumeroNFe = "1";
                    parametro.AmbienteProducao = false;
                    parametro.CscNfce = "";
                    parametro.TokenNfce = "000001";
                    parametro.InformacaoAdicionalNFCe = "OBRIGADO PELA PREFERENCIA, VOLTE SEMPRE!!!";
                    parametro.InformacaoAdicionalNFe = "DOCUMENTO EMITIDO POR ME OU EPP OPTANTE PELO SIMPLES NACIONAL NÃO GERA DIREITO A CRÉDITO FISCAL DE IPI";
                    parametro.PastaRemessaNsCloud = @"C:\LUNAR\NSNFCE";
                    parametro.AlertaEstoqueFiscal = true;
                    parametro.AlertaEstoqueGerencial = true;
                    parametro.Juro = "1";
                    parametro.Multa = "2";
                    parametro.TokenWhats = "";
                    parametro.IdInstanciaWhats = "";
                    parametroController.salvarSeNaoExistir(parametro);
                }
            }
            catch
            {
                parametro.Id = 0;
                Controller.getInstance().salvar(parametro);
            }
        }

        public void gerarNaturezaOperacaoPadrao()
        {
            try
            {
                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao.Id = 1;
                naturezaOperacao.Descricao = "DEVOLUÇÃO DE COMPRA";
                naturezaOperacao.MovimentaEstoque = true;
                naturezaOperacao.MovimentaFinanceiro = true;
                naturezaOperacao.EntradaSaida = "S";
                naturezaOperacao.FinalidadeNfe = "4";
                naturezaOperacaoController.salvarSeNaoExistir(naturezaOperacao);

                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao.Id = 2;
                naturezaOperacao.Descricao = "DEVOLUÇÃO DE VENDA";
                naturezaOperacao.MovimentaEstoque = true;
                naturezaOperacao.MovimentaFinanceiro = true;
                naturezaOperacao.EntradaSaida = "E";
                naturezaOperacao.FinalidadeNfe = "4";
                naturezaOperacaoController.salvarSeNaoExistir(naturezaOperacao);

                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao.Id = 3;
                naturezaOperacao.Descricao = "OUTRAS SAIDAS";
                naturezaOperacao.MovimentaEstoque = true;
                naturezaOperacao.MovimentaFinanceiro = true;
                naturezaOperacao.EntradaSaida = "S";
                naturezaOperacao.FinalidadeNfe = "1";
                naturezaOperacaoController.salvarSeNaoExistir(naturezaOperacao);

                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao.Id = 4;
                naturezaOperacao.Descricao = "OUTRAS ENTRADAS";
                naturezaOperacao.MovimentaEstoque = true;
                naturezaOperacao.MovimentaFinanceiro = true;
                naturezaOperacao.EntradaSaida = "E";
                naturezaOperacao.FinalidadeNfe = "1";
                naturezaOperacaoController.salvarSeNaoExistir(naturezaOperacao);

                naturezaOperacao = new NaturezaOperacao();
                naturezaOperacao.Id = 5;
                naturezaOperacao.Descricao = "REMESSA PARA CONSERTO";
                naturezaOperacao.MovimentaEstoque = true;
                naturezaOperacao.MovimentaFinanceiro = false;
                naturezaOperacao.EntradaSaida = "S";
                naturezaOperacao.FinalidadeNfe = "1";
                naturezaOperacaoController.salvarSeNaoExistir(naturezaOperacao);
            }
            catch
            {
                naturezaOperacao.Id = 0;
                Controller.getInstance().salvar(naturezaOperacao);
            }
        }
    }
}
