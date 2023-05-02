using LunarBase.Classes;
using LunarBase.ClassesBO;
using LunarBase.ConexaoBD;

namespace LunarBase.ControllerBO
{
    public class PessoaController : Controller
    {
        public IList<Pessoa> selecionarTodosClientes()
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodosClientes();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Pessoa> selecionarTodasPessoas()
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarTodasPessoas();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Pessoa> selecionarPessoasGrid()
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPessoasGrid();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Pessoa> selecionarClientesComVariosFiltros(string valor)
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarClientesComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public IList<Pessoa> selecionarPessoasComVariosFiltros(string valor)
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPessoasComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public void salvarPessoaComItensAdicionais(Pessoa pessoa, IList<PessoaTelefone> listaTelefone, IList<Endereco> listaEndereco, IList<PessoaPropriedade> listaPropriedades, IList<PessoaReferenciaPessoal> listaReferenciaPessoal, IList<PessoaReferenciaComercial> listaReferenciaComercial, IList<PessoaDependente> listaDependentes)
        {
            PessoaBO pessoaBO = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                pessoaBO.salvarPessoaComItensAdicionais(pessoa, listaTelefone, listaEndereco, listaPropriedades, listaReferenciaPessoal, listaReferenciaComercial, listaDependentes);
                Conexao.Commit();
            }
            catch (Exception e)
            {
                Conexao.RollBack();
                throw new Exception("Erro ao salvar cliente/pessoa " + e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }

        public Pessoa selecionarPessoaPorCPFCNPJ(string cpfCNPJ)
        {
            PessoaBO bo = new PessoaBO();
            Conexao.IniciaTransacao();
            try
            {
                return bo.selecionarPessoaPorCPFCNPJ(cpfCNPJ);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Conexao.FechaConexaoBD();
            }
        }
    }
}
