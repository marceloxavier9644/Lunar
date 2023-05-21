using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfeProdutoController : Controller
    {
        public IList<NfeProduto> selecionarProdutosPorNfe(int idNfe)
        {
            NfeProdutoBO bo = new NfeProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorNfe(idNfe);
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

        public IList<NfeProduto> selecionarProdutosPorNumeroNfe(int numeroNfe)
        {
            NfeProdutoBO bo = new NfeProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutosPorNumeroNfe(numeroNfe);
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
        public IList<NfeProduto> selecionarProdutoPorCNPJeReferencia(string cnpjEmitente, string referencia)
        {
            NfeProdutoBO bo = new NfeProdutoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarProdutoPorCNPJeReferencia(cnpjEmitente, referencia);
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