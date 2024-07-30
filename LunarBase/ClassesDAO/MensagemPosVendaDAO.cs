using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class MensagemPosVendaDAO : BaseDAO
    {
        public IList<MensagemPosVenda> selecionarTodasMensagensNaoEnviadasPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM MensagemPosVenda as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.FlagEnviada <> true and Tabela.DataAgendamento between '" +dataInicial+"' and '"+dataFinal+"'" +
                         " order by Tabela.DataAgendamento";
            IList<MensagemPosVenda> retorno = Session.CreateQuery(sql).List<MensagemPosVenda>();
            return retorno;
        }

        public IList<MensagemPosVenda> selecionarTodasMensagensEnviadasPorPeriodo(string dataInicial, string dataFinal)
        {
            Session = Conexao.GetSession();
            String sql = "FROM MensagemPosVenda as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.FlagEnviada = true and Tabela.DataAgendamento between '" + dataInicial + "' and '" + dataFinal + "'" +
                         " order by Tabela.DataAgendamento";
            IList<MensagemPosVenda> retorno = Session.CreateQuery(sql).List<MensagemPosVenda>();
            return retorno;
        }
    }
}
