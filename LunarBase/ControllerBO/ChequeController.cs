using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ChequeController : Controller
    {
        public IList<Cheque> selecionarChequesPorVenda(int idVenda)
        {
            ChequeBO bo = new ChequeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarChequesPorVenda(idVenda);
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
