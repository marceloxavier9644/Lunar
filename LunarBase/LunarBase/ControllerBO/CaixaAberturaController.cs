using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CaixaAberturaController : Controller
    {
        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuario(int idUsuario)
        {
            CaixaAberturaBO bo = new CaixaAberturaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarAberturaCaixaPorUsuario(idUsuario);
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

        public IList<CaixaAbertura> selecionarAberturaCaixaPorUsuarioEData(int idUsuario, string dataInicial, string dataFinal)
        {
            CaixaAberturaBO bo = new CaixaAberturaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarAberturaCaixaPorUsuarioEData(idUsuario, dataInicial, dataFinal);
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

        public IList<CaixaAbertura> selecionarAberturaCaixaPorData(string dataInicial, string dataFinal)
        {
            CaixaAberturaBO bo = new CaixaAberturaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarAberturaCaixaPorData(dataInicial, dataFinal);
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

        public IList<CaixaAbertura> selecionarTodosCaixasAbertos()
        {
            CaixaAberturaBO bo = new CaixaAberturaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosCaixasAbertos();
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
