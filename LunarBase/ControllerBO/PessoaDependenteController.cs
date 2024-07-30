using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PessoaDependenteController : Controller
    {
        public IList<PessoaDependente> selecionarDependentePorPessoa(int idPessoa)
        {
            PessoaDependenteBO bo = new PessoaDependenteBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarDependentePorPessoa(idPessoa);
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
