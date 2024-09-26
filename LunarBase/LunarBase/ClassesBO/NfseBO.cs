using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfseBO : BO
    {
        private NfseDAO dao;

        public NfseBO()
        {
            dao = new NfseDAO();
        }

        public void salvar(ObjetoPadrao nfse)
        {
            Boolean excluido = nfse.FlagExcluido;

            if (valida((Nfse)nfse))
            {
                if (((Nfse)nfse).Id == 0)
                    dao.incluir((Nfse)nfse);
                else
                    dao.alterar((Nfse)nfse);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Nfse)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfse não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfseDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Nfse!" + e.Message);
            }

        }

        private Boolean valida(Nfse nfse)
        {
            return true;
        }

        public IList<Nfse> selecionarNFSePorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarNFSePorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Nfse! Erro: " + e.Message);
            }
        }

        
    }
}