using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CstIcmsController : Controller
    {
        public void salvarSeNaoExistir(CstIcms cstIcms)
        {
            try
            {
                Conexao.IniciaTransacao();
                cstIcms.DataCadastro = DateTime.Now;
                cstIcms.OperadorCadastro = "1";
                CstIcmsBO bo = new CstIcmsBO();
                bo.salvarSeNaoExistir(cstIcms);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + cstIcms.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<CstIcms> selecionarTodosCstIcms()
        {
            CstIcmsBO bo = new CstIcmsBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCstIcms();
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

        public IList<CstIcms> selecionarCstIcmsPorCst(string cst)
        {
            CstIcmsBO bo = new CstIcmsBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCstIcmsPorCst(cst);
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
