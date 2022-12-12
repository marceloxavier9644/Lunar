using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class UnidadeMedidaBO : BO
    {
        private UnidadeMedidaDAO dao;

        public UnidadeMedidaBO()
        {
            dao = new UnidadeMedidaDAO();
        }

        public void salvar(ObjetoPadrao unidadeMedida)
        {
            Boolean excluido = unidadeMedida.FlagExcluido;

            if (valida((UnidadeMedida)unidadeMedida))
            {
                if (((UnidadeMedida)unidadeMedida).Id == 0)
                    dao.incluir((UnidadeMedida)unidadeMedida);
                else
                    dao.alterar((UnidadeMedida)unidadeMedida);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((UnidadeMedida)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Unidade de Medida não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new UnidadeMedidaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Unidade de Medida!" + e.Message);
            }

        }

        private Boolean valida(UnidadeMedida unidadeMedida)
        {
            if (string.IsNullOrWhiteSpace(unidadeMedida.Descricao))
            {
                throw new Exception("O campo \"Unidade Medida\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(unidadeMedida.Sigla))
            {
                throw new Exception("O campo \"Sigla\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(UnidadeMedida unidadeMedida)
        {
            try
            {
                UnidadeMedida unidadeMedidaAux = (UnidadeMedida)dao.Selecionar(unidadeMedida, ((UnidadeMedida)unidadeMedida).Id);
                if (unidadeMedidaAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(unidadeMedida.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(unidadeMedida.Sigla))
                {
                    throw new Exception("O campo \"Sigla\" é obrigatório!");
                }
                dao.incluir((UnidadeMedida)unidadeMedida);
            }
        }

        public UnidadeMedida selecionarUnidadeMedidaPorSigla(string sigla)
        {
            try
            {
                return dao.selecionarUnidadeMedidaPorSigla(sigla);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar unidade de medida! Erro: " + e.Message);
            }
        }

        public IList<UnidadeMedida> selecionarUnidadeMedidaComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            try
            {
                return dao.selecionarUnidadeMedidaComVariosFiltros(valor, empresa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar unidade de medida! Erro: " + e.Message);
            }
        }
    }
}