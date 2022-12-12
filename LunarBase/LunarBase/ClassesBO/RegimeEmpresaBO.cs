using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class RegimeEmpresaBO : BO
    {
        private RegimeEmpresaDAO dao;

        public RegimeEmpresaBO()
        {
            dao = new RegimeEmpresaDAO();
        }

        public void salvar(ObjetoPadrao regimeEmpresa)
        {
            Boolean excluido = regimeEmpresa.FlagExcluido;

            if (valida((RegimeEmpresa)regimeEmpresa))
            {
                if (((RegimeEmpresa)regimeEmpresa).Id == 0)
                    dao.incluir((RegimeEmpresa)regimeEmpresa);
                else
                    dao.alterar((RegimeEmpresa)regimeEmpresa);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((RegimeEmpresa)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Regime Tributário não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new RegimeEmpresaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Regime Empresa!" + e.Message);
            }

        }

        private Boolean valida(RegimeEmpresa regimeEmpresa)
        {
            if (string.IsNullOrWhiteSpace(regimeEmpresa.Descricao))
            {
                throw new Exception("O campo \"Regime\" é obrigatório!");
            }
            return true;
        }
        public void salvarSeNaoExistir(RegimeEmpresa regimeEmpresa)
        {
            try
            {
                RegimeEmpresa regimeEmpresaAux = (RegimeEmpresa)dao.Selecionar(regimeEmpresa, ((RegimeEmpresa)regimeEmpresa).Id);
                if (regimeEmpresaAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(regimeEmpresa.Descricao))
                {
                    throw new Exception("O campo \"Regime\" é obrigatório!");
                }
                dao.incluir((RegimeEmpresa)regimeEmpresa);
            }
        }
    }
}