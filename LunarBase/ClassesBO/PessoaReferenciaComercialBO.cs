using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PessoaReferenciaComercialBO : BO
    {
        private PessoaReferenciaComercialDAO dao;

        public PessoaReferenciaComercialBO()
        {
            dao = new PessoaReferenciaComercialDAO();
        }

        public void salvar(ObjetoPadrao pessoaReferenciaComercial)
        {
            Boolean excluido = pessoaReferenciaComercial.FlagExcluido;

            if (valida((PessoaReferenciaComercial)pessoaReferenciaComercial))
            {
                if (((PessoaReferenciaComercial)pessoaReferenciaComercial).Id == 0)
                    dao.incluir((PessoaReferenciaComercial)pessoaReferenciaComercial);
                else
                    dao.alterar((PessoaReferenciaComercial)pessoaReferenciaComercial);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((PessoaReferenciaComercial)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Referencia Comercial não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaReferenciaComercialDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Referencia Comercial!" + e.Message);
            }

        }

        private Boolean valida(PessoaReferenciaComercial pessoaReferenciaComercial)
        {
            if (string.IsNullOrWhiteSpace(pessoaReferenciaComercial.Empresa))
            {
                throw new Exception("O campo \"Empresa\" é obrigatório!");
            }
            return true;
        }

        public IList<PessoaReferenciaComercial> selecionarReferenciaComercialPorPessoa(int idPessoa)
        {
            try
            {
                return dao.selecionarReferenciaComercialPorPessoa(idPessoa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Referencia Comercial! Erro: " + e.Message);
            }
        }
    }
}