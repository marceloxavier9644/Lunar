using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PessoaPropriedadeController : Controller
    {
        public IList<PessoaPropriedade> selecionarPropriedadesPorPessoa(int idPessoa)
        {
            PessoaPropriedadeBO bo = new PessoaPropriedadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPropriedadesPorPessoa(idPessoa);
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
