using Lunar.consultaSPCBrasil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.SPCBrasilIntegracao
{
    public class ConsultaSpcBrasilLunar
    {
        public ResultadoConsulta ConsultarSpc()
        {
            string usuario = "10535935";
            string senha = "temp@1234";

            consultaSPCBrasil.consultaWebServiceClient client = new consultaWebServiceClient();
            client.ClientCredentials.UserName.UserName = usuario;
            client.ClientCredentials.UserName.Password = senha;
            //Homologação
            client.Endpoint.Address = new EndpointAddress("https://treina.spc.org.br:443/spc/remoting/ws/consulta/consultaWebService");

            //Produção
            //client.Endpoint.Address = new EndpointAddress("https://servicos.spc.org.br/spc/remoting/ws/consulta/consultaWebService");

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.SendTimeout = TimeSpan.FromMinutes(5);
            binding.MaxReceivedMessageSize = 10485760;
            client.Endpoint.Binding = binding;

            try
            {
                Lunar.consultaSPCBrasil.FiltroConsulta filtroConsulta = new Lunar.consultaSPCBrasil.FiltroConsulta();
                filtroConsulta.documentoconsumidor = "02358474703";
                filtroConsulta.tipoconsumidor = TipoPessoa.F;
                filtroConsulta.codigoproduto = "12";
                Lunar.consultaSPCBrasil.ResultadoConsulta res = client.consultar(filtroConsulta);
                return res;
            }
            catch (FaultException er)
            {
                GenericaDesktop.ShowAlerta(er.Message);
                return null;
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
                return null;
            }
        }

        private void listarProdutosSPCBrasil()
        {
            string usuario = "10535935";
            string senha = "temp@1234";

            consultaSPCBrasil.consultaWebServiceClient client = new consultaWebServiceClient();
            client.ClientCredentials.UserName.UserName = usuario;
            client.ClientCredentials.UserName.Password = senha;
            //Homologação
            client.Endpoint.Address = new EndpointAddress("https://treina.spc.org.br:443/spc/remoting/ws/consulta/consultaWebService");

            //Produção
            //client.Endpoint.Address = new EndpointAddress("https://servicos.spc.org.br/spc/remoting/ws/consulta/consultaWebService");

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.SendTimeout = TimeSpan.FromMinutes(5);
            binding.MaxReceivedMessageSize = 10485760;
            client.Endpoint.Binding = binding;

            try
            {
                consultaSPCBrasil.Produto[] produtos = client.listarProdutos();
                foreach (consultaSPCBrasil.Produto prd in produtos)
                {
                    //12 é o exemplo de uma consulta comum que se chama apenas SPC, a mais utilizada pelas empresas de Unaí.
                    if (prd.codigo.Equals("12"))
                    {
                        Lunar.consultaSPCBrasil.Produto resProdDetalhe = client.detalharProduto("12");
                    }
                }
            }
            catch (FaultException er)
            {
                GenericaDesktop.ShowAlerta(er.Message);
            }
            catch (Exception erro)
            {
                GenericaDesktop.ShowAlerta(erro.Message);
            }
        }
    }
}
