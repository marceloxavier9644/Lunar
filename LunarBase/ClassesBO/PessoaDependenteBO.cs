using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    internal class PessoaDependenteBO : BO
    {
        private PessoaDependenteDAO dao;

        public PessoaDependenteBO()
        {
            dao = new PessoaDependenteDAO();
        }

        public void salvar(ObjetoPadrao pessoaDependente)
        {
            Boolean excluido = pessoaDependente.FlagExcluido;

            if (valida((PessoaDependente)pessoaDependente))
            {
                if (((PessoaDependente)pessoaDependente).Id == 0)
                    dao.incluir((PessoaDependente)pessoaDependente);
                else
                    dao.alterar((PessoaDependente)pessoaDependente);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaDependente)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Dependente não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaDependenteDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Dependente!" + e.Message);
            }

        }

        private Boolean valida(PessoaDependente pessoaDependente)
        {
            if (string.IsNullOrWhiteSpace(pessoaDependente.Nome))
            {
                throw new Exception("O campo \"Nome\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(PessoaDependente pessoaDependente)
        {
            try
            {
                PessoaDependente pessoaDependenteAux = (PessoaDependente)dao.Selecionar(pessoaDependente, ((PessoaDependente)pessoaDependente).Id);
                if (pessoaDependenteAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(pessoaDependente.Nome))
                {
                    throw new Exception("O campo \"Nome\" é obrigatório!");
                }

                dao.incluir((PessoaDependente)pessoaDependente);
            }
        }

        public IList<PessoaDependente> selecionarDependentePorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarDependentePorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar dependentes! Erro: " + e.Message);
            }
        }
    }
}