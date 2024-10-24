using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunar.Utils.IntegracoesBancosBoletos
{
    public class BoletoSicrediManager
    {
        IList<ContaReceber> _listaReceber = new List<ContaReceber>();
        private readonly BoletoService _boletoService;
        private readonly SicrediIntegration _sicrediIntegration;
        private readonly BoletoConfig _boletoConfig;

        public BoletoSicrediManager(int vendaId, int osId, ContaBancaria contaBancaria, bool ambienteProducao, ContaReceber contaReceberUnica)
        {
            BoletoConfigController boletoConfigController = new BoletoConfigController();
            _boletoService = new BoletoService();
            _boletoConfig = LoadBoletoConfigVenda(boletoConfigController, vendaId, osId, contaReceberUnica);

            // Verifica se a configuração do boleto foi carregada com sucesso
            if (_boletoConfig == null)
            {
                _boletoConfig = LoadBoletoConfig_PorContaBancariaPreSelecionada(boletoConfigController, contaBancaria);
            }
            if (_boletoConfig == null)
            {
                throw new Exception("Configuração do boleto não encontrada.");
            }

            _sicrediIntegration = new SicrediIntegration(
                isProduction: ambienteProducao,
                username: _boletoConfig.Usuario,
                password: GenericaDesktop.Descriptografa(_boletoConfig.Senha),
                cooperativa: _boletoConfig.Cooperativa,
                posto: _boletoConfig.Posto,
                codigoBeneficiario: _boletoConfig.CodigoBeneficiario
            );
        }

        private BoletoConfig LoadBoletoConfigVenda(BoletoConfigController boletoConfigController, int vendaId, int osId, ContaReceber contaReceberUnica)
        {
            ContaReceberController contaReceberController = new ContaReceberController();
            ContaBancaria contaBoleto = new ContaBancaria();
            if (vendaId > 0)
                _listaReceber = contaReceberController.selecionarContaReceberPorVenda(vendaId);
            else if (osId > 0)
                _listaReceber = contaReceberController.selecionarContaReceberPorOrdemServico(osId);
            else if (contaReceberUnica != null)
                _listaReceber.Add(contaReceberUnica);

            if (_listaReceber.Count > 0)
            {
                foreach (ContaReceber contaReceber in _listaReceber)
                {
                    contaBoleto = contaReceber.ContaBoleto;
                    break; // Saia do loop após pegar a primeira conta, se necessário
                }

                IList<BoletoConfig> listaBc = boletoConfigController.selecionarBoletoConfigPorContaBancaria(contaBoleto.Id);
                return listaBc.FirstOrDefault(); // Retorna a primeira configuração encontrada ou null}
            }
            else
                return null;
        }

        private BoletoConfig LoadBoletoConfig_PorContaBancariaPreSelecionada(BoletoConfigController boletoConfigController, ContaBancaria conta)
        {
            ContaReceberController contaReceberController = new ContaReceberController();
            IList<BoletoConfig> listaBc = boletoConfigController.selecionarBoletoConfigPorContaBancaria(conta.Id);
            return listaBc.FirstOrDefault(); // Retorna a primeira configuração encontrada ou null}
        }

        public async Task<bool> GeraBoletosSicredi(Pessoa pessoa, bool imprimirNaTela)
        {
            var linhasDigitaveis = new List<string>();

            // Autenticação
            if (!await _sicrediIntegration.AuthenticateAsync())
            {
                Console.WriteLine("Falha na autenticação.");
                return false;
            }

            foreach (ContaReceber contaReceber in _listaReceber)
            {
                // Alimentar dados do boleto
                var boletoRequest = _boletoService.AlimentarDadosBoleto(
                    pessoa,
                    contaReceber.ValorParcela,
                    contaReceber.Vencimento,
                    contaReceber.Documento,
                    contaReceber.Documento,
                    _boletoConfig
                );

                // Chamada para gerar o boleto
                var boletoResponse = await _sicrediIntegration.CreateBoletoAsync(boletoRequest);
                if (boletoResponse != null)
                {
                    Console.WriteLine("Boleto gerado com sucesso.");
                    contaReceber.BoletoGerado = true;
                    contaReceber.IdBoleto = boletoResponse.Txid;
                    contaReceber.Txid = boletoResponse.Txid;
                    contaReceber.QrCode = boletoResponse.QrCode;
                    contaReceber.NossoNumero = boletoResponse.NossoNumero;
                    contaReceber.CodigoBarras = boletoResponse.CodigoBarras;
                    contaReceber.LinhaDigitavel = boletoResponse.LinhaDigitavel;
                    Controller.getInstance().salvar(contaReceber);

                    linhasDigitaveis.Add(boletoResponse.LinhaDigitavel);
                }
                else
                {
                    Console.WriteLine("Falha ao gerar o boleto.");
                }
            }
            await Task.Delay(2000);
            // Chamar método para baixar e abrir PDFs
            if (linhasDigitaveis.Count > 0)
            {
                await _sicrediIntegration.DownloadAndOpenBoletoPdfs(linhasDigitaveis.ToArray(), imprimirNaTela);
                return true;
            }
            else
                return false;
        }

        // Novo método para passar diretamente as linhas digitáveis e chamar o PDF
        public async Task<bool> BaixarBoletosExistentesSicredi1(string[] linhasDigitaveis, bool imprimirNaTela)
        {
            if (linhasDigitaveis == null || linhasDigitaveis.Length == 0)
            {
                Console.WriteLine("Nenhuma linha digitável fornecida.");
                return false;
            }

            // Autenticação
            if (!await _sicrediIntegration.AuthenticateAsync())
            {
                Console.WriteLine("Falha na autenticação.");
                return false;
            }

            // Chamar o método de download e abertura dos boletos já existentes
            IList<string> listaBoleto = await _sicrediIntegration.DownloadAndOpenBoletoPdfs(linhasDigitaveis, imprimirNaTela);
            //if(imprimirNaTela == false)
            //{
            //    if(listaBoleto.Count > 0)
            //    {
            //        foreach (string caminhoBoleto in listaBoleto)
            //        {

            //        }
            //    }
            //}
            return true;
        }

        public async Task<IList<string>> BaixarBoletosExistentesSicredi(string[] linhasDigitaveis, bool imprimirNaTela)
        {
            // Lista de caminhos dos boletos para retorno
            IList<string> listaCaminhosBoletos = new List<string>();

            if (linhasDigitaveis == null || linhasDigitaveis.Length == 0)
            {
                Console.WriteLine("Nenhuma linha digitável fornecida.");
                return listaCaminhosBoletos; // Retorna a lista vazia
            }

            // Autenticação
            if (!await _sicrediIntegration.AuthenticateAsync())
            {
                Console.WriteLine("Falha na autenticação.");
                return listaCaminhosBoletos; // Retorna a lista vazia
            }

            // Chamar o método de download e abertura dos boletos já existentes
            listaCaminhosBoletos = await _sicrediIntegration.DownloadAndOpenBoletoPdfs(linhasDigitaveis, imprimirNaTela);

            // Retorna a lista de caminhos dos boletos
            return listaCaminhosBoletos;
        }

        public async Task<string> ConsultarBoletosLiquidadosPorDiaAsync(DateTime data)
        {
            RetornoBancoController retornoBancoController = new RetornoBancoController();
            string resultadoRetornado = "";
            // Formata a data para o formato esperado pela API
            string dia = data.ToString("dd/MM/yyyy");

            // Autenticação
            if (!await _sicrediIntegration.AuthenticateAsync())
            {
                Console.WriteLine("Falha na autenticação.");
                return null;
            }
            // Chama o método da classe SicrediIntegration
            var resultado = await _sicrediIntegration.ConsultarBoletosLiquidadosPorDiaAsync(dia);
            if (resultado != null)
            {
                int contagemBoletosBaixadosNoSistema = 0;
                decimal somaTaxaBancaria = 0;
                ContaReceberController contaReceberController = new ContaReceberController();
                foreach (var boleto in resultado)
                {
                    ContaReceber contaReceberLocalizada = new ContaReceber();
                    IList<ContaReceber> listaReceber = contaReceberController.selecionarContaReceberPorSql("From ContaReceber Tabela Where Tabela.NossoNumero = '"+ boleto.NossoNumero+ "' and Tabela.Recebido = false");
                    if(listaReceber.Count > 0)
                    {

                        foreach (ContaReceber contaReceber in listaReceber)
                        {
                            if (contaReceber.Recebido == false)
                            {
                                contaReceberLocalizada = contaReceber;
                                contaReceber.Recebido = true;
                                contaReceber.ValorRecebido = boleto.ValorLiquidado;
                                contaReceber.DataRecebimento = boleto.DataPagamento;
                                contaReceber.Juro = boleto.JurosLiquido;
                                contaReceber.Multa = boleto.MultaLiquida;
                                somaTaxaBancaria = somaTaxaBancaria + boleto.AbatimentoLiquido;
                                Controller.getInstance().salvar(contaReceber);

                                Caixa caixa = new Caixa();
                                caixa.Cobrador = null;
                                caixa.Conciliado = true;
                                caixa.Concluido = true;
                                caixa.ContaBancaria = contaReceber.ContaBoleto;
                                caixa.DataLancamento = contaReceber.DataRecebimento;
                                caixa.Descricao = "REC. DE BOLETO - SICREDI " + contaReceber.Documento + " " + contaReceber.Cliente.RazaoSocial;
                                caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                caixa.FormaPagamento = contaReceber.FormaPagamento;
                                caixa.IdOrigem = contaReceber.Id.ToString();
                                caixa.Pessoa = contaReceber.Cliente;
                                caixa.PlanoConta = contaReceber.PlanoConta;
                                caixa.TabelaOrigem = "CONTARECEBER";
                                caixa.Tipo = "E";
                                caixa.Usuario = Sessao.usuarioLogado;
                                caixa.Valor = contaReceber.ValorRecebido;
                                caixa.Observacoes = "RETORNO AUTOMÁTICO BANCO, PUXADO EM: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                                Controller.getInstance().salvar(caixa);

                                //Se tem abatimento ja lanca no caixa
                                if (boleto.AbatimentoLiquido > 0)
                                {
                                    caixa = new Caixa();
                                    caixa.Cobrador = null;
                                    caixa.Conciliado = true;
                                    caixa.Concluido = true;
                                    caixa.ContaBancaria = contaReceber.ContaBoleto;
                                    caixa.DataLancamento = contaReceber.DataRecebimento;
                                    caixa.Descricao = "TARIFA DE BOLETO - SICREDI " + contaReceber.Documento + " " + contaReceber.Cliente.RazaoSocial;
                                    caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                    caixa.FormaPagamento = contaReceber.FormaPagamento;
                                    caixa.IdOrigem = contaReceber.Id.ToString();
                                    caixa.Pessoa = contaReceber.Cliente;
                                    BoletoConfigController boletoConfigController = new BoletoConfigController();
                                    IList<BoletoConfig> listaBoletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancaria(contaReceber.ContaBoleto.Id);
                                    if (listaBoletoConfig.Count > 0)
                                    {
                                        foreach (BoletoConfig boletoConfig in listaBoletoConfig)
                                        {
                                            if (boletoConfig.PlanoContaTarifa != null)
                                                caixa.PlanoConta = boletoConfig.PlanoContaTarifa;
                                            else
                                                caixa.PlanoConta = null;
                                        }
                                    }
                                    caixa.TabelaOrigem = "CONTARECEBER";
                                    caixa.Tipo = "S";
                                    caixa.Usuario = Sessao.usuarioLogado;
                                    caixa.Valor = boleto.AbatimentoLiquido;
                                    caixa.Observacoes = "TARIFA DE BOLETO, PUXADO EM: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                                    Controller.getInstance().salvar(caixa);
                                }
                                contagemBoletosBaixadosNoSistema++;
                            }
                        }
                    }

                    RetornoBanco retornoBanco = retornoBancoController.selecionarRetornoPorNossoNumero(boleto.NossoNumero);
                    // Se não encontrar um retorno, cria um novo objeto
                    if (retornoBanco == null)
                    {
                        retornoBanco = new RetornoBanco();
                    }
                    retornoBanco.AbatimentoLiquido = boleto.AbatimentoLiquido;
                    retornoBanco.CodigoBeneficiario = boleto.CodigoBeneficiario;
                    retornoBanco.Cooperativa = boleto.Cooperativa;
                    retornoBanco.CooperativaPostoBeneficiario = boleto.CooperativaPostoBeneficiario;
                    retornoBanco.DataPagamento = boleto.DataPagamento;
                    retornoBanco.DescontoLiquido = boleto.DescontoLiquido;
                    retornoBanco.Descricao = "RET.SICREDI";
                    retornoBanco.JurosLiquido = boleto.JurosLiquido;
                    retornoBanco.MultaLiquida = boleto.MultaLiquida;
                    retornoBanco.NossoNumero = boleto.NossoNumero;
                    retornoBanco.SeuNumero = boleto.SeuNumero;
                    retornoBanco.TipoCarteira = boleto.TipoCarteira;
                    retornoBanco.TipoLiquidacao = boleto.TipoLiquidacao;
                    retornoBanco.Valor = boleto.Valor;
                    retornoBanco.ValorLiquidado = boleto.ValorLiquidado;
                    if (contaReceberLocalizada != null)
                    {
                        if (contaReceberLocalizada.Id > 0)
                        {
                            retornoBanco.ContaReceber = contaReceberLocalizada;
                            retornoBanco.Descricao = "RET.SICREDI " + contaReceberLocalizada.Cliente.RazaoSocial;
                        }
                    }
                    Controller.getInstance().salvar(retornoBanco);
                }
                if(somaTaxaBancaria > 0)
                {
                    GenericaDesktop.ShowInfo("Valor das taxas dos boletos baixados hj " + somaTaxaBancaria.ToString("C"));
                }
                resultadoRetornado = resultado.Count.ToString();
            }
            else
            {
                GenericaDesktop.ShowInfo("Nenhuma baixa localixada");
                return null;
            }
            return resultadoRetornado;
        }

    }
}
