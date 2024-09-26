using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfseController : Controller
    {
        public IList<Nfse> selecionarNFSePorOrdemServico(int idOrdemServico)
        {
            NfseBO bo = new NfseBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNFSePorOrdemServico(idOrdemServico);
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
