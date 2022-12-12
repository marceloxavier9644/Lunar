using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PessoaTelefoneController : Controller
    {
        public IList<PessoaTelefone> selecionarTelefonePorPessoa(int idPessoa)
        {
            PessoaTelefoneBO bo = new PessoaTelefoneBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTelefonePorPessoa(idPessoa);
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
