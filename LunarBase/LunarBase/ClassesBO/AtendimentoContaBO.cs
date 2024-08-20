using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoContaBO : BO
    {
        private AtendimentoContaDAO dao;

        public AtendimentoContaBO()
        {
            dao = new AtendimentoContaDAO();
        }

        public void salvar(ObjetoPadrao atendimentoConta)
        {
            Boolean excluido = atendimentoConta.FlagExcluido;

            if (valida((AtendimentoConta)atendimentoConta))
            {
                if (((AtendimentoConta)atendimentoConta).Id == 0)
                    dao.incluir((AtendimentoConta)atendimentoConta);
                else
                    dao.alterar((AtendimentoConta)atendimentoConta);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AtendimentoConta)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Atendimento Conta não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoContaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Atendimento Conta!" + e.Message);
            }

        }

        private Boolean valida(AtendimentoConta atendimentoConta)
        {
            return true;
        }
        public IList<AtendimentoConta> selecionarTodosAtendimentoConta(int idAtendimento)
        {
            try
            {
                return dao.selecionarTodosAtendimentoConta(idAtendimento);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Atendimento Conta! Erro: " + e.Message);
            }
        }
    }
}
