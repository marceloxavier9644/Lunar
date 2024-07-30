using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CondicionalDevolucaoBO : BO
    {
        private CondicionalDevolucaoDAO dao;

        public CondicionalDevolucaoBO()
        {
            dao = new CondicionalDevolucaoDAO();
        }

        public void salvar(ObjetoPadrao condicionalDevolucao)
        {
            Boolean excluido = condicionalDevolucao.FlagExcluido;

            if (valida((CondicionalDevolucao)condicionalDevolucao))
            {
                if (((CondicionalDevolucao)condicionalDevolucao).Id == 0)
                    dao.incluir((CondicionalDevolucao)condicionalDevolucao);
                else
                    dao.alterar((CondicionalDevolucao)condicionalDevolucao);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CondicionalDevolucao)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Condicional Devolução não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CondicionalDevolucaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Condicional Devoluçao!" + e.Message);
            }

        }

        private Boolean valida(CondicionalDevolucao condicionalDevolucao)
        {
            if (condicionalDevolucao.Condicional == null)
            {
                throw new Exception("O campo \"Condicional\" é obrigatório!");
            }
            return true;
        }
        public IList<CondicionalDevolucao> selecionarProdutosDevolvidosPorCondicional(int idCondicional)
        {
            try
            {
                return dao.selecionarProdutosDevolvidosPorCondicional(idCondicional);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos devolvidos da condicional! Erro: " + e.Message);
            }
        }
        public CondicionalDevolucao selecionarProdutoDevolvido(int idCondicional, int idProduto)
        {
            try
            {
                CondicionalDevolucao condicionalDevolucao = dao.selecionarProdutoDevolvido(idCondicional, idProduto);
                return condicionalDevolucao;
            }
            catch (Exception e)
            {
                throw new Exception("Produto Devolvido não encontrado!" + e.ToString());
            }
        }
    }
}