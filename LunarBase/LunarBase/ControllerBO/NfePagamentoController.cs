using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfePagamentoController : Controller
    {
        public IList<NfePagamento> selecionarPagamentoPorNfe(int idNfe)
        {
            NfePagamentoBO bo = new NfePagamentoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPagamentoPorNfe(idNfe);
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
