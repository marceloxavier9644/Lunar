using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoSubGrupoBO : BO
    {
        private ProdutoSubGrupoDAO dao;

        public ProdutoSubGrupoBO()
        {
            dao = new ProdutoSubGrupoDAO();
        }

        public void salvar(ObjetoPadrao produtoSubGrupo)
        {
            Boolean excluido = produtoSubGrupo.FlagExcluido;

            if (valida((ProdutoSubGrupo)produtoSubGrupo))
            {
                if (((ProdutoSubGrupo)produtoSubGrupo).Id == 0)
                    dao.incluir((ProdutoSubGrupo)produtoSubGrupo);
                else
                    dao.alterar((ProdutoSubGrupo)produtoSubGrupo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoSubGrupo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("ProdutoSubGrupo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoSubGrupoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar ProdutoSubGrupo!" + e.Message);
            }

        }

        private Boolean valida(ProdutoSubGrupo produtoSubGrupo)
        {
            if (string.IsNullOrWhiteSpace(produtoSubGrupo.Descricao))
            {
                throw new Exception("O campo \"ProdutoSubGrupo\" é obrigatório!");
            }
            if (produtoSubGrupo.ProdutoGrupo is null)
            {
                throw new Exception("O campo \"Grupo\" é obrigatório!");
            }
            return true;
        }

        public IList<ProdutoSubGrupo> selecionarProdutoSubGrupoComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarProdutoSubGrupoComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar subGrupo! Erro: " + e.Message);
            }
        }
    }
}