using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrigemIcmsController : Controller
    {
        public void salvarSeNaoExistir(OrigemIcms origemIcms)
        {
            try
            {
                Conexao.IniciaTransacao();
                origemIcms.DataCadastro = DateTime.Now;
                origemIcms.OperadorCadastro = "1";
                OrigemIcmsBO bo = new OrigemIcmsBO();
                bo.salvarSeNaoExistir(origemIcms);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + origemIcms.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<OrigemIcms> selecionarTodasOrigemIcms()
        {
            OrigemIcmsBO bo = new OrigemIcmsBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasOrigemIcms();
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

        public OrigemIcms selecionarOrigemPorCodigoDeOrigem(String codOrigem)
        {
            OrigemIcmsBO bo = new OrigemIcmsBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarOrigemPorCodigoDeOrigem(codOrigem);
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
