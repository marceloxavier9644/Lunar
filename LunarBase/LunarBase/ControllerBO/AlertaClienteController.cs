using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AlertaClienteController : Controller
    {
        public IList<AlertaCliente> selecionarAlertaPorPessoa(int idPessoa)
        {
            AlertaClienteBO bo = new AlertaClienteBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarAlertaPorPessoa(idPessoa);
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
