using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class GrupoFiscalController : Controller
    {
        public void salvarSeNaoExistir(GrupoFiscal grupoFiscal)
        {
            try
            {
                Conexao.IniciaTransacao();
                grupoFiscal.DataCadastro = DateTime.Now;
                grupoFiscal.OperadorCadastro = "1";
                GrupoFiscalBO bo = new GrupoFiscalBO();
                bo.salvarSeNaoExistir(grupoFiscal);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + grupoFiscal.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<GrupoFiscal> selecionarGrupoFiscalComVariosFiltros(string valor)
        {
            GrupoFiscalBO bo = new GrupoFiscalBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarGrupoFiscalComVariosFiltros(valor);
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
