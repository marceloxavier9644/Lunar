using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class OrdemServicoExameDAO : BaseDAO
    {
        public IList<OrdemServicoExame> selecionarExamesPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM OrdemServicoExame as Tabela WHERE Tabela.FlagExcluido <> true and " +
                         "Tabela.OrdemServico = " + idOrdemServico;
            IList<OrdemServicoExame> retorno = Session.CreateQuery(sql).List<OrdemServicoExame>();
            return retorno;
        }
    }
}
