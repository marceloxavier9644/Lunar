using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class SpcController : Controller
    {
        public IList<Spc> selecionarRegistrosPorCliente(Pessoa pessoa)
        {
            SpcBO bo = new SpcBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarRegistrosPorCliente(pessoa);
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
