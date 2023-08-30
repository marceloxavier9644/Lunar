using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ContaReceberRecebidaController : Controller
    {
        public IList<ContaReceberRecebida> selecionarContaReceberRecebidaPorSql(string sql)
        {
            ContaReceberRecebidaBO bo = new ContaReceberRecebidaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberRecebidaPorSql(sql);
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
