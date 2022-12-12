using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class BalancoEstoqueProdutoBO : BO
    {
        private BalancoEstoqueProdutoDAO dao;

        public BalancoEstoqueProdutoBO()
        {
            dao = new BalancoEstoqueProdutoDAO();
        }

        public void salvar(ObjetoPadrao balancoEstoqueProduto)
        {
            Boolean excluido = balancoEstoqueProduto.FlagExcluido;

            if (valida((BalancoEstoqueProduto)balancoEstoqueProduto))
            {
                if (((BalancoEstoqueProduto)balancoEstoqueProduto).Id == 0)
                    dao.incluir((BalancoEstoqueProduto)balancoEstoqueProduto);
                else
                    dao.alterar((BalancoEstoqueProduto)balancoEstoqueProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((BalancoEstoqueProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Balanço Produtos não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new BalancoEstoqueProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Itens!" + e.Message);
            }

        }

        private Boolean valida(BalancoEstoqueProduto balancoEstoqueProduto)
        {
            if (balancoEstoqueProduto.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            return true;
        }

        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalanco(int idBalancoEstoque)
        {
            try
            {
                return dao.selecionarProdutosPorBalanco(idBalancoEstoque);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos do balanço! Erro: " + e.Message);
            }
        }

        public IList<BalancoEstoqueProduto> selecionarProdutosPorBalancoSemRepetirItem(int idBalancoEstoque)
        {
            try
            {
                return dao.selecionarProdutosPorBalancoSemRepetirItem(idBalancoEstoque);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos do balanço! Erro: " + e.Message);
            }
        }

        public IList<Produto> selecionarProdutosParaZerarNaoContabilizados(int idBalancoEstoque)
        {
            try
            {
                return dao.selecionarProdutosParaZerarNaoContabilizados(idBalancoEstoque);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos para zerar! Erro: " + e.Message);
            }
        }
    }
}