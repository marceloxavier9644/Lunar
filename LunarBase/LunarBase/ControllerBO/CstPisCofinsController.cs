using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CstPisCofinsController : Controller
    {
        public IList<CstPisCofins> selecionarTodosCST()
        {
            CstPisCofinsBO bo = new CstPisCofinsBO();
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

        public IList<CstPisCofins> selecionarCstPorCst(String cst)
        {
            CstPisCofinsBO bo = new CstPisCofinsBO();
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
