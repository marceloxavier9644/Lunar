using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class EmpresaBO : BO
    {
        private EmpresaDAO dao;

        public EmpresaBO()
        {
            dao = new EmpresaDAO();
        }

        public void salvar(ObjetoPadrao empresa)
        {
            Boolean excluido = empresa.FlagExcluido;

            if (valida((Empresa)empresa))
            {
                if (((Empresa)empresa).Id == 0)
                    dao.incluir((Empresa)empresa);
                else
                    dao.alterar((Empresa)empresa);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Empresa)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Empresa não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new EmpresaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Empresa!" + e.Message);
            }

        }

        private Boolean valida(Empresa empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.RazaoSocial))
            {
                throw new Exception("O campo \"Razão Social ou Nome\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(empresa.Cnpj))
            {
                throw new Exception("O campo \"CNPJ\" é obrigatório!");
            }
            return true;
        }
        public void salvarSeNaoExistir(Empresa empresa)
        {
            try
            {
                Empresa empresaAux = (Empresa)dao.Selecionar(empresa, ((Empresa)empresa).Id);
                if (empresaAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(empresa.RazaoSocial))
                {
                    throw new Exception("O campo \"Razão Social\" é obrigatório!");
                }

                dao.incluir((Empresa)empresa);
            }
        }
        public Empresa selecionarEmpresaPorCPFCNPJ(string cpfCNPJ)
        {
            try
            {
                return dao.selecionarEmpresaPorCPFCNPJ(cpfCNPJ);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar empresa! Erro: " + e.Message);
            }
        }

        public IList<Empresa> selecionarTodasEmpresas()
        {
            try
            {
                return dao.selecionarTodasEmpresas();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar empresas! Erro: " + e.Message);
            }
        }
    }
}