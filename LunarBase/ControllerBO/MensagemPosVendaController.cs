using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class MensagemPosVendaController : Controller
    {
        public IList<MensagemPosVenda> selecionarTodasMensagensNaoEnviadasPorPeriodo(string dataInicial, string dataFinal)
        {
            MensagemPosVendaBO bo = new MensagemPosVendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasMensagensNaoEnviadasPorPeriodo(dataInicial, dataFinal);
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

        public IList<MensagemPosVenda> selecionarTodasMensagensEnviadasPorPeriodo(string dataInicial, string dataFinal)
        {
            MensagemPosVendaBO bo = new MensagemPosVendaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasMensagensEnviadasPorPeriodo(dataInicial, dataFinal);
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
