using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AnpBO : BO
    {
        private AnpDAO dao;

        public AnpBO()
        {
            dao = new AnpDAO();
        }

        public void salvar(ObjetoPadrao anp)
        {
            Boolean excluido = anp.FlagExcluido;

            if (valida((Anp)anp))
            {
                if (((Anp)anp).Id == 0)
                    dao.incluir((Anp)anp);
                else
                    dao.alterar((Anp)anp);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Anp)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Anp não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AnpDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Anp!" + e.Message);
            }

        }

        private Boolean valida(Anp anp)
        {
            if (string.IsNullOrWhiteSpace(anp.Codigo))
            {
                throw new Exception("O campo \"Código\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(anp.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<Anp> selecionarTodosCodigosANP()
        {
            try
            {
                return dao.selecionarTodosCodigosANP();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Anp! Erro: " + e.Message);
            }
        }

        public IList<Anp> selecionarCodigoAnpPorCodigo(string codigoANP)
        {
            try
            {
                return dao.selecionarCodigoAnpPorCodigo(codigoANP);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Anp! Erro: " + e.Message);
            }
        }
    }
}