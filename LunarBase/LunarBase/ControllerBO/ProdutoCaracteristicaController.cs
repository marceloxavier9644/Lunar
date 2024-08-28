using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoCaracteristicaController : Controller
    {
        public IList<ProdutoCaracteristica> selecionarProdutoCaracteristica(int idProduto)
        {
            ProdutoCaracteristicaBO bo = new ProdutoCaracteristicaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoCaracteristica(idProduto);
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
