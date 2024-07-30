using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;


namespace LunarBase.ClassesBO
{
    public class TipoObjetoBO : BO
    {
        private TipoObjetoDAO dao;

        public TipoObjetoBO()
        {
            dao = new TipoObjetoDAO();
        }

        public void salvar(ObjetoPadrao tipoObjeto)
        {
            Boolean excluido = tipoObjeto.FlagExcluido;

            if (valida((TipoObjeto)tipoObjeto))
            {
                if (((TipoObjeto)tipoObjeto).Id == 0)
                    dao.incluir((TipoObjeto)tipoObjeto);
                else
                    dao.alterar((TipoObjeto)tipoObjeto);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TipoObjeto)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Tipo Objeto não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TipoObjetoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar TipoObjeto!" + e.Message);
            }

        }

        private Boolean valida(TipoObjeto tipoObjeto)
        {
            if (string.IsNullOrWhiteSpace(tipoObjeto.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            return true;
        }
        public void salvarSeNaoExistir(TipoObjeto tipoObjeto)
        {
            try
            {
                TipoObjeto tipoObjetoAux = (TipoObjeto)dao.Selecionar(tipoObjeto, ((TipoObjeto)tipoObjeto).Id);
                if (tipoObjetoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(tipoObjeto.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }

                dao.incluir((TipoObjeto)tipoObjeto);
            }
        }
       
    }
}