using LunarBase.Classes;
using LunarBase.ClassesDAO;
using LunarBase.Interface;

namespace LunarBase.ClassesBO
{
    public class MensagemPosVendaBO : BO
    {
        private MensagemPosVendaDAO dao;

        public MensagemPosVendaBO()
        {
            dao = new MensagemPosVendaDAO();
        }

        public void salvar(ObjetoPadrao mensagemPosVenda)
        {
            Boolean excluido = mensagemPosVenda.FlagExcluido;

            if (valida((MensagemPosVenda)mensagemPosVenda))
            {
                if (((MensagemPosVenda)mensagemPosVenda).Id == 0)
                    dao.incluir((MensagemPosVenda)mensagemPosVenda);
                else
                    dao.alterar((MensagemPosVenda)mensagemPosVenda);
            }
        }

        public ObjetoPadrao selecionar(ObjetoPadrao objeto)
        {
            try
            {
                objeto = dao.Selecionar(objeto, ((MensagemPosVenda)objeto).Id);
                if (objeto.FlagExcluido == true)
                {
                    throw new Exception();
                }
                return objeto;
            }
            catch
            {
                throw new Exception("Mensagem Pos Venda não encontrado!");
            }

        }
        public IList<ObjetoPadrao> selecionarTodos(string Tabela)
        {
            dao = new MensagemPosVendaDAO();
            try
            {
                return dao.SelecionarTodos(Tabela);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar Mensagem Pos Venda!" + e.Message);
            }

        }

        private Boolean valida(MensagemPosVenda mensagemPosVenda)
        {
            if (string.IsNullOrWhiteSpace(mensagemPosVenda.DataAgendamento.ToShortDateString()))
            {
                throw new Exception("O campo \"Data Agendamento\" é obrigatório!");
            }
            return true;
        }

        //public IList<MensagemPosVenda> selecionarContaPagarPorSql(string sql)
        //{
        //    try
        //    {
        //        return dao.selecionarMensagemPosVendaPorSql(sql);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Falha ao selecionar Mensagem Pos Venda! Erro: " + e.Message);
        //    }
        //}
    }
}
