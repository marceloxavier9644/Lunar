using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class BancoController : Controller
    {
        public IList<Banco> selecionarTodosBancos()
        {
            BancoBO bo = new BancoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosBancos();
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

        public IList<Banco> selecionarBancosComVariosFiltros(string valor)
        {
            BancoBO bo = new BancoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarBancosComVariosFiltros(valor);
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
