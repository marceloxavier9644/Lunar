using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class OrdemServicoExameBO : BO
    {
        private OrdemServicoExameDAO dao;

        public OrdemServicoExameBO()
        {
            dao = new OrdemServicoExameDAO();
        }

        public void salvar(ObjetoPadrao ordemServicoExame)
        {
            Boolean excluido = ordemServicoExame.FlagExcluido;

            if (valida((OrdemServicoExame)ordemServicoExame))
            {
                if (((OrdemServicoExame)ordemServicoExame).Id == 0)
                    dao.incluir((OrdemServicoExame)ordemServicoExame);
                else
                    dao.alterar((OrdemServicoExame)ordemServicoExame);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrdemServicoExame)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Ordem Servico Exame não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrdemServicoExameDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Ordem Servico Exame!" + e.Message);
            }

        }

        private Boolean valida(OrdemServicoExame ordemServicoExame)
        {
            return true;
        }

        public IList<OrdemServicoExame> selecionarExamesPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarExamesPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar exames da O.S! Erro: " + e.Message);
            }
        }
    }
}