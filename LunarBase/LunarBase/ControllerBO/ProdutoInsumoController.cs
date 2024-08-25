using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoInsumoController : Controller
    {
        public IList<ProdutoInsumo> selecionarInsumoPorProduto(int idProduto)
        {
            ProdutoInsumoBO bo = new ProdutoInsumoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarInsumoPorProduto(idProduto);
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
