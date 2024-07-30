using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class EstoqueBO : BO
    {
        private EstoqueDAO dao;

        public EstoqueBO()
        {
            dao = new EstoqueDAO();
        }

        public void salvar(ObjetoPadrao estoque)
        {
            Boolean excluido = estoque.FlagExcluido;

            if (valida((Estoque)estoque))
            {
                if (((Estoque)estoque).Id == 0)
                    dao.incluir((Estoque)estoque);
                else
                    dao.alterar((Estoque)estoque);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Estoque)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Estoque não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new EstoqueDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Estoque!" + e.Message);
            }

        }

        private Boolean valida(Estoque estoque)
        {
            if (string.IsNullOrWhiteSpace(estoque.Origem))
            {
                throw new Exception("O campo \"Origem\" é obrigatório!");
            }
            return true;
        }

        public IList<Estoque> selecionarEstoqueMovimentoPorProduto(EmpresaFilial empresa, int idProduto, bool conciliado)
        {
            try
            {
                return dao.selecionarEstoqueMovimentoPorProduto(empresa, idProduto, conciliado);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar movimento de estoque! Erro: " + e.Message);
            }
        }

        public IList<Estoque> selecionarEstoqueMovimentoPorBalanco(EmpresaFilial empresa, bool conciliado, BalancoEstoque balancoEstoque)
        {
            try
            {
                return dao.selecionarEstoqueMovimentoPorBalanco(empresa, conciliado, balancoEstoque);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar movimento de estoque! Erro: " + e.Message);
            }
        }

        public IList<Estoque> selecionarEstoqueMovimentoPorProdutoEData(EmpresaFilial empresa, int idProduto, bool conciliado, String dataInicial, String dataFinal)
        {
            try
            {
                return dao.selecionarEstoqueMovimentoPorProdutoEData(empresa, idProduto, conciliado, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar movimento de estoque! Erro: " + e.Message);
            }
        }
    }
}