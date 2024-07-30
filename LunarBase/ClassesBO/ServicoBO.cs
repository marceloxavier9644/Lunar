using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class ServicoBO : BO
    {
        private ServicoDAO dao;

        public ServicoBO()
        {
            dao = new ServicoDAO();
        }

        public void salvar(ObjetoPadrao servico)
        {
            Boolean excluido = servico.FlagExcluido;

            if (valida((Servico)servico))
            {
                if (((Servico)servico).Id == 0)
                    dao.incluir((Servico)servico);
                else
                    dao.alterar((Servico)servico);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((Servico)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("NCM não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new ServicoDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Servico!" + e.Message);
            }

        }

        private Boolean valida(Servico servico)
        {
            if (string.IsNullOrWhiteSpace(servico.Descricao))
            {
                throw new Exception("O campo \"Descrição do Serviço\" é obrigatório!");
            }
            if (servico.Valor <= 0)
            {
                throw new Exception("O campo \"Valor do Serviço\" é obrigatório e deve ser maior que 0,00");
            }
            return true;
        }

        public IList<Servico> selecionarTodosServicos()
        {
            try
            {
                return dao.selecionarTodosServico();
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar servico! Erro: " + e.Message);
            }
        }

        public IList<Servico> selecionarServicoComVariosFiltros(string valor, EmpresaFilial empresa)
        {
            try
            {
                return dao.selecionarServicoComVariosFiltros(valor, empresa);
            }
            catch (Exception e)
            {
                throw new Exception("Falha ao selecionar serviços! Erro: " + e.Message);
            }
        }
    }
}