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
        public IList<CondicionalProduto> selecionarProdutosCondicionalComVariosFiltros(string valor, int idCondicional)
        {
            CondicionalProdutoBO bo = new CondicionalProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosCondicionalComVariosFiltros(valor, idCondicional);
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

        public IList<CondicionalProduto> selecionarProdutosCondicionalPorIdProduto(string codProduto, int idCondicional)
        {
            CondicionalProdutoBO bo = new CondicionalProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosCondicionalPorIdProduto(codProduto, idCondicional);
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
