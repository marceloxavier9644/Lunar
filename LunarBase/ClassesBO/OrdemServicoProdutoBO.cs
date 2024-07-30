using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;
using LunarBase.Utilidades;

namespace LunarBase.ClassesBO
{
    public class OrdemServicoProdutoBO : BO
    {
        private OrdemServicoProdutoDAO dao;

        public OrdemServicoProdutoBO()
        {
            dao = new OrdemServicoProdutoDAO();
        }

        public void salvar(ObjetoPadrao ordemServicoProduto)
        {
            Boolean excluido = ordemServicoProduto.FlagExcluido;

            if (valida((OrdemServicoProduto)ordemServicoProduto))
            {
                if (((OrdemServicoProduto)ordemServicoProduto).Id == 0)
                    dao.incluir((OrdemServicoProduto)ordemServicoProduto);
                else
                    dao.alterar((OrdemServicoProduto)ordemServicoProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrdemServicoProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("OrdemServicoProduto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrdemServicoProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar OrdemServicoProduto!" + e.Message);
            }

        }

        private Boolean valida(OrdemServicoProduto ordemServicoProduto)
        {
            if (string.IsNullOrWhiteSpace(ordemServicoProduto.DescricaoProduto))
            {
                throw new Exception("O campo \"Descrição do Produto\" é obrigatório!");
            }
            if (ordemServicoProduto.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            return true;
        }

        public IList<OrdemServicoProduto> selecionarProdutosPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarProdutosPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da O.S! Erro: " + e.Message);
            }
        }

        public IList<OrdemServicoProduto> selecionarProdutosVendidosPorPeriodo(EmpresaFilial empresaFilial, String dataInicial, String dataFinal)
        {
            try
            {
                return dao.selecionarProdutosVendidosPorPeriodo(empresaFilial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da O.S! Erro: " + e.Message);
            }
        }
    }
}