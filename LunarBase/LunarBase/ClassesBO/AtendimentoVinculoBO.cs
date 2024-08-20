using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoVinculoBO : BO
    {
        private AtendimentoVinculoDAO dao;

        public AtendimentoVinculoBO()
        {
            dao = new AtendimentoVinculoDAO();
        }

        public void salvar(ObjetoPadrao atendimentoVinculo)
        {
            Boolean excluido = atendimentoVinculo.FlagExcluido;

            if (valida((AtendimentoVinculo)atendimentoVinculo))
            {
                if (((AtendimentoVinculo)atendimentoVinculo).Id == 0)
                    dao.incluir((AtendimentoVinculo)atendimentoVinculo);
                else
                    dao.alterar((AtendimentoVinculo)atendimentoVinculo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AtendimentoVinculo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("AtendimentoVinculo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoVinculoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Atendimento Vinculo!" + e.Message);
            }

        }

        private Boolean valida(AtendimentoVinculo atendimentoVinculo)
        {
            return true;
        }

        public IList<AtendimentoVinculo> selecionarVinculosPorAtendimento(int idAtendimento)
        {
            try
            {
                return dao.selecionarVinculosPorAtendimento(idAtendimento);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Atendimento Vinculo! Erro: " + e.Message);
            }
        }
    }
}