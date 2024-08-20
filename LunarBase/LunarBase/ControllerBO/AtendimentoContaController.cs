using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AtendimentoContaController : Controller
    {
        public IList<AtendimentoConta> selecionarTodosAtendimentoConta(int idAtendimento)
        {
            AtendimentoContaBO bo = new AtendimentoContaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosAtendimentoConta(idAtendimento);
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
