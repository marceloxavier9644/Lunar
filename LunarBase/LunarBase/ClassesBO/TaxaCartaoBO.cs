using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class TaxaCartaoBO : BO
    {
        private TaxaCartaoDAO dao;

        public TaxaCartaoBO()
        {
            dao = new TaxaCartaoDAO();
        }

        public void salvar(ObjetoPadrao taxaCartao)
        {
            Boolean excluido = taxaCartao.FlagExcluido;

            if (valida((TaxaCartao)taxaCartao))
            {
                if (((TaxaCartao)taxaCartao).Id == 0)
                    dao.incluir((TaxaCartao)taxaCartao);
                else
                    dao.alterar((TaxaCartao)taxaCartao);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TaxaCartao)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Taxa Cartao não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TaxaCartaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Taxa Cartao!" + e.Message);
            }

        }

        private Boolean valida(TaxaCartao taxaCartao)
        {
            if (string.IsNullOrWhiteSpace(taxaCartao.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(taxaCartao.TipoCartao))
            {
                throw new Exception("O campo \"Crédito ou Débito\" é obrigatório!");
            }
            if (taxaCartao.AdquirenteCartao == null)
            {
                throw new Exception("O campo \"Adquirente\" é obrigatório!");
            }
            if (taxaCartao.BandeiraCartao == null)
            {
                throw new Exception("O campo \"Bandeira\" é obrigatório!");
            }
            return true;
        }

    }
}