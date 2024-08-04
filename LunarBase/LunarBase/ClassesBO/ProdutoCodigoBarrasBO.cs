using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ProdutoCodigoBarrasBO : BO
    {
        private ProdutoCodigoBarrasDAO dao;

        public ProdutoCodigoBarrasBO()
        {
            dao = new ProdutoCodigoBarrasDAO();
        }

        public void salvar(ObjetoPadrao produtoCodigoBarras)
        {
            Boolean excluido = produtoCodigoBarras.FlagExcluido;

            if (valida((ProdutoCodigoBarras)produtoCodigoBarras))
            {
                if (((ProdutoCodigoBarras)produtoCodigoBarras).Id == 0)
                    dao.incluir((ProdutoCodigoBarras)produtoCodigoBarras);
                else
                    dao.alterar((ProdutoCodigoBarras)produtoCodigoBarras);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ProdutoCodigoBarras)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Codigo Barras não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ProdutoCodigoBarrasDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Codigo de Barras!" + e.Message);
            }

        }

        private Boolean valida(ProdutoCodigoBarras produtoCodigoBarras)
        {
            return true;
        }

        public IList<ProdutoCodigoBarras> selecionarCodigoBarrasPorSQL(string sql)
        {
            try
            {
                return dao.selecionarCodigoBarrasPorSQL(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Codigo Barras! Erro: " + e.Message);
            }
        }
        public ProdutoCodigoBarras selecionarCodigoBarrasPorGrade(int idGrade)
        {
            try
            {
                return dao.selecionarCodigoBarrasPorGrade(idGrade);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar codigo barras! Erro: " + e.Message);
            }
        }
    }
}