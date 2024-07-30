using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class BalancoEstoqueProdutoController : Controller
    {
        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalanco(int idBalancoEstoque)
        {
            BalancoEstoqueProdutoBO bo = new BalancoEstoqueProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorBalanco(idBalancoEstoque);
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
        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalancoSemRepetirItem(int idBalancoEstoque)
        {
            BalancoEstoqueProdutoBO bo = new BalancoEstoqueProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorBalancoSemRepetirItem(idBalancoEstoque);
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

        public IList<Produto> selecionarProdutosParaZerarNaoContabilizados(int idBalancoEstoque)
        {
            BalancoEstoqueProdutoBO bo = new BalancoEstoqueProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosParaZerarNaoContabilizados(idBalancoEstoque);
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
