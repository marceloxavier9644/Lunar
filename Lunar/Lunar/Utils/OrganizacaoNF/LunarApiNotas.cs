using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using Newtonsoft.Json;
using Syncfusion.Compression.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Lunar.Utils.OrganizacaoNF
{
    public class LunarApiNotas
    {
        public string consultaNotaApi(string cnpj, string chave)
        {
            if (GenericaDesktop.possuiConexaoInternet())
            {
                string url = "https://lunarsoftware.com.br/painel/api/api-invoice-get.php";
                try
                {
                    var requisicaoWeb = WebRequest.CreateHttp(url);
                    requisicaoWeb.Method = "POST";
                    requisicaoWeb.ContentType = "application/json";
                    using (var streamWriter = new
                            StreamWriter(requisicaoWeb.GetRequestStream())
                            )
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            cnpj = cnpj,
                            key = Sessao.serialPainel,
                            nfkey = chave
                        });

                        streamWriter.Write(json);

                    }
                    var httpResponse = (HttpWebResponse)requisicaoWeb.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        streamReader.Close();
                        if (result.Contains("ERR_NENHUMA_NOTA_LOCALIZADA"))
                            return "ERR_NENHUMA_NOTA_LOCALIZADA";
                        else
                            return result;
                    }
                }
                catch (Exception err)
                {
                    GenericaDesktop.ShowErro(err.Message);
                    return null;
                }
            }
            else
                return null;
        }


        public async Task<string> EnvioNotaParaNuvem(string cnpj, string chave, string tipoNota, string nfstatus, string mes, string ano, byte[] file_bytes, Nfe nfe)
        {
            if (String.IsNullOrEmpty(chave))
            {
                chave = nfe.CnpjEmitente + "_" + nfe.Modelo + "_"+ nfe.NNf + "_" + nfe.Serie;
                nfe.Chave = chave;
                Controller.getInstance().salvar(nfe);
            }
            string url = "https://lunarsoftware.com.br/painel/api/api-invoice-send.php";
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(cnpj), "cnpj");
            form.Add(new StringContent(Sessao.serialPainel), "key");
            form.Add(new StringContent(chave), "nfkey");
            form.Add(new StringContent(tipoNota), "nftype");
            form.Add(new StringContent(nfstatus), "nfstatus");
            form.Add(new StringContent(mes), "nfmonth");
            form.Add(new StringContent(ano), "nfyear");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "file", "nota.xml");
            HttpResponseMessage response = await httpClient.PostAsync(url, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
            if (sd.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
            {
                Nfe nf = new Nfe();
                nf = nfe;
                nf.Nuvem = true;
                Controller.getInstance().salvar(nf);
            }
            return sd;
        }

        public async Task<string> EnvioArquivoParaNuvem_PDF(string cnpj, string chave, string tipoNota, string nfstatus, string mes, string ano, byte[] file_bytes, Nfe nfe)
        {
            string url = "https://lunarsoftware.com.br/painel/api/api-invoice-send.php";
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(cnpj), "cnpj");
            form.Add(new StringContent(Sessao.serialPainel), "key");
            form.Add(new StringContent(chave), "nfkey");
            form.Add(new StringContent(tipoNota), "nftype");
            form.Add(new StringContent(nfstatus), "nfstatus");
            form.Add(new StringContent(mes), "nfmonth");
            form.Add(new StringContent(ano), "nfyear");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "file", "nota.pdf");
            HttpResponseMessage response = await httpClient.PostAsync(url, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
            if (sd.ToString().Contains("\"error\":false,\"msg\":\"OK\""))
            {
                Nfe nf = new Nfe();
                nf = nfe;
                nf.Nuvem = true;
                Controller.getInstance().salvar(nf);
            }
            return sd;
        }
        public async Task<string> coletarArquivosContabeis(string cnpj, string mes, string ano)
        {
            string url = "https://lunarsoftware.com.br/painel/api/api-invoice-get.php";
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(cnpj), "cnpj");
            form.Add(new StringContent(Sessao.serialPainel), "key");
            form.Add(new StringContent(mes), "month");
            form.Add(new StringContent(ano), "year");
            HttpResponseMessage response = await httpClient.PostAsync(url, form);

            string localFilePath = @"C:\Users\marce\OneDrive\Área de Trabalho\Arquivos\";
            string fileName = mes + ano + ".zip";

          
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (Stream zip = File.OpenWrite(localFilePath + fileName))
                {
                    stream.CopyTo(zip);
                }
            }
            return localFilePath + fileName;
        }

        
    }
}
