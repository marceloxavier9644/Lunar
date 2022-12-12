using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    internal class EmpresaFilialBO : BO
    {
        private EmpresaFilialDAO dao;

        public EmpresaFilialBO()
        {
            dao = new EmpresaFilialDAO();
        }

        public void salvar(ObjetoPadrao empresaFilial)
        {
            Boolean excluido = empresaFilial.FlagExcluido;

            if (valida((EmpresaFilial)empresaFilial))
            {
                if (((EmpresaFilial)empresaFilial).Id == 0)
                    dao.incluir((EmpresaFilial)empresaFilial);
                else
                    dao.alterar((EmpresaFilial)empresaFilial);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((EmpresaFilial)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Empresa Filial não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new EmpresaFilialDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Empresa Filial!" + e.Message);
            }

        }

        private Boolean valida(EmpresaFilial empresaFilial)
        {
            if (string.IsNullOrWhiteSpace(empresaFilial.RazaoSocial))
            {
                throw new Exception("O campo \"RAZÃO SOCIAL\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(empresaFilial.NomeFantasia))
            {
                throw new Exception("O campo \"NOME FANTASIA\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(empresaFilial.Cnpj))
            {
                throw new Exception("O campo \"CNPJ\" é obrigatório!");
            }
            return true;
        }

        public EmpresaFilial selecionarEmpresaFilialPorCPFCNPJ(string cpfCNPJ)
        {
            try
            {
                return dao.selecionarEmpresaFilialPorCPFCNPJ(cpfCNPJ);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar empresa filial! Erro: " + e.Message);
            }
        }
        public IList<EmpresaFilial> selecionarTodasFiliais()
        {
            try
            {
                return dao.selecionarTodasFiliais();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar empresas! Erro: " + e.Message);
            }
        }

        public IList<EmpresaFilial> selecionarEmpresaFilialComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarEmpresaFilialComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar empresas! Erro: " + e.Message);
            }
        }

        public void salvarSeNaoExistir(EmpresaFilial empresaFilial)
        {
            try
            {
                EmpresaFilial empresaFilialAux = (EmpresaFilial)dao.Selecionar(empresaFilial, ((EmpresaFilial)empresaFilial).Id);
                if (empresaFilialAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(empresaFilial.RazaoSocial))
                {
                    throw new Exception("O campo \"Razão Social\" é obrigatório!");
                }

                dao.incluir((EmpresaFilial)empresaFilial);
            }
        }
    }
}