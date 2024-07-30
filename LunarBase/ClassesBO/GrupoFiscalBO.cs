using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class GrupoFiscalBO : BO
    {
        private GrupoFiscalDAO dao;

        public GrupoFiscalBO()
        {
            dao = new GrupoFiscalDAO();
        }

        public void salvar(ObjetoPadrao grupoFiscal)
        {
            Boolean excluido = grupoFiscal.FlagExcluido;

            if (valida((GrupoFiscal)grupoFiscal))
            {
                if (((GrupoFiscal)grupoFiscal).Id == 0)
                    dao.incluir((GrupoFiscal)grupoFiscal);
                else
                    dao.alterar((GrupoFiscal)grupoFiscal);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((GrupoFiscal)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Grupo Fiscal não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new GrupoFiscalDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Grupo Fiscal!" + e.Message);
            }

        }

        private Boolean valida(GrupoFiscal grupoFiscal)
        {
            if (string.IsNullOrWhiteSpace(grupoFiscal.Descricao))
            {
                throw new Exception("O campo \"Grupo Fiscal\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(GrupoFiscal grupoFiscal)
        {
            try
            {
                GrupoFiscal grupoFiscalAux = (GrupoFiscal)dao.Selecionar(grupoFiscal, ((GrupoFiscal)grupoFiscal).Id);
                if (grupoFiscalAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(grupoFiscal.Descricao))
                {
                    throw new Exception("O campo \"Grupo Fiscal\" é obrigatório!");
                }
                dao.incluir((GrupoFiscal)grupoFiscal);
            }
        }

        public IList<GrupoFiscal> selecionarGrupoFiscalComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarGrupoFiscalComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar grupo fiscal! Erro: " + e.Message);
            }
        }
    }
}