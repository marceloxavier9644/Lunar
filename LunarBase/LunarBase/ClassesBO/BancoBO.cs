using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class BancoBO : BO
    {
        private BancoDAO dao;

        public BancoBO()
        {
            dao = new BancoDAO();
        }

        public void salvar(ObjetoPadrao banco)
        {
            Boolean excluido = banco.FlagExcluido;

            if (valida((Banco)banco))
            {
                if (((Banco)banco).Id == 0)
                    dao.incluir((Banco)banco);
                else
                    dao.alterar((Banco)banco);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Banco)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Banco não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new BancoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Banco!" + e.Message);
            }

        }

        private Boolean valida(Banco banco)
        {
            if (string.IsNullOrWhiteSpace(banco.Descricao))
            {
                throw new Exception("O campo \"Banco\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(banco.CodBanco))
            {
                throw new Exception("O campo \"Código do Banco\" é obrigatório!");
            }
            return true;
        }

        public IList<Banco> selecionarTodosBancos()
        {
            try
            {
                return dao.selecionarTodosBancos();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar bancos! Erro: " + e.Message);
            }
        }
        public IList<Banco> selecionarBancosComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarBancosComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar bancos! Erro: " + e.Message);
            }
        }
    }
}