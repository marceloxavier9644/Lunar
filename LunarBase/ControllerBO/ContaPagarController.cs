using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ContaPagarController : Controller
    {
        public IList<ContaPagar> selecionarContaPagarPorSql(string sql)
        {
            ContaPagarBO bo = new ContaPagarBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaPagarPorSql(sql);
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
