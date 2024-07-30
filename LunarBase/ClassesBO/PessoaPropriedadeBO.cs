using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PessoaPropriedadeBO : BO
    {
        private PessoaPropriedadeDAO dao;

        public PessoaPropriedadeBO()
        {
            dao = new PessoaPropriedadeDAO();
        }

        public void salvar(ObjetoPadrao pessoaPropriedade)
        {
            Boolean excluido = pessoaPropriedade.FlagExcluido;

            if (valida((PessoaPropriedade)pessoaPropriedade))
            {
                if (((PessoaPropriedade)pessoaPropriedade).Id == 0)
                    dao.incluir((PessoaPropriedade)pessoaPropriedade);
                else
                    dao.alterar((PessoaPropriedade)pessoaPropriedade);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaPropriedade)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Pessoa Propriedade não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaPropriedadeDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Pessoa Propriedade!" + e.Message);
            }

        }

        private Boolean valida(PessoaPropriedade pessoaPropriedade)
        {
            if (string.IsNullOrWhiteSpace(pessoaPropriedade.Descricao))
            {
                throw new Exception("O campo \"DESCRIÇÃO\" é obrigatório!");
            }
            return true;
        }

        public IList<PessoaPropriedade> selecionarPropriedadesPorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarPropriedadesPorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Pessoa Propriedades! Erro: " + e.Message);
            }
        }
    }
}