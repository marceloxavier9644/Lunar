using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoBO : BO
    {
        private AtendimentoDAO dao;

        public AtendimentoBO()
        {
            dao = new AtendimentoDAO();
        }

        public void salvar(ObjetoPadrao atendimento)
        {
            Boolean excluido = atendimento.FlagExcluido;

            if (valida((Atendimento)atendimento))
            {
                if (((Atendimento)atendimento).Id == 0)
                    dao.incluir((Atendimento)atendimento);
                else
                    dao.alterar((Atendimento)atendimento);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Atendimento)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Atendimento não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Atendimento!" + e.Message);
            }

        }

        private Boolean valida(Atendimento atendimento)
        {
            return true;
        }
    }
}