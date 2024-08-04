using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ProdutoCodigoBarrasController : Controller
    {
        public IList<ProdutoCodigoBarras> selecionarCodigoBarrasPorSQL(String sql)
        {
            ProdutoCodigoBarrasBO bo = new ProdutoCodigoBarrasBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCodigoBarrasPorSQL(sql);
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
        public ProdutoCodigoBarras selecionarCodigoBarrasPorGrade(int idGrade)
        {
            ProdutoCodigoBarrasBO bo = new ProdutoCodigoBarrasBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCodigoBarrasPorGrade(idGrade);
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
