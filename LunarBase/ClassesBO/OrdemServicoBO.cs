using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class OrdemServicoBO : BO
    {
        private OrdemServicoDAO dao;

        public OrdemServicoBO()
        {
            dao = new OrdemServicoDAO();
        }

        public void salvar(ObjetoPadrao ordemServico)
        {
            Boolean excluido = ordemServico.FlagExcluido;

            if (valida((OrdemServico)ordemServico))
            {
                if (((OrdemServico)ordemServico).Id == 0)
                    dao.incluir((OrdemServico)ordemServico);
                else
                    dao.alterar((OrdemServico)ordemServico);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrdemServico)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Ordem Servico não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrdemServicoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Ordem de Servico!" + e.Message);
            }

        }

        private Boolean valida(OrdemServico ordemServico)
        {
            return true;
        }

        public void salvarOrdemServicoComItensAdicionais(OrdemServico ordemServico, IList<OrdemServicoProduto> listaProdutos, IList<OrdemServicoServico> listaServicos, IList<OrdemServicoExame> listaExames, IList<Anexo> listaAnexo)
        {
            try
            {
                salvar(ordemServico);
                OrdemServicoProdutoBO ordemServicoProdutoBO = new OrdemServicoProdutoBO();
                OrdemServicoServicoBO ordemServicoServicoBO = new OrdemServicoServicoBO();
                OrdemServicoExameBO ordemServicoExameBO = new OrdemServicoExameBO();
                AnexoBO anexoBO = new AnexoBO();

                if (listaProdutos.Count > 0)
                {
                    foreach (OrdemServicoProduto ordemServicoProduto in listaProdutos)
                    {
                        ordemServicoProdutoBO.salvar(ordemServicoProduto);
                    }
                }
                if (listaServicos.Count > 0)
                {
                    foreach (OrdemServicoServico ordemServicoServico in listaServicos)
                    {
                        ordemServicoServicoBO.salvar(ordemServicoServico);
                    }
                }
                if (listaExames.Count > 0)
                {
                    foreach (OrdemServicoExame ordemServicoExame in listaExames)
                    {
                        ordemServicoExameBO.salvar(ordemServicoExame);
                    }
                }
                if (listaAnexo.Count > 0)
                {
                    foreach (Anexo anexo in listaAnexo)
                    {
                        anexoBO.salvar(anexo);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<OrdemServico> selecionarOrdemServicoPorSQL(string sql)
        {
            try
            {
                return dao.selecionarOrdemServicoPorSQL(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ordem Serviço! Erro: " + e.Message);
            }
        }

        public IList<OrdemServico> selecionarTodasOS()
        {
            try
            {
                return dao.selecionarTodasOS();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ordem Serviço! Erro: " + e.Message);
            }
        }
        public OrdemServico selecionarOrdemServicoPorNfe(int idNfe)
        {
            try
            {
                return dao.selecionarOrdemServicoPorNfe(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ordem de Servico! Erro: " + e.Message);
            }
        }

        public OrdemServico selecionarOrdemServicoPorID(int idOS)
        {
            try
            {
                return dao.selecionarOrdemServicoPorID(idOS);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Ordem de Servico! Erro: " + e.Message);
            }
        }
    }
}