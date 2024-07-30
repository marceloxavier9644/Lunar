using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class EstadoController : Controller
    {
        public Estado selecionarEstadoPorUF(String uf)
        {
            EstadoBO bo = new EstadoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEstadoPorUF(uf);
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
