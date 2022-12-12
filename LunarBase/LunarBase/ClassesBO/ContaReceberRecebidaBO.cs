using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ContaReceberRecebidaBO : BO
    {
        private ContaReceberRecebidaDAO dao;

        public ContaReceberRecebidaBO()
        {
            dao = new ContaReceberRecebidaDAO();
        }

        public void salvar(ObjetoPadrao contasReceberRecebidas)
        {
            Boolean excluido = contasReceberRecebidas.FlagExcluido;

            if (valida((ContaReceberRecebida)contasReceberRecebidas))
            {
                if (((ContaReceberRecebida)contasReceberRecebidas).Id == 0)
                    dao.incluir((ContaReceberRecebida)contasReceberRecebidas);
                else
                    dao.alterar((ContaReceberRecebida)contasReceberRecebidas);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ContaReceberRecebida)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Contas Receber Recebidas não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ContaReceberRecebidaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar ContasReceberRecebidas!" + e.Message);
            }

        }

        private Boolean valida(ContaReceberRecebida contasReceberRecebidas)
        {
            if (string.IsNullOrWhiteSpace(contasReceberRecebidas.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

      
    }
}