using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CestController : Controller
    {
        public IList<Cest> selecionarTodosCest()
        {
            CestBO bo = new CestBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCEST();
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

        public IList<Cest> selecionarCestPorNCM(String ncm)
        {
            CestBO bo = new CestBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCestPorNCM(ncm);
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
