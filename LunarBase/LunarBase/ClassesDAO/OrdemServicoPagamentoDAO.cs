using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class OrdemServicoPagamentoDAO : BaseDAO
    {
        public IList<OrdemServicoPagamento> selecionarPagamentoPorOrdemServico(int idOrdemServico)
        {
            Session = Conexao.GetSession();
            String sql = "FROM OrdemServicoPagamento as Tabela WHERE Tabela.OrdemServico = " + idOrdemServico + " and Tabela.FlagExcluido <> true";
            IList<OrdemServicoPagamento> retorno = Session.CreateQuery(sql).List<OrdemServicoPagamento>();
            return retorno;
        }
    }
}
