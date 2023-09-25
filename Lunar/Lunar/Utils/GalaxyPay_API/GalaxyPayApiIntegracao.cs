using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Script.Serialization;
using ZXing.Aztec.Internal;
using static Lunar.Utils.GalaxyPay_API.RetornoBoletoGerado;
using static LunarBase.Utilidades.ClasseRetornoJson.InutilizacaoNFCe;

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

        public string GalaxyPay_ListarCliente(string cpfCnpj, Pessoa pessoa)
        {
            try
            {
                String url = "https://api.galaxpay.com.br/v2/customers?documents=" + cpfCnpj + "&startAt=0&limit=100";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string totalQtdFoundInPage = "";
                    if (retorno != null)
                    {
                        try { totalQtdFoundInPage = retorno["totalQtdFoundInPage"].ToString(); } catch { totalQtdFoundInPage = ""; }
                        if (totalQtdFoundInPage.Equals("0"))
                        {
                            string retornoCadastro = GalaxyPay_CadastrarCliente(pessoa);
                            if (retornoCadastro.Equals("True"))
                                totalQtdFoundInPage = "1";
                        }
                        else if (totalQtdFoundInPage.Equals("1"))
                        {
                            string retornoEdicao = GalaxyPay_EditarCliente(pessoa);
                            GenericaDesktop.gravarLinhaLog("GALAXY PAY - " + pessoa.RazaoSocial + " " + pessoa.Id, "GALAXY PAY - ATUALIZAÇÃO DE CLIENTE");
                        }
                    }
                    return totalQtdFoundInPage;
                }
            }
            catch (Exception err)
            {
                GenericaDesktop.ShowAlerta("Erro ao localizar cliente no sistema Galaxy Pay: " + err.Message);
                return "";

            }
        }

        public string GalaxyPay_CadastrarCliente(Pessoa pessoa)
        {
            try
            {
                Address address = new Address();
                String url = "https://api.galaxpay.com.br/v2/customers";
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/json";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);
                string[] arrayEmail = new string[1];
                if (String.IsNullOrEmpty(pessoa.Email))
                {
                    GenericaDesktop.ShowAlerta("Cliente não possui um e-mail no cadastro, NÃO será disparado o Boleto no e-mail automaticamente!");
                    arrayEmail[0] = Sessao.empresaFilialLogada.Email;
                }
                else
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
                GenericaDesktop.ShowErro("Falha ao Cadastrar Cliente no Sistema da Galaxy Pay: " + err.Message);
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
                    GenericaDesktop.ShowAlerta("Informe um email no cadastro do cliente!");
                    return "";
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
                charges.Customer.myId = pessoa.Id.ToString();
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
                GenericaDesktop.ShowErro("Falha ao Cadastrar Cliente no Sistema da Galaxy Pay: " + err.Message);
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
                GenericaDesktop.ShowErro("Falha ao Cadastrar Cliente no Sistema da Galaxy Pay: " + err.Message);
                return "";
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
