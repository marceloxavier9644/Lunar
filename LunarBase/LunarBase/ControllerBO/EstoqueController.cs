using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class EstoqueController : Controller
    {
        public IList<Estoque> selecionarEstoqueMovimentoPorProduto(EmpresaFilial empresa, int idProduto, bool conciliado)
        {
            EstoqueBO bo = new EstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEstoqueMovimentoPorProduto(empresa, idProduto, conciliado);
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
        public IList<Estoque> selecionarEstoqueMovimentoPorBalanco(EmpresaFilial empresa, bool conciliado, BalancoEstoque balancoEstoque)
        {
            EstoqueBO bo = new EstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEstoqueMovimentoPorBalanco(empresa, conciliado, balancoEstoque);
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
        public IList<Estoque> selecionarEstoqueMovimentoPorProdutoEData(EmpresaFilial empresa, int idProduto, bool conciliado, string dataInicial, string dataFinal)
        {
            EstoqueBO bo = new EstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEstoqueMovimentoPorProdutoEData(empresa, idProduto, conciliado, dataInicial, dataFinal);
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
