using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoInsumoBO : BO
    {
        private ProdutoInsumoDAO dao;

        public ProdutoInsumoBO()
        {
            dao = new ProdutoInsumoDAO();
        }

        public void salvar(ObjetoPadrao produtoInsumo)
        {
            Boolean excluido = produtoInsumo.FlagExcluido;

            if (valida((ProdutoInsumo)produtoInsumo))
            {
                if (((ProdutoInsumo)produtoInsumo).Id == 0)
                    dao.incluir((ProdutoInsumo)produtoInsumo);
                else
                    dao.alterar((ProdutoInsumo)produtoInsumo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoInsumo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Produto Insumo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoInsumoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto Insumo!" + e.Message);
            }

        }

        private Boolean valida(ProdutoInsumo produtoInsumo)
        {
            if (produtoInsumo.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            if (produtoInsumo.Quantidade <= 0)
            {
                throw new Exception("O campo \"Quantidade\" é obrigatório!");
            }
            return true;
        }

        public IList<ProdutoInsumo> selecionarInsumoPorProduto(int idProduto)
        {
            try
            {
                return dao.selecionarInsumoPorProduto(idProduto);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar insumos! Erro: " + e.Message);
            }
        }
    }
}