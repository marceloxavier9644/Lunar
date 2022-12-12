using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class GrupoUsuarioController : Controller
    {
        public void salvarSeNaoExistir(GrupoUsuario grupoUsuario)
        {
            try
            {
                Conexao.IniciaTransacao();
                grupoUsuario.DataCadastro = DateTime.Now;
                grupoUsuario.OperadorCadastro = "1";
                GrupoUsuarioBO bo = new GrupoUsuarioBO();
                bo.salvarSeNaoExistir(grupoUsuario);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + grupoUsuario.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
