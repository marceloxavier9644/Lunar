using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AnexoController : Controller
    {
        public IList<Anexo> selecionarTodosAnexosPorOrdemServico(int idOrdemServico)
        {
            AnexoBO bo = new AnexoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosAnexosPorOrdemServico(idOrdemServico);
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
