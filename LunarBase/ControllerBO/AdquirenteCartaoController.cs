using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class AdquirenteCartaoController : Controller
    {
        public IList<AdquirenteCartao> selecionarTodasAdquirentes()
        {
            AdquirenteCartaoBO bo = new AdquirenteCartaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasAdquirentes();
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

        public void salvarSeNaoExistir(AdquirenteCartao adquirenteCartao)
        {
            try
            {
                Conexao.IniciaTransacao();
                adquirenteCartao.DataCadastro = DateTime.Now;
                adquirenteCartao.OperadorCadastro = "1";
                AdquirenteCartaoBO bo = new AdquirenteCartaoBO();
                bo.salvarSeNaoExistir(adquirenteCartao);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + adquirenteCartao.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<AdquirenteCartao> selecionarAdquirenteComVariosFiltros(string valor)
        {
            AdquirenteCartaoBO bo = new AdquirenteCartaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarAdquirenteComVariosFiltros(valor);
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
