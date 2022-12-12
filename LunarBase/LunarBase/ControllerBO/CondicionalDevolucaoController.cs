using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CondicionalDevolucaoController : Controller
    {
        public IList<CondicionalDevolucao> selecionarProdutosDevolvidosPorCondicional(int idCondicional)
        {
            CondicionalDevolucaoBO bo = new CondicionalDevolucaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosDevolvidosPorCondicional(idCondicional);
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
        public CondicionalDevolucao selecionarProdutoDevolvido(int idCondicional, int idProduto)
        {
            CondicionalDevolucaoBO bo = new CondicionalDevolucaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoDevolvido(idCondicional, idProduto);
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
