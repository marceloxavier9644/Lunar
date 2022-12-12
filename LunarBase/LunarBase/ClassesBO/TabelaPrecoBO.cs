using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class TabelaPrecoBO : BO
    {
        private TabelaPrecoDAO dao;

        public TabelaPrecoBO()
        {
            dao = new TabelaPrecoDAO();
        }

        public void salvar(ObjetoPadrao tabelaPreco)
        {
            Boolean excluido = tabelaPreco.FlagExcluido;

            if (valida((TabelaPreco)tabelaPreco))
            {
                if (((TabelaPreco)tabelaPreco).Id == 0)
                    dao.incluir((TabelaPreco)tabelaPreco);
                else
                    dao.alterar((TabelaPreco)tabelaPreco);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TabelaPreco)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("TabelaPreco não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TabelaPrecoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar TabelaPreco!" + e.Message);
            }

        }

        private Boolean valida(TabelaPreco tabelaPreco)
        {
            if (string.IsNullOrWhiteSpace(tabelaPreco.Descricao))
            {
                throw new Exception("O campo \"Tabela de Preco\" é obrigatório!");
            }
            return true;
        }
    }
}