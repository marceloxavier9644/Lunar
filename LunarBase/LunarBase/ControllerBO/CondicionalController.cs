using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CondicionalController : Controller
    {
        public void salvarCondicionalComProdutos(Condicional condicional, IList<CondicionalProduto> listaProdutos)
        {
            CondicionalBO condicionalBO = new CondicionalBO();
            Conexao.IniciaTransacao();
            try
            {
                condicionalBO.salvarCondicionalComProdutos(condicional, listaProdutos);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar Condicional " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
        public IList<Condicional> selecionarCondicionalPorSql(string sql)
        {
            CondicionalBO bo = new CondicionalBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCondicionalSql(sql);
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
