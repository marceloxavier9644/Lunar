using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoGrupoController : Controller
    {
        public IList<ProdutoGrupo> selecionarGrupoComVariosFiltros(string valor)
        {
            ProdutoGrupoBO bo = new ProdutoGrupoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarGrupoComVariosFiltros(valor);
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

        public IList<ProdutoGrupo> selecionarTodosGruposFood()
        {
            ProdutoGrupoBO bo = new ProdutoGrupoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosGruposFood();
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
