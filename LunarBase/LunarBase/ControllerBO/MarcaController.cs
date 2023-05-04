using LunarBase.Anotations;
using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class MarcaController : Controller
    {
        public void salvarSeNaoExistir(Marca marca)
        {
            try
            {
                Conexao.IniciaTransacao();
                marca.DataCadastro = DateTime.Now;
                marca.OperadorCadastro = "1";
                MarcaBO bo = new MarcaBO();
                bo.salvarSeNaoExistir(marca);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar " + marca.GetType().GetCustomAttributes(typeof(Anotacao), false)[0].ToString() + ": " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Marca> selecionarMarcaComVariosFiltros(string valor)
        {
            MarcaBO bo = new MarcaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarMarcaComVariosFiltros(valor);
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

        public Marca selecionarMarcaPorDescricao(string descricao)
        {
            MarcaBO bo = new MarcaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarMarcaPorDescricao(descricao);
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
