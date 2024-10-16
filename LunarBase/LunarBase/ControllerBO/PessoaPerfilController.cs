using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PessoaPerfilController : Controller
    {
        public IList<PessoaPerfil> selecionarPerfilPorPessoa(int idPessoa)
        {
            PessoaPerfilBO bo = new PessoaPerfilBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPerfilPorPessoa(idPessoa);
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
