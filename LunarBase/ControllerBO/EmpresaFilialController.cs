using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class EmpresaFilialController : Controller
    {
        public EmpresaFilial selecionarEmpresaFilialPorCPFCNPJ(string cpfCNPJ)
        {
            EmpresaFilialBO bo = new EmpresaFilialBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEmpresaFilialPorCPFCNPJ(cpfCNPJ);
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
        public IList<EmpresaFilial> selecionarTodasFiliais()
        {
            EmpresaFilialBO bo = new EmpresaFilialBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasFiliais();
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

        public IList<EmpresaFilial> selecionarEmpresaFilialComVariosFiltros(string valor)
        {
            EmpresaFilialBO bo = new EmpresaFilialBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarEmpresaFilialComVariosFiltros(valor);
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

        public void salvarSeNaoExistir(EmpresaFilial empresaFilial)
        {
            try
            {
                Conexao.IniciaTransacao();
                empresaFilial.DataCadastro = DateTime.Now;
                empresaFilial.OperadorCadastro = "1";
                EmpresaFilialBO bo = new EmpresaFilialBO();
                bo.salvarSeNaoExistir(empresaFilial);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + empresaFilial.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
