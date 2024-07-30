using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ParcelamentoController : Controller
    {
        public void salvarSeNaoExistir(Parcelamento parcelamento)
        {
            try
            {
                Conexao.IniciaTransacao();
                parcelamento.DataCadastro = DateTime.Now;
                parcelamento.OperadorCadastro = "1";
                ParcelamentoBO bo = new ParcelamentoBO();
                bo.salvarSeNaoExistir(parcelamento);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + parcelamento.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Parcelamento> selecionarTodasCondicoesCredito()
        {
            ParcelamentoBO bo = new ParcelamentoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasCondicoesCredito();
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

        public IList<Parcelamento> selecionarTodasCondicoesDebito()
        {
            ParcelamentoBO bo = new ParcelamentoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasCondicoesDebito();
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
