using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class CidadeController : Controller
    {
        public Cidade selecionarCidadePorDescricao(string descricao)
        {
            CidadeBO bo = new CidadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCidadePorDescricao(descricao);
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
        public Cidade selecionarCidadePorDescricaoEUf(string descricao, string uf)
        {
            CidadeBO bo = new CidadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCidadePorDescricaoEUf(descricao, uf);
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

        public Cidade selecionarCidadePorDescricaoEIBGE(string descricao, string codigoIBGE)
        {
            CidadeBO bo = new CidadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarCidadePorDescricaoECodigoIBGE(descricao, codigoIBGE);
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
        public IList<Cidade> selecionarListaCidadePorDescricao(string descricao)
        {
            CidadeBO bo = new CidadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarListaCidadePorDescricao(descricao);
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

        public IList<Cidade> selecionarTodasCidades()
        {
            CidadeBO bo = new CidadeBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasCidades();
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
