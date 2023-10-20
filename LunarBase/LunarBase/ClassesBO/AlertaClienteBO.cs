using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AlertaClienteBO : BO
    {
        private AlertaClienteDAO dao;

        public AlertaClienteBO()
        {
            dao = new AlertaClienteDAO();
        }

        public void salvar(ObjetoPadrao alertaCliente)
        {
            Boolean excluido = alertaCliente.FlagExcluido;

            if (valida((AlertaCliente)alertaCliente))
            {
                if (((AlertaCliente)alertaCliente).Id == 0)
                    dao.incluir((AlertaCliente)alertaCliente);
                else
                    dao.alterar((AlertaCliente)alertaCliente);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AlertaCliente)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Alerta Cliente não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AlertaClienteDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Alerta Cliente!" + e.Message);
            }

        }

        private Boolean valida(AlertaCliente alertaCliente)
        {
            if (string.IsNullOrWhiteSpace(alertaCliente.Descricao))
            {
                throw new Exception("O campo \"Mensagem\" é obrigatório!");
            }
            return true;
        }

    }
}