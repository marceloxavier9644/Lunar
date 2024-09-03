using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoBO : BO
    {
        private ProdutoDAO dao;

        public ProdutoBO()
        {
            dao = new ProdutoDAO();
        }

        public void salvar(ObjetoPadrao produto)
        {
            Boolean excluido = produto.FlagExcluido;

            if (valida((Produto)produto))
            {
                if (((Produto)produto).Id == 0)
                    dao.incluir((Produto)produto);
                else
                    dao.alterar((Produto)produto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Produto)objeto).Id);
                if (objeto != null)
                {
                    if (objeto.FlagExcluido == true)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Produto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto!" + e.Message);
            }

        }

        private Boolean valida(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                throw new Exception("O campo \"Descrição do Produto\" é obrigatório!");
            }
            if (produto.ValorVenda <= 0)
            {
                throw new Exception("O campo \"Valor de Venda\" é obrigatório!");
            }
            if (produto.UnidadeMedida == null)
            {
                throw new Exception("O campo \"Unidade de Medida\" é obrigatório!");
            }
            return true;
        }

        public Produto selecionarProdutoPorCodigoUnicoEFilial(int idFilial, EmpresaFilial empresaFilial)
        {
            try
            {
                return dao.selecionarProdutoPorCodigoUnicoEFilial(idFilial, empresaFilial);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produto! Erro: " + e.Message);
            }
        }


        public IList<Produto> selecionarTodosProdutorPorFilial(EmpresaFilial empresa)
        {
            try
            {
                return dao.selecionarTodosProdutorPorFilial(empresa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }

        public IList<Produto> selecionarProdutosComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            try
            {
                return dao.selecionarProdutosComVariosFiltros(valor, empresa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }

        public IList<Produto> selecionarProdutosPorSql(string sql)
        {
            try
            {
                return dao.selecionarProdutosPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }

        public IList<Produto> selecionarProdutoPorCodigoBarras(string valor)
        {
            try
            {
                return dao.selecionarProdutoPorCodigoBarras(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }

        public IList<Produto> selecionarTodosPaginando(int paginaAtual, int itensPorPagina, string valor)
        {
            dao = new ProdutoDAO();
            try
            {
                return dao.selecionarTodosProdutosPaginando(paginaAtual, itensPorPagina, valor);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto!" + e.Message);
            }

        }

        public Int64 totalTodosProdutosPaginando(string valor)
        {
            dao = new ProdutoDAO();
            try
            {
                return dao.totalTodosProdutosPaginando(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao totalizar Produto!" + e.Message);
            }

        }
    }
}