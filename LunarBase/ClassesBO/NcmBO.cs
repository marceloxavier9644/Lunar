using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NcmBO : BO
    {
        private NcmDAO dao;

        public NcmBO()
        {
            dao = new NcmDAO();
        }

        public void salvar(ObjetoPadrao ncm)
        {
            Boolean excluido = ncm.FlagExcluido;

            if (valida((Ncm)ncm))
            {
                if (((Ncm)ncm).Id == 0)
                    dao.incluir((Ncm)ncm);
                else
                    dao.alterar((Ncm)ncm);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Ncm)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("NCM não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NcmDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Ncm!" + e.Message);
            }

        }

        private Boolean valida(Ncm ncm)
        {
            if (string.IsNullOrWhiteSpace(ncm.NumeroNcm))
            {
                throw new Exception("O campo \"Numero NCM\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(ncm.DescricaoNcm))
            {
                throw new Exception("O campo \"Descrição do NCM\" é obrigatório!");
            }
            return true;
        }

        public IList<Ncm> selecionarTodosNCM()
        {
            try
            {
                return dao.selecionarTodosNCM();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar ncm! Erro: " + e.Message);
            }
        }

        public IList<Ncm> selecionarNCMPorNCM(String ncm)
        {
            try
            {
                return dao.selecionarNCMPorNCM(ncm);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar ncm! Erro: " + e.Message);
            }
        }
    }
}