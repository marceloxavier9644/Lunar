using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrdemServicoServicoController : Controller
    {
        public IList<OrdemServicoServico> selecionarServicosPorOrdemServico(int idOrdemServico)
        {
            OrdemServicoServicoBO bo = new OrdemServicoServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarServicosPorOrdemServico(idOrdemServico);
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
