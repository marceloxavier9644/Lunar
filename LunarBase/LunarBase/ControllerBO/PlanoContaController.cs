using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PlanoContaController : Controller
    {
        public void salvarSeNaoExistir(PlanoConta planoConta)
        {
            try
            {
                Conexao.IniciaTransacao();
                planoConta.DataCadastro = DateTime.Now;
                planoConta.OperadorCadastro = "1";
                PlanoContaBO bo = new PlanoContaBO();
                bo.salvarSeNaoExistir(planoConta);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + planoConta.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<PlanoConta> selecionarPlanoContaComVariosFiltros(string valor)
        {
            PlanoContaBO bo = new PlanoContaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPlanoContaComVariosFiltros(valor);
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
        public IList<PlanoConta> selecionarTodosPlanosConta()
        {
            PlanoContaBO bo = new PlanoContaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosPlanosConta();
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
