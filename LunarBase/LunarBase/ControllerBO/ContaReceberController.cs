using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class ContaReceberController : Controller
    {
        public IList<ContaReceber> selecionarContaReceberPorVendaFormaPagamento(int idVendaFormaPagamento)
        {
            ContaReceberBO bo = new ContaReceberBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberPorVendaFormaPagamento(idVendaFormaPagamento);
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

        public IList<ContaReceber> selecionarContaReceberPorVenda(int idVenda)
        {
            ContaReceberBO bo = new ContaReceberBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberPorVenda(idVenda);
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

        public IList<ContaReceber> selecionarContaReceberPorOrdemServico(int idOrdemServico)
        {
            ContaReceberBO bo = new ContaReceberBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberPorOrdemServico(idOrdemServico);
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

        public IList<ContaReceber> selecionarContaReceberPorSql(string sql)
        {
            ContaReceberBO bo = new ContaReceberBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberPorSql(sql);
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
        public IList<ContaReceber> selecionarContaReceberPorSqlNativo(string sql)
        {
            ContaReceberBO bo = new ContaReceberBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarContaReceberPorSqlNativo(sql);
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
