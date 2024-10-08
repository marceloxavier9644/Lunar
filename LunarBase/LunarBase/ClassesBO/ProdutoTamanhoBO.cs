using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoTamanhoBO : BO
    {
        private ProdutoTamanhoDAO dao;

        public ProdutoTamanhoBO()
        {
            dao = new ProdutoTamanhoDAO();
        }

        public void salvar(ObjetoPadrao produtoTamanho)
        {
            Boolean excluido = produtoTamanho.FlagExcluido;

            if (valida((ProdutoTamanho)produtoTamanho))
            {
                if (((ProdutoTamanho)produtoTamanho).Id == 0)
                    dao.incluir((ProdutoTamanho)produtoTamanho);
                else
                    dao.alterar((ProdutoTamanho)produtoTamanho);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoTamanho)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("ProdutoTamanho não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoTamanhoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar ProdutoTamanho!" + e.Message);
            }

        }

        private Boolean valida(ProdutoTamanho produtoTamanho)
        {
            if (string.IsNullOrWhiteSpace(produtoTamanho.Descricao))
            {
                throw new Exception("O campo \"Descrição do Tamanho\" é obrigatório!");
            }
            return true;
        }

        
    }     
}