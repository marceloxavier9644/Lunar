using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ContaBancariaBO : BO
    {
        private ContaBancariaDAO dao;

        public ContaBancariaBO()
        {
            dao = new ContaBancariaDAO();
        }

        public void salvar(ObjetoPadrao contaBancaria)
        {
            Boolean excluido = contaBancaria.FlagExcluido;

            if (valida((ContaBancaria)contaBancaria))
            {
                if (((ContaBancaria)contaBancaria).Id == 0)
                    dao.incluir((ContaBancaria)contaBancaria);
                else
                    dao.alterar((ContaBancaria)contaBancaria);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((ContaBancaria)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Conta Bancaria não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ContaBancariaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Conta Bancaria!" + e.Message);
            }

        }

        private Boolean valida(ContaBancaria contaBancaria)
        {
            if (string.IsNullOrWhiteSpace(contaBancaria.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
          
            return true;
        }

        public IList<ContaBancaria> selecionarTodasContas()
        {
            try
            {
                return dao.selecionarTodasContas();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Contas Bancaria! Erro: " + e.Message);
            }
        }
        public IList<ContaBancaria> selecionarTodasContasPorFilial(int idFilial)
        {
            try
            {
                return dao.selecionarTodasContasPorFilial(idFilial);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Contas Bancaria! Erro: " + e.Message);
            }
        }

        public IList<ContaBancaria> selecionarContaBancariaComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarContaBancariaComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar contas! Erro: " + e.Message);
            }
        }
    }
}