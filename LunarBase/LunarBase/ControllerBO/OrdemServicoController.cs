using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class OrdemServicoController : Controller
    {
        public void salvarOrdemServicoComItensAdicionais(OrdemServico ordemServico, IList<OrdemServicoProduto> listaProdutos, IList<OrdemServicoServico> listaServicos, IList<OrdemServicoExame> listaExames, IList<Anexo> listaAnexo)
        {
            OrdemServicoBO ordemServicoBO = new OrdemServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                ordemServicoBO.salvarOrdemServicoComItensAdicionais(ordemServico, listaProdutos, listaServicos, listaExames, listaAnexo);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar Ordem de Serviço " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<OrdemServico> selecionarOrdemServicoPorSQL(string sql)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarOrdemServicoPorSQL(sql);
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

        public OrdemServico selecionarOrdemServicoPorNfe(int idNfe)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarOrdemServicoPorNfe(idNfe);
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

        public OrdemServico selecionarOrdemServicoPorID(int idOS)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarOrdemServicoPorID(idOS);
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

        public IList<OrdemServico> selecionarTodasOS()
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasOS();
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
