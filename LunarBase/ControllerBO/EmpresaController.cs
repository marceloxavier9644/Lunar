using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class EmpresaController : Controller
    {
        public Empresa selecionarEmpresaPorCPFCNPJ(string cpfCNPJ)
        {
            EmpresaBO bo = new EmpresaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEmpresaPorCPFCNPJ(cpfCNPJ);
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

        public IList<Empresa> selecionarTodasEmpresas()
        {
            EmpresaBO bo = new EmpresaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasEmpresas();
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

        public void salvarSeNaoExistir(Empresa empresa)
        {
            try
            {
                Conexao.IniciaTransacao();
                empresa.DataCadastro = DateTime.Now;
                empresa.OperadorCadastro = "1";
                EmpresaBO bo = new EmpresaBO();
                bo.salvarSeNaoExistir(empresa);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + empresa.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
