using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class BoletoConfigController : Controller
    {
        public IList<BoletoConfig> selecionarTodosBoletoConfig()
        {
            BoletoConfigBO bo = new BoletoConfigBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosBoletoConfig();
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

        public IList<BoletoConfig> selecionarBoletoConfigPorContaBancaria(int idContaBancaria)
        {
            BoletoConfigBO bo = new BoletoConfigBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarBoletoConfigPorContaBancaria(idContaBancaria);
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
