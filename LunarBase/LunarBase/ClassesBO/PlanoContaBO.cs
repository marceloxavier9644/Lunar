using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PlanoContaBO : BO
    {
        private PlanoContaDAO dao;

        public PlanoContaBO()
        {
            dao = new PlanoContaDAO();
        }

        public void salvar(ObjetoPadrao planoConta)
        {
            Boolean excluido = planoConta.FlagExcluido;

            if (valida((PlanoConta)planoConta))
            {
                if (((PlanoConta)planoConta).Id == 0)
                    dao.incluir((PlanoConta)planoConta);
                else
                    dao.alterar((PlanoConta)planoConta);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PlanoConta)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Plano de Conta não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PlanoContaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Plano de Conta!" + e.Message);
            }

        }

        private Boolean valida(PlanoConta planoConta)
        {
            if (string.IsNullOrWhiteSpace(planoConta.Descricao))
            {
                throw new Exception("O campo \"Plano de Contas\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(PlanoConta planoConta)
        {
            try
            {
                PlanoConta planoContaAux = (PlanoConta)dao.Selecionar(planoConta, ((PlanoConta)planoConta).Id);
                if (planoContaAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(planoConta.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }
                dao.incluir((PlanoConta)planoConta);
            }
        }

        public IList<PlanoConta> selecionarPlanoContaComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarPlanoContaComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar plano Conta! Erro: " + e.Message);
            }
        }

        public IList<PlanoConta> selecionarTodosPlanosConta()
        {
            try
            {
                return dao.selecionarTodosPlanosConta();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar plano Conta! Erro: " + e.Message);
            }
        }
    }
}