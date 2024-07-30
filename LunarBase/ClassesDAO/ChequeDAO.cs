using LunarBase.Classes;
using LunarBase.ConexaoBD;

namespace LunarBase.ClassesDAO
{
    public class ChequeDAO : BaseDAO
    {
        public IList<Cheque> selecionarChequesPorVenda(int idVenda)
        {
            Session = Conexao.GetSession();
            String sql = "FROM Cheque as Tabela WHERE Tabela.FlagExcluido <> true and Tabela.Venda = " + idVenda;
            IList<Cheque> retorno = Session.CreateQuery(sql).List<Cheque>();
            return retorno;
        }
    }
}
