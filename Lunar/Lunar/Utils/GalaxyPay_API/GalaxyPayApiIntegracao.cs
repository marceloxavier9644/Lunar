using LunarBase.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Script.Serialization;
using ZXing.Aztec.Internal;

namespace Lunar.Utils.GalaxyPay_API
{

    public class GalaxyPayApiIntegracao
    {
        //Sandbox
        string galaxId = "39694";
        string galaxHash = "R040H6XbRsNtKoG3IdYi9w7qKh50UeWu1xKp62Qy";

        //SandBox Parner (Parceiro para gerar comissão)
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
                //requisicaoWeb.Headers.Add("Authorization", "Partner " + EncodeToBase64(galaxIdParceiro + ":" + galaxHashParceiro));

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
                String url = "https://api.galaxpay.com.br/v2/token/customers?Documents="+cpfCnpj;
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Authorization", "Bearer " + tokenAcesso);

                var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    streamReader.Close();
                    var retorno = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    string idret = "";
                    if (retorno != null)
                    {
                        try { idret = retorno["value"].ToString(); } catch { idret = ""; }
                    }
                    return idret;
                }
            }
            catch (Exception err)
            {
                if (err.Message.Equals("O servidor remoto retornou um erro: (404) Não Localizado."))
                {
                    GalaxyPay_CadastrarCliente(pessoa);
                    return cpfCnpj;
                }
                else
                {
                    GenericaDesktop.ShowAlerta("Erro ao localizar cliente no sistema Galaxy Pay: " + err.Message);
                    return "";
                }
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
                GenericaDesktop.ShowErro("Falha ao solicitar Token Galaxy Pay: " + err.Message);
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

    }
}
