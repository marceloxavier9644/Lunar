using LunarBase.Classes;

namespace LunarBase.Interface
{
    public interface BO
    {
        void salvar(ObjetoPadrao objeto);
        ObjetoPadrao selecionar(ObjetoPadrao objeto);
        IList<ObjetoPadrao> selecionarTodos(String Tabela);
    }
}
