using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoCaracteristicaBO : BO
    {
        private ProdutoCaracteristicaDAO dao;

        public ProdutoCaracteristicaBO()
        {
            dao = new ProdutoCaracteristicaDAO();
        }

        public void salvar(ObjetoPadrao produtoCaracteristica)
        {
            Boolean excluido = produtoCaracteristica.FlagExcluido;

            if (valida((ProdutoCaracteristica)produtoCaracteristica))
            {
                if (((ProdutoCaracteristica)produtoCaracteristica).Id == 0)
                    dao.incluir((ProdutoCaracteristica)produtoCaracteristica);
                else
                    dao.alterar((ProdutoCaracteristica)produtoCaracteristica);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoCaracteristica)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Produto Caracteristica não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoCaracteristicaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto Caracteristica!" + e.Message);
            }

        }

        private Boolean valida(ProdutoCaracteristica produtoCaracteristica)
        {
            return true;
        }

        public IList<ProdutoCaracteristica> selecionarProdutoCaracteristica(int idProduto)
        {
            try
            {
                return dao.selecionarProdutoCaracteristica(idProduto);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar ProdutoCaracteristica! Erro: " + e.Message);
            }
        }
       
    }
}