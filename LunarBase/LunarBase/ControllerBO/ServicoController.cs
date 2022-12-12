using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ServicoController : Controller
    {
        public IList<Servico> selecionarTodosServicos()
        {
            ServicoBO bo = new ServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosServicos();
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

        public IList<Servico> selecionarServicoComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            ServicoBO bo = new ServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarServicoComVariosFiltros(valor, empresa);
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
