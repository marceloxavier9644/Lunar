using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class PessoaBO : BO
    {
        private PessoaDAO dao;

        public PessoaBO()
        {
            dao = new PessoaDAO();
        }

        public void salvar(ObjetoPadrao pessoa)
        {
            Boolean excluido = pessoa.FlagExcluido;

            if (valida((Pessoa)pessoa))
            {
                if (((Pessoa)pessoa).Id == 0)
                    dao.incluir((Pessoa)pessoa);
                else
                    dao.alterar((Pessoa)pessoa);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Pessoa)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Pessoa não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new PessoaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Pessoa!" + e.Message);
            }

        }

        private Boolean valida(Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.RazaoSocial))
            {
                throw new Exception("O campo \"Nome/Razão Social\" é obrigatório!");
            }
            return true;
        }

        public IList<Pessoa> selecionarTodosClientes()
        {
            try
            {
                return dao.selecionarTodosClientes();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar clientes! Erro: " + e.Message);
            }
        }

        public IList<Pessoa> selecionarTodasPessoas()
        {
            try
            {
                return dao.selecionarTodasPessoas();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar pessoas/empresas! Erro: " + e.Message);
            }
        }
        public IList<Pessoa> selecionarClientesComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarClientesComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar clientes! Erro: " + e.Message);
            }
        }

        public IList<Pessoa> selecionarPessoasComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarPessoasComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar clientes/Fornecedores! Erro: " + e.Message);
            }
        }

        public void salvarPessoaComItensAdicionais(Pessoa pessoa, IList<PessoaTelefone> listaTelefone, IList<Endereco> listaEndereco, IList<PessoaPropriedade> listaPropriedades, IList<PessoaReferenciaPessoal> listaReferenciaPessoal, IList<PessoaReferenciaComercial> listaReferenciaComercial, IList<PessoaDependente> listaDependentes)
        {
            try
            {
                salvar(pessoa);
                PessoaTelefoneBO pessoaTelefoneBO = new PessoaTelefoneBO();
                EnderecoBO enderecoBO = new EnderecoBO();
                PessoaPropriedadeBO pessoaPropriedadeBO = new PessoaPropriedadeBO();
                PessoaReferenciaPessoalBO pessoaReferenciaPessoalBO = new PessoaReferenciaPessoalBO();
                PessoaReferenciaComercialBO pessoaReferenciaComercialBO = new PessoaReferenciaComercialBO();
                PessoaDependenteBO pessoaDependenteBO = new PessoaDependenteBO();

                foreach (PessoaTelefone pessoaTelefone in listaTelefone)
                {
                    pessoaTelefoneBO.salvar(pessoaTelefone);
                }
                foreach (Endereco endereco in listaEndereco)
                {
                    enderecoBO.salvar(endereco);
                }
                foreach (PessoaPropriedade pessoaPropriedade in listaPropriedades)
                {
                    pessoaPropriedadeBO.salvar(pessoaPropriedade);
                }
                foreach (PessoaReferenciaPessoal pessoaReferenciaPessoal in listaReferenciaPessoal)
                {
                    pessoaReferenciaPessoalBO.salvar(pessoaReferenciaPessoal);
                }
                foreach (PessoaReferenciaComercial pessoaReferenciaComercial in listaReferenciaComercial)
                {
                    pessoaReferenciaComercialBO.salvar(pessoaReferenciaComercial);
                }
                foreach (PessoaDependente pessoaDependente in listaDependentes)
                {
                    pessoaDependenteBO.salvar(pessoaDependente);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pessoa selecionarPessoaPorCPFCNPJ(string cpfCNPJ)
        {
            try
            {
                return dao.selecionarPessoaPorCPFCNPJ(cpfCNPJ);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar pessoa! Erro: " + e.Message);
            }
        }
    }
}