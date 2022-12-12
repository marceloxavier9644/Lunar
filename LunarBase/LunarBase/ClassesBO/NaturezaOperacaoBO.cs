using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class NaturezaOperacaoBO : BO
    {
        private NaturezaOperacaoDAO dao;

        public NaturezaOperacaoBO()
        {
            dao = new NaturezaOperacaoDAO();
        }

        public void salvar(ObjetoPadrao naturezaOperacao)
        {
            Boolean excluido = naturezaOperacao.FlagExcluido;

            if (valida((NaturezaOperacao)naturezaOperacao))
            {
                if (((NaturezaOperacao)naturezaOperacao).Id == 0)
                    dao.incluir((NaturezaOperacao)naturezaOperacao);
                else
                    dao.alterar((NaturezaOperacao)naturezaOperacao);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((NaturezaOperacao)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("NaturezaOperacao não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new NaturezaOperacaoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar NaturezaOperacao!" + e.Message);
            }

        }

        private Boolean valida(NaturezaOperacao naturezaOperacao)
        {
            if (string.IsNullOrWhiteSpace(naturezaOperacao.Descricao))
            {
                throw new Exception("O campo \"Descrição\" é obrigatório!");
            }
            //if (string.IsNullOrWhiteSpace(naturezaOperacao.CfopPadrao))
            //{
            //    throw new Exception("O campo \"CFOP Padrão\" é obrigatório!");
            //}
            //if (string.IsNullOrWhiteSpace(naturezaOperacao.CsosnPadrao))
            //{
            //    throw new Exception("O campo \"CSOSN Padrão\" é obrigatório!");
            //}
            return true;
        }

        public void salvarSeNaoExistir(NaturezaOperacao naturezaOperacao)
        {
            try
            {
                NaturezaOperacao naturezaOperacaoAux = (NaturezaOperacao)dao.Selecionar(naturezaOperacao, ((NaturezaOperacao)naturezaOperacao).Id);
                if (naturezaOperacaoAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(naturezaOperacao.Descricao))
                {
                    throw new Exception("O campo \"Descrição\" é obrigatório!");
                }
                dao.incluir((NaturezaOperacao)naturezaOperacao);
            }
        }
        public IList<NaturezaOperacao> selecionarTodasNaturezaOperacao()
        {
            try
            {
                return dao.selecionarTodasNaturezaOperacao();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar natureza operação! Erro: " + e.Message);
            }
        }

        public IList<NaturezaOperacao> selecionarNaturezaOperacaoVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarNaturezaOperacaoVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar natureza operacao! Erro: " + e.Message);
            }
        }
    }
}