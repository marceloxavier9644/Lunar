using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class TransferenciaEstoqueController : Controller
    {
        public IList<TransferenciaEstoque> selecionarTodasTransferencias()
        {
            TransferenciaEstoqueBO bo = new TransferenciaEstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasTransferencias();
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
