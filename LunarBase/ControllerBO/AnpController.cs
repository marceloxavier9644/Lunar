using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AnpController : Controller
    {
        public IList<Anp> selecionarTodosCodigosANP()
        {
            AnpBO bo = new AnpBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCodigosANP();
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

        public IList<Anp> selecionarCodigoAnpPorCodigo(String codigoANP)
        {
            AnpBO bo = new AnpBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCodigoAnpPorCodigo(codigoANP);
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
