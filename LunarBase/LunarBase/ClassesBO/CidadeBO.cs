using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class CidadeBO : BO
    {
        private CidadeDAO dao;

        public CidadeBO()
        {
            dao = new CidadeDAO();
        }

        public void salvar(ObjetoPadrao cidade)
        {
            Boolean excluido = cidade.FlagExcluido;

            if (valida((Cidade)cidade))
            {
                if (((Cidade)cidade).Id == 0)
                    dao.incluir((Cidade)cidade);
                else
                    dao.alterar((Cidade)cidade);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Cidade)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Cidade não encontrada!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new CidadeDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Cidade!" + e.Message);
            }

        }

        private Boolean valida(Cidade cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.Descricao))
            {
                throw new Exception("O campo \"Cidade\" é obrigatório!");
            }
            if (cidade.Estado == null)
            {
                throw new Exception("O campo \"ESTADO\" é obrigatório!");
            }
            return true;
        }

        public Cidade selecionarCidadePorDescricao(string descricao)
        {
            try
            {
                return dao.selecionarCidadePorDescricao(descricao);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cidade! Erro: " + e.Message);
            }
        }

        public Cidade selecionarCidadePorDescricaoEUf(string descricao, string uf)
        {
            try
            {
                return dao.selecionarCidadePorDescricaoEUf(descricao, uf);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cidade! Erro: " + e.Message);
            }
        }

        public Cidade selecionarCidadePorDescricaoECodigoIBGE(string descricao, string codigoIBGE)
        {
            try
            {
                return dao.selecionarCidadePorDescricaoECodigoIBGE(descricao, codigoIBGE);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cidade! Erro: " + e.Message);
            }
        }

        public IList<Cidade> selecionarListaCidadePorDescricao(string descricao)
        {
            try
            {
                return dao.selecionarListaCidadePorDescricao(descricao);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cidades! Erro: " + e.Message);
            }
        }

        public IList<Cidade> selecionarTodasCidades()
        {
            try
            {
                return dao.selecionarTodasCidades();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar cidades! Erro: " + e.Message);
            }
        }
    }
}