using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class EstadoBO : BO
    {
        private EstadoDAO dao;

        public EstadoBO()
        {
            dao = new EstadoDAO();
        }

        public void salvar(ObjetoPadrao estado)
        {
            Boolean excluido = estado.FlagExcluido;

            if (valida((Estado)estado))
            {
                if (((Estado)estado).Id == 0)
                    dao.incluir((Estado)estado);
                else
                    dao.alterar((Estado)estado);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Estado)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Estado não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new EstadoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Estado!" + e.Message);
            }

        }

        private Boolean valida(Estado estado)
        {
            if (string.IsNullOrWhiteSpace(estado.Descricao))
            {
                throw new Exception("O campo \"ESTADO\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(estado.Ibge))
            {
                throw new Exception("O campo \"IBGE\" é obrigatório!");
            }
            return true;
        }

        public Estado selecionarEstadoPorUF(String uf)
        {
            try
            {
                return dao.selecionarEstadoPorUF(uf);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar estado! Erro: " + e.Message);
            }
        }
    }
}