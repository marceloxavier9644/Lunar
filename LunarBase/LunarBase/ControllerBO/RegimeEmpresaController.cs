using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class RegimeEmpresaController : Controller
    {
        public void salvarSeNaoExistir(RegimeEmpresa regimeEmpresa)
        {
            try
            {
                Conexao.IniciaTransacao();
                regimeEmpresa.DataCadastro = DateTime.Now;
                regimeEmpresa.OperadorCadastro = "1";
                RegimeEmpresaBO bo = new RegimeEmpresaBO();
                bo.salvarSeNaoExistir(regimeEmpresa);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + regimeEmpresa.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
