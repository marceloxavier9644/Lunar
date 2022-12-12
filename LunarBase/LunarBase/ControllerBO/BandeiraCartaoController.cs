using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class BandeiraCartaoController : Controller
    {
        public IList<BandeiraCartao> selecionarTodasBandeiras()
        {
            BandeiraCartaoBO bo = new BandeiraCartaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasBandeiras();
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

        public void salvarSeNaoExistir(BandeiraCartao bandeiraCartao)
        {
            try
            {
                Conexao.IniciaTransacao();
                bandeiraCartao.DataCadastro = DateTime.Now;
                bandeiraCartao.OperadorCadastro = "1";
                BandeiraCartaoBO bo = new BandeiraCartaoBO();
                bo.salvarSeNaoExistir(bandeiraCartao);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + bandeiraCartao.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<BandeiraCartao> selecionarBandeiraCartaoComVariosFiltros(string valor)
        {
            BandeiraCartaoBO bo = new BandeiraCartaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarBandeiraCartaoComVariosFiltros(valor);
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

