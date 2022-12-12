using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class OrdemServicoServicoBO : BO
    {
        private OrdemServicoServicoDAO dao;

        public OrdemServicoServicoBO()
        {
            dao = new OrdemServicoServicoDAO();
        }

        public void salvar(ObjetoPadrao ordemServicoServico)
        {
            Boolean excluido = ordemServicoServico.FlagExcluido;

            if (valida((OrdemServicoServico)ordemServicoServico))
            {
                if (((OrdemServicoServico)ordemServicoServico).Id == 0)
                    dao.incluir((OrdemServicoServico)ordemServicoServico);
                else
                    dao.alterar((OrdemServicoServico)ordemServicoServico);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrdemServicoServico)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("OrdemServicoServico não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrdemServicoServicoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar OrdemServicoServico!" + e.Message);
            }

        }

        private Boolean valida(OrdemServicoServico ordemServicoServico)
        {
            if (string.IsNullOrWhiteSpace(ordemServicoServico.DescricaoServico))
            {
                throw new Exception("O campo \"Descrição do Serviço\" é obrigatório!");
            }
            if (ordemServicoServico.Servico == null)
            {
                throw new Exception("O campo \"Serviço\" é obrigatório!");
            }
            return true;
        }

        public IList<OrdemServicoServico> selecionarServicosPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarServicosPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar serviços da O.S! Erro: " + e.Message);
            }
        }
    }
}