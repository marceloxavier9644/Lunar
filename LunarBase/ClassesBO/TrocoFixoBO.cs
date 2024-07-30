using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    internal class TrocoFixoBO : BO
    {
        private TrocoFixoDAO dao;

        public TrocoFixoBO()
        {
            dao = new TrocoFixoDAO();
        }

        public void salvar(ObjetoPadrao trocoFixo)
        {
            Boolean excluido = trocoFixo.FlagExcluido;

            if (valida((TrocoFixo)trocoFixo))
            {
                if (((TrocoFixo)trocoFixo).Id == 0)
                    dao.incluir((TrocoFixo)trocoFixo);
                else
                    dao.alterar((TrocoFixo)trocoFixo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TrocoFixo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Troco Fixo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TrocoFixoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Troco Fixo!" + e.Message);
            }

        }

        private Boolean valida(TrocoFixo trocoFixo)
        {
            if (trocoFixo.Valor <= 0)
            {
                throw new Exception("O campo \"Valor\" é obrigatório ser maior que R$ 0,00!");
            }
            return true;
        }
        public IList<TrocoFixo> selecionarTodosTrocoFixoPorEmpresaFilial()
        {
            try
            {
                return dao.selecionarTodosTrocoFixoPorEmpresaFilial();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar troco fixo Erro: " + e.Message);
            }
        }
    }
}
