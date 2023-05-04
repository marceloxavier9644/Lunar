using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class MarcaBO : BO
    {
        private MarcaDAO dao;

    public MarcaBO()
    {
        dao = new MarcaDAO();
    }

    public void salvar(ObjetoPadrao marca)
    {
        Boolean excluido = marca.FlagExcluido;

        if (valida((Marca)marca))
        {
            if (((Marca)marca).Id == 0)
                dao.incluir((Marca)marca);
            else
                dao.alterar((Marca)marca);
        }
    }

    public ObjetoPadrao selecionar(ObjetoPadrao objeto)
    {
        try
        {
            objeto = dao.Selecionar(objeto, ((Marca)objeto).Id);
            if (objeto.FlagExcluido == true)
            {
                throw new Exception();
            }
            return objeto;
        }
        catch
        {
            throw new Exception("Marca não encontrado!");
        }

    }
    public IList<ObjetoPadrao> selecionarTodos(string Tabela)
    {
        dao = new MarcaDAO();
        try
        {
            return dao.SelecionarTodos(Tabela);
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao selecionar Marca!" + e.Message);
        }

    }

    private Boolean valida(Marca marca)
    {
        if (string.IsNullOrWhiteSpace(marca.Descricao))
        {
            throw new Exception("O campo \"Marca\" é obrigatório!");
        }
        return true;
    }

        public void salvarSeNaoExistir(Marca marca)
        {
            try
            {
                Marca marcaAux = (Marca)dao.Selecionar(marca, ((Marca)marca).Id);
                if (marcaAux == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(marca.Descricao))
                {
                    throw new Exception("O campo \"Marca\" é obrigatório!");
                }
                dao.incluir((Marca)marca);
            }
        }

        public IList<Marca> selecionarMarcaComVariosFiltros(string valor)
        {
            try
            {
                return dao.selecionarMarcaComVariosFiltros(valor);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar marca! Erro: " + e.Message);
            }
        }

        public Marca selecionarMarcaPorDescricao(string descricao)
        {
            try
            {
                return dao.selecionarMarcaPorDescricao(descricao);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar marca! Erro: " + e.Message);
            }
        }
    }
}