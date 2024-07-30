using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class OrdemServicoServicoDAO : BaseDAO
    {
        public IList<OrdemServicoServico> selecionarServicosPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM OrdemServicoServico as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.OrdemServico = " + idOrdemServico;
            IList<OrdemServicoServico> retorno = Session.CreateQuery(sql).List<OrdemServicoServico>();
            return retorno;
        }
    }
}
