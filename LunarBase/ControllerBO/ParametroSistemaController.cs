using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ParametroSistemaController : Controller
    {
        public void salvarSeNaoExistir(ParametroSistema parametroSistema)
        {
            try
            {
                Conexao.IniciaTransacao();
                parametroSistema.DataCadastro = DateTime.Now;
                parametroSistema.OperadorCadastro = "1";
                ParametroSistemaBO bo = new ParametroSistemaBO();
                bo.salvarSeNaoExistir(parametroSistema);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + parametroSistema.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
