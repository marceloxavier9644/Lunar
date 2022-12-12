using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfeReferenciaBO : BO
    {
        private NfeReferenciaDAO dao;

        public NfeReferenciaBO()
        {
            dao = new NfeReferenciaDAO();
        }

        public void salvar(ObjetoPadrao nfeReferencia)
        {
            Boolean excluido = nfeReferencia.FlagExcluido;

            if (valida((NfeReferencia)nfeReferencia))
            {
                if (((NfeReferencia)nfeReferencia).Id == 0)
                    dao.incluir((NfeReferencia)nfeReferencia);
                else
                    dao.alterar((NfeReferencia)nfeReferencia);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NfeReferencia)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfe Referencia não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfeReferenciaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Nfe Referencia!" + e.Message);
            }

        }

        private Boolean valida(NfeReferencia nfeReferencia)
        {
            if (string.IsNullOrWhiteSpace(nfeReferencia.Chave))
            {
                throw new Exception("O campo \"Chave\" é obrigatório!");
            }
            if(nfeReferencia.Nfe == null)
            {
                throw new Exception("O campo \"NFe\" é obrigatório!");
            }
            return true;
        }

        public IList<NfeReferencia> selecionarNotasReferenciadasPorNfe(int idNfe)
        {
            try
            {
                return dao.selecionarNotasReferenciadasPorNfe(idNfe);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar referencias da nfe! Erro: " + e.Message);
            }
        }
    }
}