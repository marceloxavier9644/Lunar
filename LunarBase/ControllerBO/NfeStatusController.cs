using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfeStatusController : Controller
    {
        public void salvarSeNaoExistir(NfeStatus nfeStatus)
        {
            try
            {
                Conexao.IniciaTransacao();
                nfeStatus.DataCadastro = DateTime.Now;
                nfeStatus.OperadorCadastro = "1";
                NfeStatusBO bo = new NfeStatusBO();
                bo.salvarSeNaoExistir(nfeStatus);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + nfeStatus.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
