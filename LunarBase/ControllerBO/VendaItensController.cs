using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class VendaItensController : Controller
    {
        public IList<VendaItens> selecionarProdutosVendidosPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            VendaItensBO bo = new VendaItensBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosVendidosPorPeriodo(filial, dataInicial, dataFinal);
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

        public IList<VendaItens> selecionarProdutosPorVenda(int idVenda)
        {
            VendaItensBO bo = new VendaItensBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorVenda(idVenda);
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
