using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CaracteristicaController : Controller
    {
        public void salvarSeNaoExistir(Caracteristica caracteristica)
        {
            try
            {
                Conexao.IniciaTransacao();
                caracteristica.DataCadastro = DateTime.Now;
                caracteristica.OperadorCadastro = "1";
                CaracteristicaBO bo = new CaracteristicaBO();
                bo.salvarSeNaoExistir(caracteristica);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + caracteristica.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Caracteristica> selecionarCaracteristicaPorProduto(int idProduto)
        {
            CaracteristicaBO bo = new CaracteristicaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCaracteristicaPorProduto(idProduto);
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
