using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfeCceController : Controller
    {
        public IList<NfeCce> selecionarCartaCorrecaoPorNfe(int idNfe)
        {
            NfeCceBO bo = new NfeCceBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCartaCorrecaoPorNfe(idNfe);
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
