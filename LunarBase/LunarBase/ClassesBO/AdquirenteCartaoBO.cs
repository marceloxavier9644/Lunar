using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AdquirenteCartaoBO : BO
    {
        private AdquirenteCartaoDAO dao;

        public AdquirenteCartaoBO()
        {
            dao = new AdquirenteCartaoDAO();
        }

        public void salvar(ObjetoPadrao adquirenteCartao)
        {
            Boolean excluido = adquirenteCartao.FlagExcluido;

            if (valida((AdquirenteCartao)adquirenteCartao))
            {
                if (((AdquirenteCartao)adquirenteCartao).Id == 0)
                    dao.incluir((AdquirenteCartao)adquirenteCartao);
                else
                    dao.alterar((AdquirenteCartao)adquirenteCartao);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AdquirenteCartao)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Adquirente de Cartao não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AdquirenteCartaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Adquirente de Cartao!" + e.Message);
            }

        }

        private Boolean valida(AdquirenteCartao adquirenteCartao)
        {
            if (string.IsNullOrWhiteSpace(adquirenteCartao.Descricao))
            {
                throw new Exception("O campo \"Adquirente de Cartao\" é obrigatório!");
            }
            return true;
        }

        public IList<AdquirenteCartao> selecionarTodasAdquirentes()
        {
            try
            {
                return dao.selecionarTodasAdquirentes();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Adquirente Cartao! Erro: " + e.Message);
            }
        }
        public void salvarSeNaoExistir(AdquirenteCartao adquirenteCartao)
        {
            try
            {
                AdquirenteCartao adquirenteCartaoAux = (AdquirenteCartao)dao.Selecionar(adquirenteCartao, ((AdquirenteCartao)adquirenteCartao).Id);
                if (adquirenteCartaoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(adquirenteCartao.Descricao))
                {
                    throw new Exception("O campo \"Adquirente\" é obrigatório!");
                }
                dao.incluir((AdquirenteCartao)adquirenteCartao);
            }
        }

        public IList<AdquirenteCartao> selecionarAdquirenteComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarAdquirenteComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Adquirentes de Cartao! Erro: " + e.Message);
            }
        }
    }
}