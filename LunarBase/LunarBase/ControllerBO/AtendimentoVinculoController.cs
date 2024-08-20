using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AtendimentoVinculoController : Controller
    {
        public IList<AtendimentoVinculo> selecionarVinculosPorAtendimento(int idAtendimento)
        {
            AtendimentoVinculoBO bo = new AtendimentoVinculoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarVinculosPorAtendimento(idAtendimento);
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
