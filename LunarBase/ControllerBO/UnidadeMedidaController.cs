using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class UnidadeMedidaController : Controller
    {
        public void salvarSeNaoExistir(UnidadeMedida unidadeMedida)
        {
            try
            {
                Conexao.IniciaTransacao();
                unidadeMedida.DataCadastro = DateTime.Now;
                unidadeMedida.OperadorCadastro = "1";
                UnidadeMedidaBO bo = new UnidadeMedidaBO();
                bo.salvarSeNaoExistir(unidadeMedida);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + unidadeMedida.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public UnidadeMedida selecionarUnidadeMedidaPorSigla(string sigla)
        {
            UnidadeMedidaBO bo = new UnidadeMedidaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUnidadeMedidaPorSigla(sigla);
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

        public IList<UnidadeMedida> selecionarUnidadeMedidaComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            UnidadeMedidaBO bo = new UnidadeMedidaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUnidadeMedidaComVariosFiltros(valor, empresa);
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
