using Lunar.Telas.ParametroDoSistema;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static Lunar.Utils.GalaxyPay_API.CustomerEdicao;
using static Lunar.Utils.GalaxyPay_API.CustomerRetorno;
using static Lunar.Utils.GalaxyPay_API.GalaxPay_RetornoPixGerado;
using static Lunar.Utils.GalaxyPay_API.GalaxyPay_RetornoStatusBoletos;
using static Lunar.Utils.GalaxyPay_API.RetornoBoletoGerado;

namespace Lunar.Utils.GalaxyPay_API
{

    public class GalaxyPayApiIntegracao
    {
        //Cliente
        string galaxId = Sessao.parametroSistema.IdGalaxyPay;
        string galaxHash = Sessao.parametroSistema.TokenGalaxyPay;

        //TXT Parceiro
        string galaxIdParceiro = "39087";
        string galaxHashParceiro = "7eZjPxC2M8RdIdTe2rPcXj0o0lBd6hYc544c0fYe";

        string tokenAcesso = "";

        string galaxPayId_Customer = "";
        public string GalaxyPay_TokenAcesso()
        {
            try
            {
                String url = "https://api.galaxpay.com.br/v2/token";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Basic " + EncodeToBase64(galaxId + ":" + galaxHash));
                requisicaoWeb.Headers.Add("AuthorizationPartner", "Basic " + EncodeToBase64(galaxIdParceiro + ":" + galaxHashParceiro));


                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        grant_type = "authorization_code",
                        scope = "customers.read customers.write plans.read plans.write transactions.read transactions.write webhooks.write cards.read cards.write card-brands.read subscriptions.read subscriptions.write charges.read charges.write boletos.read carnes.read"
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string tokenRetornado = "";
                    if (retorno != null)
                    {
                        tokenRetornado = retorno["access_token"].ToString();
                        tokenAcesso = tokenRetornado;
                    }
                    return tokenRetornado;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao solicitar Token Galaxy Pay: " + err.Message);
                return "";
            }
        }

        static public string EncodeToBase64(string texto)
        {
            try
            {
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GalaxyPay_ListarCliente(string cpfCnpj, Pessoa pessoa)
        {
            try
            {
                String url = "https://api.galaxpay.com.br/v2/customers?documents=" + cpfCnpj + "&startAt=0&limit=100";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);

                var httpResponse = (HttpWebResponse)await requisicaoWeb.GetResponseAsync(); // Use GetResponseAsync para uma operação assíncrona
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync(); // Use ReadToEndAsync para uma operação assíncrona
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string totalQtdFoundInPage = "";

                    if (retorno != null)
                    {
                        try
                        {
                            totalQtdFoundInPage = retorno["totalQtdFoundInPage"].ToString();
                        }
                        catch
                        {
                            totalQtdFoundInPage = "";
                        }

                        if (totalQtdFoundInPage.Equals("0"))
                        {
                            // Use await para aguardar a conclusão do método assíncrono GalaxyPay_CadastrarCliente
                            string retornoCadastro = await GalaxyPay_CadastrarCliente(pessoa);

                            if (retornoCadastro.Equals("True"))
                            {
                                totalQtdFoundInPage = "1";
                            }
                        }
                        else if (totalQtdFoundInPage.Equals("1"))
                        {
                            string retornoEdicao = GalaxyPay_EditarCliente(pessoa);
                            var customerGalaxyPay = JsonConvert.DeserializeObject<CustomerEdit>(result);
                            galaxPayId_Customer = customerGalaxyPay.Customers[0].galaxPayId.ToString();
                            GenericaDesktop.gravarLinhaLog("GALAXY PAY - " + pessoa.RazaoSocial + " " + pessoa.Id, "GALAXY PAY - ATUALIZAÇÃO DE CLIENTE");
                        }
                        else
                            totalQtdFoundInPage = "0";
                    }
                    return totalQtdFoundInPage;
                }
            }
            catch (HttpRequestException err)
            {
                GenericaDesktop.ShowAlerta("Erro ao localizar cliente no sistema Galaxy Pay: " + err.Message);
                return "";
            }
        }
        public async Task<string> GalaxyPay_CadastrarCliente(Pessoa pessoa)
        {
            try
            {
                Address address = new Address();
                string url = "https://api.galaxpay.com.br/v2/customers";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenAcesso);

                string[] arrayEmail = !string.IsNullOrEmpty(pessoa.Email)
                    ? new string[] { pessoa.Email }
                    : new string[] { pessoa.Cnpj + "@email.com.br" };

                string[] arrayFone = !string.IsNullOrEmpty(pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone)
               ? new string[] { pessoa.PessoaTelefone.Ddd + pessoa.PessoaTelefone.Telefone }
               : new string[] { pessoa.Cnpj.Substring(0,11) };

                if (pessoa.EnderecoPrincipal != null)
                {
                    address.zipCode = GenericaDesktop.RemoveCaracteres(pessoa.EnderecoPrincipal.Cep);
                    address.street = pessoa.EnderecoPrincipal.Logradouro.Trim();
                    address.number = pessoa.EnderecoPrincipal.Numero;
                    address.complement = pessoa.EnderecoPrincipal.Complemento;
                    address.neighborhood = pessoa.EnderecoPrincipal.Bairro;
                    address.city = pessoa.EnderecoPrincipal.Cidade.Descricao;
                    address.state = pessoa.EnderecoPrincipal.Cidade.Estado.Uf;
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Informe um endereço no cadastro do cliente!");
                    return "";
                }

                var requestContent = new
                {
                    myId = pessoa.Id,
                    name = pessoa.RazaoSocial,
                    document = pessoa.Cnpj,
                    emails = arrayEmail,
                    phones = arrayFone,
                    Address = address
                };

                var json = JsonConvert.SerializeObject(requestContent);
                var response = await httpClient.PostAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var customerGalaxyPay = JsonConvert.DeserializeObject<CustomerGalaxyPay>(responseContent);

                    if (customerGalaxyPay != null)
                    {
                        if (customerGalaxyPay.Customer.galaxPayId > 0)
                        {
                            galaxPayId_Customer = customerGalaxyPay.Customer.galaxPayId.ToString();
                            return "Sucesso"; // Ou qualquer outro indicador de sucesso}
                        }
                    }
                }

                GenericaDesktop.ShowAlerta("Falha ao Cadastrar Cliente na Sistema da Galaxy Pay, Não Foi Possível gerar o QRCODE: " + response.ReasonPhrase);
                return "";
            }
            catch (HttpRequestException ex)
            {
                GenericaDesktop.ShowErro("Falha na requisição HTTP: " + ex.Message);
                return "";
            }
            catch (Exception ex)
            {
                // Registre a exceção para fins de depuração.
                GenericaDesktop.gravarLinhaLog("Exceção não tratada: " + ex.ToString(), "GALAXYPAY_CADASTROCLIENTE");
                GenericaDesktop.ShowErro("Ocorreu um erro inesperado: " + ex.Message);
                return "";
            }
        }

        public string GalaxyPay_EditarCliente(Pessoa pessoa)
        {
            try
            {
                Address address = new Address();
                String url = "https://api.galaxpay.com.br/v2/customers/" + pessoa.Id + "/myId";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "PUT";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);
                if (String.IsNullOrEmpty(pessoa.Email))
                {
                   pessoa.Email = "email@email.com.br";
                }
                string[] arrayEmail = new string[1];
                arrayEmail[0] = pessoa.Email;
                if (pessoa.EnderecoPrincipal != null)
                {
                    address.zipCode = GenericaDesktop.RemoveCaracteres(pessoa.EnderecoPrincipal.Cep);
                    address.street = pessoa.EnderecoPrincipal.Logradouro.Trim();
                    address.number = pessoa.EnderecoPrincipal.Numero;
                    address.complement = pessoa.EnderecoPrincipal.Complemento;
                    address.neighborhood = pessoa.EnderecoPrincipal.Bairro;
                    address.city = pessoa.EnderecoPrincipal.Cidade.Descricao;
                    address.state = pessoa.EnderecoPrincipal.Cidade.Estado.Uf;
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Informe um endereço no cadastro do cliente!");
                    return "";
                }

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        myId = pessoa.Id,
                        name = pessoa.RazaoSocial,
                        document = pessoa.Cnpj,
                        emails = arrayEmail,
                        Address = address
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string retornoString = "";
                    if (retorno != null)
                        retornoString = retorno["type"].ToString();
                    return retornoString;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao atualizar cliente: " + err.Message);
                return "";
            }
        }

        public string GalaxyPay_GerarBoleto(Pessoa pessoa, ContaReceber contaReceber)
        {
            int contagemOk = 0;
            try
            {
                string retornoBoletosValidos = "";
                Address address = new Address();
                String url = "https://api.galaxpay.com.br/v2/charges";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);
                if(String.IsNullOrEmpty(pessoa.Email))
                    pessoa.Email = "email@email.com.br";
                string[] arrayEmail = new string[1];
                arrayEmail[0] = pessoa.Email;

                Charges charges = new Charges();
                charges.myId = contaReceber.Id.ToString();
                charges.value = int.Parse(contaReceber.ValorParcela.ToString("F").Replace(",", ""));
                charges.payday = contaReceber.Vencimento.ToString("yyyy-MM-dd");
                charges.payedOutsideGalaxPay = false; // paga fora do sistema?
                charges.mainPaymentMethodId = "boleto";
                //Cliente
                charges.Customer = new Customer();
                //alteracao para verificar
                charges.Customer.galaxPayId = int.Parse(galaxPayId_Customer);
                charges.Customer.name = pessoa.RazaoSocial;
                charges.Customer.document = GenericaDesktop.RemoveCaracteres(pessoa.Cnpj);
                charges.Customer.emails = arrayEmail;
                //Boleto
                charges.PaymentMethodBoleto = new Paymentmethodboleto();
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Multa.ToString()))
                    charges.PaymentMethodBoleto.fine = int.Parse(Sessao.parametroSistema.Multa.ToString().Replace(".", ""));
                if (!String.IsNullOrEmpty(Sessao.parametroSistema.Juro.ToString()))
                    charges.PaymentMethodBoleto.interest = int.Parse(Sessao.parametroSistema.Juro.ToString().Replace(".", ""));
                charges.PaymentMethodBoleto.deadlineDays = 59;

                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        myId = charges.myId,
                        value = charges.value,
                        payday = charges.payday,
                        payedOutsideGalaxPay = charges.payedOutsideGalaxPay,
                        mainPaymentMethodId = charges.mainPaymentMethodId,
                        Customer = charges.Customer,
                        PaymentMethodBoleto = charges.PaymentMethodBoleto
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    var ret = JsonConvert.DeserializeObject<RetBoleto>(result);
                    string retornoString = "";

                    if (ret != null)
                    {
                        retornoString = ret.type.ToString();
                        if (retornoString.Equals("True"))
                        {
                            contaReceber.IdBoleto = ret.Charge.galaxPayId.ToString();
                            contaReceber.BoletoGerado = true;
                            Controller.getInstance().salvar(contaReceber);
                            contagemOk++;
                        }
                    }
                }
                return contagemOk.ToString();
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao Gerar Boleto no Sistema da Galaxy Pay: " + err.Message);
                return contagemOk.ToString();
            }
        }


        public async Task<string> GalaxyPay_GerarPix(string tabelaOrigem, string idOrigem, decimal valorPix, Pessoa pessoa)
        {
            int contagemOk = 0;
            string retornoString = "";

            GalaxyPay_TokenAcesso();
            await Task.Delay(3000); // Use Task.Delay para uma pausa assíncrona

            string retornoCadastro = await GalaxyPay_ListarCliente(pessoa.Cnpj, pessoa);
            if (retornoCadastro.Equals("1"))
            {
                try
                {
                    Address address = new Address();
                    String url = "https://api.galaxpay.com.br/v2/charges";
                    var requisicaoWeb = WebRequest.CreateHttp(url);
                    requisicaoWeb.Method = "POST";
                    requisicaoWeb.ContentType = "application/json";
                    requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);

                    string[] arrayEmail = new string[1];
                    if (!string.IsNullOrEmpty(pessoa.Email))
                        arrayEmail[0] = pessoa.Email;
                    else
                        arrayEmail[0] = pessoa.Cnpj + "@email.com.br";

                    Charges charges = new Charges();
                    charges.myId = tabelaOrigem + "_" + idOrigem;
                    charges.value = int.Parse(valorPix.ToString("F").Replace(",", ""));
                    charges.payday = DateTime.Now.ToString("yyyy-MM-dd");
                    charges.payedOutsideGalaxPay = false; // paga fora do sistema?
                    charges.mainPaymentMethodId = "pix";

                    //Cliente
                    charges.Customer = new Customer();
                    charges.Customer.galaxPayId = int.Parse(galaxPayId_Customer);
                    charges.Customer.name = pessoa.RazaoSocial;
                    charges.Customer.document = GenericaDesktop.RemoveCaracteres(pessoa.Cnpj);
                    charges.Customer.emails = arrayEmail;

                    //Pix
                    charges.PaymentMethodPix = new Paymentmethodpix();
                    charges.PaymentMethodPix.Deadline = new Deadline();
                    charges.PaymentMethodPix.Deadline.type = "days";
                    charges.PaymentMethodPix.Deadline.value = 60;

                    using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(new
                        {
                            myId = charges.myId,
                            value = charges.value,
                            payday = charges.payday,
                            mainPaymentMethodId = charges.mainPaymentMethodId,
                            Customer = charges.Customer,
                            PaymentMethodPix = charges.PaymentMethodPix
                        });

                        await streamWriter.WriteAsync(json); // Use WriteAsync para uma operação assíncrona
                    }

                    var httpResponse = (HttpWebResponse)await requisicaoWeb.GetResponseAsync(); // Use GetResponseAsync para uma operação assíncrona
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = await streamReader.ReadToEndAsync(); // Use ReadToEndAsync para uma operação assíncrona
                                                                          //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                        var ret = JsonConvert.DeserializeObject<GalaxPayRetPixGerado>(result);

                        if (ret != null)
                        {
                            retornoString = ret.type.ToString();
                            if (retornoString.Equals("True"))
                            {
                                retornoString = ret.Charge.Transactions[0].Pix.qrCode;
                            }
                        }
                    }
                    return retornoString;
                }
                catch
                {
                    GenericaDesktop.ShowErro("Falha ao Gerar PIX no Sistema da Galax Pay: Sistema Apresenta Instabilidade");
                    return contagemOk.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        public class Address
        {
            public string zipCode { get; set; }
            public string street { get; set; }
            public string number { get; set; }
            public string complement { get; set; }
            public string neighborhood { get; set; }
            public string city { get; set; }
            public string state { get; set; }
        }

        public string GalaxyPay_ObterPDFUnico(ContaReceber contaReceber)
        {
            try
            {
                Address address = new Address();
                String url = "https://api.galaxpay.com.br/v2/boletos/charges";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);
                //ImpressaoBoletos impressaoBoletos = new ImpressaoBoletos();
                //impressaoBoletos.myIds[0] = contaReceber.Id.ToString();
                //impressaoBoletos.order = "transactionPayday.asc";
                string[] arrayFatura = new string[1];
                arrayFatura[0] = contaReceber.Id.ToString();
                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        myIds = arrayFatura,
                        order = "transactionPayday.asc"
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                   // var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    var retPDFX = JsonConvert.DeserializeObject<RetPDF>(result);
                    string retornoString = "";
                    if (retPDFX != null)
                        retornoString = retPDFX.Boleto.pdf;
                    return retornoString;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao gerar PDF no Sistema da Galaxy Pay: " + err.Message);
                return "";
            }
        }

        public string GalaxyPay_ObterPDFLista(string[] arrayFatura)
        {
            try
            {
                Address address = new Address();
                String url = "https://api.galaxpay.com.br/v2/boletos/charges";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);
                //ImpressaoBoletos impressaoBoletos = new ImpressaoBoletos();
                //impressaoBoletos.myIds[0] = contaReceber.Id.ToString();
                //impressaoBoletos.order = "transactionPayday.asc";
         
                using (var streamWriter = new StreamWriter(requisicaoWeb.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        myIds = arrayFatura,
                        order = "transactionPayday.asc"
                    });

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    // var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    var retPDFX = JsonConvert.DeserializeObject<RetPDF>(result);
                    string retornoString = "";
                    if (retPDFX != null)
                        retornoString = retPDFX.Boleto.pdf;
                    return retornoString;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowErro("Falha ao gerar PDF no Sistema da Galaxy Pay: " + err.Message);
                return "";
            }
        }

        public GalaxyPay_RetornoStatus GalaxyPay_ListarTransacoes(string dataInicial, string dataFinal)
        {
            try
            {
                int contagemNovosBoletosBaixados = 0;
                GalaxyPay_TokenAcesso();
                Thread.Sleep(3000);
                String url = "https://api.galaxpay.com.br/v2/transactions?updateStatusFrom=" + dataInicial + "&updateStatusTo="+dataFinal+ "&status=payedBoleto,payExternal,payedPix&startAt=0&limit=100";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    //galaxyPay_RetornoStatus
                    GalaxyPay_RetornoStatus retStatus = JsonConvert.DeserializeObject<GalaxyPay_RetornoStatus>(result);
                    //var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    if (retStatus != null)
                    {
                        if(retStatus.totalQtdFoundInPage > 0)
                        {
                            for (int x = 0; x < retStatus.Transactions.Length; x++)
                            {
                                if (retStatus.Transactions[x].chargeMyId != null)
                                {
                                    ContaReceber contaReceber = new ContaReceber();
                                    contaReceber.Id = int.Parse(retStatus.Transactions[x].chargeMyId);
                                    contaReceber = (ContaReceber)Controller.getInstance().selecionar(contaReceber);
                                    if (contaReceber.Recebido == false && Sessao.parametroSistema.ContaBancariaVinculadaApi != null)
                                    {
                                        contagemNovosBoletosBaixados++;
                                        contaReceber.Recebido = true;
                                        contaReceber.DataRecebimento = DateTime.Parse(retStatus.Transactions[x].paydayDate);

                                        int qt = retStatus.Transactions[x].value.ToString().Length;
                                        string valorAjustadoReais = retStatus.Transactions[x].value.ToString().Substring(0, qt - 2);
                                        string valorAjustadoCentavos = retStatus.Transactions[x].value.ToString().Substring(qt - 2, 2);
                                        string valorFinal = valorAjustadoReais + "," + valorAjustadoCentavos;
                                        decimal valorRecebido = decimal.Parse(valorFinal);
                                        contaReceber.ValorRecebido = valorRecebido;
                                    
                                        Controller.getInstance().salvar(contaReceber);
                                        Caixa caixa = new Caixa();
                                        caixa.Cobrador = null;
                                        caixa.Conciliado = true;
                                        caixa.Concluido = true;
                                        caixa.ContaBancaria = Sessao.parametroSistema.ContaBancariaVinculadaApi;
                                        caixa.DataLancamento = contaReceber.DataRecebimento;
                                        caixa.Descricao = "REC. DE BOLETO - GALAXY PAY " + contaReceber.Documento + " " + contaReceber.Cliente.RazaoSocial;
                                        caixa.EmpresaFilial = Sessao.empresaFilialLogada;
                                        caixa.FormaPagamento = contaReceber.FormaPagamento;
                                        caixa.IdOrigem = contaReceber.Id.ToString();
                                        caixa.Pessoa = contaReceber.Cliente;
                                        caixa.PlanoConta = contaReceber.PlanoConta;
                                        caixa.TabelaOrigem = "CONTARECEBER";
                                        caixa.Tipo = "E";
                                        caixa.Usuario = Sessao.usuarioLogado;
                                        caixa.Valor = valorRecebido;
                                        Controller.getInstance().salvar(caixa);
                                    }
                                    else if (Sessao.parametroSistema.ContaBancariaVinculadaApi == null)
                                    {
                                        GenericaDesktop.ShowAlerta("Para utilizar retorno automático de boletos vincule uma conta bancária a API, utilize a tela PARAMETROS DO SISTEMA / CONTAS A RECEBER e selecione uma conta bancária");
                                        FrmParametroSistema frm = new FrmParametroSistema();
                                        frm.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                    if(contagemNovosBoletosBaixados > 0)
                        GenericaDesktop.ShowInfo(contagemNovosBoletosBaixados.ToString() + " Boleto(s) Baixado(s) pelo sistema GalaxyPay!");
                    return retStatus;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowAlerta("Erro ao ler retorno do sistema Galaxy Pay: " + err.Message);
                return null;
            }
        }
        public class RetPDF
        {
            public Boleto Boleto { get; set; }
        }

        public class Boleto
        {
            public string pdf { get; set; }
        }


    }
}
