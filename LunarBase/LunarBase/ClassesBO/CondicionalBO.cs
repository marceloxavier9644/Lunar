using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CondicionalBO : BO
    {
        private CondicionalDAO dao;

        public CondicionalBO()
        {
            dao = new CondicionalDAO();
        }

        public void salvar(ObjetoPadrao condicional)
        {
            Boolean excluido = condicional.FlagExcluido;

            if (valida((Condicional)condicional))
            {
                if (((Condicional)condicional).Id == 0)
                    dao.incluir((Condicional)condicional);
                else
                    dao.alterar((Condicional)condicional);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Condicional)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Condicional não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CondicionalDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar condicional!" + e.Message);
            }

        }

        private Boolean valida(Condicional condicional)
        {
            if (condicional.Cliente == null)
            {
                throw new Exception("O campo \"Cliente\" é obrigatório!");
            }
            return true;
        }

        public void salvarCondicionalComProdutos(Condicional condicional, IList<CondicionalProduto> listaProdutos)
        {
            try
            {
                salvar(condicional);
                CondicionalProdutoBO condicionalProdutoBO = new CondicionalProdutoBO();

                if (listaProdutos.Count > 0)
                {
                    foreach (CondicionalProduto condicionalProduto in listaProdutos)
                    {
                        condicionalProdutoBO.salvar(condicionalProduto);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<Condicional> selecionarCondicionalSql(string sql)
        {
            try
            {
                return dao.selecionarCondicionalPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Condicional! Erro: " + e.Message);
            }
        }

       
    }
}