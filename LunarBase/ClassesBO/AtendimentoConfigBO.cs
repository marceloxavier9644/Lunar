using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class AtendimentoConfigBO : BO
    {
        private AtendimentoConfigDAO dao;

        public AtendimentoConfigBO()
        {
            dao = new AtendimentoConfigDAO();
        }

        public void salvar(ObjetoPadrao atendimentoConfig)
        {
            Boolean excluido = atendimentoConfig.FlagExcluido;

            if (valida((AtendimentoConfig)atendimentoConfig))
            {
                if (((AtendimentoConfig)atendimentoConfig).Id == 0)
                    dao.incluir((AtendimentoConfig)atendimentoConfig);
                else
                    dao.alterar((AtendimentoConfig)atendimentoConfig);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((AtendimentoConfig)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Atendimento Config não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new AtendimentoConfigDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar AtendimentoConfig!" + e.Message);
            }

        }

        private Boolean valida(AtendimentoConfig atendimentoConfig)
        {
            if (string.IsNullOrWhiteSpace(atendimentoConfig.PortaApi))
            {
                throw new Exception("O campo \"Porta API\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(atendimentoConfig.IpServidor))
            {
                throw new Exception("O campo \"IP Servidor\" é obrigatório!");
            }
            return true;
        }

       
    }
}