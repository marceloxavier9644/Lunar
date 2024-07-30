using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    internal class AnexoBO : BO
    {
        private AnexoDAO dao;

        public AnexoBO()
        {
            dao = new AnexoDAO();
        }

        public void salvar(ObjetoPadrao anexo)
        {
            Boolean excluido = anexo.FlagExcluido;

            if (valida((Anexo)anexo))
            {
                if (((Anexo)anexo).Id == 0)
                    dao.incluir((Anexo)anexo);
                else
                    dao.alterar((Anexo)anexo);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Anexo)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Anexo não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AnexoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Anexo!" + e.Message);
            }

        }

        private Boolean valida(Anexo anexo)
        {
            if (string.IsNullOrWhiteSpace(anexo.Caminho))
            {
                throw new Exception("O campo \"Caminho\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(anexo.Codigo))
            {
                throw new Exception("O campo \"Codigo\" é obrigatório!");
            }
            return true;
        }

        public IList<Anexo> selecionarTodosAnexosPorOrdemServico(int idOrdemServico)
        {
            try
            {
                return dao.selecionarTodosAnexosPorOrdemServico(idOrdemServico);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar anexo! Erro: " + e.Message);
            }
        }
    }
}