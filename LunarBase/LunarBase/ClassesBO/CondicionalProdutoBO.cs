using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CondicionalProdutoBO : BO
    {
        private CondicionalProdutoDAO dao;

        public CondicionalProdutoBO()
        {
            dao = new CondicionalProdutoDAO();
        }

        public void salvar(ObjetoPadrao condicionalProduto)
        {
            Boolean excluido = condicionalProduto.FlagExcluido;

            if (valida((CondicionalProduto)condicionalProduto))
            {
                if (((CondicionalProduto)condicionalProduto).Id == 0)
                    dao.incluir((CondicionalProduto)condicionalProduto);
                else
                    dao.alterar((CondicionalProduto)condicionalProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CondicionalProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Condicional Produto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CondicionalProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Condicional Produto!" + e.Message);
            }

        }

        private Boolean valida(CondicionalProduto condicionalProduto)
        {
            if (condicionalProduto.Condicional == null)
            {
                throw new Exception("O campo \"Condicional\" é obrigatório!");
            }
            return true;
        }
        public IList<CondicionalProduto> selecionarProdutosPorCondicional(int idCondicional)
        {
            try
            {
                return dao.selecionarProdutosPorCondicional(idCondicional);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da condicional! Erro: " + e.Message);
            }
        }
        public IList<CondicionalProduto> selecionarProdutosCondicionalComVariosFiltros(string valor, int idCondicional)
        {
            try
            {
                return dao.selecionarProdutosCondicionalComVariosFiltros(valor, idCondicional);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }
    }
}