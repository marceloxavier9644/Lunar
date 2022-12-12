using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CaixaController : Controller
    {
        public IList<Caixa> selecionarCaixaPorOrigem(string tabelaOrigem, string idOrigem)
        {
            CaixaBO bo = new CaixaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCaixaPorOrigem(tabelaOrigem, idOrigem);
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

        public IList<Caixa> selecionarCaixaPorUsuarioEDataCadastro(int idUsuario, string dataInicial, string dataFinal)
        {
            CaixaBO bo = new CaixaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCaixaPorUsuarioEDataCadastro(idUsuario, dataInicial, dataFinal);
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

        public IList<Caixa> selecionarCaixaPorSql(String sql)
        {
            CaixaBO bo = new CaixaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCaixaPorSql(sql);
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
