using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CstIpiController : Controller
    {
        public IList<CstIpi> selecionarTodosCST()
        {
            CstIpiBO bo = new CstIpiBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCST();
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

        public IList<CstIpi> selecionarCstPorCst(String cst)
        {
            CstIpiBO bo = new CstIpiBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCstPorCst(cst);
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
