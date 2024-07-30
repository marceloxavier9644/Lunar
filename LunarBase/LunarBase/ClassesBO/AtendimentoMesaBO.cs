using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoMesaBO : BO
    {
        private AtendimentoMesaDAO dao;

        public AtendimentoMesaBO()
        {
            dao = new AtendimentoMesaDAO();
        }

        public void salvar(ObjetoPadrao atendimentoMesa)
        {
            Boolean excluido = atendimentoMesa.FlagExcluido;

            if (valida((AtendimentoMesa)atendimentoMesa))
            {
                if (((AtendimentoMesa)atendimentoMesa).Id == 0)
                    dao.incluir((AtendimentoMesa)atendimentoMesa);
                else
                    dao.alterar((AtendimentoMesa)atendimentoMesa);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AtendimentoMesa)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Mesa/Comanda não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoMesaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Mesa/Comanda!" + e.Message);
            }

        }

        private Boolean valida(AtendimentoMesa atendimentoMesa)
        {
            if (string.IsNullOrWhiteSpace(atendimentoMesa.Numero))
            {
                throw new Exception("O campo \"Numero\" é obrigatório!");
            }
            return true;
        }

        public IList<AtendimentoMesa> selecionarTodasMesas()
        {
            try
            {
                return dao.selecionarTodasMesas();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Mesas! Erro: " + e.Message);
            }
        }
    }
}