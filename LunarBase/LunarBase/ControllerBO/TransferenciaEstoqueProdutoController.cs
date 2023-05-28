using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class TransferenciaEstoqueProdutoController : Controller
    {
        public IList<TransferenciaEstoqueProduto> selecionarProdutosPorTransferencia(int idTransferencia)
        {
            TransferenciaEstoqueProdutoBO bo = new TransferenciaEstoqueProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorTransferencia(idTransferencia);
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
