using LunarBase.Classes;
using LunarBase.ControllerBO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LunarBase.Utilidades.NFSe
{
    public class NFSeFocusService
    {
        private FocusNFSeClient client;
        private TimeSpan timeout = TimeSpan.FromMinutes(2);
        private TimeSpan pollingInterval = TimeSpan.FromSeconds(8);

        public NFSeFocusService(bool ambienteProducao)
        {
            // Definir client com base no ambiente
            if (ambienteProducao)
            {
                client = new FocusNFSeClient("xhYFolRs9uanlrt57NakIlbgvOdn8pjM", false); // Chave produção
            }
            else
            {
                client = new FocusNFSeClient("SeI85jiSMPTjLG5KriiT81ty4aYzXs15", true);  // Chave homologação
            }
        }

        public async Task EmitirNFSe(OrdemServico ordemServico)
        {
            DateTime startTime = DateTime.Now;
            NFSeResponse nfseResponse = null;

            // Validação de Ordem de Serviço
            if (!ValidarDadosOrdemServicoECliente(ordemServico))
            {
                return;
            }

            // Criar os dados da NFSe
            var nfse = CriarNFSeRequest(ordemServico);

            // Referência para a NFSe
            var referencia = Sessao.empresaFilialLogada.Cnpj + ordemServico.Id;

            // Enviar a NFSe
            var respostaCriacao = await client.CriarNFSeAsync(referencia, nfse);
            Console.WriteLine("Resposta Criação: " + respostaCriacao);

            // Aguardar 8 segundos antes de iniciar o polling
            await Task.Delay(pollingInterval);

            // Consultar o status até obter um resultado final ou atingir o timeout
            while (DateTime.Now - startTime < timeout)
            {
                var respostaConsulta = await client.ConsultarNFSeAsync(referencia);
                nfseResponse = JsonConvert.DeserializeObject<NFSeResponse>(respostaConsulta);
                Console.WriteLine("Resposta Consulta: " + respostaConsulta);
                nfseResponse = JsonConvert.DeserializeObject<NFSeResponse>(respostaConsulta);
                // Se não estiver mais processando, sair do loop
                if (!nfseResponse.status.Equals("processando_autorizacao"))
                {
                    break;
                }

                // Aguardar o próximo intervalo de polling
                await Task.Delay(pollingInterval);
            }

            // Tratar o resultado da consulta
            ProcessarResposta(nfseResponse, ordemServico);
        }

        private NFSeRequest CriarNFSeRequest(OrdemServico ordemServico)
        {
            // Aqui vai a lógica para criar o objeto NFSeRequest com base na OrdemServico
            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            var listaServico = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);
            decimal valorTotalServico = listaServico.Sum(s => s.ValorTotal);
            string descricaoServico = string.Join(" / ", listaServico.Select(s => s.DescricaoServico));
            string aliquotaIssString = listaServico.First().Servico.AliquotaIss;
            decimal? aliquotaServico = null;
            if (!string.IsNullOrEmpty(aliquotaIssString))
            {
                if (decimal.TryParse(aliquotaIssString, out decimal parsedAliquota))
                {
                    aliquotaServico = parsedAliquota == 0 ? (decimal?)null : parsedAliquota;
                }
            }

            string itemListaServico = listaServico.First().Servico.CodigoServicoNfse;

            string cpf = null;
            string cnpj = null;
            if (ordemServico.Cliente.Cnpj.Length == 11)
                cpf = ordemServico.Cliente.Cnpj;
            
            else if (ordemServico.Cliente.Cnpj.Length == 14)
                cnpj = ordemServico.Cliente.Cnpj;
            

            return new NFSeRequest
            {
                data_emissao = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                natureza_operacao = "1",
                optante_simples_nacional = true,
                prestador = new Prestador
                {
                    cnpj = Sessao.empresaFilialLogada.Cnpj,
                    inscricao_municipal = Sessao.empresaFilialLogada.InscricaoMunicipal,
                    codigo_municipio = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge
                },
                tomador = new Tomador
                {
                    cpf = cpf,
                    cnpj = cnpj,
                    razao_social = ordemServico.Cliente.RazaoSocial,
                    email = string.IsNullOrWhiteSpace(ordemServico.Cliente.Email) ? null : ordemServico.Cliente.Email,
                    endereco = new EnderecoNFs
                    {
                        logradouro = ordemServico.Cliente.EnderecoPrincipal.Logradouro,
                        numero = ordemServico.Cliente.EnderecoPrincipal.Numero,
                        complemento = string.IsNullOrEmpty(ordemServico.Cliente.EnderecoPrincipal.Complemento)
                            ? null : ordemServico.Cliente.EnderecoPrincipal.Complemento,
                        bairro = ordemServico.Cliente.EnderecoPrincipal.Bairro,
                        codigo_municipio = ordemServico.Cliente.EnderecoPrincipal.Cidade.Ibge,
                        uf = ordemServico.Cliente.EnderecoPrincipal.Cidade.Estado.Uf,
                        cep = Generica.RemoveCaracteres(ordemServico.Cliente.EnderecoPrincipal.Cep)
                    }
                },
                servico = new ServicoNFS
                {
                    valor_servicos = valorTotalServico,
                    valor_deducoes = 0m,
                    valor_pis = 0m,
                    valor_cofins = 0m,
                    valor_inss = 0m,
                    valor_ir = 0m,
                    valor_csll = 0m,
                    iss_retido = false,
                    valor_iss = 0m,
                    valor_iss_retido = 0m,
                    outras_retencoes = 0m,
                    base_calculo = 0m,
                    aliquota = aliquotaServico,
                    desconto_incondicionado = 0m,
                    desconto_condicionado = 0m,
                    item_lista_servico = itemListaServico,
                    codigo_cnae = Sessao.empresaFilialLogada.Cnae,
                    discriminacao = descricaoServico,
                    codigo_municipio = Sessao.empresaFilialLogada.Endereco.Cidade.Ibge,
                    //percentual_total_tributos = aliquotaServico,
                    fonte_total_tributos = "IBPT"
                }
            };
        }

        private void ProcessarResposta(NFSeResponse nfseResponse, OrdemServico ordemServico)
        {
            salvarNfse(ordemServico, nfseResponse);

            if (nfseResponse == null || nfseResponse.status.Equals("processando_autorizacao"))
            {
                Generica.ShowAlerta("Timeout atingido. A NFSe ainda está processando.");
            }
            else if (nfseResponse.status == "autorizado")
            {
                Generica.ShowInfo("NFSe autorizada com sucesso!");
                if (!String.IsNullOrEmpty(nfseResponse.url_danfse))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = nfseResponse.url_danfse,
                        UseShellExecute = true
                    });
                }
                else if (!String.IsNullOrEmpty(nfseResponse.url))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = nfseResponse.url,
                        UseShellExecute = true
                    });
                }
                Console.WriteLine($"Código de Verificação: {nfseResponse.codigo_verificacao}");
                Console.WriteLine($"URL da Nota: {nfseResponse.url}");
            }
            else if (nfseResponse.status == "rejeitado")
            {
                Generica.ShowErro("NFSe foi rejeitada.");
            }
            else
            {
                string erros = nfseResponse.erros != null && nfseResponse.erros.Count > 0
                    ? string.Join("\n", nfseResponse.erros.Select(e => $"Código: {e.codigo}, Mensagem: {e.mensagem}"))
                    : "Nenhum erro encontrado";

                Generica.ShowAlerta($"Status da NFSe: {nfseResponse.status}. \n\nErros:\n{erros}");
            }
        }

        private void salvarNfse(OrdemServico ordemServico, NFSeResponse nfseResponse)
        {
            Nfse nfse = new Nfse();
            nfse.Referencia = nfseResponse.referencia;
            nfse.CaminhoXml = nfseResponse.caminho_xml_nota_fiscal;
            nfse.CodigoVerificacao = nfseResponse.codigo_verificacao;
            nfse.DataEmissao = nfseResponse.data_emissao;
            nfse.Numero = int.Parse(nfseResponse.numero);
            nfse.NumeroRps = nfseResponse.numero_rps;
            nfse.OrdemServico = ordemServico;
            nfse.SerieRps = nfseResponse.serie_rps;
            nfse.Status = nfseResponse.status;
            nfse.Url = nfseResponse.url;
            nfse.UrlDanfe = nfseResponse.url_danfse;
            Controller.getInstance().salvar(nfse);
        }

        public bool ValidarDadosOrdemServicoECliente(OrdemServico ordemServico)
        {
            // Verificar se a ordem de serviço é nula
            if (ordemServico == null)
            {
                Generica.ShowAlerta("Ordem de serviço é inválida.");
                return false;
            }

            // Verificar dados do cliente
            var cliente = ordemServico.Cliente;
            if (cliente == null)
            {
                Generica.ShowAlerta("Cliente não pode ser nulo.");
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Cnpj))
            {
                Generica.ShowAlerta("CPF ou CNPJ do cliente é obrigatório.");
                return false;
            }

            if (string.IsNullOrEmpty(cliente.RazaoSocial))
            {
                Generica.ShowAlerta("Razão social do cliente é obrigatória.");
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Email))
            {
                Generica.ShowAlerta("E-mail do cliente é obrigatório.");
                return false;
            }

            // Verificar endereço do cliente
            var endereco = cliente.EnderecoPrincipal;
            if (endereco == null)
            {
                Generica.ShowAlerta("Endereço do cliente é obrigatório.");
                return false;
            }

            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                Generica.ShowAlerta("Logradouro do endereço é obrigatório.");
                return false;
            }

            if (string.IsNullOrEmpty(endereco.Numero))
            {
                Generica.ShowAlerta("Número do endereço é obrigatório.");
                return false;
            }

            if (string.IsNullOrEmpty(endereco.Bairro))
            {
                Generica.ShowAlerta("Bairro do endereço é obrigatório.");
                return false;
            }

            if (endereco.Cidade == null || string.IsNullOrEmpty(endereco.Cidade.Ibge))
            {
                Generica.ShowAlerta("Código IBGE da cidade é obrigatório.");
                return false;
            }

            if (endereco.Cidade.Estado == null || string.IsNullOrEmpty(endereco.Cidade.Estado.Uf))
            {
                Generica.ShowAlerta("UF do estado é obrigatório.");
                return false;
            }

            if (string.IsNullOrEmpty(endereco.Cep))
            {
                Generica.ShowAlerta("CEP é obrigatório.");
                return false;
            }

            // Validar serviços da ordem de serviço
            OrdemServicoServicoController ordemServicoServicoController = new OrdemServicoServicoController();
            IList<OrdemServicoServico> listaServico = ordemServicoServicoController.selecionarServicosPorOrdemServico(ordemServico.Id);

            if (listaServico == null || listaServico.Count == 0)
            {
                Generica.ShowAlerta("A ordem de serviço não possui serviços cadastrados.");
                return false;
            }

            decimal valorTotalServico = 0m;
            foreach (var servico in listaServico)
            {
                if (string.IsNullOrEmpty(servico.DescricaoServico))
                {
                    Generica.ShowAlerta("Descrição do serviço é obrigatória.");
                    return false;
                }
                if (string.IsNullOrEmpty(servico.Servico.AliquotaIss))
                {
                    servico.Servico.AliquotaIss = "0";
                    //Generica.ShowAlerta("Aliquota do serviço é obrigatória.");
                    //return false;
                }
                if (string.IsNullOrEmpty(servico.Servico.CodigoServicoNfse))
                {
                    Generica.ShowAlerta("Codigo Servico Nfse do serviço é obrigatória. Exemplo: 0107, na tela de cadastro de serviços!");
                    return false;
                }

                valorTotalServico += servico.ValorTotal;
            }

            if (valorTotalServico <= 0)
            {
                Generica.ShowAlerta("O valor total do serviço deve ser maior que zero.");
                return false;
            }

            // Validar dados da empresa (prestador)
            var empresa = Sessao.empresaFilialLogada;
            if (empresa == null)
            {
                Generica.ShowAlerta("Empresa não está logada.");
                return false;
            }

            if (string.IsNullOrEmpty(empresa.Cnpj))
            {
                Generica.ShowAlerta("CNPJ da empresa é obrigatório.");
                return false;
            }
            if (String.IsNullOrEmpty(empresa.Cnae))
            {
                Generica.ShowAlerta("Cnae da sua empresa não pode ser nulo. (Utilitário/Cadastro Filial)");
                return false;
            }
            if (String.IsNullOrEmpty(empresa.InscricaoMunicipal))
            {
                Generica.ShowAlerta("Inscrição Municipal da sua empresa não pode ser nulo. (Utilitário/Cadastro Filial)");
                return false;
            }

            if (string.IsNullOrEmpty(empresa.InscricaoEstadual))
            {
                Generica.ShowAlerta("Inscrição municipal da empresa é obrigatória.");
                return false;
            }

            if (empresa.Endereco == null || empresa.Endereco.Cidade == null || string.IsNullOrEmpty(empresa.Endereco.Cidade.Ibge))
            {
                Generica.ShowAlerta("Código IBGE do município da empresa é obrigatório.");
                return false;
            }

            // Se todas as validações passarem, retorna true
            return true;
        }

    }
}
