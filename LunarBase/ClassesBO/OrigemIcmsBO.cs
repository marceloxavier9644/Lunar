using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class OrigemIcmsBO : BO
    {
        private OrigemIcmsDAO dao;

        public OrigemIcmsBO()
        {
            dao = new OrigemIcmsDAO();
        }

        public void salvar(ObjetoPadrao origemIcms)
        {
            Boolean excluido = origemIcms.FlagExcluido;

            if (valida((OrigemIcms)origemIcms))
            {
                if (((OrigemIcms)origemIcms).Id == 0)
                    dao.incluir((OrigemIcms)origemIcms);
                else
                    dao.alterar((OrigemIcms)origemIcms);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((OrigemIcms)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Origem Icms não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new OrigemIcmsDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Origem Icms!" + e.Message);
            }

        }

        private Boolean valida(OrigemIcms origemIcms)
        {
            if (string.IsNullOrWhiteSpace(origemIcms.Descricao))
            {
                throw new Exception("O campo \"Origem\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(OrigemIcms origemIcms)
        {
            try
            {
                OrigemIcms origemIcmsAux = (OrigemIcms)dao.Selecionar(origemIcms, ((OrigemIcms)origemIcms).Id);
                if (origemIcmsAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(origemIcms.Descricao))
                {
                    throw new Exception("O campo \"Origem\" é obrigatório!");
                }
             
                dao.incluir((OrigemIcms)origemIcms);
            }
        }

        public IList<OrigemIcms> selecionarTodasOrigemIcms()
        {
            try
            {
                return dao.selecionarTodasOrigemIcms();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Origem Icms! Erro: " + e.Message);
            }
        }

        public OrigemIcms selecionarOrigemPorCodigoDeOrigem(String codOrigem)
        {
            try
            {
                return dao.selecionarOrigemPorCodigoDeOrigem(codOrigem);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar origem icms! Erro: " + e.Message);
            }
        }
    }
}
