using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfeProdutoBO : BO
    {
        private NfeProdutoDAO dao;

        public NfeProdutoBO()
        {
            dao = new NfeProdutoDAO();
        }

        public void salvar(ObjetoPadrao nfeProduto)
        {
            Boolean excluido = nfeProduto.FlagExcluido;

            if (valida((NfeProduto)nfeProduto))
            {
                if (((NfeProduto)nfeProduto).Id == 0)
                    dao.incluir((NfeProduto)nfeProduto);
                else
                    dao.alterar((NfeProduto)nfeProduto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NfeProduto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfe Produto não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfeProdutoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Nfe Produto !" + e.Message);
            }

        }

        private Boolean valida(NfeProduto nfeProduto)
        {
            if (string.IsNullOrWhiteSpace(nfeProduto.XProd))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (nfeProduto.VProd <= 0)
            {
                throw new Exception("O campo \"VPROD\" é obrigatório!");
            }
            return true;
        }

        public IList<NfeProduto> selecionarProdutosPorNfe(int idNfe)
        {
            try
            {
                return dao.selecionarProdutosPorNfe(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da nfe! Erro: " + e.Message);
            }
        }

        public IList<NfeProduto> selecionarProdutosPorNumeroNfe(int numeroNfe)
        {
            try
            {
                return dao.selecionarProdutosPorNumeroNfe(numeroNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da nfe! Erro: " + e.Message);
            }
        }

        public IList<NfeProduto> selecionarProdutoPorCNPJeReferencia(string cnpjEmitente, string referencia)
        {
            try
            {
                return dao.selecionarProdutoPorCNPJeReferencia(cnpjEmitente, referencia);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar produtos da nfe! Erro: " + e.Message);
            }
        }
    }
}