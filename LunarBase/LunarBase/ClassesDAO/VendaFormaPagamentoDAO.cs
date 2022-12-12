using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class VendaFormaPagamentoDAO : BaseDAO
    {
        public IList<VendaFormaPagamento> selecionarVendaFormaPagamentoPorVenda(int idVenda)
        {
            Session = Conexao.GetSession();
            String sql = "FROM VendaFormaPagamento as Tabela WHERE Tabela.Venda = "+ idVenda + " and Tabela.FlagExcluido <> true";
            IList<VendaFormaPagamento> retorno = Session.CreateQuery(sql).List<VendaFormaPagamento>();
            return retorno;
        }
    }
}
