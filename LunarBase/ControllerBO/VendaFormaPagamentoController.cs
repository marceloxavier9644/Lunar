using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class VendaFormaPagamentoController : Controller
    {
        public IList<VendaFormaPagamento> selecionarVendaFormaPagamentoPorVenda(int idVenda)
        {
            VendaFormaPagamentoBO bo = new VendaFormaPagamentoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarVendaFormaPagamentoPorVenda(idVenda);
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
