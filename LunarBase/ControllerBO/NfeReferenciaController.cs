using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfeReferenciaController : Controller
    {
        public IList<NfeReferencia> selecionarNotasReferenciadasPorNfe(int idNfe)
        {
            NfeReferenciaBO bo = new NfeReferenciaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasReferenciadasPorNfe(idNfe);
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
