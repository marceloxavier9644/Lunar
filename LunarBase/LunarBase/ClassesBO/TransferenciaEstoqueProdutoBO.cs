using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class TransferenciaEstoqueProdutoBO : BO
    {
        private TransferenciaEstoqueProdutoDAO dao;

        public TransferenciaEstoqueProdutoBO()
        {
            dao = new TransferenciaEstoqueProdutoDAO();
        }

        public void salvar(ObjetoPadrao transferenciaEstoqueProduto)
        {
            Boolean excluido = transferenciaEstoqueProduto.FlagExcluido;

            if (valida((TransferenciaEstoqueProduto)transferenciaEstoqueProduto))
            {
                if (((TransferenciaEstoqueProduto)transferenciaEstoqueProduto).Id == 0)
                    dao.incluir((TransferenciaEstoqueProduto)transferenciaEstoqueProduto);
                else
                    dao.alterar((TransferenciaEstoqueProduto)transferenciaEstoqueProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TransferenciaEstoqueProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("TransferenciaEstoqueProduto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TransferenciaEstoqueProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar TransferenciaEstoqueProduto!" + e.Message);
            }

        }

        private Boolean valida(TransferenciaEstoqueProduto transferenciaEstoqueProduto)
        {
            if (transferenciaEstoqueProduto.Produto is null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            return true;
        }

        public IList<TransferenciaEstoqueProduto> selecionarProdutosPorTransferencia(int idTransferencia)
        {
            try
            {
                return dao.selecionarProdutosPorTransferencia(idTransferencia);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da transferencia! Erro: " + e.Message);
            }
        }
    }
}
