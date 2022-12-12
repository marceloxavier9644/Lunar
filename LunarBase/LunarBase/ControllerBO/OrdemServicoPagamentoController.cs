using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrdemServicoPagamentoController : Controller
    {
        public IList<OrdemServicoPagamento> selecionarPagamentoPorOrdemServico(int idOrdemServico)
        {
            OrdemServicoPagamentoBO bo = new OrdemServicoPagamentoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPagamentoPorOrdemServico(idOrdemServico);
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
