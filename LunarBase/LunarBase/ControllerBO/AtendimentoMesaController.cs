using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AtendimentoMesaController : Controller
    {
        public IList<AtendimentoMesa> selecionarTodasMesas()
        {
            AtendimentoMesaBO bo = new AtendimentoMesaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasMesas();
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
