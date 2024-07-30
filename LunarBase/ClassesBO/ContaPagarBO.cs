using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ContaPagarBO : BO
    {
        private ContaPagarDAO dao;

        public ContaPagarBO()
        {
            dao = new ContaPagarDAO();
        }

        public void salvar(ObjetoPadrao contaPagar)
        {
            Boolean excluido = contaPagar.FlagExcluido;

            if (valida((ContaPagar)contaPagar))
            {
                if (((ContaPagar)contaPagar).Id == 0)
                    dao.incluir((ContaPagar)contaPagar);
                else
                    dao.alterar((ContaPagar)contaPagar);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ContaPagar)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Conta a Pagar não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ContaPagarDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Conta Pagar!" + e.Message);
            }

        }

        private Boolean valida(ContaPagar contaPagar)
        {
            if (string.IsNullOrWhiteSpace(contaPagar.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }

        public IList<ContaPagar> selecionarContaPagarPorSql(string sql)
        {
            try
            {
                return dao.selecionarContaPagarPorSql(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Conta a pagar! Erro: " + e.Message);
            }
        }
    }
}