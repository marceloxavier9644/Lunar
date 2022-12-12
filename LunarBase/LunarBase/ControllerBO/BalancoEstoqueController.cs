using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class BalancoEstoqueController : Controller
    {
        public IList<BalancoEstoque> selecionarTodosBalancoEstoque(EmpresaFilial empresaFilial)
        {
            BalancoEstoqueBO bo = new BalancoEstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosBalancoEstoque(empresaFilial);
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

        public IList<BalancoEstoque> selecionarBalancoEstoquePorSql(string sql)
        {
            BalancoEstoqueBO bo = new BalancoEstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarBalancoEstoquePorSql(sql);
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

        public void salvarBalancoEstoqueComItens(BalancoEstoque balancoEstoque, IList<BalancoEstoqueProduto> listaProdutos)
        {
            BalancoEstoqueBO balancoEstoqueBO = new BalancoEstoqueBO();
            Conexao.IniciaTransacao();
            try
            {
                balancoEstoqueBO.salvarBalancoEstoqueComItens(balancoEstoque, listaProdutos);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar Balanço de Estoque " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
