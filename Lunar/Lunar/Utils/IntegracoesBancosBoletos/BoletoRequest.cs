using LunarBase.Classes;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;

namespace Lunar.Utils.IntegracoesBancosBoletos
{
    public class BoletoRequest
    {
        // Headers
        public string XApiKey { get; set; }  // 1. x-api-key
        public string Authorization { get; set; } // 2. Authorization
        public string ContentType { get; set; } = "application/json"; // 3. Content-Type
        public string Cooperativa { get; set; } // 4. cooperativa
        public string Posto { get; set; } // 5. posto

        // Body
        public string TipoCobranca { get; set; } // 6. tipoCobranca
        public string CodigoBeneficiario { get; set; } // 7. codigoBeneficiario
        public Pagador Pagador { get; set; } // 8. pagador
        public string TipoPessoa { get; set; } // 9. tipoPessoa
        public string Documento { get; set; } // 10. documento
        public string Nome { get; set; } // 11. nome
        public string Endereco { get; set; } // 12. endereco
        public string Cidade { get; set; } // 13. cidade
        public string Uf { get; set; } // 14. uf
        public string Cep { get; set; } // 15. cep
        public string Telefone { get; set; } // 16. telefone
        public string Email { get; set; } // 17. email
        public BeneficiarioFinal BeneficiarioFinal { get; set; } // 18. beneficiarioFinal
        public string EspecieDocumento { get; set; } // 29. especieDocumento
        public string NossoNumero { get; set; } // 30. nossoNumero
        public string SeuNumero { get; set; } // 31. seuNumero
        public DateTime DataVencimento { get; set; } // 32. dataVencimento
        public int? DiasProtestoAuto { get; set; } // 33. diasProtestoAuto
        public int? DiasNegativacaoAuto { get; set; } // 34. diasNegativacaoAuto
        public int? ValidadeAposVencimento { get; set; } // 35. validadeAposVencimento
        public decimal Valor { get; set; } // 36. valor
        public string TipoDesconto { get; set; } // 37. tipoDesconto
        public decimal? ValorDesconto1 { get; set; } // 38. valorDesconto1
        public DateTime? DataDesconto1 { get; set; } // 39. dataDesconto1
        public decimal? ValorDesconto2 { get; set; } // 40. valorDesconto2
        public DateTime? DataDesconto2 { get; set; } // 41. dataDesconto2
        public decimal? ValorDesconto3 { get; set; } // 42. valorDesconto3
        public DateTime? DataDesconto3 { get; set; } // 43. dataDesconto3
        public decimal? DescontoAntecipado { get; set; } // 44. descontoAntecipado
        public string TipoJuros { get; set; } // 45. tipoJuros
        public decimal? Juros { get; set; } // 46. juros
        public decimal? Multa { get; set; } // 47. multa
        public List<string> Informativo { get; set; } // 48. informativo
        public List<string> Mensagem { get; set; } // 49. mensagem
        public List<string> SplitBoleto { get; set; } // 50. splitBoleto
        public string RegraRateio { get; set; } // 51. regraRateio
        public string RepasseAutomaticoSplit { get; set; } // 52. repasseAutomaticoSplit
        public string TipoValorRateio { get; set; } // 53. tipoValorRateio
        public List<Destinatario> Destinatarios { get; set; } // 54. destinatarios

    }

    public class Pagador
    {
        public string TipoPessoa { get; set; } // 8. tipoPessoa
        public string Documento { get; set; } // 9. documento
        public string Nome { get; set; } // 10. nome
        public string Endereco { get; set; } // 11. endereco
        public string Cidade { get; set; } // 12. cidade
        public string Uf { get; set; } // 13. uf
        public string Cep { get; set; } // 14. cep
        public string Telefone { get; set; } // 15. telefone
        public string Email { get; set; } // 16. email
    }

    public class BeneficiarioFinal
    {
        public string Documento { get; set; } // 18. documento
        public string TipoPessoa { get; set; } // 19. tipoPessoa
        public string Nome { get; set; } // 20. nome
        public string Logradouro { get; set; } // 21. logradouro
        public string NumeroEndereco { get; set; } // 23. numeroEndereco
        public string Cidade { get; set; } // 24. cidade
        public string Uf { get; set; } // 25. uf
        public string Cep { get; set; } // 26. cep
        public string Telefone { get; set; } // 27. telefone
        public string Email { get; set; } // 28. email
    }

    public class Destinatario
    {
        public string CodigoAgencia { get; set; } // 55. codigoAgencia
        public string CodigoBanco { get; set; } // 56. codigoBanco
        public int FloatSplit { get; set; } // 57. floatSplit
        public string NomeDestinatario { get; set; } // 58. nomeDestinatario
        public string NumeroContaCorrente { get; set; } // 59. numeroContaCorrente
        public string NumeroCpfCnpj { get; set; } // 60. numeroCpfCnpj
    }
    public class BoletoService
    {
        public BoletoRequest AlimentarDadosBoleto(Pessoa pessoa, decimal valor, DateTime vencimento, string numeroDocumento, string nossoNumero)
        {
            // Criar o pagador a partir do objeto pessoa
            var pagador = new Pagador
            {
                TipoPessoa = pessoa.TipoPessoa == "PF" ? "PESSOA_FISICA" : "PESSOA_JURIDICA", // Definir o tipo de pessoa (Física ou Jurídica)
                Documento = GenericaDesktop.RemoveCaracteres(pessoa.Cnpj),  // Remover caracteres especiais do CNPJ ou CPF
                Nome = pessoa.RazaoSocial,  // Nome ou Razão Social do pagador
                Endereco = pessoa.EnderecoPrincipal.Logradouro + ", " + pessoa.EnderecoPrincipal.Numero + (string.IsNullOrEmpty(pessoa.EnderecoPrincipal.Complemento) ? "" : ", " + pessoa.EnderecoPrincipal.Complemento),
                Cidade = pessoa.EnderecoPrincipal.Cidade.Descricao,
                Uf = pessoa.EnderecoPrincipal.Cidade.Estado.Uf,
                Cep = GenericaDesktop.RemoveCaracteres(pessoa.EnderecoPrincipal.Cep)
                //Telefone = GenericaDesktop.RemoveCaracteres(pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone),  // Caso tenha telefone
                //Email = pessoa.Email  // Caso tenha email
            };

            // Criar os dados do beneficiário final
            var beneficiarioFinal = new BeneficiarioFinal
            {
                Documento = Sessao.empresaFilialLogada.Cnpj,  // CNPJ do beneficiário
                TipoPessoa = pessoa.TipoPessoa == "PF" ? "PESSOA_FISICA" : "PESSOA_JURIDICA", // Tipo de pessoa (J - Jurídica, F - Física)
                Nome = Sessao.empresaFilialLogada.RazaoSocial,
                Logradouro = Sessao.empresaFilialLogada.Endereco.Logradouro,
                NumeroEndereco = Sessao.empresaFilialLogada.Endereco.Numero,
                Cidade = Sessao.empresaFilialLogada.Endereco.Cidade.Descricao,
                Uf = Sessao.empresaFilialLogada.Endereco.Cidade.Estado.Uf,
                Cep = Sessao.empresaFilialLogada.Endereco.Cep,
                Telefone = Sessao.empresaFilialLogada.DddPrincipal + GenericaDesktop.RemoveCaracteres(Sessao.empresaFilialLogada.TelefonePrincipal),  // Telefone do beneficiário
                Email = Sessao.empresaFilialLogada.Email,
                
            };

            var boletoRequest = new BoletoRequest
            {
                //XApiKey = "58ae06aa-759c-4e27-b9da-46be855eb3aa", 
                //Authorization = "Bearer 58ae06aa-759c-4e27-b9da-46be855eb3aa",  
                //Cooperativa = "6789", 
                //Posto = "03",  

                TipoCobranca = "NORMAL",  // Tipo de cobrança NORMAL OU HIBRIDO COM QRCODE
                CodigoBeneficiario = "12345", // 5 digitos para o sicredi  
                Pagador = pagador,  // Informações do pagador
                BeneficiarioFinal = beneficiarioFinal,  // Informações do beneficiário final
                Valor = valor,  // Valor do boleto
                DataVencimento = vencimento,  // Data de vencimento do boleto
                SeuNumero = numeroDocumento,  // Número do documento
                //NossoNumero = nossoNumero,  // Nosso número do boleto
                EspecieDocumento = "NOTA_PROMISSORIA",  // Tipo de documento
                //DiasProtestoAuto = 5,  // Dias automáticos para protesto (se houver)
                //DiasNegativacaoAuto = 10,  // Dias automáticos para negativação (se houver)
                //ValidadeAposVencimento = 30,  // Validade após vencimento
                //TipoDesconto = "sem desconto",  // Tipo de desconto (se aplicável)
                //ValorDesconto1 = '',
                //DataDesconto1 = "",
                TipoJuros = "PERCENTUAL",  //A - VALOR ou B - PERCENTUAL
                Juros = 1,
                Multa = 2,
                Informativo = new List<string> { "Pagamento preferencialmente na rede bancária ou correspondentes autorizados" },  // Informações adicionais
            };

            return boletoRequest;
        }
        public class BoletoResponse
        {
            public string Txid { get; set; }
            public string QrCode { get; set; }
            public string LinhaDigitavel { get; set; }
            public string CodigoBarras { get; set; }
            public string Cooperativa { get; set; }
            public string Posto { get; set; }
            public string NossoNumero { get; set; }
        }
    }

}