using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CaracteristicaBO : BO
    {
        private CaracteristicaDAO dao;

        public CaracteristicaBO()
        {
            dao = new CaracteristicaDAO();
        }

        public void salvar(ObjetoPadrao tamanho)
        {
            Boolean excluido = tamanho.FlagExcluido;

            if (valida((Caracteristica)tamanho))
            {
                if (((Caracteristica)tamanho).Id == 0)
                    dao.incluir((Caracteristica)tamanho);
                else
                    dao.alterar((Caracteristica)tamanho);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Caracteristica)objeto).Id);
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
            dao = new CaracteristicaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Tamanho!" + e.Message);
            }

        }

        private Boolean valida(Caracteristica tamanho)
        {
            if (string.IsNullOrWhiteSpace(tamanho.Descricao))
            {
                throw new Exception("O campo \"Descrição da Caracteristica\" é obrigatório!");
            }
            if (tamanho.Ordem <= 0)
            {
                throw new Exception("O campo \"Ordem\" é obrigatório!");
            }
            return true;
        }

        public void salvarSeNaoExistir(Caracteristica tamanho)
        {
            try
            {
                Caracteristica tamanhoAux = (Caracteristica)dao.Selecionar(tamanho, ((Caracteristica)tamanho).Id);
                if (tamanhoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(tamanho.Descricao))
                {
                    throw new Exception("O campo \"Descrição Caracteristica\" é obrigatório!");
                }
                dao.incluir((Caracteristica)tamanho);
            }
        }

        public IList<Caracteristica> selecionarCaracteristicaPorProduto(int idProduto)
        {
            try
            {
                return dao.selecionarCaracteristicaPorProduto(idProduto);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Caracteristica! Erro: " + e.Message);
            }
        }

    }
}