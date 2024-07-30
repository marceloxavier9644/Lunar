using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoProdutoBO : BO
    {
        private AtendimentoProdutoDAO dao;

        public AtendimentoProdutoBO()
        {
            dao = new AtendimentoProdutoDAO();
        }

        public void salvar(ObjetoPadrao atendimentoProduto)
        {
            Boolean excluido = atendimentoProduto.FlagExcluido;

            if (valida((AtendimentoProduto)atendimentoProduto))
            {
                if (((AtendimentoProduto)atendimentoProduto).Id == 0)
                    dao.incluir((AtendimentoProduto)atendimentoProduto);
                else
                    dao.alterar((AtendimentoProduto)atendimentoProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AtendimentoProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Atendimento Produto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Atendimento Produto!" + e.Message);
            }

        }

        private Boolean valida(AtendimentoProduto atendimentoProduto)
        {
            if (atendimentoProduto.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            return true;
        }
    }
}