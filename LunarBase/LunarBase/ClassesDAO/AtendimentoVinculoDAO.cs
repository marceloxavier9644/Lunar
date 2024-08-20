using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class AtendimentoVinculoDAO : BaseDAO
    {
        public IList<AtendimentoVinculo> selecionarVinculosPorAtendimento(int idAtendimento)
        {
            Session = Conexao.GetSession();
            String sql = "FROM AtendimentoVinculo as Tabela WHERE Tabela.AtendimentoId = '" + idAtendimento + "' and Tabela.FlagExcluido <> true";
            IList<AtendimentoVinculo> retorno = Session.CreateQuery(sql).List<AtendimentoVinculo>();
            return retorno;
        }
    }
}
