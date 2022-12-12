using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PessoaTelefoneBO : BO
    {
        private PessoaTelefoneDAO dao;

        public PessoaTelefoneBO()
        {
            dao = new PessoaTelefoneDAO();
        }

        public void salvar(ObjetoPadrao pessoaTelefone)
        {
            Boolean excluido = pessoaTelefone.FlagExcluido;

            if (valida((PessoaTelefone)pessoaTelefone))
            {
                if (((PessoaTelefone)pessoaTelefone).Id == 0)
                    dao.incluir((PessoaTelefone)pessoaTelefone);
                else
                    dao.alterar((PessoaTelefone)pessoaTelefone);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaTelefone)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Telefone não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaTelefoneDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Telefone!" + e.Message);
            }

        }

        private Boolean valida(PessoaTelefone pessoaTelefone)
        {
            if (string.IsNullOrWhiteSpace(pessoaTelefone.Telefone))
            {
                throw new Exception("O campo \"Telefone\" é obrigatório!");
            }
            if(pessoaTelefone.Pessoa == null)
            {
                throw new Exception("O campo \"Pessoa\" é obrigatório!");
            }
            return true;
        }

        public IList<PessoaTelefone> selecionarTelefonePorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarTelefonePorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Telefones Adicionais! Erro: " + e.Message);
            }
        }
    }
}