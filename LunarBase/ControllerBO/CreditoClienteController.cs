using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CreditoClienteController : Controller
    {
        public IList<CreditoCliente> selecionarCreditoPorCliente(int idCliente)
        {
            CreditoClienteBO bo = new CreditoClienteBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCreditoPorCliente(idCliente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<CreditoCliente> selecionarCreditoPorClienteEOrigem(int idCliente, string tabelaOrigem, string idOrigem)
        {
            CreditoClienteBO bo = new CreditoClienteBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCreditoPorClienteEOrigem(idCliente, tabelaOrigem, idOrigem);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
