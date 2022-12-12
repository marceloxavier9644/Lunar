using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NaturezaOperacaoController : Controller
    {
        public void salvarSeNaoExistir(NaturezaOperacao naturezaOperacao)
        {
            try
            {
                Conexao.IniciaTransacao();
                naturezaOperacao.DataCadastro = DateTime.Now;
                naturezaOperacao.OperadorCadastro = "1";
                NaturezaOperacaoBO bo = new NaturezaOperacaoBO();
                bo.salvarSeNaoExistir(naturezaOperacao);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + naturezaOperacao.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<NaturezaOperacao> selecionarTodasNaturezaOperacao()
        {
            NaturezaOperacaoBO bo = new NaturezaOperacaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasNaturezaOperacao();
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

        public IList<NaturezaOperacao> selecionarNaturezaOperacaoVariosFiltros(string valor)
        {
            NaturezaOperacaoBO bo = new NaturezaOperacaoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNaturezaOperacaoVariosFiltros(valor);
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
