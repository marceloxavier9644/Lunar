using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfeCceBO : BO
    {
        private NfeCceDAO dao;

        public NfeCceBO()
        {
            dao = new NfeCceDAO();
        }

        public void salvar(ObjetoPadrao nfeCce)
        {
            Boolean excluido = nfeCce.FlagExcluido;

            if (valida((NfeCce)nfeCce))
            {
                if (((NfeCce)nfeCce).Id == 0)
                    dao.incluir((NfeCce)nfeCce);
                else
                    dao.alterar((NfeCce)nfeCce);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NfeCce)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("NfeCce não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfeCceDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar NfeCce!" + e.Message);
            }

        }

        private Boolean valida(NfeCce nfeCce)
        {
            if (string.IsNullOrWhiteSpace(nfeCce.Correcao))
            {
                throw new Exception("O campo \"Correçao\" é obrigatório!");
            }
            return true;
        }

        public IList<NfeCce> selecionarCartaCorrecaoPorNfe(int idNfe)
        {
            try
            {
                return dao.selecionarCartaCorrecaoPorNfe(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar CCe! Erro: " + e.Message);
            }
        }
    }
}