using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class NfeController : Controller
    {
        public Nfe selecionarNotaPorChave(string chave)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotaPorChave(chave);
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

        public Nfe selecionarNFCePorNumeroESerie(string numero, string serie)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNFCePorNumeroESerie(numero, serie);
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
        public Nfe selecionarNFePorNumeroESerie(string numero, string serie)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNFePorNumeroESerie(numero, serie);
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
        public int selecionarUltimoNsu()
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUltimoNsu();
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

        public string selecionarUltimaDataNotaBaixada()
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUltimaDataNotaBaixada();
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

        public IList<Nfe> selecionarNotasEntradaPorPeriodo(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasEntradaPorPeriodo(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotasSaidaPorPeriodo(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasSaidaPorPeriodo(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotasSaidaModelo65PorPeriodo(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasSaidaModelo65PorPeriodo(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotasEmitidasPorPeriodo(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasEmitidasPorPeriodo(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasEntradaESaidaPorPeriodoParaSintegraReg5054(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotasSaidaEmContigenciaPorPeriodo(string dataInicial, string dataFinal)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotasSaidaEmContigenciaPorPeriodo(dataInicial, dataFinal);
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

        public IList<Nfe> selecionarNotaEntradaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotaEntradaComVariosFiltros(dataInicial, dataFinal, valorPesquisa);
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
        public IList<Nfe> selecionarNotaSaidaComVariosFiltros(string dataInicial, string dataFinal, string valorPesquisa, string modeloNota, string sqlAdicional)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarNotaSaidaComVariosFiltros(dataInicial, dataFinal, valorPesquisa, modeloNota, sqlAdicional);
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
        public Nfe selecionarUltimoNumeroNota(string modelo)
        {
            NfeBO bo = new NfeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarUltimoNumeroNota(modelo);
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

