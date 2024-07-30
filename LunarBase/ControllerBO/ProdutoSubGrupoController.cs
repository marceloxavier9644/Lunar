using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoSubGrupoController : Controller
    {
        public IList<ProdutoSubGrupo> selecionarProdutoSubGrupoComVariosFiltros(string valor)
        {
            ProdutoSubGrupoBO bo = new ProdutoSubGrupoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoSubGrupoComVariosFiltros(valor);
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
