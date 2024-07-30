using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class TransferenciaEstoqueBO : BO
    {
        private TransferenciaEstoqueDAO dao;

        public TransferenciaEstoqueBO()
        {
            dao = new TransferenciaEstoqueDAO();
        }

        public void salvar(ObjetoPadrao transferenciaEstoque)
        {
            Boolean excluido = transferenciaEstoque.FlagExcluido;

            if (valida((TransferenciaEstoque)transferenciaEstoque))
            {
                if (((TransferenciaEstoque)transferenciaEstoque).Id == 0)
                    dao.incluir((TransferenciaEstoque)transferenciaEstoque);
                else
                    dao.alterar((TransferenciaEstoque)transferenciaEstoque);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((TransferenciaEstoque)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("TransferenciaEstoque não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new TransferenciaEstoqueDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar TransferenciaEstoque!" + e.Message);
            }

        }

        private Boolean valida(TransferenciaEstoque transferenciaEstoque)
        {
            if (string.IsNullOrWhiteSpace(transferenciaEstoque.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            if (transferenciaEstoque.EmpresaDestino == null)
            {
                throw new Exception("O campo \"Empresa de Destino\" é obrigatório!");
            }
            if (transferenciaEstoque.EmpresaOrigem == null)
            {
                throw new Exception("O campo \"Empresa de Origem\" é obrigatório!");
            }
            return true;
        }

        public IList<TransferenciaEstoque> selecionarTodasTransferencias()
        {
            try
            {
                return dao.selecionarTodasTransferencias();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar Transferencia Estoque! Erro: " + e.Message);
            }
        }
    }
}