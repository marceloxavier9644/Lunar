using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NfeStatusBO : BO
    {
        private NfeStatusDAO dao;

        public NfeStatusBO()
        {
            dao = new NfeStatusDAO();
        }

        public void salvar(ObjetoPadrao nfeStatus)
        {
            Boolean excluido = nfeStatus.FlagExcluido;

            if (valida((NfeStatus)nfeStatus))
            {
                if (((NfeStatus)nfeStatus).Id == 0)
                    dao.incluir((NfeStatus)nfeStatus);
                else
                    dao.alterar((NfeStatus)nfeStatus);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NfeStatus)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Nfe Status não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NfeStatusDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar NfeStatus!" + e.Message);
            }

        }

        private Boolean valida(NfeStatus nfeStatus)
        {
            if (string.IsNullOrWhiteSpace(nfeStatus.Status))
            {
                throw new Exception("O campo \"Status\" é obrigatório!");
            }
            return true;
        }
        public void salvarSeNaoExistir(NfeStatus nfeStatus)
        {
            try
            {
                NfeStatus nfeStatusAux = (NfeStatus)dao.Selecionar(nfeStatus, ((NfeStatus)nfeStatus).Id);
                if (nfeStatusAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(nfeStatus.Status))
                {
                    throw new Exception("O campo \"Status\" é obrigatório!");
                }
                dao.incluir((NfeStatus)nfeStatus);
            }
        }

     
    }
}