using LunarBase.Classes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lunar.Utils.GalaxyPay_API
{
    public class GerarBoletoGalaxyPay
    {
        public void gerarBoletoAvulsoGalaxyPay(IList<ContaReceber> listaReceber, Pessoa pessoa)
        {
            string[] arrayFatura = new string[listaReceber.Count];
            GalaxyPayApiIntegracao galaxyPayApiIntegracao = new GalaxyPayApiIntegracao();
            string tokenAcessoGalaxyPay = galaxyPayApiIntegracao.GalaxyPay_TokenAcesso();

            if (!String.IsNullOrEmpty(tokenAcessoGalaxyPay))
            {
                Thread.Sleep(3000);
                string ret = galaxyPayApiIntegracao.GalaxyPay_ListarCliente(GenericaDesktop.RemoveCaracteres(pessoa.Cnpj.Trim()), pessoa);
                if (ret.Equals("1"))
                {
                    string retornoBoletos = "";
                    int contagem = 0;
                    foreach (ContaReceber contaReceber in listaReceber)
                    {
                        if (contaReceber.BoletoGerado == false)
                        {
                            retornoBoletos = galaxyPayApiIntegracao.GalaxyPay_GerarBoleto(pessoa, contaReceber);
                            if (retornoBoletos.Equals("1"))
                            {
                                arrayFatura[contagem] = contaReceber.Id.ToString();
                                contagem++;
                            }
                        }
                        else
                            GenericaDesktop.ShowAlerta(contaReceber.Documento + " - Essa fatura já possui boleto gerado, cancele o boleto que já existe ou utilize o boleto que foi gerado anteriormente!");
                    }
                    if (contagem > 0)
                    {
                        string link = galaxyPayApiIntegracao.GalaxyPay_ObterPDFLista(arrayFatura);
                        if (!String.IsNullOrEmpty(link))
                            System.Diagnostics.Process.Start(link);
                    }
                    else
                        GenericaDesktop.ShowAlerta("Falha ao gerar boletos");
                }
            }
        }
    }
}
