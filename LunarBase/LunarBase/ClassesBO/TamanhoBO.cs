using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class TamanhoBO : BO
    {
        private TamanhoDAO dao;

        public TamanhoBO()
        {
            dao = new TamanhoDAO();
        }

        public void salvar(ObjetoPadrao tamanho)
        {
            Boolean excluido = tamanho.FlagExcluido;

            if (valida((Tamanho)tamanho))
            {
                if (((Tamanho)tamanho).Id == 0)
                    dao.incluir((Tamanho)tamanho);
                else
                    dao.alterar((Tamanho)tamanho);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Tamanho)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Tamanho não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TamanhoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Tamanho!" + e.Message);
            }

        }

        private Boolean valida(Tamanho tamanho)
        {
            if (string.IsNullOrWhiteSpace(tamanho.Descricao))
            {
                throw new Exception("O campo \"Descrição do Tamanho\" é obrigatório!");
            }
            if (tamanho.Ordem <= 0)
            {
                throw new Exception("O campo \"Ordem\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(Tamanho tamanho)
        {
            try
            {
                Tamanho tamanhoAux = (Tamanho)dao.Selecionar(tamanho, ((Tamanho)tamanho).Id);
                if (tamanhoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(tamanho.Descricao))
                {
                    throw new Exception("O campo \"Descrição Tamanho\" é obrigatório!");
                }
                dao.incluir((Tamanho)tamanho);
            }
        }
       
    }
}