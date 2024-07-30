using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoGradeBO : BO
    {
        private ProdutoGradeDAO dao;

        public ProdutoGradeBO()
        {
            dao = new ProdutoGradeDAO();
        }

        public void salvar(ObjetoPadrao produtoGrade)
        {
            Boolean excluido = produtoGrade.FlagExcluido;

            if (valida((ProdutoGrade)produtoGrade))
            {
                if (((ProdutoGrade)produtoGrade).Id == 0)
                    dao.incluir((ProdutoGrade)produtoGrade);
                else
                    dao.alterar((ProdutoGrade)produtoGrade);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoGrade)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Produto Grade não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoGradeDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Produto Grade!" + e.Message);
            }

        }

        private Boolean valida(ProdutoGrade produtoGrade)
        {
            if(produtoGrade.UnidadeMedida == null)
            {
                throw new Exception("O campo \"Unidade de Medida\" é obrigatório!");
            }
            if (produtoGrade.Produto == null)
            {
                throw new Exception("O campo \"Produto\" é obrigatório!");
            }
            if (String.IsNullOrEmpty(produtoGrade.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<ProdutoGrade> selecionarGradePorProduto(int idProduto)
        {
            try
            {
                return dao.selecionarGradePorProduto(idProduto);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar grade! Erro: " + e.Message);
            }
        }
    }
}