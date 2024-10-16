using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class RetornoBancoBO : BO
    {
        private RetornoBancoDAO dao;

        public RetornoBancoBO()
        {
            dao = new RetornoBancoDAO();
        }

        public void salvar(ObjetoPadrao retornoBanco)
        {
            Boolean excluido = retornoBanco.FlagExcluido;

            if (valida((RetornoBanco)retornoBanco))
            {
                if (((RetornoBanco)retornoBanco).Id == 0)
                    dao.incluir((RetornoBanco)retornoBanco);
                else
                    dao.alterar((RetornoBanco)retornoBanco);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((RetornoBanco)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("RetornoBanco não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new RetornoBancoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar RetornoBanco!" + e.Message);
            }

        }

        private Boolean valida(RetornoBanco retornoBanco)
        {
            if (string.IsNullOrWhiteSpace(retornoBanco.Descricao))
            {
                throw new Exception("O campo \"Descrição do RetornoBanco\" é obrigatório!");
            }
            return true;
        }

        public IList<RetornoBanco> selecionarRetornoPorPeriodo(string dataInicial, string dataFinal)
        {
            try
            {
                return dao.selecionarRetornoPorPeriodo(dataInicial, dataFinal);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar retorno por periodo! Erro: " + e.Message);
            }
        }

        public RetornoBanco selecionarRetornoPorNossoNumero(string nossoNumero)
        {
            try
            {
                return dao.selecionarRetornoPorNossoNumero(nossoNumero);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar retorno banco! Erro: " + e.Message);
            }
        }
    }
}