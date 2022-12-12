using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ContaBancariaController : Controller
    {
        public IList<ContaBancaria> selecionarTodasContas()
        {
            ContaBancariaBO bo = new ContaBancariaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasContas();
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

        public IList<ContaBancaria> selecionarTodasContasPorFilial(int idFilial)
        {
            ContaBancariaBO bo = new ContaBancariaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasContasPorFilial(idFilial);
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

        public IList<ContaBancaria> selecionarContaBancariaComVariosFiltros(string valor)
        {
            ContaBancariaBO bo = new ContaBancariaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaBancariaComVariosFiltros(valor);
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
