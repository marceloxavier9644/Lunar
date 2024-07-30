using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class VendaItensBO : BO
    {
        private VendaItensDAO dao;

        public VendaItensBO()
        {
            dao = new VendaItensDAO();
        }

        public void salvar(ObjetoPadrao vendaItens)
        {
            Boolean excluido = vendaItens.FlagExcluido;

            if (valida((VendaItens)vendaItens))
            {
                if (((VendaItens)vendaItens).Id == 0)
                    dao.incluir((VendaItens)vendaItens);
                else
                    dao.alterar((VendaItens)vendaItens);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((VendaItens)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Venda Itens não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new VendaItensDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Venda Itens!" + e.Message);
            }

        }

        private Boolean valida(VendaItens vendaItens)
        {
            if (vendaItens.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            return true;
        }
        public IList<VendaItens> selecionarProdutosVendidosPorPeriodo(EmpresaFilial filial, string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarProdutosVendidosPorPeriodo(filial, dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos! Erro: " + e.Message);
            }
        }

        public IList<VendaItens> selecionarProdutosPorVenda(int idVenda)
        {
            try
            {
                return dao.selecionarProdutosPorVenda(idVenda);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da venda! Erro: " + e.Message);
            }
        }
    }
}
