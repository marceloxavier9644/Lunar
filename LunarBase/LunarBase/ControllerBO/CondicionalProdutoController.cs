using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CondicionalProdutoController : Controller
    {
        public IList<CondicionalProduto> selecionarProdutosPorCondicional(int idCondicional)
        {
            CondicionalProdutoBO bo = new CondicionalProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorCondicional(idCondicional);
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
        public IList<CondicionalProduto> selecionarProdutosCondicionalComVariosFiltros(string valor)
        {
            CondicionalProdutoBO bo = new CondicionalProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosCondicionalComVariosFiltros(valor);
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
