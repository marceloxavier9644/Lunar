using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PessoaReferenciaPessoalBO : BO
    {
        private PessoaReferenciaPessoalDAO dao;

        public PessoaReferenciaPessoalBO()
        {
            dao = new PessoaReferenciaPessoalDAO();
        }

        public void salvar(ObjetoPadrao pessoaReferenciaPessoal)
        {
            Boolean excluido = pessoaReferenciaPessoal.FlagExcluido;

            if (valida((PessoaReferenciaPessoal)pessoaReferenciaPessoal))
            {
                if (((PessoaReferenciaPessoal)pessoaReferenciaPessoal).Id == 0)
                    dao.incluir((PessoaReferenciaPessoal)pessoaReferenciaPessoal);
                else
                    dao.alterar((PessoaReferenciaPessoal)pessoaReferenciaPessoal);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaReferenciaPessoal)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("PessoaReferenciaPessoal não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaReferenciaPessoalDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar PessoaReferenciaPessoal!" + e.Message);
            }

        }

        private Boolean valida(PessoaReferenciaPessoal pessoaReferenciaPessoal)
        {
            if (string.IsNullOrWhiteSpace(pessoaReferenciaPessoal.Nome))
            {
                throw new Exception("O campo \"Nome\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(pessoaReferenciaPessoal.Telefone))
            {
                throw new Exception("O campo \"Telefone\" é obrigatório!");
            }
            return true;
        }

        public IList<PessoaReferenciaPessoal> selecionarReferenciaPessoalPorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarReferenciaPessoalPorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Referencia Pessoal! Erro: " + e.Message);
            }
        }
    }
}