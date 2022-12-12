using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ModuloBO : BO
    {
        private ModuloDAO dao;

        public ModuloBO()
        {
            dao = new ModuloDAO();
        }

        public void salvar(ObjetoPadrao modulo)
        {
            Boolean excluido = modulo.FlagExcluido;

            if (valida((Modulo)modulo))
            {
                if (((Modulo)modulo).Id == 0)
                    dao.incluir((Modulo)modulo);
                else
                    dao.alterar((Modulo)modulo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Modulo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Modulo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ModuloDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Modulo!" + e.Message);
            }

        }

        private Boolean valida(Modulo modulo)
        {
            if (string.IsNullOrWhiteSpace(modulo.Descricao))
            {
                throw new Exception("O campo \"Modulo\" é obrigatório!");
            }
            return true;
        }
    }
}