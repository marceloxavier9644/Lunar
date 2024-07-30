using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoGrupoBO : BO
    {
        private ProdutoGrupoDAO dao;

        public ProdutoGrupoBO()
        {
            dao = new ProdutoGrupoDAO();
        }

        public void salvar(ObjetoPadrao produtoGrupo)
        {
            Boolean excluido = produtoGrupo.FlagExcluido;

            if (valida((ProdutoGrupo)produtoGrupo))
            {
                if (((ProdutoGrupo)produtoGrupo).Id == 0)
                    dao.incluir((ProdutoGrupo)produtoGrupo);
                else
                    dao.alterar((ProdutoGrupo)produtoGrupo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoGrupo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("ProdutoGrupo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoGrupoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar ProdutoGrupo!" + e.Message);
            }

        }

        private Boolean valida(ProdutoGrupo produtoGrupo)
        {
            if (string.IsNullOrWhiteSpace(produtoGrupo.Descricao))
            {
                throw new Exception("O campo \"Grupo\" é obrigatório!");
            }
            return true;
        }

        public IList<ProdutoGrupo> selecionarGrupoComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarGrupoComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar grupo! Erro: " + e.Message);
            }
        }
    }
}