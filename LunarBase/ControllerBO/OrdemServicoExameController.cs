using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrdemServicoExameController : Controller
    {
        public IList<OrdemServicoExame> selecionarExamesPorOrdemServico(int idOrdemServico)
        {
            OrdemServicoExameBO bo = new OrdemServicoExameBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarExamesPorOrdemServico(idOrdemServico);
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
