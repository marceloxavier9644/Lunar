using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrdemServicoProdutoController : Controller
    {
        public IList<OrdemServicoProduto> selecionarProdutosPorOrdemServico(int idOrdemServico)
        {
            OrdemServicoProdutoBO bo = new OrdemServicoProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorOrdemServico(idOrdemServico);
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

        public IList<OrdemServicoProduto> selecionarProdutosVendidosPorPeriodo(EmpresaFilial empresaFilial, String dataInicial, String dataFinal)
        {
            OrdemServicoProdutoBO bo = new OrdemServicoProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosVendidosPorPeriodo(empresaFilial, dataInicial, dataFinal);
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
