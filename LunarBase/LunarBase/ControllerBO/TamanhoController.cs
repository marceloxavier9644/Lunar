using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class TamanhoController : Controller
    {
        public void salvarSeNaoExistir(Tamanho tamanho)
        {
            try
            {
                Conexao.IniciaTransacao();
                tamanho.DataCadastro = DateTime.Now;
                tamanho.OperadorCadastro = "1";
                TamanhoBO bo = new TamanhoBO();
                bo.salvarSeNaoExistir(tamanho);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + tamanho.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
