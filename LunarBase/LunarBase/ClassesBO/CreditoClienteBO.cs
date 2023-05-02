using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CreditoClienteBO : BO
    {
        private CreditoClienteDAO dao;

        public CreditoClienteBO()
        {
            dao = new CreditoClienteDAO();
        }

        public void salvar(ObjetoPadrao creditoCliente)
        {
            Boolean excluido = creditoCliente.FlagExcluido;

            if (valida((CreditoCliente)creditoCliente))
            {
                if (((CreditoCliente)creditoCliente).Id == 0)
                    dao.incluir((CreditoCliente)creditoCliente);
                else
                    dao.alterar((CreditoCliente)creditoCliente);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((CreditoCliente)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Credito Cliente não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CreditoClienteDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Credito Cliente!" + e.Message);
            }

        }

        private Boolean valida(CreditoCliente creditoCliente)
        {
            if (string.IsNullOrWhiteSpace(creditoCliente.Origem))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<CreditoCliente> selecionarCreditoPorCliente(int idCliente)
        {
            try
            {
                return dao.selecionarCreditoPorCliente(idCliente);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Crédito! Erro: " + e.Message);
            }
        }
    }
}