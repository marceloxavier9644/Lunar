using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoSetorController : Controller
    {
        public IList<ProdutoSetor> selecionarProdutoSetorComVariosFiltros(string valor, EmpresaFilial empresaFilial)
        {
            ProdutoSetorBO bo = new ProdutoSetorBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoSetorComVariosFiltros(valor, empresaFilial);
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
