using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class TrocoFixoController : Controller
    {
        public IList<TrocoFixo> selecionarTodosTrocoFixoPorEmpresaFilial()
        {
            TrocoFixoBO bo = new TrocoFixoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosTrocoFixoPorEmpresaFilial();
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
